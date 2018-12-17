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
    public class NobetAltGrupMap : EntityTypeConfiguration<NobetAltGrup>
    {
        public NobetAltGrupMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetAltGruplar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.NobetGrupGorevTipId).HasColumnName("NobetGrupGorevTipId");
            this.Property(t => t.BaslamaTarihi).HasColumnName("BaslamaTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.NobetGrupGorevTipId).IsRequired()
                .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetAltGruplar")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));

            this.Property(t => t.Adi).IsRequired()
            .HasMaxLength(70)
            .HasColumnAnnotation("Index",
             new IndexAnnotation(
                         new IndexAttribute("UN_NobetAltGruplar")
                         {
                             IsUnique = true,
                             Order = 2
                         }));

            this.Property(t => t.BaslamaTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            #endregion

            #region relationship
            this.HasRequired(t => t.NobetGrupGorevTip)
                        .WithMany(et => et.NobetAltGruplar)
                        .HasForeignKey(t =>t.NobetGrupGorevTipId);
            #endregion
        }
    }
}