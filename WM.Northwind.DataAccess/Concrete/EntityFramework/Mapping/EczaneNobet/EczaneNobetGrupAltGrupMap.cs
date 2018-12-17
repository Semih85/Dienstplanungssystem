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
    public class EczaneNobetGrupAltGrupMap : EntityTypeConfiguration<EczaneNobetGrupAltGrup>
    {
        public EczaneNobetGrupAltGrupMap()
        {
            this.HasKey(t => t.EczaneNobetGrupId);
            this.ToTable("EczaneNobetGrupAltGruplar");

            #region columns
            this.Property(t => t.EczaneNobetGrupId).HasColumnName("EczaneNobetGrupId");
            this.Property(t => t.NobetAltGrupId).HasColumnName("NobetAltGrupId");
            #endregion

            #region properties
            this.Property(t => t.EczaneNobetGrupId)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.EczaneNobetGrupId).IsRequired();
            this.Property(t => t.NobetAltGrupId).IsRequired();
            #endregion

            #region relationship
            this.HasRequired(t => t.EczaneNobetGrup)
                    .WithOptional(t => t.EczaneNobetGrupAltGrup)
                    .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetAltGrup)
                    .WithMany(et => et.EczaneNobetGrupAltGruplar)
                    .HasForeignKey(t => t.NobetAltGrupId)
                    .WillCascadeOnDelete(false);
            #endregion
        }
    }
}