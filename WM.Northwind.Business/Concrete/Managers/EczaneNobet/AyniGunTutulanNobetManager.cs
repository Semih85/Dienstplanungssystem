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

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class AyniGunTutulanNobetManager : IAyniGunTutulanNobetService
    {
        #region ctor
        private IAyniGunTutulanNobetDal _ayniGunTutulanNobetDal;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetGrupService _nobetGrupService;
        private IEczaneGrupService _eczaneGrupService;

        public AyniGunTutulanNobetManager(IAyniGunTutulanNobetDal ayniGunTutulanNobetDal,
            IEczaneNobetGrupService eczaneNobetGrupService,
            INobetGrupService nobetGrupService,
            IEczaneGrupService eczaneGrupService
            )
        {
            _ayniGunTutulanNobetDal = ayniGunTutulanNobetDal;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetGrupService = nobetGrupService;
            _eczaneGrupService = eczaneGrupService;
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
            return _ayniGunTutulanNobetDal.Get(x => x.EczaneNobetGrupId1 == eczaneNobetGrupId1 && x.EczaneNobetGrupId2 == eczaneNobetGrupId2);
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
                                                     || x.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobet> GetListSifirdanBuyukler(int nobetUstGrupId)
        {
            return _ayniGunTutulanNobetDal.GetList(x => (x.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId
                                                     || x.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId)
                                                     && x.AyniGunNobetSayisi > 0);
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
            return _ayniGunTutulanNobetDal.GetDetayList(x => x.NobetUstGrupId1 == nobetUstGrupId || x.NobetUstGrupId2 == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> GetDetaylar(List<int> nobetGrupIdList)
        {
            return _ayniGunTutulanNobetDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId1) || nobetGrupIdList.Contains(x.NobetGrupId2));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> IkiliEczaneleriOlustur(int nobetUstGrupId)
        {
            var nobetGruplar = _nobetGrupService.GetDetaylar(nobetUstGrupId);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            if (nobetUstGrupId == 5)
            {//
                var eczaneNobetGruplarSirali = eczaneNobetGruplar.OrderBy(o => o.Id).ToList();

                foreach (var eczaneNobetGrup1 in eczaneNobetGruplarSirali)
                {
                    var eczaneNobetGruplarSirali2 = eczaneNobetGruplarSirali.Where(w => w.Id > eczaneNobetGrup1.Id).ToList();

                    foreach (var eczaneNobetGrup2 in eczaneNobetGruplarSirali2)
                    {
                        var ikiliEczane = new AyniGunTutulanNobet
                        {
                            EczaneNobetGrupId1 = eczaneNobetGrup1.Id,
                            EczaneNobetGrupId2 = eczaneNobetGrup2.Id,
                            EnSonAyniGunNobetTakvimId = 1
                        };

                        try
                        {
                            _ayniGunTutulanNobetDal.Insert(ikiliEczane);
                        }
                        catch (DbUpdateException ex)
                        {
                            var hata = ex.InnerException.ToString();

                            string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                            var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                            if (dublicateRowHatasiMi)
                            {
                                throw new Exception($"<strong>{eczaneNobetGrup1.EczaneAdi} ve {eczaneNobetGrup2.EczaneAdi} eczane ikilisi zaten kayıtlıdır. Mükerrer kayır eklenemez...</strong>");
                            }

                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                }
            }
            else
            {
                var nobetGruplari = nobetGruplar
                    .Select(s => s.Id)
                    .OrderBy(o => o).ToList();


                foreach (var nobetGrup in nobetGruplari)
                {
                    var eczaneNobetGruplar1 = eczaneNobetGruplar.Where(w => w.NobetGrupId == nobetGrup).ToList();

                    foreach (var eczaneNobetGrup1 in eczaneNobetGruplar1)
                    {
                        var eczaneNobetGruplar2 = eczaneNobetGruplar.Where(w => w.NobetGrupId > eczaneNobetGrup1.NobetGrupId).ToList();

                        foreach (var eczaneNobetGrup2 in eczaneNobetGruplar2)
                        {
                            var ikiliEczane = new AyniGunTutulanNobet
                            {
                                EczaneNobetGrupId1 = eczaneNobetGrup1.Id,
                                EczaneNobetGrupId2 = eczaneNobetGrup2.Id,
                                EnSonAyniGunNobetTakvimId = 1
                            };

                            try
                            {
                                _ayniGunTutulanNobetDal.Insert(ikiliEczane);
                            }
                            catch (DbUpdateException ex)
                            {
                                var hata = ex.InnerException.ToString();

                                string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                                var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                                if (dublicateRowHatasiMi)
                                {
                                    throw new Exception($"<strong>{eczaneNobetGrup1.EczaneAdi} ve {eczaneNobetGrup2.EczaneAdi} eczane ikilisi zaten kayıtlıdır. Mükerrer kayır eklenemez...</strong>");
                                }

                                throw ex;
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }
                    }
                }
            }
            return GetDetaylar(nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunTutulanNobetDetay> IkiliEczaneleriOlustur(List<EczaneNobetGrupDetay> eczaneNobetGruplar)
        {
            var nobetUstGrupId = eczaneNobetGruplar.Select(s => s.NobetUstGrupId).Distinct().FirstOrDefault();
            var nobetGrupId = eczaneNobetGruplar.Select(s => s.NobetGrupId).Distinct().FirstOrDefault();

            var nobetGruplarTumu = _nobetGrupService.GetDetaylar(nobetUstGrupId);

            var eczaneNobetGruplarDigerleri = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId)
                .Where(w => !eczaneNobetGruplar.Select(s => s.Id).Contains(w.Id));

            var digerNobetGruplari = nobetGruplarTumu.Where(w => w.Id != nobetGrupId).ToList();

            foreach (var eczaneNobetGrup1 in eczaneNobetGruplar)
            {
                foreach (var nobetGrup in digerNobetGruplari)
                {
                    var eczaneNobetGruplar2 = eczaneNobetGruplarDigerleri.Where(w => w.NobetGrupId == nobetGrup.Id).ToList();

                    //var eczaneNobetGruplar2 = eczaneNobetGruplar.Where(w => w.NobetGrupId > eczaneNobetGrup1.NobetGrupId).ToList();

                    foreach (var eczaneNobetGrup2 in eczaneNobetGruplar2)
                    {
                        var ikiliEczane = new AyniGunTutulanNobet
                        {
                            EczaneNobetGrupId1 = eczaneNobetGrup1.Id,
                            EczaneNobetGrupId2 = eczaneNobetGrup2.Id,
                            EnSonAyniGunNobetTakvimId = 1
                        };

                        try
                        {
                            _ayniGunTutulanNobetDal.Insert(ikiliEczane);
                        }
                        catch (DbUpdateException ex)
                        {
                            var hata = ex.InnerException.ToString();

                            string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                            var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                            if (dublicateRowHatasiMi)
                            {
                                throw new Exception($"<strong>{eczaneNobetGrup1.EczaneAdi} ve {eczaneNobetGrup2.EczaneAdi} eczane ikilisi zaten kayıtlıdır. Mükerrer kayır eklenemez...</strong>");
                            }

                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }

            return GetDetaylar(nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetArasinda2FarkOlanIkiliEczaneleri(List<EczaneNobetGrupDetay> eczaneNobetGruplar, int nobetUstGrupId, int nobetFarki = 2)
        {
            var ikiliEczanelerTumu = GetDetaylar(nobetUstGrupId);

            //var ikiFarkliIkiliEczaneler = new List<AyniGunTutulanNobetDetay>();
            var eczaneGruplar = new List<EczaneGrupDetay>();

            foreach (var eczaneNobetGrup in eczaneNobetGruplar)
            {
                var bakilanEczaneninIkilileri = ikiliEczanelerTumu
                    .Where(w => w.EczaneNobetGrupId1 == eczaneNobetGrup.Id || w.EczaneNobetGrupId2 == eczaneNobetGrup.Id).ToList();

                var bakilanEczane = new AyniGunNobetIstatistik
                {
                    NobetGrupId = eczaneNobetGrup.NobetGrupId,
                    NobetGrupAdi = eczaneNobetGrup.NobetGrupAdi,
                    EczaneNobetGrupId = eczaneNobetGrup.Id,
                    EczaneAdi = eczaneNobetGrup.EczaneAdi,
                    AyniGunNobetSayisiEnBuyuk = bakilanEczaneninIkilileri.Max(x => x.AyniGunNobetSayisi),
                    AyniGunNobetSayisiEnKucuk = bakilanEczaneninIkilileri.Min(x => x.AyniGunNobetSayisi)
                };

                var takipEdilecekEczaneler = bakilanEczaneninIkilileri
                    .Where(w => bakilanEczane.AyniGunNobetSayisiEnBuyuk - bakilanEczane.AyniGunNobetSayisiEnKucuk >= nobetFarki
                             && bakilanEczane.AyniGunNobetSayisiEnBuyuk == w.AyniGunNobetSayisi).ToList();

                var eczaneGrupTanimId = 10000 + eczaneNobetGrup.Id;

                if (takipEdilecekEczaneler.Count > 0)
                {
                    //ikiFarkliIkiliEczaneler.AddRange(takipEdilecekEczaneler);
                    eczaneGruplar.Add(new EczaneGrupDetay
                    {
                        EczaneGrupTanimId = eczaneGrupTanimId,
                        EczaneId = eczaneNobetGrup.EczaneId,
                        ArdisikNobetSayisi = 0,
                        NobetUstGrupId = nobetUstGrupId,
                        EczaneGrupTanimAdi = $"{bakilanEczane.AyniGunNobetSayisiEnBuyuk} farklı olan aynı gün nöbetler",
                        EczaneGrupTanimTipAdi = "Tüm eczanelerle aynı gün nöbet",
                        EczaneGrupTanimTipId = -10, //-2,
                        NobetGrupId = eczaneNobetGrup.NobetGrupId,
                        EczaneAdi = eczaneNobetGrup.EczaneAdi,
                        NobetGrupAdi = eczaneNobetGrup.NobetGrupAdi,
                        EczaneNobetGrupId = eczaneNobetGrup.Id,
                        AyniGunNobetTutabilecekEczaneSayisi = 1
                        //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                    });

                    var kontrol = false;

                    if (kontrol && bakilanEczane.EczaneAdi == "ÇAM")
                    {
                    }

                    var bakilanEczaneninAktifEsGruplari = _eczaneGrupService.GetDetaylarAktif(eczaneNobetGrup.Id);
                    var bakilanEzaneninEsGruplarindakiEczaneler = _eczaneGrupService.GetDetaylarByEczaneGrupTanimId(bakilanEczaneninAktifEsGruplari.Select(s => s.EczaneGrupTanimId).ToList()).ToList();

                    //var takipEdilecekEsGruptaOlmayanEczaneler = takipEdilecekEczaneler.Where(w=> bakilanEzaneninEsGruplarindakiEczaneler.Select(s => s.EczaneNobetGrupId).Contains(w.EczaneNobetGrupId1))
                    foreach (var takipEdilecekEczane in takipEdilecekEczaneler)
                    {
                        var nobetGrupId = (eczaneNobetGrup.Id == takipEdilecekEczane.EczaneNobetGrupId1 ? takipEdilecekEczane.NobetGrupId2 : takipEdilecekEczane.NobetGrupId1);
                        var eczaneAdi = (eczaneNobetGrup.Id == takipEdilecekEczane.EczaneNobetGrupId1 ? takipEdilecekEczane.EczaneAdi2 : takipEdilecekEczane.EczaneAdi1);
                        var nobetGrupAdi = (eczaneNobetGrup.Id == takipEdilecekEczane.EczaneNobetGrupId1 ? takipEdilecekEczane.NobetGrupAdi2 : takipEdilecekEczane.NobetGrupAdi1);
                        var eczaneNobetGrupId = (eczaneNobetGrup.Id == takipEdilecekEczane.EczaneNobetGrupId1 ? takipEdilecekEczane.EczaneNobetGrupId2 : takipEdilecekEczane.EczaneNobetGrupId1);

                        if (kontrol && eczaneAdi == "ABANT")
                        {
                        }

                        if (bakilanEzaneninEsGruplarindakiEczaneler.Select(s => s.EczaneNobetGrupId).Contains(eczaneNobetGrupId))
                            continue;

                        eczaneGruplar.Add(new EczaneGrupDetay
                        {
                            EczaneGrupTanimId = eczaneGrupTanimId,
                            EczaneId = 0,//takipEdilecekEczane.Id,
                            ArdisikNobetSayisi = 0,
                            NobetUstGrupId = nobetUstGrupId,
                            EczaneGrupTanimAdi = $"{bakilanEczane.AyniGunNobetSayisiEnBuyuk} farklı olan aynı gün nöbetler",
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

            return eczaneGruplar;
        }

        public void AyniGunNobetTutanlariTabloyaEkle(List<AyniGunNobetTutanEczane> ayniGunNobetTutanEczaneler)
        {
            foreach (var ayniGunNobetTutanEczane in ayniGunNobetTutanEczaneler)
            {
                var ikiliEczane = GetByIkiliEczaneler(ayniGunNobetTutanEczane.G1EczaneNobetGrupId, ayniGunNobetTutanEczane.G2EczaneNobetGrupId);

                if (ikiliEczane == null)
                {
                    ikiliEczane = IkiliEczaneleriOlustur(ayniGunNobetTutanEczane.G1EczaneNobetGrupId, ayniGunNobetTutanEczane.G2EczaneNobetGrupId);
                }

                ikiliEczane.AyniGunNobetSayisi = ayniGunNobetTutanEczane.AyniGunNobetSayisi;
                ikiliEczane.EnSonAyniGunNobetTakvimId = ayniGunNobetTutanEczane.TakvimId;

                if (ayniGunNobetTutanEczane.G1EczaneNobetGrupId > 0 && ayniGunNobetTutanEczane.G2EczaneNobetGrupId > 0)
                {
                    try
                    {
                        _ayniGunTutulanNobetDal.Update(ikiliEczane);
                    }
                    catch (Exception)
                    {
                        throw new Exception($"{ayniGunNobetTutanEczane.G1Eczane} ve {ayniGunNobetTutanEczane.G2Eczane} eczanesi tabloda bulunmamaktadır!");
                    }
                }
            }
        }

        public void IkiliEczaneIstatistiginiSifirla(int nobetUstGrupId)
        {
            var ikiliEczanelerTumu = GetListSifirdanBuyukler(nobetUstGrupId);

            foreach (var ikiliEczane in ikiliEczanelerTumu)
            {
                ikiliEczane.EnSonAyniGunNobetTakvimId = 1;
                ikiliEczane.AyniGunNobetSayisi = 0;
                ikiliEczane.AyniGunNobetTutamayacaklariGunSayisi = 0;

                _ayniGunTutulanNobetDal.Update(ikiliEczane);
            }
        }

        public AyniGunTutulanNobet IkiliEczaneleriOlustur(int eczaneNobetGrupId1, int eczaneNobetGrupId2)
        {
            var ikiliEczane = new AyniGunTutulanNobet();

            if (eczaneNobetGrupId1 > 0 && eczaneNobetGrupId2 > 0)
            {
                ikiliEczane.EczaneNobetGrupId1 = eczaneNobetGrupId1;
                ikiliEczane.EczaneNobetGrupId2 = eczaneNobetGrupId2;
                ikiliEczane.EnSonAyniGunNobetTakvimId = 1;
                _ayniGunTutulanNobetDal.Insert(ikiliEczane);
            };
            return ikiliEczane;
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
