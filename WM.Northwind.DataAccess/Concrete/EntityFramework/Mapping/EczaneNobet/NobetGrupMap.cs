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
    public class NobetGrupMap : EntityTypeConfiguration<NobetGrup>
    {
        public NobetGrupMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("NobetGruplar");

            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");
            this.Property(t => t.BaslamaTarihi).HasColumnName("BaslamaTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");

            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Adi).IsRequired()
                        .HasMaxLength(70);
            
            this.Property(t => t.BaslamaTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();

            this.HasRequired(t => t.NobetUstGrup)
                        .WithMany(et => et.NobetGruplar)
                        .HasForeignKey(t => t.NobetUstGrupId)
                        .WillCascadeOnDelete(false);

            //göre tipini bu tabloya almaya gerek yok. bir-çok görev tipi olan tablo var zaten
            //this.HasRequired(t => t.NobetGorevTip)
            //    .WithMany(et => et.NobetGruplar)
            //    .HasForeignKey(t => t.NobetGorevTipId)
            //    .WillCascadeOnDelete(false);
        }
    }
}