using SanyaCountryBusinessLogic.BusinessLogic;
using SanyaCountryBusinessLogic.OfficePackage;
using SanyaCountryBusinessLogic.OfficePackage.Implements;
using SanyaCountryContracts.Attributes;
using SanyaCountryContracts.BusinessLogicsContracts;
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

            currentContainer.RegisterType<IReportLogic, ReportLogic>(new
            HierarchicalLifetimeManager());


            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new
            HierarchicalLifetimeManager());

            return currentContainer;
        }

        public static void ConfigGrid<T>(List<T> data, DataGridView grid)
        {
            var type = typeof(T);
            var config = new List<string>();
            grid.Columns.Clear();
            foreach (var prop in type.GetProperties())
            {
                // получаем список атрибутов
                var attributes =
                prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    foreach (var attr in attributes)
                    {
                        // ищем нужный нам атрибут
                        if (attr is ColumnAttribute columnAttr)
                        {
                            config.Add(prop.Name);
                            var column = new DataGridViewTextBoxColumn
                            {
                                Name = prop.Name,
                                ReadOnly = true,
                                HeaderText = columnAttr.Title,
                                Visible = columnAttr.Visible,
                                Width = columnAttr.Width
                            };
                            if (columnAttr.GridViewAutoSize !=
                            GridViewAutoSize.None)
                            {
                                column.AutoSizeMode =
                                (DataGridViewAutoSizeColumnMode)Enum.Parse(typeof(DataGridViewAutoSizeColumnMode),
                                columnAttr.GridViewAutoSize.ToString());
                            }
                            grid.Columns.Add(column);
                        }
                    }
                }
            }
            // добавляем строки
            foreach (var elem in data)
            {
                var objs = new List<object>();
                foreach (var conf in config)
                {
                    var value =
                    elem.GetType().GetProperty(conf).GetValue(elem);
                    objs.Add(value);
                }
                grid.Rows.Add(objs.ToArray());
            }
        }
    }
}