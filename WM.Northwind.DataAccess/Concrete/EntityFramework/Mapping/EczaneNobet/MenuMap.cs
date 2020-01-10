using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable(@"Menuler", @"Yetki");
            //this.ToTable("Menuler");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LinkText).HasColumnName("LinkText");
            this.Property(t => t.ActionName).HasColumnName("ActionName");
            this.Property(t => t.ControllerName).HasColumnName("ControllerName");
            this.Property(t => t.SpanCssClass).HasColumnName("SpanCssClass");
            this.Property(t => t.PasifMi).HasColumnName("PasifMi");
            
            // Properties
            this.Property(t => t.LinkText)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_Menuler")
                            {
                                IsUnique = true
                            }));

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
