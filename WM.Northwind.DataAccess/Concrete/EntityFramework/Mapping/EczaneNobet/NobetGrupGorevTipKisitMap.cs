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
    public class NobetGrupGorevTipKisitMap : EntityTypeConfiguration<NobetGrupGorevTipKisit>
    {
        public NobetGrupGorevTipKisitMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetGrupGorevTipKisitlar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetUstGrupKisitId).HasColumnName("NobetUstGrupKisitId");
            this.Property(t => t.PasifMi).HasColumnName("PasifMi");
            this.Property(t => t.SagTarafDegeri).HasColumnName("SagTarafDegeri");
            this.Property(t => t.VarsayilanPasifMi).HasColumnName("VarsayilanPasifMi");
            this.Property(t => t.SagTarafDegeriVarsayilan).HasColumnName("SagTarafDegeriVarsayilan");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.NobetGrupGorevTipId).HasColumnName("NobetGrupGorevTipId");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.PasifMi).IsRequired();
            this.Property(t => t.NobetUstGrupKisitId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupGorevTipKisitlar")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            this.Property(t => t.NobetGrupGorevTipId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_NobetGrupGorevTipKisitlar")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));
            this.Property(t => t.SagTarafDegeri).IsRequired();
            this.Property(t => t.VarsayilanPasifMi).IsRequired();
            this.Property(t => t.SagTarafDegeriVarsayilan).IsRequired();
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(200);
            #endregion

            #region relationship
            this.HasRequired(t => t.NobetUstGrupKisit)
                        .WithMany(et => et.NobetGrupGorevTipKisitlar)
                        .HasForeignKey(t => t.NobetUstGrupKisitId);

            this.HasRequired(t => t.NobetGrupGorevTip)
                        .WithMany(et => et.NobetGrupGorevTipKisitlar)
                        .HasForeignKey(t => t.NobetGrupGorevTipId);
            #endregion
        }
    }
}