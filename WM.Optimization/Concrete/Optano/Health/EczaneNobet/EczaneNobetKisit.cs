using OPTANO.Modeling.Common;
using OPTANO.Modeling.Optimization;
using System;
using System.Collections.Generic;
using System.Linq;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;
using WM.Optimization.Entities.KisitParametre;

namespace WM.Optimization.Concrete.Optano.Health.EczaneNobet
{
    public class EczaneNobetKisit : IEczaneNobetKisit
    {
        #region core kısıtlar

        #region talep

        /// <summary>
        /// Her gün talep edilen kadar eczaneye nöbet yazılır.
        /// </summary>
        /// <param name="p">KpTalebiKarsila</param>
        public virtual void TalebiKarsila(KpTalebiKarsila p)
        {
            foreach (var tarih in p.Tarihler)
            {
                TalebiKarsila(p, tarih);
            }
        }

        public virtual void TalebiKarsila(KpTalebiKarsila p, TakvimNobetGrup tarih)
        {
            var nobetGrupBilgisi = p.NobetGrupGorevTip.NobetUstGrupId == 4
                ? IsimleriBirlestir(p.NobetGrupGorevTip.NobetGorevTipAdi)
                : p.NobetGrupGorevTip.NobetUstGrupId == 5
                ? ""
                : p.NobetGrupGorevTip.NobetGrupAdi;

            var kisitTanim = IsimleriBirlestir(p.NobetUstGrupKisit.KisitKodu
                , p.NobetUstGrupKisit.KisitKategorisi
                , p.NobetUstGrupKisit.KisitAciklama.Substring(0, p.NobetUstGrupKisit.KisitAciklama.Length - 3))
                + $"{tarih.Tarih.ToString("dd.MM.yy-ddd.")}";

            var kisitAdi = IsimleriBirlestir(kisitTanim
                , tarih.TalepEdilenNobetciSayisi.ToString()
                //,
                //$"{tarih.NobetGunKuralAdi}" +

                //, $""
                , p.TalepDetay
                , nobetGrupBilgisi
                //, 
                );

            var kararIndex = p.EczaneNobetTarihAralikTumu
                .Where(k => k.TakvimId == tarih.TakvimId).ToList();

            var std = tarih.TalepEdilenNobetciSayisi;
            var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
            var cns = Constraint.Equals(exp, std);

            p.Model.AddConstraint(cns, kisitAdi);
        }

        public virtual void HerGunAyniAltGruptanEnFazla1NobetciOlsun(
            List<TakvimNobetGrup> tarihler,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikGrupBazli,
            List<NobetAltGrupDetay> altGruplar,
            KpTarihVeAltGrupBazliEnFazla tarihVeAltGrupBazliEnFazla)
        {
            for (int i = 0; i < tarihler.Count; i++)
            {
                var tarih = tarihler[i];

                tarihVeAltGrupBazliEnFazla.Tarih = tarih;

                var eczaneNobetTarihAralikGrupBazliTarihBazli = eczaneNobetTarihAralikGrupBazli.Where(w => w.TakvimId == tarih.TakvimId).ToList();

                foreach (var altGrup in altGruplar)
                {
                    tarihVeAltGrupBazliEnFazla.GunKuralAdi = altGrup.Adi;

                    tarihVeAltGrupBazliEnFazla.EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazliTarihBazli
                        .Where(w => w.NobetAltGrupId == altGrup.Id).ToList();

                    TarihVeAltGrupBazliEnFazla(tarihVeAltGrupBazliEnFazla);
                }
            }
        }

        #endregion

        #region en az

        /// <summary>
        /// Her eczaneye istenen tarih aralığında en az 1 nöbet yazılır.
        /// </summary>
        /// <param name="p">KpTarihAraligindaEnAz1NobetYaz</param>
        public virtual void TarihAraligindaEnAz1NobetYaz(KpTarihAraligindaEnAz1NobetYaz p)
        {
            var talepEdilenNobetciSayisi = p.Tarihler.Sum(s => s.TalepEdilenNobetciSayisi);

            var gruptakiEczaneSayisi = p.GruptakiNobetciSayisi - p.IstisnaOlanNobetciSayisi;

            if (!p.NobetUstGrupKisit.PasifMi && gruptakiEczaneSayisi <= talepEdilenNobetciSayisi)
            {
                var kararIndex = p.EczaneNobetTarihAralik
                                        .Where(e => p.Tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                var enAzNobetSayisi = 1;

                if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                    enAzNobetSayisi = (int)p.NobetUstGrupKisit.SagTarafDegeri;

                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}" +
                    $" {p.KuralAciklama}" +
                    $"{enAzNobetSayisi}"
                    ;

                var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                var std = enAzNobetSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                var cns = Constraint.GreaterThanOrEqual(exp, std);
                //var isTriviallyFeasible = cns.IsTriviallyFeasible();

                p.Model.AddConstraint(cns, kisitAdi);
            }
        }

        #endregion

        #region en fazla

        /// <summary>
        /// Her eczaneye istenen tarih aralığında en fazla nöbet grubunun ortalaması kadar nöbet yazılır.
        /// </summary>
        /// <param name="p">KpTarihAraligiOrtalamaEnFazla</param>
        public virtual void TarihAraligiOrtalamaEnFazla(KpTarihAraligiOrtalamaEnFazla p)
        {
            if (!p.NobetUstGrupKisit.PasifMi && p.GunSayisi > 0)
            {
                if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                    p.OrtalamaNobetSayisi += p.NobetUstGrupKisit.SagTarafDegeri;

                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanimKisa}" +
                    $" {p.OrtalamaNobetSayisi}" +
                    $"{((p.GunKuralAdi == null || p.GunKuralAdi == "") ? "" : $"- {p.GunKuralAdi}")}"
                    ;

                var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                var kararIndex = p.EczaneNobetTarihAralik
                    .Where(e => p.Tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                var std = p.OrtalamaNobetSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                var cns = Constraint.LessThanOrEqual(exp, std);
                cns.LowerBound = 0;
                p.Model.AddConstraint(cns, kisitAdi);
            }
        }

        public virtual void TarihVeAltGrupBazliEnFazla(KpTarihVeAltGrupBazliEnFazla p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                    p.OrtalamaNobetSayisi = p.NobetUstGrupKisit.SagTarafDegeri;

                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanimKisa}" +
                    $" {p.OrtalamaNobetSayisi}" +
                    $" {p.Tarih.Tarih.ToShortDateString()}" +
                    $"{((p.GunKuralAdi == null || p.GunKuralAdi == "") ? "" : $"- {p.GunKuralAdi}")}"
                    ;

                var kararIndex = p.EczaneNobetTarihAralik;

                var std = p.OrtalamaNobetSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                var cns = Constraint.LessThanOrEqual(exp, std);
                cns.LowerBound = 0;
                p.Model.AddConstraint(cns, kisitTanim);
            }
        }

        /// <summary>
        /// Tüm günlerin tur takip kısıtı
        /// </summary>
        /// <param name="p">KpKumulatifToplamEnFazla</param>
        public virtual void KumulatifToplamEnFazla(KpKumulatifToplam p)
        {
            if (!p.NobetUstGrupKisit.PasifMi && p.Tarihler.Count > 0)
            {
                var tarihAraligi = p.Tarihler
                    .Select(s => new
                    {
                        s.TakvimId,
                        s.Tarih
                    }).Distinct().ToList();

                var kararIndex = p.EczaneNobetTarihAralik
                                   .Where(e => tarihAraligi.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                var durum = "Ort.";

                if (p.EnAzMi)
                {
                    if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                        p.KumulatifOrtalamaNobetSayisi = p.NobetUstGrupKisit.SagTarafDegeri;

                    durum = "En az";
                }
                else
                {
                    //if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                    p.KumulatifOrtalamaNobetSayisi += p.NobetUstGrupKisit.SagTarafDegeri;
                }

                var fark = p.KumulatifOrtalamaNobetSayisi - p.ToplamNobetSayisi;

                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanimKisa}" +
                    //$" {fark} = ({durum}: {p.KumulatifOrtalamaNobetSayisi} - top.nöb.: {p.ToplamNobetSayisi}) " +
                    $" {fark} = {p.KumulatifOrtalamaNobetSayisi}-{p.ToplamNobetSayisi} ({durum}-Top.)Nöb." +
                    $"{(p.GunKuralAdi == null ? "" : $" - {p.GunKuralAdi}")}"
                    ;

                var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                var std = fark < 0 ? 0 : fark;
                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                var cns = Constraint.LessThanOrEqual(exp, std);

                if (p.EnAzMi)
                    cns = Constraint.GreaterThanOrEqual(exp, std);
                else
                    cns.LowerBound = 0;

                p.Model.AddConstraint(cns, kisitAdi);
            }
        }

        #endregion

        #region peş peşe nöbetler

        /// <summary>
        /// İstenen günler için bir sonraki nöbet peş peşe en az gün geçmeden yazılmaz.
        /// </summary>
        /// <param name="p">KpPesPeseGorevEnAz</param>
        public virtual void PesPeseGorevEnAz(KpPesPeseGorevEnAz p)
        {
            if (!p.NobetUstGrupKisit.PasifMi
                && p.NobetSayisi > 0 //alanya için ilk nöbette yazılamayacak tarihi yarıya düşürmüştüm. 22.04.2019 dikkatli olmak lazım.
                                     //&& (p.NobetUstGrupKisit.NobetUstGrupId == 6 bartın 
                                     //&& p.EczaneNobetGrup.EczaneAdi != "BÜYÜK")
                                     //enSonNobetTarihi >= nobetUstGrupBaslamaTarihi
                )
            {
                var tarihAralik = p.Tarihler
                      .Where(w => w.Tarih <= p.NobetYazilabilecekIlkTarih)
                      .ToList();

                if (tarihAralik.Count > 0)
                {
                    if (p.NobetUstGrupKisit.NobetUstGrupId == 1)
                    {//istisna
                        var istekVarMi = p.EczaneNobetIstekler
                        .Where(w => tarihAralik.Select(s => s.TakvimId).Contains(w.TakvimId)).ToList();

                        if (istekVarMi.Count > 0)
                        {
                            return;
                        }
                    }

                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} "
                              //+ $"{p.PespeseNobetSayisiAltLimit} gün ("
                              //+ $""
                              + $"{p.SonNobetTarihi.ToString("dd.MM.yy")}-{p.NobetYazilabilecekIlkTarih.ToString("dd.MM.yy")}=>"
                              + $"{(int)(p.NobetYazilabilecekIlkTarih - p.SonNobetTarihi).TotalDays} gün"
                              + $"";

                    var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                    var kararIndex = p.EczaneNobetTarihAralik
                                       .Where(e => tarihAralik.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                    var std = 0;
                    var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                    var cns = Constraint.Equals(exp, std);
                    p.Model.AddConstraint(cns, kisitAdi);
                }
            }
        }

        /// <summary>
        /// Hafta içi nöbetler, yazıldığı tarih aralığında peş peşe gelmesin.
        /// </summary>
        /// <param name="p">KpHerAyHaftaIciPespeseGorev</param>
        public virtual void HerAyHaftaIciPespeseGorev(KpHerAyHaftaIciPespeseGorev p)
        {
            if (!p.NobetUstGrupKisit.PasifMi
                && p.OrtamalaNobetSayisi > 1
                )
            {
                var gunSayisi = p.Tarihler.Count;

                var talepEdilenNobetciSayisi = p.Tarihler.Sum(s => s.TalepEdilenNobetciSayisi);

                var kararIndex = p.EczaneNobetTarihAralik
                        .Where(e => p.Tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                if (p.PespeseNobetSayisiAltLimit >= talepEdilenNobetciSayisi)
                {
                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}";

                    var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                    var std = p.OrtamalaNobetSayisi;
                    var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                    var cns = Constraint.LessThanOrEqual(exp, std);
                    cns.LowerBound = 0;
                    p.Model.AddConstraint(cns, kisitAdi);
                }
                else
                {
                    var atlanacakGunSayisi = gunSayisi - (int)p.PespeseNobetSayisiAltLimit;

                    if (atlanacakGunSayisi < 0)
                    {
                        atlanacakGunSayisi = 0;
                    }

                    var pespeseGunler = p.Tarihler.Take(atlanacakGunSayisi).ToList();

                    var indis = (int)Math.Ceiling(p.PespeseNobetSayisiAltLimit) - 1;

                    foreach (var tarih in pespeseGunler)
                    {
                        var altLimit = tarih.Tarih;

                        var ustLimit = p.Tarihler[indis].Tarih;
                        //tarih.Tarih.AddDays(p.PespeseNobetSayisiAltLimit);
                        //+28 pazar günü gibi birşey olmalı
                        indis++;

                        var kisitTanim2 = $"{p.NobetUstGrupKisit.KisitTanim} "
                          + $"{altLimit.ToString("dd.MM.yy")}-{ustLimit.ToString("dd.MM.yy")}=>"
                          + $"{(int)p.PespeseNobetSayisiAltLimit} gün"
                          + $"{(p.GunKuralAdi == null ? "" : $"- {p.GunKuralAdi}")}";

                        var nobetGrupBilgisi2 = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                        var kisitAdi2 = IsimleriBirlestir(kisitTanim2, nobetGrupBilgisi2, p.EczaneNobetGrup.EczaneAdi);

                        var kararIndex3 = kararIndex
                            .Where(e => e.Tarih >= altLimit && e.Tarih <= ustLimit).ToList();

                        var std2 = 1;
                        var exp2 = Expression.Sum(kararIndex3.Select(i => p.KararDegiskeni[i]));
                        var cns2 = Constraint.LessThanOrEqual(exp2, std2);
                        cns2.LowerBound = 0;
                        p.Model.AddConstraint(cns2, kisitAdi2);
                    }
                }
            }
        }

        /// <summary>
        /// Eczanelere ay içinde peşpeşe nöbet yazılmasın
        /// </summary>
        /// <param name="p">KpHerAyPespeseGorev</param>
        public virtual void HerAyPespeseGorev(KpHerAyPespeseGorev p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {//PespeseNobetSayisi std değil ama bu kısıt için arayüzdeki std kullanılabilir.
                //Çünkü std hep 1 olacak zaten.

                //if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                //    p.PespeseNobetSayisi = (int)p.NobetUstGrupKisit.SagTarafDegeri;

                var tarihler = p.Tarihler.Take(p.Tarihler.Count - p.PespeseNobetSayisi).ToList();

                foreach (var tarih in tarihler)
                {
                    var altLimit = tarih.Tarih;
                    var ustLimit = tarih.Tarih.AddDays(p.PespeseNobetSayisi);

                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} "
                        + $"{altLimit.ToString("dd.MM.yy")}-{ustLimit.ToString("dd.MM.yy")}=>"
                        + $"{p.PespeseNobetSayisi} gün"
                        + $"";

                    var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                    var kararIndex = p.EczaneNobetTarihAralik
                                       .Where(e => (e.Tarih >= altLimit && e.Tarih <= ustLimit)).ToList();

                    var std = 1;
                    var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                    var cns = Constraint.LessThanOrEqual(exp, std);
                    cns.LowerBound = 0;
                    //var isTriviallyFeasible = cns.IsTriviallyFeasible();
                    p.Model.AddConstraint(cns, kisitAdi);
                }
            }
        }

        /// <summary>
        /// Bayramlarda sırayla (dini ise milli ya da tersi) peş peşe nöbet yazılır.
        /// </summary>
        /// <param name="p">BayramPespeseFarkliTurKisitParametreModel</param>
        public virtual void PespeseFarkliTurNobetYaz(KpPespeseFarkliTurNobet p)
        {//her_eczaneye_son_tuttugu_bayram_nobetinden_farkli_bayram_nobeti_yaz
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var ilgiliTarihler = p.Tarihler.Where(w => w.NobetGunKuralId == p.SonNobet.NobetGunKuralId).ToList();

                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} "
                     + $"son nöbet günü: {p.SonNobet.NobetGunKuralAdi}"
                     + $"";

                var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                var kararIndex = p.EczaneNobetTarihAralik
                    .Where(e => ilgiliTarihler.Select(s => s.TakvimId).Contains(e.TakvimId)
                             && e.NobetGorevTipId == p.SonNobet.NobetGorevTipId //sonradan ekledim. farklı görev tiplerini de içine alsın diye.14.03.2019
                             ).ToList();

                var std = 0;
                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                var cns = Constraint.Equals(exp, std);
                p.Model.AddConstraint(cns, kisitAdi);
            }
        }
        #endregion

        #region aynı gün nöbetler

        /// <summary>
        /// Eczane Grup Tanımdaki (eş grup) eczanelere aynı gün ya da aynı gün aralığında nöbet yazılmaz.
        /// </summary>
        /// <param name="p">KpEsGrubaAyniGunNobetYazma</param>
        public virtual void EsGruptakiEczanelereAyniGunNobetYazma(KpEsGrubaAyniGunNobetYazma p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var eczaneGrupTanimlar = p.EczaneGruplar
                    .Select(s => new
                    {
                        Id = s.EczaneGrupTanimId,
                        s.EczaneGrupTanimTipId,
                        s.EczaneGrupTanimAdi,
                        s.EczaneGrupTanimBitisTarihi,
                        s.EczaneGrupTanimPasifMi,
                        s.EczaneGrupTanimTipAdi,
                        s.ArdisikNobetSayisi,
                        s.AyniGunNobetTutabilecekEczaneSayisi,
                        s.NobetGorevTipAdi
                    }).Distinct().ToList();

                var eczaneGorevTipler = p.EczaneGruplar
                    .Select(s => new
                    {
                        s.NobetGorevTipId,
                        s.NobetGorevTipAdi
                    }).Distinct().ToList();

                var tarihAraligi = p.Tarihler
                    .Select(s => new
                    {
                        s.TakvimId,
                        s.Tarih
                    }).Distinct().ToList();

                if (tarihAraligi.Count > 0)
                {
                    foreach (var eczaneGrupTanim in eczaneGrupTanimlar)
                    {
                        var eczaneGruplar = p.EczaneGruplar
                                                .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id)
                                                .Select(s => new
                                                {
                                                    s.EczaneId,
                                                    s.EczaneNobetGrupId,
                                                    s.EczaneAdi,
                                                    s.EczaneGrupTanimTipId
                                                }).Distinct().ToList();

                        //var nobetGorevTipi = eczaneGorevTipler.Count > 1 ? eczaneGrupTanim.NobetGorevTipAdi : "";
                        var nobetGorevTipi = eczaneGrupTanim.NobetGorevTipAdi;

                        #region kontrol
                        var kontol = false;

                        if (kontol)
                        {
                            if (eczaneGrupTanim.Id == 143)//g.Adi.Contains("EŞ-SEMİH-SERRA BALTA"))
                            {
                            }

                            var kontrolEdilecekGruptakiEczaneler = new string[] { 
                                //"ADALET"
                                "DÜNYA"
                            };

                            if (eczaneGruplar.Where(w => kontrolEdilecekGruptakiEczaneler.Contains(w.EczaneAdi)).Count() > 0)
                            {
                            }
                        }
                        #endregion

                        var kararIndexMaster = p.EczaneNobetTarihAralik
                                               .Where(e => eczaneGruplar.Select(s => s.EczaneId).Contains(e.EczaneId)
                                                        && tarihAraligi.Select(s => s.TakvimId).Contains(e.TakvimId)
                                               ).ToList();

                        var gruptakiEczanelerinNobetTarihleri = new List<EczaneNobetSonucListe2>();

                        //çözülen dönemde aynı eş gruptaki eczanelerin mevcut nöbetleri - öncelikli çözüm
                        if (p.EczaneNobetSonuclar.Count > 0)
                        {
                            gruptakiEczanelerinNobetTarihleri = p.EczaneNobetSonuclar
                                .Where(w => eczaneGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)
                                         && tarihAraligi.Select(s => s.TakvimId).Contains(w.TakvimId)).ToList();
                        }

                        var ardisikNobetSayisi = eczaneGrupTanim.ArdisikNobetSayisi;
                        var stdVarsayilan = eczaneGrupTanim.AyniGunNobetTutabilecekEczaneSayisi;

                        if (eczaneGrupTanim.AyniGunNobetTutabilecekEczaneSayisi == 0)
                        {//örneğin giresun kent yakınında: herhangi 3 eczane aynı gün tutamaz
                            throw new Exception($"Aynı Gün Nöbet Tutabilecek Eczane Sayısı {eczaneGrupTanim.AyniGunNobetTutabilecekEczaneSayisi} olamaz. Lütfen {eczaneGrupTanim.EczaneGrupTanimAdi} eş grubunu kontrol ediniz.");
                            //stdVarsayilan = 1;
                        }

                        if (eczaneGruplar.Count > 0)
                        {
                            //ardisikNobetSayisi = 0 aynı grupta aynı nöbet grubunda aynı gün birden fazla nöbetçi bulunmadığında aynı nöbet grubundaki eş eczaneler için gereksizdir.
                            //ancak farklı gruptakiler için gereklidir.
                            if (ardisikNobetSayisi == 0)
                            {
                                foreach (var tarih in tarihAraligi)
                                {//ayni_es_gruptaki_eczaneler_ayni_gun_birlikte_nobet_tutmasin

                                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} "
                                      + $"{tarih.Tarih.ToString("dd.MM.yy-ddd.")}"
                                      + $" {p.NobetGrupGorevTipAdi}"
                                      + $"";

                                    var nobetGrupBilgisi = eczaneGrupTanim.EczaneGrupTanimAdi;

                                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGorevTipi, nobetGrupBilgisi);

                                    var kararIndex = kararIndexMaster
                                        .Where(e => e.TakvimId == tarih.TakvimId)
                                        .ToList();

                                    if (kararIndex.Count > 0)
                                    {
                                        var std = stdVarsayilan;
                                        var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                                        var cns = Constraint.LessThanOrEqual(exp, std);
                                        cns.LowerBound = 0;

                                        p.Model.AddConstraint(cns, kisitAdi);
                                    }
                                }

                                if (gruptakiEczanelerinNobetTarihleri.Count > 0)
                                {//farklı nöbet gruplardaki eşler ile aynı gün nöbet tutmamak için - öncelikli çözüm

                                    foreach (var tarih in gruptakiEczanelerinNobetTarihleri)
                                    {//ayni_es_gruptaki_eczaneler_ayni_gun_birlikte_nobet_tutmasin_oncelikli_cozum

                                        var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} "
                                            + $" Öncelikli çözüm, "
                                            + $"{tarih.Tarih.ToString("dd.MM.yy-ddd.")}"
                                            + $" {p.NobetGrupGorevTipAdi}"
                                            + $"";

                                        var nobetGrupBilgisi = eczaneGrupTanim.EczaneGrupTanimAdi;

                                        var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGorevTipi, nobetGrupBilgisi);

                                        var kararIndex = kararIndexMaster
                                            .Where(e => e.TakvimId == tarih.TakvimId)
                                            .ToList();

                                        if (kararIndex.Count > 0)
                                        {
                                            var std = 0;
                                            var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                                            var cns = Constraint.Equals(exp, std);
                                            cns.LowerBound = 0;

                                            p.Model.AddConstraint(cns, kisitAdi);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var tarihler = tarihAraligi.Take(tarihAraligi.Count() - ardisikNobetSayisi).ToList();

                                foreach (var tarih in tarihler)
                                {//ayni_es_gruptaki_eczaneler_ayni_gunlerde_birlikte_nobet_tutmasin

                                    var altLimit = tarih.Tarih;
                                    var ustLimit = tarih.Tarih.AddDays(ardisikNobetSayisi);

                                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} "
                                       + $"{altLimit.ToString("dd.MM.yy")} - {ustLimit.ToString("dd.MM.yy")}"
                                       + $"({(int)(ustLimit - altLimit).TotalDays} gün)"
                                       + $" {p.NobetGrupGorevTipAdi}"
                                       + $"";

                                    var nobetGrupBilgisi = eczaneGrupTanim.EczaneGrupTanimAdi;

                                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGorevTipi, nobetGrupBilgisi);

                                    var kararIndex = kararIndexMaster
                                        .Where(e => e.Tarih >= altLimit && e.Tarih <= ustLimit)
                                        .ToList();

                                    var std = stdVarsayilan;
                                    var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                                    var cns = Constraint.LessThanOrEqual(exp, std);
                                    cns.LowerBound = 0;

                                    p.Model.AddConstraint(cns, kisitAdi);
                                }

                                if (gruptakiEczanelerinNobetTarihleri.Count > 0)
                                {//farklı nöbet gruplardaki eşler ile aynı gün aralığında nöbet tutmamak için - öncelikli çözüm

                                    foreach (var tarih in gruptakiEczanelerinNobetTarihleri)
                                    {//ayni_es_gruptaki_eczaneler_ayni_gunlerde_birlikte_nobet_tutmasin_oncelikli_cozum

                                        var altLimit = tarih.Tarih.AddDays(-ardisikNobetSayisi);
                                        var ustLimit = tarih.Tarih.AddDays(ardisikNobetSayisi);

                                        var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} "
                                           + $"Öncelikli çözüm "
                                           + $"{altLimit.ToString("dd.MM.yy")} - {ustLimit.ToString("dd.MM.yy")} ({(int)(ustLimit - altLimit).TotalDays} gün)"
                                           + $" {p.NobetGrupGorevTipAdi}"
                                           + $"";

                                        var nobetGrupBilgisi = eczaneGrupTanim.EczaneGrupTanimAdi;

                                        var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGorevTipi, nobetGrupBilgisi);

                                        var kararIndex = kararIndexMaster
                                            .Where(e => e.Tarih >= altLimit && e.Tarih <= ustLimit)
                                            .ToList();

                                        var std = 0;
                                        var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                                        var cns = Constraint.Equals(exp, std);
                                        cns.LowerBound = 0;

                                        p.Model.AddConstraint(cns, kisitAdi);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Birden fazla nöbet türü yazılan günlerde 1 eczaneye sadece 1 tür nöbet yazılır.
        /// </summary>
        /// <param name="p">AyniGunSadece1NobetTuruKisitParametreModel</param>
        public virtual void BirEczaneyeAyniGunSadece1GorevYaz(KpAyniGunSadece1NobetTuru p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var tarihAraligi = p.Tarihler.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                foreach (var eczaneNobetGrup in p.EczaneNobetGruplar)
                {
                    foreach (var tarih in tarihAraligi)
                    {
                        var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} ["
                             + $"tarih: {tarih.Tarih.ToShortDateString()}"
                             + $"]";

                        var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(eczaneNobetGrup);

                        var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, eczaneNobetGrup.EczaneAdi);

                        var kararIndex = p.EczaneNobetTarihAralik
                            .Where(e => e.TakvimId == tarih.TakvimId
                                     && e.EczaneId == eczaneNobetGrup.EczaneId).ToList();

                        var std = 1;
                        var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                        var cns = Constraint.LessThanOrEqual(exp, std);
                        cns.LowerBound = 0;

                        p.Model.AddConstraint(cns, kisitAdi);
                    }
                }
            }
        }

        #endregion

        #region ay içinde sadece 1 kez aynı gün nöbet tutulsun

        /// <summary>
        /// Eczane ikilileri tarih aralığı içinde sadece 1 kez aynı gün nöbet tutsun
        /// </summary>
        /// <param name="p">AyIcindeSadece1KezAyniGunNobetKisitParametreModel</param>
        public virtual void AyIcindeSadece1KezAyniGunNobetTutulsun(KpAyIcindeSadece1KezAyniGunNobet p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var tarihAraligi = p.Tarihler.Select(s => new { s.TakvimId, TarihKisa = s.Tarih.ToShortDateString(), s.Tarih }).Distinct().ToArray();

                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}" +
                     $" 3 "
                     //+ $"{i}"
                     ;

                var tarihSayisi = tarihAraligi.Count();

                //var eczaneSayisi = p.EczaneNobetTarihAralik.Select(s => s.EczaneNobetGrupId).Distinct().Count();

                //var toplamIndis = p.EczaneNobetTarihAralik.Count();

                var eczaneler = p.IkiliEczaneler;//.ToArray();

                var pespeseGunBosGunSayisi = 3;

                var kisitlar = new List<Constraint>();

                var eczaneNobetTarihAralik = p.EczaneNobetTarihAralik.OrderBy(o => o.EczaneNobetGrupId).ThenBy(o => o.TakvimId).ToList();

                for (int j = 0; j < eczaneler.Count(); j++)
                {
                    //var ayniEsgruptalarMi = p.EczaneGruplar
                    //                .Where(e => (e.EczaneId == eczaneler[j].EczaneId1 || e.EczaneId == eczaneler[j].EczaneId2)).ToArray();

                    //var sayiKontrol = ayniEsgruptalarMi
                    //    .GroupBy(g => g.EczaneGrupTanimId)
                    //    .Select(s => new { s.Key, Sayi = s.Count() })
                    //    .Where(w => w.Sayi > 1).Count();

                    //if (sayiKontrol > 1)                    
                    //    continue;

                    var kararIndexIkiliEczane1 = eczaneNobetTarihAralik
                                    .Where(e => e.EczaneNobetGrupId == eczaneler[j].EczaneNobetGrupId1).ToArray();

                    var kararIndexIkiliEczane1Sayisi = kararIndexIkiliEczane1.Count();

                    var kararIndexIkiliEczane2 = eczaneNobetTarihAralik
                                    .Where(e => e.EczaneNobetGrupId == eczaneler[j].EczaneNobetGrupId2).ToArray();

                    var kararIndexIkiliEczane2Sayisi = kararIndexIkiliEczane2.Count();

                    var ikiliEczaneler = $"{eczaneler[j].EczaneAdi1}-{eczaneler[j].EczaneAdi2}";

                    if (kararIndexIkiliEczane1Sayisi + kararIndexIkiliEczane2Sayisi == tarihSayisi * 2)
                    {
                        for (int t1 = 0; t1 < tarihSayisi - pespeseGunBosGunSayisi; t1++)
                        {
                            for (int t2 = t1 + 1; t2 < tarihSayisi; t2++)
                            {
                                if (t2 <= t1 + pespeseGunBosGunSayisi)
                                    continue;

                                if (tarihAraligi[t1].Tarih.DayOfWeek == DayOfWeek.Sunday && tarihAraligi[t2].Tarih.DayOfWeek == DayOfWeek.Sunday)
                                    continue;

                                var ikiliTarihler = $"{tarihAraligi[t1].TarihKisa}-{tarihAraligi[t2].TarihKisa}";

                                var kisitAdi = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler);

                                var kararIndex11 = kararIndexIkiliEczane1[t1];
                                var kararIndex12 = kararIndexIkiliEczane1[t2];
                                //.Where(e => (e.TakvimId == tarihAraligi[t1].TakvimId || e.TakvimId == tarihAraligi[t2].TakvimId)).ToArray();

                                var kararIndex21 = kararIndexIkiliEczane2[t1];
                                var kararIndex22 = kararIndexIkiliEczane2[t2];

                                //var kararIndex2 = kararIndexIkiliEczane2
                                //    .Where(e => (e.TakvimId == tarihAraligi[t1].TakvimId || e.TakvimId == tarihAraligi[t2].TakvimId)).ToArray();

                                #region kontrol

                                //var kontrol = false;

                                //if (kontrol)
                                //{
                                //    var ss = new string[] { "AYDOĞAN", "SARE" };
                                //    var sayi = kararIndex.Where(w => w.EczaneAdi == "AYDOĞAN" || w.EczaneAdi == "NİSA").Count();

                                //    if (sayi == 4)
                                //    {
                                //    }
                                //}

                                #endregion

                                var std = 3;
                                var exp = p.KararDegiskeni[kararIndex11] + p.KararDegiskeni[kararIndex12] + p.KararDegiskeni[kararIndex21] + p.KararDegiskeni[kararIndex22];
                                //Expression.Sum(kararIndex2.Select(d => p.KararDegiskeni[d]));
                                var cns = Constraint.LessThanOrEqual(exp, std);
                                cns.LowerBound = 0;

                                kisitlar.Add(cns);
                                //p.Model.AddConstraint(cns, kisitAdi);
                            }
                        }
                    }
                    else
                    {
                    }
                }

                p.Model.AddConstraints(kisitlar);

                #region kontrol

                //var kontrol = false;

                //if (kontrol)
                //{
                //    var ss = new string[] { "AYDOĞAN", "SARE" };
                //    var sayi = kararIndex.Where(w => w.EczaneAdi == "AYDOĞAN" || w.EczaneAdi == "NİSA").Count();

                //    if (sayi == 4)
                //    {
                //    }
                //}

                #endregion

                //var pespeseGunBosGunSayisi = 3;

                //for (int e1 = 1; e1 <= eczaneSayisi - 1; e1++)
                //{
                //    for (int e2 = 0; e2 < eczaneSayisi - 2; e2++)
                //    {
                //        for (int t1 = 1; t1 <= tarihSayisi - 1; t1++)
                //        {
                //            for (int t2 = 2; t2 <= tarihSayisi; t2++)
                //            {
                //                if (t2 <= t1 + pespeseGunBosGunSayisi)
                //                    continue;

                //                var indis11 = e1 + (t1 - 1) + tarihSayisi * e2;
                //                var indis12 = indis11 + t2;

                //                var indis21 = indis11 + tarihSayisi;
                //                var indis22 = indis21 + t2;

                //                var kararIndex11 = p.EczaneNobetTarihAralik[indis11 - 1];
                //                var kararIndex12 = p.EczaneNobetTarihAralik[indis12 - 1];

                //                var kararIndex21 = p.EczaneNobetTarihAralik[indis21 - 1];
                //                var kararIndex22 = p.EczaneNobetTarihAralik[indis22 - 1];

                //                var ikiliEczaneler = $"{kararIndex11.EczaneAdi}-{kararIndex21.EczaneAdi}";
                //                var ikiliTarihler = $"{kararIndex11.Tarih.ToShortDateString()}-{kararIndex12.Tarih.ToShortDateString()}";

                //                var kisitAdi = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler);

                //                var std = 3;
                //                var exp = p.KararDegiskeni[kararIndex11] + p.KararDegiskeni[kararIndex12] + p.KararDegiskeni[kararIndex21] + p.KararDegiskeni[kararIndex22];
                //                var cns = Constraint.LessThanOrEqual(exp, std);
                //                cns.LowerBound = 0;

                //                p.Model.AddConstraint(cns, kisitAdi);
                //            }
                //        }
                //    }
                //}

                //var indis = 1;

                //foreach (var ikiliEczane in p.IkiliEczaneler)
                //{
                //    if (kararIndexIkiliEczaneler.Count > 0)
                //    {

                //        foreach (var tarih in tarihAraligi.Take(p.Tarihler.Count - 1))
                //        {
                //            var tarihler2 = tarihAraligi.Where(w => w.Tarih > tarih.Tarih).ToList();

                //            foreach (var tarih2 in tarihler2)
                //            {
                //                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}" +
                //                                 $" 3] {indis}";

                //                var ikiliEczaneler = $"{ikiliEczane.EczaneAdi1}-{ikiliEczane.EczaneAdi2}";
                //                var ikiliTarihler = $"{tarih.Tarih.ToShortDateString()}-{tarih2.Tarih.ToShortDateString()}";

                //                var kisitAdi = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler);

                //                var kararIndex = kararIndexIkiliEczaneler
                //                        .Where(e => e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId).ToList();

                //                #region kontrol
                //                var kontrol = false;

                //                if (kontrol)
                //                {
                //                    var ss = new string[] { "AYDOĞAN", "SARE" };
                //                    var sayi = kararIndex.Where(w => w.EczaneAdi == "AYDOĞAN" || w.EczaneAdi == "NİSA").Count();

                //                    if (sayi == 4)
                //                    {
                //                    }
                //                }
                //                #endregion

                //                var std = 3;
                //                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                //                var cns = Constraint.LessThanOrEqual(exp, std);
                //                cns.LowerBound = 0;

                //                p.Model.AddConstraint(cns, kisitAdi);

                //                indis++;
                //            }
                //        }
                //    }
                //}
            }
        }

        /// <summary>
        /// Eczane ikilileri tarih aralığı içinde sadece 1 kez aynı gün nöbet tutsun
        /// </summary>
        /// <param name="p">AyIcindeSadece1KezAyniGunNobetKisitParametreModel</param>
        public virtual void AyIcindeSadece1KezAyniGunNobetTutulsunEczaneBazli(KpAyIcindeSadece1KezAyniGunNobet p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var tarihAraligi = p.Tarihler
                    .Select(s => new { s.TakvimId, s.Tarih, s.GunGrupId, s.GunGrupAdi })
                    .Distinct()
                    .OrderBy(o => o.TakvimId).ToList();

                //var cs = new List<Constraint>();

                for (int t = 0; t < p.IkiliEczaneler.Count(); t++)
                {
                    #region kontrol

                    if (p.IkiliEczaneler[t].EczaneAdi1 == "GÖKALP" && p.IkiliEczaneler[t].EczaneAdi2 == "PINAR")
                    {
                    }

                    #endregion

                    var nobetGrupKurallar = p.NobetGrupKurallar
                        .Where(w => w.NobetGrupGorevTipId == p.IkiliEczaneler[t].NobetGrupGorevTipId1
                                 || w.NobetGrupGorevTipId == p.IkiliEczaneler[t].NobetGrupGorevTipId2).ToList();

                    var pespeseGunMax = GetArdisikBosGunSayisiMax(nobetGrupKurallar);

                    var kararIndexIkiliEczaneler = p.EczaneNobetTarihAralik
                             .Where(e => e.EczaneId == p.IkiliEczaneler[t].EczaneId1 || e.EczaneId == p.IkiliEczaneler[t].EczaneId2).ToList();

                    var ayniEsGruptaOlmaSayisi = GetAyniEsGruptaOlmaSayisi(p.EczaneGruplar, p.IkiliEczaneler[t]);

                    if (AyniEsGruptaOlmaSayisiSifirdanBuyukMu(ayniEsGruptaOlmaSayisi))
                        continue;

                    if (kararIndexIkiliEczaneler.Count > 0)
                    {
                        var ikiliEczaneler = $"{p.IkiliEczaneler[t].EczaneAdi1}-{p.IkiliEczaneler[t].EczaneAdi2}";

                        for (int i = 0; i < tarihAraligi.Count - 1 - pespeseGunMax; i++)
                        {
                            var tarihIlk = tarihAraligi[i].Tarih.ToShortDateString();

                            for (int j = i + pespeseGunMax; j < tarihAraligi.Count; j++)
                            {
                                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}" +
                                    $" {t}"; //.{i}-{j}";

                                if (tarihAraligi[i].GunGrupId == 1 && tarihAraligi[j].GunGrupId == 1)
                                    continue;

                                var ikiliTarihler = $"{tarihIlk}-{tarihAraligi[j].Tarih.ToShortDateString()}";

                                var kararIndex = kararIndexIkiliEczaneler
                                        .Where(e => e.TakvimId == tarihAraligi[i].TakvimId || e.TakvimId == tarihAraligi[j].TakvimId).ToList();

                                var kisitAdi = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler);

                                #region kontrol
                                var kontrol = false;

                                if (kontrol)
                                {
                                    var sayi = kararIndex.Where(w => w.EczaneAdi == "GÖKALP" && w.EczaneAdi == "PINAR").Count();

                                    if (sayi > 0)
                                    {
                                    }
                                }
                                #endregion

                                var std = 3;
                                var exp = Expression.Sum(kararIndex.Select(k => p.KararDegiskeni[k]));
                                var cns = Constraint.LessThanOrEqual(exp, std);
                                //cns. = kisitAdi;
                                cns.LowerBound = 0;
                                p.Model.AddConstraint(cns, kisitAdi);

                                //cs.Add(cns);
                            }
                        }

                        //foreach (var tarih in tarihAraligi.Take(p.Tarihler.Count - 1))
                        //{
                        //    var tarihler2 = tarihAraligi.Where(w => w.Tarih > tarih.Tarih).ToList();

                        //    foreach (var tarih2 in tarihler2)
                        //    {
                        //        var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}" +
                        //                         $" 3 {indis}";

                        //        var ikiliEczaneler = $"{ikiliEczane.EczaneAdi1}-{ikiliEczane.EczaneAdi2}";
                        //        var ikiliTarihler = $"{tarih.Tarih.ToShortDateString()}-{tarih2.Tarih.ToShortDateString()}";

                        //        var kisitAdi = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler);

                        //        var kararIndex = kararIndexIkiliEczaneler
                        //                .Where(e => e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId).ToList();

                        //        #region kontrol
                        //        var kontrol = true;

                        //        if (kontrol)
                        //        {
                        //            var ss = new string[] { "İSTİKAMET", "KURTOĞLU" };
                        //            var sayi = kararIndex.Where(w => w.EczaneAdi == "İSTİKAMET" || w.EczaneAdi == "KURTOĞLU").Count();

                        //            if (sayi > 0)
                        //            {
                        //            }
                        //        }
                        //        #endregion

                        //        var std = 3;
                        //        var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                        //        var cns = Constraint.LessThanOrEqual(exp, std);
                        //        cns.LowerBound = 0;

                        //        p.Model.AddConstraint(cns, kisitAdi);

                        //        indis++;
                        //    }
                        //}
                    }

                    //p.Model.AddConstraints(cs);
                }
            }
        }

        private static int GetArdisikBosGunSayisiMax(List<NobetGrupKuralDetay> nobetGrupKurallar)
        {
            var pespeseGunMax = 0;

            if (nobetGrupKurallar.Count > 1)
            {
                pespeseGunMax = (int)nobetGrupKurallar.Select(s => s.Deger).Max();
            }
            else
            {
                pespeseGunMax = (int)nobetGrupKurallar.SingleOrDefault().Deger;
            }

            return pespeseGunMax;
        }

        private List<EczaneGrupTanimSayisi> GetAyniEsGruptaOlmaSayisi(List<EczaneGrupDetay> eczaneGrupDetaylar, AyniGunTutulanNobetDetay ikiliEczane)
        {
            var eczaneGruplar = eczaneGrupDetaylar
                .Where(w => w.EczaneId == ikiliEczane.EczaneId1 || w.EczaneId == ikiliEczane.EczaneId2).ToList();

            var ayniEsGruptaOlmaSayisi = eczaneGruplar
                .Where(w => w.AyniGunNobetTutabilecekEczaneSayisi == 1)
                .GroupBy(g => new
                {
                    g.EczaneGrupTanimId,
                    g.EczaneGrupTanimAdi
                })
                .Select(s => new EczaneGrupTanimSayisi
                {
                    EczaneGrupTanimId = s.Key.EczaneGrupTanimId,
                    EczaneGrupTanimAdi = s.Key.EczaneGrupTanimAdi,
                    EczaneGrupTanimMiktari = s.Count()
                })
                .Where(w => w.EczaneGrupTanimMiktari > 1).ToList();

            return ayniEsGruptaOlmaSayisi;
        }

        private bool AyniEsGruptaOlmaSayisiSifirdanBuyukMu(List<EczaneGrupTanimSayisi> ayniEsGruptaOlmaSayisi)
        {
            var ayniEsGruptaOlmaSayisiSifirdanBuyukMu = false;

            if (ayniEsGruptaOlmaSayisi.Count > 0)
            {
                ayniEsGruptaOlmaSayisiSifirdanBuyukMu = ayniEsGruptaOlmaSayisi.Select(S => S.EczaneGrupTanimMiktari).Max() > 0;
            }

            return ayniEsGruptaOlmaSayisiSifirdanBuyukMu;
        }

        /// <summary>
        /// Eczane ikilileri tarih aralığı içinde sadece 1 kez aynı gün nöbet tutsun (değişken dönüşümlü)
        /// </summary>
        /// <param name="p"></param>
        public virtual void AyIcindeSadece1KezAyniGunNobetTutulsun(KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var tarihAraligi = p.Tarihler
                    .Select(s => new { s.TakvimId, s.Tarih, s.GunGrupId, s.GunGrupAdi })
                    .Distinct()
                    .OrderBy(o => o.TakvimId).ToArray();

                var indis = 0;

                foreach (var ikiliEczane in p.IkiliEczaneler)
                {
                    #region kontrol

                    //if (ikiliEczane.EczaneAdi1 == "NİSA" && ikiliEczane.EczaneAdi2 == "SU")
                    //{
                    //}

                    #endregion

                    var nobetGrupKurallar = p.NobetGrupKurallar
                        .Where(w => w.NobetGrupGorevTipId == ikiliEczane.NobetGrupGorevTipId1
                                 || w.NobetGrupGorevTipId == ikiliEczane.NobetGrupGorevTipId2).ToList();

                    var pespeseGunMax = GetArdisikBosGunSayisiMax(nobetGrupKurallar);

                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}";

                    var ikiliEczaneler = $"{ikiliEczane.EczaneAdi1}-{ikiliEczane.EczaneAdi2}";

                    var kisitAdiMaster = IsimleriBirlestir(kisitTanim, ikiliEczaneler, $"{indis} master");

                    var kararIndex3 = p.EczaneNobetTarihAralikIkiliEczaneler
                                .Where(e => e.AyniGunTutulanNobetId == ikiliEczane.Id).ToList();

                    var kararIndexIkiliEczaneler = p.EczaneNobetTarihAralik
                            .Where(e => (e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId1 || e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId2)).ToList();

                    var ayniEsGruptaOlmaSayisi = GetAyniEsGruptaOlmaSayisi(p.EczaneGruplar, ikiliEczane);

                    if (AyniEsGruptaOlmaSayisiSifirdanBuyukMu(ayniEsGruptaOlmaSayisi))
                        continue;

                    for (int i = 0; i < tarihAraligi.Length; i++)
                    {
                        var ikiliTarihler = $"{tarihAraligi[i].Tarih.ToShortDateString()}";

                        //var sonrakiGun = tarihAraligi[i + pespeseGunMax];

                        //if (tarihAraligi[i].GunGrupId == 1 && sonrakiGun.GunGrupId == 1)
                        //    continue;

                        var kisitAdiBuyuktur = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler, $"{indis} büyüktür");
                        var kisitAdiKucuktur = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler, $"{indis} küçüktür");

                        var kararIndex = kararIndexIkiliEczaneler
                            .Where(e => e.TakvimId == tarihAraligi[i].TakvimId).ToList();

                        if (kararIndex.Count == 2)
                        {
                            var kararIndex2 = kararIndex3
                                    .Where(e => e.TakvimId == tarihAraligi[i].TakvimId).FirstOrDefault();

                            #region kontrol
                            //var ss = new string[] { "AYDOĞAN", "SARE" };
                            //var sayi = kararIndex.Where(w => w.EczaneAdi == "AYDOĞAN" || w.EczaneAdi == "NİSA").Count();

                            //if (sayi == 4)
                            //{
                            //} 
                            #endregion

                            var std1 = 2 - 2 * (1 - p.KararDegiskeniIkiliEczaneler[kararIndex2]);
                            var exp1 = Expression.Sum(kararIndex.Select(k => p.KararDegiskeni[k]));
                            var cnsBuyuktur = Constraint.GreaterThanOrEqual(exp1, std1);
                            p.Model.AddConstraint(cnsBuyuktur, kisitAdiBuyuktur);

                            var std2 = 1 + 2 * p.KararDegiskeniIkiliEczaneler[kararIndex2];
                            var exp2 = Expression.Sum(kararIndex.Select(k => p.KararDegiskeni[k]));
                            var cnsKucuktur = Constraint.LessThanOrEqual(exp2, std2);
                            p.Model.AddConstraint(cnsKucuktur, kisitAdiKucuktur);

                            indis++;
                        }
                        else
                        {
                        }
                    }

                    //for (int i = 0; i < tarihAraligi.Length - pespeseGunMax; i++)
                    //{
                    //    var ikiliTarihler = $"{tarihAraligi[i].Tarih.ToShortDateString()}";

                    //    var sonrakiGun = tarihAraligi[i + pespeseGunMax];

                    //    //if (tarihAraligi[i].GunGrupId == 1 && sonrakiGun.GunGrupId == 1)
                    //    //    continue;

                    //    var kisitAdiBuyuktur = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler, $"{indis} büyüktür");
                    //    var kisitAdiKucuktur = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler, $"{indis} küçüktür");

                    //    var kararIndex = kararIndexIkiliEczaneler
                    //        .Where(e => e.TakvimId == tarihAraligi[i].TakvimId).ToList();

                    //    if (kararIndex.Count == 2)
                    //    {
                    //        var kararIndex2 = kararIndex3
                    //                .Where(e => e.TakvimId == sonrakiGun.TakvimId).FirstOrDefault();

                    //        #region kontrol
                    //        //var ss = new string[] { "AYDOĞAN", "SARE" };
                    //        //var sayi = kararIndex.Where(w => w.EczaneAdi == "AYDOĞAN" || w.EczaneAdi == "NİSA").Count();

                    //        //if (sayi == 4)
                    //        //{
                    //        //} 
                    //        #endregion

                    //        var std1 = 2 - 2 * (1 - p.KararDegiskeniIkiliEczaneler[kararIndex2]);
                    //        var exp1 = Expression.Sum(kararIndex.Select(k => p.KararDegiskeni[k]));
                    //        var cnsBuyuktur = Constraint.GreaterThanOrEqual(exp1, std1);
                    //        p.Model.AddConstraint(cnsBuyuktur, kisitAdiBuyuktur);

                    //        var std2 = 1 + 2 * p.KararDegiskeniIkiliEczaneler[kararIndex2];
                    //        var exp2 = Expression.Sum(kararIndex.Select(k => p.KararDegiskeni[k]));
                    //        var cnsKucuktur = Constraint.LessThanOrEqual(exp2, std2);
                    //        p.Model.AddConstraint(cnsKucuktur, kisitAdiKucuktur);

                    //        indis++;
                    //    }
                    //    else
                    //    {
                    //    }
                    //}

                    //foreach (var tarih in tarihAraligi)
                    //{
                    //    var ikiliTarihler = $"{tarih.Tarih.ToShortDateString()}";

                    //    var kisitAdiBuyuktur = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler, $"{indis} büyüktür");
                    //    var kisitAdiKucuktur = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler, $"{indis} küçüktür");

                    //    var kararIndex = kararIndexIkiliEczaneler
                    //        .Where(e => e.TakvimId == tarih.TakvimId).ToList();

                    //    if (kararIndex.Count == 2)
                    //    {
                    //        var kararIndex2 = kararIndex3
                    //                .Where(e => e.TakvimId == tarih.TakvimId).FirstOrDefault();

                    //        #region kontrol
                    //        //var ss = new string[] { "AYDOĞAN", "SARE" };
                    //        //var sayi = kararIndex.Where(w => w.EczaneAdi == "AYDOĞAN" || w.EczaneAdi == "NİSA").Count();

                    //        //if (sayi == 4)
                    //        //{
                    //        //} 
                    //        #endregion

                    //        var std1 = 2 - 2 * (1 - p.KararDegiskeniIkiliEczaneler[kararIndex2]);
                    //        var exp1 = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                    //        var cnsBuyuktur = Constraint.GreaterThanOrEqual(exp1, std1);
                    //        p.Model.AddConstraint(cnsBuyuktur, kisitAdiBuyuktur);

                    //        var std2 = 1 + 2 * p.KararDegiskeniIkiliEczaneler[kararIndex2];
                    //        var exp2 = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                    //        var cnsKucuktur = Constraint.LessThanOrEqual(exp2, std2);
                    //        p.Model.AddConstraint(cnsKucuktur, kisitAdiKucuktur);

                    //        indis++;
                    //    }
                    //    else
                    //    {
                    //    }
                    //}

                    var std3 = 1;
                    var exp3 = Expression.Sum(kararIndex3.Select(i => p.KararDegiskeniIkiliEczaneler[i]));
                    var cns = Constraint.LessThanOrEqual(exp3, std3);

                    cns.LowerBound = 0;
                    p.Model.AddConstraint(cns, kisitAdiMaster);

                    indis++;
                }
            }
        }

        /// <summary>
        /// Eczane ikilileri tarih aralığı içinde sadece 1 kez aynı gün nöbet tutsun
        /// </summary>
        /// <param name="p">AyIcindeSadece1KezAyniGunNobetKisitParametreModel</param>
        public virtual void AyIcindeSadece1KezAyniGunNobetTutulsunGiresunAltGrup(KpAyIcindeSadece1KezAyniGunNobetGiresunAltGrup p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var kisitAdi = $"K{p.NobetUstGrupKisit.KisitId} ({p.NobetUstGrupKisit.KisitKategorisi} {p.NobetUstGrupKisit.KisitAdiGosterilen}):"; //, {eczaneNobetGrup.EczaneAdi}";

                var tarihAraligi = p.Tarihler.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                var altGruplar = p.EczaneNobetGrupAltGrupDetaylar
                    .Select(s => new { s.NobetAltGrupId, s.NobetAltGrupAdi }).Distinct()
                    .OrderBy(o => o.NobetAltGrupId).ToList();

                //11 Şehir içi (merkez-1)
                //12 Şehir içi (merkez-2)
                //13 Şehir dışı (doğu + batı)

                foreach (var altGrup in altGruplar)
                {
                    foreach (var altGrup2 in altGruplar.Where(w => w.NobetAltGrupId > altGrup.NobetAltGrupId))
                    {
                        var sehirIci1dekiEczaneler = p.EczaneNobetGrupAltGrupDetaylar.Where(w => w.NobetAltGrupId == altGrup.NobetAltGrupId).ToList();
                        var digerAltGrup = altGruplar.Where(w => w.NobetAltGrupId == altGrup2.NobetAltGrupId).FirstOrDefault();

                        foreach (var sehirIci1dekiEczane in sehirIci1dekiEczaneler)
                        {
                            var sehirIci1dekiEczaneIleSehirIci2dekiEczaler = p.EczaneNobetGrupAltGrupDetaylar
                                .Where(w => w.EczaneNobetGrupId == sehirIci1dekiEczane.EczaneNobetGrupId
                                         || w.NobetAltGrupId == digerAltGrup.NobetAltGrupId).ToList();

                            foreach (var tarih in tarihAraligi.Take(p.Tarihler.Count - 1))
                            {
                                var tarihler2 = tarihAraligi.Where(w => w.Tarih > tarih.Tarih).ToList();

                                foreach (var tarih2 in tarihler2)
                                {
                                    var kararIndex = p.EczaneNobetTarihAralik
                                            .Where(e => (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId)
                                            && sehirIci1dekiEczaneIleSehirIci2dekiEczaler.Select(s => s.EczaneNobetGrupId).Contains(e.EczaneNobetGrupId)).ToList();

                                    #region kontrol
                                    var kontrol = false;

                                    if (kontrol)
                                    {
                                        var ss = new string[] { "AYDOĞAN", "SARE" };
                                        var sayi = kararIndex.Where(w => w.EczaneAdi == "AYDOĞAN" || w.EczaneAdi == "NİSA").Count();

                                        if (sayi == 4)
                                        {
                                        }
                                    }
                                    #endregion

                                    var std = 3;
                                    var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                                    var cns = Constraint.LessThanOrEqual(exp, std);
                                    cns.LowerBound = 0;

                                    p.Model.AddConstraint(cns, kisitAdi);
                                }
                            }

                        }
                    }
                }
            }
        }

        #endregion

        #region mazeret ve istekler

        /// <summary>
        /// Mazeret girilen tarihlere nöbet yazılır.
        /// </summary>
        /// <param name="p">KpMazereteGorevYazma</param>
        public virtual void MazereteGorevYazma(KpMazereteGorevYazma p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var indis = 0;

                foreach (var eczaneNobetMazeret in p.EczaneNobetMazeretler)
                {
                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} "
                     //+ $"m{indis}. " +
                     + $"{eczaneNobetMazeret.Tarih.ToString("dd.MM.yy-ddd.")} - {eczaneNobetMazeret.MazeretAdi}"
                     //+ $"{eczaneNobetMazeret.MazeretAdi}"
                     + $"";

                    var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(eczaneNobetMazeret);

                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, eczaneNobetMazeret.EczaneAdi);

                    var kararIndex = p.EczaneNobetTarihAralik
                        .Where(e => e.EczaneNobetGrupId == eczaneNobetMazeret.EczaneNobetGrupId
                                 && e.TakvimId == eczaneNobetMazeret.TakvimId).ToList();

                    var std = 0;
                    var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                    var cns = Constraint.Equals(exp, std);
                    p.Model.AddConstraint(cns, kisitAdi);

                    indis++;
                }
            }
        }

        /// <summary>
        /// İstek girilen tarihlere nöbet yazılır.
        /// </summary>
        /// <param name="p">KpHerAyPespeseGorev</param>
        public virtual void IstegiKarsila(KpIstegiKarsila p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                foreach (var eczaneNobetIstek in p.EczaneNobetIstekler)
                {
                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} "
                         + $"{eczaneNobetIstek.Tarih.ToString("dd.MM.yy-ddd.")} - {eczaneNobetIstek.IstekAdi}"
                         //+ $"{eczaneNobetIstek.IstekAdi}"
                         + $"";

                    var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(eczaneNobetIstek);

                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, eczaneNobetIstek.EczaneAdi);

                    var kararIndex = p.EczaneNobetTarihAralik
                        .Where(e => e.EczaneNobetGrupId == eczaneNobetIstek.EczaneNobetGrupId
                                 && e.TakvimId == eczaneNobetIstek.TakvimId).ToList();

                    if (kararIndex.Count > 0)
                    {
                        var std = 1;
                        var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                        var cns = Constraint.Equals(exp, std);
                        //var isTriviallyFeasible = cns.IsTriviallyFeasible();

                        p.Model.AddConstraint(cns, kisitAdi);
                    }
                }
            }
        }

        /// <summary>
        /// Bazı eczanelerin nöbet tutamayacağı günlerde nöbetçi olması engellenir.
        /// </summary>
        /// <param name="p">KpIstenenEczanelerinNobetGunleriniKisitla</param>
        public virtual void IstenenEczanelerinNobetGunleriniKisitla(KpIstenenEczanelerinNobetGunleriniKisitla p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var eczaneler = p.EczaneNobetTutamazGunler.Select(s => s.EczaneNobetGrupId).Distinct().ToList();

                foreach (var eczaneNobetGrup in eczaneler)
                {
                    var nobetTutamazGunler = p.EczaneNobetTutamazGunler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup).ToList();

                    var kisitAdi = $"K{p.NobetUstGrupKisit.KisitId} ({p.NobetUstGrupKisit.KisitKategorisi} {p.NobetUstGrupKisit.KisitAdiGosterilen}): {eczaneNobetGrup}";

                    var kararIndex = p.EczaneNobetTarihAralik
                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup
                                 && nobetTutamazGunler.Select(s => s.NobetGunKuralId).Contains(e.NobetGunKuralId)).ToList();

                    var std = 0;
                    var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                    var cns = Constraint.Equals(exp, std);
                    p.Model.AddConstraint(cns, kisitAdi);
                }
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Cumartesi günü 5 farklı bölgeden nöbet dağılımı olsun. (Giresun)
        /// </summary>
        /// <param name="p">KpGorevTipineGorevDagilim</param>
        public virtual void NobetGorevTipineGoreDagilimYap(KpGorevTipineGorevDagilim p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var tumGorevTiplerindeAyniGunNobetTutmayacakEczaneGruplar = new List<int> {
                    //cumartesi ana 5'li
                    249, //Cumartesi - doğu
                    250, //Cumartesi - batı
                    251, //Cumartesi - hastane karşısı
                    252, //Cumartesi - şehir içi-1
                    253, //Cumartesi - şehir içi-2

                    //h.içi ana 3'lü
                    //238, //Şehir içi (merkez-1)
                    //239, //Şehir dışı (doğu + batı)
                    //240, //Şehir içi (merkez-2)

                    //cumartesi, diğer
                    258, //Cumartesi - Toslu,Caner -Nesl.-Çağrı,Özçakır,Arda
                    254, //Cumartesi - Sanayi bölgesi
                    255, //Cumartesi - Ada hastanesi
                };

                var eczaneGrupTanimlar = p.EczaneGruplar
                    .Where(w => tumGorevTiplerindeAyniGunNobetTutmayacakEczaneGruplar.Contains(w.EczaneGrupTanimId))
                    .Select(s => new
                    {
                        s.EczaneGrupTanimId,
                        s.EczaneGrupTanimAdi
                    }).Distinct().ToList();

                var tarihAraligi = p.Tarihler
                    .Select(s => new
                    {
                        s.TakvimId,
                        s.Tarih
                    })
                    .Distinct().ToList();

                var kisitAdi = $"K{p.NobetUstGrupKisit.KisitId}, {p.NobetUstGrupKisit.KisitKategorisi}, {p.NobetUstGrupKisit.KisitAdiGosterilen}";

                foreach (var eczaneGrupTanim in eczaneGrupTanimlar)
                {
                    var eczaneGrupTanimdakiEczaneler = p.EczaneGruplar
                        .Where(w => w.EczaneGrupTanimId == eczaneGrupTanim.EczaneGrupTanimId)
                        .Select(s => s.EczaneId).ToList();

                    foreach (var tarih in tarihAraligi)
                    {
                        var kararIndex = p.EczaneNobetTarihAralik
                        .Where(e => e.TakvimId == tarih.TakvimId
                                 && eczaneGrupTanimdakiEczaneler.Contains(e.EczaneId)).ToList();

                        var std = 1;
                        var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                        var cns = Constraint.LessThanOrEqual(exp, std);
                        cns.LowerBound = 0;

                        p.Model.AddConstraint(cns, kisitAdi);
                    }
                }
            }
        }

        /// <summary>
        /// İstenen bir gruptaki eczaneler en son hangi alt grupla nöbet tuttuysa 
        /// çözülen ayda aynı alt grupla aynı gün nöbet yazılmaz.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="eczaneNobetTarihAralik"></param>
        /// <param name="eczaneNobetSonuclar"></param>
        /// <param name="eczaneNobetGruplar"></param>
        /// <param name="eczaneNobetGrupAltGruplar"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="nobetGrupGorevTip"></param>
        /// <param name="nobetUstGrupBaslamaTarihi"></param>
        /// <param name="bayramlar"></param>
        /// <param name="pazarGunleri"></param>
        /// <param name="haftaIciGunleri"></param>
        /// <param name="_x"></param>
        public virtual void AltGruplarlaSiraliNobetTutulsun(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            NobetGrupGorevTipDetay nobetGrupGorevTip,
            DateTime nobetUstGrupBaslamaTarihi,
            List<TakvimNobetGrup> bayramlar,
            List<TakvimNobetGrup> pazarGunleri,
            List<TakvimNobetGrup> haftaIciGunleri,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {//ay içinde gün grubu bazında birden fazla nöbet düşerse bu kısıt en son nöbet tuttuğu alt grubun dışındaki ile 2 kez nöbet tutabilir. buna dikkat.
            //öncelikli çözümde tuttuğu dikkate alınmıyor. 05/10/2018
            var ayniGunNobetTutmasiTakipEdilecekGruplar = new List<int>
                {
                    13//Antalya-10
                };

            if (!nobetUstGrupKisitDetay.PasifMi && ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(nobetGrupGorevTip.NobetGrupId))
            {
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

                var eczaneNobetTarihAralik2 = eczaneNobetTarihAralik
                    .Where(w => ayniGunNobetTutmasiTakipEdilecekGruplarTumu.Contains(w.NobetGrupId)).ToList();

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

                var gunGruplar = new List<string> { "Pazar", "Bayram", "Hafta İçi" };

                foreach (var gunGrup in gunGruplar)
                {
                    var tarihAralik = gunGrup == "Pazar"
                      ? pazarGunleri
                      : gunGrup == "Bayram"
                      ? bayramlar
                      : haftaIciGunleri;

                    var nobetAltGrubuOlmayanlarinSonuclariGunGruplu = nobetAltGrubuOlmayanlarinSonuclari.Where(w => w.GunGrupAdi == gunGrup).ToList();
                    var nobetAltGrubuOlanlarinSonuclariGunGruplu = nobetAltGrubuOlanlarinSonuclari.Where(w => w.GunGrupAdi == gunGrup).ToList();
                    var ayniGunAnahtarListeGunGruplu = ayniGunAnahtarListe.Where(w => w.GunGrup == gunGrup).ToList();

                    foreach (var eczane in nobetAltGrubuOlmayanlarinEczaneleri)
                    {
                        #region kontrol

                        var kontrol = false;

                        var kontrolEdilecekEczaneler = new string[] { "ALYA" };

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
                                                                         GunGrup = g1.GunGrupAdi
                                                                     }).ToList();

                        if (gunGrup != "Hafta İçi")
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

                            //bakılan eczane
                            var birlikteNobetTutmayacakEczaneler = new List<int> { eczane.Id };

                            //alt gruptaki eczaneler
                            altGruptakiEczaneler.ForEach(x => birlikteNobetTutmayacakEczaneler.Add(x.EczaneNobetGrupId));

                            foreach (var tarih in tarihAralik)
                            {//en son hangi alt grupla tuttuysa çözülen ayda aynı alt grupla aynı gün nöbet tutma
                                var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczane.EczaneAdi}";

                                var kararIndex = eczaneNobetTarihAralik2 //data.EczaneNobetTarihAralik
                                        .Where(e => birlikteNobetTutmayacakEczaneler.Contains(e.EczaneNobetGrupId)
                                                && e.TakvimId == tarih.TakvimId).ToList();

                                var std = 1;
                                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                var cns = Constraint.LessThanOrEqual(exp, std);
                                model.AddConstraint(cns, kisitAdi);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Nöbet grubu ile alt grup arasındaki nöbetin dağılımı
        /// </summary>
        /// <param name="p"></param>
        public void AltGruplarlaAyniGunNobetGrupAltGrup(KpAltGruplarlaAyniGunNobetGrupAltGrup p)
        {
            var altGruplarlaAyniGunNobetTutma = p.NobetUstGrupKisit;

            if (!altGruplarlaAyniGunNobetTutma.PasifMi)
            {
                var tarihler = p.Tarihler.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                foreach (var nobetGrupGorevTip in p.NobetGrupGorevTipler)
                {
                    //if (nobetGrupGorevTip.Id == 53)
                    //    continue;

                    var eczaneNobetGruplar = p.EczaneNobetGruplar
                        .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                    var nobetAltGruplar = p.AyniGunNobetTakipGrupAltGruplar
                        .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                    var eczaneNobetSonuclar = p.EczaneNobetSonuclar
                        .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                 && w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi).ToList();

                    foreach (var nobetAltGrup in nobetAltGruplar)
                    {
                        //if (nobetAltGrup.NobetAltGrupId == 44)
                        //continue;

                        var altGruplaAyniGunGecmisNobetSayilari = p.AltGrupIleTutulanNobetDurumlari
                            .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                     && w.NobetAltGrupId == nobetAltGrup.NobetAltGrupId).ToList();

                        var altGruptakiEczaneninSonuclari = p.EczaneNobetSonuclar
                            .Where(w => nobetAltGrup.NobetAltGrupId == w.NobetAltGrupId
                                     && w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi).ToList();

                        var birlikteNobetTutulmayacakAltGruptakiEczaneler = p.EczaneNobetGrupAltGruplar
                            .Where(w => w.NobetAltGrupId == nobetAltGrup.NobetAltGrupId).ToList();

                        foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                        {
                            var birlikteNobetTutmayacakEczaneler = new List<int>();

                            birlikteNobetTutulmayacakAltGruptakiEczaneler.ForEach(x => birlikteNobetTutmayacakEczaneler.Add(x.EczaneNobetGrupId));

                            #region kontrol

                            var kontrol = false;

                            if (kontrol)
                            {
                                var kontrolEdilecekEczaneler = new string[] {
                                    "TURGUT",
                                    "PEHLİVAN",
                                    "ÖZEN"//çarşı
                                    //"NİLGÜN ÖNCEL"
                                };

                                if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                                {
                                }
                            }

                            #endregion

                            birlikteNobetTutmayacakEczaneler.Add(eczaneNobetGrup.Id);

                            var bakilanEczaneninSonuclari = eczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                            var bakilanEczaneninToplamNobetSayisi = bakilanEczaneninSonuclari.Count();

                            var bolum = Math.Round((double)bakilanEczaneninToplamNobetSayisi / nobetAltGruplar.Count(), 0);

                            var altGrupIleTutulacakNobetSayisiUstLimiti = bolum + 1;

                            if (nobetAltGrup.KumulatifToplamNobetSayisi > 0)
                            {
                                if (altGruplarlaAyniGunNobetTutma.SagTarafDegeri > 0)
                                {
                                    altGrupIleTutulacakNobetSayisiUstLimiti = altGruplarlaAyniGunNobetTutma.SagTarafDegeri;
                                }
                                else
                                {
                                    altGrupIleTutulacakNobetSayisiUstLimiti = nobetAltGrup.KumulatifToplamNobetSayisi;
                                }
                            }
                            else
                            {
                                altGrupIleTutulacakNobetSayisiUstLimiti = 2;
                            }

                            var altGruplaAyniGunTutulanNobetler = (from g1 in bakilanEczaneninSonuclari
                                                                   from g2 in altGruptakiEczaneninSonuclari
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

                            #region bir eczane bir grup ile aynı gün nöbet tutarsa, (tarih aralığı içinde ve kümülatif en fazla)

                            var kisitAdiBase = $"{altGruplarlaAyniGunNobetTutma.KisitTanim} {nobetAltGrup.NobetGrupAdi}, {eczaneNobetGrup.EczaneAdi} ";

                            var kisitAdiAltGrupIliski = $"{kisitAdiBase} eczanesi {nobetAltGrup.NobetAltGrupAdi} ";

                            var kisitAdiAltGrupIliskiUstLimit = $"{kisitAdiBase} eczanesinin {nobetAltGrup.NobetAltGrupAdi} ile birlikte tutacağı en fazla nöbet sayısı: ";

                            var eczaneNobetAltGrupTarihAralikEczaneVeAltGrupBazli = p.EczaneNobetAltGrupTarihAralik
                                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && e.NobetGrupAltId == nobetAltGrup.NobetAltGrupId).ToList();

                            var eczaneNobetTarihAralikIlgiliEczaneler = p.EczaneNobetTarihAralik
                                  .Where(e => birlikteNobetTutmayacakEczaneler.Contains(e.EczaneNobetGrupId)).ToList();

                            #region bakılan eczane ve ilgili alt grup aynı gün nöbetçi olursa

                            foreach (var tarih in tarihler)
                            {
                                var kisitAdi = $" {kisitAdiAltGrupIliski} ile birlikte {tarih.Tarih.ToShortDateString()} tarihinde nöbet tuttu mu?";

                                var kisitAdiBuyuktur = kisitAdi + " &ge;";

                                var kararIndex2 = eczaneNobetAltGrupTarihAralikEczaneVeAltGrupBazli
                                    .Where(e => e.TakvimId == tarih.TakvimId).FirstOrDefault();

                                var kararIndex = eczaneNobetTarihAralikIlgiliEczaneler
                                    .Where(e => e.TakvimId == tarih.TakvimId).ToList();

                                var stdBuyuktur = 2 - 2 * (1 - p.KararDegiskeni2[kararIndex2]);
                                var expBuyuktur = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                                var cnsBuyuktur = Constraint.GreaterThanOrEqual(expBuyuktur, stdBuyuktur);
                                p.Model.AddConstraint(cnsBuyuktur, kisitAdiBuyuktur);

                                var kisitAdiKucuktur = kisitAdi + " &le;";

                                var stdKucuktur = 1 + 2 * p.KararDegiskeni2[kararIndex2];
                                var expKucuktur = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                                var cnsKucuktur = Constraint.LessThanOrEqual(expKucuktur, stdKucuktur);
                                p.Model.AddConstraint(cnsKucuktur, kisitAdiKucuktur);
                            }

                            #endregion

                            var altGruplabirlikteTutulanNobetler = altGruplaAyniGunTutulanNobetler
                                .Where(w => w.NobetAltGrupId == nobetAltGrup.NobetAltGrupId).ToList();

                            var altGruplaBirlikteNobetSayisi = altGruplabirlikteTutulanNobetler.Count();

                            if (altGrupIleTutulacakNobetSayisiUstLimiti - altGruplaBirlikteNobetSayisi < 2)
                            {
                                //altGrupIleTutulacakNobetSayisiUstLimiti = 2;
                            }

                            var ayIciSinir = true;

                            if (ayIciSinir)
                            {
                                var ayIcındekiAltGruplaBirlikteNobetSayisi = 1;

                                var kisitAdiTarihAraligi = $"{kisitAdiAltGrupIliskiUstLimit} {ayIcındekiAltGruplaBirlikteNobetSayisi} - tarih araligi içinde";

                                var kararIndex = eczaneNobetAltGrupTarihAralikEczaneVeAltGrupBazli;

                                var stdTarihAraligi = ayIcındekiAltGruplaBirlikteNobetSayisi;
                                var expTarihAraligi = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni2[i]));
                                var cnsTarihAraligi = Constraint.LessThanOrEqual(expTarihAraligi, stdTarihAraligi);
                                p.Model.AddConstraint(cnsTarihAraligi, kisitAdiTarihAraligi);
                            }

                            var tumZamanlar = true;

                            if (tumZamanlar)
                            {
                                //if (nobetGrup.Id == 22)                                
                                //    altGrupIleTutulacakNobetSayisiUstLimiti = 2;   

                                var nobetDurumuGecmis = altGruplaAyniGunGecmisNobetSayilari.SingleOrDefault(x => x.EczaneNobetGrupId == eczaneNobetGrup.Id);

                                var nobetSayisiGecmis = nobetDurumuGecmis == null ? 0 : nobetDurumuGecmis.NobetSayisi;

                                var gecmisNobetSayisiToplam = altGruplaBirlikteNobetSayisi + nobetSayisiGecmis;

                                var stdFarki = altGrupIleTutulacakNobetSayisiUstLimiti - gecmisNobetSayisiToplam;

                                var stdUstLimit = stdFarki;

                                if (stdFarki < 0)
                                {
                                    stdUstLimit = 0;
                                }
                                else
                                {
                                    stdUstLimit = stdFarki;
                                }

                                var kisitAdiKumulatif = $"{kisitAdiAltGrupIliskiUstLimit} {stdUstLimit} - kümülatif toplam";

                                var kararIndex = eczaneNobetAltGrupTarihAralikEczaneVeAltGrupBazli;

                                var stdKumulatif = stdUstLimit;
                                var expKumulatif = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni2[i]));
                                var cnsKumulatif = Constraint.LessThanOrEqual(expKumulatif, stdKumulatif);
                                p.Model.AddConstraint(cnsKumulatif, kisitAdiKumulatif);
                            }

                            #endregion
                        }
                    }
                }
            }
        }

        #region yardımcı metotlar
        public void TalepleriTakvimeIsle(List<NobetGrupTalepDetay> nobetGrupTalepler, int varsayilanGunlukNobetciSayisi, List<TakvimNobetGrup> tarihler)
        {
            if (nobetGrupTalepler.Count > 0)
            {
                foreach (var tarih in tarihler)
                {
                    var talepFarkli = nobetGrupTalepler
                       .Where(s => s.NobetGrupGorevTipId == tarih.NobetGrupGorevTipId
                                && s.TakvimId == tarih.TakvimId).SingleOrDefault();

                    if (talepFarkli != null)
                    {
                        tarih.TalepEdilenNobetciSayisi = talepFarkli.NobetciSayisi;
                    }
                    else
                    {
                        tarih.TalepEdilenNobetciSayisi = varsayilanGunlukNobetciSayisi;
                    }
                }
            }
            else
            {
                foreach (var tarih in tarihler)
                {
                    tarih.TalepEdilenNobetciSayisi = varsayilanGunlukNobetciSayisi;
                }
            }

        }

        public virtual double OrtalamaNobetSayisi(int gunlukNobetciSayisi, int gruptakiNobetciSayisi, int gunSayisi)
        {
            return Math.Ceiling(((double)gunSayisi * gunlukNobetciSayisi) / gruptakiNobetciSayisi);
        }

        public virtual double OrtalamaNobetSayisi(int talepEdilenToplamNobetciSayisi, int gruptakiNobetciSayisi)
        {
            return Math.Ceiling(((double)talepEdilenToplamNobetciSayisi) / gruptakiNobetciSayisi);
        }

        public virtual NobetUstGrupKisitDetay NobetUstGrupKisit(List<NobetUstGrupKisitDetay> nobetUstGrupKisitlar, string kisitAdi, int nobetUstGrupId)
        {
            var nobetUstGrupKisit = new NobetUstGrupKisitDetay()
            {
                PasifMi = true
            };

            return nobetUstGrupKisitlar.SingleOrDefault(s => s.KisitAdi == kisitAdi
                                                         && s.NobetUstGrupId == nobetUstGrupId) ?? nobetUstGrupKisit;
        }

        public virtual NobetUstGrupKisitDetay NobetUstGrupKisit(List<NobetUstGrupKisitDetay> nobetUstGrupKisitlar, string kisitAdiKisa)
        {
            var kisitId = Convert.ToInt32(kisitAdiKisa.Remove(0, 1));

            var nobetUstGrupKisit = nobetUstGrupKisitlar.SingleOrDefault(s => s.KisitId == kisitId) ?? new NobetUstGrupKisitDetay() { PasifMi = true };

            return nobetUstGrupKisit;
        }

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

        public int GetToplamGunKuralNobetSayisi(EczaneNobetGrupGunKuralIstatistikYatay eczaneNobetIstatistik, int nobetGunKuralId)
        {
            var toplamNobetSayisi = 0;

            switch (nobetGunKuralId)
            {
                case 1:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;
                    break;
                case 2:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazartesi;
                    break;
                case 3:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiSali;
                    break;
                case 4:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCarsamba;
                    break;
                case 5:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPersembe;
                    break;
                case 6:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma;
                    break;
                case 7:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;
                    break;
                case 8:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiDiniBayram;
                    break;
                case 9:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiMilliBayram;
                    break;
                case 10:
                    toplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiArife;
                    break;
                default:
                    break;
            }

            return toplamNobetSayisi;
        }

        public List<NobetGunKuralNobetSayisi> GetNobetGunKuralNobetSayilari(List<TakvimNobetGrupGunDegerIstatistik> nobetGunKuralIstatistikler,
            EczaneNobetGrupGunKuralIstatistikYatay eczaneNobetIstatistik)
        {
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

            return nobetGunKuralNobetSayilari;
        }
        public class NobetGunKuralDetay
        {
            public List<int> FazlaNobetTutacakEczaneler { get; set; }
            public double KuralSTD { get; set; }
            public double GunKuralId { get; set; }
            public double OrtalamaNobetSayisi { get; set; }
            public double EnCokNobetTutulanNobetSayisi { get; set; }
        }

        public NobetGunKuralDetay GetNobetGunKural(int gunKuralId, AntalyaMerkezDataModel data, int nobetGrupId, int gruptakiNobetciSayisi, int cozulenAydakiNobetSayisi)
        {
            var r = new Random();

            var eczaneler = data.EczaneNobetIstatistikler
                  .Where(w => w.NobetGrupId == nobetGrupId)
                  .OrderBy(x => r.NextDouble()).ToList();

            //eczanelerin nöbet sayıları
            var nobetTutanEczaneler = data.EczaneNobetSonuclar
                .Where(w => w.NobetGunKuralId == gunKuralId
                         && w.NobetGrupId == nobetGrupId)
                .GroupBy(g => g.EczaneNobetGrupId)
                .Select(s => new
                {
                    EczaneNobetGrupId = s.Key,
                    NobetSayisi = s.Count(),
                    EnSonNobetTuttuguAy = s.Max(c => c.Tarih.Month)
                }).ToList();

            var oncekiAydakiNobetSayisi = nobetTutanEczaneler.Select(s => s.NobetSayisi).Sum();
            var toplamNobetSayisi = oncekiAydakiNobetSayisi + cozulenAydakiNobetSayisi;
            var ortalamaNobetSayisi = (double)(toplamNobetSayisi) / gruptakiNobetciSayisi;

            var kuralStd = Math.Ceiling(ortalamaNobetSayisi);

            var tumEczaneler = eczaneler
                .Select(s => new
                {
                    s.EczaneNobetGrupId,
                    NobetSayisi = nobetTutanEczaneler
                        .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId)
                        .Select(f => f.NobetSayisi).FirstOrDefault(),
                    EnSonNobetTuttuguAy = nobetTutanEczaneler
                        .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId)
                        .Select(f => f.EnSonNobetTuttuguAy).FirstOrDefault()
                }).ToList();

            var eczaneNobetIstatistik = tumEczaneler//nobetTutanEczaneler
                .Where(w => w.NobetSayisi == kuralStd - 1)
                .Select(s => s.EczaneNobetGrupId).ToList();

            var cozulenAydakiEksikNobetiOlanSayisi = cozulenAydakiNobetSayisi - eczaneNobetIstatistik.Count;//nobetTutanEczaneNobetGruplar.Count();

            if (cozulenAydakiEksikNobetiOlanSayisi > 0) kuralStd++;

            var fazlaNobetTutacakEczaneler = new List<int>();

            double enCokNobetTutulanNobetSayisi;

            //periyot başlamadan buraya girmemeli (nobetTutanEczaneler.Count() == 0)
            //if (data.Ay == 1) ayicindeEnCokNobetTutanEczaneSayisi = 100;

            if (nobetTutanEczaneler.Count() > 0)
            {
                enCokNobetTutulanNobetSayisi = nobetTutanEczaneler.Select(s => s.NobetSayisi).Max();
            }
            else
            {
                enCokNobetTutulanNobetSayisi = 100;
            }

            //hiç nöbet tutan yoksa bu işlem yapılmaz
            if (enCokNobetTutulanNobetSayisi < kuralStd)
            {
                var nobetiEksikEczaneNobetGruplar = eczaneler
                    .Where(w => !eczaneNobetIstatistik.Contains(w.EczaneNobetGrupId));//!nobetTutanEczaneNobetGruplar.Contains(w.EczaneNobetGrupId));

                var fazlaNobetTutacakEczaneSayisi = cozulenAydakiNobetSayisi - nobetiEksikEczaneNobetGruplar.Count();

                if (fazlaNobetTutacakEczaneSayisi > 0)
                {
                    if (gunKuralId == 1)
                    {
                        var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;

                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1
                                 && w.EnSonNobetTuttuguAy < (data.Ay - pazarNobetiYazilabilecekIlkAy)//5
                                 )
                        .OrderBy(o => o.EnSonNobetTuttuguAy)
                        //.OrderBy(o => r.NextDouble())
                        .Select(s => s.EczaneNobetGrupId).Take(fazlaNobetTutacakEczaneSayisi).ToList();
                    }
                    else
                    {
                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1)
                        .OrderBy(o => r.NextDouble())
                        .Select(s => s.EczaneNobetGrupId).Take(fazlaNobetTutacakEczaneSayisi).ToList();
                    }
                }

                if (cozulenAydakiEksikNobetiOlanSayisi > 0)
                {
                    if (gunKuralId == 1)
                    {
                        var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;

                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1
                                 && w.EnSonNobetTuttuguAy < (data.Ay - pazarNobetiYazilabilecekIlkAy)
                                 )
                        .OrderBy(o => o.EnSonNobetTuttuguAy)
                        //.OrderBy(o => r.NextDouble())
                        .Select(s => s.EczaneNobetGrupId).Take(cozulenAydakiEksikNobetiOlanSayisi).ToList();
                    }
                    else
                    {
                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1)
                        .OrderBy(o => r.NextDouble())
                        .Select(s => s.EczaneNobetGrupId).Take(cozulenAydakiEksikNobetiOlanSayisi).ToList();
                    }

                }
            }

            var nobetGunKuralDetay = new NobetGunKuralDetay
            {
                FazlaNobetTutacakEczaneler = fazlaNobetTutacakEczaneler,
                KuralSTD = kuralStd,
                GunKuralId = gunKuralId,
                OrtalamaNobetSayisi = ortalamaNobetSayisi,
                EnCokNobetTutulanNobetSayisi = enCokNobetTutulanNobetSayisi
            };

            return nobetGunKuralDetay;
        }

        public NobetGunKuralDetay GetNobetGunKuralHaftaIciToplam(AntalyaMerkezDataModel data, int nobetGrupId, int gruptakiNobetciSayisi, int cozulenAydakiNobetSayisi)
        {
            var r = new Random();

            var eczaneler = data.EczaneNobetIstatistikler
                  .Where(w => w.NobetGrupId == nobetGrupId)
                  .OrderBy(x => r.NextDouble()).ToList();

            //eczanelerin nöbet sayıları
            var nobetTutanEczaneler = data.EczaneNobetSonuclar
                .Where(w => (w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 7)
                          && w.NobetGrupId == nobetGrupId
                          && eczaneler.Select(s => s.EczaneNobetGrupId).Contains(w.EczaneNobetGrupId)
                         )
                .GroupBy(g => g.EczaneNobetGrupId)
                .Select(s => new
                {
                    EczaneNobetGrupId = s.Key,
                    NobetSayisi = s.Count(),
                    EnSonNobetTuttuguAy = s.Max(c => c.Tarih.Month)
                }).ToList();

            var oncekiAydakiNobetSayisi = nobetTutanEczaneler.Select(s => s.NobetSayisi).Sum();
            var toplamNobetSayisi = oncekiAydakiNobetSayisi + cozulenAydakiNobetSayisi;
            var ortalamaNobetSayisi = (double)(toplamNobetSayisi) / gruptakiNobetciSayisi;

            var kuralStd = Math.Ceiling(ortalamaNobetSayisi);

            var tumEczaneler = eczaneler
                .Select(s => new
                {
                    s.EczaneNobetGrupId,
                    NobetSayisi = nobetTutanEczaneler
                        .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId)
                        .Select(f => f.NobetSayisi).SingleOrDefault(),
                    EnSonNobetTuttuguAy = nobetTutanEczaneler
                        .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId)
                        .Select(f => f.EnSonNobetTuttuguAy).SingleOrDefault()
                }).ToList();

            var nobetFrekans = tumEczaneler
                .GroupBy(g => g.NobetSayisi)
                .Select(s => new
                {
                    NobetSayisi = s.Key,
                    EczaneSayisi = s.Count()
                }).ToList();

            var eczaneNobetIstatistik = tumEczaneler
                .Where(w => w.NobetSayisi < kuralStd)
                .Select(s => s.EczaneNobetGrupId).ToList();

            var cozulenAydakiEksikNobetiOlanSayisi = eczaneNobetIstatistik.Count; //cozulenAydakiNobetSayisi - eczaneNobetIstatistik.Count;

            if (cozulenAydakiNobetSayisi - eczaneNobetIstatistik.Count < 1)
            {
                cozulenAydakiEksikNobetiOlanSayisi = cozulenAydakiNobetSayisi;
            }
            else
            {
                //gruptakiNobetciSayisi != 29
                var bakilmayacakNobetGruplar = new int[3] { 3 + 7, 3 + 9, 3 + 10 };
                if (!bakilmayacakNobetGruplar.Contains(nobetGrupId))
                {
                    kuralStd++;
                }
            }

            if (cozulenAydakiEksikNobetiOlanSayisi == 0) kuralStd++;

            var fazlaNobetTutacakEczaneler = new List<int>();

            double enCokNobetTutulanNobetSayisi;

            //periyot başlamadan buraya girmemeli (nobetTutanEczaneler.Count() == 0)
            if (nobetTutanEczaneler.Count() > 0)
            {
                enCokNobetTutulanNobetSayisi = nobetTutanEczaneler.Select(s => s.NobetSayisi).Max();
            }
            else
            {
                enCokNobetTutulanNobetSayisi = 100;
            }

            //hiç nöbet tutan yoksa bu işlem yapılmaz
            if (enCokNobetTutulanNobetSayisi <= kuralStd)
            {
                var nobetiEksikEczaneler = eczaneler
                    .Where(w => !eczaneNobetIstatistik.Contains(w.EczaneNobetGrupId)).ToList();//!nobetTutanEczaneNobetGruplar.Contains(w.EczaneNobetGrupId));

                var hicTutmayanlar = new List<int>();
                var eksigiOlanlar = new List<int>();

                if (nobetFrekans.Count() > 1)
                {
                    hicTutmayanlar = tumEczaneler
                            .Where(w => w.NobetSayisi == nobetFrekans.Select(s => s.NobetSayisi).Min())
                            .Select(s => s.EczaneNobetGrupId).ToList();

                    hicTutmayanlar.ForEach(x => eksigiOlanlar.Add(x));
                }

                if (cozulenAydakiEksikNobetiOlanSayisi > 0)
                {
                    tumEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 2)
                        .Select(s => s.EczaneNobetGrupId).ForEach(x => eksigiOlanlar.Add(x));

                }

                var eksikler = eksigiOlanlar.Distinct();
                var fazlaNobetTutacakEczaneSayisi = cozulenAydakiNobetSayisi - eksikler.Count();

                //10.grup 3. ayda açılıyor
                //var nobetTutacakYeterliEczaneSayisi = cozulenAydakiNobetSayisi - (eksikler.Count() * 2) - tumEczaneler
                //                                            .Where(w => w.NobetSayisi == kuralStd - 1)
                //                                            .Select(s => s.EczaneNobetGrupId).Count();

                if (fazlaNobetTutacakEczaneSayisi > 0
                    //&& nobetTutacakYeterliEczaneSayisi > 0
                    )
                {
                    if (nobetGrupId == 4)
                    {
                        fazlaNobetTutacakEczaneler = tumEczaneler
                            .Where(w => w.NobetSayisi == nobetFrekans.Select(s => s.NobetSayisi).Max()
                                     && !eksigiOlanlar.Contains(w.EczaneNobetGrupId))
                            .OrderBy(o => r.NextDouble())
                            .Select(s => s.EczaneNobetGrupId)
                            .Take(fazlaNobetTutacakEczaneSayisi).ToList();
                    }
                    else
                    {
                        fazlaNobetTutacakEczaneler = tumEczaneler
                            .Where(w => w.NobetSayisi == nobetFrekans.Select(s => s.NobetSayisi).Max()
                                     //&& !eksikler.Contains(w.EczaneNobetGrupId)
                                     )
                            .OrderByDescending(o => o.NobetSayisi).ThenBy(c => r.NextDouble())
                            .Select(s => s.EczaneNobetGrupId)
                            .Take(fazlaNobetTutacakEczaneSayisi).ToList();
                    }
                }
            }

            var nobetGunKuralDetayHaftaIciToplam = new NobetGunKuralDetay
            {
                FazlaNobetTutacakEczaneler = fazlaNobetTutacakEczaneler,
                KuralSTD = kuralStd,
                GunKuralId = 0,
                OrtalamaNobetSayisi = ortalamaNobetSayisi,
                EnCokNobetTutulanNobetSayisi = enCokNobetTutulanNobetSayisi
            };

            return nobetGunKuralDetayHaftaIciToplam;
        }

        public List<EczaneGrupDetay> GetEczaneGruplarByEczaneGrupTanimTipId(List<EczaneGrupDetay> eczaneGruplar, int eczaneGrupTanimTipId)
        {
            return eczaneGruplar.Where(w => w.EczaneGrupTanimTipId == eczaneGrupTanimTipId).ToList();
        }

        public List<EczaneNobetSonucListe2> GetSonuclarByGunGrup(List<EczaneNobetSonucListe2> sonuclar, string gunGrup)
        {
            return sonuclar.Where(w => w.GunGrupAdi == gunGrup).ToList();
        }

        public List<EczaneNobetSonucListe2> GetSonuclarByGunGrup(List<EczaneNobetSonucListe2> sonuclar, int gunGrupId)
        {
            return sonuclar.Where(w => w.GunGrupId == gunGrupId).ToList();
        }

        public string CeliskileriEkle(Solution solution)
        {
            var celiskiler = "<div class='table-responsive mt-2 mb-3'>";

            celiskiler += "<table class='table table-hover table-bordered table-striped table-sm'>";

            if (solution.ConflictingSet.ConstraintsLB.Count() > 1000)
            {
                return $"Çok sayıda kural için çelişki bulunmaktadır. Lütfen tüm kuralları gözden geçiriniz.*{solution.ConflictingSet.ConstraintsLB.Count()}";
            }

            var celisikKurallar = new List<string>();

            foreach (var conflictLb in solution.ConflictingSet.ConstraintsLB)
            {
                celisikKurallar.Add(conflictLb.Name);
            }

            foreach (var conflictUb in solution.ConflictingSet.ConstraintsUB)
            {
                celisikKurallar.Add(conflictUb.Name);
            }

            var celisikKurallarTekListe = celisikKurallar.Distinct().OrderBy(o => o).ToList();

            celiskiler += "<tr>";
            celiskiler += "<thead class='thead-light'>";
            celiskiler += "<tr>";
            celiskiler += $"<td>Sıra</td>";
            celiskiler += $"<td>Kod</td>";
            celiskiler += $"<td>Kategori</td>";
            celiskiler += $"<td>Kural</td>";
            celiskiler += $"<td>Değer</td>";
            celiskiler += $"<td>Grup</td>";
            celiskiler += $"<td>(Alt/Eş) Grup/Detay</td>";
            celiskiler += $"<td>Eczane</td>";
            celiskiler += "</tr>";
            celiskiler += "</thead>";

            celiskiler += "<tbody id='kuralCeliski'>";
            var indis = 0;

            string GetIndisSayisi(int index)
            {
                return index < 10 ? $"0{index}" : index.ToString();
            }

            foreach (var kisit in celisikKurallarTekListe)
            {
                indis++;

                celiskiler += "<tr class='gridtr'>";

                var kisitAdi = kisit.Split(',').ToArray();

                celiskiler += $"<td>{GetIndisSayisi(indis)}</td>";

                foreach (var item in kisitAdi)
                {
                    celiskiler += $"<td>{item.Trim()}</td>";
                }

                celiskiler += "</tr>";
            }
            celiskiler += "</tbody>";

            celiskiler += "</tr>";
            celiskiler += "</table>";
            celiskiler += "</div>";
            celiskiler += $"*{celisikKurallarTekListe.Count}";

            return celiskiler;
        }
        public string CeliskileriTabloyaAktar(DateTime baslangicTarihi, DateTime bitisTarihi, int calismaSayisi, string iterasyonMesaj, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, string[] celiskiler)
        {
            string mesaj;

            var cozulenNobetGruplar = CozulenGruplariYazdir(nobetGrupGorevTipler);

            mesaj = "Tabloya göre "
                        + "<a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">nöbet ayarlarını</a> "
                        + "kontrol edip <strong> nöbetleri tekrar yazdırmalısınız...</strong>"
                        //+ "<br /> "
                        ;

            mesaj += $"<span class='pull-right'>Tarih Aralığı: <strong><mark>{baslangicTarihi.ToShortDateString()} - {bitisTarihi.ToShortDateString()}</mark></strong></span>"
                        + "<br /> ";

            if (nobetGrupGorevTipler.Count > 1)
            {
                mesaj += //$"<strong>Nöbet seçenekleri:</strong>" +
                            $"Nöbet Grupları:"
                            + $"<strong><mark>{cozulenNobetGruplar}</mark></strong>"
                            + $"<br /> "
                            ;
            }

            //mesaj += $"<span class='mr-2'>Toplam kayıt: <strong><mark>{celiskiler[1]}</mark></strong></span>";

            mesaj += celiskiler[0];

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

            mesaj += "<a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">Nöbet Ayarları</a> ";

            mesaj += $"<br /> <strong>Çalışma adımları <span class='badge badge-info'>{calismaSayisi}</span></strong>" +
                $"{iterasyonMesaj} ";

            for (int i = 0; i < calismaSayisi; i++)
            {
                mesaj += "<br /> " + i + " " + calismaAdimlari[i];
            }

            return mesaj;
        }

        public string CozulenGruplariYazdir(List<NobetGrupGorevTipDetay> nobetGrupGorevTipDetaylar)
        {
            string cozulenNobetGruplar = null;

            var ilkGrup = nobetGrupGorevTipDetaylar.Select(s => s.NobetGrupAdi).FirstOrDefault();

            foreach (var i in nobetGrupGorevTipDetaylar.Select(s => s.NobetGrupAdi))
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

            return cozulenNobetGruplar;
        }

        private string NobetGrupBilgisiDuzenle(EczaneNobetGrupDetay eczaneNobetGrupDetay)
        {
            return eczaneNobetGrupDetay.NobetUstGrupId == 4
                        ? IsimleriBirlestir(eczaneNobetGrupDetay.NobetGorevTipAdi, eczaneNobetGrupDetay.NobetAltGrupAdi)
                        : eczaneNobetGrupDetay.NobetUstGrupId == 5
                        ? IsimleriBirlestir(eczaneNobetGrupDetay.NobetGrupAdi, eczaneNobetGrupDetay.NobetAltGrupAdi)
                        : eczaneNobetGrupDetay.NobetGrupAdi;
        }

        private string NobetGrupBilgisiDuzenle(EczaneNobetMazeretDetay eczaneNobetMazeretDetay)
        {
            return eczaneNobetMazeretDetay.NobetUstGrupId == 4
                        ? eczaneNobetMazeretDetay.NobetGorevTipAdi
                        : eczaneNobetMazeretDetay.NobetUstGrupId == 5
                        ? ""
                        : eczaneNobetMazeretDetay.NobetGrupAdi;
        }

        private string NobetGrupBilgisiDuzenle(EczaneNobetIstekDetay eczaneNobetIstekDetay)
        {
            return eczaneNobetIstekDetay.NobetUstGrupId == 4
                        ? eczaneNobetIstekDetay.NobetGorevTipAdi
                        : eczaneNobetIstekDetay.NobetUstGrupId == 5
                        ? ""
                        : eczaneNobetIstekDetay.NobetGrupAdi;
        }

        private string IsimleriBirlestir(params string[] eklenecekIsimler)
        {
            var kisitAdi = "";

            var ilkIsim = eklenecekIsimler.FirstOrDefault();

            foreach (var eklenecekIsim in eklenecekIsimler)
            {
                if (eklenecekIsim == null || eklenecekIsim == "")
                {
                    continue;
                }
                else if (eklenecekIsim == ilkIsim)
                {
                    kisitAdi += $"{eklenecekIsim.Trim()}";
                }
                else if (kisitAdi.EndsWith(":"))
                {
                    kisitAdi += $" {eklenecekIsim.Trim()}";
                }
                else
                {
                    kisitAdi += $", {eklenecekIsim.Trim()}";
                }
            }

            return kisitAdi;
        }

        public double GetNobetGunKural(List<NobetGrupKuralDetay> nobetGrupKurallar, int nobetKuralId)
        {
            var kural = nobetGrupKurallar.SingleOrDefault(s => s.NobetKuralId == nobetKuralId);

            var deger = kural == null ? 0 : kural.Deger;

            return (double)deger;
        }

        public NobetUstGrupKisitDetay GetNobetGunKuralIlgiliKisitTarihAraligi(List<NobetUstGrupKisitDetay> kisitlarAktif, int nobetGunKuralId)
        {
            NobetUstGrupKisitDetay herAyEnFazlaIlgiliKisit;

            switch (nobetGunKuralId)
            {
                case 1://pazar
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k23");
                    break;
                case 7://cumartesi
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k38");
                    break;
                case 6://cuma
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k43");
                    break;
                default://h. içi diğer
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k42");
                    break;
            }

            return herAyEnFazlaIlgiliKisit;
        }

        public NobetUstGrupKisitDetay GetNobetGunKuralIlgiliKisitKumulatif(List<NobetUstGrupKisitDetay> kisitlarAktif, int nobetGunKuralId)
        {
            NobetUstGrupKisitDetay herAyEnFazlaIlgiliKisit;

            switch (nobetGunKuralId)
            {
                case 1://pazar
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k57");
                    break;
                case 2://pazartesi
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k75");
                    break;
                case 3://salı
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k76");
                    break;
                case 4://çarşamba
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k77");
                    break;
                case 5://perşembe
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k78");
                    break;
                case 8://dini bayram
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k79");
                    break;
                case 9://milli bayram
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k80");
                    break;
                case 10://arefe
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k81");
                    break;
                case 11://31 aralık
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k82");
                    break;
                case 12://1 ocak
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k83");
                    break;
                case 6://cuma
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k20");
                    break;
                case 7://cumartesi
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k21");
                    break;
                default://h. içi diğer
                    herAyEnFazlaIlgiliKisit = NobetUstGrupKisit(kisitlarAktif, "k34");
                    break;
            }

            return herAyEnFazlaIlgiliKisit;
        }

        public int GetKumulatifToplamNobetSayisi(List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikler, int nobetGunKuralId)
        {
            var kumulatifToplamNobetSayisiNobetGunKural = 0;

            switch (nobetGunKuralId)
            {
                case 1://pazar
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiPazar);
                    break;
                case 2://pazartesi
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiPazartesi);
                    break;
                case 3://salı
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiSali);
                    break;
                case 4://çarşamba
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiCarsamba);
                    break;
                case 5://perşembe
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiPersembe);
                    break;
                case 6://cuma
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiCuma);
                    break;
                case 7://cumartesi
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiCumartesi);
                    break;
                case 8://dini bayram
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiDiniBayram);
                    break;
                case 9://milli bayram
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiMilliBayram);
                    break;
                case 10://arefe
                    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiArife);
                    break;
                //case 11://31 aralık
                //    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiPazar);
                //    break;
                //case 12://1 ocak
                //    kumulatifToplamNobetSayisiNobetGunKural = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiPazar);
                //    break;
                default://h. içi diğer
                    kumulatifToplamNobetSayisiNobetGunKural = 0; //eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiPazar);
                    break;
            }

            return kumulatifToplamNobetSayisiNobetGunKural;
        }

        public List<NobetGunKuralTarihAralik> OrtalamaNobetSayilariniHesapla(List<TakvimNobetGrup> tarihler,
            int gruptakiEczaneSayisi,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikler,
            List<TakvimNobetGrupGunDegerIstatistik> nobetGunKuralIstatistikler)
        {
            var nobetGunKuralTarihler = new List<NobetGunKuralTarihAralik>();

            foreach (var nobetGunKural in nobetGunKuralIstatistikler)
            {
                var tarihlerNobetGunKuralBazli = tarihler.Where(w => w.NobetGunKuralId == nobetGunKural.NobetGunKuralId).ToList();
                var gunKuralGunSayisi = tarihlerNobetGunKuralBazli.Count;
                var kumulatifToplamNobetSayisiNobetGunKural = GetKumulatifToplamNobetSayisi(eczaneNobetGrupGunKuralIstatistikler, nobetGunKural.NobetGunKuralId);
                var tarihAraligiIcindekiTalepEdilenNobetciSayisi = tarihlerNobetGunKuralBazli.Sum(s => s.TalepEdilenNobetciSayisi);

                nobetGunKuralTarihler.Add(new NobetGunKuralTarihAralik
                {
                    GunGrupId = nobetGunKural.GunGrupId,
                    GunGrupAdi = nobetGunKural.GunGrupAdi,
                    NobetGunKuralId = nobetGunKural.NobetGunKuralId,
                    NobetGunKuralAdi = nobetGunKural.NobetGunKuralAdi,
                    TakvimNobetGruplar = tarihlerNobetGunKuralBazli,
                    GunSayisi = gunKuralGunSayisi,
                    OrtalamaNobetSayisi = OrtalamaNobetSayisi(tarihAraligiIcindekiTalepEdilenNobetciSayisi, gruptakiEczaneSayisi),
                    KumulatifGunSayisi = nobetGunKural.GunSayisi,
                    KumulatifOrtalamaNobetSayisi = OrtalamaNobetSayisi(//nobetGunKural.TalepEdilenNobetciSayisi 
                                                                        kumulatifToplamNobetSayisiNobetGunKural + tarihAraligiIcindekiTalepEdilenNobetciSayisi
                                                                        , gruptakiEczaneSayisi)
                });
            }

            return nobetGunKuralTarihler;
        }

        public List<NobetUstGrupKisitDetay> GetKisitlarNobetGrupBazli(List<NobetUstGrupKisitDetay> kisitlarUstGrupBazli, List<NobetGrupGorevTipKisitDetay> kisitlarGrupBazli)
        {
            var kisitlarAktif = new List<NobetUstGrupKisitDetay>();

            //üst grup kısıtlar olduğu gibi aktif listeye aktarıldı. grup bazlı değişen olursa aktiften değişecek.
            kisitlarUstGrupBazli.ForEach(x => kisitlarAktif.Add((NobetUstGrupKisitDetay)x.Clone()));

            foreach (var grupBazliKisit in kisitlarGrupBazli)
            {
                var kisitGrupBazli = kisitlarAktif.SingleOrDefault(w => w.KisitId == grupBazliKisit.KisitId);

                kisitGrupBazli.PasifMi = grupBazliKisit.PasifMi;
                kisitGrupBazli.SagTarafDegeri = grupBazliKisit.SagTarafDegeri;
            }

            return kisitlarAktif;
        }

        public void NobetGrupBuyuklugunuTakvimeEkle(List<TakvimNobetGrup> tarihler, int eczaneSayisi)
        {
            var gunGruplar = tarihler
                .Select(s => new
                {
                    s.GunGrupId,
                    s.GunGrupAdi
                })
                .OrderBy(o => o.GunGrupId)
                .Distinct().ToList();

            foreach (var gunGrup in gunGruplar)
            {
                var eczaneSayisiKumulatif = eczaneSayisi;

                var i = 1;
                var j = 1;

                var tarihlerSirali = tarihler
                    .Where(w => w.GunGrupId == gunGrup.GunGrupId)
                    .OrderBy(o => o.Tarih).ToList();

                foreach (var tarih in tarihlerSirali)
                {
                    if (i > eczaneSayisiKumulatif)
                    {
                        eczaneSayisiKumulatif += eczaneSayisi;
                        j++;
                    }
                    //else
                    //{
                    //    tarih.NobetGrubuBuyukluk = j;
                    //}

                    if (j == 0)
                    {

                    }

                    if (i <= eczaneSayisiKumulatif)
                    {
                        tarih.NobetGrubuBuyukluk = j;
                    }

                    i++;
                }
            }
        }

        public double GetArdisikBosGunSayisi(int pespeseNobetSayisi, double altLimit)
        {
            if (pespeseNobetSayisi > 0)
            {
                altLimit = pespeseNobetSayisi;
            }

            return altLimit;
        }

        public List<AyniGunTutulanNobetDetay> GetAyniGunNobetTutanEczaneler(List<EczaneNobetTarihAralik> sonuclar)
        {
            var sonuclarTarihler = sonuclar
                .Select(s => new
                {
                    s.TakvimId,
                    s.Tarih,
                    s.GunGrupId,
                    s.GunGrupAdi
                }).Distinct().ToList();

            var ayniGunNobetTutanEczaneler = new List<AyniGunTutulanNobetDetay>();

            foreach (var tarih in sonuclarTarihler)
            {
                var tarihBazliSonuclar = sonuclar
                    .Where(w => w.TakvimId == tarih.TakvimId)
                    .OrderBy(o => o.EczaneNobetGrupId)
                    .ToList();

                foreach (var tarihBazliSonuc in tarihBazliSonuclar.Take(tarihBazliSonuclar.Count - 1).ToList())
                {
                    var tarihBazliSonuclar2 = tarihBazliSonuclar
                        .Where(w => w.EczaneNobetGrupId > tarihBazliSonuc.EczaneNobetGrupId).ToList();

                    foreach (var tarihBazliSonuc2 in tarihBazliSonuclar2)
                    {
                        ayniGunNobetTutanEczaneler.Add(new AyniGunTutulanNobetDetay
                        {
                            //AltGrupAdi = "Kendisi",
                            //Grup = "Çorum-Merkez",
                            EczaneAdi1 = tarihBazliSonuc.EczaneAdi,
                            EczaneAdi2 = tarihBazliSonuc2.EczaneAdi,
                            EczaneId1 = tarihBazliSonuc.EczaneId,
                            EczaneId2 = tarihBazliSonuc2.EczaneId,
                            //EczaneNobetGrupId1 = tarihBazliSonuc.EczaneNobetGrupId,
                            //EczaneNobetGrupId2 = tarihBazliSonuc2.EczaneNobetGrupId,
                            NobetGrupAdi1 = tarihBazliSonuc.NobetGrupAdi,
                            NobetGrupAdi2 = tarihBazliSonuc2.NobetGrupAdi,
                            EnSonAyniGunNobetTakvimId = tarih.TakvimId,
                            EnSonAyniGunNobetTarihi = tarih.Tarih,
                            GunGrupAdi = tarih.GunGrupAdi
                        });
                    }
                }
            }

            return ayniGunNobetTutanEczaneler;
        }

        public KalibrasyonYatay GetKalibrasyonDegeri(List<KalibrasyonYatay> eczaneKalibrasyon)
        {
            return eczaneKalibrasyon
                .Where(w => w.KalibrasyonTipId > 7).SingleOrDefault() ?? new KalibrasyonYatay();
        }

        public bool KumulatifEnfazlaHafIciDagilimiArasindaFarkVarmi(
            NobetUstGrupKisitDetay herAyEnFazlaIlgiliKisit,
            NobetUstGrupKisitDetay kumulatifEnfazlaHaftaIciDagilimi,
            TakvimNobetGrupGunDegerIstatistik nobetGunKural,
            int gunKuralNobetSayisi,
            int haftaIciEnCokVeGunKuralNobetleriArasindakiFark,
            int haftaIciEnAzVeEnCokNobetSayisiArasindakiFark)
        {
            var durum = haftaIciEnAzVeEnCokNobetSayisiArasindakiFark >= kumulatifEnfazlaHaftaIciDagilimi.SagTarafDegeri
                                         && gunKuralNobetSayisi >= kumulatifEnfazlaHaftaIciDagilimi.SagTarafDegeri
                                         && haftaIciEnCokVeGunKuralNobetleriArasindakiFark == 0
                                         && !kumulatifEnfazlaHaftaIciDagilimi.PasifMi
                                         && !herAyEnFazlaIlgiliKisit.PasifMi
                                         && nobetGunKural.GunGrupId == 3;
            return durum;
        }

        #endregion        
    }
}

#region tip kısıt
//EczaneNobetKisit<TOpt> : IEczaneNobetKisit<TOpt>
//where TOpt : class, IDataModel, new()
#endregion

#region eski amaç fonksiyonu
/*
            /*
              + _x[i] * Convert.ToInt32(i.BayramMi) * (bayramCevrim + bayramCevrim / 
                                                          Math.Pow((i.Tarih - p.SonNobetTarihiBayram).TotalDays, 0.5))
              + _x[i] * Convert.ToInt32(i.PazarGunuMu) * (pazarCevrim + pazarCevrim / 
                                                          Math.Pow((i.Tarih - p.SonNobetTarihiPazar).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7), 0.5))
              + _x[i] * Convert.ToInt32(i.HaftaIciMi) * (haftaIciCevrim + haftaIciCevrim / 
                                                                    Math.Pow((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day, 0.5))

            v2 
             + _x[i] * Convert.ToInt32(i.BayramMi) * (bayramCevrim + bayramCevrim / 
                                                         Math.Sqrt((i.Tarih - p.SonNobetTarihiBayram).TotalDays))
             + _x[i] * Convert.ToInt32(i.PazarGunuMu) * (pazarCevrim + pazarCevrim / 
                                                         Math.Sqrt((i.Tarih - p.SonNobetTarihiPazar).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))
             + _x[i] * Convert.ToInt32(i.HaftaIciMi) * (haftaIciCevrim + haftaIciCevrim / 
                                                         Math.Sqrt((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
                                                      * (i.Tarih.DayOfWeek == p.SonNobetTarihiHaftaIci.DayOfWeek ? 1 : 0.2)
                                                                    )
             v3
             + _x[i] * Convert.ToInt32(i.HaftaIciMi) * (haftaIciCevrim + haftaIciCevrim /
                                              Math.Sqrt((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day))
                                          + ((int)i.Tarih.DayOfWeek == 2 && p.NobetSayisiPazartesi > 0
                                                    ? p.NobetSayisiPazartesi * 100
                                                    : (int)i.Tarih.DayOfWeek == 3 && p.NobetSayisiSali > 0
                                                    ? p.NobetSayisiSali * 100
                                                    : (int)i.Tarih.DayOfWeek == 4 && p.NobetSayisiCarsamba > 0
                                                    ? p.NobetSayisiCarsamba * 100
                                                    : (int)i.Tarih.DayOfWeek == 5 && p.NobetSayisiPersembe > 0
                                                    ? p.NobetSayisiPersembe * 100
                                                    : (int)i.Tarih.DayOfWeek == 6 && p.NobetSayisiCuma > 0
                                                    ? p.NobetSayisiCuma * 100
                                                    : (int)i.Tarih.DayOfWeek == 6 && p.NobetSayisiCumartesi > 0
                                                    ? p.NobetSayisiCumartesi * 100
                                                    : 0)
            */
#endregion
