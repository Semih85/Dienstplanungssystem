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
    public class NobetUstGrupMobilUygulamaYetkiMap : EntityTypeConfiguration<NobetUstGrupMobilUygulamaYetki>
    {
        public NobetUstGrupMobilUygulamaYetkiMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetUstGrupMobilUygulamaYetkiler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");
            this.Property(t => t.MobilUygulamaYetkiId).HasColumnName("MobilUygulamaYetkiId");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.NobetUstGrupId).IsRequired();
            this.Property(t => t.MobilUygulamaYetkiId).IsRequired();
            #endregion

            #region relationship
            this.HasRequired(t => t.MobilUygulamaYetki)
                        .WithMany(et => et.NobetUstGrupMobilUygulamaYetkiler)
                        .HasForeignKey(t =>t.MobilUygulamaYetkiId)
                        .WillCascadeOnDelete(false);
            this.HasRequired(t => t.NobetUstGrup)
                        .WithMany(et => et.NobetUstGrupMobilUygulamaYetkiler)
                        .HasForeignKey(t =>t.NobetUstGrupId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}