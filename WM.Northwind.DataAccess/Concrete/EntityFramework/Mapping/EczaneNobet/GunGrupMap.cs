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
    public class GunGrupMap : EntityTypeConfiguration<GunGrup>
    {
        public GunGrupMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("GunGruplar");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Adi).IsRequired()
                        .HasMaxLength(30)
                        .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_GunGrup_Adi")
                            {
                                IsUnique = true
                            }));
            #endregion

            #region relationship
            #endregion
        }
    }
}