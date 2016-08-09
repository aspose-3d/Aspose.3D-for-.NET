Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Render
Imports Aspose.ThreeD.Utilities

Namespace _3DViewPorts
    Class CaptureViewPort
        Public Shared Sub Run()
            ' ExStart:CaptureViewPort
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            ' Load an existing 3D scene
            Dim scene As New Scene(MyDir & Convert.ToString("scene.obj"))
            ' Create an instance of the camera
            Dim camera As New Camera()
            scene.RootNode.CreateChildNode("camera", camera).Transform.Translation = New Vector3(2, 44, 66)
            ' Set the target
            camera.LookAt = New Vector3(50, 12, 0)
            ' Create a light
            scene.RootNode.CreateChildNode("light", New Light() With {
                 .Color = New Vector3(Color.White),
                 .LightType = LightType.Point _
            }).Transform.Translation = New Vector3(26, 57, 43)

            ' The CreateRenderer will create a hardware OpenGL-backend renderer
            ' and some internal initializations will be done.
            ' When the renderer left using the scope, the unmanaged hardware resources will also be disposed
            Using renderer__1 = Renderer.CreateRenderer()
                renderer__1.EnableShadows = False

                ' Create a new render target that renders the scene to texture(s)
                ' Use default render parameters
                ' and one output targets
                ' Size is 1024 x 1024
                ' This render target can have multiple render output textures, but here we only need one output.
                ' The other textures and depth textures are mainly used by deferred shading in the future.
                ' but you can also access the depth texture through IRenderTexture.DepthTeture
                ' Use CreateRenderWindow method to render in window, like:
                ' Window = renderer.RenderFactory.CreateRenderWindow(new RenderParameters(), Handle);
                Using rt As IRenderTexture = renderer__1.RenderFactory.CreateRenderTexture(New RenderParameters(), 1, 1024, 1024)
                    ' This render target has one viewport to render, the viewport occupies the 100% width and 100% height
                    Dim vp As Viewport = rt.CreateViewport(camera, New RelativeRectangle() With {
                         .ScaleWidth = 1,
                         .ScaleHeight = 1
                    })
                    ' Render the target and save the target texture to external file
                    renderer__1.Render(rt)
                    rt.Targets(0).Save(MyDir & Convert.ToString("file-1viewports_out_.png"), ImageFormat.Png)

                    ' Now let's change the previous viewport only uses the half left side(50% width and 100% height)
                    vp.Area = New RelativeRectangle() With {
                         .ScaleWidth = 0.5F,
                         .ScaleHeight = 1
                    }
                    ' and create a new viewport that occupies the 50% width and 100% height and starts from 50%
                    ' both of them are using the same camera, so the rendered content should be the same
                    rt.CreateViewport(camera, New RelativeRectangle() With {
                         .ScaleX = 0.5F,
                         .ScaleWidth = 0.5F,
                         .ScaleHeight = 1
                    })
                    ' but this time let's increase the field of view of the camera to 90 degree so it can see more part of the scene
                    camera.FieldOfView = 90
                    renderer__1.Render(rt)
                    rt.Targets(0).Save(MyDir & Convert.ToString("file-2viewports_out_.png"), ImageFormat.Png)
                End Using
            End Using
            ' ExEnd:CaptureViewPort           
        End Sub
    End Class
End Namespace
