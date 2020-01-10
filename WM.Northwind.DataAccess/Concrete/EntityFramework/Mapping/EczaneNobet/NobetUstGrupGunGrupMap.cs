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
    public class NobetUstGrupGunGrupMap : EntityTypeConfiguration<NobetUstGrupGunGrup>
    {
        public NobetUstGrupGunGrupMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetUstGrupGunGruplar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GunGrupId).HasColumnName("GunGrupId");
            this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.AmacFonksiyonuKatsayisi).HasColumnName("AmacFonksiyonuKatsayisi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.AmacFonksiyonuKatsayisi).IsRequired();
            this.Property(t => t.GunGrupId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetUstGrupGunGruplar")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            this.Property(t => t.NobetUstGrupId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetUstGrupGunGruplar")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));
            this.Property(t => t.Aciklama).IsOptional()
                        .HasMaxLength(100);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.GunGrupId).IsRequired();
            this.Property(t => t.NobetUstGrupId).IsRequired();
            this.Property(t => t.Aciklama).IsOptional()
                        .HasMaxLength(100);
            #endregion

            #region relationship
            this.HasRequired(t => t.GunGrup)
                        .WithMany(et => et.NobetUstGrupGunGruplar)
                        .HasForeignKey(t => t.GunGrupId);
            this.HasRequired(t => t.NobetUstGrup)
                        .WithMany(et => et.NobetUstGrupGunGruplar)
                        .HasForeignKey(t => t.NobetUstGrupId);
            #endregion
        }
    }
}