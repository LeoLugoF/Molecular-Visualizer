using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MolecularViewer;

namespace MolecularVisualizer.Model
{
    public class Bond
    {
        //Set by the lower and higher indexes.
        public Atom Atom1 { get; set; }
        public Atom Atom2 { get; set; }
        
        public Bond(Atom atom1, Atom atom2)
        {
            int IndexA1 = atom1.Index;
            int IndexA2 = atom2.Index;

            if (IndexA1 > IndexA2)
            {
                Atom1 = atom2;
                Atom2 = atom1;
            }
            else
            {
                Atom1 = atom1;
                Atom2 = atom2;
            }


        }

        public bool IsBetween(Atom atom1, Atom atom2)
        {
            Atom LowestIndexAtom1; Atom HighestIndexAtom2;
            //Get which atom has the lowest index
            int IndexA1 = atom1.Index;
            int IndexA2 = atom2.Index;
            if (IndexA1 > IndexA2)
            {
                LowestIndexAtom1 = atom2;
                HighestIndexAtom2 = atom1;
            }
            else
            {
                LowestIndexAtom1 = atom1;
                HighestIndexAtom2 = atom2;
            }
            if (Atom1 == LowestIndexAtom1 
                && Atom2 == HighestIndexAtom2) return true;
            else return false;
        }

    }
}
