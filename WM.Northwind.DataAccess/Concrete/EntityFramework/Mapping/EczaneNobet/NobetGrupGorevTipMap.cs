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
    public class NobetGrupGorevTipMap : EntityTypeConfiguration<NobetGrupGorevTip>
    {
        public NobetGrupGorevTipMap()
        {
            this.ToTable("NobetGrupGorevTipler");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetGrupId).HasColumnName("NobetGrupId");
            this.Property(t => t.NobetGorevTipId).HasColumnName("NobetGorevTipId");
            this.Property(t => t.BaslamaTarihi).HasColumnName("BaslamaTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");

            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.BaslamaTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();

            this.Property(t => t.NobetGrupId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupGorevTipler")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            this.Property(t => t.NobetGorevTipId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupGorevTipler")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));

            this.HasRequired(t => t.NobetGorevTip)
                        .WithMany(et => et.NobetGrupGorevTipler)
                        .HasForeignKey(t => t.NobetGorevTipId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetGrup)
                        .WithMany(et => et.NobetGrupGorevTipler)
                        .HasForeignKey(t => t.NobetGrupId)
                        .WillCascadeOnDelete(false);
        }
    }
}