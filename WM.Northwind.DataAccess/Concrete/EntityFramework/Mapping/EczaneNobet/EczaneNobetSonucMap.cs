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
    public class EczaneNobetSonucMap : EntityTypeConfiguration<EczaneNobetSonuc>
    {
        public EczaneNobetSonucMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("EczaneNobetSonuclar");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneNobetGrupId).HasColumnName("EczaneNobetGrupId");
            this.Property(t => t.TakvimId).HasColumnName("TakvimId");
            this.Property(t => t.NobetGorevTipId).HasColumnName("NobetGorevTipId");
            this.Property(t => t.YayimlandiMi).HasColumnName("YayimlandiMi");

            #region Properties

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.EczaneNobetGrupId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_EczaneNobetSonuclar")
                        {
                            //IsClustered = true,
                            IsUnique = true,
                            Order = 1
                        }));

            this.Property(t => t.TakvimId)
               .IsRequired()
               .HasColumnAnnotation("Index",
                   new IndexAnnotation(
                       new IndexAttribute("UN_EczaneNobetSonuclar")
                       {
                           //IsClustered = true,
                           IsUnique = true,
                           Order = 2
                       }));

            this.Property(t => t.NobetGorevTipId)
               .IsRequired()
               .HasColumnAnnotation("Index",
                   new IndexAnnotation(
                       new IndexAttribute("UN_EczaneNobetSonuclar")
                       {
                           //IsClustered = true,
                           IsUnique = true,
                           Order = 3
                       }));

            this.Property(t => t.YayimlandiMi).IsRequired();

            #endregion

            // Relationship
            this.HasRequired(t => t.EczaneNobetGrup)
                .WithMany(et => et.EczaneNobetSonuclar)
                .HasForeignKey(t => t.EczaneNobetGrupId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Takvim)
                .WithMany(et => et.EczaneNobetSonuclar)
                .HasForeignKey(t => t.TakvimId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetGorevTip)
                .WithMany(et => et.EczaneNobetSonuclar)
                .HasForeignKey(t => t.NobetGorevTipId)
                .WillCascadeOnDelete(false);
        }
    }
}
