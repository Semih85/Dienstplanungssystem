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
    public class AyniGunNobetTakipGrupAltGrupMap : EntityTypeConfiguration<AyniGunNobetTakipGrupAltGrup>
    {
        public AyniGunNobetTakipGrupAltGrupMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("AyniGunNobetTakipGrupAltGrup");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetGrupGorevTipId).HasColumnName("NobetGrupGorevTipId");
            this.Property(t => t.NobetAltGrupId).HasColumnName("NobetAltGrupId");
            this.Property(t => t.BaslamaTarihi).HasColumnName("BaslamaTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            this.Property(t => t.KumulatifToplamNobetSayisi).HasColumnName("KumulatifToplamNobetSayisi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.KumulatifToplamNobetSayisi).IsRequired();
            this.Property(t => t.NobetGrupGorevTipId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_AyniGunNobetTakipGrupAltGrup")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            this.Property(t => t.NobetAltGrupId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_AyniGunNobetTakipGrupAltGrup")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));
            this.Property(t => t.BaslamaTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            #endregion

            #region relationship
            this.HasRequired(t => t.NobetAltGrup)
                        .WithMany(et => et.AyniGunNobetTakipGrupAltGruplar)
                        .HasForeignKey(t =>t.NobetAltGrupId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetGrupGorevTip)
                        .WithMany(et => et.AyniGunNobetTakipGrupAltGruplar)
                        .HasForeignKey(t =>t.NobetGrupGorevTipId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}