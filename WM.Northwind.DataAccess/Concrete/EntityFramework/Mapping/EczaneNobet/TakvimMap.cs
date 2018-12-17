using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class TakvimMap : EntityTypeConfiguration<Takvim>
    {
        public TakvimMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Takvimler");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Tarih).HasColumnName("Tarih");

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Tarih)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_Tarih")
                        {
                            IsUnique = true
                        }));

            // Relationship
            //Bire-bir ilişkide hangi tablonun principal (ilk önce buraya insert edilir, daha sonra bayrama insert edilir.) olduğunu belirmek için kullanılır.
            //this.HasOptional(t => t.Bayram)
            //        .WithRequired(t => t.Takvim);
        }
    }
}
