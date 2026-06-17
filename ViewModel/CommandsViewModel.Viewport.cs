using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MolecularVisualizer.MVVM;

namespace MolecularVisualizer.ViewModel
{
    public partial class CommandsViewModel 
    {

        //public RelayCommand ZoomExtentsCommand => CreateEventCommand("ZoomExtents");

        public RelayCommand OnZoomExtentsCommand => new RelayCommand(execute => InvokeEvent(RequestZoom));
        public RelayCommand OnStopRendering => new RelayCommand(execute => InvokeEvent(StopRendering));
        public RelayCommand OnStartRendering => new RelayCommand(execute => InvokeEvent(StartRendering));
        

        //Need to subscribe to event handler from view
        public event EventHandler RequestZoom;
        public event EventHandler StopRendering;
        public event EventHandler StartRendering;
        

    }
}
