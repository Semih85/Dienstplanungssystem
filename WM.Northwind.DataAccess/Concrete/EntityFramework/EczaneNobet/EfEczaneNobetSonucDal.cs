using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfEczaneNobetSonucDal : EfEntityRepositoryBase<EczaneNobetSonuc, EczaneNobetContext>, IEczaneNobetSonucDal
    {
        public EczaneNobetSonucDetay2 GetDetay(Expression<Func<EczaneNobetSonucDetay2, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetSonuclar
                    .Select(s => new EczaneNobetSonucDetay2
                    {
                        Id = s.Id,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetGrup.EczaneId,
                        EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrup.BaslangicTarihi,
                        EczaneNobetGrupBitisTarihi = s.EczaneNobetGrup.BitisTarihi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        NobetGorevTipAdi = s.NobetGorevTip.Adi,
                        NobetGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                        EczaneninAcikOlduguSaatAraligi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.EczaneninAcikOlduguSaatAraligi,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        TakvimId = s.TakvimId,
                        Tarih = s.Takvim.Tarih,
                        NobetUstGrupBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi,
                        NobetGrupGorevTipBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.BaslamaTarihi,
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        NobetAltGrupId = s.EczaneNobetGrup.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrup.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrupId
                        : 0,
                        NobetAltGrupAdi = s.EczaneNobetGrup.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrup.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrup.Adi
                        : "Aalt grup yok",
                        YayimlandiMi = s.YayimlandiMi,
                        SanalNobetMi = s.EczaneNobetSanalSonuc.EczaneNobetSonucId > 0 ? true : false,
                        SanalNobetAciklama = s.EczaneNobetSanalSonuc.Aciklama
                    }).SingleOrDefault(filter);
            }
        }

        public List<EczaneNobetSonucDetay2> GetDetayList(Expression<Func<EczaneNobetSonucDetay2, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                //var liste1 = ctx.EczaneNobetSonuclar
                //    .Where(w => w.EczaneNobetFeragat.NobetFeragatTipId == 2).ToList();

                //;
                var liste = from s in ctx.EczaneNobetSonuclar
                            where s.EczaneNobetFeragat.NobetFeragatTipId != 2
                            let sanalNobetDurumu = s.EczaneNobetSanalSonuc
                            let eczaneNobetGrup = s.EczaneNobetFeragat.NobetFeragatTipId == 4 ? s.EczaneNobetFeragat.EczaneNobetGrup : s.EczaneNobetGrup
                            let eczaneNobetGrupAltGrup = eczaneNobetGrup.EczaneNobetGrupAltGruplar
                                .Where(w => w.BaslangicTarihi <= s.Takvim.Tarih && (s.Takvim.Tarih <= w.BitisTarihi || w.BitisTarihi == null)).FirstOrDefault()
                            select new EczaneNobetSonucDetay2
                            {
                                Id = s.Id,
                                EczaneAdi = eczaneNobetGrup.Eczane.Adi,
                                EczaneId = eczaneNobetGrup.EczaneId,
                                EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrup.BaslangicTarihi,
                                EczaneNobetGrupBitisTarihi = s.EczaneNobetGrup.BitisTarihi,
                                EczaneNobetGrupId = eczaneNobetGrup.Id,
                                NobetGorevTipAdi = eczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi, //s.NobetGorevTip.Adi,
                                NobetGorevTipId = eczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                                EczaneninAcikOlduguSaatAraligi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.EczaneninAcikOlduguSaatAraligi,
                                NobetGrupAdi = eczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                                NobetGrupId = eczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                                NobetUstGrupId = eczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                                TakvimId = s.TakvimId,
                                Tarih = s.Takvim.Tarih,
                                NobetUstGrupBaslamaTarihi = eczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi,
                                NobetGrupGorevTipBaslamaTarihi = eczaneNobetGrup.NobetGrupGorevTip.BaslamaTarihi,
                                NobetGrupGorevTipId = eczaneNobetGrup.NobetGrupGorevTipId,
                                NobetAltGrupId = eczaneNobetGrupAltGrup != null ? eczaneNobetGrupAltGrup.NobetAltGrupId : 0,
                                NobetAltGrupAdi = eczaneNobetGrupAltGrup != null ? eczaneNobetGrupAltGrup.NobetAltGrup.Adi : "Aalt grup yok",
                                NobetAltGrupKapanmaTarihi = eczaneNobetGrupAltGrup != null ? eczaneNobetGrupAltGrup.NobetAltGrup.BitisTarihi : null,
                                YayimlandiMi = s.YayimlandiMi,
                                SanalNobetMi = sanalNobetDurumu.EczaneNobetSonucId > 0 ? true : false,
                                SanalNobetAciklama = sanalNobetDurumu.Aciklama
                            };

                return filter == null
                   ? liste.ToList()
                   : liste.Where(filter).ToList();
            }
        }

        public virtual void CokluSil(int[] ids)
        {
            using (var context = new EczaneNobetContext())
            {
                var deletedEntity = context.EczaneNobetSonuclar.RemoveRange(context.EczaneNobetSonuclar.Where(w => ids.Contains(w.Id)));

                context.SaveChanges();
            }
        }

        public virtual void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler)
        {
            var liste = new List<EczaneNobetSonuc>();

            using (var context = new EczaneNobetContext())
            {
                foreach (var nobetCozum in eczaneNobetCozumler)
                {
                    var nobetSonuc = new EczaneNobetSonuc
                    {
                        TakvimId = nobetCozum.TakvimId,
                        EczaneNobetGrupId = nobetCozum.EczaneNobetGrupId,
                        NobetGorevTipId = nobetCozum.NobetGorevTipId
                    };

                    liste.Add(nobetSonuc);
                }

                context.EczaneNobetSonuclar.AddRange(liste);
                context.SaveChanges();
            }
        }

        public virtual void CokluYayimla(List<EczaneNobetSonuc> eczaneNobetSonuclar, bool yayimlandiMi)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var eczaneNobetSonuc in eczaneNobetSonuclar)
                {
                    var mevcutSonuc = context.EczaneNobetSonuclar.SingleOrDefault(x => x.Id == eczaneNobetSonuc.Id);

                    mevcutSonuc.YayimlandiMi = yayimlandiMi;

                    context.SaveChanges();
                }
            }
        }
    }
}
