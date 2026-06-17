using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using MolecularVisualizer.Helpers;
using SharpDX;

namespace MolecularViewer
{
    public class Atom
    {
        public Atom(string element, Vector3 position, int index)
        {
            int atomicnumber;
            bool bAtomicnumber = int.TryParse(element, out atomicnumber);
            
            if (bAtomicnumber)
            {
                AtomicNumber = atomicnumber;
                Element = AtomBondHelper.DictPeriodicTable[AtomicNumber];
            }
            else
            {
                Element = element;
                for (int i = 1; i < AtomBondHelper.DictPeriodicTable.Count + 1; i++)
                    if (AtomBondHelper.DictPeriodicTable[i] == Element)
                        AtomicNumber = i;
            }
            Position = position;
            Radius = AtomBondHelper.GetRadius(Element);
            Color = ColorHelper.GetAtomColor(Element);
            Index = index;
        }



        public Color4 Color { get; set; }

        public int Index { get; set; }
        public double Radius { get; set; }

        public string Element { get; set; }
        public int AtomicNumber { get; set; }
        public Vector3 Position { get; set; }

    }
}
