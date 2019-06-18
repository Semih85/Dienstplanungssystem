using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.Aspects.PostSharp.TranstionAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.Abstract.Optimization;
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Enums;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Northwind.Business.Concrete.OptimizationManagers.Health.EczaneNobet
{
    public class EczaneNobetOptimizationManager : IEczaneNobetOptimizationService
    {
        #region ctor
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private ITakvimService _takvimService;
        private IEczaneNobetOptimization _eczaneNobetOptimization;
        private IEczaneNobetSonucAktifService _eczaneNobetSonucAktifService;
        private INobetGrupService _nobetGrupService;
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private INobetGrupKuralService _nobetGrupKuralService;
        private INobetGrupGunKuralService _nobetGrupGunKuralService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGrupTalepService _nobetGrupTalepService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        //private List<EczaneCiftGrup> UcAylikCiftGrupluEczanelerKumulatif;
        private List<int> sonUcAydaPazarGunuNobetTutanEczaneler;
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;

        public EczaneNobetOptimizationManager(IEczaneNobetSonucService eczaneNobetSonucService,
                                              IEczaneNobetOrtakService eczaneNobetOrtakService,
                                              ITakvimService takvimService,
                                              INobetGrupService nobetGrupService,
                                              IEczaneNobetMazeretService eczaneNobetMazeretService,
                                              IEczaneNobetGrupService eczaneNobetGrupService,
                                              IEczaneNobetSonucAktifService eczaneNobetSonucAktifService,
                                              IEczaneNobetOptimization eczaneNobetOptimization,
                                              IEczaneGrupService eczaneGrupService,
                                              IEczaneGrupTanimService eczaneGrupTanimService,
                                              IEczaneNobetIstekService eczaneNobetIstekService,
                                              INobetGrupKuralService nobetGrupKuralService,
                                              INobetGrupGunKuralService nobetGrupGunKuralService,
                                              INobetGrupGorevTipService nobetGrupGorevTipService,
                                              INobetGrupTalepService nobetGrupTalepService,
                                              INobetUstGrupKisitService nobetUstGrupKisitService,
                                              IAyniGunTutulanNobetService ayniGunTutulanNobetService
            )
        {
            _takvimService = takvimService;
            _nobetGrupService = nobetGrupService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _eczaneNobetOptimization = eczaneNobetOptimization;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneNobetSonucAktifService = eczaneNobetSonucAktifService;
            _eczaneGrupService = eczaneGrupService;
            _eczaneGrupTanimService = eczaneGrupTanimService;
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _nobetGrupKuralService = nobetGrupKuralService;
            _nobetGrupGunKuralService = nobetGrupGunKuralService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetGrupTalepService = nobetGrupTalepService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            //UcAylikCiftGrupluEczanelerKumulatif = new List<EczaneCiftGrup>();
            sonUcAydaPazarGunuNobetTutanEczaneler = new List<int>();
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
        }
        #endregion

        /// <summary>
        /// Eczane nöbet sonuc aktifteki tüm kayıtları alıp eczane nöbet sonuçlar tablosuna ekler.
        /// </summary>
        public void Kesinlestir(int nobetUstGrupId)
        {
            var eczaneNobetSonucAktifler = _eczaneNobetSonucAktifService.GetSonuclar2(nobetUstGrupId);

            foreach (var aktifSonuc in eczaneNobetSonucAktifler)
            {
                var insertEntity = new EczaneNobetSonuc
                {
                    EczaneNobetGrupId = aktifSonuc.EczaneNobetGrupId,
                    TakvimId = aktifSonuc.TakvimId,
                    NobetGorevTipId = aktifSonuc.NobetGorevTipId
                };
                _eczaneNobetSonucService.Insert(insertEntity);
            }
        }

        public void Kesinlestir(int nobetGrupId, int yil, int ay)
        {
            var eczaneNobetSonucAktifler = _eczaneNobetSonucAktifService.GetCozumler(nobetGrupId, yil, ay);
            _eczaneNobetSonucService.CokluEkle(eczaneNobetSonucAktifler);
        }

        public void Kesinlestir(int[] nobetGrupIdList, int yil, int ay)
        {
            var eczaneNobetSonucAktifler = _eczaneNobetSonucAktifService.GetCozumler(nobetGrupIdList, yil, ay);
            _eczaneNobetSonucService.CokluEkle(eczaneNobetSonucAktifler);
        }

        [TransactionScopeAspect]
        public void Kesinlestir(int[] nobetGrupIdList, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            var nobetGrup = _nobetGrupService.GetDetaylar(nobetGrupIdList.ToList()).FirstOrDefault();

            var eczaneNobetSonucAktifler = _eczaneNobetSonucAktifService.GetCozumler(nobetGrupIdList, baslangicTarihi, bitisTarihi);
            var sonuclar = _eczaneNobetSonucAktifService.GetSonuclar2(nobetGrup.NobetUstGrupId);

            _eczaneNobetSonucService.CokluEkle(eczaneNobetSonucAktifler);

            if (nobetGrup.NobetUstGrupId == 1    //alanya
                || nobetGrup.NobetUstGrupId == 3 //mersin
                || nobetGrup.NobetUstGrupId == 9 //çorum
                )
            {
                //var sonuclar = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(nobetGrupIdList);
                var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclar);
                var ayniGunNobetSayisiGrouped = _eczaneNobetOrtakService.AyniGunTutulanNobetSayisiniHesapla(ayniGunNobetTutanEczaneler);

                _ayniGunTutulanNobetService.AyniGunNobetSayisiniGuncelle(ayniGunNobetSayisiGrouped, AyniGunNobetEklemeTuru.Arttır);
            }
        }

    }
}
