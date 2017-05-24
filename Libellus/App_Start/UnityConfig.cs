using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Libellus.DataAccess.Database;
using Libellus.BusinessCore.Processors.Interface;
using Libellus.BusinessCore.Processors.Implementation;
using Libellus.DataAccess.Repositories.Implementation;
using Libellus.DataAccess.Repositories.Interface;
using Libellus.DataAccess.UoW;
using System.Data.Entity.Infrastructure;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Libellus.Managers;
using Microsoft.AspNet.Identity;

namespace Libellus.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // Register types here
            container.RegisterType<LibellusDbContext>(new PerRequestLifetimeManager());
            //container.RegisterType<RoleManager<>>()

            container.RegisterType<UserCustomManager>(new InjectionFactory(c => CreateUserCustomManager()));
            container.RegisterType<SignInCustomManager>(new InjectionFactory(c => CreateSignInManager()));
            container.RegisterType<RoleCustomManager>(new InjectionFactory(c => CreateRoleManager()));

            container.RegisterType<IBaseProcessor, BaseProcessor>();
            container.RegisterType<IDepartmentProcessor, DepartmentProcessor>();
            container.RegisterType<IFacultyProcessor, FacultyProcessor>();
            container.RegisterType<IProjectProcessor, ProjectProcessor>();
            container.RegisterType<ISubscriptionProcessor, SubscriptionProcessor>();

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IFacultyRepository, FacultyRepository>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<ITaskRepository, TaskRepository>();
            container.RegisterType<ISubscriptionRepository, SubscriptionRepository>();
        }

        private static UserCustomManager CreateUserCustomManager()
        {
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<UserCustomManager>();
            manager.EmailService = new EmailService();
            return manager;
        }

        private static SignInCustomManager CreateSignInManager()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<SignInCustomManager>();
        }

        private static RoleCustomManager CreateRoleManager()
        {
            return HttpContext.Current.GetOwinContext().Get<RoleCustomManager>();
        }
    }
}
