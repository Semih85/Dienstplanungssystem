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
    public class EczaneNobetMazeretMap : EntityTypeConfiguration<EczaneNobetMazeret>
    {
        public EczaneNobetMazeretMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            #region Table & Column Mappings

            this.ToTable("EczaneNobetMazeretler");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.MazeretId).HasColumnName("MazeretId");
            this.Property(t => t.TakvimId).HasColumnName("TakvimId");

            #endregion

            #region Properties

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Aciklama)
                .HasMaxLength(150)
                .IsRequired();

            this.Property(t => t.EczaneNobetGrupId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_EczaneNobetMazeretler")
                            {
                                IsUnique = true,
                                Order = 1
                            }));

            this.Property(t => t.TakvimId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_EczaneNobetMazeretler")
                            {
                                IsUnique = true,
                                Order = 2
                            }));

            this.Property(t => t.MazeretId)
                .IsRequired()
                .HasColumnAnnotation("Index",
                        new IndexAnnotation(
                            new IndexAttribute("UN_EczaneNobetMazeretler")
                            {
                                IsUnique = true,
                                Order = 3
                            }));
            #endregion

            #region Relationships

            this.HasRequired(t => t.EczaneNobetGrup)
                .WithMany(et => et.EczaneNobetMazeretler)
                .HasForeignKey(t => t.EczaneNobetGrupId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Mazeret)
                .WithMany(et => et.EczaneNobetMazeretler)
                .HasForeignKey(t => t.MazeretId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Takvim)
                .WithMany(et => et.EczaneNobetMazeretler)
                .HasForeignKey(t => t.TakvimId)
                .WillCascadeOnDelete(false);

            #endregion
        }
    }
}
