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
    public class EczaneGrupMap : EntityTypeConfiguration<EczaneGrup>
    {
        public EczaneGrupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            #region Table & Column Mappings
            this.ToTable("EczaneGruplar");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneGrupTanimId).HasColumnName("EczaneGrupTanimId");
            this.Property(t => t.EczaneId).HasColumnName("EczaneId");
            this.Property(t => t.BirlikteNobetYazilsinMi).HasColumnName("BirlikteNobetYazilsinMi");
            this.Property(t => t.PasifMi).HasColumnName("PasifMi");

            #endregion

            #region Properties

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.BirlikteNobetYazilsinMi)
                .IsRequired();

            this.Property(t => t.EczaneGrupTanimId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_EczaneGruplar")
                        {
                            IsUnique = true,
                            Order = 1
                        }));

            this.Property(t => t.EczaneId)
                   .IsRequired()
                   .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_EczaneGruplar")
                           {
                               IsUnique = true,
                               Order = 2
                           }));

            #endregion

            #region Relationship
            this.HasRequired(t => t.EczaneGrupTanim)
                .WithMany(et => et.EczaneGruplar)
                .HasForeignKey(t => t.EczaneGrupTanimId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Eczane)
                .WithMany(et => et.EczaneGruplar)
                .HasForeignKey(t => t.EczaneId)
                .WillCascadeOnDelete(false);

            #endregion
        }
    }
}
