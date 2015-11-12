
'///////////////////////////////////////////////////////////////////////
' Copyright 2015 Aspose Pty Ltd. All Rights Reserved.
'
' This file is part of Aspose.3D. The source code in this file
' is only intended as a supplement to the documentation, and is provided
' "as is", without warranty of any kind, either expressed or implied.
'///////////////////////////////////////////////////////////////////////
Imports System.IO
Imports System.Collections
Imports Aspose.ThreeD

Namespace Loading_Saving
    Public Class DocumentToStream
        Public Shared Sub Run()
            ' ExStart:DocumentToStream
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir_LoadingAndSaving()

            ' ExStart:LoadAndSave
            ' Load a 3D document into Aspose.3D
            Dim scene As New Scene(MyDir & Convert.ToString("document.fbx"))

            Dim dstStream As New MemoryStream()
            scene.Save(dstStream, FileFormat.FBX7400ASCII)
            ' ExEnd:LoadAndSave

            ' Rewind the stream position back to zero so it is ready for next reader.
            dstStream.Position = 0
            ' ExEnd:DocumentToStream

            Console.WriteLine(vbLf & "Converted 3D document to stream successfully.")
        End Sub
    End Class
End Namespace
