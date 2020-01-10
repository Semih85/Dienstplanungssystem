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
    public class EczaneNobetSanalSonucMap : EntityTypeConfiguration<EczaneNobetSanalSonuc>
    {
        public EczaneNobetSanalSonucMap()
        {
            this.HasKey(t => t.EczaneNobetSonucId);
            this.ToTable("EczaneNobetSanalSonuclar");

            #region columns
            this.Property(t => t.EczaneNobetSonucId).HasColumnName("EczaneNobetSonucId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.KayitTarihi).HasColumnName("KayitTarihi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            #endregion

            #region properties
            this.Property(t => t.EczaneNobetSonucId)
                       .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.EczaneNobetSonucId).IsRequired();
            this.Property(t => t.UserId).IsRequired();
            this.Property(t => t.KayitTarihi).IsRequired();
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(400);
            #endregion

            #region relationship
            //Bire-bir ilişkide hangi tablonun dependent olduğunu belirmek için kullanılır.
            //takvim master/ilk/ana (principal) tablo, bayram ise buna bağlı (dependent) bir tablodur.
            //bayram günü atanabilmesi için önce takvim'de olması gerekir. 
            //Takvimde olmayan bir güne bayram günü atanamaz.
            this.HasRequired(t => t.EczaneNobetSonuc)
                    .WithOptional(t => t.EczaneNobetSanalSonuc)
                    .WillCascadeOnDelete(false);

            //this.HasRequired(t => t.EczaneNobetSonuc)
            //            .WithMany(et => et.EczaneNobetSanalSonuclar)
            //            .HasForeignKey(t =>t.EczaneNobetSonucId)
            //            .WillCascadeOnDelete(false);

            this.HasRequired(t => t.User)
                        .WithMany(et => et.EczaneNobetSanalSonuclar)
                        .HasForeignKey(t => t.UserId)
                        .WillCascadeOnDelete(false);
            #endregion
        }
    }
}