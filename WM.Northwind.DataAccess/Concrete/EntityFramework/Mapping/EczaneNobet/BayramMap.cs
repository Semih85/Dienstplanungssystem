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
    public class BayramMap : EntityTypeConfiguration<Bayram>
    {
        public BayramMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("Bayramlar");
            this.Property(t => t.TakvimId).HasColumnName("TakvimId");
            this.Property(t => t.NobetGrupGorevTipId).HasColumnName("NobetGrupGorevTipId");
            this.Property(t => t.NobetGunKuralId).HasColumnName("NobetGunKuralId");
            this.Property(t => t.BayramTurId).HasColumnName("BayramTurId");

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.TakvimId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_Bayramlar")
                            {
                                IsUnique = true,
                                Order = 1
                            }));

            this.Property(t => t.NobetGrupGorevTipId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_Bayramlar")
                            {
                                IsUnique = true,
                                Order = 2
                            }));

            this.Property(t => t.NobetGunKuralId)
                 .IsRequired()
                 .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                             new IndexAttribute("UN_Bayramlar")
                             {
                                 IsUnique = true,
                                 Order = 3
                             }));

            this.HasRequired(t => t.NobetGunKural)
                .WithMany(et => et.Bayramlar)
                .HasForeignKey(t => t.NobetGunKuralId)
                .WillCascadeOnDelete(false); 

            this.HasRequired(t => t.NobetGrupGorevTip)
                .WithMany(et => et.Bayramlar)
                .HasForeignKey(t => t.NobetGrupGorevTipId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Takvim)
                .WithMany(et => et.Bayramlar)
                .HasForeignKey(t => t.TakvimId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.BayramTur)
                .WithMany(et => et.Bayramlar)
                .HasForeignKey(t => t.BayramTurId)
                .WillCascadeOnDelete(false);

            //Bire-bir ilişkide hangi tablonun dependent olduğunu belirmek için kullanılır.
            //takvim master/ilk/ana (principal) tablo, bayram ise buna bağlı (dependent) bir tablodur.
            //bayram günü atanabilmesi için önce takvim'de olması gerekir. 
            //Takvimde olmayan bir güne bayram günü atanamaz.

            //this.HasRequired(t => t.Takvim)
            //    .WithOptional(t => t.Bayram);

        }
    }
}