using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class NobetUstGrupMap : EntityTypeConfiguration<NobetUstGrup>
    {
        public NobetUstGrupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("NobetUstGruplar");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.EczaneOdaId).HasColumnName("EczaneOdaId");
            this.Property(t => t.BaslangicTarihi).HasColumnName("BaslangicTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.Enlem).HasColumnName("Enlem");
            this.Property(t => t.Boylam).HasColumnName("Boylam");
            this.Property(t => t.TimeLimit).HasColumnName("TimeLimit");
            this.Property(t => t.OneedeGosterilecekEnUzakMesafe).HasColumnName("OneedeGosterilecekEnUzakMesafe");
            this.Property(t => t.BaslamaTarihindenOncekiSonuclarGosterilsinMi).HasColumnName("BaslamaTarihindenOncekiSonuclarGosterilsinMi");
            // Properties 

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Adi)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_NobetUstGruplar_Adi")
                            {
                                IsUnique = true
                            }));


            this.Property(t => t.BaslamaTarihindenOncekiSonuclarGosterilsinMi).IsRequired();
            this.Property(t => t.BaslangicTarihi).IsRequired();
            this.Property(t => t.TimeLimit).IsRequired();
            this.Property(t => t.OneedeGosterilecekEnUzakMesafe).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            this.Property(t => t.Aciklama)
                .IsRequired()
                .HasMaxLength(100);

            // Relationship
            this.HasRequired(t => t.EczaneOda)
                .WithMany(g => g.NobetUstGruplar)
                .HasForeignKey(t => t.EczaneOdaId)
                .WillCascadeOnDelete(false);
        }
    }
}
