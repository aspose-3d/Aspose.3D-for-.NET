using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aspose.ThreeD;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Aspose.App.Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Aspose.ThreeD.Shading;
using Aspose.ThreeD.Utilities;
using Aspose.ThreeD.Apps.Algorithms.Repairing;
using System.Collections;
using FileIO = System.IO.File;
using Aspose.App.Api.Services;
using Microsoft.Extensions.Logging;
using System.Transactions;
using Aspose.ThreeD.Entities;
using Aspose.App.Api.Utils;

namespace Aspose.App.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/repairing")]
    [EnableCors("any")]
    [ApiController]
    public class Aspose3DRepairingController : AsposeAppControllerBase<AppMetaData>
    {

        /// <summary>
        /// Location of the intermediate file of model analyzing.
        /// </summary>
        private static readonly FileNameEntry AnalyzeResult = new FileNameEntry("AnalyzeResult");
        /// <summary>
        /// Location of repaired file
        /// </summary>
        private static readonly FileNameEntry RepairedFile = new FileNameEntry("RepairedFile");
        /// <summary>
        /// Location for storing the A3DW file used for browser side rendering
        /// </summary>
        private static readonly FileNameEntry ReviewFile = new FileNameEntry("ReviewFile");
        

        private AppOperationKind OpReview;
        private AppOperationKind OpUpload;
        private AppOperationKind OpRepair;

        public Aspose3DRepairingController(
            StorageService storageService,
            MeasurementService measurementService,
            ILoggerFactory loggerFactory,
            StatsService statsService
            )
            :base(measurementService, storageService, loggerFactory, ThreeDApp.Repairing, statsService)
        {
            _logger = loggerFactory.CreateLogger<Aspose3DRepairingController>();

            OpReview = CreateOperationKind("review");
            OpRepair = CreateOperationKind("repair");
            OpUpload = CreateOperationKind("upload");
        }

        [HttpPost("upload")]
        public ResultObject UploadFile([FromForm] IFormCollection formData)
        {
            var files = formData.Files;
            if (files == null || files.Count == 0)
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
                }
                catch(Exception e)
                {
                    return new ResultObject
                    {
                        state = "Fail",
                        resultObject = "Server error",
                        sessionId = fileNames.Id
                    };
                }


                try
                {
                    HashSet<string> s = DiagnoseResult(fileNames);
                    _logger.LogInformation("File analyzed, id = " + fileNames.Id);
                    return new ResultObject
                    {
                        state = "Success",
                        types = s,
                        sessionId = fileNames.Id,
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

        [HttpGet("result/{sessionId}")]
        public IActionResult DownloadResult(string type,string resultType,string sessionId)
        {
            var outputFormat = GetFileFormat(type);
            if (outputFormat == null)
                return NotFound();
            var fileNames = _storageRepository.ParseNames(sessionId);
            if (fileNames == null || !FileIO.Exists(fileNames[AnalyzeResult]))
            {
                return NotFound();
            }

            return OpRepair.Measure<IActionResult>(fileNames.Id, () =>
            {

                if (!string.IsNullOrEmpty(resultType))
                {
                    var issuesToRepair = ParseIssues(resultType);
                    using (var fs = FileIO.OpenRead(fileNames[AnalyzeResult]))
                    {
                        SceneDiagnoser sd;
                        try
                        {
                            sd = SceneDiagnoser.LoadFromStream(fs);
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, "Failed to restore the diagnosing result, session id = {0}", sessionId);
                            OperationFailed(e);
                            return this.Problem("Internal server error");
                        }
                        Mesh result;
                        try
                        {
                            result = sd.Repair(issuesToRepair);
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, "Failed to repair the input mesh, session id = {0}", sessionId);
                            OperationFailed(e);
                            return this.Problem("Internal server error");
                        }
                        try
                        {
                            Scene scene = new Scene(result);
                            scene.Save(fileNames[RepairedFile], outputFormat);
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, "Failed to save the repaired result, session id = {0}", sessionId);
                            OperationFailed(e);
                            return this.Problem("Internal server error");
                        }
                    }
                }
                else
                {
                    //No issues were selected, just make a direct conversion.
                    try
                    {
                        Scene scene = OpenInput(fileNames);
                        scene.Save(fileNames[RepairedFile], outputFormat);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Failed to save to the required type, session id = {0}", sessionId);
                        OperationFailed(e);
                        return this.Problem("Internal server error");
                    }
                }
                if (!FileIO.Exists(fileNames[RepairedFile]))
                    return NotFound();
                try
                {
                    var path = fileNames[RepairedFile];
                    string downfile = Guid.NewGuid().ToString();
                    return PhysicalFile(path, "application/octet-stream", downfile + "." + type);
                }
                catch(Exception e)
                {
                    OperationFailed(e);
                    return NotFound();
                }
            });

        }

        private static List<IssueType> ParseIssues(string resultType)
        {
            List<IssueType> issuesToRepair = new List<IssueType>();
            string[] res = resultType.Split(",");
            foreach (var strRes in res)
            {
                switch (strRes)
                {
                    case "4":
                        issuesToRepair.Add(IssueType.PartHasHoles);
                        break;

                    case "3":
                        issuesToRepair.Add(IssueType.PartHasNoNormal);
                        break;
                    case "6":
                        issuesToRepair.Add(IssueType.PartHasNoThickness);
                        break;
                    case "5":
                        issuesToRepair.Add(IssueType.PartHasReversedNormals);
                        break;
                    case "2":
                        issuesToRepair.Add(IssueType.SceneIsNotCentered);
                        break;
                    case "1":
                        issuesToRepair.Add(IssueType.SceneIsNotMerged);
                        break;
                }
            }
            return issuesToRepair;
        }

        private FileFormat GetFileFormat(string ext)
        {
            switch(ext)
            {
                case "stl":return FileFormat.STLASCII;
                case "amf":return FileFormat.AMF;
                case "obj":return FileFormat.WavefrontOBJ;
                default:return null;
            }
        }
        private Scene OpenInput(StorageFileNames fileNames)
        {
            var ctx = CreateLoadContext(fileNames);
            return ctx.LoadScene();
        }

        private HashSet<string> DiagnoseResult(StorageFileNames fileNames)
        {
            HashSet<string> typeList = new HashSet<string>();
            Scene scene = OpenInput(fileNames);
            //create scene diagnoser
            var sd = SceneDiagnoser.LoadFromScene(scene);
            //print the problems we just found
            if (sd.Issues.Length == 0)
            {
               //Console.WriteLine("No errors found for this scene");
            }
            else
            {
                //Console.WriteLine($"{sd.Issues.Length} errors found for this scene:");
                foreach (var issue in sd.Issues)
                {
                    typeList.Add($"{issue.IssueType}");
                    //Console.WriteLine($"\t{issue.IssueType}");

                }
            }
            using (var fs = new FileStream(fileNames[AnalyzeResult], FileMode.Create))
            {
                sd.SaveToStream(fs);
            }
            //save the mesh and delta mesh for client side review:
            Scene review = new Scene();

            //review scene has the merged mesh with a default material(grey color)
            review.RootNode.CreateChildNode(sd.Mesh).Material = new LambertMaterial()
            {
                DiffuseColor = new Vector3(0.5, 0.5, 0.5)
            };
            //delta meshes that points to the problem area, and highlighted in red color using red material
            var errorMaterial = new LambertMaterial() { DiffuseColor = new Vector3(1, 0, 0) };
            foreach (var issue in sd.Issues)
            {
                if (issue.DeltaMesh != null)
                {
                    var node = review.RootNode.CreateChildNode(issue.IssueType.ToString(), issue.DeltaMesh);
                    node.Material = errorMaterial;
                }
            }
            review.Save(fileNames[ReviewFile], FileFormat.Aspose3DWeb);
            return typeList;
        }

        [HttpGet("review/{sessionId}")]
        public IActionResult DownloadReviewFile(string sessionId) 
        {
            var fileNames = _storageRepository.ParseNames(sessionId);
            if (fileNames == null || FileIO.Exists(fileNames[ReviewFile]) == false)
            {
                return NotFound();
            }
            return OpReview.Measure(fileNames.Id, () =>
            {
                var path = fileNames[ReviewFile];
                return PhysicalFile(path, "application/octet-stream", "review.a3dw");
            });
        }
    }
}