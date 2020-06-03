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
    public class MobilBildirimMap : EntityTypeConfiguration<MobilBildirim>
    {
        public MobilBildirimMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("MobilBildirimler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Baslik).HasColumnName("Baslik");
            this.Property(t => t.Metin).HasColumnName("Metin");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");
            this.Property(t => t.GonderimTarihi).HasColumnName("GonderimTarihi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Baslik).IsRequired()
                        .HasMaxLength(50);
            this.Property(t => t.Metin).IsRequired()
                        .HasMaxLength(100);
            this.Property(t => t.Aciklama).IsOptional()
                        .HasMaxLength(1000);
            this.Property(t => t.NobetUstGrupId).IsRequired();
            this.Property(t => t.GonderimTarihi).IsRequired();
            #endregion

            #region relationship
            this.HasRequired(t => t.NobetUstGrup)
                        .WithMany(et => et.MobilBildirimler)
                        .HasForeignKey(t => t.NobetUstGrupId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}