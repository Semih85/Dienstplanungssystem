using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class MenuRoleMap : EntityTypeConfiguration<MenuRole>
    {
        public MenuRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable(@"MenuRoles", @"Yetki");
            //this.ToTable("MenuRoles");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MenuId).HasColumnName("MenuId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");

            #region Properties

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.MenuId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_MenuRoles")
                        {
                            IsUnique = true,
                            Order = 1
                        }));

            this.Property(t => t.RoleId)
                   .IsRequired()
                   .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_MenuRoles")
                           {
                               IsUnique = true,
                               Order = 2
                           }));
            #endregion

            #region Relationship
            this.HasRequired(t => t.Role)
                .WithMany(et => et.MenuRoles)
                .HasForeignKey(t => t.RoleId)
                .WillCascadeOnDelete(false);
            #endregion
        }
    }
}
