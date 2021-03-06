﻿using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;

namespace BLL.Services
{
    public class ServiceModule:NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
