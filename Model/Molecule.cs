using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using HelixToolkit.Wpf.SharpDX;
using MolecularViewer;
using MolecularVisualizer.Helpers;
using MolecularVisualizer.MVVM;
using MolecularVisualizer.Services;
using SharpDX;

namespace MolecularVisualizer.Model
{
    public class Molecule : ViewModelBase
    {
        public Molecule(List<Atom> atoms)
        {
            foreach(Atom atom in atoms)
            {
                Atoms.Add(atom);
            }
            AddBonds();
        }

        //The two first and second indexes corresponds to the atoms bonded with the lowest and highest index respectevely. (index starts at zero)
        public List<Bond> Bonds = new List<Bond>();
        public List<Atom> Atoms { get; set; } = new List<Atom>();
        private string _name { get; set; } = null;
        public string Name
        {
            get 
            { 
                if (_name == null)
                    return Atoms.Count.ToString();
                else
                {
                    return _name;
                }
            }
            set { _name = value; }
        }
        public double Energy { get; set; } = double.NaN;




        private void AddBonds()
        {

            for (int i = 0; i < Atoms.Count; i++)
            {
                for (int j = i + 1; j < Atoms.Count; j++)
                {
                    Atom atom1 = Atoms[i];
                    Atom atom2 = Atoms[j];

                    MoleculeEditor MolEditor = new MoleculeEditor(this);
                    MolEditor.AddBondVDW(atom1, atom2);
                }
            }
        }
    }


}
