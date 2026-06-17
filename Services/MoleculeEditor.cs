using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixToolkit.Wpf.SharpDX;
using MolecularViewer;
using MolecularVisualizer.Helpers;
using MolecularVisualizer.Model;
using SharpDX;

namespace MolecularVisualizer.Services
{
    public class MoleculeEditor
    {
        private readonly Molecule _molecule;

        public MoleculeEditor(Molecule molecule)
        {
            _molecule = molecule;
        }

        public void AddBondVDW(Atom atom1, Atom atom2)
        {
            var pos1 = atom1.Position;
            var pos2 = atom2.Position;

            float distance = AtomBondHelper.GetDistance(pos1, pos2);
            float vanderwaalsdistance = AtomBondHelper.vanderWaalsDistance(
                atom1.Element,
                atom2.Element);


            if (distance < 2 * vanderwaalsdistance / 3)
            {
                _molecule.Bonds.Add(new Bond(atom1, atom2));
            }
        }



    }
}
