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
    public class KalibrasyonTipMap : EntityTypeConfiguration<KalibrasyonTip>
    {
        public KalibrasyonTipMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("KalibrasyonTipler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(150);

            this.Property(t => t.Adi)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_KalibrasyonTipler")
                        {
                            IsUnique = true,
                            Order = 1
                        }));

            this.Property(t => t.NobetUstGrupId)
                   .IsRequired()
                   .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_KalibrasyonTipler")
                           {
                               IsUnique = true,
                               Order = 2
                           }));

            #endregion

            #region relationship
            this.HasRequired(t => t.NobetUstGrup)
                        .WithMany(et => et.KalibrasyonTipler)
                        .HasForeignKey(t =>t.NobetUstGrupId)
                        .WillCascadeOnDelete(false);

            #endregion
        }
    }
}