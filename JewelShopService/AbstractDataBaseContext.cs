using JewelShopModel;
using System.Data.Entity;

namespace JewelShopService
{
    public  class AbstractDataBaseContext:DbContext
    {
        public AbstractDataBaseContext():base("AbstractDatabase")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Buyer> Buyers { get; set; }

        public virtual DbSet<Element> Elements { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<ProdOrder> ProdOrders { get; set; }

        public virtual DbSet<Adornment> Adornments { get; set; }

        public virtual DbSet<AdornmentElement> AdornmentElements { get; set; }

        public virtual DbSet<Hangar> Hangars { get; set; }

        public virtual DbSet<HangarElement> HangarElements { get; set; }
    }
}
