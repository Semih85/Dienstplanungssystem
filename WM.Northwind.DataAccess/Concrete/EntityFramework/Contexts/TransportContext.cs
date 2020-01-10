using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Initializers;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework
{
    public class TransportContext : DbContext
    {

        static TransportContext() 
        {
            Database.SetInitializer(new TransportInitializer());
        }

        public TransportContext() : base("Name=TransportContext")
        {
            
        }

        #region DbTables

        public DbSet<Fabrika> Fabrikalar { get; set; }
        public DbSet<Depo> Depolar { get; set; }
        public DbSet<TransportMaliyet> Maliyetler { get; set; }
        public DbSet<TransportSonuc> TransportSonuclar { get; set; }
        
        #endregion

        #region Mapping
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Transport");
            modelBuilder.Configurations.Add(new FabrikaMap());
            modelBuilder.Configurations.Add(new DepoMap());
            modelBuilder.Configurations.Add(new TransportMaliyetMap());
            modelBuilder.Configurations.Add(new TransportSonucMap());
        }
        #endregion
    }
}
