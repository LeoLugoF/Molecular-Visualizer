using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using MolecularViewer;
using MolecularVisualizer.Model;
using SharpDX;
using SharpDX.Direct2D1.Effects;

namespace MolecularVisualizer.Parser
{
    public class XYZParser : IFileParser
    {

        public bool CanParse(string filePath)
        {
            if (filePath.Contains(".xyz"))
                return true;
            else 
                return false;
        }

        public ObservableCollection<Job> GetJobs(string filePath)
        {

            var Molecules = GetMolecules(filePath);

            if (Molecules == null) return null;

            var jobCollection = new ObservableCollection<Job>();
            
            var Job = new Job() { Name = "XYZ File" };
            Job.Molecules = Molecules;
            jobCollection.Add(Job);
            return jobCollection;
        }

        private ObservableCollection<Molecule> GetMolecules(string filePath)
        {
            var Molecules = new ObservableCollection<Molecule>();

            List<string> fileLines = File.ReadAllLines(filePath).ToList();
            
            if (fileLines.Count == 0) return null;

            if (IsSingleXYZ(fileLines))
            {
                Molecule mol = GetSingleXYZ(fileLines);
                Molecules.Add(mol);
            }
            else
            {
                Molecules = GetMultipleXYZ(fileLines);
            }

                return Molecules;
        }

        private bool IsSingleXYZ(List<string> fileLines)
        {
            bool bFirstXYZFound = false;

            foreach (var line in fileLines)
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (string.IsNullOrEmpty(line)) continue;
                if (!bFirstXYZFound && parts.Length != 4) continue;
                if (!bFirstXYZFound && parts.Length == 4)
                {
                    bFirstXYZFound = true;
                    continue;
                }

                if (parts.Length != 4) return false;
            }
            return true;

        }

        private Molecule GetSingleXYZ(List<string> fileLines)
        {
            List<Atom> Atoms = new List<Atom>();
            int AtomIndex = 0;
            foreach (var line in fileLines)
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 4) continue;
                if (parts.Length == 4 || CanConvertToFloatLastElements(parts))
                {
                    //XYZ Coordinate found
                    string Element = parts[0];
                    Vector3 Position = new Vector3(float.Parse(parts[1])
                        , float.Parse(parts[2]), float.Parse(parts[3]));
                    Atom atom = new Atom(Element, Position, AtomIndex);

                    Atoms.Add(atom);
                    AtomIndex += 1;
                }
            }
            return new Molecule(Atoms);
        }

        private ObservableCollection<Molecule> GetMultipleXYZ(List<string> fileLines)
        {
            ObservableCollection<Molecule> molecules = new ObservableCollection<Molecule>();
            List<Atom> Atoms = new List<Atom>();
            int AtomIndex = 0;
            string MoleculeTitle = "";

            for(int i = 0; i < fileLines.Count; i++)
            {
                string line = fileLines[i];
                if (string.IsNullOrEmpty(line)) continue;


                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 4 || CanConvertToFloatLastElements(parts))
                {
                    //XYZ Coordinate found
                    string Element = parts[0];
                    Vector3 Position = new Vector3(float.Parse(parts[1])
                        , float.Parse(parts[2]), float.Parse(parts[3]));
                    Atom atom = new Atom(Element, Position, AtomIndex);

                    Atoms.Add(atom);
                    AtomIndex += 1;
                }
                else
                {
                    //Create found molecule if there were atoms found.
                    if (AtomIndex != 0)
                    {
                        Molecule mol = new Molecule(Atoms);
                        mol.Name = MoleculeTitle;            
                        molecules.Add(mol);
                        Atoms.Clear();
                        AtomIndex = 0;
                        MoleculeTitle = "";
                    }

                    //Label found
                    if (MoleculeTitle == "")
                    {


                        string NextLine = fileLines[i + 1];
                        var parts2 = NextLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts2.Length == 4 || CanConvertToFloatLastElements(parts2))
                        {
                            MoleculeTitle = line;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(NextLine))
                                MoleculeTitle = line;
                            else
                            {
                                MoleculeTitle = NextLine;
                            }
                        }
                    }
                }
            }
            if (AtomIndex != 0)
            {
                Molecule mol = new Molecule(Atoms);
                mol.Name = MoleculeTitle;
                molecules.Add(mol);
                Atoms.Clear();
                AtomIndex = 0;
                MoleculeTitle = "";
            }
            return molecules;

        }

        private bool CanConvertToFloatLastElements(string[] XYZline)
        {
            if(XYZline.Length > 1)
            {
                for(int i = 1; i < XYZline.Length; i++)
                {
                    double Coordinate;
                    if (!double.TryParse(XYZline[i], out Coordinate))
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
