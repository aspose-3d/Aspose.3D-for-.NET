using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using FileIO = System.IO.File;
using Aspose.App.Api.Services;
using Microsoft.Extensions.Logging;
using Aspose.App.Api.Models;
using Aspose.ThreeD.Formats;
using Aspose.App.Api.Utils;

namespace Aspose.App.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/viewer")]
    [EnableCors("any")]
    [ApiController]
    public class Aspose3DViewerController : AsposeAppControllerBase<AppMetaData>
    {

        private AppOperationKind OpUpload;
        private AppOperationKind OpReview;
        private readonly AppOperationKind OpDownload;
        private static readonly FileNameEntry ReviewFile = new FileNameEntry("ReviewFile");

        public Aspose3DViewerController(StorageService storageService,
            MeasurementService measurementService,
            StatsService statsService,
            ILoggerFactory loggerFactory) : base(measurementService, storageService, loggerFactory, ThreeDApp.Viewer, statsService)
        {

            OpUpload = CreateOperationKind("upload");
            OpReview = CreateOperationKind("review");
            OpDownload = CreateOperationKind("download");
        }

        [HttpPost("upload")]
        public ResultObject UploadFile([FromForm] IFormCollection formData)
        {
            IFormFileCollection files = formData.Files;
            int fileCount = files.Count;
            if (files == null || fileCount == 0)
            {
                return new ResultObject
                {
                    state = "Fail",
                    resultObject = "files==null"
                };
            }
             var file = files[0];
             var originalFileName = Path.GetFileName(file.FileName);
             var fileNames = _storageRepository.NewNames();

            return OpUpload.Measure(fileNames.Id, () =>
            {
                try
                {
                    SaveUploadedFiles(fileNames, files);
                    var ctx = CreateLoadContext(fileNames);
                    var scene = ctx.LoadScene();
                    var opt = new A3DWSaveOptions();
                    opt.ExportMetaData = true;
                    scene.AssetInfo.SetProperty("OriginalFileName", ctx.MainFile);
                    scene.Save(fileNames[ReviewFile], opt);
                    _logger.LogInformation("File analyzed, id = " + fileNames.Id);
                    return new ResultObject
                    {
                        state = "Success",
                        sessionId = fileNames.Id
                    };
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to diagnose result from input file, session id = {0}", fileNames.Id);
                    OperationFailed(e);
                    return new ResultObject
                    {
                        state = "Fail",
                        resultObject = "Server error",
                        sessionId = fileNames.Id
                    };
                }
            });
        }


        [HttpGet("review/{sessionId}")]
        public IActionResult DownloadFile(string sessionId)
        {
            var fileNames = _storageRepository.ParseNames(sessionId);
            if (fileNames == null || FileIO.Exists(fileNames[ReviewFile]) == false)
            {
                return NotFound();
            }
            return OpReview.Measure(fileNames.Id, () =>
            {
                var reviewFile = fileNames[ReviewFile];
                return PhysicalFile(reviewFile, "application/octet-stream", "review.a3dw");
            });
        }

        [HttpGet("download/{sessionId}")]
        public IActionResult DownloadOriginalFile(string sessionId)
        {
            var fileNames = _storageRepository.ParseNames(sessionId);
            if (fileNames == null)
                return NotFound();
            return OpDownload.Measure<IActionResult>(fileNames.Id, () =>
            {
                var ctx = CreateLoadContext(fileNames);
                var path = ctx.PhysicalMainFile;
                if (FileIO.Exists(path) == false)
                {
                    return NotFound();
                }
                return PhysicalFile(path, "application/octet-stream", ctx.MainFile);
            });
        }
        [HttpGet("verify/{sessionId}")]
        public ResultObject verifyFile(string sessionId)
        {
            var fileNames = _storageRepository.ParseNames(sessionId);
            if (fileNames == null || FileIO.Exists(fileNames[ReviewFile]) == false)
            {
                return new ResultObject
                {
                    state = "Fail",
                    resultObject = "files==null"
                };
            }
            else
            {
                return new ResultObject
                {
                    state = "Success",
                    sessionId = fileNames.Id
                };
            }
        }
    }
}
