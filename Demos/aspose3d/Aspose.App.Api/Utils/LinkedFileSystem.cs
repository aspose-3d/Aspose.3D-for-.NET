using Aspose.ThreeD.Formats;
using Aspose.ThreeD.Utilities;
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
    /// Virtual file system remaps to physical files
    /// </summary>
    class LinkedFileSystem : FileSystem
    {
        private Dictionary<string, string> mapping = new Dictionary<string, string>();

        internal void Link(string logicalFile, string physicalFile)
        {
            mapping[logicalFile] = physicalFile;
        }
        private string GetPhysicalFile(string fileName)
        {
            string ret;
            if (mapping.TryGetValue(fileName, out ret))
                return ret;
            return null;
        }
        public override Stream ReadFile(string fileName, IOConfig options)
        {
            var file = GetPhysicalFile(fileName);
            if (string.IsNullOrEmpty(file))
                throw new FileNotFoundException();
            return File.OpenRead(file);
        }

        public override Stream WriteFile(string fileName, IOConfig options)
        {
            throw new NotSupportedException("Cannot write to local system");
        }

    }
}
