using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolecularVisualizer.Utilities
{
    public class AppEvents
    {
        public enum ViewportEvent
        {
            ZoomExtents,
            //All other herein
        }

        public enum MainwindowEvent
        {
            OnLoadedMolecule,
        }
    }
}
