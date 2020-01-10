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
    public class NobetGorevTipMap : EntityTypeConfiguration<NobetGorevTip>
    {
        public NobetGorevTipMap()
        {
            this.ToTable("NobetGorevTipler");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.EczaneninAcikOlduguSaatAraligi).HasColumnName("EczaneninAcikOlduguSaatAraligi");
            this.Property(t => t.NobetSaatAraligi).HasColumnName("NobetSaatAraligi");
            //this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");

            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();

            this.Property(t => t.EczaneninAcikOlduguSaatAraligi)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(t => t.NobetSaatAraligi)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(t => t.Adi)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_NobetGorevTip_Adi")
                            {
                                IsUnique = true
                            }));

        }
    }
}