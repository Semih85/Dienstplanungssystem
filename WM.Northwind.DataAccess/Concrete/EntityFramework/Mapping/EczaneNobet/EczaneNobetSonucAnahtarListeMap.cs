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
    public class EczaneNobetSonucAnahtarListeMap : EntityTypeConfiguration<EczaneNobetSonucAnahtarListe>
    {
        public EczaneNobetSonucAnahtarListeMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("EczaneNobetSonucAnahtarListeler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneNobetGrupId).HasColumnName("EczaneNobetGrupId");
            this.Property(t => t.NobetUstGrupGunGrupId).HasColumnName("NobetUstGrupGunGrupId");
            this.Property(t => t.KullanildiMi).HasColumnName("KullanildiMi");
            this.Property(t => t.AnahtarListeTanimId).HasColumnName("AnahtarListeTanimId");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.EczaneNobetGrupId).IsRequired();
            this.Property(t => t.NobetUstGrupGunGrupId).IsRequired();
            this.Property(t => t.KullanildiMi).IsRequired();
            this.Property(t => t.AnahtarListeTanimId).IsRequired();
            #endregion

            #region relationship
            this.HasRequired(t => t.EczaneNobetGrup)
                        .WithMany(et => et.EczaneNobetSonucAnahtarListeler)
                        .HasForeignKey(t =>t.EczaneNobetGrupId)
                        .WillCascadeOnDelete(false);
            this.HasRequired(t => t.AnahtarListeTanim)
                        .WithMany(et => et.EczaneNobetSonucAnahtarListeler)
                        .HasForeignKey(t =>t.AnahtarListeTanimId)
                        .WillCascadeOnDelete(false);
            this.HasRequired(t => t.NobetUstGrupGunGrup)
                        .WithMany(et => et.EczaneNobetSonucAnahtarListeler)
                        .HasForeignKey(t =>t.NobetUstGrupGunGrupId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}