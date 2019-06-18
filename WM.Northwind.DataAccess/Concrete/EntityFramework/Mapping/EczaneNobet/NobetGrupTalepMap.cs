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
    public class NobetGrupTalepMap : EntityTypeConfiguration<NobetGrupTalep>
    {
        public NobetGrupTalepMap()
        {
            this.ToTable("NobetGrupTalepler");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TakvimId).HasColumnName("TakvimId");
            this.Property(t => t.NobetciSayisi).HasColumnName("NobetciSayisi");
            this.Property(t => t.NobetGrupGorevTipId).HasColumnName("NobetGrupGorevTipId");

            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();

            this.Property(t => t.TakvimId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupTalepler")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            this.Property(t => t.NobetGrupGorevTipId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupTalepler")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));
            this.Property(t => t.NobetciSayisi).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupTalepler")
                                     {
                                         IsUnique = true,
                                         Order = 3
                                     }));
            this.Property(t => t.NobetGrupGorevTipId).IsRequired();

            this.HasRequired(t => t.NobetGrupGorevTip)
                        .WithMany(et => et.NobetGrupTalepler)
                        .HasForeignKey(t => t.NobetGrupGorevTipId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Takvim)
                        .WithMany(et => et.NobetGrupTalepler)
                        .HasForeignKey(t => t.TakvimId)
                        .WillCascadeOnDelete(false);
        }
    }
}