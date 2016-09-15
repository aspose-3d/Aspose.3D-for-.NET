
Imports System.IO
Imports System.Collections.Generic
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Formats

Namespace Loading_Saving
    Class SaveOptions
        Public Shared Sub Run()
            ColladaSaveOption()
            Discreet3DSSaveOption()
            FBXSaveOption()
            ObjSaveOption()
        End Sub
        Public Shared Sub ColladaSaveOption()
            'ExStart:ColladaSaveOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            Dim saveColladaopts As New ColladaSaveOptions()
            ' Generates indented XML document
            saveColladaopts.Indented = True
            ' The style of node transformation
            saveColladaopts.TransformStyle = ColladaTransformStyle.Matrix
            ' Configure the lookup paths to allow importer to find external dependencies.
            saveColladaopts.LookupPaths = New List(Of String)(New String() {MyDir})
            'ExEnd:ColladaSaveOption
        End Sub
        Public Shared Sub Discreet3DSSaveOption()
            'ExStart:Discreet3DSSaveOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize an object
            Dim saveOpts As New Discreet3DSSaveOptions()
            ' The start base for generating new name for duplicated names.
            saveOpts.DuplicatedNameCounterBase = 2
            ' The format of the duplicated counter.
            saveOpts.DuplicatedNameCounterFormat = "NameFormat"
            ' The separator between object's name and the duplicated counter.
            saveOpts.DuplicatedNameSeparator = "Separator"
            ' Allows to export cameras
            saveOpts.ExportCamera = True
            ' Allows to export light
            saveOpts.ExportLight = True
            ' Flip the coordinate system
            saveOpts.FlipCoordinateSystem = True
            ' Prefer to use gamma-corrected color if a 3ds file provides both original color and gamma-corrected color.
            saveOpts.GammaCorrectedColor = True
            ' Use high-precise color which each color channel will use 32bit float.
            saveOpts.HighPreciseColor = True
            ' Configure the look up paths to allow importer to find external dependencies.
            saveOpts.LookupPaths = New List(Of String)(New String() {MyDir})
            ' Set the master scale
            saveOpts.MasterScale = 1
            'ExEnd:Discreet3DSSaveOption
        End Sub
        Public Shared Sub FBXSaveOption()
            'ExStart:FBXSaveOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize an object
            Dim saveOpts As New FBXSaveOptions(FileFormat.FBX7500ASCII)
            ' Generates the legacy material properties.
            saveOpts.ExportLegacyMaterialProperties = True
            ' Fold repeated curve data using FBX's animation reference count
            saveOpts.FoldRepeatedCurveData = True
            ' Always generates material mapping information for geometries if the attached node contains materials.
            saveOpts.GenerateVertexElementMaterial = True
            ' Configure the look up paths to allow importer to find external dependencies.
            saveOpts.LookupPaths = New List(Of String)(New String() {MyDir})
            ' Generates a video object for texture.
            saveOpts.VideoForTexture = True
            'ExEnd:FBXSaveOption
        End Sub
        Public Shared Sub ObjSaveOption()
            'ExStart:ObjSaveOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize an object
            Dim saveObjOpts As New ObjSaveOptions()
            ' Import materials from external material library file
            saveObjOpts.EnableMaterials = True
            ' Flip the coordinate system.
            saveObjOpts.FlipCoordinateSystem = True
            ' Configure the look up paths to allow importer to find external dependencies.
            saveObjOpts.LookupPaths = New List(Of String)(New String() {MyDir})
            ' Serialize W component in model's vertex position
            saveObjOpts.SerializeW = True
            ' Generate comments for each section
            saveObjOpts.Verbose = True
            'ExEnd:ObjSaveOption
        End Sub
        Public Shared Sub STLSaveOption()
            'ExStart:STLSaveOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize an object
            Dim saveSTLOpts As New STLSaveOptions()
            ' Flip the coordinate system.
            saveSTLOpts.FlipCoordinateSystem = True
            ' Configure the look up paths to allow importer to find external dependencies.
            saveSTLOpts.LookupPaths = New List(Of String)(New String() {MyDir})
            'ExEnd:STLSaveOption
        End Sub
        Public Shared Sub U3DSaveOption()
            'ExStart:U3DSaveOption
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize an object
            Dim saveU3DOptions As New U3DSaveOptions()
            ' Export normal data.
            saveU3DOptions.ExportNormals = True
            ' Export the texture coordinates.
            saveU3DOptions.ExportTextureCoordinates = True
            ' Export the vertex diffuse color.
            saveU3DOptions.ExportVertexDiffuse = True
            ' Export vertex specular color
            saveU3DOptions.ExportVertexSpecular = True
            ' Flip the coordinate system.
            saveU3DOptions.FlipCoordinateSystem = True
            ' Configure the look up paths to allow importer to find external dependencies.
            saveU3DOptions.LookupPaths = New List(Of String)(New String() {MyDir})
            ' Compress the mesh data
            saveU3DOptions.MeshCompression = True
            'ExEnd:U3DSaveOption
        End Sub
    End Class
End Namespace