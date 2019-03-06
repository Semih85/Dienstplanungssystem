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

            var kisitlarUstGrup = data.NobetUstGrupKisitlar;
            var kisitlarAktif = new List<NobetUstGrupKisitDetay>();

            kisitlarAktif.AddRange(kisitlarUstGrup);

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

            var acilAltGruptakiEczaneler = data.EczaneNobetGruplar.Where(w => w.NobetGrupGorevTipId == 42 || w.NobetAltGrupId == 37).ToList();
            var acilEczaneler = data.EczaneNobetTarihAralik.Where(w => acilAltGruptakiEczaneler.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var palmiyeAltGruptakiEczaneler = data.EczaneNobetGruplar.Where(w => w.NobetGrupGorevTipId == 53 || w.NobetAltGrupId == 43).ToList();
            var yeniDevletAltGruptakiEczaneler = data.EczaneNobetGruplar.Where(w => w.NobetGrupGorevTipId == 53 || w.NobetAltGrupId == 44).ToList();
            var palmiyeEczaneler = data.EczaneNobetTarihAralik.Where(w => palmiyeAltGruptakiEczaneler.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();
            var yeniDevletEczaneler = data.EczaneNobetTarihAralik.Where(w => yeniDevletAltGruptakiEczaneler.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler)
            {
                #region kısıtlar grup bazlı

                var kisitlarGrupBazli = data.NobetGrupGorevTipKisitlar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                if (kisitlarGrupBazli.Count > 0)
                {
                    foreach (var grupBazliKisit in kisitlarGrupBazli)
                    {
                        var kisitUstGrup = kisitlarAktif.SingleOrDefault(w => w.KisitId == grupBazliKisit.KisitId);

                        var kisitGrup = new NobetUstGrupKisitDetay
                        {
                            Id = kisitUstGrup.Id,
                            KisitId = kisitUstGrup.KisitId,
                            KisitAdi = kisitUstGrup.KisitAdi,
                            KisitAciklama = kisitUstGrup.KisitAciklama,
                            KisitAdiGosterilen = kisitUstGrup.KisitAdiGosterilen,
                            KisitKategoriAdi = kisitUstGrup.KisitKategoriAdi,
                            KisitKategoriId = kisitUstGrup.KisitKategoriId,
                            NobetUstGrupAdi = kisitUstGrup.NobetUstGrupAdi,
                            NobetUstGrupId = kisitUstGrup.NobetUstGrupId,
                            SagTarafDegeriVarsayilan = kisitUstGrup.SagTarafDegeriVarsayilan,
                            VarsayilanPasifMi = kisitUstGrup.VarsayilanPasifMi,

                            PasifMi = grupBazliKisit.PasifMi,
                            SagTarafDegeri = grupBazliKisit.SagTarafDegeri
                        };

                        kisitlarAktif.Remove(kisitUstGrup);
                        kisitlarAktif.Add(kisitGrup);
                    }
                }
                else
                {
                    kisitlarAktif = kisitlarUstGrup;
                }
                #endregion

                #region ön hazırlama

                #region tarihler

                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var pespeseNobetSayisi = (int)nobetGrupKurallar
                        .Where(s => s.NobetKuralId == 1)
                        .Select(s => s.Deger).SingleOrDefault();

                int gunlukNobetciSayisi = (int)nobetGrupKurallar
                        .Where(s => s.NobetKuralId == 3)
                        .Select(s => s.Deger).SingleOrDefault();

                var haftaIciPespeseNobetYazilmasinKuralKatsayisi = nobetGrupKurallar
                       .Where(s => s.NobetKuralId == 5)
                       .SingleOrDefault() ?? new NobetGrupKuralDetay();
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
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var nobetGrupGunKurallar = data.NobetGrupGorevTipGunKurallar
                                                    .Where(s => s.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                                             && nobetGunKurallar.Contains(s.NobetGunKuralId))
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
                if (haftaIciPespeseNobetYazilmasinKuralKatsayisi.Deger > 0)
                {
                    altLimit = (int)haftaIciPespeseNobetYazilmasinKuralKatsayisi.Deger;
                }

                var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                var ustLimitKontrol = ustLimit * 0.95; //0.81 

                var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / (5 * gunlukNobetciSayisi)) - 1;
                var cumartersiNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / (5 * gunlukNobetciSayisi)) - 1;
                #endregion

                #region ortalama nöbet sayıları

                var cumartesiTalepEdilenNobetciSayisiTumGorevTipler = data.TarihAraligi.Where(w => w.GunGrupId == 4).Sum(s => s.TalepEdilenNobetciSayisi);

                var haftaIciOrtamalaNobetSayisi = OrtalamaNobetSayisi(haftaIciGunleri.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var ortalamaNobetSayisi = OrtalamaNobetSayisi(tarihler.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var ortalamaNobetSayisiTumGorevTipleri = OrtalamaNobetSayisi(data.TarihAraligi.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var ortalamaNobetSayisiCumartesiTumGorevTipleri = OrtalamaNobetSayisi(cumartesiTalepEdilenNobetciSayisiTumGorevTipler, gruptakiEczaneSayisi);
                var bayramOrtalamaNobetSayisi = OrtalamaNobetSayisi(bayramlar.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);

                #endregion

                var nobetGrupBayramNobetleri = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                             && w.GunGrupId == 2
                             && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                    .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                var nobetGunKuralIstatistikler = data.TakvimNobetGrupGunDegerIstatistikler
                                      .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                               && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                                               && tarihler.Select(s => s.NobetGunKuralId).Distinct().Contains(w.NobetGunKuralId)).ToList();

                var nobetGunKuralTarihler = new List<NobetGunKuralTarihAralik>();

                foreach (var item in nobetGunKuralIstatistikler)
                {
                    var tarihler2 = tarihler.Where(w => w.NobetGunKuralId == item.NobetGunKuralId).ToList();
                    var gunKuralGunSayisi = tarihler2.Count;

                    nobetGunKuralTarihler.Add(new NobetGunKuralTarihAralik
                    {
                        GunGrupId = item.GunGrupId,
                        GunGrupAdi = item.GunGrupAdi,
                        NobetGunKuralId = item.NobetGunKuralId,
                        NobetGunKuralAdi = item.NobetGunKuralAdi,
                        TakvimNobetGruplar = tarihler2,
                        GunSayisi = gunKuralGunSayisi,
                        OrtalamaNobetSayisi = OrtalamaNobetSayisi(tarihler2.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi),
                        KumulatifGunSayisi = item.GunSayisi,
                        KumulatifOrtalamaNobetSayisi = OrtalamaNobetSayisi(item.TalepEdilenNobetciSayisi, gruptakiEczaneSayisi),
                        //OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, item.GunSayisi)
                    });
                }

                #endregion

                #region talep

                var talebiKarsilaKisitParametreModel = new KpTalebiKarsila
                {
                    EczaneNobetTarihAralikTumu = eczaneNobetTarihAralikGrupBazli,
                    NobetGrupGorevTip = nobetGrupGorevTip,
                    Tarihler = tarihler,
                    Model = model,
                    KararDegiskeni = _x
                };

                TalebiKarsila(talebiKarsilaKisitParametreModel);

                #endregion

                #region eczane bazlı kısıtlar

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {
                    #region kontrol

                    var kontrol = false;

                    if (kontrol)
                    {
                        var kontrolEdilecekEczaneler = new string[] {
                            "IŞIK",
                            //"ELİF", "AYDINLAR", "DERYA", "KARABIÇAK","IŞIK"
                        };

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                        {
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

                    var nobetYazilabilirGunler = nobetGrupGunKurallar
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
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K1"),
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
                        OrtamalaNobetSayisi = haftaIciOrtamalaNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        PespeseNobetSayisiAltLimit = altLimit,
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K27"),
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
                    pesPeseGorevEnAzFarkliAylar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K18");
                    pesPeseGorevEnAzFarkliAylar.Tarihler = tarihler;
                    pesPeseGorevEnAzFarkliAylar.NobetYazilabilecekIlkTarih = nobetYazilabilecekIlkTarihTumGorevTipleri;

                    PesPeseGorevEnAz(pesPeseGorevEnAzFarkliAylar);

                    //karar değişkeni değişti
                    pesPeseGorevEnAzBase.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli;

                    #region cumartesi

                    var pesPeseGorevEnAzCumartesi = (KpPesPeseGorevEnAz)pesPeseGorevEnAzBase.Clone();

                    pesPeseGorevEnAzCumartesi.NobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;
                    pesPeseGorevEnAzCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K37");
                    pesPeseGorevEnAzCumartesi.Tarihler = cumartesiGunleri;
                    pesPeseGorevEnAzCumartesi.NobetYazilabilecekIlkTarih = yazilabilecekIlkCumartesiTarihi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzCumartesi);

                    #endregion

                    #region pazar

                    var pesPeseGorevEnAzPazar = (KpPesPeseGorevEnAz)pesPeseGorevEnAzBase.Clone();

                    pesPeseGorevEnAzPazar.NobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;
                    pesPeseGorevEnAzPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K28");
                    pesPeseGorevEnAzPazar.Tarihler = pazarGunleri;
                    pesPeseGorevEnAzPazar.NobetYazilabilecekIlkTarih = yazilabilecekIlkPazarTarihi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzPazar);

                    #endregion

                    #region hafta içi

                    var pesPeseGorevEnAzHaftaIci = (KpPesPeseGorevEnAz)pesPeseGorevEnAzBase.Clone();

                    pesPeseGorevEnAzHaftaIci.NobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;
                    pesPeseGorevEnAzHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K27");
                    pesPeseGorevEnAzHaftaIci.Tarihler = haftaIciGunleri;
                    pesPeseGorevEnAzHaftaIci.NobetYazilabilecekIlkTarih = yazilabilecekIlkHaftaIciTarihi;

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
                    ortalamaEnFazlaTumTarihAraligi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K19");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaTumTarihAraligi);

                    var ortalamaEnFazlaHaftaIici = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaHaftaIici.Tarihler = haftaIciGunleri;
                    ortalamaEnFazlaHaftaIici.GunSayisi = haftaIciSayisi;
                    ortalamaEnFazlaHaftaIici.OrtalamaNobetSayisi = haftaIciOrtamalaNobetSayisi;
                    ortalamaEnFazlaHaftaIici.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K32");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHaftaIici);

                    var ortalamaEnFazlaBayram = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaBayram.Tarihler = bayramlar;
                    ortalamaEnFazlaBayram.GunSayisi = bayramSayisi;
                    ortalamaEnFazlaBayram.OrtalamaNobetSayisi = bayramOrtalamaNobetSayisi;
                    ortalamaEnFazlaBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K14");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaBayram);

                    #endregion

                    #region Kümülatif kısıtlar

                    //var kumulatifHaftaIci = nobetGunKuralIstatistikler.Where(w => w.NobetGunKuralId >= 2 && w.NobetGunKuralId < 7).Sum(s => s.GunSayisi);// + haftaIciSayisi;

                    //var kumulatifToplamEnFazlaHaftaIci = new KpKumulatifToplamEnFazla
                    //{
                    //    Model = model,
                    //    Tarihler = haftaIciGunleri,
                    //    EczaneNobetGrup = eczaneNobetGrup,
                    //    EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                    //    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K19"),
                    //    KumulatifOrtalamaGunKuralSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, kumulatifHaftaIci),
                    //    ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci,
                    //    KararDegiskeni = _x
                    //};
                    //if (data.CalismaSayisi == 1)
                    //    kumulatifToplamEnFazlaHaftaIci.KumulatifOrtalamaGunKuralSayisi++;

                    //KumulatifToplamEnFazla(kumulatifToplamEnFazlaHaftaIci);                   

                    var nobetGunKuralNobetSayilari = new List<NobetGunKuralNobetSayisi>();

                    foreach (var gunKural in nobetGunKuralIstatistikler)
                    {
                        int toplamNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                        nobetGunKuralNobetSayilari.Add(new NobetGunKuralNobetSayisi
                        {
                            GunGrupId = gunKural.GunGrupId,
                            GunGrupAdi = gunKural.GunGrupAdi,
                            NobetGunKuralAdi = gunKural.NobetGunKuralAdi,
                            NobetGunKuralId = gunKural.NobetGunKuralId,
                            NobetSayisi = toplamNobetSayisi
                        });
                    }

                    //var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi) - nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                    var haftaIciEnCokNobetSayisi = 0;
                    var haftaIciEnAzNobetSayisi = 0;

                    if (nobetGunKuralIstatistikler.Count > 0)
                    {
                        var haftaiciNobetIstatistik = nobetGunKuralNobetSayilari
                            .Where(w => w.GunGrupId == 3).ToList();

                        haftaIciEnCokNobetSayisi = haftaiciNobetIstatistik.Max(m => m.NobetSayisi);
                        haftaIciEnAzNobetSayisi = haftaiciNobetIstatistik.Min(m => m.NobetSayisi);
                    }
                    foreach (var gunKural in nobetGunKuralIstatistikler
                        //.Where(w => w.NobetGunKuralId == 7 || w.NobetGunKuralId == 1) // cumartesi ve pazar 
                        )
                    {//gun kural bazlı

                        //var nobetTutamazEczaneGunu = nobetTutamazGunler
                        //    .Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).SingleOrDefault();

                        if (nobetTutamazGunler.Count > 0
                            //nobetTutamazEczaneGunu != null
                            && nobetYazilabilirGunler.Contains(gunKural.NobetGunKuralId)
                            && !NobetUstGrupKisit(kisitlarAktif, "K46").PasifMi
                            )
                            continue;

                        if (kontrol && gunKural.NobetGunKuralAdi == "Cuma")
                        {
                        }

                        switch (gunKural.NobetGunKuralId)
                        {
                            case 1:
                                herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "K23");
                                break;
                            case 7:
                                herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "K38");
                                break;
                            case 6:
                                herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "K43");
                                break;
                            default:
                                herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "K42");
                                break;
                        }

                        var tarihAralik = nobetGunKuralTarihler.Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();

                        //gün kural ortalama en fazla
                        var tarihAraligiOrtalamaEnFazlaIlgiliKisit = new KpTarihAraligiOrtalamaEnFazla
                        {
                            Model = model,
                            Tarihler = tarihAralik.TakvimNobetGruplar,
                            GunSayisi = tarihAralik.GunSayisi,
                            OrtalamaNobetSayisi = tarihAralik.OrtalamaNobetSayisi,
                            EczaneNobetGrup = eczaneNobetGrup,
                            EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                            NobetUstGrupKisit = herAyEnFazlaIlgiliKisit,
                            GunKuralAdi = gunKural.NobetGunKuralAdi,
                            KararDegiskeni = _x
                        };
                        TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaIlgiliKisit);

                        #region Kümülatif toplam en fazla - Tur takip kısıtı

                        var kumulatifOrtalamaGunKuralNobetSayisi = tarihAralik.KumulatifOrtalamaNobetSayisi;

                        int gunKuralNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                        var haftaIciEnCokVeGunKuralNobetleriArasindakiFark = haftaIciEnCokNobetSayisi - gunKuralNobetSayisi;
                        var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = haftaIciEnCokNobetSayisi - haftaIciEnAzNobetSayisi;

                        var gunKumulatifToplamEnFazlaStd = NobetUstGrupKisit(kisitlarAktif, "K34").SagTarafDegeri;

                        if (gunKumulatifToplamEnFazlaStd > 0 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKumulatifToplamEnFazlaStd;

                        if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                        //haftaninGunleriDagilimi.SagTarafDegeri = 3
                        if (haftaIciEnAzVeEnCokNobetSayisiArasindakiFark < NobetUstGrupKisit(kisitlarAktif, "K44").SagTarafDegeri
                            && !NobetUstGrupKisit(kisitlarAktif, "K44").PasifMi
                            )
                        {//hafta içi dağılım
                            if (data.CalismaSayisi == 1
                                //&& !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 1
                                )
                                kumulatifOrtalamaGunKuralNobetSayisi++;

                            if (data.CalismaSayisi == 2
                                //&& !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 0// haftaIciEnCokNobetSayisi
                                )
                                kumulatifOrtalamaGunKuralNobetSayisi++;

                            if (data.CalismaSayisi == 3
                                && !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                //&& !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                //&& haftaIciEnAzVeEnCokNobetSayisiArasindakiFark <= haftaIciEnCokNobetSayisi
                                )
                                kumulatifOrtalamaGunKuralNobetSayisi++;// += 2;
                        }
                        //cumartesi gününü 1 fazla yapmak için
                        if (data.CalismaSayisi >= 2
                            && gunKural.NobetGunKuralId == 7)
                            kumulatifOrtalamaGunKuralNobetSayisi++;

                        var kumulatifToplamEnFazla = new KpKumulatifToplam
                        {
                            Model = model,
                            Tarihler = tarihAralik.TakvimNobetGruplar,
                            EczaneNobetGrup = eczaneNobetGrup,
                            EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                            NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K34"),
                            KumulatifOrtalamaGunKuralSayisi = kumulatifOrtalamaGunKuralNobetSayisi,
                            ToplamNobetSayisi = gunKuralNobetSayisi,
                            GunKuralAdi = gunKural.NobetGunKuralAdi,
                            KararDegiskeni = _x
                        };
                        KumulatifToplamEnFazla(kumulatifToplamEnFazla);

                        #endregion
                    }
                    #endregion

                    #region Bayram kümülatif toplam en fazla ve farklı tür bayram

                    if (bayramlar.Count() > 0)
                    {
                        var bayramNobetleri = nobetGunKuralNobetSayilari.Where(w => w.GunGrupId == 2).ToList();

                        var bayramNobetleriAnahtarli = eczaneNobetSonuclar
                                .Where(w => w.NobetGunKuralId > 7).ToList();

                        var toplamBayramNobetSayisi = bayramNobetleri.Sum(s => s.NobetSayisi);

                        if (!NobetUstGrupKisit(kisitlarAktif, "K5").PasifMi)
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

                            if (NobetUstGrupKisit(kisitlarAktif, "K5").SagTarafDegeri > 0)
                                yillikOrtalamaGunKuralSayisi = NobetUstGrupKisit(kisitlarAktif, "K5").SagTarafDegeri;

                            var sonBayram = bayramNobetleriAnahtarli.LastOrDefault();

                            if (sonBayram != null)
                            {
                                if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayram.NobetGunKuralId) && !NobetUstGrupKisit(kisitlarAktif, "K36").PasifMi)
                                {
                                    var bayramPespeseFarkliTurKisit = new KpPespeseFarkliTurNobet
                                    {
                                        Model = model,
                                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K36"),
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
                                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K5"),
                                        KumulatifOrtalamaGunKuralSayisi = yillikOrtalamaGunKuralSayisi,
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
                    kpKumulatifToplamEnFazla.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K2");
                    kpKumulatifToplamEnFazla.ToplamNobetSayisi = eczaneBazliGunKuralIstatistikYatay.NobetSayisiToplam;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazla);

                    #endregion 

                    #region Hafta içi toplam max hedefler

                    var kpKumulatifToplamEnFazlaHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K16");
                    kpKumulatifToplamEnFazlaHaftaIci.ToplamNobetSayisi = eczaneBazliGunKuralIstatistikYatay.NobetSayisiHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIci);

                    #endregion

                    #region Toplam cuma cumartesi max hedefler

                    var kpKumulatifToplamEnFazlaCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K25");
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.ToplamNobetSayisi = eczaneBazliGunKuralIstatistikYatay.NobetSayisiCuma + eczaneBazliGunKuralIstatistikYatay.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumaVeCumartesi);

                    #endregion

                    #region Toplam cumartesi ve pazar max hedefler

                    var kpKumulatifToplamEnFazlaCumartesiVePazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesiVePazar.Tarihler = cumartesiVePazarGunleri;
                    kpKumulatifToplamEnFazlaCumartesiVePazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K50");
                    kpKumulatifToplamEnFazlaCumartesiVePazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar + eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesiVePazar);

                    #endregion

                    #region Toplam Cuma Max Hedef

                    var kpKumulatifToplamEnFazlaCuma = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCuma.Tarihler = cumaGunleri;
                    kpKumulatifToplamEnFazlaCuma.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K20");
                    kpKumulatifToplamEnFazlaCuma.ToplamNobetSayisi = eczaneBazliGunKuralIstatistikYatay.NobetSayisiCuma;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCuma);

                    #endregion

                    #region Toplam Cumartesi Max Hedef

                    var kpKumulatifToplamEnFazlaCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesi.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K21");
                    kpKumulatifToplamEnFazlaCumartesi.ToplamNobetSayisi = eczaneBazliGunKuralIstatistikYatay.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesi);

                    #endregion

                    #region Toplam Pazar Max Hedef

                    var kpKumulatifToplamEnFazlaPazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaPazar.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnFazlaPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K57");
                    kpKumulatifToplamEnFazlaPazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaPazar);

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
                        tarihAraligindaEnAz1NobetYazKisitTarihAraligi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K4");
                        tarihAraligindaEnAz1NobetYazKisitTarihAraligi.Tarihler = tarihler;

                        TarihAraligindaEnAz1NobetYaz(tarihAraligindaEnAz1NobetYazKisitTarihAraligi);
                    }

                    #endregion

                    #region tarih aralığı en az 1 hafta içi

                    var tarihAraligindaEnAz1NobetYazKisitHaftaIci = (KpTarihAraligindaEnAz1NobetYaz)KpTarihAraligindaEnAz1NobetYaz.Clone();

                    tarihAraligindaEnAz1NobetYazKisitHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K40");
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
                        kpKumulatifToplamEnAz.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K3");
                        kpKumulatifToplamEnAz.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;

                        KumulatifToplamEnFazla(kpKumulatifToplamEnAz);
                    }

                    #endregion

                    #region Kümülatif cuma ve cumartesi en az

                    var kpKumulatifToplamEnAzCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnAzCumaVeCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K26");
                    kpKumulatifToplamEnAzCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumaVeCumartesi);

                    #endregion

                    #region Kümülatif cumartesi en az

                    var kpKumulatifToplamEnAzCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumartesi.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnAzCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K30");
                    kpKumulatifToplamEnAzCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumartesi);

                    #endregion

                    #region Kümülatif pazar en az

                    var kpKumulatifToplamEnAzPazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzPazar.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnAzPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K56");
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
                        kpKumulatifToplamEnAzHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K17");
                        kpKumulatifToplamEnAzHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;

                        KumulatifToplamEnFazla(kpKumulatifToplamEnAzHaftaIci);
                    }

                    #endregion

                    #region Kümülatif bayram en az

                    var kpKumulatifToplamEnAzBayram = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzBayram.Tarihler = bayramlar;
                    kpKumulatifToplamEnAzBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K6");
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
            }

            #region Alt gruplarla eşit sayıda nöbet tutulsun - eski

            var indisAltGrup = 0;

            var ayniGunNobetTutmasiTakipEdilecekGruplar = new List<int>
                {
                    42,//çarşı,
                    53 //Ssk
                };

            var acKapa = true;

            if (acKapa
                && !NobetUstGrupKisit(kisitlarAktif, "K29").PasifMi
                )
            {
                foreach (var ayniGunNobetTutmasiTakipEdilecekGrup in ayniGunNobetTutmasiTakipEdilecekGruplar)
                {
                    if (!ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(ayniGunNobetTutmasiTakipEdilecekGrup))
                    {
                        continue;
                    }

                    var ilgiliEczaneler = data.EczaneNobetGruplar
                        .Where(w => w.NobetGrupGorevTipId == ayniGunNobetTutmasiTakipEdilecekGrup)
                        .Select(s => new
                        {
                            s.EczaneId,
                            s.EczaneAdi,
                            s.Id,
                            s.NobetGrupId,
                            s.NobetGrupAdi
                        }).ToList();

                    var altGruplarlaAyniGunNobetTutmaStd = NobetUstGrupKisit(kisitlarAktif, "K29").SagTarafDegeri;

                    var altGrubuOlanNobetGruplar = new List<int>(){
                                37//Ssk
                            };

                    if (ayniGunNobetTutmasiTakipEdilecekGrup == 53)
                    {
                        altGrubuOlanNobetGruplar = new List<int> {
                                54//Gelişim
                            };
                    }

                    var altGruplarIlgili = new List<int>(){
                                37//Ssk - acil
                            };

                    if (ayniGunNobetTutmasiTakipEdilecekGrup == 53)
                    {
                        altGruplarIlgili = new List<int> {
                                43//Gelişim - Palmiye (uzak bölge)
                            };
                    }

                    var nobetAltGruplar = data.EczaneNobetGrupAltGruplar
                           .Where(w => //w.NobetAltGrupId == 37
                            altGruplarIlgili.Contains(w.NobetAltGrupId)
                           )
                           .Select(s => s.NobetAltGrupId).Distinct()
                           .OrderBy(o => o).ToList();

                    foreach (var eczaneNobetGrup in ilgiliEczaneler)
                    {
                        var kontrolEdilecekEczaneler = new string[] { "NİLGÜN ÖNCEL" };

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                        {
                        }

                        var bakilanEczaneninSonuclari = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.Tarih >= data.NobetUstGrupBaslangicTarihi
                                     ).ToList();

                        var bakilanEczaneninToplamNobetSayisi = bakilanEczaneninSonuclari.Count();

                        var bolum = Math.Round((double)bakilanEczaneninToplamNobetSayisi / nobetAltGruplar.Count(), 0);

                        var altGrupIleTutulacakNobetSayisiUstLimiti = bolum + 1;

                        if (altGruplarlaAyniGunNobetTutmaStd > 0)
                        {
                            altGrupIleTutulacakNobetSayisiUstLimiti = altGruplarlaAyniGunNobetTutmaStd;
                        }
                        else
                        {
                            altGrupIleTutulacakNobetSayisiUstLimiti = 2;
                        }
                        //altGrupIleTutulacakNobetSayisiUstLimiti += altGruplarlaAyniGunNobetTutmaStd;

                        var sskdakiEczaneninSonuclari = data.EczaneGrupNobetSonuclarTumu
                            .Where(w => altGrubuOlanNobetGruplar.Contains(w.NobetGrupGorevTipId)
                                     && altGruplarIlgili.Contains(w.NobetAltGrupId)
                                     && w.Tarih >= data.NobetUstGrupBaslangicTarihi).ToList();

                        var altGrupluEczaneler = data.EczaneNobetGrupAltGruplar;

                        var ayniGunTutulanNobetler = (from g1 in bakilanEczaneninSonuclari
                                                      from g2 in sskdakiEczaneninSonuclari
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
                                                          NobetGrupIdAltGruplu = g2.NobetGrupId
                                                      }).ToList();

                        if (bakilanEczaneninToplamNobetSayisi < 2)
                        {
                            //bakilanEczaneninToplamNobetSayisi = 1;
                        }

                        //var l1eczaneSayisi = data.EczaneNobetTarihAralik.Select(s => s.EczaneNobetGrupId).Distinct().OrderBy(o => o).ToList();
                        //var l2eczaneSayisi = data.EczaneNobetAltGrupTarihAralik.Select(s => s.EczaneNobetGrupId).Distinct().OrderBy(o => o).ToList();

                        #region v1

                        foreach (var altGrup in nobetAltGruplar)
                        {
                            var birlikteNobetTutmayacakEczaneler = new List<int> { eczaneNobetGrup.Id };

                            var birlikteNobetTutulmayacakAltGruptakiEczaneler = altGrupluEczaneler.Where(w => w.NobetAltGrupId == altGrup).ToList();

                            birlikteNobetTutulmayacakAltGruptakiEczaneler.ForEach(x => birlikteNobetTutmayacakEczaneler.Add(x.EczaneNobetGrupId));

                            var tarihler2 = data.TarihAraligi.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                            foreach (var g in tarihler2)
                            {
                                var kararDegiskeniNobetciler = data.EczaneNobetTarihAralik
                                                   .Where(e => birlikteNobetTutmayacakEczaneler.Contains(e.EczaneNobetGrupId)
                                                            && e.TakvimId == g.TakvimId)
                                                   .Select(m => new
                                                   {
                                                       kd = _x[m],
                                                       m.Tarih,
                                                       m.TakvimId,
                                                       m.EczaneAdi,
                                                       m.NobetGrupAdi
                                                   }).ToList();

                                var kararDegiskeniAltGruplaIliskiliEczaneler = data.EczaneNobetAltGrupTarihAralik
                                                                                           .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                                                    && e.NobetGrupAltId == altGrup
                                                                                                    && e.TakvimId == g.TakvimId).FirstOrDefault();

                                #region bakılan eczane ve ilgili alt grup aynı gün nöbetçi olursa

                                model.AddConstraint(
                                     Expression.Sum(kararDegiskeniNobetciler.Select(s => s.kd)) >= 2 - 2 * (1 - _y[kararDegiskeniAltGruplaIliskiliEczaneler]),
                                                     $"{indisAltGrup} alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi, {ayniGunNobetTutmasiTakipEdilecekGrup}, {altGrup}, {eczaneNobetGrup.Id}, {g.Tarih.ToShortDateString()} >=");


                                model.AddConstraint(
                                     Expression.Sum(kararDegiskeniNobetciler.Select(s => s.kd)) <= 1 + 2 * _y[kararDegiskeniAltGruplaIliskiliEczaneler],
                                                     $"{indisAltGrup} alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi, {ayniGunNobetTutmasiTakipEdilecekGrup}, {altGrup}, {eczaneNobetGrup.Id}, {g.Tarih.ToShortDateString()} <=");
                                #endregion

                                indisAltGrup++;
                            }

                            var kararDegiskeniAltGruplaIliskiliEczanelerLimit = data.EczaneNobetAltGrupTarihAralik
                                                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                                        && e.NobetGrupAltId == altGrup)
                                                                               .Select(m => new
                                                                               {
                                                                                   kd2 = _y[m],
                                                                                   m.TakvimId,
                                                                                   m.Tarih,
                                                                                   m.NobetGrupAdi,
                                                                                   m.EczaneAdi,
                                                                                   m.NobetAltGrupAdi
                                                                               }).ToList();

                            var altGruplabirlikteTutulanNobetler = ayniGunTutulanNobetler.Where(w => w.NobetAltGrupId == altGrup).ToList();

                            var altGruplabirlikteBirlikteNobetSayisi = altGruplabirlikteTutulanNobetler.Count();

                            if (altGrupIleTutulacakNobetSayisiUstLimiti - altGruplabirlikteBirlikteNobetSayisi < 2)
                            {
                                //altGrupIleTutulacakNobetSayisiUstLimiti = 2;
                            }

                            var kararDegiskeniNobetSayisi = data.EczaneNobetTarihAralik
                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id)
                                   .Select(m => new
                                   {
                                       kd = _x[m],
                                       m.Tarih,
                                       m.TakvimId,
                                       m.EczaneAdi,
                                       m.NobetGrupAdi
                                   }).ToList();

                            var ayIcındekiAltGruplaBirlikteNobetSayisi = 1;

                            if (data.CalismaSayisi == 2)
                            {
                                ayIcındekiAltGruplaBirlikteNobetSayisi++;
                            }
                            else if (data.CalismaSayisi == 3)
                            {
                                ayIcındekiAltGruplaBirlikteNobetSayisi = ayIcındekiAltGruplaBirlikteNobetSayisi + 2;
                            }

                            var ayIciSinir = true;

                            if (ayIciSinir)
                            {
                                model.AddConstraint(Expression.Sum(kararDegiskeniAltGruplaIliskiliEczanelerLimit.Select(s => s.kd2)) <= 1,//ayIcındekiAltGruplaBirlikteNobetSayisi, //1
                                                 $"{indisAltGrup} alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi2, {ayniGunNobetTutmasiTakipEdilecekGrup}, {eczaneNobetGrup.Id}, {altGrup} - ay içi");
                            }

                            var tumZamanlar = true;

                            if (tumZamanlar)
                            {
                                //if (nobetGrup.Id == 22)                                
                                //    altGrupIleTutulacakNobetSayisiUstLimiti = 2;                                

                                model.AddConstraint(Expression.Sum(kararDegiskeniAltGruplaIliskiliEczanelerLimit.Select(s => s.kd2)) + altGruplabirlikteBirlikteNobetSayisi <= altGrupIleTutulacakNobetSayisiUstLimiti, //1
                                                   $"{indisAltGrup} alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi-2, {ayniGunNobetTutmasiTakipEdilecekGrup}, {eczaneNobetGrup.Id}, {altGrup} - kümülatif");

                                //model.AddConstraint(Expression.Sum(kararDegiskeniAltGruplaIliskiliEczanelerLimit.Select(s => s.kd2)) + altGruplabirlikteBirlikteNobetSayisi <=
                                //                                                            Math.Ceiling((herAyEnFazlaGorevSayisi + bakilanEczaneninToplamNobetSayisi) / 3) + altGruplarlaAyniGunNobetTutmaStd, //1
                                //                  $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi2, {eczaneNobetGrup.Id}");
                            }

                            //model.AddConstraint(
                            //       Expression.Sum(data.EczaneNobetTarihAralik
                            //                        .Where(e => birlikteNobetTutmayacakEczaneler.Contains(e.EczaneNobetGrupId))
                            //                        .Select(m => x[m])) <= altGrupIleTutulacakNobetSayisiUstLimiti,
                            //                        $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi, {eczaneNobetGrup.Id}");
                            indisAltGrup++;
                        }

                        #endregion

                        var siraliCoz = false;

                        #region alt gruplarla sıralı

                        if (ayniGunTutulanNobetler.Count > 0 && siraliCoz)
                        {
                            var sonNobetTarih1 = ayniGunTutulanNobetler.Max(m => m.Tarih);
                            var birlikteNobetTutulanNobetAltGrupId = (from g1 in ayniGunTutulanNobetler
                                                                      where g1.Tarih >= sonNobetTarih1
                                                                      select g1.NobetAltGrupId).ToList();
                            if (ayniGunTutulanNobetler.Count > 1)
                            {
                                var sonNobetTarih2 = ayniGunTutulanNobetler.Where(w => w.Tarih < sonNobetTarih1).Max(m => m.Tarih);

                                birlikteNobetTutulanNobetAltGrupId = (from g1 in ayniGunTutulanNobetler
                                                                      where g1.Tarih >= sonNobetTarih2
                                                                      select g1.NobetAltGrupId).ToList();
                            }

                            //daha önce aynı gün nöbet tuttuğu eczanelerin altgrupları listede sıfır yapılır
                            foreach (var nobetAltGrupId in birlikteNobetTutulanNobetAltGrupId)
                            {
                                var kontrol = false;
                                if (kontrol)
                                {
                                    kontrolEdilecekEczaneler = new string[] { "GÖNÜLCE" };

                                    if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                                    {
                                    }
                                }

                                var birlikteNobetTutulmayacakAltGruptakiEczaneler = altGrupluEczaneler.Where(w => w.NobetAltGrupId == nobetAltGrupId).ToList();

                                //bakılan eczane
                                var birlikteNobetTutmayacakEczaneler = new List<int> { eczaneNobetGrup.Id };

                                //alt gruptaki eczaneler
                                birlikteNobetTutulmayacakAltGruptakiEczaneler.ForEach(x => birlikteNobetTutmayacakEczaneler.Add(x.EczaneNobetGrupId));

                                foreach (var g in data.TarihAraligi)
                                {
                                    model.AddConstraint(
                                      Expression.Sum(data.EczaneNobetTarihAralik
                                                       .Where(e => birlikteNobetTutmayacakEczaneler.Contains(e.EczaneNobetGrupId)
                                                                && e.TakvimId == g.TakvimId)
                                                       .Select(m => _x[m])) <= 1,
                                                       $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi, {eczaneNobetGrup.Id}");
                                }
                            }

                            //var birlikteNobetSayisi = ayniGunTutulanNobetler.Count();

                            //model.AddConstraint(
                            //      Expression.Sum(data.EczaneNobetTarihAralik
                            //                       .Where(e => e.EczaneId == eczaneId)
                            //                       .Select(m => x[m])) + birlikteNobetSayisi <= altGruplarlaAyniGunNobetTutmaStd,
                            //                       $"ay icinde diger gruptaki eczaneler ile en fazla bir kez nobet tutulsun, {eczaneId}");
                            //}
                        }
                        #endregion
                    }
                }
            }
            #endregion

            var kpIstenenEczanelerinNobetGunleriniKisitla = new KpIstenenEczanelerinNobetGunleriniKisitla
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetTutamazGunler = eczaneNobetTutamazGunler,
                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K46"),
                KararDegiskeni = _x
            };
            IstenenEczanelerinNobetGunleriniKisitla(kpIstenenEczanelerinNobetGunleriniKisitla);

            #region eczane gruplar - aynı gün nöbet

            var tarihAraligi = data.TarihAraligi;

            var esGrubaAyniGunNobetYazmaEczaneGruplar = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K11"),
                EczaneGruplar = data.EczaneGruplar,
                Tarihler = tarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaEczaneGruplar);

            var esGrubaAyniGunNobetYazmaCarsiAcil = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = acilEczaneler,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu.Where(w => w.NobetGrupGorevTipId == 42 || w.NobetAltGrupId == 37).ToList(),
                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K19"),
                EczaneGruplar = data.AltGruplarlaAyniGunNobetTutmayacakEczanelerCarsi,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaCarsiAcil);

            var esGrubaAyniGunNobetYazmaSskPalmiye = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = palmiyeEczaneler,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu.Where(w => w.NobetGrupGorevTipId == 53 || w.NobetAltGrupId == 43).ToList(),
                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K19"),
                EczaneGruplar = data.AltGruplarlaAyniGunNobetTutmayacakEczanelerSsk,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaSskPalmiye);

            var esGrubaAyniGunNobetYazmaSskYeniDevlet = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = yeniDevletEczaneler,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu.Where(w => w.NobetGrupGorevTipId == 53 || w.NobetAltGrupId == 44).ToList(),
                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K19"),
                EczaneGruplar = data.AltGruplarlaAyniGunNobetTutmayacakEczanelerYeniDevlet,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaSskYeniDevlet);

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

            #region Tarih aralığı içinde aynı gün nöbet

            var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.NobetGrupGorevTipId != 54).ToList(),
                IkiliEczaneler = data.IkiliEczaneler.Where(w => w.NobetGrupId2 != 54).ToList(),
                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K10"),
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            AyIcindeSadece1KezAyniGunNobetTutulsun(ayIcindeSadece1KezAyniGunNobetKisit);

            //var kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu = new KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu
            //{
            //    Model = model,
            //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
            //    EczaneNobetTarihAralikIkiliEczaneler = data.EczaneNobetTarihAralikIkiliEczaneler,
            //    IkiliEczaneler = data.IkiliEczaneler,
            //    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K10"),
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
                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K41"),
                EczaneGruplar = data.OncekiAylardaAyniGunNobetTutanEczaneler,
                Tarihler = tarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaOncekiAylar);

            #endregion

            #region istek ve mazeret

            var istegiKarsilaKisit = new KpIstegiKarsila
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K12"),
                EczaneNobetIstekler = data.EczaneNobetIstekler,
                KararDegiskeni = _x
            };
            IstegiKarsila(istegiKarsilaKisit);

            var mazereteGorevYazmaKisit = new KpMazereteGorevYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K13"),
                EczaneNobetMazeretler = data.EczaneNobetMazeretler,
                KararDegiskeni = _x
            };
            MazereteGorevYazma(mazereteGorevYazmaKisit);

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
                        if (data.CalismaSayisi == calismaSayisiEnFazla)
                        {
                            results.Celiskiler = CeliskileriEkle(solution);
                        }

                        throw new Exception($"Uygun çözüm bulunamadı!.");
                    }
                    else
                    {
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

                        var sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value.IsAlmost(1) == true).ToList();

                        var nobetGorevTipler = data.NobetGrupGorevTipler
                            .Select(s => new
                            {
                                s.NobetGorevTipId,
                                s.NobetGorevTipAdi
                            }).Distinct().ToList();

                        //var nobetGorevTipId = 1;

                        var nobetGrupTarihler1 = data.EczaneNobetTarihAralik
                            .Where(w => nobetGorevTipler.Select(s => s.NobetGorevTipId).Contains(w.NobetGorevTipId))
                            .Select(s => new
                            {
                                s.NobetGrupId,
                                s.Tarih,
                                s.NobetGorevTipId,
                                Talep = s.TalepEdilenNobetciSayisi
                                //data.NobetGrupTalepler
                                // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                //         && w.TakvimId == s.TakvimId).SingleOrDefault() == null
                                //? (int)data.NobetGrupKurallar
                                //    .Where(k => k.NobetKuralId == 3
                                //             && k.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                //    .Select(k => k.Deger).SingleOrDefault()
                                //: data.NobetGrupTalepler
                                // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                //         && w.TakvimId == s.TakvimId).SingleOrDefault().NobetciSayisi
                            }).Distinct().ToList();

                        //nobetGorevTipId = 2;

                        //var nobetGrupTarihler2 = data.EczaneNobetTarihAralik
                        //    .Where(w => w.NobetGorevTipId == nobetGorevTipId)
                        //    .Select(s => new
                        //    {
                        //        s.NobetGrupId,
                        //        s.Tarih,
                        //        s.NobetGorevTipId,
                        //        Talep = s.TalepEdilenNobetciSayisi
                        //        //data.NobetGrupTalepler
                        //        // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                        //        //         && w.TakvimId == s.TakvimId).SingleOrDefault() == null
                        //        //? (int)data.NobetGrupKurallar
                        //        //    .Where(k => k.NobetKuralId == 3
                        //        //             && k.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                        //        //    .Select(k => k.Deger).SingleOrDefault()
                        //        //: data.NobetGrupTalepler
                        //        // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                        //        //         && w.TakvimId == s.TakvimId).SingleOrDefault().NobetciSayisi
                        //    }).Distinct().ToList();

                        var toplamArz = sonuclar.Count;
                        var toplamTalep = nobetGrupTarihler1.Sum(s => s.Talep);// + nobetGrupTarihler2.Sum(s => s.Talep);

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

                    string cozulenNobetGruplar = null;

                    var ilkGrup = data.NobetGruplar.Select(s => s.Adi).FirstOrDefault();

                    foreach (var i in data.NobetGruplar.Select(s => s.Adi))
                    {
                        if (i == ilkGrup)
                        {
                            cozulenNobetGruplar += i;
                        }
                        else
                        {
                            cozulenNobetGruplar += $", {i}";
                        }
                    }

                    //$"<strong><span class='text-danger'>Çözüm bulunamadı.</strong> "
                    //      + "<br/> "
                    //      +

                    var celiskiler = results.Celiskiler.Split('*');

                    mesaj = "Aşağıdaki açıklamalara göre <a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">nöbet ayarlarında</a> bazı değişiklikler yaparak <strong>tekrar çözmelisiniz..</strong>"
                          + "<hr /> "
                          + $"<strong>Kontrol edilecek kurallar <span class='badge badge-warning'>{celiskiler[1]}</span></strong>"
                          + "<br /> "
                          + celiskiler[0];

                    mesaj += $"<strong>Nöbet seçenekleri:</strong>" +
                        $"<br />Nöbet Grupları: {cozulenNobetGruplar}" +
                        $"<br />Tarih Aralığı: <strong>{data.BaslangicTarihi.ToShortDateString()}-{data.BitisTarihi.ToShortDateString()}</strong>" +
                        $"<hr/> " +
                        $"<strong>Çalışma adımları <span class='badge badge-info'>{data.CalismaSayisi}</span></strong>" +
                        $"{iterasyonMesaj} "
                        ;

                    var calismaAdimlari = new string[10]
                        {
                            "Çözüm bulunamadı.",
                            //"Tekrar çözüm denendi.",
                            "kumulatifOrtalamaGunKuralSayisi 1 artırıldı.",
                            "haftaIciOrtalama (satır ortalaması) 1 artırıldı.",
                            "haftaIciOrtalama (satır ortalaması) 2 artırıldı.",
                            "Ayda en fazla 1 gorev kaldırıldı!",
                            "Farklı ay peşpeşe görev gevşetildi!",
                            "Ayda en fazla 1 gorev kaldırıldı ve Farklı Ay Peşpeşe Görev gevşetildi!",
                            "Cuma ve cumartesi en fazla 3 olmadı 4 olarak gevşetildi!",
                            "Farklı ay peşpeşe görev sayısı en çok 5 olarak gevşetildi!",
                            "default"
                        };

                    for (int i = 0; i < data.CalismaSayisi; i++)
                    {
                        mesaj += "<br /> " + i + " " + calismaAdimlari[i];
                    }

                    throw new Exception(mesaj);
                }
                else
                {
                    throw ex;
                }
            }
            return results;
        }

    }

}
