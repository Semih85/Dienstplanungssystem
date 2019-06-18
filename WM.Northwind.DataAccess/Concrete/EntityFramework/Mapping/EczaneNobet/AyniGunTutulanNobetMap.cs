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
    public class AyniGunTutulanNobetMap : EntityTypeConfiguration<AyniGunTutulanNobet>
    {
        public AyniGunTutulanNobetMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("AyniGunTutulanNobetler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneNobetGrupId1).HasColumnName("EczaneNobetGrupId1");
            this.Property(t => t.EczaneNobetGrupId2).HasColumnName("EczaneNobetGrupId2");
            this.Property(t => t.EnSonAyniGunNobetTakvimId).HasColumnName("EnSonAyniGunNobetTakvimId");
            this.Property(t => t.AyniGunNobetSayisi).HasColumnName("AyniGunNobetSayisi");
            //this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");
            this.Property(t => t.AyniGunNobetTutamayacaklariGunSayisi).HasColumnName("AyniGunNobetTutamayacaklariGunSayisi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            //this.Property(t => t.NobetUstGrupId).IsRequired();
                 //.HasColumnAnnotation("Index",
                 //       new IndexAnnotation(
                 //                    new IndexAttribute("Ix_NobetUstGrupId")
                 //                    { IsClustered = true }
                 //                    ));

            this.Property(t => t.EczaneNobetGrupId1).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("Un_AyniGunTutulanNobetler")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            this.Property(t => t.EczaneNobetGrupId2).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("Un_AyniGunTutulanNobetler")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));
            //this.Property(t => t.EnSonAyniGunNobetTakvimId).IsRequired()
            //            .HasColumnAnnotation("Index",
            //             new IndexAnnotation(
            //                         new IndexAttribute("Un_AyniGunTutulanNobetler")
            //                          {
            //                                      IsUnique = true,
            //                                      Order = 3
            //                          }));
            this.Property(t => t.AyniGunNobetSayisi).IsRequired();
            this.Property(t => t.AyniGunNobetTutamayacaklariGunSayisi).IsRequired();
            #endregion

            #region relationship
            this.HasRequired(t => t.EczaneNobetGrupl)
                        .WithMany(et => et.AyniGunTutulanNobetler1)
                        .HasForeignKey(t => t.EczaneNobetGrupId1)
                        .WillCascadeOnDelete(false);
            this.HasRequired(t => t.EczaneNobetGrup2)
                        .WithMany(et => et.AyniGunTutulanNobetler2)
                        .HasForeignKey(t => t.EczaneNobetGrupId2)
                        .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Takvim)
                        .WithMany(et => et.AyniGunTutulanNobetler)
                        .HasForeignKey(t => t.EnSonAyniGunNobetTakvimId)
                        .WillCascadeOnDelete(false);
            //this.HasRequired(t => t.NobetUstGrup)
            //            .WithMany(et => et.AyniGunTutulanNobetler)
            //            .HasForeignKey(t => t.NobetUstGrupId)
            //            .WillCascadeOnDelete(false);
            #endregion
        }
    }
}