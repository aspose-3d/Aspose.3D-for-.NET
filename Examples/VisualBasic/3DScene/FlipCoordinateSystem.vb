Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animations
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.IO

Namespace _3DScene
    Class FlipCoordinateSystem
        Public Shared Sub Run()
            ' ExStart:FlipCoordinateSystem
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize scene object
            Dim scene As New Scene()
            scene.Open(MyDir & Convert.ToString("camera.3ds"), New Discreet3DSConfig() With {.FlipCoordinateSystem = True})
            MyDir = MyDir & Convert.ToString("FlipCoordinateSystem.obj")
            scene.Save(MyDir, New ObjConfig() With {.EnableMaterials = False})
            ' ExEnd:FlipCoordinateSystem
            Console.WriteLine(Convert.ToString(vbLf & "Coordinate system has been flipped successfully." & vbLf & "File saved at ") & MyDir)
        End Sub
    End Class
End Namespace