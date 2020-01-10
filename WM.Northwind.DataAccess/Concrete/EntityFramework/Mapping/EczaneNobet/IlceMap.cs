using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class IlceMap : EntityTypeConfiguration<Ilce>
    {
        public IlceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            #region Table & Column Mappings

            this.ToTable("Ilceler");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.SehirId).HasColumnName("SehirId");

            #endregion

            #region Properties

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Adi)
                .HasMaxLength(50)
                .IsRequired();
            
            #endregion

            #region Relationships

            this.HasRequired(t => t.Sehir)
                .WithMany(et => et.Ilceler)
                .HasForeignKey(t => t.SehirId)
                .WillCascadeOnDelete(false);

            #endregion
        }
    }
}
