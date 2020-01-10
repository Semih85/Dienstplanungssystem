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
using System.Data.Entity.Infrastructure;
using WM.Core.Aspects.PostSharp.TranstionAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Entities.Concrete.Enums;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class AyniGunTutulanNobetManager : IAyniGunTutulanNobetService
    {
        #region ctor
        private IAyniGunTutulanNobetDal _ayniGunTutulanNobetDal;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IEczaneGrupService _eczaneGrupService;
        //private INobetGrupService _nobetGrupService;
        //private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        //private ITakvimService _takvimService;//takvim servis ortak servisle birlikte kullanılamıyor. cycle oluşuyor.

        public AyniGunTutulanNobetManager(IAyniGunTutulanNobetDal ayniGunTutulanNobetDal,
            IEczaneNobetGrupService eczaneNobetGrupService,
            IEczaneGrupService eczaneGrupService,
            INobetGrupGorevTipService nobetGrupGorevTipService
            //INobetGrupService nobetGrupService,
            //IEczaneNobetOrtakService eczaneNobetOrtakService
            //ITakvimService takvimService
            )
        {
            _ayniGunTutulanNobetDal = ayniGunTutulanNobetDal;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneGrupService = eczaneGrupService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            //_nobetGrupService = nobetGrupService;
            //_eczaneNobetOrtakService = eczaneNobetOrtakService;
            //_takvimService = takvimService;
        }
        #endregion

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int ayniGunTutulanNobetId)
        {
            _ayniGunTutulanNobetDal.Delete(new AyniGunTutulanNobet { Id = ayniGunTutulanNobetId });
        }

        public AyniGunTutulanNobet GetById(int ayniGunTutulanNobetId)
        {
            return _ayniGunTutulanNobetDal.Get(x => x.Id == ayniGunTutulanNobetId);
        }

        public AyniGunTutulanNobet GetByIkiliEczaneler(int eczaneNobetGrupId1, int eczaneNobetGrupId2)
        {
            return _ayniGunTutulanNobetDal.Get(w => (w.EczaneNobetGrupId1 == eczaneNobetGrupId1 && w.EczaneNobetGrupId2 == eczaneNobetGrupId2)
                                                 || (w.EczaneNobetGrupId1 == eczaneNobetGrupId2 && w.EczaneNobetGrupId2 == eczaneNobetGrupId1));
        }

        public AyniGunTutulanNobetDetay GetDetay(int eczaneNobetGrupId1, int eczaneNobetGrupId2)
        {
            return _ayniGunTutulanNobetDal.GetDetay(x => x.EczaneNobetGrupId1 == eczaneNobetGrupId1 || x.EczaneNobetGrupId2 == eczaneNobetGrupId2);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobet> GetList()
        {
            return _ayniGunTutulanNobetDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobet> GetList(int nobetUstGrupId)
        {
            return _ayniGunTutulanNobetDal.GetList(x => x.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId
                                                     //|| x.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId
                                                     );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobet> GetListSifirdanBuyukler(int nobetUstGrupId)
        {
            return _ayniGunTutulanNobetDal.GetList(x => (x.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId
                                                      //|| x.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId
                                                      )
                                                      && x.AyniGunNobetSayisi > 0);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobet> GetListSifirdanFarkli(int nobetUstGrupId)
        {
            return _ayniGunTutulanNobetDal.GetList(x => (x.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId
                                                      //|| x.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId
                                                      )
                                                      && x.AyniGunNobetSayisi != 0);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(AyniGunTutulanNobet ayniGunTutulanNobet)
        {
            _ayniGunTutulanNobetDal.Insert(ayniGunTutulanNobet);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(AyniGunTutulanNobet ayniGunTutulanNobet)
        {
            _ayniGunTutulanNobetDal.Update(ayniGunTutulanNobet);
        }

        public AyniGunTutulanNobetDetay GetDetayById(int ayniGunTutulanNobetId)
        {
            return _ayniGunTutulanNobetDal.GetDetay(x => x.Id == ayniGunTutulanNobetId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> GetDetaylar()
        {
            return _ayniGunTutulanNobetDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _ayniGunTutulanNobetDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> GetDetaylar(List<int> nobetGrupIdList)
        {
            return _ayniGunTutulanNobetDal.GetDetayList(x => (nobetGrupIdList.Contains(x.NobetGrupId1) && nobetGrupIdList.Contains(x.NobetGrupId2))
                                            || (nobetGrupIdList.Contains(x.NobetGrupId2) && nobetGrupIdList.Contains(x.NobetGrupId1))
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> GetDetaylar(int[] nobetGrupGorevTipIdList)
        {
            return _ayniGunTutulanNobetDal.GetDetayList(x => (nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId1) && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId2))
                                                          || (nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId2) && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId1)));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> GetDetaylar(int nobetUstGrupId, int ayniGunNobetSayisi)
        {
            return _ayniGunTutulanNobetDal.GetDetayList(x => (x.NobetUstGrupId == nobetUstGrupId)
            && x.AyniGunNobetSayisi >= ayniGunNobetSayisi
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> IkiliEczaneleriOlustur(int nobetUstGrupId)
        {
            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            IkiliEczaneleriOlustur(eczaneNobetGruplar);

            return GetDetaylar(nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> IkiliEczaneleriOlustur(List<EczaneNobetGrupDetay> eczaneNobetGruplar)
        {
            var nobetUstGrupId = eczaneNobetGruplar.Select(s => s.NobetUstGrupId).Distinct().FirstOrDefault();

            if (nobetUstGrupId == 4 || nobetUstGrupId == 5)
            {//giresun, Osmaniye

                if (nobetUstGrupId == 4)
                {
                    eczaneNobetGruplar = eczaneNobetGruplar.Where(w => w.NobetGorevTipId == 2).ToList();
                }

                var eczaneNobetGruplarSirali = eczaneNobetGruplar.OrderBy(o => o.Id).ToList();

                var altGruplar = eczaneNobetGruplarSirali
                    .Select(s => new { s.NobetAltGrupAdi, s.NobetAltGrupId })
                    .Distinct()
                    .OrderBy(o => o.NobetAltGrupId).ToList();

                foreach (var altGrup in altGruplar)
                {
                    var eczaneNobetGruplar1 = eczaneNobetGruplarSirali
                        .Where(w => w.NobetAltGrupId == altGrup.NobetAltGrupId).ToList();

                    foreach (var eczaneNobetGrup1 in eczaneNobetGruplar1)
                    {
                        var eczaneNobetGruplar2 = eczaneNobetGruplar
                            .Where(w => w.NobetAltGrupId > eczaneNobetGrup1.NobetAltGrupId).ToList();

                        if (nobetUstGrupId == 5)
                        {
                            if (altGrup.NobetAltGrupAdi.StartsWith("B"))
                            {
                                eczaneNobetGruplar2 = eczaneNobetGruplar2.Where(w => !w.NobetAltGrupAdi.StartsWith("B")).ToList();
                            }
                            else if (altGrup.NobetAltGrupAdi.StartsWith("D"))
                            {
                                eczaneNobetGruplar2 = eczaneNobetGruplar2.Where(w => !w.NobetAltGrupAdi.StartsWith("D")).ToList();
                            }
                        }
                        //else if (altGrup.NobetAltGrupAdi.StartsWith("A"))
                        //{
                        //    eczaneNobetGruplar2 = eczaneNobetGruplar2.Where(w => w.NobetAltGrupAdi != altGrup.NobetAltGrupAdi).ToList();
                        //}

                        foreach (var eczaneNobetGrup2 in eczaneNobetGruplar2)
                        {
                            IkiliEczaneleriOlustur(eczaneNobetGrup1.Id, eczaneNobetGrup2.Id);
                        }
                    }
                }
            }
            else if (nobetUstGrupId == 6 || nobetUstGrupId == 9)
            {//bartın, Çorum 
           
                var eczaneNobetGruplar1 = eczaneNobetGruplar.OrderBy(o => o.Id).ToArray();

                foreach (var eczaneNobetGrup1 in eczaneNobetGruplar1)
                {
                    var eczaneNobetGruplar2 = eczaneNobetGruplar
                        .Where(w => w.Id > eczaneNobetGrup1.Id).ToArray();

                    foreach (var eczaneNobetGrup2 in eczaneNobetGruplar2)
                    {
                        IkiliEczaneleriOlustur(eczaneNobetGrup1.Id, eczaneNobetGrup2.Id);
                    }
                }
            }
            else
            {
                var nobetGrupGorevTipIdList = eczaneNobetGruplar
                    .Select(s => s.NobetGrupGorevTipId)
                    .Distinct()
                    .OrderBy(o => o).ToList();

                foreach (var nobetGrupGorevTipId in nobetGrupGorevTipIdList)
                {
                    var eczaneNobetGruplar1 = eczaneNobetGruplar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTipId).ToArray();

                    foreach (var eczaneNobetGrup1 in eczaneNobetGruplar1)
                    {
                        var eczaneNobetGruplar2 = eczaneNobetGruplar.Where(w => w.NobetGrupGorevTipId > eczaneNobetGrup1.NobetGrupGorevTipId).ToArray();

                        foreach (var eczaneNobetGrup2 in eczaneNobetGruplar2)
                        {
                            #region kontrol

                            var kontrol = false;

                            if (kontrol)
                            {
                                if (eczaneNobetGrup1.EczaneAdi == "ALPER" && eczaneNobetGrup2.EczaneAdi == "GÜLERYÜZ")
                                {

                                }
                            }

                            #endregion

                            IkiliEczaneleriOlustur(eczaneNobetGrup1.Id, eczaneNobetGrup2.Id);
                        }
                    }
                }
            }

            //var nobetGrupGorevTipId = eczaneNobetGruplar.Select(s => s.NobetGrupGorevTipId).Distinct().FirstOrDefault();

            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);

            //var eczaneNobetGruplarDigerleri = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId)
            //    .Where(w => !eczaneNobetGruplar.Select(s => s.Id).Contains(w.Id));

            //var digerNobetGruplari = nobetGrupGorevTipler.Where(w => w.Id != nobetGrupGorevTipId).ToList();

            //foreach (var eczaneNobetGrup1 in eczaneNobetGruplar)
            //{
            //    foreach (var nobetGrupGorevTip in digerNobetGruplari)
            //    {
            //        var eczaneNobetGruplar2 = eczaneNobetGruplarDigerleri.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

            //        foreach (var eczaneNobetGrup2 in eczaneNobetGruplar2)
            //        {
            //            IkiliEczaneleriOlustur(eczaneNobetGrup1.Id, eczaneNobetGrup2.Id);
            //        }
            //    }
            //}

            return GetDetaylar(nobetUstGrupId);
        }

        public void IkiliEczaneleriOlustur(int eczaneNobetGrupId1, int eczaneNobetGrupId2)
        {
            if (eczaneNobetGrupId1 > 0 && eczaneNobetGrupId2 > 0)
            {
                try
                {
                    _ayniGunTutulanNobetDal.Insert(new AyniGunTutulanNobet
                    {
                        EczaneNobetGrupId1 = eczaneNobetGrupId1,
                        EczaneNobetGrupId2 = eczaneNobetGrupId2,
                        EnSonAyniGunNobetTakvimId = 1
                    });
                }
                catch (DbUpdateException ex)
                {
                    var eczane1 = _eczaneNobetGrupService.GetDetayById(eczaneNobetGrupId1);
                    var eczane2 = _eczaneNobetGrupService.GetDetayById(eczaneNobetGrupId2);

                    var hata = ex.InnerException.ToString();

                    var dublicateHata = new string[2] {
                        "Cannot insert dublicate row in object",
                        "with unique index"
                    };

                    var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                    if (dublicateRowHatasiMi)
                    {
                        throw new Exception($"Mükerrer kayıt (<strong>{eczane1.EczaneAdi} - {eczane2.EczaneAdi} eczaneleri </strong>) eklenemez...");
                    }

                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception($"eczaneNobetGrupId 0 olamaz (eczaneNobetGrupId1: {eczaneNobetGrupId1}, eczaneNobetGrupId2: {eczaneNobetGrupId2})!");
            };

            //return ikiliEczane;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetArasinda2FarkOlanIkiliEczaneleri(List<EczaneNobetGrupDetay> eczaneNobetGruplar, int nobetUstGrupId, int nobetFarki = 2)
        {
            var ikiliEczanelerTumu = GetDetaylar(nobetUstGrupId);

            var eczaneGruplar = ArasindaKritereGoreFarkOlanEczaneler(eczaneNobetGruplar, ikiliEczanelerTumu, nobetFarki);

            return eczaneGruplar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetArasinda2FarkOlanIkiliEczaneleri(List<EczaneNobetGrupDetay> eczaneNobetGruplar, int[] nobetGrupGorevTipIdList, int nobetFarki = 2)
        {
            var ikiliEczaneler = GetDetaylar(nobetGrupGorevTipIdList);

            var eczaneGruplar = ArasindaKritereGoreFarkOlanEczaneler(eczaneNobetGruplar, ikiliEczaneler, nobetFarki);

            return eczaneGruplar;
        }

        public List<EczaneGrupDetay> ArasindaKritereGoreFarkOlanEczaneler(
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<AyniGunTutulanNobetDetay> ikiliEczanelerTumu,
            int nobetFarki)
        {
            var eczaneGruplar = new List<EczaneGrupDetay>();

            var kontrol = false;

            foreach (var eczaneNobetGrup in eczaneNobetGruplar)
            {
                var bakilanEczaneninIkilileri = ikiliEczanelerTumu
                    .Where(w => w.EczaneNobetGrupId1 == eczaneNobetGrup.Id || w.EczaneNobetGrupId2 == eczaneNobetGrup.Id).ToList();

                var digerNobetGruplari = bakilanEczaneninIkilileri
                    .Select(s => s.NobetGrupId1 == eczaneNobetGrup.NobetGrupId
                    ? s.NobetGrupId2
                    : s.NobetGrupId1).Distinct()
                    .OrderBy(o => o).ToArray();

                var bakilanEczane = new AyniGunNobetIstatistik
                {
                    NobetGrupId = eczaneNobetGrup.NobetGrupId,
                    NobetGrupAdi = eczaneNobetGrup.NobetGrupAdi,
                    EczaneNobetGrupId = eczaneNobetGrup.Id,
                    EczaneAdi = eczaneNobetGrup.EczaneAdi
                };

                #region kontrol

                if (kontrol && bakilanEczane.EczaneAdi == "BİLGE")
                {
                    var cc = eczaneGruplar.Where(w => w.EczaneGrupTanimId == 839).ToList();
                }

                #endregion

                foreach (var digerNobetGrup in digerNobetGruplari)
                {
                    var bakilanEczaneninIkilileri2 = bakilanEczaneninIkilileri
                        .Where(w => w.NobetGrupId1 == digerNobetGrup || w.NobetGrupId2 == digerNobetGrup).ToArray();

                    bakilanEczane.AyniGunNobetSayisiEnBuyuk = bakilanEczaneninIkilileri2.Max(x => x.AyniGunNobetSayisi);
                    bakilanEczane.AyniGunNobetSayisiEnKucuk = bakilanEczaneninIkilileri2.Min(x => x.AyniGunNobetSayisi);

                    var takipEdilecekEczaneler = bakilanEczaneninIkilileri2
                    .Where(w => bakilanEczane.AyniGunNobetSayisiEnBuyuk - bakilanEczane.AyniGunNobetSayisiEnKucuk >= nobetFarki
                             && bakilanEczane.AyniGunNobetSayisiEnBuyuk == w.AyniGunNobetSayisi).ToList();

                    if (takipEdilecekEczaneler.Count > 0)
                    {
                        var eczaneGrupTanimId = Convert.ToInt32($"{digerNobetGrup}{eczaneNobetGrup.Id}"); //eczaneNobetGrup.Id; //10000 + eczaneNobetGrup.Id;

                        var bakilanEczaneGrupTanimAdi = $"{bakilanEczane.NobetGrupAdi}, {bakilanEczane.EczaneAdi} - nobetGrubu {digerNobetGrup}: {bakilanEczane.AyniGunNobetSayisiEnBuyuk} kez aynı gün nöbet.";

                        var bakilanEczaneEklenecek = new EczaneGrupDetay
                        {
                            EczaneGrupTanimId = eczaneGrupTanimId,
                            EczaneId = eczaneNobetGrup.EczaneId,
                            ArdisikNobetSayisi = 0,
                            NobetUstGrupId = eczaneNobetGrup.NobetUstGrupId,
                            EczaneGrupTanimAdi = bakilanEczaneGrupTanimAdi,
                            EczaneGrupTanimTipAdi = "Tüm eczanelerle aynı gün nöbet",
                            EczaneGrupTanimTipId = -10, //-2,
                            NobetGrupId = eczaneNobetGrup.NobetGrupId,
                            EczaneAdi = eczaneNobetGrup.EczaneAdi,
                            NobetGrupAdi = eczaneNobetGrup.NobetGrupAdi,
                            EczaneNobetGrupId = eczaneNobetGrup.Id,
                            AyniGunNobetTutabilecekEczaneSayisi = 1
                            //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                        };

                        eczaneGruplar.Add(bakilanEczaneEklenecek);

                        var bakilanEczaneninAktifEsGruplari = _eczaneGrupService.GetDetaylarAktif(eczaneNobetGrup.Id);
                        var bakilanEzaneninEsGruplarindakiEczaneler = _eczaneGrupService.GetDetaylarByEczaneGrupTanimId(bakilanEczaneninAktifEsGruplari.Select(s => s.EczaneGrupTanimId).ToList()).ToList();

                        //var takipEdilecekEsGruptaOlmayanEczaneler = takipEdilecekEczaneler.Where(w=> bakilanEzaneninEsGruplarindakiEczaneler.Select(s => s.EczaneNobetGrupId).Contains(w.EczaneNobetGrupId1))
                        foreach (var takipEdilecekEczane in takipEdilecekEczaneler)
                        {
                            var nobetGrupId = (eczaneNobetGrup.Id == takipEdilecekEczane.EczaneNobetGrupId1 ? takipEdilecekEczane.NobetGrupId2 : takipEdilecekEczane.NobetGrupId1);
                            var eczaneAdi = (eczaneNobetGrup.Id == takipEdilecekEczane.EczaneNobetGrupId1 ? takipEdilecekEczane.EczaneAdi2 : takipEdilecekEczane.EczaneAdi1);
                            var eczaneId = (eczaneNobetGrup.Id == takipEdilecekEczane.EczaneNobetGrupId1 ? takipEdilecekEczane.EczaneId2 : takipEdilecekEczane.EczaneId1);
                            var nobetGrupAdi = (eczaneNobetGrup.Id == takipEdilecekEczane.EczaneNobetGrupId1 ? takipEdilecekEczane.NobetGrupAdi2 : takipEdilecekEczane.NobetGrupAdi1);
                            var eczaneNobetGrupId = (eczaneNobetGrup.Id == takipEdilecekEczane.EczaneNobetGrupId1 ? takipEdilecekEczane.EczaneNobetGrupId2 : takipEdilecekEczane.EczaneNobetGrupId1);

                            if (kontrol && eczaneAdi == "ABANT")
                            {
                            }

                            if (bakilanEzaneninEsGruplarindakiEczaneler.Select(s => s.EczaneId).Contains(eczaneId))
                                continue;

                            eczaneGruplar.Add(new EczaneGrupDetay
                            {
                                EczaneGrupTanimId = eczaneGrupTanimId,
                                EczaneId = eczaneId,
                                ArdisikNobetSayisi = 0,
                                NobetUstGrupId = eczaneNobetGrup.NobetUstGrupId,
                                EczaneGrupTanimAdi = bakilanEczaneGrupTanimAdi,
                                EczaneGrupTanimTipAdi = "Tüm eczanelerle aynı gün nöbet",
                                EczaneGrupTanimTipId = -10, //-2,
                                NobetGrupId = nobetGrupId,
                                EczaneAdi = eczaneAdi,
                                NobetGrupAdi = nobetGrupAdi,
                                EczaneNobetGrupId = eczaneNobetGrupId,
                                AyniGunNobetTutabilecekEczaneSayisi = 1
                                //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                            });
                        }
                    }
                }
            }

            return eczaneGruplar;
        }

        public void AyniGunNobetTutanlariTabloyaEkle(List<AyniGunTutulanNobetDetay> ayniGunNobetTutanEczaneler)
        {
            foreach (var ayniGunNobetTutanEczane in ayniGunNobetTutanEczaneler)
            {
                var eczaneNobetGrupId1 = ayniGunNobetTutanEczane.EczaneNobetGrupId1;
                var eczaneNobetGrupId2 = ayniGunNobetTutanEczane.EczaneNobetGrupId2;

                var ikiliEczane = GetByIkiliEczaneler(eczaneNobetGrupId1, eczaneNobetGrupId2);

                if (ikiliEczane == null)
                {
                    IkiliEczaneleriOlustur(eczaneNobetGrupId1, eczaneNobetGrupId2);

                    ikiliEczane = GetByIkiliEczaneler(eczaneNobetGrupId1, eczaneNobetGrupId2);

                    //ikiliEczane = new AyniGunTutulanNobet
                    //{
                    //    EczaneNobetGrupId1 = eczaneNobetGrupId1,
                    //    EczaneNobetGrupId2 = eczaneNobetGrupId2,
                    //    EnSonAyniGunNobetTakvimId = 1
                    //};
                }

                ikiliEczane.AyniGunNobetSayisi = ayniGunNobetTutanEczane.AyniGunNobetSayisi;
                ikiliEczane.EnSonAyniGunNobetTakvimId = ayniGunNobetTutanEczane.EnSonAyniGunNobetTakvimId;

                if (eczaneNobetGrupId1 > 0 && eczaneNobetGrupId2 > 0 && ikiliEczane.Id > 0)
                {
                    try
                    {
                        _ayniGunTutulanNobetDal.Update(ikiliEczane);
                    }
                    catch (Exception e)
                    {
                        throw e;
                        //new Exception($"{ayniGunNobetTutanEczane.G1Eczane} ve {ayniGunNobetTutanEczane.G2Eczane} eczanesi tabloda bulunmamaktadır!");
                    }
                }
            }
        }

        public void AyniGunNobetSayisiniGuncelle(List<AyniGunTutulanNobetDetay> ayniGunNobetTutanEczaneler, AyniGunNobetEklemeTuru ayniGunNobetEklemeTuru)
        {
            var ayniGunNobetTutanEczaneListesi = new List<AyniGunTutulanNobet>();

            foreach (var ayniGunNobetTutanEczane in ayniGunNobetTutanEczaneler)
            {
                var eczaneNobetGrupId1 = ayniGunNobetTutanEczane.EczaneNobetGrupId1;
                var eczaneNobetGrupId2 = ayniGunNobetTutanEczane.EczaneNobetGrupId2;

                if (eczaneNobetGrupId1 > 0 && eczaneNobetGrupId2 > 0)
                {
                    var ikiliEczaneDb = GetByIkiliEczaneler(eczaneNobetGrupId1, eczaneNobetGrupId2);

                    var ikiliEczane = new AyniGunTutulanNobet();

                    if (ikiliEczaneDb != null)
                    {
                        ikiliEczane.AyniGunNobetSayisi = ikiliEczaneDb.AyniGunNobetSayisi;
                        ikiliEczane.Id = ikiliEczaneDb.Id;
                    }

                    //var islemYapilacakIkiliEczaneler = ayniGunNobetTutanEczaneListesi
                    //    .Where(w => (w.EczaneNobetGrupId1 == eczaneNobetGrupId1 && w.EczaneNobetGrupId2 == eczaneNobetGrupId2)
                    //             || (w.EczaneNobetGrupId1 == eczaneNobetGrupId2 && w.EczaneNobetGrupId2 == eczaneNobetGrupId1)).SingleOrDefault();

                    //if (islemYapilacakIkiliEczaneler == null)
                    //{

                    if (ikiliEczaneDb == null)
                    {
                        IkiliEczaneleriOlustur(eczaneNobetGrupId1, eczaneNobetGrupId2);

                        ikiliEczaneDb = GetByIkiliEczaneler(eczaneNobetGrupId1, eczaneNobetGrupId2);

                        ikiliEczane.Id = ikiliEczaneDb.Id;
                        ikiliEczane.AyniGunNobetSayisi = ikiliEczaneDb.AyniGunNobetSayisi;
                    }

                    if (ayniGunNobetEklemeTuru == AyniGunNobetEklemeTuru.Azalt)
                    {
                        ikiliEczane.AyniGunNobetSayisi -= ayniGunNobetTutanEczane.AyniGunNobetSayisi;
                    }
                    else if (ayniGunNobetEklemeTuru == AyniGunNobetEklemeTuru.Arttır)
                    {
                        ikiliEczane.AyniGunNobetSayisi += ayniGunNobetTutanEczane.AyniGunNobetSayisi;
                    }
                    else if (ayniGunNobetEklemeTuru == AyniGunNobetEklemeTuru.Eşitle)
                    {
                        ikiliEczane.AyniGunNobetSayisi = ayniGunNobetTutanEczane.AyniGunNobetSayisi;
                    }

                    ikiliEczane.EnSonAyniGunNobetTakvimId = ayniGunNobetTutanEczane.EnSonAyniGunNobetTakvimId;

                    ayniGunNobetTutanEczaneListesi.Add(ikiliEczane);
                    //}
                    //else
                    //{
                    //    throw new Exception($"Mükerrer kayıt ({ayniGunNobetTutanEczane.EczaneAdi1} - {ayniGunNobetTutanEczane.EczaneAdi2} eczaneleri) eklenemez!");
                    //}

                    //try
                    //{
                    //    _ayniGunTutulanNobetDal.Update(ikiliEczane);
                    //}
                    //catch (Exception e)
                    //{
                    //    throw e;
                    //        //new Exception($"{ayniGunNobetTutanEczane.G1Eczane} ve {ayniGunNobetTutanEczane.G2Eczane} eczanesi tabloda bulunmamaktadır!");
                    //}
                }
            }

            #region kontrol

            var kontrol = false;

            if (kontrol)
            {
                var liste2 = ayniGunNobetTutanEczaneListesi
                    .GroupBy(w => w.Id)
                    .Select(s => new { s.Key, sayi = s.Count() })
                    .Where(w => w.sayi > 1)
                    .ToList();

                //var liste3 = ayniGunNobetTutanEczaneListesi.Where(w => w.Id > 0).ToList();
                //var listeHatali = ayniGunNobetTutanEczaneListesi.Where(w => w.Id == 0).ToList();
            }

            //var sonuclar = ayniGunNobetTutanEczaneListesi
            //    .GroupBy(s => new
            //    {
            //        s.Id,
            //        s.EczaneNobetGrupId1,
            //        s.EczaneNobetGrupId2,
            //        s.AyniGunNobetSayisi
            //    })
            //    .Select(g => new AyniGunTutulanNobet
            //    {
            //        Id = g.Key.Id,
            //        EczaneNobetGrupId1 = g.Key.EczaneNobetGrupId1,
            //        EczaneNobetGrupId2 = g.Key.EczaneNobetGrupId2,
            //        AyniGunNobetSayisi = g.Sum(x => x.AyniGunNobetSayisi),
            //        EnSonAyniGunNobetTakvimId = _takvimService.GetByTarih(g.Max(x => _takvimService.GetById(x.EnSonAyniGunNobetTakvimId).Tarih)).Id
            //    }).ToList(); 
            #endregion

            try
            {
                //_ayniGunTutulanNobetDal.Update(ayniGunNobetTutanEczaneListesi);
                _ayniGunTutulanNobetDal.UpdateAyniGunNobetSayisi(ayniGunNobetTutanEczaneListesi);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void IkiliEczaneIstatistiginiSifirla(int nobetUstGrupId)
        {
            var ikiliEczanelerTumu = GetListSifirdanFarkli(nobetUstGrupId);

            foreach (var ikiliEczane in ikiliEczanelerTumu)
            {
                ikiliEczane.EnSonAyniGunNobetTakvimId = 1;
                ikiliEczane.AyniGunNobetSayisi = 0;
                ikiliEczane.AyniGunNobetTutamayacaklariGunSayisi = 0;

               //_ayniGunTutulanNobetDal.Update(ikiliEczane);
            }

            _ayniGunTutulanNobetDal.UpdateTumKolonlar(ikiliEczanelerTumu);
        }

    }

    class AyniGunNobetIstatistik
    {
        public int NobetGrupId { get; internal set; }
        public string NobetGrupAdi { get; internal set; }
        public int EczaneNobetGrupId { get; internal set; }
        public string EczaneAdi { get; internal set; }
        public int AyniGunNobetSayisiEnBuyuk { get; internal set; }
        public int AyniGunNobetSayisiEnKucuk { get; internal set; }
    }
}
