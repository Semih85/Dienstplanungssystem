﻿using OPTANO.Modeling.Common;
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
    public class GiresunOptano : EczaneNobetKisit, IEczaneNobetGiresunOptimization
    {
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }

        private Model Model(GiresunDataModel data)
        {
            var model = new Model() { Name = "Giresun Eczane Nöbet" };

            #region Veriler

            #region kısıtlar

            var eczaneGrup = NobetUstGrupKisit(data.Kisitlar, "eczaneGrup", data.NobetUstGrupId);

            var pespeseHaftaIciAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "pespeseHaftaIciAyniGunNobet", data.NobetUstGrupId);
            var nobetBorcOdeme = NobetUstGrupKisit(data.Kisitlar, "nobetBorcOdeme", data.NobetUstGrupId);

            var ayIcindeAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "ayIcindeAyniGunNobet", data.NobetUstGrupId);
            var altGruplarlaAyniGunNobetTutma = NobetUstGrupKisit(data.Kisitlar, "altGruplarlaAyniGunNobetTutma", data.NobetUstGrupId);
            var oncekiAylarAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "oncekiAylarAyniGunNobet", data.NobetUstGrupId);

            var eczanelerinNobetGunleriniKisitla = NobetUstGrupKisit(data.Kisitlar, "eczanelerinNobetGunleriniKisitla", data.NobetUstGrupId);
            var birEczaneyeAyniGunSadece1GorevTipYaz = NobetUstGrupKisit(data.Kisitlar, "birEczaneyeAyniGunSadece1GorevTipYaz", data.NobetUstGrupId);
            var cumartesiGorevTiplerineGoreNobetleriDagit = NobetUstGrupKisit(data.Kisitlar, "cumartesiGorevTiplerineGoreNobetleriDagit", data.NobetUstGrupId);

            #endregion

            var eczaneNobetTutamazGunler = new List<EczaneNobetTutamazGun>
            {
                //istikamet (cuma, cts, pazar tutabilir)
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 920, NobetGunKuralId = 2 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 920, NobetGunKuralId = 3 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 920, NobetGunKuralId = 4 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 920, NobetGunKuralId = 5 },

                 //elif (cts ve pazar tutabilir)
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1024, NobetGunKuralId = 2 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1024, NobetGunKuralId = 3 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1024, NobetGunKuralId = 4 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1024, NobetGunKuralId = 5 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1024, NobetGunKuralId = 6 },

                 //aydınlar (cts tutabilir)
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1022, NobetGunKuralId = 1 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1022, NobetGunKuralId = 2 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1022, NobetGunKuralId = 3 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1022, NobetGunKuralId = 4 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1022, NobetGunKuralId = 5 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1022, NobetGunKuralId = 6 },

                 //DERYA (cts tutabilir)
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1023, NobetGunKuralId = 1 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1023, NobetGunKuralId = 2 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1023, NobetGunKuralId = 3 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1023, NobetGunKuralId = 4 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1023, NobetGunKuralId = 5 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1023, NobetGunKuralId = 6 },

                 //KARABIÇAK (cts tutabilir)
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1025, NobetGunKuralId = 1 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1025, NobetGunKuralId = 2 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1025, NobetGunKuralId = 3 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1025, NobetGunKuralId = 4 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1025, NobetGunKuralId = 5 },
                 new EczaneNobetTutamazGun{ EczaneNobetGrupId = 1025, NobetGunKuralId = 6 },
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

            var eczaneNobetTarihAralikTumGorevTipleri = data.EczaneNobetTarihAralik.Where(w => w.NobetGunKuralId >= 7).ToList();

            #region Gece nöbetçileri

            var nobetGorevTipId = 1;
            var nobetGrupGorevTip = data.NobetGrupGorevTipler.Where(w => w.NobetGorevTipId == nobetGorevTipId).SingleOrDefault();

            if (nobetGrupGorevTip != null)
            {
                #region kısıtlar grup bazlı

                var kisitlarAktif = new List<NobetUstGrupKisitDetay>();

                //üst grup kısıtlar olduğu gibi aktif listeye aktarıldı. grup bazlı değişen olursa aktiften değişecek.
                data.Kisitlar.ForEach(x => kisitlarAktif.Add((NobetUstGrupKisitDetay)x.Clone()));

                var kisitlarGrupBazli = data.NobetGrupGorevTipKisitlar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                if (kisitlarGrupBazli.Count > 0)
                {
                    foreach (var grupBazliKisit in kisitlarGrupBazli)
                    {
                        var kisitGrupBazli = kisitlarAktif.SingleOrDefault(w => w.KisitId == grupBazliKisit.KisitId);

                        kisitGrupBazli.PasifMi = grupBazliKisit.PasifMi;
                        kisitGrupBazli.SagTarafDegeri = grupBazliKisit.SagTarafDegeri;
                    }
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

                #region hafta Ici Pespese Gorev EnAz

                var farkliAyPespeseGorevAraligi = (gruptakiEczaneSayisi / gunlukNobetciSayisi * 1.2);
                var altLimit = farkliAyPespeseGorevAraligi / gunlukNobetciSayisi * 0.7666; //0.95
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
                        PespeseNobetSayisiAltLimit = gruptakiEczaneSayisi * 0.6, //altLimit,
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

                    var tarihAraligiOrtalamaEnFazlaBase = new KpTarihAraligiOrtalamaEnFazla
                    {
                        Model = model,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };

                    var tarihAraligiOrtalamaEnFazlaTumTarihAraligiTumGorevTipler = (KpTarihAraligiOrtalamaEnFazla)tarihAraligiOrtalamaEnFazlaBase.Clone();

                    tarihAraligiOrtalamaEnFazlaTumTarihAraligiTumGorevTipler.Tarihler = tarihler;
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligiTumGorevTipler.GunSayisi = gunSayisi;
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligiTumGorevTipler.OrtalamaNobetSayisi = ortalamaNobetSayisiTumGorevTipleri;//ortalamaNobetSayisi == 1 ? 2 : ortalamaNobetSayisi,
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligiTumGorevTipler.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler;
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligiTumGorevTipler.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K19");
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligiTumGorevTipler.GunKuralAdi = "Tüm görev tipler ve tarihler";

                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaTumTarihAraligiTumGorevTipler);

                    var tarihAraligiOrtalamaEnFazlaCumartesiTumGorevTipler = (KpTarihAraligiOrtalamaEnFazla)tarihAraligiOrtalamaEnFazlaBase.Clone();

                    tarihAraligiOrtalamaEnFazlaCumartesiTumGorevTipler.Tarihler = cumartesiGunleri;
                    tarihAraligiOrtalamaEnFazlaCumartesiTumGorevTipler.GunSayisi = cumartesiSayisi;
                    tarihAraligiOrtalamaEnFazlaCumartesiTumGorevTipler.OrtalamaNobetSayisi = ortalamaNobetSayisiCumartesiTumGorevTipleri;
                    tarihAraligiOrtalamaEnFazlaCumartesiTumGorevTipler.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler;
                    tarihAraligiOrtalamaEnFazlaCumartesiTumGorevTipler.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K38");
                    tarihAraligiOrtalamaEnFazlaCumartesiTumGorevTipler.GunKuralAdi = "Tüm görev tipler ve cumartesi";

                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaCumartesiTumGorevTipler);

                    var tarihAraligiOrtalamaEnFazlaTumTarihAraligi = (KpTarihAraligiOrtalamaEnFazla)tarihAraligiOrtalamaEnFazlaBase.Clone();

                    tarihAraligiOrtalamaEnFazlaTumTarihAraligi.Tarihler = tarihler;
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligi.GunSayisi = gunSayisi;
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligi.OrtalamaNobetSayisi = ortalamaNobetSayisi;
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligi.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli;
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K19");
                    tarihAraligiOrtalamaEnFazlaTumTarihAraligi.GunKuralAdi = "Gece nöbetçileri ve tüm tarihler";

                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaTumTarihAraligi);

                    var tarihAraligiOrtalamaEnFazlaHaftaIici = (KpTarihAraligiOrtalamaEnFazla)tarihAraligiOrtalamaEnFazlaBase.Clone();

                    tarihAraligiOrtalamaEnFazlaHaftaIici.Tarihler = haftaIciGunleri;
                    tarihAraligiOrtalamaEnFazlaHaftaIici.GunSayisi = haftaIciSayisi;
                    tarihAraligiOrtalamaEnFazlaHaftaIici.OrtalamaNobetSayisi = haftaIciOrtamalaNobetSayisi;
                    tarihAraligiOrtalamaEnFazlaHaftaIici.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli;
                    tarihAraligiOrtalamaEnFazlaHaftaIici.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K32");
                    tarihAraligiOrtalamaEnFazlaHaftaIici.GunKuralAdi = "Gece nöbetçileri ve hafta içi";

                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaHaftaIici);

                    var tarihAraligiOrtalamaEnFazlaBayram = (KpTarihAraligiOrtalamaEnFazla)tarihAraligiOrtalamaEnFazlaBase.Clone();

                    tarihAraligiOrtalamaEnFazlaBayram.Tarihler = bayramlar;
                    tarihAraligiOrtalamaEnFazlaBayram.GunSayisi = bayramSayisi;
                    tarihAraligiOrtalamaEnFazlaBayram.OrtalamaNobetSayisi = bayramOrtalamaNobetSayisi;
                    tarihAraligiOrtalamaEnFazlaBayram.EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli;
                    tarihAraligiOrtalamaEnFazlaBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K14");
                    tarihAraligiOrtalamaEnFazlaBayram.GunKuralAdi = "Gece nöbetçileri ve bayram";

                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaBayram);

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
                            && !eczanelerinNobetGunleriniKisitla.PasifMi
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

                        if (NobetUstGrupKisit(kisitlarAktif, "K34").SagTarafDegeri > 0 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                            kumulatifOrtalamaGunKuralNobetSayisi = NobetUstGrupKisit(kisitlarAktif, "K34").SagTarafDegeri;

                        if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                        //NobetUstGrupKisit(kisitlarAktif, "K44").SagTarafDegeri = 3
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

                    var istisnaEczaneler = new string[] {
                        "AYDINLAR",
                        "DERYA",
                        "ELİF",
                        "KARABIÇAK"
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

                    var pazarPespeseGorevEnFazla = data.Kisitlar
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

                #region eczane gruplar - aynı gün nöbet

                var tarihAraligi = data.TarihAraligi
                    .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var esGrubaAyniGunNobetYazmaEczaneGruplar = new KpEsGrubaAyniGunNobetYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    EczaneNobetSonuclar = eczaneNobetSonuclarGorevTipBazli,
                    NobetUstGrupKisit = eczaneGrup,
                    EczaneGruplar = data.EczaneGruplar.Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList(),
                    Tarihler = tarihAraligi,
                    KararDegiskeni = _x
                };
                EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaEczaneGruplar);

                var esGrubaAyniGunNobetYazmaOncekiAylar = new KpEsGrubaAyniGunNobetYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    EczaneNobetSonuclar = eczaneNobetSonuclarGorevTipBazli,
                    NobetUstGrupKisit = oncekiAylarAyniGunNobet,
                    EczaneGruplar = data.OncekiAylardaAyniGunNobetTutanEczaneler,
                    Tarihler = tarihAraligi,
                    KararDegiskeni = _x
                };
                EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaOncekiAylar);

                #endregion

                #region alt gruplar - aynı gün nöbet

                var eczaneNobetTarihAralikAtlGruplu = eczaneNobetTarihAralikGrupBazli;

                var kpEsGrubaAyniGunNobetYazma = new KpEsGrubaAyniGunNobetYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikAtlGruplu,
                    NobetUstGrupKisit = altGruplarlaAyniGunNobetTutma,
                    KararDegiskeni = _x
                };

                foreach (var gunGrup in gunGruplar)
                {
                    #region ş.dışı

                    kpEsGrubaAyniGunNobetYazma.Tarihler = tarihAraligi.Where(w => w.GunGrupId == gunGrup.GunGrupId).ToList();
                    kpEsGrubaAyniGunNobetYazma.EczaneNobetSonuclar = GetSonuclarByGunGrup(data.EczaneNobetSonuclarAltGruplarlaBirlikte, gunGrup.GunGrupId);

                    var kpEsGrubaAyniGunNobetYazmaSehirDisi = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

                    kpEsGrubaAyniGunNobetYazmaSehirDisi.EczaneGruplar = GetEczaneGruplarByEczaneGrupTanimTipId(data.AltGruplarlaAyniGunNobetTutmayacakEczanelerSehirDisi, gunGrup.GunGrupId);

                    EsGruptakiEczanelereAyniGunNobetYazma(kpEsGrubaAyniGunNobetYazmaSehirDisi);

                    #endregion

                    #region ş.içi - 1

                    var kpEsGrubaAyniGunNobetYazmaSehirIci1 = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

                    kpEsGrubaAyniGunNobetYazmaSehirIci1.EczaneGruplar = GetEczaneGruplarByEczaneGrupTanimTipId(data.AltGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci1, gunGrup.GunGrupId);

                    EsGruptakiEczanelereAyniGunNobetYazma(kpEsGrubaAyniGunNobetYazmaSehirIci1);

                    #endregion

                    #region ş.içi - 2

                    var kpEsGrubaAyniGunNobetYazmaSehirIci2 = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

                    kpEsGrubaAyniGunNobetYazmaSehirIci2.EczaneGruplar = GetEczaneGruplarByEczaneGrupTanimTipId(data.AltGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci2, gunGrup.GunGrupId);

                    EsGruptakiEczanelereAyniGunNobetYazma(kpEsGrubaAyniGunNobetYazmaSehirIci2);

                    #endregion
                }

                #endregion

                #region istek ve mazeret

                var istegiKarsilaKisit = new KpIstegiKarsila
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K12"),
                    EczaneNobetIstekler = data.EczaneNobetIstekler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                IstegiKarsila(istegiKarsilaKisit);

                var mazereteGorevYazmaKisit = new KpMazereteGorevYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K13"),
                    EczaneNobetMazeretler = data.EczaneNobetMazeretler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                MazereteGorevYazma(mazereteGorevYazmaKisit);

                #endregion

                var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobetGiresunAltGrup
                {
                    Model = model,
                    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                    EczaneNobetGrupAltGrupDetaylar = data.EczaneNobetGrupAltGruplar.Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList(),
                    NobetUstGrupKisit = ayIcindeAyniGunNobet,
                    Tarihler = tarihAraligi,
                    KararDegiskeni = _x
                };
                AyIcindeSadece1KezAyniGunNobetTutulsunGiresunAltGrup(ayIcindeSadece1KezAyniGunNobetKisit);

                var kpIstenenEczanelerinNobetGunleriniKisitla = new KpIstenenEczanelerinNobetGunleriniKisitla
                {
                    Model = model,
                    EczaneNobetTutamazGunler = eczaneNobetTutamazGunler,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = eczanelerinNobetGunleriniKisitla,
                    KararDegiskeni = _x
                };
                IstenenEczanelerinNobetGunleriniKisitla(kpIstenenEczanelerinNobetGunleriniKisitla);
            }
            #endregion

            #region Cumartesi gündüz nöbetçileri

            nobetGorevTipId = 2;
            nobetGrupGorevTip = data.NobetGrupGorevTipler
                .Where(w => w.NobetGorevTipId == nobetGorevTipId).SingleOrDefault();

            if (nobetGrupGorevTip != null)
            {
                #region kısıtlar grup bazlı

                var kisitlarAktif = new List<NobetUstGrupKisitDetay>();

                //üst grup kısıtlar olduğu gibi aktif listeye aktarıldı. grup bazlı değişen olursa aktiften değişecek.
                data.Kisitlar.ForEach(x => kisitlarAktif.Add((NobetUstGrupKisitDetay)x.Clone()));

                var kisitlarGrupBazli = data.NobetGrupGorevTipKisitlar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                if (kisitlarGrupBazli.Count > 0)
                {
                    foreach (var grupBazliKisit in kisitlarGrupBazli)
                    {
                        var kisitGrupBazli = kisitlarAktif.SingleOrDefault(w => w.KisitId == grupBazliKisit.KisitId);

                        kisitGrupBazli.PasifMi = grupBazliKisit.PasifMi;
                        kisitGrupBazli.SagTarafDegeri = grupBazliKisit.SagTarafDegeri;
                    }
                }       

                #endregion

                #region ön hazırlık

                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                int gunlukNobetciSayisi = (int)nobetGrupKurallar
                    .Where(s => s.NobetKuralId == 3)
                    .Select(s => s.Deger).SingleOrDefault();

                var pespeseNobetSayisi = nobetGrupKurallar
                            .Where(s => s.NobetKuralId == 1).SingleOrDefault() != null
                        ? (int)nobetGrupKurallar
                            .Where(s => s.NobetKuralId == 1).SingleOrDefault().Deger
                        : 0;

                //var nobetGrupTalepler = data.NobetGrupTalepler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                #region tarihler

                var tarihler = data.TarihAraligi
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                //TalepleriTakvimeIsle(nobetGrupTalepler, gunlukNobetciSayisi, tarihler);
                var nobetGrupTalepler = tarihler
                    .GroupBy(g => g.TalepEdilenNobetciSayisi)
                    .Select(s => new
                    {
                        TalepEdilenNobetciSayisi = s.Key,
                        GunSayisi = s.Count()
                    }).ToList();

                var pazarGunleri = tarihler.Where(w => w.GunGrupId == 1).OrderBy(o => o.Tarih).ToList();
                var bayramlar = tarihler.Where(w => w.GunGrupId == 2).OrderBy(o => o.Tarih).ToList();
                var haftaIciGunleri = tarihler.Where(w => w.GunGrupId == 3).OrderBy(o => o.Tarih).ToList();
                var cumartesiGunleri = tarihler.Where(w => w.GunGrupId == 4).OrderBy(o => o.Tarih).ToList();

                var gunSayisi = tarihler.Count();
                var haftaIciSayisi = haftaIciGunleri.Count();
                var pazarSayisi = pazarGunleri.Count();
                var cumartesiSayisi = cumartesiGunleri.Count();
                var bayramSayisi = bayramlar.Count();

                #endregion

                var r = new Random();

                var eczaneNobetGruplar = data.EczaneNobetGruplar
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId)
                    .OrderBy(x => r.NextDouble())
                    .ToList();

                var gruptakiEczaneSayisi = eczaneNobetGruplar.Count;

                var nobetGunKurallar = tarihler.Select(s => s.NobetGunKuralId).Distinct().ToList();

                var bayramNobetleri = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.GunGrupId == 2 //bayramlar
                             ).OrderBy(o => o.Tarih).ToList();

                var cumartesiNobetleri = data.EczaneNobetSonuclar
                  .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                           && w.GunGrupId == 4 //cumartesiler
                           ).OrderBy(o => o.Tarih).ToList();

                var nobetGrupBayramNobetleri = bayramNobetleri
                    .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                             && w.Tarih >= data.NobetUstGrupBaslangicTarihi).ToList();

                var cumartesiNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / (5 * gunlukNobetciSayisi)) - 1;

                var eczaneNobetTarihAralikGrupBazli = data.EczaneNobetTarihAralik
                   .Where(e => e.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                            && e.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                            ).ToList();

                //var bayramOrtalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, bayramSayisi);
                //var cumartesiOrtamalaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, cumartesiSayisi);
                var cumartesiOrtamalaNobetSayisi = OrtalamaNobetSayisi(cumartesiGunleri.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var bayramOrtalamaNobetSayisi = OrtalamaNobetSayisi(bayramlar.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);

                var eczaneNobetSonuclarGorevTipBazli = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var eczaneNobetGrupGunKuralIstatistikler = data.EczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var eczaneNobetGrupGunKuralIstatistiklerCumartesi = data.EczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var nobetGrupGunKurallar = data.NobetGrupGorevTipGunKurallar
                                                .Where(s => s.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                                         && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                                .Select(s => s.NobetGunKuralId).ToList();

                var ilgiliTarihler = tarihler.Where(w => nobetGrupGunKurallar.Contains(w.NobetGunKuralId)).ToList();

                var nobetGunKuralIstatistikler = data.TakvimNobetGrupGunDegerIstatistikler
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                             && ilgiliTarihler.Select(s => s.NobetGunKuralId).Distinct().Contains(w.NobetGunKuralId)).ToList();

                var nobetGunKuralTarihler = new List<NobetGunKuralTarihAralik>();

                var nobetGunKuralIstatistikler2 = nobetGunKuralIstatistikler.Where(w => w.NobetGunKuralId == 7).ToList();

                foreach (var item in nobetGunKuralIstatistikler2)
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

                #region ortak - görev tiplerinden bağımsız

                var ayniGunSadece1NobetTuru = new KpAyniGunSadece1NobetTuru
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikTumGorevTipleri,
                    EczaneNobetGruplar = data.EczaneNobetGruplar,
                    Tarihler = ilgiliTarihler,//cumartesiGunleri,
                    NobetUstGrupKisit = birEczaneyeAyniGunSadece1GorevTipYaz,
                    KararDegiskeni = _x
                };
                BirEczaneyeAyniGunSadece1GorevYaz(ayniGunSadece1NobetTuru);

                var eczaneGruplarGorevTip2 = data.EczaneGruplar
                    .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                //var gorevTipineGorenDagilimKisit = new KpGorevTipineGorevDagilim
                //{
                //    Model = model,
                //    EczaneNobetTarihAralik = eczaneNobetTarihAralikTumGorevTipleri,
                //    EczaneGruplar = eczaneGruplarGorevTip2,
                //    Tarihler = cumartesiGunleri,
                //    KararDegiskeni = _x,
                //    NobetUstGrupKisit = cumartesiGorevTiplerineGoreNobetleriDagit
                //};
                //NobetGorevTipineGoreDagilimYap(gorevTipineGorenDagilimKisit); 

                #endregion

                #region talep

                var talebiKarsilaKisitParametreModel = new KpTalebiKarsila
                {
                    EczaneNobetTarihAralikTumu = eczaneNobetTarihAralikGrupBazli,
                    NobetGrupGorevTip = nobetGrupGorevTip,
                    Tarihler = ilgiliTarihler,
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
                        var kontrolEdilecekEczaneler = new string[] { "ŞENOL" };

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                        {
                        }
                    }
                    #endregion

                    #region eczane bazlı veriler

                    // karar değişkeni - eczane bazlı filtrelenmiş
                    var eczaneNobetTarihAralikEczaneBazli = eczaneNobetTarihAralikGrupBazli
                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    var eczaneNobetTarihAralikEczaneBazliTumGrorevTipler = eczaneNobetTarihAralikTumGorevTipleri//data.EczaneNobetTarihAralik
                            .Where(e => e.EczaneId == eczaneNobetGrup.EczaneId
                                     //&& e.CumartesiGunuMu == true
                                     ).ToList();

                    var eczaneNobetIstatistikCumartesiTumGorevTipler = eczaneNobetGrupGunKuralIstatistiklerCumartesi
                            .Where(w => w.EczaneId == eczaneNobetGrup.EczaneId).ToList();

                    var eczaneNobetIstatistik = eczaneNobetGrupGunKuralIstatistikler
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    var eczaneNobetSonuclar = eczaneNobetSonuclarGorevTipBazli
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    //var yazilabilecekIlkPazarTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar.AddMonths(pazarNobetiYazilabilecekIlkAy - 1);
                    var yazilabilecekIlkCumartesiTarihi = eczaneNobetIstatistik.SonNobetTarihiCumartesi.AddMonths(cumartesiNobetiYazilabilecekIlkAy); //cumartesiNobetiYazilabilecekIlkAy-1
                    //var yazilabilecekIlkHaftaIciTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci.AddDays(altLimit);
                    var nobetYazilabilecekIlkTarih = eczaneNobetIstatistik.SonNobetTarihi.AddDays(pespeseNobetSayisi);

                    #endregion

                    #region Peş peşe nöbet

                    var kpHerAyPespeseGorev = new KpHerAyPespeseGorev
                    {
                        Model = model,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K1"),
                        Tarihler = ilgiliTarihler,
                        PespeseNobetSayisi = pespeseNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };
                    HerAyPespeseGorev(kpHerAyPespeseGorev);

                    var pesPeseGorevEnAzCumartesi = new KpPesPeseGorevEnAz
                    {
                        Model = model,
                        NobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K37"),
                        Tarihler = cumartesiGunleri,
                        NobetYazilabilecekIlkTarih = yazilabilecekIlkCumartesiTarihi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };
                    PesPeseGorevEnAz(pesPeseGorevEnAzCumartesi);

                    #endregion

                    #region Tarih aralığı ortalama en fazla

                    var tarihAraligiOrtalamaEnFazlaCumartesi = new KpTarihAraligiOrtalamaEnFazla
                    {
                        Model = model,
                        Tarihler = cumartesiGunleri,
                        GunSayisi = cumartesiSayisi,
                        OrtalamaNobetSayisi = cumartesiOrtamalaNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K38"),
                        GunKuralAdi = "Cumartesi_master",
                        KararDegiskeni = _x
                    };
                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaCumartesi);

                    var tarihAraligiOrtalamaEnFazlaBayram = new KpTarihAraligiOrtalamaEnFazla
                    {
                        Model = model,
                        Tarihler = bayramlar,
                        GunSayisi = bayramSayisi,
                        OrtalamaNobetSayisi = bayramOrtalamaNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K14"),
                        GunKuralAdi = "Bayram_master",
                        KararDegiskeni = _x
                    };
                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaBayram);

                    #endregion

                    #region Kümülatif ve her ay en falza kısıtları

                    var nobetGunKuralNobetSayilari = new List<NobetGunKuralNobetSayisi>();

                    foreach (var gunKural in nobetGunKuralIstatistikler2)
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

                    var haftaIciEnCokNobetSayisi = 0;
                    var haftaIciEnAzNobetSayisi = 0;
                    var gruptakiEncokNobetSayisi = 0;

                    if (nobetGunKuralIstatistikler2.Count > 0 && eczaneNobetGrupGunKuralIstatistikler.Count > 0)
                    {
                        haftaIciEnCokNobetSayisi = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi);
                        haftaIciEnAzNobetSayisi = nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                        gruptakiEncokNobetSayisi = eczaneNobetGrupGunKuralIstatistikler.Max(m => m.NobetSayisiToplam);
                    }

                    foreach (var gunKural in nobetGunKuralIstatistikler2)
                    {//gun kural bazlı

                        if (kontrol && gunKural.NobetGunKuralAdi == "Cumartesi")
                        {
                        }

                        var tarihAralik = nobetGunKuralTarihler.Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();

                        if (gunKural.NobetGunKuralId == 7)
                        {
                            var kumulatifCumartesiSayisi = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiCumartesi) / gunlukNobetciSayisi + cumartesiSayisi;
                            var kumulatifOrtalamaCumartesiSayisi = OrtalamaNobetSayisi(5, gruptakiEczaneSayisi, kumulatifCumartesiSayisi);

                            if (data.CalismaSayisi == 1)
                                kumulatifOrtalamaCumartesiSayisi++;

                            if (data.CalismaSayisi == 2)
                                kumulatifOrtalamaCumartesiSayisi += 2;

                            var kumulatifToplamEnFazlaCumartesi = new KpKumulatifToplam
                            {
                                Model = model,
                                Tarihler = cumartesiGunleri,
                                EczaneNobetGrup = eczaneNobetGrup,
                                EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler,
                                NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K34"),
                                KumulatifOrtalamaGunKuralSayisi = kumulatifOrtalamaCumartesiSayisi,
                                ToplamNobetSayisi = eczaneNobetIstatistikCumartesiTumGorevTipler.Sum(s => s.NobetSayisiCumartesi),
                                GunKuralAdi = gunKural.NobetGunKuralAdi,
                                KararDegiskeni = _x
                            };
                            KumulatifToplamEnFazla(kumulatifToplamEnFazlaCumartesi);
                        }

                        //if (data.CalismaSayisi > 1)
                        //    tarihAralik.OrtalamaNobetSayisi++;

                        //gün kural ortalama en fazla
                        var tarihAraligiOrtalamaEnFazlaIlgiliKisit = new KpTarihAraligiOrtalamaEnFazla
                        {
                            Model = model,
                            Tarihler = tarihAralik.TakvimNobetGruplar,
                            GunSayisi = tarihAralik.GunSayisi,
                            OrtalamaNobetSayisi = tarihAralik.OrtalamaNobetSayisi,
                            EczaneNobetGrup = eczaneNobetGrup,
                            EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                            NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K38"),
                            GunKuralAdi = gunKural.NobetGunKuralAdi,
                            KararDegiskeni = _x
                        };
                        TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaIlgiliKisit);

                        #region Kümülatif toplam en fazla - Tur takip kısıtı

                        var kumulatifOrtalamaGunKuralNobetSayisi = tarihAralik.KumulatifOrtalamaNobetSayisi;

                        int gunKuralNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                        var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = gruptakiEncokNobetSayisi - haftaIciEnCokNobetSayisi; //- haftaIciEnAzNobetSayisi;

                        if (NobetUstGrupKisit(kisitlarAktif, "K34").SagTarafDegeri > 0 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                            kumulatifOrtalamaGunKuralNobetSayisi = NobetUstGrupKisit(kisitlarAktif, "K34").SagTarafDegeri;

                        if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                        //NobetUstGrupKisit(kisitlarAktif, "K44").SagTarafDegeri
                        if (haftaIciEnAzVeEnCokNobetSayisiArasindakiFark > 0
                            && !NobetUstGrupKisit(kisitlarAktif, "K44").PasifMi)
                        {
                            if (data.CalismaSayisi >= 1)
                                kumulatifOrtalamaGunKuralNobetSayisi++;

                            //if (data.CalismaSayisi >= 2)
                            //    kumulatifOrtalamaGunKuralNobetSayisi++;
                        }

                        if (data.CalismaSayisi > 2
                            && !NobetUstGrupKisit(kisitlarAktif, "K44").PasifMi)
                            kumulatifOrtalamaGunKuralNobetSayisi++;

                        //var kumulatifToplamEnFazla = new KpKumulatifToplam
                        //{
                        //    Model = model,
                        //    Tarihler = tarihAralik.TakvimNobetGruplar,
                        //    EczaneNobetGrup = eczaneNobetGrup,
                        //    EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        //    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K34"),
                        //    KumulatifOrtalamaGunKuralSayisi = kumulatifOrtalamaGunKuralNobetSayisi,
                        //    ToplamNobetSayisi = gunKuralNobetSayisi,
                        //    GunKuralAdi = gunKural.NobetGunKuralAdi,
                        //    KararDegiskeni = _x
                        //};

                        //KumulatifToplamEnFazla(kumulatifToplamEnFazla);

                        #endregion
                    }
                    #endregion

                    var cumartesiNobetleriEczaneBazli = cumartesiNobetleri.Where(w => w.EczaneId == eczaneNobetGrup.EczaneId).ToList();

                    var sonCumartesi = cumartesiNobetleriEczaneBazli.LastOrDefault();

                    if (cumartesiNobetleriEczaneBazli.Count() > 0)
                    {
                        var cumartesiPespeseFarkliGorevTipindeNobetYaz = new KpPespeseFarkliTurNobet
                        {
                            Model = model,
                            NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K49"),
                            Tarihler = cumartesiGunleri,
                            EczaneNobetGrup = eczaneNobetGrup,
                            EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler,
                            SonNobet = sonCumartesi,
                            KararDegiskeni = _x
                        };
                        PespeseFarkliTurNobetYaz(cumartesiPespeseFarkliGorevTipindeNobetYaz);
                    }

                    #region Bayram kümülatif toplam en fazla ve farklı tür bayram

                    if (bayramlar.Count() > 0)
                    {
                        var bayramNobetListesi = nobetGunKuralNobetSayilari.Where(w => w.GunGrupId == 2).ToList();

                        var bayramNobetleriAnahtarli = bayramNobetleri
                                .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                        var toplamBayramNobetSayisi = bayramNobetListesi.Sum(s => s.NobetSayisi);
                        //bayramNobetleri.Count();

                        if (!NobetUstGrupKisit(kisitlarAktif, "K5").PasifMi)
                        {
                            var bayramGunKuralIstatistikler = nobetGunKuralIstatistikler
                                                            .Where(w => w.GunGrupId == 2).ToList();

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
                                if (!NobetUstGrupKisit(kisitlarAktif, "K36").PasifMi)
                                {
                                    if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayram.NobetGunKuralId))
                                    {
                                        var bayramPespeseFarkliTurKisit = new KpPespeseFarkliTurNobet
                                        {
                                            Model = model,
                                            Tarihler = bayramlar,
                                            EczaneNobetGrup = eczaneNobetGrup,
                                            EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                            SonNobet = sonBayram,
                                            KararDegiskeni = _x,
                                            NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K36")
                                        };
                                        PespeseFarkliTurNobetYaz(bayramPespeseFarkliTurKisit);
                                    }

                                    //if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayram.NobetGunKuralId))
                                    //{
                                    //    var bayramPespeseFarkliGorevTipi = new KpPespeseFarkliTurNobet
                                    //    {
                                    //        Model = model,
                                    //        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K36"),
                                    //        Tarihler = bayramlar,
                                    //        EczaneNobetGrup = eczaneNobetGrup,
                                    //        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler,
                                    //        SonNobet = sonBayram,
                                    //        KararDegiskeni = _x
                                    //    };
                                    //    PespeseFarkliTurNobetYaz(bayramPespeseFarkliGorevTipi);
                                    //}
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

                            //model.AddConstraint(
                            //  Expression.Sum(data.EczaneNobetTarihAralik
                            //                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                         && e.GunDegerId > 7)
                            //                .Select(m => _x[m])) + toplamBayramNobetSayisi <= toplamBayramMax,
                            //                $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {eczaneNobetGrup}");
                        }
                    }
                    #endregion
                }

                #endregion

                #region eczane gruplar

                var esGrubaAyniGunNobetYazmaEczaneGruplar = new KpEsGrubaAyniGunNobetYazma();

                if (cumartesiGorevTiplerineGoreNobetleriDagit.PasifMi != true)
                {
                    esGrubaAyniGunNobetYazmaEczaneGruplar = new KpEsGrubaAyniGunNobetYazma
                    {
                        Model = model,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikTumGorevTipleri,
                        EczaneNobetSonuclar = eczaneNobetSonuclarGorevTipBazli,
                        NobetUstGrupKisit = cumartesiGorevTiplerineGoreNobetleriDagit,
                        EczaneGruplar = eczaneGruplarGorevTip2,
                        Tarihler = ilgiliTarihler,
                        KararDegiskeni = _x
                    };
                }
                else
                {
                    esGrubaAyniGunNobetYazmaEczaneGruplar = new KpEsGrubaAyniGunNobetYazma
                    {
                        Model = model,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                        EczaneNobetSonuclar = eczaneNobetSonuclarGorevTipBazli,
                        NobetUstGrupKisit = eczaneGrup,
                        EczaneGruplar = eczaneGruplarGorevTip2,
                        Tarihler = cumartesiGunleri,
                        KararDegiskeni = _x
                    };
                }
                EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaEczaneGruplar);

                #endregion

                #region istek ve mazeretler

                var istegiKarsilaKisit = new KpIstegiKarsila
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K12"),
                    EczaneNobetIstekler = data.EczaneNobetIstekler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                IstegiKarsila(istegiKarsilaKisit);

                var mazereteGorevYazmaKisit = new KpMazereteGorevYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "K13"),
                    EczaneNobetMazeretler = data.EczaneNobetMazeretler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                MazereteGorevYazma(mazereteGorevYazmaKisit);

                #endregion
            }

            #endregion

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(GiresunDataModel data)
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

                        var nobetGorevTipId = 1;

                        var nobetGrupTarihler1 = data.EczaneNobetTarihAralik
                            .Where(w => w.NobetGorevTipId == nobetGorevTipId)
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

                        nobetGorevTipId = 2;

                        var nobetGrupTarihler2 = data.EczaneNobetTarihAralik
                            .Where(w => w.NobetGorevTipId == nobetGorevTipId)
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

                        var toplamArz = sonuclar.Count;
                        var toplamTalep = nobetGrupTarihler1.Sum(s => s.Talep) + nobetGrupTarihler2.Sum(s => s.Talep);

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
