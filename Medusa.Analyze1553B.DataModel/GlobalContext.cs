using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Olympus.DynamicAssemblyLoader;
using Medusa.Analyze1553B.Common;
namespace Medusa.Analyze1553B.DataModel
{
    public class GlobalContext
    {
        public void LoadAssemblies(EventHandler<AssemblyLoadProgressEventArgs> onProgress)
        {
            AssemblyLoader.AssemblyLoadProgress += onProgress;
            var assemblies = AssemblyLoader.LoadNeighborAssembliesRecursive(typeof(GlobalContext).Assembly).ToArray();
            AssemblyLoader.AssemblyLoadProgress -= onProgress;

            DataLoaders = AssemblyLoader
                .EnumerateDerivedTypes<IDataLoader>(EnumerateOptions.ParameterlessConstructor, assemblies)
                .CreateInstances<IDataLoader>();

            ProductLoaders = AssemblyLoader
                .EnumerateDerivedTypes<IProductLoader>(EnumerateOptions.ParameterlessConstructor, assemblies)
                .CreateInstances<IProductLoader>();
        }

        public IObjectsHolder<IProductLoader> ProductLoaders { get; set; }

        public IObjectsHolder<IDataLoader> DataLoaders { get; private set; }
    }
}
