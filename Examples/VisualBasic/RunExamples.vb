Imports System.IO
Imports Aspose._3D.Examples.VisualBasic.Geometry_Hierarchy
Imports Aspose._3D.Examples.VisualBasic.Animation
Imports Aspose._3D.Examples.VisualBasic.AssetInformation
Imports Aspose._3D.Examples.VisualBasic.Loading_Saving
Imports Aspose._3D.Examples.VisualBasic._3DScene
Imports Aspose._3D.Examples.VisualBasic._3DViewPorts
Imports Aspose._3D.Examples.VisualBasic._3DModeling
Imports Aspose._3D.Examples.VisualBasic.Working_with_Objects
Imports Aspose._3D.Examples.VisualBasic.Rendering

Module RunExamples
    Sub Main()
        Console.WriteLine("Open RunExamples.vb. " & vbLf & "In Main() method uncomment the example that you want to run.")
        Console.WriteLine("=====================================================")

        ' Uncomment the one you want to try out

        '' =====================================================
        '' =====================================================
        '' Loading and Saving
        '' =====================================================
        '' =====================================================

        'Save3DScene.Run()
        'ReadExistingScene.Run()
        'CreateEmpty3DDocument.Run()

        '' =====================================================
        '' =====================================================
        '' Animation
        '' =====================================================
        '' =====================================================

        'PropertyToDocument.Run()
        'SetupTargetAndCamera.Run()

        '' =====================================================
        '' =====================================================
        '' 3DScene
        '' =====================================================
        '' =====================================================

        'FlipCoordinateSystem.Run()

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
        'TransformationToNodeByQuaternion.Run()
        'TransformationToNodeByEulerAngles.Run()
        'TransformationToNodeByTransformationMatrix.Run()
        'NodeHierarchy.Run()
        'MeshGeometryData.Run()
        'SetupNormalsOnCube.Run()
        'SetupUVOnCube.Run()
        'TriangulateMesh.Run()
        'ConcatenateQuaternions.Run()

        '' =====================================================
        '' =====================================================
        '' 3D Modeling
        '' =====================================================
        '' =====================================================

        'Primitive3DModels.Run()

        '' =====================================================
        '' =====================================================
        '' Working with Objects
        '' =====================================================
        '' =====================================================

        'SplitMeshbyMaterial.Run()
        'ConvertSpherePrimitivetoMesh.Run()
        'ConvertBoxPrimitivetoMesh.Run()
        'ConvertPlanePrimitivetoMesh.Run()
        'ConvertCylinderPrimitivetoMesh.Run()
        'ConvertTorusPrimitivetoMesh.Run()
        'ConvertSphereMeshtoTriangleMeshCustomMemoryLayout.Run()
        'ConvertBoxMeshtoTriangleMeshCustomMemoryLayout.Run()
        'GenerateDataForMeshes.Run()

        '' =====================================================
        '' =====================================================
        '' Rendering
        '' =====================================================
        '' =====================================================

        'CastAndReceiveShadow.Run()
        'Render3DModelImageFromCamera.Run()

        '' =====================================================
        '' =====================================================
        '' 3DViewPorts
        '' =====================================================
        '' =====================================================

        'ApplyVisualEffects.Run()
        'CaptureViewPort.Run()

        ' Stop before exiting
        Console.WriteLine(vbLf & vbLf & "Program Finished. Press any key to exit....")
        Console.ReadKey()
    End Sub

    Public Function GetDataDir() As String
        Dim parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent
        Dim startDirectory As String = Nothing
        If parent IsNot Nothing Then
            Dim directoryInfo = parent.Parent
            If directoryInfo IsNot Nothing Then
                startDirectory = directoryInfo.FullName
            End If
        Else
            startDirectory = parent.FullName
        End If
        Return Path.Combine(startDirectory, "Data\")
    End Function
    Public Function GetOutputFilePath(inputFilePath As [String]) As String
        Dim extension As String = Path.GetExtension(inputFilePath)
        Dim filename As String = Path.GetFileNameWithoutExtension(inputFilePath)
        Return Convert.ToString(filename & Convert.ToString("_out_")) & extension
    End Function
End Module
