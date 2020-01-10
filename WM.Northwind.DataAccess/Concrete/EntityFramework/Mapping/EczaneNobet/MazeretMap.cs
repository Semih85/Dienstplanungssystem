using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class MazeretMap : EntityTypeConfiguration<Mazeret>
    {
        public MazeretMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Mazeretler");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.MazeretTurId).HasColumnName("MazeretTurId");

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
            this.HasRequired(t => t.MazeretTur)
                .WithMany(et => et.Mazeretler)
                .HasForeignKey(t => t.MazeretTurId)
                .WillCascadeOnDelete(false);

        }
    }
}
