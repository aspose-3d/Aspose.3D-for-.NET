using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CSharp.Loading_Saving;
using CSharp.AssetInformation;
using CSharp.Animation;
using CSharp.Geometry_Hierarchy;
using CSharp._3DScene;
using System.Reflection;
using CSharp._3DModeling;
using CSharp._Working_with_Objects;

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

            //CreateEmpty3DDocument.Run();
            //DocumentToStream.Run();
            //ReadExistingScene.Run();

            //// =====================================================
            //// =====================================================
            //// Animation
            //// =====================================================
            //// =====================================================

            //PropertyToDocument.Run();
            //SetupTargetAndCamera.Run();

            //// =====================================================
            //// =====================================================
            //// 3DScene
            //// =====================================================
            //// =====================================================

            //FlipCoordinateSystem.Run();

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

            //CubeScene.Run();
            //MaterialToCube.Run();            
            //TransformationToNodeByQuaternion.Run();
            //TransformationToNodeByEulerAngles.Run();
            //TransformationToNodeByTransformationMatrix.Run();
            //NodeHierarchy.Run();
            //MeshGeometryData.Run();
            //SetupNormalsOnCube.Run();
            //TriangulateMesh.Run();

            //// =====================================================
            //// =====================================================
            //// 3D Modeling
            //// =====================================================
            //// =====================================================

            //Primitive3DModels.Run();

            //// =====================================================
            //// =====================================================
            //// Working with Objects
            //// =====================================================
            //// =====================================================

            //SplitMeshbyMaterial.Run();            
            //ConvertSpherePrimitivetoMesh.Run();
            //ConvertBoxPrimitivetoMesh.Run();
            //ConvertPlanePrimitivetoMesh.Run();
            //ConvertCylinderPrimitivetoMesh.Run();
            //ConvertTorusPrimitivetoMesh.Run();
            //ConvertSphereMeshtoTriangleMeshCustomMemoryLayout.Run();
            ConvertBoxMeshtoTriangleMeshCustomMemoryLayout.Run();

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
