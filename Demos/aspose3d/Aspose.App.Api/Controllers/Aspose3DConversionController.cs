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
using Aspose.App.Api.Models;
using System.Net.Mail;
using System.Net;
using FileIO = System.IO.File;
using Aspose.App.Api.Services;
using Microsoft.Extensions.Logging;
using Aspose.App.Api.Utils;

namespace Aspose.App.Api.Controllers
{
        public class ConversionMetaData : AppMetaData
        {
            /// <summary>
            /// The file name of converted file
            /// </summary>
            public string OutputFile { get; set; }
        }




    [Produces("application/json")]
    [Route("api/conversion")]
    [EnableCors("any")]
    [ApiController]
    public class Aspose3DConversionController : AsposeAppControllerBase<ConversionMetaData>
    {
        private const long MaximumOutputSize = 200 * 1024 * 1024;

        /// <summary>
        /// Converted file name
        /// </summary>
        public static readonly FileNameEntry OutputFile = new FileNameEntry("OutputFile");

        private IConfiguration _configuration;
        private Dictionary<string, FileFormat> formats = new Dictionary<string, FileFormat>()
        {
            { "FBX", FileFormat.FBX7400ASCII },
            { "OBJ", FileFormat.WavefrontOBJ },
            { "3DS", FileFormat.Discreet3DS},
            { "DRC", FileFormat.Draco},
            { "AMF", FileFormat.AMF },
            { "RVM", FileFormat.RvmBinary},
            { "DAE", FileFormat.Collada},
            { "GLTF",FileFormat.GLTF2 },
            { "GLB", FileFormat.GLTF2_Binary},
            { "PDF", FileFormat.PDF},
            { "HTML",FileFormat.HTML5},
            { "PLY", FileFormat.PLY},
            { "STL", FileFormat.STLBinary},
            { "U3D", FileFormat.Universal3D}
        };

        private readonly AppOperationKind OpUpload;
        private readonly AppOperationKind OpConvert;
        private readonly AppOperationKind OpDownload;
        private readonly AppOperationKind OpEmail;

        public Aspose3DConversionController(IConfiguration configuration,
            StorageService storageService,
            MeasurementService measurementService,
            StatsService statsService,
            ILoggerFactory loggerFactory)
            :base(measurementService, storageService, loggerFactory, ThreeDApp.Conversion, statsService)
        {
            _configuration = configuration;
            this.statsService = statsService;


            OpUpload = CreateOperationKind("upload");
            OpConvert = CreateOperationKind("convert");
            OpDownload = CreateOperationKind("download");
            OpEmail = CreateOperationKind("email");
        }


        [HttpPost("files")]
        public ResultModel UploadIForm([FromForm] IFormCollection formData)
        {
            IFormFileCollection files = formData.Files;
            int fileCount = files.Count;
            if (files == null || fileCount == 0)
            {               
                return ResultModel.Error("file can't be null!", 400);
            }
            var fileNames = _storageRepository.NewNames();
            return OpUpload.Measure(fileNames.Id, () =>
            {
                try
                {
                    SaveUploadedFiles(fileNames, files);
                }
                catch(Exception e)
                {
                    OperationFailed(e);
                    return ResultModel.Error(fileNames.Id, e.Message,400);
                }
                return ResultModel.Ok(fileNames.Id);
            });
        }


        [HttpPost("convertFile")]
        public ResultModel ConvertFile(ConversionDto conversionDto)
        {
            var fileNames = _storageRepository.ParseNames(conversionDto.sessionId);
            if(fileNames == null)
                return ResultModel.Error(fileNames.Id, "Invalid session id", 400);
            return OpConvert.Measure(fileNames.Id, () =>
            {
                FileFormat fmt;
                if (conversionDto.outputType != null && formats.TryGetValue(conversionDto.outputType, out fmt))
                {
                    SceneImportContext importContext = CreateLoadContext(fileNames);
                    //User uploaded an unsupported file format.
                    if(importContext.SourceFormat == null)
                    {
                        _logger.LogError("Failed to detect file type from file {0}", fileNames.Id);
                        return ResultModel.Error(fileNames.Id, "Unsupported input file", 400);
                    }

                    Scene scene;
                    try
                    {
                        scene = importContext.LoadScene();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Failed to open input file {0}", fileNames.Id);
                        OperationFailed(e);
                        return ResultModel.Error(fileNames.Id, "Internal server error", 500);
                    }
                    var originalFile = importContext.MainFile;
                    try
                    {
                        var fileName = fileNames[OutputFile];
                        using (var stream = LimitedStream.CreateFile(fileName, MaximumOutputSize))
                        {
                            scene.Save(stream, fmt);
                        }
                    }
                    catch (Exception e)
                    {
                        var msg = "Internal server error";
                        if (e is ExportException)
                            msg = e.Message;
                        _logger.LogError(e, "Failed to save converted file to {0}", fileNames[SourceFile]);
                        OperationFailed(e);
                        return ResultModel.Error(fileNames.Id, msg, 500);
                    }
                    var newFileName = Path.ChangeExtension(originalFile, fmt.Extension);
                    importContext.MetaData.OutputFile = newFileName;
                    SetMetaData(fileNames, importContext.MetaData);
                    return ResultModel.Ok(true);
                }
                return ResultModel.Error(fileNames.Id, "Output type not found", 400);
            });
        }

        [HttpGet("download/{sessionId}")]
        public IActionResult DownloadFile(string sessionId)
        {
            var fileNames = _storageRepository.ParseNames(sessionId);
            if(fileNames == null)
                return NotFound();
            return OpDownload.Measure<IActionResult>(fileNames.Id, () =>
            {
                string path = fileNames[OutputFile];
                if (FileIO.Exists(path) == false)
                {
                    return NotFound();
                }
                var metaData = GetMetaData(fileNames);
                var outputFile = metaData.OutputFile;
                return PhysicalFile(path, "application/octet-stream", outputFile);
            });
        }

        [HttpGet("sendEmail")]
        public ResultModel SendEmail(string address, string url)
        {
            return OpEmail.Measure(url, () =>
            {
                var uid = _configuration["SystemConfig:uid"];
                var pwd = _configuration["SystemConfig:MailServerPassword"];
                var applicationUrl = _configuration["SystemConfig:applicationUrl"];
                var fromEmail = _configuration["SystemConfig:FromAddress"];
                var MailServer = _configuration["SystemConfig:MailServer"];
                var MailServerPort = _configuration["SystemConfig:MailServerPort"];

                try
                {
                    MailAddress from = new MailAddress(fromEmail);
                    MailAddress to = new MailAddress(address);
                    MailMessage mailMessage = new MailMessage(from, to);
                    string strbody = ReplaceText("Free Converter App for 3D File Formats ", "Your file has been converted successfully", applicationUrl + "/api/conversion/download/" + url);
                    mailMessage.Subject = "Download File";
                    mailMessage.Body = strbody; 
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtpClient = new SmtpClient();

                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Host = MailServer;
                    smtpClient.Port = int.Parse(MailServerPort);
                    smtpClient.EnableSsl = true;

                    smtpClient.Credentials = new NetworkCredential(uid, pwd);
                    smtpClient.Send(mailMessage);

                    _logger.LogInformation("Send file {0} to {1}", url, address);

                    return ResultModel.Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to send email");
                    OperationFailed(e);
                    return ResultModel.Error("send Error", 500);

                }
            });
        }

        private string ReplaceText(String FeatureTitle, string SuccessMessage, string Url)
        {

            string path = string.Empty;
            path = "EmailTemplate/EmailTemplate.html";
            if (path == string.Empty)
            {
                return string.Empty;
            }
            System.IO.StreamReader sr = new System.IO.StreamReader(path);
            string str = string.Empty;
            str = sr.ReadToEnd();
            str = str.Replace("{FeatureTitle}", FeatureTitle);
            str = str.Replace("{SuccessMessage}", SuccessMessage);
            str = str.Replace("{Url}", Url);
            return str;
        }

    }
}
