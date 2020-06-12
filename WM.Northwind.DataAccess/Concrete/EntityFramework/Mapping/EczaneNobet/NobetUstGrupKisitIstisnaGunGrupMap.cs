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
    public class NobetUstGrupKisitIstisnaGunGrupMap : EntityTypeConfiguration<NobetUstGrupKisitIstisnaGunGrup>
    {
        public NobetUstGrupKisitIstisnaGunGrupMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetUstGrupKisitIstisnaGunGruplar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetUstGrupKisitId).HasColumnName("NobetUstGrupKisitId");
            this.Property(t => t.NobetUstGrupGunGrupId).HasColumnName("NobetUstGrupGunGrupId");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.BaslangicTarihi).HasColumnName("BaslangicTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.NobetUstGrupKisitId).IsRequired();
            this.Property(t => t.NobetUstGrupGunGrupId).IsRequired();
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(1000);
            this.Property(t => t.BaslangicTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            #endregion

            #region relationship
            this.HasRequired(t => t.NobetUstGrupGunGrup)
                        .WithMany(et => et.NobetUstGrupKisitIstisnaGunGruplar)
                        .HasForeignKey(t =>t.NobetUstGrupGunGrupId)
                        .WillCascadeOnDelete(false);
            this.HasRequired(t => t.NobetUstGrupKisit)
                        .WithMany(et => et.NobetUstGrupKisitIstisnaGunGruplar)
                        .HasForeignKey(t =>t.NobetUstGrupKisitId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}