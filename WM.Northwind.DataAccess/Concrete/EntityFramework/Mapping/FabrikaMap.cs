using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping
{
    public class FabrikaMap : EntityTypeConfiguration<Fabrika>
    {
        public FabrikaMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            #region Navigation
            HasMany(t => t.Maliyetler)
                .WithRequired(od => od.Fabrika)
                .HasForeignKey(od => od.FabrikaId);

            #endregion

            #region Properties
            Property(t => t.Adi)
                .IsRequired()
                .HasMaxLength(30);
            #endregion

            #region Mappings
            ToTable("Fabrikalar");
            Property(t => t.Adi).HasColumnName("Adi");
            #endregion

        }
    }
}
