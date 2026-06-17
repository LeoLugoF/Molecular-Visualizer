using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using HelixToolkit.Wpf.SharpDX;
using MolecularVisualizer;
using SharpDX;

namespace MolecularVisualizer.View
{
    public partial class MainWindow : Window
    {

        public void SetUpViewPortEvents()
        {
            vm.SceneCommands.RequestZoom += (_, e) => SafeInvoke(() => viewport.ZoomExtents());
            vm.MoleculeVM.ClearVieport += (_,e) => CleanViewport();
            vm.MoleculeVM.HideViewport += (_,e) => HideViewport();
            
        }

        private void HideViewport()
        {
            //viewport.Visibility = Visibility.Hidden;
        }


        private void CleanViewport()
        {
            viewport.Visibility = Visibility.Visible;
            GC.Collect();
            viewport.InvalidateSceneGraph();

            
        }

        private void SafeInvoke(Action action)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, action);
        }


    }
}
