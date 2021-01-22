using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aspose.ThreeD;
using Aspose.App.Api.Services;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Security.Permissions;
using Aspose.App.Api.Utils;
using Microsoft.Extensions.Configuration;

namespace Aspose.App.Api.Controllers
{
    [DataContract]
    public class MeasurementInfo
    {
        [DataMember]
        public int Successed { get; set; }
        [DataMember]
        public int Failed { get; set; }
        [DataMember]
        public OperationInfo[] ErrorOperations { get; set; }
        [DataMember]
        public OperationInfo[] ProlongOperations { get; set; }
        [DataMember]
        public OperationInfo[] ActiveOperations { get; set; }
        [DataMember]
        public OperationInfo LastOperation { get; set; }

        [DataMember]
        public Dictionary<string, int> Counters { get; set; }
    }
    [DataContract]
    public class OperationInfo
    {
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public double Duration { get; set; }
        [DataMember]
        public OperationState State { get; set; }
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string Operation { get; set; }

        public OperationInfo()
        {

        }
        public static OperationInfo From(AppOperation op)
        {
            if (op == null)
                return null;
            return new OperationInfo()
            {
                StartTime = op.Start,
                Duration = op.Duration.TotalSeconds,
                State = op.State,
                SessionId = op.SessionId,
                Operation = op.Operation.ToString()
            };
        }
    }
    [DataContract]
    public class ServerInfo
    {
        [DataMember]
        public DateTime BuiltTime { get; set; }
        [DataMember]
        public DateTime Aspose3DBuiltTime { get; set; }
        [DataMember]
        public string Uptime { get; set; }
        [DataMember]
        public DateTime StartupTime { get; set; }
        [DataMember]
        public DateTime ServerTime { get; set; }
        [DataMember]
        public double WorkingSet { get; set; }

        [DataMember]
        public OperationInfo[] ActiveTasks { get; set; }
    }

    [Route("api/state")]
    [ApiController]
    public class StateController : ControllerBase
    {

        private MeasurementService measurementService;
        private string statsKey;
        public StateController(MeasurementService measurementService, 
            IConfiguration configuration)
        {
            this.measurementService = measurementService;
            statsKey = configuration["SystemConfig:StatsKey"];
        }
        [HttpGet]
        public ActionResult<ServerInfo> GetServerInfo(string key)
        {
            if (string.Compare(key, statsKey) != 0)
                return BadRequest();
            var ret = new ServerInfo();
            var file = new FileInfo(typeof(StateController).Assembly.Location);
            ret.BuiltTime = file.LastWriteTime;
            file = new FileInfo(typeof(Aspose.ThreeD.Scene).Assembly.Location);
            ret.Aspose3DBuiltTime = file.LastWriteTime;

            var process = Process.GetCurrentProcess();
            ret.ServerTime = DateTime.Now;
            ret.StartupTime = process.StartTime;
            ret.Uptime = (ret.ServerTime - ret.StartupTime).ToString();
            ret.WorkingSet = process.WorkingSet64 / 1024.0 / 1024.0;
            List<OperationInfo> activeTasks = new List<OperationInfo>();
            var measurements = measurementService.GetMeasurements();
            foreach(var measurement in measurements)
            {
                var ops = measurement.GetActiveOperations();
                for(int i = 0; i < ops.Length; i++)
                {
                    activeTasks.Add(OperationInfo.From(ops[i]));
                }
            }
            ret.ActiveTasks = activeTasks.ToArray();
            return new ActionResult<ServerInfo>(ret);
        }

        ThreeDApp? ParseApp(string app)
        {
            if (app == null)
                return null;
            switch (app)
            {
                case "conversion":return ThreeDApp.Conversion;
                case "repairing":return ThreeDApp.Repairing;
                case "measurement":return ThreeDApp.Measurement;
                case "viewer":return ThreeDApp.Viewer;
            }
            return null;
        }

        // GET api/values
        [HttpGet("app/{appName}/measurement")]
        public ActionResult<MeasurementInfo> GetMeasurement(string appName, string key)
        {
            if (string.Compare(key, statsKey) != 0)
                return BadRequest();
            var ret = new MeasurementInfo();
            var app = ParseApp(appName);
            if(app == null)
                return NotFound();
            var measurement = measurementService.GetMeasurement(app.Value);
            if (measurement == null)
                return NotFound();
            var active = measurement.GetActiveOperations();
            var error = measurement.GetErrorOperations();
            ret.Successed = measurement.SuccessedOperations;
            ret.Failed = measurement.FailedOperations;
            ret.ActiveOperations = ToOperationInfos(active);
            ret.ErrorOperations = ToOperationInfos(error);
            ret.ProlongOperations = ToOperationInfos(measurement.GetProlongOperations());
            ret.LastOperation = OperationInfo.From(measurement.LastOperation);
            ret.Counters = new Dictionary<string, int>();

            foreach(var kind in measurement.GetOperationKinds())
            {
                ret.Counters[kind.ToString()] = kind.Counter;
            }


            return new ActionResult<MeasurementInfo>(ret);
        }

        private OperationInfo[] ToOperationInfos(AppOperation[] ops)
        {
            var ret = new OperationInfo[ops.Length];
            for(int i = 0; i <ret.Length; i++)
            {
                ret[i] = OperationInfo.From(ops[i]);
            }
            return ret;
        }
    }
}
