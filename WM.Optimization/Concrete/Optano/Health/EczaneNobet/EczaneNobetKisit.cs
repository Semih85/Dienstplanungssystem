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

        /// <summary>
        /// Her gün talep edilen kadar eczaneye nöbet yazılır.
        /// </summary>
        /// <param name="p">KpTalebiKarsila</param>
        public virtual void TalebiKarsila(KpTalebiKarsila p)
        {
            foreach (var tarih in p.Tarihler)
            {
                var kisitAdi = $"Talep: {tarih.TalepEdilenNobetciSayisi}, Tarih: {tarih.Tarih}";

                var kararIndex = p.EczaneNobetTarihAralikTumu
                                            .Where(k => k.TakvimId == tarih.TakvimId).ToList();

                var std = tarih.TalepEdilenNobetciSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                var cns = Constraint.Equals(exp, std);

                p.Model.AddConstraint(cns, kisitAdi);
            }
        }

        /// <summary>
        /// Her eczaneye istenen tarih aralığında en fazla nöbet grubunun ortalaması kadar nöbet yazılır.
        /// </summary>
        /// <param name="p">KpTarihAraligiOrtalamaEnFazla</param>
        public virtual void TarihAraligiOrtalamaEnFazla(KpTarihAraligiOrtalamaEnFazla p)
        {
            if (!p.NobetUstGrupKisit.PasifMi && p.GunSayisi > 0)
            {
                var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {p.EczaneNobetGrup.EczaneAdi}";

                if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                    p.OrtalamaNobetSayisi += p.NobetUstGrupKisit.SagTarafDegeri;

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
                var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {p.EczaneNobetGrup.EczaneAdi}";

                var kararIndex = p.EczaneNobetTarihAralik
                                   .Where(e => p.Tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                    p.KumulatifOrtalamaGunKuralSayisi = p.NobetUstGrupKisit.SagTarafDegeri;

                var std = p.KumulatifOrtalamaGunKuralSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i])) + p.ToplamNobetSayisi;
                var cns = Constraint.LessThanOrEqual(exp, std);

                if (p.EnAzMi)
                    cns = Constraint.GreaterThanOrEqual(exp, std);
                else
                    cns.LowerBound = 0;

                p.Model.AddConstraint(cns, kisitAdi);
            }
        }

        #region peş peşe nöbetler

        /// <summary>
        /// İstenen günler için bir sonraki nöbet peş peşe en az gün geçmeden yazılmaz.
        /// </summary>
        /// <param name="p">KpPesPeseGorevEnAz</param>
        public virtual void PesPeseGorevEnAz(KpPesPeseGorevEnAz p)
        {
            if (!p.NobetUstGrupKisit.PasifMi && p.NobetSayisi > 0
                //enSonNobetTarihi >= nobetUstGrupBaslamaTarihi
                )
            {
                var tarihAralik = p.Tarihler
                  .Where(w => w.Tarih <= p.NobetYazilabilecekIlkTarih)
                  //.Select(s => s.TakvimId)
                  .ToList();

                if (tarihAralik.Count > 0)
                {
                    var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {p.EczaneNobetGrup.EczaneAdi}";

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
            if (!p.NobetUstGrupKisit.PasifMi && p.HaftaIciOrtamalaNobetSayisi > 1)
            {
                var gunSayisi = p.HaftaIciGunleri.Count;

                var kararIndex = p.EczaneNobetTarihAralik
                        .Where(e => p.HaftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                if (p.PespeseNobetSayisiAltLimit >= gunSayisi)
                {
                    var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}_1, {p.EczaneNobetGrup.EczaneAdi}";

                    var std = p.HaftaIciOrtamalaNobetSayisi;
                    var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                    var cns = Constraint.LessThanOrEqual(exp, std);
                    cns.LowerBound = 0;
                    p.Model.AddConstraint(cns, kisitAdi);
                }
                else
                {
                    var haftaiciPespeseGunler = p.HaftaIciGunleri.Take(gunSayisi - (int)p.PespeseNobetSayisiAltLimit).ToList();

                    foreach (var tarih in haftaiciPespeseGunler)
                    {
                        var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {p.EczaneNobetGrup.EczaneAdi}";

                        var altLimit = tarih.Tarih;
                        var ustLimit = tarih.Tarih.AddDays(p.PespeseNobetSayisiAltLimit);

                        var kararIndex2 = kararIndex
                            .Where(e => (e.Tarih >= altLimit && e.Tarih <= ustLimit)).ToList();

                        var std = 1;
                        var exp = Expression.Sum(kararIndex2.Select(i => p.KararDegiskeni[i]));
                        var cns = Constraint.LessThanOrEqual(exp, std);
                        cns.LowerBound = 0;
                        p.Model.AddConstraint(cns, kisitAdi);
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
                var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {p.EczaneNobetGrup.EczaneAdi}";

                var tarihler2 = p.Tarihler.Take(p.Tarihler.Count - p.PespeseNobetSayisi).ToList();

                foreach (var tarih in tarihler2)
                {
                    var altLimit = tarih.Tarih;
                    var ustLimit = tarih.Tarih.AddDays(p.PespeseNobetSayisi);

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
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var ilgiliTarihler = p.Tarihler.Where(w => w.NobetGunKuralId == p.SonNobet.NobetGunKuralId).ToList();

                var kisitAdi = $"her_eczaneye_son_tuttugu_bayram_nobetinden_farkli_bayram_nobeti_yaz, {p.EczaneNobetGrup}";

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

                            var kontrolEdilecekGruptakiEczaneler = new string[] { "ÇOTANAK", "PINAR" };

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
                                {
                                    var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gun_birlikte_nobet_tutmasin, {tarih.TakvimId}";

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
                                    {
                                        var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gun_birlikte_nobet_tutmasin_oncelikli_cozum, {tarih.TakvimId}";

                                        var kararIndex = kararIndexMaster
                                            .Where(e => e.TakvimId == tarih.TakvimId)
                                            .ToList();

                                        var std = 0;
                                        var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                                        var cns = Constraint.Equals(exp, std);
                                        cns.LowerBound = 0;

                                        p.Model.AddConstraint(cns, kisitAdi);
                                    }
                                }
                            }
                            else
                            {
                                var tarihler = tarihAraligi.Take(tarihAraligi.Count() - ardisikNobetSayisi).ToList();

                                foreach (var tarih in tarihler)
                                {
                                    var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gunlerde_birlikte_nobet_tutmasin, {tarih.TakvimId}";

                                    var kararIndex = kararIndexMaster
                                        .Where(e => (e.Tarih >= tarih.Tarih && e.Tarih <= tarih.Tarih.AddDays(ardisikNobetSayisi)))
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
                                    {
                                        var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gunlerde_birlikte_nobet_tutmasin_oncelikli_cozum, {tarih.TakvimId}";

                                        var kararIndex = kararIndexMaster
                                            .Where(e => (e.Tarih >= tarih.Tarih.AddDays(-ardisikNobetSayisi) && e.Tarih <= tarih.Tarih.AddDays(ardisikNobetSayisi)))
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

        #region Mazeret ve istekler

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
                    var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {eczaneNobetMazeret.EczaneAdi}, {eczaneNobetMazeret.Tarih}, {eczaneNobetMazeret.MazeretAdi}";

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
                    var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {eczaneNobetIstek.EczaneAdi}, {eczaneNobetIstek.Tarih}, {eczaneNobetIstek.IstekAdi}";

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

        #endregion

        #endregion

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
                var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {p.EczaneNobetGrup.EczaneAdi}";

                var kararIndex = p.EczaneNobetTarihAralik
                                        .Where(e => p.Tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                var enAzNobetSayisi = 1;

                if (p.NobetUstGrupKisit.SagTarafDegeri > 0)
                    enAzNobetSayisi = (int)p.NobetUstGrupKisit.SagTarafDegeri;

                var std = enAzNobetSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => p.KararDegiskeni[i]));
                var cns = Constraint.GreaterThanOrEqual(exp, std);
                //var isTriviallyFeasible = cns.IsTriviallyFeasible();

                p.Model.AddConstraint(cns, kisitAdi);
            }
        }

        /// <summary>
        /// Eczane ikilileri tarih aralığı içinde sadece 1 kez aynı gün nöbet tutsun
        /// </summary>
        /// <param name="p">AyIcindeSadece1KezAyniGunNobetKisitParametreModel</param>
        public virtual void AyIcindeSadece1KezAyniGunNobetTutulsun(KpAyIcindeSadece1KezAyniGunNobet p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}"; //, {eczaneNobetGrup.EczaneAdi}";

                var tarihAraligi = p.Tarihler.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                foreach (var ikiliEczane in p.IkiliEczaneler)
                {
                    var kararIndexOnce = p.EczaneNobetTarihAralik
                             .Where(e => (e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId1 || e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId2)).ToList();

                    foreach (var tarih in tarihAraligi.Take(p.Tarihler.Count - 1))
                    {
                        var tarihler2 = tarihAraligi.Where(w => w.Tarih > tarih.Tarih).ToList();

                        foreach (var tarih2 in tarihler2)
                        {
                            var kararIndex = kararIndexOnce
                                    .Where(e => (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId)).ToList();

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
                    var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                    foreach (var tarih in tarihAraligi)
                    {
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

        /// <summary>
        /// Eczane ikilileri tarih aralığı içinde sadece 1 kez aynı gün nöbet tutsun
        /// </summary>
        /// <param name="p">AyIcindeSadece1KezAyniGunNobetKisitParametreModel</param>
        public virtual void AyIcindeSadece1KezAyniGunNobetTutulsunGiresunAltGrup(KpAyIcindeSadece1KezAyniGunNobetGiresunAltGrup p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}"; //, {eczaneNobetGrup.EczaneAdi}";

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

                var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}";

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

                    var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}, {eczaneNobetGrup}";

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

        public virtual void AyIcindeSadece1KezAyniGunNobetTutulsunDegiskenDonusumlu(KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu p)
        {
            if (!p.NobetUstGrupKisit.PasifMi)
            {
                var kisitAdi = $"{p.NobetUstGrupKisit.KisitAdi}"; //, {eczaneNobetGrup.EczaneAdi}";

                var tarihAraligi = p.Tarihler.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                foreach (var ikiliEczane in p.IkiliEczaneler)
                {
                    var kararIndexIkiliEczane = p.EczaneNobetTarihAralik
                             .Where(e => (e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId1 || e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId2)).ToList();

                    var kararIndex3 = p.EczaneNobetTarihAralikIkiliEczaneler
                                .Where(e => e.AyniGunTutulanNobetId == ikiliEczane.Id).ToList();

                    foreach (var tarih in p.Tarihler)
                    {
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
                    p.Model.AddConstraint(cns, kisitAdi);
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
        #endregion        
    }
}

/* gerek kalmadı
 #region gerek kalmadı

        //gerek kalmadı - yerine HerAyEnFazlaOrtalama
        /// <summary>
        /// Her eczaneye bir ayda en fazla grubun nöbet ortalaması ya da belirtilen kadar nöbet yazılmalı
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ortalamaNobetSayisi"></param>
        /// <param name="eczaneNobetGrup"></param>
        /// <param name="eczaneNobetTarihAralik"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="_x"></param>
        public virtual void HerAyEnFazlaGorev(Model model,
            double ortalamaNobetSayisi,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                if (nobetUstGrupKisitDetay.SagTarafDegeri > 0)
                    ortalamaNobetSayisi += (int)nobetUstGrupKisitDetay.SagTarafDegeri;

                var kararIndex = eczaneNobetTarihAralik;
                var std = ortalamaNobetSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                var cns = Constraint.LessThanOrEqual(exp, std);
                cns.LowerBound = 0;
                model.AddConstraint(cns, kisitAdi);
            }
        }

        //gerek kalmadı - yerine HerAyEnFazlaOrtalama
        /// <summary>
        /// Her eczaneye bir ayda en fazla nöbet grubunun hafta içi ortalaması kadar nöbet yazılmalı
        /// </summary>
        /// <param name="model"></param>
        /// <param name="haftaIciGunleri"></param>
        /// <param name="haftaIciOrtamalaNobetSayisi"></param>
        /// <param name="eczaneNobetGrup"></param>
        /// <param name="eczaneNobetTarihAralik"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="_x"></param>
        public virtual void HerAyEnFazlaHaftaIci(Model model,
            List<TakvimNobetGrup> haftaIciGunleri,
            double haftaIciOrtamalaNobetSayisi,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                if (nobetUstGrupKisitDetay.SagTarafDegeri > 0)
                    haftaIciOrtamalaNobetSayisi += (int)nobetUstGrupKisitDetay.SagTarafDegeri;

                var kararIndex = eczaneNobetTarihAralik
                    .Where(e => haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                var std = haftaIciOrtamalaNobetSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                var cns = Constraint.LessThanOrEqual(exp, std);
                cns.LowerBound = 0;

                model.AddConstraint(cns, kisitAdi);
            }
        }

        //gerek kalmadı - yerine HerAyEnFazlaOrtalama
        public virtual void HerAyEnFazla1Gunler(Model model,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            int gunDegerId,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            var kisitAdi = $"her_eczaneye_bir_ayda_en_az_nobet_grubunun_hedefinden_bir_eksik_nobet_yazilmali, {eczaneNobetGrup}";

            var kararIndex = eczaneNobetTarihAralik
                .Where(e => e.GunDegerId == gunDegerId).ToList();

            var std = 1;
            var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
            var cns = Constraint.LessThanOrEqual(exp, std);
            cns.LowerBound = 0;
            model.AddConstraint(cns, kisitAdi);
        }

        //gerek kalmadı - yerine HerAyEnFazlaOrtalama
        public virtual void HerAyEnFazla1Bayram(Model model,
            List<TakvimNobetGrup> bayramlar,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            VariableCollection<EczaneNobetTarihAralik> _x,
            bool kisitAktifMi)
        {
            if (bayramlar.Count > 0 && kisitAktifMi)
            {//aynı ayda en fazla 1 bayram nöbeti yazılsın
                var kisitAdi = $"her_ay_en_fazla_1_bayram, {eczaneNobetGrup}";

                var kararIndex = eczaneNobetTarihAralik
                    .Where(e => bayramlar.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                var std = 1;
                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                var cns = Constraint.LessThanOrEqual(exp, std);
                cns.LowerBound = 0;
                model.AddConstraint(cns, kisitAdi);
            }
        }

        //gerek kalmadı - yerine GunKumulatifToplamEnFazla
        public virtual void BayramToplamEnFazla(Model model,
            List<TakvimNobetGrup> bayramlar,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            int toplamBayramNobetSayisi,
            double yillikOrtalamaGunKuralSayisi,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            var kisitAdi = $"her_eczaneye_bir_ayda_nobet_grubunun_hedefi_kadar_toplam_bayram_nobeti_yazilmali, {eczaneNobetGrup.EczaneAdi}";

            var kararIndex = eczaneNobetTarihAralik
                .Where(e => bayramlar.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

            var std = yillikOrtalamaGunKuralSayisi;
            var exp = Expression.Sum(kararIndex.Select(i => _x[i])) + toplamBayramNobetSayisi;
            var cns = Constraint.LessThanOrEqual(exp, std);
            cns.LowerBound = 0;
            model.AddConstraint(cns, kisitAdi);
        }

        //gerek kalmadı - yerine HerAyEnFazlaOrtalama - yerine EsGruptakiEczanelereAyniGunNobetYazilmasin
        /// <summary>
        /// Eczane Grup Tanımdaki eczaneler aynı gün (aralığında) nöbet tutmasın
        /// </summary>
        /// <param name="model"></param>
        /// <param name="eczaneNobetTarihAralikTumu"></param>
        /// <param name="eczaneNobetSonuclarTumu"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="eczaneGrupTanimlar"></param>
        /// <param name="eczaneGruplarTumu"></param>
        /// <param name="nobetGrupGorevTip"></param>
        /// <param name="tarihler"></param>
        /// <param name="_x"></param>
        public virtual void EczaneGrup(
            Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
            List<EczaneNobetSonucListe2> eczaneNobetSonuclarTumu, //EczaneGrupNobetSonuc
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<EczaneGrupTanimDetay> eczaneGrupTanimlar,
            List<EczaneGrupDetay> eczaneGruplarTumu,
            NobetGrupGorevTipDetay nobetGrupGorevTip,
            List<TakvimNobetGrup> tarihler,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var eczaneGrupTanimlar2 = eczaneGruplarTumu.Select(s => new
                {
                    Id = s.EczaneGrupTanimId,
                    s.EczaneGrupTanimAdi,
                    s.EczaneGrupTanimBitisTarihi,
                    s.EczaneGrupTanimPasifMi,
                    s.EczaneGrupTanimTipAdi,
                    s.ArdisikNobetSayisi
                }).Distinct().ToList();

                foreach (var eczaneGrupTanim in eczaneGrupTanimlar2)
                {
                    if (eczaneGrupTanim.Id == 143)//g.Adi.Contains("EŞ-SEMİH-SERRA BALTA"))
                    {
                    }

                    var eczaneGruplar = eczaneGruplarTumu
                                            .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id)
                                            .Select(s => new
                                            {
                                                s.EczaneId,
                                                s.EczaneAdi,
                                                s.EczaneGrupTanimTipId
                                            }).Distinct().ToList();
                    #region kontrol
                    var kontol = false;

                    if (kontol)
                    {
                        var kontrolEdilecekGruptakiEczaneler = new string[] { "BAŞGÖR", "AVKAN" };

                        if (eczaneGruplar.Where(w => kontrolEdilecekGruptakiEczaneler.Contains(w.EczaneAdi)).Count() > 0)
                        {

                        }
                    }
                    #endregion

                    //çözülen dönemdeki eczanelerin mevcut nöbetleri
                    var gruptakiEczanelerinNobetTarihleri = eczaneNobetSonuclarTumu
                        .Where(w => eczaneGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

                    //değişkendeki eczaneler
                    var eczaneGruplar2 = eczaneGruplarTumu
                                            .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id
                                                     && x.NobetGrupId == nobetGrupGorevTip.NobetGrupId)
                                            .Select(s => new
                                            {
                                                s.EczaneId,
                                                s.EczaneAdi,
                                                s.EczaneGrupTanimTipId
                                            }).Distinct().ToList();

                    var ardisikNobetSayisi = eczaneGrupTanim.ArdisikNobetSayisi;

                    var tarihler2 = tarihler.Take(tarihler.Count() - ardisikNobetSayisi).ToList();

                    //peşpeşe gelmesin
                    if (eczaneGruplar2.Count > 0)
                    {
                        //ardisikNobetSayisi=0 ise bu kısıt, aynı nöbet grubundaki eş olan eczaneler için gereksizdir. 
                        //çünkü aynı grupta zaten aynı gün birden fazla nöbetçi bulunmuyor.
                        //ancak farklı gruptakiler için gereklidir. bu durum aşağıdaki eczane grup kısıtında tanımlıdır
                        if (ardisikNobetSayisi > 1)
                        {//aynı gruptaki eşler 
                            foreach (var tarih in tarihler2)
                            {
                                var kisitAdi = $"eczaneGrupTanimda_ayni_gruptaki_eczaneler_birlikte_nobet_tutmasin, {tarih.TakvimId}";

                                var kararIndex = eczaneNobetTarihAralikTumu
                                                   .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                                               && (e.Tarih >= tarih.Tarih && e.Tarih <= tarih.Tarih.AddDays(ardisikNobetSayisi))
                                                                    //&& (e.Gun >= tarih.Gun && e.Gun <= tarih.Gun + ardisikNobetSayisi)
                                                                    ).ToList();

                                var std = 1;
                                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                var cns = Constraint.LessThanOrEqual(exp, std);
                                cns.LowerBound = 0;

                                model.AddConstraint(cns, kisitAdi);
                            }
                        }

                        if (gruptakiEczanelerinNobetTarihleri.Count > 0)
                        {//farklı gruplardaki eşler
                            if (ardisikNobetSayisi > 0)
                            {
                                foreach (var tarih in gruptakiEczanelerinNobetTarihleri)
                                {
                                    var kisitAdi = $"eczaneGrupTanimdaki_eczaneler_birlikte_nobet_tutmasin, {tarih.TakvimId}";

                                    var kararIndex = eczaneNobetTarihAralikTumu
                                        .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                                && (e.Tarih >= tarih.Tarih.AddDays(-ardisikNobetSayisi) && e.Tarih <= tarih.Tarih.AddDays(ardisikNobetSayisi))
                                                //&& (e.Gun >= tarih.Tarih.Day - ardisikNobetSayisi && e.Gun <= tarih.Tarih.Day + ardisikNobetSayisi)
                                                )
                                        .ToList();

                                    var std = 0;
                                    var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                    var cns = Constraint.Equals(exp, std);
                                    cns.LowerBound = 0;

                                    model.AddConstraint(cns, kisitAdi);
                                }
                            }
                            else
                            {
                                foreach (var tarih in gruptakiEczanelerinNobetTarihleri)
                                {
                                    var kisitAdi = $"eczaneGrupTanimdaki_eczaneler_birlikte_nobet_tutmasin, {tarih.TakvimId}";

                                    var kararIndex = eczaneNobetTarihAralikTumu
                                        .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                                 && e.TakvimId == tarih.TakvimId)
                                        .ToList();

                                    var std = 0;
                                    var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                    var cns = Constraint.Equals(exp, std);
                                    cns.LowerBound = 0;

                                    model.AddConstraint(cns, kisitAdi);
                                }
                            }
                        }
                    }
                }
            }
        }

        //gerek kalmadı - yerine HerAyEnFazlaOrtalama - yerine EsGruptakiEczanelereAyniGunNobetYazilmasin
        /// <summary>
        /// Eczane Grup Tanımdaki eczaneler aynı gün (aralığında) nöbet tutmasın - önceliksiz çözüm
        /// </summary>
        /// <param name="model"></param>
        /// <param name="eczaneNobetTarihAralikTumu"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="eczaneGrupTanimlar"></param>
        /// <param name="eczaneGruplarTumu"></param>
        /// <param name="nobetGrupGorevTipler"></param>
        /// <param name="tarihlerTumu"></param>
        /// <param name="_x"></param>
        public virtual void EczaneGrupCokluCozum(
            Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<EczaneGrupTanimDetay> eczaneGrupTanimlar,
            List<EczaneGrupDetay> eczaneGruplarTumu,
            List<NobetGrupGorevTipDetay> nobetGrupGorevTipler,
            List<TakvimNobetGrup> tarihlerTumu,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var eczaneGrupTanimlar2 = eczaneGruplarTumu.Select(s => new
                {
                    Id = s.EczaneGrupTanimId,
                    s.EczaneGrupTanimAdi,
                    s.EczaneGrupTanimBitisTarihi,
                    s.EczaneGrupTanimPasifMi,
                    s.EczaneGrupTanimTipAdi,
                    s.ArdisikNobetSayisi
                }).Distinct().ToList();

                foreach (var eczaneGrupTanim in eczaneGrupTanimlar2)
                {
                    var eczaneGruplar = eczaneGruplarTumu
                                            .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id)
                                            .Select(s => new
                                            {
                                                s.EczaneId,
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

                        var kontrolEdilecekGruptakiEczaneler = new string[] { "BAŞGÖR", "AVKAN" };

                        if (eczaneGruplar.Where(w => kontrolEdilecekGruptakiEczaneler.Contains(w.EczaneAdi)).Count() > 0)
                        {
                        }
                    }
                    #endregion

                    //değişkendeki eczaneler
                    var eczaneGruplar2 = eczaneGruplarTumu
                                            .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id
                                                     && nobetGrupGorevTipler.Select(s => s.NobetGrupId).Contains(x.NobetGrupId))
                                            .Select(s => new
                                            {
                                                s.EczaneId,
                                                s.EczaneAdi,
                                                s.EczaneGrupTanimTipId
                                            }).Distinct().ToList();

                    var ardisikNobetSayisi = eczaneGrupTanim.ArdisikNobetSayisi;

                    var tarihAraligi = tarihlerTumu
                            .Select(s => new
                            {
                                s.TakvimId,
                                s.Tarih,
                                //s.Gun
                            }).Distinct().ToList();

                    var tarihler = tarihAraligi.Take(tarihAraligi.Count() - ardisikNobetSayisi).ToList();

                    //peşpeşe gelmesin
                    if (eczaneGruplar2.Count > 0)
                    {
                        //ardisikNobetSayisi=0 ise bu kısıt, aynı nöbet grubundaki eş olan eczaneler için gereksizdir. 
                        //çünkü aynı grupta zaten aynı gün birden fazla nöbetçi bulunmuyor.
                        //ancak farklı gruptakiler için gereklidir.
                        if (ardisikNobetSayisi == 0)
                        {
                            foreach (var tarih in tarihler)
                            {
                                var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gun_birlikte_nobet_tutmasin, {tarih.TakvimId}";

                                var kararIndex = eczaneNobetTarihAralikTumu
                                                   .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                                                && e.TakvimId == tarih.TakvimId).ToList();

                                var std = 1;
                                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                var cns = Constraint.LessThanOrEqual(exp, std);
                                cns.LowerBound = 0;

                                model.AddConstraint(cns, kisitAdi);
                            }
                        }
                        else
                        {
                            foreach (var tarih in tarihler)
                            {
                                var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gunlerde_birlikte_nobet_tutmasin, {tarih.TakvimId}";

                                var kararIndex = eczaneNobetTarihAralikTumu
                                    .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                                                && (e.Tarih >= tarih.Tarih && e.Tarih <= tarih.Tarih.AddDays(ardisikNobetSayisi))
                                                                //&& (e.Gun >= tarih.Gun && e.Gun <= tarih.Gun + ardisikNobetSayisi)
                                                                )
                                    .ToList();

                                var std = 1;
                                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                var cns = Constraint.LessThanOrEqual(exp, std);
                                cns.LowerBound = 0;

                                model.AddConstraint(cns, kisitAdi);
                            }
                        }
                    }
                }
            }
        }

        //gerek kalmadı - yerine PesPeseGorevEnAz
        /// <summary>
        /// Hafta içi nöbeti peş peşe en az ([gruptaki nöbetçi sayısı] * 1.2 * 0.7666) kadar gün geçmeden yazılmasın.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="eczaneGrupNobetSonuclar"></param>
        /// <param name="eczaneNobetTarihAralik"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="haftaIciGunleri"></param>
        /// <param name="eczaneNobetGrup"></param>
        /// <param name="altLimit"></param>
        /// <param name="_x"></param>
        public virtual void HaftaIciPespeseGorevEnAz(Model model,
            List<EczaneNobetSonucListe2> eczaneGrupNobetSonuclar,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<TakvimNobetGrup> haftaIciGunleri,
            EczaneNobetGrupDetay eczaneNobetGrup,
            double altLimit,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                var enSonNobetTarihi = eczaneGrupNobetSonuclar
                    .Where(w => haftaIciGunleri.Select(s => s.NobetGunKuralId).Contains(w.NobetGunKuralId))
                    .Select(s => s.Tarih)
                    .OrderByDescending(o => o)
                    .FirstOrDefault();

                if (enSonNobetTarihi != null)
                {
                    var nobetYazilabilecekIlkTarih = enSonNobetTarihi.AddDays(altLimit);

                    var tarihAralik = haftaIciGunleri
                        .Where(w => w.Tarih <= nobetYazilabilecekIlkTarih)
                        .Select(s => s.TakvimId).ToList();

                    if (tarihAralik.Count > 0)
                    {
                        var kararIndex = eczaneNobetTarihAralik
                                           .Where(e => tarihAralik.Contains(e.TakvimId)).ToList();

                        var std = 0;
                        var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                        var cns = Constraint.Equals(exp, std);
                        cns.LowerBound = 0;
                        model.AddConstraint(cns, kisitAdi);
                    }
                }
            }
        }

        //gerek kalmadı - yerine PesPeseGorevEnAz
        /// <summary>
        /// Eczanelere farklı aylarda pazar günü peşpeşe nöbet yazılmasın enaz
        /// </summary>
        /// <param name="model"></param>
        /// <param name="eczaneGrupNobetSonuclar"></param>
        /// <param name="eczaneNobetTarihAralik"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="pazarGunleri"></param>
        /// <param name="gruptakiNobetciSayisi"></param>
        /// <param name="eczaneNobetGrup"></param>
        /// <param name="_x"></param>
        public virtual void PazarPespeseGorevEnAz(Model model,
            List<EczaneNobetSonucListe2> eczaneGrupNobetSonuclar,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<TakvimNobetGrup> pazarGunleri,
            int gruptakiNobetciSayisi,
            EczaneNobetGrupDetay eczaneNobetGrup,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var enSonPazarNobetTarihi = eczaneGrupNobetSonuclar
                    .Where(w => w.NobetGunKuralId == 1)
                    .Select(s => s.Tarih)
                    .OrderByDescending(o => o)
                    .FirstOrDefault();

                if (enSonPazarNobetTarihi != null)
                {
                    var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;

                    var yazilabilecekIlkTarih = enSonPazarNobetTarihi.AddMonths(pazarNobetiYazilabilecekIlkAy - 1);

                    var tarihAralik = pazarGunleri
                           .Where(w => w.Tarih <= yazilabilecekIlkTarih)
                           .Select(s => s.TakvimId).ToList();

                    if (tarihAralik.Count > 0)
                    {
                        var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                        var kararIndex = eczaneNobetTarihAralik
                                           .Where(e => tarihAralik.Contains(e.TakvimId)).ToList();

                        var std = 0;
                        var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                        var cns = Constraint.Equals(exp, std);
                        model.AddConstraint(cns, kisitAdi);
                    }
                }
            }
        }

        //gerek kalmadı - yerine PesPeseGorevEnAz
        /// <summary>
        /// Eczanelere farklı iki ay arasında peşpeşe nöbet yazılmasın
        /// </summary>
        /// <param name="model"></param>
        /// <param name="eczaneGrupNobetSonuclar"></param>
        /// <param name="eczaneNobetTarihAralik"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="tarihler"></param>
        /// <param name="pespeseNobetSayisi"></param>
        /// <param name="eczaneNobetGrup"></param>
        /// <param name="_x"></param>
        public virtual void FarkliAyPespeseGorev(Model model,
            List<EczaneNobetSonucListe2> eczaneGrupNobetSonuclar,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<TakvimNobetGrup> tarihler,
            int pespeseNobetSayisi,
            EczaneNobetGrupDetay eczaneNobetGrup,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                var enSonNobetTarihi = eczaneGrupNobetSonuclar
                    .Select(s => s.Tarih)
                    .OrderByDescending(o => o)
                    .FirstOrDefault();

                if (enSonNobetTarihi != null)
                {
                    var nobetYazilabilecekIlkTarih = enSonNobetTarihi.AddDays(pespeseNobetSayisi);

                    var tarihAralik = tarihler
                        .Where(w => w.Tarih <= nobetYazilabilecekIlkTarih)
                        .Select(s => s.TakvimId).ToList();

                    if (tarihAralik.Count > 0)
                    {
                        var kararIndex = eczaneNobetTarihAralik
                                           .Where(e => tarihAralik.Contains(e.TakvimId)).ToList();

                        var std = 0;
                        var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                        var cns = Constraint.Equals(exp, std);
                        model.AddConstraint(cns, kisitAdi);
                    }
                }
            }
        }

        //gerek kalmadı - yerine IstenenTarihAraligindaEnAz1NobetYaz

        ///// <summary>
        ///// her eczaneye bir ayda en az 1 nobet yazılsın
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="nobetUstGrupKisitDetay"></param>
        ///// <param name="tarihler"></param>
        ///// <param name="gruptakiNobetciSayisi"></param>
        ///// <param name="eczaneNobetGrup"></param>
        ///// <param name="eczaneNobetTarihAralikEczaneBazli"></param>
        ///// <param name="_x"></param>
        //public void HerAyEnAz1Gorev(Model model,
        //    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        //    List<TakvimNobetGrup> tarihler,
        //    int gruptakiNobetciSayisi,
        //    EczaneNobetGrupDetay eczaneNobetGrup,
        //    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikEczaneBazli,
        //    VariableCollection<EczaneNobetTarihAralik> _x)
        //{
        //    if (!nobetUstGrupKisitDetay.PasifMi && gruptakiNobetciSayisi <= tarihler.Count)
        //    {
        //        var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

        //        var kararIndex = eczaneNobetTarihAralikEczaneBazli;

        //        var enAzNobetSayisi = 1;

        //        if (nobetUstGrupKisitDetay.SagTarafDegeri > 0)
        //            enAzNobetSayisi = (int)nobetUstGrupKisitDetay.SagTarafDegeri;

        //        var std = enAzNobetSayisi;
        //        var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
        //        var cns = Constraint.GreaterThanOrEqual(exp, std);
        //        //var isTriviallyFeasible = cns.IsTriviallyFeasible();

        //        model.AddConstraint(cns, kisitAdi);
        //    }
        //}

        //gerek kalmadı - yerine HerAyEnAz1Gorev
        ///// <summary>
        ///// her eczaneye bir ayda en az 1 hafta içi nöobeti yazılsın
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="nobetUstGrupKisitDetay"></param>
        ///// <param name="haftaIciGunleri"></param>
        ///// <param name="gruptakiNobetciSayisi"></param>
        ///// <param name="eczaneNobetGrup"></param>
        ///// <param name="eczaneNobetTarihAralikEczaneBazli"></param>
        ///// <param name="_x"></param>
        //public void HerAyEnAz1HaftaIciGorev(Model model,
        //    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        //    List<TakvimNobetGrup> haftaIciGunleri,
        //    int gruptakiNobetciSayisi,
        //    EczaneNobetGrupDetay eczaneNobetGrup,
        //    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikEczaneBazli,
        //    VariableCollection<EczaneNobetTarihAralik> _x)
        //{
        //    if (!nobetUstGrupKisitDetay.PasifMi && gruptakiNobetciSayisi <= haftaIciGunleri.Count)
        //    {
        //        var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";
        //        var kararIndex = eczaneNobetTarihAralikEczaneBazli
        //                                .Where(e => haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

        //        var enAzNobetSayisi = 1;

        //        if (nobetUstGrupKisitDetay.SagTarafDegeri > 0)
        //            enAzNobetSayisi = (int)nobetUstGrupKisitDetay.SagTarafDegeri;

        //        var std = enAzNobetSayisi;
        //        var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
        //        var cns = Constraint.GreaterThanOrEqual(exp, std);
        //        //var isTriviallyFeasible = cns.IsTriviallyFeasible();

        //        model.AddConstraint(cns, kisitAdi);
        //    }
        //}

        public virtual void HaftaIciGunleri(Model model,
            List<TakvimNobetGrup> tarihler,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikEczaneBazli,
            double yazilabilecekHaftaIciNobetSayisi,
            int toplamNobetSayisi,
            NobetUstGrupKisitDetay haftaninGunleriDagilimi,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!haftaninGunleriDagilimi.PasifMi)
            {
                var kisitAdi = $"hafta ici ortalamasini gecmesin, {eczaneNobetGrup.EczaneAdi}";

                var kararIndex = eczaneNobetTarihAralikEczaneBazli
                                   .Where(e => tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                var std = yazilabilecekHaftaIciNobetSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => _x[i])) + toplamNobetSayisi;
                var cns = Constraint.LessThanOrEqual(exp, std);
                cns.LowerBound = 0;
                model.AddConstraint(cns, kisitAdi);
            }
        }
        #endregion
 */

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

/*
#region Diğer Günlerin Hedefleri
//gunDegerler = tarihler.Select(s => s.GunDegerId).Distinct().ToList();
//gunKurallar = data.NobetGrupGunKurallar
//                            .Where(s => s.NobetGrupId == nobetGrupGorevTip.NobetGrupId)
//                            .Select(s => s.NobetGunKuralId);

//var gunler = gunDegerler.Where(s => gunKurallar.Contains(s)).ToList();

//foreach (var gunDeger in gunler)
//{
//    GetEczaneGunHedef(eczaneNobetGrup, out double maxArz, out double minArz, gunDeger);

//    var digerGunlerMaxHedef = data.NobetUstGrupKisitlar
//                                .Where(s => s.KisitAdi == "digerGunlerMaxHedef"
//                                         && s.NobetUstGrupId == data.NobetUstGrupId)
//                                .Select(w => w.PasifMi == false).SingleOrDefault();

//    if (digerGunlerMaxHedef)
//    {
//        if (maxArz < 1) maxArz = 0;

//        //if (gunDeger == 1 && pazarSayisi > pazarEksikEczaneler.Count)
//        //{
//        //    //pazar günü yazılacak eczane sayısı azsa, ihtiyaç kadar eczaneye 1 pazar ekle
//        //    if (pazarYazilacakEczaneler.Contains(eczaneNobetGrup))
//        //    {
//        //        maxArz = hedef.Pazar + 1;
//        //    }

//        //}

//        model.AddConstraint(
//          Expression.Sum(data.EczaneNobetTarihAralik
//                           .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
//                                       && e.GunDegerId == gunDeger
//                                       && e.NobetGrupId == nobetGrupGorevTip.NobetGrupId)
//                           .Select(m => _x[m])) <= maxArz,
//                           $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {eczaneNobetGrup}");
//    }

//    var digerGunlerMinHedef = data.NobetUstGrupKisitlar
//                                .Where(s => s.KisitAdi == "digerGunlerMinHedef"
//                                         && s.NobetUstGrupId == data.NobetUstGrupId)
//                                .Select(w => w.PasifMi == false).SingleOrDefault();

//    if (digerGunlerMinHedef)
//    {

//        if (minArz < 1) minArz = 0;

//        model.AddConstraint(
//            Expression.Sum(data.EczaneNobetTarihAralik
//                            .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
//                                        && e.GunDegerId == gunDeger
//                                        && e.NobetGrupId == nobetGrupGorevTip.NobetGrupId
//                                        )
//                            .Select(m => _x[m])) >= minArz,
//                            $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {eczaneNobetGrup}");
//    }
//}
#endregion

* 
        var nobetIstatistigiCumartesiler = data.EczaneNobetGrupGunKuralIstatistikler
            .Where(w => w.NobetGunKuralId == 7).ToList();
        var nobetIstatistigiPazarlar = data.EczaneNobetGrupGunKuralIstatistikler
            .Where(w => w.NobetGunKuralId == 1).ToList();
        var nobetIstatistigiBayramlar = data.EczaneNobetGrupGunKuralIstatistikler
            .Where(w => w.NobetGunKuralId > 7).ToList();
        var nobetIstatistigiHaftaIci = data.EczaneNobetGrupGunKuralIstatistikler
            .Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId < 7).ToList();



            //+ _x[i] * Convert.ToInt32(i.CumartesiGunuMu)  * (BigMCumartesiCevrim / ((cozulenAyIlkCumartesi.Tarih - p.SonNobetTarihiCumartesi).TotalDays))
        //+_x[i] * Convert.ToInt32(i.PazarGunuMu)       * (BigMPazarCevrim    / ((cozulenAyIlkPazar.Tarih - p.SonNobetTarihiPazar).TotalDays))
        //+ _x[i] * Convert.ToInt32(i.BayramMi)         * (BigMBayramCevrim   / ((cozulenAyIlkBayram.Tarih - p.SonNobetTarihiBayram).TotalDays))
        //+ _x[i] * Convert.ToInt32(i.HaftaIciMi)       * (BigMHaftaIciCevrim / ((cozulenAyIlkHaftaIci.Tarih - p.SonNobetTarihiHaftaIci).TotalDays))

        //int BigMCuma = 20000;
        //int BigMCumartesi = 40000;
        //int BigMPazartesi = 500;
        //int BigMSali = 500;
        //int BigMCarsamba = 500;
        //int BigMPersembe = 500;
        //int BigMHaftaIciToplam = 80000;//200000;//15000;

            //var nobetIstatistigi = data.EczaneNobetGrupGunKuralIstatistikler
        //   .GroupBy(g => new
        //   {
        //       g.EczaneNobetGrupId,
        //       g.EczaneId,
        //       g.EczaneAdi,
        //       g.NobetGrupAdi,
        //       g.NobetGrupId,
        //       g.NobetGorevTipId,
        //       g.NobetAltGrupId
        //   })
        //   .Select(s => new EczaneNobetGrupGunKuralIstatistikYatay
        //   {
        //       EczaneNobetGrupId= s.Key.EczaneNobetGrupId,
        //       EczaneId = s.Key.EczaneId,
        //       EczaneAdi = s.Key.EczaneAdi,
        //       NobetGrupId = s.Key.NobetGrupId,
        //       NobetGrupAdi = s.Key.NobetGrupAdi,
        //       NobetGorevTipId = s.Key.NobetGorevTipId,
        //       NobetAltGrupId = s.Key.NobetAltGrupId,

        //       NobetSayisiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisi),
        //       SonNobetTarihiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisi) > 0
        //       ? s.Where(w => w.NobetGunKuralId == 7).Max(f => f.SonNobetTarihi)
        //       : new DateTime(2010, 1, 1),

        //       NobetSayisiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisi),
        //       SonNobetTarihiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisi) > 0
        //       ? s.Where(w => w.NobetGunKuralId == 1).Max(f => f.SonNobetTarihi)
        //       : new DateTime(2010, 1, 1),

        //       NobetSayisiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisi),
        //       SonNobetTarihiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisi) > 0
        //       ? s.Where(w => w.NobetGunKuralId > 7).Max(f => f.SonNobetTarihi)
        //       : new DateTime(2010, 1, 1),

        //       NobetSayisiHaftaIci = s.Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId <= 7).Sum(f => f.NobetSayisi),
        //       SonNobetTarihiHaftaIci = s.Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId <= 7).Sum(f => f.NobetSayisi) > 0
        //       ? s.Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId <= 7).Max(f => f.SonNobetTarihi)
        //       : new DateTime(2010, 1, 1),
        //       //NobetSayisiPazartesi = s.Where(w => w.NobetGunKuralId == 2).Sum(f => f.NobetSayisi),
        //       //NobetSayisiSali = s.Where(w => w.NobetGunKuralId == 3).Sum(f => f.NobetSayisi),
        //       //NobetSayisiCarsamba = s.Where(w => w.NobetGunKuralId == 4).Sum(f => f.NobetSayisi),
        //       //NobetSayisiPersembe = s.Where(w => w.NobetGunKuralId == 5).Sum(f => f.NobetSayisi),
        //       //NobetSayisiCuma = s.Where(w => w.NobetGunKuralId == 6).Sum(f => f.NobetSayisi)
        //   }).ToList();
        */

/*
 *         public virtual void TarihAraligiOrtalamaEnFazla(Model model,
        List<TakvimNobetGrup> tarihler,
        int gunSayisi,
        double ortalamaNobetSayisi,
        EczaneNobetGrupDetay eczaneNobetGrup,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
        NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        VariableCollection<EczaneNobetTarihAralik> _x)
    {
        if (gunSayisi > 0 && !nobetUstGrupKisitDetay.PasifMi)
        {
            var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

            if (nobetUstGrupKisitDetay.SagTarafDegeri > 0)
                ortalamaNobetSayisi += (int)nobetUstGrupKisitDetay.SagTarafDegeri;

            var kararIndex = eczaneNobetTarihAralik
                .Where(e => tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

            var std = ortalamaNobetSayisi;
            var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
            var cns = Constraint.LessThanOrEqual(exp, std);
            cns.LowerBound = 0;

            model.AddConstraint(cns, kisitAdi);
        }
    }
 */

/*
    /// <summary>
    /// İstenen günler için bir sonraki nöbet peş peşe en az gün geçmeden yazılmaz.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="enSonNobetTarihi"></param>
    /// <param name="nobetUstGrupBaslamaTarihi"></param>
    /// <param name="eczaneNobetTarihAralik"></param>
    /// <param name="nobetUstGrupKisitDetay"></param>
    /// <param name="tarihler"></param>
    /// <param name="nobetYazilabilecekIlkTarih"></param>
    /// <param name="eczaneNobetGrup"></param>
    /// <param name="_x"></param>
    public virtual void PesPeseGorevEnAz(Model model,
        int nobetSayisi,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
        NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        List<TakvimNobetGrup> tarihler,
        DateTime nobetYazilabilecekIlkTarih,
        EczaneNobetGrupDetay eczaneNobetGrup,
        VariableCollection<EczaneNobetTarihAralik> _x)
    {
        if (!nobetUstGrupKisitDetay.PasifMi && nobetSayisi > 0
            //enSonNobetTarihi >= nobetUstGrupBaslamaTarihi
            )
        {
            var tarihAralik = tarihler
              .Where(w => w.Tarih <= nobetYazilabilecekIlkTarih)
              .Select(s => s.TakvimId).ToList();

            if (tarihAralik.Count > 0)
            {
                var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                //var ss = eczaneNobetTarihAralik.Where(w => w.NobetGorevTipId == 2).ToList();

                //if (ss.Count() > 0)
                //{
                //}

                var kararIndex = eczaneNobetTarihAralik
                                   .Where(e => tarihAralik.Contains(e.TakvimId)).ToList();

                var std = 0;
                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                var cns = Constraint.Equals(exp, std);
                model.AddConstraint(cns, kisitAdi);
            }
        }
    }     
 */

/*
         /// <summary>
    /// Eczane Grup Tanımdaki (eş grup) eczanelere aynı gün ya da aynı gün aralığında nöbet yazılmaz.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="eczaneNobetTarihAralikTumu"></param>
    /// <param name="eczaneNobetSonuclarTumu"></param>
    /// <param name="nobetUstGrupKisitDetay"></param>
    /// <param name="eczaneGruplarTumu"></param>
    /// <param name="nobetGrupGorevTipler"></param>
    /// <param name="tarihlerTumu"></param>
    /// <param name="_x"></param>
    public virtual void EsGruptakiEczanelereAyniGunNobetYazma(
        Model model,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
        List<EczaneNobetSonucListe2> eczaneNobetSonuclarTumu,
        NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        List<EczaneGrupDetay> eczaneGruplarTumu,
        List<TakvimNobetGrup> tarihlerTumu,
        VariableCollection<EczaneNobetTarihAralik> _x)
    {
        if (!nobetUstGrupKisitDetay.PasifMi)
        {
            var eczaneGrupTanimlar = eczaneGruplarTumu
                .Select(s => new
                {
                    Id = s.EczaneGrupTanimId,
                    s.EczaneGrupTanimTipId,
                    s.EczaneGrupTanimAdi,
                    s.EczaneGrupTanimBitisTarihi,
                    s.EczaneGrupTanimPasifMi,
                    s.EczaneGrupTanimTipAdi,
                    s.ArdisikNobetSayisi
                }).Distinct().ToList();

            var tarihAraligi = tarihlerTumu
                .Select(s => new
                {
                    s.TakvimId,
                    s.Tarih,
                    //s.Gun
                }).Distinct().ToList();

            if (tarihAraligi.Count > 0)
            {
                foreach (var eczaneGrupTanim in eczaneGrupTanimlar)
                {
                    var eczaneGruplar = eczaneGruplarTumu
                                            .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id)
                                            .Select(s => new
                                            {
                                                s.EczaneId,
                                                s.EczaneNobetGrupId,
                                                s.EczaneAdi,
                                                s.EczaneGrupTanimTipId
                                            }).Distinct().ToList();

                    #region kontrol
                    var kontol = true;

                    if (kontol)
                    {
                        if (eczaneGrupTanim.Id == 143)//g.Adi.Contains("EŞ-SEMİH-SERRA BALTA"))
                        {
                        }

                        var kontrolEdilecekGruptakiEczaneler = new string[] { "ÇOTANAK", "PINAR" };

                        if (eczaneGruplar.Where(w => kontrolEdilecekGruptakiEczaneler.Contains(w.EczaneAdi)).Count() > 0)
                        {
                        }
                    }
                    #endregion

                    var kararIndexMaster = eczaneNobetTarihAralikTumu
                                           .Where(e => eczaneGruplar.Select(s => s.EczaneNobetGrupId).Contains(e.EczaneNobetGrupId)
                                                    && tarihAraligi.Select(s => s.TakvimId).Contains(e.TakvimId)
                                           ).ToList();

                    var gruptakiEczanelerinNobetTarihleri = new List<EczaneNobetSonucListe2>();

                    //çözülen dönemde aynı eş gruptaki eczanelerin mevcut nöbetleri - öncelikli çözüm
                    if (eczaneNobetSonuclarTumu.Count > 0)
                    {
                        gruptakiEczanelerinNobetTarihleri = eczaneNobetSonuclarTumu
                            .Where(w => eczaneGruplar.Select(s => s.EczaneNobetGrupId).Contains(w.EczaneNobetGrupId)
                                     && tarihAraligi.Select(s => s.TakvimId).Contains(w.TakvimId)).ToList();
                    }

                    var ardisikNobetSayisi = eczaneGrupTanim.ArdisikNobetSayisi;

                    if (eczaneGruplar.Count > 0)
                    {
                        //ardisikNobetSayisi = 0 aynı grupta aynı nöbet grubunda aynı gün birden fazla nöbetçi bulunmadığında aynı nöbet grubundaki eş eczaneler için gereksizdir.
                        //ancak farklı gruptakiler için gereklidir.
                        if (ardisikNobetSayisi == 0)
                        {
                            foreach (var tarih in tarihAraligi)
                            {
                                var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gun_birlikte_nobet_tutmasin, {tarih.TakvimId}";

                                var kararIndex = kararIndexMaster
                                    .Where(e => e.TakvimId == tarih.TakvimId)
                                    .ToList();

                                if (kararIndex.Count > 0)
                                {
                                    var std = 1;
                                    var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                    var cns = Constraint.LessThanOrEqual(exp, std);
                                    cns.LowerBound = 0;

                                    model.AddConstraint(cns, kisitAdi);
                                }
                            }

                            if (gruptakiEczanelerinNobetTarihleri.Count > 0)
                            {//farklı nöbet gruplardaki eşler ile aynı gün nöbet tutmamak için - öncelikli çözüm
                                foreach (var tarih in gruptakiEczanelerinNobetTarihleri)
                                {
                                    var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gun_birlikte_nobet_tutmasin_oncelikli_cozum, {tarih.TakvimId}";

                                    var kararIndex = kararIndexMaster
                                        .Where(e => e.TakvimId == tarih.TakvimId)
                                        .ToList();

                                    var std = 0;
                                    var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                    var cns = Constraint.Equals(exp, std);
                                    cns.LowerBound = 0;

                                    model.AddConstraint(cns, kisitAdi);
                                }
                            }
                        }
                        else
                        {
                            var tarihler = tarihAraligi.Take(tarihAraligi.Count() - ardisikNobetSayisi).ToList();

                            foreach (var tarih in tarihler)
                            {
                                var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gunlerde_birlikte_nobet_tutmasin, {tarih.TakvimId}";

                                var kararIndex = kararIndexMaster
                                    .Where(e => (e.Tarih >= tarih.Tarih && e.Tarih <= tarih.Tarih.AddDays(ardisikNobetSayisi)))
                                    .ToList();

                                var std = 1;
                                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                var cns = Constraint.LessThanOrEqual(exp, std);
                                cns.LowerBound = 0;

                                model.AddConstraint(cns, kisitAdi);
                            }

                            if (gruptakiEczanelerinNobetTarihleri.Count > 0)
                            {//farklı nöbet gruplardaki eşler ile aynı gün aralığında nöbet tutmamak için - öncelikli çözüm
                                foreach (var tarih in gruptakiEczanelerinNobetTarihleri)
                                {
                                    var kisitAdi = $"ayni_es_gruptaki_eczaneler_ayni_gunlerde_birlikte_nobet_tutmasin_oncelikli_cozum, {tarih.TakvimId}";

                                    var kararIndex = kararIndexMaster
                                        .Where(e => (e.Tarih >= tarih.Tarih.AddDays(-ardisikNobetSayisi) && e.Tarih <= tarih.Tarih.AddDays(ardisikNobetSayisi)))
                                        .ToList();

                                    var std = 0;
                                    var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                                    var cns = Constraint.Equals(exp, std);
                                    cns.LowerBound = 0;

                                    model.AddConstraint(cns, kisitAdi);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
 */

/*
 *         /// <summary>
    /// Tüm günlerin tur takip kısıtı
    /// </summary>
    /// <param name="model"></param>
    /// <param name="tarihler"></param>
    /// <param name="eczaneNobetGrup"></param>
    /// <param name="eczaneNobetTarihAralik"></param>
    /// <param name="nobetUstGrupKisitDetay"></param>
    /// <param name="kumulatifOrtalamaGunKuralSayisi"></param>
    /// <param name="toplamNobetSayisi"></param>
    /// <param name="_x"></param>
    public virtual void KumulatifToplamEnFazla(Model model,
        List<TakvimNobetGrup> tarihler,
        EczaneNobetGrupDetay eczaneNobetGrup,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
        NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        double kumulatifOrtalamaGunKuralSayisi,
        int toplamNobetSayisi,
        VariableCollection<EczaneNobetTarihAralik> _x)
    {
        if (!nobetUstGrupKisitDetay.PasifMi)
        {
            var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

            var kararIndex = eczaneNobetTarihAralik
                               .Where(e => tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

            var std = kumulatifOrtalamaGunKuralSayisi;
            var exp = Expression.Sum(kararIndex.Select(i => _x[i])) + toplamNobetSayisi;
            var cns = Constraint.LessThanOrEqual(exp, std);
            cns.LowerBound = 0;
            model.AddConstraint(cns, kisitAdi);
        }
    }
 */

/*
    /// <summary>
    /// Eczanelere ay içinde peşpeşe nöbet yazılmasın
    /// </summary>
    /// <param name="model"></param>
    /// <param name="eczaneNobetTarihAralik"></param>
    /// <param name="nobetUstGrupKisitDetay"></param>
    /// <param name="tarihler"></param>
    /// <param name="pespeseNobetSayisi"></param>
    /// <param name="eczaneNobetGrup"></param>
    /// <param name="_x"></param>
    public virtual void HerAyPespeseGorev(Model model,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
        NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        List<TakvimNobetGrup> tarihler,
        int pespeseNobetSayisi,
        EczaneNobetGrupDetay eczaneNobetGrup,
        VariableCollection<EczaneNobetTarihAralik> _x)
    {
        if (!nobetUstGrupKisitDetay.PasifMi)
        {
            var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";
            var tarihler2 = tarihler.Take(tarihler.Count - pespeseNobetSayisi).ToList();

            foreach (var tarih in tarihler2)
            {
                var kararIndex = eczaneNobetTarihAralik
                                   .Where(e => (e.Tarih >= tarih.Tarih && e.Tarih <= tarih.Tarih.AddDays(pespeseNobetSayisi))).ToList();

                var std = 1;
                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                var cns = Constraint.LessThanOrEqual(exp, std);
                cns.LowerBound = 0;
                //var isTriviallyFeasible = cns.IsTriviallyFeasible();

                model.AddConstraint(cns, kisitAdi);
            }
        }
    }     
 */

/*
         /// <summary>
        /// Mazeret girilen tarihlere nöbet yazılır.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="eczaneNobetTarihAralikTumu"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="eczaneNobetMazeretler"></param>
        /// <param name="_x"></param>
        public virtual void MazereteGorevYazma(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<EczaneNobetMazeretDetay> eczaneNobetMazeretler,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                foreach (var eczaneNobetMazeret in eczaneNobetMazeretler)
                {
                    var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetMazeret.EczaneAdi}, {eczaneNobetMazeret.Tarih}, {eczaneNobetMazeret.MazeretAdi}";

                    var kararIndex = eczaneNobetTarihAralikTumu
                        .Where(e => e.EczaneNobetGrupId == eczaneNobetMazeret.EczaneNobetGrupId
                                 && e.TakvimId == eczaneNobetMazeret.TakvimId).ToList();

                    var std = 0;
                    var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                    var cns = Constraint.Equals(exp, std);
                    model.AddConstraint(cns, kisitAdi);
                }
            }
        } 
 */

/*
         /// <summary>
        /// İstek girilen tarihlere nöbet yazılır.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="eczaneNobetTarihAralikTumu"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="eczaneNobetIstekler"></param>
        /// <param name="_x"></param>
        public virtual void IstegiKarsila(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<EczaneNobetIstekDetay> eczaneNobetIstekler,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                foreach (var eczaneNobetIstek in eczaneNobetIstekler)
                {
                    var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetIstek.EczaneAdi}, {eczaneNobetIstek.Tarih}, {eczaneNobetIstek.IstekAdi}";

                    var kararIndex = eczaneNobetTarihAralikTumu
                        .Where(e => e.EczaneNobetGrupId == eczaneNobetIstek.EczaneNobetGrupId
                                 && e.TakvimId == eczaneNobetIstek.TakvimId).ToList();

                    if (kararIndex.Count > 0)
                    {
                        var std = 1;
                        var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                        var cns = Constraint.Equals(exp, std);
                        //var isTriviallyFeasible = cns.IsTriviallyFeasible();

                        model.AddConstraint(cns, kisitAdi);
                    }
                }
            }
        }
     */

/*
/// <summary>
    /// Bayramlarda sırayla (dini ise milli ya da tersi) peş peşe nöbet yazılır.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="bayramlar"></param>
    /// <param name="eczaneNobetGrup"></param>
    /// <param name="eczaneNobetTarihAralik"></param>
    /// <param name="sonBayramTuru"></param>
    /// <param name="_x"></param>
    public virtual void PespeseFarkliTurNobetYaz(Model model,
        List<TakvimNobetGrup> bayramlar,
        EczaneNobetGrupDetay eczaneNobetGrup,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
        int sonBayramTuru,
        VariableCollection<EczaneNobetTarihAralik> _x)
    {
        var bayramlar2 = bayramlar.Where(w => w.NobetGunKuralId == sonBayramTuru).ToList();

        var kisitAdi = $"her_eczaneye_son_tuttugu_bayram_nobetinden_farkli_bayram_nobeti_yaz, {eczaneNobetGrup}";

        var kararIndex = eczaneNobetTarihAralik
            .Where(e => bayramlar2.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

        var std = 0;
        var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
        var cns = Constraint.Equals(exp, std);
        model.AddConstraint(cns, kisitAdi);
    }     
 */

/*
public virtual void HerAyHaftaIciPespeseGorev(Model model,
        List<TakvimNobetGrup> tarihler,
        List<TakvimNobetGrup> haftaIciGunleri,
        double haftaIciOrtamalaNobetSayisi,
        EczaneNobetGrupDetay eczaneNobetGrup,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
        double pespeseNobetSayisiAltLimit,
        VariableCollection<EczaneNobetTarihAralik> _x,
        bool kisitAktifMi)
    {
        if (kisitAktifMi && haftaIciOrtamalaNobetSayisi > 1)
        {
            var gunSayisi = tarihler.Count;

            if (pespeseNobetSayisiAltLimit >= gunSayisi)
            {
                var kisitAdi = $"eczanelere_haftaici_pespese_nobet_yazilmasin_1, {eczaneNobetGrup}";

                var kararIndex = eczaneNobetTarihAralik
                    .Where(e => haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                var std = 1;
                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                var cns = Constraint.LessThanOrEqual(exp, std);
                cns.LowerBound = 0;
                model.AddConstraint(cns, kisitAdi);
            }
            else
            {
                foreach (var tarih in tarihler.Take(gunSayisi - (int)pespeseNobetSayisiAltLimit))
                {
                    var kisitAdi = $"eczanelere_haftaici_pespese_nobet_yazilmasin, {eczaneNobetGrup}";

                    var kararIndex = eczaneNobetTarihAralik
                        .Where(e => (e.Tarih >= tarih.Tarih && e.Tarih <= tarih.Tarih.AddDays(pespeseNobetSayisiAltLimit))
                                  && haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                    var std = 1;
                    var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                    var cns = Constraint.LessThanOrEqual(exp, std);
                    cns.LowerBound = 0;
                    model.AddConstraint(cns, kisitAdi);
                }
            }
        }
    }     
 */
/*
         /// <summary>
        /// Her eczaneye istenen tarih aralığında en az 1 nöbet yazılır.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="nobetUstGrupKisitDetay"></param>
        /// <param name="tarihler"></param>
        /// <param name="gruptakiNobetciSayisi"></param>
        /// <param name="eczaneNobetGrup"></param>
        /// <param name="eczaneNobetTarihAralikEczaneBazli"></param>
        /// <param name="_x"></param>
        public virtual void TarihAraligindaEnAz1NobetYaz(Model model,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<TakvimNobetGrup> tarihler,
            int gruptakiNobetciSayisi,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikEczaneBazli,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi && gruptakiNobetciSayisi <= tarihler.Count)
            {
                var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                var kararIndex = eczaneNobetTarihAralikEczaneBazli
                                        .Where(e => tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)).ToList();

                var enAzNobetSayisi = 1;

                if (nobetUstGrupKisitDetay.SagTarafDegeri > 0)
                    enAzNobetSayisi = (int)nobetUstGrupKisitDetay.SagTarafDegeri;

                var std = enAzNobetSayisi;
                var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                var cns = Constraint.GreaterThanOrEqual(exp, std);
                //var isTriviallyFeasible = cns.IsTriviallyFeasible();

                model.AddConstraint(cns, kisitAdi);
            }
        } 
 */

/*
 *public virtual void AyIcindeSadece1KezAyniGunNobetTutulsun(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            List<AyniGunTutulanNobetDetay> ikiliEczaneler,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<TakvimNobetGrup> tarihler,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var kisitAdi = "ayni gun"; //$"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                var tarihAraligi = tarihler.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                foreach (var ikiliEczane in ikiliEczaneler)
                //eczaneNobetTarihAralikIkiliEczaneler)
                {
                    var kararIndexOnce = eczaneNobetTarihAralik
                             .Where(e => (e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId1 || e.EczaneNobetGrupId == ikiliEczane.EczaneNobetGrupId2)).ToList();

                    foreach (var tarih in tarihAraligi.Take(tarihler.Count - 1))
                    {
                        var tarihler2 = tarihAraligi.Where(w => w.Tarih > tarih.Tarih).ToList();

                        foreach (var tarih2 in tarihler2)
                        {
                            var kararIndex = kararIndexOnce
                                    .Where(e => (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId)).ToList();

                            #region kontrol
                            //var ss = new string[] { "AYDOĞAN", "SARE" };
                            //var sayi = kararIndex.Where(w => w.EczaneAdi == "AYDOĞAN" || w.EczaneAdi == "NİSA").Count();

                            //if (sayi == 4)
                            //{
                            //} 
                            #endregion

                            var std = 3;
                            var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                            var cns = Constraint.LessThanOrEqual(exp, std);
                            cns.LowerBound = 0;

                            model.AddConstraint(cns, kisitAdi);
                        }
                    }
                }
            }
        }
 */

/*
 public virtual void BirEczaneyeAyniGunSadece1GorevYaz(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<TakvimNobetGrup> tarihler,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                var tarihAraligi = tarihler.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {
                    var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                    foreach (var tarih in tarihAraligi)
                    {
                        var kararIndex = eczaneNobetTarihAralik
                            .Where(e => e.TakvimId == tarih.TakvimId
                                     && e.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                        var std = 1;
                        var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                        var cns = Constraint.LessThanOrEqual(exp, std);
                        cns.LowerBound = 0;

                        model.AddConstraint(cns, kisitAdi);
                    }
                }
            }
        }
 */
/*
         public virtual void IstenenEczanelerinNobetGunleriniKisitla(Model model,
            List<int> nobetYazilmayacakGunKuralIdList,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            VariableCollection<EczaneNobetTarihAralik> _x)
        {
            if (!nobetUstGrupKisitDetay.PasifMi)
            {
                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {
                    var kisitAdi = $"{nobetUstGrupKisitDetay.KisitAdi}, {eczaneNobetGrup.EczaneAdi}";

                    var kararIndex = eczaneNobetTarihAralikTumu
                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                 && nobetYazilmayacakGunKuralIdList.Contains(e.GunDegerId)).ToList();

                    var std = 0;
                    var exp = Expression.Sum(kararIndex.Select(i => _x[i]));
                    var cns = Constraint.Equals(exp, std);
                    model.AddConstraint(cns, kisitAdi);
                }
            }
        }*/

/*
               //1-2
               foreach (var altGrup in altGruplar.Where(w => w.NobetAltGrupId == 11))
               {
                   var sehirIci1dekiEczaneler = p.EczaneNobetGrupAltGrupDetaylar.Where(w => w.NobetAltGrupId == altGrup.NobetAltGrupId).ToList();
                   var digerAltGrup = altGruplar.Where(w => w.NobetAltGrupId == 12).FirstOrDefault();

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

               //1-3
               foreach (var altGrup in altGruplar.Where(w => w.NobetAltGrupId == 11))
               {
                   var sehirIci1dekiEczaneler = p.EczaneNobetGrupAltGrupDetaylar.Where(w => w.NobetAltGrupId == altGrup.NobetAltGrupId).ToList();
                   var digerAltGrup = altGruplar.Where(w => w.NobetAltGrupId == 13).FirstOrDefault();

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

               //2-3
               foreach (var altGrup in altGruplar.Where(w => w.NobetAltGrupId == 12))
               {
                   var sehirIci1dekiEczaneler = p.EczaneNobetGrupAltGrupDetaylar.Where(w => w.NobetAltGrupId == altGrup.NobetAltGrupId).ToList();
                   var digerAltGrup = altGruplar.Where(w => w.NobetAltGrupId == 13).FirstOrDefault();

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

               */
