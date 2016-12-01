Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD

Namespace Loading_Saving
    Class DetectFormat
        Public Shared Sub Run()
            ' ExStart:DetectFormat
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Detect the format of a 3D file
            Dim inputFormat As FileFormat = FileFormat.Detect(MyDir & Convert.ToString("document.fbx"))
            ' Display the file format
            Console.WriteLine("File Format: " + inputFormat.ToString())
            ' ExEnd:DetectFormat            
        End Sub
    End Class
End Namespace

