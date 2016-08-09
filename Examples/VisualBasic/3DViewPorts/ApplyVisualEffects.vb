Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Render
Imports Aspose.ThreeD.Utilities

Namespace _3DViewPorts
    Class ApplyVisualEffects
        Public Shared Sub Run()
            ' ExStart:ApplyVisualEffects
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

            ' The CreateRenderer will create a hardware OpenGL-backend renderer, more renderer will be added in the future
            ' and some internal initializations will be done.
            ' When the renderer left using the scope, the unmanaged hardware resources will also be disposed
            Using renderer__1 = Renderer.CreateRenderer()
                renderer__1.EnableShadows = False

                ' Create a new render target that renders the scene to texture(s)
                ' Use default render parameters
                ' And one output targets
                ' Size is 1024 x 1024
                ' This render target can have multiple render output textures, but here we only need one output.
                ' The other textures and depth textures are mainly used by deferred shading in the future.
                ' but you can also access the depth texture through IRenderTexture.DepthTeture
                Using rt As IRenderTexture = renderer__1.RenderFactory.CreateRenderTexture(New RenderParameters(), 1, 1024, 1024)
                    ' This render target has one viewport to render, the viewport occupies the 100% width and 100% height
                    Dim vp As Viewport = rt.CreateViewport(camera, New RelativeRectangle() With {
                         .ScaleWidth = 1, _
                         .ScaleHeight = 1 _
                    })
                    ' Render the target and save the target texture to external file
                    renderer__1.Render(rt)
                    rt.Targets(0).Save(MyDir & Convert.ToString("Original_viewport_out_.png"), ImageFormat.Png)

                    ' Create a post-processing effect
                    Dim pixelation As PostProcessing = renderer__1.GetPostProcessing("pixelation")
                    renderer__1.PostProcessings.Add(pixelation)
                    renderer__1.Render(rt)
                    rt.Targets(0).Save(MyDir & Convert.ToString("VisualEffect_pixelation_out_.png"), ImageFormat.Png)

                    ' Clear previous post-processing effects and try another one
                    Dim grayscale As PostProcessing = renderer__1.GetPostProcessing("grayscale")
                    renderer__1.PostProcessings.Clear()
                    renderer__1.PostProcessings.Add(grayscale)
                    renderer__1.Render(rt)
                    rt.Targets(0).Save(MyDir & Convert.ToString("VisualEffect_grayscale_out_.png"), ImageFormat.Png)

                    ' We can also combine post-processing effects
                    renderer__1.PostProcessings.Clear()
                    renderer__1.PostProcessings.Add(grayscale)
                    renderer__1.PostProcessings.Add(pixelation)
                    renderer__1.Render(rt)
                    rt.Targets(0).Save(MyDir & Convert.ToString("VisualEffect_grayscale+pixelation_out_.png"), ImageFormat.Png)

                    ' Clear previous post-processing effects and try another one
                    Dim edgedetection As PostProcessing = renderer__1.GetPostProcessing("edge-detection")
                    renderer__1.PostProcessings.Clear()
                    renderer__1.PostProcessings.Add(edgedetection)
                    renderer__1.Render(rt)
                    rt.Targets(0).Save(MyDir & Convert.ToString("VisualEffect_edgedetection_out_.png"), ImageFormat.Png)

                    ' Clear previous post-processing effects and try another one
                    Dim blur As PostProcessing = renderer__1.GetPostProcessing("blur")
                    renderer__1.PostProcessings.Clear()
                    renderer__1.PostProcessings.Add(blur)
                    renderer__1.Render(rt)
                    rt.Targets(0).Save(MyDir & Convert.ToString("VisualEffect_blur_out_.png"), ImageFormat.Png)
                End Using
            End Using
            ' ExEnd:ApplyVisualEffects           
        End Sub
    End Class
End Namespace
