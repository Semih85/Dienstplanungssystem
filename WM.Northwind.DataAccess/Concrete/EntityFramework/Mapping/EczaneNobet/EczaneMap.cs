using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet
{
    public class EczaneMap : EntityTypeConfiguration<Eczane>
    {
        public EczaneMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Eczaneler");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.Adres).HasColumnName("Adres");
            this.Property(t => t.AdresTarifi).HasColumnName("AdresTarifi");
            this.Property(t => t.AdresTarifiKisa).HasColumnName("AdresTarifiKisa");
            this.Property(t => t.AcilisTarihi).HasColumnName("AcilisTarihi");
            this.Property(t => t.KapanisTarihi).HasColumnName("KapanisTarihi");
            this.Property(t => t.MailAdresi).HasColumnName("MailAdresi");
            this.Property(t => t.TelefonNo).HasColumnName("TelefonNo");
            this.Property(t => t.WebSitesi).HasColumnName("WebSitesi");
            this.Property(t => t.Enlem).HasColumnName("Enlem");
            this.Property(t => t.Boylam).HasColumnName("Boylam");
            //this.Property(t => t.EczaneOdaId).HasColumnName("EczaneOdaId");

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AcilisTarihi).IsRequired();
            this.Property(t => t.KapanisTarihi).IsOptional();

            this.Property(t => t.Adi)
                .HasMaxLength(100)
                .IsRequired();
                //.HasColumnAnnotation("Index",
                //                   new IndexAnnotation(
                //                       new IndexAttribute("UN_Eczaneler_Ad_AcilisTarihi")
                //                       {
                //                           IsUnique = true,
                //                           Order = 1
                //                       }));

            this.Property(t => t.TelefonNo)
                .HasMaxLength(10)
                .IsOptional();
            //.HasColumnAnnotation("Index",
            //        new IndexAnnotation(
            //            new IndexAttribute("UN_Eczaneler_TelefonNo")
            //            {
            //                IsUnique = true
            //            }));

            this.Property(t => t.Adres)
                .HasMaxLength(250)
                .IsOptional();
            //.HasColumnAnnotation("Index",
            //        new IndexAnnotation(
            //            new IndexAttribute("UN_Eczaneler_Adres")
            //            {
            //                IsUnique = true
            //            }));

            this.Property(t => t.MailAdresi)
                .HasMaxLength(100)
                .IsOptional();
            //.HasColumnAnnotation("Index",
            //        new IndexAnnotation(
            //            new IndexAttribute("UN_Eczaneler_MailAdresi")
            //            {
            //                IsUnique = true
            //            }));

            //this.Property(t => t.WebSitesi)
            //    .HasMaxLength(50)
            //    .IsOptional()
            //    .HasColumnAnnotation("Index",
            //            new IndexAnnotation(
            //                new IndexAttribute("UN_Eczaneler_WebSitesi")
            //                {
            //                    IsUnique = true
            //                }));

            this.Property(t => t.Enlem)
               .IsRequired();
            //.HasColumnAnnotation("Index",
            //    new IndexAnnotation(
            //        new IndexAttribute("UN_Eczaneler_Koordinat")
            //        {
            //            IsUnique = true,
            //            Order = 1
            //        }));

            this.Property(t => t.Boylam)
                   .IsRequired();
            //.HasColumnAnnotation("Index",
            //    new IndexAnnotation(
            //        new IndexAttribute("UN_Eczaneler_Koordinat")
            //        {
            //            IsUnique = true,
            //            Order = 2
            //        }));

            // Relationship
            this.HasRequired(t => t.NobetUstGrup)
                .WithMany(et => et.Eczaneler)
                .HasForeignKey(t => t.NobetUstGrupId)
                .WillCascadeOnDelete(false);
        }
    }
}
