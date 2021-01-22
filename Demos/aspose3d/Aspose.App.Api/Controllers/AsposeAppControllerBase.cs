using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aspose.ThreeD;
using Aspose.App.Api.Services;
using Aspose.App.Api.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json;
using FileIO = System.IO.File;
using Microsoft.Extensions.Logging;
using Aspose.App.Api.Utils;
using Aspose.ThreeD.Utilities;

namespace Aspose.App.Api.Controllers
{
    public class AppMetaData
    {
        /// <summary>
        /// The original file name of the uploaded files 
        /// </summary>
        public string[] InputFilenames { get; set; }
    }
    public class AsposeAppControllerBase<MetaDataT> : Controller where MetaDataT : AppMetaData, new()
    {
        private ThreeDApp app;
        protected AppMeasurement measurement;
        protected static readonly FileNameEntry SourceFile = new FileNameEntry("SourceFile");
        protected static readonly FileNameEntry MetaFile = new FileNameEntry("MetaFile");
        private const long MaximumInputSize = 200 * 1024 * 1024;
        protected StorageRepository _storageRepository;
        protected ILogger _logger;
        protected StatsService statsService;
        public AsposeAppControllerBase(MeasurementService measurementService,
            StorageService storageService,
            ILoggerFactory loggerFactory,
            ThreeDApp app,
            StatsService statsService
            )
        {
            this.app = app;
            this.statsService = statsService;
            measurement = measurementService.CreateMeasurement(app);
            _storageRepository = storageService.GetRepository(app.ToString().ToLower());


            _logger = loggerFactory.CreateLogger(GetType());
        }
        protected void OperationFailed(Exception e)
        {
            var op = measurement.CurrentOperation;
            if (op == null)
                return;
            measurement.Error(op, e);
        }

        protected AppOperationKind CreateOperationKind(string operationKind)
        {
            return measurement.CreateOperationKind(operationKind);
        }

        /// <summary>
        /// Upload the uploaded files to local disk
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        protected MetaDataT SaveUploadedFiles(StorageFileNames fileNames, IFormFileCollection files)
        {
            var fileId = 0;
            var inputFileNames = new string[files.Count];
            foreach (var file in files)
            {
                if (file.Length > MaximumInputSize)
                    throw new Exception("Uploaded file is too large!");

                inputFileNames[fileId] = Path.GetFileName(file.FileName);
                // Prepare a path in which the result file will be
                // Check directroy already exist or not
                try
                {
                    using (FileStream fs = FileIO.Create(fileNames.GetFileName(SourceFile, fileId)))
                    {
                        file.CopyTo(fs);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to save uploaded file, session id = {0}", fileNames.Id);
                    OperationFailed(e);
                }
                fileId++;
            }
            _logger.LogInformation("Uploaded file to {0}", fileNames.Id);
            var metaData = new MetaDataT() { InputFilenames = inputFileNames };
            SetMetaData(fileNames, metaData);
            return metaData;
        }
        protected MetaDataT GetMetaData(StorageFileNames fileNames)
        {
            var fileName = fileNames[MetaFile];
            try
            {
                if (FileIO.Exists(fileName))
                {
                    var json = FileIO.ReadAllText(fileName);
                    return JsonSerializer.Deserialize<MetaDataT>(json);
                }
            }
            catch(Exception)
            {
            }
            return new MetaDataT();
        }
        protected void SetMetaData(StorageFileNames fileNames, MetaDataT metaData)
        {
            var json = JsonSerializer.Serialize(metaData);
            FileIO.WriteAllText(fileNames[MetaFile], json);
        }

        /// <summary>
        /// The context of importing uploaded files into scene
        /// This class handles the details of multiple files, and uses the virtual FileSystem
        /// to make sure the dependencies between files will not affect the security of server.
        /// </summary>
        protected class SceneImportContext
        {
            private MetaDataT metaData;
            private FileFormat sourceFormat;
            private StorageFileNames fileNames;
            private int mainFile;
            private ThreeDApp app;

            /// <summary>
            /// Gets the meta data used by current session
            /// </summary>
            public MetaDataT MetaData => metaData;
            /// <summary>
            /// Gets the 3D file format of uploaded file, null means unsupported file
            /// </summary>
            public FileFormat SourceFormat => sourceFormat;
            private StatsService statsService;

            /// <summary>
            /// Return the original file name of the main file from uploaded multiple files.
            /// </summary>
            public string MainFile
            {
                get
                {
                    if (mainFile == -1 || metaData == null || metaData.InputFilenames == null)
                        return null;
                    return metaData.InputFilenames[mainFile];
                }
            }

            /// <summary>
            /// Physical path of the main file from uploaded files.
            /// </summary>
            public string PhysicalMainFile
            {
                get
                {
                    if (metaData == null || metaData.InputFilenames == null)
                        return null;
                    var idx = mainFile == -1 ? 0 : mainFile;
                    return fileNames.GetFileName(AsposeAppControllerBase<MetaDataT>.SourceFile, idx);
                }

            }
            public SceneImportContext(ThreeDApp app, StatsService statsService, StorageFileNames fileNames, MetaDataT metaData, int mainFile, FileFormat sourceFormat)
            {
                this.app = app;
                this.statsService = statsService;
                this.fileNames = fileNames;
                this.metaData = metaData;
                this.sourceFormat = sourceFormat;
                this.mainFile = mainFile;

            }
            /// <summary>
            /// Create a scene from the uploaded files.
            /// </summary>
            /// <returns></returns>
            /// <exception cref="NotSupportedException">Unsupported 3D file format</exception>
            public Scene LoadScene()
            {
                if (SourceFormat == null)
                    throw new NotSupportedException("Unsupported 3D file format");
                var loadOpt = sourceFormat.CreateLoadOptions();
                loadOpt.FileSystem = CreateFileSystem();
                var id = statsService.BeforeOpenSession(app, fileNames.Id);
                Scene scene = new Scene();
                scene.Open(fileNames.GetFileName(SourceFile, mainFile), loadOpt);
                statsService.AfterLoadScene(id);
                return scene;
            }
            private FileSystem CreateFileSystem()
            {
                var ret = new LinkedFileSystem();
                if (metaData.InputFilenames != null)
                {
                    for (int i = 0; i < metaData.InputFilenames.Length; i++)
                    {
                        //Map the local physical file to the logical file name for security.
                        ret.Link(metaData.InputFilenames[i], fileNames.GetFileName(SourceFile, i));
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Create a scene import context instance by the file names
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        protected SceneImportContext CreateLoadContext(StorageFileNames fileNames)
        {
            var metaData = GetMetaData(fileNames);
            int mainFile;
            FileFormat sourceFmt = DetectFormat(fileNames, metaData, out mainFile);
            return new SceneImportContext(app, statsService, fileNames, metaData, mainFile, sourceFmt);
        }

        private FileFormat DetectFormat(StorageFileNames fileNames, MetaDataT metaData, out int mainFile)
        {
            mainFile = -1;
            if (metaData.InputFilenames == null || metaData.InputFilenames.Length == 0)
                return null;
            for(int i = 0; i < metaData.InputFilenames.Length; i++)
            {
                var inputFile = fileNames.GetFileName(SourceFile, i);
                using (var fs = FileIO.OpenRead(inputFile))
                {
                    var sourceFmt = FileFormat.Detect(fs, metaData.InputFilenames[i]);
                    if(sourceFmt != null)
                    {
                        mainFile = i;
                        return sourceFmt;
                    }
                }
            }
            return null;
        }

    }
}
