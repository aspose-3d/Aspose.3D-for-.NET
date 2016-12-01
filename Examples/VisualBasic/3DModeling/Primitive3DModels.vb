Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats

Namespace _3DModeling
    Class Primitive3DModels
        Public Shared Sub Run()
            ' ExStart:Primitive3DModels
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            ' Initialize a Scene object
            Dim scene As New Scene()
            ' Create a Box model
            scene.RootNode.CreateChildNode("box", New Box())
            ' Create a Cylinder model
            scene.RootNode.CreateChildNode("cylinder", New Cylinder())
            ' Save drawing in the FBX format
            MyDir = MyDir & RunExamples.GetOutputFilePath("test.fbx")
            scene.Save(MyDir, FileFormat.FBX7500ASCII)

            ' ExEnd:Primitive3DModels
            Console.WriteLine(Convert.ToString(vbLf & "Building a scene from primitive 3D models successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace