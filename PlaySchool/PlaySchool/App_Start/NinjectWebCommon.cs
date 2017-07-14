using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PlaySchool.Data;
using PlaySchool.Services;
using PlaySchool.Services.Interfaces;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PlaySchool.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PlaySchool.App_Start.NinjectWebCommon), "Stop")]

namespace PlaySchool.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Microsoft.AspNet.Identity;
    using Models.EntityModels;
    using Microsoft.AspNet.Identity.EntityFramework;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IManageAccountService>().To<ManageAccountService>();
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<IAdminHomeService>().To<AdminHomeService>();
            kernel.Bind<DbContext>().To<PlaySchoolContext>();
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>> ();
            kernel.Bind<UserManager<ApplicationUser>>().ToSelf();

            kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();

            kernel.Bind<ApplicationSignInManager>().ToMethod((context) =>
            {
                var cbase = new HttpContextWrapper(HttpContext.Current);
                return cbase.GetOwinContext().Get<ApplicationSignInManager>();
            });

            kernel.Bind<ApplicationUserManager>().ToSelf();
            //kernel.Bind<PlaySchoolContext>().To<PlaySchoolContext>();
        }        
    }
}
