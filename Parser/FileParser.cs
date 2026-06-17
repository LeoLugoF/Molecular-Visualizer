using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MolecularViewer;
using MolecularVisualizer.Model;

namespace MolecularVisualizer.Parser
{
    public interface IFileParser
    {
        bool CanParse(string filePath);
        ObservableCollection<Job> GetJobs(string filePath);

    }

    public class FileParsers
    {
        private readonly List<IFileParser> _parsers;

        public FileParsers()
        {
            _parsers = new List<IFileParser>
            {
                new SoftwareGaussianParser(),
                new XYZParser(),
                //new OrcaParser()
                //new SoftwareBParser(),
                // Agrega más aquí...
            };
        }

        public IFileParser GetParser(string filePath)
        {
            return _parsers.FirstOrDefault(p => p.CanParse(filePath));
        }
    }


}
