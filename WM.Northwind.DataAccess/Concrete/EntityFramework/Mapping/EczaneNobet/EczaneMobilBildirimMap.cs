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
    public class EczaneMobilBildirimMap : EntityTypeConfiguration<EczaneMobilBildirim>
    {
        public EczaneMobilBildirimMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("EczaneMobilBildirimler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneId).HasColumnName("EczaneId");
            this.Property(t => t.MobilBildirimId).HasColumnName("MobilBildirimId");
            this.Property(t => t.BildirimGormeTarihi).HasColumnName("BildirimGormeTarihi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.EczaneId).IsRequired();
            this.Property(t => t.MobilBildirimId).IsRequired();
            this.Property(t => t.BildirimGormeTarihi).IsOptional();
            #endregion

            #region relationship
            this.HasRequired(t => t.Eczane)
                        .WithMany(et => et.EczaneMobilBildirimler)
                        .HasForeignKey(t => t.EczaneId)
                        .WillCascadeOnDelete(false);
            this.HasRequired(t => t.MobilBildirim)
                        .WithMany(et => et.EczaneMobilBildirimler)
                        .HasForeignKey(t =>t.MobilBildirimId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}