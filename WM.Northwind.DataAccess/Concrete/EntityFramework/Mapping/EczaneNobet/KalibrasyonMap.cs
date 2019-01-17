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
    public class KalibrasyonMap : EntityTypeConfiguration<Kalibrasyon>
    {
        public KalibrasyonMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Kalibrasyonlar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneNobetGrupId).HasColumnName("EczaneNobetGrupId");
            this.Property(t => t.KalibrasyonTipId).HasColumnName("KalibrasyonTipId");
            this.Property(t => t.NobetUstGrupGunGrupId).HasColumnName("NobetUstGrupGunGrupId");
            this.Property(t => t.Deger).HasColumnName("Deger");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Deger).IsRequired();
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(150);

            this.Property(t => t.EczaneNobetGrupId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_Kalibrasyonlar")
                        {
                            IsUnique = true,
                            Order = 1
                        }));

            this.Property(t => t.KalibrasyonTipId)
                   .IsRequired()
                   .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_Kalibrasyonlar")
                           {
                               IsUnique = true,
                               Order = 2
                           }));

            this.Property(t => t.NobetUstGrupGunGrupId)
                   .IsRequired()
                   .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_Kalibrasyonlar")
                           {
                               IsUnique = true,
                               Order = 3
                           }));
            #endregion

            #region relationship
            this.HasRequired(t => t.EczaneNobetGrup)
                        .WithMany(et => et.Kalibrasyonlar)
                        .HasForeignKey(t =>t.EczaneNobetGrupId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.KalibrasyonTip)
                        .WithMany(et => et.Kalibrasyonlar)
                        .HasForeignKey(t => t.KalibrasyonTipId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetUstGrupGunGrup)
                        .WithMany(et => et.Kalibrasyonlar)
                        .HasForeignKey(t => t.NobetUstGrupGunGrupId)
                        .WillCascadeOnDelete(false);

            #endregion
        }
    }
}