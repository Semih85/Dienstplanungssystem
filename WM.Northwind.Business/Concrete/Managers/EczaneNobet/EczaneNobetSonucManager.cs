using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.Aspects.PostSharp.TranstionAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class EczaneNobetSonucManager : IEczaneNobetSonucService
    {
        #region ctor
        private IEczaneNobetSonucDal _eczaneNobetSonucDal;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetGrupService _nobetGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private IBayramService _bayramService;
        private INobetGrupGorevTipTakvimOzelGunService _nobetGrupGorevTipTakvimOzelGunService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private IEczaneNobetDegisimService _eczaneNobetDegisimService;

        public EczaneNobetSonucManager(IEczaneNobetSonucDal eczaneNobetSonucDal,
                                       IEczaneNobetOrtakService eczaneNobetOrtakService,
                                       IEczaneNobetGrupService eczaneNobetGrupService,
                                       INobetGrupService nobetGrupService,
                                       INobetUstGrupService nobetUstGrupService,
                                       IBayramService bayramService,
                                       INobetGrupGorevTipTakvimOzelGunService nobetGrupGorevTipTakvimOzelGunService,
                                       INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
                                       INobetGrupGorevTipService nobetGrupGorevTipService,
                                       IEczaneNobetMazeretService eczaneNobetMazeretService,
                                       IEczaneNobetIstekService eczaneNobetIstekService,
                                       IEczaneNobetGrupAltGrupService eczaneNobetGrupAltGrupService,
                                       IEczaneNobetDegisimService eczaneNobetDegisimService
                                      )
        {
            _eczaneNobetSonucDal = eczaneNobetSonucDal;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _nobetGrupService = nobetGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _bayramService = bayramService;
            _nobetGrupGorevTipTakvimOzelGunService = nobetGrupGorevTipTakvimOzelGunService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _eczaneNobetGrupAltGrupService = eczaneNobetGrupAltGrupService;
            _eczaneNobetDegisimService = eczaneNobetDegisimService;
        }
        #endregion

        #region Listele, Ekle, sil, güncelle

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int Id)
        {
            _eczaneNobetSonucDal.Delete(new EczaneNobetSonuc { Id = Id });
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetSonuc eczaneNobetSonuc)
        {
            _eczaneNobetSonucDal.Insert(eczaneNobetSonuc);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetSonuc eczaneNobetSonuc)
        {
            _eczaneNobetSonucDal.Update(eczaneNobetSonuc);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluSil(int[] ids)
        {
            _eczaneNobetSonucDal.CokluSil(ids);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler)
        {
            _eczaneNobetSonucDal.CokluEkle(eczaneNobetCozumler);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void UpdateSonuclarInsertDegisim(EczaneNobetSonuc eczaneNobetSonuc, EczaneNobetDegisim eczaneNobetDegisim)
        {
            Update(eczaneNobetSonuc);
            _eczaneNobetDegisimService.Insert(eczaneNobetDegisim);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void UpdateSonuclarInsertDegisim(List<NobetDegisim> nobetDegisimler)
        {
            foreach (var nobetDegisim in nobetDegisimler)
            {
                Update(nobetDegisim.EczaneNobetSonuc);
                _eczaneNobetDegisimService.Insert(nobetDegisim.EczaneNobetDegisim);
            }
        }

        public EczaneNobetSonuc GetById(int Id)
        {
            return _eczaneNobetSonucDal.Get(x => x.Id == Id);
        }

        public EczaneNobetSonucDetay2 GetDetay2ById(int Id)
        {
            return _eczaneNobetSonucDal.GetDetay(x => x.Id == Id);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public DateTime GetSonNobetTarihi(int nobetUstGrupId)
        {
            var sonuclar = _eczaneNobetSonucDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);

            return sonuclar.Count == 0
                ? _nobetUstGrupService.GetById(nobetUstGrupId).BaslangicTarihi.AddDays(-1)
                : _eczaneNobetSonucDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId).Max(s => s.Tarih);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonuc> GetList()
        {
            return _eczaneNobetSonucDal.GetList();
        }

        #endregion

        #region detaylar

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetSonucDal.GetDetayList(w => w.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylarByEczaneNobetGrupId(int eczaneNobetGrupId)
        {
            return _eczaneNobetSonucDal.GetDetayList(w => w.EczaneNobetGrupId == eczaneNobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylar()
        {
            return _eczaneNobetSonucDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylar(List<int> nobetGrupIdList)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylar(int yil, int ay, int nobetUstGrupId)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => x.Tarih.Year == yil && x.Tarih.Month == ay && x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi) && x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int[] nobetGrupIdList)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi) && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylarGunluk(DateTime nobetTarihi, int nobetUstGrupId)
        {
            var sonuclar = _eczaneNobetSonucDal.GetDetayList(x => x.Tarih == nobetTarihi && x.NobetUstGrupId == nobetUstGrupId);

            return sonuclar;
            //sonuclar.Count == 0
            //? throw new Exception($"{nobetTarihi} tarihinde nöbet tutan eczane bulunmamaktadır.")
            //: sonuclar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylarUstGrupBaslamaTarihindenSonra(int[] nobetGrupIdList)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId) && x.Tarih >= x.NobetUstGrupBaslamaTarihi);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylarUstGrupBaslamaTarihindenSonra(int nobetUstGrupId)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId && x.Tarih >= x.NobetUstGrupBaslamaTarihi);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylarUstGrupBaslamaTarihindenOnce(int nobetUstGrupId)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId && x.Tarih < x.NobetUstGrupBaslamaTarihi);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylar2ByYilAyNobetGrup(int yil, int ay, int nobetGrupId, bool buAyVeSonrasi)
        {
            return buAyVeSonrasi == true
                ? _eczaneNobetSonucDal.GetDetayList(x => x.Tarih.Year == yil
                                                        && x.Tarih.Month >= ay
                                                       && (x.NobetGrupId == nobetGrupId || nobetGrupId == 0))
                : _eczaneNobetSonucDal.GetDetayList(x => x.Tarih.Year == yil
                                                        && x.Tarih.Month == ay
                                                       && (x.NobetGrupId == nobetGrupId || nobetGrupId == 0));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylarAylik2(int yil, int ay, int nobetUstGrupId)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => x.Tarih.Year == yil && x.Tarih.Month == ay && x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylarYillikKumulatif(int yil, int ay, int nobetUstGrupId)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => (x.Tarih.Year == yil && x.Tarih.Month <= ay) && x.NobetUstGrupId == nobetUstGrupId);
        }

        public List<EczaneNobetSonucDetay2> GetDetaylarYillikKumulatif(int yil, int ay, List<int> eczaneIdList)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => (x.Tarih.Year == yil && x.Tarih.Month <= ay) && eczaneIdList.Contains(x.EczaneId));
        }
        #endregion

        #region Nöbet istatistik

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar()
        {
            var sonuclar = GetDetaylar();

            return GetSonuclar(sonuclar);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar(nobetUstGrupId);

            return GetSonuclar(sonuclar, nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(int yil, int ay, int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar(yil, ay, nobetUstGrupId);

            return GetSonuclar(sonuclar, nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar(baslangicTarihi, bitisTarihi, nobetUstGrupId);

            return GetSonuclar(sonuclar, baslangicTarihi, bitisTarihi, nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(List<EczaneNobetSonucDetay2> eczaneNobetSonucDetaylar, int nobetUstGrupId)
        {
            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(nobetUstGrupId);
            var mazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrupId);
            var istekler = _eczaneNobetIstekService.GetDetaylar(nobetUstGrupId);
            //var culture = new CultureInfo("tr-TR");

            var sonuclar = EczaneNobetSonucBirlesim(nobetGrupGorevTipGunKurallar, eczaneNobetSonucDetaylar, nobetGrupGorevTipTakvimOzelGunler, mazeretler, istekler);

            return sonuclar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        private List<EczaneNobetSonucListe2> GetSonuclar(List<EczaneNobetSonucDetay2> eczaneNobetSonucDetaylar)
        {
            var nobetUstGrupId = eczaneNobetSonucDetaylar.Select(s => s.NobetUstGrupId).Distinct().FirstOrDefault();

            //var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(nobetUstGrupId);
            var mazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrupId);
            var istekler = _eczaneNobetIstekService.GetDetaylar(nobetUstGrupId);
            //var culture = new CultureInfo("tr-TR");

            var liste = EczaneNobetSonucBirlesim(nobetGrupGorevTipGunKurallar, eczaneNobetSonucDetaylar, nobetGrupGorevTipTakvimOzelGunler, mazeretler, istekler);

            return liste;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        private List<EczaneNobetSonucListe2> GetSonuclar(List<EczaneNobetSonucDetay2> eczaneNobetSonucDetaylar, DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            //var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetUstGrupId);
            var mazeretler = _eczaneNobetMazeretService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetUstGrupId);
            var istekler = _eczaneNobetIstekService.GetDetaylar(nobetUstGrupId);
            //var culture = new CultureInfo("tr-TR");

            var liste = EczaneNobetSonucBirlesim(nobetGrupGorevTipGunKurallar, eczaneNobetSonucDetaylar, nobetGrupGorevTipTakvimOzelGunler, mazeretler, istekler);

            return liste;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(DateTime baslangicTarihi, DateTime bitisTarihi, List<EczaneNobetSonucListe2> eczaneNobetSonuclar)
        {
            return eczaneNobetSonuclar.Where(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi)).ToList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclarUstGrupBaslamaTarihindenSonra(int[] nobetGrupIdList)
        {
            var sonuclar = GetDetaylarUstGrupBaslamaTarihindenSonra(nobetGrupIdList);

            return GetSonuclar(sonuclar);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclarUstGrupBaslamaTarihindenSonra(int nobetUstGrupId)
        {
            var sonuclar = GetDetaylarUstGrupBaslamaTarihindenSonra(nobetUstGrupId);

            return GetSonuclar(sonuclar);
        }

        private List<EczaneNobetSonucListe2> EczaneNobetSonucBirlesim(
            List<NobetGrupGorevTipGunKuralDetay> nobetGrupGorevTipGunKurallar,
            List<EczaneNobetSonucDetay2> eczaneNobetSonuclar,
            List<NobetGrupGorevTipTakvimOzelGunDetay> nobetGrupGorevTipTakvimOzelGunler,
            List<EczaneNobetMazeretDetay> mazeretler,
            List<EczaneNobetIstekDetay> istekler)
        {
            var eczaneNobetSonuclarBirlesim = (from s in eczaneNobetSonuclar
                                               from b in nobetGrupGorevTipTakvimOzelGunler
                                                              .Where(w => w.TakvimId == s.TakvimId
                                                                      && w.NobetGrupGorevTipId == s.NobetGrupGorevTipId).DefaultIfEmpty()
                                               from m in mazeretler
                                                              .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId
                                                                        && w.TakvimId == s.TakvimId).DefaultIfEmpty()
                                               from i in istekler
                                                    .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId
                                                                        && w.TakvimId == s.TakvimId).DefaultIfEmpty()
                                               select new EczaneNobetSonucListe2
                                               {
                                                   Id = s.Id,
                                                   Yil = s.Tarih.Year,
                                                   Ay = s.Tarih.Month,
                                                   EczaneNobetGrupId = s.EczaneNobetGrupId,
                                                   EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrupBaslamaTarihi,
                                                   EczaneId = s.EczaneId,
                                                   EczaneAdi = s.EczaneAdi,
                                                   NobetGrupId = s.NobetGrupId,
                                                   NobetGrupAdi = s.NobetGrupAdi,
                                                   NobetUstGrupId = s.NobetUstGrupId,
                                                   NobetUstGrupBaslamaTarihi = s.NobetUstGrupBaslamaTarihi,
                                                   SonucTuru = "Kesin",
                                                   NobetGunKuralId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                        ? b.NobetGunKuralId
                                                        : (int)s.Tarih.DayOfWeek + 1,
                                                   GunTanim = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                        ? b.NobetGunKuralAdi
                                                        : (nobetGrupGorevTipGunKurallar.SingleOrDefault(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                                                      && w.NobetGunKuralId == (int)s.Tarih.DayOfWeek + 1).NobetGunKuralAdi ?? "Tanımsız gün kuralı"),
                                                   //culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek),
                                                   GunGrup = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                        ? b.GunGrupAdi
                                                        : (nobetGrupGorevTipGunKurallar.SingleOrDefault(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                                                      && w.NobetGunKuralId == (int)s.Tarih.DayOfWeek + 1).GunGrupAdi ?? "Tanımsız gün grubu"),
                                                   GunGrupId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                        ? b.GunGrupId
                                                        : (nobetGrupGorevTipGunKurallar.SingleOrDefault(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                                                      && w.NobetGunKuralId == (int)s.Tarih.DayOfWeek + 1) == null
                                                                      ? 0
                                                                      : nobetGrupGorevTipGunKurallar.SingleOrDefault(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                                                      && w.NobetGunKuralId == (int)s.Tarih.DayOfWeek + 1).GunGrupId),
                                                   Gun = s.Tarih.Day,
                                                   Tarih = s.Tarih,
                                                   TakvimId = s.TakvimId,
                                                   MazeretId = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretId : 0,
                                                   IstekId = (i?.TakvimId == s.TakvimId && i?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? i.IstekId : 0,
                                                   Mazeret = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretAdi : null,
                                                   MazeretTuru = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretTuru : null,
                                                   NobetGorevTipAdi = s.NobetGorevTipAdi,
                                                   NobetGorevTipId = s.NobetGorevTipId,
                                                   NobetAltGrupId = s.NobetAltGrupId,
                                                   NobetAltGrupAdi = s.NobetAltGrupAdi
                                               }).ToList();

            return eczaneNobetSonuclarBirlesim;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistik(List<EczaneNobetSonucListe2> eczaneNobetSonuc)
        {
            var liste = eczaneNobetSonuc
               .GroupBy(g => new
               {
                   g.GunGrup,
                   g.NobetGunKuralId,
                   g.EczaneNobetGrupId,
                   g.EczaneId,
                   g.EczaneAdi,
                   g.NobetGrupId,
                   g.NobetGrupAdi,
                   g.NobetGorevTipId,
                   g.EczaneNobetGrupBaslamaTarihi,
                   g.NobetUstGrupId
               })
               .Select(s => new EczaneNobetGrupGunKuralIstatistik
               {
                   NobetUstGrupId = s.Key.NobetUstGrupId,
                   GunGrup = s.Key.GunGrup,
                   NobetGunKuralId = s.Key.NobetGunKuralId,
                   EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                   EczaneNobetGrupBaslamaTarihi = s.Key.EczaneNobetGrupBaslamaTarihi,
                   EczaneId = s.Key.EczaneId,
                   EczaneAdi = s.Key.EczaneAdi,
                   NobetGrupId = s.Key.NobetGrupId,
                   NobetGrupAdi = s.Key.NobetGrupAdi,
                   NobetGorevTipId = s.Key.NobetGorevTipId,
                   IlkNobetTarihi = s.Min(c => c.Tarih),
                   SonNobetTarihi = s.Max(c => c.Tarih),
                   NobetSayisi = s.Count(),
                   NobetSayisiGercek = s.Count(c => c.Tarih >= c.NobetUstGrupBaslamaTarihi //new DateTime(2018, 6, 1)
                   ),
               }).ToList();

            //var eczane = liste.Where(w => w.EczaneAdi == "ALYA").ToList();

            return liste;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistik(List<EczaneNobetGrupDetay> eczaneNobetGruplar, List<EczaneNobetSonucListe2> eczaneNobetSonuc)
        {
            var enSonNobetler = GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetSonuc);

            //var eczaneSonucuOlan = enSonNobetler.Where(w => w.EczaneAdi == "ALYA").ToList();

            var sonucuOlanGunler = enSonNobetler
                .Select(s => new
                {
                    s.NobetGunKuralId,
                    s.NobetGorevTipId,
                    s.GunGrup
                })
                .Distinct()
                .OrderBy(o => o.NobetGorevTipId)
                .ThenBy(t => t.NobetGunKuralId).ToList();

            var varsayilanBaslangicNobetTarihi = new DateTime(2012, 1, 1);

            foreach (var nobetGunKural in sonucuOlanGunler)
            {
                var nobetDurumlari = enSonNobetler
                    .Where(w => w.NobetGunKuralId == nobetGunKural.NobetGunKuralId
                             && w.NobetGorevTipId == nobetGunKural.NobetGorevTipId)
                    .Select(s => s.EczaneNobetGrupId).ToList();

                var sonucuOlmayanlar = eczaneNobetGruplar
                    .Where(w => !nobetDurumlari.Contains(w.Id)
                             && w.NobetGorevTipId == nobetGunKural.NobetGorevTipId).ToList();

                var istikametMi = sonucuOlmayanlar.Where(w => w.Id == 920 && w.NobetGorevTipId == 1).Count(); //istikamet eczanesi
                var cumaDegilse = nobetGunKural.NobetGunKuralId != 6 && nobetGunKural.GunGrup == "Hafta İçi"; //sadece cuma günü nöbet tutabilir

                if (istikametMi > 0
                    && cumaDegilse)
                    continue;

                if (sonucuOlmayanlar.Count > 0)
                {
                    foreach (var eczaneNobetGrup in sonucuOlmayanlar)
                    {
                        //varsayilanBaslangicNobetTarihi = eczaneNobetGrup.BaslangicTarihi;
                        //anahtar liste başlangıç sırası
                        enSonNobetler.Add(new EczaneNobetGrupGunKuralIstatistik
                        {
                            EczaneId = eczaneNobetGrup.EczaneId,
                            EczaneAdi = eczaneNobetGrup.EczaneAdi,
                            NobetGrupAdi = eczaneNobetGrup.NobetGrupAdi,
                            NobetAltGrupId = 0,
                            EczaneNobetGrupId = eczaneNobetGrup.Id,
                            IlkNobetTarihi = varsayilanBaslangicNobetTarihi,
                            SonNobetTarihi = varsayilanBaslangicNobetTarihi,
                            NobetGorevTipId = nobetGunKural.NobetGorevTipId,
                            NobetGunKuralId = nobetGunKural.NobetGunKuralId,
                            GunGrup = nobetGunKural.GunGrup,
                            NobetGrupId = eczaneNobetGrup.NobetGrupId,
                            NobetSayisi = 1,
                            EczaneNobetGrupBaslamaTarihi = eczaneNobetGrup.BaslangicTarihi,
                            NobetUstGrupId = eczaneNobetGrup.NobetUstGrupId
                        });
                    }
                }
            }

            //var eczaneTumu = enSonNobetler.Where(w => w.EczaneAdi == "ALYA").ToList();

            return enSonNobetler;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupGunKuralIstatistikYatay> GetEczaneNobetGrupGunKuralIstatistikYatay(List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistik)
        {
            //var a = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetGorevTipId).Distinct().ToList();
            //var eczaneNobetGruplar = eczaneNobetGrupGunKuralIstatistik.Select(s => s.EczaneNobetGrupId).Distinct().ToList();
            //var nobetAltGruplar = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetAltGrupId).Distinct().ToList();
            //var NobetGorevTiler = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetGorevTipId).Distinct().ToList();
            ;
            var nobetUstGrupId = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetUstGrupId).Distinct().SingleOrDefault();

            var eczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistik
               .GroupBy(g => new
               {
                   g.EczaneNobetGrupId,
                   //g.EczaneNobetGrupBaslamaTarihi,
                   g.EczaneId,
                   g.EczaneAdi,
                   g.NobetGrupAdi,
                   g.NobetGrupId,
                   g.NobetGorevTipId,
                   g.NobetAltGrupId
               })
               .Select(s => new EczaneNobetGrupGunKuralIstatistikYatay
               {
                   EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                   EczaneId = s.Key.EczaneId,
                   EczaneAdi = s.Key.EczaneAdi,
                   NobetGrupId = s.Key.NobetGrupId,
                   NobetGrupAdi = s.Key.NobetGrupAdi,
                   NobetGorevTipId = s.Key.NobetGorevTipId,
                   NobetAltGrupId = s.Key.NobetAltGrupId,

                   NobetSayisiToplam = s.Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihi = s.Sum(f => f.NobetSayisi) > 0
                       ? s.Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.NobetGunKuralId == 7).Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.NobetGunKuralId == 1).Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.NobetGunKuralId > 7).Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiArife = s.Where(w => w.NobetGunKuralId == 10).Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihiArife = s.Where(w => w.NobetGunKuralId == 10).Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.NobetGunKuralId == 10).Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiHaftaIci = s.Where(w => w.GunGrup == "Hafta İçi").Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihiHaftaIci = s.Where(w => w.GunGrup == "Hafta İçi").Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.GunGrup == "Hafta İçi").Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiPazartesi = s.Where(w => w.NobetGunKuralId == 2).Sum(f => f.NobetSayisiGercek),
                   NobetSayisiSali = s.Where(w => w.NobetGunKuralId == 3).Sum(f => f.NobetSayisiGercek),
                   NobetSayisiCarsamba = s.Where(w => w.NobetGunKuralId == 4).Sum(f => f.NobetSayisiGercek),
                   NobetSayisiPersembe = s.Where(w => w.NobetGunKuralId == 5).Sum(f => f.NobetSayisiGercek),
                   NobetSayisiCuma = s.Where(w => w.NobetGunKuralId == 6).Sum(f => f.NobetSayisiGercek),
                   NobetSayisiDiniBayram = s.Where(w => w.NobetGunKuralId == 8).Sum(f => f.NobetSayisiGercek),
                   NobetSayisiMilliBayram = s.Where(w => w.NobetGunKuralId == 9).Sum(f => f.NobetSayisiGercek)

               }).ToList();

            //var eczane = eczaneNobetGrupGunKuralIstatistikYatay.Where(w => w.EczaneAdi == "ALYA").ToList();

            return eczaneNobetGrupGunKuralIstatistikYatay;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupGunKuralIstatistikYatay> GetEczaneBazliGunKuralIstatistikYatay(List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistik)
        {
            //var a = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetGorevTipId).Distinct().ToList();
            //var eczaneNobetGruplar = eczaneNobetGrupGunKuralIstatistik.Select(s => s.EczaneNobetGrupId).Distinct().ToList();
            //var nobetAltGruplar = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetAltGrupId).Distinct().ToList();
            //var NobetGorevTiler = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetGorevTipId).Distinct().ToList();
            ;

            var eczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistik
                 .GroupBy(g => new
                 {
                     //g.EczaneNobetGrupId,
                     //g.EczaneNobetGrupBaslamaTarihi,
                     g.EczaneId,
                     g.EczaneAdi,
                     g.NobetGrupAdi,
                     g.NobetGrupId,
                     //g.NobetGorevTipId,
                     g.NobetAltGrupId
                 })
                 .Select(s => new EczaneNobetGrupGunKuralIstatistikYatay
                 {
                     //EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                     EczaneId = s.Key.EczaneId,
                     EczaneAdi = s.Key.EczaneAdi,
                     NobetGrupId = s.Key.NobetGrupId,
                     NobetGrupAdi = s.Key.NobetGrupAdi,
                     //NobetGorevTipId = s.Key.NobetGorevTipId,
                     NobetAltGrupId = s.Key.NobetAltGrupId,

                     NobetSayisiToplam = s.Sum(f => f.NobetSayisiGercek),
                     SonNobetTarihi = s.Sum(f => f.NobetSayisi) > 0
                         ? s.Max(f => f.SonNobetTarihi)
                         : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                     NobetSayisiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisiGercek),
                     SonNobetTarihiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisi) > 0
                         ? s.Where(w => w.NobetGunKuralId == 7).Max(f => f.SonNobetTarihi)
                         : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                     NobetSayisiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisiGercek),
                     SonNobetTarihiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisi) > 0
                         ? s.Where(w => w.NobetGunKuralId == 1).Max(f => f.SonNobetTarihi)
                         : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                     NobetSayisiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisiGercek),
                     SonNobetTarihiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisi) > 0
                         ? s.Where(w => w.NobetGunKuralId > 7).Max(f => f.SonNobetTarihi)
                         : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                     NobetSayisiArife = s.Where(w => w.NobetGunKuralId == 10).Sum(f => f.NobetSayisiGercek),
                     SonNobetTarihiArife = s.Where(w => w.NobetGunKuralId == 10).Sum(f => f.NobetSayisi) > 0
                         ? s.Where(w => w.NobetGunKuralId == 10).Max(f => f.SonNobetTarihi)
                         : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                     NobetSayisiHaftaIci = s.Where(w => w.GunGrup == "Hafta İçi").Sum(f => f.NobetSayisiGercek),
                     SonNobetTarihiHaftaIci = s.Where(w => w.GunGrup == "Hafta İçi").Sum(f => f.NobetSayisi) > 0
                         ? s.Where(w => w.GunGrup == "Hafta İçi").Max(f => f.SonNobetTarihi)
                         : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                     NobetSayisiPazartesi = s.Where(w => w.NobetGunKuralId == 2).Sum(f => f.NobetSayisiGercek),
                     NobetSayisiSali = s.Where(w => w.NobetGunKuralId == 3).Sum(f => f.NobetSayisiGercek),
                     NobetSayisiCarsamba = s.Where(w => w.NobetGunKuralId == 4).Sum(f => f.NobetSayisiGercek),
                     NobetSayisiPersembe = s.Where(w => w.NobetGunKuralId == 5).Sum(f => f.NobetSayisiGercek),
                     NobetSayisiCuma = s.Where(w => w.NobetGunKuralId == 6).Sum(f => f.NobetSayisiGercek),
                     NobetSayisiDiniBayram = s.Where(w => w.NobetGunKuralId == 8).Sum(f => f.NobetSayisiGercek),
                     NobetSayisiMilliBayram = s.Where(w => w.NobetGunKuralId == 9).Sum(f => f.NobetSayisiGercek)

                 }).ToList();

            //var eczane = eczaneNobetGrupGunKuralIstatistikYatay.Where(w => w.EczaneAdi == "ALYA").ToList();

            return eczaneNobetGrupGunKuralIstatistikYatay;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstatistikGunFarki> EczaneNobetIstatistikGunFarkiHesapla(int nobetUstGrupId)
        {
            var sonuclar = GetSonuclar(nobetUstGrupId);
            var gunDegerler = sonuclar.Select(s => s.NobetGunKuralId).Distinct().OrderBy(o => o).ToList();
            var gunFarki = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar);

            return gunFarki;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> OncekiAylardaAyniGunNobetTutanlar(DateTime baslangicTarihi, List<EczaneNobetSonucListe2> eczaneNobetSonuclarOncekiAylar, int indisId, int oncekiBakilacakAylar = 2)
        {
            var oncekiAylardaAyniGunNobetTutanEczaneGruplar = new List<EczaneGrupDetay>();

            //if (!oncekiAylarAyniGunNobet.PasifMi)
            //{
            var oncekiBakilacakAySayisi = -oncekiBakilacakAylar;

            if (!(oncekiBakilacakAySayisi >= -6 && oncekiBakilacakAySayisi < 0))
            {
                throw new Exception("Geriye dönük aynı gün nöbet tutanlar en az 0 en fazla 6 ay engellenebilir");
            }

            var oncekiAylardaBakilacakSonuclar = eczaneNobetSonuclarOncekiAylar.Where(w => w.Tarih >= baslangicTarihi.AddMonths(oncekiBakilacakAySayisi)).ToList();

            var ocnekiNobetlerTarihAraligi = oncekiAylardaBakilacakSonuclar
                .Select(s => new
                {
                    s.TakvimId,
                    s.Tarih,
                    s.TarihAciklama
                })
                .Distinct()
                .OrderBy(o => o.Tarih).ToList();

            foreach (var tarih in ocnekiNobetlerTarihAraligi)
            {
                var gunlukSonuclar = oncekiAylardaBakilacakSonuclar.Where(w => w.TakvimId == tarih.TakvimId).ToList();

                foreach (var sonuc in gunlukSonuclar)
                {
                    oncekiAylardaAyniGunNobetTutanEczaneGruplar
                        .Add(new EczaneGrupDetay
                        {
                            EczaneGrupTanimId = indisId + tarih.TakvimId,
                            EczaneId = sonuc.EczaneId,
                            ArdisikNobetSayisi = 0,
                            NobetUstGrupId = sonuc.NobetUstGrupId,
                            EczaneGrupTanimAdi = $"{tarih.TarihAciklama} tarihindeki nöbetler",
                            EczaneGrupTanimTipAdi = "Aynı gün nöbet",
                            EczaneGrupTanimTipId = -1,
                            NobetGrupId = sonuc.NobetGrupId,
                            EczaneAdi = sonuc.EczaneAdi,
                            NobetGrupAdi = sonuc.NobetGrupAdi,
                            EczaneNobetGrupId = sonuc.EczaneNobetGrupId,
                            AyniGunNobetTutabilecekEczaneSayisi = 1
                            //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                        });
                }
            }
            //}
            return oncekiAylardaAyniGunNobetTutanEczaneGruplar;
        }

        #region eski

        public List<EczaneNobetSonucListe2> GetSonuclarYillikKumulatif(int yil, int ay, List<int> eczaneIdList)
        {
            return GetSonucListe(GetDetaylarYillikKumulatif(yil, ay, eczaneIdList));
        }

        public List<EczaneNobetSonucListe2> GetSonuclarYillikKumulatif(int yil, int ay, int nobetUstGrupId)
        {
            return GetSonucListe(GetDetaylarYillikKumulatif(yil, ay, nobetUstGrupId));
        }

        private List<EczaneNobetSonucListe2> GetSonucListe(List<EczaneNobetSonucDetay2> sonucDetaylar)
        {
            return sonucDetaylar
                            .Select(s => new EczaneNobetSonucListe2
                            {
                                Ay = s.Tarih.Month,
                                EczaneAdi = s.EczaneAdi,
                                EczaneId = s.EczaneId,
                                EczaneNobetGrupId = s.EczaneNobetGrupId,
                                Gun = s.Tarih.Day,
                                //GunGrup = s,
                                //GunTanim = s.GunTanim,
                                //HaftaninGunu = s.HaftaninGunu,
                                Id = s.Id,
                                NobetGorevTipAdi = s.NobetGorevTipAdi,
                                NobetGorevTipId = s.NobetGorevTipId,
                                NobetGrupAdi = s.NobetGrupAdi,
                                NobetGrupId = s.NobetGrupId,
                                //NobetGunKuralId = s.NobetGunKuralId,
                                NobetUstGrupId = s.NobetUstGrupId,
                                TakvimId = s.TakvimId,
                                Tarih = s.Tarih,
                                Yil = s.Tarih.Year
                            }).ToList();
        }

        public List<EczaneNobetIstatistik> GetEczaneNobetIstatistik2(List<int> nobetGrupIdList)
        {
            var eczaneNobetSonuclar = GetDetaylarForIstatistik2(nobetGrupIdList);

            var eczaneNobetSonuclardaOlmayanEczaneler = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdList)
                .Where(w => !eczaneNobetSonuclar.Select(s => s.EczaneNobetGrupId).Contains(w.Id))
                .Select(c => new
                {
                    c.Id,
                    c.EczaneId,
                    c.NobetGrupId,
                    c.NobetGorevTipId,
                    c.NobetGorevTipAdi,
                    c.NobetGrupGorevTipId
                });

            //sonuç tablosunda olmayanların listeye eklenmesi için
            foreach (var t in eczaneNobetSonuclardaOlmayanEczaneler)
            {
                eczaneNobetSonuclar.Add(new EczaneNobetSonucGunKural()
                {
                    EczaneNobetGrupId = t.Id,
                    EczaneId = t.EczaneId,
                    NobetGrupId = t.NobetGrupId,
                    NobetGorevTipId = 1,
                    NobetGunKuralId = 0
                });
            }

            var nobetIstatistik = eczaneNobetSonuclar
                                   .GroupBy(t => new
                                   {
                                       t.EczaneNobetGrupId,
                                       t.EczaneId,
                                       t.NobetGrupId,
                                       t.NobetGorevTipId
                                   })
                                   .Select(grouped => new EczaneNobetIstatistik
                                   {
                                       EczaneNobetGrupId = grouped.Key.EczaneNobetGrupId,
                                       EczaneId = grouped.Key.EczaneId,
                                       NobetGrupId = grouped.Key.NobetGrupId,
                                       NobetGorevTipId = grouped.Key.NobetGorevTipId,

                                       Pazar = grouped.Count(c => c.NobetGunKuralId == 1),
                                       Pazartesi = grouped.Count(c => c.NobetGunKuralId == 2),
                                       Sali = grouped.Count(c => c.NobetGunKuralId == 3),
                                       Carsamba = grouped.Count(c => c.NobetGunKuralId == 4),
                                       Persembe = grouped.Count(c => c.NobetGunKuralId == 5),
                                       Cuma = grouped.Count(c => c.NobetGunKuralId == 6),
                                       Cumartesi = grouped.Count(c => c.NobetGunKuralId == 7),
                                       DiniBayram = grouped.Count(c => c.NobetGunKuralId == 8),
                                       MilliBayram = grouped.Count(c => c.NobetGunKuralId == 9),
                                       ToplamBayram = grouped.Count(c => c.NobetGunKuralId > 7),
                                       ToplamHaftaIci = grouped.Count(c => (c.NobetGunKuralId >= 2 && c.NobetGunKuralId <= 7)),
                                       ToplamCumaCumartesi = grouped.Count(c => (c.NobetGunKuralId >= 6 && c.NobetGunKuralId <= 7)),
                                       Toplam = grouped.Count(c => c.NobetGunKuralId > 0)
                                   }).ToList();

            return nobetIstatistik;
        }

        public List<EczaneNobetSonucGunKural> GetDetaylarForIstatistik()
        {
            var sonuclar = GetDetaylar()
                .Select(e => new
                {
                    e.NobetGorevTipId,
                    e.TakvimId,
                    e.EczaneNobetGrupId,
                    e.EczaneAdi,
                    e.NobetGrupAdi,
                    e.NobetGrupId,
                    e.Tarih,
                    e.EczaneId
                });

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar()
                .Select(v => new
                {
                    v.NobetGorevTipAdi,
                    v.NobetGorevTipId,
                    v.NobetGrupId,
                    v.Id
                });

            var bayramlar = _bayramService.GetDetaylar();

            var liste2 = (from s in sonuclar
                          join b in nobetGrupGorevTipler
                              on new { s.NobetGrupId, s.NobetGorevTipId }
                              equals new { b.NobetGrupId, b.NobetGorevTipId }
                          select new
                          {
                              s.NobetGorevTipId,
                              s.TakvimId,
                              s.EczaneNobetGrupId,
                              s.EczaneAdi,
                              s.NobetGrupAdi,
                              s.NobetGrupId,
                              NobetGrupGorevTipId = b.Id,
                              s.Tarih,
                              s.EczaneId
                          }).ToList();

            var liste = (from s in liste2
                         from b in bayramlar
                                       .Where(w => w.TakvimId == s.TakvimId
                                                && w.NobetGrupGorevTipId == s.NobetGrupGorevTipId).DefaultIfEmpty()
                         select new
                         {
                             s.EczaneNobetGrupId,
                             s.EczaneId,
                             s.NobetGrupId,
                             s.NobetGorevTipId,
                             NobetGunKuralId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId) ? b.NobetGunKuralId : (int)s.Tarih.DayOfWeek + 1,
                         }).ToList();

            return liste
                .Select(t => new EczaneNobetSonucGunKural
                {
                    EczaneNobetGrupId = t.EczaneNobetGrupId,
                    EczaneId = t.EczaneId,
                    NobetGrupId = t.NobetGrupId,
                    NobetGorevTipId = t.NobetGorevTipId,
                    NobetGunKuralId = t.NobetGunKuralId
                }).ToList();
        }

        public List<EczaneNobetSonucGunKural> GetDetaylarForIstatistik2(List<int> nobetGrupIdList)
        {
            var sonuclar = GetDetaylar(nobetGrupIdList)
                .Select(e => new
                {
                    e.NobetGorevTipId,
                    e.TakvimId,
                    e.EczaneNobetGrupId,
                    e.EczaneAdi,
                    e.NobetGrupAdi,
                    e.NobetGrupId,
                    e.Tarih,
                    e.EczaneId
                });

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar()
                .Select(v => new
                {
                    v.NobetGorevTipAdi,
                    v.NobetGorevTipId,
                    v.NobetGrupId,
                    v.Id
                });

            var bayramlar = _bayramService.GetDetaylar();

            var liste2 = (from s in sonuclar
                          join b in nobetGrupGorevTipler
                              on new { s.NobetGrupId, s.NobetGorevTipId }
                              equals new { b.NobetGrupId, b.NobetGorevTipId }
                          select new
                          {
                              s.NobetGorevTipId,
                              s.TakvimId,
                              s.EczaneNobetGrupId,
                              s.EczaneAdi,
                              s.NobetGrupAdi,
                              s.NobetGrupId,
                              NobetGrupGorevTipId = b.Id,
                              s.Tarih,
                              s.EczaneId
                          }).ToList();

            var liste = (from s in liste2
                         from b in bayramlar
                                       .Where(w => w.TakvimId == s.TakvimId
                                                && w.NobetGrupGorevTipId == s.NobetGrupGorevTipId).DefaultIfEmpty()
                         select new
                         {
                             s.EczaneNobetGrupId,
                             s.EczaneId,
                             s.NobetGrupId,
                             s.NobetGorevTipId,
                             NobetGunKuralId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId) ? b.NobetGunKuralId : (int)s.Tarih.DayOfWeek + 1,
                         }).ToList();

            return liste
                .Select(t => new EczaneNobetSonucGunKural
                {
                    EczaneNobetGrupId = t.EczaneNobetGrupId,
                    EczaneId = t.EczaneId,
                    NobetGrupId = t.NobetGrupId,
                    NobetGorevTipId = t.NobetGorevTipId,
                    NobetGunKuralId = t.NobetGunKuralId
                }).ToList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupNobetSonuc> GetEczaneGrupNobetSonuc2(int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);

            var liste2 = (from s in sonuclar
                          join b in nobetGrupGorevTipler
                              on new { s.NobetGrupId, s.NobetGorevTipId }
                              equals new { b.NobetGrupId, b.NobetGorevTipId }
                          select new
                          {
                              s.NobetGorevTipId,
                              s.TakvimId,
                              s.EczaneNobetGrupId,
                              s.EczaneAdi,
                              s.NobetGrupAdi,
                              s.NobetGrupId,
                              NobetGrupGorevTipId = b.Id,
                              s.Tarih,
                              s.EczaneId,
                              s.NobetUstGrupId,
                              s.NobetUstGrupBaslamaTarihi
                          }).ToList();

            var bayramlar = _bayramService.GetDetaylar(nobetUstGrupId);
            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrupId);
            var culture = new CultureInfo("tr-TR");

            var eczaneNobetSonuclar = (from s in liste2
                                       from b in bayramlar
                                                     .Where(w => w.TakvimId == s.TakvimId
                                                              && w.NobetGrupId == s.NobetGrupId
                                                              && w.NobetGorevTipId == s.NobetGorevTipId).DefaultIfEmpty()
                                       from a in eczaneNobetGrupAltGruplar
                                                    .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId).DefaultIfEmpty()
                                       select new EczaneGrupNobetSonuc
                                       {
                                           EczaneNobetGrupId = s.EczaneNobetGrupId,
                                           EczaneId = s.EczaneId,
                                           EczaneAdi = s.EczaneAdi,
                                           NobetGrupAdi = s.NobetGrupAdi,
                                           NobetGorevTipId = s.NobetGorevTipId,
                                           TakvimId = s.TakvimId,
                                           NobetGrupId = s.NobetGrupId,
                                           Tarih = s.Tarih,
                                           NobetAltGrupId = (a?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? a.NobetAltGrupId : 0,
                                           NobetGunKuralId = (b?.TakvimId == s.TakvimId
                                                           && b?.NobetGrupId == s.NobetGrupId
                                                           && b?.NobetGorevTipId == s.NobetGorevTipId) ? b.NobetGunKuralId : (int)s.Tarih.DayOfWeek + 1,//SqlFunctions.DatePart("weekday", s.Tarih)
                                           GunGrup = s.NobetUstGrupId == 3
                                                         ? ((b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                            ? "Bayram"
                                                            : (s.Tarih.DayOfWeek == 0 || (int)s.Tarih.DayOfWeek == 6)
                                                            ? culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek)
                                                            : "Hafta İçi")
                                                         : ((b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                            ? "Bayram"
                                                            : s.Tarih.DayOfWeek == 0
                                                            ? "Pazar"
                                                            : "Hafta İçi"),
                                           NobetUstGrupId = s.NobetUstGrupId,
                                           NobetUstGrupBaslamaTarihi = s.NobetUstGrupBaslamaTarihi
                                       }).ToList();
            return eczaneNobetSonuclar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistik(int nobetUstGrupId)
        {
            var sonuclar = GetEczaneGrupNobetSonuc(nobetUstGrupId);

            var enSonNobetPazarlar = sonuclar
                   .Where(w => w.NobetGunKuralId == 1)
                   .GroupBy(g => new
                   {
                       g.NobetUstGrupId,
                       g.EczaneNobetGrupId,
                       g.EczaneId,
                       g.NobetGrupId,
                       g.NobetGorevTipId,
                       g.NobetAltGrupId
                   })
                   .Select(s => new EczaneNobetGrupGunKuralIstatistik
                   {
                       NobetUstGrupId = s.Key.NobetUstGrupId,
                       EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                       EczaneId = s.Key.EczaneId,
                       NobetAltGrupId = s.Key.NobetAltGrupId,
                       NobetGrupId = s.Key.NobetGrupId,
                       NobetGorevTipId = s.Key.NobetGorevTipId,
                       SonNobetTarihi = s.Max(c => c.Tarih),
                       NobetSayisi = s.Count()
                   }).ToList();

            return enSonNobetPazarlar;
        }//pazar günü

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupNobetSonuc> GetEczaneGrupNobetSonuc(int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrupId);

            var liste2 = (from s in sonuclar
                          join b in nobetGrupGorevTipler
                              on new { s.NobetGrupId, s.NobetGorevTipId }
                              equals new { b.NobetGrupId, b.NobetGorevTipId }
                          select new
                          {
                              s.NobetGorevTipId,
                              s.TakvimId,
                              s.EczaneNobetGrupId,
                              s.EczaneAdi,
                              s.NobetGrupAdi,
                              s.NobetGrupId,
                              NobetGrupGorevTipId = b.Id,
                              s.Tarih,
                              s.EczaneId,
                              s.NobetUstGrupId,
                              s.NobetUstGrupBaslamaTarihi
                          }).ToList();

            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(nobetUstGrupId);
            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrupId);
            var culture = new CultureInfo("tr-TR");

            var eczaneNobetSonuclar = (from s in liste2
                                       from b in nobetGrupGorevTipTakvimOzelGunler
                                                     .Where(w => w.TakvimId == s.TakvimId
                                                              && w.NobetGrupGorevTipId == s.NobetGrupGorevTipId).DefaultIfEmpty()
                                       from a in eczaneNobetGrupAltGruplar
                                                    .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId).DefaultIfEmpty()
                                       select new EczaneGrupNobetSonuc
                                       {
                                           EczaneNobetGrupId = s.EczaneNobetGrupId,
                                           EczaneId = s.EczaneId,
                                           EczaneAdi = s.EczaneAdi,
                                           NobetGrupAdi = s.NobetGrupAdi,
                                           NobetGorevTipId = s.NobetGorevTipId,
                                           TakvimId = s.TakvimId,
                                           NobetGrupId = s.NobetGrupId,
                                           Tarih = s.Tarih,
                                           NobetAltGrupId = (a?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? a.NobetAltGrupId : 0,
                                           NobetGunKuralId = (b?.TakvimId == s.TakvimId
                                                           && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId) ? b.NobetGunKuralId : (int)s.Tarih.DayOfWeek + 1,//SqlFunctions.DatePart("weekday", s.Tarih)
                                           GunGrup = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                                 ? b.GunGrupAdi
                                                 : nobetGrupGorevTipGunKurallar.SingleOrDefault(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                                    && w.NobetGunKuralId == (int)s.Tarih.DayOfWeek + 1).GunGrupAdi,
                                           NobetUstGrupId = s.NobetUstGrupId,
                                           NobetUstGrupBaslamaTarihi = s.NobetUstGrupBaslamaTarihi
                                       }).ToList();
            return eczaneNobetSonuclar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupNobetSonuc> GetEczaneGrupNobetSonuc(List<int> nobetGrupIdList)
        {
            var sonuclar = GetDetaylar(nobetGrupIdList);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetGrupIdList);
            var bayramlar = _bayramService.GetDetaylar(nobetGrupGorevTipler.Select(s => s.Id).ToList());

            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar();

            var liste2 = (from s in sonuclar
                          join b in nobetGrupGorevTipler
                              on new { s.NobetGrupId, s.NobetGorevTipId }
                              equals new { b.NobetGrupId, b.NobetGorevTipId }
                          select new
                          {
                              s.NobetGorevTipId,
                              s.TakvimId,
                              s.EczaneNobetGrupId,
                              s.EczaneAdi,
                              s.NobetGrupAdi,
                              s.NobetGrupId,
                              NobetGrupGorevTipId = b.Id,
                              s.Tarih,
                              s.EczaneId
                          }).ToList();

            var eczaneNobetSonuclar = (from s in liste2
                                       from b in bayramlar
                                                     .Where(w => w.TakvimId == s.TakvimId
                                                              && w.NobetGrupId == s.NobetGrupId
                                                              && w.NobetGorevTipId == s.NobetGorevTipId).DefaultIfEmpty()
                                       from a in eczaneNobetGrupAltGruplar
                                                    .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId).DefaultIfEmpty()
                                       select new EczaneGrupNobetSonuc
                                       {
                                           EczaneNobetGrupId = s.EczaneNobetGrupId,
                                           EczaneId = s.EczaneId,
                                           EczaneAdi = s.EczaneAdi,
                                           NobetGrupAdi = s.NobetGrupAdi,
                                           NobetGorevTipId = s.NobetGorevTipId,
                                           TakvimId = s.TakvimId,
                                           NobetGrupId = s.NobetGrupId,
                                           Tarih = s.Tarih,
                                           NobetAltGrupId = (a?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? a.NobetAltGrupId : 0,
                                           NobetGunKuralId = (b?.TakvimId == s.TakvimId
                                                           && b?.NobetGrupId == s.NobetGrupId
                                                           && b?.NobetGorevTipId == s.NobetGorevTipId) ? b.NobetGunKuralId : (int)s.Tarih.DayOfWeek + 1//SqlFunctions.DatePart("weekday", s.Tarih)
                                       }).ToList();
            return eczaneNobetSonuclar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistik(List<EczaneGrupNobetSonuc> eczaneGrupNobetSonuc)
        {
            return eczaneGrupNobetSonuc
               .GroupBy(g => new
               {
                   g.GunGrup,
                   g.NobetGunKuralId,
                   g.EczaneNobetGrupId,
                   g.EczaneId,
                   g.EczaneAdi,
                   g.NobetGrupId,
                   g.NobetGrupAdi,
                   g.NobetGorevTipId,
                   g.NobetUstGrupId
               })
               .Select(s => new EczaneNobetGrupGunKuralIstatistik
               {
                   NobetUstGrupId = s.Key.NobetUstGrupId,
                   GunGrup = s.Key.GunGrup,
                   NobetGunKuralId = s.Key.NobetGunKuralId,
                   EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                   EczaneId = s.Key.EczaneId,
                   EczaneAdi = s.Key.EczaneAdi,
                   NobetGrupId = s.Key.NobetGrupId,
                   NobetGrupAdi = s.Key.NobetGrupAdi,
                   NobetGorevTipId = s.Key.NobetGorevTipId,
                   IlkNobetTarihi = s.Min(c => c.Tarih),
                   SonNobetTarihi = s.Max(c => c.Tarih),
                   NobetSayisi = s.Count(),
                   NobetSayisiGercek = s.Count(c => c.Tarih >= c.NobetUstGrupBaslamaTarihi //new DateTime(2018, 6, 1)
                   ),
               }).ToList();
        }


        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistik(List<EczaneNobetGrupDetay> eczaneNobetGruplar, List<EczaneGrupNobetSonuc> eczaneGrupNobetSonuc)
        {
            var enSonNobetler = GetEczaneNobetGrupGunKuralIstatistik(eczaneGrupNobetSonuc);

            var sonucuOlanGunler = enSonNobetler
                .Select(s => new
                {
                    s.NobetGunKuralId,
                    s.NobetGorevTipId,
                    s.GunGrup,
                    s.NobetUstGrupId
                })
                .Distinct()
                .OrderBy(o => o.NobetGorevTipId)
                .ThenBy(t => t.NobetGunKuralId).ToList();

            var varsayilanBaslangicNobetTarihi = new DateTime(2012, 1, 1);

            foreach (var nobetGunKural in sonucuOlanGunler)
            {
                var nobetDurumlari = enSonNobetler
                    .Where(w => w.NobetGunKuralId == nobetGunKural.NobetGunKuralId)
                    .Select(s => s.EczaneNobetGrupId).ToList();

                var sonucuOlmayanlar = eczaneNobetGruplar
                    .Where(w => !nobetDurumlari.Contains(w.Id)).ToList();

                if (sonucuOlmayanlar.Count > 0)
                {
                    foreach (var eczaneNobetGrup in sonucuOlmayanlar)
                    {
                        enSonNobetler.Add(new EczaneNobetGrupGunKuralIstatistik
                        {
                            EczaneId = eczaneNobetGrup.EczaneId,
                            EczaneAdi = eczaneNobetGrup.EczaneAdi,
                            NobetGrupAdi = eczaneNobetGrup.NobetGrupAdi,
                            NobetAltGrupId = 0,
                            EczaneNobetGrupId = eczaneNobetGrup.Id,
                            IlkNobetTarihi = varsayilanBaslangicNobetTarihi, // eczaneNobetGrup.BaslangicTarihi, 
                            SonNobetTarihi = varsayilanBaslangicNobetTarihi, // eczaneNobetGrup.BaslangicTarihi, 
                            NobetGorevTipId = nobetGunKural.NobetGorevTipId,
                            NobetGunKuralId = nobetGunKural.NobetGunKuralId,
                            GunGrup = nobetGunKural.GunGrup,
                            NobetGrupId = eczaneNobetGrup.NobetGrupId,
                            NobetSayisi = 1,
                            NobetUstGrupId = nobetGunKural.NobetUstGrupId
                        });
                    }
                }
            }

            return enSonNobetler;
        }
        #endregion


        #endregion

        #region Çift eczaneler nodes - edges

        public List<EczaneNobetSonucNode> GetEczaneNobetSonucNodes()
        {
            var nodes = GetSonuclar()
                .Select(s => new { s.EczaneId, s.EczaneAdi, s.NobetGrupId }).Distinct()
                              .Select(h => new EczaneNobetSonucNode
                              {
                                  Id = h.EczaneId,
                                  Label = h.EczaneAdi,
                                  Value = 5,
                                  Level = h.NobetGrupId,
                                  Group = h.NobetGrupId
                              }).ToList();
            return nodes;
        }

        public List<EczaneNobetSonucNode> GetEczaneNobetSonucNodes(int nobetUstGrupId)
        {
            var nodes = GetSonuclar(nobetUstGrupId)
                .Select(s => new
                {
                    s.EczaneId,
                    s.EczaneAdi,
                    s.NobetGrupId
                }).Distinct()
                              .Select(h => new EczaneNobetSonucNode
                              {
                                  Id = h.EczaneId,
                                  Label = h.EczaneAdi,
                                  Value = 5,
                                  Level = h.NobetGrupId,
                                  Group = h.NobetGrupId
                              }).ToList();
            return nodes;
        }

        public List<EczaneNobetSonucNode> GetEczaneNobetSonucNodes(int yil, int ay, List<int> eczaneIdList)
        {
            var nodes = GetSonuclarYillikKumulatif(yil, ay, eczaneIdList)
                .Select(s => new
                {
                    s.EczaneId,
                    s.EczaneAdi,
                    s.NobetGrupId
                }).Distinct()
                              .Select(h => new EczaneNobetSonucNode
                              {
                                  Id = h.EczaneId,
                                  Label = h.EczaneAdi,
                                  Value = 5,
                                  Level = h.NobetGrupId,
                                  Group = h.NobetGrupId
                              }).ToList();
            return nodes;
        }

        public List<EczaneNobetSonucEdge> GetEczaneNobetSonucEdges(int yil, int ay, int ayniGuneDenkGelenNobetSayisi, int nobetUstGrupId)
        {
            //var nobetSonuclar = GetSonuclarYillikKumulatif(yil, ay, nobetUstGrupId);
            //var edges = _eczaneNobetOrtakService.GrupSayisinaGoreAnalizeGonder(nobetSonuclar, ayniGuneDenkGelenNobetSayisi)
            //                .Select(h => new EczaneNobetSonucEdge
            //                {
            //                    NobetUstGrupId = nobetUstGrupId,
            //                    From = h.G1EczaneId,
            //                    To = h.G2EczaneId,
            //                    Value = h.AyniGunNobetTutmaSayisi,
            //                    Label = h.AyniGunNobetTutmaSayisi,
            //                    Title = "From eczane: " + h.G1EczaneId + " To eczane: " + h.G2EczaneId + ": " + h.AyniGunNobetTutmaSayisi + " adet birlikte nöbet"
            //                })
            //                .ToList();
            return new List<EczaneNobetSonucEdge>(); //edges;
        }

        #endregion

    }
}