using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class IstekMap : EntityTypeConfiguration<Istek>
    {
        public IstekMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Istekler");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.IstekTurId).HasColumnName("IstekTurId");

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Adi)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_Mazeretler_Adi")
                            {
                                IsUnique = true
                            }));

            // Relationship
            this.HasRequired(t => t.IstekTur)
                .WithMany(et => et.Istekler)
                .HasForeignKey(t => t.IstekTurId)
                .WillCascadeOnDelete(false);
        }
    }
}
