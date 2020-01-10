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
    public class UserNobetUstGrupMap : EntityTypeConfiguration<UserNobetUstGrup>
    {
        public UserNobetUstGrupMap()
        {
            // Key
            this.HasKey(t => t.Id);

            #region Table & Column Mappings
            this.ToTable(@"UserNobetUstGruplar", @"Yetki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NobetUstGrupId).HasColumnName("NobetUstGrupId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.BaslamaTarihi).HasColumnName("BaslamaTarihi");
            this.Property(t => t.BitisTarihi).HasColumnName("BitisTarihi");

            #endregion

            #region Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.BaslamaTarihi).IsRequired();
            this.Property(t => t.BitisTarihi).IsOptional();

            this.Property(t => t.NobetUstGrupId)
               .IsRequired()
               .HasColumnAnnotation("Index",
                       new IndexAnnotation(
                           new IndexAttribute("UN_UserNobetUstGruplar")
                           {
                               IsUnique = true,
                               Order = 1
                           }));

            this.Property(t => t.UserId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_UserNobetUstGruplar")
                            {
                                IsUnique = true,
                                Order = 2
                            }));
            #endregion

            #region Relationships
            this.HasRequired(t => t.User)
                    .WithMany(et => et.UserNobetUstGruplar)
                    .HasForeignKey(t => t.UserId)
                    .WillCascadeOnDelete(false);

            this.HasRequired(t => t.NobetUstGrup)
                    .WithMany(et => et.UserNobetUstGruplar)
                    .HasForeignKey(t => t.NobetUstGrupId)
                    .WillCascadeOnDelete(false);

            #endregion
        }
    }
}
