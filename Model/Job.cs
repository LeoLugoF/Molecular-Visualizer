using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MolecularVisualizer.MVVM;

namespace MolecularVisualizer.Model
{
    public class Job : ViewModelBase
    {

        public string Name { get; set; }

        public ObservableCollection<Molecule> Molecules { get; set; } = new ObservableCollection<Molecule>();

    }
}
