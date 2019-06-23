using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using System.Data.Entity;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfAyniGunTutulanNobetDal : EfEntityRepositoryBase<AyniGunTutulanNobet, EczaneNobetContext>, IAyniGunTutulanNobetDal
    {
        public AyniGunTutulanNobetDetay GetDetay(Expression<Func<AyniGunTutulanNobetDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.AyniGunTutulanNobetler
                    .Select(s => new AyniGunTutulanNobetDetay
                    {
                        Id = s.Id,
                        EnSonAyniGunNobetTakvimId = s.EnSonAyniGunNobetTakvimId,
                        NobetUstGrupId = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.Adi,

                        NobetAltGrupAdi1 = s.EczaneNobetGrupl.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrupl.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrup.Adi
                        : "",
                        NobetAltGrupAdi2 = s.EczaneNobetGrup2.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrupl.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrup.Adi
                        : "",

                        EczaneNobetGrupId1 = s.EczaneNobetGrupl.Id,
                        EczaneId1 = s.EczaneNobetGrupl.EczaneId,
                        EczaneAdi1 = s.EczaneNobetGrupl.Eczane.Adi,
                        NobetGrupId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGorevTipAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGorevTip.Adi,
                        //NobetUstGrupId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        //NobetUstGrupAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,

                        EczaneNobetGrupId2 = s.EczaneNobetGrup2.Id,
                        EczaneId2 = s.EczaneNobetGrup2.EczaneId,
                        EczaneAdi2 = s.EczaneNobetGrup2.Eczane.Adi,
                        NobetGrupId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGorevTipAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGorevTip.Adi,
                        //NobetUstGrupId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        //NobetUstGrupAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,

                        EnSonAyniGunNobetTarihi = s.Takvim.Tarih,
                        AyniGunNobetSayisi = s.AyniGunNobetSayisi,
                        AyniGunNobetTutamayacaklariGunSayisi = s.AyniGunNobetTutamayacaklariGunSayisi
                    }).SingleOrDefault(filter);
            }
        }
        public List<AyniGunTutulanNobetDetay> GetDetayList(Expression<Func<AyniGunTutulanNobetDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.AyniGunTutulanNobetler
                    .Select(s => new AyniGunTutulanNobetDetay
                    {
                        Id = s.Id,
                        EnSonAyniGunNobetTakvimId = s.EnSonAyniGunNobetTakvimId,
                        NobetUstGrupId = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.Adi,

                        NobetAltGrupAdi1 = s.EczaneNobetGrupl.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrupl.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrup.Adi
                        : "",
                        NobetAltGrupAdi2 = s.EczaneNobetGrup2.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrupl.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrup.Adi
                        : "",

                        EczaneNobetGrupId1 = s.EczaneNobetGrupl.Id,
                        EczaneId1 = s.EczaneNobetGrupl.EczaneId,
                        EczaneAdi1 = s.EczaneNobetGrupl.Eczane.Adi,
                        NobetGrupId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupGorevTipId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.Id,
                        NobetGrupAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGorevTipAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGorevTip.Adi,
                        //NobetUstGrupId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        //NobetUstGrupAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,

                        EczaneNobetGrupId2 = s.EczaneNobetGrup2.Id,
                        EczaneId2 = s.EczaneNobetGrup2.EczaneId,
                        EczaneAdi2 = s.EczaneNobetGrup2.Eczane.Adi,
                        NobetGrupId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupGorevTipId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.Id,
                        NobetGrupAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGorevTipAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGorevTip.Adi,
                        //NobetUstGrupId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        //NobetUstGrupAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,

                        EnSonAyniGunNobetTarihi = s.Takvim.Tarih,

                        AyniGunNobetSayisi = s.AyniGunNobetSayisi,
                        AyniGunNobetTutamayacaklariGunSayisi = s.AyniGunNobetTutamayacaklariGunSayisi
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

        public void UpdateAyniGunNobetSayisi(List<AyniGunTutulanNobet> ayniGunTutulanNobetler)
        {
            using (var ctx = new EczaneNobetContext())
            {
                //var liste = ctx.AyniGunTutulanNobetler.Where(x => entities.Select(s => s.Id).Contains(x.Id)).ToArray();

                foreach (var ayniGunTutulanNobet in ayniGunTutulanNobetler)
                {
                    var guncellenecekKayit = ctx.AyniGunTutulanNobetler.SingleOrDefault(x => x.Id == ayniGunTutulanNobet.Id);

                    if (guncellenecekKayit != null)
                    {
                        guncellenecekKayit.AyniGunNobetSayisi = ayniGunTutulanNobet.AyniGunNobetSayisi;
                    }
                }

                ctx.SaveChanges();
            }
        }

        public void UpdateTumKolonlar(List<AyniGunTutulanNobet> ayniGunTutulanNobetler)
        {
            using (var ctx = new EczaneNobetContext())
            {
                //var liste = ctx.AyniGunTutulanNobetler.Where(x => entities.Select(s => s.Id).Contains(x.Id)).ToArray();

                foreach (var ayniGunTutulanNobet in ayniGunTutulanNobetler)
                {
                    var guncellenecekKayit = ctx.AyniGunTutulanNobetler.SingleOrDefault(x => x.Id == ayniGunTutulanNobet.Id);

                    if (guncellenecekKayit != null)
                    {
                        guncellenecekKayit.EnSonAyniGunNobetTakvimId = ayniGunTutulanNobet.EnSonAyniGunNobetTakvimId;
                        guncellenecekKayit.AyniGunNobetSayisi = ayniGunTutulanNobet.AyniGunNobetSayisi;
                        guncellenecekKayit.AyniGunNobetTutamayacaklariGunSayisi = ayniGunTutulanNobet.AyniGunNobetTutamayacaklariGunSayisi;
                    }
                }

                ctx.SaveChanges();
            }
        }
    }
}