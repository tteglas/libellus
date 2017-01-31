using Libellus.BusinessCore.Processors.Implementation;
using Libellus.BusinessCore.Processors.Interface;
using Libellus.DataAccess.Injector;
using Ninject.Modules;
using System;
using System.Collections.Generic;

namespace Libellus.BusinessCore.Injector
{
    public class CoreInjector : NinjectModule
    {
        private readonly Func<Ninject.Activation.IContext, object> _scope;

        public CoreInjector(Func<Ninject.Activation.IContext, object> scope)
        {
            _scope = scope;
        }

        public override void Load()
        {
            Kernel.Load(new List<NinjectModule> { new DataAccessInjector(_scope) });

            Bind<IFacultyProcessor>().To<FacultyProcessor>().InScope(_scope);
            Bind<IProjectProcessor>().To<ProjectProcessor>().InScope(_scope);
            Bind<IDepartmentProcessor>().To<DepartmentProcessor>().InScope(_scope);

        }
    }
}
