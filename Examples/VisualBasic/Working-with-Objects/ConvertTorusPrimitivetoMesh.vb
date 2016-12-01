Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats

Namespace Working_with_Objects
    Class ConvertTorusPrimitivetoMesh
        Public Shared Sub Run()
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            MyDir = MyDir & Convert.ToString("test.fbx")

            ' load a 3D file
            Dim scene As New Scene(MyDir)
            ' Initialize Node class object
            Dim cubeNode As New Node("torus")

            ' ExStart:ConvertTorusPrimitivetoMesh
            ' Initialize object by Torus class
            Dim convertible As IMeshConvertible = New Torus()

            ' Convert a Torus to Mesh
            Dim mesh As Mesh = convertible.ToMesh()
            ' ExEnd:ConvertTorusPrimitivetoMesh

            ' Point node to the Mesh geometry
            cubeNode.Entity = mesh

            ' Add Node to a scene
            scene.RootNode.ChildNodes.Add(cubeNode)

            ' The path to the documents directory.
            MyDir = RunExamples.GetDataDir() + RunExamples.GetOutputFilePath("TorusToMeshScene.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)

            Console.WriteLine(Convert.ToString(vbLf & " Converted the primitive Torus to a mesh successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace