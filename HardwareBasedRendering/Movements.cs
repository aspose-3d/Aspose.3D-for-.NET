using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.ThreeD;
using Aspose.ThreeD.Animation;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;

namespace AssetBrowser
{
    interface IKeyState
    {
        bool ControlPressed { get; }
        bool ShiftPressed { get; }
        bool AltPressed { get; }
        MouseButtons Buttons { get; }
    }


    abstract class Movement
    {
        protected Camera camera;
        protected Node cameraNode;
        protected IKeyState keyState;
        protected Point lastLocation;
        protected Vector2 delta;
        protected float scale = 0.05f;

        public static Movement Create<T>(IKeyState keyState, Camera camera, Scene scene) where T : Movement, new()
        {
            T ret = new T();
            ret.camera = camera;
            ret.cameraNode = camera.ParentNode;
            ret.keyState = keyState;
            ret.Initialize(scene);
            return ret;
        }

        public virtual void KeyUp(Keys keys)
        {
            
        }

        public virtual void KeyDown(Keys keys)
        {
            
        }

        public virtual void MouseDown(Point location)
        {
            this.lastLocation = location;
            delta = new Vector2(0, 0);
        }

        public virtual void MouseMove(Point location)
        {
            
        }
        public virtual void MouseDrag(Point location)
        {
            delta.x = scale*(location.X - lastLocation.X);
            delta.y = scale*(location.Y - lastLocation.Y);
            lastLocation = location;
        }

        public virtual void MouseUp(Point location)
        {
            delta.x = scale*(location.X - lastLocation.X);
            delta.y = scale*(location.Y - lastLocation.Y);
            
        }

        public virtual void MouseWheel(int delta)
        {
            
        }

        public virtual void Initialize(Scene scene)
        {
            
        }

        public virtual void Update()
        {
            
        }
        
    }

    /// <summary>
    /// Standard movement, use middle button to pan the view and left button to rotate the view
    /// </summary>
    class StandardMovement : Movement
    {
        public override void Initialize(Scene scene)
        {
            base.Initialize(scene);
            camera.RotationMode = RotationMode.FixedDirection;
        }

        public override void MouseWheel(int delta)
        {
            base.MouseWheel(delta);
            if (camera.ProjectionType == ProjectionType.Orthographic)
            {
                //in orthographic mode change the magnification
                double scale = delta > 0 ? 1.1 : 0.9;
                camera.Magnification = scale * camera.Magnification;

            }
            else
            {
                //in perspective mode change the translation
                var dir = camera.Direction;
                cameraNode.Transform.Translation += scale * delta * dir;
            }
        }

        public override void MouseDrag(Point location)
        {
            base.MouseDrag(location);
            var dir = camera.Direction;
            var left = dir.Cross(camera.Up);
            var up = dir.Cross(left);
            if (keyState.Buttons == MouseButtons.Middle)
            {
                //move the camera 
                double scale = 1;
                if (keyState.ControlPressed)
                    scale = 2;
                else if (keyState.ShiftPressed)
                    scale = 0.5;
                cameraNode.Transform.Translation += scale * (delta.x * left + delta.y * up);
            }
            else if (keyState.Buttons == MouseButtons.Left)
            {
                //rotate the camera
                var q = Quaternion.FromAngleAxis( MathUtils.ToRadian(delta.x), up);
                q = q.Concat(Quaternion.FromAngleAxis(MathUtils.ToRadian(-delta.y), left));
                var newDir = q * dir;
                camera.Direction = newDir;
                
            }
        }
    }
    class FPSMovement : Movement
    {
        public override void MouseDrag(Point location)
        {
            base.MouseDrag(location);

            var dir = camera.Direction;
            var left = dir.Cross(camera.Up);
            var up = dir.Cross(left);
            var q = Quaternion.FromAngleAxis( MathUtils.ToRadian(delta.x), up);
            q = q.Concat(Quaternion.FromAngleAxis(MathUtils.ToRadian(-delta.y), left));
            var newDir = q * dir;
            camera.Direction = newDir;
        }

        public override void KeyDown(Keys keys)
        {
            base.KeyDown(keys);
            float dx = 0;
            float dy = 0;
            float step = 1;
            switch (keys)
            {
                case Keys.W:
                    dx += step;
                    break;
                case Keys.S:
                    dx -= step;
                    break;
                case Keys.A:
                    dy -= step;
                    break;
                case Keys.D:
                    dy += step;
                    break;
            }
            var dir = camera.Direction;
            var left = dir.Cross(camera.Up);
            var translate = dir * dx + left * dy;

            cameraNode.Transform.Translation += translate;
        }

        public override void Initialize(Scene scene)
        {
            base.Initialize(scene);
            camera.RotationMode = RotationMode.FixedDirection;
            camera.LookAt = Vector3.Origin;
        }
    }
    class OrbitalMovement : Movement
    {
        private double latitude;
        private double longitude;
        private double elevation = 10;
        public override void MouseWheel(int delta)
        {
            base.MouseWheel(delta);
            //move the camera forward using mouse wheel
            //and fast/slow the scale by pressing control/shift key
            float scale = 0.1f;
            if (keyState.ControlPressed)
                scale *= 10;
            else if (keyState.ShiftPressed)
                scale *= 0.1f;
            elevation -= delta*scale;
        }

        public override void MouseDrag(Point location)
        {
            base.MouseDrag(location);
            longitude += delta.x;
            latitude += delta.y;
        }

        public override void Initialize(Scene scene)
        {
            camera.RotationMode = RotationMode.FixedTarget;
            //reset the camera, and calculate the elevation from bounding box
            BoundingBox bb = scene.RootNode.GetBoundingBox();


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
        }

        public override void Update()
        {
            //we simulate the camera only moves on a sphere that coordinated by elevation/latitude/longitude
            cameraNode.Transform.Translation = new Vector3(
                -elevation * Math.Cos(latitude) * Math.Cos(longitude),
                elevation * Math.Sin(latitude),
                elevation * Math.Cos(latitude) * Math.Sin(longitude)
            );
        }
    }
}
