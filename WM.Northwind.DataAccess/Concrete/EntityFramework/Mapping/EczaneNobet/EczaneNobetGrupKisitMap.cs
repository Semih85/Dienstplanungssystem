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
    public class EczaneNobetGrupKisitMap : EntityTypeConfiguration<EczaneNobetGrupKisit>
    {
        public EczaneNobetGrupKisitMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("EczaneNobetGrupKisitlar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetUstGrupKisitId).HasColumnName("NobetUstGrupKisitId");
            this.Property(t => t.EczaneNobetGrupId).HasColumnName("EczaneNobetGrupId");
            this.Property(t => t.PasifMi).HasColumnName("PasifMi");
            this.Property(t => t.SagTarafDegeri).HasColumnName("SagTarafDegeri");
            this.Property(t => t.VarsayilanPasifMi).HasColumnName("VarsayilanPasifMi");
            this.Property(t => t.SagTarafDegeriVarsayilan).HasColumnName("SagTarafDegeriVarsayilan");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.NobetUstGrupKisitId).IsRequired();
            this.Property(t => t.EczaneNobetGrupId).IsRequired();
            this.Property(t => t.PasifMi).IsRequired();
            this.Property(t => t.SagTarafDegeri).IsRequired();
            this.Property(t => t.VarsayilanPasifMi).IsRequired();
            this.Property(t => t.SagTarafDegeriVarsayilan).IsRequired();
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(200);
            #endregion

            #region relationship
            this.HasRequired(t => t.EczaneNobetGrup)
                        .WithMany(et => et.EczaneNobetGrupKisitlar)
                        .HasForeignKey(t =>t.EczaneNobetGrupId)
                        .WillCascadeOnDelete(false);
            this.HasRequired(t => t.NobetUstGrupKisit)
                        .WithMany(et => et.EczaneNobetGrupKisitlar)
                        .HasForeignKey(t =>t.NobetUstGrupKisitId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}