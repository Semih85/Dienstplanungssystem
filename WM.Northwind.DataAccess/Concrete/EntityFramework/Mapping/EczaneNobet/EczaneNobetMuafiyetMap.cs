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
    public class EczaneNobetMuafiyetMap : EntityTypeConfiguration<EczaneNobetMuafiyet>
    {
        public EczaneNobetMuafiyetMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("EczaneNobetMuafiyetler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneId).HasColumnName("EczaneId");
            this.Property(t => t.BaslamaTarihi).HasColumnName("BaslamaTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.EczaneId).IsRequired();
            this.Property(t => t.BaslamaTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsRequired();
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(250);
            #endregion

            #region relationship
            this.HasRequired(t => t.Eczane)
                        .WithMany(et => et.EczaneNobetMuafiyetler)
                        .HasForeignKey(t => t.EczaneId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}