using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aspose.ThreeD;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Collections;
using FileIO = System.IO.File;
using Aspose.App.Api.Services;
using Microsoft.Extensions.Logging;
using Aspose.App.Api.Models;
using Aspose.ThreeD.Shading;

using Aspose.ThreeD.Apps.Algorithms.Repairing;
using Aspose.ThreeD.Utilities;
using Aspose.ThreeD.Formats;
using Aspose.App.Api.Utils;

namespace Aspose.App.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/measurement")]
    [EnableCors("any")]
    [ApiController]
    public class Aspose3DMeasurementController : AsposeAppControllerBase<AppMetaData>
    {


        private AppOperationKind OpUpload;
        private AppOperationKind OpReview;
        private readonly AppOperationKind OpDownload;
        private static readonly FileNameEntry ReviewFile = new FileNameEntry("ReviewFile");

        public Aspose3DMeasurementController(StorageService storageService,
            MeasurementService measurementService,
            StatsService statsService,
            ILoggerFactory loggerFactory) : base(measurementService, storageService, loggerFactory, ThreeDApp.Measurement, statsService)
        {

            OpUpload = CreateOperationKind("upload");
            OpReview = CreateOperationKind("review");
            OpDownload = CreateOperationKind("download");
        }

        [HttpPost("upload")]
        public ResultObject UploadIForm([FromForm] IFormCollection formData)
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
        public IActionResult Fileres(string sessionId)
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
        public IActionResult Download(string sessionId)
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
        public ResultObject VerifyFile(string sessionId)
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
