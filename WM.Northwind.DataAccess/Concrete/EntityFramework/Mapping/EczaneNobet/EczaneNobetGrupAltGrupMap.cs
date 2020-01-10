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
    public class EczaneNobetGrupAltGrupMap : EntityTypeConfiguration<EczaneNobetGrupAltGrup>
    {
        public EczaneNobetGrupAltGrupMap()
        {
            //this.HasKey(t => t.EczaneNobetGrupId);

            this.HasKey(t => t.Id);
            this.ToTable("EczaneNobetGrupAltGruplar");

            #region columns
            this.Property(t => t.EczaneNobetGrupId).HasColumnName("EczaneNobetGrupId");
            this.Property(t => t.NobetAltGrupId).HasColumnName("NobetAltGrupId");
            this.Property(t => t.BaslangicTarihi).HasColumnName("BaslangicTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            #endregion

            #region properties
            //this.Property(t => t.EczaneNobetGrupId)
            //        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.Id)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.EczaneNobetGrupId).IsRequired();
            this.Property(t => t.NobetAltGrupId).IsRequired();
            this.Property(t => t.BaslangicTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            this.Property(t => t.Aciklama)
            .IsRequired()
            .HasMaxLength(200);

            this.Property(t => t.EczaneNobetGrupId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_EczaneNobetGrupAltlar")
                        {
                            //IsClustered = true,
                            IsUnique = true,
                            Order = 1
                        }));

            this.Property(t => t.NobetAltGrupId)
               .IsRequired()
               .HasColumnAnnotation("Index",
                   new IndexAnnotation(
                       new IndexAttribute("UN_EczaneNobetGrupAltlar")
                       {
                           //IsClustered = true,
                           IsUnique = true,
                           Order = 2
                       }));

            this.Property(t => t.BaslangicTarihi)
               .IsRequired()
               .HasColumnAnnotation("Index",
                   new IndexAnnotation(
                       new IndexAttribute("UN_EczaneNobetGrupAltlar")
                       {
                           //IsClustered = true,
                           IsUnique = true,
                           Order = 3
                       }));
            #endregion

            #region relationship
            //this.HasRequired(t => t.EczaneNobetGrup)
            //        .WithOptional(t => t.EczaneNobetGrupAltGrup)
            //        .WillCascadeOnDelete(false);
            this.HasRequired(t => t.EczaneNobetGrup)
                .WithMany(et => et.EczaneNobetGrupAltGruplar)
                .HasForeignKey(t => t.EczaneNobetGrupId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetAltGrup)
                    .WithMany(et => et.EczaneNobetGrupAltGruplar)
                    .HasForeignKey(t => t.NobetAltGrupId)
                    .WillCascadeOnDelete(false);
            #endregion
        }
    }
}