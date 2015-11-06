Imports System.IO
Imports VisualBasic.Geometry_Hierarchy
Imports VisualBasic.Animation
Imports VisualBasic.AssetInformation
Imports VisualBasic.Loading_Saving

Module RunExamples
    Sub Main()
        Console.WriteLine("Open RunExamples.cs. In Main() method, Un-comment the example that you want to run")
        Console.WriteLine("=====================================================")

        ' Un-comment the one you want to try out

        '' =====================================================
        '' =====================================================
        '' Loading and Saving
        '' =====================================================
        '' =====================================================

        'DocumentToStream.Run()

        '' =====================================================
        '' =====================================================
        '' Animation
        '' =====================================================
        '' =====================================================

        'PropertyToDocument.Run()

        '' =====================================================
        '' =====================================================
        '' Asset Information
        '' =====================================================
        '' =====================================================

        'InformationToScene.Run()

        '' =====================================================
        '' =====================================================
        '' Geometry and Hierarchy
        '' =====================================================

        'CubeScene.Run()
        'MaterialToCube.Run()
        'TransformationToNode.Run()
        'NodeHierarchy.Run()
        MeshGeometryData.Run()

        ' Stop before exiting
        Console.WriteLine(vbLf & vbLf & "Program Finished. Press any key to exit....")
        Console.ReadKey()
    End Sub
    Public Function GetDataDir_LoadingAndSaving() As [String]
        Return Path.GetFullPath("../../Loading-and-Saving/Data/")
    End Function
    Public Function GetDataDir_AssetInformation() As [String]
        Return Path.GetFullPath("../../AssetInformation/Data/")
    End Function
    Public Function GetDataDir_Animation() As [String]
        Return Path.GetFullPath("../../Animation/Data/")
    End Function
    Public Function GetDataDir_GeometryAndHierarchy() As [String]
        Return Path.GetFullPath("../../Geometry-and-Hierarchy/Data/")
    End Function
End Module