using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixToolkit.Wpf.SharpDX;

namespace MolecularVisualizer.Model
{
    public class ViewportEventArgs : EventArgs
    {
        public IEnumerable<Element3D> Elements { get; set; }
        public bool ClearExisting { get; set; }
    }
}
