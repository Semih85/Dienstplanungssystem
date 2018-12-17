using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping
{
    public class TransportMaliyetMap : EntityTypeConfiguration<TransportMaliyet>
    {
        public TransportMaliyetMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            #region Navigation
            
            HasRequired(t => t.Fabrika)
                .WithMany(o => o.Maliyetler)
                .HasForeignKey(t => t.FabrikaId);

            HasRequired(t => t.Depo)
                .WithMany(o => o.Maliyetler)
                .HasForeignKey(t => t.DepoId);
            #endregion

            #region Properties
            
            Property(t => t.FabrikaId)
                .IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_depoId_fabrikaId", 1) { IsUnique = true }));
            Property(t => t.DepoId)
                .IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_depoId_fabrikaId", 2) { IsUnique = true }));
            Property(t => t.Deger)
                .IsRequired();
            #endregion

            #region Mappings
            ToTable("TransportMaliyetler");
            Property(t => t.FabrikaId).HasColumnName("FabrikaId");
            Property(t => t.DepoId).HasColumnName("DepoId");
            #endregion
        }
    }
}
