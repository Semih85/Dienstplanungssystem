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
    public class RaporMap : EntityTypeConfiguration<Rapor>
    {
        public RaporMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Raporlar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.RaporKategoriId).HasColumnName("RaporKategoriId");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Adi).IsRequired()
                        .HasMaxLength(250);
            this.Property(t => t.RaporKategoriId).IsRequired();
            this.Property(t => t.SiraId).IsRequired()
                 .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_SiraId")
                                     {
                                         IsUnique = true
                                     }));
            #endregion

            #region relationship
            this.HasRequired(t => t.RaporKategori)
                        .WithMany(et => et.Raporlar)
                        .HasForeignKey(t =>t.RaporKategoriId)
                .WillCascadeOnDelete(false);

            #endregion
        }
    }
}