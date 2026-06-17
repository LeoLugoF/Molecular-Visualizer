using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MolecularViewer;
using MolecularVisualizer.Helpers;
using MolecularVisualizer.Model;
using MolecularVisualizer.MVVM;
using MolecularVisualizer.Parser;
using MolecularVisualizer.Services;


namespace MolecularVisualizer.ViewModel
{
    public class MoleculeViewModel : ViewModelBase
    {
        public RelayCommand OnNextMolecule => new RelayCommand(execute => NextMolecule());
        public RelayCommand OnDebugMolecule => new RelayCommand(execute => AddDebugMolecule());
        public event EventHandler ClearVieport;

        public MoleculeViewModel()
        {
            
        }


        protected void InvokeEvent(EventHandler eventHandler, EventArgs args = null)
        {
            // Thread-safe invocation with null check
            eventHandler?.Invoke(this, args ?? EventArgs.Empty);
        }

        public void NextMolecule()
        {
            Jobs.Clear();
            var firstmolecule = Jobs[0].Molecules.FirstOrDefault();
            var lastmolecule = Jobs[0].Molecules.LastOrDefault();

            if(SelectedMolecule == firstmolecule) SelectedMolecule = lastmolecule;
            else SelectedMolecule = firstmolecule;
        }

        public ObservableCollection<Job> Jobs { get; set; } = new ObservableCollection<Job>();
        public List<Molecule> molecules { get; set; } = new List<Molecule>();

        private Molecule selectedMolecule;
        public Molecule SelectedMolecule
        {
            get => selectedMolecule;
            set
            {
                selectedMolecule = value;

                Debug.WriteLine("----New Molecule---");
                Update3DViewModels(selectedMolecule);


                Debug.WriteLine($"Total atom geometries cached: {Object3DCache.materials.Count}");
                Debug.WriteLine($"Total bond geometries cached: {Object3DCache.atomGeometries.Count}");

            }
        }

        public ObservableCollection<AtomViewModel> AtomModels { get; set; } = new ObservableCollection<AtomViewModel>();
        public ObservableCollection<BondViewModel> BondModels { get; set; } = new ObservableCollection<BondViewModel>();

        public ObservableCollection<Object3DViewModel> selectedObjects { get; } = new ObservableCollection<Object3DViewModel>();
        public EventHandler HideViewport;

        public void ParseMoleculeFromFile(string filePath)
        {
            ClearSceneObjects();
            FileParsers Parsers = new FileParsers();
            IFileParser Parser = Parsers.GetParser(filePath);

            foreach (Job job in Parser.GetJobs(filePath))
                Jobs.Add(job);

            SelectedMolecule = Jobs[0].Molecules.LastOrDefault();

            var firstmolecule = Jobs[0].Molecules.FirstOrDefault();
            var lastmolecule = Jobs[0].Molecules.LastOrDefault();

            molecules.Add(firstmolecule);
            molecules.Add(lastmolecule);
        }

        private void ClearSceneObjects()
        {
            Jobs.Clear();
            selectedObjects.Clear();
            AtomModels.Clear();
            BondModels.Clear();
        }

        private void AddDebugMolecule()
        {
            ClearSceneObjects();

            Atom atom1 = new Atom("H", new SharpDX.Vector3(0, 0, 0), 0);
            Atom atom2 = new Atom("H", new SharpDX.Vector3(0, 0, 1), 1);
            Molecule mol = new Molecule(new List<Atom>(){ atom1, atom2 });
            mol.Name = "Debug";
            mol.Energy = 0.001;

            Job job = new Job();
            job.Molecules.Add(mol);
            job.Name = "Debug";
            Jobs.Add(job);

            SelectedMolecule = Jobs[0].Molecules.LastOrDefault();

            var firstmolecule = Jobs[0].Molecules.FirstOrDefault();
            var lastmolecule = Jobs[0].Molecules.LastOrDefault();

            molecules.Add(firstmolecule);
            molecules.Add(lastmolecule);
        }


        private void Update3DViewModels(Molecule molecule)
        {

            selectedObjects.Clear();
            var sw = Stopwatch.StartNew();
            ObservableCollection<AtomViewModel> newatoms = new ObservableCollection<AtomViewModel>();
            foreach (var atom in molecule.Atoms)
                newatoms.Add(new AtomViewModel(atom));
            AtomModels = newatoms;


            ObservableCollection<BondViewModel> newbonds = new ObservableCollection<BondViewModel>();
            foreach (var bond in molecule.Bonds)
                newbonds.Add(new BondViewModel(bond));
            BondModels = newbonds;
            Debug.WriteLine($"Time reassing mesh: {sw.ElapsedMilliseconds}ms");

            InvokeEvent(HideViewport);
            sw = Stopwatch.StartNew();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Render, (Action)(() => 
            {
                OnPropertyChanged(nameof(AtomModels));
                OnPropertyChanged(nameof(BondModels));
                Debug.WriteLine($"Time For changing visuals: {sw.ElapsedMilliseconds}ms");
            }));

            InvokeEvent(ClearVieport);
        }

        public void AddRemoveBond(AtomViewModel atomvm1, AtomViewModel atomvm2)
        {
            Atom Atom1 = atomvm1.Atom; Atom Atom2 = atomvm2.Atom;

            BondViewModel BondTemplate = null;

            foreach(var BondVModel in BondModels)
            {
                if(BondVModel.Bond.IsBetween(Atom1, Atom2))
                {
                    BondTemplate = BondVModel;
                    break;
                }
            }

            if (BondTemplate != null)
            {
                //SelectedMolecule.Bonds.Add(NewBond);
                //Commenting the above line does not modifies the internal objects of the molecule.
                BondModels.Remove(BondTemplate);
            }
            else
            {
                //SelectedMolecule.Bonds.Add(NewBond);
                Bond NewBond = new Bond(Atom1,Atom2);
                BondModels.Add(new BondViewModel(NewBond));
            }

        }

    }
}
