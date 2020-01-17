using OPTANO.Modeling.Common;
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Configuration;
using OPTANO.Modeling.Optimization.Enums;
using OPTANO.Modeling.Optimization.Solver;
using OPTANO.Modeling.Optimization.Solver.Cplex128;
using System;
using System.Collections.Generic;
using System.Linq;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Optimization.Concrete.Optano.Samples
{
    public class EczaneNobetTekGrupAltOptano : IEczaneNobetTekGrupAltOptimization
    {
        #region Değişkenler

        //karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }
        public EczaneNobetSonucModel Results { get; set; }
        #endregion

        private Model Model(EczaneNobetTekGrupDataModel data)
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
            var gunDegerler = data.TarihAraligi.Select(s => s.NobetGunKuralId).Distinct().ToList();
            var diniBayramGunDegerleri = data.TarihAraligi.Where(w => w.NobetGunKuralId == 8).Select(s => s.NobetGunKuralId).Distinct().ToList();
            var milliBayramGunDegerleri = data.TarihAraligi.Where(w => w.NobetGunKuralId == 9).Select(s => s.NobetGunKuralId).Distinct().ToList();
            var bayramGunDegerleri = data.TarihAraligi.Where(w => w.NobetGunKuralId > 7).Select(s => s.NobetGunKuralId).Distinct().ToList();
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

            foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler)
            {
                foreach (var d in data.TarihAraligi)
                {
                    //nöbet gruplarının günlük nöbetçi sayısı
                    int talep = data.GerekliNobetSayisi;

                    var talepFarkli = data.NobetGrupTalepler
                                        .Where(s => s.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                                 && s.TakvimId == d.TakvimId)
                                        .Select(s => s.NobetciSayisi).SingleOrDefault();

                    if (talepFarkli > 0)
                        talep = talepFarkli;

                    model.AddConstraint(
                               Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(k => k.TakvimId == d.TakvimId)
                                                .Select(m => _x[m])) == talep,
                                                $"her güne bir eczane atanmalı, {1}");
                }
            }

            #endregion

            #region Arz Kısıtları

            #region Peşpeşe Görev Yazılmasın
            foreach (var f in data.EczaneKumulatifHedefler)
            {
                foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - data.PespeseNobet))
                {
                    //model.AddConstraint(
                    //  Expression.Sum(data.EczaneNobetTarihAralik
                    //                   .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                    //                               && e.NobetGorevTipId == f.NobetGorevTipId
                    //                               && (e.Gun >= g.Gun && e.Gun <= g.Gun + data.PespeseNobet)
                    //                         )
                    //                   .Select(m => _x[m])) <= 1,
                    //                   $"eczanelere peşpeşe nöbet yazılmasın, {f}");
                }
            }
            #endregion

            #region Her eczaneye yazılması gereken nöbetler Nöbet arzlarını(kapasitelerini-hedeflerini) ayarla

            foreach (var hedef in data.EczaneKumulatifHedefler)
            {
                #region Toplam

                var gruptakiEczaneler = data.EczaneKumulatifHedefler.Select(s => s.EczaneId).Distinct().ToList();

                int maxToplam;
                int minToplam;

                maxToplam = hedef.Toplam <= 1 ? hedef.Toplam + 1 : hedef.Toplam + 2;
                minToplam = hedef.Toplam - 1 > 1 ? 1 : 0;

                //if (gruptakiEczaneler.Count == 55)
                //{
                //    maxToplam += 3;
                //    minToplam = hedef.Toplam - 2;
                //}

                model.AddConstraint(
                      Expression.Sum(data.EczaneNobetTarihAralik
                                       .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId)
                                       .Select(m => _x[m])) <= maxToplam,
                                       $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {hedef}");

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
                foreach (var gunDeger in data.NobetGrupGunKurallar.Where(s => s.NobetGrupId == data.NobetGrup.Id).Select(s => s.NobetGunKuralId))
                {   
                    GetEczaneGunHedef(hedef, out maxArz, out minArz, gunDeger);

                    if (gruptakiEczaneler.Count == 55)
                    {
                        maxArz += 1;
                        //minToplam = hedef.Toplam - 2;
                    }

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

            #region İstek Karşılansın
            //foreach (var f in data.EczaneNobetIstekler)
            //{
            //    model.AddConstraint(
            //              Expression.Sum(data.EczaneNobetTarihAralik
            //                               .Where(e => e.EczaneId == f.EczaneId
            //                                        && e.NobetGrupId == f.NobetGrupId
            //                                        && e.TakvimId == f.TakvimId
            //                                     )
            //                               .Select(m => _x[m])) == 1,
            //                               $"istege nobet yaz, {f}");
            //}
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
                                           .Select(m => _x[m])) == 0,
                                           $"mazerete nobet yazma, {f}");
            }
            #endregion

            #region Bayram günlerinde en fazla 1 görev yazılsın.
            //eğer bayram günleri ardışık günlerden fazlaysa
            if (data.TarihAraligi.Where(w => w.NobetGunKuralId > 7).Count() > pespese)
            {
                foreach (var f in data.EczaneKumulatifHedefler)
                {
                    //foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId > 7))
                    //{
                    //    model.AddConstraint(
                    //      Expression.Sum(data.EczaneNobetTarihAralik
                    //                       .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                    //                                   && e.Gun == g.Gun
                    //                             )
                    //                       .Select(m => _x[m])) <= 1,
                    //                       $"bayram nöbeti sınırlama, {f}");
                    //}
                }
            }

            #endregion

            #endregion

            #endregion
            return model;
        }

        public EczaneNobetSonucModel Solve(EczaneNobetTekGrupDataModel data)
        {
            var model = Model(data);
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

                if (modelStatus != ModelStatus.Feasible)
                {
                    throw new Exception("Uygun çözüm bulunamadı!");
                }
                else
                {
                    // import the results back into the model 
                    model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));
                    var objective = solution.ObjectiveValues.Single();
                    var sure = solution.OverallWallTime;

                    Results = new EczaneNobetSonucModel
                    {
                        CozumSuresi = sure,
                        ObjectiveValue = objective.Value,
                        ResultModel = new List<EczaneNobetCozum>()
                    };

                    foreach (var r in data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1))
                    {
                        Results.ResultModel.Add(new EczaneNobetCozum()
                        {
                            TakvimId = r.TakvimId,
                            EczaneNobetGrupId = r.EczaneNobetGrupId,
                            NobetGorevTipId = r.NobetGorevTipId
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                var mesaj = ex.Message;
                throw new Exception(mesaj);
            }
            return Results;
        }

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
    }

}
