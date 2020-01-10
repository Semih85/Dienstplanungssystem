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
    public class EczaneGorevTipMap : EntityTypeConfiguration<EczaneGorevTip>
    {
        public EczaneGorevTipMap()
        {
            this.ToTable("EczaneGorevTipler");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneId).HasColumnName("EczaneId");
            this.Property(t => t.GorevTipId).HasColumnName("GorevTipId");

            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(t => t.Id).IsRequired() 
;            this.Property(t => t.EczaneId).IsRequired() 
;            this.Property(t => t.GorevTipId).IsRequired() 
;
            this.HasRequired(t => t.Eczane)
                        .WithMany(et => et.EczaneGorevTipler)
                        .HasForeignKey(t =>t.EczaneId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.GorevTip)
                        .WithMany(et => et.EczaneGorevTipler)
                        .HasForeignKey(t =>t.GorevTipId)
                        .WillCascadeOnDelete(false);
        }
    }
}