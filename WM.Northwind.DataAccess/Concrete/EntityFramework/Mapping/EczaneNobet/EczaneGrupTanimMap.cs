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
    public class EczaneGrupTanimMap : EntityTypeConfiguration<EczaneGrupTanim>
    {
        public EczaneGrupTanimMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("EczaneGrupTanimlar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.ArdisikNobetSayisi).HasColumnName("ArdisikNobetSayisi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.BaslangicTarihi).HasColumnName("BaslangicTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");
            this.Property(t => t.EczaneGrupTanimTipId).HasColumnName("EczaneGrupTanimTipId");
            this.Property(t => t.NobetGorevTipId).HasColumnName("NobetGorevTipId");
            this.Property(t => t.PasifMi).HasColumnName("PasifMi");
            this.Property(t => t.AyniGunNobetTutabilecekEczaneSayisi).HasColumnName("AyniGunNobetTutabilecekEczaneSayisi");
            
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Adi).IsRequired()
                        .HasMaxLength(100);
            this.Property(t => t.ArdisikNobetSayisi).IsRequired();
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(250);
            this.Property(t => t.BaslangicTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            this.Property(t => t.EczaneGrupTanimTipId).IsRequired();
            this.Property(t => t.NobetGorevTipId).IsRequired();
            this.Property(t => t.AyniGunNobetTutabilecekEczaneSayisi).IsRequired();

            this.Property(t => t.NobetUstGrupId)
               .IsRequired()
               .HasColumnAnnotation("Index",
                   new IndexAnnotation(
                       new IndexAttribute("UN_EczaneGrupTanimlar")
                       {
                           IsUnique = true,
                           Order = 1
                       }));

            this.Property(t => t.Adi)
               .HasColumnAnnotation("Index",
                   new IndexAnnotation(
                       new IndexAttribute("UN_EczaneGrupTanimlar")
                       {
                           IsUnique = true,
                           Order = 2
                       }));

            this.Property(t => t.NobetGorevTipId)
                   .IsRequired()
                   .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_EczaneGrupTanimlar")
                           {
                               IsUnique = true,
                               Order = 3
                           }));
            #endregion

            #region relationship
            this.HasRequired(t => t.EczaneGrupTanimTip)
                .WithMany(et => et.EczaneGrupTanimlar)
                .HasForeignKey(t => t.EczaneGrupTanimTipId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetUstGrup)
                .WithMany(et => et.EczaneGrupTanimlar)
                .HasForeignKey(t =>t.NobetUstGrupId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetGorevTip)
                .WithMany(et => et.EczaneGrupTanimlar)
                .HasForeignKey(t => t.NobetGorevTipId)
                .WillCascadeOnDelete(false);
            #endregion
        }
    }
}