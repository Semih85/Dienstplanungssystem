using System.Data.Entity;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Initializers;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using System.Collections.Generic;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts
{
    public class IdentityContext : DbContext
    {
        static IdentityContext()
        {
            Database.SetInitializer(new IdentityInitializer());
        }

        public IdentityContext() : base("Name=EczaneNobetContext")
        {
        }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; } 

        //public DbSet<MenuRole> MenuRoles { get; set; }
        //public DbSet<MenuAltRole> MenuAltRoles { get; set; }
        //public DbSet<Menu> Menuler { get; set; }
        //public DbSet<MenuAlt> MenuAltlar { get; set; }

        #region Mapping
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Yetki");

            //modelBuilder.Configurations.Add(new UserMap());
            //modelBuilder.Configurations.Add(new RoleMap());
            //modelBuilder.Configurations.Add(new UserRoleMap()); 

            //modelBuilder.Configurations.Add(new MenuRoleMap());
            //modelBuilder.Configurations.Add(new MenuAltRoleMap());

            //modelBuilder.Configurations.Add(new MenuMap());
            //modelBuilder.Configurations.Add(new MenuAltMap());
        }
        #endregion
    }
}


//Eczane grubu eczanelerin birlikte atanmasıyla ilgili
//Eczane nöbet grubu eczanelerin hangi grupta nöbetçi olacağını belirler