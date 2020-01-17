using OPTANO.Modeling.Common;
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Configuration;
using OPTANO.Modeling.Optimization.Enums;
using OPTANO.Modeling.Optimization.Solver;
using OPTANO.Modeling.Optimization.Solver.Cplex128;
using System;
using System.Collections.Generic;
using System.Linq;
using WM.Core.Optimization;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Optimization.Concrete.Optano.Samples
{
    public class EczaneNobetOptano : IEczaneNobetOptimization
    {
        private IEczaneNobetTekGrupOptimization _eczaneNobetTekGrupOptimization;
        private IEczaneNobetCokGrupOptimization _eczaneNobetCokGrupOptimization;

        public EczaneNobetOptano(IEczaneNobetTekGrupOptimization eczaneNobetTekGrupOptimization,
                                 IEczaneNobetCokGrupOptimization eczaneNobetCokGrupOptimization)
        {
            _eczaneNobetTekGrupOptimization = eczaneNobetTekGrupOptimization;
            _eczaneNobetCokGrupOptimization = eczaneNobetCokGrupOptimization;
        }

        #region Değişkenler

        //Karar Değişkeni
        //karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }
        //private VariableCollection<EczaneNobetTarihAralik> _y { get; set; }
        public IEnumerable<Constraint> ConstraintsUB { get; set; }
        #region Sapma Değişkenleri
        //private VariableCollection<EczaneNobetIstatistik> _y { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h1PazarPS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h1PazarNS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h2PazartesiPS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h2PazartesiNS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h3SaliPS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h3SaliNS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h4CarsambaPS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h4CarsambaNS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h5PersembePS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h5PersembeNS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h6CumaPS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h6CumaNS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h7CumartesiPS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h7CumartesiNS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h8DiniBayramPS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h8DiniBayramNS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h9MilliBayramPS { get; set; }
        //private VariableCollection<EczaneNobetIstatistik2> _h9MilliBayramNS { get; set; } 
        #endregion

        //sonuçlar
        public EczaneNobetSonucModel Results { get; set; }
        #endregion
        /*
        #region 1. Model: Tek grup bazında nöbetçi eczane optimizasyon modelli

        #region Model: Tek grup
        private Model Model(EczaneNobetTekGrupDataModel data)
        {
            var model = new Model() { Name = "Eczane Nöbet Tekli Model" };

            #region Veriler
            //var eczaneKumulatifHedefler = data.EczaneKumulatifHedefler;
            //var nobetGrup = data.NobetGrup;
            int pespese = 2;//nobetGrup.ArdisikNobetSayisi;
                            //var eczaneNobetTarihAralik = data.EczaneNobetTarihAralik;
                            // var tarihAraligi = data.TarihAraligi;
            var gunDegerler = data.TarihAraligi.Select(s => s.GunDegerId).Distinct().ToList();
            var diniBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 8).Select(s => s.GunDegerId).Distinct().ToList();
            var milliBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 9).Select(s => s.GunDegerId).Distinct().ToList();
            var bayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId > 7).Select(s => s.GunDegerId).Distinct().ToList();
            // var eczaneNobetMazeretler = data.EczaneNobetMazeretListe;
            //var mazeretliEczaneler = data.EczaneNobetMazeretListe
            //                                 .Select(s => new { s.EczaneId, s.NobetGrupId })
            //                                 .Distinct().ToList();
            #endregion

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x", null,
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);
            #endregion

            #region Amaç Fonksiyonu
            // Add the objective
            var amac = new Objective(Expression.Sum((data.EczaneNobetTarihAralik
                                           .Select(i => _x[i]))),
                                             "Sum of all item-values: ",
                                             ObjectiveSense.Minimize);
            model.AddObjective(amac);
            #endregion

            #region Kısıtlar

            #region Talep Kısıtları
            foreach (var d in data.TarihAraligi)
            {
                //nöbet gruplarının günlük nöbetçi sayısı
                int talep = GetGunIdTalep(data.NobetGrup, d);

                model.AddConstraint(
                           Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(k => k.TakvimId == d.TakvimId)
                                            .Select(m => _x[m])) == talep,
                                            $"her güne bir eczane atanmalı, {1}");
            }
            #endregion

            #region Arz Kısıtları

            #region Peşpeşe Görev Yazılmasın
            foreach (var f in data.EczaneKumulatifHedefler)
            {
                foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespese))
                {
                    model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                       .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                   && (e.GunId >= g.GunId && e.GunId <= g.GunId + pespese)
                                             )
                                       .Select(m => _x[m])) <= 1,
                                       $"eczanelere peşpeşe nöbet yazılmasın, {f}");
                }
            }
            #endregion

            #region Her eczaneye yazılması gereken nöbetler Nöbet arzlarını(kapasitelerini-hedeflerini) ayarla

            foreach (var hedef in data.EczaneKumulatifHedefler)
            {
                #region Toplam
                //var toplamMaxArz = _data.EczaneKumulatifHedefler.Where(w => w.EczaneId == f.EczaneId).Select(s => s.Toplam).SingleOrDefault();
                //var toplamMinArz = toplamMaxArz - 1;

                model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                       .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                                       .Select(m => _x[m])) <= hedef.Toplam,
                                       $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");

                //var min = hedef.Toplam - 1 > 1 ? 1 : 0;

                model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                                        .Select(m => _x[m])) >= hedef.Toplam - 1,
                                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");

                #endregion

                #region Bayram Toplamları

                if (bayramGunDegerleri.Count() > 0)
                {
                    var temp = hedef.ToplamBayram - 1;
                    if (temp < 0) temp = 0;
                    model.AddConstraint(
                     Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                && e.GunDegerId > 7)
                                        .Select(m => _x[m])) <= hedef.ToplamBayram,
                                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                && e.GunDegerId > 7)
                                        .Select(m => _x[m])) >= temp,
                                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");
                }
                #endregion

                #region Diğer günler
                var maxArz = 1.1;
                var minArz = 0.1;

                //gunDegerler: nöbet yazılacak tarih aralığındaki hafta ve bayram günleri
                foreach (var gunDeger in gunDegerler)
                {
                    //GetEczaneGunHedef2(out maxArz, out minArz, gunDeger, f.EczaneId);

                    GetEczaneGunHedef(hedef, out maxArz, out minArz, gunDeger);

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                       && e.GunDegerId == gunDeger)
                                           .Select(m => _x[m])) <= maxArz,
                                           $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                       && e.GunDegerId == gunDeger)
                                           .Select(m => _x[m])) >= minArz,
                                           $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");
                }
                #endregion
            }
            #endregion

            #region Mazerete Görev Yazılmasın

            foreach (var f in data.EczaneNobetMazeretListe)
            {
                model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneId == f.EczaneId
                                                    && e.NobetGrupId == f.NobetGrupId
                                                    && e.TakvimId == f.TakvimId
                                                 )
                                           .Select(m => _x[m])) == 1,
                                           $"isteğe nöbet yaz, {f}");
            }

            #endregion

            #region Bayram günlerinde en fazla 1 görev yazılsın.
            //eğer bayram günleri ardışık günlerden fazlaysa
            if (data.TarihAraligi.Where(w => w.GunDegerId > 7).Count() > pespese)
            {
                foreach (var f in data.EczaneKumulatifHedefler)
                {
                    foreach (var g in data.TarihAraligi.Where(w => w.GunDegerId > 7))
                    {
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                       && e.GunId == g.GunId
                                                 )
                                           .Select(m => _x[m])) <= 1,
                                           $"bayram nöbeti sınırlama, {f}");
                    }
                }
            }
            #endregion
            #endregion
            #endregion
            return model;
        }
        #endregion

        #region Solve: Tek grup 
        public EczaneNobetSonucModel Solve(EczaneNobetTekGrupDataModel data)
        {
            var config = new Configuration
            {
                NameHandling = NameHandlingStyle.UniqueLongNames,
                ComputeRemovedVariables = true
            };

            using (var scope = new ModelScope(config))
            {
                //_dataModelTekGrup = data;
                var model = Model(data);

                // Get a solver instance, change your solver
                var solver = new CplexSolver();

                // solve the model
                var solution = solver.Solve(model);

                bool alternatifSolveCalistiMi = false;

                try
                {
                    model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));
                }
                catch (Exception)
                {
                    SolveAlt(data);
                    alternatifSolveCalistiMi = true;
                }
                // import the results back into the model 

                if (!alternatifSolveCalistiMi)
                {
                    // print objective and variable decisions
                    var objective = solution.ObjectiveValues.Single();

                    Results = new EczaneNobetSonucModel
                    {
                        ObjectiveValue = objective.Value,
                        ResultModel = new List<EczaneNobetSonucAktif>()
                    };

                    foreach (var r in data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1))
                    {
                        Results.ResultModel.Add(new EczaneNobetSonucAktif()
                        {
                            TakvimId = r.TakvimId,
                            EczaneNobetGrupId = r.EczaneNobetGrupId
                        });
                    }
                }

            }

            return Results;
        }
        #endregion

        #endregion
        */

        public EczaneNobetSonucModel Solve(EczaneNobetTekGrupDataModel data)
        {
            return _eczaneNobetTekGrupOptimization.Solve(data);
        }

        public EczaneNobetSonucModel Solve(EczaneNobetCokGrupDataModel data)
        {
            return _eczaneNobetCokGrupOptimization.Solve(data);
        }

        /*
        #region 2. Model: Eş durumu gibi nedenlerle birbirine bağlı grupların birlikte çözümü için nöbetçi eczane optimizasyon modelli

        #region Model: Eş Durumu

        private Model Model(EczaneNobetCokGrupDataModel data)
        {
            var model = new Model() { Name = "Eczane Nöbet Çoklu Model" };
            //_dataModelCokGrup = data;

            #region Veriler
            //int BigM = 1000;
            //var eczaneKumulatifHedefler = data.EczaneKumulatifHedefler;
            //var eczaneGrupTanimlar = data.EczaneGrupTanimlar.Where(f => f.BitisTarihi == null).ToList();
            //var eczaneGruplar = data.EczaneGruplar;
            //var eczaneGrupIdList = data.EczaneGruplar.Select(s => new { s.EczaneId, s.EczaneGrupTanimId }).Distinct().ToList();
            //var nobetGruplar = data.NobetGruplar.OrderBy(s => s.Id).ToList();
            //var eczaneNobetTarihAralik = data.EczaneNobetTarihAralik.ToList();
            //var tarihAraligi = data.TarihAraligi.ToList();
            var gunDegerler = data.TarihAraligi.Select(s => s.GunDegerId).Distinct().ToList();
            var diniBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 8).Select(s => s.GunDegerId).Distinct().ToList();
            var milliBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 9).Select(s => s.GunDegerId).Distinct().ToList();
            var bayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId > 7).Select(s => s.GunDegerId).Distinct().ToList();
            //var eczaneNobetMazeretler = data.EczaneNobetMazeretListe.ToList();
            //var mazeretliEczaneler = data.EczaneNobetMazeretListe
            //                                 .Select(s => new { s.EczaneId, s.NobetGrupId })
            //                                 .Distinct().ToList();
            var cozulecekAy = data.EczaneNobetTarihAralik.Select(s => s.Ay).Distinct().SingleOrDefault();

            var eczaneCiftGruplar = data.CiftGrupOlanEczaneler
                                            .Select(s => s.Id)
                                            .Distinct()
                                            .ToList();

            //var ciftGrupOlanEczaneler = data.CiftGrupOlanEczaneler.ToList();

            var eczaneCiftGruplar2 = data.CiftGrupOlanEczaneler2
                                            .Select(s => s.Id)
                                            .Distinct()
                                            .ToList();

            //var ciftGrupluEczaneler2 = data.CiftGrupOlanEczaneler2.ToList();

            //var ayIcindekiCiftGrupluEczaneler = data.AyIcindekiCiftGrupOlanEczaneler;

            //var eczaneNobetIstekler = data.EczaneNobetIstekListe;
            //var eskiTakvimIdler = new List<int>();
            //var eczaneNobetSonucDetaylar = new List<EczaneNobetSonucListe>();

            //if (cozulecekAy > 1)
            //{
            //    eczaneNobetSonucDetaylar = _dataModelCokGrup.EczaneNobetSonucDetaylar;
            //    eskiTakvimIdler = eczaneNobetSonucDetaylar.Select(s => s.TakvimId).Distinct().ToList();
            //}
            #endregion

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x", null,
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            //_y = new VariableCollection<EczaneNobetTarihAralik>(
            //        _modelCokGrup,
            //        data.EczaneNobetTarihAralik,
            //        "_y", null,
            //        h => _dataModelCokGrup.LowerBound,
            //        h => _dataModelCokGrup.UpperBound,
            //        a => VariableType.Binary);
            #endregion

            #region Amaç Fonksiyonu
            // Add the objective

            var amac = new Objective(Expression.Sum((data.EczaneNobetTarihAralik
                                           .Select(i => _x[i]))),
                                             "Sum of all item-values: ",
                                             ObjectiveSense.Minimize);
            model.AddObjective(amac);

            #endregion

            #region Kısıtlar

            #region Talep Kısıtları
            //nöbet grubu bazında olacak
            foreach (var nobetGrup in data.NobetGruplar)
            {
                foreach (var d in data.TarihAraligi)
                {
                    //nöbet gruplarının günlük nöbetçi sayısı
                    int talep = GetGunIdTalep(nobetGrup, d);

                    model.AddConstraint(
                               Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(k => k.TakvimId == d.TakvimId
                                                         && k.NobetGrupId == nobetGrup.Id
                                                )
                                                .Select(m => _x[m])) == 1,
                                                $"her güne bir eczane atanmalı, {1}");
                }
            }

            #endregion

            #region Arz Kısıtları

            #region Peşpeşe Aynı Grup Oluşmasın - Kümülatif Çift Grup Engelleme 

            #region kümülatif toplam
            //foreach (var ciftGrup in eczaneCiftGruplar)
            //{
            //    var eczaneler = data.CiftGrupOlanEczaneler.Where(s => s.Id == ciftGrup).Select(w => w.EczaneId).ToList();

            //    foreach (var tarih in data.TarihAraligi)
            //    {
            //        _modelCokGrup.AddConstraint(
            //          Expression.Sum(data.EczaneNobetTarihAralik
            //                           .Where(e => eczaneler.Contains(e.EczaneId)
            //                                    && e.TakvimId == tarih.TakvimId)
            //                           .Select(m => _x[m])) <= 1,
            //                           $"eczaneler peşpeşe aynı grup olmasın, {tarih}");
            //    }
            //} 
            #endregion

            #region bir önceki aydaki bir eksik frekans değerine göre

            foreach (var ciftGrup in eczaneCiftGruplar2)
            {
                var eczaneler = data.CiftGrupOlanEczaneler2.Where(s => s.Id == ciftGrup).Select(w => w.EczaneId).ToList();

                foreach (var tarih in data.TarihAraligi)
                {
                    model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                       .Where(e => eczaneler.Contains(e.EczaneId)
                                                && e.TakvimId == tarih.TakvimId)
                                       .Select(m => _x[m])) <= 1,
                                       $"eczaneler peşpeşe aynı grup olmasın, {tarih}");
                }
            }

            //if (dataModel.Ay > 3)
            //{
            //    foreach (var tarih in data.TarihAraligi)
            //    {
            //        _modelCokGrup.AddConstraint(
            //          Expression.Sum(data.EczaneNobetTarihAralik
            //                           .Where(e => e.EczaneId == 32 || e.EczaneId == 43
            //                                    && e.TakvimId == tarih.TakvimId)
            //                           .Select(m => _x[m])) <= 1,
            //                           $"eczaneler peşpeşe aynı grup olmasın1, {tarih}");
            //    }
            //}

            #endregion

            #region v5

            //ay içinde iki kez nöbet tutan eczane çiftleri nedeniyle çözüm yeniden yapılıyor.  
            //Yeni modelde bu çiftlerin herhangi iki gündeki toplamları 3'ten küçük olma kısıtı eklenince 
            //sadece 1 kez çift olmaları sağlanıyor.

            //var _ayIcindekiCiftGrupluEczaneler = data.AyIcindekiCiftGrupOlanEczaneler.Select(s => s.Id).Distinct().ToList();

            //foreach (var g in eczaneCiftGruplar2)
            //{
            //    var ikiliEczaneler = data.CiftGrupOlanEczaneler2
            //                                    .Where(w => w.Id == g)
            //                                    .Select(s => s.EczaneId).ToList();

            //    foreach (var tarih in data.TarihAraligi.Take(data.TarihAraligi.Count() - 1))
            //    {
            //        foreach (var tarih2 in data.TarihAraligi.Where(s => s.TakvimId > tarih.TakvimId))
            //        {
            //            _modelCokGrup.AddConstraint(
            //                  Expression.Sum(data.EczaneNobetTarihAralik
            //                                   .Where(e => ikiliEczaneler.Contains(e.EczaneId)
            //                                            && (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId))
            //                                   .Select(m => _x[m])) <= 3,
            //                                   $"her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun, {g}");
            //        }
            //    }
            //}
            #endregion

            #endregion

            #region Peşpeşe Görev Yazılmasın

            foreach (var nobetGrup in data.NobetGruplar)
            {
                int pespese = 2;// nobetGrup.ArdisikNobetSayisi;

                foreach (var f in data.EczaneKumulatifHedefler.Where(w => w.NobetGrupId == nobetGrup.Id))
                {
                    foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespese))
                    {
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                       && (e.GunId >= g.GunId && e.GunId <= g.GunId + pespese)
                                                       && e.NobetGrupId == nobetGrup.Id
                                                       )
                                           .Select(m => _x[m])) <= 1,
                                           $"eczanelere peşpeşe nöbet yazılmasın, {f}");
                    }
                }
            }

            #endregion

            #region Her eczaneye yazılması gereken nöbetler Nöbet arzlarını(kapasitelerini-hedeflerini) ayarla

            foreach (var nobetGrup in data.NobetGruplar)
            {
                foreach (var hedef in data.EczaneKumulatifHedefler.Where(w => w.NobetGrupId == nobetGrup.Id))
                {
                    #region Toplam Hedefler

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    && e.NobetGrupId == nobetGrup.Id)
                                           .Select(m => _x[m])) <= hedef.Toplam,
                                           $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    && e.NobetGrupId == nobetGrup.Id)
                                            .Select(m => _x[m])) >= hedef.Toplam - 1,
                                            $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");
                    #endregion

                    #region Bayram Toplam Hedefleri

                    if (bayramGunDegerleri.Count() > 0)
                    {
                        var temp = hedef.ToplamBayram - 1;
                        if (temp < 0) temp = 0;
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    && e.GunDegerId > 7)
                                            .Select(m => _x[m])) <= hedef.ToplamBayram,
                                            $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    && e.GunDegerId > 7)
                                            .Select(m => _x[m])) >= temp,
                                            $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");
                    }
                    #endregion

                    #region Diğer Günlerin Hedefleri
                    var maxArz = 1.1;
                    var minArz = 0.1;

                    var gunler = new List<int> { 7, 8, 9 };
                    //var gunler2 = new List<int> { 2, 3, 4, 5, 6 };
                    //gunler2.ForEach(a => gunler.Add(a));

                    //gunDegerler: nöbet yazılacak tarih aralığındaki hafta ve bayram günleri
                    foreach (var gunDeger in gunDegerler.Where(x => gunler.Contains(x)))
                    {
                        GetEczaneGunHedef(hedef, out maxArz, out minArz, gunDeger);

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                           && e.GunDegerId == gunDeger
                                                           && e.NobetGrupId == nobetGrup.Id)
                                               .Select(m => _x[m])) <= maxArz,
                                               $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                           && e.GunDegerId == gunDeger
                                                           && e.NobetGrupId == nobetGrup.Id)
                                               .Select(m => _x[m])) >= minArz,
                                               $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");
                    }
                    #endregion
                }
            }
            #endregion

            #region Her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun

            #region v1

            //foreach (var hedef in data.EczaneKumulatifHedefler)
            //{
            //    var digerEczaneler = data.EczaneKumulatifHedefler.Where(s => s.NobetGrupId > hedef.NobetGrupId).ToList();

            //    foreach (var hedef2 in digerEczaneler)
            //    {
            //        var eczaneler = new List<int>
            //            {
            //                hedef.EczaneId,
            //                hedef2.EczaneId
            //            };

            //        foreach (var tarih in data.TarihAraligi)
            //        {
            //            foreach (var tarih2 in data.TarihAraligi.Where(s => s.TakvimId > tarih.TakvimId))
            //            {
            //                var sonuc = data.EczaneNobetTarihAralik
            //                                        .Where(e => eczaneler.Contains(e.EczaneId)
            //                                                 && (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId))
            //                                        .Select(m => _x[m]).ToList();

            //                _modelCokGrup.AddConstraint(
            //                      Expression.Sum(data.EczaneNobetTarihAralik
            //                                       .Where(e => eczaneler.Contains(e.EczaneId)
            //                                                && (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId))
            //                                       .Select(m => _x[m])) <= 3,
            //                                       $"her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun, {hedef}");
            //            }
            //        }
            //    }
            //}
            #endregion

            #region v2
            //foreach (var eczane1 in data.EczaneKumulatifHedefler)
            //{
            //    var digerEczaneler = data.EczaneKumulatifHedefler.Where(s => s.NobetGrupId > eczane1.NobetGrupId).ToList();

            //    foreach (var eczane2 in digerEczaneler)
            //    {
            //        var eczaneler = new List<int>
            //            {
            //                eczane1.EczaneId,
            //                eczane2.EczaneId
            //            };

            //        foreach (var tarih in data.TarihAraligi)
            //        {
            //            _modelCokGrup.AddConstraint(
            //                  Expression.Sum(data.EczaneNobetTarihAralik
            //                                   .Where(e => eczaneler.Contains(e.EczaneId)
            //                                            && (e.TakvimId == tarih.TakvimId))
            //                                   .Select(m => _x[m])) <= 2 + ((data.EczaneNobetTarihAralik
            //                                                                            .Where(e => e.EczaneId == eczane1.EczaneId
            //                                                                                    && e.TakvimId == tarih.TakvimId)
            //                                                                            .Select(m => _y[m])).SingleOrDefault() - 1),
            //                            $"her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun, {eczane1}");
            //        }
            //    }

            //    _modelCokGrup.AddConstraint(
            //                  Expression.Sum(data.EczaneNobetTarihAralik
            //                                   .Where(e => e.EczaneId == eczane1.EczaneId)
            //                                   .Select(m => _y[m])) <= 1 ,
            //                                   $"her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun2, {eczane1}");
            //} 
            #endregion

            #region v3

            //var _ayIcindekiCiftGrupluEczaneler = data.AyIcindekiCiftGrupOlanEczaneler.Select(s => s.Id).Distinct().ToList();

            //foreach (var g in _ayIcindekiCiftGrupluEczaneler)
            //{
            //    foreach (var tarih in data.TarihAraligi)
            //    {
            //        _modelCokGrup.AddConstraint(
            //                 Expression.Sum(data.EczaneNobetTarihAralik
            //                                  .Where(e => data.AyIcindekiCiftGrupOlanEczaneler
            //                                                .Where(w => w.Id == g)
            //                                                .Select(s => s.EczaneId).Contains(e.EczaneId)
            //                                           && e.TakvimId == tarih.TakvimId)
            //                                  .Select(m => _x[m])) <= 1,
            //                                  $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {tarih.GunId}");
            //    }
            //}
            #endregion

            #region v4

            //ay içinde iki kez nöbet tutan eczane çiftleri nedeniyle çözüm yeniden yapılıyor.  
            //Yeni modelde bu çiftlerin herhangi iki gündeki toplamları 3'ten küçük olma kısıtı eklenince 
            //sadece 1 kez çift olmaları sağlanıyor.

            var _ayIcindekiCiftGrupluEczaneler = data.AyIcindekiCiftGrupOlanEczaneler.Select(s => s.Id).Distinct().ToList();

            foreach (var g in _ayIcindekiCiftGrupluEczaneler)
            {
                var ikiliEczaneler = data.AyIcindekiCiftGrupOlanEczaneler
                                                .Where(w => w.Id == g)
                                                .Select(s => s.EczaneId).ToList();

                foreach (var tarih in data.TarihAraligi.Take(data.TarihAraligi.Count() - 1))
                {
                    foreach (var tarih2 in data.TarihAraligi.Where(s => s.TakvimId > tarih.TakvimId))
                    {
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => ikiliEczaneler.Contains(e.EczaneId)
                                                        && (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId))
                                               .Select(m => _x[m])) <= 3,
                                               $"her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun, {g}");
                    }
                }
            }
            #endregion

            #endregion

            #region Eş durumu (karı-koca) kısıtı

            //gelen nöbet gruplarının listesi alınacak
            //Birbirine yakın farklı gruplardaki eczanelere sürekli aynı gün nöbet yazılmasın
            //İki farklı eczanenin (eş durumu) birlikte nöbet tutma durumu
            //ilk çalışan gruptaki kişinin görevi diğer ilişkili gruptaki kişi için mazeret olarak eklenecek
            //bu hususu gerekirse daha sonra modeli bütün olarak ele alıp sonuç verilecek
            //pazar günleri alanya için özel sıra ile verildiğinden eşler bu gün için geçerli değildir.
            foreach (var g in data.EczaneGrupTanimlar)
            {
                int pespese = g.ArdisikNobetSayisi;

                var eczaneler = data.EczaneGruplar
                                        .Where(x => x.EczaneGrupTanimId == g.Id)
                                        .Select(s => s.EczaneId).Distinct().ToList();

                foreach (var tarih in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespese))
                {
                    var grupTanimlar = Expression.Sum(data.EczaneNobetTarihAralik
                                                            .Where(e => eczaneler.Contains(e.EczaneId)
                                                                     && (e.GunId >= tarih.GunId && e.GunId <= tarih.GunId + pespese)
                                                                     && e.GunDegerId != 1
                                                                  )
                                                            .Select(m => _x[m])) <= 1;

                    model.AddConstraint(grupTanimlar,
                                                $"her_eczaneye_bir_ayda_nobet_grubunun_hedefi_kadar_nobet_yazilmali, {tarih.GunId}");
                }
            }

            #endregion

            #region İsteğe Görev Yazılsın

            foreach (var f in data.EczaneNobetIstekListe)
            {
                model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneId == f.EczaneId
                                                    && e.NobetGrupId == f.NobetGrupId
                                                    && e.TakvimId == f.TakvimId
                                                 )
                                           .Select(m => _x[m])) == 1,
                                           $"isteğe nöbet yaz, {f}");
            }


            #endregion

            #region Mazerete Görev Yazılmasın

            foreach (var f in data.EczaneNobetMazeretListe)
            {
                model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneId == f.EczaneId
                                                    && e.NobetGrupId == f.NobetGrupId
                                                    && e.TakvimId == f.TakvimId
                                                 )
                                           .Select(m => _x[m])) == 1,
                                           $"isteğe nöbet yaz, {f}");
            }
            #endregion

            #region Bayram günlerinde en fazla 1 görev yazılsın.

            //eğer bayram günleri ardışık günlerden fazlaysa
            foreach (var nobetGrup in data.NobetGruplar)
            {
                if (data.TarihAraligi.Where(w => w.GunDegerId > 7).Count() > 2) // nobetGrup.ArdisikNobetSayisi)
                {
                    foreach (var f in data.EczaneKumulatifHedefler.Where(x => x.NobetGrupId == nobetGrup.Id))
                    {
                        foreach (var g in data.TarihAraligi.Where(w => w.GunDegerId > 7))
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                           && e.GunId == g.GunId
                                                     )
                                               .Select(m => _x[m])) <= 1,
                                               $"bayram nöbeti sınırlama, {f}");
                        }
                    }
                }
            }


            #endregion

            #endregion

            #endregion
            return model;
        }
        #endregion

        #region Solve: Çok grup 
        public EczaneNobetSonucModel Solve(EczaneNobetCokGrupDataModel data) 
        {
            var config = new Configuration
            {
                NameHandling = NameHandlingStyle.UniqueLongNames,
                ComputeRemovedVariables = true
            };

            using (var scope = new ModelScope(config))
            {
                //_dataModelCokGrup = data;
                var model = Model(data);

                // Get a solver instance, change your solver
                var solver = new CplexSolver();

                //bool alternatifSolveCalistiMi = false;

                try
                {
                    // solve the model
                    var solution = solver.Solve(model);
                    //var cplexConfiguration = solver.Configuration;

                    var modelStatus = solution.ModelStatus;
                    var solutionStatus = solution.Status;
                    var modelName = solution.ModelName;
                    var bestBound = solution.BestBound;

                    //var confilicts = new ConflictingSet();
                    //confilicts = solution.ConflictingSet;
                    //ConstraintsUB = new IEnumerable<Constraint>();
                    //ConstraintsUB = confilicts.ConstraintsUB;

                    //var kisitAdlari = new List<string>();

                    //foreach (var item in ConstraintsUB)
                    //{
                    //    kisitAdlari.Add(item.Name);
                    //}

                    if (modelStatus != ModelStatus.Feasible)
                    {
                        throw new Exception("Uygun çözüm bulunamadı!");
                    }
                    else
                    {
                        // import the results back into the model 
                        model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));

                        //if (!alternatifSolveCalistiMi)
                        //{
                        // print objective and variable decisions
                        var objective = solution.ObjectiveValues.Single();
                        var sure = solution.OverallWallTime;

                        Results = new EczaneNobetSonucModel
                        {
                            CozumSuresi = sure,
                            ObjectiveValue = objective.Value,
                            ResultModel = new List<EczaneNobetSonucAktif>()
                        };

                        foreach (var r in data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1))
                        {
                            Results.ResultModel.Add(new EczaneNobetSonucAktif()
                            {
                                TakvimId = r.TakvimId,
                                EczaneNobetGrupId = r.EczaneNobetGrupId,
                                NobetGorevTipId = 1
                            });
                        }
                        //}
                    }

                }
                catch (Exception ex)
                {
                    var mesaj = ex.Message;

                    //throw new Exception("Uygun çözüm bulunamadı!");
                    SolveCokAlt(data);
                    //alternatifSolveCalistiMi = true;
                }
            }

            return Results;
        }
        #endregion

        #endregion

        */

        #region 3. Model: Hedef Programlama Tek grup bazında sapma değişkenli nöbetçi eczane optimizasyon modelli

        #region Model: Tek grup sapma değişkenli
        private Model Model(EczaneNobetTekGrupSapmaDataModel data)
        {
            var model = new Model() { Name = "Eczane Nöbet Tekli Model" };
            //_dataModelTekGrupSapma = _dataModelTekGrupSapma;

            #region Veriler
            //_eczaneKumulatifHedefler2 = _dataModel.EczaneKumulatifHedefler2;
            //var _data.EczaneKumulatifHedefler = _dataModelTekGrup.data.EczaneKumulatifHedefler;
            //var nobetGrup = data.NobetGrup;
            int pespese = 2; //nobetGrup.ArdisikNobetSayisi;
                             //  var eczaneNobetTarihAralik = data.EczaneNobetTarihAralik;
                             //var tarihAraligi = data.TarihAraligi;
            var gunDegerler = data.TarihAraligi.Select(s => s.GunDegerId).Distinct().ToList();
            var diniBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 8).Select(s => s.GunDegerId).Distinct().ToList();
            var milliBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 9).Select(s => s.GunDegerId).Distinct().ToList();
            var bayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId > 7).Select(s => s.GunDegerId).Distinct().ToList();
            // var eczaneNobetMazeretler = data.EczaneNobetMazeretListe;
            //var mazeretliEczaneler = data.EczaneNobetMazeretListe
            //                                 .Select(s => new { s.EczaneId, s.NobetGrupId })
            //                                 .Distinct().ToList();

            //_nobetGrupKumulatifHedefler = _dataModel.NobetGrupKumulatifHedefler.Where(w => w.Ay == _dataModel.Ay).ToList();
            //_istatistikler = _dataModel.EczaneNobetIstatistikler;
            //eczaneNobetGruplar: Nöbet grubundaki eczanler--gelen nöbet grubu hesaplanmalı
            //var eczaneNobetGruplar = _dataModel.EczaneNobetGruplar;
            #endregion

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x", null,
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            #region Sapma Değişkenleri
            //int BigM = 1000;
            //h*ps: hedef * .. günü pozitif sapma değişkeni
            //h*ns: hedef * .. günü negatif sapma değişkeni
            //int sapmaUpperBonud = 2;

            //_h1PazarPS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h1PazarPS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);

            //_h1PazarNS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h1PazarNS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);


            //_h2PazartesiPS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "h2PazartesiPS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);

            //_h2PazartesiNS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h2PazartesiNS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);


            //_h3SaliPS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h3SaliPS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);

            //_h3SaliNS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h3SaliNS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);


            //_h4CarsambaPS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h4CarsambaPS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);

            //_h4CarsambaNS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h4CarsambaNS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);


            //_h5PersembePS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h5PersembePS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);

            //_h5PersembeNS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h5PersembeNS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);


            //_h6CumaPS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h6CumaPS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);

            //_h6CumaNS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h6CumaNS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);


            //_h7CumartesiPS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h7CumartesiPS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);

            //_h7CumartesiNS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h7CumartesiNS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);


            //_h8DiniBayramPS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h8DiniBayramPS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);

            //_h8DiniBayramNS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h8DiniBayramNS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);


            //_h9MilliBayramPS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h9MilliBayramPS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);

            //_h9MilliBayramNS = new VariableCollection<EczaneNobetIstatistik2>(
            //                    _model,
            //                    _eczaneKumulatifHedefler2,
            //                    "_h9MilliBayramNS", null,
            //                    h => _dataModel.LowerBound,
            //                    h => sapmaUpperBonud,
            //                    a => VariableType.Continuous);
            #endregion

            #endregion

            #region Amaç Fonksiyonu
            // Add the objective

            var amac = new Objective(Expression.Sum((data.EczaneNobetTarihAralik
                                           .Select(i => _x[i]))),
                                             "Sum of all item-values: ",
                                             ObjectiveSense.Minimize);

            #region Eski Amaç Fonksiyonu

            //var amac = new Objective(Expression.Sum((from i in data.EczaneNobetTarihAralik
            //                            from j in _eczaneKumulatifHedefler
            //                            where i.EczaneNobetGrupId == j.EczaneNobetGrupId
            //                            select (x[i] + h1PazarPS[j]*100)).ToList(),
            //                            "Sum of all item-values: ",
            //                            ObjectiveSense.Minimize);

            //var amac2 = new Objective(Expression.Sum((from i in data.EczaneNobetTarihAralik
            //                                select _x[i]
            //                                )),
            //                                "Sum of all item-values: ",
            //                                ObjectiveSense.Minimize);

            #endregion

            #region Sapmalı Değişkenli Amaç Fonksiyonu
            //var amac = new Objective(Expression.Sum((from i in data.EczaneNobetTarihAralik
            //                                         from j in _eczaneKumulatifHedefler2
            //                                         where i.EczaneNobetGrupId == j.EczaneNobetGrupId
            //                                         select (_x[i] 
            //                                                 //+ _h1PazarPS[j] * BigM
            //                                                 //+ _h2PazartesiPS[j] * BigM
            //                                                 //+ _h3SaliPS[j] * BigM
            //                                                 //+ _h4CarsambaPS[j] * BigM
            //                                                 //+ _h5PersembePS[j] * BigM
            //                                                 //+ _h6CumaPS[j] * BigM
            //                                                 //+ _h7CumartesiPS[j] * BigM
            //                                                 //+ _h8DiniBayramPS[j] * BigM
            //                                                 //+ _h9MilliBayramPS[j] * BigM
            //                                                ))),
            //                                           "Sum of all item-values: ",
            //                                           ObjectiveSense.Minimize);
            #endregion

            model.AddObjective(amac);

            #endregion

            #region Kısıtlar

            #region Talep Kısıtları

            foreach (var d in data.TarihAraligi)
            {
                //nöbet gruplarının günlük nöbetçi sayısı
                int talep = GetGunIdTalep(data.NobetGrup, d);

                model.AddConstraint(
                           Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(k => k.TakvimId == d.TakvimId)
                                            .Select(m => _x[m])) == talep,
                                            $"her güne bir eczane atanmalı, {1}");
            }

            #endregion

            #region Arz Kısıtları

            #region Peşpeşe Görev Yazılmasın
            foreach (var f in data.EczaneKumulatifHedefler)
            {
                //foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespese))
                //{
                //    model.AddConstraint(
                //      Expression.Sum(data.EczaneNobetTarihAralik
                //                       .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                //                                   && (e.Gun >= g.GunId && e.Gun <= g.GunId + pespese)
                //                             )
                //                       .Select(m => _x[m])) <= 1,
                //                       $"eczanelere peşpeşe nöbet yazılmasın, {f}");
                //}
            }
            #endregion

            #region Her eczaneye yazılması gereken nöbetler Nöbet arzlarını(kapasitelerini-hedeflerini) ayarla

            foreach (var hedef in data.EczaneKumulatifHedefler)
            {
                #region Toplam
                //var toplamMaxArz = data.EczaneKumulatifHedefler.Where(w => w.EczaneId == f.EczaneId).Select(s => s.Toplam).SingleOrDefault();
                //var toplamMinArz = toplamMaxArz - 1;

                model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                       .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                                       .Select(m => _x[m])) <= hedef.Toplam,
                                       $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");

                model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                                        .Select(m => _x[m])) >= hedef.Toplam - 1,
                                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");
                #endregion

                #region Bayram Toplamları

                if (bayramGunDegerleri.Count() > 0)
                {
                    var temp = hedef.ToplamBayram - 1;
                    if (temp < 0) temp = 0;
                    model.AddConstraint(
                     Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                && e.NobetGunKuralId > 7)
                                        .Select(m => _x[m])) <= hedef.ToplamBayram,
                                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                && e.NobetGunKuralId > 7)
                                        .Select(m => _x[m])) >= temp,
                                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");
                }
                #endregion

                #region Diğer günler
                var maxArz = 1.1;
                var minArz = 0.1;

                //gunDegerler: nöbet yazılacak tarih aralığındaki hafta ve bayram günleri
                foreach (var gunDeger in gunDegerler)
                {
                    //GetEczaneGunHedef2(out maxArz, out minArz, gunDeger, f.EczaneId);

                    GetEczaneGunHedef(hedef, out maxArz, out minArz, gunDeger);

                    #region Hedef Kısıtlar -- Sapma değişkenleri
                    //_model.AddConstraint(
                    //        Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                       from j in data.EczaneKumulatifHedefler2
                    //                       where i.EczaneNobetGrupId == j.EczaneNobetGrupId
                    //                             && i.GunDegerId == 1
                    //                       select (_x[i] + _h1PazarNS[j] - _h1PazarPS[j])) == hedef.Pazar,
                    //                        $"her eczaneye bir ayda nöbet grubunun {1} hedefi kadar nöbet yazılmalı, {hedef.Pazar}");

                    //_model.AddConstraint(
                    //         Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                        from j in data.EczaneKumulatifHedefler2
                    //                        where i.EczaneNobetGrupId == j.EczaneNobetGrupId
                    //                              && i.GunDegerId == 2
                    //                        select (_x[i] + _h2PazartesiNS[j] - _h2PazartesiPS[j])) == hedef.Pazartesi,
                    //                         $"her eczaneye bir ayda nöbet grubunun {2} hedefi kadar nöbet yazılmalı, {hedef.Pazartesi}");

                    //_model.AddConstraint(
                    //         Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                        from j in data.EczaneKumulatifHedefler2
                    //                        where i.EczaneNobetGrupId == j.EczaneNobetGrupId
                    //                              && i.GunDegerId == 3
                    //                        select (_x[i] + _h3SaliNS[j] - _h3SaliPS[j])) == hedef.Sali,
                    //                         $"her eczaneye bir ayda nöbet grubunun {3} hedefi kadar nöbet yazılmalı, {hedef.Sali}");

                    //_model.AddConstraint(
                    //         Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                        from j in data.EczaneKumulatifHedefler2
                    //                        where i.EczaneNobetGrupId == j.EczaneNobetGrupId
                    //                              && i.GunDegerId == 4
                    //                        select (_x[i] + _h4CarsambaNS[j] - _h4CarsambaPS[j])) == hedef.Carsamba,
                    //                         $"her eczaneye bir ayda nöbet grubunun {4} hedefi kadar nöbet yazılmalı, {hedef.Carsamba}");

                    //_model.AddConstraint(
                    //         Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                        from j in eczaneKumulatifHedefler2
                    //                        where i.EczaneNobetGrupId == j.EczaneNobetGrupId
                    //                              && i.GunDegerId == 5
                    //                        select (_x[i] + _h5PersembeNS[j] - _h5PersembePS[j])) == hedef.Persembe,
                    //                         $"her eczaneye bir ayda nöbet grubunun {5} hedefi kadar nöbet yazılmalı, {hedef.Persembe}");

                    //_model.AddConstraint(
                    //         Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                        from j in eczaneKumulatifHedefler2
                    //                        where i.EczaneNobetGrupId == j.EczaneNobetGrupId
                    //                              && i.GunDegerId == 6
                    //                        select (_x[i] + _h6CumaNS[j] - _h6CumaPS[j])) == hedef.Cuma,
                    //                         $"her eczaneye bir ayda nöbet grubunun {6} hedefi kadar nöbet yazılmalı, {hedef.Cuma}");

                    //_model.AddConstraint(
                    //         Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                        from j in eczaneKumulatifHedefler2
                    //                        where i.EczaneNobetGrupId == j.EczaneNobetGrupId
                    //                              && i.GunDegerId == 7
                    //                        select (_x[i] + _h7CumartesiNS[j] - _h7CumartesiPS[j])) == hedef.Cumartesi,
                    //                         $"her eczaneye bir ayda nöbet grubunun {7} hedefi kadar nöbet yazılmalı, {hedef.Cumartesi}"); 


                    //if (diniBayramGunDegerleri.Count() > 0)
                    //{
                    //    _model.AddConstraint(
                    //             Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                            from j in eczaneKumulatifHedefler2
                    //                            where i.EczaneNobetGrupId == j.EczaneNobetGrupId
                    //                                  && i.GunDegerId == 8
                    //                            select (_x[i] + _h8DiniBayramNS[j] - _h8DiniBayramPS[j])) == hedef.DiniBayram,
                    //                             $"her eczaneye bir ayda nöbet grubunun {8} hedefi kadar nöbet yazılmalı, {hedef.DiniBayram}");
                    //}

                    //if (milliBayramGunDegerleri.Count() > 0)
                    //{
                    //_model.AddConstraint(
                    //         Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                        from j in data.EczaneKumulatifHedefler2
                    //                        where i.EczaneNobetGrupId == j.EczaneNobetGrupId
                    //                              && i.GunDegerId == 9
                    //                        select (_x[i] + _h9MilliBayramNS[j] - _h9MilliBayramPS[j])) == hedef.MilliBayram,
                    //                         $"her eczaneye bir ayda nöbet grubunun {9} hedefi kadar nöbet yazılmalı, {hedef.MilliBayram}");
                    //}

                    #endregion

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                       && e.NobetGunKuralId == gunDeger)
                                           .Select(m => _x[m])) <= maxArz,
                                           $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                       && e.NobetGunKuralId == gunDeger)
                                           .Select(m => _x[m])) >= minArz,
                                           $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");
                }
                #endregion
            }

            #endregion

            #region Mazerete Görev Yazılmasın

            foreach (var f in data.EczaneNobetMazeretListe)
            {
                model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneId == f.EczaneId
                                                    && e.NobetGrupId == f.NobetGrupId
                                                    && e.TakvimId == f.TakvimId
                                                 )
                                           .Select(m => _x[m])) == 1,
                                           $"isteğe nöbet yaz, {f}");
            }

            #endregion

            #region Bayram günlerinde en fazla 1 görev yazılsın.
            //eğer bayram günleri ardışık günlerden fazlaysa
            if (data.TarihAraligi.Where(w => w.GunDegerId > 7).Count() > pespese)
            {
                foreach (var f in data.EczaneKumulatifHedefler)
                {
                    foreach (var g in data.TarihAraligi.Where(w => w.GunDegerId > 7))
                    {
                        //model.AddConstraint(
                        //  Expression.Sum(data.EczaneNobetTarihAralik
                        //                   .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                        //                               && e.Gun == g.GunId
                        //                         )
                        //                   .Select(m => _x[m])) <= 1,
                        //                   $"bayram nöbeti sınırlama, {f}");
                    }
                }
            }

            #endregion

            #endregion

            #endregion
            return model;
        }
        #endregion

        #region Solve: Sapma Değişkenli Tek grup 
        public EczaneNobetSonucModel Solve(EczaneNobetTekGrupSapmaDataModel data)
        {
            var config = new Configuration
            {
                NameHandling = NameHandlingStyle.UniqueLongNames,
                ComputeRemovedVariables = true
            };

            using (var scope = new ModelScope(config))
            {
                //_dataModelTekGrupSapma = data;

                var model = Model(data);

                // Get a solver instance, change your solver
                var solver = new CplexSolver();

                // solve the model
                var solution = solver.Solve(model);

                try
                {
                    model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));
                }
                catch (Exception)
                {
                    ;
                }
                // import the results back into the model 


                // print objective and variable decisions
                var objective = solution.ObjectiveValues.Single();

                Results = new EczaneNobetSonucModel
                {
                    ObjectiveValue = objective.Value,
                    ResultModel = new List<EczaneNobetCozum>()
                };

                foreach (var r in data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1))
                {
                    Results.ResultModel.Add(new EczaneNobetCozum()
                    {
                        TakvimId = r.TakvimId,
                        EczaneNobetGrupId = r.EczaneNobetGrupId
                    });
                }
            }

            return Results;
        }
        #endregion

        #endregion

        /*
        #region 4. Model: Tek grup bazında toplam hedefin minimum değeri için alternatifli nöbetçi eczane optimizasyon modelli

        #region Model: Tek grup alternatifli
        private Model ModelAlt(EczaneNobetTekGrupDataModel data)
        {
            var model = new Model() { Name = "Eczane Nöbet Tekli Minimum Maliyetli Model" };
            //_dataModelTekGrup = dataModel;

            #region Veriler
            //var BigM = 99999;
            // var data.EczaneKumulatifHedefler = data.EczaneKumulatifHedefler;
            //var nobetGrup = data.NobetGrup;
            int pespese = 2;// nobetGrup.ArdisikNobetSayisi;
                            // var data.EczaneNobetTarihAralik = data.EczaneNobetTarihAralik;
                            // var tarihAraligi = data.TarihAraligi;
            var gunDegerler = data.TarihAraligi.Select(s => s.GunDegerId).Distinct().ToList();
            var diniBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 8).Select(s => s.GunDegerId).Distinct().ToList();
            var milliBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 9).Select(s => s.GunDegerId).Distinct().ToList();
            var bayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId > 7).Select(s => s.GunDegerId).Distinct().ToList();
            // var eczaneNobetMazeretler = data.EczaneNobetMazeretListe;
            //var mazeretliEczaneler = data.EczaneNobetMazeretListe
            //                                 .Select(s => new { s.EczaneId, s.NobetGrupId })
            //                                 .Distinct().ToList();

            #endregion

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x", null,
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            //_y = new VariableCollection<EczaneNobetIstatistik>(
            //        _modelTekGrup,
            //        data.EczaneKumulatifHedefler,
            //        "_y", null,
            //        h => _dataModelTekGrup.LowerBound,
            //        h => _dataModelTekGrup.UpperBound,
            //        a => VariableType.Binary);

            #endregion

            #region Amaç Fonksiyonu
            // Add the objective

            var amac = new Objective(Expression.Sum((data.EczaneNobetTarihAralik
                                           .Select(i => _x[i]))),
                                             "Sum of all item-values: ",
                                             ObjectiveSense.Minimize);

            model.AddObjective(amac);

            #endregion

            #region Kısıtlar

            #region Talep Kısıtları

            foreach (var d in data.TarihAraligi)
            {
                //nöbet gruplarının günlük nöbetçi sayısı
                int talep = GetGunIdTalep(data.NobetGrup, d);

                model.AddConstraint(
                           Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(k => k.TakvimId == d.TakvimId)
                                            .Select(m => _x[m])) == talep,
                                            $"her güne bir eczane atanmalı, {1}");
            }

            #endregion

            #region Arz Kısıtları

            #region Peşpeşe Görev Yazılmasın
            foreach (var f in data.EczaneKumulatifHedefler)
            {
                foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespese))
                {
                    model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                       .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                   && (e.GunId >= g.GunId && e.GunId <= g.GunId + pespese)
                                             )
                                       .Select(m => _x[m])) <= 1,
                                       $"eczanelere peşpeşe nöbet yazılmasın, {f}");
                }
            }
            #endregion

            #region Her eczaneye yazılması gereken nöbetler Nöbet arzlarını(kapasitelerini-hedeflerini) ayarla

            foreach (var hedef in data.EczaneKumulatifHedefler)
            {
                #region Toplam

                var gruptakiEczaneler = data.EczaneKumulatifHedefler.Select(s => s.EczaneId).Distinct().ToList();
                int maxToplam;
                if (gruptakiEczaneler.Count == 39)
                {
                    maxToplam = hedef.Toplam <= 1 ? 2 : 1;
                }
                else
                {
                    maxToplam = hedef.Toplam;
                }

                model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                       .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                                       .Select(m => _x[m])) <= maxToplam,
                                       $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");

                var minToplam = hedef.Toplam - 1 > 1 ? 1 : 0;

                model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                                        .Select(m => _x[m])) >= minToplam,
                                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");

                #region Ya ya da kısıtları
                //var _yValue = data.EczaneKumulatifHedefler
                //                    .Where(f => f.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                //                    .Select(g => g.EczaneNobetGrupId).SingleOrDefault();

                //_modelTekGrupAlt.AddConstraint(
                //      Expression.Sum(data.EczaneNobetTarihAralik
                //                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                //                        .Select(m => _x[m])) >= 1 + (1 - data.EczaneKumulatifHedefler
                //                                                             .Where(f => f.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                //                                                             .Select(g => _y[g]).SingleOrDefault()),
                //                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");

                //_modelTekGrupAlt.AddConstraint(
                //      Expression.Sum(data.EczaneNobetTarihAralik
                //                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                //                        .Select(m => _x[m])) >= (data.EczaneKumulatifHedefler
                //                                                         .Where(f => f.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                //                                                         .Select(g => _y[g]).SingleOrDefault()),
                //                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}"); 
                #endregion

                #endregion

                #region Bayram Toplamları

                if (bayramGunDegerleri.Count() > 0)
                {
                    var temp = hedef.ToplamBayram - 1;
                    if (temp < 0) temp = 0;
                    model.AddConstraint(
                     Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                && e.GunDegerId > 7)
                                        .Select(m => _x[m])) <= hedef.ToplamBayram,
                                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                && e.GunDegerId > 7)
                                        .Select(m => _x[m])) >= temp,
                                        $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");
                }
                #endregion

                #region Diğer günler
                var maxArz = 1.1;
                var minArz = 0.1;

                //gunDegerler: nöbet yazılacak tarih aralığındaki hafta ve bayram günleri
                foreach (var gunDeger in gunDegerler)
                {
                    //GetEczaneGunHedef2(out maxArz, out minArz, gunDeger, f.EczaneId);

                    GetEczaneGunHedef(hedef, out maxArz, out minArz, gunDeger);

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                       && e.GunDegerId == gunDeger)
                                           .Select(m => _x[m])) <= maxArz,
                                           $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                       && e.GunDegerId == gunDeger)
                                           .Select(m => _x[m])) >= minArz,
                                           $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");
                }
                #endregion
            }

            #endregion

            #region Mazerete Görev Yazılmasın

            foreach (var f in data.EczaneNobetMazeretListe)
            {
                model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneId == f.EczaneId
                                                    && e.NobetGrupId == f.NobetGrupId
                                                    && e.TakvimId == f.TakvimId
                                                 )
                                           .Select(m => _x[m])) == 1,
                                           $"isteğe nöbet yaz, {f}");
            }

            #endregion

            #region Bayram günlerinde en fazla 1 görev yazılsın.
            //eğer bayram günleri ardışık günlerden fazlaysa
            if (data.TarihAraligi.Where(w => w.GunDegerId > 7).Count() > pespese)
            {
                foreach (var f in data.EczaneKumulatifHedefler)
                {
                    foreach (var g in data.TarihAraligi.Where(w => w.GunDegerId > 7))
                    {
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                       && e.GunId == g.GunId
                                                 )
                                           .Select(m => _x[m])) <= 1,
                                           $"bayram nöbeti sınırlama, {f}");
                    }
                }
            }

            #endregion

            #endregion

            #endregion
            return model;
        }
        #endregion

        #region Solve: Tek grup alternatifli
        public EczaneNobetSonucModel SolveAlt(EczaneNobetTekGrupDataModel data)
        {
            //var config = new Configuration
            //{
            //    NameHandling = NameHandlingStyle.UniqueLongNames,
            //    ComputeRemovedVariables = true
            //};

            //using (var scope = new ModelScope(config))
            //{
            var model = ModelAlt(data);

            // Get a solver instance, change your solver
            var solver = new CplexSolver();

            // solve the model
            var solution = solver.Solve(model);

            try
            {
                model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));
            }
            catch (Exception)
            {
                ;
            }
            // import the results back into the model 


            // print objective and variable decisions
            var objective = solution.ObjectiveValues.Single();

            Results = new EczaneNobetSonucModel
            {
                ObjectiveValue = objective.Value,
                ResultModel = new List<EczaneNobetSonucAktif>()
            };

            foreach (var r in data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1))
            {
                Results.ResultModel.Add(new EczaneNobetSonucAktif()
                {
                    TakvimId = r.TakvimId,
                    EczaneNobetGrupId = r.EczaneNobetGrupId
                });
            }
            //}

            return Results;
        }
        #endregion

        #endregion
        



        #region 5. Model: Eş durumu gibi nedenlerle birbirine bağlı grupların birlikte çözümü için alternatif nöbetçi eczane optimizasyon modelli

        #region Model: Eş Durumu Alternatifli

        private Model ModelAlt(EczaneNobetCokGrupDataModel data)
        {
            var model = new Model() { Name = "Eczane Nöbet Çoklu Model Alternatif" };
            //_dataModelCokGrup = dataModel;

            #region Veriler
            //var eczaneKumulatifHedefler = data.EczaneKumulatifHedefler.ToList();
            var eczaneGrupTanimlar = data.EczaneGrupTanimlar.Where(f => f.BitisTarihi == null).ToList();
            //var data.EczaneGruplar = data.EczaneGruplar.ToList();
            //var eczaneGrupIdList = data.EczaneGruplar.Select(s => new { s.EczaneId, s.EczaneGrupTanimId }).Distinct().ToList();
            // var nobetGruplar = data.NobetGruplar.ToList();
            //var eczaneNobetTarihAralik = data.EczaneNobetTarihAralik.ToList();
            //var tarihAraligi = data.TarihAraligi.ToList();
            var gunDegerler = data.TarihAraligi.Select(s => s.GunDegerId).Distinct().ToList();
            var diniBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 8).Select(s => s.GunDegerId).Distinct().ToList();
            var milliBayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId == 9).Select(s => s.GunDegerId).Distinct().ToList();
            var bayramGunDegerleri = data.TarihAraligi.Where(w => w.GunDegerId > 7).Select(s => s.GunDegerId).Distinct().ToList();
            // var eczaneNobetMazeretler = data.EczaneNobetMazeretListe.ToList();
            //var mazeretliEczaneler = data.EczaneNobetMazeretListe
            //                                 .Select(s => new { s.EczaneId, s.NobetGrupId })
            //                                 .Distinct().ToList();
            #endregion

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x", null,
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            #endregion

            #region Amaç Fonksiyonu
            // Add the objective

            var amac = new Objective(Expression.Sum((data.EczaneNobetTarihAralik
                                           .Select(i => _x[i]))),
                                             "Sum of all item-values: ",
                                             ObjectiveSense.Minimize);
            model.AddObjective(amac);

            #endregion

            #region Kısıtlar

            #region Talep Kısıtları
            //nöbet grubu bazında olacak
            foreach (var nobetGrup in data.NobetGruplar)
            {
                foreach (var d in data.TarihAraligi)
                {
                    //nöbet gruplarının günlük nöbetçi sayısı
                    int talep = GetGunIdTalep(nobetGrup, d);

                    model.AddConstraint(
                               Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(k => k.TakvimId == d.TakvimId
                                                         && k.NobetGrupId == nobetGrup.Id
                                                )
                                                .Select(m => _x[m])) == talep,
                                                $"her güne bir eczane atanmalı, {1}");
                }
            }

            #endregion

            #region Arz Kısıtları

            #region Peşpeşe Görev Yazılmasın

            foreach (var nobetGrup in data.NobetGruplar)
            {
                int pespese = 0;
                var ardisik = 5; // nobetGrup.ArdisikNobetSayisi;

                if (ardisik - 2 >= 3)
                {
                    pespese = ardisik - 2;
                }

                foreach (var f in data.EczaneKumulatifHedefler.Where(w => w.NobetGrupId == nobetGrup.Id))
                {
                    foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespese))
                    {
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                       && (e.GunId >= g.GunId && e.GunId <= g.GunId + pespese)
                                                       && e.NobetGrupId == nobetGrup.Id
                                                       )
                                           .Select(m => _x[m])) <= 1,
                                           $"eczanelere peşpeşe nöbet yazılmasın, {f}");
                    }
                }
            }

            #endregion

            #region Her eczaneye yazılması gereken nöbetler Nöbet arzlarını(kapasitelerini-hedeflerini) ayarla

            foreach (var nobetGrup in data.NobetGruplar)
            {
                foreach (var hedef in data.EczaneKumulatifHedefler.Where(w => w.NobetGrupId == nobetGrup.Id))
                {
                    #region Toplam Hedefler

                    var maxToplam = hedef.Toplam <= 1 ? 2 : 1;

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    && e.NobetGrupId == nobetGrup.Id)
                                           .Select(m => _x[m])) <= maxToplam, //hedef.Toplam,
                                           $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");

                    var minToplam = hedef.Toplam - 1 > 1 ? 1 : 0;
                    //var minToplam = 0;

                    model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    && e.NobetGrupId == nobetGrup.Id)
                                            .Select(m => _x[m])) >= minToplam,
                                            $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");
                    #endregion

                    #region Bayram Toplam Hedefleri

                    if (bayramGunDegerleri.Count() > 0)
                    {
                        var temp = hedef.ToplamBayram - 1;
                        //temp < 0
                        if (temp < 0) temp = 0;
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    && e.GunDegerId > 7)
                                            .Select(m => _x[m])) <= hedef.ToplamBayram,
                                            $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    && e.GunDegerId > 7)
                                            .Select(m => _x[m])) >= temp,
                                            $"her eczaneye bir ayda nöbet grubunun hedefi kadar toplam bayram nöbeti yazılmalı, {hedef}");
                    }
                    #endregion

                    #region Diğer Günlerin Hedefleri
                    var maxArz = 1.1;
                    var minArz = 0.1;

                    var gunler = new List<int> { 1, 7, 8, 9 };
                    //var gunler2 = new List<int> { 2, 3, 4, 5, 6 };
                    //gunler2.ForEach(a => gunler.Add(a));

                    //gunDegerler: nöbet yazılacak tarih aralığındaki hafta ve bayram günleri
                    foreach (var gunDeger in gunDegerler.Where(x => gunler.Contains(x)))
                    {
                        GetEczaneGunHedef(hedef, out maxArz, out minArz, gunDeger);

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                           && e.GunDegerId == gunDeger
                                                           && e.NobetGrupId == nobetGrup.Id)
                                               .Select(m => _x[m])) <= maxArz,
                                               $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                           && e.GunDegerId == gunDeger
                                                           && e.NobetGrupId == nobetGrup.Id)
                                               .Select(m => _x[m])) >= minArz,
                                               $"her eczaneye bir ayda nöbet grubunun {gunDeger} hedefi kadar nöbet yazılmalı, {hedef}");
                    }
                    #endregion
                }
            }
            #endregion

            #region Eş durumu (karı-koca) kısıtı
            //gelen nöbet gruplarının listesi alınacak
            //Birbirine yakın farklı gruplardaki eczanelere sürekli aynı gün nöbet yazılmasın
            //İki farklı eczanenin (eş durumu) birlikte nöbet tutma durumu
            //ilk çalışan gruptaki kişinin görevi diğer ilişkili gruptaki kişi için mazeret olarak eklenecek
            //bu hususu gerekirse daha sonra modeli bütün olarak ele alıp sonuç verilecek

            foreach (var g in eczaneGrupTanimlar)
            {
                int pespese = g.ArdisikNobetSayisi;

                var eczaneler = data.EczaneGruplar
                                        .Where(x => x.EczaneGrupTanimId == g.Id)
                                        .Select(s => s.EczaneId).Distinct().ToList();

                foreach (var tarih in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespese))
                {
                    model.AddConstraint(
                             Expression.Sum(data.EczaneNobetTarihAralik
                                              .Where(e => eczaneler.Contains(e.EczaneId)
                                                       && (e.GunId >= tarih.GunId && e.GunId <= tarih.GunId + pespese)
                                                    )
                                              .Select(m => _x[m])) <= 1,
                                              $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {tarih.GunId}");
                }
            }

            #endregion

            #region Mazerete Görev Yazılmasın

            foreach (var f in data.EczaneNobetMazeretListe)
            {
                model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneId == f.EczaneId
                                                    && e.NobetGrupId == f.NobetGrupId
                                                    && e.TakvimId == f.TakvimId
                                                 )
                                           .Select(m => _x[m])) == 1,
                                           $"isteğe nöbet yaz, {f}");
            }

            #endregion

            #region Bayram günlerinde en fazla 1 görev yazılsın.

            //eğer bayram günleri ardışık günlerden fazlaysa
            foreach (var nobetGrup in data.NobetGruplar)
            {
                if (data.TarihAraligi.Where(w => w.GunDegerId > 7).Count() > 2)// nobetGrup.ArdisikNobetSayisi)
                {
                    foreach (var f in data.EczaneKumulatifHedefler.Where(x => x.NobetGrupId == nobetGrup.Id))
                    {
                        foreach (var g in data.TarihAraligi.Where(w => w.GunDegerId > 7))
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                           && e.GunId == g.GunId
                                                     )
                                               .Select(m => _x[m])) <= 1,
                                               $"bayram nöbeti sınırlama, {f}");
                        }
                    }
                }
            }
            #endregion

            #endregion

            #endregion
            return model;
        }
        #endregion

        #region Solve: Çok grup Alternatifli 
        public EczaneNobetSonucModel SolveCokAlt(EczaneNobetCokGrupDataModel data)
        {
            //var config = new Configuration
            //{
            //    NameHandling = NameHandlingStyle.UniqueLongNames,
            //    ComputeRemovedVariables = true
            //};

            //using (var scope = new ModelScope(config))
            //{ 
            var model = ModelAlt(data);

            // Get a solver instance, change your solver
            var solver = new CplexSolver();

            try
            {
                // solve the model
                var solution = solver.Solve(model);
                //var cplexConfiguration = solver.Configuration;

                var modelStatus = solution.ModelStatus;
                var solutionStatus = solution.Status;
                var modelName = solution.ModelName;
                var bestBound = solution.BestBound;

                //var confilicts = new ConflictingSet();
                //confilicts = solution.ConflictingSet;
                //ConstraintsUB = new IEnumerable<Constraint>();
                //ConstraintsUB = confilicts.ConstraintsUB;

                //var kisitAdlari = new List<string>();

                //foreach (var item in ConstraintsUB)
                //{
                //    kisitAdlari.Add(item.Name);
                //}

                if (modelStatus != ModelStatus.Feasible)
                {
                    throw new Exception("Uygun çözüm bulunamadı!");
                }
                else
                {
                    // import the results back into the model 
                    model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));

                    //if (!alternatifSolveCalistiMi)
                    //{
                    // print objective and variable decisions
                    var objective = solution.ObjectiveValues.Single();
                    var sure = solution.OverallWallTime;

                    Results = new EczaneNobetSonucModel
                    {
                        CozumSuresi = sure,
                        ObjectiveValue = objective.Value,
                        ResultModel = new List<EczaneNobetSonucAktif>()
                    };

                    foreach (var r in data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1))
                    {
                        Results.ResultModel.Add(new EczaneNobetSonucAktif()
                        {
                            TakvimId = r.TakvimId,
                            EczaneNobetGrupId = r.EczaneNobetGrupId
                        });
                    }
                    //}
                }

            }
            catch (Exception ex)
            {
                var mesaj = ex.Message;

                throw new Exception(mesaj);
                //SolveCokAlt();
                //alternatifSolveCalistiMi = true;
            }

            #region eski
            /*
    // solve the model
    var solution = solver.Solve(_modelCokGrupAlt);

    try
    {
        // import the results back into the model 
        _modelCokGrupAlt.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));
    }
    catch (Exception)
    {
        ;
    }

    // print objective and variable decisions
    var objective = solution.ObjectiveValues.Single();

    Results = new EczaneNobetSonucModel
    {
        ObjectiveValue = objective.Value,
        ResultModel = new List<EczaneNobetSonucAktif>()
    };

    foreach (var r in _dataModelCokGrup.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1))
    {
        Results.ResultModel.Add(new EczaneNobetSonucAktif()
        {
            TakvimId = r.TakvimId,
            EczaneNobetGrupId = r.EczaneNobetGrupId
        });
    }
    //}
    
            #endregion
            return Results;
        }
        #endregion

        #endregion
    */

        private void GetEczaneGunHedef(EczaneNobetIstatistik hedef, out double maxArz, out double minArz, int gunDeger)
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

        #region Günlük Talep Değeri Eşleştir

        private int GetGunIdTalep(NobetGrup n, TarihAraligi d)
        {
            int talep = 0;

            switch (d.GunId)
            {
                case 1:
                    // talep = n.PazarNobetciSayisi;
                    break;
                case 2:
                    //  talep = n.PazartesiNobetciSayisi;
                    break;
                case 3:
                    //  talep = n.SaliNobetciSayisi;
                    break;
                case 4:
                    // talep = n.CarsambaNobetciSayisi;
                    break;
                case 5:
                    //  talep = n.PersembeNobetciSayisi;
                    break;
                case 6:
                    //  talep = n.CumaNobetciSayisi;
                    break;
                case 7:
                    // talep = n.CumartesiNobetciSayisi;
                    break;
                case 8:
                    //  talep = n.DiniBayramNobetciSayisi;
                    break;
                case 9:
                    //  talep = n.MilliBayramNobetciSayisi;
                    break;
                default:
                    talep = 1;
                    break;
            }

            return talep;
        }
        #endregion
    }

}
