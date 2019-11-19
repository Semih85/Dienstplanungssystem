using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WM.Northwind.Entities.Concrete.Enums;

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
        private INobetDurumService _nobetDurumService;
        private IEczaneNobetSanalSonucService _eczaneNobetSanalSonuc;

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
                                       IEczaneNobetDegisimService eczaneNobetDegisimService,
                                       INobetDurumService nobetDurumService,
                                       IEczaneNobetSanalSonucService eczaneNobetSanalSonuc
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
            _nobetDurumService = nobetDurumService;
            _eczaneNobetSanalSonuc = eczaneNobetSanalSonuc;
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
        public void Delete(int[] ids)
        {
            var liste = new List<EczaneNobetSonuc>();

            foreach (var id in ids)
            {
                liste.Add(new EczaneNobetSonuc { Id = id });
            }

            _eczaneNobetSonucDal.Delete(liste);
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

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluSil(int[] ids)
        {
            _eczaneNobetSonucDal.CokluSil(ids);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluNobetYayimla(int[] ids, bool yayimlandiMi)
        {
            var sonuclar = GetList(ids);

            _eczaneNobetSonucDal.CokluYayimla(sonuclar, yayimlandiMi);
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

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void InsertSonuclarInsertSanalSonuclar(EczaneNobetSonuc eczaneNobetSonuc, EczaneNobetSanalSonuc eczaneNobetSanalSonuc)
        {
            Insert(eczaneNobetSonuc);

            var sonuc = GetDetay(eczaneNobetSonuc.TakvimId, eczaneNobetSonuc.EczaneNobetGrupId);

            eczaneNobetSanalSonuc.EczaneNobetSonucId = sonuc.Id;

            _eczaneNobetSanalSonuc.Insert(eczaneNobetSanalSonuc);
        }


        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void UpdateSonuclarUpdateSanalSonuclar(EczaneNobetSonuc eczaneNobetSonuc, EczaneNobetSanalSonuc eczaneNobetSanalSonuc)
        {
            Update(eczaneNobetSonuc);

            var sonuc = GetDetay(eczaneNobetSonuc.TakvimId, eczaneNobetSonuc.EczaneNobetGrupId);

            eczaneNobetSanalSonuc.EczaneNobetSonucId = sonuc.Id;

            _eczaneNobetSanalSonuc.Update(eczaneNobetSanalSonuc);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void SilSonuclarSilSanalSonuclar(int eczaneNobetSonucId)
        {
            _eczaneNobetSanalSonuc.Delete(eczaneNobetSonucId);

            Delete(eczaneNobetSonucId);
        }

        public EczaneNobetSonuc GetById(int Id)
        {
            return _eczaneNobetSonucDal.Get(x => x.Id == Id);
        }

        public EczaneNobetSonucDetay2 GetDetay2ById(int Id)
        {
            return _eczaneNobetSonucDal.GetDetay(x => x.Id == Id);
        }

        public EczaneNobetSonucDetay2 GetDetay(int takvimId, int eczaneNobetGrupId)
        {
            return _eczaneNobetSonucDal.GetDetay(x => x.TakvimId == takvimId && x.EczaneNobetGrupId == eczaneNobetGrupId);
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

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonuc> GetList(int[] ids)
        {
            return _eczaneNobetSonucDal.GetList(x => ids.Contains(x.Id));
        }

        #endregion

        #region detaylar

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylar(int nobetUstGrupId)
        {
            //var sw = new Stopwatch();
            //sw.Start();
            //var detaylar = 
            //sw.Stop();
            return _eczaneNobetSonucDal.GetDetayList(w => w.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<MyDrop> GetNobetGrupSonYayimNobetTarihleri(int nobetUstGrupId)
        {
            var liste = new List<MyDropSonuclar>();

            liste = _eczaneNobetSonucDal
                .GetDetayList(w => w.NobetUstGrupId == nobetUstGrupId && w.YayimlandiMi)
                .GroupBy(g => new
                {
                    g.NobetGrupGorevTipId,
                    g.NobetGorevTipAdi,
                    g.NobetGrupAdi
                })
                .Select(s => new MyDropSonuclar
                {
                    Id = s.Key.NobetGrupGorevTipId,
                    Value = $"{s.Key.NobetGrupGorevTipId}, {s.Key.NobetGrupAdi}, {s.Key.NobetGorevTipAdi}, Son Nöbet Yayım Tarihi: ",
                    Tarih = s.Max(m => m.Tarih)
                }).ToList();

            if (liste.Count == 0)
            {
                liste = _eczaneNobetSonucDal
                    .GetDetayList(w => w.NobetUstGrupId == nobetUstGrupId)
                    .GroupBy(g => new
                    {
                        g.NobetGrupGorevTipId,
                        g.NobetGorevTipAdi,
                        g.NobetGrupAdi
                    })
                    .Select(s => new MyDropSonuclar
                    {
                        Id = s.Key.NobetGrupGorevTipId,
                        Value = $"{s.Key.NobetGrupGorevTipId}, {s.Key.NobetGrupAdi}, {s.Key.NobetGorevTipAdi}, Nöbetler hiç yayımlanmamış! ({s.Min(m => m.Tarih).ToShortDateString()}-{s.Max(m => m.Tarih).ToShortDateString()}) ",
                        Tarih = s.Max(m => m.Tarih)
                    }).ToList();
            }

            return liste
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.Value} {s.Tarih.ToShortDateString()}"
                }).ToList();
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
        public List<EczaneNobetSonucDetay2> GetDetaylarById(int[] ids)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => ids.Contains(x.Id));
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
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId);
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
        public List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipIdList(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                                       && (x.Tarih <= bitisTarihi || bitisTarihi == null)
                                                       && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
                                                       );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipIdList(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList, bool sanalNobetler)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                                       && (x.Tarih <= bitisTarihi || bitisTarihi == null)
                                                       && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
                                                       && (x.EczaneNobetGrupBitisTarihi == null || sanalNobetler)
                                                       && (x.SanalNobetMi || x.SanalNobetMi == false)
                                                       );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipIdList(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList, bool kapaliEczaneler, bool sanalNobetler)
        {
            var sonuclar = new List<EczaneNobetSonucDetay2>();

            //sonuclar = _eczaneNobetSonucDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
            //                           && (x.Tarih <= bitisTarihi || bitisTarihi == null)
            //                           && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
            //                           && (x.EczaneNobetGrupBitisTarihi == null || kapaliEczaneler)
            //                           && (x.SanalNobetMi == sanalNobetler || sanalNobetler == false)
            //                           );

            if (sanalNobetler)
            {
                sonuclar = _eczaneNobetSonucDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                                       && (x.Tarih <= bitisTarihi || bitisTarihi == null)
                                                       && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
                                                       && (x.EczaneNobetGrupBitisTarihi == null || kapaliEczaneler)
                                                       //&& (x.SanalNobetMi == sanalNobetler || sanalNobetler == true)
                                                       );
            }
            else
            {
                sonuclar = _eczaneNobetSonucDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                                       && (x.Tarih <= bitisTarihi || bitisTarihi == null)
                                                       && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
                                                       && (x.EczaneNobetGrupBitisTarihi == null || kapaliEczaneler)
                                                       && (x.SanalNobetMi == sanalNobetler)
                                                       );
            }

            return sonuclar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipIdList(int[] nobetGrupGorevTipIdList)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylarGunluk(DateTime nobetTarihi, int nobetUstGrupId)
        {
            var sonuclar = _eczaneNobetSonucDal.GetDetayList(x => x.Tarih == nobetTarihi
            && (x.NobetUstGrupId == nobetUstGrupId || nobetUstGrupId == 0));

            return sonuclar;
            //sonuclar.Count == 0
            //? throw new Exception($"{nobetTarihi} tarihinde nöbet tutan eczane bulunmamaktadır.")
            //: sonuclar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslamaTarihi, int nobetGrupGorevTipId)
        {
            var sonuclar = _eczaneNobetSonucDal.GetDetayList(x => x.Tarih >= baslamaTarihi
            && (x.NobetGrupGorevTipId == nobetGrupGorevTipId));

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
        public List<EczaneNobetSonucDetay2> GetDetaylarUstGrupBaslamaTarihindenSonraEczaneNobetGrupId(int eczaneNobetGrupId)
        {
            return _eczaneNobetSonucDal.GetDetayList(x => x.EczaneNobetGrupId == eczaneNobetGrupId && x.Tarih >= x.NobetUstGrupBaslamaTarihi);
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
        public List<EczaneNobetSonucListe2> GetSonuclar(int nobetGrupGorevTipId, int gunGrupId)
        {
            var sonuclar = GetDetaylarByNobetGrupGorevTipId(nobetGrupGorevTipId);

            return GetSonuclar(sonuclar)
                .Where(w => w.GunGrupId == gunGrupId).ToList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(int nobetUstGrupId)
        {
            var detaylar = GetDetaylar(nobetUstGrupId);

            if (nobetUstGrupId == 5)
            {
                var sonuclar = GetSonuclar(detaylar, nobetUstGrupId);

                var nobetDurumlar = _nobetDurumService.GetDetaylar();
                //.Where(w => w.NobetDurumTipId != 4).ToList();

                return GetSonuclar(sonuclar, nobetDurumlar);
            }
            //var sw = new Stopwatch();
            //sw.Start();
            //var sonuclarT = GetSonuclar(detaylar, nobetUstGrupId);
            //sw.Stop();

            return GetSonuclar(detaylar, nobetUstGrupId); ;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(int[] nobetGrupGorevTipIdList)
        {
            var detaylar = GetDetaylarByNobetGrupGorevTipIdList(nobetGrupGorevTipIdList);

            var nobetUstGrupId = detaylar.Select(s => s.NobetUstGrupId).FirstOrDefault();

            if (nobetUstGrupId == 5)
            {
                var sonuclar = GetSonuclar(detaylar, nobetUstGrupId);

                var nobetDurumlar = _nobetDurumService.GetDetaylar();
                //.Where(w => w.NobetDurumTipId != 4).ToList();

                return GetSonuclar(sonuclar, nobetDurumlar);
            }
            //var sw = new Stopwatch();
            //sw.Start();
            //var sonuclarT = GetSonuclar(detaylar, nobetUstGrupId);
            //sw.Stop();

            return GetSonuclar(detaylar, nobetUstGrupId); ;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar(int[] nobetGrupGorevTipIdList, DateTime? baslangicTarihi, DateTime? bitisTarihi)
        {
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylarByIdList(nobetGrupGorevTipIdList.ToList());

            var detaylar = GetDetaylarByNobetGrupGorevTipIdList(baslangicTarihi, bitisTarihi, nobetGrupGorevTipIdList);

            var nobetUstGrupId = detaylar.Select(s => s.NobetUstGrupId).FirstOrDefault();

            //if (nobetUstGrupId == 5)
            //{
            //    var sonuclar = GetSonuclar(detaylar, nobetUstGrupId);

            //    var nobetDurumlar = _nobetDurumService.GetDetaylar();
            //    //.Where(w => w.NobetDurumTipId != 4).ToList();

            //    return GetSonuclar(sonuclar, nobetDurumlar);
            //}

            //var sw = new Stopwatch();
            //sw.Start();
            //var sonuclarT = GetSonuclar(detaylar, nobetUstGrupId);
            //sw.Stop();

            return GetSonuclar(detaylar, nobetUstGrupId);
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
            //var sw = new Stopwatch();
            //sw.Start();
            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrupId);
            //var s1 = sw.Elapsed;
            //sw.Restart();
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(nobetUstGrupId);
            //var s2 = sw.Elapsed;
            //sw.Restart();
            //var mazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrupId);
            //var s3 = sw.Elapsed;
            //sw.Restart();
            //var istekler = _eczaneNobetIstekService.GetDetaylar(nobetUstGrupId);
            //var s4 = sw.Elapsed;
            //sw.Restart();
            var sonuclar = _eczaneNobetOrtakService.EczaneNobetSonucBirlesim(
                nobetGrupGorevTipGunKurallar,
                eczaneNobetSonucDetaylar,
                nobetGrupGorevTipTakvimOzelGunler,
                //mazeretler,
                //istekler,
                EczaneNobetSonucTuru.Kesin);
            //var s5 = sw.Elapsed;
            //sw.Stop();

            return sonuclar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        private List<EczaneNobetSonucListe2> GetSonuclar(List<EczaneNobetSonucDetay2> eczaneNobetSonucDetaylar)
        {
            var nobetUstGrupId = eczaneNobetSonucDetaylar.Select(s => s.NobetUstGrupId).Distinct().FirstOrDefault();

            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrupId);
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(nobetUstGrupId);
            //var mazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrupId);
            //var istekler = _eczaneNobetIstekService.GetDetaylar(nobetUstGrupId);

            var liste = _eczaneNobetOrtakService.EczaneNobetSonucBirlesim(
                nobetGrupGorevTipGunKurallar,
                eczaneNobetSonucDetaylar,
                nobetGrupGorevTipTakvimOzelGunler,
                //mazeretler,
                //istekler,
                EczaneNobetSonucTuru.Kesin);

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

            var liste = _eczaneNobetOrtakService.EczaneNobetSonucBirlesim(
                nobetGrupGorevTipGunKurallar,
                eczaneNobetSonucDetaylar,
                nobetGrupGorevTipTakvimOzelGunler,
                mazeretler,
                istekler,
                EczaneNobetSonucTuru.Kesin);

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

        public List<EczaneNobetSonucListe2> GetSonuclar(List<EczaneNobetSonucListe2> eczaneNobetSonuclar, List<NobetDurumDetay> nobetDurumDetaylar)
        {
            eczaneNobetSonuclar = eczaneNobetSonuclar
                .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi).ToList();

            var tarihler = eczaneNobetSonuclar
                //.Where(w => w.Tarih >= w.NobetUstGrupBaslamaTarihi)
                .Select(s => new { s.TakvimId, s.Tarih })
                .Distinct().ToList();

            var sonuclar = new List<EczaneNobetSonucListe2>();

            var kontrol = false;

            foreach (var tarih in tarihler)
            {
                if (kontrol)
                {
                    if (tarih.Tarih == new DateTime(2019, 10, 27))
                    {
                    }
                }

                var sonuclar2 = eczaneNobetSonuclar
                    .Where(w => w.TakvimId == tarih.TakvimId).ToList();

                if (sonuclar2.Count != 3)
                    continue;

                var altGruplar = sonuclar2
                    .Select(s => new { s.NobetAltGrupId, s.NobetAltGrupAdi }).Distinct()
                    .OrderBy(o => o.NobetAltGrupAdi).ToArray();

                if (altGruplar.Length != 3)
                    continue;

                for (int i = 0; i < altGruplar.Length; i++)
                {
                    var altGrupId1 = 0;
                    var altGrupId2 = 0;
                    var altGrupId3 = 0;

                    if (i == 0)
                    {
                        altGrupId1 = altGruplar[0].NobetAltGrupId;
                        altGrupId2 = altGruplar[1].NobetAltGrupId;
                        altGrupId3 = altGruplar[2].NobetAltGrupId;
                    }
                    else if (i == 1)
                    {
                        altGrupId1 = altGruplar[1].NobetAltGrupId;
                        altGrupId2 = altGruplar[0].NobetAltGrupId;
                        altGrupId3 = altGruplar[2].NobetAltGrupId;
                    }
                    else if (i == 2)
                    {
                        altGrupId1 = altGruplar[2].NobetAltGrupId;
                        altGrupId2 = altGruplar[0].NobetAltGrupId;
                        altGrupId3 = altGruplar[1].NobetAltGrupId;
                    }

                    var nobetDurum = nobetDurumDetaylar
                        .Where(w => w.NobetAltGrupId1 == altGrupId1
                                 && (w.NobetAltGrupId2 == altGrupId2 && w.NobetAltGrupId3 == altGrupId3)
                                 ).SingleOrDefault() ?? new NobetDurumDetay { NobetDurumTipAdi = "Tanımsız Durum" };

                    var sonuc = sonuclar2
                        .Where(w => w.NobetAltGrupId == altGrupId1).SingleOrDefault() ?? new EczaneNobetSonucListe2();

                    if (sonuc.Id != 0)
                    {
                        sonuc.NobetDurumId = nobetDurum.Id;
                        sonuc.NobetDurumTipId = nobetDurum.NobetDurumTipId;
                        sonuc.NobetDurumTipAdi = nobetDurum.NobetDurumTipAdi;
                        sonuc.NobetDurumAdi = $"{nobetDurum.NobetAltGrupAdi1}, {nobetDurum.NobetAltGrupAdi2}, {nobetDurum.NobetAltGrupAdi3}";
                    }

                    sonuclar.Add(sonuc);
                }
            }

            var anahtarSonuclar = eczaneNobetSonuclar
                .Where(w => w.Tarih < w.NobetUstGrupBaslamaTarihi)
                .ToList();

            sonuclar.AddRange(anahtarSonuclar);

            return sonuclar;
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

            if (!(oncekiBakilacakAySayisi >= -8 && oncekiBakilacakAySayisi < 0))
            {
                throw new Exception("Geriye dönük aynı gün nöbet tutanlar en az 0 en fazla 8 ay engellenebilir");
            }

            var oncekiAylardaBakilacakSonuclar = eczaneNobetSonuclarOncekiAylar.Where(w => w.Tarih >= baslangicTarihi.AddMonths(oncekiBakilacakAySayisi)).ToList();

            var ocnekiNobetlerTarihAraligi = oncekiAylardaBakilacakSonuclar
                .Select(s => new
                {
                    s.TakvimId,
                    s.Tarih,
                    s.TarihAciklama,
                    s.NobetGunKuralId
                })
                .Distinct()
                .OrderBy(o => o.Tarih).ToList();

            foreach (var tarih in ocnekiNobetlerTarihAraligi)
            {
                var gunlukSonuclar = oncekiAylardaBakilacakSonuclar.Where(w => w.TakvimId == tarih.TakvimId).ToList();

                if (gunlukSonuclar.Count > 1)
                {
                    foreach (var sonuc in gunlukSonuclar)
                    {
                        oncekiAylardaAyniGunNobetTutanEczaneGruplar
                            .Add(new EczaneGrupDetay
                            {
                                EczaneGrupTanimId = indisId + tarih.TakvimId,
                                EczaneId = sonuc.EczaneId,
                                ArdisikNobetSayisi = 0,
                                NobetUstGrupId = sonuc.NobetUstGrupId,
                                EczaneGrupTanimAdi = $"{tarih.Tarih.ToString("dd.MM.yy-ddd.")} nöbeti",
                                EczaneGrupTanimTipAdi = "Aynı gün nöbet",
                                EczaneGrupTanimTipId = tarih.NobetGunKuralId,
                                NobetGrupId = sonuc.NobetGrupId,
                                EczaneAdi = sonuc.EczaneAdi,
                                NobetGrupAdi = sonuc.NobetGrupAdi,
                                NobetGorevTipAdi = sonuc.NobetGorevTipAdi,
                                NobetGorevTipId = sonuc.NobetGorevTipId,
                                EczaneNobetGrupId = sonuc.EczaneNobetGrupId,
                                AyniGunNobetTutabilecekEczaneSayisi = 1
                                //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                            });
                    }
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
                   GunGrupAdi = s.Key.GunGrup,
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
                    s.GunGrupAdi,
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
                            GunGrupAdi = nobetGunKural.GunGrupAdi,
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