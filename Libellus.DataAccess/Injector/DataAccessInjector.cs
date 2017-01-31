using Libellus.DataAccess.Database;
using Libellus.DataAccess.Repositories.Implementation;
using Libellus.DataAccess.Repositories.Interface;
using Libellus.DataAccess.UoW;
using Ninject.Modules;
using System;
using System.Data.Entity.Infrastructure;

namespace Libellus.DataAccess.Injector
{
    public class DataAccessInjector : NinjectModule
    {
        private readonly Func<Ninject.Activation.IContext, object> _scope;

        public DataAccessInjector(Func<Ninject.Activation.IContext, object> scope)
        {
            _scope = scope;
        }

        public override void Load()
        {
            Bind<IObjectContextAdapter>().To<LibellusDbContext>().InScope(_scope);
            Bind<IUnitOfWork>().To<UnitOfWork>().InScope(_scope);

            //todo: not sure if the context to bind to is required
            Bind<IFacultyRepository>().To<FacultyRepository>().InScope(_scope);
            Bind<IDepartmentRepository>().To<DepartmentRepository>().InScope(_scope);
            Bind<IProjectRepository>().To<ProjectRepository>().InScope(_scope);
            Bind<ITaskRepository>().To<TaskRepository>().InScope(_scope);
            Bind<IFacultyRoleRepository>().To<FacultyRoleRepository>().InScope(_scope);
        }
    }
}
