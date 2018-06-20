using System;
using JewelShopService.ImplementationsList;
using JewelShopService.ImplementationsBD;
using JewelShopService.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using System.Data.Entity;
using JewelShopService;

namespace JewelShopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormJewelShop>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDataBaseContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IBuyerService, BuyerServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IElementService, ElementServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAdornmentService, AdornmentServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IHangarService, HangarServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportServiceBD>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
