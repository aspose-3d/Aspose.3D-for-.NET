using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.ThreeD;
using System.IO;

namespace Aspose._3D.Examples.CSharp
{
    partial class RunExamples
    {
        private static readonly string projectDir;

        static RunExamples()
        {
            projectDir = GetProjectDir();
            if(File.Exists("License.lic"))
            {
                var lic = new Aspose.ThreeD.License();
                lic.SetLicense("License.lic");
            }

        }
        private static string GetProjectDir()
        { 
            var dir = Directory.GetCurrentDirectory();
            while(true)
            {
                var path = Path.Combine(dir, "Aspose.3D.Examples.CSharp.sln");
                if (File.Exists(path))
                    return dir;
                dir = Path.GetDirectoryName(dir);
                if (dir == null)
                    throw new InvalidOperationException("Cannot locate the directory of project");
            }
        }
        public static string GetDataDir()
        {
            return Path.Combine(projectDir, "Data\\");
        }
        public static string GetDataFilePath(String inputFilePath)
        {
            return GetDataDir() + inputFilePath;
        }
        public static string GetOutputFilePath(String inputFilePath)
        {
            return Path.Combine(projectDir, "Output", inputFilePath);
        }
    }
}
