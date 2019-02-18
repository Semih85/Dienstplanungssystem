using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Samples;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Core.Aspects.PostSharp.TranstionAspects;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class EczaneNobetSonucPlanlananManager : IEczaneNobetSonucPlanlananService
    {
        private IEczaneNobetSonucPlanlananDal _eczaneNobetSonucPlanlananDal;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private INobetGrupGorevTipTakvimOzelGunService _nobetGrupGorevTipTakvimOzelGunService;

        public EczaneNobetSonucPlanlananManager(IEczaneNobetSonucPlanlananDal eczaneNobetSonucPlanlananDal,
            IEczaneNobetSonucService eczaneNobetSonucService,
            INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
            INobetGrupGorevTipTakvimOzelGunService nobetGrupGorevTipTakvimOzelGunService)
        {
            _eczaneNobetSonucPlanlananDal = eczaneNobetSonucPlanlananDal;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
            _nobetGrupGorevTipTakvimOzelGunService = nobetGrupGorevTipTakvimOzelGunService;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetSonucPlanlananId)
        {
            _eczaneNobetSonucPlanlananDal.Delete(new EczaneNobetSonucPlanlanan { Id = eczaneNobetSonucPlanlananId });
        }

        public EczaneNobetSonucPlanlanan GetById(int eczaneNobetSonucPlanlananId)
        {
            return _eczaneNobetSonucPlanlananDal.Get(x => x.Id == eczaneNobetSonucPlanlananId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucPlanlanan> GetList()
        {
            return _eczaneNobetSonucPlanlananDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan)
        {
            _eczaneNobetSonucPlanlananDal.Insert(eczaneNobetSonucPlanlanan);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan)
        {
            _eczaneNobetSonucPlanlananDal.Update(eczaneNobetSonucPlanlanan);
        }

        public EczaneNobetSonucDetay2 GetDetayById(int eczaneNobetSonucPlanlananId)
        {
            return _eczaneNobetSonucPlanlananDal.GetDetay(x => x.Id == eczaneNobetSonucPlanlananId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylar()
        {
            return _eczaneNobetSonucPlanlananDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetSonucPlanlananDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId)
        {
            return _eczaneNobetSonucPlanlananDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar(nobetUstGrupId);

            return GetSonuclar(sonuclar);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(int nobetUstGrupId, int gunGrupId, int alinacakEczaneSayisi)
        {
            var sonuclar = GetDetaylar(nobetUstGrupId);

            return GetSonuclar(sonuclar).Where(w => w.GunGrupId == gunGrupId).ToList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(int nobetGrupGorevTipId, int gunGrupId)
        {
            var sonuclar = GetDetaylarByNobetGrupGorevTipId(nobetGrupGorevTipId);

            return GetSonuclar(sonuclar).Where(w => w.GunGrupId == gunGrupId).ToList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        private List<EczaneNobetSonucListe2> GetSonuclar(List<EczaneNobetSonucDetay2> eczaneNobetSonucDetaylar)
        {
            var nobetUstGrupId = eczaneNobetSonucDetaylar.Select(s => s.NobetUstGrupId).Distinct().FirstOrDefault();

            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(nobetUstGrupId);

            var liste = EczaneNobetSonucBirlesim(nobetGrupGorevTipGunKurallar, eczaneNobetSonucDetaylar, nobetGrupGorevTipTakvimOzelGunler);

            return liste;
        }

        private List<EczaneNobetSonucListe2> EczaneNobetSonucBirlesim(List<NobetGrupGorevTipGunKuralDetay> nobetGrupGorevTipGunKurallar,
            List<EczaneNobetSonucDetay2> eczaneNobetSonuclar,
            List<NobetGrupGorevTipTakvimOzelGunDetay> nobetGrupGorevTipTakvimOzelGunler)
        {
            var eczaneNobetSonuclarBirlesim = (from s in eczaneNobetSonuclar
                                               from b in nobetGrupGorevTipTakvimOzelGunler
                                                              .Where(w => w.TakvimId == s.TakvimId
                                                                      && w.NobetGrupGorevTipId == s.NobetGrupGorevTipId).DefaultIfEmpty()
                                               let nobetGrupGorevTipGunKural = nobetGrupGorevTipGunKurallar.SingleOrDefault(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                                && w.NobetGunKuralId == (int)s.Tarih.DayOfWeek + 1)
                                               select new EczaneNobetSonucListe2
                                               {
                                                   Id = s.Id,
                                                   Yil = s.Tarih.Year,
                                                   Ay = s.Tarih.Month,
                                                   EczaneNobetGrupId = s.EczaneNobetGrupId,
                                                   EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrupBaslamaTarihi,
                                                   EczaneNobetGrupBitisTarihi = s.EczaneNobetGrupBitisTarihi,
                                                   EczaneId = s.EczaneId,
                                                   EczaneAdi = s.EczaneAdi,
                                                   NobetGrupId = s.NobetGrupId,
                                                   NobetGrupAdi = s.NobetGrupAdi,
                                                   NobetUstGrupId = s.NobetUstGrupId,
                                                   NobetUstGrupBaslamaTarihi = s.NobetUstGrupBaslamaTarihi,
                                                   SonucTuru = "Planlanan",
                                                   NobetGunKuralId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                        ? b.NobetGunKuralId
                                                        : (int)s.Tarih.DayOfWeek + 1,
                                                   GunTanim = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                        ? b.NobetGrupGorevTipGunKuralAdi
                                                        : (nobetGrupGorevTipGunKural == null ? "Tanımsız gün kuralı" : nobetGrupGorevTipGunKural.NobetGunKuralAdi),
                                                   ////culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek),
                                                   GunGrup = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                        ? b.GunGrupAdi
                                                        : (nobetGrupGorevTipGunKural == null ? "Tanımsız gün grubu" : nobetGrupGorevTipGunKural.GunGrupAdi),
                                                   GunGrupId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                        ? b.GunGrupId
                                                        : (nobetGrupGorevTipGunKural == null ? 0 : nobetGrupGorevTipGunKural.GunGrupId),
                                                   MazeretId = 0,
                                                   IstekId = 0,
                                                   Mazeret = "yok",
                                                   MazeretTuru = "yok",
                                                   Gun = s.Tarih.Day,
                                                   Tarih = s.Tarih,
                                                   TakvimId = s.TakvimId,
                                                   NobetGorevTipAdi = s.NobetGorevTipAdi,
                                                   NobetGorevTipId = s.NobetGorevTipId,
                                                   NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                                                   NobetAltGrupId = s.NobetAltGrupId,
                                                   NobetAltGrupAdi = s.NobetAltGrupAdi
                                               }).ToList();

            return eczaneNobetSonuclarBirlesim;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> SiraliNobetYaz(int nobetUstGrupId)
        {
            var sonuclar = _eczaneNobetSonucService.GetDetaylarUstGrupBaslamaTarihindenOnce(nobetUstGrupId);
            var sonuclarSirali = _eczaneNobetSonucService.GetSonuclar(sonuclar, nobetUstGrupId)
                .OrderBy(o => o.GunGrup)
                .ThenBy(o => o.Tarih)
                .ToList();

            return sonuclarSirali;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluSil(int[] ids)
        {
            _eczaneNobetSonucPlanlananDal.CokluSil(ids);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler)
        {
            _eczaneNobetSonucPlanlananDal.CokluEkle(eczaneNobetCozumler);
        }
    }
}