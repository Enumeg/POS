using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using POS.Domain.Interfaces;
using POS.Domain.Services;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using POS.Domain.Infrastructure;
using System.Web.Http;
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(POS.Portal.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(POS.Portal.App_Start.NinjectWebCommon), "Stop")]

namespace POS.Portal.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
                GlobalConfiguration.Configuration.DependencyResolver = kernel.Get<System.Web.Http.Dependencies.IDependencyResolver>();

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
            kernel.Bind<PosContext>().ToProvider(new Helpers.PosContextWebProvider());
            kernel.Bind<IShiftsService>().To<ShiftsService>();
            kernel.Bind<IMachinesService>().To<MachinesService>();
            kernel.Bind<ISettingsService>().To<SettingsService>();
            kernel.Bind<ICategoriesService>().To<CategoriesService>();
            kernel.Bind<IUnitsService>().To<UnitsService>();
            kernel.Bind<IPropertiesService>().To<PropertiesService>();
            kernel.Bind<IPeopleService>().To<PeopleService>();
            kernel.Bind<IPointsService>().To<PointsService>();
            kernel.Bind<IProductsService>().To<ProductsService>();
            kernel.Bind<ITransactionsService>().To<TransactionsService>();
            kernel.Bind<IBanksService>().To<BanksService>();
            kernel.Bind<IBankAccountsService>().To<BankAccountsService>();
            kernel.Bind<IStockService>().To<StockService>();
            kernel.Bind<IIncomesService>().To<IncomesService>();
            kernel.Bind<IExpensesService>().To<ExpensesService>();
            kernel.Bind<ISafeService>().To<SafeService>();
        }
    }
}
