using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class TakvimManager : ITakvimService
    {
        #region ctor
        private ITakvimDal _takvimDal;
        private IBayramService _bayramService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetMuafiyetService _eczaneNobetMuafiyetService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private INobetAltGrupService _nobetAltGrupService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IEczaneNobetSonucPlanlananService _eczaneNobetSonucPlanlananService;
        private INobetGrupGorevTipTakvimOzelGunService _nobetGrupGorevTipTakvimOzelGunService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;
        private INobetGrupTalepService _nobetGrupTalepService;
        private INobetGrupKuralService _nobetGrupKuralService;
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;

        public TakvimManager(ITakvimDal takvimDal,
            IBayramService bayramService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            IEczaneNobetMazeretService eczaneNobetMazeretService,
            IEczaneNobetMuafiyetService eczaneNobetMuafiyetService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            IEczaneNobetSonucService eczaneNobetSonucService,
            INobetAltGrupService nobetAltGrupService,
            IEczaneNobetGrupAltGrupService eczaneNobetGrupAltGrupService,
            INobetGrupGorevTipTakvimOzelGunService nobetGrupGorevTipTakvimOzelGunService,
            INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
            IAyniGunTutulanNobetService ayniGunTutulanNobetService,
            IEczaneNobetSonucPlanlananService eczaneNobetSonucPlanlananService,
            INobetGrupTalepService nobetGrupTalepService,
            INobetGrupKuralService nobetGrupKuralService,
            INobetUstGrupGunGrupService nobetUstGrupGunGrupService)
        {
            _takvimDal = takvimDal;
            _bayramService = bayramService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _eczaneNobetMuafiyetService = eczaneNobetMuafiyetService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetAltGrupService = nobetAltGrupService;
            _eczaneNobetGrupAltGrupService = eczaneNobetGrupAltGrupService;
            _nobetGrupGorevTipTakvimOzelGunService = nobetGrupGorevTipTakvimOzelGunService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
            _eczaneNobetSonucPlanlananService = eczaneNobetSonucPlanlananService;
            _nobetGrupTalepService = nobetGrupTalepService;
            _nobetGrupKuralService = nobetGrupKuralService;
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
        }
        #endregion

        #region genel

        public int AyFarkiHesapla(DateTime sonTarih, DateTime ilkTarih)
        {
            int ayFarki = 0;

            var ilkYil = ilkTarih.Year;
            var ilkAy = ilkTarih.Month;

            var sonYil = sonTarih.Year;
            var sonAy = sonTarih.Month;

            if (ilkYil < sonYil)
            {
                ayFarki += 12 * (sonYil - ilkYil);
            }

            if (ilkAy < sonAy)
            {
                ayFarki += sonAy - ilkAy;
            }
            else
            {
                ayFarki = 12 - ilkAy + sonAy;
            }

            return ayFarki;
        }

        public List<Takvim> GetList()
        {
            return _takvimDal.GetList();
        }

        public List<TakvimDetay> GetDetaylar()
        {
            return _takvimDal.GetTakvimDetaylar();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<TakvimDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            return _takvimDal.GetTakvimDetaylar(t => t.Tarih >= baslangicTarihi && t.Tarih <= bitisTarihi);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<TakvimDetay> GetDetaylar(DateTime baslangicTarihi)
        {
            return _takvimDal.GetTakvimDetaylar(t => t.Tarih >= baslangicTarihi);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<TakvimDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int haftaninGunu)
        {
            return _takvimDal.GetTakvimDetaylar(t => (t.Tarih >= baslangicTarihi && t.Tarih <= bitisTarihi)
                                                  && (t.HaftaninGunu == haftaninGunu || haftaninGunu == 0));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<TakvimDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int[] haftaninGunleri)
        {
            return _takvimDal.GetTakvimDetaylar(t => (t.Tarih >= baslangicTarihi && t.Tarih <= bitisTarihi)
                                                  && (haftaninGunleri.Contains(t.HaftaninGunu)));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<MyDrop> GetHaftaninGunleri()
        {
            var haftaninGunleri = new List<MyDrop>
            {
                new MyDrop {Id=1, Value="Pazar"},
                new MyDrop {Id=2, Value="Pazartesi"},
                new MyDrop {Id=3, Value="Salı"},
                new MyDrop {Id=4, Value="Çarşamba"},
                new MyDrop {Id=5, Value="Perşembe"},
                new MyDrop {Id=6, Value="Cuma"},
                new MyDrop {Id=7, Value="Cumartesi"},
                new MyDrop {Id=8, Value="Dini Bayram"},
                new MyDrop {Id=9, Value="Milli Bayram"}
            };

            return haftaninGunleri;
        }

        public List<TakvimDetay> GetDetaylar(int yil, int ay)
        {
            return _takvimDal.GetTakvimDetaylar(x => x.Yil == yil && x.Ay == ay);
        }

        public List<TakvimDetay> GetDetaylar(int yil)
        {
            return _takvimDal.GetTakvimDetaylar(x => x.Yil == yil);
        }

        public TakvimDetay GetDetay(DateTime tarih)
        {
            return _takvimDal.GetTakvimDetay(x => x.Tarih == tarih);
        }

        public List<MyDrop> GetAylar()
        {
            var aylar = (from t in GetList()
                         group new { t } by new
                         {
                             Ay = t.Tarih.Month,
                             AyAdi = DateTimeFormatInfo.CurrentInfo.GetMonthName(t.Tarih.Month)
                         } into g
                         select new MyDrop
                         {
                             Id = g.Key.Ay,
                             Value = $"{g.Key.AyAdi} ({g.Key.Ay})"
                         }).ToList();
            return aylar;
        }

        public List<MyDrop> GetAylarDdl()
        {
            return new List<MyDrop>
            {
                new MyDrop{ Id=1, Value="Ocak (1)"},
                new MyDrop{ Id=2, Value="Şubat (2)"},
                new MyDrop{ Id=3, Value="Mart (3)"},
                new MyDrop{ Id=4, Value="Nisan (4)"},
                new MyDrop{ Id=5, Value="Mayıs (5)"},
                new MyDrop{ Id=6, Value="Haziran (6)"},
                new MyDrop{ Id=7, Value="Temmuz (7)"},
                new MyDrop{ Id=8, Value="Ağustos (8)"},
                new MyDrop{ Id=9, Value="Eylül (9)"},
                new MyDrop{ Id=10, Value="Ekim (10)"},
                new MyDrop{ Id=11, Value="Kasım (11)"},
                new MyDrop{ Id=12, Value="Aralık (12)"},
            };
        }

        public List<MyDrop> GetAylar(int yil)
        {
            var aylar = (from t in GetList()
                         where t.Tarih.Year == yil
                         group new { t } by new
                         {
                             Ay = t.Tarih.Month,
                             AyAdi = DateTimeFormatInfo.CurrentInfo.GetMonthName(t.Tarih.Month)
                         } into g
                         select new MyDrop
                         {
                             Id = g.Key.Ay,
                             Value = $"{g.Key.AyAdi} ({g.Key.Ay})"
                         }).ToList();
            return aylar;
        }

        public List<MyDrop> GetAylar(DateTime baslangicTarihi, int yil)
        {
            var aylar = (from t in GetList()
                         where t.Tarih >= baslangicTarihi && t.Tarih.Year == yil
                         group new { t } by new
                         {
                             Ay = t.Tarih.Month,
                             AyAdi = DateTimeFormatInfo.CurrentInfo.GetMonthName(t.Tarih.Month)
                         } into g
                         select new MyDrop
                         {
                             Id = g.Key.Ay,
                             Value = $"{g.Key.AyAdi} ({g.Key.Ay})"
                         }).ToList();
            return aylar;
        }

        public List<MyDrop> GetGelecekAy()
        {
            var aylar = (from t in GetList()
                         where t.Tarih.Month == Convert.ToInt32(DateTime.Now.AddMonths(1))
                         group new { t } by new
                         {
                             Ay = t.Tarih.Month,
                             AyAdi = DateTimeFormatInfo.CurrentInfo.GetMonthName(t.Tarih.Month)
                         } into g
                         select new MyDrop
                         {
                             Id = g.Key.Ay,
                             Value = $"{g.Key.AyAdi} ({g.Key.Ay})"
                         }).ToList();
            return aylar;
        }

        public Takvim GetById(int takvimId)
        {
            return _takvimDal.Get(x => x.Id == takvimId);
        }

        public Takvim GetByTarih(DateTime tarih)
        {
            return _takvimDal.Get(x => x.Tarih == tarih);
        }

        public void Insert(Takvim takvim)
        {
            _takvimDal.Insert(takvim);
        }

        public void Update(Takvim takvim)
        {
            _takvimDal.Update(takvim);
        }

        public void Delete(int takvimId)
        {
            _takvimDal.Delete(new Takvim { Id = takvimId });
        }

        #endregion

        #region 1. takvim nöbet gruplar

        #region eski
        public List<TakvimNobetGrup> GetTakvimNobetGruplar(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, int nobetGrupId, int nobetGorevTipId)
        {
            int aydakiGunSayisi = DateTime.DaysInMonth(yilBitis, ayBitis);//AyGunSayisi(ayBitis);
            var baslangicTarihi = new DateTime(yilBaslangic, ayBaslangic, 1);
            var bitisTarihi = new DateTime(yilBitis, ayBitis, aydakiGunSayisi);

            var tarihler = GetDetaylar(baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetList(nobetGorevTipId, nobetGrupId);
            var bayramlar = _bayramService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupId, nobetGorevTipId);
            var takvimNobetGruplar = GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, bayramlar);

            return takvimNobetGruplar;
        }
        public List<TakvimNobetGrup> GetTakvimNobetGruplar(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            int aydakiGunSayisi = DateTime.DaysInMonth(yilBitis, ayBitis);//AyGunSayisi(ayBitis);
            var baslangicTarihi = new DateTime(yilBaslangic, ayBaslangic, 1);
            var bitisTarihi = new DateTime(yilBitis, ayBitis, aydakiGunSayisi);

            var tarihler = GetDetaylar(baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetList(nobetGorevTipId, nobetGrupIdList);
            var bayramlar = _bayramService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupIdList, nobetGorevTipId);
            var takvimNobetGruplar = GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, bayramlar);

            return takvimNobetGruplar;
        }
        private List<TakvimNobetGrup> GetTakvimNobetGruplar(List<TakvimDetay> tarihler, List<NobetGrupGorevTip> nobetGrupGorevTipler, List<BayramDetay> bayramlar)
        {
            return (from t in tarihler
                    from g in nobetGrupGorevTipler
                    select new TakvimNobetGrup
                    {
                        TakvimId = t.TakvimId,
                        Yil = t.Yil,
                        Ay = t.Ay,
                        Gun = t.Gun,
                        HaftaninGunu = t.HaftaninGunu,
                        NobetGrupId = g.NobetGrupId,
                        NobetGorevTipId = g.NobetGorevTipId,
                        NobetGunKuralId = bayramlar.Where(w => w.TakvimId == t.TakvimId
                                                            && w.NobetGrupId == g.NobetGrupId
                                                            && w.NobetGorevTipId == g.NobetGorevTipId)
                                              .Select(s => s.NobetGunKuralId).FirstOrDefault() > 0
                                              ? bayramlar.Where(w => w.TakvimId == t.TakvimId
                                                                  && w.NobetGrupId == g.NobetGrupId
                                                                  && w.NobetGorevTipId == g.NobetGorevTipId)
                                              .Select(s => s.NobetGunKuralId).FirstOrDefault()
                                              : t.HaftaninGunu,
                        Tarih = t.Tarih
                        //BaslangicTarihi = g.NobetGrup.BaslamaTarihi,
                        //BitisTarihi = g.NobetGrup.BitisTarihi
                    }).ToList();
        }
        #endregion

        private List<TakvimNobetGrup> GetTakvimNobetGruplar(List<TakvimDetay> tarihler, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, List<NobetGrupGorevTipTakvimOzelGunDetay> bayramlar, List<NobetGrupTalepDetay> nobetGrupTalepler)
        {
            var nobetGrupGorevTipIdList = nobetGrupGorevTipler.Select(s => s.Id).ToList();

            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipIdList(nobetGrupGorevTipIdList);

            //var nobetGrupGorevTipTakvimOzelGunDetaylar = new List<NobetGrupGorevTipTakvimOzelGunDetay>();

            //if (bayramlar.Count > 0)
            //{
            //    nobetGrupGorevTipTakvimOzelGunDetaylar = bayramlar;
            //}
            //var cc = nobetGrupGorevTipGunKurallar
            //    .Where(w => w.NobetGunKuralId == 9).SingleOrDefault();

            var nobetGrupKurallar = _nobetGrupKuralService.GetDetaylar(nobetGrupGorevTipIdList, 3); //3: varsayılan nöbetçi sayısı

            var takvimNobetGrupGorevTipler = (from t in tarihler
                                              from g in nobetGrupGorevTipler
                                              from b in bayramlar
                                                    .Where(w => g.Id == w.NobetGrupGorevTipId && t.TakvimId == w.TakvimId).DefaultIfEmpty()
                                              from d in nobetGrupTalepler
                                                    .Where(w => g.Id == w.NobetGrupGorevTipId && t.TakvimId == w.TakvimId).DefaultIfEmpty()
                                              from k in nobetGrupKurallar
                                                    .Where(w => g.Id == w.NobetGrupGorevTipId).DefaultIfEmpty()
                                              let nobetGrupGorevTipGunKural = nobetGrupGorevTipGunKurallar
                                                                .SingleOrDefault(w => w.NobetGrupGorevTipId == g.Id
                                                                                   && w.NobetGunKuralId ==
                                                                                   ((g.Id == b?.NobetGrupGorevTipId && t.TakvimId == b?.TakvimId)
                                                                                       ? b.NobetGunKuralId
                                                                                       : t.HaftaninGunu)
                                                                                   )
                                              select new TakvimNobetGrup
                                              {
                                                  TakvimId = t.TakvimId,
                                                  Yil = t.Yil,
                                                  Ay = t.Ay,
                                                  Gun = t.Gun,
                                                  HaftaninGunu = t.HaftaninGunu,
                                                  NobetGrupId = g.NobetGrupId,
                                                  NobetGorevTipId = g.NobetGorevTipId,
                                                  NobetGrupGorevTipId = g.Id,
                                                  NobetGunKuralId = (g.Id == b?.NobetGrupGorevTipId && t.TakvimId == b?.TakvimId) ? b.NobetGunKuralId : t.HaftaninGunu,
                                                  NobetGorevTipAdi = g.NobetGorevTipAdi,
                                                  NobetGrupAdi = g.NobetGrupAdi,
                                                  NobetGunKuralAdi = (g.Id == b?.NobetGrupGorevTipId && t.TakvimId == b?.TakvimId)
                                                       ? b.NobetGunKuralAdi
                                                       : nobetGrupGorevTipGunKural?.NobetGunKuralAdi,
                                                  GunGrupAdi = (g.Id == b?.NobetGrupGorevTipId && t.TakvimId == b?.TakvimId)
                                                       ? b.GunGrupAdi
                                                       : nobetGrupGorevTipGunKural?.GunGrupAdi,
                                                  GunGrupId = (int)((g.Id == b?.NobetGrupGorevTipId && t.TakvimId == b?.TakvimId)
                                                       ? b.GunGrupId
                                                       : nobetGrupGorevTipGunKural == null
                                                        ? 0
                                                        : nobetGrupGorevTipGunKural?.GunGrupId),
                                                  TalepEdilenNobetciSayisi = (g.Id == d?.NobetGrupGorevTipId && t.TakvimId == d?.TakvimId)
                                                          ? d.NobetciSayisi
                                                          : (int)k.Deger != nobetGrupGorevTipGunKural?.NobetciSayisi
                                                            ? (int)nobetGrupGorevTipGunKural?.NobetciSayisi
                                                            : (int)k.Deger,
                                                  Tarih = t.Tarih,
                                                  NobetGunKuralKapanmaTarihi = nobetGrupGorevTipGunKural.BitisTarihi
                                              })
                                              .Where(w => w.GunGrupAdi != null)
                                              .ToList();

            //var vv = takvimNobetGrupGorevTipler
            //    .Where(w => w.Tarih == new DateTime(2019, 4, 23)).SingleOrDefault();

            return takvimNobetGrupGorevTipler;
        }
        private List<TakvimNobetGrup> GetTakvimNobetGruplar(List<TakvimDetay> tarihler, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, List<NobetGrupGorevTipTakvimOzelGunDetay> bayramlar, List<NobetGrupTalepDetay> nobetGrupTalepler, List<int> nobetGunKuralIdList)
        {
            return GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, bayramlar, nobetGrupTalepler)
                    .Where(w => nobetGunKuralIdList.Contains(w.NobetGunKuralId)).ToList();
        }
        private List<TakvimNobetGrup> GetTakvimNobetGruplar(List<TakvimDetay> tarihler, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, List<NobetGrupGorevTipTakvimOzelGunDetay> bayramlar, List<NobetGrupTalepDetay> nobetGrupTalepler, string gunGrup)
        {
            return GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, bayramlar, nobetGrupTalepler)
                    .Where(w => w.GunGrupAdi == gunGrup).ToList();
        }

        private List<TakvimNobetGrup> GetTakvimNobetGruplar(List<TakvimDetay> tarihler, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, List<NobetGrupGorevTipTakvimOzelGunDetay> bayramlar, List<NobetGrupTalepDetay> nobetGrupTalepler, int gunGrupId)
        {
            return GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, bayramlar, nobetGrupTalepler)
                    .Where(w => w.GunGrupId == gunGrupId).ToList();
        }

        public List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            var tarihler = GetDetaylar(baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetGorevTipId, nobetGrupIdList);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupIdList, nobetGorevTipId);
            var nobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler.Select(s => s.Id).ToList());

            var takvimNobetGruplar = GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, nobetGrupGorevTipTakvimOzelGunler, nobetGrupTalepler);

            return takvimNobetGruplar;
        }
        public List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            var tarihler = GetDetaylar(baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetUstGrupId);
            var nobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler.Select(s => s.Id).ToList());

            var takvimNobetGruplar = GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, nobetGrupGorevTipTakvimOzelGunler, nobetGrupTalepler);

            return takvimNobetGruplar;
        }
        public List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId, List<int> nobetGunKuralIdList)
        {
            var tarihler = GetDetaylar(baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetGorevTipId, nobetGrupIdList);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupIdList, nobetGorevTipId);
            var nobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler.Select(s => s.Id).ToList());

            var takvimNobetGruplar = GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, nobetGrupGorevTipTakvimOzelGunler, nobetGrupTalepler, nobetGunKuralIdList);

            return takvimNobetGruplar;
        }
        public List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler)
        {
            var tarihler = GetDetaylar(baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);
            var nobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler.Select(s => s.Id).ToList());

            var takvimNobetGruplar = GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, nobetGrupGorevTipTakvimOzelGunler, nobetGrupTalepler);

            return takvimNobetGruplar;
        }
        public List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, List<int> nobetGunKuralIdList)
        {
            var tarihler = GetDetaylar(baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);
            var nobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler.Select(s => s.Id).ToList());
            var takvimNobetGruplar = GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, nobetGrupGorevTipTakvimOzelGunler, nobetGrupTalepler, nobetGunKuralIdList);

            return takvimNobetGruplar;
        }

        public List<TakvimNobetGrupAltGrup> GetTakvimNobetGrupAltGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            var tarihler = GetDetaylar(baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetList(nobetGorevTipId, nobetGrupIdList);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupIdList, nobetGorevTipId);
            var takvimNobetGruplar = GetTakvimNobetAltGrupluGruplar(tarihler, nobetGrupGorevTipler, nobetGrupGorevTipTakvimOzelGunler);

            return takvimNobetGruplar;
        }
        private List<TakvimNobetGrupAltGrup> GetTakvimNobetAltGrupluGruplar(List<TakvimDetay> tarihler, List<NobetGrupGorevTip> nobetGrupGorevTipler, List<NobetGrupGorevTipTakvimOzelGunDetay> bayramlar)
        {
            var nobetUstGrupId = 3;
            var altGrupluGruplar = _nobetAltGrupService.GetDetaylar(nobetUstGrupId);

            return (from t in tarihler
                    from g in nobetGrupGorevTipler
                    from a in altGrupluGruplar
                    select new TakvimNobetGrupAltGrup
                    {
                        TakvimId = t.TakvimId,
                        Yil = t.Yil,
                        Ay = t.Ay,
                        Gun = t.Gun,
                        HaftaninGunu = t.HaftaninGunu,
                        NobetGrupId = g.NobetGrupId,
                        NobetGorevTipId = g.NobetGorevTipId,
                        NobetAltGrupId = a.Id,
                        NobetGunKuralId = bayramlar.Where(w => w.TakvimId == t.TakvimId
                                                            && w.NobetGrupId == g.NobetGrupId
                                                            && w.NobetGorevTipId == g.NobetGorevTipId)
                                              .Select(s => s.NobetGunKuralId).FirstOrDefault() > 0
                                              ? bayramlar.Where(w => w.TakvimId == t.TakvimId
                                                                  && w.NobetGrupId == g.NobetGrupId
                                                                  && w.NobetGorevTipId == g.NobetGorevTipId)
                                              .Select(s => s.NobetGunKuralId).FirstOrDefault()
                                              : t.HaftaninGunu,
                        Tarih = t.Tarih
                        //BaslangicTarihi = g.NobetGrup.BaslamaTarihi,
                        //BitisTarihi = g.NobetGrup.BitisTarihi
                    }).ToList();
        }

        public List<TakvimNobetGrupPeriyot> GetTakvimNobetGruplar(DateTime baslangicTarihi, List<int> ayFarklari, int uzunluk, List<int> nobetGrupIdList, int nobetGorevTipId, string gunGrup)
        {
            var tarihler = GetDetaylar(baslangicTarihi);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetGorevTipId, nobetGrupIdList);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, nobetGrupIdList, nobetGorevTipId);
            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipIdList(nobetGrupGorevTipler.Select(s => s.Id).ToList());

            var nobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(baslangicTarihi, nobetGrupGorevTipler.Select(s => s.Id).ToList());

            var takvimNobetGruplar = GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, nobetGrupGorevTipTakvimOzelGunler, nobetGrupTalepler, gunGrup);
            var takvimNobetGruplarPeriyot = GetTakvimNobetGruplar(takvimNobetGruplar, ayFarklari, uzunluk);

            return takvimNobetGruplarPeriyot;
        }
        private List<TakvimNobetGrupPeriyot> GetTakvimNobetGruplar(List<TakvimNobetGrup> takvimNobetGruplar, List<int> ayFarklari, int uzunluk)
        {
            var atlanacakTarihSayisi = 0;

            var takvimNobetGrupPeriyot = new List<TakvimNobetGrupPeriyot>();

            foreach (var ayFarki in ayFarklari)
            {
                if (ayFarki > 1)
                    atlanacakTarihSayisi = uzunluk * (ayFarki - 1);

                var takvimNobetGrupPeriyot1 = (from t in takvimNobetGruplar
                                               select new TakvimNobetGrupPeriyot
                                               {
                                                   TakvimId = t.TakvimId,
                                                   Yil = t.Yil,
                                                   Ay = t.Ay,
                                                   Gun = t.Gun,
                                                   HaftaninGunu = t.HaftaninGunu,
                                                   NobetGrupId = t.NobetGrupId,
                                                   NobetGorevTipId = t.NobetGorevTipId,
                                                   NobetGunKuralId = t.NobetGunKuralId,
                                                   GunGrupAdi = t.GunGrupAdi,
                                                   Tarih = t.Tarih,
                                                   NobetGorevTipAdi = t.NobetGorevTipAdi,
                                                   NobetGrupAdi = t.NobetGrupAdi,
                                                   NobetGrupGorevTipId = t.NobetGrupGorevTipId,
                                                   NobetGunKuralAdi = t.NobetGunKuralAdi,
                                                   NobetSayisi = ayFarki
                                               })
                        .Skip(atlanacakTarihSayisi)
                        .Take(uzunluk).ToList();

                takvimNobetGrupPeriyot.AddRange(takvimNobetGrupPeriyot1);
            }

            return takvimNobetGrupPeriyot;
        }

        public List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, int gunGrupId)
        {
            var tarihler = GetDetaylar(baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);
            var nobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler.Select(s => s.Id).ToList());

            var takvimNobetGruplar = GetTakvimNobetGruplar(tarihler, nobetGrupGorevTipler, nobetGrupGorevTipTakvimOzelGunler, nobetGrupTalepler, gunGrupId);

            return takvimNobetGruplar;
        }

        public List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunDegerIstatistikler(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            var tarihler = GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdList, nobetGorevTipId);

            return GetTakvimNobetGrupGunKuralIstatistik(tarihler);
        }
        public List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunDegerIstatistikler(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler)
        {
            var tarihler = GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);

            return GetTakvimNobetGrupGunKuralIstatistik(tarihler);
        }
        private List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunKuralIstatistik(List<TakvimNobetGrup> takvimNobetGruplar)
        {
            return takvimNobetGruplar
                .GroupBy(g => new
                {
                    g.NobetGrupGorevTipId,
                    g.NobetGrupId,
                    g.NobetGunKuralId,
                    g.NobetGunKuralAdi,
                    g.NobetGorevTipId,
                    g.GunGrupAdi,
                    g.GunGrupId,
                    g.NobetGunKuralKapanmaTarihi
                })
                .Select(s => new TakvimNobetGrupGunDegerIstatistik
                {
                    NobetGrupGorevTipId = s.Key.NobetGrupGorevTipId,
                    NobetGrupId = s.Key.NobetGrupId,
                    NobetGorevTipId = s.Key.NobetGorevTipId,
                    NobetGunKuralId = s.Key.NobetGunKuralId,
                    NobetGunKuralAdi = s.Key.NobetGunKuralAdi,
                    GunGrupAdi = s.Key.GunGrupAdi,
                    GunGrupId = s.Key.GunGrupId,
                    GunSayisi = s.Count(),
                    TalepEdilenNobetciSayisi = s.Sum(f => f.TalepEdilenNobetciSayisi),
                    IstatistikBaslamaTarihi = s.Max(f => f.Tarih),
                    IstatistikBitisTarihi = s.Min(f => f.Tarih),
                    NobetGunKuralKapanmaTarihi = s.Key.NobetGunKuralKapanmaTarihi
                }).ToList();
        }

        #endregion

        #region 2. takvim nöbet grup pivot

        /// <summary>
        /// Takvim Nöbet Gruptaki liste nöbet grup ve nöbet görev tiplerine göre gruplandırılır.
        /// </summary>
        /// <param name="takvimNobetGruplar"></param>
        /// <returns></returns>
        private List<GrupIciAylikKumulatifHedef> GetTakvimNobetGrupPivot(List<TakvimNobetGrup> takvimNobetGruplar)
        {
            return (from t in takvimNobetGruplar
                    group t by new
                    {
                        t.NobetGrupId,
                        t.NobetGorevTipId,
                        t.NobetGrupGorevTipId
                    } into grouped
                    select new GrupIciAylikKumulatifHedef
                    {
                        NobetGrupGorevTipId = grouped.Key.NobetGrupGorevTipId,
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
                        Arife = grouped.Count(c => c.NobetGunKuralId == 10),
                        ToplamBayram = grouped.Count(c => c.GunGrupId == 2),
                        ToplamCumaCumartesi = grouped.Count(c => c.NobetGunKuralId >= 6 && c.NobetGunKuralId <= 7),
                        ToplamHaftaIci = grouped.Count(c => c.GunGrupId == 3),
                        Toplam = grouped.Count()
                    }).ToList();
        }

        #endregion

        #region 3. eczane kumulatif nöbet ortalamaları

        public List<EczaneNobetIstatistik> GetEczaneKumulatifHedefler(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            int aydakiGunSayisi = DateTime.DaysInMonth(yilBitis, ayBitis);//AyGunSayisi(ayBitis);
            var baslangicTarihi = new DateTime(yilBitis, ayBitis, 1);
            var bitisTarihi = new DateTime(yilBitis, ayBitis, aydakiGunSayisi);

            //var eczaneNobetMazeretler = _eczaneNobetMazeretService.GetEczaneNobetMazeretSayilari(baslangicTarihi, bitisTarihi, nobetGrupIdList);
            //var eczaneNobetMuafiyetler = _eczaneNobetMuafiyetService.GetDetaylar(baslangicTarihi, bitisTarihi);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetList(nobetGrupIdList, baslangicTarihi, bitisTarihi);
            //.Where(w => !eczaneNobetMuafiyetler.Select(s => s.EczaneId).Contains(w.EczaneId)
            //         && !eczaneNobetMazeretler.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var takvimNobetGruplar = GetTakvimNobetGruplar(yilBaslangic, yilBitis, ayBaslangic, ayBitis, nobetGrupIdList, nobetGorevTipId);
            var takvimPivot = GetTakvimNobetGrupPivot(takvimNobetGruplar);
            //var takvimPivotKumulatif = GetTakvimNobetGrupPivotKumulatif(takvimPivot);

            var istatistik = _eczaneNobetSonucService.GetEczaneNobetIstatistik2(nobetGrupIdList)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            //NobetTutmayanEcaneleriIstatistikYeniyeEkle(eczaneNobetGruplar, istatistikYeni);
            //var istatistikson = GetEczaneNobetIstatistik(istatistikYeni);

            var hedefler = (from t in istatistik
                            from k in takvimPivot //takvimPivotKumulatif
                            where t.NobetGrupId == k.NobetGrupId
                            && t.NobetGorevTipId == k.NobetGorevTipId
                            select new EczaneNobetIstatistik
                            {
                                //EczaneNobetGrupId = t.EczaneNobetGrupId,
                                //EczaneId = t.EczaneId,
                                //NobetGrupId = t.NobetGrupId,
                                //NobetGorevTipId = t.NobetGorevTipId,
                                //Pazar = (int)Math.Ceiling(k.Pazar / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.Pazar,
                                //Pazartesi = (int)Math.Ceiling(k.Pazartesi / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.Pazartesi,
                                //Sali = (int)Math.Ceiling(k.Sali / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.Sali,
                                //Carsamba = (int)Math.Ceiling(k.Carsamba / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.Carsamba,
                                //Persembe = (int)Math.Ceiling(k.Persembe / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.Persembe,
                                //Cuma = (int)Math.Ceiling(k.Cuma / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.Cuma,
                                //Cumartesi = (int)Math.Ceiling(k.Cumartesi / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.Cumartesi,
                                //DiniBayram = (int)Math.Ceiling(k.DiniBayram / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.DiniBayram,
                                //MilliBayram = (int)Math.Ceiling(k.MilliBayram / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.MilliBayram,
                                //ToplamBayram = (int)Math.Ceiling(k.ToplamBayram / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.ToplamBayram,
                                //ToplamCumaCumartesi = (int)Math.Ceiling(k.ToplamCumaCumartesi / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.ToplamCumaCumartesi,
                                //ToplamHaftaIci = (int)Math.Ceiling(k.ToplamHaftaIci / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.ToplamHaftaIci,
                                //Toplam = (int)Math.Ceiling(k.Toplam / eczaneNobetGruplar.Count(x => x.NobetGrupId == k.NobetGrupId)) - t.Toplam
                            }).ToList();

            var tarihAralik = GetDetaylar(baslangicTarihi, bitisTarihi);

            var pazarSayisi = tarihAralik.Where(s => s.HaftaninGunu == 1).Count();
            //pazar hedefi 1 olanlar
            var pazarEksikEczaneler = hedefler.Where(w => w.Pazar == 1).ToList();
            var pazarYazilacakEczaneSayisi = pazarSayisi - pazarEksikEczaneler.Count;

            return hedefler;
        }

        #endregion

        #region Karar değişkeni index seti

        public List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            var takvimNobetGrupGorevTipler = GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetUstGrupId);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarNobetUstGrupId(baslangicTarihi, bitisTarihi, nobetUstGrupId);
            var eczaneNobetKararDegiskeni = GetEczaneNobetTarihAralik(takvimNobetGrupGorevTipler, eczaneNobetGruplar);

            return eczaneNobetKararDegiskeni;
        }
        public List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList)
        {
            var takvimNobetGrupGorevTipler = GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdList, nobetGorevTipId);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdList, baslangicTarihi, bitisTarihi);
            var eczaneNobetKararDegiskeni = GetEczaneNobetTarihAralik(takvimNobetGrupGorevTipler, eczaneNobetGruplar);

            return eczaneNobetKararDegiskeni;
        }
        public List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList, List<int> nobetGunKuralIdList)
        {
            var takvimNobetGrupGorevTipler = GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdList, nobetGorevTipId, nobetGunKuralIdList);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdList, baslangicTarihi, bitisTarihi);
            var eczaneNobetKararDegiskeni = GetEczaneNobetTarihAralik(takvimNobetGrupGorevTipler, eczaneNobetGruplar);

            return eczaneNobetKararDegiskeni;
        }

        public List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler)
        {
            var takvimNobetGrupGorevTipler = GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);
            var nobetGruplar = nobetGrupGorevTipler.Select(s => s.NobetGrupId).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGruplar, baslangicTarihi, bitisTarihi);
            var eczaneNobetKararDegiskeni = GetEczaneNobetTarihAralik(takvimNobetGrupGorevTipler, eczaneNobetGruplar);

            return eczaneNobetKararDegiskeni;
        }
        public List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, List<int> nobetGunKuralIdList)
        {
            var takvimNobetGrupGorevTipler = GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler, nobetGunKuralIdList);
            var nobetGruplar = nobetGrupGorevTipler.Select(s => s.NobetGrupId).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGruplar, baslangicTarihi, bitisTarihi);
            var eczaneNobetKararDegiskeni = GetEczaneNobetTarihAralik(takvimNobetGrupGorevTipler, eczaneNobetGruplar);

            return eczaneNobetKararDegiskeni;
        }

        private List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(List<TakvimNobetGrup> takvimNobetGrupGorevTipler, List<EczaneNobetGrupDetay> eczaneNobetGruplar)
        {
            return (from e in eczaneNobetGruplar
                    from t in takvimNobetGrupGorevTipler
                    where e.NobetGrupGorevTipId == t.NobetGrupGorevTipId
                    //e.NobetGrupId == t.NobetGrupId
                    select new EczaneNobetTarihAralik
                    {
                        NobetUstGrupId = e.NobetUstGrupId,
                        EczaneId = e.EczaneId,
                        EczaneAdi = e.EczaneAdi,
                        NobetGrupAdi = e.NobetGrupAdi,
                        NobetGrupId = e.NobetGrupId,
                        NobetGorevTipId = t.NobetGorevTipId,
                        NobetGrupGorevTipId = t.NobetGrupGorevTipId,
                        EczaneNobetGrupId = e.Id,
                        GunGrupId = t.GunGrupId,
                        GunGrupAdi = t.GunGrupAdi,
                        NobetGunKuralAdi = t.NobetGunKuralAdi,
                        NobetGorevTipAdi = t.NobetGorevTipAdi,
                        TakvimId = t.TakvimId,
                        NobetGunKuralId = t.NobetGunKuralId,
                        HaftaninGunu = t.HaftaninGunu,
                        Tarih = t.Tarih,
                        CumartesiGunuMu = t.NobetGunKuralId == 7 ? true : false,
                        PazarGunuMu = t.NobetGunKuralId == 1 ? true : false,
                        BayramMi = t.GunGrupId == 2 ? true : false,
                        DiniBayramMi = t.NobetGunKuralId == 8 ? true : false,
                        MilliBayramMi = t.NobetGunKuralId == 9 ? true : false,
                        YilbasiMi = t.NobetGunKuralId == 12 ? true : false,
                        YilSonuMu = t.NobetGunKuralId == 11 ? true : false,
                        ArifeMi = t.NobetGunKuralId == 10 ? true : false,
                        HaftaIciMi = t.GunGrupId == 3 ? true : false,
                        TalepEdilenNobetciSayisi = t.TalepEdilenNobetciSayisi
                        //Yil = t.Yil,
                        //Ay = t.Ay,
                        //Gun = t.Gun,
                        //(e.NobetUstGrupId == 3 || e.NobetUstGrupId == 4)
                        //     ? ((t.NobetGunKuralId > 1 && t.NobetGunKuralId < 7) ? true : false)
                        //     : ((t.NobetGunKuralId > 1 && t.NobetGunKuralId <= 7) ? true : false)
                    }).ToList();
        }

        public List<EczaneNobetTarihAralikIkili> GetIkiliEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList)
        {
            var takvimNobetGrupGorevTipler = GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdList, nobetGorevTipId)
                //.Where(w => w.Tarih == baslangicTarihi)
                .Select(s => new
                {
                    s.TakvimId,
                    s.Tarih,
                    //s.NobetGunKuralId,
                    //s.HaftaninGunu,
                    //s.Yil,
                    //s.Ay,
                    //s.Gun
                }).Distinct().ToList();

            var ikiliEczaneler = _ayniGunTutulanNobetService.GetDetaylar(nobetGrupIdList);

            var eczaneNobetKararDegiskeni = (from e in ikiliEczaneler
                                             from t in takvimNobetGrupGorevTipler
                                                 //where e.NobetGrupId1 == t.NobetGrupId
                                                 //|| e.NobetGrupId2 == t.NobetGrupId
                                             select new EczaneNobetTarihAralikIkili
                                             {
                                                 EczaneId1 = e.EczaneId1,
                                                 EczaneAdi1 = e.EczaneAdi1,
                                                 NobetGrupAdi1 = e.NobetGrupAdi1,
                                                 NobetGrupId1 = e.NobetGrupId1,
                                                 NobetGorevTipId = 1,
                                                 EczaneNobetGrupId1 = e.EczaneNobetGrupId1,
                                                 AyniGunTutulanNobetId = e.Id,

                                                 EczaneId2 = e.EczaneId2,
                                                 EczaneAdi2 = e.EczaneAdi2,
                                                 NobetGrupAdi2 = e.NobetGrupAdi2,
                                                 NobetGrupId2 = e.NobetGrupId2,
                                                 EczaneNobetGrupId2 = e.EczaneNobetGrupId2,
                                                 //Yil = t.Yil,
                                                 //Ay = t.Ay,
                                                 //Gun = t.Gun,
                                                 TakvimId = t.TakvimId,
                                                 //GunDegerId = t.NobetGunKuralId,
                                                 //HaftaninGunu = t.HaftaninGunu,
                                                 Tarih = t.Tarih,
                                                 //CumartesiGunuMu = t.NobetGunKuralId == 7 ? true : false,
                                                 //PazarGunuMu = t.NobetGunKuralId == 1 ? true : false,
                                                 //BayramMi = t.NobetGunKuralId > 7 ? true : false,
                                                 //ArifeMi = t.NobetGunKuralId == 10 ? true : false,
                                                 //HaftaIciMi = e.NobetUstGrupId1 == 3
                                                 //     ? ((t.NobetGunKuralId > 1 && t.NobetGunKuralId < 7) ? true : false)
                                                 //     : ((t.NobetGunKuralId > 1 && t.NobetGunKuralId <= 7) ? true : false)
                                                 //(t.NobetGunKuralId > 1 && t.NobetGunKuralId <= 7) ? true : false
                                             }).ToList();

            return eczaneNobetKararDegiskeni;
        }

        public List<EczaneNobetAltGrupTarihAralik> GetEczaneNobetAltGrupTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList)
        {
            var takvimNobetGrupGorevTipler = GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdList, nobetGorevTipId);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdList, baslangicTarihi, bitisTarihi);
            var eczaneNobetKararDegiskeni = GetEczaneNobetAltGrupTarihAralik(takvimNobetGrupGorevTipler, eczaneNobetGruplar);

            return eczaneNobetKararDegiskeni;
        }
        private List<EczaneNobetAltGrupTarihAralik> GetEczaneNobetAltGrupTarihAralik(List<TakvimNobetGrup> takvimNobetGrupGorevTipler, List<EczaneNobetGrupDetay> eczaneNobetGruplar)
        {
            var nobetUstGrupId = 3;
            var altGrupluGruplar = _nobetAltGrupService.GetDetaylar(nobetUstGrupId)
                .Where(w => w.NobetGrupId == 21);

            return (from e in eczaneNobetGruplar
                    from t in takvimNobetGrupGorevTipler
                    from a in altGrupluGruplar
                    where e.NobetGrupId == t.NobetGrupId
                       && (e.NobetGrupId == 20 || e.NobetGrupId == 22)
                    select new EczaneNobetAltGrupTarihAralik
                    {
                        EczaneId = e.EczaneId,
                        EczaneAdi = e.EczaneAdi,
                        NobetGrupAdi = e.NobetGrupAdi,
                        NobetGrupId = e.NobetGrupId,
                        NobetGrupAltId = a.Id,
                        NobetAltGrupAdi = a.Adi,
                        NobetGorevTipId = t.NobetGorevTipId,
                        EczaneNobetGrupId = e.Id,
                        Yil = t.Yil,
                        Ay = t.Ay,
                        Gun = t.Gun,
                        TakvimId = t.TakvimId,
                        GunDegerId = t.NobetGunKuralId,
                        HaftaninGunu = t.HaftaninGunu,
                        Tarih = t.Tarih,
                        CumartesiGunuMu = t.NobetGunKuralId == 7 ? true : false,
                        PazarGunuMu = t.NobetGunKuralId == 1 ? true : false,
                        BayramMi = t.NobetGunKuralId > 7 ? true : false,
                        HaftaIciMi = e.NobetUstGrupId == 3
                             ? ((t.NobetGunKuralId > 1 && t.NobetGunKuralId < 7) ? true : false)
                             : ((t.NobetGunKuralId > 1 && t.NobetGunKuralId <= 7) ? true : false)
                        //(t.NobetGunKuralId > 1 && t.NobetGunKuralId <= 7) ? true : false
                    }).ToList();
        }

        #endregion

        #region anahtar listeti bu güne taşı

        public List<AnahtarListe> AnahtarListeyiBuGuneTasi(List<int> nobetGrupIdListe,
            int nobetGorevTipId,
            DateTime nobetUstGrupBaslangicTarihi,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumu,
            List<EczaneNobetSonucListe2> anahtarListeTumu,
            string gunGrubu)
        {
            var anahtarListeTumEczanelerHepsi = new List<AnahtarListe>();

            foreach (var nobetGrupId in nobetGrupIdListe)
            {
                var eczaneNobetGruplar = eczaneNobetGruplarTumu
                    .Where(w => w.NobetGrupId == nobetGrupId
                             && w.NobetGorevTipId == nobetGorevTipId).ToList();

                var anahtarListeGunGrup = anahtarListeTumu
                    .Where(w => w.GunGrup == gunGrubu
                             && w.NobetGrupId == nobetGrupId
                             && w.NobetGorevTipId == nobetGorevTipId).ToList();

                var eczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistikYatayTumu
                    .Where(w => w.NobetGrupId == nobetGrupId
                             && w.NobetGorevTipId == nobetGorevTipId).ToList();

                var nobetSayilari = new List<int>();

                if (gunGrubu == "Pazar")
                {
                    nobetSayilari = eczaneNobetGrupGunKuralIstatistikYatay
                        .Select(s => s.NobetSayisiPazar)
                        .Distinct().OrderBy(o => o).ToList();
                }
                else if (gunGrubu == "Bayram")
                {
                    nobetSayilari = eczaneNobetGrupGunKuralIstatistikYatay
                        .Select(s => s.NobetSayisiBayram)
                        .Distinct().OrderBy(o => o).ToList();
                }
                else if (gunGrubu == "Cumartesi")
                {
                    nobetSayilari = eczaneNobetGrupGunKuralIstatistikYatay
                        .Select(s => s.NobetSayisiCumartesi)
                        .Distinct().OrderBy(o => o).ToList();
                }
                else if (gunGrubu == "Arife")
                {
                    nobetSayilari = eczaneNobetGrupGunKuralIstatistikYatay
                        .Select(s => s.NobetSayisiArife)
                        .Distinct().OrderBy(o => o).ToList();
                }
                else
                {
                    nobetSayilari = eczaneNobetGrupGunKuralIstatistikYatay
                        .Select(s => s.NobetSayisiHaftaIci)
                        .Distinct().OrderBy(o => o).ToList();
                }

                var anahtarListeTumEczaneler = (from s in eczaneNobetGruplar
                                                from b in anahtarListeGunGrup
                                                    .Where(w => w.EczaneNobetGrupId == s.Id).DefaultIfEmpty()
                                                select new AnahtarListe
                                                {
                                                    EczaneNobetGrupId = s.Id,
                                                    EczaneAdi = s.EczaneAdi,
                                                    NobetGrupAdi = s.NobetGrupAdi,
                                                    NobetGrupId = s.NobetGrupId,
                                                    Tarih = (b?.EczaneNobetGrupId == s.Id) ? b.Tarih : s.BaslangicTarihi, //AnahtarListe
                                                    NobetUstGrupBaslamaTarihi = nobetUstGrupBaslangicTarihi,
                                                    //EczaneNobetGrupBaslamaTarihi = s.no 
                                                })
                                                .OrderBy(o => o.Tarih)
                                                .ToList();

                var gruptakiEczaneSayisi = anahtarListeTumEczaneler.Count;

                var nobetGrupIdListe2 = new List<int> { nobetGrupId };

                var olmasiGerenNobetler = GetTakvimNobetGruplar(nobetUstGrupBaslangicTarihi, nobetSayilari, gruptakiEczaneSayisi, nobetGrupIdListe2, nobetGorevTipId, gunGrubu);

                foreach (var nobetSayisi in nobetSayilari)
                {
                    var olmasiGerenNobetler2 = olmasiGerenNobetler
                        .Where(w => w.NobetSayisi == nobetSayisi).ToList();

                    var anahtarListeTumEczaneler2 = anahtarListeTumEczaneler.Take(olmasiGerenNobetler2.Count).ToList();

                    for (int i = 0; i < anahtarListeTumEczaneler2.Count; i++)
                    {
                        anahtarListeTumEczanelerHepsi.Add(new AnahtarListe
                        {
                            GunGrup = gunGrubu,
                            Id = i + 1,
                            EczaneNobetGrupId = anahtarListeTumEczaneler[i].EczaneNobetGrupId,
                            EczaneAdi = anahtarListeTumEczaneler[i].EczaneAdi,
                            NobetGrupId = anahtarListeTumEczaneler[i].NobetGrupId,
                            NobetGrupAdi = anahtarListeTumEczaneler[i].NobetGrupAdi,
                            Tarih = olmasiGerenNobetler2[i].Tarih,
                            NobetUstGrupBaslamaTarihi = anahtarListeTumEczaneler[i].NobetUstGrupBaslamaTarihi,
                            NobetSayisi = nobetSayisi //haftaIciOlmasiGerenNobetler2[i].NobetSayisi
                        });
                    }
                }
            }
            return anahtarListeTumEczanelerHepsi;
        }

        public void SiraliNobetYaz(List<NobetGrupGorevTipDetay> nobetGrupGorevTipler,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            DateTime nobetBaslangicTarihi,
            DateTime nobetBitisTarihi,
            int nobetUstGrupId)
        {
            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrupId);
            //.Where(w => w.GunGrupId == 3);

            foreach (var gunGrup in nobetUstGrupGunGruplar)
            {
                var ilgiliTarihler = GetTakvimNobetGruplar(nobetBaslangicTarihi, nobetBitisTarihi, nobetGrupGorevTipler, gunGrup.GunGrupId);

                if (ilgiliTarihler.Count > 0)
                {
                    foreach (var nobetGrupGorevTip in nobetGrupGorevTipler)
                    {
                        var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipId(nobetGrupGorevTip.Id);

                        var nobetGrupGunGruplar = nobetGrupGorevTipGunKurallar.Select(s => s.GunGrupId).Distinct().ToList();

                        if (!nobetGrupGunGruplar.Contains(gunGrup.GunGrupId))
                        {
                            continue;
                        }

                        var ilgiliTarihlerByNobetGrup = ilgiliTarihler
                            .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id)
                            .OrderBy(o => o.Tarih).ToList();

                        var sonuclarTumu = new List<EczaneNobetCozum>();
                        //var siraliAnahtarListeSon = new List<EczaneNobetCozumAnaharListeGecis>();

                        var anahtarListeIlk = _eczaneNobetSonucPlanlananService.GetSonuclar(nobetGrupGorevTip.Id, gunGrup.GunGrupId).OrderBy(o => o.Tarih).ToList();

                        var alinacakEczaneSayisi = ilgiliTarihlerByNobetGrup.Count;

                        var tekrarEdecekDonguSayisi = 1;

                        if (ilgiliTarihlerByNobetGrup.Count > anahtarListeIlk.Count)
                        {
                            alinacakEczaneSayisi = anahtarListeIlk.Count;

                            var bolum = (int)Math.Ceiling((double)ilgiliTarihlerByNobetGrup.Count / alinacakEczaneSayisi);

                            tekrarEdecekDonguSayisi = bolum;// ilgiliTarihlerByNobetGrup.Count % alinacakEczaneSayisi == 0 ? bolum + 1 : bolum;
                        }

                        var j = 0;

                        var nobetYazilacakTarihSayisi = ilgiliTarihlerByNobetGrup.Count;

                        var gecis = new List<EczaneNobetCozumAnaharListeGecis>();

                        for (int d = 0; d < tekrarEdecekDonguSayisi; d++)
                        {
                            var anahtarListe = new List<EczaneNobetSonucListe2>();

                            var tarih = ilgiliTarihlerByNobetGrup[j];
                            var baslangicTarihi = tarih.Tarih;

                            if (d == 0)
                            {
                                anahtarListe = anahtarListeIlk;
                            }
                            else
                            {
                                anahtarListe = _eczaneNobetSonucPlanlananService.GetSonuclar(nobetGrupGorevTip.Id, gunGrup.GunGrupId).OrderBy(o => o.Tarih).ToList();
                            }

                            if (d == tekrarEdecekDonguSayisi - 1 && d > 0)
                            {
                                var kalanTarihSayisi = ilgiliTarihler.Where(w => w.Tarih > baslangicTarihi).Count() - anahtarListe.Count;

                                alinacakEczaneSayisi = kalanTarihSayisi < 0 ? 0 : kalanTarihSayisi;
                            }

                            if (sonuclarTumu.Count < nobetYazilacakTarihSayisi && d == tekrarEdecekDonguSayisi - 1 && d > 0)
                            {
                                alinacakEczaneSayisi = nobetYazilacakTarihSayisi - sonuclarTumu.Count;
                            }

                            var sonrakiTarihIndex = alinacakEczaneSayisi;

                            if (d > 0)
                            {
                                sonrakiTarihIndex += sonuclarTumu.Count;
                            }

                            var bitisTarihi = ilgiliTarihlerByNobetGrup[sonrakiTarihIndex - 1].Tarih;

                            var eczaneNobetGruplar = eczaneNobetGruplarTumu
                                .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                && (w.BitisTarihi >= bitisTarihi || w.BitisTarihi == null)
                                ).ToList();

                            //son Anahtar Listede Olan EczaneNobetGruplarda olmayan Eczaneler (yeni anahtar listede olmayacak eczaneler)
                            var kapanmadanDolayiYeniAnahtarListedencikarilacakEczaneler = anahtarListe
                                .Where(w => !eczaneNobetGruplar.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

                            //anahtar listeden kapanan eczaneyi çıkardık
                            anahtarListe = anahtarListe
                                .Where(w => !kapanmadanDolayiYeniAnahtarListedencikarilacakEczaneler.Select(s => s.EczaneNobetGrupId).Contains(w.EczaneNobetGrupId)).ToList();


                            var sonNobetci = anahtarListe
                                   .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                            && w.GunGrupId == gunGrup.GunGrupId)
                                            .LastOrDefault();

                            var sonNobetcininOncekiNobeti = anahtarListe
                                   .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                            && w.GunGrupId == gunGrup.GunGrupId
                                            && w.Tarih < sonNobetci.Tarih
                                            && w.EczaneNobetGrupId == sonNobetci.EczaneNobetGrupId)
                                            .LastOrDefault() ?? new EczaneNobetSonucListe2();

                            var anahtarListeTumu = anahtarListe
                                .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                         && w.GunGrupId == gunGrup.GunGrupId
                                         && (w.Tarih > sonNobetcininOncekiNobeti.Tarih && w.Tarih <= sonNobetci.Tarih)
                                         )
                                         .OrderBy(o => o.Tarih)
                                         .ToList();

                            //ihtiyaç kadar çekildi
                            anahtarListe = anahtarListeTumu
                                .Take(alinacakEczaneSayisi)
                                .ToList();

                            if (alinacakEczaneSayisi > anahtarListe.Count)
                            {
                                alinacakEczaneSayisi = anahtarListe.Count;
                            }

                            //son Anahtar Listede Olmayan EczaneNobetGruplarda olan Eczaneler (yeni anahtar listeye eklenecek eczaneler - yeni eklenenler bakılan tarih aralığında ise)
                            var yeniAnahtarListeyeEklenecekEczaneler = eczaneNobetGruplar
                                .Where(w => !anahtarListeTumu.Select(s => s.EczaneNobetGrupId).Contains(w.Id)
                                        && (w.BaslangicTarihi >= baslangicTarihi && w.BaslangicTarihi <= bitisTarihi)).ToList();

                            var eczaneNobetCozumAnaharListeGecis = new List<EczaneNobetCozumAnaharListeGecis>();

                            //var av = eczaneNobetGruplar.Where(w => w.EczaneAdi == "AVDANLIOĞLU");

                            //if (av.Count() > 0)
                            //{

                            //}

                            if (yeniAnahtarListeyeEklenecekEczaneler.Count > 0)
                            {
                                foreach (var yeniEczane in yeniAnahtarListeyeEklenecekEczaneler)
                                {
                                    var yeniEczaneTarih = yeniEczane.BaslangicTarihi < yeniEczane.NobetUstGrupBaslamaTarihi ? yeniEczane.NobetUstGrupBaslamaTarihi : yeniEczane.BaslangicTarihi;

                                    var takvim = GetDetay(yeniEczaneTarih);

                                    eczaneNobetCozumAnaharListeGecis.Add(new EczaneNobetCozumAnaharListeGecis
                                    {
                                        EczaneNobetGrupId = yeniEczane.Id,
                                        NobetGorevTipId = yeniEczane.NobetGorevTipId,
                                        TakvimId = takvim.TakvimId,
                                        Tarih = takvim.Tarih,
                                        EczaneTipId = 0//anahtar tarihte yeni eklenen eczane varsa liste kayacak
                                    });
                                }

                            }

                            for (int i = 0; i < alinacakEczaneSayisi; i++)
                            {
                                tarih = ilgiliTarihlerByNobetGrup[j];

                                eczaneNobetCozumAnaharListeGecis.Add(new EczaneNobetCozumAnaharListeGecis
                                {
                                    EczaneNobetGrupId = anahtarListe[i].EczaneNobetGrupId,
                                    NobetGorevTipId = anahtarListe[i].NobetGorevTipId,
                                    TakvimId = tarih.TakvimId,
                                    Tarih = tarih.Tarih,
                                    EczaneTipId = 1
                                });

                                j++;
                            }

                            var siraliAnahtarListeSon = eczaneNobetCozumAnaharListeGecis
                                    .OrderBy(o => o.Tarih)
                                    .ThenBy(o => o.EczaneTipId)
                                    .Take(alinacakEczaneSayisi)
                                    .ToList();

                            gecis.AddRange(siraliAnahtarListeSon);

                            var k = sonuclarTumu.Count;

                            var araCozum = new List<EczaneNobetCozum>();

                            for (int i = 0; i < alinacakEczaneSayisi; i++)
                            {
                                araCozum.Add(new EczaneNobetCozum
                                {
                                    EczaneNobetGrupId = siraliAnahtarListeSon[i].EczaneNobetGrupId,
                                    NobetGorevTipId = siraliAnahtarListeSon[i].NobetGorevTipId,
                                    TakvimId = ilgiliTarihlerByNobetGrup[k].TakvimId
                                });

                                k++;
                            }

                            sonuclarTumu.AddRange(araCozum);

                            _eczaneNobetSonucPlanlananService.CokluEkle(araCozum);

                            if (sonuclarTumu.Count < nobetYazilacakTarihSayisi && d == tekrarEdecekDonguSayisi - 1 && d > 0)
                            {
                                tekrarEdecekDonguSayisi++;
                            }
                        }
                    }

                }
            }
        }

        #endregion
    }
}
