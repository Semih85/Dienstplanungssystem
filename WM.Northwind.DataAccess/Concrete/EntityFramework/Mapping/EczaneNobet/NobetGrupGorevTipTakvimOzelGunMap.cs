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
    public class NobetGrupGorevTipTakvimOzelGunMap : EntityTypeConfiguration<NobetGrupGorevTipTakvimOzelGun>
    {
        public NobetGrupGorevTipTakvimOzelGunMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetGrupGorevTipTakvimOzelGunler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TakvimId).HasColumnName("TakvimId");
            this.Property(t => t.NobetGunKuralId).HasColumnName("NobetGunKuralId");
            this.Property(t => t.NobetGrupGorevTipGunKuralId).HasColumnName("NobetGrupGorevTipGunKuralId");
            this.Property(t => t.NobetOzelGunId).HasColumnName("NobetOzelGunId");
            this.Property(t => t.FarkliGunGosterilsinMi).HasColumnName("FarkliGunGosterilsinMi");
            this.Property(t => t.AgirlikDegeri).HasColumnName("AgirlikDegeri");
            this.Property(t => t.NobetOzelGunKategoriId).HasColumnName("NobetOzelGunKategoriId");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.NobetOzelGunKategoriId).IsRequired();

            this.Property(t => t.TakvimId)
                .IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupGorevTipTakvimOzelGunler")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            //this.Property(t => t.NobetGunKuralId).IsRequired()
            //            .HasColumnAnnotation("Index",
            //             new IndexAnnotation(
            //                         new IndexAttribute("UN_NobetGrupGorevTipTakvimOzelGunler")
            //                         {
            //                             IsUnique = true,
            //                             Order = 2
            //                         }));
            this.Property(t => t.NobetGrupGorevTipGunKuralId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupGorevTipTakvimOzelGunler")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));

            //this.Property(t => t.FarkliGunGosterilsinMi)
            //            //.IsRequired()
            //            .HasColumnAnnotation("Index",
            //             new IndexAnnotation(
            //                         new IndexAttribute("UN_NobetGrupGorevTipTakvimOzelGunler")
            //                         {
            //                             IsUnique = true,
            //                             Order = 3
            //                         }));

            //this.Property(t => t.NobetOzelGunId).IsRequired()
            //            .HasColumnAnnotation("Index",
            //             new IndexAnnotation(
            //                         new IndexAttribute("UN_NobetGrupGorevTipTakvimOzelGunler")
            //                         {
            //                             IsUnique = true,
            //                             Order = 4
            //                         }));
            #endregion

            #region relationship
            this.HasRequired(t => t.NobetGrupGorevTipGunKural)
                        .WithMany(et => et.NobetGrupGorevTipTakvimOzelGunler)
                        .HasForeignKey(t => t.NobetGrupGorevTipGunKuralId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetGunKural)
                        .WithMany(et => et.NobetGrupGorevTipTakvimOzelGunler)
                        .HasForeignKey(t => t.NobetGunKuralId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetOzelGun)
                        .WithMany(et => et.NobetGrupGorevTipTakvimOzelGunler)
                        .HasForeignKey(t => t.NobetOzelGunId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Takvim)
                        .WithMany(et => et.NobetGrupGorevTipTakvimOzelGunler)
                        .HasForeignKey(t => t.TakvimId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetOzelGunKategori)
                .WithMany(et => et.NobetGrupGorevTipTakvimOzelGunler)
                .HasForeignKey(t => t.NobetOzelGunKategoriId)
                .WillCascadeOnDelete(false);

            #endregion
        }
    }
}