using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Store.Domain.Abstractions;
using Store.Nhibernate;

namespace Store.Web.Infrastructure
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer _container;
        public UnityControllerFactory()
        {
            _container = new UnityContainer();
            RegisterTypes();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_container.Resolve(controllerType);
        }

        private void RegisterTypes()
        {
            _container.RegisterType<IUnitOfWorkFactory, NHUnitOfWorkFactory>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IUnitOfWork>(new InjectionFactory(c => c.Resolve<IUnitOfWorkFactory>().Create()));

            _container.RegisterType<IAccountRepository, AccountRepository>();
        }
    }
}