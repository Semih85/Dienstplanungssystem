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
    public class NobetUstGrupKisitMap : EntityTypeConfiguration<NobetUstGrupKisit>
    {
        public NobetUstGrupKisitMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetUstGrupKisitlar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");
            this.Property(t => t.KisitId).HasColumnName("KisitId");
            this.Property(t => t.SagTarafDegeri).HasColumnName("SagTarafDegeri");
            this.Property(t => t.SagTarafDegeriVarsayilan).HasColumnName("SagTarafDegeriVarsayilan");
            this.Property(t => t.PasifMi).HasColumnName("PasifMi");
            this.Property(t => t.VarsayilanPasifMi).HasColumnName("VarsayilanPasifMi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.PasifMi).IsRequired();
            this.Property(t => t.VarsayilanPasifMi).IsRequired();
            this.Property(t => t.SagTarafDegeri).IsRequired();
            this.Property(t => t.Aciklama).HasMaxLength(200);

            this.Property(t => t.NobetUstGrupId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_NobetUstGrupKisitlar")
                        {
                            IsUnique = true,
                            Order = 1
                        }));

            this.Property(t => t.KisitId)
                   .IsRequired()
                   .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_NobetUstGrupKisitlar")
                           {
                               IsUnique = true,
                               Order = 2
                           }));
            #endregion

            #region relationship
            this.HasRequired(t => t.Kisit)
                        .WithMany(et => et.NobetUstGrupKisitlar)
                        .HasForeignKey(t =>t.KisitId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetUstGrup)
                        .WithMany(et => et.NobetUstGrupKisitlar)
                        .HasForeignKey(t =>t.NobetUstGrupId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}