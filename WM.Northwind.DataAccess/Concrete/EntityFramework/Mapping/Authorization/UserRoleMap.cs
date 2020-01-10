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
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            // Key
            this.HasKey(t => t.Id);

            #region Table & Column Mappings
            this.ToTable(@"UserRoles", @"Yetki");
            //this.ToTable("UserRoles");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.BaslamaTarihi).HasColumnName("BaslamaTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");

            #endregion

            #region Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.BaslamaTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();

            this.Property(t => t.RoleId)
               .IsRequired()
               .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_UserRoles")
                           {
                               IsUnique = true,
                               Order = 1
                           }));

            this.Property(t => t.UserId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_UserRoles")
                            {
                                IsUnique = true,
                                Order = 2
                            }));
            #endregion

            #region Relationships
            this.HasRequired(t => t.User)
                    .WithMany(et => et.UserRoles)
                    .HasForeignKey(t => t.UserId);

            this.HasRequired(t => t.Role)
                    .WithMany(et => et.UserRoles)
                    .HasForeignKey(t => t.RoleId);

            #endregion
        }
    }
}
