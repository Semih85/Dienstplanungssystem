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
    public class EczaneNobetSonucEskiMap : EntityTypeConfiguration<EczaneNobetSonucEski>
    {
        public EczaneNobetSonucEskiMap()
        {
            //this.HasKey(t => t.Id);
            //this.ToTable("EczaneNobetSonucEskiler");

            //#region columns
            //this.Property(t => t.Id).HasColumnName("Id");
            //this.Property(t => t.EczaneNobetGrupId).HasColumnName("EczaneNobetGrupId");
            //this.Property(t => t.Tarih).HasColumnName("Tarih");
            //#endregion

            //#region properties
            //this.Property(t => t.Id)
            //            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            //this.Property(t => t.Id).IsRequired();
            //this.Property(t => t.EczaneNobetGrupId).IsRequired()
            //            .HasColumnAnnotation("Index",
            //             new IndexAnnotation(
            //                         new IndexAttribute("UN_EczaneNobetSonucEskiler")
            //                          {
            //                                      IsUnique = true,
            //                                      Order = 1
            //                          }));
            //this.Property(t => t.Tarih).IsRequired()
            //            .HasColumnAnnotation("Index",
            //             new IndexAnnotation(
            //                         new IndexAttribute("UN_EczaneNobetSonucEskiler")
            //                          {
            //                                      IsUnique = true,
            //                                      Order = 2
            //                          }));
            //#endregion

            //#region relationship
            //this.HasRequired(t => t.EczaneNobetGrup)
            //            .WithMany(et => et.EczaneNobetSonucEskiler)
            //            .HasForeignKey(t =>t.EczaneNobetGrupId)
            //            .WillCascadeOnDelete(false);
            //#endregion
        }
    }
}