using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CSharp.Loading_Saving;
using CSharp.AssetInformation;
using CSharp.Animation;
using CSharp.Geometry_Hierarchy;
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

            //InformationToScene.Run();

            //// =====================================================
            //// =====================================================
            //// Geometry and Hierarchy
            //// =====================================================

            CubeScene.Run();
            //MaterialToCube.Run();            
            //TransformationToNode.Run();
            //NodeHierarchy.Run();
            //MeshGeometryData.Run();

            // Stop before exiting
            Console.WriteLine("\n\nProgram Finished. Press any key to exit....");
            Console.ReadKey();
        }
        public static String GetDataDir_LoadingAndSaving()
        {
            return Path.GetFullPath("../../Loading-and-Saving/Data/");
        }
        public static String GetDataDir_AssetInformation()
        {
            return Path.GetFullPath("../../AssetInformation/Data/");
        }
        public static String GetDataDir_Animation()
        {
            return Path.GetFullPath("../../Animation/Data/");
        }
        public static String GetDataDir_GeometryAndHierarchy()
        {
            return Path.GetFullPath("../../Geometry-and-Hierarchy/Data/");
        }
    }
}
