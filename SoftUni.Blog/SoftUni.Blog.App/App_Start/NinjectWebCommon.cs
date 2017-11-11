[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SoftUni.Blog.App.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SoftUni.Blog.App.App_Start.NinjectWebCommon), "Stop")]

namespace SoftUni.Blog.App.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Data.UnitOfWork;
    using Data;
    using Microsoft.AspNet.Identity;
    using Blog.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;

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
            kernel.Bind<ISoftUniBlogData>().To<SoftUniBlogData>()
                .InRequestScope()
                .WithConstructorArgument("context", p => new BlogDbContext());
            kernel.Bind<IUserStore<User>>().To<UserStore<User>>()
                .InRequestScope()
                .WithConstructorArgument("context", kernel.Get<BlogDbContext>());
            //kernel.Bind<IAuthenticationManager>().To<>();
            kernel.Bind<IAuthenticationManager>()
                .ToMethod<IAuthenticationManager>(context => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
        }
    }
}
