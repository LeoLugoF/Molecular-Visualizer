using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using HelixToolkit.Wpf.SharpDX;
using Microsoft.Win32;
using MolecularVisualizer.Helpers;
using MolecularVisualizer.Model;
using MolecularVisualizer.Services;
using MolecularVisualizer.ViewModel;
using SharpDX;


namespace MolecularVisualizer.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm = new MainWindowViewModel();
        public MainWindow()
        {
 
            DataContext = vm;
            InitializeComponent();
            SetUpViewPortEvents();
            SetUpMainHandling();

            System.Windows.Media.CompositionTarget.Rendering += OnRendering;
            viewport.MouseDown += Viewport_MouseDown;
            viewport.KeyDown += Viewport_KeyDown;
            MainLight.Direction = viewport.Camera.LookDirection;
        }

        private void Viewport_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var hits = viewport.FindHits(e.GetPosition(viewport));

            if (hits == null || hits.Count == 0)
                return;

            var mesh = hits[0].ModelHit as MeshGeometryModel3D;

            if (mesh == null)
                return;

            if (e.LeftButton == MouseButtonState.Pressed && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                if(mesh.DataContext is AtomViewModel atom)
                {

                }
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                vm.ToggleObjectSelection(mesh);
            }

        }

        private void Viewport_KeyDown(object sender, KeyEventArgs e)
        {
            Molecule mol = vm.MoleculeVM.SelectedMolecule;

            if (e.Key == Key.B && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                vm.ToggleBondBetweenSelectedAtoms();
            }
        }

        private DateTime lastLightUpdate;

        private void OnRendering(object sender, EventArgs e)
        {

            if ((DateTime.Now - lastLightUpdate).TotalMilliseconds < 16)
                return;

            // Cache camera direction (avoid repeated conversions)
            var camDir = viewport.Camera.LookDirection;
            var difference = (MainLight.Direction - camDir).LengthSquared;
            MainLight.Direction = camDir; // Direct assignment avoids conversion

            // Selective invalidation - only if direction changed significantly
            if (difference > 0.01)
            {
                viewport.InvalidateRender(); // More efficient than InvalidateArrange
                lastLightUpdate = DateTime.Now;
            }

        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var sw = Stopwatch.StartNew();
            
            if (!(sender is TreeViewItem item)) return;

            if (!item.IsSelected || item.DataContext == null) return;

            if (DataContext is MainWindowViewModel vm)
            {
                // Safe access to bound data
                if (item.DataContext is Job job)
                {
                    vm.MoleculeVM.SelectedMolecule = job.Molecules.LastOrDefault();
                }
                else if (item.DataContext is Molecule molecule)
                {
                    vm.MoleculeVM.SelectedMolecule = molecule;
                }
                Debug.WriteLine($"Total Time: {sw.ElapsedMilliseconds}ms");
            }
        }

    }


}


