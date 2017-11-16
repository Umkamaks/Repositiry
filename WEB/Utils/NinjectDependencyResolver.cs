using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Services;
using Ninject;

namespace WEB.Utils
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }



        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<INewsService>().To<NewsService>();
            kernel.Bind<IIdentityServices>().To<IdentityServices>();
        }
    }
}