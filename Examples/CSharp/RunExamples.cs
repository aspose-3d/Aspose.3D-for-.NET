using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Aspose._3D.Examples.CSharp.Loading_Saving;
using Aspose._3D.Examples.CSharp.AssetInformation;
using Aspose._3D.Examples.CSharp.Animation;
using Aspose._3D.Examples.CSharp.Rendering;
using Aspose._3D.Examples.CSharp.Geometry_Hierarchy;
using Aspose._3D.Examples.CSharp._3DScene;
using System.Reflection;
using Aspose._3D.Examples.CSharp._3DModeling;
using Aspose._3D.Examples.CSharp._3DViewPorts;
using Aspose._3D.Examples.CSharp._Working_with_Objects;
using Aspose._3D.Examples.CSharp.Polygons;
using Aspose._3D.Examples.CSharp.Working_with_Objects;
using Aspose._3D.Examples.CSharp.Geometry_and_Hierarchy;
using Aspose._3D.Examples.CSharp.Loading_and_Saving;
using Aspose._3D.Examples.CSharp.WorkingWithLinearExtrusion;
using Aspose._3D.Examples.CSharp.Materials;
using Aspose._3D.Examples.CSharp.WorkingWithVRML;

namespace Aspose._3D.Examples.CSharp
{
    class RunExamples
    {
        [STAThread]
        public static void Main()
        {
            Console.WriteLine("Open RunExamples.cs. \nIn Main() method uncomment the example that you want to run.");
            Console.WriteLine("=====================================================");
            // Uncomment the one you want to try out          


            // =====================================================
            // =====================================================
            // Material and texture access
            // =====================================================
            // =====================================================

            // CreateSceneWithEmbeddedTexture.Run();
            // DumpEmbeddedTextures.Run();

            // =====================================================
            // =====================================================
            // Loading and Saving
            // =====================================================
            // =====================================================

            // CreateEmpty3DDocument.Run();          
            // ReadExistingScene.Run();
            // DetectFormat.Run();
            // Save3DInPdf.Run();
            // OpenSceneFromProtectedPdf.Run();
            // ExtractAll3DScenes.Run();
            // SaveOptions.SavingDependenciesInMemoryFileSystem();
            // LoadOptions.Run();
            // SaveOptions.Run();
            // Non_PBRtoPBRMaterial.Run();
            // CancellationToken.Run();
            // Save3DScene.Run();
            // Save3DScene.Compression();

            // =====================================================
            // =====================================================
            // Animation
            // =====================================================
            // =====================================================

            // PropertyToDocument.Run();
            // SetupTargetAndCamera.Run();

            // =====================================================
            // =====================================================
            // 3DScene
            // =====================================================
            // =====================================================

            // FlipCoordinateSystem.Run();
            // Save3DMeshesInCustomBinaryFormat.Run();
            // ExportSceneToCompressedAMF.Run();
            // ChangePlaneOrientation.Run();

            // =====================================================
            // =====================================================
            // Asset Information
            // =====================================================
            // =====================================================

            // InformationToScene.Run();

            // =====================================================
            // =====================================================
            // Geometry and Hierarchy
            // =====================================================

            // CubeScene.Run();
            // MaterialToCube.Run();            
            // TransformationToNodeByQuaternion.Run();
            // TransformationToNodeByEulerAngles.Run();
            // TransformationToNodeByTransformationMatrix.Run();
            // NodeHierarchy.Run();
            // MeshGeometryData.Run();
            // SetupNormalsOnCube.Run();
            // TriangulateMesh.Run();
            // ConcatenateQuaternions.Run();
            // ApplyPBRMaterialToBox.Run();

            //// =====================================================
            //// =====================================================
            //// 3D Modeling
            //// =====================================================
            //// =====================================================

            // Primitive3DModels.Run();

            // =====================================================
            // =====================================================
            // Working with Objects
            // =====================================================
            // =====================================================

            // SplitMeshbyMaterial.Run();            
            // ConvertSpherePrimitivetoMesh.Run();
            // ConvertBoxPrimitivetoMesh.Run();
            // ConvertPlanePrimitivetoMesh.Run();
            // ConvertCylinderPrimitivetoMesh.Run();
            // ConvertTorusPrimitivetoMesh.Run();
            // ConvertSphereMeshtoTriangleMeshCustomMemoryLayout.Run();
            // ConvertBoxMeshtoTriangleMeshCustomMemoryLayout.Run();
            // GenerateDataForMeshes.Run();
            // BuildTangentAndBinormalData.Run();
            // Encode3DMeshinGoogleDraco.Run();
            // XPathLikeObjectQueries.Run();
            //WorkingWithSphereRadius.Run();

            // =====================================================
            // =====================================================
            // Rendering
            // =====================================================
            // =====================================================
            // RenderSceneIntoCubemapwithsixfaces.Run();
            // RenderPanaromaViewof3DScene.Run();
            // RenderFisheyeLensEffectof3DScene.Run();
            // Render3DModelImageFromCamera.Run();
            // CastAndReceiveShadow.Run();
            // RenderSceneWithPanoramaInDepth.Run();

            // =====================================================
            // =====================================================
            // 3DViewPorts
            // =====================================================
            // =====================================================
            // ApplyVisualEffects.Run();
            // CaptureViewPort.Run();

            // =====================================================
            // =====================================================
            // Polygons
            // =====================================================
            // =====================================================
            // ConvertPolygonsToTriangles.Run();
            // GenerateUV.Run();

            // =====================================================
            // =====================================================
            // Working With Linear Extrusion
            // =====================================================
            // =====================================================
            // PerformingLinearExtrusion.Run();
            // SlicesInLinearExtrusion.Run();
            // CenterInLinearExtrusion.Run();
            // TwistInLinearExtrusion.Run();
            // TwistOffsetInLinearExtrusion.Run();
            // DirectionInLinearExtrusion.Run();

            // =====================================================
            // =====================================================
            // Working With VRML files
            // =====================================================
            // =====================================================
            // OpenVRML.Run();

            // Stop before exiting
            Console.WriteLine("\n\nProgram Finished. Press any key to exit....");
            Console.ReadKey();
        }

        private static string GetProjectDir()
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
            return startDirectory;
        }
        public static string GetDataDir()
        {
            return Path.Combine(GetProjectDir(), "Data\\");
        }
        public static string GetDataFilePath(String inputFilePath)
        {
            return GetDataDir() + inputFilePath;
        }
        public static string GetOutputFilePath(String inputFilePath)
        {
            return Path.Combine(GetProjectDir(), "Output", inputFilePath);
        }
    }
}
