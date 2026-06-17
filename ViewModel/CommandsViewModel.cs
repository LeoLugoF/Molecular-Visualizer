using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Threading;
using MolecularVisualizer.MVVM;
using MolecularVisualizer.Parser;
using SharpDX;
using HelixToolkit.Wpf.SharpDX;
using System.Windows;

namespace MolecularVisualizer.ViewModel
{
    public partial class CommandsViewModel
    {
        private MoleculeViewModel moleculeVM;

        public CommandsViewModel(MoleculeViewModel moleculeVM)
        {
            this.moleculeVM = moleculeVM;
        }

        protected void InvokeEvent(EventHandler eventHandler, EventArgs args = null)
        {
            // Thread-safe invocation with null check
            eventHandler?.Invoke(this, args ?? EventArgs.Empty);
        }

        // For custom EventHandler<T> types
        protected void InvokeEvent<T>(EventHandler<T> eventHandler, T args)
            where T : EventArgs
        {
            eventHandler?.Invoke(this, args);
        }

        public RelayCommand OnLoadedMoleculeFromFile => new RelayCommand(execute => { InvokeEvent(LoadMoleculeFromFile); InvokeEvent(RequestZoom); }, canExecute => { return true; });
        public event EventHandler LoadMoleculeFromFile;



    }
}
