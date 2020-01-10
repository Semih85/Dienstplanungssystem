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
    public class RaporRolMap : EntityTypeConfiguration<RaporRol>
    {
        public RaporRolMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("RaporRoller");

            #region columns
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RaporId).HasColumnName("RaporId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            #endregion

            #region properties
            this.Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.RaporId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_RaporRoller")
                                     {
                                         IsUnique = true,
                                         Order = 1
                                     }));
            this.Property(t => t.RoleId).IsRequired()
                        .HasColumnAnnotation("Index",
                         new IndexAnnotation(
                                     new IndexAttribute("UN_RaporRoller")
                                     {
                                         IsUnique = true,
                                         Order = 2
                                     }));
            #endregion

            #region relationship
            this.HasRequired(t => t.Rapor)
                        .WithMany(et => et.RaporRoller)
                        .HasForeignKey(t =>t.RaporId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Role)
                        .WithMany(et => et.RaporRoller)
                        .HasForeignKey(t =>t.RoleId)
                .WillCascadeOnDelete(false);

            #endregion
        }
    }
}