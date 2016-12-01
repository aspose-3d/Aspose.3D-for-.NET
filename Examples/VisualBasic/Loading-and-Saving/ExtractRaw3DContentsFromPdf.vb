Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Formats

Namespace Loading_Saving
    Class ExtractRaw3DContentsFromPdf
        Public Shared Sub Run()
            ' ExStart:ExtractRaw3DContentsFromPdf
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            Dim password As Byte() = Nothing
            ' Extract 3D contents
            Dim contents As List(Of Byte()) = FileFormat.PDF.Extract(MyDir & Convert.ToString("House_Design.pdf"), password)
            Dim i As Integer = 1
            ' Iterate through the contents and in separate 3D files
            For Each content As Byte() In contents
                Dim fileName As String = "3d-" + (System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1))
                File.WriteAllBytes(fileName, content)
            Next
            ' ExEnd:ExtractRaw3DContentsFromPdf            
        End Sub
    End Class
End Namespace
