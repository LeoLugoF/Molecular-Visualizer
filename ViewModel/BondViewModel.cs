using System.Windows.Media.Media3D;
using System;
using HelixToolkit.Wpf.SharpDX;
using MolecularViewer;
using MolecularVisualizer.Helpers;
using MolecularVisualizer.Model;
using HelixToolkit.Wpf;
using SharpDX;

namespace MolecularVisualizer.ViewModel
{
    public class BondViewModel : Object3DViewModel
    {

        public Bond Bond { get; }

        public BondViewModel(Bond bond)
        {
            Bond = bond;


            var bondBuilder = new MeshBuilder();
            bondBuilder.AddCylinder(Bond.Atom1.Position, Bond.Atom2.Position,
                GlobalVariables.BondRadius, GlobalVariables.GraphicResolution);

            Geometry = bondBuilder.ToMeshGeometry3D();
            
            //Geometry = Object3DCache.GetBondTemplate();
            ObjectMaterial = Object3DCache.GetMaterial(Color.LightGray);
            ObjectMaterial.Freeze();
            
            //Transform = CreateBondTransform(Bond.Atom1.Position, Bond.Atom2.Position, GlobalVariables.BondRadius);
        }

        public static Transform3D CreateBondTransform(Vector3 start, Vector3 end, double radius)
        {
            var direction = end - start;
            float length = direction.Length();
            direction.Normalize();

            var transformGroup = new Transform3DGroup();

            // 1. Scale first (X=length, Y/Z=radius)
            transformGroup.Children.Add(new ScaleTransform3D(length, radius, radius));

            // 2. Then rotate from X-axis to bond direction
            Vector3 axis = Vector3.Cross(Vector3.UnitX, direction);
            double angle = Math.Acos(Vector3.Dot(Vector3.UnitX, direction)) * 180 / Math.PI;
            transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(axis.ToVector3D(), angle)));

            // 3. Finally translate to start position
            transformGroup.Children.Add(new TranslateTransform3D(start.ToVector3D()));

            return transformGroup;
        }
    }
}
