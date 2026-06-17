using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixToolkit.Wpf.SharpDX;
using MolecularVisualizer.MVVM;
using Media3D = System.Windows.Media.Media3D;
using SharpDX;

namespace MolecularVisualizer.ViewModel
{
    public class Object3DViewModel: ViewModelBase
    {

        private PhongMaterial material { get; set; }

        public PhongMaterial ObjectMaterial
        {
            get
            {
                if (IsSelected)
                    return SelectedMaterial;
                else
                    return material;
            }
            set
            {
                material = value;
            }
        }

        public MeshGeometry3D Geometry { get; set; }
        public Media3D.Transform3D Transform { get; set; }

        public PhongMaterial SelectedMaterial
        {
            get
            {
                PhongMaterial selectedmaterial = new PhongMaterial
                {
                    DiffuseColor = Color.Yellow,
                    //SpecularColor = Color.White,
                    //SpecularShininess = 100f
                };
                return selectedmaterial;
            }
        }
        private bool isSelected { get; set; }
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(ObjectMaterial));
            }
        }

    }
}
