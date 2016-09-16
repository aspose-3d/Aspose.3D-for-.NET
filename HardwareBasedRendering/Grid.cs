using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Render;
using Aspose.ThreeD.Utilities;

namespace AssetBrowser
{
    /// <summary>
    /// Inherit from ManualEntity can simplify the construction of renderable geometry
    /// </summary>
    // ExStart:ManualEntity
    class Grid : ManualEntity
    {
        public Grid(Renderer renderer, ShaderProgram shader)
        {
            // Render state for grid
            RenderState = renderer.RenderFactory.CreateRenderState();
            RenderState.DepthTest = true;
            RenderState.DepthMask = true;
            this.Shader = shader;
            // Define the format of the control point to render the line
            VertexDeclaration vd = new VertexDeclaration();
            vd.AddField(VertexFieldDataType.FVector3, VertexFieldSemantic.Position);
            // and create a vertex buffer for storing this kind of data
            this.VertexBuffer = renderer.RenderFactory.CreateVertexBuffer(vd);
            // Draw the primitive as lines
            this.DrawOperation = DrawOperation.Lines;
            this.RenderGroup = RenderQueueGroupId.Geometries;

            List<FVector3> lines = new List<FVector3>();
            for (int i = -10; i <= 10; i++)
            {
                // Draw - line
                lines.Add(new FVector3(i, 0, -10));
                lines.Add(new FVector3(i,0, 10));


                // Draw | line
                lines.Add(new FVector3(-10, 0, i));
                lines.Add(new FVector3(10, 0, i));
            }
            // Put it to vertex buffer
            VertexBuffer.LoadData(lines.ToArray());
        }
    }
    // ExEnd:ManualEntity
}
