using System;
using System.Collections.Generic;
using System.IO;
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
    /// The <see cref="Grid"/> inherits from the <see cref="Lines"/> so it shares the rendering implementation with <see cref="Axises"/>
    /// </summary>
    class Grid : Lines
    {
        /// <summary>
        /// Set the up vector, and 
        /// </summary>
        /// <param name="up"></param>
        public void SetUpVector(Axis up)
        {
            lines.Clear();
            var color = new FVector3(1, 1, 1);//white color of the grid
            if (up == Axis.YAxis)//y axis up
            {

                for (int i = -10; i <= 10; i++)
                {
                    //draw - line
                    Plot(i, 0, -10, color);
                    Plot(i, 0, 10, color);


                    //draw | line
                    Plot(-10, 0, i, color);
                    Plot(10, 0, i, color);
                }
            }
            else if(up == Axis.ZAxis)//z axis up
            {
                for (int i = -10; i <= 10; i++)
                {
                    //draw - line
                    Plot(i, -10, 0, color);
                    Plot(i, 10, 0, color);


                    //draw | line
                    Plot(-10, i, 0, color);
                    Plot(10, i, 0, color);
                }
            }
            dirty = true;
        }

    }

}
