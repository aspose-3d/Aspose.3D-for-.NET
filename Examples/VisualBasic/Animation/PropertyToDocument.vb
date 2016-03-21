Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utilities
Imports VisualBasic.Geometry_Hierarchy

Namespace Animation
    Public Class PropertyToDocument
        Public Shared Sub Run()

            ' ExStart:AddAnimationPropertyToDocument
            ' Initialize scene object
            Dim scene As New Scene()

            ' Call Common class create mesh using polygon builder method to set mesh instance 
            Dim mesh As Mesh = Common.CreateMeshUsingPolygonBuilder()

            ' Each cube node has their own translation
            Dim cube1 As Node = scene.RootNode.CreateChildNode("cube1", mesh)

            ' Find translation property on node's transform object
            Dim translation As [Property] = cube1.Transform.FindProperty("Translation")

            ' Create a curve mapping based on translation property
            Dim mapping As New CurveMapping(scene, translation)

            ' Create curve on channel X and Z
            Dim curveX As Curve = mapping.CreateCurve("X")
            Dim curveZ As Curve = mapping.CreateCurve("Z")

            ' Move node's translation to (10, 0, 10) at 0 sec using bezier interpolation
            curveX.CreateKeyFrame(0, 10.0F, Interpolation.Bezier)
            curveZ.CreateKeyFrame(0, 10.0F, Interpolation.Bezier)

            ' Move node's translation to (20, 0, -10) at 3 sec
            curveX.CreateKeyFrame(3, 20.0F, Interpolation.Bezier)
            curveZ.CreateKeyFrame(3, -10.0F, Interpolation.Bezier)

            ' Move node's translation to (30, 0, 0) at 5 sec
            curveX.CreateKeyFrame(5, 30.0F, Interpolation.Linear)
            curveZ.CreateKeyFrame(5, 0.0F, Interpolation.Bezier)

            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            MyDir = MyDir & RunExamples.GetOutputFilePath("PropertyToDocument.fbx")
          
            ' Save 3D scene in the supported file formats
            scene.Save(MyDir, FileFormat.FBX7400ASCII)
            ' ExEnd:AddAnimationPropertyToDocument

            Console.WriteLine(Convert.ToString(vbLf & "Animation property added successfully to document." & vbLf & "File saved at ") & MyDir)

        End Sub
    End Class
End Namespace

