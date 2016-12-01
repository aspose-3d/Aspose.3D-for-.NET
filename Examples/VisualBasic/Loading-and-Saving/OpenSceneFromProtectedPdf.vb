Imports System.IO
Imports System.Text
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Formats

Namespace Loading_Saving
    Class OpenSceneFromProtectedPdf
        Public Shared Sub Run()
            ' ExStart:OpenSceneFromProtectedPdf
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Create a new scene
            Dim scene As New Scene()
            ' Use loading options and apply password
            Dim opt As New PdfLoadOptions() With {
                 .Password = Encoding.UTF8.GetBytes("password") _
            }
            ' Open scene
            scene.Open(MyDir & Convert.ToString("House_Design.pdf"), opt)
            ' ExEnd:OpenSceneFromProtectedPdf            
        End Sub
    End Class
End Namespace
