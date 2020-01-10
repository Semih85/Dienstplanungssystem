using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class MenuAltRoleMap : EntityTypeConfiguration<MenuAltRole>
    {
        public MenuAltRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable(@"MenuAltRoles", @"Yetki");
            //this.ToTable("MenuAltRoles");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MenuAltId).HasColumnName("MenuAltId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");

            #region Properties

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.MenuAltId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_MenuAltRoles")
                        {
                            IsUnique = true,
                            Order = 1
                        }));

            this.Property(t => t.RoleId)
                   .IsRequired()
                   .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_MenuAltRoles")
                           {
                               IsUnique = true,
                               Order = 2
                           }));
            #endregion

            #region Relationship
            this.HasRequired(t => t.Role)
                .WithMany(et => et.MenuAltRoles)
                .HasForeignKey(t => t.RoleId)
                .WillCascadeOnDelete(false);
            #endregion

        }
    }
}
