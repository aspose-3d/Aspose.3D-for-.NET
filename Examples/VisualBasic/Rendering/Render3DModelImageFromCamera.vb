Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Formats
Imports Aspose.ThreeD.Utilities
Imports System.Drawing
Imports System.Drawing.Imaging

Namespace Rendering
    Class Render3DModelImageFromCamera
        Public Shared Sub Run()
            Try
                ' ExStart:Render3DModelImageFromCamera
                ' The path to the documents directory.
                Dim MyDir As String = RunExamples.GetDataDir()

                ' Load scene from file
                Dim scene As New Scene(MyDir & Convert.ToString("camera.3ds"))
                ' Create a camera at (10,10,10) and look at the origin point for rendering,
                ' It must be attached to the scene before render
                Dim camera As New Camera()
                scene.RootNode.CreateChildNode("camera", camera)
                camera.ParentNode.Transform.Translation = New Vector3(10, 10, 10)
                camera.LookAt = Vector3.Origin

                ' Specify the image render option
                Dim opt As New ImageRenderOptions()
                ' Set the background color
                opt.BackgroundColor = Color.AliceBlue
                ' Tells renderer where the it can find textures
                opt.AssetDirectories.Add(MyDir & Convert.ToString("textures"))
                ' Turn on shadow
                opt.EnableShadows = True
                ' Render the scene in given camera' S perspective into specified png file with size 1024x1024
                scene.Render(camera, MyDir & Convert.ToString("Render3DModelImageFromCamera_out.png"), New Size(1024, 1024), ImageFormat.Png, opt)
                ' ExEnd:Render3DModelImageFromCamera  
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try


        End Sub
    End Class
End Namespace
