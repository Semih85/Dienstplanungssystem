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
    public class EczaneNobetGrupMap : EntityTypeConfiguration<EczaneNobetGrup>
    {
        public EczaneNobetGrupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("EczaneNobetGruplar");
            this.Property(t => t.EczaneId).HasColumnName("EczaneId");
            this.Property(t => t.NobetGrupGorevTipId).HasColumnName("NobetGrupGorevTipId");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BaslangicTarihi).HasColumnName("BaslangicTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.EczaneId).IsRequired();
            this.Property(t => t.NobetGrupGorevTipId).IsRequired();
            this.Property(t => t.BaslangicTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            this.Property(t => t.Aciklama)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.EczaneId)
            .IsRequired()
            .HasColumnAnnotation("Index",
                new IndexAnnotation(
                    new IndexAttribute("UN_EczaneNobetGruplar")
                    {
                                    //IsClustered = true,
                                    IsUnique = true,
                        Order = 1
                    }));

            this.Property(t => t.NobetGrupGorevTipId)
               .IsRequired()
               .HasColumnAnnotation("Index",
                   new IndexAnnotation(
                       new IndexAttribute("UN_EczaneNobetGruplar")
                       {
                           //IsClustered = true,
                           IsUnique = true,
                           Order = 2
                       }));

            this.Property(t => t.BaslangicTarihi)
               .IsRequired()
               .HasColumnAnnotation("Index",
                   new IndexAnnotation(
                       new IndexAttribute("UN_EczaneNobetGruplar")
                       {
                           //IsClustered = true,
                           IsUnique = true,
                           Order = 3
                       }));

            // Relationship
            this.HasRequired(t => t.Eczane)
                .WithMany(et => et.EczaneNobetGruplar)
                .HasForeignKey(t => t.EczaneId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetGrupGorevTip)
                .WithMany(et => et.EczaneNobetGruplar)
                .HasForeignKey(t => t.NobetGrupGorevTipId)
                .WillCascadeOnDelete(false);
        }
    }
}
