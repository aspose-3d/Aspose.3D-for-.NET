using System;
using System.IO;
using System.Collections;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Formats;
using Aspose.ThreeD.Utilities;
using System.Runtime.InteropServices;

namespace Aspose._3D.Examples.CSharp._Working_with_Objects
{
    class ConvertSphereMeshtoTriangleMeshCustomMemoryLayout
    {
        //ExStart:ConvertSphereMeshtoTriangleMeshCustomMemoryLayout
        [StructLayout(LayoutKind.Sequential)]
        struct MyVertex
        {
            [Semantic(VertexFieldSemantic.Position)]
            FVector3 position;
            [Semantic(VertexFieldSemantic.Normal)]
            FVector3 normal;
        }

        public static void Run()
        {
            // Initialize scene object
            Scene scene = new Scene();

            // Initialize Node class object
            Node cubeNode = new Node("sphere");

            Mesh sphere = (new Sphere()).ToMesh();
            //convert any mesh into typed TriMesh
            var myMesh = TriMesh<MyVertex>.FromMesh(sphere);
            //Get the vertex data in customized vertex structure.
            MyVertex[] vertex = myMesh.VerticesToTypedArray();
            //get the 32bit and 16bit indices
            int[] indices32bit;
            ushort[] indices16bit;
            myMesh.IndicesToArray(out indices32bit);
            myMesh.IndicesToArray(out indices16bit);
            using (MemoryStream ms = new MemoryStream())
            {
                //or we can write the vertex directly into stream like:
                myMesh.WriteVerticesTo(ms);
                //the indice data can be directly write to stream, we support 32-bit and 16-bit indice.
                myMesh.Write16bIndicesTo(ms);
                myMesh.Write32bIndicesTo(ms);
            }
            // Point node to the Mesh geometry
            cubeNode.Entity = sphere;

            // Add Node to a scene
            scene.RootNode.ChildNodes.Add(cubeNode);

            // The path to the documents directory.
            string MyDir = RunExamples.GetDataDir() + RunExamples.GetOutputFilePath("SphereToTriangleMeshCustomMemoryLayoutScene.fbx");

            // Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII);

            Console.WriteLine("Indices = {0}, Actual vertices = {1}, vertices before merging = {2}", myMesh.IndicesCount, myMesh.VerticesCount, myMesh.UnmergedVerticesCount);
            Console.WriteLine("Total bytes of vertices in memory {0}bytes", myMesh.VerticesSizeInBytes);
            Console.WriteLine("\n Converted a Sphere mesh to triangle mesh with custom memory layout of the vertex successfully.\nFile saved at " + MyDir);
        }
        //ExEnd:ConvertSphereMeshtoTriangleMeshCustomMemoryLayout

    }
}
