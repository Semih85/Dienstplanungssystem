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
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;
using WM.Optimization.Entities.KisitParametre;

namespace WM.Optimization.Concrete.Optano.Health.EczaneNobet
{
    public class IskenderunOptano : EczaneNobetKisit, IEczaneNobetIskenderunOptimization
    {
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }
        private VariableCollection<EczaneNobetAltGrupTarihAralik> _y { get; set; }

        private Model Model(IskenderunDataModel data)
        {
            var model = new Model() { Name = "Iskenderun Eczane Nöbet" };

            #region kısıtlar

            var cumartesiPespeseFarkliGorevTipi = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "cumartesiPespeseFarkliGorevTipi", data.NobetUstGrupId);

            var eczaneGrup = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "eczaneGrup", data.NobetUstGrupId);
            var ikiliEczaneAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "ikiliEczaneAyniGunNobet", data.NobetUstGrupId);

            var pespeseHaftaIciAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "pespeseHaftaIciAyniGunNobet", data.NobetUstGrupId);
            var nobetBorcOdeme = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "nobetBorcOdeme", data.NobetUstGrupId);

            var ayIcindeAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "ayIcindeAyniGunNobet", data.NobetUstGrupId);
            var oncekiAylarAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "oncekiAylarAyniGunNobet", data.NobetUstGrupId);
            var eczanelerinNobetGunleriniKisitla = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "eczanelerinNobetGunleriniKisitla", data.NobetUstGrupId);
            var birEczaneyeAyniGunSadece1GorevTipYaz = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "birEczaneyeAyniGunSadece1GorevTipYaz", data.NobetUstGrupId);
            var cumartesiGorevTiplerineGoreNobetleriDagit = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "cumartesiGorevTiplerineGoreNobetleriDagit", data.NobetUstGrupId);

            #endregion

            #region Veriler                        

            var eczaneNobetTutamazGunler = new List<EczaneNobetTutamazGun>
            {                
                 //körfez (cts ve pazar tutabilir)
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1436, NobetGunKuralId = 2 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1436, NobetGunKuralId = 3 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1436, NobetGunKuralId = 4 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1436, NobetGunKuralId = 5 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1436, NobetGunKuralId = 6 }
            };

            #region tur çevrim katsayıları

            //int bayramCevrim = 8000;
            ////int arifeCevrim = 5000;
            //int cumartesiCevrim = 900;
            //int pazarCevrim = 1000;
            //int haftaIciCevrim = 500;
            #endregion

            #region özel tur takibi yapılacak günler

            var ozelTurTakibiYapilacakGunler =
                new List<int> {
                    1, // pazar
                    7  // cumartesi
                };

            #endregion

            var herAyEnFazlaIlgiliKisit = new NobetUstGrupKisitDetay();
            var kumulatifEnFazlaIlgiliGunkural = new NobetUstGrupKisitDetay();

            #endregion

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x",
                    t => $"{t.EczaneNobetGrupId}_{t.TakvimId}, {t.NobetGrupAdi}, {t.EczaneAdi}, {t.Tarih.ToShortDateString()}",
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            _y = new VariableCollection<EczaneNobetAltGrupTarihAralik>(
                    model,
                    data.EczaneNobetAltGrupTarihAralik,
                    "y",
                    t => $"{t.EczaneNobetGrupId}_{t.TakvimId}, {t.NobetGrupAdi}, {t.NobetAltGrupAdi}, {t.EczaneAdi}, {t.Tarih.ToShortDateString()}",
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            #endregion

            #region Amaç Fonksiyonu
            var amac = new Objective(Expression
                .Sum(from i in data.EczaneNobetTarihAralik
                     select (_x[i] * i.AmacFonksiyonKatsayi)),
                     "Sum of all item-values: ",
                     ObjectiveSense.Minimize);

            //var amac = new Objective(Expression.Sum(
            //    (from i in data.EczaneNobetTarihAralik
            //     from p in data.EczaneBazliGunKuralIstatistikYatay//EczaneNobetGrupGunKuralIstatistikYatay
            //     where i.EczaneId == p.EczaneId
            //     //&& i.NobetGorevTipId == p.NobetGorevTipId
            //     select (_x[i]
            //            //ilk yazılan nöbet öncelikli olsun:
            //            + _x[i] * Convert.ToInt32(i.BayramMi)
            //                    * (bayramCevrim + bayramCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiBayram).TotalDays))

            //            //tümü
            //            + _x[i] * Convert.ToInt32(i.CumartesiGunuMu)
            //                    * (cumartesiCevrim + cumartesiCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiCumartesi).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

            //            //+ (i.NobetGorevTipId == 1
            //            //    ? _x[i] * Convert.ToInt32(i.CumartesiGunuMu)
            //            //        * (cumartesiCevrim + cumartesiCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiCumartesi).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))
            //            //    : _x[i]
            //            //   )

            //            //+ (i.NobetGorevTipId == 2
            //            // ? _x[i] * Convert.ToInt32(i.CumartesiGunuMu)
            //            //     * (cumartesiCevrim2 + cumartesiCevrim2 / Math.Sqrt((i.Tarih - p.SonNobetTarihiCumartesi).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))
            //            // : _x[i]
            //            //)

            //            + _x[i] * Convert.ToInt32(i.PazarGunuMu)
            //                    * (pazarCevrim + pazarCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiPazar).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

            //            + _x[i] * Convert.ToInt32(i.HaftaIciMi) //* Math.Pow(p.NobetGorevTipId, 10)
            //                                                    //* (haftaIciCevrim + haftaIciCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
            //                    * (haftaIciCevrim + haftaIciCevrim / Math.Sqrt(
            //                        //(  i.EczaneAdi == "borçlu eczane" //BADE
            //                        //? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + 15 
            //                        //: (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays)
            //                        nobetBorcOdeme.PasifMi == false
            //                        ? (p.BorcluNobetSayisiHaftaIci >= 0 //-5
            //                            ?
            //     #region Manuel borç düzeltme
            //                                  //(i.EczaneAdi == "SERPİL"
            //                                  //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + 7
            //                                  // : i.EczaneAdi == "ELİFSU"
            //                                  //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                                  // : i.EczaneAdi == "KÖYÜM"
            //                                  //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + 8
            //                                  // : i.EczaneAdi == "DOLUNAY"
            //                                  //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                                  // : i.EczaneAdi == "SUN"
            //                                  //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                                  // : i.EczaneAdi == "TATLICAN"
            //                                  //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                                  // : i.EczaneAdi == "TEZCAN"
            //                                  //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                                  // : i.EczaneAdi == "YEŞİLIRMAK"
            //                                  //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                                  //    : (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                                  //  ) 
            //     #endregion
            //                                  (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                            : ((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci < 1
            //                                ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays
            //                                : (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                              //(i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays
            //                              )
            //                          ) * i.Tarih.Day
            //                        : (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
            //                    * (pespeseHaftaIciAyniGunNobet.PasifMi == false
            //                    ? (i.Tarih.DayOfWeek == p.SonNobetTarihiHaftaIci.DayOfWeek ? 1 : 0.2)
            //                    : 1)//aynı gün peşpeşe gelmesin
            //                    )
            //            ))),
            //            "Sum of all item-values: ",
            //            ObjectiveSense.Minimize);

            model.AddObjective(amac);
            #endregion

            #region Kısıtlar

            #region Kış nöbeti

            foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler)
            {
                #region kısıtlar grup bazlı

                var kisitlarGrupBazli = data.NobetGrupGorevTipKisitlar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var kisitlarAktif = GetKisitlarNobetGrupBazli(data.Kisitlar, kisitlarGrupBazli);

                #endregion

                #region ön hazırlama

                #region tarihler

                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var pespeseNobetSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 1);
                var gunlukNobetciSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 3);
                var pespeseNobetSayisiHaftaIci = (int)GetNobetGunKural(nobetGrupKurallar, 5);
                var pespeseNobetSayisiPazar = (int)GetNobetGunKural(nobetGrupKurallar, 6);
                var pespeseNobetSayisiCumartesi = (int)GetNobetGunKural(nobetGrupKurallar, 7);

                //var nobetGrupTalepler = data.NobetGrupTalepler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var tarihler = data.TarihAraligi
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var nobetGrupTalepler = tarihler
                    .GroupBy(g => g.TalepEdilenNobetciSayisi)
                    .Select(s => new
                    {
                        TalepEdilenNobetciSayisi = s.Key,
                        GunSayisi = s.Count()
                    }).ToList();
                //TalepleriTakvimeIsle(nobetGrupTalepler, gunlukNobetciSayisi, tarihler);

                var pazarGunleri = tarihler.Where(w => w.GunGrupId == 1).OrderBy(o => o.Tarih).ToList();
                var bayramlar = tarihler.Where(w => w.GunGrupId == 2).OrderBy(o => o.Tarih).ToList();
                var haftaIciGunleri = tarihler.Where(w => w.GunGrupId == 3).OrderBy(o => o.Tarih).ToList();
                var cumartesiGunleri = tarihler.Where(w => w.GunGrupId == 4).OrderBy(o => o.Tarih).ToList();
                var cumaGunleri = tarihler.Where(w => w.NobetGunKuralId == 6).OrderBy(o => o.Tarih).ToList();
                var cumaVeCumartesiGunleri = tarihler.Where(w => w.NobetGunKuralId >= 6 && w.NobetGunKuralId <= 7).OrderBy(o => o.Tarih).ToList();
                var cumartesiVePazarGunleri = tarihler.Where(w => w.NobetGunKuralId == 1 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();

                var gunGruplar = tarihler.Select(s => new { s.GunGrupId, s.GunGrupAdi }).Distinct().OrderBy(o => o.GunGrupId).ToList();

                var gunSayisi = tarihler.Count();
                var haftaIciSayisi = haftaIciGunleri.Count();
                var pazarSayisi = pazarGunleri.Count();
                var cumartesiSayisi = cumartesiGunleri.Count();
                var bayramSayisi = bayramlar.Count();
                #endregion

                var nobetGunKurallar = tarihler.Select(s => s.NobetGunKuralId).Distinct().ToList();

                var r = new Random();

                var eczaneNobetGruplar = data.EczaneNobetGruplar
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId)
                    .OrderBy(x => r.NextDouble())
                    .ToList();

                var gruptakiEczaneSayisi = eczaneNobetGruplar.Count;

                var eczaneNobetSonuclarGorevTipBazli = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var eczaneNobetGrupGunKuralIstatistikler = data.EczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var nobetGrupGunKurallarAktifGunler = data.NobetGrupGorevTipGunKurallar
                                                        .Where(s => s.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                                                 && nobetGunKurallar.Contains(s.NobetGunKuralId)
                                                                 )
                                                        .Select(s => s.NobetGunKuralId).ToList();

                //var nobetGrupGorevTipler = data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                // karar değişkeni - nöbet grup bazlı filtrelenmiş
                var eczaneNobetTarihAralikGrupBazli = data.EczaneNobetTarihAralik
                           .Where(e => e.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                                    && e.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                #region haftaIciPespeseGorevEnAz

                var farkliAyPespeseGorevAraligi = (gruptakiEczaneSayisi / gunlukNobetciSayisi * 1.2);

                //var altLimit = farkliAyPespeseGorevAraligi / gunlukNobetciSayisi * 0.7666; //0.95
                var altLimit = (gruptakiEczaneSayisi / gunlukNobetciSayisi) * 0.6; //0.95

                //hafta içi                
                if (pespeseNobetSayisiHaftaIci > 0)
                {
                    altLimit = pespeseNobetSayisiHaftaIci;
                }

                var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                var ustLimitKontrol = ustLimit * 0.95; //0.81 

                var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / (5 * gunlukNobetciSayisi)) - 1;
                var cumartersiNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / (5 * gunlukNobetciSayisi)) - 1;
                #endregion

                #region ortalama nöbet sayıları

                var talepEdilenNobetciSayisi = tarihler.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiHaftaIci = haftaIciGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiCuma = cumaGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiCumartesi = cumartesiGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiPazar = pazarGunleri.Sum(s => s.TalepEdilenNobetciSayisi);

                var cumartesiTalepEdilenNobetciSayisiTumGorevTipler = data.TarihAraligi.Where(w => w.GunGrupId == 4).Sum(s => s.TalepEdilenNobetciSayisi);

                var ortamalaNobetSayisiHaftaIci = OrtalamaNobetSayisi(haftaIciGunleri.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var ortalamaNobetSayisi = OrtalamaNobetSayisi(tarihler.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var ortalamaNobetSayisiTumGorevTipleri = OrtalamaNobetSayisi(data.TarihAraligi.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var ortalamaNobetSayisiCumartesiTumGorevTipleri = OrtalamaNobetSayisi(cumartesiTalepEdilenNobetciSayisiTumGorevTipler, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiBayram = OrtalamaNobetSayisi(bayramlar.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);

                #region kümülatif ortalamalar

                #region daha önce tutulan toplam nöbet sayıları

                var kumulatifToplamNobetSayisi = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiToplam);
                var kumulatifToplamNobetSayisiHaftaIci = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiHaftaIci);
                var kumulatifToplamNobetSayisiCuma = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiCuma);
                var kumulatifToplamNobetSayisiCumartesi = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiCumartesi);
                var kumulatifToplamNobetSayisiCumaVeCumartesi = kumulatifToplamNobetSayisiCuma + kumulatifToplamNobetSayisiCumartesi;
                var kumulatifToplamNobetSayisiPazar = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiPazar);

                #endregion

                var ortalamaNobetSayisiKumulatif = OrtalamaNobetSayisi(talepEdilenNobetciSayisi + kumulatifToplamNobetSayisi, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifHaftaIci = OrtalamaNobetSayisi(talepEdilenNobetciSayisiHaftaIci + kumulatifToplamNobetSayisiHaftaIci, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifCuma = OrtalamaNobetSayisi(talepEdilenNobetciSayisiCuma + kumulatifToplamNobetSayisiCuma, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifCumartesi = OrtalamaNobetSayisi(talepEdilenNobetciSayisiCumartesi
                                                                                + kumulatifToplamNobetSayisiCumartesi, gruptakiEczaneSayisi);

                var ortalamaNobetSayisiKumulatifCumaVeCumartesi = OrtalamaNobetSayisi(talepEdilenNobetciSayisiCuma
                                                                                      + talepEdilenNobetciSayisiCumartesi
                                                                                      + kumulatifToplamNobetSayisiCumaVeCumartesi, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifPazar = OrtalamaNobetSayisi(talepEdilenNobetciSayisiPazar + kumulatifToplamNobetSayisiPazar, gruptakiEczaneSayisi);

                #endregion

                #endregion

                var nobetGrupBayramNobetleri = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                             && w.GunGrupId == 2
                             && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                    .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                var nobetGunKuralIstatistikler = data.TakvimNobetGrupGunDegerIstatistikler
                                      .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                               && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                                               && tarihler.Select(s => s.NobetGunKuralId).Distinct().Contains(w.NobetGunKuralId))
                                      .OrderBy(o => o.NobetGunKuralId).ToList();

                var nobetGunKuralTarihler = OrtalamaNobetSayilariniHesapla(tarihler, gruptakiEczaneSayisi, eczaneNobetGrupGunKuralIstatistikler, nobetGunKuralIstatistikler);

                #endregion

                #region talep

                var talebiKarsilaKisitParametreModel = new KpTalebiKarsila
                {
                    EczaneNobetTarihAralikTumu = eczaneNobetTarihAralikGrupBazli,
                    NobetGrupGorevTip = nobetGrupGorevTip,
                    Tarihler = tarihler,
                    Model = model,
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k89"),
                    KararDegiskeni = _x
                };

                TalebiKarsila(talebiKarsilaKisitParametreModel);

                #endregion

                #region eczane bazlı kısıtlar

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {
                    #region kontrol

                    var kontrol = true;

                    if (kontrol)
                    {
                        var kontrolEdilecekEczaneler = data.DebugYapilacakEczaneler.Select(s => s.EczaneNobetGrupId).ToArray();

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.Id))
                        {
                            var kontrolEdilenEczane = eczaneNobetGrup.EczaneAdi;
                        }
                    }

                    #endregion

                    #region eczane bazlı veriler

                    // karar değişkeni - eczane bazlı filtrelenmiş

                    var eczaneNobetTarihAralikEczaneBazli = eczaneNobetTarihAralikGrupBazli
                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    var eczaneNobetTarihAralikEczaneBazliTumGrorevTipler = data.EczaneNobetTarihAralik
                        .Where(e => e.EczaneId == eczaneNobetGrup.EczaneId
                                //&& e.CumartesiGunuMu == true
                                ).ToList();

                    var eczaneNobetSonuclar = eczaneNobetSonuclarGorevTipBazli
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    var eczaneNobetIstatistik = eczaneNobetGrupGunKuralIstatistikler
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    var eczaneBazliGunKuralIstatistikYatay = data.EczaneBazliGunKuralIstatistikYatay
                            .Where(w => w.EczaneId == eczaneNobetGrup.EczaneId).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    var yazilabilecekIlkPazarTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar.AddMonths(pazarNobetiYazilabilecekIlkAy - 1);
                    var yazilabilecekIlkCumartesiTarihi = eczaneNobetIstatistik.SonNobetTarihiCumartesi.AddMonths(cumartersiNobetiYazilabilecekIlkAy - 1);

                    var yazilabilecekIlkHaftaIciTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci.AddDays(altLimit);
                    var nobetYazilabilecekIlkTarih = eczaneNobetIstatistik.SonNobetTarihi.AddDays(pespeseNobetSayisi);
                    var nobetYazilabilecekIlkTarihTumGorevTipleri = eczaneBazliGunKuralIstatistikYatay.SonNobetTarihi.AddDays(pespeseNobetSayisi);

                    var nobetTutamazGunler = eczaneNobetTutamazGunler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    var nobetYazilabilirGunler = nobetGrupGunKurallarAktifGunler
                        .Where(w => !nobetTutamazGunler.Select(s => s.NobetGunKuralId).Contains(w)
                                 && !ozelTurTakibiYapilacakGunler.Contains(w)).ToList();

                    var kpKumulatifToplam = new KpKumulatifToplam
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup
                    };

                    var KpTarihAraligindaEnAz1NobetYaz = new KpTarihAraligindaEnAz1NobetYaz
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        GruptakiNobetciSayisi = gruptakiEczaneSayisi,
                        //GunlukNobetciSayisi = gunlukNobetciSayisi,
                    };

                    #endregion

                    #region aktif kısıtlar

                    #region Peş peşe nöbet

                    #region ay içinde

                    var kpHerAyPespeseGorev = new KpHerAyPespeseGorev
                    {
                        Model = model,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler,
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k1"),
                        Tarihler = tarihler,
                        PespeseNobetSayisi = pespeseNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };
                    HerAyPespeseGorev(kpHerAyPespeseGorev);

                    var kpHerAyHaftaIciPespeseGorev = new KpHerAyHaftaIciPespeseGorev
                    {
                        Model = model,
                        Tarihler = haftaIciGunleri,
                        OrtamalaNobetSayisi = ortamalaNobetSayisiHaftaIci,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        PespeseNobetSayisiAltLimit = altLimit,
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k27"),
                        KararDegiskeni = _x
                    };
                    HerAyHaftaIciPespeseGorev(kpHerAyHaftaIciPespeseGorev);

                    #endregion

                    #region farklı aylar peş peşe nöbet

                    var pesPeseGorevEnAzBase = new KpPesPeseGorevEnAz
                    {
                        Model = model,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };

                    var pesPeseGorevEnAzFarkliAylar = (KpPesPeseGorevEnAz)pesPeseGorevEnAzBase.Clone();

                    pesPeseGorevEnAzFarkliAylar.NobetSayisi = eczaneBazliGunKuralIstatistikYatay.NobetSayisiToplam;
                    pesPeseGorevEnAzFarkliAylar.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler;
                    pesPeseGorevEnAzFarkliAylar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k18");
                    pesPeseGorevEnAzFarkliAylar.Tarihler = tarihler;
                    pesPeseGorevEnAzFarkliAylar.NobetYazilabilecekIlkTarih = nobetYazilabilecekIlkTarihTumGorevTipleri;
                    pesPeseGorevEnAzFarkliAylar.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzFarkliAylar);

                    //karar değişkeni değişti
                    pesPeseGorevEnAzBase.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli;

                    #region cumartesi

                    var pesPeseGorevEnAzCumartesi = (KpPesPeseGorevEnAz)pesPeseGorevEnAzBase.Clone();

                    pesPeseGorevEnAzCumartesi.NobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;
                    pesPeseGorevEnAzCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k37");
                    pesPeseGorevEnAzCumartesi.Tarihler = cumartesiGunleri;
                    pesPeseGorevEnAzCumartesi.NobetYazilabilecekIlkTarih = yazilabilecekIlkCumartesiTarihi;
                    pesPeseGorevEnAzCumartesi.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiCumartesi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzCumartesi);

                    #endregion

                    #region pazar

                    var pesPeseGorevEnAzPazar = (KpPesPeseGorevEnAz)pesPeseGorevEnAzBase.Clone();

                    pesPeseGorevEnAzPazar.NobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;
                    pesPeseGorevEnAzPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k28");
                    pesPeseGorevEnAzPazar.Tarihler = pazarGunleri;
                    pesPeseGorevEnAzPazar.NobetYazilabilecekIlkTarih = yazilabilecekIlkPazarTarihi;
                    pesPeseGorevEnAzPazar.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar;

                    PesPeseGorevEnAz(pesPeseGorevEnAzPazar);

                    #endregion

                    #region hafta içi

                    var pesPeseGorevEnAzHaftaIci = (KpPesPeseGorevEnAz)pesPeseGorevEnAzBase.Clone();

                    pesPeseGorevEnAzHaftaIci.NobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;
                    pesPeseGorevEnAzHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k27");
                    pesPeseGorevEnAzHaftaIci.Tarihler = haftaIciGunleri;
                    pesPeseGorevEnAzHaftaIci.NobetYazilabilecekIlkTarih = yazilabilecekIlkHaftaIciTarihi;
                    pesPeseGorevEnAzHaftaIci.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci;

                    PesPeseGorevEnAz(pesPeseGorevEnAzHaftaIci);

                    #endregion

                    #endregion

                    #endregion

                    #region Tarih aralığı ortalama en fazla

                    var kpTarihAraligiOrtalamaEnFazla = new KpTarihAraligiOrtalamaEnFazla
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    var ortalamaEnFazlaTumTarihAraligi = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaTumTarihAraligi.Tarihler = tarihler;
                    ortalamaEnFazlaTumTarihAraligi.GunSayisi = gunSayisi;
                    ortalamaEnFazlaTumTarihAraligi.OrtalamaNobetSayisi = ortalamaNobetSayisi;
                    ortalamaEnFazlaTumTarihAraligi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k19");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaTumTarihAraligi);

                    var ortalamaEnFazlaHaftaIici = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaHaftaIici.Tarihler = haftaIciGunleri;
                    ortalamaEnFazlaHaftaIici.GunSayisi = haftaIciSayisi;
                    ortalamaEnFazlaHaftaIici.OrtalamaNobetSayisi = ortamalaNobetSayisiHaftaIci;
                    ortalamaEnFazlaHaftaIici.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k32");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHaftaIici);

                    var ortalamaEnFazlaBayram = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaBayram.Tarihler = bayramlar;
                    ortalamaEnFazlaBayram.GunSayisi = bayramSayisi;
                    ortalamaEnFazlaBayram.OrtalamaNobetSayisi = ortalamaNobetSayisiBayram;
                    ortalamaEnFazlaBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k14");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaBayram);

                    #endregion

                    #region Tur takip kısıtı - nöbet ortalamaları

                    #region aylık en fazla

                    var aylar = tarihler.Select(s => s.Ay).Distinct().ToList();

                    if (aylar.Count > 1)
                    {
                        foreach (var ay in aylar)
                        {
                            var ayTumGunler = tarihler.Where(w => w.Ay == ay).ToList();

                            var ortalamaNobetSayisiAylikTumu = OrtalamaNobetSayisi(ayTumGunler.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                            var ortalamaEnFazlaHerAy = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();

                            ortalamaEnFazlaHerAy.Tarihler = ayTumGunler;
                            ortalamaEnFazlaHerAy.GunSayisi = ayTumGunler.Count;
                            ortalamaEnFazlaHerAy.OrtalamaNobetSayisi = ortalamaNobetSayisiAylikTumu;
                            ortalamaEnFazlaHerAy.GunKuralAdi = $"{ay}.ay en fazla";
                            ortalamaEnFazlaHerAy.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k65");

                            TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHerAy);

                            var ayHaftaIiciGunler = haftaIciGunleri.Where(w => w.Ay == ay).ToList();
                            var ortalamaNobetSayisiAylikHaftaIci = OrtalamaNobetSayisi(ayHaftaIiciGunler.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);

                            var ortalamaEnFazlaHerAyHaftaIici = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                            ortalamaEnFazlaHerAyHaftaIici.Tarihler = haftaIciGunleri.Where(w => w.Ay == ay).ToList();
                            ortalamaEnFazlaHerAyHaftaIici.GunSayisi = ayHaftaIiciGunler.Count;
                            ortalamaEnFazlaHerAyHaftaIici.GunKuralAdi = $"{ay}.ay en fazla";
                            ortalamaEnFazlaHerAyHaftaIici.OrtalamaNobetSayisi = ortalamaNobetSayisiAylikHaftaIci;
                            ortalamaEnFazlaHerAyHaftaIici.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k66");

                            TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHerAyHaftaIici);
                        }
                    }

                    #endregion

                    #region hafta içi nöbet sayıları arasındaki farklar - max/min

                    var nobetGunKuralNobetSayilari = GetNobetGunKuralNobetSayilari(nobetGunKuralIstatistikler, eczaneNobetIstatistik);

                    //var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi) - nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                    var haftaIciEnCokNobetSayisi = 0;
                    var haftaIciEnAzNobetSayisi = 0;

                    if (nobetGunKuralIstatistikler.Count > 0)
                    {
                        var haftaiciNobetIstatistik = nobetGunKuralNobetSayilari
                            .Where(w => w.GunGrupId == 3).ToList();

                        haftaIciEnCokNobetSayisi = haftaiciNobetIstatistik.Max(m => m.NobetSayisi);
                        haftaIciEnAzNobetSayisi = haftaiciNobetIstatistik.Min(m => m.NobetSayisi);

                        //haftaIciEnCokNobetSayisi = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi);
                        //haftaIciEnAzNobetSayisi = nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                    }

                    #endregion

                    #region nöbet ortalamaları - gün kurallar

                    var kumulatifEnfazlaHaftaIciDagilimi = NobetUstGrupKisit(kisitlarAktif, "k44");

                    var kpOrtalamaEnFazlaKumulatif = new KpKumulatifToplam
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    foreach (var nobetGunKural in nobetGunKuralIstatistikler)//aktifGunKurallar)
                    {
                        //if (nobetGunKural.NobetGunKuralKapanmaTarihi != null)
                        //    continue;

                        //if (kontrol && gunKural.NobetGunKuralAdi == "Cuma")
                        //{
                        //}

                        //if (nobetGunKural.GunGrupId == 1)
                        //{//istisna
                        //    continue;
                        //}

                        #region tarih aralığı en fazla

                        var tarihAralik = nobetGunKuralTarihler
                            .Where(w => w.NobetGunKuralId == nobetGunKural.NobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();

                        herAyEnFazlaIlgiliKisit = GetNobetGunKuralIlgiliKisitTarihAraligi(kisitlarAktif, nobetGunKural.NobetGunKuralId);

                        var tarihAraligiOrtalamaEnFazlaIlgiliKisit = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();

                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.Tarihler = tarihAralik.TakvimNobetGruplar;
                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.GunSayisi = tarihAralik.GunSayisi;
                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.OrtalamaNobetSayisi = tarihAralik.OrtalamaNobetSayisi;
                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.NobetUstGrupKisit = herAyEnFazlaIlgiliKisit;
                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.GunKuralAdi = herAyEnFazlaIlgiliKisit.KisitId == 42 ? nobetGunKural.NobetGunKuralAdi : "";

                        TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaIlgiliKisit);

                        #endregion

                        #region kümülatif en fazla

                        var kumulatifOrtalamaGunKuralNobetSayisi = tarihAralik.KumulatifOrtalamaNobetSayisi;

                        var gunKuralNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, nobetGunKural.NobetGunKuralId);

                        var haftaIciEnCokVeGunKuralNobetleriArasindakiFark = haftaIciEnCokNobetSayisi - gunKuralNobetSayisi;
                        var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = haftaIciEnCokNobetSayisi - haftaIciEnAzNobetSayisi;

                        if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                        kumulatifEnFazlaIlgiliGunkural = GetNobetGunKuralIlgiliKisitKumulatif(kisitlarAktif, nobetGunKural.NobetGunKuralId);

                        var kumulatifToplamEnFazla = (KpKumulatifToplam)kpOrtalamaEnFazlaKumulatif.Clone();

                        if (KumulatifEnfazlaHafIciDagilimiArasindaFarkVarmi(
                            herAyEnFazlaIlgiliKisit,
                            kumulatifEnfazlaHaftaIciDagilimi,
                            nobetGunKural,
                            gunKuralNobetSayisi,
                            haftaIciEnCokVeGunKuralNobetleriArasindakiFark,
                            haftaIciEnAzVeEnCokNobetSayisiArasindakiFark)
                            )
                        {//hafta içi dağılım

                            kumulatifOrtalamaGunKuralNobetSayisi = 0;

                            kumulatifToplamEnFazla.GunKuralAdi = nobetGunKural.NobetGunKuralAdi;

                            kumulatifEnFazlaIlgiliGunkural = kumulatifEnfazlaHaftaIciDagilimi;

                            //kumulatifEnFazlaIlgiliGunkural.SagTarafDegeri = kumulatifOrtalamaGunKuralNobetSayisi;
                        }

                        kumulatifToplamEnFazla.Tarihler = tarihAralik.TakvimNobetGruplar;
                        kumulatifToplamEnFazla.KumulatifOrtalamaNobetSayisi = kumulatifOrtalamaGunKuralNobetSayisi;
                        kumulatifToplamEnFazla.ToplamNobetSayisi = gunKuralNobetSayisi;
                        kumulatifToplamEnFazla.NobetUstGrupKisit = kumulatifEnFazlaIlgiliGunkural;
                        //kumulatifToplamEnFazla.GunKuralAdi = nobetGunKural.NobetGunKuralAdi;

                        KumulatifToplamEnFazla(kumulatifToplamEnFazla);

                        #endregion
                    }

                    #endregion

                    #endregion

                    #region Bayram kümülatif toplam en fazla ve farklı tür bayram

                    if (bayramlar.Count() > 0)
                    {
                        var bayramNobetleriAnahtarli = eczaneNobetSonuclar
                                .Where(w => w.NobetGunKuralId > 7).ToList();

                        var toplamBayramNobetSayisi = eczaneNobetIstatistik.NobetSayisiBayram;

                        if (!NobetUstGrupKisit(kisitlarAktif, "k5").PasifMi)
                        {
                            var bayramGunKuralIstatistikler = nobetGunKuralIstatistikler
                                                            .Where(w => w.NobetGunKuralId > 7).ToList();

                            var toplamBayramGrupToplamNobetSayisi = nobetGrupBayramNobetleri.Count();

                            var kumulatifBayramSayisi = bayramGunKuralIstatistikler.Sum(x => x.GunSayisi);

                            var yillikOrtalamaGunKuralSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, kumulatifBayramSayisi);

                            if (kumulatifBayramSayisi == gruptakiEczaneSayisi)
                            {
                                //yillikOrtalamaGunKuralSayisi++;
                            }

                            if (toplamBayramGrupToplamNobetSayisi == gruptakiEczaneSayisi)
                            {
                                //yillikOrtalamaGunKuralSayisi++;
                            }

                            if (yillikOrtalamaGunKuralSayisi < 1) yillikOrtalamaGunKuralSayisi = 0;

                            if (toplamBayramNobetSayisi > yillikOrtalamaGunKuralSayisi)
                                yillikOrtalamaGunKuralSayisi = toplamBayramNobetSayisi;

                            if (NobetUstGrupKisit(kisitlarAktif, "k5").SagTarafDegeri > 0)
                                yillikOrtalamaGunKuralSayisi = NobetUstGrupKisit(kisitlarAktif, "k5").SagTarafDegeri;

                            var sonBayram = bayramNobetleriAnahtarli.LastOrDefault();

                            if (sonBayram != null)
                            {
                                if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayram.NobetGunKuralId) && !NobetUstGrupKisit(kisitlarAktif, "k36").PasifMi)
                                {
                                    var bayramPespeseFarkliTurKisit = new KpPespeseFarkliTurNobet
                                    {
                                        Model = model,
                                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k36"),
                                        Tarihler = bayramlar,
                                        EczaneNobetGrup = eczaneNobetGrup,
                                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                        SonNobet = sonBayram,
                                        KararDegiskeni = _x
                                    };
                                    PespeseFarkliTurNobetYaz(bayramPespeseFarkliTurKisit);
                                }
                                else
                                {
                                    var kumulatifToplamEnFazlaBayram = new KpKumulatifToplam
                                    {
                                        Model = model,
                                        Tarihler = bayramlar,
                                        EczaneNobetGrup = eczaneNobetGrup,
                                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k5"),
                                        KumulatifOrtalamaNobetSayisi = yillikOrtalamaGunKuralSayisi,
                                        ToplamNobetSayisi = toplamBayramNobetSayisi,
                                        GunKuralAdi = "Bayram",
                                        KararDegiskeni = _x
                                    };
                                    KumulatifToplamEnFazla(kumulatifToplamEnFazlaBayram);
                                }
                            }
                        }
                    }
                    #endregion

                    #endregion

                    #region pasif kısıtlar                                                         

                    #region kümülatif en fazla

                    kpKumulatifToplam.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler;

                    #region Toplam max

                    var kpKumulatifToplamEnFazla = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazla.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler;
                    kpKumulatifToplamEnFazla.Tarihler = tarihler;
                    kpKumulatifToplamEnFazla.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k2");
                    kpKumulatifToplamEnFazla.ToplamNobetSayisi = eczaneBazliGunKuralIstatistikYatay.NobetSayisiToplam;
                    kpKumulatifToplamEnFazla.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatif;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazla);

                    #endregion 

                    #region Hafta içi toplam max hedefler

                    var kpKumulatifToplamEnFazlaHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k16");
                    kpKumulatifToplamEnFazlaHaftaIci.ToplamNobetSayisi = eczaneBazliGunKuralIstatistikYatay.NobetSayisiHaftaIci;
                    kpKumulatifToplamEnFazlaHaftaIci.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIci);

                    #endregion

                    #region Toplam cuma ve cumartesi max hedefler

                    var kpKumulatifToplamEnFazlaCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k25");
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifCumaVeCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumaVeCumartesi);

                    #endregion

                    #region Toplam cumartesi ve pazar max hedefler

                    var kpKumulatifToplamEnFazlaCumartesiVePazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesiVePazar.Tarihler = cumartesiVePazarGunleri;
                    kpKumulatifToplamEnFazlaCumartesiVePazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k50");
                    kpKumulatifToplamEnFazlaCumartesiVePazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar + eczaneNobetIstatistik.NobetSayisiCumartesi;
                    kpKumulatifToplamEnFazlaCumartesiVePazar.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesiVePazar);

                    #endregion

                    #endregion

                    #region en az

                    #region tarih aralığı en az

                    #region tarih aralığı en az 1 görev

                    var tarihAraligindaEnAz1NobetYazKisitTarihAraligi = (KpTarihAraligindaEnAz1NobetYaz)KpTarihAraligindaEnAz1NobetYaz.Clone();

                    var istisnaEczaneler = new string[1] {
                        "KÖRFEZ"
                    };

                    //var istisnalarDahilOlmasin = true;

                    if (!istisnaEczaneler.Contains(KpTarihAraligindaEnAz1NobetYaz.EczaneNobetGrup.EczaneAdi)
                        //&& istisnalarDahilOlmasin
                        )
                    {
                        tarihAraligindaEnAz1NobetYazKisitTarihAraligi.IstisnaOlanNobetciSayisi = istisnaEczaneler.Count();
                        tarihAraligindaEnAz1NobetYazKisitTarihAraligi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k4");
                        tarihAraligindaEnAz1NobetYazKisitTarihAraligi.Tarihler = tarihler;

                        TarihAraligindaEnAz1NobetYaz(tarihAraligindaEnAz1NobetYazKisitTarihAraligi);
                    }

                    #endregion

                    #region tarih aralığı en az 1 hafta içi

                    var tarihAraligindaEnAz1NobetYazKisitHaftaIci = (KpTarihAraligindaEnAz1NobetYaz)KpTarihAraligindaEnAz1NobetYaz.Clone();

                    tarihAraligindaEnAz1NobetYazKisitHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k40");
                    tarihAraligindaEnAz1NobetYazKisitHaftaIci.Tarihler = haftaIciGunleri;

                    TarihAraligindaEnAz1NobetYaz(tarihAraligindaEnAz1NobetYazKisitHaftaIci);

                    #endregion

                    #endregion

                    #region kümülatif en az

                    kpKumulatifToplam.EnAzMi = true;

                    #region Kümülatif en az

                    if (!istisnaEczaneler.Contains(KpTarihAraligindaEnAz1NobetYaz.EczaneNobetGrup.EczaneAdi)
                    //&& istisnalarDahilOlmasin
                    )
                    {
                        var kpKumulatifToplamEnAz = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                        kpKumulatifToplamEnAz.Tarihler = tarihler;
                        kpKumulatifToplamEnAz.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k3");
                        kpKumulatifToplamEnAz.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;

                        KumulatifToplamEnFazla(kpKumulatifToplamEnAz);
                    }

                    #endregion

                    #region Kümülatif cuma ve cumartesi en az

                    var kpKumulatifToplamEnAzCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnAzCumaVeCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k26");
                    kpKumulatifToplamEnAzCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumaVeCumartesi);

                    #endregion

                    #region Kümülatif cumartesi ve pazar en az

                    var kpKumulatifToplamEnAzCumartesiVePazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumartesiVePazar.Tarihler = cumartesiVePazarGunleri;
                    kpKumulatifToplamEnAzCumartesiVePazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k62");
                    kpKumulatifToplamEnAzCumartesiVePazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi + eczaneNobetIstatistik.NobetSayisiPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumartesiVePazar);

                    #endregion

                    #region Kümülatif cumartesi en az

                    var kpKumulatifToplamEnAzCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumartesi.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnAzCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k30");
                    kpKumulatifToplamEnAzCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumartesi);

                    #endregion

                    #region Kümülatif pazar en az

                    var kpKumulatifToplamEnAzPazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzPazar.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnAzPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k56");
                    kpKumulatifToplamEnAzPazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzPazar);

                    #endregion

                    #region Kümülatif hafta içi en az
                    if (!istisnaEczaneler.Contains(KpTarihAraligindaEnAz1NobetYaz.EczaneNobetGrup.EczaneAdi)
                       //&& istisnalarDahilOlmasin
                       )
                    {
                        var kpKumulatifToplamEnAzHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                        kpKumulatifToplamEnAzHaftaIci.Tarihler = haftaIciGunleri;
                        kpKumulatifToplamEnAzHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k17");
                        kpKumulatifToplamEnAzHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;

                        KumulatifToplamEnFazla(kpKumulatifToplamEnAzHaftaIci);
                    }

                    #endregion

                    #region Kümülatif bayram en az

                    var kpKumulatifToplamEnAzBayram = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzBayram.Tarihler = bayramlar;
                    kpKumulatifToplamEnAzBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k6");
                    kpKumulatifToplamEnAzBayram.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiBayram;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzBayram);

                    #endregion

                    #endregion

                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın (Pazar peşpeşe en fazla)

                    var pazarPespeseGorevEnFazla = data.NobetUstGrupKisitlar
                                            .Where(s => s.KisitAdi == "pazarPespeseGorevEnFazla"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (pazarPespeseGorevEnFazla)
                    {
                        var enSonPazarTuttuguTarih1 = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 1)
                            .Select(s => s.TakvimId).OrderByDescending(o => o).FirstOrDefault();

                        //var enSonPazarTuttuguNobetAyi = enSonPazarTuttuguTarih1.Month;

                        //if (data.CalismaSayisi == 5) yazilabilecekSonTarih = yazilabilecekSonTarih++;

                        var kontrolListesi = new int[] { 154 };
                        if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                        {
                        }

                        if (enSonPazarTuttuguTarih1 > 0) //enSonPazarTuttuguNobetAyi
                        {
                            var enSonPazarTuttuguTarih = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 1)
                            .Select(s => new { s.Tarih, s.TakvimId }).OrderByDescending(o => o.Tarih).FirstOrDefault();

                            //var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 1;
                            var pazarNobetiYazilabilecekSonAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 4) + 1;

                            //var yazilabilecekIlkTarih = enSonPazarTuttuguTarih.Tarih.AddMonths(pazarNobetiYazilabilecekIlkAy);
                            var yazilabilecekSonTarih = enSonPazarTuttuguTarih.Tarih.AddMonths(pazarNobetiYazilabilecekSonAy);

                            var tarihlerGruplu = pazarGunleri
                                .GroupBy(g => new { g.Yil, g.Ay })
                                .Select(s => new
                                {
                                    s.Key.Yil,
                                    s.Key.Ay,
                                    IlkTarih = s.Min(m => m.Tarih),
                                    SonTarih = s.Max(m => m.Tarih)
                                }).ToList();

                            var tarihAralik = (from t1 in pazarGunleri
                                               from t2 in tarihlerGruplu
                                               where t1.Yil == t2.Yil
                                               && t1.Yil == t2.Yil
                                               //&& !(t2.IlkTarih > yazilabilecekIlkTarih && t2.SonTarih <= yazilabilecekSonTarih)
                                               && t2.SonTarih > yazilabilecekSonTarih
                                               select new { t1.TakvimId, t1.Tarih, t2.IlkTarih, t2.SonTarih }).ToList();

                            foreach (var g in tarihAralik)
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                                               )
                                                   .Select(m => _x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin, {eczaneNobetGrup.Id}");
                            }

                            //var tarihler = tarihler
                            //    .Where(w => w.NobetGunKuralId == 1)
                            //    .Select(s => s.TakvimId).ToList();

                            //var periyottakiNobetSayisi = data.EczaneNobetSonuclar
                            //           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                    && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                            //                    && (w.Tarih >= yazilabilecekIlkTarih.AddMonths(1) && w.Tarih <= yazilabilecekSonTarih)
                            //                    && w.NobetGunKuralId == 1)
                            //           .Select(s => s.TakvimId).Count();

                            //var nobetYazilanTarih = new DateTime(data.Yil, data.Ay, 1);

                            //var ilkTarihKontrol = nobetYazilanTarih.AddMonths(-pazarNobetiYazilabilecekIlkAy - 2);
                            ////periyodu başladıktan 1 ay sonra hala pazar nöbeti yazılmamışsa yazılsın.
                            //if (enSonPazarTuttuguTarih.Tarih < ilkTarihKontrol //data.Ay - yazilabilecekIlkTarih //- 1
                            //    && periyottakiNobetSayisi == 0
                            //    )
                            //{
                            //    model.AddConstraint(
                            //      Expression.Sum(data.EczaneNobetTarihAralik
                            //                       .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                                   && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                            //                                   && tarihler.Contains(e.TakvimId)
                            //                                   )
                            //                       .Select(m => _x[m])) == 1,
                            //                       $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                            //}
                        }
                    }
                    #endregion

                    #endregion
                }

                #endregion

                #region istek ve mazeret

                var istegiKarsilaKisit = new KpIstegiKarsila
                {
                    Model = model,
                    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k12"),
                    EczaneNobetIstekler = data.EczaneNobetIstekler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                IstegiKarsila(istegiKarsilaKisit);

                var mazereteGorevYazmaKisit = new KpMazereteGorevYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k13"),
                    EczaneNobetMazeretler = data.EczaneNobetMazeretler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                MazereteGorevYazma(mazereteGorevYazmaKisit);

                #endregion
            }

            #region Alt gruplarla eşit sayıda nöbet tutulsun

            //1. çarşı - ssk acil
            //2. ssk   - gelişim palmiye (uzak)
            //3. ssk   - gelişim yeni devlet

            #region eski nöbetler

            var gecmisNobetSayilari = new List<AltGrupIleTutulanNobetDurumu>
                {
                    #region çarşı - acil

                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1196,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1186,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1187,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1199,NobetAltGrupId=37,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1436,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1198,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1184,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1197,NobetAltGrupId=37,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1188,NobetAltGrupId=37,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1189,NobetAltGrupId=37,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1190,NobetAltGrupId=37,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1191,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1192,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1193,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1194,NobetAltGrupId=37,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1195,NobetAltGrupId=37,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1200,NobetAltGrupId=37,NobetSayisi=3},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1201,NobetAltGrupId=37,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1202,NobetAltGrupId=37,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1203,NobetAltGrupId=37,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1185,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1204,NobetAltGrupId=37,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1205,NobetAltGrupId=37,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1206,NobetAltGrupId=37,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1207,NobetAltGrupId=37,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1208,NobetAltGrupId=37,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1209,NobetAltGrupId=37,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 42, EczaneNobetGrupId =1210,NobetAltGrupId=37,NobetSayisi=0},

	                    #endregion

                    #region ssk - palmiye

                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1251,NobetAltGrupId=43,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1253,NobetAltGrupId=43,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1263,NobetAltGrupId=43,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1242,NobetAltGrupId=43,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1248,NobetAltGrupId=43,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1255,NobetAltGrupId=43,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1243,NobetAltGrupId=43,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1244,NobetAltGrupId=43,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1256,NobetAltGrupId=43,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1258,NobetAltGrupId=43,NobetSayisi=3},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1245,NobetAltGrupId=43,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1261,NobetAltGrupId=43,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1262,NobetAltGrupId=43,NobetSayisi=0},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1246,NobetAltGrupId=43,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1247,NobetAltGrupId=43,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1249,NobetAltGrupId=43,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1250,NobetAltGrupId=43,NobetSayisi=3},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1252,NobetAltGrupId=43,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1254,NobetAltGrupId=43,NobetSayisi=3},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1257,NobetAltGrupId=43,NobetSayisi=2},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1259,NobetAltGrupId=43,NobetSayisi=1},
                    new AltGrupIleTutulanNobetDurumu { NobetGrupGorevTipId = 53, EczaneNobetGrupId =1260,NobetAltGrupId=43,NobetSayisi=1}

	                    #endregion
                };

            #endregion

            var ayniGunNobetTakipEdilecekNobetGrupGorevTipIdList = data.AyniGunNobetTakipGrupAltGruplar
                .Select(s => s.NobetGrupGorevTipId).Distinct().ToList();

            var nobetGrupGorevTipler = data.NobetGrupGorevTipler
                .Where(w => ayniGunNobetTakipEdilecekNobetGrupGorevTipIdList.Contains(w.Id)).ToList();

            var altGruplarlaAyniGunNobet = new KpAltGruplarlaAyniGunNobetGrupAltGrup
            {
                AltGrupIleTutulanNobetDurumlari = gecmisNobetSayilari,
                AyniGunNobetTakipGrupAltGruplar = data.AyniGunNobetTakipGrupAltGruplar,
                EczaneNobetAltGrupTarihAralik = data.EczaneNobetAltGrupTarihAralik,
                EczaneNobetGrupAltGruplar = data.EczaneNobetGrupAltGruplar,
                EczaneNobetGruplar = data.EczaneNobetGruplar,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                KararDegiskeni = _x,
                KararDegiskeni2 = _y,
                Model = model,
                NobetGrupGorevTipler = nobetGrupGorevTipler,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k29"),
                Tarihler = data.TarihAraligi
            };

            AltGruplarlaAyniGunNobetGrupAltGrup(altGruplarlaAyniGunNobet);

            #endregion

            #region istenen Eczanelerin Nöbet Günlerini Kısıtla

            var kpIstenenEczanelerinNobetGunleriniKisitla = new KpIstenenEczanelerinNobetGunleriniKisitla
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetTutamazGunler = eczaneNobetTutamazGunler,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "K46"),
                KararDegiskeni = _x
            };
            IstenenEczanelerinNobetGunleriniKisitla(kpIstenenEczanelerinNobetGunleriniKisitla);

            #endregion

            #region eczane gruplar - aynı gün nöbet

            var tarihAraligi = data.TarihAraligi;

            #region eş grup

            var esGrubaAyniGunNobetYazmaEczaneGruplar = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k11"),
                EczaneGruplar = data.EczaneGruplar,
                Tarihler = tarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaEczaneGruplar);

            #endregion

            #region En son tuttuğu alt grup ile tekrar aynı gün tutma - bu iskenderun için iptal

            #region veriler

            var acilAltGruptakiEczaneler = data.EczaneNobetGruplar.Where(w => w.NobetGrupGorevTipId == 42 || w.NobetAltGrupId == 37).ToList();
            var acilEczaneler = data.EczaneNobetTarihAralik.Where(w => acilAltGruptakiEczaneler.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var palmiyeAltGruptakiEczaneler = data.EczaneNobetGruplar.Where(w => w.NobetGrupGorevTipId == 53 || w.NobetAltGrupId == 43).ToList();
            var yeniDevletAltGruptakiEczaneler = data.EczaneNobetGruplar.Where(w => w.NobetGrupGorevTipId == 53 || w.NobetAltGrupId == 44).ToList();

            var palmiyeEczaneler = data.EczaneNobetTarihAralik.Where(w => palmiyeAltGruptakiEczaneler.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();
            var yeniDevletEczaneler = data.EczaneNobetTarihAralik.Where(w => yeniDevletAltGruptakiEczaneler.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            #endregion

            var kpEsGrubaAyniGunNobetYazma = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                KararDegiskeni = _x,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k58"), //29 yerine k58 yazdım sadece bu kuralı iptal etmek için
                Tarihler = data.TarihAraligi
            };

            var esGrubaAyniGunNobetYazmaCarsiAcil = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

            esGrubaAyniGunNobetYazmaCarsiAcil.EczaneNobetTarihAralik = acilEczaneler;
            esGrubaAyniGunNobetYazmaCarsiAcil.EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu.Where(w => w.NobetGrupGorevTipId == 42 || w.NobetAltGrupId == 37).ToList();
            esGrubaAyniGunNobetYazmaCarsiAcil.EczaneGruplar = data.AltGruplarlaAyniGunNobetTutmayacakEczanelerCarsi;

            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaCarsiAcil);

            var esGrubaAyniGunNobetYazmaSskPalmiye = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

            esGrubaAyniGunNobetYazmaSskPalmiye.EczaneNobetTarihAralik = palmiyeEczaneler;
            esGrubaAyniGunNobetYazmaSskPalmiye.EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu.Where(w => w.NobetGrupGorevTipId == 53 || w.NobetAltGrupId == 43).ToList();
            esGrubaAyniGunNobetYazmaSskPalmiye.EczaneGruplar = data.AltGruplarlaAyniGunNobetTutmayacakEczanelerSsk;

            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaSskPalmiye);

            var esGrubaAyniGunNobetYazmaSskYeniDevlet = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

            esGrubaAyniGunNobetYazmaSskYeniDevlet.EczaneNobetTarihAralik = yeniDevletEczaneler;
            esGrubaAyniGunNobetYazmaSskYeniDevlet.EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu.Where(w => w.NobetGrupGorevTipId == 53 || w.NobetAltGrupId == 44).ToList();
            esGrubaAyniGunNobetYazmaSskYeniDevlet.EczaneGruplar = data.AltGruplarlaAyniGunNobetTutmayacakEczanelerYeniDevlet;

            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaSskYeniDevlet);

            #endregion

            //var esGrubaAyniGunNobetYazmaIkiliEczaneler = new KpEsGrubaAyniGunNobetYazma
            //{
            //    Model = model,
            //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
            //    EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
            //    NobetUstGrupKisit = ikiliEczaneAyniGunNobet,
            //    EczaneGruplar = data.ArasindaAyniGun2NobetFarkiOlanIkiliEczaneler,
            //    Tarihler = data.TarihAraligi,
            //    KararDegiskeni = _x
            //};
            //EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaIkiliEczaneler);

            #region Tarih aralığı içinde aynı gün 2 kez nöbet 

            //var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
            //{
            //    Model = model,
            //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.NobetGrupGorevTipId != 54).ToList(),
            //    IkiliEczaneler = data.IkiliEczaneler.Where(w => w.NobetGrupId2 != 54).ToList(),
            //    NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k10"),
            //    Tarihler = data.TarihAraligi,
            //    KararDegiskeni = _x
            //};
            //AyIcindeSadece1KezAyniGunNobetTutulsun(ayIcindeSadece1KezAyniGunNobetKisit);

            //var kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu = new KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu
            //{
            //    Model = model,
            //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
            //    EczaneNobetTarihAralikIkiliEczaneler = data.EczaneNobetTarihAralikIkiliEczaneler,
            //    IkiliEczaneler = data.IkiliEczaneler,
            //    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k10"),
            //    Tarihler = data.TarihAraligi,
            //    KararDegiskeni = _x,
            //    KararDegiskeniIkiliEczaneler = _y
            //};
            //AyIcindeSadece1KezAyniGunNobetTutulsunDegiskenDonusumlu(kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu);

            #endregion

            var esGrubaAyniGunNobetYazmaOncekiAylar = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k41"),
                EczaneGruplar = data.OncekiAylardaAyniGunNobetTutanEczaneler,
                Tarihler = tarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaOncekiAylar);

            var sonrakiAylarAyniGunIstekGirilenler = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = oncekiAylarAyniGunNobet,
                EczaneGruplar = data.SonrakiDonemAyniGunNobetIstekGirilenler,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(sonrakiAylarAyniGunIstekGirilenler);

            #endregion            

            #endregion

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(IskenderunDataModel data)
        {
            var results = new EczaneNobetSonucModel();
            var calismaSayisiEnFazla = data.CalismaSayisiLimit;

            var config = new Configuration
            {
                NameHandling = NameHandlingStyle.Manual,
                ComputeRemovedVariables = true
            };

            try
            {
                using (var scope = new ModelScope(config))
                {
                    var model = Model(data);

                    // Get a solver instance, change your solver
                    var solverConfig = new CplexSolverConfiguration()
                    {
                        ComputeIIS = true,
                        TimeLimit = data.TimeLimit
                    };

                    var solver = new CplexSolver(solverConfig);

                    //solver.Abort();
                    //solver.Configuration.TimeLimit = 1;

                    // solve the model
                    var solution = solver.Solve(model);

                    //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint1));
                    //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint2));

                    var modelStatus = solution.ModelStatus;
                    var solutionStatus = solution.Status;
                    var modelName = solution.ModelName;

                    if (modelStatus != ModelStatus.Feasible)
                    {
                        //data.CalismaSayisi++;

                        if (data.CalismaSayisi == calismaSayisiEnFazla)
                        {
                            results.Celiskiler = CeliskileriEkle(solution);
                        }

                        throw new Exception($"Uygun çözüm bulunamadı!.");
                    }
                    else
                    {
                        var sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value.IsAlmost(1) == true).ToList();

                        var ayniGunNobetTutanEczaneler = GetAyniGunNobetTutanEczaneler(sonuclar);

                        var ayniGunNobetSayisi1denFazlaGrouped = (from s in ayniGunNobetTutanEczaneler
                                                                  group s by new
                                                                  {
                                                                      //s.EczaneBirlesim,
                                                                      s.EczaneAdi1,
                                                                      s.EczaneAdi2,
                                                                      s.EczaneId1,
                                                                      s.EczaneId2
                                                                      //s.EczaneNobetGrupId1,
                                                                      //s.EczaneNobetGrupId2,
                                                                      //s.Grup,
                                                                      //s.AltGrupAdi
                                                                  } into grouped
                                                                  //where grouped.Count() > 1
                                                                  select new AyniGunTutulanNobetDetay
                                                                  {
                                                                      //Grup = grouped.Key.Grup,
                                                                      EczaneAdi1 = grouped.Key.EczaneAdi1,
                                                                      EczaneAdi2 = grouped.Key.EczaneAdi2,
                                                                      EczaneId1 = grouped.Key.EczaneId1,
                                                                      EczaneId2 = grouped.Key.EczaneId2,
                                                                      //EczaneNobetGrupId1 = grouped.Key.EczaneNobetGrupId1,
                                                                      //EczaneNobetGrupId2 = grouped.Key.EczaneNobetGrupId2,
                                                                      //AltGrupAdi = grouped.Key.AltGrupAdi,
                                                                      AyniGunNobetSayisi = grouped.Count(),
                                                                      //Tarih = grouped.Key.Tarih,
                                                                      //TakvimId = s.TakvimId,
                                                                      //GunGrup = grouped.Key.GunGrup
                                                                  })
                                          .Where(w => w.AyniGunNobetSayisi > 1)
                                          .ToList();

                        if (ayniGunNobetSayisi1denFazlaGrouped.Count > 0)
                        {
                            var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
                            {
                                Model = model,
                                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                                //.Where(w => w.NobetGrupGorevTipId != 54).ToList(),
                                IkiliEczaneler = data.IkiliEczaneler,
                                //.Where(w => w.NobetGrupId2 != 54).ToList(),
                                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k10"),
                                Tarihler = data.TarihAraligi,
                                KararDegiskeni = _x
                            };
                            AyIcindeSadece1KezAyniGunNobetTutulsunEczaneBazli(ayIcindeSadece1KezAyniGunNobetKisit);

                            //var kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu = new KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu
                            //{
                            //    Model = model,
                            //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                            //    EczaneNobetTarihAralikIkiliEczaneler = data.EczaneNobetTarihAralikIkiliEczaneler,
                            //    IkiliEczaneler = data.IkiliEczaneler,
                            //    NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k10"),
                            //    Tarihler = data.TarihAraligi,
                            //    KararDegiskeni = _x,
                            //    KararDegiskeniIkiliEczaneler = _y
                            //};
                            //AyIcindeSadece1KezAyniGunNobetTutulsun(kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu);

                            solution = solver.Solve(model);

                            modelStatus = solution.ModelStatus;

                            if (modelStatus != ModelStatus.Feasible)
                            {
                                //data.CalismaSayisi++;

                                results.Celiskiler = $"<h6 class= 'text-danger'>k10 tarih aralığında aynı gün 2 kez nöbet eczaneler bulunmaktadır.</h6>*1";
                                results.Celiskiler += CeliskileriEkle(solution);

                                throw new Exception($"Uygun çözüm bulunamadı!");
                            }
                        }

                        // import the results back into the model 
                        model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));
                        var objective = solution.ObjectiveValues.Single();
                        var sure = solution.OverallWallTime;
                        var bestBound = solution.BestBound;
                        var bakilanDugumSayisi = solution.NumberOfExploredNodes;
                        var kisitSayisi = model.ConstraintsCount;

                        results.CozumSuresi = sure;
                        results.ObjectiveValue = objective.Value;
                        results.BakilanDugumSayisi = bakilanDugumSayisi;
                        results.KisitSayisi = kisitSayisi;
                        results.ResultModel = new List<EczaneNobetCozum>();
                        results.KararDegikeniSayisi = data.EczaneNobetTarihAralik.Count;
                        results.CalismaSayisi = data.CalismaSayisi;
                        results.NobetGrupSayisi = data.NobetGruplar.Count;
                        results.IncelenenEczaneSayisi = data.EczaneNobetGruplar.Count;

                        sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value.IsAlmost(1) == true).ToList();//BUNU SAKIN KAPATMA.. 

                        var nobetGrupTarihler1 = data.EczaneNobetTarihAralik
                             .Select(s => new
                             {
                                 s.NobetGrupId,
                                 s.Tarih,
                                 s.NobetGorevTipId,
                                 Talep = s.TalepEdilenNobetciSayisi
                             }).Distinct().ToList();

                        var toplamArz = sonuclar.Count;
                        var toplamTalep = nobetGrupTarihler1.Sum(s => s.Talep);

                        if (toplamArz != toplamTalep)
                        {
                            throw new Exception("Talebi karşılanmayan günler var");
                        }

                        foreach (var r in sonuclar)
                        {
                            results.ResultModel.Add(new EczaneNobetCozum()
                            {
                                TakvimId = r.TakvimId,
                                EczaneNobetGrupId = r.EczaneNobetGrupId,
                                NobetGorevTipId = r.NobetGorevTipId
                            });
                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                data.CalismaSayisi++;

                if (data.CalismaSayisi <= calismaSayisiEnFazla)//10
                {
                    results = Solve(data);
                }
                else if (ex.Message.StartsWith("Uygun çözüm bulunamadı"))
                {//çözüm yok
                    var mesaj = ex.Message;

                    var iterasyonMesaj = "";

                    if (data.CozumItereasyon.IterasyonSayisi > 0)
                    {
                        iterasyonMesaj = $"<br/>Aynı gün nöbet: Çözüm esnasında ay içinde aynı gün nöbet tutan eczaneler;" +
                            $"<br/>İterasyon sayısı: <strong>{data.CozumItereasyon.IterasyonSayisi}</strong>."
                            //+ $"<br/>Elenen eczane sayısı: <strong>{data.AyIcindeAyniGunNobetTutanEczaneler.Count}</strong>."
                            ;
                    }

                    var celiskiler = results.Celiskiler.Split('*');

                    mesaj = CeliskileriTabloyaAktar(data.BaslangicTarihi, data.BitisTarihi, data.CalismaSayisi, iterasyonMesaj, data.NobetGrupGorevTipler, celiskiler);

                    throw new Exception(mesaj);
                }
                else
                {
                    throw ex;
                }
            }
            return results;
        }

        //public EczaneNobetSonucModel Solve(IskenderunDataModel data)
        //{
        //    var results = new EczaneNobetSonucModel();
        //    var calismaSayisiEnFazla = data.CalismaSayisiLimit;

        //    var config = new Configuration
        //    {
        //        NameHandling = NameHandlingStyle.Manual,
        //        ComputeRemovedVariables = true
        //    };

        //    try
        //    {
        //        using (var scope = new ModelScope(config))
        //        {
        //            var model = Model(data);

        //            // Get a solver instance, change your solver
        //            var solverConfig = new CplexSolverConfiguration()
        //            {
        //                ComputeIIS = true,
        //                TimeLimit = data.TimeLimit
        //            };

        //            var solver = new CplexSolver(solverConfig);

        //            //solver.Abort();
        //            //solver.Configuration.TimeLimit = 1;

        //            // solve the model
        //            var solution = solver.Solve(model);

        //            //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint1));
        //            //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint2));

        //            var modelStatus = solution.ModelStatus;
        //            var solutionStatus = solution.Status;
        //            var modelName = solution.ModelName;

        //            if (modelStatus != ModelStatus.Feasible)
        //            {
        //                if (data.CalismaSayisi == calismaSayisiEnFazla)
        //                {
        //                    results.Celiskiler = CeliskileriEkle(solution);
        //                }

        //                throw new Exception($"Uygun çözüm bulunamadı!.");
        //            }
        //            else
        //            {
        //                // import the results back into the model 
        //                model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));
        //                var objective = solution.ObjectiveValues.Single();
        //                var sure = solution.OverallWallTime;
        //                var bestBound = solution.BestBound;
        //                var bakilanDugumSayisi = solution.NumberOfExploredNodes;
        //                var kisitSayisi = model.ConstraintsCount;

        //                results.CozumSuresi = sure;
        //                results.ObjectiveValue = objective.Value;
        //                results.BakilanDugumSayisi = bakilanDugumSayisi;
        //                results.KisitSayisi = kisitSayisi;
        //                results.ResultModel = new List<EczaneNobetCozum>();
        //                results.KararDegikeniSayisi = data.EczaneNobetTarihAralik.Count;
        //                results.CalismaSayisi = data.CalismaSayisi;
        //                results.NobetGrupSayisi = data.NobetGruplar.Count;
        //                results.IncelenenEczaneSayisi = data.EczaneNobetGruplar.Count;

        //                var sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value.IsAlmost(1) == true).ToList();

        //                var nobetGorevTipler = data.NobetGrupGorevTipler
        //                    .Select(s => new
        //                    {
        //                        s.NobetGorevTipId,
        //                        s.NobetGorevTipAdi
        //                    }).Distinct().ToList();

        //                //var nobetGorevTipId = 1;

        //                var nobetGrupTarihler1 = data.EczaneNobetTarihAralik
        //                    .Where(w => nobetGorevTipler.Select(s => s.NobetGorevTipId).Contains(w.NobetGorevTipId))
        //                    .Select(s => new
        //                    {
        //                        s.NobetGrupId,
        //                        s.Tarih,
        //                        s.NobetGorevTipId,
        //                        Talep = s.TalepEdilenNobetciSayisi
        //                        //data.NobetGrupTalepler
        //                        // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
        //                        //         && w.TakvimId == s.TakvimId).SingleOrDefault() == null
        //                        //? (int)data.NobetGrupKurallar
        //                        //    .Where(k => k.NobetKuralId == 3
        //                        //             && k.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
        //                        //    .Select(k => k.Deger).SingleOrDefault()
        //                        //: data.NobetGrupTalepler
        //                        // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
        //                        //         && w.TakvimId == s.TakvimId).SingleOrDefault().NobetciSayisi
        //                    }).Distinct().ToList();

        //                //nobetGorevTipId = 2;

        //                //var nobetGrupTarihler2 = data.EczaneNobetTarihAralik
        //                //    .Where(w => w.NobetGorevTipId == nobetGorevTipId)
        //                //    .Select(s => new
        //                //    {
        //                //        s.NobetGrupId,
        //                //        s.Tarih,
        //                //        s.NobetGorevTipId,
        //                //        Talep = s.TalepEdilenNobetciSayisi
        //                //        //data.NobetGrupTalepler
        //                //        // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
        //                //        //         && w.TakvimId == s.TakvimId).SingleOrDefault() == null
        //                //        //? (int)data.NobetGrupKurallar
        //                //        //    .Where(k => k.NobetKuralId == 3
        //                //        //             && k.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
        //                //        //    .Select(k => k.Deger).SingleOrDefault()
        //                //        //: data.NobetGrupTalepler
        //                //        // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
        //                //        //         && w.TakvimId == s.TakvimId).SingleOrDefault().NobetciSayisi
        //                //    }).Distinct().ToList();

        //                var toplamArz = sonuclar.Count;
        //                var toplamTalep = nobetGrupTarihler1.Sum(s => s.Talep);// + nobetGrupTarihler2.Sum(s => s.Talep);

        //                if (toplamArz != toplamTalep)
        //                {
        //                    throw new Exception("Talebi karşılanmayan günler var");
        //                }

        //                foreach (var r in sonuclar)
        //                {
        //                    results.ResultModel.Add(new EczaneNobetCozum()
        //                    {
        //                        TakvimId = r.TakvimId,
        //                        EczaneNobetGrupId = r.EczaneNobetGrupId,
        //                        NobetGorevTipId = r.NobetGorevTipId
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        data.CalismaSayisi++;

        //        if (data.CalismaSayisi <= calismaSayisiEnFazla)//10
        //        {
        //            results = Solve(data);
        //        }
        //        else if (ex.Message.StartsWith("Uygun çözüm bulunamadı"))
        //        {//çözüm yok
        //            var mesaj = ex.Message;

        //            var iterasyonMesaj = "";
        //            if (data.CozumItereasyon.IterasyonSayisi > 0)
        //            {
        //                iterasyonMesaj = $"<br/>Aynı gün nöbet: Çözüm esnasında ay içinde aynı gün nöbet tutan eczaneler;" +
        //                    $"<br/>İterasyon sayısı: <strong>{data.CozumItereasyon.IterasyonSayisi}</strong>."
        //                    //+ $"<br/>Elenen eczane sayısı: <strong>{data.AyIcindeAyniGunNobetTutanEczaneler.Count}</strong>."
        //                    ;
        //            }

        //            string cozulenNobetGruplar = null;

        //            var ilkGrup = data.NobetGruplar.Select(s => s.Adi).FirstOrDefault();

        //            foreach (var i in data.NobetGruplar.Select(s => s.Adi))
        //            {
        //                if (i == ilkGrup)
        //                {
        //                    cozulenNobetGruplar += i;
        //                }
        //                else
        //                {
        //                    cozulenNobetGruplar += $", {i}";
        //                }
        //            }

        //            //$"<strong><span class='text-danger'>Çözüm bulunamadı.</strong> "
        //            //      + "<br/> "
        //            //      +

        //            var celiskiler = results.Celiskiler.Split('*');

        //            mesaj = "Tabloya göre <a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">nöbet ayarlarında</a> bazı değişiklikler yaparak <strong>tekrar çözmelisiniz..</strong>"
        //                  + "<hr /> "
        //                  + $"<strong>Kontrol edilecek kurallar <span class='badge badge-warning'>{celiskiler[1]}</span></strong>"
        //                  + "<br /> "
        //                  + celiskiler[0];

        //            mesaj += $"<strong>Nöbet seçenekleri:</strong>" +
        //                $"<br />Nöbet Grupları: {cozulenNobetGruplar}" +
        //                $"<br />Tarih Aralığı: <strong>{data.BaslangicTarihi.ToShortDateString()}-{data.BitisTarihi.ToShortDateString()}</strong>" +
        //                $"<hr/> " +
        //                $"<strong>Çalışma adımları <span class='badge badge-info'>{data.CalismaSayisi}</span></strong>" +
        //                $"{iterasyonMesaj} "
        //                ;

        //            var calismaAdimlari = new string[10]
        //                {
        //                    "Çözüm bulunamadı.",
        //                    //"Tekrar çözüm denendi.",
        //                    "kumulatifOrtalamaGunKuralSayisi 1 artırıldı.",
        //                    "haftaIciOrtalama (satır ortalaması) 1 artırıldı.",
        //                    "haftaIciOrtalama (satır ortalaması) 2 artırıldı.",
        //                    "Ayda en fazla 1 gorev kaldırıldı!",
        //                    "Farklı ay peşpeşe görev gevşetildi!",
        //                    "Ayda en fazla 1 gorev kaldırıldı ve Farklı Ay Peşpeşe Görev gevşetildi!",
        //                    "Cuma ve cumartesi en fazla 3 olmadı 4 olarak gevşetildi!",
        //                    "Farklı ay peşpeşe görev sayısı en çok 5 olarak gevşetildi!",
        //                    "default"
        //                };

        //            for (int i = 0; i < data.CalismaSayisi; i++)
        //            {
        //                mesaj += "<br /> " + i + " " + calismaAdimlari[i];
        //            }

        //            throw new Exception(mesaj);
        //        }
        //        else
        //        {
        //            throw ex;
        //        }
        //    }
        //    return results;
        //}

    }

}
