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
    public class EczaneNobetFeragatMap : EntityTypeConfiguration<EczaneNobetFeragat>
    {
        public EczaneNobetFeragatMap()
        {
            this.HasKey(t => t.EczaneNobetSonucId);
            this.ToTable("EczaneNobetFeragatlar");

            #region columns
            this.Property(t => t.EczaneNobetSonucId).HasColumnName("EczaneNobetSonucId");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            #endregion

            #region properties
            this.Property(t => t.EczaneNobetSonucId)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); 
            this.Property(t => t.EczaneNobetSonucId).IsRequired();
            this.Property(t => t.Aciklama).IsRequired()
                        .HasMaxLength(250);
            #endregion

            #region relationship
            //Bire-bir ilişkide hangi tablonun dependent olduğunu belirmek için kullanılır.
            //takvim master/ilk/ana (principal) tablo, bayram ise buna bağlı (dependent) bir tablodur.
            //bayram günü atanabilmesi için önce takvim'de olması gerekir. 
            //Takvimde olmayan bir güne bayram günü atanamaz.
            this.HasRequired(t => t.EczaneNobetSonuc)
                    .WithOptional(t => t.EczaneNobetFeragat)
                    .WillCascadeOnDelete(false);

            // Relationship
            this.HasRequired(t => t.NobetFeragatTip)
                .WithMany(et => et.EczaneNobetFeragatlar)
                .HasForeignKey(t => t.NobetFeragatTipId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.EczaneNobetGrup)
                .WithMany(et => et.EczaneNobetFeragatlar)
                .HasForeignKey(t => t.EczaneNobetGrupId)
                .WillCascadeOnDelete(false);
            #endregion
        }
    }
}