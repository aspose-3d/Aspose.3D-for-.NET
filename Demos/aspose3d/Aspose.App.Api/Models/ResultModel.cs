using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspose.App.Api.Models
{
    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public string sessionId { get; set; }

        /// <summary>
        /// Successed
        /// </summary>
        /// <returns></returns>
        public static ResultModel Ok()
        {
            return new ResultModel { Data = null, Message = null, IsSuccess = true, Code = 200 };
        }
        /// <summary>
        /// Operation successed
        /// </summary>
        /// <param name="data">Returned data</param>
        /// <returns></returns>
        public static ResultModel Ok(object data)
        {
            return new ResultModel { Data = data, Message = null, IsSuccess = true, Code = 200 };
        }
        /// <summary>
        /// Operation failed.
        /// </summary>
        /// <param name="str">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns></returns>
        public static ResultModel Error(string str, int code)
        {
            return new ResultModel { Data = null, Message = str, IsSuccess = false, Code = code };
        }
        /// <summary>
        /// Operation failed
        /// </summary>
        /// <param name="sessionId">The related session id</param>
        /// <param name="str">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns></returns>
        public static ResultModel Error(string sessionId, string str, int code)
        {
            return new ResultModel { sessionId = sessionId, Message = str, IsSuccess = false, Code = code };
        }
    }
}
