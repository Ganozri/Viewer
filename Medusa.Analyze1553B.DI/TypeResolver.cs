using System;
using StructureMap;

namespace Medusa.Analyze1553B.DI
{
    public class TypeResolver
    {
        public static object Resolve(Type type) => Default.Resolve(type);

        public static T Resolve<T>() where T : class => Default.Resolve<T>();

        public static IContainer Container => Default?.Container;

        public static TypeResolverInstance CreateNew()
        {
            return new TypeResolverInstance(Default.Container);
        }

        private static readonly TypeResolverInstance Default = new TypeResolverInstance(null);
    }

    public class TypeResolverInstance : IDisposable
    {
        internal TypeResolverInstance(IContainer parentContainer)
        {
            Container = parentContainer?.CreateChildContainer() ?? new Container();
        }

        public object Resolve(Type type)
        {
            return Container.GetInstance(type);
        }

        public T Resolve<T>() where T : class
        {
            return Container.GetInstance<T>();
        }

        public IContainer Container { get; }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
