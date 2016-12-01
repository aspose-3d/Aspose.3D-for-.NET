Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Animation
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Shading
Imports Aspose.ThreeD.Utilities
Imports System.Drawing
Imports System.Drawing.Imaging

Namespace Rendering
    Class CastAndReceiveShadow
        Public Shared Sub Run()
            Try
                ' ExStart:CastAndReceiveShadow
                ' The path to the documents directory.
                Dim MyDir As String = RunExamples.GetDataDir()

                Dim scene As New Scene()
                Dim camera As New Camera()
                camera.NearPlane = 0.1
                scene.RootNode.CreateChildNode("camera", camera)
                Dim light As New Light() With {
                    .NearPlane = 0.1,
                    .CastShadows = True,
                    .Color = New Vector3(Color.White)
                }
                scene.RootNode.CreateChildNode("light", light).Transform.Translation = New Vector3(9.4785, 5, 3.18)
                light.LookAt = Vector3.Origin
                light.Falloff = 90

                ' Create a plane
                Dim plane As Node = scene.RootNode.CreateChildNode("plane", New Plane(20, 20))
                plane.Material = New PhongMaterial() With {
                    .DiffuseColor = New Vector3(Color.DarkOrange)
                }
                plane.Transform.Translation = New Vector3(0, 0, 0)

                ' Create a torus for casting shadows
                Dim m As Mesh = (New Torus("", 1, 0.4, 20, 20, Math.PI * 2)).ToMesh()
                Dim torus As Node = scene.RootNode.CreateChildNode("torus", m)
                torus.Material = New PhongMaterial() With {
                    .DiffuseColor = New Vector3(Color.Cornsilk)
                }
                torus.Transform.Translation = New Vector3(2, 1, 1)

                If True Then
                    ' Create a blue box don' T cast shadows
                    m = (New Box()).ToMesh()
                    m.CastShadows = False
                    Dim box As Node = scene.RootNode.CreateChildNode("box", m)
                    box.Material = New PhongMaterial() With {
                        .DiffuseColor = New Vector3(Color.Blue)
                    }
                    box.Transform.Translation = New Vector3(2, 1, -1)
                End If
                If True Then
                    ' Create a red box that don' T receive shadow but cast shadows
                    m = (New Box()).ToMesh()
                    m.ReceiveShadows = False
                    Dim box As Node = scene.RootNode.CreateChildNode("box", m)
                    box.Material = New PhongMaterial() With {
                        .DiffuseColor = New Vector3(Color.Red)
                    }
                    box.Transform.Translation = New Vector3(-2, 1, 1)
                End If
                camera.ParentNode.Transform.Translation = New Vector3(10, 10, 10)
                camera.LookAt = Vector3.Origin
                Dim opt As New ImageRenderOptions() With {
                    .EnableShadows = True
                }
                scene.Render(camera, MyDir & Convert.ToString("CastAndReceiveShadow_out.png"), New Size(1024, 1024), ImageFormat.Png, opt)
                ' ExEnd:CastAndReceiveShadow  
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try


        End Sub
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace


