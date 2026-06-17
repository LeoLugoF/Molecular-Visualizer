using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HelixToolkit.Wpf.SharpDX;
using MolecularViewer;
using MolecularVisualizer.Model;
using MolecularVisualizer.MVVM;
using MolecularVisualizer.Parser;

namespace MolecularVisualizer.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {


        public MoleculeViewModel MoleculeVM { get; }
        public EffectsManager EffectsManager { get; }
        
        public CommandsViewModel SceneCommands { get; }

        


        public Camera Camera { get; }
        public MainWindowViewModel()
        {
            MoleculeVM = new MoleculeViewModel();
            EffectsManager = new DefaultEffectsManager();
            Camera = new PerspectiveCamera();
            SceneCommands = new CommandsViewModel(MoleculeVM);

        }


        private object selectedTreeItem;
        public object SelectedTreeItem
        {
            get => selectedTreeItem;
            set
            {
                selectedTreeItem = value;
                OnPropertyChanged();
                HandleTreeSelection(value);
            }
        }

        private void HandleTreeSelection(object selectedItem)
        {
            if (selectedItem is Job job)
                SelectedTreeItem = job.Molecules.LastOrDefault();
            else if (selectedItem is Molecule molecule)
                selectedTreeItem = molecule;
        }

        public void ToggleObjectSelection(GeometryModel3D Object3D)
        {
            if (Object3D.DataContext is Object3DViewModel object3DVM)
            {
                if (!object3DVM.IsSelected)
                    MoleculeVM.selectedObjects.Add(object3DVM);
                if (object3DVM.IsSelected)
                    MoleculeVM.selectedObjects.Remove(object3DVM);
                object3DVM.IsSelected = !object3DVM.IsSelected;
            }

        }

        public void ToggleBondBetweenSelectedAtoms()
        {
            if (MoleculeVM.selectedObjects.Count != 2)
                return;

            if (!(MoleculeVM.selectedObjects[0] is AtomViewModel &&
                MoleculeVM.selectedObjects[1] is AtomViewModel))
                return;

            AtomViewModel AtomVM1 = MoleculeVM.selectedObjects[0] as AtomViewModel;
            AtomViewModel AtomVM2 = MoleculeVM.selectedObjects[1] as AtomViewModel;

            MoleculeVM.AddRemoveBond(AtomVM1, AtomVM2);

        }

    }
}
