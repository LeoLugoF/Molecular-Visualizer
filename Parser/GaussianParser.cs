using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MolecularViewer;
using MolecularVisualizer.Model;

namespace MolecularVisualizer.Parser
{
    public class SoftwareGaussianParser : IFileParser
    {
        public bool CanParse(string filePath)
        {
            System.IO.StreamReader File = new System.IO.StreamReader(filePath);
            bool Readable = false;
            string line;
            while ((line = File.ReadLine()) != null)
            {
                //gaussian File
                if (line.Contains("Gaussian") && line.Contains("System") && line.Contains("Link"))
                {
                    Readable = true;
                    break;
                }
            }
            File.Close();
            return Readable;
        }

        public ObservableCollection<Job> GetJobs(string filePath)
        {
            List<List<double>> XYZMatrix = new List<List<double>>();
            System.IO.StreamReader File = new System.IO.StreamReader(filePath);
            string line;

            while ((line = File.ReadLine()) != null)
            {
                if (line.Contains("----------------------------------------------------------"))
                {
                    line = File.ReadLine();
                    if (line.Contains("tomic") && line.Contains("oordinates") && line.Contains("enter"))
                    {
                        line = File.ReadLine();
                        // Center Number   Atomic  Number       Type             X           Y           Z
                        {
                            if (line.Contains("X") && line.Contains("Y") && line.Contains("Z") && line.Contains("Type") && line.Contains("Number"))
                            {
                                XYZMatrix.Clear();
                                line = File.ReadLine();
                                //  ---------------------------------------------------------------------
                                while (!(line = File.ReadLine()).Contains("----------------------------------------------------------"))
                                {
                                    var LineValueList = line.Split(' ').ToList().Where(s => !string.IsNullOrEmpty(s)).ToList();
                                    XYZMatrix.Add(new List<double> {
                                            double.Parse(LineValueList[3]), double.Parse(LineValueList[4]), double.Parse(LineValueList[5])
                                            });
                                }
                            }
                        }
                    }

                }
            }
            File.Close();
            return null;
        }


    }

}
