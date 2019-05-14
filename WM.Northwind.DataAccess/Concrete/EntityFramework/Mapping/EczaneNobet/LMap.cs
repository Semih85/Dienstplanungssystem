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
    public class LMap : EntityTypeConfiguration<L>
    {
        public LMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Logs");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Detail).HasColumnName("Detail");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Audit).HasColumnName("Audit");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Detail).IsRequired()
                        .HasMaxLength(-1);
            this.Property(t => t.Date).IsRequired();
            this.Property(t => t.Audit).IsRequired()
                        .HasMaxLength(50);
            #endregion

            #region relationship
            #endregion
        }
    }
}