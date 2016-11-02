using Aimp.DataAccess.Ef;
using Aimp.DataAccess.Interfaces;
using Microsoft.Practices.Unity;
using System;

namespace Aimp.Domain
{
    public static class IoC
    {
        private static UnityContainer _container;

        static IoC()
        {
            _container = new UnityContainer();
        }
        public static void Register<TTo, TFrom>(bool singleton = true) 
            where TFrom : TTo
        {
            if (_container.IsRegistered<TTo>())
                throw new ArgumentException($"Type {typeof(TTo)} is alredy registred");

            if (singleton)
                _container.RegisterType<TTo, TFrom>(new ContainerControlledLifetimeManager());
            else
                _container.RegisterType<TTo, TFrom>();
        }
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
