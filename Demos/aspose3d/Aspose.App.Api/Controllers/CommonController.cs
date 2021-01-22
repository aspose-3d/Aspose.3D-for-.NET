using Aspose.App.Api.Services;
using Aspose.App.Api.Utils;
using Aspose.App.Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Aspose.App.Api.Controllers
{
    /// <summary>
    /// Shared infrustructures of all app controllers.
    /// </summary>
    [Produces("application/json")]
    [Route("api")]
    [EnableCors("any")]
    [ApiController]
    public class CommonController : Controller
    {
        private AppMeasurement error2forum;
        private StorageService storageService;
        private ConholdateService conholdateService;
        private ILogger logger;
        public CommonController(
            MeasurementService measurementService,
            ConholdateService conholdateService,
            StorageService storageService,
            ILoggerFactory loggerFactory
            )
        {
            this.storageService = storageService;
            this.conholdateService = conholdateService;
            error2forum = measurementService.CreateMeasurement(ThreeDApp.Common);
            logger = loggerFactory.CreateLogger<CommonController>();
        }


        private string FormatAppName(string app)
        {
            switch(app)
            {
                case "conversion":
                    return "Conversion";
                case "viewer":
                    return "Viewer";
                case "repairing":
                    return "Model repairing";
                default:
                    return app;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("error2forum")]
        public async Task<ActionResult<ResultModel>> Error2Forum([FromBody]Error2ForumRequest req)
        {
            var repo = storageService.GetRepository(req.Application);
            if (repo == null)
                return new ActionResult<ResultModel>(ResultModel.Error("Invalid application", 400));
            var fileNames = repo.ParseNames(req.Session);
            if (fileNames == null || !Directory.Exists(fileNames.Directory))
                return new ActionResult<ResultModel>(ResultModel.Error("Invalid session id", 400));
            var now = DateTime.Now.ToString();
            var appName = FormatAppName(req.Application);
            const string username = "aspose.3d.app";
            var title = $"{appName} issue - {now}";
            var content = $"<p><b>Session :</b> {req.Session}</p><p><b>Date :</b> {now}</p>";
            try
            {
                var url = await conholdateService.PostToForum(title, req.Email, username, content);
                return new ActionResult<ResultModel>(ResultModel.Ok(url));
            }
            catch(Exception e)
            {

                logger.LogError("Failed to post error to forum", e);
            }
            return new ActionResult<ResultModel>(ResultModel.Error("Something bad happened", 500));


        }
    }
}
