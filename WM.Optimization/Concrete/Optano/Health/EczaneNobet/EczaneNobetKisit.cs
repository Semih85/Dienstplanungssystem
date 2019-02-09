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
                var nobetGrupBilgisi = p.NobetGrupGorevTip.NobetUstGrupId == 4
                    ? IsimleriBirlestir(p.NobetGrupGorevTip.NobetGorevTipAdi)
                    : p.NobetGrupGorevTip.NobetUstGrupId == 5
                    ? ""
                    : p.NobetGrupGorevTip.NobetGrupAdi;

                var kisitAdi = IsimleriBirlestir("K0 (Talep):", nobetGrupBilgisi, $"{tarih.Tarih.ToShortDateString()} tarihindeki talep edilen nöbetçi sayısı: ", tarih.TalepEdilenNobetciSayisi.ToString());

                var kararIndex = p.EczaneNobetTarihAralikTumu
                    .Where(k => k.TakvimId == tarih.TakvimId).ToList();

                var std = tarih.TalepEdilenNobetciSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                var cns = Constraint.Equals(exp, std);

                p.Model.AddConstraint(cns, kisitAdi);
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
                    $" [Std. {enAzNobetSayisi}"
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

                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}" +
                    $" [Std. {p.OrtalamaNobetSayisi}" +
                    $"{(p.GunKuralAdi == null ? "" : $"- {p.GunKuralAdi}")}]"
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

        /// <summary>
        /// Tüm günlerin tur takip kısıtı
        /// </summary>
        /// <param name="p">KpKumulatifToplamEnFazla</param>
        public virtual void KumulatifToplamEnFazla(KpKumulatifToplam p)
        {
            if (!p.NobetUstGrupKisit.PasifMi && p.Tarihler.Count > 0)
            {
                var kararIndex = p.EczaneNobetTarihAralik
                                   .Where(e => p.Tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                    p.KumulatifOrtalamaGunKuralSayisi = p.NobetUstGrupKisit.SagTarafDegeri;

                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}" +
                    $" [Std. {p.KumulatifOrtalamaGunKuralSayisi}" +
                    $"{(p.GunKuralAdi == null ? "" : $"- {p.GunKuralAdi}")}]"
                    ;

                var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                var fark = p.KumulatifOrtalamaGunKuralSayisi - p.ToplamNobetSayisi;

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

            if (!p.NobetUstGrupKisit.PasifMi && p.NobetSayisi > 0
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
                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} ["
                              //+ $"{p.PespeseNobetSayisiAltLimit} gün ("
                              + $"{p.SonNobetTarihi.ToShortDateString()} - {p.NobetYazilabilecekIlkTarih.ToShortDateString()}"
                              + $"]";

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
            if (!p.NobetUstGrupKisit.PasifMi && p.OrtamalaNobetSayisi > 1)
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

                    //var pespeseGunlerdenKalanlar = p.Tarihler.Skip(atlanacakGunSayisi).ToList();

                    //var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} ["
                    //         + $"{p.PespeseNobetSayisiAltLimit} gün ("
                    //         + $"{pespeseGunlerdenKalanlar.Count})"
                    //         + $"{(p.GunKuralAdi == null ? "" : $"- {p.GunKuralAdi}")}]";

                    //var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                    //var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                    //var kararIndex2 = kararIndex
                    //    .Where(e => pespeseGunlerdenKalanlar.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                    //var std = 1;
                    //var exp = Expression.Sum(kararIndex2.Select(i => p.KararDegiskeni[i]));
                    //var cns = Constraint.LessThanOrEqual(exp, std);
                    //cns.LowerBound = 0;
                    //p.Model.AddConstraint(cns, kisitAdi);

                    var pespeseGunler = p.Tarihler.Take(atlanacakGunSayisi).ToList();

                    var indis = (int)Math.Ceiling(p.PespeseNobetSayisiAltLimit) - 1;

                    foreach (var tarih in pespeseGunler)
                    {
                        var altLimit = tarih.Tarih;

                        var ustLimit = p.Tarihler[indis].Tarih;
                        //tarih.Tarih.AddDays(p.PespeseNobetSayisiAltLimit);
                        //+28 pazar günü gibi birşey olmalı
                        indis++;

                        var kisitTanim2 = $"{p.NobetUstGrupKisit.KisitTanim} ["
                          + $"{p.PespeseNobetSayisiAltLimit} gün ("
                          + $"{altLimit.ToShortDateString()} - {ustLimit.ToShortDateString()})"
                          + $"{(p.GunKuralAdi == null ? "" : $"- {p.GunKuralAdi}")}]";

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
            {
                var tarihler2 = p.Tarihler.Take(p.Tarihler.Count - p.PespeseNobetSayisi).ToList();

                foreach (var tarih in tarihler2)
                {
                    var altLimit = tarih.Tarih;
                    var ustLimit = tarih.Tarih.AddDays(p.PespeseNobetSayisi);

                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} ["
                        + $"{p.PespeseNobetSayisi} gün ("
                        + $"{altLimit.ToShortDateString()} - {ustLimit.ToShortDateString()})"
                        + $"]";

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

                var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} ["
                     + $"son bayram nöbeti: {p.SonNobet.NobetGorevTipAdi}"
                     + $"]";

                var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(p.EczaneNobetGrup);

                var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, p.EczaneNobetGrup.EczaneAdi);

                var kararIndex = p.EczaneNobetTarihAralik
                    .Where(e => ilgiliTarihler.Select(s => s.TakvimId).Contains(e.TakvimId)
                             && e.NobetGorevTipId == p.SonNobet.NobetGorevTipId //sonradan ekledim.
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
                        s.AyniGunNobetTutabilecekEczaneSayisi
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

                        #region kontrol
                        var kontol = false;

                        if (kontol)
                        {
                            if (eczaneGrupTanim.Id == 143)//g.Adi.Contains("EŞ-SEMİH-SERRA BALTA"))
                            {
                            }

                            var kontrolEdilecekGruptakiEczaneler = new string[] { "EBRU" };

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

                                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} ["
                                      + $"{tarih.Tarih.ToShortDateString()}"
                                      + $"]";

                                    var nobetGrupBilgisi = eczaneGrupTanim.EczaneGrupTanimAdi;

                                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi);

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

                                        var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} [Öncelikli çözüm "
                                            + $"{tarih.Tarih.ToShortDateString()}"
                                            + $"]";

                                        var nobetGrupBilgisi = eczaneGrupTanim.EczaneGrupTanimAdi;

                                        var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi);

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

                                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} ["
                                       + $"{altLimit.ToShortDateString()} - {ustLimit.ToShortDateString()}"
                                       + $"]";

                                    var nobetGrupBilgisi = eczaneGrupTanim.EczaneGrupTanimAdi;

                                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi);

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

                                        var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} [Öncelikli çözüm "
                                           + $"{altLimit.ToShortDateString()} - {ustLimit.ToShortDateString()}"
                                           + $"]";

                                        var nobetGrupBilgisi = eczaneGrupTanim.EczaneGrupTanimAdi;

                                        var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi);

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
                var tarihAraligi = p.Tarihler.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                foreach (var ikiliEczane in p.IkiliEczaneler)
                {
                    var kararIndexIkiliEczaneler = p.EczaneNobetTarihAralik
                             .Where(e => e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId1 || e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId2).ToList();

                    foreach (var tarih in tarihAraligi.Take(p.Tarihler.Count - 1))
                    {
                        var tarihler2 = tarihAraligi.Where(w => w.Tarih > tarih.Tarih).ToList();

                        foreach (var tarih2 in tarihler2)
                        {
                            var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}" +
                                             $" [Std. 3]";

                            var ikiliEczaneler = $"{ikiliEczane.EczaneAdi1}-{ikiliEczane.EczaneAdi2}";
                            var ikiliTarihler = $"{tarih.Tarih.ToShortDateString()}-{tarih2.Tarih.ToShortDateString()}";

                            var kisitAdi = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler);

                            var kararIndex = kararIndexIkiliEczaneler
                                    .Where(e => e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId).ToList();

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

        /// <summary>
        /// Eczane ikilileri tarih aralığı içinde sadece 1 kez aynı gün nöbet tutsun (değişken dönüşümlü)
        /// </summary>
        /// <param name="p"></param>
        public virtual void AyIcindeSadece1KezAyniGunNobetTutulsun(KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var tarihAraligi = p.Tarihler.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                foreach (var ikiliEczane in p.IkiliEczaneler)
                {
                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim}" +
                         $" [Std. 1]";

                    var ikiliEczaneler = $"{ikiliEczane.EczaneAdi1}-{ikiliEczane.EczaneAdi2}";

                    var kisitAdi1 = IsimleriBirlestir(kisitTanim, ikiliEczaneler);

                    var kararIndexIkiliEczane = p.EczaneNobetTarihAralik
                             .Where(e => (e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId1 || e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId2)).ToList();

                    var kararIndex3 = p.EczaneNobetTarihAralikIkiliEczaneler
                                .Where(e => e.AyniGunTutulanNobetId == ikiliEczane.Id).ToList();

                    foreach (var tarih in p.Tarihler)
                    {

                        var ikiliTarihler = $"{tarih.Tarih.ToShortDateString()}";

                        var kisitAdi = IsimleriBirlestir(kisitTanim, ikiliEczaneler, ikiliTarihler);

                        var kararIndex = kararIndexIkiliEczane
                                .Where(e => e.TakvimId == tarih.TakvimId).ToList();

                        var kararIndex2 = kararIndex3
                                .Where(e => e.TakvimId == tarih.TakvimId).FirstOrDefault();

                        #region kontrol
                        //var ss = new string[] { "AYDOĞAN", "SARE" };
                        //var sayi = kararIndex.Where(w => w.EczaneAdi == "AYDOĞAN" || w.EczaneAdi == "NİSA").Count();

                        //if (sayi == 4)
                        //{
                        //} 
                        #endregion

                        var std1 = 2 - 2 * (1 - p.KararDegiskeniIkiliEczaneler[kararIndex2]);
                        var exp1 = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                        var cnsBuyuktur = Constraint.GreaterThanOrEqual(exp1, std1);
                        p.Model.AddConstraint(cnsBuyuktur, kisitAdi);
                        cnsBuyuktur.LowerBound = 0;

                        var std2 = 1 + 2 * p.KararDegiskeniIkiliEczaneler[kararIndex2];
                        var exp2 = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                        var cnsKucuktur = Constraint.LessThanOrEqual(exp2, std2);
                        p.Model.AddConstraint(cnsKucuktur, kisitAdi);
                        cnsKucuktur.LowerBound = 0;
                    }

                    var std3 = 1;
                    var exp3 = Expression.Sum(kararIndex3.Select(i => p.KararDegiskeniIkiliEczaneler[i]));
                    var cns = Constraint.LessThanOrEqual(exp3, std3);

                    cns.LowerBound = 0;
                    p.Model.AddConstraint(cns, kisitAdi1);
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
                var kisitAdi = $"K{p.NobetUstGrupKisit.KisitId} ({p.NobetUstGrupKisit.KisitKategorisi}, {p.NobetUstGrupKisit.KisitAdiGosterilen}):"; //, {eczaneNobetGrup.EczaneAdi}";

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
                foreach (var eczaneNobetMazeret in p.EczaneNobetMazeretler)
                {
                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} ["
                     + $"mazeret tarihi: {eczaneNobetMazeret.Tarih.ToShortDateString()}"
                     //+ $"{eczaneNobetMazeret.MazeretAdi}"
                     + $"]";

                    var nobetGrupBilgisi = NobetGrupBilgisiDuzenle(eczaneNobetMazeret);

                    var kisitAdi = IsimleriBirlestir(kisitTanim, nobetGrupBilgisi, eczaneNobetMazeret.EczaneAdi);

                    var kararIndex = p.EczaneNobetTarihAralik
                        .Where(e => e.EczaneNobetGrupId == eczaneNobetMazeret.EczaneNobetGrupId
                                 && e.TakvimId == eczaneNobetMazeret.TakvimId).ToList();

                    var std = 0;
                    var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                    var cns = Constraint.Equals(exp, std);
                    p.Model.AddConstraint(cns, kisitAdi);
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
                    var kisitTanim = $"{p.NobetUstGrupKisit.KisitTanim} ["
                         + $"istek tarihi: {eczaneNobetIstek.Tarih.ToShortDateString()}"
                         //+ $"{eczaneNobetIstek.IstekAdi}"
                         + $"]";

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

                    var kisitAdi = $"K{p.NobetUstGrupKisit.KisitId} ({p.NobetUstGrupKisit.KisitKategorisi}, {p.NobetUstGrupKisit.KisitAdiGosterilen}): {eczaneNobetGrup}";

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

                    var nobetAltGrubuOlmayanlarinSonuclariGunGruplu = nobetAltGrubuOlmayanlarinSonuclari.Where(w => w.GunGrup == gunGrup).ToList();
                    var nobetAltGrubuOlanlarinSonuclariGunGruplu = nobetAltGrubuOlanlarinSonuclari.Where(w => w.GunGrup == gunGrup).ToList();
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
                                                                         GunGrup = g1.GunGrup
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
            return sonuclar.Where(w => w.GunGrup == gunGrup).ToList();
        }

        public List<EczaneNobetSonucListe2> GetSonuclarByGunGrup(List<EczaneNobetSonucListe2> sonuclar, int gunGrupId)
        {
            return sonuclar.Where(w => w.GunGrupId == gunGrupId).ToList();
        }

        public string CeliskileriEkle(Solution solution)
        {
            var celiskiler = "<ul class='list-group list-group-flush mt-2 mb-3'>";

            var celisikKurallar = new List<string>();

            foreach (var conflictLb in solution.ConflictingSet.ConstraintsLB)
            {
                celisikKurallar.Add(conflictLb.Name);
            }

            foreach (var conflictUb in solution.ConflictingSet.ConstraintsUB)
            {
                celisikKurallar.Add(conflictUb.Name);
            }

            var celisikKurallarTekListe = celisikKurallar.Distinct().ToList();

            foreach (var kisit in celisikKurallarTekListe)
            {
                celiskiler += $"<li class='list-group-item list-group-item-action py-1'>{kisit}</li>";
            }
            celiskiler += "</ul>";
            celiskiler += $"*{celisikKurallarTekListe.Count}";

            return celiskiler;
        }

        private string NobetGrupBilgisiDuzenle(EczaneNobetGrupDetay eczaneNobetGrupDetay)
        {
            return eczaneNobetGrupDetay.NobetUstGrupId == 4
                        ? IsimleriBirlestir(eczaneNobetGrupDetay.NobetGorevTipAdi, eczaneNobetGrupDetay.NobetAltGrupAdi)
                        : eczaneNobetGrupDetay.NobetUstGrupId == 5
                        ? eczaneNobetGrupDetay.NobetAltGrupAdi
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
