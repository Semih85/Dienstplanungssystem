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
    public class NobetGrupGorevTipGunKuralMap : EntityTypeConfiguration<NobetGrupGorevTipGunKural>
    {
        public NobetGrupGorevTipGunKuralMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetGrupGorevTipGunKurallar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetGrupGorevTipId).HasColumnName("NobetGrupGorevTipId");
            this.Property(t => t.NobetGunKuralId).HasColumnName("NobetGunKuralId");
            this.Property(t => t.NobetUstGrupGunGrupId).HasColumnName("NobetUstGrupGunGrupId");
            this.Property(t => t.BaslangicTarihi).HasColumnName("BaslangicTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            this.Property(t => t.NobetciSayisi).HasColumnName("NobetciSayisi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.NobetGrupGorevTipId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupGorevTipGunKurallar")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            this.Property(t => t.NobetGunKuralId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupGorevTipGunKurallar")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));
            this.Property(t => t.NobetUstGrupGunGrupId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupGorevTipGunKurallar")
                                     {
                                         IsUnique = true,
                                         Order = 3
                                     }));
            this.Property(t => t.BaslangicTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            #endregion

            #region relationship
            this.HasRequired(t => t.NobetGrupGorevTip)
                        .WithMany(et => et.NobetGrupGorevTipGunKurallar)
                        .HasForeignKey(t => t.NobetGrupGorevTipId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetGunKural)
                        .WithMany(et => et.NobetGrupGorevTipGunKurallar)
                        .HasForeignKey(t => t.NobetGunKuralId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetUstGrupGunGrup)
                        .WithMany(et => et.NobetGrupGorevTipGunKurallar)
                        .HasForeignKey(t => t.NobetUstGrupGunGrupId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}