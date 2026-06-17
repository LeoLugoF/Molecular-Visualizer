using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MolecularVisualizer.Parser;
using Microsoft.Win32;
using System.Windows;

namespace MolecularVisualizer.View
{
    public partial class MainWindow : Window
    {
        public void SetUpMainHandling()
        {
            vm.SceneCommands.LoadMoleculeFromFile += (_, e) => OpenMoleculeFileDialog();
        }


        private void OpenMoleculeFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool? success = ofd.ShowDialog();
            if (success == true)
                if (!string.IsNullOrEmpty(ofd.FileNames[0]))
                    vm.MoleculeVM.ParseMoleculeFromFile(ofd.FileName);
            
        }

    }
}
