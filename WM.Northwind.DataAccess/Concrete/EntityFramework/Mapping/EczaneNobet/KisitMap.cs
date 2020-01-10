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
    public class KisitMap : EntityTypeConfiguration<Kisit>
    {
        public KisitMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Kisitlar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.AdiGosterilen).HasColumnName("AdiGosterilen");
            this.Property(t => t.KisitKategoriId).HasColumnName("KisitKategoriId");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.OlusturmaTarihi).HasColumnName("OlusturmaTarihi");
            this.Property(t => t.DegerPasifMi).HasColumnName("DegerPasifMi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.KisitKategoriId).IsRequired();
            this.Property(t => t.Adi).IsRequired()
                        .HasMaxLength(100);
            this.Property(t => t.AdiGosterilen).IsRequired()
                        .HasMaxLength(100);
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(300);
            this.Property(t => t.OlusturmaTarihi).IsRequired();
            #endregion

            #region Relationships
            this.HasRequired(t => t.KisitKategori)
                .WithMany(et => et.Kisitlar)
                .HasForeignKey(t => t.KisitKategoriId)
                .WillCascadeOnDelete(false);
            #endregion
        }
    }
}