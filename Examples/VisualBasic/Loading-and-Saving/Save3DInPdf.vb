Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utilities
Imports Aspose.ThreeD.Shading
Imports Aspose.ThreeD.Formats
Imports System.Drawing

Namespace Loading_Saving
    Class Save3DInPdf
        Public Shared Sub Run()
            ' ExStart:Save3DInPdf
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()

            ' Create a new scene
            Dim scene As New Scene()
            ' Create a cylinder child node
            scene.RootNode.CreateChildNode("cylinder", New Cylinder()).Material = New PhongMaterial() With {
                 .DiffuseColor = New Vector3(Color.DarkCyan) _
            }
            ' Set rendering mode and lighting scheme
            Dim opt As New PdfSaveOptions()
            opt.LightingScheme = PdfLightingScheme.CAD
            opt.RenderMode = PdfRenderMode.ShadedIllustration
            ' Save in the PDF format
            scene.Save(MyDir & Convert.ToString("output_out.pdf"), opt)
            ' ExEnd:Save3DInPdf           
        End Sub
    End Class
End Namespace
