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
    public class UserEczaneMap : EntityTypeConfiguration<UserEczane>
    {
        public UserEczaneMap()
        {
            // Key
            this.HasKey(t => t.Id);

            #region Table & Column Mappings

            this.ToTable(@"UserEczaneler", @"Yetki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EczaneId).HasColumnName("EczaneId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.BaslamaTarihi).HasColumnName("BaslamaTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");

            #endregion

            #region Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.BaslamaTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();

            this.Property(t => t.EczaneId)
               .IsRequired()
               .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_UserEczaneler")
                           {
                               IsUnique = true,
                               Order = 1
                           }));

            this.Property(t => t.UserId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_UserEczaneler")
                            {
                                IsUnique = true,
                                Order = 2
                            }));
            #endregion

            #region Relationships
            this.HasRequired(t => t.User)
                    .WithMany(et => et.UserEczaneler)
                    .HasForeignKey(t => t.UserId)
                    .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Eczane)
                    .WithMany(et => et.UserEczaneler)
                    .HasForeignKey(t => t.EczaneId)
                    .WillCascadeOnDelete(false);

            #endregion
        }
    }
}
