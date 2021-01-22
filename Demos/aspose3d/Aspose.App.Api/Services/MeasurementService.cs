using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aspose.ThreeD;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using Aspose.App.Api.Utils;

namespace Aspose.App.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AppMeasurement
    {
        class TimedOperation : IComparable<TimedOperation>
        {
            public int Duration { get; }
            public AppOperation Operation { get; }
            public TimedOperation(AppOperation op)
            {
                Duration = (int)op.Duration.TotalMilliseconds;
                Operation = op;
            }

            public int CompareTo(TimedOperation other)
            {
                return Duration.CompareTo(other.Duration);
            }
            public override string ToString()
            {
                return $"{Duration}ms";
            }
        }
        private const int MaxErrorOperations = 100; 
        private const int MaxProlongOperations = 5;
        private ThreeDApp app;
        private HashSet<AppOperation> operations = new HashSet<AppOperation>();
        private LinkedList<AppOperation> errorOperations = new LinkedList<AppOperation>();
        private List<TimedOperation> prolongOperations = new List<TimedOperation>();
        private Dictionary<string, AppOperationKind> kinds = new Dictionary<string, AppOperationKind>();
        private AppOperation lastOperation;
        private int successedOperations;
        private int failedOperations;
        private ThreadLocal<AppOperation> currentOperation = new ThreadLocal<AppOperation>();
        public int SuccessedOperations => successedOperations;
        public int FailedOperations => failedOperations;
        public AppOperation LastOperation => lastOperation;

        public AppOperation CurrentOperation => currentOperation.Value;
        private StatsService statsService;


        public AppMeasurement(ThreeDApp app, StatsService statsService)
        {
            this.app = app;
            this.statsService = statsService;
        }

        public AppOperationKind CreateOperationKind(string operation)
        {
            lock (kinds)
            {
                AppOperationKind kind;
                if (kinds.TryGetValue(operation, out kind))
                    return kind;
                var ret = new AppOperationKind(this, operation);
                kinds.Add(operation, ret);
                return ret;
            }
        }
        public AppOperationKind[] GetOperationKinds()
        {
            lock(kinds)
            {
                var ret = new AppOperationKind[kinds.Count];
                int i = 0;
                foreach(var entry in kinds)
                {
                    ret[i++] = entry.Value;
                }
                return ret;
            }
        }

        public AppOperation Begin(AppOperationKind operationKind, string sessionId)
        {
            var ret = new AppOperation(operationKind, sessionId, Thread.CurrentThread, app);
            lock(operations)
            {
                operations.Add(ret);
                lastOperation = ret;
            }
            currentOperation.Value = ret;
            return ret;
        }
        public AppOperation[] GetActiveOperations()
        {
            lock(operations)
            {
                return operations.ToArray();
            }
        }
        public void Done(AppOperation op)
        {
            if (op.State != OperationState.Running)
                return;
            lock(operations)
            {
                operations.Remove(op);
            }
            op.Stop(false);
            currentOperation.Value = null;
            Interlocked.Increment(ref successedOperations);
            //record long operations
            lock(prolongOperations)
            {
                var to = new TimedOperation(op);
                if (prolongOperations.Count > 0 && to.Duration <= prolongOperations[0].Duration)
                    return;
                int i = prolongOperations.BinarySearch(to);
                if (i < 0)
                {
                    i = ~i;
                    if(i == prolongOperations.Count)
                        prolongOperations.Add(to);
                    else
                        prolongOperations.Insert(i, to);
                }
                else
                    prolongOperations.Insert(i, to);
                if(prolongOperations.Count > MaxProlongOperations)
                {
                    prolongOperations.RemoveAt(0);
                }
            }
        }
        public void Error(AppOperation op, Exception ex)
        {
            if (op.State != OperationState.Running)
                return;
            lock(operations)
            {
                operations.Remove(op);
            }
            op.Stop(true);
            currentOperation.Value = null;
            op.Exception = ex;
            Interlocked.Increment(ref failedOperations);
            //add the op to the error operation list 
            lock (errorOperations)
            {
                errorOperations.AddLast(op);
                if (errorOperations.Count > MaxErrorOperations)
                    errorOperations.RemoveFirst();
            }
            //record to database
            statsService.LogFailedTask(op, ErrorReason.Exception);
        }
        public AppOperation[] GetProlongOperations()
        {
            lock(prolongOperations)
            {
                AppOperation[] ret = new AppOperation[prolongOperations.Count];
                for (int i = 0; i < ret.Length; i++)
                    ret[i] = prolongOperations[i].Operation;
                return ret;
            }
        }
        public AppOperation[] GetErrorOperations()
        {
            lock(errorOperations)
            {
                return errorOperations.ToArray();
            }
        }
    }
    public enum OperationState
    {
        Pending,
        Running,
        Done,
        Error
    }
    public class AppOperationKind
    {
        private string name;
        private int counter;
        private AppMeasurement measurement;
        public int Counter => counter;
        public AppOperationKind(AppMeasurement measurement, string name)
        {
            this.measurement = measurement;
            this.name = name;
        }
        public T Measure<T>(string sessionId, Func<T> op)
        {
            Interlocked.Increment(ref counter);
            var appOp = measurement.Begin(this, sessionId);
            try
            {
                var ret = op();
                measurement.Done(appOp);
                return ret;
            }
            catch(Exception e)
            {
                measurement.Error(appOp, e);
                throw e;
            }
        }
        public override string ToString()
        {
            return name;
        }
    }


    public class AppOperation
    {
        /// <summary>
        /// 该操作是否已经记录到数据库中了
        /// </summary>
        public bool DbLogged { get; set; }
        public AppOperationKind Operation { get; }
        public string SessionId { get; }
        public DateTime Start { get; }
        public DateTime End { get; private set; }
        public OperationState State { get; private set; }
        public Exception Exception { get; set; }
        public Thread Thread { get; } 
        public ThreeDApp App { get; }
        public AppOperation(AppOperationKind operation, string sessionId, Thread thread, ThreeDApp app)
        {
            this.Operation = operation;
            this.SessionId = sessionId;
            this.Thread = thread;
            Start = DateTime.Now;
            End = DateTime.Now;
            State = OperationState.Running;
            this.App = app;
        }
        public TimeSpan Duration
        {
            get
            {
                switch(State)
                {
                    case OperationState.Running:
                        return DateTime.Now - Start;  
                    case OperationState.Done:
                    case OperationState.Error:
                        return End - Start;
                    case OperationState.Pending:
                    default:
                        return TimeSpan.FromSeconds(0);
                }
            }
        }
        public void Stop(bool error)
        {
            End = DateTime.Now;
            State = error ? OperationState.Error: OperationState.Done;
        }
    }

    public class MeasurementService
    {
        private string workingDirectory;
        private Dictionary<ThreeDApp, AppMeasurement> apps = new Dictionary<ThreeDApp, AppMeasurement>();
        private StatsService statsService;


        public MeasurementService(
            IConfiguration configuration,
            StatsService statsService
            )
        {
            workingDirectory = configuration["SystemConfig:WorkingDirectory"];
            this.statsService = statsService;
        }
        public AppMeasurement CreateMeasurement(ThreeDApp app)
        {
            lock(apps)
            {
                AppMeasurement ret;
                if(!apps.TryGetValue(app, out ret))
                {
                    ret = new AppMeasurement(app, statsService);
                    apps.Add(app, ret);
                }
                return ret;
            }

        }
        public AppMeasurement[] GetMeasurements()
        {
            lock(apps)
            {
                return apps.Values.ToArray();
            }
        }
        public AppMeasurement GetMeasurement(ThreeDApp app)
        {
            lock(apps)
            {
                AppMeasurement ret;
                apps.TryGetValue(app, out ret);
                return ret;
            }

        }

    }
}
