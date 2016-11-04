Imports System.IO
Imports System.Collections.Generic
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Formats

Namespace Loading_Saving
    Class LoadOptions
        Public Shared Sub Run()
            Discreet3DSLoadOption()
            ObjLoadOption()
            STLLoadOption()
            U3DLoadOption()
        End Sub
        Public Shared Sub Discreet3DSLoadOption()
            'ExStart:Discreet3DSOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            Dim loadOpts As New Discreet3DSLoadOptions()
            ' Sets wheather to use the transformation defined in the first frame of animation track.
            loadOpts.ApplyAnimationTransform = True
            ' Flip the coordinate system
            loadOpts.FlipCoordinateSystem = True
            ' Prefer to use gamma-corrected color if a 3ds file provides both original color and gamma-corrected color.
            loadOpts.GammaCorrectedColor = True
            ' Configure the look up paths to allow importer to find external dependencies.
            loadOpts.LookupPaths = New List(Of String)(New String() {MyDir})
            'ExEnd:Discreet3DSOption
        End Sub
        Public Shared Sub ObjLoadOption()
            'ExStart:ObjLoadOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize an object
            Dim loadObjOpts As New ObjLoadOptions()
            ' Import materials from external material library file
            loadObjOpts.EnableMaterials = True
            ' Flip the coordinate system.
            loadObjOpts.FlipCoordinateSystem = True
            ' Configure the look up paths to allow importer to find external dependencies.
            loadObjOpts.LookupPaths = New List(Of String)(New String() {MyDir})
            'ExEnd:ObjLoadOption
        End Sub
        Public Shared Sub STLLoadOption()
            'ExStart:STLLoadOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize an object
            Dim loadSTLOpts As New STLLoadOptions()
            ' Flip the coordinate system.
            loadSTLOpts.FlipCoordinateSystem = True
            ' Configure the look up paths to allow importer to find external dependencies.
            loadSTLOpts.LookupPaths = New List(Of String)(New String() {MyDir})
            'ExEnd:STLLoadOption
        End Sub
        Public Shared Sub U3DLoadOption()
            'ExStart:U3DLoadOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize an object
            Dim loadU3DOpts As New U3DLoadOptions()
            ' Flip the coordinate system.
            loadU3DOpts.FlipCoordinateSystem = True
            ' Configure the look up paths to allow importer to find external dependencies.
            loadU3DOpts.LookupPaths = New List(Of String)(New String() {MyDir})
            'ExEnd:U3DLoadOption
        End Sub
        Public Shared Sub glTFLoadOptions()
            'ExStart:glTFLoadOptions
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize Scene class object
            Dim scene As New Scene()
            ' Set load options
            Dim loadOpt As New GLTFLoadOptions()
            ' The default value is true, usually we don't need to change it. Aspose.3D will automatically flip the V/T texture coordinate during load and save.       
            loadOpt.FlipTexCoordV = True
            scene.Open(MyDir & Convert.ToString("Duck.gltf"), loadOpt)
            'ExEnd:glTFLoadOptions
        End Sub

    End Class
End Namespace
