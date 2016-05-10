Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats

Namespace Working_with_Objects
    Class ConvertCylinderPrimitivetoMesh
        Public Shared Sub Run()
            ' Initialize scene object
            Dim scene As New Scene()

            ' Initialize Node class object
            Dim cubeNode As New Node("cylinder")

            ' ExStart:ConvertCylinderPrimitivetoMesh
            ' initialize object by Cylinder class
            Dim convertible As IMeshConvertible = New Cylinder()

            ' convert a Cylinder to Mesh
            Dim mesh As Mesh = convertible.ToMesh()
            ' ExEnd:ConvertCylinderPrimitivetoMesh

            ' Point node to the Mesh geometry
            cubeNode.Entity = mesh

            ' Add Node to a scene
            scene.RootNode.ChildNodes.Add(cubeNode)

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir() + RunExamples.GetOutputFilePath("CylinderToMeshScene.fbx")

            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)

            Console.WriteLine(Convert.ToString(vbLf & " Converted the primitive Cylinder to a mesh successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace