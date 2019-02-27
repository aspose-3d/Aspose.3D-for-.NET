using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Render;
using Aspose.ThreeD.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Aspose._3D.Examples.CSharp.Rendering
{
    class RenderSceneWithPanoramaInDepth
    {
        // ExStart:RenderSceneWithPanoramaInDepth
        public static void Run()
        {
            //load the scene
            Scene scene = new Scene(RunExamples.GetDataFilePath("skybox.obj"));
            //create a camera for capturing the cube map
            Camera cam = new Camera(ProjectionType.Perspective);
            cam.NearPlane = 0.1;
            cam.FarPlane = 200;
            scene.RootNode.CreateChildNode(cam).Transform.Translation = new Vector3(5, 6, 0);
            cam.RotationMode = RotationMode.FixedDirection;

            //create two lights to illuminate the scene
            scene.RootNode.CreateChildNode(new Light() { LightType = LightType.Point }).Transform.Translation = new Vector3(-10, 7, -10);
            scene.RootNode.CreateChildNode(new Light()
            {
                LightType = LightType.Point,
                ConstantAttenuation = 0.1,
                Color = new Vector3(Color.CadetBlue)
            }).Transform.Translation = new Vector3(49, 0, 49);

            //create a render target
            using (var renderer = Renderer.CreateRenderer())
            {
                //Create a cube map render target with depth texture, depth is required when rendering a scene.
                IRenderTexture rt = renderer.RenderFactory.CreateCubeRenderTexture(new RenderParameters(false), 512, 512);
                //create a 2D texture render target with no depth texture used for image processing
                IRenderTexture final = renderer.RenderFactory.CreateRenderTexture(new RenderParameters(false, 32, 0, 0), 1024 * 3, 1024);

                //a viewport is required on the render target
                rt.CreateViewport(cam, RelativeRectangle.FromScale(0, 0, 1, 1));
                renderer.ShaderSet = CreateDepthShader(renderer);
                renderer.Render(rt);

                //execute the equirectangular projection post-processing with the previous rendered cube map as input
                PostProcessing equirectangular = renderer.GetPostProcessing("equirectangular");
                equirectangular.Input = rt.Targets[0];
                renderer.Execute(equirectangular, final);
                //save the texture into disk
                ((ITexture2D)final.Targets[0]).Save(RunExamples.GetOutputFilePath("RenderSceneWithPanoramaInDepth_Out.png"), ImageFormat.Png);
            }
        }
        private static ShaderSet CreateDepthShader(Renderer renderer)
        {
            GLSLSource src = new GLSLSource();
            src.VertexShader = @"#version 330 core
            layout (location = 0) in vec3 position;
            uniform mat4 matWorldViewProj;
            out float depth;
            void main()
            {
                gl_Position = matWorldViewProj * vec4(position, 1.0f);
                float zfar = 200.0;
                float znear = 0.5;
                //visualize the depth by linearize it so we don't get a blank screen
                depth = (2.0 * znear) / (zfar + znear - gl_Position.z /gl_Position.w  * (zfar - znear));
            }";
                    src.FragmentShader = @"#version 330 core
            in float depth;
            out vec4 color;
            void main()
            {
                color = vec4(depth, depth, depth, 1);
            }";
            //we only need the position to render the depth map
            VertexDeclaration fd = new VertexDeclaration();
            fd.AddField(VertexFieldDataType.FVector3, VertexFieldSemantic.Position);
            //compile shader from GLSL source code and specify the vertex input format
            var shader = renderer.RenderFactory.CreateShaderProgram(src, fd);
            //connect GLSL uniform to renderer's internal variable
            shader.Variables = new ShaderVariable[]
            {
        new ShaderVariable("matWorldViewProj", VariableSemantic.MatrixWorldViewProj)
            };
            //create a shader set
            ShaderSet ret = new ShaderSet();
            //we only use the fallback, and left other shaders unassigned, so all materials will be rendered by this shader
            ret.Fallback = shader;
            return ret;
        }
        // ExEnd:RenderSceneWithPanoramaInDepth
    }
}
