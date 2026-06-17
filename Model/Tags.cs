using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixToolkit.Wpf.SharpDX;

namespace MolecularVisualizer.Model
{
    public class Tags
    {

        public PhongMaterial OriginalMaterial { get; set; }

    }

    public class AtomTags : Tags
    {
        public int Index { get; set; }
        public string Type { get; set; } = "atom";
    }

    public class BondTags : Tags
    {
        public string Type { get; set; } = "bond";
        public int AtomIndex1 { get; set; }
        public int AtomIndex2 { get; set; }


    }
}
