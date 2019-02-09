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
    public class EczaneUzaklikMatrisMap : EntityTypeConfiguration<EczaneUzaklikMatris>
    {
        public EczaneUzaklikMatrisMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("EczaneUzaklikMatrisler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneIdFrom).HasColumnName("EczaneIdFrom");
            this.Property(t => t.EczaneIdTo).HasColumnName("EczaneIdTo");
            this.Property(t => t.Mesafe).HasColumnName("Mesafe");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.EczaneIdFrom).IsRequired();
            this.Property(t => t.EczaneIdTo).IsRequired();
            this.Property(t => t.Mesafe).IsRequired();
            #endregion

            #region relationship
            this.HasRequired(t => t.EczaneFrom)
                        .WithMany(et => et.EczaneUzaklikMatrislerFrom)
                        .HasForeignKey(t =>t.EczaneIdFrom)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.EczaneTo)
                        .WithMany(et => et.EczaneUzaklikMatrislerTo)
                        .HasForeignKey(t =>t.EczaneIdTo)
                .WillCascadeOnDelete(false);
            #endregion
        }
    }
}