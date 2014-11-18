[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DigitalLibrary.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DigitalLibrary.Web.App_Start.NinjectWebCommon), "Stop")]

namespace DigitalLibrary.Web.App_Start
{
    using System;
    using System.Web;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Infrastructure.Cashing;
    using DigitalLibrary.Web.Infrastructure.Populators;
    using DigitalLibrary.Web.Infrastructure.Services;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
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
            kernel.Bind<IDigitalLibraryDbContext>().To<DigitalLibraryDbContext>();
            kernel.Bind<IDigitalLibraryData>().To<DigitalLibraryData>();

            kernel.Bind<IHomeServices>().To<HomeServices>();
            kernel.Bind<IAuthorService>().To<AuthorServices>();
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<ILikeService>().To<LikeService>();
            kernel.Bind<ITrustedUserService>().To<TrustedUserService>();
            kernel.Bind<IWorkService>().To<WorkService>();
            kernel.Bind<ICacheService>().To<InMemoryCache>();
            kernel.Bind<IDropDownListPopulator>().To<DropDownListPopulator>();
        }        
    }
}
