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
    public class NobetDurumTipMap : EntityTypeConfiguration<NobetDurumTip>
    {
        public NobetDurumTipMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NobetDurumTipler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Adi).IsRequired()
                        .HasMaxLength(50)
                        .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_NobetDurumTipler_Adi")
                            {
                                IsUnique = true
                            }));

            this.Property(t => t.Aciklama)
                        //.IsRequired()
                        .HasMaxLength(200);
            #endregion

            #region relationship
            #endregion
        }
    }
}