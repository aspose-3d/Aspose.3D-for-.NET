
Imports System.IO
Imports System.Collections.Generic
Imports System.Collections
Imports Aspose.ThreeD
Imports Aspose.ThreeD.Formats
Imports Aspose.ThreeD.Entities
Imports Aspose.ThreeD.Utilities
Imports Aspose.ThreeD.Shading

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
        Public Shared Sub glTFSaveOptions()
            'ExStart:glTFSaveOptions
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize Scene object
            Dim scene As New Scene()
            ' Create a child node
            scene.RootNode.CreateChildNode("sphere", New Sphere())
            ' Set glTF saving options. The code example embeds all assets into the target file usually a glTF file comes with some dependencies, a bin file for model's vertex/indices, two .glsl files for vertex/fragment shaders
            ' use opt.EmbedAssets to tells the Aspose.3D API to export scene and embed the dependencies inside the target file.
            Dim opt As New GLTFSaveOptions(FileContentType.ASCII)
            opt.EmbedAssets = True
            ' Use KHR_materials_common extension to define the material, thus no GLSL files are generated.
            opt.UseCommonMaterials = True
            ' Customize the name of the buffer file which defines model
            opt.BufferFile = "mybuf.bin"
            ' Save glTF file
            scene.Save(MyDir & Convert.ToString("glTFSaveOptions_out_.gltf"), opt)

            ' Save a binary glTF file using KHR_binary_glTF extension
            scene.Save(MyDir & Convert.ToString("glTFSaveOptions_out_.glb"), FileFormat.GLTF_Binary)

            ' Developers may use saving options to create a binary glTF file using KHR_binary_glTF extension
            Dim opts As New GLTFSaveOptions(FileContentType.Binary)
            scene.Save(MyDir & Convert.ToString("Test_out_.glb"), opts)
            'ExEnd:glTFSaveOptions
        End Sub
        Public Shared Sub DiscardSavingMaterial()
            'ExStart:DiscardSavingMaterial
            ' The code example uses the DummyFileSystem, so the material files are not created.
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize Scene object
            Dim scene As New Scene()
            ' Create a child node
            scene.RootNode.CreateChildNode("sphere", New Sphere()).Material = New PhongMaterial()
            ' Set saving options
            Dim opt As New ObjSaveOptions()
            opt.FileSystem = New DummyFileSystem()
            ' Save 3D scene
            scene.Save(MyDir & Convert.ToString("DiscardSavingMaterial_out_.obj"), opt)
            'ExEnd:DiscardSavingMaterial
        End Sub
        Public Shared Sub SavingDependenciesInLocalDirectory()
            'ExStart:SavingDependenciesInLocalDirectory
            ' The code example uses the LocalFileSystem class to save dependencies to the local directory.
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize Scene object
            Dim scene As New Scene()
            ' Create a child node
            scene.RootNode.CreateChildNode("sphere", New Sphere()).Material = New PhongMaterial()
            ' Set saving options
            Dim opt As New ObjSaveOptions()
            opt.FileSystem = New LocalFileSystem(MyDir)
            ' Save 3D scene
            scene.Save(MyDir & Convert.ToString("SavingDependenciesInLocalDirectory_out_.obj"), opt)
            'ExEnd:SavingDependenciesInLocalDirectory
        End Sub
        Public Shared Sub SavingDependenciesInMemoryFileSystem()
            'ExStart:SavingDependenciesInMemoryFileSystem
            ' The code example uses the MemoryFileSystem to intercepts the dependencies writing.
            ' The path to the documents directory.
            Dim MyDir As String = RunExamples.GetDataDir()
            ' Initialize Scene object
            Dim scene As New Scene()
            ' Create a child node
            scene.RootNode.CreateChildNode("sphere", New Sphere()).Material = New PhongMaterial()
            ' Set saving options
            Dim opt As New ObjSaveOptions()
            Dim mfs As New MemoryFileSystem()
            opt.FileSystem = mfs
            ' Save 3D scene
            scene.Save(MyDir & Convert.ToString("SavingDependenciesInMemoryFileSystem_out_.obj"), opt)
            ' Get the test.mtl file content
            Dim mtl As Byte() = mfs.GetFileContent(MyDir & Convert.ToString("test.mtl"))
            File.WriteAllBytes(MyDir & Convert.ToString("Material.mtl"), mtl)
            'ExEnd:SavingDependenciesInMemoryFileSystem
        End Sub

    End Class
End Namespace