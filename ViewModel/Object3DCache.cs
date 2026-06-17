using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HelixToolkit.Wpf.SharpDX;
using Media3D = System.Windows.Media.Media3D;
using System.Windows.Threading;
using MolecularVisualizer.Helpers;
using MolecularVisualizer.Model;
using MolecularVisualizer.Parser;
using SharpDX;
using SharpDX.Direct3D9;

namespace MolecularVisualizer.ViewModel
{
    public static class Object3DCache
    {
        public static readonly ConcurrentDictionary<string, MeshGeometry3D> atomGeometries = new ConcurrentDictionary<string, MeshGeometry3D>();
        public static readonly ConcurrentDictionary<Color4, PhongMaterial> materials = new ConcurrentDictionary<Color4, PhongMaterial>();

        public static readonly List<MeshGeometry3D> cacheBonds = new List<MeshGeometry3D>();

        public static void GenerateCache()
        {
            atomGeometries.Clear();
            materials.Clear();
            cacheBonds.Clear();

        }

        public static MeshGeometry3D GetAtomGeometry(string element)
        {
            return atomGeometries.GetOrAdd(element, e =>
            {
                var radius = AtomBondHelper.GetRadius(e);
                var builder = new MeshBuilder();
                builder.AddSphere(new Vector3(0, 0, 0), radius, 32, 32);
                return builder.ToMeshGeometry3D();
            });
        }

        public static PhongMaterial GetMaterial(Color4 color)
        {
            var material = materials.GetOrAdd(color, c => new PhongMaterial
            {
                DiffuseColor = new Color(c.Red, c.Green, c.Blue),
                SpecularColor = Color.White,
                SpecularShininess = 100f,
                
            });

            return material;
        }

        public static MeshGeometry3D GetBondGeometry(Vector3 start, Vector3 end)
        {
            var direction = (end - start);
            var length = Vector3.Distance(start, end);
            var midpoint = (start + end) / 2;

            var builder = new MeshBuilder();
            builder.AddCylinder(start, end,
                GlobalVariables.BondRadius, 3);

            return builder.ToMeshGeometry3D();
        }
        private static MeshGeometry3D CreateBondTemplate()
        {
            var builder = new MeshBuilder();
            builder.AddCylinder(new Vector3(0,0,0), new Vector3(1, 0, 0),
                0.1f, 8); // Default radius
            return builder.ToMeshGeometry3D();
            
        }
    }
}
