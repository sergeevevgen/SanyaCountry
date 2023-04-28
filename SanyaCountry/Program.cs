using SanyaCountryBusinessLogic.BusinessLogic;
using SanyaCountryDatabaseImplement.Implements;
using SanyaCountryLogicContracts.BusinessLogicsContracts;
using SanyaCountryLogicContracts.StoragesContracts;
using Unity;
using Unity.Lifetime;

namespace SanyaCountry
{
    internal static class Program
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<IBuildingStorage,
            BuildingStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<ISettlementStorage, SettlementStorage>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<IBuildingLogic, BuildingLogic>(new
            HierarchicalLifetimeManager());

            currentContainer.RegisterType<ISettlementLogic, SettlementLogic>(new
            HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}