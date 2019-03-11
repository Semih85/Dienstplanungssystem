using System;
using System.Collections.Generic;
using System.Linq;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public partial class EczaneNobetOrtakManager : IEczaneNobetOrtakService
    {
        #region ctor

        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneGrupService _eczaneGrupService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private INobetGrupGorevTipKisitService _nobetGrupGorevTipKisitService;

        public EczaneNobetOrtakManager(IEczaneNobetGrupService eczaneNobetGrupService,
            IEczaneGrupService eczaneGrupService,
            INobetUstGrupKisitService nobetUstGrupKisitService,
            INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
            INobetGrupGorevTipKisitService nobetGrupGorevTipKisitService)
        {
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneGrupService = eczaneGrupService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
            _nobetGrupGorevTipKisitService = nobetGrupGorevTipKisitService;
        }

        #endregion

        #region aynı gün nöbet tutma istatistiği

        public List<NobetSonucGrupAnaliz> GetSonuclarEczaneGrupAnalizUcGrup(List<EczaneNobetSonucListe> pSonuclar)
        {
            var sonuclar = pSonuclar.Select(s => s.EczaneId).Distinct().ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar()
                .Where(w => sonuclar.Contains(w.EczaneId));

            var eczaneNobetSonucDetaylar = pSonuclar
                                            .Select(s => new
                                            {
                                                s.TakvimId,
                                                s.EczaneNobetGrupId,
                                                s.EczaneId,
                                                s.NobetGrupId,
                                                s.NobetGunKuralId
                                            });

            var gruplar = pSonuclar.Select(s => s.NobetGrupId).Distinct().OrderBy(o => o).ToArray();

            var grup1 = gruplar[0];
            var grup2 = gruplar[1];
            var grup3 = gruplar[2];

            var sonuclar0 = (from t in eczaneNobetSonucDetaylar
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = t.TakvimId,
                                 G1EczaneId = eczaneNobetGruplar
                                                .Where(g1 => g1.Id == t.NobetGrupId
                                                          && g1.NobetGrupId == grup1)
                                                .Select(s => s.EczaneId).FirstOrDefault(),
                                 G2EczaneId = eczaneNobetGruplar
                                                .Where(g1 => g1.Id == t.NobetGrupId
                                                          && g1.NobetGrupId == grup2)
                                                .Select(s => s.EczaneId).FirstOrDefault(),
                                 G3EczaneId = eczaneNobetGruplar
                                                .Where(g1 => g1.Id == t.NobetGrupId
                                                          && g1.NobetGrupId == grup3)
                                                .Select(s => s.EczaneId).FirstOrDefault()
                             }).ToList();


            var sonuclar2 = (from t in sonuclar0
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = t.TakvimId,
                                 G1EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == grup1)
                                       .Select(s => s.EczaneId).FirstOrDefault(),
                                 G2EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == grup2)
                                       .Select(s => s.EczaneId).FirstOrDefault(),
                                 G3EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == grup3)
                                       .Select(s => s.EczaneId).FirstOrDefault()
                             }).OrderBy(f => f.TakvimId).ToList();


            var sonuclar3 = (from t in sonuclar2
                             group t by t.TakvimId into g
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = g.Key,
                                 G1EczaneId = g.Select(t => t.G1EczaneId).FirstOrDefault(),
                                 G2EczaneId = g.Select(t => t.G2EczaneId).FirstOrDefault(),
                                 G3EczaneId = g.Select(t => t.G3EczaneId).FirstOrDefault()
                             }).ToList();

            return sonuclar3;
        }
        public List<NobetSonucGrupAnaliz> GetSonuclarEczaneGrupAnalizUcGrup(List<EczaneNobetSonucListe2> pSonuclar)
        {
            var sonuclar = pSonuclar.Select(s => s.EczaneId).Distinct().ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar()
                .Where(w => sonuclar.Contains(w.EczaneId));

            var eczaneNobetSonucDetaylar = pSonuclar
                                            .Select(s => new
                                            {
                                                s.TakvimId,
                                                s.EczaneNobetGrupId,
                                                s.EczaneId,
                                                s.NobetGrupId,
                                                s.NobetGunKuralId
                                            });

            var gruplar = pSonuclar.Select(s => s.NobetGrupId).Distinct().OrderBy(o => o).ToArray();
            var grup1 = gruplar[0];
            var grup2 = gruplar[1];
            var grup3 = gruplar[2];

            var sonuclar0 = (from t in eczaneNobetSonucDetaylar
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = t.TakvimId,
                                 G1EczaneId = eczaneNobetGruplar
                                                .Where(g1 => g1.Id == t.NobetGrupId
                                                          && g1.NobetGrupId == grup1)
                                                .Select(s => s.EczaneId).FirstOrDefault(),
                                 G2EczaneId = eczaneNobetGruplar
                                                .Where(g1 => g1.Id == t.NobetGrupId
                                                          && g1.NobetGrupId == grup2)
                                                .Select(s => s.EczaneId).FirstOrDefault(),
                                 G3EczaneId = eczaneNobetGruplar
                                                .Where(g1 => g1.Id == t.NobetGrupId
                                                          && g1.NobetGrupId == grup3)
                                                .Select(s => s.EczaneId).FirstOrDefault()
                             }).ToList();


            var sonuclar2 = (from t in sonuclar0
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = t.TakvimId,
                                 G1EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == grup1)
                                       .Select(s => s.EczaneId).FirstOrDefault(),
                                 G2EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == grup2)
                                       .Select(s => s.EczaneId).FirstOrDefault(),
                                 G3EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == grup3)
                                       .Select(s => s.EczaneId).FirstOrDefault()
                             }).OrderBy(f => f.TakvimId).ToList();


            var sonuclar3 = (from t in sonuclar2
                             group t by t.TakvimId into g
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = g.Key,
                                 G1EczaneId = g.Select(t => t.G1EczaneId).FirstOrDefault(),
                                 G2EczaneId = g.Select(t => t.G2EczaneId).FirstOrDefault(),
                                 G3EczaneId = g.Select(t => t.G3EczaneId).FirstOrDefault()
                             }).ToList();

            return sonuclar3;
        }
        public List<NobetSonucGrupAnaliz> GetSonuclarEczaneGrupAnalizIkiGrup(List<EczaneNobetSonucListe> pSonuclar)
        {
            var nobetGruplar = pSonuclar.Select(s => s.NobetGrupId).Distinct().ToList();

            var aktifSonuclar = pSonuclar.Select(s => s.EczaneId).Distinct().ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar()
                .Where(w => aktifSonuclar.Contains(w.EczaneId));

            var eczaneNobetSonucDetaylar = pSonuclar
                                            .Select(s => new
                                            {
                                                s.TakvimId,
                                                s.EczaneNobetGrupId,
                                                s.EczaneId,
                                                s.NobetGrupId,
                                                s.NobetGunKuralId
                                            });

            var sonuclar = (from t in eczaneNobetSonucDetaylar
                            select new NobetSonucGrupAnaliz
                            {
                                TakvimId = t.TakvimId,
                                G1EczaneId = eczaneNobetGruplar
                                               .Where(g1 => g1.Id == t.NobetGrupId
                                                         && g1.NobetGrupId == nobetGruplar.FirstOrDefault())
                                               .Select(s => s.EczaneId).FirstOrDefault(),
                                G2EczaneId = eczaneNobetGruplar
                                               .Where(g1 => g1.Id == t.NobetGrupId
                                                         && g1.NobetGrupId == nobetGruplar.LastOrDefault())
                                               .Select(s => s.EczaneId).FirstOrDefault()
                            }).ToList();


            var sonuclar2 = (from t in sonuclar
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = t.TakvimId,
                                 G1EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == nobetGruplar.FirstOrDefault())
                                       .Select(s => s.EczaneId).FirstOrDefault(),
                                 G2EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == nobetGruplar.LastOrDefault())
                                       .Select(s => s.EczaneId).FirstOrDefault()
                             }).OrderBy(f => f.TakvimId).ToList();


            var sonuclar3 = (from t in sonuclar2
                             group t by t.TakvimId into g
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = g.Key,
                                 G1EczaneId = g.Select(t => t.G1EczaneId).FirstOrDefault(),
                                 G2EczaneId = g.Select(t => t.G2EczaneId).FirstOrDefault()
                             }).ToList();

            return sonuclar3;
        }
        public List<NobetSonucGrupAnaliz> GetSonuclarEczaneGrupAnalizIkiGrup(List<EczaneNobetSonucListe2> pSonuclar)
        {
            var nobetGruplar = pSonuclar.Select(s => s.NobetGrupId).Distinct().ToList();

            var aktifSonuclar = pSonuclar.Select(s => s.EczaneId).Distinct().ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar()
                .Where(w => aktifSonuclar.Contains(w.EczaneId));

            var eczaneNobetSonucDetaylar = pSonuclar
                                            .Select(s => new
                                            {
                                                s.TakvimId,
                                                s.EczaneNobetGrupId,
                                                s.EczaneId,
                                                s.NobetGrupId,
                                                s.NobetGunKuralId
                                            });

            var sonuclar = (from t in eczaneNobetSonucDetaylar
                            select new NobetSonucGrupAnaliz
                            {
                                TakvimId = t.TakvimId,
                                G1EczaneId = eczaneNobetGruplar
                                               .Where(g1 => g1.Id == t.NobetGrupId
                                                         && g1.NobetGrupId == nobetGruplar.FirstOrDefault())
                                               .Select(s => s.EczaneId).FirstOrDefault(),
                                G2EczaneId = eczaneNobetGruplar
                                               .Where(g1 => g1.Id == t.NobetGrupId
                                                         && g1.NobetGrupId == nobetGruplar.LastOrDefault())
                                               .Select(s => s.EczaneId).FirstOrDefault()
                            }).ToList();


            var sonuclar2 = (from t in sonuclar
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = t.TakvimId,
                                 G1EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == nobetGruplar.FirstOrDefault())
                                       .Select(s => s.EczaneId).FirstOrDefault(),
                                 G2EczaneId = eczaneNobetSonucDetaylar
                                       .Where(g1 => g1.TakvimId == t.TakvimId
                                                 && g1.NobetGrupId == nobetGruplar.LastOrDefault())
                                       .Select(s => s.EczaneId).FirstOrDefault()
                             }).OrderBy(f => f.TakvimId).ToList();


            var sonuclar3 = (from t in sonuclar2
                             group t by t.TakvimId into g
                             select new NobetSonucGrupAnaliz
                             {
                                 TakvimId = g.Key,
                                 G1EczaneId = g.Select(t => t.G1EczaneId).FirstOrDefault(),
                                 G2EczaneId = g.Select(t => t.G2EczaneId).FirstOrDefault()
                             }).ToList();

            return sonuclar3;
        }

        public List<EczaneCiftGrupEsli> GetCiftGrupluEczanelerYanYanaUcGrup(List<EczaneNobetSonucListe> pSonuclar, int ayniGuneDenkGelenNobetSayisi)
        {
            var nobetSonucGrupListe = GetSonuclarEczaneGrupAnalizUcGrup(pSonuclar);
            var nobetSonucGrupListe2 = nobetSonucGrupListe;
            var nobetSonucGrupListe3 = nobetSonucGrupListe;

            //var ayniGuneDenkGelenNobetSayisi = 2;

            var liste = (from s in nobetSonucGrupListe
                         group s by new
                         {
                             s.G1EczaneId,
                             s.G2EczaneId
                         } into g
                         where g.Count() >= ayniGuneDenkGelenNobetSayisi
                         select new EczaneCiftGrupEsli
                         {
                             G1EczaneId = g.Key.G1EczaneId,
                             G2EczaneId = g.Key.G2EczaneId,
                             AyniGunNobetTutmaSayisi = g.Count()
                         }).Union
                    (from s in nobetSonucGrupListe2
                     group s by new
                     {
                         s.G2EczaneId,
                         s.G3EczaneId
                     } into g
                     where g.Count() >= ayniGuneDenkGelenNobetSayisi
                     select new EczaneCiftGrupEsli
                     {
                         G1EczaneId = g.Key.G2EczaneId,
                         G2EczaneId = g.Key.G3EczaneId,
                         AyniGunNobetTutmaSayisi = g.Count()
                     }).Union
                    (from s in nobetSonucGrupListe3
                     group s by new
                     {
                         s.G1EczaneId,
                         s.G3EczaneId
                     } into g
                     where g.Count() >= ayniGuneDenkGelenNobetSayisi
                     select new EczaneCiftGrupEsli
                     {
                         G1EczaneId = g.Key.G1EczaneId,
                         G2EczaneId = g.Key.G3EczaneId,
                         AyniGunNobetTutmaSayisi = g.Count()
                     }).ToList();

            return liste;
        }
        public List<EczaneCiftGrupEsli> GetCiftGrupluEczanelerYanYanaUcGrup(List<EczaneNobetSonucListe2> pSonuclar, int ayniGuneDenkGelenNobetSayisi)
        {
            var nobetSonucGrupListe = GetSonuclarEczaneGrupAnalizUcGrup(pSonuclar);
            var nobetSonucGrupListe2 = nobetSonucGrupListe;
            var nobetSonucGrupListe3 = nobetSonucGrupListe;

            //var ayniGuneDenkGelenNobetSayisi = 2;

            var liste = (from s in nobetSonucGrupListe
                         group s by new
                         {
                             s.G1EczaneId,
                             s.G2EczaneId
                         } into g
                         where g.Count() >= ayniGuneDenkGelenNobetSayisi
                         select new EczaneCiftGrupEsli
                         {
                             G1EczaneId = g.Key.G1EczaneId,
                             G2EczaneId = g.Key.G2EczaneId,
                             AyniGunNobetTutmaSayisi = g.Count()
                         }).Union
                    (from s in nobetSonucGrupListe2
                     group s by new
                     {
                         s.G2EczaneId,
                         s.G3EczaneId
                     } into g
                     where g.Count() >= ayniGuneDenkGelenNobetSayisi
                     select new EczaneCiftGrupEsli
                     {
                         G1EczaneId = g.Key.G2EczaneId,
                         G2EczaneId = g.Key.G3EczaneId,
                         AyniGunNobetTutmaSayisi = g.Count()
                     }).Union
                    (from s in nobetSonucGrupListe3
                     group s by new
                     {
                         s.G1EczaneId,
                         s.G3EczaneId
                     } into g
                     where g.Count() >= ayniGuneDenkGelenNobetSayisi
                     select new EczaneCiftGrupEsli
                     {
                         G1EczaneId = g.Key.G1EczaneId,
                         G2EczaneId = g.Key.G3EczaneId,
                         AyniGunNobetTutmaSayisi = g.Count()
                     }).ToList();

            return liste;
        }
        public List<EczaneCiftGrupEsli> GetCiftGrupluEczanelerYanYanaIkiGrup(List<EczaneNobetSonucListe> pSonuclar, int ayniGuneDenkGelenNobetSayisi)
        {
            var nobetSonucGrupListe = GetSonuclarEczaneGrupAnalizIkiGrup(pSonuclar);

            //var ayniGuneDenkGelenNobetSayisi = 2;

            var liste = (from s in nobetSonucGrupListe
                         group s by new
                         {
                             s.G1EczaneId,
                             s.G2EczaneId
                         } into g
                         where g.Count() >= ayniGuneDenkGelenNobetSayisi
                         select new EczaneCiftGrupEsli
                         {
                             G1EczaneId = g.Key.G1EczaneId,
                             G2EczaneId = g.Key.G2EczaneId,
                             AyniGunNobetTutmaSayisi = g.Count()
                         }).ToList();

            return liste;
        }
        public List<EczaneCiftGrupEsli> GetCiftGrupluEczanelerYanYanaIkiGrup(List<EczaneNobetSonucListe2> pSonuclar, int ayniGuneDenkGelenNobetSayisi)
        {
            var nobetSonucGrupListe = GetSonuclarEczaneGrupAnalizIkiGrup(pSonuclar);

            //var ayniGuneDenkGelenNobetSayisi = 2;

            var liste = (from s in nobetSonucGrupListe
                         group s by new
                         {
                             s.G1EczaneId,
                             s.G2EczaneId
                         } into g
                         where g.Count() >= ayniGuneDenkGelenNobetSayisi
                         select new EczaneCiftGrupEsli
                         {
                             G1EczaneId = g.Key.G1EczaneId,
                             G2EczaneId = g.Key.G2EczaneId,
                             AyniGunNobetTutmaSayisi = g.Count()
                         }).ToList();

            return liste;
        }

        public List<EczaneCiftGrupEsli> GrupSayisinaGoreAnalizeGonder(List<EczaneNobetSonucListe> pSonuclar, int ayniGuneDenkGelenNobetSayisi)
        {
            var grupSayisi = pSonuclar.Select(s => s.NobetGrupId).Distinct().Count();

            var ciftGrupOlanEczaneler = new List<EczaneCiftGrupEsli>();

            if (grupSayisi == 2)
            {
                ciftGrupOlanEczaneler = GetCiftGrupluEczanelerYanYanaIkiGrup(pSonuclar, ayniGuneDenkGelenNobetSayisi);
            }
            else
            {
                ciftGrupOlanEczaneler = GetCiftGrupluEczanelerYanYanaUcGrup(pSonuclar, ayniGuneDenkGelenNobetSayisi);
            }

            return ciftGrupOlanEczaneler;
        }
        public List<EczaneCiftGrupEsli> GrupSayisinaGoreAnalizeGonder(List<EczaneNobetSonucListe2> pSonuclar, int ayniGuneDenkGelenNobetSayisi)
        {
            var grupSayisi = pSonuclar.Select(s => s.NobetGrupId).Distinct().Count();

            var ciftGrupOlanEczaneler = new List<EczaneCiftGrupEsli>();

            if (grupSayisi == 2)
            {
                ciftGrupOlanEczaneler = GetCiftGrupluEczanelerYanYanaIkiGrup(pSonuclar, ayniGuneDenkGelenNobetSayisi);
            }
            else
            {
                ciftGrupOlanEczaneler = GetCiftGrupluEczanelerYanYanaUcGrup(pSonuclar, ayniGuneDenkGelenNobetSayisi);
            }

            return ciftGrupOlanEczaneler;
        }
        public List<EczaneCiftGrup> GetCiftGrupluEczaneler(List<EczaneNobetSonucListe> pSonuclar, int ayniGuneDenkGelenNobetSayisi)
        {//grup id'si ile birlikte alt alta getirir.
            var ciftGrupOlanEczaneler = GrupSayisinaGoreAnalizeGonder(pSonuclar, ayniGuneDenkGelenNobetSayisi);

            var ciftGrupluEczaneler = new List<EczaneCiftGrup>();

            //Bu değer sabit 2'dir. Çünkü bu sayı ay içinde iki kez aynı gün nöbet tutmayı ifade ediyor.
            //var ayniGuneDenkGelenNobetSayisi = 2;
            int i = 0;

            foreach (var ciftGrup in ciftGrupOlanEczaneler)
            {
                i++;
                ciftGrupluEczaneler
                    .Add(new EczaneCiftGrup
                    {
                        Id = i,
                        EczaneId = ciftGrup.G1EczaneId,
                        BirlikteNobetTutmaSayisi = ciftGrup.AyniGunNobetTutmaSayisi
                    });
                ciftGrupluEczaneler
                    .Add(new EczaneCiftGrup
                    {
                        Id = i,
                        EczaneId = ciftGrup.G2EczaneId,
                        BirlikteNobetTutmaSayisi = ciftGrup.AyniGunNobetTutmaSayisi
                    });
            }

            var optimizeEdilecekCiftGrupFrekans = ciftGrupluEczaneler
                .Where(q => q.BirlikteNobetTutmaSayisi >= ayniGuneDenkGelenNobetSayisi).ToList();

            return optimizeEdilecekCiftGrupFrekans;
        }
        public List<EczaneCiftGrup> GetCiftGrupluEczaneler(List<EczaneNobetSonucListe2> pSonuclar, int ayniGuneDenkGelenNobetSayisi)
        {//grup id'si ile birlikte alt alta getirir.
            var ciftGrupOlanEczaneler = GrupSayisinaGoreAnalizeGonder(pSonuclar, ayniGuneDenkGelenNobetSayisi);

            var ciftGrupluEczaneler = new List<EczaneCiftGrup>();

            //Bu değer sabit 2'dir. Çünkü bu sayı ay içinde iki kez aynı gün nöbet tutmayı ifade ediyor.
            //var ayniGuneDenkGelenNobetSayisi = 2;
            int i = 0;

            foreach (var ciftGrup in ciftGrupOlanEczaneler)
            {
                i++;
                ciftGrupluEczaneler
                    .Add(new EczaneCiftGrup
                    {
                        Id = i,
                        EczaneId = ciftGrup.G1EczaneId,
                        BirlikteNobetTutmaSayisi = ciftGrup.AyniGunNobetTutmaSayisi
                    });
                ciftGrupluEczaneler
                    .Add(new EczaneCiftGrup
                    {
                        Id = i,
                        EczaneId = ciftGrup.G2EczaneId,
                        BirlikteNobetTutmaSayisi = ciftGrup.AyniGunNobetTutmaSayisi
                    });
            }

            var optimizeEdilecekCiftGrupFrekans = ciftGrupluEczaneler
                .Where(q => q.BirlikteNobetTutmaSayisi >= ayniGuneDenkGelenNobetSayisi).ToList();

            return optimizeEdilecekCiftGrupFrekans;
        }

        /// <summary>
        /// Nöbet Grubu Bazındaki Arz Değeri Eşleştir
        /// </summary>
        /// <param name="hedef"></param>
        /// <param name="maxArz"></param>
        /// <param name="minArz"></param>
        /// <param name="gunDeger"></param>
        public void GetEczaneGunHedef(EczaneNobetIstatistik hedef, out double maxArz, out double minArz, int gunDeger)
        {
            switch (gunDeger)
            {
                case 1:
                    maxArz = hedef.Pazar;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;

                    break;
                case 2:
                    maxArz = hedef.Pazartesi;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 3:
                    maxArz = hedef.Sali;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 4:
                    maxArz = hedef.Carsamba;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 5:
                    maxArz = hedef.Persembe;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 6:
                    maxArz = hedef.Cuma;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 7:
                    maxArz = hedef.Cumartesi;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 8:
                    maxArz = hedef.DiniBayram;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 9:
                    maxArz = hedef.MilliBayram;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                default:
                    maxArz = hedef.Toplam;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
            }
        }

        #endregion

        public List<EczaneNobetAlacakVerecek> EczaneNobetAlacakVerecekHesapla(NobetUstGrupDetay nobetUstGrup,
               List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumu,
               List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumuPlanlanan,
               List<NobetUstGrupGunGrupDetay> nobetUstGrupGunGruplar)
        {
            var anahtarListeTumEczanelerHepsi = new List<AnahtarListe>();
            var eczaneNobetAlacakVerecek = new List<EczaneNobetAlacakVerecek>();

            var eczaneGercek = eczaneNobetGrupGunKuralIstatistikYatayTumu.Where(w => w.EczaneAdi == "CERENSU").ToList();
            var eczanePlanlanan = eczaneNobetGrupGunKuralIstatistikYatayTumuPlanlanan.Where(w => w.EczaneAdi == "CERENSU").ToList();

            foreach (var gunGrup in nobetUstGrupGunGruplar)
            {
                var eczaneNobetAlacakVerecekGunGrup = (from s in eczaneNobetGrupGunKuralIstatistikYatayTumu
                                                       from b in eczaneNobetGrupGunKuralIstatistikYatayTumuPlanlanan
                                                       where s.EczaneNobetGrupId == b.EczaneNobetGrupId
                                                       //&& (gunGrup.GunGrupAdi == "Pazar"
                                                       //    ? s.NobetSayisiPazar == b.NobetSayisiPazar
                                                       //    : gunGrup.GunGrupAdi == "Arife"
                                                       //    ? s.NobetSayisiArife == b.NobetSayisiArife
                                                       //    : gunGrup.GunGrupAdi == "Bayram"
                                                       //    ? s.NobetSayisiBayram == b.NobetSayisiBayram
                                                       //    : gunGrup.GunGrupAdi == "Cumartesi"
                                                       //    ? s.NobetSayisiCumartesi == b.NobetSayisiCumartesi
                                                       //    : s.NobetSayisiHaftaIci == b.NobetSayisiHaftaIci
                                                       //    )
                                                       select new EczaneNobetAlacakVerecek
                                                       {
                                                           EczaneNobetGrupId = s.EczaneNobetGrupId,
                                                           EczaneId = s.EczaneId,
                                                           EczaneAdi = s.EczaneAdi,
                                                           NobetGrupAdi = s.NobetGrupAdi,
                                                           NobetGrupId = s.NobetGrupId,
                                                           NobetSayisi = gunGrup.GunGrupAdi == "Pazar"
                                                               ? s.NobetSayisiPazar
                                                               : gunGrup.GunGrupAdi == "Arife"
                                                               ? s.NobetSayisiArife
                                                               : gunGrup.GunGrupAdi == "Bayram"
                                                               ? s.NobetSayisiBayram
                                                               : gunGrup.GunGrupAdi == "Cumartesi"
                                                               ? s.NobetSayisiCumartesi
                                                               : s.NobetSayisiHaftaIci,
                                                           SonNobetTarihi = gunGrup.GunGrupAdi == "Pazar"
                                                               ? s.SonNobetTarihiPazar
                                                               : gunGrup.GunGrupAdi == "Arife"
                                                               ? s.SonNobetTarihiArife
                                                               : gunGrup.GunGrupAdi == "Bayram"
                                                               ? s.SonNobetTarihiBayram
                                                               : gunGrup.GunGrupAdi == "Cumartesi"
                                                               ? s.SonNobetTarihiCumartesi
                                                               : s.SonNobetTarihiHaftaIci,
                                                           AnahtarTarih = gunGrup.GunGrupAdi == "Pazar"
                                                               ? b.SonNobetTarihiPazar
                                                               : gunGrup.GunGrupAdi == "Arife"
                                                               ? b.SonNobetTarihiArife
                                                               : gunGrup.GunGrupAdi == "Bayram"
                                                               ? b.SonNobetTarihiBayram
                                                               : gunGrup.GunGrupAdi == "Cumartesi"
                                                               ? b.SonNobetTarihiCumartesi
                                                               : b.SonNobetTarihiHaftaIci,
                                                           BorcluGunSayisi = gunGrup.GunGrupAdi == "Pazar"
                                                            ? (int)(s.NobetSayisiPazar > 0
                                                                   ? (s.SonNobetTarihiPazar - b.SonNobetTarihiPazar).TotalDays
                                                                   : (s.SonNobetTarihiPazar - b.SonNobetTarihiPazar).TotalDays - (b.SonNobetTarihiPazar - nobetUstGrup.BaslangicTarihi).TotalDays)
                                                            : gunGrup.GunGrupAdi == "Arife"
                                                            ? (int)(s.NobetSayisiArife > 0
                                                                   ? (s.SonNobetTarihiArife - b.SonNobetTarihiArife).TotalDays
                                                                   : (s.SonNobetTarihiArife - b.SonNobetTarihiArife).TotalDays - (b.SonNobetTarihiArife - nobetUstGrup.BaslangicTarihi).TotalDays)
                                                            : gunGrup.GunGrupAdi == "Bayram"
                                                            ? (int)(s.NobetSayisiBayram > 0
                                                                   ? (s.SonNobetTarihiBayram - b.SonNobetTarihiBayram).TotalDays
                                                                   : (s.SonNobetTarihiBayram - b.SonNobetTarihiBayram).TotalDays - (b.SonNobetTarihiBayram - nobetUstGrup.BaslangicTarihi).TotalDays)
                                                            : gunGrup.GunGrupAdi == "Cumartesi"
                                                            ? (int)(s.NobetSayisiCumartesi > 0
                                                                   ? (s.SonNobetTarihiCumartesi - b.SonNobetTarihiCumartesi).TotalDays
                                                                   : (s.SonNobetTarihiCumartesi - b.SonNobetTarihiCumartesi).TotalDays - (b.SonNobetTarihiCumartesi - nobetUstGrup.BaslangicTarihi).TotalDays)
                                                            : (int)(s.NobetSayisiHaftaIci > 0
                                                                   ? (s.SonNobetTarihiHaftaIci - b.SonNobetTarihiHaftaIci).TotalDays
                                                                   : (s.SonNobetTarihiHaftaIci - b.SonNobetTarihiHaftaIci).TotalDays - (b.SonNobetTarihiHaftaIci - nobetUstGrup.BaslangicTarihi).TotalDays),
                                                           GunGrup = gunGrup.GunGrupAdi,
                                                           GunGrupId = gunGrup.GunGrupId
                                                           //Nobets = b.NobetSayisi,
                                                           //AnahtarSıra = b.Id
                                                       }).ToList();

                eczaneNobetAlacakVerecek.AddRange(eczaneNobetAlacakVerecekGunGrup);
            }

            return eczaneNobetAlacakVerecek;//.Where(w => w.NobetSayisi > 0).ToList();
        }

        public List<MyDrop> GetPivotSekiller()
        {
            return new List<MyDrop>
            {
                new MyDrop{ Id=1, Value="Nöbetçi Eczane Listesi" },
                new MyDrop{ Id=2, Value="Gün Değer Dağılım" },
                new MyDrop{ Id=3, Value="Aylık Dağılım" },
                new MyDrop{ Id=4, Value="Günlük Dağılım" }
            };
        }
        public List<MyDrop> GetPivotSekillerGunFarki()
        {
            return new List<MyDrop>
            {
                new MyDrop{ Id=1, Value="En Fazla" },
                new MyDrop{ Id=2, Value="Frekans" },
                new MyDrop{ Id=3, Value="Detaylı" },
                new MyDrop{ Id=4, Value="Detaylı-2" }
            };
        }

        public List<EczaneNobetIstatistikGunFarki> EczaneNobetIstatistikGunFarkiHesapla(List<EczaneNobetSonucListe2> eczaneNobetSonuclar)
        {
            var gunGruplar = eczaneNobetSonuclar.Select(s => s.GunGrup).Distinct().OrderBy(o => o).ToList();
            var gunFarki = new List<EczaneNobetIstatistikGunFarki>();

            var eczanelerTumu = eczaneNobetSonuclar.Select(s => s.EczaneNobetGrupId).Distinct().ToList();

            foreach (var eczane in eczanelerTumu)
            {
                var liste = eczaneNobetSonuclar.Where(w => w.EczaneNobetGrupId == eczane).OrderBy(o => o.Tarih).ToList();

                int index = 0;

                foreach (var l1 in liste.Take(liste.Count() - 1))
                {
                    index++;

                    foreach (var l2 in liste.Where(w => w.Tarih > l1.Tarih).Take(1))
                    {
                        gunFarki.Add(new EczaneNobetIstatistikGunFarki
                        {
                            EczaneId = l2.EczaneId,
                            EczaneNobetGrupId = l2.EczaneNobetGrupId,
                            NobetGrupId = l2.NobetGrupId,
                            NobetGorevTipId = l2.NobetGorevTipId,
                            NobetGorevTipAdi = l2.NobetGorevTipAdi,
                            EczaneAdi = l2.EczaneAdi,
                            NobetGrupAdi = l2.NobetGrubu,
                            GunGrup = "Tümü",
                            Index = index,
                            Nobet1Tarih = l1.Tarih,
                            //Nobet1Tanim = String.Format("{0:dd MMM yyyy, ddd}", l1.Tarih),
                            Nobet1 = String.Format("{0:yyyy MM dd, ddd}", l1.Tarih),
                            Nobet1Gun = String.Format("{0:ddd.}", l1.Tarih),
                            //Nobet1 = long.Parse(l1.Tarih.ToString("yyyyMMdd")),
                            Nobet1Yil = l1.Tarih.Year,
                            Nobet1Ay = l1.Tarih.Month,
                            Nobet2Tarih = l2.Tarih,
                            Nobet2 = String.Format("{0:yyyy MM dd, ddd}", l2.Tarih),
                            Nobet2Gun = String.Format("{0:ddd.}", l2.Tarih),
                            //Nobet2 = long.Parse(l2.Tarih.ToString("yyyyMMdd")),
                            Nobet2Yil = l2.Tarih.Year,
                            Nobet2Ay = l2.Tarih.Month,
                            GunFarki = (int)(l2.Tarih - l1.Tarih).TotalDays,
                            NobetSonucDemoTipId = l2.NobetSonucDemoTipId
                        });
                    }
                }
            }


            foreach (var g in gunGruplar)
            {
                var sonuclarByGunGrup = eczaneNobetSonuclar.Where(w => w.GunGrup == g).Distinct().ToList();
                var eczaneler = sonuclarByGunGrup.Select(s => s.EczaneNobetGrupId).Distinct().ToList();

                foreach (var eczane in eczaneler)
                {
                    var liste = sonuclarByGunGrup.Where(w => w.EczaneNobetGrupId == eczane).OrderBy(o => o.Tarih).ToList();

                    //if (liste.Count > 1)
                    //{

                    //}

                    int index = 0;

                    foreach (var l1 in liste.Take(liste.Count() - 1))
                    {
                        index++;

                        foreach (var l2 in liste.Where(w => w.Tarih > l1.Tarih).Take(1))
                        {
                            gunFarki.Add(new EczaneNobetIstatistikGunFarki
                            {
                                EczaneId = l2.EczaneId,
                                EczaneNobetGrupId = l2.EczaneNobetGrupId,
                                NobetGrupId = l2.NobetGrupId,
                                NobetGorevTipId = l2.NobetGorevTipId,
                                NobetGorevTipAdi = l2.NobetGorevTipAdi,
                                EczaneAdi = l2.EczaneAdi,
                                NobetGrupAdi = l2.NobetGrubu,
                                GunGrup = g,
                                Index = index,
                                Nobet1Tarih = l1.Tarih,
                                //Nobet1Tanim = String.Format("{0:dd MMM yyyy, ddd}", l1.Tarih),
                                Nobet1 = String.Format("{0:yyyy MM dd, ddd}", l1.Tarih),
                                Nobet1Gun = String.Format("{0:ddd.}", l1.Tarih),
                                //Nobet1 = long.Parse(l1.Tarih.ToString("yyyyMMdd")),
                                Nobet1Yil = l1.Tarih.Year,
                                Nobet1Ay = l1.Tarih.Month,
                                Nobet2Tarih = l2.Tarih,
                                Nobet2 = String.Format("{0:yyyy MM dd, ddd}", l2.Tarih),
                                Nobet2Gun = String.Format("{0:ddd.}", l2.Tarih),
                                //Nobet2 = long.Parse(l2.Tarih.ToString("yyyyMMdd")),
                                Nobet2Yil = l2.Tarih.Year,
                                Nobet2Ay = l2.Tarih.Month,
                                GunFarki = (int)(l2.Tarih - l1.Tarih).TotalDays,
                                NobetSonucDemoTipId = l2.NobetSonucDemoTipId
                            });
                        }
                    }
                }
            }

            //var eczane = gunFarki.Where(w => w.EczaneAdi == "ALYA").ToList();

            return gunFarki;
        }

        public List<EczaneNobetIstatistikGunFarkiFrekans> EczaneNobetIstatistikGunFarkiFrekans(List<EczaneNobetIstatistikGunFarki> eczaneNobetIstatistikGunFarkiSonuclar)
        {
            var gunFarkiFrekans = eczaneNobetIstatistikGunFarkiSonuclar
                    .GroupBy(g => new
                    {
                        g.NobetSonucDemoTipId,
                        g.NobetGrupId,
                        g.NobetGrupAdi,
                        g.NobetGorevTipId,
                        g.NobetGorevTipAdi,
                        g.EczaneId,
                        g.EczaneNobetGrupId,
                        g.EczaneAdi,
                        g.GunGrup,
                        g.GunFarki
                    })
                    .Select(s => new EczaneNobetIstatistikGunFarkiFrekans
                    {
                        NobetSonucDemoTipId = s.Key.NobetSonucDemoTipId,
                        NobetGrupId = s.Key.NobetGrupId,
                        EczaneId = s.Key.EczaneId,
                        NobetGorevTipId = s.Key.NobetGorevTipId,
                        NobetGorevTipAdi = s.Key.NobetGorevTipAdi,
                        NobetGrupAdi = s.Key.NobetGrupAdi,
                        EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                        EczaneAdi = s.Key.EczaneAdi,
                        GunGrup = s.Key.GunGrup,
                        GunFarki = s.Key.GunFarki,
                        FrekanstakiEczaneSayisi = s.Count()
                    }).ToList();

            return gunFarkiFrekans;
        }
        public List<EsGrubaAyniGunYazilanNobetler> GetEsGrubaAyniGunYazilanNobetler(List<EczaneNobetSonucListe2> eczaneNobetSonuclar)
        {
            var nobetUstGrupId = eczaneNobetSonuclar.Select(s => s.NobetUstGrupId).Distinct().FirstOrDefault();

            var eczaneGruplar = _eczaneGrupService.GetDetaylarAktifGruplar(nobetUstGrupId);

            var liste = (from s in eczaneNobetSonuclar
                         from g in eczaneGruplar
                         where s.EczaneId == g.EczaneId
                         && s.NobetGrupId == g.NobetGrupId
                         && s.NobetGorevTipId == g.NobetGorevTipId
                         group new { g, s } by new
                         {
                             s.NobetUstGrupId,
                             s.TakvimId,
                             s.Tarih,
                             g.EczaneGrupTanimId,
                             g.EczaneGrupTanimAdi,
                             g.EczaneGrupTanimTipAdi,
                             g.EczaneGrupTanimTipId,
                             g.AyniGunNobetTutabilecekEczaneSayisi
                         } into gr
                         where gr.Count() > gr.Key.AyniGunNobetTutabilecekEczaneSayisi
                         orderby gr.Key.Tarih descending
                         select new EsGrubaAyniGunYazilanNobetler
                         {
                             EczaneGrupTanimAdi = gr.Key.EczaneGrupTanimAdi,
                             EczaneGrupTanimId = gr.Key.EczaneGrupTanimId,
                             EczaneGrupTanimTipAdi = gr.Key.EczaneGrupTanimTipAdi,
                             EczaneGrupTanimTipId = gr.Key.EczaneGrupTanimTipId,
                             NobetTarihi = String.Format("{0:dd MMM yyyy, ddd}", gr.Key.Tarih),
                             TakvimId = gr.Key.TakvimId,
                             NobetUstGrupId = gr.Key.NobetUstGrupId,
                             AyniGunNobetTutanEczaneSayisi = gr.Count()
                         }).ToList();

            return liste;
        }

        public List<AyniGunNobetTutanEczane> GetAyniGunNobetTutanAltGrupluEczaneler(List<EczaneNobetSonucListe2> ayniGunNobetTutanEczaneler)
        {
            var nobetGrupNobetAltGrupEslemeli = ayniGunNobetTutanEczaneler
                .Select(s => new
                {
                    s.NobetUstGrupId,
                    s.NobetGrupId
                }).Distinct().ToList();

            var nobetUstGrupId = nobetGrupNobetAltGrupEslemeli.Select(s => s.NobetUstGrupId).FirstOrDefault();
            var nobetGrupIds = nobetGrupNobetAltGrupEslemeli.Select(s => s.NobetGrupId).OrderBy(o => o).ToArray();
            var altGrupluNobetGrupIds = ayniGunNobetTutanEczaneler.Where(w => w.NobetAltGrupId > 0).Select(s => s.NobetGrupId).ToArray();

            var ayniGunNobetSayisi = new List<AyniGunNobetTutanEczane>();

            if (nobetUstGrupId == 1)
            {
                altGrupluNobetGrupIds = new int[1] { 1 };
            }

            if (altGrupluNobetGrupIds.Count() > 0)
            {
                if (nobetUstGrupId == 1)
                {
                    var listePivot = (from s in ayniGunNobetTutanEczaneler
                                      group s by new { s.TakvimId, s.Tarih, s.GunGrup } into grouped
                                      select new
                                      {
                                          grouped.Key.TakvimId,
                                          grouped.Key.Tarih,
                                          //grouped.Key.GunGrup,
                                          Y1EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneAdi).FirstOrDefault(), //1
                                          Y2EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneAdi).FirstOrDefault(), //2
                                          Y3EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneAdi).FirstOrDefault() //3                                        
                                      }).ToList();

                    var alanya1_2AyniGunNobetSayisi = (from s in listePivot
                                                       group s by new { s.Y1EczaneAdi, s.Y2EczaneAdi } into grouped
                                                       where grouped.Count() > 0
                                                       select new AyniGunNobetTutanEczane
                                                       {
                                                           Grup = "Alanya 1-2",
                                                           G1Eczane = grouped.Key.Y1EczaneAdi,
                                                           G2Eczane = grouped.Key.Y2EczaneAdi,
                                                           AltGrupAdi = "Kendisi",
                                                           AyniGunNobetSayisi = grouped.Count(),
                                                           //Tarih = grouped.Key.Tarih,
                                                           //GunGrup = grouped.Key.GunGrup
                                                       }).ToList();

                    var alanya1_3AyniGunNobetSayisi = (from s in listePivot
                                                       group s by new { s.Y1EczaneAdi, s.Y3EczaneAdi } into grouped
                                                       where grouped.Count() > 0
                                                       select new AyniGunNobetTutanEczane
                                                       {
                                                           Grup = "Alanya 1-3",
                                                           G1Eczane = grouped.Key.Y1EczaneAdi,
                                                           G2Eczane = grouped.Key.Y3EczaneAdi,
                                                           AltGrupAdi = "Kendisi",
                                                           AyniGunNobetSayisi = grouped.Count(),
                                                           //Tarih = grouped.Key.Tarih,
                                                           //GunGrup = grouped.Key.GunGrup
                                                       }).ToList();

                    var alanya2_3AyniGunNobetSayisi = (from s in listePivot
                                                       group s by new { s.Y2EczaneAdi, s.Y3EczaneAdi } into grouped
                                                       where grouped.Count() > 0
                                                       select new AyniGunNobetTutanEczane
                                                       {
                                                           Grup = "Alanya 2-3",
                                                           G1Eczane = grouped.Key.Y2EczaneAdi,
                                                           G2Eczane = grouped.Key.Y3EczaneAdi,
                                                           AltGrupAdi = "Kendisi",
                                                           AyniGunNobetSayisi = grouped.Count(),
                                                           //Tarih = grouped.Key.Tarih,
                                                           //GunGrup = grouped.Key.GunGrup
                                                       }).ToList();

                    ayniGunNobetSayisi = alanya1_2AyniGunNobetSayisi
                        .Union(alanya1_3AyniGunNobetSayisi)
                        .Union(alanya2_3AyniGunNobetSayisi)
                        .ToList();
                }
                else if (nobetUstGrupId == 2)
                {
                    var listePivot = (from s in ayniGunNobetTutanEczaneler
                                      group s by new { s.TakvimId, s.Tarih, s.GunGrup } into grouped
                                      select new
                                      {
                                          grouped.Key.TakvimId,
                                          grouped.Key.Tarih,
                                          grouped.Key.GunGrup,
                                          Y1EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneAdi).FirstOrDefault(), //4
                                          Y2EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneAdi).FirstOrDefault(), //5
                                          Y3EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneAdi).FirstOrDefault(), //6
                                          Y4EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[3]).Select(s => s.EczaneAdi).FirstOrDefault(), //7
                                          Y5EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[4]).Select(s => s.EczaneAdi).FirstOrDefault(), //8
                                          Y6EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[5]).Select(s => s.EczaneAdi).FirstOrDefault(), //9
                                          Y7EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[6]).Select(s => s.EczaneAdi).FirstOrDefault(), //10
                                          Y8EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[7]).Select(s => s.EczaneAdi).FirstOrDefault(), //11
                                          Y9EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[8]).Select(s => s.EczaneAdi).FirstOrDefault(), //12
                                          Y10EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[9]).Select(s => s.EczaneAdi).FirstOrDefault(), //13
                                          Y11EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[10]).Select(s => s.EczaneAdi).FirstOrDefault(), //14
                                          NobetAltGrupId = grouped.Where(w => w.NobetGrupId == altGrupluNobetGrupIds[0]).Select(s => s.NobetAltGrupId).FirstOrDefault(),
                                          NobetAltGrupAdi = grouped.Where(w => w.NobetGrupId == altGrupluNobetGrupIds[0]).Select(s => s.NobetAltGrupAdi).FirstOrDefault()
                                      }).ToList();

                    var antalya1_2AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new { s.Y1EczaneAdi, s.Y2EczaneAdi, s.Tarih, s.GunGrup } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "Antalya 1-2",
                                                            G1Eczane = grouped.Key.Y1EczaneAdi,
                                                            G2Eczane = grouped.Key.Y2EczaneAdi,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Key.Tarih,
                                                            GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var antalya7_8AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new { s.Y7EczaneAdi, s.Y8EczaneAdi, s.Tarih, s.GunGrup } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "Antalya 7-8",
                                                            G1Eczane = grouped.Key.Y7EczaneAdi,
                                                            G2Eczane = grouped.Key.Y8EczaneAdi,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Key.Tarih,
                                                            GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var antalya10_11AyniGunNobetSayisi = (from s in listePivot
                                                          group s by new { s.Y10EczaneAdi, s.Y11EczaneAdi, s.NobetAltGrupAdi, s.Tarih, s.GunGrup } into grouped
                                                          where grouped.Count() > 0
                                                          select new AyniGunNobetTutanEczane
                                                          {
                                                              Grup = "Antalya 10-11",
                                                              G1Eczane = grouped.Key.Y10EczaneAdi,
                                                              G2Eczane = grouped.Key.Y11EczaneAdi,
                                                              AltGrupAdi = grouped.Key.NobetAltGrupAdi,
                                                              AyniGunNobetSayisi = grouped.Count(),
                                                              Tarih = grouped.Key.Tarih,
                                                              GunGrup = grouped.Key.GunGrup
                                                          }).ToList();

                    ayniGunNobetSayisi = antalya1_2AyniGunNobetSayisi
                        .Union(antalya7_8AyniGunNobetSayisi)
                        .Union(antalya10_11AyniGunNobetSayisi)
                        .ToList();
                }
                else if (nobetUstGrupId == 3)
                {
                    var listePivot = (from s in ayniGunNobetTutanEczaneler
                                      group s by new { s.TakvimId, s.Tarih, s.GunGrup } into grouped
                                      select new
                                      {
                                          grouped.Key.TakvimId,
                                          grouped.Key.Tarih,
                                          grouped.Key.GunGrup,
                                          G1EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneAdi).FirstOrDefault(), //15
                                          G2EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneAdi).FirstOrDefault(), //16
                                          G3EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneAdi).FirstOrDefault(), //17
                                          G4EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[3]).Select(s => s.EczaneAdi).FirstOrDefault(), //18
                                          G5EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[4]).Select(s => s.EczaneAdi).FirstOrDefault(), //19
                                          G6EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[5]).Select(s => s.EczaneAdi).FirstOrDefault(), //20
                                          G7EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[6]).Select(s => s.EczaneAdi).FirstOrDefault(), //21
                                          G8EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[7]).Select(s => s.EczaneAdi).FirstOrDefault(), //22
                                          G9EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[8]).Select(s => s.EczaneAdi).FirstOrDefault(), //23
                                          G10EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[9]).Select(s => s.EczaneAdi).FirstOrDefault(), //24
                                          NobetAltGrupId = grouped.Where(w => w.NobetGrupId == 21).Select(s => s.NobetAltGrupId).FirstOrDefault(),
                                          NobetAltGrupAdi = grouped.Where(w => w.NobetGrupId == 21).Select(s => s.NobetAltGrupAdi).FirstOrDefault(),
                                          NobetAltGrupIdToroslar = grouped.Where(w => w.NobetGrupId == 15).Select(s => s.NobetAltGrupId).FirstOrDefault(),
                                          NobetAltGrupAdiToroslar = grouped.Where(w => w.NobetGrupId == 15).Select(s => s.NobetAltGrupAdi).FirstOrDefault()
                                      }).ToList();

                    #region toroslar
                    var toroslar1_2AyniGunNobetSayisi = (from s in listePivot
                                                         group s by new { s.G1EczaneAdi, s.G2EczaneAdi } into grouped
                                                         where grouped.Count() > 0
                                                         select new AyniGunNobetTutanEczane
                                                         {
                                                             Grup = "1 Toroslar 1-2",
                                                             G1Eczane = grouped.Key.G1EczaneAdi,
                                                             G2Eczane = grouped.Key.G2EczaneAdi,
                                                             AltGrupAdi = "Kendisi",
                                                             AyniGunNobetSayisi = grouped.Count(),
                                                             //Tarih = grouped.Key.Tarih,
                                                             //GunGrup = grouped.Key.GunGrup
                                                         }).ToList();

                    var t2_1AyniGunNobetSayisi = (from s in listePivot
                                                  group s by new
                                                  {
                                                      s.G2EczaneAdi,
                                                      s.G1EczaneAdi,
                                                      s.NobetAltGrupAdiToroslar,
                                                      s.Tarih,
                                                      s.GunGrup
                                                  } into grouped
                                                  where grouped.Count() > 0
                                                  select new AyniGunNobetTutanEczane
                                                  {
                                                      Grup = "Toroslar 2-1",
                                                      G1Eczane = grouped.Key.G2EczaneAdi,
                                                      G2Eczane = grouped.Key.G1EczaneAdi,
                                                      AltGrupAdi = grouped.Key.NobetAltGrupAdiToroslar,
                                                      AyniGunNobetSayisi = grouped.Count(),
                                                      Tarih = grouped.Key.Tarih,
                                                      GunGrup = grouped.Key.GunGrup
                                                  }).ToList();

                    #endregion

                    #region akdeniz
                    var akdeniz1_2AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new { s.G3EczaneAdi, s.G4EczaneAdi } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "2 Akdeniz 1-2",
                                                            G1Eczane = grouped.Key.G3EczaneAdi,
                                                            G2Eczane = grouped.Key.G4EczaneAdi,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            //Tarih = grouped.Key.Tarih,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var akdeniz2_3AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new { s.G4EczaneAdi, s.G5EczaneAdi } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "3 Akdeniz 2-3",
                                                            G1Eczane = grouped.Key.G4EczaneAdi,
                                                            G2Eczane = grouped.Key.G5EczaneAdi,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            //Tarih = grouped.Key.Tarih,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var akdeniz1_3AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new { s.G3EczaneAdi, s.G5EczaneAdi } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "4 Akdeniz 1-3",
                                                            G1Eczane = grouped.Key.G3EczaneAdi,
                                                            G2Eczane = grouped.Key.G5EczaneAdi,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            //Tarih = grouped.Key.Tarih,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();
                    #endregion

                    #region yenisehir

                    #region alt grup takip
                    var y1_2AyniGunNobetSayisi = (from s in listePivot
                                                  group s by new
                                                  {
                                                      s.G6EczaneAdi,
                                                      s.G7EczaneAdi,
                                                      s.NobetAltGrupAdi,
                                                      s.Tarih,
                                                      s.GunGrup
                                                  } into grouped
                                                  where grouped.Count() > 0
                                                  select new AyniGunNobetTutanEczane
                                                  {
                                                      Grup = "Yenişehir 1-2",
                                                      G1Eczane = grouped.Key.G6EczaneAdi,
                                                      G2Eczane = grouped.Key.G7EczaneAdi,
                                                      AltGrupAdi = grouped.Key.NobetAltGrupAdi,
                                                      AyniGunNobetSayisi = grouped.Count(),
                                                      Tarih = grouped.Key.Tarih,
                                                      GunGrup = grouped.Key.GunGrup
                                                  }).ToList();

                    var y3_2AyniGunNobetSayisi = (from s in listePivot
                                                  group s by new
                                                  {
                                                      s.G8EczaneAdi,
                                                      s.G7EczaneAdi,
                                                      s.NobetAltGrupAdi,
                                                      s.Tarih,
                                                      s.GunGrup
                                                  } into grouped
                                                  where grouped.Count() > 0
                                                  select new AyniGunNobetTutanEczane
                                                  {
                                                      Grup = "Yenişehir 3-2",
                                                      G1Eczane = grouped.Key.G8EczaneAdi,
                                                      G2Eczane = grouped.Key.G7EczaneAdi,
                                                      AltGrupAdi = grouped.Key.NobetAltGrupAdi,
                                                      AyniGunNobetSayisi = grouped.Count(),
                                                      Tarih = grouped.Key.Tarih,
                                                      GunGrup = grouped.Key.GunGrup
                                                  }).ToList();
                    #endregion

                    var yenisehir1_2AyniGunNobetSayisi = (from s in listePivot
                                                          group s by new { s.G6EczaneAdi, s.G7EczaneAdi, s.NobetAltGrupAdi } into grouped
                                                          where grouped.Count() > 0
                                                          select new AyniGunNobetTutanEczane
                                                          {
                                                              Grup = "5 Yenişehir 1-2",
                                                              G1Eczane = grouped.Key.G6EczaneAdi,
                                                              G2Eczane = grouped.Key.G7EczaneAdi,
                                                              AltGrupAdi = grouped.Key.NobetAltGrupAdi,
                                                              AyniGunNobetSayisi = grouped.Count(),
                                                              //Tarih = grouped.Key.Tarih,
                                                              //GunGrup = grouped.Key.GunGrup
                                                          }).ToList();

                    var yenisehir3_2AyniGunNobetSayisi = (from s in listePivot
                                                          group s by new { s.G7EczaneAdi, s.G8EczaneAdi, s.NobetAltGrupAdi } into grouped
                                                          where grouped.Count() > 0
                                                          select new AyniGunNobetTutanEczane
                                                          {
                                                              Grup = "6 Yenişehir 3-2",
                                                              G1Eczane = grouped.Key.G8EczaneAdi,
                                                              G2Eczane = grouped.Key.G7EczaneAdi,
                                                              AltGrupAdi = grouped.Key.NobetAltGrupAdi,
                                                              AyniGunNobetSayisi = grouped.Count(),
                                                              //Tarih = grouped.Key.Tarih,
                                                              //GunGrup = grouped.Key.GunGrup
                                                          }).ToList();

                    var yenisehir1_3AyniGunNobetSayisi = (from s in listePivot
                                                          group s by new { s.G6EczaneAdi, s.G8EczaneAdi } into grouped
                                                          where grouped.Count() > 0
                                                          select new AyniGunNobetTutanEczane
                                                          {
                                                              Grup = "7 Yenişehir 1-3",
                                                              G1Eczane = grouped.Key.G6EczaneAdi,
                                                              G2Eczane = grouped.Key.G8EczaneAdi,
                                                              AltGrupAdi = "Kendisi",
                                                              AyniGunNobetSayisi = grouped.Count(),
                                                              //Tarih = grouped.Key.Tarih,
                                                              //GunGrup = grouped.Key.GunGrup
                                                          }).ToList();
                    #endregion

                    #region mezitli
                    var mezitli1_2AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new { s.G9EczaneAdi, s.G10EczaneAdi } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "8 Mezitli 1-2",
                                                            G1Eczane = grouped.Key.G9EczaneAdi,
                                                            G2Eczane = grouped.Key.G10EczaneAdi,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            //Tarih = grouped.Key.Tarih,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();
                    #endregion

                    ayniGunNobetSayisi = y1_2AyniGunNobetSayisi
                                  .Union(y3_2AyniGunNobetSayisi)
                                  .Union(t2_1AyniGunNobetSayisi)
                                        //toroslar1_2AyniGunNobetSayisi
                                        //                .Union(akdeniz1_2AyniGunNobetSayisi)
                                        //                .Union(akdeniz2_3AyniGunNobetSayisi)
                                        //                .Union(akdeniz1_3AyniGunNobetSayisi)

                                        //                .Union(yenisehir1_2AyniGunNobetSayisi)
                                        //                .Union(yenisehir3_2AyniGunNobetSayisi)
                                        //                .Union(yenisehir1_3AyniGunNobetSayisi)

                                        //                .Union(mezitli1_2AyniGunNobetSayisi)
                                        .ToList();

                    //ayniGunNobetSayisi = yenisehir2_3AyniGunNobetSayisi;

                }
                else if (nobetUstGrupId == 8)
                {
                    var listePivot = (from s in ayniGunNobetTutanEczaneler
                                      group s by new { s.TakvimId, s.Tarih, s.GunGrup } into grouped
                                      select new
                                      {
                                          grouped.Key.TakvimId,
                                          grouped.Key.Tarih,
                                          grouped.Key.GunGrup,
                                          Y1EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneAdi).FirstOrDefault(), //42 - çarşı
                                          Y2EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneAdi).FirstOrDefault(), //53 - ssk
                                          Y3EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneAdi).FirstOrDefault(), //54 - gelişim
                                          NobetAltGrupId = grouped.Where(w => w.NobetGrupGorevTipId == 53).Select(s => s.NobetAltGrupId).FirstOrDefault(),
                                          NobetAltGrupAdi = grouped.Where(w => w.NobetGrupGorevTipId == 53).Select(s => s.NobetAltGrupAdi).FirstOrDefault(),
                                          NobetAltGrupIdGelisim = grouped.Where(w => w.NobetGrupGorevTipId == 54).Select(s => s.NobetAltGrupId).FirstOrDefault(),
                                          NobetAltGrupAdiGelisim = grouped.Where(w => w.NobetGrupGorevTipId == 54).Select(s => s.NobetAltGrupAdi).FirstOrDefault(),
                                          NobetAltGrupIdCarsi = grouped.Where(w => w.NobetGrupGorevTipId == 42).Select(s => s.NobetAltGrupId).FirstOrDefault(),
                                          NobetAltGrupAdiCarsi = grouped.Where(w => w.NobetGrupGorevTipId == 42).Select(s => s.NobetAltGrupAdi).FirstOrDefault(),
                                      }).ToList();

                    var iskenderunCarsiSkkAyniGunNobetSayisi = (from s in listePivot
                                                                group s by new
                                                                {
                                                                    s.Y1EczaneAdi,
                                                                    s.Y2EczaneAdi,
                                                                    s.NobetAltGrupAdi,
                                                                    s.Tarih,
                                                                    s.GunGrup
                                                                } into grouped
                                                                where grouped.Count() > 0
                                                                select new AyniGunNobetTutanEczane
                                                                {
                                                                    Grup = "Çarşı - Ssk",
                                                                    G1Eczane = grouped.Key.Y1EczaneAdi,
                                                                    G2Eczane = grouped.Key.Y2EczaneAdi,
                                                                    AltGrupAdi = grouped.Key.NobetAltGrupAdi,
                                                                    AyniGunNobetSayisi = grouped.Count(),
                                                                    Tarih = grouped.Key.Tarih,
                                                                    GunGrup = grouped.Key.GunGrup
                                                                }).ToList();

                    var iskenderunSkkGelisimAyniGunNobetSayisi = (from s in listePivot
                                                                  group s by new
                                                                  {
                                                                      s.Y2EczaneAdi,
                                                                      s.Y3EczaneAdi,
                                                                      s.NobetAltGrupAdiGelisim,
                                                                      s.Tarih,
                                                                      s.GunGrup
                                                                  } into grouped
                                                                  where grouped.Count() > 0
                                                                  select new AyniGunNobetTutanEczane
                                                                  {
                                                                      Grup = "Ssk - Gelişim",
                                                                      G1Eczane = grouped.Key.Y2EczaneAdi,
                                                                      G2Eczane = grouped.Key.Y3EczaneAdi,
                                                                      AltGrupAdi = grouped.Key.NobetAltGrupAdiGelisim,
                                                                      AyniGunNobetSayisi = grouped.Count(),
                                                                      Tarih = grouped.Key.Tarih,
                                                                      GunGrup = grouped.Key.GunGrup
                                                                  }).ToList();


                    ayniGunNobetSayisi = iskenderunCarsiSkkAyniGunNobetSayisi
                        .Union(iskenderunSkkGelisimAyniGunNobetSayisi)
                        //.Union(alanya2_3AyniGunNobetSayisi)
                        .ToList();
                }
            }

            return ayniGunNobetSayisi;
        }
        public List<AyniGunNobetTutanEczane> GetAyniGunNobetTutanEczaneler(List<EczaneNobetSonucListe2> ayniGunNobetTutanEczaneler)
        {
            var nobetGrupNobetAltGrupEslemeli = ayniGunNobetTutanEczaneler
                .Select(s => new
                {
                    s.NobetUstGrupId,
                    s.NobetGrupId
                }).Distinct().ToList();

            var nobetUstGrupId = nobetGrupNobetAltGrupEslemeli.Select(s => s.NobetUstGrupId).FirstOrDefault();
            var nobetGrupIds = nobetGrupNobetAltGrupEslemeli.Select(s => s.NobetGrupId).OrderBy(o => o).ToArray();

            var ayniGunNobetSayisi = new List<AyniGunNobetTutanEczane>();
            if (nobetGrupIds.Count() > 0)
            {
                if (nobetUstGrupId == 1)
                {
                    var listePivot = (from s in ayniGunNobetTutanEczaneler
                                      group s by new { s.TakvimId, s.Tarih, s.GunGrup } into grouped
                                      select new
                                      {
                                          grouped.Key.TakvimId,
                                          grouped.Key.Tarih,
                                          //grouped.Key.GunGrup,
                                          Y1EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneAdi).FirstOrDefault(), //1
                                          Y2EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneAdi).FirstOrDefault(), //2
                                          Y3EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneAdi).FirstOrDefault(), //3                                        

                                          Y1EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //1
                                          Y2EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //2
                                          Y3EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneNobetGrupId).FirstOrDefault() //3      
                                      }).ToList();

                    var alanya1_2AyniGunNobetSayisi = (from s in listePivot
                                                       group s by new
                                                       {
                                                           s.Y1EczaneAdi,
                                                           s.Y2EczaneAdi,
                                                           s.Y1EczaneNobetGrupId,
                                                           s.Y2EczaneNobetGrupId,
                                                       } into grouped
                                                       where grouped.Count() > 0
                                                       select new AyniGunNobetTutanEczane
                                                       {
                                                           Grup = "Alanya 1-2",
                                                           G1Eczane = grouped.Key.Y1EczaneAdi,
                                                           G2Eczane = grouped.Key.Y2EczaneAdi,
                                                           G1EczaneNobetGrupId = grouped.Key.Y1EczaneNobetGrupId,
                                                           G2EczaneNobetGrupId = grouped.Key.Y2EczaneNobetGrupId,
                                                           AltGrupAdi = "Kendisi",
                                                           AyniGunNobetSayisi = grouped.Count(),
                                                           Tarih = grouped.Max(m => m.Tarih),
                                                           TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                           //GunGrup = grouped.Key.GunGrup
                                                       }).ToList();

                    var alanya1_3AyniGunNobetSayisi = (from s in listePivot
                                                       group s by new
                                                       {
                                                           s.Y1EczaneAdi,
                                                           s.Y3EczaneAdi,
                                                           s.Y1EczaneNobetGrupId,
                                                           s.Y3EczaneNobetGrupId,
                                                       } into grouped
                                                       where grouped.Count() > 0
                                                       select new AyniGunNobetTutanEczane
                                                       {
                                                           Grup = "Alanya 1-3",
                                                           G1Eczane = grouped.Key.Y1EczaneAdi,
                                                           G2Eczane = grouped.Key.Y3EczaneAdi,
                                                           G1EczaneNobetGrupId = grouped.Key.Y1EczaneNobetGrupId,
                                                           G2EczaneNobetGrupId = grouped.Key.Y3EczaneNobetGrupId,
                                                           AltGrupAdi = "Kendisi",
                                                           AyniGunNobetSayisi = grouped.Count(),
                                                           Tarih = grouped.Max(m => m.Tarih),
                                                           TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                           //GunGrup = grouped.Key.GunGrup
                                                       }).ToList();

                    var alanya2_3AyniGunNobetSayisi = (from s in listePivot
                                                       group s by new
                                                       {
                                                           s.Y2EczaneAdi,
                                                           s.Y3EczaneAdi,
                                                           s.Y2EczaneNobetGrupId,
                                                           s.Y3EczaneNobetGrupId,
                                                       } into grouped
                                                       where grouped.Count() > 0
                                                       select new AyniGunNobetTutanEczane
                                                       {
                                                           Grup = "Alanya 2-3",
                                                           G1Eczane = grouped.Key.Y2EczaneAdi,
                                                           G2Eczane = grouped.Key.Y3EczaneAdi,
                                                           G1EczaneNobetGrupId = grouped.Key.Y2EczaneNobetGrupId,
                                                           G2EczaneNobetGrupId = grouped.Key.Y3EczaneNobetGrupId,
                                                           AltGrupAdi = "Kendisi",
                                                           AyniGunNobetSayisi = grouped.Count(),
                                                           Tarih = grouped.Max(m => m.Tarih),
                                                           TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                           //GunGrup = grouped.Key.GunGrup
                                                       }).ToList();

                    ayniGunNobetSayisi = alanya1_2AyniGunNobetSayisi
                        .Union(alanya1_3AyniGunNobetSayisi)
                        .Union(alanya2_3AyniGunNobetSayisi)
                        .ToList();
                }
                else if (nobetUstGrupId == 2)
                {
                    var listePivot = (from s in ayniGunNobetTutanEczaneler
                                      group s by new { s.TakvimId, s.Tarih, s.GunGrup } into grouped
                                      select new
                                      {
                                          grouped.Key.TakvimId,
                                          grouped.Key.Tarih,
                                          grouped.Key.GunGrup,
                                          Y1EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneAdi).FirstOrDefault(), //4
                                          Y2EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneAdi).FirstOrDefault(), //5
                                          Y3EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneAdi).FirstOrDefault(), //6
                                          Y4EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[3]).Select(s => s.EczaneAdi).FirstOrDefault(), //7
                                          Y5EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[4]).Select(s => s.EczaneAdi).FirstOrDefault(), //8
                                          Y6EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[5]).Select(s => s.EczaneAdi).FirstOrDefault(), //9
                                          Y7EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[6]).Select(s => s.EczaneAdi).FirstOrDefault(), //10
                                          Y8EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[7]).Select(s => s.EczaneAdi).FirstOrDefault(), //11
                                          Y9EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[8]).Select(s => s.EczaneAdi).FirstOrDefault(), //12
                                          Y10EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[9]).Select(s => s.EczaneAdi).FirstOrDefault(), //13
                                          Y11EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[10]).Select(s => s.EczaneAdi).FirstOrDefault(), //14

                                          Y1EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //4
                                          Y2EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //5
                                          Y3EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //6
                                          Y4EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[3]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //7
                                          Y5EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[4]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //8
                                          Y6EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[5]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //9
                                          Y7EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[6]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //10
                                          Y8EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[7]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //11
                                          Y9EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[8]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //12
                                          Y10EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[9]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //13
                                          Y11EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[10]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //14
                                                                                                                                                                          //NobetAltGrupId = grouped.Where(w => w.NobetGrupId == altGrupluNobetGrupIds[0]).Select(s => s.NobetAltGrupId).FirstOrDefault(),
                                                                                                                                                                          //NobetAltGrupAdi = grouped.Where(w => w.NobetGrupId == altGrupluNobetGrupIds[0]).Select(s => s.NobetAltGrupAdi).FirstOrDefault()
                                      }).ToList();

                    var antalya1_2AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new
                                                        {
                                                            s.Y1EczaneAdi,
                                                            s.Y2EczaneAdi,
                                                            s.Y1EczaneNobetGrupId,
                                                            s.Y2EczaneNobetGrupId,
                                                            //s.Tarih,
                                                            //s.GunGrup
                                                        } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "Antalya 1-2",
                                                            G1Eczane = grouped.Key.Y1EczaneAdi,
                                                            G2Eczane = grouped.Key.Y2EczaneAdi,
                                                            G1EczaneNobetGrupId = grouped.Key.Y1EczaneNobetGrupId,
                                                            G2EczaneNobetGrupId = grouped.Key.Y2EczaneNobetGrupId,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Max(m => m.Tarih),
                                                            TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var antalya7_8AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new
                                                        {
                                                            s.Y7EczaneAdi,
                                                            s.Y8EczaneAdi,
                                                            s.Y7EczaneNobetGrupId,
                                                            s.Y8EczaneNobetGrupId,
                                                            //s.Tarih,
                                                            //s.GunGrup
                                                        } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "Antalya 7-8",
                                                            G1Eczane = grouped.Key.Y7EczaneAdi,
                                                            G2Eczane = grouped.Key.Y8EczaneAdi,
                                                            G1EczaneNobetGrupId = grouped.Key.Y7EczaneNobetGrupId,
                                                            G2EczaneNobetGrupId = grouped.Key.Y8EczaneNobetGrupId,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Max(m => m.Tarih),
                                                            TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var antalya10_11AyniGunNobetSayisi = (from s in listePivot
                                                          group s by new
                                                          {
                                                              s.Y10EczaneAdi,
                                                              s.Y11EczaneAdi,
                                                              s.Y10EczaneNobetGrupId,
                                                              s.Y11EczaneNobetGrupId,
                                                              //s.NobetAltGrupAdi,
                                                              //s.Tarih,
                                                              //s.GunGrup
                                                          } into grouped
                                                          where grouped.Count() > 0
                                                          select new AyniGunNobetTutanEczane
                                                          {
                                                              Grup = "Antalya 10-11",
                                                              G1Eczane = grouped.Key.Y10EczaneAdi,
                                                              G2Eczane = grouped.Key.Y11EczaneAdi,
                                                              G1EczaneNobetGrupId = grouped.Key.Y10EczaneNobetGrupId,
                                                              G2EczaneNobetGrupId = grouped.Key.Y11EczaneNobetGrupId,
                                                              //AltGrupAdi = grouped.Key.NobetAltGrupAdi,
                                                              AyniGunNobetSayisi = grouped.Count(),
                                                              Tarih = grouped.Max(m => m.Tarih),
                                                              TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                              //GunGrup = grouped.Key.GunGrup
                                                          }).ToList();

                    ayniGunNobetSayisi = antalya1_2AyniGunNobetSayisi
                        .Union(antalya7_8AyniGunNobetSayisi)
                        .Union(antalya10_11AyniGunNobetSayisi)
                        .ToList();
                }
                else if (nobetUstGrupId == 3)
                {
                    var listePivot = (from s in ayniGunNobetTutanEczaneler
                                      group s by new { s.TakvimId, s.Tarih, s.GunGrup } into grouped
                                      select new
                                      {
                                          grouped.Key.TakvimId,
                                          grouped.Key.Tarih,
                                          grouped.Key.GunGrup,
                                          G1EczaneAdi = grouped.Where(w => w.NobetGrupId == 15).Select(s => s.EczaneAdi).FirstOrDefault(), //15
                                          G2EczaneAdi = grouped.Where(w => w.NobetGrupId == 16).Select(s => s.EczaneAdi).FirstOrDefault(), //16
                                          G3EczaneAdi = grouped.Where(w => w.NobetGrupId == 17).Select(s => s.EczaneAdi).FirstOrDefault(), //17
                                          G4EczaneAdi = grouped.Where(w => w.NobetGrupId == 18).Select(s => s.EczaneAdi).FirstOrDefault(), //18
                                          G5EczaneAdi = grouped.Where(w => w.NobetGrupId == 19).Select(s => s.EczaneAdi).FirstOrDefault(), //19
                                          G6EczaneAdi = grouped.Where(w => w.NobetGrupId == 20).Select(s => s.EczaneAdi).FirstOrDefault(), //20
                                          G7EczaneAdi = grouped.Where(w => w.NobetGrupId == 21).Select(s => s.EczaneAdi).FirstOrDefault(), //21
                                          G8EczaneAdi = grouped.Where(w => w.NobetGrupId == 22).Select(s => s.EczaneAdi).FirstOrDefault(), //22
                                          G9EczaneAdi = grouped.Where(w => w.NobetGrupId == 23).Select(s => s.EczaneAdi).FirstOrDefault(), //23
                                          G10EczaneAdi = grouped.Where(w => w.NobetGrupId == 24).Select(s => s.EczaneAdi).FirstOrDefault(), //24

                                          G1EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 15).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //15
                                          G2EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 16).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //16
                                          G3EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 17).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //17
                                          G4EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 18).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //18
                                          G5EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 19).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //19
                                          G6EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 20).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //20
                                          G7EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 21).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //21
                                          G8EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 22).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //22
                                          G9EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 23).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //23
                                          G10EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == 24).Select(s => s.EczaneNobetGrupId).FirstOrDefault()
                                          //NobetAltGrupId = grouped.Where(w => w.NobetGrupId == altGrupluNobetGrupIds[0]).Select(s => s.NobetAltGrupId).FirstOrDefault(),
                                          //NobetAltGrupAdi = grouped.Where(w => w.NobetGrupId == altGrupluNobetGrupIds[0]).Select(s => s.NobetAltGrupAdi).FirstOrDefault()
                                      }).ToList();

                    #region toroslar
                    var toroslar1_2AyniGunNobetSayisi = (from s in listePivot
                                                         group s by new
                                                         {
                                                             s.G1EczaneAdi,
                                                             s.G2EczaneAdi,
                                                             s.G1EczaneNobetGrupId,
                                                             s.G2EczaneNobetGrupId,
                                                         } into grouped
                                                         where grouped.Count() > 0
                                                         select new AyniGunNobetTutanEczane
                                                         {
                                                             Grup = "1_1 Toroslar 1-2",
                                                             G1Eczane = grouped.Key.G1EczaneAdi,
                                                             G2Eczane = grouped.Key.G2EczaneAdi,
                                                             G1EczaneNobetGrupId = grouped.Key.G1EczaneNobetGrupId,
                                                             G2EczaneNobetGrupId = grouped.Key.G2EczaneNobetGrupId,
                                                             AltGrupAdi = "Kendisi",
                                                             AyniGunNobetSayisi = grouped.Count(),
                                                             Tarih = grouped.Max(m => m.Tarih),
                                                             TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                             //GunGrup = grouped.Key.GunGrup
                                                         }).ToList();

                    var toroslar1_3AyniGunNobetSayisi = (from s in listePivot
                                                         group s by new
                                                         {
                                                             s.G1EczaneAdi,
                                                             s.G3EczaneAdi,
                                                             s.G1EczaneNobetGrupId,
                                                             s.G3EczaneNobetGrupId,
                                                         } into grouped
                                                         where grouped.Count() > 0
                                                         select new AyniGunNobetTutanEczane
                                                         {
                                                             Grup = "1_2 Toroslar 1-Akdeniz 1",
                                                             G1Eczane = grouped.Key.G1EczaneAdi,
                                                             G2Eczane = grouped.Key.G3EczaneAdi,
                                                             G1EczaneNobetGrupId = grouped.Key.G1EczaneNobetGrupId,
                                                             G2EczaneNobetGrupId = grouped.Key.G3EczaneNobetGrupId,
                                                             AltGrupAdi = "Kendisi",
                                                             AyniGunNobetSayisi = grouped.Count(),
                                                             Tarih = grouped.Max(m => m.Tarih),
                                                             TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                             //GunGrup = grouped.Key.GunGrup
                                                         }).ToList();

                    var toroslar2_3AyniGunNobetSayisi = (from s in listePivot
                                                         group s by new
                                                         {
                                                             s.G2EczaneAdi,
                                                             s.G3EczaneAdi,
                                                             s.G2EczaneNobetGrupId,
                                                             s.G3EczaneNobetGrupId,
                                                         } into grouped
                                                         where grouped.Count() > 0
                                                         select new AyniGunNobetTutanEczane
                                                         {
                                                             Grup = "1_3 Toroslar 2-Akdeniz 1",
                                                             G1Eczane = grouped.Key.G2EczaneAdi,
                                                             G2Eczane = grouped.Key.G3EczaneAdi,
                                                             G1EczaneNobetGrupId = grouped.Key.G2EczaneNobetGrupId,
                                                             G2EczaneNobetGrupId = grouped.Key.G3EczaneNobetGrupId,
                                                             AltGrupAdi = "Kendisi",
                                                             AyniGunNobetSayisi = grouped.Count(),
                                                             Tarih = grouped.Max(m => m.Tarih),
                                                             TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                             //GunGrup = grouped.Key.GunGrup
                                                         }).ToList();
                    #endregion

                    #region akdeniz
                    var akdeniz1_2AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new
                                                        {
                                                            s.G3EczaneAdi,
                                                            s.G4EczaneAdi,
                                                            s.G3EczaneNobetGrupId,
                                                            s.G4EczaneNobetGrupId,
                                                        } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "2_1 Akdeniz 1-2",
                                                            G1Eczane = grouped.Key.G3EczaneAdi,
                                                            G2Eczane = grouped.Key.G4EczaneAdi,
                                                            G1EczaneNobetGrupId = grouped.Key.G3EczaneNobetGrupId,
                                                            G2EczaneNobetGrupId = grouped.Key.G4EczaneNobetGrupId,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Max(m => m.Tarih),
                                                            TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var akdeniz2_3AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new
                                                        {
                                                            s.G4EczaneAdi,
                                                            s.G5EczaneAdi,
                                                            s.G4EczaneNobetGrupId,
                                                            s.G5EczaneNobetGrupId,
                                                        } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "2_2 Akdeniz 2-3",
                                                            G1Eczane = grouped.Key.G4EczaneAdi,
                                                            G2Eczane = grouped.Key.G5EczaneAdi,
                                                            G1EczaneNobetGrupId = grouped.Key.G4EczaneNobetGrupId,
                                                            G2EczaneNobetGrupId = grouped.Key.G5EczaneNobetGrupId,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Max(m => m.Tarih),
                                                            TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var akdeniz1_3AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new
                                                        {
                                                            s.G3EczaneAdi,
                                                            s.G5EczaneAdi,
                                                            s.G3EczaneNobetGrupId,
                                                            s.G5EczaneNobetGrupId,
                                                        } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "2_3 Akdeniz 1-3",
                                                            G1Eczane = grouped.Key.G3EczaneAdi,
                                                            G2Eczane = grouped.Key.G5EczaneAdi,
                                                            G1EczaneNobetGrupId = grouped.Key.G3EczaneNobetGrupId,
                                                            G2EczaneNobetGrupId = grouped.Key.G5EczaneNobetGrupId,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Max(m => m.Tarih),
                                                            TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();
                    #endregion

                    #region yenisehir

                    var yenisehir1_2AyniGunNobetSayisi = (from s in listePivot
                                                          group s by new
                                                          {
                                                              s.G6EczaneAdi,
                                                              s.G7EczaneAdi,
                                                              s.G6EczaneNobetGrupId,
                                                              s.G7EczaneNobetGrupId,
                                                              //s.NobetAltGrupAdi,
                                                          } into grouped
                                                          where grouped.Count() > 0
                                                          select new AyniGunNobetTutanEczane
                                                          {
                                                              Grup = "3_1 Yenişehir 1-2",
                                                              G1Eczane = grouped.Key.G6EczaneAdi,
                                                              G2Eczane = grouped.Key.G7EczaneAdi,
                                                              G1EczaneNobetGrupId = grouped.Key.G6EczaneNobetGrupId,
                                                              G2EczaneNobetGrupId = grouped.Key.G7EczaneNobetGrupId,
                                                              //AltGrupAdi = grouped.Key.NobetAltGrupAdi,
                                                              AyniGunNobetSayisi = grouped.Count(),
                                                              Tarih = grouped.Max(m => m.Tarih),
                                                              TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                              //GunGrup = grouped.Key.GunGrup
                                                          }).ToList();

                    var yenisehir3_2AyniGunNobetSayisi = (from s in listePivot
                                                          group s by new
                                                          {
                                                              s.G7EczaneAdi,
                                                              s.G8EczaneAdi,
                                                              s.G7EczaneNobetGrupId,
                                                              s.G8EczaneNobetGrupId,
                                                              //s.NobetAltGrupAdi
                                                          } into grouped
                                                          where grouped.Count() > 0
                                                          select new AyniGunNobetTutanEczane
                                                          {
                                                              Grup = "3_2 Yenişehir 3-2",
                                                              G1Eczane = grouped.Key.G8EczaneAdi,
                                                              G2Eczane = grouped.Key.G7EczaneAdi,
                                                              G1EczaneNobetGrupId = grouped.Key.G7EczaneNobetGrupId,
                                                              G2EczaneNobetGrupId = grouped.Key.G8EczaneNobetGrupId,
                                                              //AltGrupAdi = grouped.Key.NobetAltGrupAdi,
                                                              AyniGunNobetSayisi = grouped.Count(),
                                                              Tarih = grouped.Max(m => m.Tarih),
                                                              TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                              //GunGrup = grouped.Key.GunGrup
                                                          }).ToList();

                    var yenisehir1_3AyniGunNobetSayisi = (from s in listePivot
                                                          group s by new
                                                          {
                                                              s.G6EczaneAdi,
                                                              s.G8EczaneAdi,
                                                              s.G6EczaneNobetGrupId,
                                                              s.G8EczaneNobetGrupId,
                                                          } into grouped
                                                          where grouped.Count() > 0
                                                          select new AyniGunNobetTutanEczane
                                                          {
                                                              Grup = "3_3 Yenişehir 1-3",
                                                              G1Eczane = grouped.Key.G6EczaneAdi,
                                                              G2Eczane = grouped.Key.G8EczaneAdi,
                                                              G1EczaneNobetGrupId = grouped.Key.G6EczaneNobetGrupId,
                                                              G2EczaneNobetGrupId = grouped.Key.G8EczaneNobetGrupId,
                                                              AltGrupAdi = "Kendisi",
                                                              AyniGunNobetSayisi = grouped.Count(),
                                                              Tarih = grouped.Max(m => m.Tarih),
                                                              TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                              //GunGrup = grouped.Key.GunGrup
                                                          }).ToList();
                    #endregion

                    #region mezitli
                    var mezitli1_2AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new
                                                        {
                                                            s.G9EczaneAdi,
                                                            s.G10EczaneAdi,
                                                            s.G9EczaneNobetGrupId,
                                                            s.G10EczaneNobetGrupId,
                                                        } into grouped
                                                        where grouped.Count() > 0
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "4 Mezitli 1-2",
                                                            G1Eczane = grouped.Key.G9EczaneAdi,
                                                            G2Eczane = grouped.Key.G10EczaneAdi,
                                                            G1EczaneNobetGrupId = grouped.Key.G9EczaneNobetGrupId,
                                                            G2EczaneNobetGrupId = grouped.Key.G10EczaneNobetGrupId,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Max(m => m.Tarih),
                                                            TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();
                    #endregion

                    ayniGunNobetSayisi = toroslar1_2AyniGunNobetSayisi
                                  .Union(toroslar1_3AyniGunNobetSayisi)
                                  .Union(toroslar2_3AyniGunNobetSayisi)
                                  .Union(akdeniz1_2AyniGunNobetSayisi)
                                  .Union(akdeniz2_3AyniGunNobetSayisi)
                                  .Union(akdeniz1_3AyniGunNobetSayisi)

                                  .Union(yenisehir1_2AyniGunNobetSayisi)
                                  .Union(yenisehir3_2AyniGunNobetSayisi)
                                  .Union(yenisehir1_3AyniGunNobetSayisi)

                                  .Union(mezitli1_2AyniGunNobetSayisi)
                                  .ToList();
                }
                else if (nobetUstGrupId == 8)
                {
                    var listePivot = (from s in ayniGunNobetTutanEczaneler
                                      group s by new { s.TakvimId, s.Tarih, s.GunGrup } into grouped
                                      select new
                                      {
                                          grouped.Key.TakvimId,
                                          grouped.Key.Tarih,
                                          //grouped.Key.GunGrup,
                                          Y1EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneAdi).FirstOrDefault(), //1
                                          Y2EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneAdi).FirstOrDefault(), //2
                                          Y3EczaneAdi = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneAdi).FirstOrDefault(), //3                                        

                                          Y1EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[0]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //1
                                          Y2EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[1]).Select(s => s.EczaneNobetGrupId).FirstOrDefault(), //2
                                          Y3EczaneNobetGrupId = grouped.Where(w => w.NobetGrupId == nobetGrupIds[2]).Select(s => s.EczaneNobetGrupId).FirstOrDefault() //3      
                                      }).ToList();

                    var iskenderun1_2AyniGunNobetSayisi = (from s in listePivot
                                                           group s by new
                                                           {
                                                               s.Y1EczaneAdi,
                                                               s.Y2EczaneAdi,
                                                               s.Y1EczaneNobetGrupId,
                                                               s.Y2EczaneNobetGrupId,
                                                           } into grouped
                                                           where grouped.Count() > 0
                                                           select new AyniGunNobetTutanEczane
                                                           {
                                                               Grup = "İskenderun 1-2",
                                                               G1Eczane = grouped.Key.Y1EczaneAdi,
                                                               G2Eczane = grouped.Key.Y2EczaneAdi,
                                                               G1EczaneNobetGrupId = grouped.Key.Y1EczaneNobetGrupId,
                                                               G2EczaneNobetGrupId = grouped.Key.Y2EczaneNobetGrupId,
                                                               AltGrupAdi = "Kendisi",
                                                               AyniGunNobetSayisi = grouped.Count(),
                                                               Tarih = grouped.Max(m => m.Tarih),
                                                               TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                               //GunGrup = grouped.Key.GunGrup
                                                           }).ToList();

                    var iskenderun1_3AyniGunNobetSayisi = (from s in listePivot
                                                           group s by new
                                                           {
                                                               s.Y1EczaneAdi,
                                                               s.Y3EczaneAdi,
                                                               s.Y1EczaneNobetGrupId,
                                                               s.Y3EczaneNobetGrupId,
                                                           } into grouped
                                                           where grouped.Count() > 0
                                                           select new AyniGunNobetTutanEczane
                                                           {
                                                               Grup = "İskenderun 1-3",
                                                               G1Eczane = grouped.Key.Y1EczaneAdi,
                                                               G2Eczane = grouped.Key.Y3EczaneAdi,
                                                               G1EczaneNobetGrupId = grouped.Key.Y1EczaneNobetGrupId,
                                                               G2EczaneNobetGrupId = grouped.Key.Y3EczaneNobetGrupId,
                                                               AltGrupAdi = "Kendisi",
                                                               AyniGunNobetSayisi = grouped.Count(),
                                                               Tarih = grouped.Max(m => m.Tarih),
                                                               TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                               //GunGrup = grouped.Key.GunGrup
                                                           }).ToList();

                    var iskenderun2_3AyniGunNobetSayisi = (from s in listePivot
                                                           group s by new
                                                           {
                                                               s.Y2EczaneAdi,
                                                               s.Y3EczaneAdi,
                                                               s.Y2EczaneNobetGrupId,
                                                               s.Y3EczaneNobetGrupId,
                                                           } into grouped
                                                           where grouped.Count() > 0
                                                           select new AyniGunNobetTutanEczane
                                                           {
                                                               Grup = "İskenderun 2-3",
                                                               G1Eczane = grouped.Key.Y2EczaneAdi,
                                                               G2Eczane = grouped.Key.Y3EczaneAdi,
                                                               G1EczaneNobetGrupId = grouped.Key.Y2EczaneNobetGrupId,
                                                               G2EczaneNobetGrupId = grouped.Key.Y3EczaneNobetGrupId,
                                                               AltGrupAdi = "Kendisi",
                                                               AyniGunNobetSayisi = grouped.Count(),
                                                               Tarih = grouped.Max(m => m.Tarih),
                                                               TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                               //GunGrup = grouped.Key.GunGrup
                                                           }).ToList();

                    ayniGunNobetSayisi = iskenderun1_2AyniGunNobetSayisi
                        .Union(iskenderun1_3AyniGunNobetSayisi)
                        .Union(iskenderun2_3AyniGunNobetSayisi)
                        .ToList();
                }
            }

            return ayniGunNobetSayisi;
        }
        public List<AyniGunNobetTutanEczane> GetAyniGunNobetTutanEczanelerGiresun(List<EczaneNobetSonucListe2> ayniGunNobetTutanEczaneler, List<EczaneGrupDetay> eczaneGrupDetaylar)
        {
            var nobetGrupNobetAltGrupEslemeli = (from a in ayniGunNobetTutanEczaneler
                                                 from g in eczaneGrupDetaylar
                                                 where a.EczaneNobetGrupId == g.EczaneNobetGrupId
                                                 select new
                                                 {
                                                     a.NobetUstGrupId,
                                                     a.NobetGrupId,
                                                     a.NobetGrupAdi,
                                                     g.EczaneGrupTanimId,
                                                     g.EczaneGrupTanimAdi
                                                 }).Distinct().ToList();

            var nobetUstGrupId = nobetGrupNobetAltGrupEslemeli.Select(s => s.NobetUstGrupId).FirstOrDefault();
            var EczaneGrupTanimIds = nobetGrupNobetAltGrupEslemeli.Select(s => s.EczaneGrupTanimId).OrderBy(o => o).ToArray();

            var ayniGunNobetSayisi = new List<AyniGunNobetTutanEczane>();
            if (EczaneGrupTanimIds.Count() > 0)
            {
                if (nobetUstGrupId == 4)
                {
                    var listePivot = (from a in ayniGunNobetTutanEczaneler
                                      from g in eczaneGrupDetaylar
                                      where a.EczaneNobetGrupId == g.EczaneNobetGrupId
                                      group new { a, g } by new
                                      {
                                          a.TakvimId,
                                          a.Tarih,
                                          a.GunGrup
                                      } into grouped
                                      select new
                                      {
                                          grouped.Key.TakvimId,
                                          grouped.Key.Tarih,
                                          grouped.Key.GunGrup,
                                          Y1EczaneAdi = grouped.Where(w => w.g.EczaneGrupTanimId == EczaneGrupTanimIds[0]).Select(s => s.a.EczaneAdi).FirstOrDefault(), //1
                                          Y2EczaneAdi = grouped.Where(w => w.g.EczaneGrupTanimId == EczaneGrupTanimIds[1]).Select(s => s.a.EczaneAdi).FirstOrDefault(), //2
                                          Y3EczaneAdi = grouped.Where(w => w.g.EczaneGrupTanimId == EczaneGrupTanimIds[2]).Select(s => s.a.EczaneAdi).FirstOrDefault(), //3                                        

                                          Y1EczaneNobetGrupId = grouped.Where(w => w.g.EczaneGrupTanimId == EczaneGrupTanimIds[0]).Select(s => s.a.EczaneNobetGrupId).FirstOrDefault(), //1
                                          Y2EczaneNobetGrupId = grouped.Where(w => w.g.EczaneGrupTanimId == EczaneGrupTanimIds[1]).Select(s => s.a.EczaneNobetGrupId).FirstOrDefault(), //2
                                          Y3EczaneNobetGrupId = grouped.Where(w => w.g.EczaneGrupTanimId == EczaneGrupTanimIds[2]).Select(s => s.a.EczaneNobetGrupId).FirstOrDefault()  //3      
                                      }).ToList();

                    var giresun1_2AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new
                                                        {
                                                            s.Y1EczaneAdi,
                                                            s.Y2EczaneAdi,
                                                            s.Y1EczaneNobetGrupId,
                                                            s.Y2EczaneNobetGrupId,
                                                            s.GunGrup
                                                        } into grouped
                                                        where grouped.Count() > 0
                                                        && (grouped.Key.Y1EczaneAdi != null && grouped.Key.Y2EczaneAdi != null)
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "Giresun 1-2",
                                                            G1Eczane = grouped.Key.Y1EczaneAdi,
                                                            G2Eczane = grouped.Key.Y2EczaneAdi,
                                                            G1EczaneNobetGrupId = grouped.Key.Y1EczaneNobetGrupId,
                                                            G2EczaneNobetGrupId = grouped.Key.Y2EczaneNobetGrupId,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Max(m => m.Tarih),
                                                            TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var giresun1_3AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new
                                                        {
                                                            s.Y1EczaneAdi,
                                                            s.Y3EczaneAdi,
                                                            s.Y1EczaneNobetGrupId,
                                                            s.Y3EczaneNobetGrupId,
                                                            s.GunGrup
                                                        } into grouped
                                                        where grouped.Count() > 0
                                                        && (grouped.Key.Y1EczaneAdi != null && grouped.Key.Y3EczaneAdi != null)
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "Giresun 1-3",
                                                            G1Eczane = grouped.Key.Y1EczaneAdi,
                                                            G2Eczane = grouped.Key.Y3EczaneAdi,
                                                            G1EczaneNobetGrupId = grouped.Key.Y1EczaneNobetGrupId,
                                                            G2EczaneNobetGrupId = grouped.Key.Y3EczaneNobetGrupId,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Max(m => m.Tarih),
                                                            TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    var giresun2_3AyniGunNobetSayisi = (from s in listePivot
                                                        group s by new
                                                        {
                                                            s.Y2EczaneAdi,
                                                            s.Y3EczaneAdi,
                                                            s.Y2EczaneNobetGrupId,
                                                            s.Y3EczaneNobetGrupId,
                                                            s.GunGrup
                                                        } into grouped
                                                        where grouped.Count() > 0
                                                        && (grouped.Key.Y2EczaneAdi != null && grouped.Key.Y3EczaneAdi != null)
                                                        select new AyniGunNobetTutanEczane
                                                        {
                                                            Grup = "Giresun 2-3",
                                                            G1Eczane = grouped.Key.Y2EczaneAdi,
                                                            G2Eczane = grouped.Key.Y3EczaneAdi,
                                                            G1EczaneNobetGrupId = grouped.Key.Y2EczaneNobetGrupId,
                                                            G2EczaneNobetGrupId = grouped.Key.Y3EczaneNobetGrupId,
                                                            AltGrupAdi = "Kendisi",
                                                            AyniGunNobetSayisi = grouped.Count(),
                                                            Tarih = grouped.Max(m => m.Tarih),
                                                            TakvimId = grouped.SingleOrDefault(m => m.Tarih == grouped.Max(c => c.Tarih)).TakvimId,
                                                            //GunGrup = grouped.Key.GunGrup
                                                        }).ToList();

                    ayniGunNobetSayisi = giresun1_2AyniGunNobetSayisi
                        .Union(giresun1_3AyniGunNobetSayisi)
                        .Union(giresun2_3AyniGunNobetSayisi)
                        .ToList();
                }
            }

            return ayniGunNobetSayisi;
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
                   g.NobetGrupGorevTipBaslamaTarihi,
                   g.NobetUstGrupId,
                   g.NobetGrupGorevTipId
               })
               .Select(s => new EczaneNobetGrupGunKuralIstatistik
               {
                   NobetGrupGorevTipId = s.Key.NobetGrupGorevTipId,
                   NobetUstGrupId = s.Key.NobetUstGrupId,
                   GunGrup = s.Key.GunGrup,
                   NobetGunKuralId = s.Key.NobetGunKuralId,
                   EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                   EczaneNobetGrupBaslamaTarihi = s.Key.EczaneNobetGrupBaslamaTarihi,
                   NobetGrupGorevTipBaslamaTarihi = s.Key.NobetGrupGorevTipBaslamaTarihi,
                   EczaneId = s.Key.EczaneId,
                   EczaneAdi = s.Key.EczaneAdi,
                   NobetGrupId = s.Key.NobetGrupId,
                   NobetGrupAdi = s.Key.NobetGrupAdi,
                   NobetGorevTipId = s.Key.NobetGorevTipId,
                   IlkNobetTarihi = s.Min(c => c.Tarih),
                   SonNobetTarihi = s.Max(c => c.Tarih),
                   NobetSayisi = s.Count(),
                   NobetSayisiGercek = s.Count(c => c.Tarih >= c.NobetGrupGorevTipBaslamaTarihi //new DateTime(2018, 6, 1)
                   ),
               }).ToList();

            //var eczane = liste.Where(w => w.EczaneAdi == "EFENDİOĞLU").ToList();

            return liste;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AnahtarListe> GetEczaneNobetIstatistik(List<EczaneNobetSonucListe2> eczaneNobetSonuc)
        {
            var anahtarListe = new List<AnahtarListe>();

            var eczaneNobetSonucSirali = eczaneNobetSonuc.OrderBy(o => o.EczaneNobetGrupId).ThenBy(o => o.Tarih).ToList();


            foreach (var eczaneNobet in eczaneNobetSonucSirali)
            {
                var gunGruplar = eczaneNobetSonuc.Select(s => new { s.GunGrupId, s.GunGrup }).Distinct().ToList();

                anahtarListe.Add(new AnahtarListe
                {
                    EczaneNobetGrupId = eczaneNobet.EczaneNobetGrupId,
                    Tarih = eczaneNobet.Tarih,
                    EczaneAdi = eczaneNobet.EczaneAdi,
                    EczaneNobetGrupBaslamaTarihi = eczaneNobet.EczaneNobetGrupBaslamaTarihi,
                    GunGrup = eczaneNobet.GunGrup,
                    NobetGrupAdi = eczaneNobet.NobetGrupAdi,
                    NobetGrupId = eczaneNobet.NobetGrupId,
                    NobetSayisi = 0
                });
            }
            //var eczane = liste.Where(w => w.EczaneAdi == "EFENDİOĞLU").ToList();

            return anahtarListe;
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
                    s.NobetGrupGorevTipId,
                    s.GunGrup
                })
                .Distinct()
                .OrderBy(o => o.NobetGorevTipId)
                .ThenBy(t => t.NobetGunKuralId).ToList();

            var varsayilanBaslangicNobetTarihi = new DateTime(2008, 1, 15);

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
                            NobetGrupGorevTipId = eczaneNobetGrup.NobetGrupGorevTipId,
                            EczaneNobetGrupId = eczaneNobetGrup.Id,
                            IlkNobetTarihi = varsayilanBaslangicNobetTarihi,
                            SonNobetTarihi = varsayilanBaslangicNobetTarihi,
                            NobetGorevTipId = nobetGunKural.NobetGorevTipId,
                            NobetGunKuralId = nobetGunKural.NobetGunKuralId,
                            GunGrup = nobetGunKural.GunGrup,
                            NobetGrupId = eczaneNobetGrup.NobetGrupId,
                            NobetSayisi = 1,
                            EczaneNobetGrupBaslamaTarihi = eczaneNobetGrup.BaslangicTarihi,
                            NobetUstGrupId = eczaneNobetGrup.NobetUstGrupId,
                            NobetGrupGorevTipBaslamaTarihi = eczaneNobetGrup.NobetGrupGorevTipBaslamaTarihi
                        });
                    }
                }
            }

            var eczaneTumu = enSonNobetler.Where(w => w.EczaneAdi == "ADA").ToList();

            return enSonNobetler;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupGunKuralIstatistikYatay> GetEczaneNobetGrupGunKuralIstatistikYatay(List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistik)
        {
            //var a = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetGorevTipId).Distinct().ToList();
            //var eczaneNobetGruplar = eczaneNobetGrupGunKuralIstatistik.Select(s => s.EczaneNobetGrupId).Distinct().ToList();
            //var nobetAltGruplar = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetAltGrupId).Distinct().ToList();
            //var NobetGorevTiler = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetGorevTipId).Distinct().ToList();
            //var nobetGunKurallar = eczaneNobetGrupGunKuralIstatistik.Select(w => w.NobetGunKuralId).Distinct().ToList();
            //var eczane = eczaneNobetGrupGunKuralIstatistik.Where(w => w.NobetGunKuralId == 12).ToList();
            //var eczane2 = eczane.Where(w => w.EczaneAdi == "TUNA").ToList();

            ;
            var nobetUstGrupId = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetUstGrupId).Distinct().SingleOrDefault();

            var eczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistik
               .GroupBy(g => new
               {
                   g.EczaneNobetGrupId,
                   //g.EczaneNobetGrupBaslamaTarihi,
                   g.NobetGrupGorevTipId,
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
                   NobetGrupGorevTipId = s.Key.NobetGrupGorevTipId,
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

                   NobetSayisiBayram = s.Where(w => w.GunGrup == "Bayram").Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihiBayram = s.Where(w => w.GunGrup == "Bayram").Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.GunGrup == "Bayram").Max(f => f.SonNobetTarihi)
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
                   SonNobetTarihiDiniBayram = s.Where(w => w.NobetGunKuralId == 8).Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.NobetGunKuralId == 8).Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiMilliBayram = s.Where(w => w.NobetGunKuralId == 9).Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihiMilliBayram = s.Where(w => w.NobetGunKuralId == 9).Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.NobetGunKuralId == 9).Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisi1Ocak = s.Where(w => w.NobetGunKuralId == 12).Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihi1Ocak = s.Where(w => w.NobetGunKuralId == 12).Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.NobetGunKuralId == 12).Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiYilSonu = s.Where(w => w.NobetGunKuralId == 11).Sum(f => f.NobetSayisiGercek),
                   SonNobetTarihiYilSonu = s.Where(w => w.NobetGunKuralId == 11).Sum(f => f.NobetSayisi) > 0
                       ? s.Where(w => w.NobetGunKuralId == 11).Max(f => f.SonNobetTarihi)
                       : new DateTime(2010, 1, 1), //s.Key.EczaneNobetGrupBaslamaTarihi,
               }).ToList();

            var eczane = eczaneNobetGrupGunKuralIstatistikYatay.Where(w => w.EczaneAdi == "ADA").ToList();

            return eczaneNobetGrupGunKuralIstatistikYatay;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupGunKuralIstatistikYatay> GetEczaneNobetGrupGunKuralIstatistikYatay(List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistik,
            List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistikPlanlanan)
        {
            //var nobetUstGrupId = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetUstGrupId).Distinct().SingleOrDefault();
            //var eczaneGercek = eczaneNobetGrupGunKuralIstatistik.Where(w => w.EczaneAdi == "EFENDİOĞLU").ToList();
            //var eczanePlanlanan = eczaneNobetGrupGunKuralIstatistikPlanlanan.Where(w => w.EczaneAdi == "EFENDİOĞLU").ToList();

            var sonuclar = (from s in eczaneNobetGrupGunKuralIstatistik
                            from p in eczaneNobetGrupGunKuralIstatistikPlanlanan
                            where s.EczaneNobetGrupId == p.EczaneNobetGrupId
                            && s.NobetGunKuralId == p.NobetGunKuralId
                            //&& s.NobetSayisi == s.NobetSayisi
                            select new { s, p }).ToList();
            ;
            var eczaneNobetGrupGunKuralIstatistikYatay = sonuclar
               .GroupBy(g => new
               {
                   g.s.EczaneNobetGrupId,
                   //g.EczaneNobetGrupBaslamaTarihi,
                   g.s.EczaneId,
                   g.s.EczaneAdi,
                   g.s.NobetGrupAdi,
                   g.s.NobetGrupId,
                   g.s.NobetGorevTipId,
                   g.s.NobetAltGrupId
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

                   NobetSayisiToplam = s.Sum(f => f.s.NobetSayisiGercek),
                   SonNobetTarihi = s.Sum(f => f.s.NobetSayisi) > 0
                       ? s.Max(f => f.s.SonNobetTarihi)
                       : s.Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiCumartesi = s.Where(w => w.s.NobetGunKuralId == 7).Sum(f => f.s.NobetSayisiGercek),
                   SonNobetTarihiCumartesi = s.Where(w => w.s.NobetGunKuralId == 7).Sum(f => f.s.NobetSayisiGercek) > 0
                       ? s.Where(w => w.s.NobetGunKuralId == 7).Max(f => f.s.SonNobetTarihi)
                       : s.Where(w => w.p.NobetGunKuralId == 7).Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiPazar = s.Where(w => w.s.NobetGunKuralId == 1).Sum(f => f.s.NobetSayisiGercek),
                   SonNobetTarihiPazar = s.Where(w => w.s.NobetGunKuralId == 1).Sum(f => f.s.NobetSayisiGercek) > 0
                       ? s.Where(w => w.s.NobetGunKuralId == 1).Max(f => f.s.SonNobetTarihi)
                       : s.Where(w => w.p.NobetGunKuralId == 1).Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiBayram = s.Where(w => w.s.GunGrup == "Bayram").Sum(f => f.s.NobetSayisiGercek),
                   SonNobetTarihiBayram = s.Where(w => w.s.GunGrup == "Bayram").Sum(f => f.s.NobetSayisiGercek) > 0
                       ? s.Where(w => w.s.GunGrup == "Bayram").Max(f => f.s.SonNobetTarihi)
                       : s.Where(w => w.p.GunGrup == "Bayram").Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiArife = s.Where(w => w.s.NobetGunKuralId == 10).Sum(f => f.s.NobetSayisiGercek),
                   SonNobetTarihiArife = s.Where(w => w.s.NobetGunKuralId == 10).Sum(f => f.s.NobetSayisi) > 0
                       ? s.Where(w => w.s.NobetGunKuralId == 10).Max(f => f.s.SonNobetTarihi)
                       : s.Where(w => w.p.NobetGunKuralId == 10).Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiHaftaIci = s.Where(w => w.s.GunGrup == "Hafta İçi").Sum(f => f.s.NobetSayisiGercek),
                   SonNobetTarihiHaftaIci = s.Where(w => w.s.GunGrup == "Hafta İçi").Sum(f => f.s.NobetSayisiGercek) > 0
                       ? s.Where(w => w.s.GunGrup == "Hafta İçi").Max(f => f.s.SonNobetTarihi)
                       : s.Where(w => w.p.GunGrup == "Hafta İçi").Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiPazartesi = s.Where(w => w.s.NobetGunKuralId == 2).Sum(f => f.s.NobetSayisiGercek),
                   NobetSayisiSali = s.Where(w => w.s.NobetGunKuralId == 3).Sum(f => f.s.NobetSayisiGercek),
                   NobetSayisiCarsamba = s.Where(w => w.s.NobetGunKuralId == 4).Sum(f => f.s.NobetSayisiGercek),
                   NobetSayisiPersembe = s.Where(w => w.s.NobetGunKuralId == 5).Sum(f => f.s.NobetSayisiGercek),
                   NobetSayisiCuma = s.Where(w => w.s.NobetGunKuralId == 6).Sum(f => f.s.NobetSayisiGercek),
                   NobetSayisiDiniBayram = s.Where(w => w.s.NobetGunKuralId == 8).Sum(f => f.s.NobetSayisiGercek),
                   SonNobetTarihiDiniBayram = s.Where(w => w.s.NobetGunKuralId == 8).Sum(f => f.s.NobetSayisiGercek) > 0
                       ? s.Where(w => w.s.NobetGunKuralId == 8).Max(f => f.s.SonNobetTarihi)
                       : s.Where(w => w.p.NobetGunKuralId == 8).Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   NobetSayisiMilliBayram = s.Where(w => w.s.NobetGunKuralId == 9).Sum(f => f.s.NobetSayisiGercek),
                   SonNobetTarihiMilliBayram = s.Where(w => w.s.NobetGunKuralId == 9).Sum(f => f.s.NobetSayisiGercek) > 0
                       ? s.Where(w => w.s.NobetGunKuralId == 9).Max(f => f.s.SonNobetTarihi)
                       : s.Where(w => w.p.NobetGunKuralId == 9).Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   //NobetSayisi1Ocak = s.Where(w => w.s.NobetGunKuralId == 12).Sum(f => f.s.NobetSayisiGercek),
                   //SonNobetTarihi1Ocak = s.Where(w => w.s.NobetGunKuralId == 12).Sum(f => f.s.NobetSayisi) > 0
                   //    ? s.Where(w => w.s.NobetGunKuralId == 12).Max(f => f.s.SonNobetTarihi)
                   //    : s.Where(w => w.p.NobetGunKuralId == 12).Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,

                   //NobetSayisiYilSonu = s.Where(w => w.s.NobetGunKuralId == 11).Sum(f => f.s.NobetSayisiGercek),
                   //SonNobetTarihiYilSonu = s.Where(w => w.s.NobetGunKuralId == 11).Sum(f => f.s.NobetSayisi) > 0
                   //    ? s.Where(w => w.s.NobetGunKuralId == 11).Max(f => f.s.SonNobetTarihi)
                   //    : s.Where(w => w.p.NobetGunKuralId == 11).Max(f => f.p.SonNobetTarihi), //s.Key.EczaneNobetGrupBaslamaTarihi,
               }).ToList();

            //var eczane = eczaneNobetGrupGunKuralIstatistikYatay.Where(w => w.EczaneAdi == "EFENDİOĞLU").ToList();

            return eczaneNobetGrupGunKuralIstatistikYatay;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetAlacakVerecek> GetEczaneNobetGrupGunKuralIstatistikYatay(
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistik,
            List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistikPlanlanan)
        {
            //var nobetUstGrupId = eczaneNobetGrupGunKuralIstatistik.Select(s => s.NobetUstGrupId).Distinct().SingleOrDefault();
            //var eczaneGercek = eczaneNobetGrupGunKuralIstatistik.Where(w => w.EczaneAdi == "EFENDİOĞLU").ToList();
            //var eczanePlanlanan = eczaneNobetGrupGunKuralIstatistikPlanlanan.Where(w => w.EczaneAdi == "EFENDİOĞLU").ToList();

            var sonuclar = (from s in eczaneNobetGrupGunKuralIstatistik
                            from b in eczaneNobetGrupGunKuralIstatistikPlanlanan
                            where s.EczaneNobetGrupId == b.EczaneNobetGrupId
                            && s.NobetSayisiHaftaIci == b.NobetSayisi
                            //&& s.NobetSayisi == s.NobetSayisi
                            select new EczaneNobetAlacakVerecek
                            {
                                EczaneNobetGrupId = s.EczaneNobetGrupId,
                                EczaneId = s.EczaneId,
                                EczaneAdi = s.EczaneAdi,
                                NobetGrupAdi = s.NobetGrupAdi,
                                NobetGrupId = s.NobetGrupId,
                                NobetSayisi = s.NobetSayisiHaftaIci,
                                SonNobetTarihi = s.SonNobetTarihiHaftaIci,
                                AnahtarTarih = b.SonNobetTarihi,
                                BorcluGunSayisi = s.NobetSayisiHaftaIci > 0
                                                            ? Convert.ToInt32((s.SonNobetTarihiHaftaIci - b.SonNobetTarihi).TotalDays)
                                                            : Convert.ToInt32((s.SonNobetTarihiHaftaIci - b.SonNobetTarihi).TotalDays - (s.SonNobetTarihiHaftaIci - new DateTime(2018, 6, 1)).TotalDays),
                                GunGrup = "Hafta İçi"
                            }).ToList();

            //var eczane = eczaneNobetGrupGunKuralIstatistikYatay.Where(w => w.EczaneAdi == "EFENDİOĞLU").ToList();

            return sonuclar;
        }

        /// <summary>
        /// İstenen bir gruptaki eczaneler en son hangi alt grupla nöbet tuttuysa 
        /// çözülen ayda aynı alt grupla aynı gün nöbet yazılmaz.
        /// </summary>
        /// <param name="eczaneNobetTarihAralik"></param>
        /// <param name="eczaneNobetSonuclar"></param>
        /// <param name="eczaneNobetGruplar"></param>
        /// <param name="eczaneNobetGrupAltGruplar"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="nobetUstGrupBaslamaTarihi"></param>
        /// <param name="indisId"></param>
        /// <returns></returns>
        public virtual List<EczaneGrupDetay> AltGruplarlaSiraliNobetListesiniOlustur(
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            DateTime nobetUstGrupBaslamaTarihi,
            int indisId)
        {//ay içinde gün grubu bazında birden fazla nöbet düşerse bu kısıt en son nöbet tuttuğu alt grubun dışındaki ile 2 kez nöbet tutabilir. buna dikkat.
            var eczaneGruplar = new List<EczaneGrupDetay>();

            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var ayniGunNobetTutmasiTakipEdilecekGruplar = new List<int>
                {
                    13//Antalya-10
                };

                var nobetAltGrubuOlmayanlarinEczaneleri = eczaneNobetGruplar //data.EczaneNobetGruplar
                    .Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.NobetGrupId)).ToList();

                var altGrubuOlanNobetGruplar = new List<int>
                {
                     14//Antalya-11
                };

                //tüm liste
                var ayniGunNobetTutmasiTakipEdilecekGruplarTumu = new List<int>();

                ayniGunNobetTutmasiTakipEdilecekGruplarTumu.AddRange(ayniGunNobetTutmasiTakipEdilecekGruplar);
                ayniGunNobetTutmasiTakipEdilecekGruplarTumu.AddRange(altGrubuOlanNobetGruplar);

                //alt grubu olmayanlar
                var nobetAltGrubuOlmayanlarinSonuclari = eczaneNobetSonuclar
                    .Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.NobetGrupId)
                             && w.Tarih >= nobetUstGrupBaslamaTarihi).ToList();

                //alt grubu olanlar
                var nobetAltGrubuOlanlarinSonuclari = eczaneNobetSonuclar
                    .Where(w => altGrubuOlanNobetGruplar.Contains(w.NobetGrupId)).ToList();

                var ayniGunAnahtarListe = new List<AltGrupIleAyniGunNobetDurumu>
                        {
                            #region pazar
		                    new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "SİBEL", EczaneNobetGrupIdAltGrubuOlmayan = 412, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "SEVGİ", EczaneNobetGrupIdAltGrubuOlmayan = 416, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "MERKEZ", EczaneNobetGrupIdAltGrubuOlmayan = 427, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "EDA", EczaneNobetGrupIdAltGrubuOlmayan = 425, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "DEMİRGÜL", EczaneNobetGrupIdAltGrubuOlmayan = 435, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "ASİL", EczaneNobetGrupIdAltGrubuOlmayan = 424, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "AKYILDIZ", EczaneNobetGrupIdAltGrubuOlmayan = 429, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "TURGAY", EczaneNobetGrupIdAltGrubuOlmayan = 418, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "YURTPINAR UĞUR", EczaneNobetGrupIdAltGrubuOlmayan = 436, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "SAKARYA", EczaneNobetGrupIdAltGrubuOlmayan = 433, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "HAZAL", EczaneNobetGrupIdAltGrubuOlmayan = 415, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "YÜCEL", EczaneNobetGrupIdAltGrubuOlmayan = 437, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "YILDIZ RÜYA", EczaneNobetGrupIdAltGrubuOlmayan = 423, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "NEZİH", EczaneNobetGrupIdAltGrubuOlmayan = 422, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "SEZER", EczaneNobetGrupIdAltGrubuOlmayan = 421, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "YEŞİLIRMAK", EczaneNobetGrupIdAltGrubuOlmayan = 419, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "AYLİN", EczaneNobetGrupIdAltGrubuOlmayan = 417, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "İKRA", EczaneNobetGrupIdAltGrubuOlmayan = 420, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "UTKU", EczaneNobetGrupIdAltGrubuOlmayan = 413, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "BABACAN", EczaneNobetGrupIdAltGrubuOlmayan = 434, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "CANAN", EczaneNobetGrupIdAltGrubuOlmayan = 426, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "FREZYA", EczaneNobetGrupIdAltGrubuOlmayan = 414, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "LEVENT", EczaneNobetGrupIdAltGrubuOlmayan = 431, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "BİLGE", EczaneNobetGrupIdAltGrubuOlmayan = 430, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "NİLAY", EczaneNobetGrupIdAltGrubuOlmayan = 438, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Pazar", EczaneAdiAltGrubuOlmayan = "EMEK", EczaneNobetGrupIdAltGrubuOlmayan = 428, NobetAltGrupId = 5}, 
	                        #endregion

                            #region bayram
		                    new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "NİLAY", EczaneNobetGrupIdAltGrubuOlmayan = 438, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "FREZYA", EczaneNobetGrupIdAltGrubuOlmayan = 414, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "YEŞİLIRMAK", EczaneNobetGrupIdAltGrubuOlmayan = 419, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "EDA", EczaneNobetGrupIdAltGrubuOlmayan = 425, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "SAKARYA", EczaneNobetGrupIdAltGrubuOlmayan = 433, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "AYLİN", EczaneNobetGrupIdAltGrubuOlmayan = 417, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "EMEK", EczaneNobetGrupIdAltGrubuOlmayan = 428, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "BİLGE", EczaneNobetGrupIdAltGrubuOlmayan = 430, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "BABACAN", EczaneNobetGrupIdAltGrubuOlmayan = 434, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "YILDIZ RÜYA", EczaneNobetGrupIdAltGrubuOlmayan = 423, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "İKRA", EczaneNobetGrupIdAltGrubuOlmayan = 420, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "YÜCEL", EczaneNobetGrupIdAltGrubuOlmayan = 437, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "NEZİH", EczaneNobetGrupIdAltGrubuOlmayan = 422, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "SEVGİ", EczaneNobetGrupIdAltGrubuOlmayan = 416, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "AKYILDIZ", EczaneNobetGrupIdAltGrubuOlmayan = 429, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "ASİL", EczaneNobetGrupIdAltGrubuOlmayan = 424, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "MERKEZ", EczaneNobetGrupIdAltGrubuOlmayan = 427, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "SEZER", EczaneNobetGrupIdAltGrubuOlmayan = 421, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "UTKU", EczaneNobetGrupIdAltGrubuOlmayan = 413, NobetAltGrupId = 4},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "SİBEL", EczaneNobetGrupIdAltGrubuOlmayan = 412, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "CANAN", EczaneNobetGrupIdAltGrubuOlmayan = 426, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "DEMİRGÜL", EczaneNobetGrupIdAltGrubuOlmayan = 435, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "YURTPINAR UĞUR", EczaneNobetGrupIdAltGrubuOlmayan = 436, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "TURGAY", EczaneNobetGrupIdAltGrubuOlmayan = 418, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "HAZAL", EczaneNobetGrupIdAltGrubuOlmayan = 415, NobetAltGrupId = 5},
                            new AltGrupIleAyniGunNobetDurumu{ GunGrup="Bayram", EczaneAdiAltGrubuOlmayan = "LEVENT", EczaneNobetGrupIdAltGrubuOlmayan = 431, NobetAltGrupId = 4} 
	                        #endregion
                        };

                var gunGruplar = nobetAltGrubuOlanlarinSonuclari
                    .Select(s => new { s.GunGrupId, s.GunGrup }).Distinct()
                    .OrderBy(o => o.GunGrupId).ToList();

                foreach (var gunGrup in gunGruplar)
                {
                    var nobetAltGrubuOlmayanlarinSonuclariGunGruplu = nobetAltGrubuOlmayanlarinSonuclari.Where(w => w.GunGrupId == gunGrup.GunGrupId).ToList();
                    var nobetAltGrubuOlanlarinSonuclariGunGruplu = nobetAltGrubuOlanlarinSonuclari.Where(w => w.GunGrupId == gunGrup.GunGrupId).ToList();
                    var ayniGunAnahtarListeGunGruplu = ayniGunAnahtarListe.Where(w => w.GunGrup == gunGrup.GunGrup).ToList();

                    foreach (var eczane in nobetAltGrubuOlmayanlarinEczaneleri)
                    {
                        #region kontrol

                        var kontrol = false;

                        var kontrolEdilecekEczaneler = new string[] { "AKYILDIZ" };

                        if (kontrol && kontrolEdilecekEczaneler.Contains(eczane.EczaneAdi))
                        {
                        }
                        #endregion

                        var bakilanEczaneninSonuclari = nobetAltGrubuOlmayanlarinSonuclariGunGruplu
                            .Where(w => w.EczaneNobetGrupId == eczane.Id).ToList();

                        var altGruptaklerleAyniGunTutulanNobetler = (from g1 in bakilanEczaneninSonuclari
                                                                     from g2 in nobetAltGrubuOlanlarinSonuclariGunGruplu
                                                                     where g1.TakvimId == g2.TakvimId
                                                                     select new AltGrupIleAyniGunNobetDurumu
                                                                     {
                                                                         TakvimId = g1.TakvimId,
                                                                         Tarih = g1.Tarih,
                                                                         NobetAltGrupId = g2.NobetAltGrupId,
                                                                         EczaneNobetGrupIdAltGrubuOlmayan = g1.EczaneNobetGrupId,
                                                                         EczaneNobetGrupIdAltGruplu = g2.EczaneNobetGrupId,
                                                                         EczaneIdAltGrubuOlmayan = g1.EczaneId,
                                                                         EczaneIdAltGruplu = g2.EczaneId,
                                                                         EczaneAdiAltGrubuOlmayan = g1.EczaneAdi,
                                                                         EczaneAdiAltGruplu = g2.EczaneAdi,
                                                                         NobetGrupAdiAltGrubuOlmayan = g1.NobetGrupAdi,
                                                                         NobetGrupAdiAltGruplu = g2.NobetGrupAdi,
                                                                         NobetGrupIdAltGrubuOlmayan = g1.NobetGrupId,
                                                                         NobetGrupIdAltGruplu = g2.NobetGrupId,
                                                                         GunGrup = g1.GunGrup
                                                                     }).ToList();

                        if (gunGrup.GunGrup != "Hafta İçi")
                        {
                            var anahtardanAlinacaklar = ayniGunAnahtarListeGunGruplu
                            .Where(w => w.EczaneNobetGrupIdAltGrubuOlmayan == eczane.Id
                                    && !altGruptaklerleAyniGunTutulanNobetler.Select(s => s.EczaneNobetGrupIdAltGrubuOlmayan).Contains(w.EczaneNobetGrupIdAltGrubuOlmayan)).ToList();

                            altGruptaklerleAyniGunTutulanNobetler.AddRange(anahtardanAlinacaklar);
                        }

                        if (altGruptaklerleAyniGunTutulanNobetler.Count > 0)
                        {
                            var sonNobetTarih1 = altGruptaklerleAyniGunTutulanNobetler.Max(m => m.Tarih);

                            var birlikteNobetTutulanNobetAltGrupIdListe = (from s in altGruptaklerleAyniGunTutulanNobetler
                                                                           where s.Tarih >= sonNobetTarih1
                                                                           select s.NobetAltGrupId).ToList();

                            var altGruptakiEczaneler = eczaneNobetGrupAltGruplar
                                .Where(w => birlikteNobetTutulanNobetAltGrupIdListe.Contains(w.NobetAltGrupId)).ToList();

                            var bakilanEczaneGrupTanimAdi = $"{eczane.NobetGrupAdi}, {eczane.EczaneAdi} - {gunGrup.GunGrup} aynı gün nöbetler";

                            //bakılan eczane
                            eczaneGruplar.Add(new EczaneGrupDetay
                            {
                                EczaneGrupTanimId = eczane.Id,
                                EczaneId = eczane.EczaneId,
                                ArdisikNobetSayisi = 0,
                                NobetUstGrupId = eczane.NobetUstGrupId,
                                EczaneGrupTanimAdi = bakilanEczaneGrupTanimAdi,
                                EczaneGrupTanimTipAdi = $"Alt gruplarla aynı gün nöbet {gunGrup.GunGrup}",
                                EczaneGrupTanimTipId = gunGrup.GunGrupId,
                                NobetGrupId = eczane.NobetGrupId,
                                EczaneAdi = eczane.EczaneAdi,
                                NobetGrupAdi = eczane.NobetGrupAdi,
                                EczaneNobetGrupId = eczane.Id,
                                AyniGunNobetTutabilecekEczaneSayisi = 1
                                //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                            });

                            //aynı gün nöbet tuttuğu alt gruptaki eczaneler
                            foreach (var item in altGruptakiEczaneler)
                            {
                                eczaneGruplar.Add(new EczaneGrupDetay
                                {
                                    EczaneGrupTanimId = eczane.Id,
                                    EczaneId = item.EczaneId,
                                    ArdisikNobetSayisi = 0,
                                    NobetUstGrupId = eczane.NobetUstGrupId,
                                    EczaneGrupTanimAdi = bakilanEczaneGrupTanimAdi,
                                    EczaneGrupTanimTipAdi = $"Alt gruplarla aynı gün nöbet {gunGrup.GunGrup}",
                                    EczaneGrupTanimTipId = gunGrup.GunGrupId,
                                    NobetGrupId = item.NobetGrupId,
                                    EczaneAdi = item.EczaneAdi,
                                    NobetGrupAdi = item.NobetGrupAdi,
                                    EczaneNobetGrupId = item.EczaneNobetGrupId,
                                    AyniGunNobetTutabilecekEczaneSayisi = 1
                                    //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                                });
                            }
                        }
                    }
                }
            }

            return eczaneGruplar;
        }

        /// <summary>
        /// İstenen bir gruptaki eczaneler en son hangi alt grupla nöbet tuttuysa 
        /// çözülen ayda aynı alt grupla aynı gün nöbet yazılmaz.
        /// </summary>
        /// <param name="eczaneNobetTarihAralik"></param>
        /// <param name="eczaneNobetSonuclar"></param>
        /// <param name="eczaneNobetGruplar"></param>
        /// <param name="eczaneNobetGrupAltGruplar"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="nobetUstGrupBaslamaTarihi"></param>
        /// <param name="indisId"></param>
        /// <returns></returns>
        public virtual List<EczaneGrupDetay> AltGruplarlaSiraliNobetListesiniOlusturMersin(
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            DateTime nobetUstGrupBaslamaTarihi,
            int indisId,
            List<int> ayniGunNobetTutmasiTakipEdilecekNobetGrupIdList,
            List<int> altGrubuOlanNobetGrupIdList,
            int nobetAltGrupId,
            int enSonNobetSayisi = 1)//en son aynı gün nöbet tuttuğu alt grup sayısı
        {//ay içinde gün grubu bazında birden fazla nöbet düşerse bu kısıt en son nöbet tuttuğu alt grubun dışındaki ile 2 kez nöbet tutabilir. buna dikkat.
            var eczaneGruplar = new List<EczaneGrupDetay>();

            var enSonNobetSayisi2 = enSonNobetSayisi;

            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var ayniGunNobetTutmasiTakipEdilecekGruplar = ayniGunNobetTutmasiTakipEdilecekNobetGrupIdList;

                var nobetAltGrubuOlmayanlarinEczaneleri = eczaneNobetGruplar
                      .Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.NobetGrupId)).ToList();

                var toroslar2dekiAltGruplarlaTakipEdilecekEczaneler = new List<int>
                    {
                         530,//Hekim   toroslar-2
                         534,//Mesut   toroslar-2
                         546,//Siteler toroslar-2
                         549,//Tolga   toroslar-2
                    };

                if (ayniGunNobetTutmasiTakipEdilecekNobetGrupIdList.Contains(16))
                {
                    nobetAltGrubuOlmayanlarinEczaneleri = nobetAltGrubuOlmayanlarinEczaneleri
                        .Where(w => toroslar2dekiAltGruplarlaTakipEdilecekEczaneler.Contains(w.Id)).ToList();
                }

                var altGrubuOlanNobetGruplar = altGrubuOlanNobetGrupIdList;

                var sadeceSonNobetTuttuguAltGruplaNobetTutmayacakEczaneler = new List<int>
                    {
                         713,//Menteş  y.şehir-3
                         717,//Okyanus y.şehir-3
                         722,//Yonca   y.şehir-3

                         641,//Cadde   y.şehir-1
                         644,//Dünya   y.şehir-1

                         //530 //Hekim   toroslar-2 - hekim, mesut,tolga,siteler ile aynı. özel bir istisna yok
                    };

                //tüm liste
                var ayniGunNobetTutmasiTakipEdilecekGruplarTumu = new List<int>();

                ayniGunNobetTutmasiTakipEdilecekGruplarTumu.AddRange(ayniGunNobetTutmasiTakipEdilecekGruplar);
                ayniGunNobetTutmasiTakipEdilecekGruplarTumu.AddRange(altGrubuOlanNobetGruplar);

                //alt grubu olmayanlar
                var nobetAltGrubuOlmayanlarinSonuclari = eczaneNobetSonuclar
                    .Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.NobetGrupId)
                             && w.Tarih >= nobetUstGrupBaslamaTarihi).ToList();

                //alt grubu olanlar
                var nobetAltGrubuOlanlarinSonuclari = eczaneNobetSonuclar
                    .Where(w => altGrubuOlanNobetGruplar.Contains(w.NobetGrupId)
                             && w.NobetAltGrupId == nobetAltGrupId || nobetAltGrupId == 0).ToList();

                var gunGruplar = nobetAltGrubuOlanlarinSonuclari
                    .Select(s => new { s.GunGrupId, s.GunGrup }).Distinct()
                    .OrderBy(o => o.GunGrupId).ToList();

                foreach (var gunGrup in gunGruplar)
                {
                    var nobetAltGrubuOlmayanlarinSonuclariGunGruplu = nobetAltGrubuOlmayanlarinSonuclari.Where(w => w.GunGrupId == gunGrup.GunGrupId).ToList();
                    var nobetAltGrubuOlanlarinSonuclariGunGruplu = nobetAltGrubuOlanlarinSonuclari.Where(w => w.GunGrupId == gunGrup.GunGrupId).ToList();

                    foreach (var eczane in nobetAltGrubuOlmayanlarinEczaneleri)
                    {
                        #region kontrol

                        var kontrol = false;

                        var kontrolEdilecekEczaneler = new string[] { "OKTAY YILMAZ", "AKSU" };

                        if (kontrol && kontrolEdilecekEczaneler.Contains(eczane.EczaneAdi))
                        {
                        }
                        #endregion

                        if (sadeceSonNobetTuttuguAltGruplaNobetTutmayacakEczaneler.Contains(eczane.Id))
                        {
                            enSonNobetSayisi = 1;
                        }
                        else
                        {
                            enSonNobetSayisi = enSonNobetSayisi2;
                        }

                        var bakilanEczaneninSonuclari = nobetAltGrubuOlmayanlarinSonuclariGunGruplu
                            .Where(w => w.EczaneNobetGrupId == eczane.Id).ToList();

                        var altGruptaklerleAyniGunTutulanNobetler = (from g1 in bakilanEczaneninSonuclari
                                                                     from g2 in nobetAltGrubuOlanlarinSonuclariGunGruplu
                                                                     where g1.TakvimId == g2.TakvimId
                                                                     select new AltGrupIleAyniGunNobetDurumu
                                                                     {
                                                                         TakvimId = g1.TakvimId,
                                                                         Tarih = g1.Tarih,
                                                                         NobetAltGrupId = g2.NobetAltGrupId,
                                                                         EczaneNobetGrupIdAltGrubuOlmayan = g1.EczaneNobetGrupId,
                                                                         EczaneNobetGrupIdAltGruplu = g2.EczaneNobetGrupId,
                                                                         EczaneIdAltGrubuOlmayan = g1.EczaneId,
                                                                         EczaneIdAltGruplu = g2.EczaneId,
                                                                         EczaneAdiAltGrubuOlmayan = g1.EczaneAdi,
                                                                         EczaneAdiAltGruplu = g2.EczaneAdi,
                                                                         NobetGrupAdiAltGrubuOlmayan = g1.NobetGrupAdi,
                                                                         NobetGrupAdiAltGruplu = g2.NobetGrupAdi,
                                                                         NobetGrupIdAltGrubuOlmayan = g1.NobetGrupId,
                                                                         NobetGrupIdAltGruplu = g2.NobetGrupId,
                                                                         GunGrup = g1.GunGrup
                                                                     })
                                                                     .OrderByDescending(o => o.Tarih)
                                                                     .ToList();

                        if (altGruptaklerleAyniGunTutulanNobetler.Count > 0)
                        {
                            var sonNobetTarihleri = altGruptaklerleAyniGunTutulanNobetler.Take(enSonNobetSayisi).ToList();

                            var birlikteNobetTutulanNobetAltGrupIdListe = (from s in altGruptaklerleAyniGunTutulanNobetler
                                                                           where sonNobetTarihleri.Select(n => n.TakvimId).Contains(s.TakvimId)
                                                                           select s.NobetAltGrupId).Distinct().ToList();

                            var altGruptakiEczaneler = eczaneNobetGrupAltGruplar
                                .Where(w => birlikteNobetTutulanNobetAltGrupIdListe.Contains(w.NobetAltGrupId)).ToList();

                            var bakilanEczaneGrupTanimAdi = nobetAltGrupId > 0
                                ? $"{eczane.NobetGrupAdi}, alt grup id: {nobetAltGrupId}, {eczane.EczaneAdi} - {gunGrup.GunGrup} aynı gün nöbetler"
                                : $"{eczane.NobetGrupAdi}, {eczane.EczaneAdi} - {gunGrup.GunGrup} aynı gün nöbetler";

                            //bakılan eczane
                            eczaneGruplar.Add(new EczaneGrupDetay
                            {
                                EczaneGrupTanimId = eczane.Id,
                                EczaneId = eczane.EczaneId,
                                ArdisikNobetSayisi = 0,
                                NobetUstGrupId = eczane.NobetUstGrupId,
                                EczaneGrupTanimAdi = bakilanEczaneGrupTanimAdi,
                                EczaneGrupTanimTipAdi = $"Alt gruplarla aynı gün nöbet {gunGrup.GunGrup}",
                                EczaneGrupTanimTipId = gunGrup.GunGrupId,
                                NobetGrupId = eczane.NobetGrupId,
                                EczaneAdi = eczane.EczaneAdi,
                                NobetGrupAdi = eczane.NobetGrupAdi,
                                EczaneNobetGrupId = eczane.Id,
                                AyniGunNobetTutabilecekEczaneSayisi = 1
                                //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                            });

                            //aynı gün nöbet tuttuğu alt gruptaki eczaneler
                            foreach (var item in altGruptakiEczaneler)
                            {
                                #region kontrol

                                kontrol = true;

                                kontrolEdilecekEczaneler = new string[] { "OKTAY YILMAZ", "AKSU" };

                                if (kontrol && kontrolEdilecekEczaneler.Contains(item.EczaneAdi))
                                {
                                }
                                #endregion

                                eczaneGruplar.Add(new EczaneGrupDetay
                                {
                                    EczaneGrupTanimId = eczane.Id,
                                    EczaneId = item.EczaneId,
                                    ArdisikNobetSayisi = 0,
                                    NobetUstGrupId = eczane.NobetUstGrupId,
                                    EczaneGrupTanimAdi = bakilanEczaneGrupTanimAdi,
                                    EczaneGrupTanimTipAdi = $"Alt gruplarla aynı gün nöbet {gunGrup.GunGrup}",
                                    EczaneGrupTanimTipId = gunGrup.GunGrupId,
                                    NobetGrupId = item.NobetGrupId,
                                    EczaneAdi = item.EczaneAdi,
                                    NobetGrupAdi = item.NobetGrupAdi,
                                    EczaneNobetGrupId = item.EczaneNobetGrupId,
                                    AyniGunNobetTutabilecekEczaneSayisi = 1
                                    //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                                });
                            }
                        }
                    }
                }
            }

            return eczaneGruplar;
        }


        /// <summary>
        /// İstenen bir gruptaki eczaneler en son hangi alt grupla nöbet tuttuysa 
        /// çözülen ayda aynı alt grupla aynı gün nöbet yazılmaz.
        /// </summary>
        /// <param name="eczaneNobetTarihAralik"></param>
        /// <param name="eczaneNobetSonuclar"></param>
        /// <param name="eczaneNobetGruplar"></param>
        /// <param name="eczaneNobetGrupAltGruplar"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="nobetUstGrupBaslamaTarihi"></param>
        /// <param name="indisId"></param>
        /// <returns></returns>
        public virtual List<EczaneGrupDetay> AltGruplarlaSiraliNobetListesiniOlusturGiresun(
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            DateTime nobetUstGrupBaslamaTarihi,
            int indisId,
            List<int> ayniGunNobetTutmasiTakipEdilecekAltGruplar,
            List<int> altGrubuOlanNobetGruplar)
        {//ay içinde gün grubu bazında birden fazla nöbet düşerse bu kısıt en son nöbet tuttuğu alt grubun dışındaki ile 2 kez nöbet tutabilir. buna dikkat.
            var eczaneGruplar = new List<EczaneGrupDetay>();

            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var nobetAltGrubuOlmayanlarinEczaneleri = eczaneNobetGrupAltGruplar
                    .Where(w => ayniGunNobetTutmasiTakipEdilecekAltGruplar.Contains(w.NobetAltGrupId)).ToList();

                //tüm liste
                var ayniGunNobetTutmasiTakipEdilecekGruplarTumu = new List<int>();

                ayniGunNobetTutmasiTakipEdilecekGruplarTumu.AddRange(ayniGunNobetTutmasiTakipEdilecekAltGruplar);
                ayniGunNobetTutmasiTakipEdilecekGruplarTumu.AddRange(altGrubuOlanNobetGruplar);

                //alt grubu olmayanlar
                var nobetAltGrubuOlmayanlarinSonuclari = eczaneNobetSonuclar
                    .Where(w => ayniGunNobetTutmasiTakipEdilecekAltGruplar.Contains(w.NobetAltGrupId)
                             && w.Tarih >= nobetUstGrupBaslamaTarihi).ToList();

                //alt grubu olanlar
                var nobetAltGrubuOlanlarinSonuclari = eczaneNobetSonuclar
                    .Where(w => altGrubuOlanNobetGruplar.Contains(w.NobetAltGrupId)).ToList();

                var gunGruplar = nobetAltGrubuOlanlarinSonuclari
                    .Select(s => new { s.GunGrupId, s.GunGrup }).Distinct()
                    .OrderBy(o => o.GunGrupId).ToList();

                foreach (var gunGrup in gunGruplar)
                {
                    var nobetAltGrubuOlmayanlarinSonuclariGunGruplu = nobetAltGrubuOlmayanlarinSonuclari.Where(w => w.GunGrupId == gunGrup.GunGrupId).ToList();
                    var nobetAltGrubuOlanlarinSonuclariGunGruplu = nobetAltGrubuOlanlarinSonuclari.Where(w => w.GunGrupId == gunGrup.GunGrupId).ToList();

                    foreach (var eczane in nobetAltGrubuOlmayanlarinEczaneleri)
                    {
                        #region kontrol

                        var kontrol = false;

                        var kontrolEdilecekEczaneler = new string[] { "PINAR", "ESİN" };

                        if (kontrol && kontrolEdilecekEczaneler.Contains(eczane.EczaneAdi))
                        {
                        }
                        #endregion

                        var bakilanEczaneninSonuclari = nobetAltGrubuOlmayanlarinSonuclariGunGruplu
                            .Where(w => w.EczaneNobetGrupId == eczane.EczaneNobetGrupId).ToList();

                        var altGruptaklerleAyniGunTutulanNobetler = (from g1 in bakilanEczaneninSonuclari
                                                                     from g2 in nobetAltGrubuOlanlarinSonuclariGunGruplu
                                                                     where g1.TakvimId == g2.TakvimId
                                                                     select new AltGrupIleAyniGunNobetDurumu
                                                                     {
                                                                         TakvimId = g1.TakvimId,
                                                                         Tarih = g1.Tarih,
                                                                         NobetAltGrupId = g2.NobetAltGrupId,
                                                                         EczaneNobetGrupIdAltGrubuOlmayan = g1.EczaneNobetGrupId,
                                                                         EczaneNobetGrupIdAltGruplu = g2.EczaneNobetGrupId,
                                                                         EczaneIdAltGrubuOlmayan = g1.EczaneId,
                                                                         EczaneIdAltGruplu = g2.EczaneId,
                                                                         EczaneAdiAltGrubuOlmayan = g1.EczaneAdi,
                                                                         EczaneAdiAltGruplu = g2.EczaneAdi,
                                                                         NobetGrupAdiAltGrubuOlmayan = g1.NobetGrupAdi,
                                                                         NobetGrupAdiAltGruplu = g2.NobetGrupAdi,
                                                                         NobetGrupIdAltGrubuOlmayan = g1.NobetGrupId,
                                                                         NobetGrupIdAltGruplu = g2.NobetGrupId,
                                                                         GunGrup = g1.GunGrup
                                                                     }).ToList();

                        if (altGruptaklerleAyniGunTutulanNobetler.Count > 0)
                        {
                            var sonNobetTarih1 = altGruptaklerleAyniGunTutulanNobetler.Max(m => m.Tarih);

                            var birlikteNobetTutulanNobetAltGrupIdListe = (from s in altGruptaklerleAyniGunTutulanNobetler
                                                                           where s.Tarih >= sonNobetTarih1
                                                                           select s.NobetAltGrupId).ToList();

                            var altGruptakiEczaneler = eczaneNobetGrupAltGruplar
                                .Where(w => birlikteNobetTutulanNobetAltGrupIdListe.Contains(w.NobetAltGrupId)).ToList();

                            var bakilanEczaneGrupTanimAdi = $"{eczane.NobetGrupAdi}, {eczane.EczaneAdi} - {gunGrup.GunGrup} aynı gün nöbetler";
                            //bakılan eczane
                            eczaneGruplar.Add(new EczaneGrupDetay
                            {
                                EczaneGrupTanimId = eczane.EczaneNobetGrupId,
                                EczaneId = eczane.EczaneId,
                                ArdisikNobetSayisi = 0,
                                NobetUstGrupId = eczane.NobetUstGrupId,
                                EczaneGrupTanimAdi = bakilanEczaneGrupTanimAdi,
                                EczaneGrupTanimTipAdi = $"Alt gruplarla aynı gün nöbet {gunGrup.GunGrup}",
                                EczaneGrupTanimTipId = gunGrup.GunGrupId,
                                NobetGrupId = eczane.NobetGrupId,
                                EczaneAdi = eczane.EczaneAdi,
                                NobetGrupAdi = eczane.NobetGrupAdi,
                                EczaneNobetGrupId = eczane.EczaneNobetGrupId,
                                AyniGunNobetTutabilecekEczaneSayisi = 1
                                //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                            });

                            //aynı gün nöbet tuttuğu alt gruptaki eczaneler
                            foreach (var item in altGruptakiEczaneler)
                            {
                                eczaneGruplar.Add(new EczaneGrupDetay
                                {
                                    EczaneGrupTanimId = eczane.EczaneNobetGrupId,
                                    EczaneId = item.EczaneId,
                                    ArdisikNobetSayisi = 0,
                                    NobetUstGrupId = eczane.NobetUstGrupId,
                                    EczaneGrupTanimAdi = bakilanEczaneGrupTanimAdi,
                                    EczaneGrupTanimTipAdi = $"Alt gruplarla aynı gün nöbet {gunGrup.GunGrup}",
                                    EczaneGrupTanimTipId = gunGrup.GunGrupId,
                                    NobetGrupId = item.NobetGrupId,
                                    EczaneAdi = item.EczaneAdi,
                                    NobetGrupAdi = item.NobetGrupAdi,
                                    EczaneNobetGrupId = item.EczaneNobetGrupId,
                                    AyniGunNobetTutabilecekEczaneSayisi = 1
                                    //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                                });
                            }
                        }
                    }
                }
            }

            return eczaneGruplar;
        }

        public List<EczaneNobetSonucListe2> EczaneNobetSonucBirlesim(
            List<EczaneNobetSonucDetay2> sonuclar,
            List<TakvimNobetGrup> takvimNobetGruplar,
            List<EczaneNobetMazeretDetay> mazeretler,
            List<EczaneNobetIstekDetay> istekler
            )
        {
            var eczaneNobetSonuclar = (from s in sonuclar
                                       from b in takvimNobetGruplar
                                       from m in mazeretler
                                         .Where(w => w.TakvimId == s.TakvimId
                                                  && w.EczaneNobetGrupId == s.EczaneNobetGrupId).DefaultIfEmpty()
                                       from i in istekler
                                         .Where(w => w.TakvimId == s.TakvimId
                                                  && w.EczaneNobetGrupId == s.EczaneNobetGrupId).DefaultIfEmpty()
                                       where s.TakvimId == b.TakvimId
                                          && s.NobetGorevTipId == b.NobetGrupGorevTipId
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
                                           NobetGunKuralId = b.NobetGunKuralId,
                                           GunTanim = b.NobetGunKuralAdi,
                                           GunGrup = b.GunGrupAdi,
                                           Gun = s.Tarih.Day,
                                           Tarih = s.Tarih,
                                           TakvimId = s.TakvimId,
                                           MazeretId = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretId : 0,
                                           IstekId = (i?.TakvimId == s.TakvimId && i?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? i.IstekId : 0,
                                           Mazeret = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretAdi : null,
                                           MazeretTuru = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretTuru : null,
                                           NobetGorevTipAdi = s.NobetGorevTipAdi,
                                           NobetGorevTipId = s.NobetGorevTipId,
                                           NobetAltGrupId = s.NobetAltGrupId, //(a?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? a.NobetAltGrupId : 0,
                                           NobetAltGrupAdi = s.NobetAltGrupAdi //(a?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? a.NobetAltGrupAdi : "Alt grup yok"
                                       }).ToList();
            return eczaneNobetSonuclar;
        }

        public List<AnahtarListe> AnahtarListeyiBuGuneTasi2(List<NobetGrupGorevTipDetay> nobetGrupGorevTipler,
            DateTime nobetUstGrupBaslangicTarihi,
            List<TakvimNobetGrup> takvimNobetGruplar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumu,
            List<EczaneNobetSonucListe2> anahtarListeTumu,
            List<NobetUstGrupGunGrupDetay> nobetUstGrupGunGruplar,
            DateTime nobetBaslangicTarihi,
            DateTime nobetBitisTarihi)
        {
            var anahtarListeTumEczanelerHepsi = new List<AnahtarListe>();

            foreach (var nobetUstGrupGunGrup in nobetUstGrupGunGruplar)
            {
                var ilgiliTarihler = takvimNobetGruplar;

                foreach (var nobetGrupGorevTip in nobetGrupGorevTipler)
                {
                    var tarihler = ilgiliTarihler
                        .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id)
                        .ToList();

                    var eczaneNobetGruplar = eczaneNobetGruplarTumu
                        .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                    var anahtarListeGunGrup = anahtarListeTumu
                        .Where(w => w.GunGrup == nobetUstGrupGunGrup.GunGrupAdi
                                 && w.NobetGrupId == nobetGrupGorevTip.Id).ToList();

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
                                                        NobetUstGrupBaslamaTarihi = nobetUstGrupBaslangicTarihi
                                                    })
                                                    .OrderBy(o => o.Tarih)
                                                    .ToList();

                    var gruptakiEczaneSayisi = anahtarListeTumEczaneler.Count;

                }
            }

            return anahtarListeTumEczanelerHepsi;
        }

        public List<EczaneNobetTarihAralik> AmacFonksiyonuKatsayisiBelirle(List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatay,
            List<KalibrasyonYatay> kalibrasyonDetaylar = null)
        {
            //bartın, 1 ocak, milli bayram, dini bayram öncelik sırası
            int bayramCevrimDini = 8000;
            int bayramCevrimMilli = 8000;
            int bayramCevrim = 8000;
            int yilbasiCevrim = 7000;
            int yilSonuCevrim = 7000;

            int arifeCevrim = 5000;
            int cumartesiCevrim = 900;
            int pazarCevrim = 1000;
            int haftaIciCevrim = 500;
            var manuelSayi = 0;
            double amacFonksiyonKatsayi = 1;

            var nobetUstGrupId = eczaneNobetTarihAralik.Select(s => s.NobetUstGrupId).Distinct().FirstOrDefault();
            var ilkTarih = eczaneNobetTarihAralik.Min(s => s.Tarih).AddDays(-1);

            var nobetBorcOdeme = _nobetUstGrupKisitService.GetDetay("nobetBorcOdeme", nobetUstGrupId);
            var pespeseHaftaIciAyniGunNobet = _nobetUstGrupKisitService.GetDetay("pespeseHaftaIciAyniGunNobet", nobetUstGrupId);
            var bayramPespeseFarkliTur = _nobetUstGrupKisitService.GetDetay("bayramPespeseFarkliTur", nobetUstGrupId);
            var bayramPespeseFarkliTurAktif = (NobetUstGrupKisitDetay)bayramPespeseFarkliTur.Clone();
            var bayramFarkliTur = _nobetUstGrupKisitService.GetDetay("bayramFarkliTur", nobetUstGrupId);
            var bayramFarkliTurAktif = bayramFarkliTur;

            var nobetGrupGorevTipler = eczaneNobetTarihAralik
                .Select(s => new
                {
                    s.NobetGrupGorevTipId,
                    s.NobetGrupAdi,
                    s.NobetGorevTipId,
                    s.NobetGrupId
                }).Distinct().ToList();

            foreach (var nobetGrupGorevTip in nobetGrupGorevTipler)
            {
                var bayramPespeseFarkliTurGrupBazli = _nobetGrupGorevTipKisitService.GetDetay(36, nobetGrupGorevTip.NobetGrupGorevTipId);
                var bayramFarkliTurGrupBazli = _nobetGrupGorevTipKisitService.GetDetay(61, nobetGrupGorevTip.NobetGrupGorevTipId);

                bayramPespeseFarkliTurAktif = KisitiGrupBazliGuncelle(bayramPespeseFarkliTur, bayramPespeseFarkliTurAktif, bayramPespeseFarkliTurGrupBazli);
                bayramFarkliTurAktif = KisitiGrupBazliGuncelle(bayramFarkliTur, bayramFarkliTurAktif, bayramFarkliTurGrupBazli);

                var gunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipId(nobetGrupGorevTip.NobetGrupGorevTipId);

                var gunKuralIstatistikGrupBazli = eczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var toplamKalibrasyon = kalibrasyonDetaylar != null
                    ? kalibrasyonDetaylar.Where(w => w.KalibrasyonTipId == 7).ToList()
                    : new List<KalibrasyonYatay>();

                var enKucukKalibrasyonDegeriCumartesi = toplamKalibrasyon.Count > 0 ? toplamKalibrasyon
                    .Min(m => m.KalibrasyonToplamCumartesi) : 0;

                var enKucukKalibrasyonDegeriPazar = toplamKalibrasyon.Count > 0 ? toplamKalibrasyon
                    .Min(m => m.KalibrasyonToplamPazar) : 0;

                var enKucukKalibrasyonDegeriHaftaIci = toplamKalibrasyon.Count > 0 ? toplamKalibrasyon
                    .Min(m => m.KalibrasyonToplamHaftaIci) : 0;

                var diniBayramTakipEdilsinMi = GunGrubuTakipDurumu(8, gunKurallar);
                var milliBayramTakipEdilsinMi = GunGrubuTakipDurumu(9, gunKurallar);
                var yilbasiTakipEdilsinMi = GunGrubuTakipDurumu(12, gunKurallar);
                var yilSonuTakipEdilsinMi = GunGrubuTakipDurumu(11, gunKurallar);

                var arifeTakipEdilsinMi = GunGrubuTakipDurumuByGunGrupId(5, gunKurallar);
                var pazarTakipEdilsinMi = GunGrubuTakipDurumuByGunGrupId(1, gunKurallar);
                var cumartesiTakipEdilsinMi = GunGrubuTakipDurumuByGunGrupId(4, gunKurallar);
                var haftaIciTakipEdilsinMi = GunGrubuTakipDurumuByGunGrupId(3, gunKurallar);
                var bayramTakipEdilsinMi = GunGrubuTakipDurumuByGunGrupId(2, gunKurallar);

                if (gunKuralIstatistikGrupBazli.Count > 0)
                {
                    var sonNobetTarihiEnKucukHaftaIci = gunKuralIstatistikGrupBazli.Min(s => s.SonNobetTarihiHaftaIci);
                    var sonNobetTarihiEnKucukPazar = gunKuralIstatistikGrupBazli.Min(s => s.SonNobetTarihiPazar);
                    var sonNobetTarihiEnKucukCumartesi = gunKuralIstatistikGrupBazli.Min(s => s.SonNobetTarihiCumartesi);
                    var sonNobetTarihiEnKucukArife = gunKuralIstatistikGrupBazli.Min(s => s.SonNobetTarihiArife);
                    var sonNobetTarihiEnKucukDini = gunKuralIstatistikGrupBazli.Min(s => s.SonNobetTarihiDiniBayram);
                    var sonNobetTarihiEnKucukMilli = gunKuralIstatistikGrupBazli.Min(s => s.SonNobetTarihiMilliBayram);
                    var sonNobetTarihiEnKucukBayram = gunKuralIstatistikGrupBazli.Min(s => s.SonNobetTarihiBayram);
                    var sonNobetTarihiEnKucukYilbasi = gunKuralIstatistikGrupBazli.Min(s => s.SonNobetTarihi1Ocak);
                    var sonNobetTarihiEnKucukYilSonu = gunKuralIstatistikGrupBazli.Min(s => s.SonNobetTarihiYilSonu);

                    var kd = eczaneNobetTarihAralik.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.NobetGrupGorevTipId).ToList();

                    foreach (var eczaneNobetTarih in kd)
                    {
                        var eczaneIstatistik = gunKuralIstatistikGrupBazli.SingleOrDefault(w => w.EczaneNobetGrupId == eczaneNobetTarih.EczaneNobetGrupId);

                        var eczaneKalibrasyonlar = kalibrasyonDetaylar != null
                            ? kalibrasyonDetaylar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetTarih.EczaneNobetGrupId
                                         && w.KalibrasyonTipAdi == eczaneNobetTarih.AyIkili).ToList()
                            : new List<KalibrasyonYatay>();

                        #region kontrol

                        var kontrol = false;

                        if (kontrol)
                        {
                            var kontrolEdilecekEczaneler = new string[] {
                                //"ŞADIRVAN", //23.4.2008
                                "SAĞLIK",    //01.5.2008
                                "ÖZCAN"     //19.5.2008
                            };

                            if (kontrolEdilecekEczaneler.Contains(eczaneNobetTarih.EczaneAdi)
                                && eczaneNobetTarih.BayramMi
                                )
                            {

                            }
                        } 
                        #endregion

                        //dini milli ayru gün kural tanımlanmış olabilir.
                        //bayramPespeseFarkliTur kısıtı aktif ise else'e girecek.
                        //bayramPespeseFarkliTur kısıtı pasif iken else'e girmesi için bayramFarkliTur kısıtının da aktif olması gerekir.
                        if (bayramPespeseFarkliTurAktif.PasifMi
                            && bayramFarkliTurAktif.PasifMi
                            )
                        {
                            if (eczaneNobetTarih.BayramMi && bayramTakipEdilsinMi)
                            {
                                amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(bayramCevrim, sonNobetTarihiEnKucukBayram, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihiBayram, tipAdi: "bayram");
                            }
                        }
                        else
                        {
                            if (eczaneNobetTarih.DiniBayramMi && diniBayramTakipEdilsinMi)
                            {
                                amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(bayramCevrimDini, sonNobetTarihiEnKucukDini, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihiDiniBayram, tipAdi: "dini");
                            }
                            else if (eczaneNobetTarih.MilliBayramMi && milliBayramTakipEdilsinMi)
                            {
                                amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(bayramCevrimMilli, sonNobetTarihiEnKucukMilli, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihiMilliBayram, tipAdi: "milli");
                            }
                        }
                        if (eczaneNobetTarih.YilbasiMi && yilbasiTakipEdilsinMi)
                        {
                            amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(yilbasiCevrim, sonNobetTarihiEnKucukYilbasi, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihi1Ocak);
                        }
                        else if (eczaneNobetTarih.YilSonuMu && yilSonuTakipEdilsinMi)
                        {
                            amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(yilSonuCevrim, sonNobetTarihiEnKucukYilSonu, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihiYilSonu);
                        }
                        else if (eczaneNobetTarih.ArifeMi && arifeTakipEdilsinMi)
                        {
                            amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(arifeCevrim, sonNobetTarihiEnKucukArife, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihiArife);
                        }
                        else if (eczaneNobetTarih.CumartesiGunuMu && cumartesiTakipEdilsinMi)
                        {
                            var kalibrasyonDegeriCumartesi = kalibrasyonDetaylar != null
                                ? eczaneKalibrasyonlar
                                   .Sum(s => (s.KalibrasyonToplamCumartesi - enKucukKalibrasyonDegeriCumartesi) //bunu kontrol et, gerek yok gibi
                                            + s.KalibrasyonCumartesi / s.KalibrasyonToplamCumartesi)
                                : 1;

                            amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(cumartesiCevrim, sonNobetTarihiEnKucukCumartesi, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihiCumartesi, ozelKatsayi: 7, mevsimKatsayisi: kalibrasyonDegeriCumartesi);
                        }
                        else if (eczaneNobetTarih.PazarGunuMu && pazarTakipEdilsinMi)
                        {
                            var kalibrasyonDegeriPazar = kalibrasyonDetaylar != null
                                ? eczaneKalibrasyonlar
                                    .Sum(s => (s.KalibrasyonToplamPazar - enKucukKalibrasyonDegeriPazar)
                                             + s.KalibrasyonPazar / s.KalibrasyonToplamPazar)
                                : 1;

                            if (!nobetBorcOdeme.PasifMi)
                            {
                                manuelSayi = 0;

                                if (eczaneIstatistik.EczaneAdi == "sem")
                                {//manuel borç düzeltme
                                    manuelSayi = 7;
                                }
                            }

                            amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(pazarCevrim, sonNobetTarihiEnKucukPazar, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihiPazar, 7, manuelSayi, eczaneIstatistik.BorcluNobetSayisiPazar, mevsimKatsayisi: kalibrasyonDegeriPazar);
                            //amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(pazarCevrim, sonNobetTarihiEnKucukPazar, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihiPazar, ozelKatsayi: 7, mevsimKatsayisi: kalibrasyonDegeriPazar);
                        }
                        else if (eczaneNobetTarih.HaftaIciMi && haftaIciTakipEdilsinMi)
                        {
                            if (!nobetBorcOdeme.PasifMi)
                            {
                                manuelSayi = 0;

                                if (eczaneIstatistik.EczaneAdi == "sem")
                                {//manuel borç düzeltme
                                    manuelSayi = 7;
                                }
                            }

                            amacFonksiyonKatsayi = GetAmacFonksiyonKatsayisi(haftaIciCevrim, sonNobetTarihiEnKucukHaftaIci, ilkTarih, eczaneNobetTarih.Tarih, eczaneIstatistik.SonNobetTarihiHaftaIci, 1, manuelSayi, eczaneIstatistik.BorcluNobetSayisiHaftaIci);
                        }

                        eczaneNobetTarih.AmacFonksiyonKatsayi = Math.Round(amacFonksiyonKatsayi, 5);
                    }
                }

            }

            return eczaneNobetTarihAralik;
        }

        private NobetUstGrupKisitDetay KisitiGrupBazliGuncelle(NobetUstGrupKisitDetay bayramPespeseFarkliTur, NobetUstGrupKisitDetay bayramPespeseFarkliTurAktif, NobetGrupGorevTipKisitDetay bayramPespeseFarkliTurGrupBazli)
        {
            if (bayramPespeseFarkliTurGrupBazli != null)
            {
                bayramPespeseFarkliTurAktif.PasifMi = bayramPespeseFarkliTurGrupBazli.PasifMi;
                bayramPespeseFarkliTurAktif.SagTarafDegeri = bayramPespeseFarkliTurGrupBazli.SagTarafDegeri;
            }
            else
            {
                bayramPespeseFarkliTurAktif = bayramPespeseFarkliTur;
            }

            return bayramPespeseFarkliTurAktif;
        }

        private bool GunGrubuTakipDurumu(int nobetUstGrupId, int[] takipEdilecekNobetUstGruplar)
        {
            if (takipEdilecekNobetUstGruplar.Contains(nobetUstGrupId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GunGrubuTakipDurumu(int nobetGunKuralId, List<NobetGrupGorevTipGunKuralDetay> nobetGrupGorevTipGunKurallar)
        {
            if (nobetGrupGorevTipGunKurallar.Select(s => s.NobetGunKuralId).Contains(nobetGunKuralId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GunGrubuTakipDurumuByGunGrupId(int gunGrupId, List<NobetGrupGorevTipGunKuralDetay> nobetGrupGorevTipGunKurallar)
        {
            if (nobetGrupGorevTipGunKurallar.Select(s => s.GunGrupId).Contains(gunGrupId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private double GetAmacFonksiyonKatsayisi(int cevrimKatSayisi, DateTime sonNobetTarihiEnKucuk, DateTime ilkTarih, DateTime bakilanTarih, DateTime sonNobetTarihi, double ozelKatsayi = 1,
            int manuelSayi = 0, int borcluNobetSayisi = 0, bool farkliGunMuOlsun = false, double mevsimKatsayisi = 1, string tipAdi = "")
        {
            var ilkTarihtenSonrakiGecenGunSayisi = (bakilanTarih - ilkTarih).TotalDays;

            var enKucukSonNobettenIlkTariheKadarGecenGunSayisi = (ilkTarih - sonNobetTarihiEnKucuk).TotalDays;
            var enKucukSonNobettenIlkTariheKadarKatsayi = Math.Sqrt(enKucukSonNobettenIlkTariheKadarGecenGunSayisi) * 100 - cevrimKatSayisi;

            //var ayFarki = (int)Math.Ceiling(ilkTarihtenSonrakiGecenGunSayisi / 30);

            //var sonNobetTarihindenUstGrupBaslamaTarihineKadarGecenGunSayisi = (bakilanTarih - sonNobetTarihi).TotalDays;
            var sonNobetTarihindenSonraGecenGunSayisi = (bakilanTarih - sonNobetTarihi).TotalDays;

            //negatif olmamalı
            var karekokuAlinacakGunFarki = sonNobetTarihindenSonraGecenGunSayisi > 0 ? sonNobetTarihindenSonraGecenGunSayisi : 1;

            if (mevsimKatsayisi < 1)
                mevsimKatsayisi = 1;

            var gunFarkiKarekokIcinde = Math.Sqrt(karekokuAlinacakGunFarki);

            //var fark2 = cevrimKatSayisi + (cevrimKatSayisi * mevsimKatsayisi) / gunFarkiKarekokIcinde + manuelSayi;
            var fark = cevrimKatSayisi + (cevrimKatSayisi * mevsimKatsayisi) / gunFarkiKarekokIcinde + manuelSayi;

            var farkBoclu = fark + borcluNobetSayisi;

            if (farkBoclu > 0)
            {
                fark = farkBoclu;
            }

            var toplamFark = fark + ilkTarihtenSonrakiGecenGunSayisi;

            if (tipAdi == "dini"
             || tipAdi == "milli"
             || tipAdi == "bayram"
                )
            {
                toplamFark = toplamFark * 100 - 800000;
                //toplamFark = toplamFark * 100;
            }

            if (ozelKatsayi > 1)
            {
                var haftaSonuIndis = Math.Ceiling(ilkTarihtenSonrakiGecenGunSayisi / ozelKatsayi);

                return toplamFark;
            }
            else
            {
                if (farkliGunMuOlsun
                    && bakilanTarih.DayOfWeek == sonNobetTarihi.DayOfWeek)
                {
                    return toplamFark * 0.2;
                }
                else
                {
                    return toplamFark;
                }
            }
        }




        #region kısıt kontrol

        public void KurallariKontrolEtHaftaIciEnAzEnCok(int nobetUstGrupId, List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetSonuclarYatay)
        {
            var herAyEnaz1HaftaIciGorev = _nobetUstGrupKisitService.GetDetay("herAyEnaz1HaftaIciGorev", nobetUstGrupId);
            var haftaIciToplamMaxHedef = _nobetUstGrupKisitService.GetDetay("haftaIciToplamMaxHedef", nobetUstGrupId);

            //var herAyEnaz1Gorev = _nobetUstGrupKisitService.GetDetay("herAyEnaz1Gorev", nobetUstGrupId);
            //var toplamMaxHedef = _nobetUstGrupKisitService.GetDetay("toplamMaxHedef", nobetUstGrupId);

            var eczaneler = new List<string>();

            if (!herAyEnaz1HaftaIciGorev.PasifMi
                && !haftaIciToplamMaxHedef.PasifMi
                && haftaIciToplamMaxHedef.SagTarafDegeri > 0
                )
            {
                foreach (var eczaneNobetSonuc in eczaneNobetSonuclarYatay.OrderBy(o => o.NobetGrupAdi).ThenBy(o => o.EczaneAdi).ToList())
                {
                    var enAzNobetSayisi = eczaneNobetSonuc.NobetSayisiHaftaIci + herAyEnaz1HaftaIciGorev.SagTarafDegeri;

                    if (haftaIciToplamMaxHedef.SagTarafDegeri < enAzNobetSayisi)
                    {
                        eczaneler.Add($"{eczaneNobetSonuc.NobetGrupAdi} {eczaneNobetSonuc.EczaneAdi} eczanesinin toplam hafta içi nöbet sayısı <span class='badge badge-info'>{eczaneNobetSonuc.NobetSayisiHaftaIci}</span>");
                    }
                }

                var ilgiliEczaneSayisi = eczaneler.Count;

                var kuralIhlalMesaj = $"Kural kontol! "
                        + $"Aşağıdaki açıklamalara göre "
                        + $"<a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">nöbet ayarlarında</a> "
                        + $"bazı değişiklikler yaparak <strong>tekrar çözmelisiniz..</strong>"
                        + "<hr /> "
                        + $"<p><strong> {herAyEnaz1HaftaIciGorev.KisitTanim} std. [{herAyEnaz1HaftaIciGorev.SagTarafDegeri}]</strong> ile "
                        + $"<strong>{haftaIciToplamMaxHedef.KisitTanim} std. [{haftaIciToplamMaxHedef.SagTarafDegeri}]</strong> kuralları aktif olduğunda aşağıdaki eczaneler için; "
                        + $"kümülatif en fazla yazılabilecek hafta içi nöbet sayısı <span class='badge badge-danger'>{haftaIciToplamMaxHedef.SagTarafDegeri}</span> değerinden daha küçük olamaz. "//ya da eşit 
                                                                                                                                                                                                   //+ "<br /> "
                        + $"İlgili eczaneler <span class='badge badge-light'>{ilgiliEczaneSayisi}</span></p>";

                var celiskiler = "<ul class='list-group list-group-flush mt-2 mb-3'>";

                foreach (var eczane in eczaneler)
                {
                    celiskiler += $"<li class='list-group-item list-group-item-action py-1'>{eczane}</li>";
                }
                celiskiler += "</ul>";

                kuralIhlalMesaj += celiskiler;

                if (ilgiliEczaneSayisi > 0)
                {
                    throw new Exception(kuralIhlalMesaj);
                }
            }
        }

        public void KurallariKontrolEtMazeretIstek(int nobetUstGrupId, List<EczaneNobetMazeretDetay> eczaneNobetMazeretler, List<EczaneNobetIstekDetay> eczaneNobetIstekler)
        {
            var mazeret = _nobetUstGrupKisitService.GetDetay("mazeret", nobetUstGrupId);
            var istek = _nobetUstGrupKisitService.GetDetay("istek", nobetUstGrupId);

            var eczaneler = new List<string>();

            if (!mazeret.PasifMi && !istek.PasifMi)
            {
                var mazeretler = eczaneNobetMazeretler.Select(w => new { w.TakvimId, w.Tarih, w.NobetGrupAdi, w.EczaneAdi, w.EczaneNobetGrupId, w.EczaneId, Tip = "mazeret" }).Distinct().ToList();
                var istekler = eczaneNobetIstekler.Select(w => new { w.TakvimId, w.Tarih, w.NobetGrupAdi, w.EczaneAdi, w.EczaneNobetGrupId, w.EczaneId, Tip = "istek" }).Distinct().ToList();
                var istekVeMazeretler = istekler.Union(mazeretler).ToList();

                var ayniGunMazeretveIstekGirilenEczaneler = istekVeMazeretler
                    .GroupBy(g => new
                    {
                        g.NobetGrupAdi,
                        g.EczaneId,
                        g.EczaneAdi,
                        g.TakvimId,
                        g.Tarih
                    })
                    .Select(s => new
                    {
                        s.Key.NobetGrupAdi,
                        s.Key.EczaneAdi,
                        s.Key.EczaneId,
                        s.Key.Tarih,
                        s.Key.TakvimId,
                        Sayi = s.Count()
                    })
                    .Where(w => w.Sayi > 1).ToList();

                foreach (var eczaneNobetSonuc in ayniGunMazeretveIstekGirilenEczaneler.OrderBy(o => o.NobetGrupAdi).ThenBy(o => o.EczaneAdi).ToList())
                {
                    eczaneler.Add($"{eczaneNobetSonuc.NobetGrupAdi} {eczaneNobetSonuc.EczaneAdi} eczanesi <span class='badge badge-info'>{eczaneNobetSonuc.Tarih.ToShortDateString()}</span> tarihi");
                }

                var ilgiliEczaneSayisi = eczaneler.Count;

                var kuralIhlalMesaj = $"Kural kontol! "
                        + $"Aşağıdaki açıklamalara göre "
                        + $"<a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">nöbet ayarlarında</a> "
                        + $"bazı değişiklikler yaparak <strong>tekrar çözmelisiniz..</strong>"
                        + "<hr /> "
                        + $"<p><strong>K{mazeret.KisitId} ({mazeret.KisitAdiGosterilen})</strong> ile "
                        + $"<strong>K{istek.KisitId} ({istek.KisitAdi})</strong> kuralları aktif olduğunda <strong>bir eczane için aynı güne hem mazeret hem de istek girilemez.</strong> "
                        + $"Lütfen aşağıdaki "
                        + $"eczane ve tarihleri <span class='badge badge-light'>{ilgiliEczaneSayisi}</span> kontrol ediniz.</p>";

                var celiskiler = "<ul class='list-group list-group-flush mt-2 mb-3'>";

                foreach (var eczane in eczaneler)
                {
                    celiskiler += $"<li class='list-group-item list-group-item-action py-1'>{eczane}</li>";
                }
                celiskiler += "</ul>";

                kuralIhlalMesaj += celiskiler;

                if (ilgiliEczaneSayisi > 0)
                {
                    throw new Exception(kuralIhlalMesaj);
                }
            }
        }

        public void KurallariKontrolEtIstek(int nobetUstGrupId, List<EczaneNobetIstekDetay> eczaneNobetIstekler, List<NobetGrupKuralDetay> nobetGrupKuralDetaylar)
        {//bir eczaneye peşpeşe günlerde istek girilemez
            //son tuttuğu nöbetten sonra en az ardışık nöbet sayısı kadar boş gün geçmeden istek girilemez
            var istek = _nobetUstGrupKisitService.GetDetay("istek", nobetUstGrupId);

            var eczaneler = new List<string>();

            if (!istek.PasifMi)
            {
                var nobetGrupKurallar = nobetGrupKuralDetaylar
                    .Where(w => w.NobetKuralId == 1)
                    .Select(s => new { s.NobetGrupGorevTipId, s.NobetGorevTipAdi, s.NobetGrupAdi, s.NobetGrupId, s.NobetKuralAdi, s.NobetKuralId, s.Deger }).Distinct().ToList();

                foreach (var nobetGrupKural in nobetGrupKurallar)
                {
                    var varsayilanArdisikBosGunSayisi = (int)nobetGrupKural.Deger;

                    var istekGirilenGruplar = eczaneNobetIstekler
                      .Where(w => w.NobetGrupGorevTipId == nobetGrupKural.NobetGrupGorevTipId).ToList();

                    var istekGirilenEczaneler = istekGirilenGruplar
                      .Select(s => new
                      {
                          s.EczaneAdi,
                          s.EczaneId,
                          s.EczaneNobetGrupId
                      }).Distinct().ToList();

                    foreach (var istekGirilenEczane in istekGirilenEczaneler)
                    {
                        var istekTarihleri = istekGirilenGruplar
                            .Where(w => w.EczaneNobetGrupId == istekGirilenEczane.EczaneNobetGrupId)
                            .Select(s => new
                            {
                                s.TakvimId,
                                s.Tarih
                            }).ToList();

                        foreach (var istekTarih in istekTarihleri)
                        {
                            var ilkTarih = istekTarih.Tarih;

                            var sonrakiIstekGirilebilecekIstekTarihi = istekTarih.Tarih.AddDays(varsayilanArdisikBosGunSayisi);

                            var pespeseGirilenIstekler = istekTarihleri.Where(w => w.Tarih >= ilkTarih && w.Tarih <= sonrakiIstekGirilebilecekIstekTarihi).ToList();

                            var istekSayisi = pespeseGirilenIstekler.Count;

                            if (istekSayisi > 1)
                            {
                                eczaneler.Add($"{nobetGrupKural.NobetGrupAdi} {istekGirilenEczane.EczaneAdi} eczanesi için; " +
                                        $"<span class='badge badge-info'>{ilkTarih.ToShortDateString()}-{sonrakiIstekGirilebilecekIstekTarihi.ToShortDateString()}</span> tarihleri " +
                                        $"arasında <b>{istekSayisi} adet istek</b> girilmiştir." +
                                        $"(İki nöbet arasında <b>en az {varsayilanArdisikBosGunSayisi} boş gün</b> olmalıdır.)");
                            }

                            //foreach (var pespeseGirilenIstek in pespeseGirilenIstekler)
                            //{
                            //    eczaneler.Add($"<span class='badge badge-info'>{pespeseGirilenIstek.Tarih.ToShortDateString()}</span> tarihi " +
                            //        $"arasında {pespeseGirilenIstekler.Count} gün istek girilmiştir."
                            //        );
                            //}                            
                        }
                    }
                }

                var ilgiliEczaneSayisi = eczaneler.Count;

                var kuralIhlalMesaj = $"Kural kontol! "
                        + $"Aşağıdaki açıklamalara göre "
                        + $"<a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">nöbet ayarlarında</a> "
                        + $"bazı değişiklikler yaparak <strong>tekrar çözmelisiniz..</strong>"
                        + "<hr /> "
                        + $"<strong>K{istek.KisitId} ({istek.KisitAdiGosterilen})</strong> kuralı aktiftir.</strong> "
                        + $"Lütfen aşağıdaki "
                        + $"eczane ve tarihleri <span class='badge badge-light'>{ilgiliEczaneSayisi}</span> kontrol ediniz.</p>";

                var celiskiler = "<ul class='list-group list-group-flush mt-2 mb-3'>";

                foreach (var eczane in eczaneler)
                {
                    celiskiler += $"<li class='list-group-item list-group-item-action py-1'>{eczane}</li>";
                }
                celiskiler += "</ul>";

                kuralIhlalMesaj += celiskiler;

                if (ilgiliEczaneSayisi > 0)
                {
                    throw new Exception(kuralIhlalMesaj);
                }
            }
        }

        #endregion
    }
}
