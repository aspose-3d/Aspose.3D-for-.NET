using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Render;
using Aspose.ThreeD.Utilities;

namespace AssetBrowser.Controls
{
    class RenderView : Control
    {
        public Scene Scene = new Scene();
        private Renderer renderer;
        private IRenderWindow window;
        private Camera camera;
        private Node cameraNode;
        private Viewport[] viewports;
        private Point lastLocation;
        private double latitude;
        private double longitude;
        private double elevation = 10;
        private bool controlPressed;
        private bool shiftPressed;

        private ShaderProgram gridShader;

        private RelativeRectangle[][] presets =
        {
            null,
            new [] {RelativeRectangle.FromScale(0, 0, 1, 1)},//1 viewport, start from (0%, 0%) and height/width is 100%
            //two viewports, their width is 50% of the window and height is 100%, left viewport starts from (0%, 0%) and right starts from (50%, 0%)
            new [] {RelativeRectangle.FromScale(0, 0, 0.5f, 1), RelativeRectangle.FromScale(0.5f, 0, 0.5f, 1)},
            //Assets Browser don't support 3 viewports
            null,
            //4 viewports, all of their height/width are 50%, and their location is (0%, 0%), (50%, 0%), (0%, 50%), (50%, 50%)
            new []
            {
                RelativeRectangle.FromScale(0, 0, 0.5f, 0.5f),
                RelativeRectangle.FromScale(0.5f, 0, 0.5f, 0.5f),
                RelativeRectangle.FromScale(0, 0.5f, 0.5f, 0.5f),
                RelativeRectangle.FromScale(0.5f, 0.5f, 0.5f, 0.5f),
            },

        };

        public RenderView()
        {
            if (!DesignMode)
                InitUI();
        }

        private void InitUI()
        {
            //in order to avoid window flicking, these have to be done before initialize the renderer to make sure all rendering jobs are handled by Aspose.3D. 
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            DoubleBuffered = false;

            //make the control selectable, so we can receive keyboard events
            SetStyle(ControlStyles.Selectable, true);
        }

        private void InitRenderer()
        {
            //create a default camera, because it's required during the viewport's creation.
            camera = new Camera();
            Scene.RootNode.CreateChildNode("camera", camera);
            //create the renderer and render window from window's native handle
            renderer = Renderer.CreateRenderer();
            //Right now we only support native window handle from Microsoft Windows
            //we'll support more platform on user's demand.
            window = renderer.RenderFactory.CreateRenderWindow(new RenderParameters(), Handle);
            //create 4 viewports, the viewport's area is meanless here because we'll change it to the right area in the SetViewports later
            viewports = new[]
            {
                window.CreateViewport(camera, Color.Gray, RelativeRectangle.FromScale(0, 0, 1, 1)),
                window.CreateViewport(camera, Color.Gray, RelativeRectangle.FromScale(0, 0, 1, 1)),
                window.CreateViewport(camera, Color.Gray, RelativeRectangle.FromScale(0, 0, 1, 1)),
                window.CreateViewport(camera, Color.Gray, RelativeRectangle.FromScale(0, 0, 1, 1))
            };
            SetViewports(1);


            //initialize shader for grid

            GLSLSource src = new GLSLSource();
            src.VertexShader = @"#version 330 core
layout (location = 0) in vec3 position;
uniform mat4 matWorldViewProj;
void main()
{
    gl_Position = matWorldViewProj * vec4(position, 1.0f);
}";
            src.FragmentShader = @"#version 330 core
out vec4 color;
void main()
{
    color = vec4(1, 1, 1, 1);
}";
            //define the input format used by GLSL vertex shader
            //the format is 
            // struct ControlPoint {
            //    FVector3 Position;
            //}
            VertexDeclaration fd = new VertexDeclaration();
            fd.AddField(VertexFieldDataType.FVector3, VertexFieldSemantic.Position);
            //compile shader from GLSL source code and specify the vertex input format
            gridShader = renderer.RenderFactory.CreateShaderProgram(src, fd);
            //connect GLSL uniform to renderer's internal variable
            gridShader.Variables = new ShaderVariable[]
            {
                new ShaderVariable("matWorldViewProj", VariableSemantic.MatrixWorldViewProj)
            };

            SceneUpdated("");
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!DesignMode)
            {
                //the renderer is initialized here because the Aspose.3d will not allow you create a render window with zero area
                //and in this event, it can get the non-zero area
                if (renderer == null)
                    InitRenderer();
                else//if the renderer has been initialized, need to update the render window's size
                    window.Size = Size;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if(!DesignMode)
                renderer.Render(window);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            controlPressed = e.Control;
            shiftPressed = e.Shift;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            controlPressed = e.Control;
            shiftPressed = e.Shift;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            //move the camera forward using mouse wheel
            double scale = 0.01;
            //and fast/slow the scale by pressing control/shift key
            if (controlPressed)
                scale *= 10;
            else if (shiftPressed)
                scale *= 0.1;
            elevation -= e.Delta*scale;
            UpdateCameraPosition();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //focus the control and remember the mouse position
            //it's used in the OnMouseMove to calculate the mouse's dragging direction.
            lastLocation = e.Location;
            Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.None)
                return;
            double scale = 0.01;
            //mouse's x coordinate will change longitude
            double dx = scale*(e.X - lastLocation.X);
            longitude += dx;

            //mouse's y coordinate will change latitude
            double dy = scale*(e.Y - lastLocation.Y);
            latitude += dy;

            UpdateCameraPosition();
            lastLocation = e.Location;
        }

        private void UpdateCameraPosition()
        {
            if (cameraNode != null)
            {

                //we simulate the camera only moves on a sphere that coordinated by elevation/latitude/longitude
                cameraNode.Transform.Translation = new Vector3(
                    -elevation*Math.Cos(latitude)*Math.Cos(longitude),
                    elevation*Math.Sin(latitude),
                    elevation*Math.Cos(latitude)*Math.Sin(longitude)
                    );
                //camera's LookAt property is Vector3(0, 0, 0) means it always looks at the origin point.
                Invalidate();
            }
        }

        public void SetViewports(int num)
        {
            //how many viewports will be used, we support 1/2/4 viewports
            var preset = presets[num];
            for (int i = 0; i < viewports.Length; i++)
            {
                //disable useless viewport
                viewports[i].Enabled = i < num;
                //update the viewport's pre-defined position
                if (viewports[i].Enabled)
                    viewports[i].Area = preset[i];
            }
            Invalidate();
        }

        public void SceneUpdated(string fileName)
        {
            //create the default camera, and update all viewports to use this camera
            //not all file formats support camera(e.g. STL/obj), and even they support, the file may not define a camera
            //so we need to manually define one
            camera = new Camera();
            cameraNode = Scene.RootNode.CreateChildNode("aab-camera", camera);
            cameraNode.Excluded = true;//generated by Asset browser, we don't want it to be exported
            camera.FarPlane = 4000;
            camera.NearPlane = 0.1;
            foreach (Viewport vp in viewports)
            {
                vp.Frustum = camera;
            }
            //reset the camera, and calculate the elevation from bounding box
            BoundingBox bb = Scene.RootNode.GetBoundingBox();
            latitude = longitude = 0;
            elevation = 10;//default elevation
            //infinite or null bounding box means the scene has no geometries, only adjust camera's elevation when it's finite
            if (bb.Extent == BoundingBoxExtent.Finite)
            {
                double newElevation = Math.Max(bb.Minimum.Length, bb.Maximum.Length) * 1.1;
                if (newElevation > 0.1)//if the bounding box is too small, ignore it
                {
                    //we also need to make sure the camera can see objects in side the elevation by setting the far and near plane
                    elevation = newElevation;
                    camera.FarPlane = elevation*100;
                    camera.NearPlane = Math.Max(0.1,  elevation*0.01);
                }
            }
            //update camera's position with new elevation
            UpdateCameraPosition();


            //if the scene loaded has no light, we should also add some lights, otherwise the renderer will disable lighting and only show diffuse color
            int lights = 0;
            Scene.RootNode.Accept(delegate(Node n)
            {
                if (n.GetEntity<Light>() != null)//found the light, increment the light counter
                    lights++;
                return lights == 0;//continue to search if no lights found
            });
            if (lights == 0)
            {
                //no light found in the scene, manually add some lights in random positions
                Random r = new Random();
                for (int i = 0; i < 2; i++)
                {
                    Vector3 pos = new Vector3();
                    pos.x = (r.NextDouble() - 0.5)*2*elevation;
                    pos.y = (r.NextDouble() - 0.5)*2*elevation;
                    pos.z = (r.NextDouble() - 0.5)*2*elevation;

                    Node lightNode = Scene.RootNode.CreateChildNode("aab-light#" + i, new Light()
                    {
                        LightType = LightType.Point,
                        NearPlane = 0.1,
                        Color = new Vector3(Color.Lavender)
                    });
                    lightNode.Transform.Translation = pos;
                    lightNode.Excluded = true;//generated by Asset browser, we don't want it to be exported
                }
            }
            //prepare the global ambient color, because a lot of file formats doesn't have this
            if(!Scene.AssetInfo.Ambient.HasValue)
                Scene.AssetInfo.Ambient = new Vector4(Color.AliceBlue);

            //make sure the renderer can find the texture in the same folder
            renderer.AssetDirectories.Clear();
            if(!string.IsNullOrEmpty(fileName))
                renderer.AssetDirectories.Add(Path.GetDirectoryName(fileName));

            //when scene reloaded, should manually clear the internal cache(textures/models) used in previous scene
            //it's ok to not clear the cache but may make your application consume more system and video memories
            renderer.ClearCache();

            //now we attach a grid object to the scene
            ABUtils.CreateInternalNode(Scene.RootNode, "aab-grid", new Grid(renderer, gridShader));

            Invalidate();
        }


        public void SetPostProcessings(List<string> postProcessingEffects)
        {
            renderer.PostProcessings.Clear();
            foreach (string effectId in postProcessingEffects)
            {
                PostProcessing pp = renderer.GetPostProcessing(effectId);
                renderer.PostProcessings.Add(pp);
            }
            Invalidate();
        }

        public void SetCamera(Camera camera)
        {
            foreach (Viewport vp in viewports)
            {
                vp.Frustum = camera;
            }
            this.camera = camera;
            this.cameraNode = camera.ParentNode;
            Invalidate();
        }
    }
}
