using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class EczaneIlceMap : EntityTypeConfiguration<EczaneIlce>
    {
        public EczaneIlceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            #region Table & Column Mappings

            this.ToTable("EczaneIlceler");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.EczaneId).HasColumnName("EczaneId");
            this.Property(t => t.IlceId).HasColumnName("IlceId");
            this.Property(t => t.BaslangicTarihi).HasColumnName("BaslangicTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");

            #endregion

            #region Properties

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.EczaneId).IsRequired();
            this.Property(t => t.IlceId).IsOptional();
            this.Property(t => t.BaslangicTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            this.Property(t => t.Aciklama)
                .HasMaxLength(150)
                .IsRequired();
            
            #endregion

            #region Relationships

            this.HasRequired(t => t.Eczane)
                .WithMany(et => et.EczaneIlceler)
                .HasForeignKey(t => t.EczaneId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Ilce)
                .WithMany(et => et.EczaneIlceler)
                .HasForeignKey(t => t.IlceId)
                .WillCascadeOnDelete(false);

            #endregion
        }
    }
}
