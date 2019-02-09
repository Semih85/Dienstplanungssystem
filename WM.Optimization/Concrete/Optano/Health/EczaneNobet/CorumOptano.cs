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
    public class CorumOptano : EczaneNobetKisit, IEczaneNobetCorumOptimization
    {
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }

        private Model Model(CorumDataModel data)
        {
            var model = new Model() { Name = "Corum Eczane Nöbet" };

            #region Veriler

            #region kısıtlar

            var herAyPespeseGorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyPespeseGorev", data.NobetUstGrupId);
            var farkliAyPespeseGorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "farkliAyPespeseGorev", data.NobetUstGrupId);
            var pazarPespeseGorevEnAz = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "pazarPespeseGorevEnAz", data.NobetUstGrupId);
            var cumartesiPespeseGorevEnAz = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "cumartesiPespeseGorevEnAz", data.NobetUstGrupId);

            var herAyEnFazlaGorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazlaGorev", data.NobetUstGrupId);
            var herAyEnFazlaHaftaIci = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazlaHaftaIci", data.NobetUstGrupId);

            var haftaIciPespeseGorevEnAz = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "haftaIciPespeseGorevEnAz", data.NobetUstGrupId);

            var gunKumulatifToplamEnFazla = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "gunKumulatifToplamEnFazla", data.NobetUstGrupId);
            var herAyEnFazla1HaftaIciGunler = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1HaftaIciGunler", data.NobetUstGrupId);

            var herAyEnFazla1Pazar = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1Pazar", data.NobetUstGrupId);
            var herAyEnFazla1Cumartesi = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1Cumartesi", data.NobetUstGrupId);
            var herAyEnFazla1Cuma = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1Cuma", data.NobetUstGrupId);

            var bayramToplamEnFazla = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "bayramToplamEnFazla", data.NobetUstGrupId);
            var bayramPespeseFarkliTur = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "bayramPespeseFarkliTur", data.NobetUstGrupId);
            var herAyEnFazla1Bayram = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1Bayram", data.NobetUstGrupId);

            var eczaneGrup = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "eczaneGrup", data.NobetUstGrupId);
            var istek = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "istek", data.NobetUstGrupId);
            var mazeret = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "mazeret", data.NobetUstGrupId);

            var pespeseHaftaIciAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "pespeseHaftaIciAyniGunNobet", data.NobetUstGrupId);
            var nobetBorcOdeme = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "nobetBorcOdeme", data.NobetUstGrupId);

            var altGruplarlaAyniGunNobetTutma = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "altGruplarlaAyniGunNobetTutma", data.NobetUstGrupId);
            var oncekiAylarAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "oncekiAylarAyniGunNobet", data.NobetUstGrupId);
            var eczanelerinNobetGunleriniKisitla = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "eczanelerinNobetGunleriniKisitla", data.NobetUstGrupId);
            var birEczaneyeAyniGunSadece1GorevTipYaz = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "birEczaneyeAyniGunSadece1GorevTipYaz", data.NobetUstGrupId);
            var cumartesiGorevTiplerineGoreNobetleriDagit = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "cumartesiGorevTiplerineGoreNobetleriDagit", data.NobetUstGrupId);

            var ayIcindeAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "ayIcindeAyniGunNobet", data.NobetUstGrupId);

            //pasifler
            var herAyEnaz1Gorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnaz1Gorev", data.NobetUstGrupId);
            var herAyEnaz1HaftaIciGorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnaz1HaftaIciGorev", data.NobetUstGrupId);
            var haftaninGunleriDagilimi = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "haftaninGunleriDagilimi", data.NobetUstGrupId);

            var toplamMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamMaxHedef", data.NobetUstGrupId);
            var haftaIciToplamMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "haftaIciToplamMaxHedef", data.NobetUstGrupId);
            var toplamCumaCumartesiMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumaCumartesiMaxHedef", data.NobetUstGrupId);
            var toplamCumartesiPazarMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumartesiPazarMaxHedef", data.NobetUstGrupId);
            var toplamCumaMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumaMaxHedef", data.NobetUstGrupId);
            var toplamCumartesiMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumartesiMaxHedef", data.NobetUstGrupId);
            var toplamPazarMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamPazarMaxHedef", data.NobetUstGrupId);

            var toplamMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamMinHedef", data.NobetUstGrupId);
            var haftaIciToplamMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "haftaIciToplamMinHedef", data.NobetUstGrupId);
            var toplamCumaCumartesiMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumaCumartesiMinHedef", data.NobetUstGrupId);
            var toplamCumartesiMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumartesiMinHedef", data.NobetUstGrupId);
            var toplamPazarMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamPazarMinHedef", data.NobetUstGrupId);
            var bayramToplamMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "bayramToplamMinHedef", data.NobetUstGrupId);

            #endregion

            #region tur çevrim katsayıları

            //int bayramCevrimDini = 8000;
            //int bayramCevrimMilli = 8000;
            //int yilbasiCevrim = 7000;
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
                    t => $"{t.EczaneNobetGrupId}_{t.TakvimId}, {t.EczaneAdi}, {t.Tarih.ToShortDateString()}",
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            #endregion

            #region Amaç Fonksiyonu

            //ilk yazılan nöbet öncelikli olsun...
            var amac = new Objective(Expression
                .Sum(from i in data.EczaneNobetTarihAralik
                     select (_x[i] * i.AmacFonksiyonKatsayi)),
                     "Sum of all item-values: ",
                     ObjectiveSense.Minimize);

            model.AddObjective(amac);

            //var amac = new Objective(Expression.Sum(
            //    from i in data.EczaneNobetTarihAralik
            //    from p in data.EczaneNobetGrupGunKuralIstatistikYatay
            //    where i.EczaneNobetGrupId == p.EczaneNobetGrupId
            //    select (_x[i]
            //           //bayram - dini
            //           + _x[i] * Convert.ToInt32(i.DiniBayramMi)
            //                   * (bayramCevrimDini + bayramCevrimDini / Math.Sqrt((i.Tarih - p.SonNobetTarihiDiniBayram).TotalDays)
            //                   )
            //           //bayram - milli
            //           + _x[i] * Convert.ToInt32(i.MilliBayramMi)
            //                   * (bayramCevrimMilli + bayramCevrimMilli / Math.Sqrt((i.Tarih - p.SonNobetTarihiMilliBayram).TotalDays)
            //                   )
            //           //1 ocak
            //           + _x[i] * Convert.ToInt32(i.YilbasiMi)
            //                   * (yilbasiCevrim + yilbasiCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihi1Ocak).TotalDays)
            //                   )
            //           //cumartesi
            //           + _x[i] * Convert.ToInt32(i.CumartesiGunuMu)
            //                   * (cumartesiCevrim + cumartesiCevrim
            //                       / Math.Sqrt((i.Tarih - p.SonNobetTarihiCumartesi).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7))
            //                   )
            //           //pazar
            //           + _x[i] * Convert.ToInt32(i.PazarGunuMu)
            //                   * (pazarCevrim + pazarCevrim
            //                       / Math.Sqrt((i.Tarih - p.SonNobetTarihiPazar).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))
            //           //hafta içi
            //           + _x[i] * Convert.ToInt32(i.HaftaIciMi) //* Math.Pow(p.NobetGorevTipId, 10)
            //                                                   //* (haftaIciCevrim + haftaIciCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
            //                   * (haftaIciCevrim + haftaIciCevrim
            //                   / Math.Sqrt(
            //                       //(  i.EczaneAdi == "borçlu eczane" //BADE
            //                       //? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + 15 
            //                       //: (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays)
            //                       nobetBorcOdeme.PasifMi == false
            //                       ? (p.BorcluNobetSayisiHaftaIci >= 0 //-5
            //                           ?
            //    #region Manuel borç düzeltme
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
            //    #endregion
            //                                  (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                           : ((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci < 1
            //                               ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays
            //                               : (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
            //                             //(i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays
            //                             )
            //                         ) * i.Tarih.Day
            //                       : (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
            //                   * (pespeseHaftaIciAyniGunNobet.PasifMi == false
            //                   ? (i.Tarih.DayOfWeek == p.SonNobetTarihiHaftaIci.DayOfWeek ? 1 : 0.2)
            //                   : 1)//aynı gün peşpeşe gelmesin
            //                   )
            //           )),
            //            "Sum of all item-values: ",
            //            ObjectiveSense.Minimize);

            #endregion

            #region Kısıtlar

            #region Gece nöbetçileri

            var nobetGorevTipId = 1;
            var nobetGrupGorevTip = data.NobetGrupGorevTipler.Where(w => w.NobetGorevTipId == nobetGorevTipId).SingleOrDefault();

            if (nobetGrupGorevTip != null)
            {
                #region ön hazırlama

                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var pespeseNobetSayisi = (int)nobetGrupKurallar
                        .Where(s => s.NobetKuralId == 1)
                        .Select(s => s.Deger).SingleOrDefault();

                int gunlukNobetciSayisi = (int)nobetGrupKurallar
                        .Where(s => s.NobetKuralId == 3)
                        .Select(s => s.Deger).SingleOrDefault();

                //var nobetGrupTalepler = data.NobetGrupTalepler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                #region tarihler

                var tarihler = data.TarihAraligi
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId)
                             .OrderBy(o => o.Tarih).ToList();

                var nobetGrupTalepler = tarihler
                    .GroupBy(g => g.TalepEdilenNobetciSayisi)
                    .Select(s => new
                    {
                        TalepEdilenNobetciSayisi = s.Key,
                        GunSayisi = s.Count()
                    }).ToList();

                //TalepleriTakvimeIsle(nobetGrupTalepler, gunlukNobetciSayisi, tarihler);

                var pazarGunleri = tarihler.Where(w => w.GunGrupId == 1).OrderBy(o => o.Tarih).ToList();
                var cumaGunleri = tarihler.Where(w => w.NobetGunKuralId == 6).OrderBy(o => o.Tarih).ToList();
                var cumaVeCumartesiGunleri = tarihler.Where(w => w.NobetGunKuralId == 6 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
                var cumartesiVePazarGunleri = tarihler.Where(w => w.NobetGunKuralId == 1 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
                var bayramlar = tarihler.Where(w => w.GunGrupId == 2).OrderBy(o => o.Tarih).ToList();
                var haftaIciGunleri = tarihler.Where(w => w.GunGrupId == 3).OrderBy(o => o.Tarih).ToList();
                var cumartesiGunleri = tarihler.Where(w => w.GunGrupId == 4).OrderBy(o => o.Tarih).ToList();

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
                var altLimit = farkliAyPespeseGorevAraligi / gunlukNobetciSayisi * 0.6;//0.7666; //0.95
                var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                var ustLimitKontrol = ustLimit * 0.95; //0.81 

                var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / (5 * gunlukNobetciSayisi)) - 1;
                var cumartersiNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / (5 * 2)) - 1;
                #endregion

                #region ortalama nöbetçi sayıları
                //var haftaIciOrtamalaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, haftaIciSayisi);
                //var ortalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, gunSayisi);
                //var bayramOrtalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, bayramSayisi);

                var haftaIciOrtamalaNobetSayisi = OrtalamaNobetSayisi(haftaIciGunleri.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var ortamalaNobetSayisiPazar = OrtalamaNobetSayisi(pazarGunleri.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var ortamalaNobetSayisiCumartesi = OrtalamaNobetSayisi(cumartesiGunleri.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                var ortalamaNobetSayisi = OrtalamaNobetSayisi(tarihler.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
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

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {//eczane bazlı kısıtlar

                    #region kontrol

                    var kontrol = false;

                    if (kontrol)
                    {
                        var kontrolEdilecekEczaneler = new string[] {
                            "NUR ACAR"
                            ,"DORUK"
                            ,"GÖZDE"
                            ,"YASEMİN"
                            ,"KUZEY"
                            ,"GÜVEN"
                            ,"ELİF"
                            ,"YEDİTEPE"
                        };

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                        {
                        }
                        //else
                        //{
                        //    continue;
                        //}
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

                    var eczaneNobetIstatistikTumGorevTipleri = data.EczaneNobetGrupGunKuralIstatistikYatay
                        .GroupBy(g => new
                        {
                            g.EczaneNobetGrupId,
                            g.NobetGrupId,
                            g.NobetGrupAdi,
                            g.EczaneAdi,
                            g.EczaneId
                        })
                        .Select(s => new EczaneNobetGrupGunKuralIstatistikYatay
                        {
                            NobetGrupId = s.Key.NobetGrupId,
                            EczaneId = s.Key.EczaneId,
                            EczaneAdi = s.Key.EczaneAdi,
                            NobetGrupAdi = s.Key.NobetGrupAdi,
                            EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                            SonNobetTarihi = s.Max(m => m.SonNobetTarihi),
                            NobetSayisiToplam = s.Sum(c => c.NobetSayisiToplam)
                        })
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    var yazilabilecekIlkPazarTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar.AddMonths(pazarNobetiYazilabilecekIlkAy - 1);
                    var yazilabilecekIlkCumartesiTarihi = eczaneNobetIstatistik.SonNobetTarihiCumartesi.AddMonths(cumartersiNobetiYazilabilecekIlkAy - 1);

                    var yazilabilecekIlkHaftaIciTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci.AddDays(altLimit);
                    var nobetYazilabilecekIlkTarih = eczaneNobetIstatistik.SonNobetTarihi.AddDays(pespeseNobetSayisi);
                    var nobetYazilabilecekIlkTarihTumGorevTipleri = eczaneNobetIstatistikTumGorevTipleri.SonNobetTarihi.AddDays(pespeseNobetSayisi);

                    var kpKumulatifToplam = new KpKumulatifToplam
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    var KpTarihAraligindaEnAz1NobetYaz = new KpTarihAraligindaEnAz1NobetYaz
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        GruptakiNobetciSayisi = gruptakiEczaneSayisi
                    };
                    //var nobetTutamazGunler = eczaneNobetTutamazGunler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    //var nobetYazilabilirGunler = nobetGrupGunKurallar
                    //    .Where(w => !nobetTutamazGunler.Select(s => s.NobetGunKuralId).Contains(w)
                    //             && !ozelTurTakibiYapilacakGunler.Contains(w)).ToList();
                    #endregion

                    #region aktif kısıtlar

                    #region Peş peşe nöbet

                    #region ay içinde

                    var kpHerAyPespeseGorev = new KpHerAyPespeseGorev
                    {
                        Model = model,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler,
                        NobetUstGrupKisit = herAyPespeseGorev,
                        Tarihler = tarihler,
                        PespeseNobetSayisi = pespeseNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };

                    //if (kpHerAyPespeseGorev.EczaneNobetGrup.EczaneAdi != "HAKAN")
                    //{
                    //}
                        HerAyPespeseGorev(kpHerAyPespeseGorev);

                    var kpHerAyPespeseGorevHaftaIci = new KpHerAyHaftaIciPespeseGorev
                    {
                        Model = model,
                        Tarihler = haftaIciGunleri,
                        OrtamalaNobetSayisi = haftaIciOrtamalaNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        PespeseNobetSayisiAltLimit = gruptakiEczaneSayisi * 0.3,//0,4
                        NobetUstGrupKisit = haftaIciPespeseGorevEnAz,
                        GunKuralAdi = "h.içi",
                        KararDegiskeni = _x
                    };
                    HerAyHaftaIciPespeseGorev(kpHerAyPespeseGorevHaftaIci);

                    var kpHerAyPespeseGorevPazar = new KpHerAyHaftaIciPespeseGorev
                    {
                        Model = model,
                        Tarihler = pazarGunleri,
                        OrtamalaNobetSayisi = ortamalaNobetSayisiPazar,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        PespeseNobetSayisiAltLimit = gruptakiEczaneSayisi * 0.8,
                        NobetUstGrupKisit = haftaIciPespeseGorevEnAz,
                        GunKuralAdi = "pazar",
                        KararDegiskeni = _x
                    };
                    HerAyHaftaIciPespeseGorev(kpHerAyPespeseGorevPazar);

                    var kpHerAyPespeseGorevCumartesi = new KpHerAyHaftaIciPespeseGorev
                    {
                        Model = model,
                        Tarihler = cumartesiGunleri,
                        OrtamalaNobetSayisi = ortamalaNobetSayisiCumartesi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        PespeseNobetSayisiAltLimit = gruptakiEczaneSayisi * 0.5, //0,5
                        NobetUstGrupKisit = haftaIciPespeseGorevEnAz,
                        GunKuralAdi = "cumartesi",
                        KararDegiskeni = _x
                    };
                    HerAyHaftaIciPespeseGorev(kpHerAyPespeseGorevCumartesi);

                    #endregion

                    #region farklı aylar peş peşe görev

                    var kpPesPeseGorevEnAz = new KpPesPeseGorevEnAz
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    var pesPeseGorevEnAzFarkliAylar = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzFarkliAylar.NobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;
                    pesPeseGorevEnAzFarkliAylar.NobetUstGrupKisit = farkliAyPespeseGorev;
                    pesPeseGorevEnAzFarkliAylar.Tarihler = tarihler;
                    pesPeseGorevEnAzFarkliAylar.NobetYazilabilecekIlkTarih = nobetYazilabilecekIlkTarih;
                    pesPeseGorevEnAzFarkliAylar.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzFarkliAylar);

                    var pesPeseGorevEnAzPazar = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzPazar.NobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;
                    pesPeseGorevEnAzPazar.NobetUstGrupKisit = pazarPespeseGorevEnAz;
                    pesPeseGorevEnAzPazar.Tarihler = pazarGunleri;
                    pesPeseGorevEnAzPazar.NobetYazilabilecekIlkTarih = yazilabilecekIlkPazarTarihi;
                    pesPeseGorevEnAzPazar.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar;

                    PesPeseGorevEnAz(pesPeseGorevEnAzPazar);

                    var pesPeseGorevEnAzCumartesi = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzCumartesi.NobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;
                    pesPeseGorevEnAzCumartesi.NobetUstGrupKisit = cumartesiPespeseGorevEnAz;
                    pesPeseGorevEnAzCumartesi.Tarihler = cumartesiGunleri;
                    pesPeseGorevEnAzCumartesi.NobetYazilabilecekIlkTarih = yazilabilecekIlkCumartesiTarihi;
                    pesPeseGorevEnAzCumartesi.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiCumartesi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzCumartesi);

                    var pesPeseGorevEnAzHaftaIci = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzHaftaIci.NobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;
                    pesPeseGorevEnAzHaftaIci.NobetUstGrupKisit = haftaIciPespeseGorevEnAz;
                    pesPeseGorevEnAzHaftaIci.Tarihler = haftaIciGunleri;
                    pesPeseGorevEnAzHaftaIci.NobetYazilabilecekIlkTarih = yazilabilecekIlkHaftaIciTarihi;
                    pesPeseGorevEnAzHaftaIci.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci;

                    PesPeseGorevEnAz(pesPeseGorevEnAzHaftaIci);

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

                    #region seçilen aralık

                    var ortalamaEnFazlaTumTarihAraligi = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaTumTarihAraligi.Tarihler = tarihler;
                    ortalamaEnFazlaTumTarihAraligi.GunSayisi = gunSayisi;
                    ortalamaEnFazlaTumTarihAraligi.OrtalamaNobetSayisi = ortalamaNobetSayisi;
                    ortalamaEnFazlaTumTarihAraligi.NobetUstGrupKisit = herAyEnFazlaGorev;

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaTumTarihAraligi);

                    var ortalamaEnFazlaHaftaIici = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaHaftaIici.Tarihler = haftaIciGunleri;
                    ortalamaEnFazlaHaftaIici.GunSayisi = haftaIciSayisi;
                    ortalamaEnFazlaHaftaIici.OrtalamaNobetSayisi = haftaIciOrtamalaNobetSayisi;
                    ortalamaEnFazlaHaftaIici.NobetUstGrupKisit = herAyEnFazlaHaftaIci;

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHaftaIici);

                    var ortalamaEnFazlaBayram = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaBayram.Tarihler = bayramlar;
                    ortalamaEnFazlaBayram.GunSayisi = bayramSayisi;
                    ortalamaEnFazlaBayram.OrtalamaNobetSayisi = bayramOrtalamaNobetSayisi;
                    ortalamaEnFazlaBayram.NobetUstGrupKisit = herAyEnFazla1Bayram;

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaBayram);

                    #endregion

                    #region her ay en fazla

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
                            ortalamaEnFazlaHerAy.GunKuralAdi = $"{ay}.ay";
                            ortalamaEnFazlaHerAy.NobetUstGrupKisit = herAyEnFazlaGorev;

                            TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHerAy);

                            var ayHaftaIiciGunler = haftaIciGunleri.Where(w => w.Ay == ay).ToList();
                            var ortalamaNobetSayisiAylikHaftaIci = OrtalamaNobetSayisi(ayHaftaIiciGunler.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);

                            var ortalamaEnFazlaHerAyHaftaIici = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                            ortalamaEnFazlaHerAyHaftaIici.Tarihler = haftaIciGunleri.Where(w => w.Ay == ay).ToList();
                            ortalamaEnFazlaHerAyHaftaIici.GunSayisi = ayHaftaIiciGunler.Count;
                            ortalamaEnFazlaHerAyHaftaIici.GunKuralAdi = $"{ay}.ay";
                            ortalamaEnFazlaHerAyHaftaIici.OrtalamaNobetSayisi = ortalamaNobetSayisiAylikHaftaIci;
                            ortalamaEnFazlaHerAyHaftaIici.NobetUstGrupKisit = herAyEnFazlaHaftaIci;

                            TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHerAyHaftaIici);
                        }
                    }

                    #endregion

                    #endregion

                    #region Kümülatif kısıtlar

                    //var kumulatifHaftaIci = nobetGunKuralIstatistikler.Where(w => w.NobetGunKuralId >= 2 && w.NobetGunKuralId < 7).Sum(s => s.GunSayisi);// + haftaIciSayisi;

                    //var kumulatifToplamEnFazlaHaftaIci = new KpKumulatifToplamEnFazla
                    //{
                    //    Model = model,
                    //    Tarihler = haftaIciGunleri,
                    //    EczaneNobetGrup = eczaneNobetGrup,
                    //    EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                    //    NobetUstGrupKisit = herAyEnFazlaGorev,
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
                            NobetGunKuralAdi = gunKural.NobetGunKuralAdi,
                            NobetGunKuralId = gunKural.NobetGunKuralId,
                            NobetSayisi = toplamNobetSayisi,
                            GunGrupId = gunKural.GunGrupId,
                            GunGrupAdi = gunKural.GunGrupAdi
                        });
                    }

                    //var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi) - nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                    var haftaIciEnCokNobetSayisi = 0;
                    var haftaIciEnAzNobetSayisi = 0;

                    if (nobetGunKuralIstatistikler.Count > 0)
                    {
                        var haftaiciNobetIstatistik = nobetGunKuralNobetSayilari.Where(w => w.GunGrupId == 3).ToList();

                        haftaIciEnCokNobetSayisi = haftaiciNobetIstatistik.Max(m => m.NobetSayisi);
                        haftaIciEnAzNobetSayisi = haftaiciNobetIstatistik.Min(m => m.NobetSayisi);
                    }
                    foreach (var gunKural in nobetGunKuralIstatistikler
                        //.Where(w => w.NobetGunKuralId == 7 || w.NobetGunKuralId == 1) // cumartesi ve pazar 
                        )
                    {//gun kural bazlı                        

                        //if (nobetTutamazGunler.Count > 0
                        //    //nobetTutamazEczaneGunu != null
                        //    && nobetYazilabilirGunler.Contains(gunKural.NobetGunKuralId)
                        //    && !eczanelerinNobetGunleriniKisitla.PasifMi
                        //    )
                        //    continue;

                        if (kontrol && gunKural.NobetGunKuralAdi == "Cuma")
                        {
                        }

                        switch (gunKural.NobetGunKuralId)
                        {
                            case 1:
                                herAyEnFazlaIlgiliKisit = herAyEnFazla1Pazar;
                                break;
                            case 7:
                                herAyEnFazlaIlgiliKisit = herAyEnFazla1Cumartesi;
                                break;
                            case 6:
                                herAyEnFazlaIlgiliKisit = herAyEnFazla1Cuma;
                                break;
                            default:
                                herAyEnFazlaIlgiliKisit = herAyEnFazla1HaftaIciGunler;
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

                        //gün kural eklenecek
                        #region Kümülatif toplam en fazla - Tur takip kısıtı

                        var kumulatifOrtalamaGunKuralNobetSayisi = tarihAralik.KumulatifOrtalamaNobetSayisi;

                        int gunKuralNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                        var haftaIciEnCokVeGunKuralNobetleriArasindakiFark = haftaIciEnCokNobetSayisi - gunKuralNobetSayisi;
                        var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = haftaIciEnCokNobetSayisi - haftaIciEnAzNobetSayisi;

                        if (gunKumulatifToplamEnFazla.SagTarafDegeri > 0 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKumulatifToplamEnFazla.SagTarafDegeri;

                        if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                        //haftaninGunleriDagilimi.SagTarafDegeri = 3
                        if (haftaIciEnAzVeEnCokNobetSayisiArasindakiFark < haftaninGunleriDagilimi.SagTarafDegeri
                            && !haftaninGunleriDagilimi.PasifMi
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
                            NobetUstGrupKisit = gunKumulatifToplamEnFazla,
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
                        var bayramNobetleri = nobetGunKuralNobetSayilari.Where(w => w.NobetGunKuralId > 7).ToList();

                        var bayramNobetleriAnahtarli = eczaneNobetSonuclar
                                .Where(w => w.NobetGunKuralId > 7).ToList();
                        //.Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var toplamBayramNobetSayisi = bayramNobetleri.Sum(s => s.NobetSayisi);
                        //bayramNobetleri.Count();

                        if (!bayramToplamEnFazla.PasifMi)
                        {
                            var bayramGunKuralIstatistikler = nobetGunKuralIstatistikler
                                                            .Where(w => w.NobetGunKuralId > 7).ToList();

                            var toplamBayramGrupToplamNobetSayisi = nobetGrupBayramNobetleri.Count();

                            var kumulatifBayramSayisi = bayramGunKuralIstatistikler.Sum(x => x.GunSayisi);

                            var yillikOrtalamaGunKuralSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, kumulatifBayramSayisi);
                            //Math.Ceiling((double)kumulatifBayramSayisi / gruptakiEczaneSayisi);

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

                            if (bayramToplamEnFazla.SagTarafDegeri > 0)
                                yillikOrtalamaGunKuralSayisi = bayramToplamEnFazla.SagTarafDegeri;

                            var sonBayram = bayramNobetleriAnahtarli.LastOrDefault();

                            if (sonBayram != null)
                            {
                                var sonBayramTuru = bayramNobetleriAnahtarli.LastOrDefault();

                                if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayramTuru.NobetGunKuralId) && !bayramPespeseFarkliTur.PasifMi)
                                {
                                    var bayramPespeseFarkliTurKisit = new KpPespeseFarkliTurNobet
                                    {
                                        Model = model,
                                        NobetUstGrupKisit = bayramPespeseFarkliTur,
                                        Tarihler = bayramlar,
                                        EczaneNobetGrup = eczaneNobetGrup,
                                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                        SonNobet = sonBayramTuru,
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
                                        NobetUstGrupKisit = bayramToplamEnFazla,
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

                    #endregion

                    #region pasif kısıtlar                                                         

                    #region kümülatif en fazla

                    #region Toplam max

                    var kpKumulatifToplamEnFazla = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazla.Tarihler = tarihler;
                    kpKumulatifToplamEnFazla.NobetUstGrupKisit = toplamMaxHedef;
                    kpKumulatifToplamEnFazla.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazla);

                    #endregion 

                    #region Hafta içi toplam max hedefler

                    var kpKumulatifToplamEnFazlaHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIci.NobetUstGrupKisit = haftaIciToplamMaxHedef;
                    kpKumulatifToplamEnFazlaHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIci);

                    #endregion

                    #region Toplam cuma cumartesi max hedefler

                    var kpKumulatifToplamEnFazlaCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.NobetUstGrupKisit = toplamCumaCumartesiMaxHedef;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumaVeCumartesi);

                    #endregion

                    #region Toplam cumartesi ve pazar max hedefler

                    var kpKumulatifToplamEnFazlaCumartesiVePazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesiVePazar.Tarihler = cumartesiVePazarGunleri;
                    kpKumulatifToplamEnFazlaCumartesiVePazar.NobetUstGrupKisit = toplamCumartesiPazarMaxHedef;
                    kpKumulatifToplamEnFazlaCumartesiVePazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar + eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesiVePazar);

                    #endregion

                    #region Toplam Cuma Max Hedef

                    var kpKumulatifToplamEnFazlaCuma = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCuma.Tarihler = cumaGunleri;
                    kpKumulatifToplamEnFazlaCuma.NobetUstGrupKisit = toplamCumaMaxHedef;
                    kpKumulatifToplamEnFazlaCuma.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCuma);

                    #endregion

                    #region Toplam Cumartesi Max Hedef

                    var kpKumulatifToplamEnFazlaCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesi.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumartesi.NobetUstGrupKisit = toplamCumartesiMaxHedef;
                    kpKumulatifToplamEnFazlaCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesi);

                    #endregion

                    #region Toplam Pazar Max Hedef

                    var kpKumulatifToplamEnFazlaPazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaPazar.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnFazlaPazar.NobetUstGrupKisit = toplamPazarMaxHedef;
                    kpKumulatifToplamEnFazlaPazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaPazar);

                    #endregion

                    #endregion

                    #region en az

                    #region tarih aralığı en az

                    #region tarih aralığı en az 1 görev

                    var tarihAraligindaEnAz1NobetYazKisitTarihAraligi = (KpTarihAraligindaEnAz1NobetYaz)KpTarihAraligindaEnAz1NobetYaz.Clone();

                    tarihAraligindaEnAz1NobetYazKisitTarihAraligi.NobetUstGrupKisit = herAyEnaz1Gorev;
                    tarihAraligindaEnAz1NobetYazKisitTarihAraligi.Tarihler = tarihler;

                    TarihAraligindaEnAz1NobetYaz(tarihAraligindaEnAz1NobetYazKisitTarihAraligi);

                    #endregion

                    #region tarih aralığı en az 1 hafta içi

                    var tarihAraligindaEnAz1NobetYazKisitHaftaIci = (KpTarihAraligindaEnAz1NobetYaz)KpTarihAraligindaEnAz1NobetYaz.Clone();

                    tarihAraligindaEnAz1NobetYazKisitHaftaIci.NobetUstGrupKisit = herAyEnaz1HaftaIciGorev;
                    tarihAraligindaEnAz1NobetYazKisitHaftaIci.Tarihler = haftaIciGunleri;

                    TarihAraligindaEnAz1NobetYaz(tarihAraligindaEnAz1NobetYazKisitHaftaIci);

                    #endregion

                    #endregion

                    #region kümülatif en az

                    kpKumulatifToplam.EnAzMi = true;

                    #region Kümülatif en az

                    var kpKumulatifToplamEnAz = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAz.Tarihler = tarihler;
                    kpKumulatifToplamEnAz.NobetUstGrupKisit = toplamMinHedef;
                    kpKumulatifToplamEnAz.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAz);

                    #endregion

                    #region Kümülatif cuma ve cumartesi en az

                    var kpKumulatifToplamEnAzCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnAzCumaVeCumartesi.NobetUstGrupKisit = toplamCumaCumartesiMinHedef;
                    kpKumulatifToplamEnAzCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumaVeCumartesi);

                    #endregion

                    #region Kümülatif cumartesi en az

                    var kpKumulatifToplamEnAzCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumartesi.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnAzCumartesi.NobetUstGrupKisit = toplamCumartesiMinHedef;
                    kpKumulatifToplamEnAzCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumartesi);

                    #endregion

                    #region Kümülatif pazar en az

                    var kpKumulatifToplamEnAzPazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzPazar.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnAzPazar.NobetUstGrupKisit = toplamPazarMinHedef;
                    kpKumulatifToplamEnAzPazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzPazar);

                    #endregion

                    #region Kümülatif hafta içi en az

                    var kpKumulatifToplamEnAzHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnAzHaftaIci.NobetUstGrupKisit = haftaIciToplamMinHedef;
                    kpKumulatifToplamEnAzHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzHaftaIci);

                    #endregion

                    #region Kümülatif bayram en az

                    var kpKumulatifToplamEnAzBayram = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzBayram.Tarihler = bayramlar;
                    kpKumulatifToplamEnAzBayram.NobetUstGrupKisit = bayramToplamMinHedef;
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

                #region eczane gruplar - aynı gün nöbet

                var tarihAraligi = data.TarihAraligi
                    .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var esGrubaAyniGunNobetYazmaEczaneGruplar = new KpEsGrubaAyniGunNobetYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    EczaneNobetSonuclar = eczaneNobetSonuclarGorevTipBazli,
                    NobetUstGrupKisit = eczaneGrup,
                    EczaneGruplar = data.EczaneGruplar
                        .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                        //&& !(w.EczaneGrupTanimAdi == "3. HASTANE" && (w.EczaneAdi == "HAKAN" || w.EczaneAdi == "YAŞAM"))
                        ).ToList(),
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

                #region istek ve mazeret

                var istegiKarsilaKisit = new KpIstegiKarsila
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = istek,
                    EczaneNobetIstekler = data.EczaneNobetIstekler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };

                #region manuel istek

                //var manuelIstekEczane = data.EczaneNobetGruplar
                //    .SingleOrDefault(w => w.EczaneAdi == "YAŞAM");

                //var manuelIstekTarih = data.TarihAraligi.SingleOrDefault(w => w.Tarih == new DateTime(2019, 5, 4));

                //istegiKarsilaKisit.EczaneNobetIstekler.Add(new EczaneNobetIstekDetay
                //{
                //    EczaneNobetGrupId = manuelIstekEczane.Id,
                //    EczaneAdi = manuelIstekEczane.EczaneAdi,
                //    TakvimId = manuelIstekTarih.TakvimId,
                //    Tarih = manuelIstekTarih.Tarih
                //}); 

                #endregion

                IstegiKarsila(istegiKarsilaKisit);

                var mazereteGorevYazmaKisit = new KpMazereteGorevYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = mazeret,
                    EczaneNobetMazeretler = data.EczaneNobetMazeretler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                MazereteGorevYazma(mazereteGorevYazmaKisit);

                #endregion

                #region Tarih aralığı içinde aynı gün nöbet

                var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
                {
                    Model = model,
                    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                    IkiliEczaneler = data.IkiliEczaneler,
                    NobetUstGrupKisit = ayIcindeAyniGunNobet,
                    Tarihler = data.TarihAraligi.Where(w => w.TalepEdilenNobetciSayisi > 1).ToList(),
                    KararDegiskeni = _x
                };
                AyIcindeSadece1KezAyniGunNobetTutulsun(ayIcindeSadece1KezAyniGunNobetKisit);

                #endregion

            }
            #endregion

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(CorumDataModel data)
        {
            var results = new EczaneNobetSonucModel();
            var calismaSayisiEnFazla = 3;

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
                        //TimeLimit = 60
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

                        //var nobetGorevTipId = 1;

                        var nobetGrupTarihler1 = data.EczaneNobetTarihAralik
                             //.Where(w => w.NobetGorevTipId == nobetGorevTipId)
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
