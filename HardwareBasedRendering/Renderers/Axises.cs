using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.ThreeD;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Render;
using Aspose.ThreeD.Utilities;

namespace AssetBrowser.Renderers
{
    /// <summary>
    /// Line objects used to represent the x,y,z axises
    /// This class inherits from <see cref="Lines"/> so it shares the same entity renderer with <see cref="Grid"/>
    /// </summary>
    class Axises : Lines
    {
        public Axises()
        {
            // draw the primitive as lines
            Draw(100, 0, 0);
            Draw(0, 100, 0);
            Draw(0, 0, 100);
        }
        private void Draw(float x, float y, float z)
        {
            FVector3 color = new FVector3(x, y, z).Normalize();
            Plot(0, 0, 0, color);
            Plot(x, y, z, color);
        }
    }
}
