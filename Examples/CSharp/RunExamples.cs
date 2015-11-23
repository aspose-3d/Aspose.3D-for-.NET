using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CSharp.Loading_Saving;
using CSharp.AssetInformation;
using CSharp.Animation;
using CSharp.Geometry_Hierarchy;
using System.Reflection;

namespace CSharp
{
    class RunExamples
    {
        [STAThread]
        public static void Main()
        {            
            Console.WriteLine("Open RunExamples.cs. In Main() method, Un-comment the example that you want to run");
            Console.WriteLine("=====================================================");
            // Un-comment the one you want to try out

            //// =====================================================
            //// =====================================================
            //// Loading and Saving
            //// =====================================================
            //// =====================================================

            //DocumentToStream.Run();

            //// =====================================================
            //// =====================================================
            //// Animation
            //// =====================================================
            //// =====================================================

            //PropertyToDocument.Run();

            //// =====================================================
            //// =====================================================
            //// Asset Information
            //// =====================================================
            //// =====================================================

            InformationToScene.Run();

            //// =====================================================
            //// =====================================================
            //// Geometry and Hierarchy
            //// =====================================================

            //CubeScene.Run();
            //MaterialToCube.Run();            
            //TransformationToNode.Run();
            //NodeHierarchy.Run();
            //MeshGeometryData.Run();

            // Stop before exiting
            Console.WriteLine("\n\nProgram Finished. Press any key to exit....");
            Console.ReadKey();
        }
        public static string GetDataDir()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            string startDirectory = null;
            if (parent != null)
            {
                var directoryInfo = parent.Parent;
                if (directoryInfo != null)
                {
                    startDirectory = directoryInfo.FullName;
                }
            }
            else
            {
                startDirectory = parent.FullName;
            }
            return Path.Combine(startDirectory, "Data\\");
        }
        public static string GetOutputFilePath(String inputFilePath)
        {           
            string extension = Path.GetExtension(inputFilePath);
            string filename = Path.GetFileNameWithoutExtension(inputFilePath);
            return filename + "_out_" + extension;            
        }
    }
}
