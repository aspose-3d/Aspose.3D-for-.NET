using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aspose.App.Api.Utils
{
    /// <summary>
    /// File stream decorator with limited output size.
    /// </summary>
    class RemoteOpException : Exception
    {
        public RemoteOpException(string msg)
            :base(msg)
        {

        }
    }
}
