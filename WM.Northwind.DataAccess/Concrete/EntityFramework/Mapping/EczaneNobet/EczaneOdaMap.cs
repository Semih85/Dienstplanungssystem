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
    public class EczaneOdaMap : EntityTypeConfiguration<EczaneOda>
    {
        public EczaneOdaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("EczaneOdalar");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adres).HasColumnName("Adres");
            this.Property(t => t.MailAdresi).HasColumnName("MailAdresi");
            this.Property(t => t.TelefonNo).HasColumnName("TelefonNo");
            this.Property(t => t.WebSitesi).HasColumnName("WebSitesi");
            this.Property(t => t.Adi).HasColumnName("Adi");

            #region Properties
            this.Property(t => t.Id)
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Adi)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_EczaneOdalar_Adi")
                            {
                                IsUnique = true
                            }));                 

            this.Property(t => t.TelefonNo)
                .HasMaxLength(10)
                .IsOptional()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_EczaneOdalar_TelefonNo")
                            {
                                IsUnique = true
                            })); 

            this.Property(t => t.Adres)
                .HasMaxLength(150)
                .IsOptional()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_EczaneOdalar_Adres")
                            {
                                IsUnique = true
                            }));

            this.Property(t => t.MailAdresi)
                .HasMaxLength(100)
                .IsOptional()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_EczaneOdalar_MailAdresi")
                            {
                                IsUnique = true
                            }));

            this.Property(t => t.WebSitesi)
                .HasMaxLength(50)
                .IsOptional()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_EczaneOdalar_WebSitesi")
                            {
                                IsUnique = true
                            }));
            #endregion
            
        }
    }
}
