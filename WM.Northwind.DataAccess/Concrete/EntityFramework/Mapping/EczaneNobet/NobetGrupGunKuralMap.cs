using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Data.Entity.Infrastructure.Annotations;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class NobetGrupGunKuralMap : EntityTypeConfiguration<NobetGrupGunKural>
    {
        public NobetGrupGunKuralMap()
        {
            this.ToTable("NobetGrupGunKurallar");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetGrupId).HasColumnName("NobetGrupId");
            this.Property(t => t.NobetGunKuralId).HasColumnName("NobetGunKuralId");
            this.Property(t => t.BaslangicTarihi).HasColumnName("BaslangicTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");

            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.NobetGrupId).IsRequired()
             .HasColumnAnnotation("Index",
              new IndexAnnotation(
                          new IndexAttribute("UN_NobetGrupKurallar")
                          {
                              IsUnique = true,
                              Order = 1
                          }));
            this.Property(t => t.NobetGunKuralId).IsRequired()
                         .HasColumnAnnotation("Index",
                          new IndexAnnotation(
                            new IndexAttribute("UN_NobetGrupKurallar")
                            {
                                IsUnique = true,
                                Order = 2
                            }));
            this.Property(t => t.BaslangicTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();

            this.HasRequired(t => t.NobetGrup)
                        .WithMany(et => et.NobetGrupGunKurallar)
                        .HasForeignKey(t => t.NobetGrupId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetGunKural)
                        .WithMany(et => et.NobetGrupGunKurallar)
                        .HasForeignKey(t => t.NobetGunKuralId)
                        .WillCascadeOnDelete(false);
        }
    }
}