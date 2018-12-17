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
    public class EczaneNobetDegisimMap : EntityTypeConfiguration<EczaneNobetDegisim>
    {
        public EczaneNobetDegisimMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("EczaneNobetDegisimler");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneNobetSonucId).HasColumnName("EczaneNobetSonucId");
            this.Property(t => t.EczaneNobetGrupId).HasColumnName("EczaneNobetGrupId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.KayitTarihi).HasColumnName("KayitTarihi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.UserId).IsRequired();
            this.Property(t => t.KayitTarihi).IsRequired();
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(400);

            this.Property(t => t.EczaneNobetSonucId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_EczaneNobetDegisimler")
                        {                            
                            IsUnique = true,
                            Order = 1
                        }));

            this.Property(t => t.EczaneNobetGrupId)
               .IsRequired()
               .HasColumnAnnotation("Index",
                   new IndexAnnotation(
                       new IndexAttribute("UN_EczaneNobetDegisimler")
                       {                           
                           IsUnique = true,
                           Order = 2
                       }));
            #endregion

            #region relationship
            this.HasRequired(t => t.EczaneNobetGrup)
                        .WithMany(et => et.EczaneNobetDegisimler)
                        .HasForeignKey(t => t.EczaneNobetGrupId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.EczaneNobetSonuc)
                        .WithMany(et => et.EczaneNobetDegisimler)
                        .HasForeignKey(t => t.EczaneNobetSonucId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(t => t.User)
                        .WithMany(et => et.EczaneNobetDegisimler)
                        .HasForeignKey(t => t.UserId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}