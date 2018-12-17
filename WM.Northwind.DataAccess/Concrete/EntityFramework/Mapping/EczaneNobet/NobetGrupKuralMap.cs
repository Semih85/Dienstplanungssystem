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
    public class NobetGrupKuralMap : EntityTypeConfiguration<NobetGrupKural>
    {
        public NobetGrupKuralMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetGrupKurallar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetGrupGorevTipId).HasColumnName("NobetGrupGorevTipId");
            this.Property(t => t.NobetKuralId).HasColumnName("NobetKuralId");
            this.Property(t => t.BaslangicTarihi).HasColumnName("BaslangicTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            this.Property(t => t.Deger).HasColumnName("Deger");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            //this.Property(t => t.NobetGrupGorevTipId).IsRequired();
            //this.Property(t => t.NobetKuralId).IsRequired();
            this.Property(t => t.BaslangicTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();
            this.Property(t => t.Deger).IsOptional();

            this.Property(t => t.NobetGrupGorevTipId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupKurallar")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));

            this.Property(t => t.NobetKuralId).IsRequired()
                .IsRequired()
                .HasColumnAnnotation("Index",
                 new IndexAnnotation(
                             new IndexAttribute("UN_NobetGrupKurallar")
                             {
                                 IsUnique = true,
                                 Order = 2
                             }));
            #endregion

            #region relationship
            this.HasRequired(t => t.NobetGrupGorevTip)
                        .WithMany(et => et.NobetGrupKurallar)
                        .HasForeignKey(t =>t.NobetGrupGorevTipId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetKural)
                        .WithMany(et => et.NobetGrupKurallar)
                        .HasForeignKey(t =>t.NobetKuralId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}