Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Shading
Imports Aspose.ThreeD.Utilities

Namespace Geometry_Hierarchy
    Class ConcatenateQuaternions
        Public Shared Sub Run()
            'ExStart:ConcatenateQuaternions     
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            Dim scene As New Scene()

            Dim q1 As Quaternion = Quaternion.FromEulerAngle(Math.PI * 0.5, 0, 0)
            Dim q2 As Quaternion = Quaternion.FromAngleAxis(-Math.PI * 0.5, Vector3.XAxis)
            ' Concatenate q1 and q2. q1 and q2 rotate alone x-axis with same angle but different direction,
            ' so the concatenated result will be identity quaternion.
            Dim q3 As Quaternion = q1.Concat(q2)

            ' Create 3 cylinders to represent each quaternion
            Dim cylinder As Node = scene.RootNode.CreateChildNode("cylinder-q1", New Cylinder(0.1, 1, 2))
            cylinder.Transform.Rotation = q1
            cylinder.Transform.Translation = New Vector3(-5, 2, 0)

            cylinder = scene.RootNode.CreateChildNode("cylinder-q2", New Cylinder(0.1, 1, 2))
            cylinder.Transform.Rotation = q2
            cylinder.Transform.Translation = New Vector3(0, 2, 0)

            cylinder = scene.RootNode.CreateChildNode("cylinder-q3", New Cylinder(0.1, 1, 2))
            cylinder.Transform.Rotation = q3
            cylinder.Transform.Translation = New Vector3(5, 2, 0)
            MyDir = MyDir & Convert.ToString("test_out_.fbx")
            ' Save to file
            scene.Save(MyDir, FileFormat.FBX7400ASCII)
            'ExEnd:ConcatenateQuaternions

            Console.WriteLine(Convert.ToString(vbLf & "Quaternions concatenated successfully." & vbLf & "File saved at ") & MyDir)

        End Sub
    End Class
End Namespace