using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aspose.ThreeD;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Aspose.App.Api.Utils;
using System.Net.Http;

namespace Aspose.App.Api.Services
{
    public enum ErrorReason
    { 
        Exception,
        Prolonged
    }
    /// <summary>
    /// StatsService reports possible issues to stats server for late analyzing.
    /// </summary>
    public class StatsService
    {
        private readonly string statsServer;
        private readonly ILogger logger;
        private readonly HttpClient httpClient;
        private readonly TimeSpan timeout = TimeSpan.FromSeconds(1);

        public StatsService(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory
            )
        {
            logger = loggerFactory.CreateLogger<StatsService>();
            statsServer = configuration["SystemConfig:StatsServer"];
            httpClient = httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(statsServer))
            {
                logger.LogInformation($"Use stats server: {statsServer}");
            }
            else
            {
                logger.LogError("Stats server is not enabled");
                statsServer = null;
            }
        }
        public void LogFailedTask(AppOperation op, ErrorReason reason)
        {
            if (statsServer ==  null)
                return;
            if (op.DbLogged)
                return;
            op.DbLogged = true;
            var fullUrl = $"{statsServer}/stats/aspose3d/error";
            var form = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"sessionId", op.SessionId},
                { "app", ((int)op.App).ToString() },
                { "duration", ((long)op.Duration.TotalMilliseconds).ToString()},
                { "reason", ((int)reason).ToString() }
            });
            try
            {
                var task = httpClient.PostAsync(fullUrl, form);
                Task.WaitAll(new Task[] { task }, timeout);
            }
            catch(Exception e)
            {
                logger.LogError("Failed to forward failed task to stats server", e);
            }
        }

        public int BeforeOpenSession(ThreeDApp app, string sessionId)
        {
            if (statsServer ==  null)
                return -1;
            var fullUrl = $"{statsServer}/stats/aspose3d/task";
            var form = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"sessionId", sessionId},
                { "app", ((int)app).ToString() },
            });
            try
            {
                Func<Task<int>> req = async ()  =>
                {
                    var resp = await httpClient.PostAsync(fullUrl, form);
                    var content = await resp.Content.ReadAsStringAsync();
                    if (int.TryParse(content, out int ret))
                        return ret;
                    else
                        return -1;
                };
                var task = req();
                Task.WaitAll(new Task[] { task }, timeout);
                return task.Result;
            }
            catch(Exception e)
            {
                logger.LogError("Failed to forward audit task to stats server", e);
                return -1;
            }
        }

        public void AfterLoadScene(int id)
        {
            if (statsServer ==  null)
                return;
            if (id == -1)
                return;
            var fullUrl = $"{statsServer}/stats/aspose3d/task/{id}";
            try
            {
                var task = httpClient.DeleteAsync(fullUrl);
                Task.WaitAll(new Task[] { task }, timeout);
            }
            catch(Exception e)
            {
                logger.LogError("Failed to forward audit task to stats server", e);
            }
        }



    }
}
