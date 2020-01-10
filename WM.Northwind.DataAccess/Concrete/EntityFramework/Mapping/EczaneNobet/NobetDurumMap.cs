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
    public class NobetDurumMap : EntityTypeConfiguration<NobetDurum>
    {
        public NobetDurumMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetDurumlar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetAltGrupId1).HasColumnName("NobetAltGrupId1");
            this.Property(t => t.NobetAltGrupId2).HasColumnName("NobetAltGrupId2");
            this.Property(t => t.NobetAltGrupId3).HasColumnName("NobetAltGrupId3");
            this.Property(t => t.NobetDurumTipId).HasColumnName("NobetDurumTipId");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.NobetAltGrupId1).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetDurumlar")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            this.Property(t => t.NobetAltGrupId2).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetDurumlar")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));
            this.Property(t => t.NobetAltGrupId3).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetDurumlar")
                                     {
                                         IsUnique = true,
                                         Order = 3
                                     }));
            this.Property(t => t.NobetDurumTipId).IsRequired();
            #endregion

            #region relationship
            this.HasRequired(t => t.NobetAltGrupl)
                        .WithMany(et => et.NobetDurumlar1)
                        .HasForeignKey(t =>t.NobetAltGrupId1)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetAltGrup2)
                        .WithMany(et => et.NobetDurumlar2)
                        .HasForeignKey(t =>t.NobetAltGrupId2)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetAltGrup3)
                        .WithMany(et => et.NobetDurumlar3)
                        .HasForeignKey(t =>t.NobetAltGrupId3)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetDurumTip)
                        .WithMany(et => et.NobetDurumlar)
                        .HasForeignKey(t =>t.NobetDurumTipId)
                        .WillCascadeOnDelete(false);

            #endregion
        }
    }
}