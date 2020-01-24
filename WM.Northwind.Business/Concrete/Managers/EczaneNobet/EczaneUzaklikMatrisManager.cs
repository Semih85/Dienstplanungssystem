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

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class EczaneUzaklikMatrisManager : IEczaneUzaklikMatrisService
    {
        private IEczaneUzaklikMatrisDal _eczaneUzaklikMatrisDal;
        private IEczaneService _eczaneService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;

        public EczaneUzaklikMatrisManager(IEczaneUzaklikMatrisDal eczaneUzaklikMatrisDal,
            IEczaneService eczaneService,
            IEczaneNobetGrupService eczaneNobetGrupService)
        {
            _eczaneUzaklikMatrisDal = eczaneUzaklikMatrisDal;
            _eczaneService = eczaneService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneUzaklikMatrisId)
        {
            _eczaneUzaklikMatrisDal.Delete(new EczaneUzaklikMatris { Id = eczaneUzaklikMatrisId });
        }

        public EczaneUzaklikMatris GetById(int eczaneUzaklikMatrisId)
        {
            return _eczaneUzaklikMatrisDal.Get(x => x.Id == eczaneUzaklikMatrisId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneUzaklikMatris> GetList()
        {
            return _eczaneUzaklikMatrisDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneUzaklikMatris eczaneUzaklikMatris)
        {
            _eczaneUzaklikMatrisDal.Insert(eczaneUzaklikMatris);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneUzaklikMatris eczaneUzaklikMatris)
        {
            _eczaneUzaklikMatrisDal.Update(eczaneUzaklikMatris);
        }

        public EczaneUzaklikMatrisDetay GetDetayById(int eczaneUzaklikMatrisId)
        {
            return _eczaneUzaklikMatrisDal.GetDetay(x => x.Id == eczaneUzaklikMatrisId);
        }

        public EczaneUzaklikMatrisDetay GetDetay(int eczaneIdFrom, int eczaneIdTo)
        {
            return _eczaneUzaklikMatrisDal.GetDetay(x => x.EczaneIdFrom == eczaneIdFrom && x.EczaneIdTo == eczaneIdTo);
        }

        public EczaneUzaklikMatrisDetay GetDetay(int eczaneIdFrom, int eczaneIdTo, List<EczaneUzaklikMatrisDetay> eczaneUzaklikMatrisDetaylar)
        {
            return eczaneUzaklikMatrisDetaylar.SingleOrDefault(x => x.EczaneIdFrom == eczaneIdFrom && x.EczaneIdTo == eczaneIdTo) ?? new EczaneUzaklikMatrisDetay();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneUzaklikMatrisDetay> GetDetaylar()
        {
            return _eczaneUzaklikMatrisDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneUzaklikMatrisDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneUzaklikMatrisDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneUzaklikMatrisDetay> GetDetaylar(int nobetUstGrupId, int mesafeKriteri)
        {
            return _eczaneUzaklikMatrisDal
                .GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId && x.Mesafe <= mesafeKriteri);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneUzaklikMatrisDetay> GetDetaylarByEczaneId(int eczaneId)
        {
            var eczanelerArasiMesafeler = _eczaneUzaklikMatrisDal
                .GetDetayList(x => x.EczaneIdFrom == eczaneId || x.EczaneIdTo == eczaneId);

            var tumListe = EczanedenDigerEczanelereMesafeler(eczaneId, eczanelerArasiMesafeler);

            return tumListe;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneUzaklikMatrisDetay> GetDetaylarByEczaneId(int eczaneId, int mesafe)
        {
            var eczanelerArasiMesafeler = _eczaneUzaklikMatrisDal
                .GetDetayList(x => (x.EczaneIdFrom == eczaneId || x.EczaneIdTo == eczaneId)
                && (x.Mesafe <= mesafe) || mesafe == 0);

            var tumListe = EczanedenDigerEczanelereMesafeler(eczaneId, eczanelerArasiMesafeler);

            return tumListe;
        }

        private List<EczaneUzaklikMatrisDetay> EczanedenDigerEczanelereMesafeler(int eczaneId, List<EczaneUzaklikMatrisDetay> eczanelerArasiMesafeler)
        {
            var tumListe = new List<EczaneUzaklikMatrisDetay>();

            var eczane = _eczaneService.GetDetayById(eczaneId);

            foreach (var eczanelerArasiMesafe in eczanelerArasiMesafeler)
            {
                int eczaneIdFrom;
                int eczaneIdTo;

                string eczaneAdiFrom;
                string eczaneAdiTo;

                if (eczaneId == eczanelerArasiMesafe.EczaneIdFrom)
                {
                    eczaneIdFrom = eczanelerArasiMesafe.EczaneIdFrom;
                    eczaneAdiFrom = eczanelerArasiMesafe.EczaneAdiFrom;

                    eczaneIdTo = eczanelerArasiMesafe.EczaneIdTo;
                    eczaneAdiTo = eczanelerArasiMesafe.EczaneAdiTo;
                }
                else
                {
                    eczaneIdFrom = eczanelerArasiMesafe.EczaneIdTo;
                    eczaneAdiFrom = eczanelerArasiMesafe.EczaneAdiTo;

                    eczaneIdTo = eczanelerArasiMesafe.EczaneIdFrom;
                    eczaneAdiTo = eczanelerArasiMesafe.EczaneAdiFrom;
                }

                tumListe.Add(new EczaneUzaklikMatrisDetay
                {
                    EczaneIdFrom = eczaneIdFrom,
                    EczaneAdiFrom = eczaneAdiFrom,

                    EczaneAdiTo = eczaneAdiTo,
                    EczaneIdTo = eczaneIdTo,

                    Mesafe = eczanelerArasiMesafe.Mesafe,
                    NobetUstGrupId = eczanelerArasiMesafe.NobetUstGrupId
                });
            }

            return tumListe;
        }

        public List<EczaneGrupDetay> GetMesafeKriterineGoreKontrolEdilecekEczaneGruplar(int mesafeKriter, List<EczaneUzaklikMatrisDetay> eczaneMesafeler)
        {
            var mesafeKontrolEczaneler = new List<EczaneGrupDetay>();

            var nobetUstGrupId = eczaneMesafeler.Select(s => s.NobetUstGrupId).FirstOrDefault();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            foreach (var eczaneMesafe in eczaneMesafeler)
            {
                var eczaneNobetGrupFrom = eczaneNobetGruplar.FirstOrDefault(x => x.EczaneId == eczaneMesafe.EczaneIdFrom);
                var eczaneNobetGrupTo = eczaneNobetGruplar.FirstOrDefault(x => x.EczaneId == eczaneMesafe.EczaneIdTo);
                ;
                var eczaneGrupMaster = new EczaneGrupDetay
                {
                    EczaneGrupTanimId = eczaneMesafe.Id,
                    //EczaneId = eczaneMesafe.EczaneIdFrom,
                    ArdisikNobetSayisi = 0,
                    NobetUstGrupId = nobetUstGrupId,
                    EczaneGrupTanimAdi = $"{eczaneMesafe.Id}, {eczaneMesafe.EczaneAdiFrom}-{eczaneMesafe.EczaneAdiTo} {eczaneMesafe.Mesafe} &#8804; {mesafeKriter}",
                    EczaneGrupTanimTipAdi = $"Mesafeler aynı gün nöbet ",
                    EczaneGrupTanimTipId = 1,
                    NobetGrupId = 1,
                    //EczaneAdi = eczaneMesafe.EczaneAdiFrom,
                    NobetGrupAdi = "Mesafe",
                    //EczaneNobetGrupId = eczaneMesafe.EczaneIdFrom,
                    AyniGunNobetTutabilecekEczaneSayisi = 1,
                    //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                    NobetGrupGorevTipIdFrom = eczaneNobetGrupFrom.NobetGrupGorevTipId,
                    NobetGrupGorevTipIdTo = eczaneNobetGrupTo.NobetGrupGorevTipId
                };

                #region eczane from

                var eczaneFrom = eczaneGrupMaster.Clone();

                eczaneFrom.EczaneId = eczaneMesafe.EczaneIdFrom;
                eczaneFrom.EczaneAdi = eczaneMesafe.EczaneAdiFrom;

                mesafeKontrolEczaneler.Add(eczaneFrom);

                #endregion

                #region eczane to

                var eczaneTo = eczaneGrupMaster.Clone();

                eczaneTo.EczaneId = eczaneMesafe.EczaneIdTo;
                eczaneTo.EczaneAdi = eczaneMesafe.EczaneAdiTo;

                mesafeKontrolEczaneler.Add(eczaneTo);

                #endregion
            }

            return mesafeKontrolEczaneler;
        }

        public void CokluEkle(List<EczaneUzaklikMatrisDetay> eczaneUzaklikMatrisDetaylar)
        {
            _eczaneUzaklikMatrisDal.CokluEkle(eczaneUzaklikMatrisDetaylar);
        }
    }
}