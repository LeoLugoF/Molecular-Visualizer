using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Media3D = System.Windows.Media.Media3D;
using MolecularViewer;
using MolecularVisualizer.Model;
using MolecularVisualizer.ViewModel;
using SharpDX;

namespace MolecularVisualizer.Helpers
{
    public static class AtomBondHelper
    {

        public static float GetDistance(Vector3 atom1, Vector3 atom2)
        {
            return Vector3.Distance(atom1, atom2);
        }

        public static float GetRadius(string element)
        {
            switch (element)
            {
                case "H": return 0.2f;
                case "O": return 0.3f;
                case "C": return 0.25f;
                case "N": return 0.28f;
                case "P": return 0.3f;
                case "B": return 0.25f;
                default: return 0.22f;
            }
        }

        public static float vanderWaalsDistance(string atom1, string atom2)
        {
            return vanderWaalsRadius(atom1) + vanderWaalsRadius(atom2);
        }

        public static float vanderWaalsRadius(string element)
        {
            switch (element)
            {
                case "H": return 1.10f;
                case "O": return 1.52f;
                case "C": return 1.70f;
                case "N": return 1.55f;
                case "P": return 1.8f;
                case "B": return 1.92f;
                default: return 1.55f;
            }
        }

        public static Color4 GetAtomColor(string element)
        {
            switch (element)
            {
                case "H": return Color.White;
                case "O": return Color.Red;
                case "C": return Color.Gray;
                case "N": return Color.Blue;
                case "P": return Color.DarkBlue;
                case "B": return Color.Green;
                default: return Color.Green;
            }
        }

        public static int? GetAtomicNumber(string element)
        {
            int? atomicNumber = null;
            for (int i = 1; i < DictPeriodicTable.Count + 1; i++)
                if (DictPeriodicTable[i] == element)
                    atomicNumber = i;

            return atomicNumber;
        }

        public static string GetElement(int atomicNumber)
        {
            var element = DictPeriodicTable[atomicNumber];
            return element;
        }

        public static Dictionary<int, string> DictPeriodicTable = new Dictionary<int, string>()
        {
            {1,"H"},{2,"He"},{3,"Li"},{4,"Be"},{5,"B"},{6,"C"},{7,"N"},{8,"O"},{9,"F"},{10,"Ne"},
            {11,"Na"},{12,"Mg"},{13,"Al"},{14,"Si"},{15,"P"},{16,"S"},{17,"Cl"},{18,"Ar"},{19,"K"},
            {20,"Ca"},{21,"Sc"},{22,"Ti"},{23,"V"},{24,"Cr"},{25,"Mn"},{26,"Fe"},{27,"Co"},{28,"Ni"},
            {29,"Cu"},{30,"Zn"},{31,"Ga"},{32,"Ge"},{33,"As"},{34,"Se"},{35,"Br"},{36,"Kr"},{37,"Rb"},
            {38,"Sr"},{39,"Y"},{40,"Zr"},{41,"Nb"},{42,"Mo"},{43,"Tc"},{44,"Ru"},{45,"Rh"},{46,"Pd"},
            {47,"Ag"},{48,"Cd"},{49,"In"},{50,"Sn"},{51,"Sb"},{52,"Te"},{53,"I"},{54,"Xe"},{55,"Cs"},
            {56,"Ba"},{57,"La"},{58,"Ce"},{59,"Pr"},{60,"Nd"},{61,"Pm"},{62,"Sm"},{63,"Eu"},{64,"Gd"},
            {65,"Tb"},{66,"Dy"},{67,"Ho"},{68,"Er"},{69,"Tm"},{70,"Yb"},{71,"Lu"},{72,"Hf"},{73,"Ta"},
            {74,"W"},{75,"Re"},{76,"Os"},{77,"Ir"},{78,"Pt"},{79,"Au"},{80,"Hg"},{81,"Tl"},{82,"Pb"},
            {83,"Bi"},{84,"P"},{85,"At"},{86,"Rn"},{87,"Fr"},{88,"Ra"},{89,"Ac"},{90,"Th"},{91,"Pa"},
            {92,"U"},{93,"Np"},{94,"Pu"},{95,"Am"},{96,"Cm"},{97,"Bk"},{98,"Cf"},{99,"Es"},{100,"Fm"},
            {101,"Md"},{102,"No"},{103,"Lr"},{104,"Rf"},{105,"Db"},{106,"Sg"},{107,"Bh"},{108,"Hs"},
            {109,"Mt"},{110,"Ds"},{111,"Rg"},{112,"Cn"},{113,"Nh"},{114,"Fl"},{115,"Mc"},{116,"Lv"},
            {117,"Ts"},{118,"Og"}
        };
    }
}
