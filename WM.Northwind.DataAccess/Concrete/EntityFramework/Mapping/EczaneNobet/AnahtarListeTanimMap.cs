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
    public class AnahtarListeTanimMap : EntityTypeConfiguration<AnahtarListeTanim>
    {
        public AnahtarListeTanimMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("AnahtarListeTanimlar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.KayitTarihi).HasColumnName("KayitTarihi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Adi).IsRequired()
                        .HasMaxLength(50);
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(150);
            this.Property(t => t.KayitTarihi).IsRequired();
            #endregion

            #region relationship
            #endregion
        }
    }
}