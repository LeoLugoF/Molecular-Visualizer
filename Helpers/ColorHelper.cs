using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace MolecularVisualizer.Helpers
{
    public static class ColorHelper
    {
        public static Color4 GetAtomColor(string element)
        {
            switch (element)
            {
                case "H": return Color.White;
                case "O": return Color.Red;
                case "C": return Color.Gray;
                case "N": return Color.Blue;
                case "P": return Color.DarkBlue;
                case "B": return Color.Green;
                default: return Color.Green;
            }
        }



    }
}
