# Aspose.3D for .NET

[Aspose.3D for .NET](https://products.aspose.com/3d/net) is a feature-rich Gameware and Computer-Aided-Designing (CAD) API that empowers Mono and .NET applications including ASP.NET, Windows Forms and Web Services to connect with prevalent 3D document formats automatically without any 3D modeling and rendering software being installed on the server. It supports Discreet3DS, WavefrontOBJ, [FBX](https://wiki.fileformat.com/3d/fbx/) (ASCII, Binary), [STL](https://wiki.fileformat.com/cad/stl/) (ASCII, Binary), Universal3D, Collada, [glTF](https://wiki.fileformat.com/3d/gltf/), [GLB](https://wiki.fileformat.com/3d/glb/), [PLY](https://wiki.fileformat.com/3d/ply/), DirectX and Google Draco file formats, allowing developers to easily create, read, convert, modify and control the substance of these 3D document formats using Aspose.3D API.

<p align="center">

  <a title="Download complete Aspose.3D for .NET source code" href="https://github.com/aspose-3d/Aspose.3D-for-.NET/archive/master.zip">
	<img src="http://i.imgur.com/hwNhrGZ.png" />
  </a>
</p>

This repository contains [Demos](Demos), [Examples](Examples), Plugins and Showcases for [Aspose.3D for .NET](https://products.aspose.com/3d/net) to help you learn and write your own applications.

Directory | Description
--------- | -----------
[Demos](Demos)  | Aspose.3D for .NET Live Demos Source Code
[Examples](Examples)  | A collection of .NET examples that help you learn and explore the API features


# .NET API for 3D File Formats

[Aspose.3D for .NET](http://products.aspose.com/3d/net) empowers .NET applications to connect with 3D document formats. 3D .NET API lets engineers read, convert, build, alter and control the substance of the [3D document formats](https://docs.aspose.com/display/3dnet/Supported+File+Formats) without any 3D modeling and rendering software installed on the machine.

## 3D File Processing Features

- [Import 3D scenes from PDF](https://docs.aspose.com/display/3dnet/Import+3D+Scenes+and+Contents+from+a+PDF).
- Import, create, customize, & save 3D scenes.
- [Create 3D mesh](https://docs.aspose.com/display/3dnet/Create+3D+Mesh+and+Scene) & [scale geometries of a 3D scene](https://docs.aspose.com/display/3dnet/Scale+geometries+of+a+3D+Scene).
- Configure cube by setting up normals or UV.
- Perform element formatting using 3D transformations.
- Share geometry data among multiple nodes of a mesh.
- [Add 3D scene animation](https://docs.aspose.com/display/3dnet/Add+Animation+Property+and+Setup+Target+Camera+in+3D+document).
- Work with 3D objects & models.

## Read & Write 3D Formats

**Autodesk:** FBX 7.2 to 7.5 (ASCII/Binary)
**3D Systems CAD::** STL (ASCII/Binary)
**Wavefront:** OBJ
**Discreet 3D Studio:** 3DS
**Universal3D:** U3D
**Collada:** DAE
**GL Transmission:** glTF (ASCII/Binary)
**Google Draco:** DRC
**RVM:** (Text/Binary)
**Portable Document Format:** PDF
**Other:** AMF, PLY (ASCII/Binary)

## Save 3D Files As

**WEB:** HTML

## Read 3D Formats

**DirectX:** X (ASCII/Binary)
**Siemens:** JT 
**Other:** DXF, ASF, VRML, 3MF

## Platform Independence

Aspose.3D for .NET is written in C# and supports Windows Forms as well as ASP.NET apps. Development can be performed on any platform that has .NET environment for both 32-bit and 64-bit applications. It supports .NET Frameworks 2.0 till 4.7.2 as well as Client Profile version for .NET Framework 4.0.

## Build a Scene with Primitive 3D Models using C# Code

You can execute below code snippet to see how Aspose.3D performs in your environment or check the [GitHub Repository](https://github.com/aspose-3d/Aspose.3D-for-.NET) for other common usage scenarios.

```csharp
// initialize a Scene object
Scene scene = new Scene();
// create a Box model
scene.RootNode.CreateChildNode("box", new Box());
// create a Cylinder model
scene.RootNode.CreateChildNode("cylinder", new Cylinder());
// save drawing in FBX format
scene.Save(dir + "output.fbx", FileFormat.FBX7500ASCII);
```

## Export 3D Scene to Compressed AMF via C# Code

Aspose.3D for .NET enables you to save 3D meshes in custom binary format, get all property values of 3D scenes as well as flip their coordinate system. Following example demonstrates the conversion of a 3D scene to AMF format while applying compression to it.

```csharp
// load a scene
Scene scene = new Scene();
var box = new Box();
var tr = scene.RootNode.CreateChildNode(box).Transform;
tr.Scale = new Vector3(12, 12, 12);
tr.Translation = new Vector3(10, 0, 0);
tr = scene.RootNode.CreateChildNode(box).Transform;
// scale transform
tr.Scale = new Vector3(5, 5, 5);
// set Euler angles
tr.EulerAngles = new Vector3(50, 10, 0);
scene.RootNode.CreateChildNode();
scene.RootNode.CreateChildNode().CreateChildNode(box);
scene.RootNode.CreateChildNode().CreateChildNode(box);
// save compressed AMF file
scene.Save(dir + "output.amf", new AMFSaveOptions() { EnableCompression = true });
```

[Product Page](https://products.aspose.com/3d/net) | [Docs](https://docs.aspose.com/display/3dnet/Home) | [Demos](https://products.aspose.app/3d/family) | [API Reference](https://apireference.aspose.com/3d/net) | [Examples](https://github.com/aspose-3d/Aspose.3D-for-.NET) | [Blog](https://blog.aspose.com/category/3d/) | [Free Support](https://forum.aspose.com/c/3d) |  [Temporary License](https://purchase.aspose.com/temporary-license)

