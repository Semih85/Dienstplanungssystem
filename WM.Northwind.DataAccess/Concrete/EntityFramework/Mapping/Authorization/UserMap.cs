using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.Authorization
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Key
            this.HasKey(t => t.Id);

            #region Table & Column Mappings

            this.ToTable(@"Users", @"Yetki");
            //this.ToTable("Users");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.BaslamaTarihi).HasColumnName("BaslamaTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");
            this.Property(t => t.TelefonNo).HasColumnName("TelefonNo");

            #endregion

            #region Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.BaslamaTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_UserEmail")
                        {
                            IsUnique = true
                        })); 

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(
                        new IndexAttribute("UN_UserName")
                        {
                            IsUnique = true
                        }));

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(64)
                ;

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TelefonNo)
                .HasMaxLength(10)
                .IsOptional();
            #endregion

        }
    }
}
