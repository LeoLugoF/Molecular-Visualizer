using System.Windows.Media.Media3D;
using HelixToolkit.Wpf.SharpDX;
using MolecularViewer;
using SharpDX;

namespace MolecularVisualizer.ViewModel
{
    public class AtomViewModel : Object3DViewModel
    {

        public Atom Atom { get; }

        public AtomViewModel(Atom atom)
        {
            Atom = atom;

            Geometry = Object3DCache.GetAtomGeometry(atom.Element);
            ObjectMaterial = Object3DCache.GetMaterial(atom.Color);
            Transform = new TranslateTransform3D(atom.Position.ToVector3D());
            ObjectMaterial.Freeze();

            
            
        }



    }


}
