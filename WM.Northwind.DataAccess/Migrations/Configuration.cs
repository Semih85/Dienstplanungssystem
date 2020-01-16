//using System.Collections.Generic;
//using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.SqlServer;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
    using WM.Northwind.Entities.Concrete.Authorization;
    using WM.Northwind.Entities.Concrete.EczaneNobet;

    internal sealed class Configuration : DbMigrationsConfiguration<WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts.EczaneNobetContext>
    {
        public Configuration()
        {
            //veri tabanýnda deðiþikliðe izin vermek için istendiði zaman true olacak.
            AutomaticMigrationsEnabled = false;
            //alan silineceði zaman true olacak. silmede veri kaybýný önlemek için false 
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts.EczaneNobetContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //  örneðin yaz dönemi cts. ile ilgili bir þey yapýlmak isteniyorsa bunun için ayrý bir görev tipine gerek olmayabilir. bayramlar gibi özel gün olarak eklenip devam edilebilir.
            bool guncelle = false;

            if (guncelle)
            {
                //var ayniGunTutulanNobetler = new List<AyniGunTutulanNobet>()
                //                {
                //                    new AyniGunTutulanNobet(){ NobetUstGrupId = 1 },
                //                };

                //context.AyniGunTutulanNobetler.AddOrUpdate(s => new { s.NobetUstGrupId }, ayniGunTutulanNobetler.ToArray());
                //context.SaveChanges();
                VeriEkleGuncelle(context);
            }
        }

        private static void VeriEkleGuncelle(Concrete.EntityFramework.Contexts.EczaneNobetContext context)
        {
            //var ustGruplar = context.NobetUstGruplar.ToList();

            //foreach (var nobetUstGrup in ustGruplar)
            //{
            //    var tarih = nobetUstGrup.BaslangicTarihi;

            //    var gruplar = context.NobetGrupGorevTipler.Where(w => w.NobetGrup.NobetUstGrupId == nobetUstGrup.Id).ToList();

            //    foreach (var item in gruplar)
            //    {
            //        if (item.Id == 31 || item.Id == 28)
            //        {
            //            tarih = new DateTime(2018, 4, 1);
            //        }
            //        else
            //        {
            //            tarih = nobetUstGrup.BaslangicTarihi;
            //        }

            //        item.BaslamaTarihi = tarih;

            //        context.SaveChanges();
            //    }
            //}

            //NobetGrupGorevTipTakvimOzelGunEkle(context, 71);
            //NobetGrupGorevTipTakvimOzelGunEkle(context, 67);
            //NobetGrupGorevTipTakvimOzelGunEkle(context, 60);
            ;
            #region örnek
            //var kisitKategoriler = new List<KisitKategori>()
            //                {
            //                    new KisitKategori(){ Adi="ktg 1 ", Aciklama = "ilk ktg" },
            //                };

            //context.KisitKategoriler.AddOrUpdate(s => new { s.Adi }, kisitKategoriler.ToArray());
            //context.SaveChanges();

            ////örnek
            //foreach (var item in context.EczaneGrupTanimlar)
            //{
            //    item.AyniGunNobetTutabilecekEczaneSayisi = 1;
            //}
            //context.SaveChanges();

            ////örnek
            //foreach (var item in context.NobetGrupKurallar)
            //{
            //    if (item.NobetGrupId == 27 || item.NobetGrupId == 29)
            //    {
            //        item.NobetGrupGorevTipId = item.NobetGrupId - 2;
            //    }
            //    else
            //    {
            //        item.NobetGrupGorevTipId = item.NobetGrupId;
            //    }
            //}
            //context.SaveChanges();

            #endregion

            #region örnek
            //var eczaneGrupTanimTipler = new List<EczaneGrupTanimTip>()
            //                {
            //                    new EczaneGrupTanimTip(){ Adi="Coðrafi yakýnlýk" },
            //                    new EczaneGrupTanimTip(){ Adi="Eþ Durumu" }
            //                };

            //context.EczaneGrupTanimTipler.AddOrUpdate(s => new { s.Adi }, eczaneGrupTanimTipler.ToArray());
            //context.SaveChanges();

            //örnek
            //foreach (var item in context.EczaneGrupTanimlar)
            //{
            //	item.EczaneGrupTanimTipId = 1;
            //}
            //context.SaveChanges();

            //foreach (var item in context.EczaneNobetIstekler)
            //{
            //    item.EczaneNobetGrupId = item.EczaneId;
            //}
            //context.SaveChanges();

            //foreach (var item in context.EczaneNobetMazeretler)
            //{
            //    item.EczaneNobetGrupId = item.EczaneId;
            //}
            //context.SaveChanges(); 
            #endregion

            //NobetGrupGorevTipTakvimOzelGunEkle(context, 65);

            var baslamaTarihi = new DateTime(2020, 2, 1);
            var odaId = 6;
            var nobetUstGrupId = 15;// context.NobetUstGruplar.Max(x => x.Id) + 1;
            var nobetGrupGorevTipId = context.NobetGrupGorevTipler.Max(x => x.Id) + 1;
            var varsayilanNobetciSayisi = 1;

            var gerekliBilgilerAntakya = new GerekliBilgiler(context, odaId, nobetUstGrupId, nobetGrupGorevTipId, baslamaTarihi, varsayilanNobetciSayisi)
            {
                //var baslamaTarihi = new DateTime(2019, 3, 5);
                //var odaId = 6;
                //var nobetUstGrupId = 7;
                //var nobetGrupGorevTipId = 30;

                BaslamaTarihi = baslamaTarihi, // new DateTime(2019, 3, 5),

                EczaneOdalalar = context.EczaneOdalar.Where(w => w.Id == odaId).ToList(),

                Eczaneler = new List<Eczane>()
                            {
                                #region Antakya
new Eczane{ Adi="AKÖZCAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKSARAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKSOY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ALKAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ANTAKYA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ATEÞ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BÝLDÝREN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CANBOLAT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÇIRAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ESENOCAK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ESENTEPE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ESRA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="FATÝH", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="FATMA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="GENCO", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="GONCA (DEFNE)", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HAKAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="IHLAMUR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="IÞIK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÝREZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KATÝPOÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KOROÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KUDRET", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="METÝN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="OKAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÖZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="RENK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SAYIN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SEVÝM", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SÝNAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÞENOL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÞÝRÝNCE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="TUTAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="TÜRKER", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="UÐURGÜL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YASEMÝN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YAÞAM", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YILMAZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YUSUF", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ZORKUN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKDOÐAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ALEV", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ANDI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BÜYÜK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CUMHURÝYET", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DARAOÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DERÝNKÖK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DERMAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DOÐA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="GÖNENÇ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="GÜNAL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HALK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HASTANE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HATAY GÖKÇE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÝPEKÇÝOÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KARAGÖZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KEMALPAÞA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KURTULUÞ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="LEVEND", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MELTEM", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MUHSÝN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MUNA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MÜGE  ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MÜZEYYEN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="NURSEL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÖMEROÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÖZKAYA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="REÞAT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SELÝM", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SELÝN   ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SEVÝNÇ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SÜNER", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÞANLI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÞÝFA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÞÝRÝNTEPE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="TAHSÝN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="TÜLAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÜMÝT   ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YENÝ MOZAÝK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKADEMÝ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKYOL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ASLAN ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ASLANLAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AÞKAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AYDIN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BAÞARAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BÝNNUR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CADDE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CEM", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CEYHUN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CÝLLÝ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÇARE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÇARÞI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÇINAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DAMLA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DEFNE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="EDA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ERGÖNÜL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ESKÝOCAK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="GÜLBAÞ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HATAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HEKÝMOÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÝMGE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="K.DALYAN MERKEZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KIRKICI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KURÞUN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KUZEYTEPE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="NAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="OKAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="OKYANUS", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="OVALI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÖZTOPRAK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SAÐLIKPINARI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SARAYKENT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SÝGORTA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SÜRMELÝ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="UYGUN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YÜKSEL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ADA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKBAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKDENÝZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ALTUNAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ANIL UÇUCU  ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ATIF", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BAYRAMOÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BETÜL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BURCU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BÜLENT ÇIRAY   ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÇEKMECE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DEMET", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DURU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DURU FAYSAL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ECE GÖÇMEN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HALK HAR.", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HATAY HAYAT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HAYAT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HAYAT SÖNMEZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÝYÝLÝK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KAMÝL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KESKÝN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KUMSAL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KUNT ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MANSUROÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MELÝS CAN   ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MENEL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MENGÜLLÜOÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="NEVRA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ORGANÝK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÖZGE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÖZLEM  ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ÖZTÜRK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="RÜYA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="RÜYA HAR.", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SAÐLIK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SAÐLIK HAR.", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SENDÝOÐLU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SEVÝNGÜL GÖKÇE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ZEYNEP", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15}
                                #endregion
                            },

                NobetUstGruplar = new List<NobetUstGrup>() {
                                //new NobetUstGrup(){ Adi = "Antakya", Aciklama = "Antakya Merkez", EczaneOdaId = odaId, BaslangicTarihi=baslamaTarihi, TimeLimit = 60, Enlem = 36.1968031, Boylam = 36.1612344 },
                            },

                NobetGruplar = new List<NobetGrup>() {
                                new NobetGrup(){ Adi = "Antakya-1", BaslamaTarihi = baslamaTarihi, NobetUstGrupId = nobetUstGrupId },
                                //new NobetGrup(){ Adi = "Antakya-2", BaslamaTarihi = baslamaTarihi, NobetUstGrupId = nobetUstGrupId },
                                //new NobetGrup(){ Adi = "Antakya-3", BaslamaTarihi = baslamaTarihi, NobetUstGrupId = nobetUstGrupId },
                                //new NobetGrup(){ Adi = "Antakya-4", BaslamaTarihi = baslamaTarihi, NobetUstGrupId = nobetUstGrupId },
                            },

                Kullanicilar = new List<User>()
                            {//ordu2019
                                //new User(){ Email="odaDiyarbakir@nobetyaz.com", FirstName="Oda Diyarbakýr", LastName="Oda Diyarbakýr", Password="odaDiyarbakir8", UserName="odaDiyarbakir", BaslamaTarihi = baslamaTarihi},
                                //new User(){ Email="ustGrupDiyarbakir@nobetyaz.com", FirstName="Üst Grup", LastName="Üst grp", Password="ustGrup8", UserName="ustGrupDiyarbakir", BaslamaTarihi = baslamaTarihi},
                                new User(){ Email="utkuergonul@yahoo.com", FirstName="Utku", LastName="Gönül", Password="antakya2020", UserName="Utku", BaslamaTarihi = baslamaTarihi}
                            },

                NobetGrupKurallar = new List<NobetGrupKural>()
                {
                    //new NobetGrupKural() { NobetKuralId = 1, BaslangicTarihi = baslamaTarihi, Deger = 5 },//Ardýþýk Boþ Gün Sayýsý
                    ////new NobetGrupKural() { NobetGrupGorevTipId = 28, NobetKuralId = 2, BaslangicTarihi = baslamaTarihi, Deger = 5 },
                    //new NobetGrupKural() { NobetKuralId = 3, BaslangicTarihi = baslamaTarihi, Deger = 1 }//Varsayýlan günlük nöbetçi sayýsý
                },

                NobetKurallar = context.NobetKurallar.Where(w => new int[]
                {
                    1, //Ardýþýk Boþ Gün Sayýsý
                    3  //Varsayýlan günlük nöbetçi sayýsý
                }.Contains(w.Id)).ToList(),

                NobetUstGrupGunGruplar = new List<NobetUstGrupGunGrup>()
                            {
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 1, AmacFonksiyonuKatsayisi = 1000 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 2, AmacFonksiyonuKatsayisi = 8000 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 3, AmacFonksiyonuKatsayisi = 900 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 4, AmacFonksiyonuKatsayisi = 100 },
                                //new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 5, AmacFonksiyonuKatsayisi = 7000 }
                            }
            };

            //NobetGrupGunKuralEkle(context, baslamaTarihi, nobetUstGrupId, new List<int> { 53, 54 }, varsayilanNobetciSayisi, 42);
            //NobetGrupGorevTipTakvimOzelGunEkle(context, 71);
            //NobetGrupGorevTipTakvimOzelGunEkle(context, 54);

            UstGrupPaketiEkle(gerekliBilgilerAntakya);
            //NobetGrupGunKuralEkle(context, baslamaTarihi, nobetUstGrupId, new List<int> { 71 }, varsayilanNobetciSayisi, 70);
            //NobetGrupGorevTipTakvimOzelGunEkle(context, 71);
            //TalepEkle(context, 28, 2);

            //UstGrupPaketiEkleKompakt(gerekliBilgilerKirikhan);
        }

        private static void VeriEkleGuncelleMaster(Concrete.EntityFramework.Contexts.EczaneNobetContext context)
        {
            #region users
            var vUser = new List<User>()
                            {
            new User(){ Email="ozdamar85@gmail.com", FirstName="Semih", LastName="ÖZDAMAR", Password="123456", UserName="semih"},
            new User(){ Email="atesates2012@gmail.com", FirstName="Ateþ", LastName="Ateþ", Password="123456", UserName="ates"},
            new User(){ Email="huseyinecz@gmail.com", FirstName="Hüseyin", LastName="Eczane", Password="Alanya", UserName="huseyin"},

            new User(){ Email="odaAntalya@nobetyaz.com", FirstName="oda", LastName="Oda", Password="oda123", UserName="oda"},
            new User(){ Email="ustGrupAlanya@nobetyaz.com", FirstName="ustgrup", LastName="Oda", Password="ustgrup", UserName="ustGrupAlanya"},
            new User(){ Email="eczane@nobetyaz.com", FirstName="eczane", LastName="Oda", Password="eczane", UserName="eczane"},
            new User(){ Email="misafir@nobetyaz.com", FirstName="misafir", LastName="Oda", Password="misafir", UserName="misafir"},

            new User(){ Email="ustGrupAntalyaMerkez@nobetyaz.com", FirstName="ustgrup", LastName="Oda", Password="ustgrup", UserName="ustGrupAntalyaMerkez"}
                            };

            context.Users.AddOrUpdate(s => new { s.Email }, vUser.ToArray());
            context.SaveChanges();
            #endregion

            #region roles
            var vRole = new List<Role>()
                            {
                                new Role(){ Name="Admin" },
                                new Role(){ Name="Oda" },
                                new Role(){ Name="Üst Grup" },
                                new Role(){ Name="Eczane" },
                                new Role(){ Name="Misafir" }
                            };

            context.Roles.AddOrUpdate(s => new { s.Name }, vRole.ToArray());
            //vRole.ForEach(d => context.Roles.Add(d));
            context.SaveChanges();
            #endregion

            #region user roles
            var vuserRole = new List<UserRole>()
                            {
                                new UserRole(){ RoleId=1, UserId=1 },
                                new UserRole(){ RoleId=1, UserId=2 },
                                new UserRole(){ RoleId=3, UserId=3 },

                                new UserRole(){ RoleId=2, UserId=4 },
                                new UserRole(){ RoleId=3, UserId=5 },
                                new UserRole(){ RoleId=4, UserId=6 },
                                new UserRole(){ RoleId=5, UserId=7 },
                                new UserRole(){ RoleId=3, UserId=8 }
                            };

            context.UserRoles.AddOrUpdate(s => new { s.RoleId, s.UserId }, vuserRole.ToArray());
            context.SaveChanges();
            #endregion

            #region menüler
            var vMenu = new List<Menu>()
            {
			//Menü Single Items 1,2
			new Menu(){ LinkText="Eczaneler", ActionName="Index", ControllerName="Eczane", AreaName="EczaneNobet", SpanCssClass="fa fa-medkit fa-lg" },
            new Menu(){ LinkText="Eczane Mazeret", ActionName="Index", ControllerName="EczaneNobetMazeret", AreaName="EczaneNobet", SpanCssClass="fa fa-bug fa-lg" },

			//Menü Dropdown Titles 3,4,5,6,7
			new Menu(){ LinkText="Nöbet Gruplar", SpanCssClass="fa fa-bars"},
            new Menu(){ LinkText="Eczane Gruplar", SpanCssClass="fa fa-users"},
            new Menu(){ LinkText="Sonuçlar", SpanCssClass="fa fa-cubes" },
            new Menu(){ LinkText="Tanýmlar", SpanCssClass="fa fa-database" },
            new Menu(){ LinkText="Yetki", SpanCssClass="fa fa-shield" },
            new Menu(){ LinkText="Nöbet Kural", SpanCssClass="fa fa-cogs" }
            };

            context.Menuler.AddOrUpdate(s => new { s.LinkText }, vMenu.ToArray());
            //vMenu.ForEach(d => context.Menuler.Add(d));
            context.SaveChanges();
            #endregion

            #region menü roller
            var menuRoles = new List<MenuRole>()
                            {
								//admin 
								new MenuRole(){ MenuId=1, RoleId=1 },
                                new MenuRole(){ MenuId=2, RoleId=1 },
                                new MenuRole(){ MenuId=3, RoleId=1 },
                                new MenuRole(){ MenuId=4, RoleId=1 },
                                new MenuRole(){ MenuId=5, RoleId=1 },
                                new MenuRole(){ MenuId=6, RoleId=1 },
                                new MenuRole(){ MenuId=7, RoleId=1 },
                                new MenuRole(){ MenuId=8, RoleId=1 },
								
								//oda 
								new MenuRole(){ MenuId=1, RoleId=2 },
                                new MenuRole(){ MenuId=2, RoleId=2 },
                                new MenuRole(){ MenuId=3, RoleId=2 },
                                new MenuRole(){ MenuId=4, RoleId=2 },
                                new MenuRole(){ MenuId=5, RoleId=2 },
                                new MenuRole(){ MenuId=7, RoleId=2 },
                                new MenuRole(){ MenuId=8, RoleId=2 },
								
								//nöbet üst grup 
								new MenuRole(){ MenuId=1, RoleId=3 },
                                new MenuRole(){ MenuId=2, RoleId=3 },
                                new MenuRole(){ MenuId=3, RoleId=3 },
                                new MenuRole(){ MenuId=4, RoleId=3 },
                                new MenuRole(){ MenuId=5, RoleId=3 },
                                new MenuRole(){ MenuId=7, RoleId=3 },
                                new MenuRole(){ MenuId=8, RoleId=3 },

								//eczacý
								new MenuRole(){ MenuId=1, RoleId=4 },
                                new MenuRole(){ MenuId=2, RoleId=4 },
                                new MenuRole(){ MenuId=5, RoleId=4 },

								//misafir
								new MenuRole(){ MenuId=1, RoleId=5 },
                                new MenuRole(){ MenuId=2, RoleId=5 },
                                new MenuRole(){ MenuId=3, RoleId=5 },
                                new MenuRole(){ MenuId=4, RoleId=5 },
                                new MenuRole(){ MenuId=5, RoleId=5 }
                            };

            context.MenuRoles.AddOrUpdate(s => new { s.MenuId, s.RoleId }, menuRoles.ToArray());
            //vMenuRole.ForEach(d => context.MenuRoles.Add(d));
            context.SaveChanges();

            #endregion

            #region menü altlar
            var menuAltlar = new List<MenuAlt>()
            {
				//Nöbet Gruplar 1,2,3
				new MenuAlt(){ LinkText="Nöbet Üst Grup", ActionName="Index", ControllerName="NobetUstGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3 },
                new MenuAlt(){ LinkText="Nöbet Grup", ActionName="Index", ControllerName="NobetGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3 },
                new MenuAlt(){ LinkText="Eczane Nöbet Grup", ActionName="Index", ControllerName="EczaneNobetGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3 },

				//Eczane Gruplar 4,5
				new MenuAlt(){ LinkText="Eczane Grup Taným", ActionName="Index", ControllerName="EczaneGrupTanim", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=4 },
                new MenuAlt(){ LinkText="Eczane Grup", ActionName="Index", ControllerName="EczaneGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=4},

				//Sonuçlar 6,7
				new MenuAlt(){ LinkText="Pivot Tablo", ActionName="Index", ControllerName="EczaneNobetSonuc", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=5 },
                new MenuAlt(){ LinkText="Görsel Sonuçlar", ActionName="GorselSonuclar", ControllerName="EczaneNobetSonuc", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=5},

				//tanýmlar 8,9,10
				new MenuAlt(){ LinkText="Eczanene Oda", ActionName="Index", ControllerName="EczaneOda", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6 },
                new MenuAlt(){ LinkText="Mazeret Tür", ActionName="Index", ControllerName="MazeretTur", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6 },
                new MenuAlt(){ LinkText="Mazeret", ActionName="Index", ControllerName="Mazeret", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},

				//yetkiler 11,12,13,14,15,16,17,18
				new MenuAlt(){ LinkText="Kullanýcý", ActionName="Register", ControllerName="Account", AreaName="", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Menü", ActionName="Index", ControllerName="Menu", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Menü Alt", ActionName="Index", ControllerName="MenuAlt", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Menü Rol", ActionName="Index", ControllerName="MenuRole", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Menü Alt Rol", ActionName="Index", ControllerName="MenuAltRole", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Kullanýcý Oda", ActionName="Index", ControllerName="UserOda", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Kullanýcý Nöbet Üst Grup", ActionName="Index", ControllerName="UserNobetUstGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Kullanýcý Eczane", ActionName="Index", ControllerName="UserEczane", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},

				//tanýmlar 19,20,21,22,23,24,25,26
				new MenuAlt(){ LinkText="Ýstek", ActionName="Index", ControllerName="Istek", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Ýstek Tür", ActionName="Index", ControllerName="IstekTur", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Nöbet Görev Tip", ActionName="Index", ControllerName="NobetGorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Þehir", ActionName="Index", ControllerName="Sehir", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Ýlçe", ActionName="Index", ControllerName="Ilce", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Rol", ActionName="Index", ControllerName="Role", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},

                new MenuAlt(){ LinkText="Eczane Nöbet Mazeret", ActionName="Index", ControllerName="EczaneNobetMazeret", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=2},
                new MenuAlt(){ LinkText="Eczane Nöbet Ýstek", ActionName="Index", ControllerName="EczaneNobetIstek", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=2},

				//yetkiler 27
				new MenuAlt(){ LinkText="Kullanýcý Rol", ActionName="Index", ControllerName="UserRole", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},

				//nöbet kural 28,29
				new MenuAlt(){ LinkText="Nöbet Kural", ActionName="Index", ControllerName="NobetKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},
                new MenuAlt(){ LinkText="Nöbet Grup Kural", ActionName="Index", ControllerName="NobetGrupKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},

				//tanýmlar 30,31,32
				new MenuAlt(){ LinkText="Görev Tip", ActionName="Index", ControllerName="GorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Eczane Görev Tip", ActionName="Index", ControllerName="EczaneGorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Nöbet Grup Görev Tip", ActionName="Index", ControllerName="NobetGrupGorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},

				//Nöbet Gruplar 33
				new MenuAlt(){ LinkText="Nöbet Alt Grup", ActionName="Index", ControllerName="NobetAltGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3},

				//nöbet kural 34
				new MenuAlt(){ LinkText="Nöbet Grup Talepler", ActionName="Index", ControllerName="NobetGrupTalep", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},

				//nöbet kural 35,36
				new MenuAlt(){ LinkText="Nöbet Gün Kural", ActionName="Index", ControllerName="NobetGunKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},
                new MenuAlt(){ LinkText="Nöbet Grup Gün Kural", ActionName="Index", ControllerName="NobetGrupGunKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},
            };

            context.MenuAltlar.AddOrUpdate(s => new { s.LinkText }, menuAltlar.ToArray());
            //vMenuAlt.ForEach(d => context.MenuAltlar.Add(d));
            context.SaveChanges();

            #endregion

            #region menü alt roller

            var menuAltRoles = new List<MenuAltRole>()
                            {   
								#region admin
								new MenuAltRole(){ MenuAltId=1, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=2, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=4, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=5, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=8, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=9, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=10, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=11, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=12, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=13, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=14, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=15, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=16, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=17, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=18, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=19, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=20, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=21, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=22, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=23, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=24, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=27, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=28, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=29, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=30, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=31, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=32, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=33, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=34, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=35, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=36, RoleId=1 }, 
								#endregion
								
								#region oda
								new MenuAltRole(){ MenuAltId=1, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=2, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=4, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=5, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=11, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=18, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=28, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=29, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=34, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=35, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=36, RoleId=2 }, 
								#endregion
								
								#region nöbet üst grup
								//new MenuAltRole(){ MenuAltId=1, RoleId=3 },
								new MenuAltRole(){ MenuAltId=2, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=4, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=5, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=11, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=18, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=29, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=34, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=36, RoleId=3 }, 
								#endregion
								
								#region eczacý
								//new MenuAltRole(){ MenuAltId=1, RoleId=4 },
								new MenuAltRole(){ MenuAltId=2, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=4 }, 
								#endregion
								
								#region misafir
								//new MenuAltRole(){ MenuAltId=1, RoleId=5 },
								new MenuAltRole(){ MenuAltId=2, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=4, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=5, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=11, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=5 } 
								#endregion
							};

            context.MenuAltRoles.AddOrUpdate(s => new { s.MenuAltId, s.RoleId }, menuAltRoles.ToArray());
            //vMenuAltRole.ForEach(d => context.MenuAltRoles.Add(d));
            context.SaveChanges();
            #endregion

            #region nobet gorev tipler
            var vNobetGorevTip = new List<NobetGorevTip>()
            {
                new NobetGorevTip(){ Adi="18:00 - 08.30" },
                new NobetGorevTip(){ Adi="18:00 - 00:00" },
                new NobetGorevTip(){ Adi="08:30 - 18:00" },
                new NobetGorevTip(){ Adi="08:30 - 00:00" }
            };

            context.NobetGorevTipler.AddOrUpdate(s => new { s.Adi }, vNobetGorevTip.ToArray());
            //vNobetGorevTip.ForEach(d => context.NobetGorevTipler.Add(d));
            context.SaveChanges();

            #endregion

            #region nöbet gün kurallar
            var nobetGunKurallar = new List<NobetGunKural>()
                            {
                                new NobetGunKural(){ Adi="Pazar", Aciklama="Pazar günü eþit daðýlýma dahil olsun"},
                                new NobetGunKural(){ Adi="Pazartesi", Aciklama="Pazartesi günü eþit daðýlýma dahil olsun"},
                                new NobetGunKural(){ Adi="Salý", Aciklama="Salý günü eþit daðýlýma dahil olsun"},
                                new NobetGunKural(){ Adi="Çarþamba", Aciklama="Çarþamba günü eþit daðýlýma dahil olsun"},
                                new NobetGunKural(){ Adi="Perþembe", Aciklama="Perþembe günü eþit daðýlýma dahil olsun"},
                                new NobetGunKural(){ Adi="Cuma", Aciklama="Cuma günü eþit daðýlýma dahil olsun"},
                                new NobetGunKural(){ Adi="Cumartesi", Aciklama="Cumartesi günü eþit daðýlýma dahil olsun"},
                                new NobetGunKural(){ Adi="Dini Bayram", Aciklama="Dini Bayram eþit daðýlýma dahil olsun"},
                                new NobetGunKural(){ Adi="Milli Bayram", Aciklama="Milli Bayram eþit daðýlýma dahil olsun"},
                            };

            context.NobetGunKurallar.AddOrUpdate(s => new { s.Adi }, nobetGunKurallar.ToArray());
            //nobetGunKurallar.ForEach(d => context.NobetGunKurallar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet kurallar
            var nobetKurallar = new List<NobetKural>()
                            {
                                new NobetKural(){ Adi="Ardýþýk Nöbet Sayýsý", Aciklama="Peþpeþe nöbet yazýlmayacak ardýþýk gün sayýsý"},
                                new NobetKural(){ Adi="Birlikte Nöbet Sayýsý", Aciklama="Ayný güne denk gelen nöbet sayýsý"}, // bu sayý için 4 uygun
								new NobetKural(){ Adi="Varsayýlan Nöbet Sayýsý", Aciklama="Grubun varsayýlan nöbet sayýsý"} // bu sayý için 1 uygun
							};

            context.NobetKurallar.AddOrUpdate(s => new { s.Adi }, nobetKurallar.ToArray());
            //nobetKurallar.ForEach(d => context.NobetKurallar.Add(d));
            context.SaveChanges();
            #endregion


            #region takvimler
            List<Takvim> takvimler = new List<Takvim>();

            for (int y = 2018; y < 2023; y++)
            {
                for (int m = 1; m < 13; m++)
                {
                    var aydakiGunler = DateTime.DaysInMonth(y, m);

                    for (int d = 1; d <= aydakiGunler; d++)
                    {
                        takvimler.Add(new Takvim()
                        {
                            Tarih = new DateTime(y, m, d)
                        });
                    }
                }
            }
            context.Takvimler.AddOrUpdate(s => new { s.Tarih }, takvimler.ToArray());
            //takvimler.ForEach(d => context.Takvimler.Add(d));
            context.SaveChanges();

            #endregion

            #region nöbet görev tipler
            var nobbetGorevTipler = new List<NobetGorevTip>()
            {
                                new NobetGorevTip(){Adi = "Tam Gün Nöbetçi"},
                                new NobetGorevTip(){Adi = "Gündüz Nöbetçi"}
            };
            context.NobetGorevTipler.AddOrUpdate(s => new { s.Adi }, nobbetGorevTipler.ToArray());
            //nobbetGorevTipler.ForEach(d => context.NobetGorevTipler.Add(d));
            context.SaveChanges();
            #endregion

            #region bayram türler
            var bayramTurler = new List<BayramTur>()
                            {
                                new BayramTur(){ Adi="Yýlbaþý" },
                                new BayramTur(){ Adi="23 Nisan" },
                                new BayramTur(){ Adi="1 Mayýs Emek ve Dayanýþma Günü" },
                                new BayramTur(){ Adi="Zafer Bayramý" },
                                new BayramTur(){ Adi="29 Ekim Cumhuriyet Bayramý" },
                                new BayramTur(){ Adi="Ramazan Bayramý" },
                                new BayramTur(){ Adi="Kurban Bayramý" }
                            };

            context.BayramTurler.AddOrUpdate(s => new { s.Adi }, bayramTurler.ToArray());
            context.SaveChanges();
            #endregion

            #region eczane odalar
            var eczaneOdalar = new List<EczaneOda>()
            {
                new EczaneOda(){ Adi="Antalya", Adres="Antalya Merkez", TelefonNo="2423110329", MailAdresi="antalyaeczaciodasi@gmail.com", WebSitesi ="https://www.antalyaeo.org.tr/tr"}
            };

            context.EczaneOdalar.AddOrUpdate(s => new { s.Adi }, eczaneOdalar.ToArray());
            //eczaneOdalar.ForEach(d => context.EczaneOdalar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet üst gruplar
            var nobetUstGruplar = new List<NobetUstGrup>() {
                new NobetUstGrup(){Adi = "Alanya",Aciklama = "Antalya ilçesi",EczaneOdaId = 1, BaslangicTarihi=new DateTime(2018,1,1)},
                new NobetUstGrup(){Adi = "Antalya Merkez",Aciklama = "Antalya merkez",EczaneOdaId = 1, BaslangicTarihi=new DateTime(2018,1,1)},
            };

            context.NobetUstGruplar.AddOrUpdate(s => new { s.Adi }, nobetUstGruplar.ToArray());
            context.SaveChanges();
            #endregion

            #region nöbet gruplar

            var nobetGruplar = new List<NobetGrup>();

            int ustGrup = 1;
            for (int g = 1; g <= 3; g++)
            {
                nobetGruplar.Add(new NobetGrup()
                {
                    Adi = "Alanya-" + g,
                    BaslamaTarihi = new DateTime(2018, 1, 1),
                    NobetUstGrupId = ustGrup
                });
            };

            ustGrup = 2;
            for (int g = 4; g <= 14; g++)
            {
                nobetGruplar.Add(new NobetGrup()
                {
                    Adi = "Antalya-" + (g - 3),
                    BaslamaTarihi = new DateTime(2018, 1, 1),
                    NobetUstGrupId = ustGrup
                });
            };

            context.NobetGruplar.AddOrUpdate(s => new { s.Adi }, nobetGruplar.ToArray());
            context.SaveChanges();
            #endregion            

            #region nöbet grup kurallar
            /*
            var nobetGrupKurallar = new List<NobetGrupKural>()
                    {
					#region Alanya
					new NobetGrupKural(){ NobetGrupId=1, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=2, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=3, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},

                    new NobetGrupKural(){ NobetGrupId=1, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=2, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=3, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},

                    new NobetGrupKural(){ NobetGrupId=1, NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=2, NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=3, NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1}, 
					#endregion

					#region Antalya Merkez
					new NobetGrupKural(){ NobetGrupId=4,  NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=5,  NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=6,  NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=7,  NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=8,  NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=9,  NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=10, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=11, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=12, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=13, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
                    new NobetGrupKural(){ NobetGrupId=14, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},

                    new NobetGrupKural(){ NobetGrupId=4, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=5, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=6, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=7, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=8, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=9, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=10, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=11, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=12, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=13, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=14, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},

                    new NobetGrupKural(){ NobetGrupId=4,  NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=5,  NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=6,  NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=7,  NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=8,  NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=9,  NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=10, NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=11, NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=12, NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=13, NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1},
                    new NobetGrupKural(){ NobetGrupId=14, NobetKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=1}
					#endregion
					};

            context.NobetGrupKurallar.AddOrUpdate(s => new { s.NobetGrupId, s.NobetKuralId, s.BaslangicTarihi }, nobetGrupKurallar.ToArray());
            //nobetGrupKurallar.ForEach(d => context.NobetGrupKurallar.Add(d));
            context.SaveChanges();
            */
            #endregion

            #region nöbet grup görev tipler
            var nobetGrupGorevTipler = new List<NobetGrupGorevTip>()
                            {
                    new NobetGrupGorevTip(){ NobetGrupId=1, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=2, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=3, NobetGorevTipId=1},

                    new NobetGrupGorevTip(){ NobetGrupId=4, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=5, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=6, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=7, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=8, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=9, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=10, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=11, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=12, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=13, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=14, NobetGorevTipId=1}
                            };

            context.NobetGrupGorevTipler.AddOrUpdate(s => new { s.NobetGrupId, s.NobetGorevTipId }, nobetGrupGorevTipler.ToArray());
            //nobetGrupGorevTipler.ForEach(d => context.NobetGrupGorevTipler.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet grup talepler
            //var nobetGrupTalepler = new List<NobetGrupTalep>()
            //                {
            //                    new NobetGrupTalep(){ TakvimId=1, NobetGrupGorevTipId=1, NobetciSayisi=2}
            //                };

            //context.NobetGrupTalepler.AddOrUpdate(s => new { s.TakvimId, s.NobetGrupGorevTipId, s.NobetciSayisi }, nobetGrupTalepler.ToArray());
            //context.SaveChanges();
            #endregion

            #region eczaneler

            var eczaneler = new List<Eczane>()
            {
				#region Alanya
				#region 1
				new Eczane { Adi = "ERENLEROBA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALAÝYE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SÝNAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜLAY", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AKÇALIOÐLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜLER", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NAZ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AY", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NUR", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "EYÜP", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ÞÝMÞEK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AYDOÐAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ÞEKER", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜNEYLÝOÐLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SARE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AYNUR", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "FARUK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ÝKSÝR", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "HÝDAYET", AcilisTarihi = new DateTime(2018,1,1) }, 
				#endregion
				
				#region 2
				new Eczane { Adi = "MARTI", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "DEFNE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TEZCAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "KAMBUROÐLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "YÜKSEK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NOYAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ARIKAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "KASAPOÐLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ÞAHÝN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "KOÇAK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÖKSU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ASLI", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AKSU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALANYA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SELCEN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SÝPAHÝOÐLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "HAYAT", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TOROS", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALPER", AcilisTarihi = new DateTime(2018,1,1) }, 
				#endregion
				
				#region 3
				new Eczane { Adi = "SAÐLIK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ECE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ÞÜKRAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TUNA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TURUNÇ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜLERYÜZ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BÜKE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BAÞAK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BERNA AKÇALIOÐLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALTUNBAÞ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TUÐBA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NÝSA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ÞÝRÝN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AYYÜCE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜNEÞ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SEVÝNDÝ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BÝLGE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "FÝLÝZ", AcilisTarihi = new DateTime(2018,1,1) },
				#endregion
				#endregion

				#region Antalya
		 
				#region 1
				new Eczane { Adi = "SEVDA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ YAPRAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜVENÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PORTAKAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇÝÐDEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PASTÖR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANSU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HÝLAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BUYRUKÇU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KOÇAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "IRMAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUNAHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NOKTA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DERÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZMEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "RODOPLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SOYAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FUNDA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KIYMET", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ NÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIZ ADEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAVDIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ MELTEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELÝF ÝNCE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAFRAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MELTEM MERT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PAMUK ÞEKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURDUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEFNE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NERGÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZNUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SÝZÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZÇAÐLAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ ELMALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TÜRKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖNDER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKYÜZÜ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEÇKÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KALE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LOKMAN NUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜNEY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ORUÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YÝÐÝT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OLGAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FEYZA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FÝDAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AÝLE=ÖZGÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZEYTÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAYSAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DENÝZÝN", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 2
				new Eczane { Adi = "BAHAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ITIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DÝLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜREL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ BURDUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLLÜK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERPÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DURUKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KORAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZLEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASPENDOS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERTEKÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YAKAMOZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYGÜN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKÇEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEDÝR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERKÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELMALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERKÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜNEÞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖMÜR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "VERÝMLÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÞARAMPOL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELVAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "METÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇOBAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ ÖZLEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BÝLGEHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÜMÝT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HONAMLI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOÐUÞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURCU DURAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MEKÝK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÞULE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYKUT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ILGIN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AZÝM", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 3
				new Eczane { Adi = "ETÝLER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEVÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝGÜN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TÜLÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BEYAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DÜZGÜN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUÐTEKÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BATUHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KÖK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KADIOÐLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KONUKSEVER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ IÞIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GAMZEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OKTAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EVRE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEÇKÝNER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALÝ BERK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERTAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜVEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CEMRE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EZDEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AVDANLIOÐLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GAMZE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MAVÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DÝKÝLÝTAÞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BALCI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAÐLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MEVLANA", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 4
				new Eczane { Adi = "CÝHAN DÝNÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MEYDAN ALPER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZEYNEP AKMAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ HAYAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MUZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOÐU GARAJI SAÐLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CUMHURÝYET", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ TUBA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UÐUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PINAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OLCAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOKUÇOÐLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YUNUS EMRE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKDENÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKÖZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKÇEN EFE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MURATPAÞA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MURAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KARAMANLI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UÐURCAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEMET", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAYBAÞI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MERVE=ANTALYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BEYZA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ULU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KÝRAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERKAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAPLAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MÜJGAN", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 5
				new Eczane { Adi = "KEREM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEZÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MANDALÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ANANAS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÝDÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZUHAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEREN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PORTAKAL ÇÝÇEÐÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TARÇIN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EVRÝM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GENÝÞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BULVAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BENGÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERHAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FULYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PERGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SÖNMEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TERMESSOS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LOTUS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZENCEFÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARZU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "IÞIN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DORUK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAMLILAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KÖYÜM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÞENDÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TURUNÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FLORA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAÐLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PARK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SENTEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ANADOLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIRIM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOÐA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KESKÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BALKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEÞÝLBAHÇE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MÜÐREN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAHÇELÝEVLER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KUMBUL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TALYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELÝFSU", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 6
				new Eczane { Adi = "VOLKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALARA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÝBRAHÝM ÖZER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YALÇINER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SIHHÝYE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURCU-M", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERRÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUÐBA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FARMA LARA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GENCA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANDEMÝR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZDENÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖKALÝPTUS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÝKÝZLER=SARIKUM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FERAH", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TURKUAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÞÝRÝNYALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUANA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OKYANUS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DORA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "VÝTAMÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "IÞILAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAHADIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEMA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÜNAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZKENT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZDEMÝR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKÇE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAHÇELÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "POSTACILAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ATLAS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ YILDIZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERTER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BARINAKLAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LARA SAÐLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURCU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÝNCESU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZGÜR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DAMLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UÐUR UYSAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SIHHAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERDEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖRNEKKÖY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKÞEHÝR", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 7
				new Eczane { Adi = "GÜLENYÜZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BÜYÜK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEVA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELÝF", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAÞGÖR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERÇÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÜLKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYDINLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERSOY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SELÝS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MASAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÝKÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KÜLTÜR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOLUNAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAZLI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CENGÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TEMÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ESRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MERKEZ GÖLHÝSAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARSLAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ESEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEMT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAÐLAYAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAÞAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MELDA", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 8
				new Eczane { Adi = "ARAPSUYU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ATILGAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANSEV", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SELMÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KIVANÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLNUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KONYAALTI BÝLGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AVKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PERA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜZEL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LÝMAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EZGÝM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZSOY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DURU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELÝT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SUN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KURTOÐLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TALYA SU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EGE BORA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLÞÝFA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DENÝZ YILDIZI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALTINKUM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PAPATYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELVÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DAÐ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SÝMGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ESMELER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKINCI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BIÇAKÇI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TÜTÜNCÜ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UYAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUÐÇE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SOYTÜRK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYSUN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LEYLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOÐANTAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUNCAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ ARAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ANTALYA MODERN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALPSOY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MATRUÞKA=KB", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 9
				new Eczane { Adi = "SÖÐÜT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇÖZEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TEZCAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERDOÐAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DUYGU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CERENSU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEHER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KEPEZ ANADOLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖZDE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYÞEGÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÝNANIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKTAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TATLICAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZCAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAYRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KÜÇÜKSU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÜNSAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLTALYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NADÝREM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇALIM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NEHÝR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TAÞELÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BARIÞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DÝLÞAD", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KEPEZALTI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EREÐLÝ ANIL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOPKAYA", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 10
				new Eczane { Adi = "SÝBEL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UTKU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FREZYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HAZAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEVGÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYLÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TURGAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEÞÝLIRMAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÝKRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEZER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NEZÝH", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIZ RÜYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EDA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MERKEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EMEK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKYILDIZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BÝLGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LEVENT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALKIÞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAKARYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BABACAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEMÝRGÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YURTPINAR UÐUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YÜCEL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NÝLAY", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 11
				new Eczane { Adi = "YENÝ VARSAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YÝÐÝTBAÞI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LALE PARK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENÝ EGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEMÝH", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FÝLÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ONUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOÐANTÜRK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CÝHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BEREN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOÐRU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KORKUTELÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAR ÇÝÇEÐÝ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "VARSAK GÜNEY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAYGILI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DUYGU TOPLUK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EYLÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAHÝL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZERRÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SÜTÇÜLER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERRA BALTA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ATA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GEBÝZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KALAYCI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEÞÝLYAYLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DÜNYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SARIÇOBAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SÜTÇÜLER SAÐLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASYA KEPEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BÝLLUR ÇELÝK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BANU YALÇIN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LOKMAN HEKÝM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HAYAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KARAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TALAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARPEK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CEVHER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUNCALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KURU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZYURT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KOLSUZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKTÝN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖMER YILDIZ", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion 

				#endregion
			};

            context.Eczaneler.AddOrUpdate(s => new { s.Adi, s.AcilisTarihi }, eczaneler.ToArray());
            context.SaveChanges();
            #endregion

            #region eczaneler - enlem, boylam ekle - osmaniye

            var osmaniyeEnlemBoylamlar = new List<Eczane>()
            {
                #region osmaniye enlem boylam
       
new Eczane { Id =953,Enlem=37.083649,Boylam= 36.265817,Adres ="KAZIM KARABEKÝR MAH.6502 SK.9102", AdresTarifiKisa = "Kadýn Doðum Hastanesi Yaný", TelefonNo = "3288141474"},
new Eczane { Id =932,Enlem=37.074959,Boylam= 36.250194,Adres ="RAHIME HATUN MAH. DR. SADIK AHMET CAD. NO:22/A MERKEZ/OSMANIYE", AdresTarifiKisa = "Cumhuriyet Meydaný Karþýsý", TelefonNo = "3288129400"},
new Eczane { Id =918,Enlem=37.0675690,Boylam=36.2490990,Adres ="YEDÝOCAK MAH.DR.DEVLET BAHÇELÝ BUL.NO 70/B MERKEZ/OSMANIYE", AdresTarifiKisa = "Dr. Devlet Bahçeli bulvarý 3. Etap 17 nolu aile hekimliði karþýsý", TelefonNo = "3288142051"},
new Eczane { Id =940,Enlem=37.080859,Boylam= 36.252531,Adres ="RIZAÝYE MAH.ÝSKENDER TÜRKMEN CAD.NO:106/A", AdresTarifiKisa = "Rýzaiye Mah.Ý.Türkmen Cad.Kadirli Yolu Üzeri BÝM Civarý", TelefonNo = "3284000086"},
new Eczane { Id =903,Enlem=37.063517,Boylam= 36.235955,Adres ="ADNAN MENDERES MAH.19538 SK.NO:6/B", AdresTarifiKisa = "AVM OTOPARK ÇIKIÞI SAÐLIK OCAÐI KARÞISI", TelefonNo = "3288257013"},
new Eczane { Id =951,Enlem=37.083783,Boylam= 36.265931,Adres ="KAZIM KARABEKÝR MH.6502 SK.NO:11/A", AdresTarifiKisa = "Kadýn Doðum Hastanesi Yaný", TelefonNo = "3288148333"},
new Eczane { Id =926,Enlem=37.076427,Boylam= 36.246032,Adres ="ESENEVLER MH.KAYALAR SK.NO:8/B", AdresTarifiKisa = "Esenevler mahallesi kurtuluþ saðlýk ocaðý ilerisi Büyük PTT arkasý Eski vali konaðý karþýsý", TelefonNo = "3288125678"},
new Eczane { Id =927,Enlem=37.076632,Boylam= 36.246031,Adres ="ESENEVLER MAHALLESÝ KAYALAR SOKAK NO:8/A", AdresTarifiKisa = "kurtuluþ saðlýk ocaðý yaný", TelefonNo = "3287862123"},
new Eczane { Id =900,Enlem=37.063006,Boylam= 36.236542,Adres ="ADNAN MENDERES MAH. 19537 SK.NO:5/E", AdresTarifiKisa = "Park 328 AVM Arkasý, Metin Tamer Sitesi 11.Blok No:34", TelefonNo = "3288260390"},
new Eczane { Id =912,Enlem=37.073745,Boylam= 36.235997,Adres ="YILDIRIM BEYAZIT MAH.14015 SK.NO:1/A", AdresTarifiKisa = "Musa Þahin bulvarý Gülbahçem sitesinden aþaðý 400 m ileride cuma pazarý giriþi sol köþe", TelefonNo = "3288133464"},
new Eczane { Id =907,Enlem=37.062219,Boylam= 36.238711,Adres ="MEHMET AKIF ERSOY MAH. ATATÜRK CAD. NO:475 B-C MERKEZ/OSMANIYE", AdresTarifiKisa = "ÖZEL YENÝ HAYAT HASTANESÝ YANI", TelefonNo = "3288250055"},
new Eczane { Id =938,Enlem=37.072128,Boylam= 36.252760,Adres ="HACI OSMANLI MH. DR. AHMET SADIK CD.DANIS APT. NO:86/A MERKEZ/OSMANIYE", AdresTarifiKisa = "Dr.Sadýk Ahmet Cd.Eski Vergi Dairesi Karþýsý", TelefonNo = "3288128405"},
new Eczane { Id =936,Enlem=37.073538,Boylam= 36.251795,Adres ="ATATÜRK CAD. IS BANKASI KARSISI NO:179 MERKEZ/OSMANIYE", AdresTarifiKisa = "ATATÜRK CAD. IS BANKASI KARSISI", TelefonNo = "3288141617"},
new Eczane { Id =954,Enlem=37.059333,Boylam= 36.246000,Adres ="MEHMET AKÝF ERSOY MAH.8507 (AZÝZ HÜDAÝ) SK.NO:58/A", AdresTarifiKisa = "", TelefonNo = "3284300020"},
new Eczane { Id =937,Enlem=37.072714,Boylam= 36.252197,Adres ="ISTIKLAL MAH. DR. SADIK AHMET CAD. NO:99 MERKEZ/OSMANIYE", AdresTarifiKisa = "Dr.Sadýk Ahmet Cd.ÇINARLI KAHVE KARÞISI", TelefonNo = "3288143420"},
new Eczane { Id =942,Enlem=37.082748,Boylam= 36.258156,Adres ="M. FEVZI ÇAKMAK MH. HOCA AHMET YESEVI CD. NO:67/c", AdresTarifiKisa = "Nahar Yolu Cumhuriyet Saðlýk Ocaðý Karþýsý", TelefonNo = "3288136722"},
new Eczane { Id =934,Enlem=37.073472,Boylam= 36.250960,Adres ="ALIBEYLI MH. ATATÜRK CD. NO:186 MERKEZ/OSMANIYE", AdresTarifiKisa = "Atatürk Cad.ÇARÞI Ýþ Bankasý Civarý Vestel Bayii Yaný", TelefonNo = "3288143848"},
new Eczane { Id =899,Enlem=37.059034,Boylam= 36.256193,Adres ="Karaboyunlu Mah.5550 Sk.No:31/A", AdresTarifiKisa = "KARABOYUNLU SAÐLIK OCAÐI KARÞISI", TelefonNo = "3288250065"},
new Eczane { Id =950,Enlem=37.083597,Boylam= 36.266085,Adres ="KAZIM KARABEKÝR MH.6502 SK.NO:11/B", AdresTarifiKisa = "Eski Devlet Hastanesi Karþýsý", TelefonNo = "3288142180"},
new Eczane { Id =919,Enlem=37.067596,Boylam= 36.249096,Adres ="YEDÝOCAK MAH.DR.DEVLET BAHÇELÝ BULVARI 3.ETAP DR.DEVLET BAHÇELÝ BULVARI 3.ETAP", AdresTarifiKisa = "DR.DEVLET BAHÇELÝ BULVARI 3.ETAP", TelefonNo = "3284020002"},
new Eczane { Id =945,Enlem=37.081341,Boylam= 36.262527,Adres ="MARAÞAL FEVZÝ ÇAKMAK MH.7520 SK NO:4/A", AdresTarifiKisa = "ÝBN-Ý SÝNA Hastanesi Arkasý Acil Giriþi", TelefonNo = "3288141199"},
new Eczane { Id =914,Enlem=37.071232,Boylam= 36.244700,Adres ="ALÝBEYLÝ MAHALLESÝ DR.DEVLET BAHÇELÝ BULVARI NO 27/B OSMANÝYE/MERKEZ", AdresTarifiKisa = "ÖZEL SEVGÝ HASTANESÝ KARÞISI", TelefonNo = "3288133453"},
new Eczane { Id =925,Enlem=37.076404,Boylam= 36.246201,Adres ="ESENEVLER MH. KAYALAR SK. NO:13/E MERKEZ/ OSMANIYE", AdresTarifiKisa = "Ýstasyon Cad. PTT Arkasý Kurtuluþ Saðlýk Ocaðý Karþýsý", TelefonNo = "3288145516"},
new Eczane { Id =922,Enlem=37.072431,Boylam= 36.245920,Adres ="ALIBEYLI MAH.KARAOGLANOGLU CAD. NO:17 MERKEZ/ OSMANIYE", AdresTarifiKisa = "Özel Park Hastanesi Karþýsý", TelefonNo = "3288147262"},
new Eczane { Id =931,Enlem=37.073967,Boylam= 36.249472,Adres ="ALIBEYLI MAH.PALALI SÜLEYMAN CD. ZAFER CAMII YANI NO:23 MERKEZ/OSMANIYE", AdresTarifiKisa = "Palalý Süleyman Cd.No:23 Zafer Camii yaný", TelefonNo = "3288126535"},
new Eczane { Id =910,Enlem=37.061515,Boylam= 36.239532,Adres ="MEHMET AKIF ERSOY MAH.8009 SKK.NO:8 MERKEZ/OSMANIYE", AdresTarifiKisa = "Özel Yeni Hayat Hastanesi Acil Çýkýþý", TelefonNo = "3288254004"},
new Eczane { Id =901,Enlem=37.063068,Boylam= 36.236504,Adres ="ADNAN MENDERES MAH.19537 SK.no:5/A MERKEZ/OSMANIYE", AdresTarifiKisa = "Metin Tamer Sitesi Arkasý AVM Tarafý Saðlýk Ocaðý Karþýsý Saray Pastanesi Yanýndaki Sokak", TelefonNo = "3288258010"},
new Eczane { Id =939,Enlem=37.074076,Boylam= 36.253939,Adres ="ISTIKLAL MAH. SEHT. MEHMET EROGLU CAD. NO:137 MERKEZ/OSMANIYE", AdresTarifiKisa = "Þehit Mehmet Eroðlu Cad. HALK BANKASI Karþýsý", TelefonNo = "3288146599"},
new Eczane { Id =924,Enlem=37.0723410,Boylam=36.2461990,Adres ="ALÝBEYLÝ MAH.KARAOÐLANOÐLU SOK. 14/A", AdresTarifiKisa = "Özel Park Hastanesi Yaný", TelefonNo = "3288127070"},
new Eczane { Id =898,Enlem=37.039324,Boylam= 36.227989,Adres ="FAKUÞAÐI MAH.45018 SK.NO:8/34 OSMANÝYE", AdresTarifiKisa = "", TelefonNo = "3288020099"},
new Eczane { Id =928,Enlem=37.074496,Boylam= 36.247938,Adres ="ALIBEYLI MAH. S. MEHMET TATLI SK. CEREN IS HANI ZEMIN KAT NO:10 MERKEZ/OSMANIYE", AdresTarifiKisa = "Ziraat Bankasý Karþý Sokaðý", TelefonNo = "3288133666"},
new Eczane { Id =933,Enlem=37.073140,Boylam= 36.250133,Adres ="YEDIOCAK MAH. ATATÜRK CD.NO:311 MERKEZ/OSMANIYE", AdresTarifiKisa = "Atatürk Cd. Kobaner Pasajý Yaný", TelefonNo = "3288141064"},
new Eczane { Id =948,Enlem=37.083038,Boylam= 36.266513,Adres ="MARESAL FEVZI ÇAKMAK MAH. HASTANE KARSISI NO:581 MERKEZ/OSMANIYE", AdresTarifiKisa = "Kadýn Doðum Hastanesi Yaný", TelefonNo = "3288143598"},
new Eczane { Id =913,Enlem=37.0710640,Boylam=36.2447360,Adres ="ALÝBEYLÝ MAH.DR.DEVLET BAHÇELÝ BULVARI 1.ETAP NO:27/A", AdresTarifiKisa = "ÖZEL SEVGÝ HASTANESÝ KARÞISI", TelefonNo = "3288123838"},
new Eczane { Id =915,Enlem=37.070599,Boylam= 36.245437,Adres ="DR.DEVLET BAHÇELÝ BULVARI.DEMÝRPEN ÝNÞAAT APT.NO:33 B/B", AdresTarifiKisa = "Sevgi Hastanesi Karþýsý Dr.Devlet Bahçeli Bulvarý 1.Etap", TelefonNo = "3288137070"},
new Eczane { Id =944,Enlem=37.083360,Boylam= 36.258149,Adres ="RIZAIYE MAH.HOCA AHMET YESEVI CAD.NO:79/B OSMANIYE", AdresTarifiKisa = "Nahar Yolu Cumhuriyet Saðlýk Ocaðý Yaný", TelefonNo = "3288132149"},
new Eczane { Id =943,Enlem=37.083374,Boylam= 36.258133,Adres ="Mareþal Fevzi Çakmak mah.Hoca Ahmet Yesevi cd.No:44/B", AdresTarifiKisa = "Nahar yolu, cumhuriyet saðlýk ocaðý karþýsý", TelefonNo = "3288127666"},
new Eczane { Id =909,Enlem=37.061855,Boylam= 36.238855,Adres ="M.AKIF ERSOY MH. 8010 SK. ÖZYURT SITESI B BLOK NO : 4", AdresTarifiKisa = "Yeni hayat hastanesi orta kapý karþýsý", TelefonNo = "3288251919"},
new Eczane { Id =935,Enlem=37.073485,Boylam= 36.251060,Adres ="ISTIKLAL MAH. ATATÜRK CAD. NO:182 MERKEZ/OSMANIYE", AdresTarifiKisa = "Atatürk Cad. Eski Belediye ve ÇARÞI Ýþ Bankasý arasý", TelefonNo = "3288143601"},
new Eczane { Id =930,Enlem=37.074911,Boylam= 36.248520,Adres ="CEVDET SUNAY CAD. NO:33 OSMANIYE", AdresTarifiKisa = "", TelefonNo = "3288143667"},
new Eczane { Id =920,Enlem=37.072707,Boylam= 36.245708,Adres ="ALÝBEYLÝ MAH.KARAOÐLANOÐLU SK.ORHUN ÖTÜKEN ÝNÞ.APT.NO:19/A OSMANÝYE", AdresTarifiKisa = "Özel Park Hastanesi Karþýsý", TelefonNo = "3288143701"},
new Eczane { Id =952,Enlem=37.083542,Boylam= 36.265977,Adres ="KAZIM KARABEKÝR MAH.6502 SK.", AdresTarifiKisa = "Kadýn Doðum Hastanesi Yaný", TelefonNo = "3288131513"},
new Eczane { Id =897,Enlem=37.055776,Boylam= 36.189888,Adres ="Akyar Köyü Hastane Mevkii 111/2", AdresTarifiKisa = "BÜYÜK OSMANÝYE OTELÝ YANI OPET KIDIK PETROL ÝÇÝ", TelefonNo = "5415689788"},
new Eczane { Id =929,Enlem=37.073829,Boylam= 36.247108,Adres ="ALIBEYLI MAH. SEHIT MEHMET SK. NO:4 MERKEZ/OSMANIYE", AdresTarifiKisa = "Ziraat Bankasý Karþý Sokaðý Toprakkale Garajý Yaný", TelefonNo = "3288149637"},
new Eczane { Id =949,Enlem=37.083618,Boylam= 36.266187,Adres ="KAZIM KARABEKÝR MAH.6542 SK.NO:13/A OSMANÝYE", AdresTarifiKisa = "Kadýn Doðum Hastanesi Karþýsý", TelefonNo = "3288147164"},
new Eczane { Id =947,Enlem=37.074039,Boylam= 36.250443,Adres ="ALIBEYLI MAH. CEVDET SUNAY CAD. NO:44/A MERKEZ/OSMANIYE", AdresTarifiKisa = "ÇARÞI POLÝS KARAKOLU ARKASI, AKBANK GÝRÝÞ KAPISI KARÞISI", TelefonNo = "3288124252"},
new Eczane { Id =917,Enlem=37.067728,Boylam= 36.2489378,Adres ="YEDÝOCAK MAH.DR.DEVLET BAHÇELÝ BULVARI.NO:87-A/D MERKEZ OSMANÝYE", AdresTarifiKisa = "Devlet bahçeli bulvarý 3.etap", TelefonNo = "5063864196"},
new Eczane { Id =902,Enlem=37.062298,Boylam= 36.233553,Adres ="ADNAN MENDERES MAH.19535 SK.ZÜMRÜT APT ZEMÝN KAT 3/A", AdresTarifiKisa = "Metin Tamer Sitesi Arkasý, AVM otopark çýkýþý tarafý, Sanayi Saðlýk Ocaðý karþýsý, Saray Pastanesi yanýndaki sokakta", TelefonNo = "3288256676"},
new Eczane { Id =904,Enlem=37.063585,Boylam= 36.235971,Adres ="ADNAN MENDERES MAH.19538:SK NO:8/A", AdresTarifiKisa = "Metin tamer Sitesi Arkasý PARK 328 Otopark Çýkýþý", TelefonNo = "3288120331"},
new Eczane { Id =908,Enlem=37.062033,Boylam= 36.238864,Adres ="Yeni Hayat Hastanesi Karþýsý M AKÝF ERSOY MAH.ATATÜRK CAD.8008 SK.NO.3", AdresTarifiKisa = "Yeni Hayat Hastanesi Karþýsý", TelefonNo = "3288255252"},
new Eczane { Id =921,Enlem=37.072501,Boylam= 36.245900,Adres ="ALIBEYLI MH.KARAOLANOGLU CD. NO:17/C MERKEZ/OSMANIYE", AdresTarifiKisa = "Özel Park Hastanesi Yaný", TelefonNo = "3288147000"},
new Eczane { Id =916,Enlem=37.071005,Boylam= 36.244900,Adres ="ALIBEYLI MAH. DR. DEVLET BAHÇELI BULV. NO: 29/B MERKEZ/ OSMANIYE", AdresTarifiKisa = "Sevgi Hastanesi Karþýsý, Devlet Bahçeli Bulvarý, Vatan Bilgisayar Yaný", TelefonNo = "3288136059"},
new Eczane { Id =905,Enlem=37.063544,Boylam= 36.235848,Adres ="ADNAN MENDERES MAHALLESI 19538 SOKAK NO:10/B MERKEZ/ OSMANIYE", AdresTarifiKisa = "Metin tamer Sitesi Arkasý PARK 328 Otopark Çýkýþý", TelefonNo = "3288256618"},
new Eczane { Id =906,Enlem=37.062447,Boylam= 36.238758,Adres ="RAUFBEY MH. ATATÜRK CD. SAFAEVLER SITESI B1 BLOK NO:7 MERKEZ/OSMANIYE", AdresTarifiKisa = "Yenihayat hastanesi karþýsý", TelefonNo = "3288251565"},
new Eczane { Id =946,Enlem=37.080450,Boylam= 36.261678,Adres ="MAREÞAL FEVZÝ ÇAKMAK MAHALLESÝ MUSA ÞAHÝN BULVARI 499/B ZEMÝN KAT DAÝRE 1", AdresTarifiKisa = "ÝBN-Ý SÝNA Hastanesi Yaný", TelefonNo = "3288120200"},


            	#endregion
            };

            foreach (var eczane in osmaniyeEnlemBoylamlar)
            {
                var result = context.Eczaneler.SingleOrDefault(w => w.Id == eczane.Id);

                result.Boylam = eczane.Boylam;
                result.Enlem = eczane.Enlem;
                result.TelefonNo = eczane.TelefonNo;
                result.Adres = eczane.Adres;
                result.AdresTarifiKisa = eczane.AdresTarifiKisa;
                result.AdresTarifi = eczane.AdresTarifi;

                context.SaveChanges();
            }

            #endregion

            #region eczaneler - enlem, boylam ekle - alanya

            var antalyaEnlemBoylamlar = new List<Eczane>()
            {
                #region Antalya enlem boylam

            	//new Eczane { Id = 803,
             //       Enlem = 36.7789507,
             //       Boylam = 34.5384343,
             //       TelefonNo = "3245023377",
             //       Adres = "ÇÝFTLÝKKÖY MH. 32133 SK. NO:6/B",
             //       AdresTarifiKisa = "ÇÝFTLÝKKÖY AÝLE SAÐLIÐI MERKEZÝ KARÞISI A101 YANI",
             //       AdresTarifi ="ÇÝFTLÝKKÖY AÝLE SAÐLIÐI MERKEZÝ KARÞISI A101 YANI"
             //   },
new Eczane { Id =5,Enlem=36.5395879643729,Boylam=32.041003704071,Adres = "OBA MAH. FÝDANLIK CAD. ALANYA EÐÝTÝM ARAÞTIRMA HAST. YANI NO: 88/D"},
new Eczane { Id =33,Enlem=36.5474827812,Boylam=32.0073777484,Adres = "HACET MAH. 25 M.LIK YOL HANCI PASTANESI KARSISI"},
new Eczane { Id =2,Enlem=36.551405,Boylam=32.012825,Adres = "GÜLLER PINARI MAH. 3 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =34,Enlem=36.5442578091,Boylam=32.007648431,Adres = "GÜLLER PINARI MH. ESREFKAHVECIOGLU CD. BÜYÜK OTEL KARSISI"},
new Eczane { Id =39,Enlem=36.5474486097,Boylam=31.9973150267,Adres = "SEVKET TOKUÇ CAD. 25 M.LIK YOL GARANTI BATI ÇAPRAZI"},
new Eczane { Id =49,Enlem=36.5579249180445,Boylam=31.9998854398727,Adres = "KÜÇÜKHAS BAHÇE MAH. 610 SOK. NO:3/B ÇEVRE YOLU SEYÝR TERASI KAVÞAÐI ÝSTÝKBAL ARKASI ALANYA"},
new Eczane { Id =26,Enlem=36.5476141164,Boylam=32.0021473201,Adres = "SEKERHANE MH.ÖZEL YASAM HASTANESI KARSISI"},
new Eczane { Id =32,Enlem=36.5471883084014,Boylam=31.996342241764,Adres = "ÞEVKET TOKUÞ CAD. 25 METRELÝK YOL ÜZERÝ. TURKCELL PLAZA KARÞISI. 57/A ALANYA"},
new Eczane { Id =8,Enlem=36.5470913424577,Boylam=32.0266216993331,Adres = "CÝKCÝLLÝ MEYDAN PAZARI GÝRÝÞÝ KÝPA ARKASI"},
new Eczane { Id =12,Enlem=36.533588185858,Boylam=32.0469689369201,Adres = "OBA MAH. OBA BAÞKENT HAST. SEMT POLÝKLÝNÝÐÝ KARÞISI GÖÇMENTÜRK YANI"},
new Eczane { Id =16,Enlem=36.5402818742377,Boylam=32.0414704084396,Adres = "OBA MAH. FÝDANLIK CAD. NO:5/A ALANYA"},
new Eczane { Id =53,Enlem=36.5469069824,Boylam=31.9938537966,Adres = "SARAY MAH. BAÞKENT HASTANESÝ YANI NO:13 ALANYA"},
new Eczane { Id =47,Enlem=36.5552874688,Boylam=31.9901414311,Adres = "SARAY MH. STAD CD. ÖZEL ANADOLU HASTANESI KARSISI"},
new Eczane { Id =48,Enlem=36.5536373479571,Boylam=31.9920426607131,Adres = "SARAY MAH.STAD CAD.NO:9/C ALANYAANADOLU CAN HASTANESÝ DOÐU ÇAPRAZI TADIM TANTUNÝ YANI"},
new Eczane { Id =56,Enlem=36.5471193548539,Boylam=31.9939175248146,Adres = "SARAY MAH. BAÞKENT HASTANESÝ YANI NO:15 ALANYA"},
new Eczane { Id =46,Enlem=36.5468198371682,Boylam=31.9926059246063,Adres = "SARAY MAH. BAÞKENT HASTANESÝ BATISI MÝGROS YANI"},
new Eczane { Id =21,Enlem=36.547494512,Boylam=32.0048767373,Adres = "SEKERHANE MH. SEVKET TOKUS CD TULUKLAR YANI 8/A"},
new Eczane { Id =41,Enlem=36.5523076635,Boylam=31.9940133666,Adres = "SARAY MH. SU GÖZÜ CD. SIFA TIP MERKEZI KARSISI"},
new Eczane { Id =1,Enlem=36.55661928119,Boylam=32.064419388771,Adres = "CAMÝ ALANI MH. ÇARÞAMBA CD. OBA KASABASI"},
new Eczane { Id =10,Enlem=36.5475761709598,Boylam=32.0332118868827,Adres = "CÝKCÝLLÝ SAÐLIK OCAÐI KARÞISI ALANYUM ALIÞVERÝÞ MERKEZÝ ÜSTÜ"},
new Eczane { Id =17,Enlem=36.5333769782763,Boylam=32.0463573932647,Adres = "OBA MAH. OBA BAÞKENT HAST. SEMT POLÝKLÝNÝÐÝ KARÞISI GÖÇMENTÜRK ALTI"},
new Eczane { Id =57,Enlem=36.5465779107,Boylam=31.994877266,Adres = "SARAY MH. KÜLTÜR CD. BASKENT HASTANESI ACIL YANI"},
new Eczane { Id =30,Enlem=36.5474586772,Boylam=31.9980432322,Adres = "SEKERHANE MH. SEVKET TOKUÇ CD 25 M.LIK YOL GARANTI BANKASI KARSISI"},
new Eczane { Id =4,Enlem=36.5477765658524,Boylam=32.0330321788787,Adres = "CÝKCÝLLÝ SAÐLIK OCAÐI YANI ALANYUM ALIÞVERÝÞ MERKEZÝ ÜSTÜ"},
new Eczane { Id =6,Enlem=36.5407085610625,Boylam=32.0418620109558,Adres = "OBA MAH. FÝDANLIK CAD. No:72/A YENÝ DEVLET HASTANESÝ KARÞISI"},
new Eczane { Id =45,Enlem=36.5546800729,Boylam=31.9740871795,Adres = "KIZLARPINARI MAH. 4 NOLU SAGLIK OCAGI YANI MIGROS KARSISI"},
new Eczane { Id =54,Enlem=36.5445512465,Boylam=31.997729226,Adres = "SEKERHANE MH. CUMAPAZARI GIRISI LCWAIKIKI DOGU YANI"},
new Eczane { Id =14,Enlem=36.5467224977,Boylam=32.0157013817,Adres = "GÜLLERPINARI MH. HASANAKÇALIOGLU CD. DAYIOGLU OTO YIKAMA KARSISI"},
new Eczane { Id =37,Enlem=36.5460327654,Boylam=32.0002030715,Adres = "SEKERHANE MH.ECZACILAR CD. 1 NOLU SAGLIK OCAGI ALTI"},
new Eczane { Id =19,Enlem=36.5463694810534,Boylam=31.9987133145332,Adres = "OBA MAH.YÜZBAÞIOÐLU SOKAK.NO:17/C-D ALANYA/ANTALYA"},
new Eczane { Id =18,Enlem=36.5379156591102,Boylam=32.0446515083312,Adres = "OBA MAH. FABRÝKA CAD. NO:11/A DÝÞ HASTANESÝ AÞAÐISI,MAKÝ KARÞISI,YEÞÝL ÇAM KAHVESÝ YANI"},
new Eczane { Id =23,Enlem=36.5471970693,Boylam=32.0001889845,Adres = "SEKERHANE MAH. 25M.LIK YOL ÜZERI BANK ASYA YANI"},
new Eczane { Id =27,Enlem=36.5471990823876,Boylam=32.0061993598937,Adres = "GÜLLERPINARI MH. 25 M.LIK YOL ÜZERI HACET KÖPRÜSÜ SÜVARI GIYIM ÇAPRAZI"},
new Eczane { Id =29,Enlem=36.5470891414,Boylam=31.9975965897,Adres = "SEVKET TOKUÇ CAD. 25 M.LIK YOL GARANTI BANKASI YANI"},
new Eczane { Id =7,Enlem=36.5403508338841,Boylam=32.0412880182266,Adres = "OBA MAH. FÝDANLIK CAD. No:5/A YENÝ DEVLET HASTANESÝ KARÞISI"},
new Eczane { Id =51,Enlem=36.5473984008621,Boylam=31.9943359494209,Adres = "SARAY MH. YAVUZ SULTAN SELÝM CD. BAÞKENT ÜNV. HASTANESÝ KARÞISI"},
new Eczane { Id =25,Enlem=36.5473711197,Boylam=32.001871954,Adres = "SEKERHANE MAH. SEVKET TOKUS CAD. ÖZEL YASAM HASTANESI YANI"},
new Eczane { Id =9,Enlem=36.5475158369119,Boylam=32.0330590009689,Adres = "CÝKCÝLLÝ SAÐLIK OCAÐI YANI ALANYUM ALIÞVERÝÞ MERKEZÝ ÜSTÜ"},
new Eczane { Id =827,Enlem=36541957,Boylam=32043492,Adres = "OBA MAH.FÝDANLIK CADDESÝ.ÞÜKRÜ KIR APT. NO:64/C ALANYA/ANTALYA"},
new Eczane { Id =891,Enlem=36.5553036249,Boylam=31.9910755187,Adres = "KADIPASA MH. STAD CD. ÖZEL ANADOLU HASTANESI YANI 26/C"},
new Eczane { Id =40,Enlem=36.5527087185,Boylam=31.9961353519,Adres = "KADIPASA MAH. IKIZLER SOK. 2 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =15,Enlem=36.5419842975234,Boylam=32.0440506935119,Adres = "METRO ALIÞVERÝÞ MERKEZÝ ARKASI, YENÝ BÖLGE HASTANESÝ YANI, AÐIZ VE DÝÞ SAÐLIÐI ARKASI."},
new Eczane { Id =35,Enlem=36.5468583508,Boylam=32.0014392466,Adres = "SEKERHANE MH.SEVKET TOKUÇ CD. KÖY DOLMUSLARI GIRISI"},
new Eczane { Id =55,Enlem=36.5428542212,Boylam=31.9970757529,Adres = "HAYATE HANIM CAD. NO:14/D SEKERBANK KARSISI"},
new Eczane { Id =3,Enlem=36.539989,Boylam=32.041135,Adres = "OBA MAH.FÝDANLIK CAD. NO:88/A ALANYA EÐÝTÝM VE ARAÞTIRMA HASTANESÝ YANI"},
new Eczane { Id =36,Enlem=36.5455264403,Boylam=32.000176411,Adres = "SEKERHANE MH.ECZACILAR CD. CUMA PAZARI"},
new Eczane { Id =31,Enlem=36547552,Boylam=32011564,Adres = "GÜLLERPINARI MAH. HASAN AKÇALIOÐLU CAD. NO:33/B ALANYA"},
new Eczane { Id =28,Enlem=36.547251,Boylam=32.006742,Adres = "25 M. YOL HACET KÖPRÜSÜ HANCI PASTANESI YANI"},
new Eczane { Id =11,Enlem=36.5399540349172,Boylam=32.041452974081,Adres = "OBA MAH. METRO MARKET ARKASI. YENÝ ALANYA EÐÝTÝM ARAÞTIMA HASTANESÝ ACÝL ÇIKIÞI KARÞISI"},
new Eczane { Id =52,Enlem=36.5471133972,Boylam=31.9938741276,Adres = "SARAY MH. YUNUS EMRE CD. BASKENT HASTANESI YANI"},
new Eczane { Id =42,Enlem=36.5521194831,Boylam=31.9940777626,Adres = "SARAY MH. SUGÖZÜ CD. SIFA POLIKLINIGI KARSISI"},
new Eczane { Id =22,Enlem=36.5441392099494,Boylam=31.9998130202293,Adres = "CUMA PAZARI GÜNEY GÝRÝÞÝ ÞEKERCÝLER MARKET KARÞISI 3/B (TÝCARET ODASI KARÞISI)"},
new Eczane { Id =38,Enlem=36.5452579766,Boylam=31.9982906073,Adres = "SEKERHANE MAH. TEVFIKIYE CD. DOLMUS DURAGI IÇI"},
new Eczane { Id =50,Enlem=36.5474808167,Boylam=31.9942393444,Adres = "SARAY MH. YUNUS EMRE CD. BASKENT HASTANESI KARSISI"},
new Eczane { Id =43,Enlem=36.5529758775211,Boylam=31.9959023594856,Adres = "KADIPAÞA MAH. TEL SOK. 2. NOLU SAÐLIK OCAÐI KARÞISI"},
new Eczane { Id =44,Enlem=36.5551163655,Boylam=31.9904135367,Adres = "SARAY MAH. STAD CAD. ÖZEL ANADOLU HASTANESI KARSISI"},
new Eczane { Id =24,Enlem=36.5477193615,Boylam=32.0021294428,Adres = "SEKERHANE MH. YUNUS GÜCÜOGLU SOK. ÖZEL YASAM HASTANESI KARSISI"},
       


            	#endregion
            };

            foreach (var eczane in antalyaEnlemBoylamlar)
            {
                var result = context.Eczaneler.SingleOrDefault(w => w.Id == eczane.Id);

                result.Boylam = eczane.Boylam;
                result.Enlem = eczane.Enlem;
                result.TelefonNo = eczane.TelefonNo;
                result.Adres = eczane.Adres;
                result.AdresTarifiKisa = eczane.AdresTarifiKisa;
                result.AdresTarifi = eczane.AdresTarifi;

                context.SaveChanges();
            }

            #endregion

            #region eczaneler - enlem, boylam ekle - antalya

            var anlanyaEnlemBoylamlar = new List<Eczane>()
            {
                #region Antalya enlem boylam

            	//new Eczane { Id = 803,
             //       Enlem = 36.7789507,
             //       Boylam = 34.5384343,
             //       TelefonNo = "3245023377",
             //       Adres = "ÇÝFTLÝKKÖY MH. 32133 SK. NO:6/B",
             //       AdresTarifiKisa = "ÇÝFTLÝKKÖY AÝLE SAÐLIÐI MERKEZÝ KARÞISI A101 YANI",
             //       AdresTarifi ="ÇÝFTLÝKKÖY AÝLE SAÐLIÐI MERKEZÝ KARÞISI A101 YANI"
             //   },
new Eczane { Id =109,Enlem=36.89657,Boylam=30.67808,Adres = "SOÐUKSU MAH. TOROSLAR CAD. NO:26/A SOÐUKSU AÝLE HEKÝMLÝÐÝ YANI MURATPAÞA"},
new Eczane { Id =69,Enlem=36.89058,Boylam=30.68008,Adres = "K.KARABEKIR CD.EGITIM ARASTIRMA HASTANESI ACIL KARSISI"},
new Eczane { Id =111,Enlem=36.89376,Boylam=30.68449,Adres = "YILDIZ MAH.YILDIZ CAD. 220. SOK.NO:25/B MURATPAÞA YILDIZ MEDSTAR HASTANESÝ KARÞISI"},
new Eczane { Id =88,Enlem=36.89366,Boylam=30.68033,Adres = "SOGUKSU CD. 78/B DEFTERDARLIK KARSISI"},
new Eczane { Id =67,Enlem=36.89385,Boylam=30.68445,Adres = "YILDIZ MAHALLESI 220 SOKAK NO : 25 / A MEDSTAR ANTALYA HASTANESI KARSISI"},
new Eczane { Id =65,Enlem=36.89019,Boylam=30.66844,Adres = "MELTEMDEKI YENI ANTALYA STADININ KUZEY KAPISININ KARSISI(MELTEM MAH.ASYA SITESI D3 BLOK ALTI)"},
new Eczane { Id =82,Enlem=36.89322,Boylam=30.68026,Adres = "KAZIM KARABEKIR CD.YILDIZ MH NO:70/2 DEFTERDARLIK KARSISI"},
new Eczane { Id =62,Enlem=36.89283,Boylam=30.66994,Adres = "EGITIM ARASTIRMA HASTANESI ARKASI MELTEM MAH. MELTEM BALIK ÇARSISI KARSISI"},
new Eczane { Id =89,Enlem=36.89209,Boylam=30.67408,Adres = "MELTEM MH.MELTEM CD.ÖZLEM SITESI IÇINDE DEVLET HAST.YAKINI MEGAPOL SINEMASI KUZEYINDE"},
new Eczane { Id =112,Enlem=36.89231,Boylam=30.68188,Adres = "YILDIZ MAH.YILDIZ CAD.NO:59/A-B MURATPASA ANTALYA EGITIM ARASTIRMA HASTANESINDEN TRT CADDESINE DOGRU 100 MT ILERIDE SAGDA"},
new Eczane { Id =73,Enlem=36.88996,Boylam=30.67999,Adres = "EGITIM ARASTIRMA HASTANESI KARSISI K.KARABEKIR CD."},
new Eczane { Id =84,Enlem=36.89931,Boylam=30.68271,Adres = "GÜVENLIK MAH. 259. SOK. NO:20/1 SOGUKSU CAMII KARSI SOKAGI 25 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =107,Enlem=36.89456,Boylam=30.68457,Adres = "YILDIZ MH 220 SK.GÜÇLÜ APT.NO:26 MEDSTAR ANTALYA HASTANESI KARSISI TRT CAD.ILE EGITIM ARASTIRMA HASTANESI ARASI"},
new Eczane { Id =108,Enlem=36.8997,Boylam=30.68043,Adres = "KAZIM KARABEKIR CAD.NO:144 SOÐUKSU CAMÝ KARÞISI SARIYAR TAKSÝ DURAÐI CÝVARI"},
new Eczane { Id =78,Enlem=36.89411,Boylam=30.68441,Adres = "YILDIZ MAH.YILDIZ CAD.220 SOKAK 27/C MEDSTAR ANTALYA HAST. KARSISI"},
new Eczane { Id =99,Enlem=36.90021,Boylam=30.68216,Adres = "GÜVENLÝK MAH.261 SOK.NO:5/A DEFTERDARLIKTAN ÇALLIYA DOÐRU GIDERKEN SOÐUKSU BÝM KARÞISI"},
new Eczane { Id =92,Enlem=36.89047,Boylam=30.67537,Adres = "EGITIM ARASTIRMA HAST.BATISI MELTEM MH. ÖZLEM SIT. A 17 BLOK NO:1 (YENÝ STADYUM YOLU ÜZERÝ)"},
new Eczane { Id =93,Enlem=36.89192,Boylam=30.6753,Adres = "MELTEM MAH.TARIKAKILTOPU CAD. ÖZLEM SÝT.A-10 BLK.NO:7-B/A EÐÝTÝM ARAÞTIRMA HAST. KARÞ. MEGAPOL SÝNEMASI YANI"},
new Eczane { Id =103,Enlem=36.89928,Boylam=30.68248,Adres = "GÜVENLIK MAH. 259. SOK. NO:18 SOGUKSU CAMII KARSI SOKAGI 25 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =60,Enlem=36.88948,Boylam=30.67995,Adres = "VARLIK MH. KAZIM KARABEKIR. CD. NO: 26/1 EGITIM ARASTIRMA HASTANESI KARS."},
new Eczane { Id =66,Enlem=36.89431,Boylam=30.68436,Adres = "MEDSTAR ANTALYA (ESKI YILDIZ ANDEVA) HASTANESI KARSISI YILDIZ MAH.220 SK."},
new Eczane { Id =70,Enlem=36.8997,Boylam=30.68043,Adres = "K.KARABEKIR CD. SARIYAR DÜGÜN SALONU YANI (SOGUKSU)"},
new Eczane { Id =101,Enlem=36.88929,Boylam=30.67994,Adres = "KAZIM KARABEKIR CAD.ANTALYA EGITIM ARASTIRMA HASTANESI ACIL KARSISI"},
new Eczane { Id =79,Enlem=36.89686,Boylam=30.67264,Adres = "EGITIM ARASTIRMA HAST.KUZEYI BAYINDIR MAH.ANTALYA KOLEJI JANDARMA YOLU ÜZERI"},
new Eczane { Id =68,Enlem=36.89041,Boylam=30.68004,Adres = "K.KARABEKIR CD.EGITIM ARASTIRMA HASTANESI KARSISI"},
new Eczane { Id =102,Enlem=36.89303,Boylam=30.68025,Adres = "KAZIM KARABEKIR CAD. NO:68 2 NOLU SAGLIK OCAGI KARSISI DEFTERDARLIK KARSISI"},
new Eczane { Id =86,Enlem=36.89273,Boylam=30.66856,Adres = "MELTEM MAH. MELTEM BULV. MELTEM CAMÝÝ YANI ANTALYASPOR SÝTESÝ ALTI MURATPAÞA"},
new Eczane { Id =90,Enlem=36.89032,Boylam=30.68001,Adres = "EGITIM ARASTIRMA HASTANESI ACIL ÇIKISI KARS. ESKI DEVLET HASTANESI"},
new Eczane { Id =72,Enlem=36.89199,Boylam=30.68018,Adres = "EGITIM ARASTIRMA HASTANESI KARSISI DEFTERDARLIK KAVSAGI"},
new Eczane { Id =483,Enlem=36.89436,Boylam=30.68438,Adres = "YILDIZ MAH.220 SOK. NO:27/B MURATPAÞA YILDIZ MEDSTAR HASTANESÝ KARÞISI"},
new Eczane { Id =106,Enlem=36.90021,Boylam=30.68248,Adres = "GÜVENLIK MAH.261 SK.NO:13 SARIYAR TAKSÝ SOK.ÝSMET YÜCE CAMÝÝ KARÞISI"},
new Eczane { Id =104,Enlem=36.89284,Boylam=30.68026,Adres = "EGITIM ARASTIRMA HASTANESI VE DEFTERDARLIK KARSISI"},
new Eczane { Id =98,Enlem=36.89086,Boylam=30.67572,Adres = "MELTEM MH.ÖZLEM ST.EGITIM ARASTIRMA HAST.BATI KAPISI KARSISI MEGAPOL SINEMASI YANI"},
new Eczane { Id =95,Enlem=36.89476,Boylam=30.68429,Adres = "YILDIZ MAH. HAMIDIYE CAD.SALIH ERCIYES APT 21/A YILDIZ ANDEVA HAST (MEDSTAR ANTALYA HAST) ACIL KARSISI"},
new Eczane { Id =74,Enlem=36.89722,Boylam=30.66878,Adres = "MELTEM MH.VATAN SITESI JANDARMA KARSISI YENI ADLIYE ARKASI ANTALYA KOLEJI KARSISI"},
new Eczane { Id =91,Enlem=36.89176,Boylam=30.68021,Adres = "K.KARABEKIR CD. YILDIZ MH. NO:54 EGITIM ARASTIRMA HASTANESI KARS."},
new Eczane { Id =87,Enlem=36.88953,Boylam=30.67995,Adres = "VARLIK MAH. KAZIM KARABEKIR CAD. EGITIM ARASTIRMA HAST. YENÝ ACIL KAPISI KARÞIS"},
new Eczane { Id =63,Enlem=36.89005,Boylam=30.68001,Adres = "EGITIM ARASTIRMA HASTANESI KARSISI K.KARABEKIR CD."},
new Eczane { Id =61,Enlem=36.88968,Boylam=30.67996,Adres = "MURATPAÞA ÝLÇESÝ VARLIK MAH.KAZIMKARABEKÝR CAD. NO:28/A ANTALYA EÐÝTÝM ARAÞTIRMA ACÝL KARÞISI"},
new Eczane { Id =75,Enlem=36.8968,Boylam=30.67294,Adres = "BAYINDIR MH.TOROSLAR CAD.JANDARMA VE DOGA KOLEJI YOLU ÜZERI"},
new Eczane { Id =85,Enlem=36.89246,Boylam=30.68026,Adres = "YILDIZ MAH. KAZIM KARABEKIR CAD. NO:60/3 (DEFTERDARLIK KARSISI)"},
new Eczane { Id =100,Enlem=36.89286,Boylam=30.66674,Adres = "MELTEM MH. 3. CD. MELTEM CAMII (50 M) ILERISI NO:3 (ANKARA SITESI)"},
new Eczane { Id =58,Enlem=36.89643,Boylam=30.67835,Adres = "SOGUKSU MAH.TOROSLAR CAD.HEDEF ECZA DEPOSU YOLU ÜZERI PASABAHÇEYE 100 METRE MESAFEDE"},
new Eczane { Id =94,Enlem=36.89977,Boylam=30.6807,Adres = "GÜVENLIK MAH.KAZIM KARABEKIR CAD.NO:150/A SARIYAR DÜGÜN SALONU YANI"},
new Eczane { Id =76,Enlem=36.89711,Boylam=30.6732,Adres = "BAYINDIR MAHALLESI PINAR CADDESI .ÇAKMAK APT.MANAVOGLU PARK KARSISI"},
new Eczane { Id =64,Enlem=36.88941,Boylam=30.67994,Adres = "VARLIK MAH. KAZIM KARABEKÝR CAD.( ANTALYA EÐÝTÝM ARAÞTIRMA HASTANESÝ ACÝL KARÞISI ) NO:24 MURATPAÞA"},
new Eczane { Id =71,Enlem=36.8932,Boylam=30.67513,Adres = "MELTEM MH.EGITIM ARASTIRMA HAST. MELTEM KAVSAGI ELEKTRIK TRAFOSU YANI"},
new Eczane { Id =97,Enlem=36.89345,Boylam=30.6803,Adres = "YILDIZ MAH. KAZIM KARABEKÝR CAD. 74/A DEFTERDARLIK KARÞISI VAKIFBANK BÝTÝÞÝÐÝ"},
new Eczane { Id =806,Enlem=36.89405,Boylam=30.68431,Adres = "YILDIZ MAHALLESÝ.ÇAKIRLAR CADDESÝ.NO:26/A YILDIZ MEDSTAR HASTANESÝ ACÝL KARÞISI MURATPAÞA/ANTALYA"},
new Eczane { Id =96,Enlem=36.89051,Boylam=30.68005,Adres = "KAZIM KARABEKIR CAD. EGITIM ARASTIRMA HASTANESI KARSISI"},
new Eczane { Id =83,Enlem=36.89407,Boylam=30.67356,Adres = "MELTEM MH.BELGEN SITESI 3.BLOK NO:1"},
new Eczane { Id =59,Enlem=36.90093,Boylam=30.68049,Adres = "SOÐUKSU MAH. KAZIM KARABEKÝR CAD. NO:95/B KIRAL APT. MURATPAÞA"},
new Eczane { Id =77,Enlem=36.8921,Boylam=30.68049,Adres = "EGITIM ARASTIRMA-DEFTERDARLIK KVS.DAN TRT YE GIDERKEN 10 METRE ILERDE SAGDA"},
new Eczane { Id =81,Enlem=36.89362,Boylam=30.68471,Adres = "TRT CADDESI ILE DEVLET HASTANESI ARASI MEDSTAR ANTALYA HASTANESI YANI"},
new Eczane { Id =105,Enlem=36.89271,Boylam=30.68225,Adres = "YILDIZ MH.YILDIZ CAD. FATIH I.Ö.OKULU KÖSESI (EGITIM ARS. HAST.-TRT ISTIKAMETI)"},
new Eczane { Id =110,Enlem=36.88953,Boylam=30.67995,Adres = "VARLIK MAH.KAZIM KARABEKÝR CAD. NO:22/A ANTALYA EÐÝTÝM ARAÞTIRMA HASTANESÝ ACÝL KARÞISI MURATPAÞA"},
new Eczane { Id =118,Enlem=36.89652,Boylam=30.6973,Adres = "ÜÇGEN MH. TONGUÇ CD. SALÝH ENER AP. NO:56/C"},
new Eczane { Id =124,Enlem=36.89216,Boylam=30.69165,Adres = "100.YIL ILGIM (TIP)MERK.ARKASI.TRT KARS.ARA SOKAGI 15 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =177,Enlem=36.88588,Boylam=30.69562,Adres = "DENÝZ MAHALLESÝ 122 SOKAK 4/A SELEKLERDEN KONYAALTI CADDESÝNE DOÐRU SAÐDAN 2. SOKAK"},
new Eczane { Id =128,Enlem=36.89383,Boylam=30.70034,Adres = "SARAMPOL CADDESI ÜÇGEN MAH. 90. SOKAK NO:4/B ÇORBACI ALÝ BABANIN ARKA SOKAÐI"},
new Eczane { Id =151,Enlem=36.89947,Boylam=30.68952,Adres = "GÜLLÜK CD.ATATÜRK DEVLET HAST.(ESKI SSK) OTOPARK ÇIKISI KARSISI ACIL KARSISI"},
new Eczane { Id =153,Enlem=36.89756,Boylam=30.69079,Adres = "GÜLLÜK CAD.AKTAS APT.ATATÜRK DEVLET HASTANESI KARSISI"},
new Eczane { Id =113,Enlem=36.89742,Boylam=30.69085,Adres = "GÜLLÜK CAD.NO:129/2 ATATÜRK DEVLET HASTANESI ACIL KARS."},
new Eczane { Id =144,Enlem=36.89697,Boylam=30.69344,Adres = "SSK ARKASI MERKEZ ORTAOKULU YANI 5 NOLU SAGLIK OCAGI KARS."},
new Eczane { Id =148,Enlem=36.89066,Boylam=30.69391,Adres = "ALTINDAG MH.100 YIL BULV.YAVUZ APT. GÜLLÜK ILE 100 YIL KAVS.YAPI KREDI BANKASI BATISI (100.YIL AYDIN KANZA PARKI KARÞISI)"},
new Eczane { Id =142,Enlem=36.89748,Boylam=30.69082,Adres = "GÜLLÜK CD.ATATÜRK DEVLET HASTANESI ( ESKI SSK HASTANESI) ACIL KARSISI"},
new Eczane { Id =115,Enlem=36.8984,Boylam=30.69031,Adres = "MEMUREVLERI MAH.GÜLLÜK CAD. NO:139/A ATATÜRK DEVLET HAST.KARS."},
new Eczane { Id =147,Enlem=36.88564,Boylam=30.69647,Adres = "KONYAALTI CD.ATESEN AP.8/C YAVUZ ÖZCAN PARKI KARSISI GÜLLÜK PTT ARKASI"},
new Eczane { Id =121,Enlem=36.89365,Boylam=30.70172,Adres = "SARAMPOL CAD.MARKANTALYA AVM KARÞISINDAKÝ BÝLGE HAN OTELÝ YANI"},
new Eczane { Id =294,Enlem=36.89633,Boylam=30.6997,Adres = "MURATPAÞA MAH. 563 SOK.NO:46/9 MURATPAÞA"},
new Eczane { Id =133,Enlem=36.89908,Boylam=30.68983,Adres = "GÜLLÜK CAD.ATATÜRK DEVLET HASTAHANESI KARSISI (ESKI SSK ANTALYA)"},
new Eczane { Id =140,Enlem=36.89721,Boylam=30.69131,Adres = "GÜLLÜK CD.ATATÜRK DEVLET HAST.(ESKI SSK)"},
new Eczane { Id =132,Enlem=36.89773,Boylam=30.69065,Adres = "ATATÜRK DEVLET HAST.KARSISI GÜLLÜK CADDESI NO:133/A SSK ACIL KARSISI"},
new Eczane { Id =125,Enlem=36.88963,Boylam=30.68624,Adres = "100. YIL BULVARI 54/C VARLIK MAH."},
new Eczane { Id =130,Enlem=36.90406,Boylam=30.68421,Adres = "SEDIR MH.VATAN BULV.ÇALLI ÜST GEÇIT YANI NO:34"},
new Eczane { Id =129,Enlem=36.8889,Boylam=30.68583,Adres = "YÜZÜNCÜYIL CAD. RADON TIP MERKEZÝ YANI VARLIK MAH. 172. SOK. NO:15/A-B MURATPAÞA"},
new Eczane { Id =119,Enlem=36.89237,Boylam=30.69385,Adres = "GÜLLÜK CAD. GÜLLÜK PETROL OFISI KARSISI MURATPASA ILÇE EMNIYET YANI"},
new Eczane { Id =135,Enlem=36.89445,Boylam=30.69257,Adres = "TRT (TONGUÇ) CD. TRT - GÜLLÜK KAVSAGI KENT PASTANESÝ KARÞISI"},
new Eczane { Id =146,Enlem=36.90021,Boylam=30.68915,Adres = "ATATÜRK DEVLET HASTANESI (ESKI SSK) CIVARI NO:155 ORMAN BÖLGE MÜD.KARSISI"},
new Eczane { Id =152,Enlem=36.89061,Boylam=30.69359,Adres = "100. YIL BULVARI AYDIN KANZA PARKI KARS.(100.YIL-GÜLLÜK KAVS.) NO : 9/B"},
new Eczane { Id =114,Enlem=36.8991,Boylam=30.69438,Adres = "ABDI IPEKÇI CAD. 72/D SARAMPOL ÇARSAMBA PAZARI GIRISI ANTALYA RAF YANI"},
new Eczane { Id =122,Enlem=36.89761,Boylam=30.69637,Adres = "ABDÝ ÝPEKÇÝ CAD. PTT ÞARAMPOL ÞUBESÝ KARÞISI - MURATPAÞA"},
new Eczane { Id =219,Enlem=36.89791,Boylam=30.69047,Adres = "MEMUREVLERÝ MAH.GÜLLÜK CAD. AKTAÞ APT.NO.131/B ATATÜRK DEVLET HASTANESÝ ACÝL KARÞISI"},
new Eczane { Id =149,Enlem=36.89432,Boylam=30.692,Adres = "TRT (TONGUÇ) CD. SARAY APT. ALTI NO: 28/B TRT KARSISI"},
new Eczane { Id =141,Enlem=36.89844,Boylam=30.69027,Adres = "ATATÜRK DEVLET HASTANESI KARSISI (ESKI SSK HASTANESI) GÜLLÜK CADDESI"},
new Eczane { Id =139,Enlem=36.8974,Boylam=30.69137,Adres = "GÜLLÜK CADDESI ATATÜRK DEVLET HASTANESI POLIKLINIK KARSISI NO:2/B (ESKI SSK) KARSISI"},
new Eczane { Id =136,Enlem=36.8982,Boylam=30.6904,Adres = "GÜLLÜK CAD. NO:137/C VATAN APT.ATATÜRK DEVLET HASTANESI (ESKI SSK) KARS."},
new Eczane { Id =123,Enlem=36.89826,Boylam=30.69034,Adres = "ATATÜRK DEVLET HAST.(ESKÝ SSK HAST.)KARSISI GÜLLÜK CADDESI NO:137/D OTOPARK GIRIS KARSISI"},
new Eczane { Id =131,Enlem=36.9039,Boylam=30.68704,Adres = "SEDÝR MAH. 725.SOK. NO:15/A-B MURATPAÞA ÇALLI EMNÝYET MÜDÜRLÜÐÜ KARÞISINDAKÝ ARA SOKAK HALÝDE EDÝP ADIVAR ANA OKULU KARÞISI"},
new Eczane { Id =126,Enlem=36.89665,Boylam=30.69362,Adres = "SSK HASTANESI ACIL YANI KÜTÜPHANE SOKAGI 5 NOLU SAGLIK OCAGI KARS."},
new Eczane { Id =120,Enlem=36.88767,Boylam=30.69854,Adres = "GÜLLÜK PTT KARS. KATLI OTOPARK ARKASI VALILIK YOLU ÜZERI SEHIT CENGIZ TOYTUNÇ CAD."},
new Eczane { Id =138,Enlem=36.89537,Boylam=30.69942,Adres = "SARAMPOL CD. (ABDI IPEKÇI CAD.) ÇORBACI ALI BABANIN YANI"},
new Eczane { Id =150,Enlem=36.89878,Boylam=30.69002,Adres = "ANAFARTALAR CAD. ATATÜRK DEVLET HAST. KARSISI (SSK OTOPARK KARSISI) NO:141/B"},
new Eczane { Id =145,Enlem=36.8922,Boylam=30.69182,Adres = "ALTINDAG MH.TRT CAD.151 SK.15 NOLU SAGLIK OCAGI KARS.TRT BINASI KARS.ARA SOKAGI"},
new Eczane { Id =137,Enlem=36.90306,Boylam=30.68535,Adres = "SEDÝR MAH.VATAN CADDESÝ. NO:8/B MURATPAÞA. ÇALLI EMNÝYET MÜDÜRLÜÐÜ KARÞISI."},
new Eczane { Id =127,Enlem=36.899,Boylam=30.6899,Adres = "ATATÜRK DEV. HAST. KARSISI (ESKI SSK) GÜVENLIK MAH.GÜLLÜK CAD. 277 SK.NO:143/A"},
new Eczane { Id =117,Enlem=36.89774,Boylam=30.69065,Adres = "GÜLLÜK CAD.ANTALYA ATATÜRK DEVLET HASTANESÝ(ESKÝ SSK) ACÝL KARÞISI MEMUREVLERÝ MAH."},
new Eczane { Id =143,Enlem=36.89303,Boylam=30.68845,Adres = "ALTINDAG MAH.TURGUT REIS CAD.ESKÝ GÜNEYLÝLER RESTAURANT ÝLE SAMANYOLU PASTANESÝ ARASI MERVE APT. NO:60 MURATPASA"},
new Eczane { Id =498,Enlem=36.89895,Boylam=30.72447,Adres = "GEBÝZLÝ MAH. 1115 SOK. KARALAR APT. NO:10/E ASV YAÞAM HASTANESÝ ACÝL KAPISI KARÞISI KARALAR DÜÐÜN SALONU ALTI / MURATPAÞA"},
new Eczane { Id =164,Enlem=36.90683,Boylam=30.68551,Adres = "SEDIR MAH.GAZI BULVARI ÜNSAL APT.76/A ÇALLI MEYDAN TIP MERKEZI YANI"},
new Eczane { Id =171,Enlem=36.90165,Boylam=30.69896,Adres = "CUMHURIYET MH.ESKI SANAYI ALTI OPERA YASAM HASTANESI YANI"},
new Eczane { Id =181,Enlem=36.90294,Boylam=30.72707,Adres = "YENIGÜN MAH.YUNUS EMRE CAD. ANTALYA ANADOLU LISESI GIRISI YANI"},
new Eczane { Id =161,Enlem=36.89884,Boylam=30.72406,Adres = "GEBÝZLÝ MAH.1115 SOKAK NO:6 / A KARALAR DÜÐÜN SARAYI YANI - ASV YAÞAM HAST. KARÞISI"},
new Eczane { Id =229,Enlem=36.89543,Boylam=30.72521,Adres = "KIZILTOPRAK MAH.ATÝLA NÝZAM SEMT POLÝKLÝNÝÐÝ KARÞISI.KARALAR DÜÐÜN SALONU GÜNEYÝ.YAÞAM HAST.ACÝL KARÞI SOKAÐI."},
new Eczane { Id =158,Enlem=36.90589,Boylam=30.69906,Adres = "ESKI SANAYI MH.FATIH CD.659 SK.9 NOLU SAGLIK OCAGI KARS. (ITFAIYE CIVARI)"},
new Eczane { Id =228,Enlem=36.8992,Boylam=30.70894,Adres = "ETÝLER MAH.882 SK.NO:19/A MEVLANA LOKANTISI ARKASI . ÖZEL ÇOCUK TIP MERKEZÝ KARÞISI MURATPAÞA"},
new Eczane { Id =494,Enlem=36.90774,Boylam=30.718,Adres = "KIZILARIK MAH. 2769 SOK. NO:11/B KIZILARIK TAKSÝ DURAÐI SAÐINDAN GÝRÝNCE 200 MT ÝLERDE MURATPAÞA"},
new Eczane { Id =175,Enlem=36.90326,Boylam=30.72855,Adres = "GEBIZLI MH.ASIK VEYSEL CD.32 NOLU AILE HEKIMLIGI ÇAPRAZ KARS. ZEYTINKÖY KAVSAGI"},
new Eczane { Id =180,Enlem=36.90711,Boylam=30.69973,Adres = "DUTLUBAHÇE MAH. FATIH.CAD.NO 38 REAL - ESKISANAYI ARASI LUKOIL YANI"},
new Eczane { Id =159,Enlem=36.90588,Boylam=30.69929,Adres = "ESKÝ SANAYÝ MAH. FATÝH CAD. 659 SOK. 9 NOLU SAÐLIK OCAÐI KARÞISI ÝTFAÝYE CÝVARI"},
new Eczane { Id =172,Enlem=36.90573,Boylam=30.69949,Adres = "ESKI SANAYI MAH.637 SOKAK ITFAIYE VE 9 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =154,Enlem=36.90288,Boylam=30.7124,Adres = "ETILER MAH.845 SOK.NO:49/A-B ETÝLER DÜÐÜN SALONU ARASI ALTAY FIRIN KARÞISI"},
new Eczane { Id =169,Enlem=36.89875,Boylam=30.7244,Adres = "GEBIZLI MAH.1115 SOK.KARALAR APT.NO:10/C KARALAR DÜGÜN SARAYI YANI GEBÝZLÝ MAH.YAÞAM HASTANESÝ ACÝL KARÞISI"},
new Eczane { Id =176,Enlem=36.90129,Boylam=30.72544,Adres = "YENIGÜN MAH.YUNUS EMRE CAD.NO:93A/A ANTALYA ANADOLU LISESI YANI (ÇOCUK ESIRGEME KURUMU KARSISI)"},
new Eczane { Id =167,Enlem=36.89643,Boylam=30.73334,Adres = "DOÐUYAKA MAH.1216 SOK.MOON LÝGHT APT.DOÐUYAKA ASM ÇAPRAZI TERMESOS BULV. MÝGROS ARKASI MURATPAÞA"},
new Eczane { Id =173,Enlem=36.90333,Boylam=30.72683,Adres = "YENIGÜN MAH. KÖROGLU BULVARI 54/B ANTALYA ANADOLU LISESI MEVKI 32 NOLU SAÐLIK OCAÐI ARKASI"},
new Eczane { Id =163,Enlem=36.90734,Boylam=30.68742,Adres = "GAZI BULVARI SEDIR MAHALLESI 86/C MURATPASA ÇALLI MEYDAN TIP MERKEZI YANI"},
new Eczane { Id =162,Enlem=36.90808,Boylam=30.70983,Adres = "KONUKSEVER MAH. 819 .SOK.NO:46/A-B MURATPAÞA 3 NOLU VALÝ SAÝM ÇOTUR ASM KARÞISI"},
new Eczane { Id =179,Enlem=36.90418,Boylam=30.72015,Adres = "YENÝGÜN MAH. KÖROÐLU BULVARI AYTEMÝZ PETROL ÝLE ZEYTÝNKÖY ARASI MURATPAÞA"},
new Eczane { Id =183,Enlem=36.8992,Boylam=30.70894,Adres = "ETILER MAH.MEVLANA LOKANTASI YANI ETILER TAKSI ARKASI EVLIYA ÇELEBI CD.861 SK.NO:7/A MAVI APT."},
new Eczane { Id =174,Enlem=36.89782,Boylam=30.73432,Adres = "DOÐUYAKA MAH. 1216 SOK. NO:5/B MURATPAÞA.TERMESOS KÝPA ARKASI."},
new Eczane { Id =168,Enlem=36.90398,Boylam=30.72652,Adres = "KIZILARIK MAH. KÖROÐLU BULV. YAVUZEVLERÝ APT. NO:65/B ANADOLU LÝSESÝ MEVKÝÝ 32 NO'LU SAÐLIK OCAÐI YANI MURATPAÞA"},
new Eczane { Id =182,Enlem=36.89887,Boylam=30.72531,Adres = "GEBÝZLÝ MAH. 1115 SOK. 4/C ANTALYA VAKIF YAÞAM HASTANESÝ KARÞISI ( KARALAR DÜÐÜN SALONU YAKINI)"},
new Eczane { Id =170,Enlem=36.90797,Boylam=30.7102,Adres = "KONUKSEVER MAHALLESI 821 SOKAK 3 NOLU SAGLIK OCAGI KARSISI KONUKSEVER KAPALI CUMARTESÝ PAZARI KARÞISI"},
new Eczane { Id =155,Enlem=36.89798,Boylam=30.7056,Adres = "REAL ALISVERIS MERKEZI DEPO GIRISI YANI"},
new Eczane { Id =160,Enlem=36.90692,Boylam=30.68602,Adres = "SEDIR MH.GAZI BULV.ÜNSAL APARTMANI NO:122/4 ÇALLI MEYDAN TIP MERKEZI YANI"},
new Eczane { Id =157,Enlem=36.89908,Boylam=30.72375,Adres = "GEBÝZLÝ MAH. 1115 SOK. VAKIF YAÞAM HASTANESÝ KARÞISI MURATPAÞA"},
new Eczane { Id =166,Enlem=36.90125,Boylam=30.69879,Adres = "CUMHURIYET MAH.640 SK.OPERA YASAM HASTANESI YANI"},
new Eczane { Id =156,Enlem=36.90301,Boylam=30.72683,Adres = "YENIGÜN MAH.YUNUS EMRE CAD.32 NOLU ASM ARKASI ANTALYA ANADOLU LISESI YANI"},
new Eczane { Id =198,Enlem=36.88614,Boylam=30.70853,Adres = "ATATÜRK CAD. NO:15/A DÖNERCILER ÇARSISI ILE ÜÇKAPILAR ARASI"},
new Eczane { Id =493,Enlem=36.89257,Boylam=30.71571,Adres = "MEVLANA CADDESI BP KARSI ÇAPRAZI SALI PAZARI GIRISI 30 NOLU SAGLIK OCAGI ÖNÜ"},
new Eczane { Id =186,Enlem=36.88685,Boylam=30.71424,Adres = "ÇAYBASI MH.1343 SK.NO:23/ A 1 NOLU AILE SAGLIGI MRK. 50 METRE YANI DOGU GARAJI"},
new Eczane { Id =212,Enlem=36.8891,Boylam=30.72189,Adres = "ALI ÇETINKAYA CAD. MEYDAN TIP MERKEZI YANI"},
new Eczane { Id =209,Enlem=36.88778,Boylam=30.72024,Adres = "ÇAYBASI MH.B.ONAT CD.MEYDAN KAVSAGINDAN B.ONATA DÖNÜSTE ANADOLU HAST.GELMEDEN 6. NOTER YANI"},
new Eczane { Id =191,Enlem=36.8867,Boylam=30.70198,Adres = "CUMHURÝYET MEYDANI TOPHANE PARKI KARÞISI"},
new Eczane { Id =207,Enlem=36.88722,Boylam=30.71399,Adres = "ÇAYBASI MAH. ALI ÇETINKAYA CD. YIKILAN HALK PAZARI DOGUSU 1 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =205,Enlem=36.89179,Boylam=30.7034,Adres = "MARKANTALYA AVM YANI MURATPASA CAMII KARSISI SARAMPOL CAD. 82/C"},
new Eczane { Id =190,Enlem=36.88962,Boylam=30.71092,Adres = "YÜKSEKALAN MAH. FAHRETTÝN ALTAY CAD. 1. BENLÝOÐLU ÝÞHANI NO:8/B MURATPAÞA DOÐU GARAJI START OTEL YANI"},
new Eczane { Id =200,Enlem=36.89227,Boylam=30.71583,Adres = "MEVLANA CADDESI BP ÇAPRAZ KARÞISI SALI PAZARI 30 NOLU SAÐLIK OCAÐI ÖNÜ"},
new Eczane { Id =199,Enlem=36.8876,Boylam=30.71371,Adres = "DOGU GARAJI 1 NOLU SAGLIK OCAGI ALTI"},
new Eczane { Id =214,Enlem=36.8925,Boylam=30.71574,Adres = "MEVLANA CD. SALI PAZARI GIRISI NO:41 BP PETROL KARSISI"},
new Eczane { Id =203,Enlem=36.89267,Boylam=30.70145,Adres = "SARAMPOLDEN 100. YIL GIRISI MARK ANTALYA AVM BATISI SISÇI RAMAZAN YANI"},
new Eczane { Id =211,Enlem=36.88693,Boylam=30.71848,Adres = "ÇAYBASI MAH.BURHANETTIN ONAT CAD.NO:20/A ANADOLU HASTANESI GÝRÝÞ KAPISI KARSISI"},
new Eczane { Id =208,Enlem=36.88583,Boylam=30.71909,Adres = "ÇAYBAÞI MAH. BURHANETTÝN ONAT CAD. KANARYA APT. NO:30/A-B MURATPAÞA"},
new Eczane { Id =185,Enlem=36.88901,Boylam=30.72199,Adres = "KIZILTOPRAK MAH.ALI ÇETINKAYA CAD.NO:133/C MEYDAN TIP HAST.YANI"},
new Eczane { Id =202,Enlem=36.88943,Boylam=30.70454,Adres = "ÞARAMPOL CAD. 40/Ý (KAPALI YOL) KIÞLAHAN ÇARÞI YAKINI GARANTÝ BANKASI KARÞISI"},
new Eczane { Id =201,Enlem=36.89042,Boylam=30.70251,Adres = "ELMALI MAH.HASAN SUBASI CAD. 24/3 MURATPASA CAMII GÜNEYI"},
new Eczane { Id =189,Enlem=36.88723,Boylam=30.70773,Adres = "ATATÜRK CD.VAKIF ISHANI DÖNERCILER ÇARSISI KARSISI NO:48"},
new Eczane { Id =215,Enlem=36.89288,Boylam=30.70362,Adres = "MARKANTALYA AVM IÇI ANA GIRIS KAPISI YANI"},
new Eczane { Id =195,Enlem=36.88896,Boylam=30.71246,Adres = "YÜKSEKALAN MAH.ALÝ ÇETÝNKAYA CAD. NO:23/A MURATPAÞA"},
new Eczane { Id =194,Enlem=36.88696,Boylam=30.7194,Adres = "MEYDAN ANADOLU HASTANESI GÝRÝÞÝ KARÞISI DR.BURHANETTIN ONAT CAD. NERGÝZHAN APT.NO:20/B-C"},
new Eczane { Id =213,Enlem=36.88773,Boylam=30.70734,Adres = "BALBEY MAH. ESKI TEKEL BINASI YANI DÖNERCILER ÇARSISI ÇAPRAZI ÝÞ BANKASI KARÞISI"},
new Eczane { Id =196,Enlem=36.88818,Boylam=30.71361,Adres = "ÇAYBASI MH.1343 SK.NO:7 1 NOLU SAGLIK OCAGI 50 METRE ÜSTÜ"},
new Eczane { Id =193,Enlem=36.88692,Boylam=30.7025,Adres = "CUMHURIYET MEYDANI ATATÜRK ANITI KARSISI ESKI VALILIK YANI NO:1"},
new Eczane { Id =204,Enlem=36.88925,Boylam=30.7211,Adres = "KIZILTOPRAK MAH. MEYDAN KAVSAGI (YAPI KREDI BANKASI VE MEYDAN TIP MERKEZÝ YANI)"},
new Eczane { Id =210,Enlem=36.8877,Boylam=30.71369,Adres = "ÇAYBAÞI MAH. 1343.SOK. NO:11/A DOÐUGARAJI 1 NOLU SAÐLIK OCAÐI YANI"},
new Eczane { Id =188,Enlem=36.89234,Boylam=30.71581,Adres = "YÜKSEKALAN MAH.MEVLANA CAD.KEMAL TUNCAY APT. NO:37/C MURATPAÞA"},
new Eczane { Id =192,Enlem=36.88717,Boylam=30.71403,Adres = "ÇAYBASI MAH.1343 SOKAK NO:19/3 DOGU GARAJI 1 NOLU SAGLIK OCAGI 10 METRE YANI"},
new Eczane { Id =197,Enlem=36.88691,Boylam=30.71858,Adres = "ÇAYBASI MAH. 1352/1 SOK. NO 6/A MEYDAN ANADOLU HASTANESI ACIL KAPISI YANI"},
new Eczane { Id =187,Enlem=36.88316,Boylam=30.71317,Adres = "BURHANETTÝN ONAT CADDESÝ BATISINDAKÝ CEBESOY CADDESÝNDE A 101 TAKSÝ DURAÐI ARASI KUMRUCU ÝSO YANI"},
new Eczane { Id =248,Enlem=36.86714,Boylam=30.73592,Adres = "SIRINYALI MAH. 1515 SOKAK KIRCAMI CÝVÝL MAÐAZASI ARKASI SIRINYALI AILE SAGLIGI MERKEZI KARSISI"},
new Eczane { Id =237,Enlem=36.88059,Boylam=30.71763,Adres = "BURHANETTIN ONAT CADDESI ÇELÝKLER OTO GALERÝ YANI NO:72/B MURATPAÞA"},
new Eczane { Id =808,Enlem=36.8634,Boylam=30.73199,Adres = "ÞÝRÝNYALI MAH. ÝSMET GÖKÞEN CADDESÝ..1487 SOKAK. NO:3/B-C MURATPAÞA YAÞAM HASTANESÝ KARÞISI"},
new Eczane { Id =257,Enlem=36.86316,Boylam=30.73142,Adres = "SIRINYALI MH.1487 SK.NO:8/B YASAM HASTANESI YANI"},
new Eczane { Id =254,Enlem=36.87171,Boylam=30.73226,Adres = "KIRCAMÝ AÐIZ VE DÝÞ SAÐLIÐI MERKEZÝNDEN LAURA ÝSTÝKAMETÝNE GÝDERKEN ÝLK YAYA TRAFÝK IÞI YANINDA"},
new Eczane { Id =218,Enlem=36.87715,Boylam=30.71118,Adres = "ISIKLAR CADDESI STADYUMU GEÇINCE SOLDA TRAMVAY YOLU ÜZERINDE CENDER OTELE GELMEDEN"},
new Eczane { Id =245,Enlem=36.88731,Boylam=30.72457,Adres = "ÇAYBASI MH.1359 SOKAK NO:5 MEYDAN KIZILTOPRAK PTT ILERISI ANTALYA TIP MERKEZI ACIL YANI ANTALYA TIP MERKEZI ACIL YANI"},
new Eczane { Id =240,Enlem=36.88472,Boylam=30.73991,Adres = "TARIM MAH.PERGE BULV.ERÜST APT.25/B - TOPÇULAR MEDSTAR YANI"},
new Eczane { Id =250,Enlem=36.8674,Boylam=30.73608,Adres = "SIRINYALI MH.1515 SK. KIRCAMI BIM VE CARREFOUR ARKASINDAKI SOKAK SIRINYALI ASM KARSISI"},
new Eczane { Id =239,Enlem=36.86896,Boylam=30.72688,Adres = "YESILBAHÇE MAH.ÇINARLI CAD.KIRMIZIGL ÇIÇEKÇILIK DEDEMAN MEVKI ILERISI FIZIKALYA TIP MERKEZI YANI"},
new Eczane { Id =226,Enlem=36.87123,Boylam=30.73117,Adres = "YESILBAHÇE MH.1482 SK.DÜRIYE KAPSIR APT.KIRCAMII AKDENIZ DONDURMA SOKAGI"},
new Eczane { Id =244,Enlem=36.8785,Boylam=30.71687,Adres = "ZERDALÝLÝK MAH. 1387 SOK. GÜLPERÝ ZEYBEK APT. NO:17/B BURHANETTÝN ONAT(ESKÝ KOMAÞ YENÝ MÝGROS YANI) SOKAÐI B.ONAT ASM KARÞISI"},
new Eczane { Id =231,Enlem=36.86335,Boylam=30.72998,Adres = "ISMET GÖKSEN CADDESI YASAM HASTANESI KARSISI DEGIRMENCI BABA YANI"},
new Eczane { Id =227,Enlem=36.87674,Boylam=30.73489,Adres = "PERGE CAD.MEYDAN KAVAÐI MAH.62/A KITIR EKMEK FIRINI YANI"},
new Eczane { Id =238,Enlem=36.88787,Boylam=30.7292,Adres = "KIZILTOPRAK MAH. 920 SOK. 12/B YUNUS EMRE ASM KARÞISI(LÝKYA HAST. VE M.N.ÇAKALLIKLI ANADOLU LÝSESÝNÝN BATISI)"},
new Eczane { Id =221,Enlem=36.88608,Boylam=30.73968,Adres = "TARIM MAH.PERGE BULV.NO:13/H MURATPAÞA MEDSTAR TOPÇULAR HASTANESÝ YANI"},
new Eczane { Id =251,Enlem=36.88524,Boylam=30.73982,Adres = "TARIM MAH. PERGE CAD NO:21/B MEDSTAR TOPÇULAR HASTANESI YANI"},
new Eczane { Id =216,Enlem=36.86228,Boylam=30.73126,Adres = "SIRINYALI MH.1488 SK. 8 NOLU SAGLIK OCAGI YANI YASAM HASTANESI SIRTI"},
new Eczane { Id =252,Enlem=6.88778,Boylam=30.73312,Adres = "KIZILTOPRAK MAH. 921 SOK. NO:36 ÖZEL LÝKYA HASTANESÝ GÝRÝÞÝ KARÞISI FESTÝVAL ÇARÞISI KUZEYÝ"},
new Eczane { Id =241,Enlem=36.87101,Boylam=30.72618,Adres = "YEÞÝLBAHÇE MAH.1450 SOK.NO:35/A MURATPAÞA DEDEMAN DONKÝÞOT SOKAÐI ÇARÞAMBA PAZARI ARKASI 27 NOLU ASM KARÞISI"},
new Eczane { Id =259,Enlem=36.88779,Boylam=30.72354,Adres = "ALI ÇETINKAYA CD.MEYDAN KIZILTOPRAK PTT KARSISI ANTALYA TIP MERKEZI YANI"},
new Eczane { Id =235,Enlem=36.88509,Boylam=30.7248,Adres = "MEYDAN KAVAÐI MAH. DEÐÝRMENÖNÜ CAD. YILDIZ APT. NO:122/A MURATPAÞA GEBÝZLÝ CAMÝ VE GEBÝZLÝ LÝSESÝ 50 MT AÞAÐISI"},
new Eczane { Id =256,Enlem=36.88797,Boylam=30.72394,Adres = "KIZILTOPRAK MAH. ALÝ ÇETÝNKAYA CAD. NO:145/A MURATPAÞA ÖZEL ANTALYA TIP MERKEZÝ KARÞISI.MEYDAN POSTANESÝ HÝZASI ."},
new Eczane { Id =253,Enlem=36.87784,Boylam=30.71071,Adres = "ISIKLAR CD. TÜRKAY APT. NO:27/A ESKÝ STADYUMDAN CENDER OTELÝNE DOÐRU ÝNERKEN 200 MT ÝLERDE SOLDA TRAMVAY HATTI ÜZERINDE"},
new Eczane { Id =246,Enlem=36.8738,Boylam=30.72245,Adres = "YESILBAHÇE MAH.PORTAKAL ÇIÇEGI BULVARI NO:21 OLIMPIK YÜZME HAVUZU KARSISI"},
new Eczane { Id =232,Enlem=36.88094,Boylam=30.72712,Adres = "ISMAIL CEM CAD.(12.CAD) MEYDAN KAVAGI MAH.CARREFOURSA YANI EKSIOGLU BAYRAKTAR SITESI ASAGISI"},
new Eczane { Id =224,Enlem=36.87442,Boylam=30.73234,Adres = "KIRCAMI MH. PERGE BLV. KADIN DOGUM VE ÇOCUK HAST.YANI TOTAL BENZIN ISTASYONU YANI"},
new Eczane { Id =247,Enlem=36.86319,Boylam=30.73157,Adres = "ÞÝRÝNYALI MAH.1487 SOK.NO:10/A LARA YAÞAM HASTANESÝ YANI MURATPAÞA"},
new Eczane { Id =230,Enlem=36.87112,Boylam=30.71913,Adres = "YESILBAHÇE MAH.METIN KASAPOGLU CAD.NO:24/B SAMPÝ KAVÞAÐINDAN LARA YÖNÜNE 200 MT SONRA ÖZSÜT YANI"},
new Eczane { Id =217,Enlem=36.86792,Boylam=30.73655,Adres = "SIRINYALI MAH. SINANOGLU CD. (KIRCAMII CARREFOUR-SA MARKET YANI)"},
new Eczane { Id =233,Enlem=36.87808,Boylam=30.71283,Adres = "19 MAYIS CD. ZERDALÝLÝK CAMÝ KARÞISI YENIKAPI POLÝS MERKEZÝ YOLU ÜZERÝ NO: 30/A"},
new Eczane { Id =242,Enlem=36.88104,Boylam=30.73112,Adres = "MEYDANKAVAGI MAH. ISMAIL CEM CAD.( 12. CADDE ) NO :36/B EKSIOGLU BAYRAKTAR SITESI KARSISI"},
new Eczane { Id =225,Enlem=36.86214,Boylam=30.73052,Adres = "SIRINYALI MH. ISMET GÖKSEN CD.DEDEMAN MC DONALS ÇAPRAZ KARÞISI YASAM HASTANESI CIVARI"},
new Eczane { Id =234,Enlem=36.89025,Boylam=30.73822,Adres = "MEHMETÇÝK MAH. TERMESSOS BULV. CANPARK SÝT. A BLOK NO:12/A-A OYAK SÝTESÝ KARÞISI"},
new Eczane { Id =243,Enlem=36.88066,Boylam=30.7357,Adres = "MEYDAN KAVAGI MH.1610 SK.TURUNCU SIT.12.CD.ISMAIL CEM CD. GÜLGEN MARKET KARSISI"},
new Eczane { Id =258,Enlem=36.86305,Boylam=30.73026,Adres = "SIRINYALI MAH. ÝSMET GÖKÞENCAD.1487 SK.305/2 YASAM HASTANESI YANI"},
new Eczane { Id =255,Enlem=36.86947,Boylam=30.73533,Adres = "KIRCAMI MAH. SINANOGLU CAD. 7/B E BEBEK ÝLE ÖZLEM PASTANESÝ ARASI, DÝLARA YAÞAR KARÞISI"},
new Eczane { Id =249,Enlem=36.87646,Boylam=30.71787,Adres = "B. ONAT CD. BERNA DEDEMAN SAG. MRK. SONRA A101 YANI"},
new Eczane { Id =236,Enlem=36.87028,Boylam=30.72017,Adres = "YEÞÝLBAHÇE MAH. METÝN KASAPOÐLU CAD. NO:27/C SULTANYAR KARÞISI"},
new Eczane { Id =222,Enlem=36.86256,Boylam=30.73035,Adres = "SIRINYALI MAH. ISMET GÖKSEN CD. 15/B YASAM HASTANESI CIVARI MC DONALDS KARSISI"},
new Eczane { Id =292,Enlem=36.85609,Boylam=30.74723,Adres = "FENER MAH. TEKELÝOÐLU CAD. SEDA APT. NO:11/A MURATPAÞA MEDÝKALPARK HASTANESÝ YANI BOZKAN PETROL KARÞISI"},
new Eczane { Id =309,Enlem=36.85773,Boylam=30.79332,Adres = "GÜZELOBA MAH.HAVAALANI CAD.NO:27 / A GÜZELOBA PAZARI KAVSAGI A101 MARKET YANI"},
new Eczane { Id =263,Enlem=36.8581,Boylam=30.79282,Adres = "GÜZELOBA MH.HAVAALANI CAD. ÖZEL LARA ANADOLU HASTANESÝ KARSISI NO:33/A"},
new Eczane { Id =220,Enlem=36.85708,Boylam=30.79016,Adres = "GÜZELOBA MAH.2118 SOK.GÜZELOBA PTT KARÞISINDA BULUNAN ARÇELÝKTEN DENÝZE ÝNEN SOKAÐA ÝNÝNCE SOLDAN 4. DÜKKAN MURATPAÞA"},
new Eczane { Id =296,Enlem=36.85869,Boylam=30.79431,Adres = "GÜZELOBA MH. 2239 SK.LARA ANADOLU HAST.ACÝL GÝRÝÞ YANI CUMA PAZARI KARÞISI"},
new Eczane { Id =286,Enlem=36.85009,Boylam=30.76349,Adres = "ÇAGLAYAN MAH. BARINAKLAR BULV.2000 SOKAK MIGROS ARKASI 28 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =293,Enlem=36.85825,Boylam=30.74394,Adres = "ÞÝRÝNYALI MAH. ÝSMET GÖKÞEN CAD. 118 A RUMELÝ ÝÞKEMBECÝSÝ YANI"},
new Eczane { Id =299,Enlem=36.8518,Boylam=30.76371,Adres = "BARINAKLAR BULV.GÜZELOBA YÖNÜNE TERRA CITY VE BEYAZ DÜNYA ILERISI DENIZBANK KARSISI FENER KAVS. 200 MT ILERISI"},
new Eczane { Id =268,Enlem=36.85167,Boylam=30.75853,Adres = "FENER MH.TEKELIOGLU CAD.ASTUR SIT.NO:90/B TERRACITY AVM KARS. MURATPASA BELEDIYESI KARSISI"},
new Eczane { Id =301,Enlem=36.85644,Boylam=30.74635,Adres = "SIRINYALI MAHALLESI TEKELIOGLU CADDESI MEDICAL PARK. HAST. KARSISI ( SHEMALL HASTANE KAPISI KARÞISI)"},
new Eczane { Id =267,Enlem=36.85762,Boylam=30.7705,Adres = "ÇAÐLAYAN MAH. BÜLENT ECEVIT BULVARI BANIO ILERISI PEKÝYÝ FIRIN ÇAPRAZI"},
new Eczane { Id =273,Enlem=36.85524,Boylam=30.77151,Adres = "ÇAÐLAYAN MAH. BARINAKLAR BULV. 2033 SOK. NO:10/A KAPALI PAZAR PAZARI ÇAPRAZI LARA ASM YANI MURATPAÞA"},
new Eczane { Id =184,Enlem=36.85668,Boylam=30.7897,Adres = "BARINAKLAR BULV.GÜZELOBA MAH.2118 SOK. NO:10/A GÜZELOBA PTT ÇAPRAZI KARÞISI ARÇELÝK BAYÝ SOKAÐI MURATPAÞA"},
new Eczane { Id =304,Enlem=36.85647,Boylam=30.74631,Adres = "SIRINYALI MAHALLESI TEKELIOGLU CADDESI MEDICALPARK HASTANESI KARSISI"},
new Eczane { Id =283,Enlem=36.85868,Boylam=30.79463,Adres = "GÜZELOBA MAH.2246 SK.ÖZEL LARA ANADOLU HASTANESI YAN KAPI KARSISI"},
new Eczane { Id =276,Enlem=36.86024,Boylam=30.78026,Adres = "ÇAGLAYAN MH. BÜLENT ECEVIT BULV.CELAL SAHIN SIT. C BLOK NO:166/B GELECEK TÜP BEBEK MERKEZI KARSISI"},
new Eczane { Id =307,Enlem=36.85436,Boylam=30.77717,Adres = "BARINAKLAR BULV.ÇAGLAYAN OPETI GEÇINCE YÖRÜKOGLU ALYA MARKET KARSISI"},
new Eczane { Id =270,Enlem=36.85827,Boylam=30.74535,Adres = "SIRINYALI MAH.ISMET GÖKSEN CAD.LAURA AVM KAVSAGI VESTEL VE HAYVAN HASTANESÝ YANI"},
new Eczane { Id =278,Enlem=36.85842,Boylam=30.74651,Adres = "LAURA ALISVERIS MERKEZI GIRIS KAPISI KARS.RÜYAMKENT1 SIT BÜLENT ECEVIT BULVARI"},
new Eczane { Id =272,Enlem=36.85175,Boylam=30.76882,Adres = "ÇAGLAYAN MAH.BARINAKLAR BULV.YÖRÜK APT. 47 / B YAPIKREDI BANKASI KARSISI"},
new Eczane { Id =291,Enlem=36.86184,Boylam=30.79355,Adres = "GÜZELOBA MAH. RAUF DENKTAÞ CAD. NO:18 /B KÝPA KAVÞAÐININ 200 MT ÝLERÝSÝ LARA UNLU MAMÜLLERÝ YANI"},
new Eczane { Id =285,Enlem=36.858,Boylam=30.74555,Adres = "SIRINYALI MAH.TEKELIOGLU CAD.PEHLIVAN APT NO:2/A MURATPASA LAURA AVM KARSISI BOLULU HASAN USTA YANI"},
new Eczane { Id =264,Enlem=36.85868,Boylam=30.7416,Adres = "SIRINYALI MAH. ISMET GÖKSEN CAD. ATLAS APT.97/1 SIRINYALI OCAKBASI KARSI ÇAPRAZI EFE KUYUMCULUK KARSISI"},
new Eczane { Id =277,Enlem=36.8533,Boylam=30.7573,Adres = "FENER MAH. 1968 SOK. LEÞENOÐLU APT.NO:5/A TERRACÝTY ARKASI TÝCARET BORSASI SEMT POLÝKÝLÝNÝÐÝ KARÞISI MURATPAÞA"},
new Eczane { Id =302,Enlem=36.85329,Boylam=30.75777,Adres = "TERRACITY AVM ARKA SOKAGI, BORSA SEMT POLIKLINIGI KARSISI FENER MAH. 1968.SOK 7/1 MURATPASA"},
new Eczane { Id =300,Enlem=36.85466,Boylam=30.79356,Adres = "KARPUZ KALDIRAN KAMPI KARSISI GÜZELOBA MAHALLESI 2121 SOKAK 15/B"},
new Eczane { Id =282,Enlem=36.85148,Boylam=30.76206,Adres = "ÇAGLAYAN MAH.BARINAKLAR BULV. NO:2/B BEYAZ DÜNYA AVM.KARS. BURSA MEFRUSAT YANI"},
new Eczane { Id =275,Enlem=36.86204,Boylam=30.76467,Adres = "ZÜMRÜTOVA MAH. YALI CAD. PÝDEX KARÞISI MAKRO MARKET ARKASI"},
new Eczane { Id =308,Enlem=36.84967,Boylam=30.80314,Adres = "ÖRNEKKÖY ÇARSISI NO:6 ÖRNEKKÖY HALISAHA VE MESCIDI YANI LARA - ÖRNEKKÖY LARA - ÖRNEKKÖY"},
new Eczane { Id =290,Enlem=36.85319,Boylam=30.75822,Adres = "FENER MH. BORSA SEMT POLIK. KARS. 1968 SK. NO:9/1 TERRACITY AVM ARKASI"},
new Eczane { Id =274,Enlem=36.85821,Boylam=30.79262,Adres = "GÜZELOBA MAHALLESI GÜZELOBA PAZARI VE LARA ANADOLU HASTANESI KARS. CARREFOUR- SA MARKET YANI"},
new Eczane { Id =303,Enlem=36.85632,Boylam=30.7464,Adres = "SIRINYALI MAH. TEKELIOGLU CAD. IZCI APT. NO:20/1 MEDICALPARK HASTANESI KARSISI"},
new Eczane { Id =289,Enlem=36.85781,Boylam=30.75657,Adres = "FENER MAH. BÜLENT ECEVIT BULVARI 1981 SOK.NO:1 LAURA ILE MAKRO MARKET ARASI KARATAÞ EKMEK FIRINI KARÞISI"},
new Eczane { Id =295,Enlem=36.85854,Boylam=30.76363,Adres = "ÇAÐLAYAN MAH.BÜLENT ECEVÝT BULVARI FENER CAD.(YALI CADDESÝ YANI MAKRO MARKET ARKASI PÝDEX YAKINI)"},
new Eczane { Id =287,Enlem=36.85521,Boylam=30.77144,Adres = "ÇAÐLAYAN MAH. 2033 SOK. NO:12/ B BARINAKLAR MEZARLIÐI ÝLE PEKÝYÝ EKMEK FIRINI ARASI ÝBRAHÝM ATEÞ CAMÝÝ KARÞISI"},
new Eczane { Id =298,Enlem=36.85855,Boylam=30.79392,Adres = "GÜZELOBA MAH. ÖZEL LARA ANADOLU HASTANESI YANI"},
new Eczane { Id =306,Enlem=36.85791,Boylam=30.75129,Adres = "BÜLENT ECEVIT BULV.ÖZGÜL KENT SIT.A BLOK NO:22 LAURA AVM 300 MT. ÝLERÝSÝ"},
new Eczane { Id =266,Enlem=36.85438,Boylam=30.79478,Adres = "GÜZELOBA MAH.NO:517 KARPUZKALDIRAN KAMPI KARSISI LARA"},
new Eczane { Id =280,Enlem=36.85609,Boylam=30.74723,Adres = "FENER MAH. TEKELIOGLU CAD. NO:9/A MURATPAÞA MEDICAL PARK HASTANESI YANI"},
new Eczane { Id =499,Enlem=36.85183,Boylam=30.7647,Adres = "BARINAKLAR BULV. GÜZELOBA YÖNÜNE TERRACÝTY VE BEYAZ DÜNYA ÝLERÝSÝ DENÝZBANK KARÞISI FENER KAVÞAÐI 200 MT ÝLERÝSÝ MURATPAÞA"},
new Eczane { Id =271,Enlem=36.85365,Boylam=30.75552,Adres = "FENER MAH.BORSA SEMT POLK. BATISI TERRA CITY AVM ARKASI SEDIR RESTAURANT YANI"},
new Eczane { Id =281,Enlem=36.8495,Boylam=30.80682,Adres = "GÜZELOBA MAH. SERA OTELI KARSISI LARA CADDESI ÖRNEKKÖY"},
new Eczane { Id =269,Enlem=36.85613,Boylam=30.74669,Adres = "SIRINYALI MAH.TEKELIOGLU CAD.NO:24 / 1 MEDICALPARK HASTANESI KARSISI"},
new Eczane { Id =279,Enlem=36.85179,Boylam=30.76409,Adres = "BARINAKLAR BULV.TERRA CITY ILERISI DENIZ BANK ÇAPRAZ KARÞISI VE BEYAZ DÜNYA ILERISI"},
new Eczane { Id =305,Enlem=36.85019,Boylam=30.76347,Adres = "BARINAKLAR BULV.2000 SOK. FENER MÝGROS ARKASI 28 NOLU AHMET ATMACA ASM KARÞISI"},
new Eczane { Id =288,Enlem=36.859,Boylam=30.77648,Adres = "BÜLENT ECEVIT BULVARI OPET YANI"},
new Eczane { Id =284,Enlem=36.85857,Boylam=30.73914,Adres = "SIRINYALI MAH. ISMET GÖKSEN CAD. SEMIHA KURT APT. 78/A LARA MEMORIAL HAST. YANI"},
new Eczane { Id =262,Enlem=36.85102,Boylam=30.80522,Adres = "ÖRNEKKÖY MÝGROS KARSISI LARA PLAJLARINA INISTE CLUB OTEL SERA YANI SGK ILE ANLASMASI YOKTUR"},
new Eczane { Id =265,Enlem=36.85799,Boylam=30.79026,Adres = "GÜZELOBA MAH. BARINAKLAR BULVARI CAD. NO:230 GÜZELOBA CAMII KARSISI"},
new Eczane { Id =297,Enlem=36.86045,Boylam=30.7409,Adres = "ÞÝRÝNYALI MAH. 1533 SOK. NO:25/B DEMOKRASÝ ÞEHÝTLERÝ ASM YANI RAMAZAN SAVAÞ ÝLKOKULU KARÞISI MURATPAÞA"},
new Eczane { Id =333,Enlem=36.88859,Boylam=30.62883,Adres = "UNCALI 2M MIGROS DOGUSU 35.CD.ÜZERI DEFNE 1 KONUTLARI 16/3 KONYAALTI"},
new Eczane { Id =317,Enlem=36.90114,Boylam=30.66076,Adres = "KÜLTÜR MAH.TIP FAKÜLTESI OTOPARK ÇIKISI KARSISI YENI ÇAKIRLAR YOLU"},
new Eczane { Id =326,Enlem=36.89198,Boylam=30.62616,Adres = "UNCALI SEMT POLIKLINIGININ YAN TARAFI ÇEVIK KUVVET KARSISI"},
new Eczane { Id =337,Enlem=36.901,Boylam=30.66037,Adres = "TIP FAKÜLTESI ARABA ÇIKISI KARSISI KÜLTÜR MAH.12/2 ILLER BANKASI ILERISI"},
new Eczane { Id =314,Enlem=36.88745,Boylam=30.63366,Adres = "UNCALI MAH.ÜNÝVERSÝTE CAD. NO:16/CA ATLANTÝK TAKSÝ KARÞISI KONYAALTI"},
new Eczane { Id =311,Enlem=36.89311,Boylam=30.62129,Adres = "MOLLA YUSUF MAH.1425 SK. UNCALI MEYDAN HASTANESI YANI"},
new Eczane { Id =327,Enlem=36.9099,Boylam=30.6456,Adres = "AKDENIZ SAN.SIT.YOLU ÜZERI AHATLI 17 NO'LU SAÐLIK OCAGI KARS.TURGUT REIS CAMII KARSISI"},
new Eczane { Id =336,Enlem=36.90111,Boylam=30.6603,Adres = "KÜLTÜR MAH.TIP FAKÜLTESI OTOPARK ÇIKISI KARSISI YENI ÇAKIRLAR YOLU"},
new Eczane { Id =312,Enlem=36.90117,Boylam=30.66116,Adres = "KÜLTÜR MAH.TIP FAKÜLTESI OTOPARK ÇIKISI KARSISI YENI ÇAKIRLAR YOLU"},
new Eczane { Id =324,Enlem=36.8929,Boylam=30.62152,Adres = "MOLLA YUSUF MAH.1425 SK.ÖZEL UNCALI MEYDAN HASTANESI YANI HACI SADIYE AY APT.7/5 KONYAALTI"},
new Eczane { Id =313,Enlem=36.90382,Boylam=30.66179,Adres = "KÜLTÜR MAH. YENI ÖGRETMENEVI ARKASI KÜLTÜR SAGLIK OCAGI YANI"},
new Eczane { Id =315,Enlem=36.90115,Boylam=30.66084,Adres = "AKDENIZ ÜNI.TIP FAKÜLTESI KUZEYI OTOPARK ÇIKISI KARSISI YENI ÇAKIRLAR YOLU ÜZERI"},
new Eczane { Id =318,Enlem=36.90365,Boylam=30.66205,Adres = "KÜLTÜR MH. YENI ÖGRETMENEVI ARK. KÜLTÜR SAGLIK OCAGI KARS."},
new Eczane { Id =334,Enlem=36.89273,Boylam=30.62145,Adres = "MOLLA YUSUF MH. 1425 SK.UNCALI MEYDAN HAST.YANI"},
new Eczane { Id =329,Enlem=36.90862,Boylam=30.65562,Adres = "AHATLI MAH. 3128 SOK. KAÇMAZ APT. NO:3/A ULUSOY CAD. AHATLI FIRINI ARKASI"},
new Eczane { Id =331,Enlem=36.90953,Boylam=30.64643,Adres = "YESILYURT MAH.4301 SOK NO:35/A KEPEZ (Akdeniz Sanayi yolu üzeri, Ahatli Saglik Ocagi karsisi)"},
new Eczane { Id =310,Enlem=36.89315,Boylam=30.6215,Adres = "MOLLA YUSUF MH.1425 SK.ÖZEL UNCALI MEYDAN HASTANESI YANI KONYAALTI"},
new Eczane { Id =322,Enlem=36.88856,Boylam=30.62944,Adres = "UNCALI 2M MIGROS DOGUSU 35.CD.ÜZERI DEFNE 1 KONUTLARI 16/AB KONYAALTI"},
new Eczane { Id =323,Enlem=36.90116,Boylam=30.66098,Adres = "KÜLTÜR MH.TIP FAK.HAST.OTO ÇIKIS KARS.ILLER BANK.SIRASI ÇAKIRLAR YOLU"},
new Eczane { Id =320,Enlem=36.89527,Boylam=30.63398,Adres = "SITELER MAH. 1327 SOKAK SITELER KAPALI PAZAR YERI YAKINI ÇILEM APT NO:28/D MAVITEK SITESI YANI"},
new Eczane { Id =338,Enlem=36.90923,Boylam=30.64747,Adres = "75.YIL CAD.YENI SANAYI YOLU IS BANKASI KARSISI YESILYURT / KEPEZ / ANTALYA"},
new Eczane { Id =332,Enlem=36.88823,Boylam=30.62821,Adres = "UNCALI 2 M MIGROS DOGUSU 35. CAD.SUN CITY KONUTLARI"},
new Eczane { Id =321,Enlem=36.91002,Boylam=30.6457,Adres = "AKDENÝZ SAN.SÝT. YOLU ÜZERÝ AHATLI 17 NOLU SAÐLIK OCAÐI KARÞISI TURGUT REÝS CAMÝÝ KARÞISI"},
new Eczane { Id =325,Enlem=36.90355,Boylam=30.66206,Adres = "KÜLTÜR MAH.3806 SK. 26/6 ILLER BANKASI ARKASI 18 NOLU AÝLE SAÐLIÐI MERKEZÝ KARÞISI"},
new Eczane { Id =330,Enlem=36.91574,Boylam=30.66367,Adres = "YENIDOGAN MAH. ELMALI SITESI SÖFÖRLER ODASI YANI PAZARYERI KARSISI"},
new Eczane { Id =319,Enlem=36.89191,Boylam=30.6261,Adres = "UNCALI SEMT POLIKLINIGININ YANI ÇEVIK KUVVET KARSISI"},
new Eczane { Id =335,Enlem=36.90987,Boylam=30.64634,Adres = "AKDENIZ SAN.SIT.YOLU ÜZERI AHATLI SAGLIK OCAGI KARS."},
new Eczane { Id =366,Enlem=36.91254,Boylam=30.64322,Adres = "ÞAFAK MAH. 4256 SOK. NO:24/8 KEPEZ ELEGANT PLAZA ARKASI BÝM KARÞISI"},
new Eczane { Id =328,Enlem=36.90966,Boylam=30.64717,Adres = "AHATLI MAH. (AKDENIZ SANAYI SITESI YOLU ÜZERI) AHATLI SAGLIK OCAGI KARSISI"},
new Eczane { Id =316,Enlem=36.90924,Boylam=30.65082,Adres = "AHATLI MAH. ULUSOY CAD.NO:70/C"},
new Eczane { Id =368,Enlem=36.88287,Boylam=30.66046,Adres = "5 M MIGROS ALISVERIS MERKEZI IÇI MELTEM MAH. 100.YIL BULVARI"},
new Eczane { Id =805,Enlem=36.87312,Boylam=30.64238,Adres = "KUÞKAVAÐI MAH.ATATÜRK BULVARI.83/A MEHMET KODAK APT. KONYALTI*AKDENÝZ ÞÝFA HASTANESÝ YANI .ANTALYA"},
new Eczane { Id =355,Enlem=36.86885,Boylam=30.63002,Adres = "GÜRSU MAH. GAZÝ MUSTAFA KEMAL BULV.NO:53/A KONYAALTI"},
new Eczane { Id =381,Enlem=36.85386,Boylam=30.61371,Adres = "LIMAN MAH.16 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =362,Enlem=36.87422,Boylam=30.62667,Adres = "GAZI MUSTAFA KEMAL BULV. DÝLEK APT.NO:2 SPORLAND YANI MIGROS JET KARSISI"},
new Eczane { Id =380,Enlem=36.85373,Boylam=30.6138,Adres = "LÝMAN MAH.BOÐAÇAYI CAD.NO:36/A KONYAALTI"},
new Eczane { Id =339,Enlem=36.87359,Boylam=30.64691,Adres = "ARAPSUYU MAH. 6.CD.BASEL OTEL YANI"},
new Eczane { Id =340,Enlem=36.85345,Boylam=30.61358,Adres = "LIMAN MAH.BOGAÇAYI CAD.32.SOK. YALÇIN APT. NO: 14/C KONYAALTI"},
new Eczane { Id =346,Enlem=36.87077,Boylam=30.63352,Adres = "ÖZEL OLIMPOS HASTANESI KARSISI ALTINKUM MH.460 SOKAK NO:18"},
new Eczane { Id =885,Enlem=36.85427,Boylam=30.61465,Adres = "LÝMAN MAH. 15. SOK. NO:5/B ALTINYAKA YOLU ÜZERÝ 16 NOLU SAÐLIK OCAÐI KARÞISI KONYAALTI"},
new Eczane { Id =375,Enlem=36.87044,Boylam=30.63382,Adres = "ALTINKUM MH.460 SK.NO: 20/B ÖZEL OLIMPOS HASTANESI KARSISI"},
new Eczane { Id =495,Enlem=36.88425,Boylam=30.64213,Adres = "TOROS MAH. 836 SOK. ÇETÝN APT. NO:7/1 KONYAALTI"},
new Eczane { Id =370,Enlem=36.87519,Boylam=30.64079,Adres = "ARAPSUYU CARREFOURSA ARKASI ÖGRETMENEVLERI MH.18.CD.NO:36/10 KONYAALTI"},
new Eczane { Id =807,Enlem=36.85981,Boylam=30.60507,Adres = "HURMA MAHALLESÝ.BOÐAÇAY CADDESÝ.NO:88/A KONYALTI/ANTALYA"},
new Eczane { Id =341,Enlem=36.87305,Boylam=30.65082,Adres = "ARAPSUYU MAH.BELEDÝYE CAD.( ÝL SAÐLIK MÜD.'DEN DENÝZ ÝSTÝKAMETÝNE DOÐRU YOL BÝTÝNCE SAÐA DÖNÜLECEK) KONYAALTI"},
new Eczane { Id =365,Enlem=36.85954,Boylam=30.60483,Adres = "HURMA MAH.BOGAÇAY CAD.NO:91/A-B(ESKI SEMAZEN) URFA SULTAN SOFRASI KARSISI"},
new Eczane { Id =361,Enlem=36.8836,Boylam=30.62989,Adres = "UNCALI MAKRO MARKET KARSISI ANTALYA PARK KONUTLARI BANIO YAPI MARKET YAKINI"},
new Eczane { Id =377,Enlem=36.87352,Boylam=30.64261,Adres = "AKDENIZ SIFA KONYAALTI TIP MERKEZI YANI ATATÜRK BULVARI NO 79"},
new Eczane { Id =353,Enlem=36.86434,Boylam=30.63458,Adres = "ALTINKUM MH.G.MUSTAFA KEMAL BULV.AVEA (ESKI TÜRKAY OTEL)KVS.50 METRE GÜNEYINDE SEALIFE OTEL KUZEYI"},
new Eczane { Id =359,Enlem=36.87249,Boylam=30.64093,Adres = "ÖGRETMENEVLERI MAH. 924 SK. AKIN APT. 4 / 3 CANET ARKASI KONYAALTI"},
new Eczane { Id =354,Enlem=36.88778,Boylam=30.64537,Adres = "TOROS MAH.ÜNÝVERSÝTE CAD. NO:54/A KONYAALTI ÜNÝVERSÝTENÝN GÜNEY ÇIKIÞ KAPISI"},
new Eczane { Id =364,Enlem=36.87076,Boylam=30.62802,Adres = "GÜRSU MAH.GAZI MUSTAFA KEMAL BULV.SPOARLAND KAVÞAÐINDAN DENÝZE DOÐRU 500 MT ÝLERÝDE ANA YOLDA SAÐDA"},
new Eczane { Id =367,Enlem=36.88248,Boylam=30.65563,Adres = "PINARBASI MAH. KAMAÇ APT. B BLOK NO:1 5M MIGROS KARSISI"},
new Eczane { Id =351,Enlem=36.86687,Boylam=30.63232,Adres = "ALTINKUM MAH.GAZÝ MUSTAFA KEMAL BULV.ESKÝ TÜRKAY OTELÝ (TÜRK TELEKOM) YUKARISI TÝVAK MARKET KARÞI ÇAPRAZI"},
new Eczane { Id =344,Enlem=36.87077,Boylam=30.63366,Adres = "ALTINKUM MAH.ÇAMLIK CAD.OLIMPOS HASTANESI KARSISI 100 METRE ILERISI"},
new Eczane { Id =360,Enlem=36.84904,Boylam=30.60634,Adres = "LIMAN MH.3. CD. (BILEYDILER CD) SARMAÞIK APT. NO:44/B"},
new Eczane { Id =349,Enlem=36.85927,Boylam=30.60519,Adres = "HURMA MAH. BOGAÇAY CAD. NO:87/A-B (ESKI SEMAZEN) URFA SULTAN SOFRASI KARSISI"},
new Eczane { Id =347,Enlem=36.85354,Boylam=30.61343,Adres = "LIMAN MAH. BOGAÇAYI CAD. 16 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =343,Enlem=36.88724,Boylam=30.64726,Adres = "PINARBASI MH.14.CD.SONU ÜNÝVERSÝTE STAD GÝRÝÞ KAPISI KARÞISI BÝM VE A101 MARKET KARÞISI"},
new Eczane { Id =345,Enlem=36.87598,Boylam=30.64375,Adres = "ATATÜRK BULVARI IL SAGLIK MÜD. GEÇINCE KONYAALTI SEDÝR RESTAURANT YANI"},
new Eczane { Id =357,Enlem=36.8778,Boylam=30.65137,Adres = "ARAPSUYU MAH. ARAPSUYU CAD. ÝL SAÐLIK MÜDÜRLÜÐÜNDEN SAHÝLE DOÐRU A101 MARKET YAKINI"},
new Eczane { Id =376,Enlem=36.84981,Boylam=30.61409,Adres = "LÝMAN MAH. ATATÜRK BULVARI CAD. NO:230/A KONYAALTI"},
new Eczane { Id =350,Enlem=36.85396,Boylam=30.61941,Adres = "LIMAN MAH.YENI BOGAÇAYI KÖPRÜSÜ BASI"},
new Eczane { Id =363,Enlem=36.87475,Boylam=30.64186,Adres = "ÖGRETMENLER MAH. ÖGRETMENLER CAD.NO: 3/B KASAP SEREF YANI VATAN COMPUTER KARISISI"},
new Eczane { Id =348,Enlem=36.86543,Boylam=30.63479,Adres = "ALTINKUM MH.ATATÜRK BULV.GARANTI BANKASI YANI ESKI TÜRKAY OTELI KARSISI"},
new Eczane { Id =342,Enlem=36.86919,Boylam=30.62833,Adres = "GÜRSU MAH ANADOLU CAD.NO: 28/A ESKI TURKAY OTELDEN UNCALIYA ÇIKARKEN CARREFOURSA ARKASI SOK MARKET KARSISI"},
new Eczane { Id =369,Enlem=36.8479,Boylam=30.60752,Adres = "LIMAN MAH.45.SOK.BELLONA VE SEZERLER PETROL ARKASI HURMA AILE SAGLIGI MERKEZI KARSISI"},
new Eczane { Id =374,Enlem=36.87101,Boylam=30.63332,Adres = "ALTINKUM MAH 460 SOKAK AYFER APT 27/B OLÝMPOS HASTANESÝ KARÞISI"},
new Eczane { Id =356,Enlem=36.87225,Boylam=30.63262,Adres = "ALTINKUM MAH.OLIMPOS HASTANESI YUKARISI ( 50 MT ) ÇAGLARSOY MARKET KARSISI KONYAALTI"},
new Eczane { Id =358,Enlem=36.85393,Boylam=30.61615,Adres = "LÝMAN MAH. 20.SOK. DERYA APT. NO:13/1 KONYAALTI"},
new Eczane { Id =373,Enlem=36.84819,Boylam=30.60686,Adres = "LÝMAN MAH. . 33.SOKAK HURMA ASM YANI BELLONA OKYANUS KOLEJÝ ARKASI"},
new Eczane { Id =378,Enlem=36.88057,Boylam=30.64004,Adres = "AKKUYU MH. 20. CD. 114/A KONYAALTI NASHIRA PARK ILE ÜNIVERSITE ARASI"},
new Eczane { Id =371,Enlem=36.88724,Boylam=30.64672,Adres = "TOROS MAH. PINARBAÞI CAD. SEVÝLAY APT. NO:31/A AKDENÝZ ÜNÝVERSÝTESÝ GÜNEY KAPISI ( ESKÝ STADYUM GÝRÝÞÝ ) YANI"},
new Eczane { Id =372,Enlem=36.87145,Boylam=30.63286,Adres = "ALTINKUM MAH.460 SOKAK ÇALIKUSU HUZUREVI ARKASI ÖZEL OLIMPOS HASTANESI KARSISI"},
new Eczane { Id =379,Enlem=36.87071,Boylam=30.62788,Adres = "GÜRSU MAH.GAZI MUSTAFA KEMAL BULVARI A 101 MARKET KARSISI 16 NOLU SAGLIK OCAGI BIRIMI KARSISI"},
new Eczane { Id =394,Enlem=36.90791,Boylam=30.67422,Adres = "FABRIKALAR MAH. FIKRI ERTEN CAD. BANDOCULAR YOLU KIPA KARSISI"},
new Eczane { Id =392,Enlem=36.9172,Boylam=30.69378,Adres = "ZAFER MH. YILDIRIM BEYAZIT CD. 2615 SK. DÜNYA GÖZ HAST.YANI MEMORÝAL HAST.ACÝL KARÞISI KEPEZ"},
new Eczane { Id =407,Enlem=36.92562,Boylam=30.68706,Adres = "BARIÞ MH.2906 SOK. HALÝL EFENDÝ APT. 24 NOLU BARIÞ SAÐLIK OCAÐI ÇAPRAZ KARÞISI"},
new Eczane { Id =388,Enlem=36.5515,Boylam=30.5513,Adres = "KANAL MAH.NAMIK KEMAL CAD.5.ÜNAL APT. NO:136/A KEPEZ VÝTALE HASTANESÝ ANTALYA TÜP BEBEK MERKEZÝ YANI"},
new Eczane { Id =403,Enlem=36.91335,Boylam=30.68818,Adres = "MITHATPASA CAD.NO:58/A OFM HASTANESINDEN ÖZDILEKE DOGRU 200 MT ILERDE TRAFIK LAMBALARININ KÖSESINDE"},
new Eczane { Id =384,Enlem=36.91714,Boylam=30.68544,Adres = "BARIS TAKSI KARSISI MITHAT PASA CADDESI NO: 118/A DOKUMA-KEPEZ"},
new Eczane { Id =408,Enlem=36.90915,Boylam=30.68361,Adres = "ULUS MH.RASIH KAPLAN CD.EVREN BÜFE ASAGISI MEPAS KARSISI DOKUMA"},
new Eczane { Id =387,Enlem=36.914,Boylam=30.683,Adres = "ÖZGÜRLÜK MAH. 2666 SOKAK KAPALI HALK PAZARI YANI DOKUMA"},
new Eczane { Id =386,Enlem=36.91051,Boylam=30.67719,Adres = "ÖZDILEK PARK AVM IÇI DOKUMA MEVKII"},
new Eczane { Id =410,Enlem=36.90847,Boylam=30.68365,Adres = "ÇALLI KAVSAGI DOKUMA ISTIKAMETI 50 MT SAGINDA 14 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =496,Enlem=36.90905,Boylam=30.68354,Adres = "ULUS MAH. RASÝH KAPLAN CAD. EVREN BÜFE AÞAÐISI MEPAÞ KARÞISI - DOKUMA"},
new Eczane { Id =391,Enlem=36.91518,Boylam=30.69206,Adres = "ZAFER MAH. MEHMET AKIF CAD. NO:113/2 KEPEZ - OFM HASTANESI KARSI ÇAPRAZI"},
new Eczane { Id =401,Enlem=36.91475,Boylam=30.69101,Adres = "ZAFER MH.MEHMET AKIF CD.CUMARTESI PAZARI YANI DOKUMA OFM HASTANESI KARSISI"},
new Eczane { Id =393,Enlem=36.9162,Boylam=30.67746,Adres = "YESILTEPE MH. ZIYA GÖKALP CAD.ÖZKAVAK OTELI YANI SAGLIK BIRIMI YANI - DOKUMA"},
new Eczane { Id =397,Enlem=36.91718,Boylam=30.69432,Adres = "ZAFER MAH.YILDIRIM BEYAZIT CAD. NO: 8/1 MEMORÝAL HASTANESÝ VE DÜNYA GÖZ HASTANESI YANI"},
new Eczane { Id =390,Enlem=36.91794,Boylam=30.69449,Adres = "KARSIYAKA MAH. YILDIRIM BEYAZIT CAD. NO:88/7 KEPEZ - DÜNYAGÖZ VE MEMORIAL HASTANELERI KARSISI"},
new Eczane { Id =409,Enlem=36.91847,Boylam=30.67563,Adres = "PIL FABRIKASI KARSISI ANTALYA TÜP BEBEK MERKEZI YAKINI SUISLERI YOLU(ORÜS KARS.)DOKUMA"},
new Eczane { Id =399,Enlem=36.91672,Boylam=30.69452,Adres = "ZAFER MAH.YILDIRIM BEYAZIT CAD.KARDIÇ APT. DÜNYA GÖZ HASTANESI YANI"},
new Eczane { Id =402,Enlem=36.90704,Boylam=30.67204,Adres = "FABRÝKALAR MAH. 3024 SOK. FÝKRÝ ERTEN CAD.NO:10-B/5 KEPEZ ÖZDÝLEK KÝPA 150 MT. ÝLERÝSÝ"},
new Eczane { Id =405,Enlem=36.91803,Boylam=30.6754,Adres = "PIL FABRIKASI KARSISI ANTALYA TÜP BEBEKMERKEZI YANI SUISLERI YOLU(ORÜS KARS.)DOKUMA"},
new Eczane { Id =396,Enlem=36.9165,Boylam=30.68556,Adres = "MITHATPASA CAD.BARIS TAKSI ASAGISI NO: 109/B ESKI DOKUMA AN-DEVA HASTANESI BITISIGI"},
new Eczane { Id =389,Enlem=36.9162,Boylam=30.67746,Adres = "YEÞÝLTEPE MAH. ZÝYA GÖKALP CAD. NO:24/B KEPEZ - ÖZKAVAK OTELI YANI ASM YANI - DOKUMA"},
new Eczane { Id =383,Enlem=36.9147,Boylam=30.69094,Adres = "ZAFER MAH.MEHMET AKIF CAD.CUMARTESI PAZARI YANI OFM HASTANESI KARSISI"},
new Eczane { Id =406,Enlem=36.90704,Boylam=30.67204,Adres = "FABRÝKALAR MAH.3022 SOK. NO:4-A/7 ÖZDÝLEK-KÝPA AVM ARKASI ÞÖHRETLER HALISAHA KARÞISI 07STAR DÜÐÜN SALONU YANI KEPEZ"},
new Eczane { Id =395,Enlem=36.91911,Boylam=30.68751,Adres = "SAKARYA BULV.DÜNYA GÖZ HASTANESINDEN OTOGAR YÖNÜNE 200 MT.ILERI ANTKOOP KARS.DOKUMA"},
new Eczane { Id =385,Enlem=36.91434,Boylam=30.69169,Adres = "YÜKSELIS MAH.2121 SOKAK OFM HAST.ACIL ÇIKISI KARSISI CUMARTESI PAZARI KARS. DOKUMA"},
new Eczane { Id =398,Enlem=36.91469,Boylam=30.69085,Adres = "ZAFER MAH.MEHMET AKIF CAD.CUMARTESI PAZARI YANI DOKUMA OFM HASTANESI KARSISI"},
new Eczane { Id =411,Enlem=36.90832,Boylam=30.68392,Adres = "ÇALLI KAVSAGI DOKUMA YÖNÜ YAPIKREDI BANKASI ARKASI 14 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =400,Enlem=36.91129,Boylam=30.68321,Adres = "ÖZGÜRLÜK MAH. M.AKIF CAD. ESKI CUMARTESI PAZARI CADDESI EVREN BÜFE YAKINI"},
new Eczane { Id =429,Enlem=36.92002,Boylam=30.78609,Adres = "DEEPO - MALL OF ANTALYA AVM ÝÇÝ HAVAALANI YOLU ÜZERÝ"},
new Eczane { Id =826,Enlem=36.9155,Boylam=30.77161,Adres = "ALTINOVA SÝNAN MAH.ANTALYA CADDESÝ.AGORA A.V.M.FAZ-2NO:14/23 KEPEZ"},
new Eczane { Id =424,Enlem=36.91718,Boylam=30.7016,Adres = "KARÞIYAKA MAH.3933 SOK.SAKARYA BULVARI ERDEM BEYAZIT KÜLTÜR MERKEZÝ KARÞISI LAÇÝN FIRIN ARKASI KEPEZ"},
new Eczane { Id =417,Enlem=36.91755,Boylam=30.70905,Adres = "EMEK MAH.2184 SK.I ÞEHÝTLER PARKI AÞAÐISI 34 NOLU SAGLIK OCAGI KARÞISI"},
new Eczane { Id =434,Enlem=36.91388,Boylam=30.72037,Adres = "TEOMANPASA MH.GAZI BULV.SEMA YAZAR DEVLET HAST.YANI"},
new Eczane { Id =430,Enlem=36.91388,Boylam=30.72058,Adres = "ÇEVREYOLU GAZI BULVARI SEMA YAZAR POLIKLINIGI YANI NO:327"},
new Eczane { Id =426,Enlem=36.9188,Boylam=30.73267,Adres = "GÜNES MAH. 6025 SOK. EKIN SIT. A3 BLOK NO:1( DEMIRGÜL MÝGROS YANI (ESKI MAKRO )"},
new Eczane { Id =435,Enlem=36.9193,Boylam=30.7324,Adres = "GÜNES MAH. 6022 SOK. SINEM SITESI DEMIRGÜL MAKRO MARKET(ESKÝ GENPA) YANI 7 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =425,Enlem=36.92264,Boylam=30.70243,Adres = "YENI MH.1 NOLU ANA ÇOCUK SAGLIGI MERK.KEPEZ BELEDIYE FIRINI PAZAR PAZARI KARSISI"},
new Eczane { Id =428,Enlem=36.91387,Boylam=30.71988,Adres = "SEMA YAZAR HASTANESI YANI GAZI BULVARI NO:319"},
new Eczane { Id =414,Enlem=36.91753,Boylam=30.70868,Adres = "EMEK MH.SAKARYA PARKI KARSISI 34 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =415,Enlem=36.91894,Boylam=30.72765,Adres = "GÜNES MH. GÜNES CD. NO:66 KEPEZ DEVLET HASTANESÝ 800 M GÜNEYÝ 15 KATLI BÝNA ÇAPRAZI"},
new Eczane { Id =420,Enlem=36.9222,Boylam=30.71114,Adres = "YENÝ MAH. ALÝYE ÝZZETBEGOVÝÇ CAD. NO:48/A KEPEZ MOBÝL ÝLKÖÐRETÝM OKULU ÇAPRAZI"},
new Eczane { Id =431,Enlem=36.91759,Boylam=30.73406,Adres = "DÜDENBASI MAH.2354 SOKAK GÜNESEVLER SIT. A BLOK NO:5 DEMIRGÜL GENPA KARSISI"},
new Eczane { Id =427,Enlem=36.91777,Boylam=30.70968,Adres = "EMEK MAH. SAKARYA PARKI KARSISI 34 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =422,Enlem=36.92313,Boylam=30.69723,Adres = "YENI EMEK MAH. 2566 SOKAK NO:71 6 NOLU SAGLIK OCAGI KARSISI KEPEZ MÜFTÜLÜÐÜ KUZEYÝNDEKÝ MODA DÜÐÜN SALONU ARKASI, CUMA PAZARI YANI"},
new Eczane { Id =438,Enlem=36.91756,Boylam=30.70936,Adres = "EMEK MAH.2184 SOKAK NO: 16 ÞEHÝTLER PARKI AÞAÐISI 34 NOLU SAÐLIK OCAÐI KARÞISI"},
new Eczane { Id =433,Enlem=36.91735,Boylam=30.71485,Adres = "EMEK MH.YESILIRMAK CD.41/2 SAKARYA PARKI KARSISI SELALE TIP MERKEZI YANI VAN GÖLÜ LOKANTASI KARÞISI"},
new Eczane { Id =416,Enlem=36.91973,Boylam=30.73353,Adres = "GÜNES MAH. EKIN SITESI DEMIRGÜL MÝGROS (ESKI GENPA) ARKASI 7 NOLU ASM YANI"},
new Eczane { Id =421,Enlem=36.91381,Boylam=30.70849,Adres = "EMEK MAH. 2179 SOK. 32/1 SAKARYA SAGLIK OCAGI YANI GENERAL SADI ÇETINKAYA OKULU KARSISI"},
new Eczane { Id =412,Enlem=36.91833,Boylam=30.72833,Adres = "GÜNES MAH.NECIP FAZIL KISAKÜREK TURAN APT. CAD.95/A ( TEOMANPASA CIGERCI EMIN USTA YANI )"},
new Eczane { Id =418,Enlem=36.9185,Boylam=30.72746,Adres = "TEOMANPAÞA MAH.GÜNEÞ CAD.CÝÐERCÝ EMÝN USTA ÇAPRAZI NO:65/A GÜNEÞ SAÐLIK OCAÐI KARÞISI"},
new Eczane { Id =413,Enlem=36.92301,Boylam=30.703,Adres = "YENI MH.1 NOLU ANA ÇOCUK SAGLIGI MERK.KEPEZ BELEDIYE FIRINI PAZAR PAZARI KARSISI (KARATAY LÝSESÝ BÖLGESÝ)"},
new Eczane { Id =419,Enlem=36.91775,Boylam=30.71604,Adres = "TEOMANPASA MAH.NECÝP FAZIL KISAKÜREK CAD. NO:12/B ÞELALE TIP MERKEZÝ KARÞISI TEOMANPAÞA GÝRÝÞÝ ÞOK MARKET KARÞISI"},
new Eczane { Id =423,Enlem=36.91391,Boylam=30.72069,Adres = "TEOMANPASA MAH.GAZI BULVARI SEMA YAZAR DEVLET HASTANESI YANI"},
new Eczane { Id =436,Enlem=36.91382,Boylam=30.71953,Adres = "GAZI BULV. TEOMANPASA MAH.SEMA YAZAR DEVLET HAST.YANI"},
new Eczane { Id =437,Enlem=36.91707,Boylam=30.72398,Adres = "TEOMANPAÞA MAH. 2230 SOK. NO:5 KEPEZ NECÝP FAZIL KISAKÜREK ASM YANI KAPALI CUMA PAZARI YANI"},
new Eczane { Id =481,Enlem=36.93155,Boylam=30.72391,Adres = "SÜTÇÜLER MAH.HASTANE CAD. NO:31/F KEPEZ DEVLET HASTANESÝ KARÞISI"},
new Eczane { Id =474,Enlem=36.93391,Boylam=30.72538,Adres = "SÜTÇÜLER MAH. GÜNEÞ CAD. 127-A - KEPEZ DEVLET HAST. KARÞISI"},
new Eczane { Id =467,Enlem=36.93327,Boylam=30.72523,Adres = "SÜTÇÜLER MAH. GÜNEÞ CAD. 125/A NO:7 KEPEZ DEVLET HASTANESÝ YANI"},
new Eczane { Id =460,Enlem=36.93367,Boylam=30.72594,Adres = "HÜSNÜ KARAKAÞ MAH.GÜNEÞ CAD. DERYAM APT. NO:126/B KEPEZ DEVLET HASTANESÝ YANI KEPEZ"},
new Eczane { Id =469,Enlem=36.92352,Boylam=30.71685,Adres = "MEHMET AKÝF ERSOY MAH. MEHMET ATAY CAD. 6758 SOK.NO:5/7 DÜDEN ASM YANI"},
new Eczane { Id =448,Enlem=36.93405,Boylam=30.70714,Adres = "KUZEYYAKA MAH.GÖKSU CAD.NO:93/A GOLD DÜÐÜN SARAYININ 500 METRE KUZEYÝ KUZEYYAKA SAÐLIK OCAÐI KARÞISI"},
new Eczane { Id =478,Enlem=36.95052,Boylam=30.71813,Adres = "HABIBLER MAH.5603 SOKAK NO:7 35 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =468,Enlem=36.98143,Boylam=30.71528,Adres = "VARSAK DEMÝREL MAH. SÜLEYMAN DEMÝREL BULVARI VARSAK SAÐLIK OCAÐI KARÞISI NO:224/A KEPEZ (MASAL PARKA GELMEDEN 300 MT GERÝDE)"},
new Eczane { Id =828,Enlem=36.92323,Boylam=30.71402,Adres = "MEHMET AKIF ERSOY MAH. YESILIRMAK CAD. NO:48 MOBÝL KAVSAÐI ESKÝ ANADOLU REKLAM KARÞI ÇAPRAZI 06 ASPAVA KOKOREÇ YANI"},
new Eczane { Id =475,Enlem=36.93362,Boylam=30.7258,Adres = "HÜSNÜ KARAKAÞ MAH. GÜNEÞ CAD. NO:126/F KEPEZ DEVLET HASTANESÝ YANI"},
new Eczane { Id =447,Enlem=36.9397,Boylam=30.72364,Adres = "HABIBLER MH.SÜTCÜLER CD.NO:8 11 NOLU ZEHRA KOZ SAGLIK OCAGI YANI"},
new Eczane { Id =809,Enlem=36.93111,Boylam=30.72603,Adres = "GÜNEÞ MAH.ÞEHÝT AST.ÖMER HALÝS DEMÝR CAD. NO:42/11 KEPEZ DEVLET HASTANESÝ YANI KEPEZ / ANTALYA"},
new Eczane { Id =446,Enlem=36.98122,Boylam=30.71413,Adres = "VARSAK KARÞIYAKA MAH. 1161 SOKAK NO:3-B ( VARSAK SAÐLIK OCAÐI / DÝÞ SAÐLIÐI MERKEZÝ YANI) - KEPEZ"},
new Eczane { Id =449,Enlem=36.93075,Boylam=30.72602,Adres = "SÜTÇÜLER MAH. GÜNEÞ CAD. NO:125/6 KEPEZ DEVLET HASTANESÝ KARÞISI"},
new Eczane { Id =454,Enlem=36.9309,Boylam=30.72548,Adres = "SÜTÇÜLER MAH.GÜNEÞ CAD.NO:121/F KEPEZ DEVLET HASTANESÝ GÝRÝÞÝ KARÞISI"},
new Eczane { Id =464,Enlem=36.93092,Boylam=30.72539,Adres = "SÜTÇÜLER MAH.GÜNES CAD. NO:121/D KEPEZ DEVLET HASTANESI ÇAPRAZI"},
new Eczane { Id =455,Enlem=36.98138,Boylam=30.71525,Adres = "SÜLEYMAN DEMIREL BULVARI VARSAK SAGLIK OCAGI KARSISI VARSAK"},
new Eczane { Id =461,Enlem=36.93059,Boylam=30.72744,Adres = "GÜNEÞ MAH.HASTANE CAD. ÝBRAHÝM ÇAVUÞ ÝÞ MERKEZÝ NO:52/2 KEPEZ DEVLET HASTANESÝ KARÞISI"},
new Eczane { Id =471,Enlem=36.94383,Boylam=30.70907,Adres = "KUZEYYAKA MH.YESILIRMAK CD.VARSAK KÖPRÜSÜ ALTI AKDENIZ SIFA HASTANESI YANI"},
new Eczane { Id =462,Enlem=36.93921,Boylam=30.72224,Adres = "HABIBLER MH.SÜTCÜLER CD.NO:75/B 11 NOLU ZEHRA KOZ SAGLIK OCAGI KARSISI"},
new Eczane { Id =472,Enlem=36.98076,Boylam=30.71454,Adres = "VARSAK KARSIYAKA MAH.1161 SOK. NO:3 KEPEZ - VARSAK DIS SAGLIGI MERKEZI YANI"},
new Eczane { Id =480,Enlem=36.94155,Boylam=30.70395,Adres = "FEVZI ÇAKMAK MAH.6168 SOKAK NO:20 AILE SAGLIGI MERKEZI KARSISI"},
new Eczane { Id =477,Enlem=36.96761,Boylam=30.72832,Adres = "SELALE MAH.21.CAD.KEPEZ DÜDEN SELALESI GIRISI ILERISI"},
new Eczane { Id =441,Enlem=36.94462,Boylam=30.70963,Adres = "GAZI MH. 6615 SK. AKDENIZ SIFA HASTANESI KARSISI LALEPARK EVLERI A3-BLOK NO:12 KEPEZ"},
new Eczane { Id =470,Enlem=36.94401,Boylam=30.70904,Adres = "KUZEYYAKA MH.YESILIRMAK CD.VARSAK KÖPRÜSÜ ALTI AKDENIZ SIFA HASTANESI YANI"},
new Eczane { Id =451,Enlem=36.97213,Boylam=30.70174,Adres = "VARSAK KARÞIYAKA MAH. TÜRKOÐLU CAD. VARSAK PÝDEDEN YUKARI 3. KAVÞAKTAN SOLA DÖNÜNCE 37 NOLU SAÐLIK OCAÐI KARÞISI"},
new Eczane { Id =445,Enlem=36.93044,Boylam=30.73165,Adres = "HÜSNÜ KARAKAÞ MAH. HASTANE CAD.NO:86/7-8 KEPEZ"},
new Eczane { Id =482,Enlem=36.93342,Boylam=30.72586,Adres = "HÜSNÜ KARAKAÞ MAH. GÜNEÞ CAD. NO:126/D KEPEZ DEVLET HASTANESÝ YANI"},
new Eczane { Id =352,Enlem=36.93092,Boylam=30.72619,Adres = "GÜNEÞ MAH. ÞHT. AST.ÖMER HALÝSDEMÝR CAD. 42/14 KEPEZ DEVLET HAST.YANI"},
new Eczane { Id =479,Enlem=36.92439,Boylam=30.72125,Adres = "MEHMET AKÝF ERSOY MAH .6752 SOK. NO: 22/B KEPEZ (MEHMET ATAY CAD.)15 KATLI BÝNADAN 500 MT BATIDA BÝM ARKASI"},
new Eczane { Id =456,Enlem=36.9331,Boylam=30.72622,Adres = "SÜTÇÜLER MAH.KEPEZ DEVLET HASTANESÝ KARÞISI KEPEZ"},
new Eczane { Id =465,Enlem=36.93524,Boylam=30.70743,Adres = "KUZEY YAKA MAH. GÖKSU CAD. NO:95/5 GOLD DÜGÜN SARAYININ 500 MT KUZEYI KUZEYYAKA ASM KARSISI"},
new Eczane { Id =453,Enlem=36.93972,Boylam=30.72257,Adres = "HABIBLER MH.SÜTCÜLER CD.5500 SK.NO:8/1 11 NOLU ZEHRA KOZ SAGLIK OCAGI KARSISI"},
new Eczane { Id =443,Enlem=36.93073,Boylam=30.72643,Adres = "GÜNEÞ MAH. HASTANE CAD. NO:42/12 KEPEZ KEPEZ DEVLET HASTANESÝ KARÞISI"},
new Eczane { Id =459,Enlem=36.93073,Boylam=30.72583,Adres = "GÜNEÞ MAH.HASTANE CAD.NO:42/13 KEPEZ DEVLET HASTANESÝ YANI"},
new Eczane { Id =458,Enlem=36.92824,Boylam=30.71472,Adres = "MEHMET AKIF ERSOY MAH. SÜTÇÜLER CAD. NO:16 DÜDEN SELALESI YOLU"},
new Eczane { Id =466,Enlem=36.93042,Boylam=30.73156,Adres = "HÜSNÜ KARAKAÞ MAH. HASTANE CAD. NO:86 D-10 KEPEZ SÜTÇÜLER ASM YANI"},
new Eczane { Id =473,Enlem=36.93852,Boylam=30.70518,Adres = "KUZEYYAKA MAH. 2546 SOK. NO:12/1-2 KEPEZ YEÞÝLIRMAK ASM YANI"},
new Eczane { Id =497,Enlem=36.97258,Boylam=30.70151,Adres = "VARSAK-KARÞIYAKA MAH. TÜRKOÐLU CAD. VARSAK TAHTAKALE'DEN YUKARI 37 NOLU ASM KARÞISI ( A101 VE ÞOK MARKET ARASINDA) NO:44B/1 KEPEZ"},
new Eczane { Id =476,Enlem=36.93056,Boylam=30.72835,Adres = "GÜNES MAH.HASTANE CAD.ÝBRAHÝM ÇAVUÞ ÝÞ MERKEZÝ NO:52/1 KEPEZ DEVLET HASTANESÝ KARÞISI"},
new Eczane { Id =452,Enlem=36.98133,Boylam=30.71433,Adres = "SÜLEYMAN DEMIREL BULVARI VARSAK SAGLIK OCAGI YANI VARSAK"},
new Eczane { Id =442,Enlem=36.9441,Boylam=30.70837,Adres = "KUZEYYAKA MAH. VARSAK KÖPRÜSÜ ALTI AKDENIZ SIFA HASTANESI ACIL ÇIKISI YANI"},
new Eczane { Id =439,Enlem=36.9813,Boylam=30.71524,Adres = "SÜLEYMAN DEMIREL BULV.VARSAK SAGLIK OCAGI KARSISI"},
new Eczane { Id =463,Enlem=36.93301,Boylam=30.72524,Adres = "SÜTÇÜLER MAH.GÜNEÞ CAD. NO:125/8 KEPEZ DEVLET HASTANESÝ KARÞISI"},
new Eczane { Id =440,Enlem=36.93919,Boylam=30.72384,Adres = "SÜTÇÜLER CAD. DÜDEN SELALESI YOLU ÜZERI 11 NOLU ZEHRA KOZ SAGLIK OCAGI YANI"},
new Eczane { Id =457,Enlem=36.98171,Boylam=30.71541,Adres = "DEMÝREL MAH. 14.CAD. NO:19/A MÝMAR SÝNAN AÝLE MERKEZÝ YANI KEPEZ VARSAK"},             


            	#endregion
            };

            var ekle = false;

            if (ekle)
            {
                foreach (var eczane in antalyaEnlemBoylamlar)
                {
                    var result = context.Eczaneler.SingleOrDefault(w => w.Id == eczane.Id);

                    result.Boylam = eczane.Boylam;
                    result.Enlem = eczane.Enlem;
                    result.TelefonNo = eczane.TelefonNo;
                    result.Adres = eczane.Adres;
                    result.AdresTarifiKisa = eczane.AdresTarifiKisa;
                    result.AdresTarifi = eczane.AdresTarifi;

                    context.SaveChanges();
                }
            }

            #endregion

            #region eczaneler - enlem, boylam ekle - mersin

            //foreach (var item in context.Eczaneler)
            //{
            //    item.NobetUstGrupId = 1;
            //}
            //context.SaveChanges();

            var mersinEnlemBoylamlar = new List<Eczane>()
            {
                #region Mersin enlem boylam

            	//new Eczane { Id = 803,
             //       Enlem = 36.7789507,
             //       Boylam = 34.5384343,
             //       TelefonNo = "3245023377",
             //       Adres = "ÇÝFTLÝKKÖY MH. 32133 SK. NO:6/B",
             //       AdresTarifiKisa = "ÇÝFTLÝKKÖY AÝLE SAÐLIÐI MERKEZÝ KARÞISI A101 YANI",
             //       AdresTarifi ="ÇÝFTLÝKKÖY AÝLE SAÐLIÐI MERKEZÝ KARÞISI A101 YANI"
             //   },

                new Eczane { Id = 608,Enlem = 36.8178860,Boylam = 34.6315580,TelefonNo = "3242344480",Adres = "GÜNDOGDU MAH.5790 SK.NO:17 AKDENIZ/MERSIN",AdresTarifiKisa = "MERSÝN FEN LÝSESÝ YANI, 1 NO LU SAÐLIK OCAÐI KARÞI",AdresTarifi ="GÜNDOÐDU MAH,KURDALÝ YOLU,MERSÝN FEN LÝSESÝ YANI, 1 NO LU SAÐLIK OCAÐI KARÞISI/AKDENÝZ"},
new Eczane { Id = 575,Enlem = 36.8097244,Boylam = 34.6222612,TelefonNo = "3243371489",Adres = "NUSRATIYE MAH.5026 SK. NO:44 AKDENIZ/MERSIN",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ ARKA SOKAÐI ",AdresTarifi ="ESKÝ DEVLET HASTANESÝ ARKA SOKAÐI/AKDENÝZ"},
new Eczane { Id = 500,Enlem = 36.8361900,Boylam = 34.6107400,TelefonNo = "3242241600",Adres = "ÇAGDASKENT MAH. ERZÝNCANLILAR SÝTESÝ CÝVARI VEREM SAVAÞ DÝSPANSERÝ KARÞISI TOROSLAR-MERSÝN",AdresTarifiKisa = "ÇAÐDAÞKENT MAH. SÜLEYMAN GÖK ASM KARÞISI ",AdresTarifi ="ÇAÐDAÞKENT MAH. SÜLEYMAN GÖK ASM KARÞISI ERZÝNCANLILAR SÝTESÝ YANI/TOROSLAR"},
new Eczane { Id = 661,Enlem = 36.7823977,Boylam = 34.5880877,TelefonNo = "3243254600",Adres = "GÜVENLER MH. 1914 SK. DERSANELER SOKAÐI ARKASI - GMK BULVARI GROSERÝ MARKET ARKASI ",AdresTarifiKisa = " GÜVENEVLER MH. 1914 SK. 5/AB NO:20",AdresTarifi =" GÜVENEVLER MH. 1914 SK. 5/AB NO:20"},
new Eczane { Id = 726,Enlem = 36.7652420,Boylam = 34.5472450,TelefonNo = "3243585008",Adres = "ATATÜRK MH. 31045 SK. NO:23",AdresTarifiKisa = "Ortadoðu Hastanesi Civarý",AdresTarifi ="Ortadoðu Hastanesi Civarý Mezitli Belediye Ýlköðretim Okulu karþýsý Üniversite caddesi civarý/MEZÝTLÝ"},
new Eczane { Id = 551,Enlem = 36.8053340,Boylam = 34.6337520,TelefonNo = "3243365144",Adres = "YENÝ MH. 5328 SK. NO:5/C",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ ACÝL KARÞISI",AdresTarifi ="TOROS DEVLET HASTANESÝ ACÝL KARÞISI"},
new Eczane { Id = 609,Enlem = 36.8222380,Boylam = 34.6375961,TelefonNo = "3242344445",Adres = "GÜNEÞ MH. 5822 SK. NO:11/B",AdresTarifiKisa = "GÜNEÞ MAH. 5822 SK. SELAHATTÝN EYYÜBÝ ASM YANI",AdresTarifi ="GÜNEÞ MAH. 5822 SK. SELAHATTÝN EYYÜBÝ ASM YANI/AKDENÝZ"},
new Eczane { Id = 699,Enlem = 36.7797360,Boylam = 34.5566010,TelefonNo = "3242904494",Adres = "BATIKENT MH. YAÐMUR SULTAN KONUTLARI NO:57/AB",AdresTarifiKisa = "BATIKENT MH. ÞAHÝN TEPESÝ  NECATÝ BOLKAN OKULU YAN",AdresTarifi ="BATIKENT MH. ÞAHÝN TEPESÝ  NECATÝ BOLKAN Ý.Ö.OKULU YANI ÝL ÖZEL ÝDARESÝ KUZEYÝ  YENÝÞEHÝR 3 NOLU ASM CÝVARI/YENÝÞEHÝR"},
new Eczane { Id = 727,Enlem = 36.7635190,Boylam = 34.5481180,TelefonNo = "3243575957",Adres = "ATATÜRK MAH.35.SK.NO:24/B MEZITLI/MERSIN",AdresTarifiKisa = "MERSÝN ORTADOÐU HASTANESÝ ACÝL YANI/MEZÝTLÝ",AdresTarifi ="ÜNÝVERSÝTE YOLU ÖZEL MERSÝN ORTADOÐU HASTANESÝ ACÝL YANI/MEZÝTLÝ"},
new Eczane { Id = 728,Enlem = 36.7572490,Boylam = 34.5350140,TelefonNo = "3243599990",Adres = "MEZITLI ILÇESI YENI MH.33180 SK.NO:27/AMERSIN",AdresTarifiKisa = "YENÝ MH. KALE YOLU ASM 1 NOLU SAÐLIK OCAÐI KARÞISI",AdresTarifi ="YENÝ MH. KALE YOLU ASM (1 NOLU SAÐLIK OCAÐI KARÞISI) MEZÝTLÝ/MEZÝTLÝ"},
new Eczane { Id = 662,Enlem = 36.7840490,Boylam = 34.5856750,TelefonNo = "3243260990",Adres = "Güvenevler Mh. Dumlupýnar Cd. No:36/A",AdresTarifiKisa = "FORUM AVM CÝVARI YENÝÞEHÝR ÝTFAÝYE 100 MT. KUZEYÝ ",AdresTarifi ="FORUM AVM CÝVARI YENÝÞEHÝR ÝTFAÝYE 100 MT. KUZEYÝ MÝREÞ MARKET KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 756,Enlem = 36.7401684,Boylam = 34.5261162,TelefonNo = "3243583037",Adres = "MENDERES MAHALLESI CEVAT KUTLU NO.15/A MEZITLI MERSIN",AdresTarifiKisa = "DENÝZHAN-2 KARÞISI, NAFÝZ ÇOLAK SAÐLIK OCAÐI KARÞI",AdresTarifi ="DENÝZHAN-2 KARÞISI, NAFÝZ ÇOLAK SAÐLIK OCAÐI KARÞISI/MEZÝTLÝ"},
new Eczane { Id = 700,Enlem = 36.7706280,Boylam = 34.5620680,TelefonNo = "3243416690",Adres = "EGRIÇAM MAHALLESI GMK BULVARI.NO.3/C YENISEHIR MERSIN",AdresTarifiKisa = "GMK BULV.DSÝ YANI SULTAÞA OTEL CÝVARI.",AdresTarifi ="GMK BULV.EÐRÝÇAM MH.DSÝ YANI SULTAÞA OTEL CÝVARI."},
new Eczane { Id = 663,Enlem = 36.7862290,Boylam = 34.5932560,TelefonNo = "3243278020",Adres = "GÜVENEVLER MAH.1.CAD.NO:112/3 YENISEHIR/MERSIN",AdresTarifiKisa = "GÜVENEVLER MH. 1.CD. POZCU ÇETÝNKAYA BÝTÝÞÝÐÝ/YENÝ",AdresTarifi ="GÜVENEVLER MH. 1.CD. POZCU ÇETÝNKAYA BÝTÝÞÝÐÝ/YENÝÞEHÝR"},
new Eczane { Id = 576,Enlem = 36.8106930,Boylam = 34.6243060,TelefonNo = "3243371421",Adres = "NUSRATIYE MAH.5021 SOK.NO.38.AKDENIZ",AdresTarifiKisa = "NUSRATÝYE MH ANA ÇOCUK SAÐLIÐI KARÞISI ",AdresTarifi ="NUSRATÝYE MH ANA ÇOCUK SAÐLIÐI KARÞISI 23 EVLER KAVÞAÐI/AKDENÝZ"},
new Eczane { Id = 623,Enlem = 36.7950930,Boylam = 34.6235790,TelefonNo = "3242336845",Adres = "Kiremithane Mh. 4406 Sk. No:4",AdresTarifiKisa = " ÖZEL DOÐUÞ HASTANESÝ YANI/AKDENÝZ",AdresTarifi ="ÝSTÝKLAL CD. ÖZGÜR ÇOCUK PARKI ARKASI ÖZEL DOÐUÞ HASTANESÝ YANI/AKDENÝZ"},
new Eczane { Id = 703,Enlem = 36.7781860,Boylam = 34.5340880,TelefonNo = "3243364250",Adres = "ÇÝFTLÝKKÖY MH. ÜNÝVERSÝTE CD. 81 SK. NO: 12/A",AdresTarifiKisa = " ÜNÝVERSÝTE CADDESÝ KAMPÜS  GÝRÝÞ YANI",AdresTarifi ="ÇÝFTLÝKKÖY MAH. ÜNÝVERSÝTE CADDESÝ KAMPÜS GÝRÝÞ KAPISI ÖNÜ/YENÝÞEHÝR"},
new Eczane { Id = 625,Enlem = 36.7999120,Boylam = 34.6199050,TelefonNo = "3243371353",Adres = "BAHÇE MAH.SOGUKSU 124 CAD.125/D AKDENIZ-MERSIN",AdresTarifiKisa = "BURHANFELEK CD ERCÝYES TAKSÝ DURAÐI/AKDENÝZ",AdresTarifi ="BURHANFELEK CD ERCÝYES TAKSÝ DURAÐI/AKDENÝZ"},
new Eczane { Id = 664,Enlem = 36.7896146,Boylam = 34.5870556,TelefonNo = "3243311407",Adres = "GÜVENEVLER MH.20.CAD.NO:19/C",AdresTarifiKisa = "FORUM YAÞAM HAS. KARÞISI",AdresTarifi ="FORUM YAÞAM HASTANESÝ KARÞISI GÜVENEVLER MH. 20 CD. NO:19/C/YENÝÞEHÝR"},
new Eczane { Id = 610,Enlem = 36.8238940,Boylam = 34.6542330,TelefonNo = "3242352726",Adres = "HAL MAH.6025 SK.NO:39 8 NOLU SAG.OC.YANI AKDENIZ ILÇESI MERSIN",AdresTarifiKisa = "HAL MAH. 6025 SK. NO:39 8 NOLU SAÐLIK OCAÐI BÝTÝÞÝ",AdresTarifi ="HAL MAH. 6025 SK. NO:39 8 NOLU SAÐLIK OCAÐI BÝTÝÞÝÐÝ/AKDENÝZ"},
new Eczane { Id = 757,Enlem = 36.7151070,Boylam = 34.4770510,TelefonNo = "3244815448",Adres = "SILIFKE   CAD.NO:86/DAVULTEPE MEZITLI-MERSIN",AdresTarifiKisa = "DAVULTEPE SAÐLIK OCAÐI KARÞISI - DAVULTEPE",AdresTarifi ="GMK BULV. KAZIM KARABEKÝR CD. 1110-A DAVULTEPE SAÐLIK OCAÐI KARÞISI - DAVULTEPE"},
new Eczane { Id = 758,Enlem = 36.7406600,Boylam = 34.5256200,TelefonNo = "3243571142",Adres = "MENDERES MAHALLSI CEVAT KUTLU CADDESI N0.15/D MEZITLI/MERSIN",AdresTarifiKisa = "NAFÝZ ÇOLAK SAÐLIK OCAÐI KARÞISI ",AdresTarifi ="NAFÝZ ÇOLAK SAÐLIK OCAÐI KARÞISI FLORYA LÝFE EVLERÝ ALTI/MEZÝTLÝ"},
new Eczane { Id = 520,Enlem = 36.8376547,Boylam = 34.5975522,TelefonNo = "3243243042",Adres = "ÇUKUROVA MAH.85119 SK.NO:130/A TOROSLAR-MERSIN",AdresTarifiKisa = "ÇUKUROVA MH.  ÇUKUROVA SAÐLIK OCAÐI KARÞISI",AdresTarifi ="ÇUKUROVA MAH 85119 SK NO 130 ÇUKUROVA SAÐLIK OCAÐI KARÞISI/TOROSLAR"},
new Eczane { Id = 637,Enlem = 36.7835680,Boylam = 34.6038540,TelefonNo = "3243270492",Adres = "GAZI MAH.1304 SK.INCI SIT.12/C YENISEHIR MERSIN",AdresTarifiKisa = " MUÐDAT CAMÝÝ DOÐUSU BÝM ARKASI GAZÝ ASM KARÞISI",AdresTarifi ="MUÐDAT CAMÝÝ DOÐUSU BÝM MARKET ARKASI GAZÝ AÝLE SAÐLIÐI MERKEZÝ KARÞISI POZCU/YENÝÞEHÝR"},
new Eczane { Id = 521,Enlem = 36.8192690,Boylam = 34.6148440,TelefonNo = "3243202026",Adres = "TOZKOPARAN MH. 87045 SK. NO:19/A",AdresTarifiKisa = " TOZKOPARAN SAÐLIK OCAÐI KARÞISI TOROSLAR",AdresTarifi ="TOZKOPARAN MAH 87045 SOK. TOZKOPARAN SAÐLIK OCAÐI KARÞISI (PALMÝYE DÜÐÜN SALONU KARÞISI)/TOROSLAR"},
new Eczane { Id = 730,Enlem = 36.7561180,Boylam = 34.5377100,TelefonNo = "3243592200",Adres = "MEZITLI ILÇESI YENI MAH.KELVELI CD. ZIYA ÜNSAL APT. A BLOK ALTI. NO:3/D             MERSIN",AdresTarifiKisa = "YENÝ MH. KELVELÝ CD. MEZÝTLÝ SEMT POLÝK. KARÞISI",AdresTarifi ="YENÝ MH. KELVELÝ CD. ZÝYA ÜNSAL APT A BLOK - KALE YOLU - TOROS DEVLET HAST. MEZÝTLÝ SEMT POLÝKLÝNÝÐÝ KARÞISI   MEZÝTLÝ/MEZÝTLÝ"},
new Eczane { Id = 522,Enlem = 36.8081280,Boylam = 34.6121810,TelefonNo = "3243204178",Adres = "TURGUT TÜRKALP MH.79028SK.NO:6 TOROSLAR/MERSIN",AdresTarifiKisa = "TURGUT TÜRKALP MAH 10 NOLU SAÐLIK OCAÐI YANI",AdresTarifi ="TURGUT TÜRKALP MAH 10 NOLU SAÐLIK OCAÐI YANI/TOROSLAR"},
new Eczane { Id = 731,Enlem = 36.7577930,Boylam = 34.5426580,TelefonNo = "3243592666",Adres = "BABIL KAVSAGI MEZITLI/MERSIN",AdresTarifiKisa = "GMK.BLV.BABÝL KAVÞAÐI SANAYÝ GÝRÝÞÝ/MEZÝTLÝ",AdresTarifi ="GMK.BLV.BABÝL KAVÞAÐI SANAYÝ GÝRÝÞÝ/MEZÝTLÝ"},
new Eczane { Id = 704,Enlem = 36.7711900,Boylam = 34.5627800,TelefonNo = "3243414101",Adres = "Eðriçam Mh. GMK Bulv. No:564",AdresTarifiKisa = "GMK BULV. UZMAN ATA TIP MERKEZÝ KARÞISI ",AdresTarifi ="EÐRÝÇAM MH. GMK BULV. UZMAN ATA TIP MERKEZÝ KARÞISI SULTAÞA OTELÝ KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 552,Enlem = 36.8062000,Boylam = 34.6359500,TelefonNo = "3242376797",Adres = "YENI MH.5314SK.GÜNDÜZ APT.ALTI NO:2/A AKDENIZ/MERSIN",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ KARÞISI ( ESKÝ SSK )/AKDENÝ",AdresTarifi ="TOROS DEVLET HASTANESÝ KARÞISI ( ESKÝ SSK )/AKDENÝZ"},
new Eczane { Id = 611,Enlem = 36.8149150,Boylam = 34.6384100,TelefonNo = "3242343311",Adres = "MERSIN ILI AKDENIZ ILÇESI SITELER MH. 5651 SK. NO:8 MERSIN",AdresTarifiKisa = "SÝTELER MAH MUHTARLIÐI ARKASI ÞEHÝT CENGÝZ KIR ASM",AdresTarifi ="SÝTELER MH. SÝTELER MAHALLE MUHTARLIÐI ARKASI ÞEHÝT CENGÝZ KIR ASM KARÞISI/AKDENÝZ"},
new Eczane { Id = 638,Enlem = 36.7901034,Boylam = 34.6022923,TelefonNo = "3243290055",Adres = "YENISEHIR ILÇESI CUMHURIYET MH.ISMET INÖNÜ BLV.93/B",AdresTarifiKisa = "ESKÝ GÜLERYÜZ PAST-ÇÝLEK MOBÝLYA ÇARPRAZ KARÞISI",AdresTarifi ="ESKÝ GÜLERYÜZ PASTANESÝ 100 MT GÜNEYÝ ÇÝLEK MOBÝLYA ÇAPRAZ KARÞISI YENÝÞEHÝR"},
new Eczane { Id = 577,Enlem = 36.8103850,Boylam = 34.6274350,TelefonNo = "3243369032",Adres = "NUSRATÝYE MH 5004 SK NO:5",AdresTarifiKisa = "NUSRATÝYE MH DÝABET HASTANESÝ YANI ",AdresTarifi ="NUSRATÝYE MH DÝABET HASTANESÝ YANI ATATÜRK LÝSESÝ ARKASI/AKDENÝZ"},
new Eczane { Id = 666,Enlem = 36.779475,Boylam = 34.5749355,TelefonNo = "3243255740",Adres = "BARBAROS MH. GÖÇMEN SEMTÝ  2148 SOK. NO:22/A YENISEHIR/MERSIN",AdresTarifiKisa = " YENÝÞEHÝR KAYMAKAMLIÐI KARÞ.ÇOCUK PARKI ARKASI.",AdresTarifi =" BARBAROS MAH.YENÝÞEHÝR KAYMAKAMLIÐI KARÞ.ÇOCUK  PARKI ARKASI SAÐLIK OCAÐI KARÞISI GÖÇMENKÖY/MERSÝN"},
new Eczane { Id = 578,Enlem = 36.8053090,Boylam = 34.6297930,TelefonNo = "3242312745",Adres = "ÇAKMAK CAD.NO:51/B-AKDENIZ/MERSIN",AdresTarifiKisa = "ÇAKMAK CD YAPI KREDÝ BANK VE HASIRCI CAMÝ ARASI",AdresTarifi ="ÇAKMAK CADDESÝ YAPI KREDÝ BANKASI VE HASIRCI CAMÝ ARASI SEMT PAZARI YANI/AKDENÝZ"},
new Eczane { Id = 736,Enlem = 36.7568890,Boylam = 34.5351060,TelefonNo = "3243595474",Adres = " YENI MH.YÜKSEK HARMAN CD. 180 SK. AKKOYUNCU AP. NO:12 MEZÝTL",AdresTarifiKisa = "",AdresTarifi ="MEZÝTLÝ KÝTAPSAN (300 MT YUKARISI) KALEYOLU SAÐLIK OCAÐI KARÞISI/MEZÝTLÝ"},
new Eczane { Id = 737,Enlem = 36.7541709,Boylam =  34.544972,TelefonNo = "3243599733",Adres = "FATIH MAH.BABIL CAD. ARTEMIS SITESI NO:27 MEZITLI/MERSIN",AdresTarifiKisa = "FATÝH MH. BABÝL CD.MEZÝTLÝ MADO KARÞISI/MEZÝTLÝ",AdresTarifi ="FATÝH MH. BABÝL CD.MEZÝTLÝ MADO KARÞISI/MEZÝTLÝ"},
new Eczane { Id = 523,Enlem = 36.8260160,Boylam = 34.6227100,TelefonNo = "3243206383",Adres = "KURDALÝ MH. MERSÝNLÝ AHMET BULV. NO:63",AdresTarifiKisa = "1 NOLU ASM YANI /TOROSLAR",AdresTarifi ="KURDALÝ MH.MERSÝNLÝ AHMET CD.NO:63 TOROSLAR 1 NOLU ASM YANI /TOROSLAR"},
new Eczane { Id = 667,Enlem = 36.7807260,Boylam = 34.5849790,TelefonNo = "3243294060",Adres = "Aydýnlýkevler Mh. 2001 Sk. No:18/B Ýlyas Apt. Altý  Pozcu /YENÝÞEHÝR",AdresTarifiKisa = "POZCU ADESE KARÞISI BÝM MARKET YANI",AdresTarifi ="POZCU ADESE KARÞISI BÝM MARKET YANI"},
new Eczane { Id = 579,Enlem = 36.8044300,Boylam = 34.6237100,TelefonNo = "3243371017",Adres = "IHSANIYE MH. KUVAAI MILLIYE CD. AKARSU PLAZA NO:6",AdresTarifiKisa = "HASTANE CD. AKDENÝZ BELEDÝYESÝ ARKASI ",AdresTarifi ="HASTANE CD. AKDENÝZ BELEDÝYESÝ ARKASI ZÝRAAT BANKASI KURUÇEÞME ÞUBESÝ KARÞISI /AKDENÝZ"},
new Eczane { Id = 553,Enlem = 36.8064457,Boylam = 34.6356586,TelefonNo = "3243366620",Adres = "YENÝ MH. 5314 SK. NO:4",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ (ESKÝ SSK) KARÞISI",AdresTarifi ="TOROS DEVLET HASTANESÝ (ESKÝ SSK) KARÞISI"},
new Eczane { Id = 639,Enlem = 36.7892857,Boylam = 34.6065127,TelefonNo = "3243266662",Adres = "CUMHURÝYET MH. 1628 SK. NO: 6/A-B",AdresTarifiKisa = "PÝRÝREÝS AÝLE SAÐLIÐI MERKEZÝ KARÞISI YENÝÞEHÝR",AdresTarifi ="ÝSMET ÝNÖNÜ BULV.YURT ÝÇÝ KARGO-ABÝDÝN TANTUNÝ ARKA SOKAÐI PÝRÝREÝS AÝLE SAÐLIÐI MERKEZÝ KARÞISI"},
new Eczane { Id = 640,Enlem = 36.784282,Boylam = 34.5981303,TelefonNo = "3243257130",Adres = "GMK BULV. NO:330/A TUBA APT. ALTI",AdresTarifiKisa = "GMK BLV.TÜRK TELEKOM BATISI DOMÝNOS PÝZZA  KARÞISI",AdresTarifi ="GMK BULVARI TÜRK TELEKOM BATISI DOMÝNOS PÝZZA KARÞISI POZCU/YENÝÞEHÝR"},
new Eczane { Id = 759,Enlem = 36.7499690,Boylam = 34.5197960,TelefonNo = "3243571992",Adres = "Merkez Mh. 52081 Sk. Lider Konutlarý Altý No:5/A",AdresTarifiKisa = "KUYULUK YOLU - BÝM MARKET ARKASI",AdresTarifi ="KUYULUK YOLU - BÝM MARKET ARKASI LÝDER KONUTLARI ALTI OSMANGAZÝ ASM CÝVARI - MEZÝTLÝ"},
new Eczane { Id = 628,Enlem = 36.7959509,Boylam = 34.6266830,TelefonNo = "3242324168",Adres = "SILIFKE CAD.BAHRI OK ISHANI NO:51/B AKDENIZ/MERSIN",AdresTarifiKisa = "SÝLÝFKE CD ESKÝ ÖÐRETMENEVÝ KARÞISI/AKDENÝZ",AdresTarifi ="SÝLÝFKE CD ESKÝ ÖÐRETMENEVÝ KARÞISI/AKDENÝZ"},
new Eczane { Id = 580,Enlem = 36.8069150,Boylam = 34.6179420,TelefonNo = "3243361533",Adres = "ÝHSANÝYE MAH.SAÝT ÇÝFTÇÝ CAD.NO:36 ÖZEL SU HASTANESÝ ACÝL KARÞISI AKDENÝZ/MERSÝN",AdresTarifiKisa = "ÖZEL SU HASTANESÝ ACÝL KARÞISI AKDENÝZ",AdresTarifi ="ÖZEL SU HASTANESÝ ACÝL KARÞISI AKDENÝZ/MERSÝN"},
new Eczane { Id = 502,Enlem = 36.8357010,Boylam = 34.6110080,TelefonNo = "3242233636",Adres = "MERSIN ILI TOROSLAR ILÇESI ÇAGDASKENT MH. 229 CD.10/C",AdresTarifiKisa = "ERZÝNCANLILAR SÝT KARÞISI SÜLEYMAN GÖK ASM CÝVARI",AdresTarifi ="ÇAÐDAÞKENT MH. 229. CD. ERZÝNCANLILAR SÝTESÝ KARÞISI SÜLEYMAN GÖK ASM CÝVARI/TOROSLAR"},
new Eczane { Id = 760,Enlem = 36.7489010,Boylam = 34.5292870,TelefonNo = "3243574585",Adres = "MERKEZ MAH.GMK BUL.MEZITLI ÇARS.C BLOK NO:192 MEZITLI/MERSIN",AdresTarifiKisa = "MEZÝTLÝ BLD.ÇAPRAZ KARÞISI 2 NOLU SAÐLIK OCAÐI YAN",AdresTarifi ="MEZÝTLÝ BLD.ÇAPRAZ KARÞISI 2 NOLU SAÐLIK OCAÐI YANI/MEZÝTLÝ"},
new Eczane { Id = 668,Enlem = 36.7936810,Boylam = 34.5844320,TelefonNo = "3243287747",Adres = "LÝMONLUK MH. KANÞIRAY APT. ZEMÝNKAT NO: 48/A",AdresTarifiKisa = "CEMEVÝ KARÞISI MÜFÝDE ÝLHAN ÝLKÖÐRETÝM OKULU YANI",AdresTarifi ="LÝMONLUK MH. 9. CD. CEMEVÝ KARÞISI MÜFÝDE ÝLHAN ÝLKÖÐRETÝM OKULU YANI/YENÝÞEHÝR"},
new Eczane { Id = 503,Enlem = 36.8320303,Boylam = 34.6139739,TelefonNo = "3242240102",Adres = "GAZÝ OSMAN PAÞA CAD. 93016 SK. NO:16/A",AdresTarifiKisa = " GAZÝ OSMAN PAÞA CAD. EMEK SÝT. YANI /TOROSLAR",AdresTarifi ="GAZÝ OSMAN PAÞA 93003 SK. CELLO ÇEÞMESÝ BATISI EMEK SÝTESÝ YANI GAZÝ OSMAN PAÞA ASM KARÞISI /TOROSLAR"},
new Eczane { Id = 554,Enlem = 36.8045130,Boylam = 34.6326180,TelefonNo = "3242373799",Adres = "YENI MAH.CEMALPASA CAD. 74/A-AKDENIZ",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ TREN ÝSTASYONU ARASI",AdresTarifi ="MERSÝN TOROS DEVLET HASTANESÝNDEN TREN ÝSTASYONUNA GÝDEN GÜZERGAHTA SAÐDA PETROL OFÝSÝ KARÞISI"},
new Eczane { Id = 524,Enlem = 36.8379040,Boylam = 34.5987590,TelefonNo = "3243244060",Adres = "Çukurova Mh. 85119 Sk. No:134 A1-2",AdresTarifiKisa = "ÇUKUROVA SAÐLIK OCAÐI KARÞISI/TOROSLAR",AdresTarifi ="ÇUKUROVA MH. 85119 SK.ÇUKUROVA SAÐLIK OCAÐI KARÞISI/TOROSLAR"},
new Eczane { Id = 525,Enlem = 36.8249017,Boylam = 34.6008625,TelefonNo = "3243413434",Adres = "AKBELEN MH. TOROS CD. NO:6 TOROSLAR",AdresTarifiKisa = "AKBELEN SAÐLIK OCAÐI KARÞISI ",AdresTarifi ="AKBELEN MH. AKBELEN SAÐLIK OCAÐI KARÞISI KORAY AYDIN STADI ALTI/TOROSLAR"},
new Eczane { Id = 641,Enlem = 36.7939650,Boylam = 34.6009070,TelefonNo = "3243287355",Adres = "HÜRRIYET MAHALLESI 1742 SOKAK NO.31 YENISEHIR.MERSIN",AdresTarifiKisa = " CARREFOUR YOLU ÜZERÝ MÝGROS MARKET ARKASI ",AdresTarifi ="HÜRRÝYET MH CARREFOUR YOLU ÜZERÝ MÝGROS MARKET ARKASI SAÐLIK OC. KRÞ/YENÝÞEHÝR"},
new Eczane { Id = 629,Enlem = 36.7926482,Boylam = 34.6238667,TelefonNo = "3242379310",Adres = "KÜLTÜR MH.4303 SK.ÇAMLIBEL AP.23/D AKDENIZ MERSIN",AdresTarifiKisa = "SÝSTEM TIP ACÝL GÝRÝÞÝ KARÞISI/AKDENÝZ",AdresTarifi ="KÜLTÜR MH. 4303 SK. 23/D  SÝSTEM TIP ACÝL GÝRÝÞÝ KARÞISI/AKDENÝZ"},
new Eczane { Id = 738,Enlem = 36.7527600,Boylam = 34.5357940,TelefonNo = "3243291918",Adres = "GMK BULVARI GÜNCEL AP. ALTI 675 /A NO:9 MEZÝTLÝ OPET VE MC DONALT KARÞISI MCÝTY HOSPÝTAL YANI",AdresTarifiKisa = "GMK BUL. OPET KARÞISI M CÝTY HOSPÝTAL YANI",AdresTarifi ="GMK BULVARI GÜNCEL AP. ALTI 675 /A NO:9 MEZÝTLÝ OPET VE MC DONALT KARÞISI MCÝTY HOSPÝTAL YANI"},
new Eczane { Id = 761,Enlem = 36.7444300,Boylam = 34.5236880,TelefonNo = "3243582210",Adres = "MENDERES MH. GMK BULV. 35433 SK. TÝGRÝS 5 APT ALTI NO:1",AdresTarifiKisa = "MEZÝTLÝ GMK BULV. 11. NOTER YANI - LCW KARÞISI",AdresTarifi ="GMK BULV. MEZÝTLÝ  11. NOTER YANI VE ÖZEL MERSÝN ÇOCUK HASTALIKLARI MERKEZÝ YANI - LCW KARÞISI MEZÝTLÝ"},
new Eczane { Id = 581,Enlem = 36.8009530,Boylam = 34.6269220,TelefonNo = "3242393722",Adres = "CAMI SERIF MAH.170.CAD. CEMALPASA CADDE NO.93/C. AKDENIZ",AdresTarifiKisa = "CAMÝÞERÝF MH. EVKUR ALIÞVERÝÞ MERKEZÝ YANI",AdresTarifi ="CAMÝÞERÝF MH. FABRÝKALAR CD. EVKUR ALIÞVERÝÞ MERKEZÝ YANI / AKDENÝZ"},
new Eczane { Id = 706,Enlem = 36.7854140,Boylam = 34.5400070,TelefonNo = "3243363915",Adres = "MÝMAR SÝNAN CD. ÇÝFTLÝKKÖY MH. 32231 SK. NO:2D BLOK ZEMÝN NO:4",AdresTarifiKisa = "MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞIS",AdresTarifi ="ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. (MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI) YENÝÞEHÝR"},
new Eczane { Id = 739,Enlem = 36.7567710,Boylam = 34.5376750,TelefonNo = "3243580664",Adres = "KALE YOLUZIRAATCILAR SITESI A.BLOKNO.1 MEZITLI",AdresTarifiKisa = "TOROS DEVLET HAST.SEMT POLKLÝNÝÐÝ KARÞISI ",AdresTarifi ="TOROS DEVLET HAST.SEMT POLKLÝNÝÐÝ KARÞISI (KALEYOLU)/MEZÝTLÝ"},
new Eczane { Id = 582,Enlem = 36.8105690,Boylam = 34.6244940,TelefonNo = "3243372588",Adres = "NUSRATITE MAH. M.AKIF ERSOY CAD.5021.SOK.NO.33/1 AKDENIZ .MERSIN",AdresTarifiKisa = "MERSÝN DEVLET HASTANESÝ ESKÝ ACÝL CÝVARI",AdresTarifi ="MERSÝN DEVLET HASTANESÝ ESKÝ ACÝL CÝVARI 23 EVLER KAVÞAÐI/AKDENÝZ"},
new Eczane { Id = 679,Enlem = 36.7891370,Boylam = 34.5881090,TelefonNo = "3249991151",Adres = "GÜVENEVLER MH. 1949 SK. NO:14/A",AdresTarifiKisa = "FORUM YAÞAM HASTANESÝ YANI",AdresTarifi =" GÜVENEVLER MH. 1949 SK. NO:23 FORUM YAÞAM HASTANESÝ YANI/YENÝÞEHÝR"},
new Eczane { Id = 555,Enlem = 36.8053340,Boylam = 34.6337520,TelefonNo = "3243365152",Adres = "YENÝ  MH. 5328 SK. NO:7/C",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ ACÝL  KARÞISI ",AdresTarifi ="TOROS DEVLET HASTANESÝ ACÝL  KARÞISI "},
new Eczane { Id = 762,Enlem = 36.7493960,Boylam = 34.5288860,TelefonNo = "3243581741",Adres = "MERKEZ MAH.KARAOGLAN CAD. ÖZTÜRK APT NO/5A/5B MEZITLI",AdresTarifiKisa = "MEZÝTLÝ BELEDÝYESÝ KARÞISI 2.NOLIK OCAÐI YANI ",AdresTarifi ="MERKEZLU SAÐ MH. MEZÝTLÝ BELEDÝYESÝ KARÞISI EKMEK FIRINLARI ARKASI 2.NOLIK OCAÐI YANI MEZÝTLÝ/MEZÝTLÝ"},
new Eczane { Id = 642,Enlem = 36.7942510,Boylam = 34.6009570,TelefonNo = "3243292452",Adres = "HÜRRIYET MH.1747SK.I.SINCAR APT.ALTI NO:6/17 YENISEHIR/MERSIN",AdresTarifiKisa = "CARREFOUR YOLU ÜZERÝ HÜRRÝYET ASM YANI",AdresTarifi ="HÜRRÝYET MH. CARREFOUR YOLU ÜZERÝ MÝGROSMARKET ARKA SOKAÐI HÜRRÝYET ASM YANI/YENÝÞEHÝR"},
new Eczane { Id = 526,Enlem = 36.8195340,Boylam = 34.6153040,TelefonNo = "3243215850",Adres = "TOSKOPARAN MAH.87045.SOK NO.14.TOROSLAR",AdresTarifiKisa = " TOZKOPARAN SAÐLIK OCAÐI YANI/TOROSLAR",AdresTarifi ="TOZKOPARAN MAH 87045 SOK. NO 24 TOZKOPARAN SAÐLIK OCAÐI YANI/TOROSLAR"},
new Eczane { Id = 505,Enlem = 36.8425510,Boylam = 34.6235280,TelefonNo = "3242235252",Adres = "HALKKENT MH. FATÝH SULTAN MEHMET BULV.NO:25",AdresTarifiKisa = "ESKÝ KADIN DOÐUM VE ÇOCUK HASTANESÝ  KARÞ",AdresTarifi ="ESKÝ KADIN DOÐUM VE ÇOCUK HASTANESÝ  KARÞISI/TOROSLAR"},
new Eczane { Id = 669,Enlem = 36.7857040,Boylam = 34.5924850,TelefonNo = "3243285493",Adres = "GÜVENEVLER MAH..CAD.CIHAN SAFAK APT.NO:27-2/B YENISEHIR",AdresTarifiKisa = "GÜVEN SÝTESÝ KARÞISI  4 NOLU SAÐLIK OCAÐI KARÞISI",AdresTarifi ="GÜVENEVLER MAH.GÜVEN SÝTESÝ KARÞISI FORUM DOÐU TRF 1.CD. 4 NOLU SAÐLIK OCAÐI KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 603,Enlem = 36.8008300,Boylam = 34.6233490,TelefonNo = "3242372202",Adres = "MAHMUDÝYE MAH. 4812 SOK. NO:28/A AKDENÝZ",AdresTarifiKisa = "MAHMUDÝYE MH. ENDÜSTRÝ MESLEK LÝSESÝ CÝVARI",AdresTarifi ="NOBEL OTELÝ KARÞI SOKAÐI YENÝ ADLÝYEYE DOÐRU ENDÜSTRÝ MESLEK LÝSESÝ CÝVARI"},
new Eczane { Id = 765,Enlem = 36.7492090,Boylam = 34.5292890,TelefonNo = "3243599385",Adres = "MERKEZ MH. 2030 SK.MEZITLI 2 NO LU SAGLIK OCAGI YANI MEZITLI/MERSIN",AdresTarifiKisa = "MEZÝTLÝ BELEDÝYESÝ KARÞISI ALDA MARKET ARKASI ",AdresTarifi ="MEZÝTLÝ BELEDÝYESÝ KARÞISI ALDA MARKET ARKASI MERKEZ MH. ASM YANI/MEZÝTLÝ"},
new Eczane { Id = 670,Enlem = 36.7832833,Boylam = 34.5920555,TelefonNo = "3243597755",Adres = "Güvenevler Mh. 1908 Sk. Serpil Apt. Altý No: 8/A   YENÝÞEHÝR",AdresTarifiKisa = " FORUM AVM CÝVARI ÖZEL YENÝÞEHÝR HASTANESÝ ARKASI",AdresTarifi =" FORUM AVM CÝVARI ÖZEL YENÝÞEHÝR HASTANESÝ ARKASI - POZCU"},
new Eczane { Id = 713,Enlem = 36.7741661,Boylam = 34.5677681,TelefonNo = "3243270483",Adres = "EGRIÇAM MAH. GMK BUL.NO:458 YENISEHIR MERSIN",AdresTarifiKisa = "GMK BULVARI VATAN BÝLGÝSAYAR - 48 ELEKTRÝK  ARASI ",AdresTarifi ="GMK BULVARI EÐRÝÇAM MH VATAN BÝLGÝSAYAR - 48 ELEKTRÝK ARASI/YENÝÞEHÝR"},
new Eczane { Id = 643,Enlem = 36.7856220,Boylam = 34.6032450,TelefonNo = "3243273353",Adres = "GMK BULV. ALDO SÝTESÝ 300/B",AdresTarifiKisa = "GMK BULV POZCU ET BALIK KURUMU KARÞISI GRATÝS YANI",AdresTarifi ="GMK BULV. ANA CADDE ÜZERÝ POZCU ET BALIK KURUMU KARÞISI GRATÝS YANI YENÝÞEHÝR"},
new Eczane { Id = 556,Enlem = 36.8073300,Boylam = 34.6351400,TelefonNo = "3242388215",Adres = "YENÝ MH. 5314 SK. NO:12",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ KARÞISI ( ESKÝ SSK)/AKDENÝZ",AdresTarifi ="TOROS DEVLET HASTANESÝ KARÞISI ( ESKÝ SSK)/AKDENÝZ"},
new Eczane { Id = 689,Enlem = 36.7834040,Boylam =  34.587422,TelefonNo = "3245020304",Adres = "GÜVENEVLER  MAHALLESÝ 1.CADDE NO:9 YENÝÞEHÝR/MERSÝN",AdresTarifiKisa = "FORUM ALIÞVERÝÞ MERKEZÝ ÝLE ÝTFAÝYE ARASI",AdresTarifi ="FORUM ALIÞVERÝÞ MERKEZÝ ÝLE ÝTFAÝYE ARASI "},
new Eczane { Id = 707,Enlem = 36.7854310,Boylam = 34.5402370,TelefonNo = "3243372522",Adres = "MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI",AdresTarifiKisa = "MERSÝN ÜNÝVERSÝTESÝ TIPFAKÜLTESÝ HASTANESÝ KARÞISI",AdresTarifi ="ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. (MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI) /YENÝÞEHÝR"},
new Eczane { Id = 644,Enlem = 36.7941148,Boylam = 34.600818,TelefonNo = "3243262599",Adres = "HÜRRIYET MAHALLESI 1753 SOKAK NO.11/C YENISEHIR.MERSIN",AdresTarifiKisa = " BÜYÜK CARREFOUR YOLU ÜZERÝ MÝGROS MARKET ARKASI",AdresTarifi ="HÜRRÝYET MH. BÜYÜK CARREFOUR YOLU ÜZERÝ MÝGROS MARKET ARKA SOKAÐI HÜRRÝYET ASM KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 740,Enlem = 36.7563370,Boylam = 34.5373640,TelefonNo = "3243595175",Adres = "KALEYOLU MEZÝTLÝ SEMT POLÝKLÝNÝÐÝ KARÞISI NO : 23/B YENÝMAHALLE / MEZÝTLÝ / MERSÝN",AdresTarifiKisa = "KALE YOLU MEZÝTLÝ TOROS SEMT POLÝKLÝNÝÐÝ KARÞISI",AdresTarifi ="KALE YOLU MEZÝTLÝ TOROS SEMT POLÝKLÝNÝÐÝ KARÞISI/MEZÝTLÝ"},
new Eczane { Id = 557,Enlem = 36.8058940,Boylam = 34.6332380,TelefonNo = "3242335948",Adres = "YENÝ MH. 5328 SK NO: 5/A",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ ACÝL KAPISI KARÞISI ",AdresTarifi ="TOROS DEVLET HASTANESÝ ACÝL KAPISI KARÞISI YENÝ MH. 5328 SK /AKDENÝZ"},
new Eczane { Id = 671,Enlem = 36.7823700,Boylam = 34.5954880,TelefonNo = "3243259717",Adres = "KUSHÝMOTO SOKAÐI POZCU AKBANK SOKAÐI NO:17/B YENÝÞEHÝR ",AdresTarifiKisa = "KUSHÝMOTO SOKAÐI POZCU AKBANK SOKAÐI ",AdresTarifi ="KUSHÝMOTO SOKAÐI POZCU AKBANK SOKAÐI NO:17/B YENÝÞEHÝR "},
new Eczane { Id = 741,Enlem = 36.7635770,Boylam = 34.5487250,TelefonNo = "3243575524",Adres = "ATATÜRK MAHALLESI 35 SOKAK 23/E MEZITLI MERSIN",AdresTarifiKisa = "ÜNÝV. YOLU ÖZEL MERSÝN ORTADOÐU HASTANESÝ YANI",AdresTarifi ="ÜNÝVERSÝTE YOLU ÖZEL MERSÝN ORTADOÐU HASTANESÝ YANI MEZÝTLÝ/MEZÝTLÝ"},
new Eczane { Id = 645,Enlem = 36.7878990,Boylam = 34.5987870,TelefonNo = "3243259805",Adres = "CUMHURIYET MH.16.CD.NO:22/B YENISEHIR/MERSIN",AdresTarifiKisa = "45 EVLER YOLU ADA ETÜD KARÞISI YENÝÞEHÝR/MERSÝN",AdresTarifi ="CUMHURÝYET MH 45 EVLER YOLU 16. CD NO 22/B VENÜS PASTANESÝ YANI/YENÝÞEHÝR"},
new Eczane { Id = 583,Enlem = 36.8042160,Boylam = 34.6308310,TelefonNo = "3242315880",Adres = "MESUDIYE MAHALLESI ÇAKMAK CADDESI S.G.K. IL MÜD.KARSISI NO 31/A AKDENIZ MERSIN",AdresTarifiKisa = "ÇAKMAK CADDESÝ BAÐ KUR KARÞISI",AdresTarifi ="ÇAKMAK CADDESÝ BAÐ KUR KARÞISI/AKDENÝZ"},
new Eczane { Id = 767,Enlem = 36.7601200,Boylam = 34.5119440,TelefonNo = "3243588451",Adres = "ÇANKAYA MAHALLESÝ 202 SOK. NO: 17 / A KUYULUK/ MEZÝTLÝ /MERSÝN",AdresTarifiKisa = "FINDIKPINARI CADDESÝ KUYULUK ASM KARÞISI ",AdresTarifi ="FINDIKPINARI CADDESÝ KUYULUK ASM KARÞISI KUYULUK/ MEZÝTLÝ/MERSÝN"},
new Eczane { Id = 507,Enlem = 36.8456810,Boylam =  34.633806,TelefonNo = "3242292696",Adres = " YALINAYAK MH. ATATÜRK CD. NO:1/A",AdresTarifiKisa = "YALINAYAK SAÐLIK OCAÐI YANI/TOROSLAR",AdresTarifi ="YALINAYAK MH. ATATÜRK CAD. YALINAYAK SAÐLIK OCAÐI YANI/TOROSLAR"},
new Eczane { Id = 708,Enlem = 36.7860620,Boylam = 34.5397650,TelefonNo = "3242902206",Adres = "ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. PARADÝSE HOMES C BLOK NO: 24 CG",AdresTarifiKisa = "MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞIS",AdresTarifi ="MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 613,Enlem = 36.8222380,Boylam = 34.6375960,TelefonNo = "3242357656",Adres = "GÜNEÞ MAH. 5822 SOK. NO:5/A AKDENÝZ/MERSÝN",AdresTarifiKisa = "GÜNEÞ MH.SELAHATTÝN EYYUBÝ SAÐLIK OCAÐI YANI ",AdresTarifi ="GÜNEÞ MAH. SELAHATTÝN EYYUBÝ SAÐLIK OCAÐI YANI KURDALÝ CAMÝSÝ ARKASI"},
new Eczane { Id = 647,Enlem = 36.7833990,Boylam = 34.6039080,TelefonNo = "3243260979",Adres = "GAZÝ MH. 1304 SK. NO:10",AdresTarifiKisa = "MUÐDAT CAMÝÝ DOÐUSU GAZÝ SAÐLIK OCAÐI KARÞISI ",AdresTarifi ="MUÐDAT CAMÝÝ DOÐUSU BÝM MARKET ARKASI GAZÝ SAÐLIK OCAÐI KARÞISI - POZCU/YENÝÞEHÝR"},
new Eczane { Id = 604,Enlem = 36.7896080,Boylam = 34.6173200,TelefonNo = "3242388573",Adres = "HAMÝDÝYE MH. 4209 SK. NO:20/B",AdresTarifiKisa = "MERSÝN HAMAMI SOKAÐI HAMÝDÝYE ASM YANI ",AdresTarifi ="HAMÝDÝYE MH 4209 SK NO: 20/B MERSÝN HAMAMI SOKAÐI HAMÝDÝYE ASM YANI  YANI / AKDENÝZ"},
new Eczane { Id = 527,Enlem = 36.8240273,Boylam = 34.6036559,TelefonNo = "3243225650",Adres = "YUSUF KILIÇ MAH.217.CAD NO.76.TOROSLAR",AdresTarifiKisa = "AYIÞIÐI DÜÐÜN SALONU KARÞISI KARÞISI AKBELEN",AdresTarifi ="YUSUF KILIÇ MH 217 CAD AYIÞIÐI DÜÐÜN SALONU KARÞISI AKBELEN/TOROSLAR"},
new Eczane { Id = 673,Enlem = 36.7857569,Boylam = 34.5930935,TelefonNo = "3243258804",Adres = "Güvenevler Mh. 1. Cd. No:103/B",AdresTarifiKisa = " GÜVEN SÝTESÝ POZCU ÇETÝNKAYA KARÞISI/YENÝÞEHÝR",AdresTarifi ="GÜVENEVLER MAH. 1 CAD. GÜVEN SÝTESÝ POZCU ÇETÝNKAYA KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 674,Enlem = 36.7788175,Boylam = 34.5751753,TelefonNo = "3243260767",Adres = "BARBAROS MH.2148SK.NO:22 YENISEHIR/MERSIN",AdresTarifiKisa = " YENÝÞEHÝR KAYMAKAMLIÐI KARÞISI. ",AdresTarifi ="BARBAROS MAH. YENÝÞEHÝR KAYMAKAMLIÐI KARÞISI. BARBAROS ÇOCUK PARKI ARKASI. 15 NOLU SAÐLIK OCAÐI KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 584,Enlem = 36.8090630,Boylam = 34.6210450,TelefonNo = "3243366180",Adres = "IHSANIYE MAH.SALT ÇIFCI CAD.113.CAD.4903 SOK.NO.5 AKDENIZ",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ KARÞISI ARA SOKAK/AKDENÝZ",AdresTarifi ="ESKÝ DEVLET HASTANESÝ KARÞISI ARA SOKAK/AKDENÝZ"},
new Eczane { Id = 649,Enlem = 36.7937800,Boylam = 34.6011220,TelefonNo = "3243276507",Adres = "HÜRRÝYET MH. 1742 SK",AdresTarifiKisa = "CARREFOUR YOLU ÜZERÝ MÝGROS MARKET ARKA SOKAÐI ",AdresTarifi ="HÜRRÝYET MH. 1742 SK. CARREFOUR YOLU ÜZERÝ MÝGROS MARKET ARKA SOKAÐI 11 NOLU SAÐLIK OCAÐI KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 606,Enlem = 36.7951053,Boylam = 34.6231860,TelefonNo = "3242330101",Adres = "Kiremithane Mh. 4409 Sk. No: 1/D-E-F / AKDENÝZ",AdresTarifiKisa = "ÖZEL DOÐUÞ HASTANESÝ YENÝ BÝNA YANI ",AdresTarifi ="KÝREMÝTHANE MH. 4409 SK. ÖZEL DOÐUÞ HASTANESÝ YENÝ BÝNA YANI - AKDENÝZ"},
new Eczane { Id = 650,Enlem = 36.7868565,Boylam = 34.6099304,TelefonNo = "3243283220",Adres = "PIRIREIS MH.SILIFKE CD.NO:22/A YENISEHIR/MERSIN",AdresTarifiKisa = "PÝRÝREÝS MH. KAPALI SPOR SAL. ARK.BATTI ÇIKTI  CÝV",AdresTarifi ="PÝRÝREÝS MAH. KAPALI SPOR SALONU ARKASI SÝLÝFKE CAD. SONU TULUMBA DURAÐI BATTI ÇIKTIYA GÝRMEDEN SAÐ  YOLUN SONU "},
new Eczane { Id = 768,Enlem = 36.7324059,Boylam = 34.5073086,TelefonNo = "3243582896",Adres = "AKDENÝZ MH. CUMHURÝYET CD. MERSÝNE PARK 3 APT. ALTI NO:12/F  MEZÝTLÝ",AdresTarifiKisa = "YAÞARDOÐU CD-CUMHURÝYET CD. KAVÞAÐI  ",AdresTarifi ="AKDENÝZ MH. CUMHURÝYET CD.- YAÞARDOÐU CD. KAVÞAÐI ÞOK MARKET KARÞISI MERSÝNE PARK 3 APT. ALTI   /MEZÝTLÝ"},
new Eczane { Id = 675,Enlem = 36.7856610,Boylam = 34.5923670,TelefonNo = "3243294406",Adres = "GÜVENEVLER MAH 1.CAD.113/A YENISEHIR",AdresTarifiKisa = "GÜVENEVLER MAH 1. CAD. GÜVEN SÝTESÝ KARÞISI/YENÝÞE",AdresTarifi ="GÜVENEVLER MAH 1. CAD. GÜVEN SÝTESÝ KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 742,Enlem = 36.7654070,Boylam = 34.5472560,TelefonNo = "3242215186",Adres = "ATATÜRK MH. 31045 SK. NO:18/A",AdresTarifiKisa = " ÜNÝVERSÝTE YOLU ÖZEL ORTADOÐU HASTANESÝ CÝVARI ",AdresTarifi =" ÜNÝVERSÝTE YOLU ÖZEL ORTADOÐU HASTANESÝ CÝVARI MEZÝTLÝ BELEDÝYE ÝLK ÖÐRETÝM OKULU YANI  MEZÝTLÝ"},
new Eczane { Id = 528,Enlem = 36.8512597,Boylam = 34.6072272,TelefonNo = "3243211203",Adres = "KORUKENT MH. 96004SK. NO:17/4 (B)",AdresTarifiKisa = "ÞEHÝR HASTANESÝ YANI KORUKENT MAH. 96004 SOK. NO: ",AdresTarifi ="ÞEHÝR HASTANESÝ YANI KORUKENT MAH. 96004 SOK. NO: 17/A"},
new Eczane { Id = 631,Enlem = 36.8176090,Boylam = 34.6315660,TelefonNo = "3242359602",Adres = "GÜNDOGDU MAH.5790 SK.NO:15 AKDENIZ ILÇESI-MERSIN",AdresTarifiKisa = "GÜNDOÐDU MAH  1 NOLU SAÐLIK OCAÐI YANI/AKDENÝZ",AdresTarifi ="GÜNDOÐDU MAH 5790 SOK. NO 12/A 1 NOLU SAÐLIK OCAÐI YANI/AKDENÝZ"},
new Eczane { Id = 585,Enlem = 36.8089900,Boylam = 34.6211470,TelefonNo = "3243375235",Adres = "IHSANIYE MAHALLESI KUVAIMILIYE CADDESI .NO.173 AKDENIZ",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ KARÞISI/AKDENÝZ",AdresTarifi ="ESKÝ DEVLET HASTANESÝ KARÞISI/AKDENÝZ"},
new Eczane { Id = 529,Enlem = 36.8264900,Boylam = 34.6254140,TelefonNo = "3243202177",Adres = "Kurdali Mh. 100075 sk. No:33/A",AdresTarifiKisa = "SELAHATTÝN YANPAR ASM KARÞISI ",AdresTarifi ="SELAHATTÝN YANPAR ASM KARÞISI  KURDALÝ MAH. 100075 SK. NO:33 TOROSLAR/MERSÝN"},
new Eczane { Id = 632,Enlem = 36.8257230,Boylam = 34.6418280,TelefonNo = "3242355600",Adres = "SEVKET SÜMER MAH.156.CAD.NO:27AKDENIZ/MERSIN",AdresTarifiKisa = "ÞEVKET SÜMER MAH 156 CAD SÝTELER KARAKOLU KARÞISI/",AdresTarifi ="ÞEVKET SÜMER MAH 156 CAD SÝTELER KARAKOLU KARÞISI/AKDENÝZ"},
new Eczane { Id = 769,Enlem = 36.7404360,Boylam = 34.5260331,TelefonNo = "3243583818",Adres = "MENDERES MH. CEVAT KUTLU CD. 35415 SK. NO:29/B",AdresTarifiKisa = "NAFÝZ ÇOLAK SAÐLIK OCAÐI CÝVARI MEZÝTLÝ",AdresTarifi ="DENÝZHAN-2 KARÞISINDAKÝ CADDE ÜZERÝ, NAFÝZ ÇOLAK SAÐLIK OCAÐI CÝVARI MEZÝTLÝ/MEZÝTLÝ"},
new Eczane { Id = 677,Enlem = 36.7844320,Boylam = 34.5958580,TelefonNo = "3243283549",Adres = "GÜVENEVLER MAH 18.CAD.CAD.1/B YENISEHIR",AdresTarifiKisa = "DÝKENLÝ YOLDENÝZKIZI PASTAHANESÝ KARÞISI/YENÝÞEHÝR",AdresTarifi ="GÜVENEVLER MAH.18.CAD.3/B DÝKENLÝ YOLDENÝZKIZI PASTAHANESÝ KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 560,Enlem = 36.8074310,Boylam = 34.6350570,TelefonNo = "3242371874",Adres = "YENIMAH.54 SOK.UGUR APT.NO.146 AKDENIZ",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ (ESKÝ SSK HAST.)KARÞISI",AdresTarifi ="TOROS DEVLET HASTANESÝ (ESKÝ SSK HASTANESÝ) KARÞISI/AKDENÝZ"},
new Eczane { Id = 633,Enlem = 36.8313738,Boylam = 34.6355564,TelefonNo = "3242349959",Adres = "AKDENIZ ILÇESI GÜNES MH. ÇIFTÇILER CD. NO:212 MERSIN",AdresTarifiKisa = "GÜNEÞ MH.KURDALÝ KAVÞAÐI TUZ DEPOSU CÝVARI/AKDENÝZ",AdresTarifi ="GÜNEÞ MH. KURDALÝ KAVÞAÐI TUZ DEPOSU CÝVARI/AKDENÝZ"},
new Eczane { Id = 607,Enlem = 36.7942800,Boylam = 34.6155800,TelefonNo = "3242382023",Adres = "TURGUT REIS MAH 4136 SOKAK NO.3 AKDENIZ",AdresTarifiKisa = "TURGUT REÝS MH. IMC HASTANESÝ ACÝL KARÞISI/AKDENÝZ",AdresTarifi ="TURGUT REÝS MH. IMC HASTANESÝ ACÝL KARÞISI/AKDENÝZ"},
new Eczane { Id = 744,Enlem = 36.7580250,Boylam = 34.5455410,TelefonNo = "3243574767",Adres = "FATIH MAH.28.SK.NO:7/A MEZITLI-MERSIN",AdresTarifiKisa = "HUNDAÝ ARKASI SAHÝL SAÐLIK OCAÐI YANI/MEZÝTLÝ",AdresTarifi ="HUNDAÝ ARKASI SAHÝL SAÐLIK OCAÐI YANI/MEZÝTLÝ"},
new Eczane { Id = 743,Enlem = 36.7571577,Boylam = 34.5363259,TelefonNo = "3243570787",Adres = "YENI MAH.KELVELI CAD.ZIYA ÜNSAL APT.MEZITLI/MERSIN",AdresTarifiKisa = "TOROS DEVLET HAS.MEZÝTLÝ SEMT POLÝKLÝNÝÐÝ KARÞISI",AdresTarifi ="MEZÝTLÝ YENÝ MH. KALE YOLU TOROS DEVLET HASTANESÝ MEZÝTLÝ SEMT POLÝKLÝNÝÐÝ KARÞISI MEZÝTLÝ"},
new Eczane { Id = 587,Enlem = 36.8089230,Boylam =  34.621192,TelefonNo = "3243366817",Adres = "KUVAYIMILLIYE CAD.NO:235/A NO:40 AKDENIZ/MERSIN",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ KARÞISI/AKDENÝZ",AdresTarifi ="ESKÝ DEVLET HASTANESÝ KARÞISI/AKDENÝZ"},
new Eczane { Id = 561,Enlem = 36.8066500,Boylam = 34.6355270,TelefonNo = "3242379900",Adres = "YENI MAH.5314SK.NO:6/A AKDENIZ/MERSIN",AdresTarifiKisa = "TOROS DEVLET HST KARÞISI (ESKÝ SSK HAS.)/AKDENÝZ",AdresTarifi ="TOROS DEVLET HST KARÞISI (ESKÝ SSK HAS.)/AKDENÝZ"},
new Eczane { Id = 634,Enlem = 36.8254870,Boylam = 34.6422460,TelefonNo = "3242342078",Adres = "ÞEVKET SÜMER MH. 5975 SK. NO:59/A",AdresTarifiKisa = "ÞEVKET SÜMER MH. SÝTELER KARAKOLU YANI/AKDENÝZ",AdresTarifi ="ÞEVKET SÜMER MH. SÝTELER KARAKOLU YANI/AKDENÝZ"},
new Eczane { Id = 652,Enlem = 36.7827593,Boylam = 34.6042718,TelefonNo = "3243268594",Adres = "GAZI MAHALLESI 1304 SOKAK NO.6-B YENISEHIR MERSIN",AdresTarifiKisa = "MUÐDAT CAMÝ ALTI MAKRO MARKET KARÞI SOKAÐI",AdresTarifi ="Muðdat Camii  Altý Makro Market Karþý Sokaðýndan 100 Metre Ýlerde Ýlk Sokaktan Sola Dönünce?"},
new Eczane { Id = 745,Enlem = 36.7575100,Boylam = 34.5368350,TelefonNo = "3243592067",Adres = "YENÝ MH. KELVELÝ CD. NO: 11/A",AdresTarifiKisa = "TOROS DEVLET HAST. MEZÝTLÝ SEMT POLÝKLÝNÝÐÝ CÝVARI",AdresTarifi ="YENÝ MH. KELVELÝ CD. - KALE YOLU - TOROS DEVLET HAST. MEZÝTLÝ SEMT POLÝKLÝNÝÐÝ CÝVARI YENÝ MH. MUHTARLIÐI KARÞISI /MEZÝTLÝ"},
new Eczane { Id = 588,Enlem = 36.8090990,Boylam =  34.621091,TelefonNo = "3243364676",Adres = " IHSANIYE MH. KUVAI MILLIYE CAD.173/C",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ KARÞISI/AKDENÝZ",AdresTarifi ="ESKÝ DEVLET HASTANESÝ KARÞISI/AKDENÝZ"},
new Eczane { Id = 531,Enlem = 36.8250450,Boylam = 34.6313610,TelefonNo = "3243210990",Adres = "SELÇULAR MAH. 212CD.206/A TOROSLAR/MERSIN",AdresTarifiKisa = "SELÇUKLAR MH.KURDALÝ CAMÝÝ KARÞISI",AdresTarifi ="SELÇUKLAR MH. 212. CD. NO:206/C KURDALÝ CAMÝÝ KARÞISI/TOROSLAR"},
new Eczane { Id = 589,Enlem = 36.8081990,Boylam = 34.6214920,TelefonNo = "3243361952",Adres = "ÝHSANÝYE MH. 121.CAD NO:159/A (KUVAYI MILLIYE) ",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ KARÞISI/AKDENÝZ",AdresTarifi ="ESKÝ DEVLET HASTANESÝ KARÞISI/AKDENÝZ"},
new Eczane { Id = 562,Enlem = 36.8048610,Boylam = 34.6322300,TelefonNo = "3242388684",Adres = "YENI MH. 5334 SK. NO:6",AdresTarifiKisa = "YENÝ MAH.SAÐLIK OCAÐI KARÞISI ",AdresTarifi ="YENÝ MAH.SAÐLIK OCAÐI KARÞISI BAÐ KUR ÝL MÜD ARKASI/AKDENÝZ"},
new Eczane { Id = 563,Enlem = 36.8063540,Boylam = 34.6327040,TelefonNo = "3242380380",Adres = "YENÝ MH. 5319 SK. NO:38/A",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ ACÝL SERVÝS KUZEYÝ",AdresTarifi ="TOROS DEVLET HASTANESÝ ACÝL CÝVARI ÝTFAÝYE YANI/AKDENÝZ"},
new Eczane { Id = 678,Enlem = 36.7840370,Boylam = 34.5895280,TelefonNo = "3243315411",Adres = "GÜVENEVLER.MAH.FORUM.YENISEHIR.MERSIN",AdresTarifiKisa = "FORUM MERSÝN AVM A BLOK 28/A/YENÝÞEHÝR",AdresTarifi ="FORUM MERSÝN AVM A BLOK 28/A/YENÝÞEHÝR"},
new Eczane { Id = 710,Enlem = 36.7856132,Boylam = 34.5399163,TelefonNo = "3243412510",Adres = "MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI ÇÝFTLÝKKÖY MAH. MÝMAR SÝNAN CADDESÝ",AdresTarifiKisa = "MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞIS",AdresTarifi ="MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI ÇÝFTLÝKKÖY MAH. MÝMAR SÝNAN CADDESÝ/YENÝÞEHÝR"},
new Eczane { Id = 508,Enlem = 36.8397220,Boylam = 34.6305740,TelefonNo = "3242240499",Adres = "GÜNEYKENT MAH.FARABI CAD.NO.14.TOROSLAR",AdresTarifiKisa = "7 NOLU SAÐLIK OCAÐI YANI/TOROSLAR",AdresTarifi ="FARABÝ CAD BELEDÝYE DÜKKANLARI GÜNEYKENT 7 NOLU SAÐLIK OCAÐI YANI/TOROSLAR"},
new Eczane { Id = 773,Enlem = 36.7351330,Boylam = 34.5138130,TelefonNo = "3243582500",Adres = "AKDENÝZ MH. 39607 SK. ÇEVÝK-1 PLAZA ALTI NO:19",AdresTarifiKisa = "SOLÝ CENTER ARKASI 2 NOLU ASM KARÞISI",AdresTarifi ="AKDENÝZ MH. SOLÝ CENTER ARKASI - BÝM MARKET SOKAÐI - ÇEVÝK 1 PLAZA - 2 NOLU ASM KARÞISI - MEZÝTLÝ/MEZÝTLÝ"},
new Eczane { Id = 614,Enlem = 36.7939993,Boylam = 34.6174615,TelefonNo = "3242370102",Adres = "ISTIKLAL CAD.ÇELIKLER APT.ALTI NO.199/18-19",AdresTarifiKisa = " IMC HASTANESÝ CÝVARI ÖZKAN ÞALGAM KARÞISI/AKDENÝZ",AdresTarifi ="ÝSTÝKLAL CD ÖZEL IMC HASTANESÝ CÝVARI ÖZKAN ÞALGAM KARÞISI/AKDENÝZ"},
new Eczane { Id = 711,Enlem = 36.7859270,Boylam = 34.5398470,TelefonNo = "3242904635",Adres = "ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. PARADÝSE HOMES SÝTESÝ C BLOK NO: 24/CD",AdresTarifiKisa = "MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞIS",AdresTarifi ="ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. (MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI) /YENÝÞEHÝR"},
new Eczane { Id = 509,Enlem = 36.8426390,Boylam = 34.6234960,TelefonNo = "3242372817",Adres = "HALKKENT MH. F.SULTAN MEHMET BULV. 25B/3",AdresTarifiKisa = "ESKÝ KADIN DOÐUM VE ÇOCUK HASTANESÝ KARÞISI",AdresTarifi ="HALKKENT MAH MERÝÞ ÝLERÝSÝ ESKÝ KADIN DOÐUM VE ÇOCUK HASTANESÝ KARÞISI/TOROSLAR"},
new Eczane { Id = 566,Enlem = 36.8063230,Boylam = 34.6328370,TelefonNo = "3242375070",Adres = "YENÝ MH. 5328 SK. NO:11/A-B",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ  YENÝ ACÝL KAPISI KARÞISI",AdresTarifi ="YENÝ MH. TOROS DEVLET HASTANESÝ  YENÝ ACÝL KAPISI KARÞISI/AKDENÝZ"},
new Eczane { Id = 680,Enlem = 36.7790720,Boylam =  34.575277,TelefonNo = "3243269184",Adres = "BARBAROS MAH.2148 SOK.NO20 YENISEHIR",AdresTarifiKisa = "YENÝÞEHÝR KAYMAKAMLIÐI KARÞISI ÇOCUK PARKI ARKASI ",AdresTarifi ="YENÝÞEHÝR KAYMAKAMLIÐI KARÞISI ÇOCUK PARKI ARKASI BARBAROS ASM KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 532,Enlem = 36.8139610,Boylam = 34.6283680,TelefonNo = "3243208960",Adres = "MERSIN ILI TOROSLAR ILÇESI SAGLIK MH.86019 SK.ISTANBUL AP.ALTI NO:6/A-B",AdresTarifiKisa = "TAÇGÜN HALISAHA BÝTÝÞÝÐÝ ÇEVÝK KUVVET CÝVARI",AdresTarifi ="SAÐLIK MH. 86019 SK TAÇGÜN HALISAHA BÝTÝÞÝÐÝ ÇEVÝK KUVVET CÝVARI/TOROSLAR"},
new Eczane { Id = 564,Enlem = 36.8064457,Boylam = 34.6356586,TelefonNo = "3242312080",Adres = "YENÝ MH. 5314 SK. NO:2",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ KARÞISI (ESKÝ SSK)",AdresTarifi ="TOROS DEVLET HASTANESÝ KARÞISI (ESKÝ SSK)"},
new Eczane { Id = 746,Enlem = 36.7573520,Boylam = 34.5384730,TelefonNo = "3243411412",Adres = "YENÝ MH. 33167 SK. NO:10/B -  MEZÝTLÝ",AdresTarifiKisa = "DÝMAX MOBÝLYA - TOYOTA PLAZA ARA SOKAÐI - MEZÝTLÝ",AdresTarifi ="YENÝ MH. 33167 SK. DÝMAX MOBÝLYA - TOYOTA PLAZA ARA SOKAÐI - MEZÝTLÝ"},
new Eczane { Id = 653,Enlem = 36.7852110,Boylam = 34.6146820,TelefonNo = "3243284375",Adres = "PIRIREIS MAH.1103.SOK.18/A YENISEHIR",AdresTarifiKisa = " ESKÝ PLAJ YOLU, 24 KASIM ÝLKOKULU ARKA SOKAÐI",AdresTarifi ="PÝRÝREÝS MH 1103 SK. ESKÝ PLAJ YOLU, 24 KASIM ÝLKOKULU ARKA SOKAÐI"},
new Eczane { Id = 775,Enlem = 36.7416020,Boylam = 34.5250720,TelefonNo = "3243580998",Adres = "MENDERES MAHALLESI CEVAT KUTLU CADDESI NO.11/E MEZILI MERSIN",AdresTarifiKisa = "NAFÝZ ÇOLAK ASM CÝVARI  MEZÝTLÝ ",AdresTarifi ="11. NOTERÝN DENÝZE DOÐRU 200 METRE  ÝLERÝSÝ NAFÝZ ÇOLAK ASM CÝVARI  MEZÝTLÝ /MEZÝTLÝ"},
new Eczane { Id = 567,Enlem = 36.8059820,Boylam = 34.6331660,TelefonNo = "3242379733",Adres = "YENÝ MH. 5328 SK. OSMAN BEY APT. NO:7/A",AdresTarifiKisa = "TOROS DEVLET HAST. ARKA SOK. YENÝ ACÝL KAPISý KARÞ",AdresTarifi ="YENÝ MH. TOROS DEVLET HASTANESÝ ARKA SOKAÐI YENÝ ACÝL KAPISI KARÞISI/AKDENÝZ"},
new Eczane { Id = 715,Enlem = 36.7862000,Boylam = 34.5395670,TelefonNo = "3245020010",Adres = "ÇÝFTLÝK KÖY MAHALLESÝ MÝMAR SÝNAN CADDESÝ PARADÝSE HOMES C BLOK NO:24CH",AdresTarifiKisa = "MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜTSL. HASTANESÝ KARÞISI",AdresTarifi ="MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI ÇÝFTLÝKKÖY MAH. MÝMAR SÝNAN CAD. PARADÝSE HOMES SÝT. C BLOK NO:24 CH / YENÝÞEHÝR"},
new Eczane { Id = 533,Enlem = 36.8261570,Boylam = 34.6252830,TelefonNo = "3243218998",Adres = "KURDALI MH.100041 SK.6/B",AdresTarifiKisa = "KURDALÝ CAMÝÝ CÝVARI SELAHATTÝN YANPAR ASM KARÞISI",AdresTarifi ="KURDALÝ MH. 100041 SK. KURDALÝ CAMÝÝ CÝVARI SELAHATTÝN YANPAR ASM KARÞISI/TOROSLAR"},
new Eczane { Id = 590,Enlem = 36.8096870,Boylam = 34.6222990,TelefonNo = "3243362660",Adres = "NUSRATIYE MH. 5026 SK. NO:44/C AKDENIZ MERSIN",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ ARKA SOKAÐI",AdresTarifi ="ESKÝ DEVLET HASTANESÝ ARKA SOKAÐI  SAÐLIK KURULU - KAN BANKASI KARÞISI/AKDENÝZ"},
new Eczane { Id = 776,Enlem = 36.7492830,Boylam = 34.5289610,TelefonNo = "3243583685",Adres = "MERKEZ MAHALESI KARAOGLAN CADDESI NO.5/A MEZITLI MERSIN",AdresTarifiKisa = "MEZÝTLÝ MERKEZ MH. 2.NOLU ASM KARÞISI ",AdresTarifi ="MEZÝTLÝ MERKEZ MH. 2.NOLU ASM KARÞISI MEZÝTLÝ BELEDÝYESÝ CÝVARI"},
new Eczane { Id = 568,Enlem = 36.8068940,Boylam = 34.6353930,TelefonNo = "3242324094",Adres = "YENI MAH.5314 SK.MELTEM APT.ALTI NO:8/2 AKDENIZ/MERSIN",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ KARÞISI (ESKÝ SSK )/AKDENÝZ",AdresTarifi ="TOROS DEVLET HASTANESÝ KARÞISI ( ESKÝ SSK )/AKDENÝZ"},
new Eczane { Id = 615,Enlem = 36.7977990,Boylam = 34.6259700,TelefonNo = "3242373086",Adres = "ISTIKLAL CD. NO:75/A AKDENIZ/MERSIN",AdresTarifiKisa = "ÝSTÝKLAL CD NO 75/A  NOBEL OTELÝ YANI/AKDENÝZ",AdresTarifi ="ÝSTÝKLAL CD NO 75/A  NOBEL OTELÝ YANI/AKDENÝZ"},
new Eczane { Id = 535,Enlem = 36.8196334,Boylam = 34.6152043,TelefonNo = "3243227343",Adres = "TOZKOPARAN MH. 87045 SK. NO:23/B",AdresTarifiKisa = "TOZKOPARAN SAÐLIK OCAÐI KARÞISI ",AdresTarifi ="TOZKOPARAN MH. TOZKOPARAN SAÐLIK OCAÐI KARÞISI PALMÝYE DÜÐÜN SALONU KARÞI SOKAÐI ÇUKUROVA ÝLKÖÐRETÝM OKULU CÝVARI / TOROSLAR"},
new Eczane { Id = 635,Enlem = 36.8180620,Boylam = 34.6315320,TelefonNo = "3242357974",Adres = "GÜNDOGDU MAH.1 NOLU SAG.OC.KARS.5790 SK.NO:19 AKDENIZ/MERSIN",AdresTarifiKisa = "FEN LÝSESÝ CÝVARI 1 NOLU SAÐLIK OCAÐI KRÞ/AKDENÝZ",AdresTarifi ="FEN LÝSESÝ CÝVARI 1 NOLU SAÐLIK OCAÐI KRÞ/AKDENÝZ"},
new Eczane { Id = 536,Enlem = 36.8250076,Boylam = 34.6004830,TelefonNo = "3243222255",Adres = "AKBELEN MH.TOROSLAR CD. NO:6/E TOROSLAR/MERSIN",AdresTarifiKisa = "AKBELEN SAÐLIK OCAÐI KARÞSI KORAY AYDIN STADI ALTý",AdresTarifi ="AKBELEN MH. AKBELEN SAÐLIK OCAÐI KARÞISI KORAY AYDIN STADI ALTI /TOROSLAR"},
new Eczane { Id = 616,Enlem = 36.7950770,Boylam = 34.6237710,TelefonNo = "3242393444",Adres = "KIREMITHANE MH.4406 DR. CEMALETTIN TANRIÖVER SK. FAKS AP. ALTI NO:10/C",AdresTarifiKisa = "ÖZGÜR ÇOCUK PARKI ARKASI  ÖZEL DOÐUÞ HAST. KARÞISI",AdresTarifi ="KÝREMÝTHANE MH. ÖZGÜR ÇOCUK PARKI ARKASI  ÖZEL DOÐUÞ HASTANESÝ KARÞISI/AKDENÝZ"},
new Eczane { Id = 591,Enlem = 36.8099035,Boylam = 34.6226827,TelefonNo = "3243364906",Adres = "NUSRATÝYE MH. 5021 SK. NO:15/3",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ ARKA SOKAÐI /AKDENÝZ",AdresTarifi ="ESKÝ DEVLET HASTANESÝ ARKA SOKAÐI /AKDENÝZ"},
new Eczane { Id = 617,Enlem = 36.7916780,Boylam = 34.6238340,TelefonNo = "3242315854",Adres = "KÜLTÜR MH. ATATÜRK CD. NO:35",AdresTarifiKisa = "ATATÜRK CD. ALTINANAHTAR APARTMAN ALTI ÇAMLIBEL",AdresTarifi ="ATATÜRK CD. ALTINANAHTAR APARTMAN ALTI ÇAMLIBEL/AKDENÝZ"},
new Eczane { Id = 654,Enlem = 36.7828734,Boylam = 34.6061372,TelefonNo = "3243254191",Adres = "PALMIYE MAH.1207.SOK.YALÇINÖZ.APT.ALTI.NO.16/A. YENISEHIR.MERSIN",AdresTarifiKisa = "PALMÝYE MH PÝRÝREÝS ÝLKÖÐRETÝM OKULU YOLU ÝLERÝSÝ ",AdresTarifi ="PALMÝYE MH PÝRÝREÝS ÝLKÖÐRETÝM OKULU YOLU ÝLERÝSÝ SAHÝLDEN BEÞÝKTAÞ KAPISI KARÞI YOLU ÝLERÝSÝ YENÝÞEHÝR"},
new Eczane { Id = 537,Enlem = 36.8179110,Boylam = 34.6298850,TelefonNo = "3243207473",Adres = "ÇIFTÇILER CAD.97/ASELÇUKLAR TOROSLAR/MERSIN",AdresTarifiKisa = "(KURDALÝ YOLU) TÜRKÝYE PETROLLERÝ KARÞ.TOROSLAR",AdresTarifi ="SELÇUKLAR MAH ÇÝFTÇÝLER CAD (KURDALÝ YOLU) TÜRKÝYE PETROLLERÝ KARÞISI ÇEVÝK KUVVET CÝVARI/TOROSLAR"},
new Eczane { Id = 655,Enlem = 36.7858036,Boylam = 34.6005056,TelefonNo = "3243285802",Adres = "CUMHURIYET MH. 16 CD.4/B",AdresTarifiKisa = "POZCU TELEKOM  CÝVARI 45 EVLER YOLU",AdresTarifi ="CUMHURÝYET MH. 16. CD. ÝL TELEKOM MÜDÜRLÜÐÜ YANI - 45 EVLER YOLU SUPHÝ ÞALGAM BÝTÝÞÝÐÝ YENÝÞEHÝR"},
new Eczane { Id = 716,Enlem = 36.7982060,Boylam = 34.5714440,TelefonNo = "3243284282",Adres = "50. YIL MH. 27140 SK. NO:2/A",AdresTarifiKisa = "METRO ALIÞ VERÝÞ MERKEZÝ ARKASI - 50. YIL",AdresTarifi ="METRO ALIÞ VERÝÞ MERKEZÝ ARKASI - 50. YIL POLÝS KARAKOLU CÝVARI 50. YIL ASM KARÞISI YENÝÞEHÝR/YENÝÞEHÝR"},
new Eczane { Id = 749,Enlem = 36.7579230,Boylam = 34.5458420,TelefonNo = "3243587867",Adres = "MEZITLI ILÇESI FATIH MH.28 SK.MELTEM AP. ALTI.NO:2",AdresTarifiKisa = "MEZÝTLÝ HUNDAÝ ARKASI SAHÝL SAÐLIK OCAÐI CÝVARI",AdresTarifi ="FATÝH MH. MEZÝTLÝ HUNDAÝ ARKASI SAHÝL SAÐLIK OCAÐI CÝVARI/MEZÝTLÝ"},
new Eczane { Id = 750,Enlem = 36.7631620,Boylam = 34.5478980,TelefonNo = "3243575811",Adres = "ATATÜRK MAH.VALÝ ÞENOL ENGÝN CAD.NO:8 A/20 MEZITLI/MERSIN",AdresTarifiKisa = " VALÝ ÞENOL ENGÝN CD.ORTADOÐU HAST. KÖÞESÝ MEZÝTLÝ",AdresTarifi ="VALÝ ÞENOL ENGÝN CD.ÖZEL ORTADOÐU HASTANESÝ KÖÞESÝ MEZÝTLÝ"},
new Eczane { Id = 751,Enlem = 36.7560770,Boylam = 34.5377100,TelefonNo = "3243570105",Adres = "YENÝ MH. KELVELÝ CD. ZÝYA ÜNSAL APT. ALTI NO:: 23/A   MEZÝTLÝ",AdresTarifiKisa = "MEZÝTLÝ TOROS DEVLET HAST.SEMT POLÝKLÝNÝÐÝ KARÞISI",AdresTarifi ="TOROS DEVLET HASTANESÝ SEMT POLÝKLÝNÝÐÝ KARÞISI - KALEYOLU MEZÝTLÝ"},
new Eczane { Id = 778,Enlem = 36.7493360,Boylam = 34.5291970,TelefonNo = "3245020016",Adres = "MERKEZ MH. 52030 SK. DURDU ULUÞ APT. NO.6 Z13",AdresTarifiKisa = "MEZÝTLÝ BELEDÝYESÝ KARÞISI MERKEZ ASM YANI",AdresTarifi ="MEZÝTLÝ BELEDÝYESÝ KARÞISI ALDA MARKET ARKASI MERKEZ ASM YANI - MEZÝTLÝ/MEZÝTLÝ"},
new Eczane { Id = 510,Enlem = 36.8489600,Boylam = 34.6075910,TelefonNo = "3242230207",Adres = "KORUKENT MH. 96017 SK. NO:20",AdresTarifiKisa = "ÞEHÝR HAST. CÝVARI KORUKENT MH. MUHTARLIÐI KARÞISI",AdresTarifi ="KORUKENT MH. 96017 SK. ÞEHÝR HASTANESÝ CÝVARI KORUKENT MH. MUHTARLIÐI KARÞISI"},
new Eczane { Id = 656,Enlem = 36.7833990,Boylam = 34.6039080,TelefonNo = "3243290585",Adres = "GAZI MAH.1304.SOK.NO.12/A YENISEHIR",AdresTarifiKisa = "MUÐDAT CAMÝ CÝVARI, GAZÝ ASM KARÞISI",AdresTarifi ="MUÐDAT CAMÝ CÝVARI, GAZÝ ASM KARÞISI - POZCU'"},
new Eczane { Id = 538,Enlem = 36.8122550,Boylam = 34.6195570,TelefonNo = "3243220704",Adres = "KÜVAYI MILLIYE CADDESI NO.154.TOROSLAR MERSIN",AdresTarifiKisa = "HASTANE CD ESKÝ TOROSLAR BELEDÝYESÝ KARÞISI.",AdresTarifi ="HASTANE CD ESKÝ TOROSLAR BELEDÝYESÝ KARÞISI/TOROSLAR"},
new Eczane { Id = 592,Enlem = 36.8100900,Boylam = 34.6222500,TelefonNo = "3243363634",Adres = "NUSRATÝYE MH. 5021 SK. ÞAHÝN APT ALTI NO:53/1",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ ESKÝ ACÝL KARÞISI",AdresTarifi ="ESKÝ DEVLET HASTANESÝ ESKÝ ACÝL KARÞISI/AKDENÝZ"},
new Eczane { Id = 681,Enlem = 36.7858300,Boylam = 34.5928860,TelefonNo = "3243250192",Adres = "GÜVENEVLER MAH. CADDE NO:13 YENISEHIR MERSIN",AdresTarifiKisa = "GÜVENEVLER MH1. CAD. GÜVEN SÝTESÝ KARÞISI POZCU",AdresTarifi ="GÜVENEVLER MH1. CAD. GÜVEN SÝTESÝ KARÞISI POZCU/YENÝÞEHÝR"},
new Eczane { Id = 779,Enlem = 36.7411090,Boylam = 34.5253560,TelefonNo = "3243588856",Adres = "MENDERES MAHALLESI CEVAT KUTLU CADDESI YALI KENT AP.NO.37/22/C MEZITLI MERSIN",AdresTarifiKisa = " 11. NOTER SOKAÐI NAFÝZ ÇOLAK SAG.OC.YANI.MEZÝTLÝ",AdresTarifi ="LÝDER GEDÝKLER KARÞISI 11. NOTER SOKAÐI SAHÝLDEN DENÝZHAN 2 KARÞI CD. ÜZERÝ/MEZÝTLÝ"},
new Eczane { Id = 825,Enlem = 36.7990250,Boylam = 34.6251280,TelefonNo = "3242385119",Adres = "ÇANKAYA MAHALLE 4738 SOKAK NO 22. AKDENIZ",AdresTarifiKisa = "ÇANKAYA MH 4738 SK NOBEL OTELÝ KRÞ SOKAÐI  AKDENÝZ",AdresTarifi ="ÇANKAYA MH 4738 SK NOBEL OTELÝ KRÞ SOKAÐI NO 22/AKDENÝZ"},
new Eczane { Id = 657,Enlem = 36.7860200,Boylam = 34.6044200,TelefonNo = "3243294690",Adres = "GMK BULVARI PALMIYE MAH.EREN AP.NO.263/32. YENISEHIR",AdresTarifiKisa = "ESKÝ DUYGU TIP MERKEZÝ YANI TOPÇULAR DURAÐI",AdresTarifi ="ESKÝ DUYGU TIP MERKEZÝ YANI TOPÇULAR DURAÐI/YENÝÞEHÝR"},
new Eczane { Id = 682,Enlem = 36.7900090,Boylam = 34.5875560,TelefonNo = "3243363569",Adres = "GÜVENEVLER MH. 1953 SK. NO:14 A/B",AdresTarifiKisa = "FORUM YAÞAM HAST. ACÝL KARÞISI",AdresTarifi ="ÖZEL FORUM HASTANESÝ ACÝL SERVÝSÝ KARÞISI"},
new Eczane { Id = 569,Enlem = 36.8060730,Boylam = 34.6330850,TelefonNo = "3242380016",Adres = "Yeni Mh. 5328 Sk. No: 9/A",AdresTarifiKisa = "TOROS DEVLET HAST. ARKA SOKAÐI YENÝ ACÝL KARÞISI",AdresTarifi ="YENÝ MH. TOROS DEVLET HASTANESÝ ARKA SOKAÐI YENÝ ACÝL KAPISI KARÞISI/AKDENÝZ"},
new Eczane { Id = 593,Enlem = 36.8078420,Boylam = 34.6239200,TelefonNo = "3243575857",Adres = "NUSRATÝYE MH. 5020 SK. NO:17/A",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ CÝVARI OPET ARKASI AKDENÝZ",AdresTarifi ="ESKÝ DEVLET HASTANESÝ CÝVARI OPET ARKASI AKDENÝZ"},
new Eczane { Id = 512,Enlem = 36.8396980,Boylam = 34.6303550,TelefonNo = "3242233030",Adres = "FARABÝ CD. NO:29 GÜNEYKENT",AdresTarifiKisa = "GÜNEYKENT ÇARÞISI NO:29 - 7 NOLU SAÐLIK OCAÐI YANI",AdresTarifi ="FARABÝ CD GÜNEYKENT ÇARÞISI NO:29 - 7 NOLU SAÐLIK OCAÐI YANI/AKDENÝZ"},
new Eczane { Id = 513,Enlem = 36.8428770,Boylam = 34.6232710,TelefonNo = "3242230818",Adres = "HALKKENT MAH.2996 ADA.L BLOK NO:15 TOROSLAR ",AdresTarifiKisa = "ESKÝ KADIN DOÐUM VE ÇOCUK HASTANESÝ  KARÞISI",AdresTarifi ="ESKÝ KADIN DOÐUM VE ÇOCUK HASTANESÝ ÇAPRAZ KARÞISI HALKKENT MH. TOROSLAR"},
new Eczane { Id = 594,Enlem = 36.8062440,Boylam = 34.6279400,TelefonNo = "3242371598",Adres = "ÇAKMAK CAD. NO : 77",AdresTarifiKisa = "ÇAKMAK CADDESÝ  BOLKEPÇE LOKANTASI KARÞISI",AdresTarifi ="ÇAKMAK CADDESÝ NO:79 BEÞYOL / BOLKEPÇE LOKANTASI KARÞISI/AKDENÝZ"},
new Eczane { Id = 658,Enlem = 36.7894770,Boylam = 34.6066050,TelefonNo = "3243275848",Adres = "CUMHURÝYET MH. 1617 SK. MEHMETOÐLU APT. NO:3/B",AdresTarifiKisa = "CUMHURÝYET MH. TULUMBA KAVÞAÐI 50 METRE KUZEYÝ",AdresTarifi ="CUMHURÝYET MH. 1617 SK. TULUMBA KAVÞAÐI 50 METRE KUZEYÝ CÝÐERCÝ HAKAN VE DONAT TÝCARET ARKASI - POZCU/YENÝÞEHÝR"},
new Eczane { Id = 781,Enlem = 36.7469908,Boylam = 34.5265523,TelefonNo = "3243593233",Adres = "MENDERES MH. GMK BULV. BEKO APT. ALTI 725/C",AdresTarifiKisa = "KUYULUK KAVÞAÐI KARÞISI VAKIFBANK YANI MEZÝTLÝ/MEZ",AdresTarifi ="KUYULUK KAVÞAÐI KARÞISI VAKIFBANK YANI MEZÝTLÝ/MEZÝTLÝ"},
new Eczane { Id = 570,Enlem = 36.8068280,Boylam = 34.6355220,TelefonNo = "3242335072",Adres = "YENI MAH.5314 SOK. MELTEM APT. ALTI NO:8 AKDENÝZ",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ (ESKÝ SSK) KARÞISI/AKDENÝZ",AdresTarifi ="TOROS DEVLET HASTANESÝ (ESKÝ SSK) KARÞISI/AKDENÝZ"},
new Eczane { Id = 595,Enlem = 36.8056810,Boylam = 34.6230680,TelefonNo = "3243361048",Adres = "KUVAIMIILLIYE CADDESI NO.124 AKDENIZ.MERSIN",AdresTarifiKisa = "HASTANE CADDESÝ KURUÇEÞME KARÞISI/AKDENÝZ",AdresTarifi ="HASTANE CADDESÝ KURUÇEÞME KARÞISI/AKDENÝZ"},
new Eczane { Id = 665,Enlem = 36.7925630,Boylam = 34.5826280,TelefonNo = "3243369980",Adres = "MENTEÞ MH.25161 SK. NO:27 AKADEMÝ HASTANESÝ YANI",AdresTarifiKisa = "MENTEÞ MAHALLESÝ ÖZEL AKADEMÝ HASTANESÝ YANI ",AdresTarifi ="MENTEÞ MAHALLESÝ ÖZEL AKADEMÝ HASTANESÝ YANI "},
new Eczane { Id = 659,Enlem = 36.7835080,Boylam = 34.6029690,TelefonNo = "3243265620",Adres = "GAZI MAH.1307 SOK.NO.16/B                YENISEHIR",AdresTarifiKisa = "GAZÝ MH. 1307 SK MUÐDAT CAMÝ KARÞISI",AdresTarifi ="GAZÝ MH. 1307 SK MUÐDAT CAMÝ KARÞISI BÝM MARKET ARKASI/YENÝÞEHÝR"},
new Eczane { Id = 539,Enlem = 36.8185790,Boylam = 34.6294650,TelefonNo = "3243200202",Adres = "SELÇUKLAR MH.206 CD.NO:146/TOROSLAR MERSIN",AdresTarifiKisa = "SELÇUKLAR MH. 206. CD.SELÇUKLAR ASM KARÞISI ",AdresTarifi ="SELÇUKLAR MH. 206. CD.SELÇUKLAR ASM KARÞISI AKABE CAMÝÝ CÝVARI/TOROSLAR"},
new Eczane { Id = 540,Enlem = 36.8381480,Boylam = 34.5984400,TelefonNo = "3243251404",Adres = "ÇUKUROVA MH. 85119 SK. NO.132",AdresTarifiKisa = " ÇUKUROVA SAÐLIK OCAÐI KARÞISI/TOROSLAR",AdresTarifi ="ÇUKUROVA MH. 85119 SK. - ÇUKUROVA SAÐLIK OCAÐI KARÞISI/TOROSLAR"},
new Eczane { Id = 597,Enlem = 36.8080099,Boylam = 34.6180385,TelefonNo = "3243365717",Adres = "IHSANIYE MAHALLESI HAVUZLAR CADDESI NO.74",AdresTarifiKisa = "ESKÝ DEVLET HAST. KARÞI SOKAÐI GÖZDE TIP MRK YAN",AdresTarifi ="ESKÝ DEVLET HASTANESÝ KARÞI SOKAÐI  ÝHSANÝYE MH HAVUZLAR CD. GÖZDE TIP MRK YANI/AKDENÝZ"},
new Eczane { Id = 683,Enlem = 36.7925642,Boylam = 34.5827809,TelefonNo = "3249999745",Adres = "MENTEÞ MH. BARBAROS BULV. NO:107/B",AdresTarifiKisa = "",AdresTarifi ="ÖZEL AKADEMÝ HASTANESÝ YANI"},
new Eczane { Id = 541,Enlem = 36.8249269,Boylam = 34.6003380,TelefonNo = "3243201120",Adres = "AKBELEN MH. TOROSLAR CD. NO:6/B1-B2-B3",AdresTarifiKisa = "AKBELEN MH. TOROSLAR CD. NO:6",AdresTarifi ="AKBELEN KONUTEVLERÝ KORAY AYDIN STADI ALTI AKBELEN SAÐLIK OCAÐI KARÞISI/TOROSLAR"},
new Eczane { Id = 619,Enlem = 36.7960134,Boylam = 34.6240593,TelefonNo = "3242388985",Adres = "ISTIKLAL CADDESI KIREMITHANE MAHALLESI 4413 SOKAK.NO.12/34 AKDENIZ.MERSIN",AdresTarifiKisa = "ÝSTÝKLAL CD ÖZGÜR ÇOCUK PARKI YANI/AKDENÝZ",AdresTarifi ="ÝSTÝKLAL CD ÖZGÜR ÇOCUK PARKI YANI/AKDENÝZ"},
new Eczane { Id = 782,Enlem = 36.7464175,Boylam = 34.5376006,TelefonNo = "3243597880",Adres = "Viranþehir Mh. Viranþehir Cd. No:39/D",AdresTarifiKisa = "VÝRANÞEHÝR MH.VÝRANÞEHÝR CD NO:39/D /MEZÝTLÝ",AdresTarifi ="VÝRANÞEHÝR MH.VÝRANÞEHÝR CD NO:39/D /MEZÝTLÝ"},
new Eczane { Id = 571,Enlem = 36.8057740,Boylam = 34.6333050,TelefonNo = "3243289050",Adres = "YENÝ MH. 5328 SK. ÖZBEY APT. NO:1/A",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ ACÝL KAPISI KARÞISI ",AdresTarifi ="TOROS DEVLET HASTANESÝ ACÝL KAPISI KARÞISI  SOL ÇAPRAZ KÖÞE /AKDENÝZ"},
new Eczane { Id = 514,Enlem = 36.8512210,Boylam = 34.6110120,TelefonNo = "3242233770",Adres = "KORUKENT MH. 96004 SK. NO:13/1",AdresTarifiKisa = " MERSÝN ÞEHÝR HASTANESÝ CÝVARI TOKÝ EVLERÝ,KARÞISI",AdresTarifi ="KORUKENT MH. 96004 SK. MERSÝN ÞEHÝR HASTANESÝ CÝVARI TOKÝ EVLERÝ, KARÞISI TOKÝ CAMÝÝ ÜZERÝ"},
new Eczane { Id = 684,Enlem = 36.7854340,Boylam = 34.5928900,TelefonNo = "3243254891",Adres = "GÜVENEVLER MAH.1911 SK.ÇALIKOGLU APT.NO:25/12-YENISEHIR-MERSIN",AdresTarifiKisa = "GÜVEN SÝTESÝ CÝVARI ALANYA FIRINI SOKAÐI/YENÝÞEHÝR",AdresTarifi ="GÜVENEVLER MH 1902 SOK. NO 25 GÜVEN SÝTESÝ CÝVARI ALANYA FIRINI SOKAÐI/YENÝÞEHÝR"},
new Eczane { Id = 545,Enlem = 36.8262290,Boylam = 34.6250660,TelefonNo = "3243205590",Adres = "KURDALI MAH. 10041 SOK. NO:5 TOROSLAR",AdresTarifiKisa = "SELAHATTÝN YANPAR SAÐLIK OCAÐI YANI/TOROSLAR",AdresTarifi ="KURDALÝ MAH 100041 SK NO 5 SELAHATTÝN YANPAR SAÐLIK OCAÐI YANI/TOROSLAR"},
new Eczane { Id = 717,Enlem = 36.7804870,Boylam = 34.5574340,TelefonNo = "3242904485",Adres = "BATIKENT MH. 2635 SK. YAÐMUR SULTAN KONUTLARI C BLOK ALTI NO:25",AdresTarifiKisa = " MERSÝN VALÝLÝK BÝNASI KUZEYÝ - MEVLANA ASM CÝVARI",AdresTarifi ="BATIKENT MH. ÞAHÝN TEPESÝ - NECATÝ BOLKAN Ý.Ö.OKULU YANI MERSÝN VALÝLÝK BÝNASI KUZEYÝ - MEVLANA ASM CÝVARI YENÝÞEHÝR"},
new Eczane { Id = 687,Enlem = 36.7793040,Boylam = 34.5748770,TelefonNo = "3243254457",Adres = "BARBAROS MAH MAH.2121 SOKAK NO.2/A YENISEHIR",AdresTarifiKisa = "YENÝÞEHÝR KAYMAKAMLIÐI KARÞISI BARBAROS ASM YANI",AdresTarifi ="YENÝÞEHÝR KAYMAKAMLIÐI KARÞISI BARBAROS ÇOCUK PARKI ARKASI 15 NOLU SAÐLIK OCAÐI YANI/YENÝÞEHÝR"},
new Eczane { Id = 546,Enlem = 36.8168600,Boylam = 34.6195440,TelefonNo = "3243233383",Adres = "TOZKOPARAN MH. 87002 SK. NO:33 TOROSLAR",AdresTarifiKisa = "TOZKOPARAN MH. HASAN AKEL LÝSESÝ CÝVARI",AdresTarifi =" TOZKOPARAN MH. HASAN AKEL LÝSESÝ CÝVARI ÇARÞAMBA PAZARI SOKAÐI"},
new Eczane { Id = 596,Enlem = 36.8108630,Boylam = 34.6223590,TelefonNo = "3243368218",Adres = "NUSRATIYE MAH.GMK BULV.M.MUSTAFA EFENDI SIT.C BLOK 119/E AKDENIZ/MERSIN",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ BÝTÝÞÝÐÝ ÝÞ BANKASI KARÞISI",AdresTarifi ="ESKÝ DEVLET HASTANESÝ BÝTÝÞÝÐÝ ÇEVRE YOLU (GMK) TARAFI ÝÞBANKASI KARÞISI - YAYA ÜST GEÇÝDÝ YANI/AKDENÝZ"},
new Eczane { Id = 516,Enlem = 36.8426390,Boylam = 34.6234960,TelefonNo = "3242237061",Adres = "Halkkent Mh. Fatih Sultan Mehmet Bulv. A Blok No:1",AdresTarifiKisa = "ESKÝ KADIN DOÐUM VE ÇOCUK HAS. KARÞISI/TOROSLAR",AdresTarifi ="HALKKENT MAH. ESKÝ  KADIN DOÐUM VE ÇOCUK HAS. KARÞISI/TOROSLAR"},
new Eczane { Id = 691,Enlem = 36.7832833,Boylam = 34.5920555,TelefonNo = "3243268033",Adres = " YENÝÞEHÝR HASTANESÝ ARKASI (GÜVENEVLER MH.) YENÝÞEHÝR",AdresTarifiKisa = "ÖZEL YENÝÞEHÝR HASTANESÝ ARKASI (FORUM AVM CÝVARI)",AdresTarifi ="ÖZEL YENÝÞEHÝR HASTANESÝ ARKASI (FORUM AVM CÝVARI)"},
new Eczane { Id = 718,Enlem = 36.7709311,Boylam = 34.5628202,TelefonNo = "3243412144",Adres = "G.M.K. BULVARI EGRIÇAM MAH.NOÇ.568/A YENISEHIR",AdresTarifiKisa = "UZMAN ATA TIP MRKZ VE SULTAÞA OTELÝ KARÞISI",AdresTarifi ="GMK BULV. UZMAN ATA TIP MRKZ VE SULTAÞA OTELÝ KARÞISI EÐRÝÇAM/YENÝÞEHÝR"},
new Eczane { Id = 572,Enlem = 36.8063230,Boylam = 34.6328370,TelefonNo = "3242370500",Adres = "YENÝMAHALLE 5328 SOKAK FAHRÝYE ABLA APT. 11/B AKDENÝZ MERSÝN",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ  ACÝL KARÞISI",AdresTarifi ="TOROS DEVLET HASTANESÝ  ACÝL KARÞISI"},
new Eczane { Id = 692,Enlem = 36.7792690,Boylam = 34.5750540,TelefonNo = "3243282333",Adres = "BARBAROS MAHALLESÝ 2148 SOKAK NO:22/B",AdresTarifiKisa = " BARBAROS ASM KARÞISI / YENÝÞEHÝR",AdresTarifi ="BARBAROS MH. YENÝÞEHÝR KAYMAKAMLIÐI KARÞISI - ÇOCUK PARKI ARKASI - BARBAROS ASM KARÞISI / YENÝÞEHÝR"},
new Eczane { Id = 517,Enlem = 36.8367167,Boylam = 34.6102022,TelefonNo = "3242241809",Adres = "MERSIN ILI TOROSLAR ILÇESI ÇAGDASKENT MH. 229 CD. NO:10/7",AdresTarifiKisa = "ERZÝNCANLILAR SÝTESÝ KARÞISI / TOROSLAR",AdresTarifi ="ÇAÐDAÞKENT MH 229. CD. MAKRO MARKET CÝVARI ERZÝNCANLILAR SÝTESÝ KARÞISI / TOROSLAR/TOROSLAR"},
new Eczane { Id = 660,Enlem = 36.7827040,Boylam = 34.6043000,TelefonNo = "3243274541",Adres = "GAZI MAH.1304 SOK.SELIM BEY AP.NO.6/A.YENISEHIR",AdresTarifiKisa = "MUÐDAT CAMÝÝ CÝVARI 9 NOLU SAÐLIK OCAÐI KARÞISI ",AdresTarifi ="MUÐDAT CAMÝÝ CÝVARI 9 NOLU SAÐLIK OCAÐI KARÞISI BÝM MARKET ARKASI POZCU/YENÝÞEHÝR"},
new Eczane { Id = 548,Enlem = 36.8261510,Boylam = 34.6252270,TelefonNo = "3243370084",Adres = "KURDALÝ MH. 100041 SK. NO:6/A",AdresTarifiKisa = " SELAHATTÝN YANPAR ASM KARÞISI/TOROSLAR",AdresTarifi ="KURDALÝ MH. 100041 SK. SELAHATTÝN YANPAR ASM KARÞISI/TOROSLAR"},
new Eczane { Id = 573,Enlem = 36.8070710,Boylam = 34.6352780,TelefonNo = "3242335624",Adres = "YENI MAH.5314 SOK.NO.10 AKDENIZ",AdresTarifiKisa = "TOROS DEVLET HASTANESÝ KARÞISI ( ESKÝ SSK )/AKDENÝ",AdresTarifi ="TOROS DEVLET HASTANESÝ KARÞISI ( ESKÝ SSK )/AKDENÝZ"},
new Eczane { Id = 630,Enlem = 36.8254350,Boylam = 34.6419190,TelefonNo = "3242355581",Adres = "ÞEVKET SÜMER MAH. 156. CAD. NO:21  AKDENÝZ",AdresTarifiKisa = "ÞEVKET SÜMER MH SÝTELER KARAKOLU KARÞISI / AKDENÝZ",AdresTarifi ="ÞEVKET SÜMER MH SÝTELER KARAKOLU KARÞISI / AKDENÝZ"},
new Eczane { Id = 785,Enlem = 36.7190860,Boylam = 34.4860625,TelefonNo = "3244814797",Adres = "GMK BULV. EBRU SÝTESÝ B BLOK NO:14 DAVULTEPE",AdresTarifiKisa = "GMK BULV. DAVULTEPE BÝM YANI  - DAVULTEPE/MEZÝTLÝ",AdresTarifi ="GMK BULV. DAVULTEPE BÝM YANI  - DAVULTEPE/MEZÝTLÝ"},
new Eczane { Id = 693,Enlem = 36.7833349,Boylam = 34.5919858,TelefonNo = "3243363079",Adres = "GÜVENEVLER MH. 1908 SK. ÜMÝT ATP. NO:8/A",AdresTarifiKisa = "ÖZEL YENÝÞEHÝR HASTANESÝ ARKA SOKAÐI/YENÝÞEHÝR",AdresTarifi ="GÜVENEVLER MH. 1908 SK. NO:8 ÖZEL YENÝÞEHÝR HASTANESÝ ARKA SOKAÐI/YENÝÞEHÝR"},
new Eczane { Id = 753,Enlem = 36.7580250,Boylam = 34.5456210,TelefonNo = "3243598636",Adres = "FATIH MAH.HUNDAI ARKASI 28.SOK.NO MELTEM AP.MEZITLI",AdresTarifiKisa = "HYUNDAÝ ARKASI SAHÝL SAÐLIK OCAÐI BÝTÝÞÝÐÝ/MEZÝTLÝ",AdresTarifi ="HYUNDAÝ ARKASI SAHÝL SAÐLIK OCAÐI BÝTÝÞÝÐÝ/MEZÝTLÝ"},
new Eczane { Id = 598,Enlem = 36.8075860,Boylam = 34.6184420,TelefonNo = "3243363590",Adres = "Ýhsaniye Mahallesi Sait Çiftçi Cd.  No:19/E   Akdeniz /MERSÝN",AdresTarifiKisa = "Su Hastanesi (Eski Gözde Hastanesi) Karþýsý",AdresTarifi ="Su Hastanesi (Eski Gözde Hastanesi) karþýsý"},
new Eczane { Id = 694,Enlem = 36.7897859,Boylam = 34.5875678,TelefonNo = "3243252211",Adres = "YENISEHIR ILÇESI GÜVENEVLER MH.20 CD.1953 SK.NO:25 MERSIN",AdresTarifiKisa = "FORUM YAÞAM HASTANESÝ ACÝL KARÞISI ",AdresTarifi ="FORUM YAÞAM HASTANESÝ ACÝL KARÞISI 1953 SK. NO:25  YENÝÞEHÝR/YENÝÞEHÝR"},
new Eczane { Id = 549,Enlem = 36.8240273,Boylam = 34.6036559,TelefonNo = "3243225270",Adres = "YUSUF KILIÇ MH.641 SK.NO:53/1 TOROSLAR/MERSIN",AdresTarifiKisa = "YUSUF KILIÇ MAH. 217.CAD AYIÞIÐI DÜÐÜN SALONU YANI",AdresTarifi ="YUSUF KILIÇ MAH. 217. CAD AYIÞIÐI DÜÐÜN SALONU YANI"},
new Eczane { Id = 819,Enlem = 36.7704810,Boylam = 34.5623980,TelefonNo = "3243370045",Adres = "GMK BULVARI NO 529/A SULTAÞA OTEL VE UZMAN ATA TIP MERKEZÝ YAKINI DSÝ KARÞISI /YENÝÞEHÝR",AdresTarifiKisa = "GMK BULV. TOLGA SÝTESÝ A BLOK NO:20",AdresTarifi ="GMK BULV. TOLGA SÝTESÝ A BLOK NO:20"},
new Eczane { Id = 620,Enlem = 36.7908560,Boylam = 34.6193880,TelefonNo = "3242316273",Adres = "HAMIDIYE MAH.CENGIZ TOPEL CAD.17/B AKDENIZ ILÇESI-MERSIN",AdresTarifiKisa = "ÇAMLIBEL ÞOK MARKET CÝVARI ",AdresTarifi ="CENGÝZ TOPEL CD ÇAMLIBEL ÞOK MARKET CÝVARI SALÝM YILMAZ L.KÖÞE/AKDENÝZ"},
new Eczane { Id = 720,Enlem = 36.7756830,Boylam = 34.5377920,TelefonNo = "3242905590",Adres = "ÇÝFTLÝKKÖY MH. ÜNÝVERSÝTE CD. ÖZLEM ÜNÝVERSÝTE KONUTLARI NO:36/BK",AdresTarifiKisa = "ÜNÝVERSÝTE CD. VE ÝSTEMÝHAN TALAY BULVARI KAVÞAÐI",AdresTarifi ="ÇÝFTLÝKKÖY MH. ÖZLEM ÜNÝVERSÝTE KONUTLARI 2 - ÜNÝVERSÝTE CD. VE ÝSTEMÝHAN TALAY BULVARI KAVÞAÐI/YENÝÞEHÝR"},
new Eczane { Id = 599,Enlem = 36.8094490,Boylam = 34.6208790,TelefonNo = "3243375354",Adres = "ÝHSANÝYE MH. KUVAYÝÝ MÝLLÝYE CD. NO:175/C",AdresTarifiKisa = "ÝHSANÝYE MAH. ESKÝ DEVLET HASTANESÝ KARÞISI ",AdresTarifi ="ÝHSANÝYE MAH. ESKÝ DEVLET HASTANESÝ KARÞISI "},
new Eczane { Id = 786,Enlem = 36.7490039,Boylam = 34.5316538,TelefonNo = "3243581130",Adres = "MENDERES MH.MILLI EGEMENLIK CD. MIZRAK 5 AP./B",AdresTarifiKisa = "MEZÝTLÝ BELEDÝYE ARKASI TEDAÞ ÖDEME NOKTASI KARÞIS",AdresTarifi ="MEZÝTLÝ BELEDÝYE ARKASI TEDAÞ ÖDEME NOKTASI KARÞISI/MEZÝTLÝ"},
new Eczane { Id = 722,Enlem = 36.7708620,Boylam = 34.5621910,TelefonNo = "3245021112",Adres = "GMK BULVARI  570/A KILIÇ APT.ALTI",AdresTarifiKisa = "SULTAÞA OTEL KARÞISI",AdresTarifi ="GMK BULV. EÐRÝÇAM MH. ADNAN ÖZÇELÝK ASM YANI - SULTAÞA OTEL KARÞISI - DSÝ BÝTÝÞÝÐÝ /YENÝÞEHÝR"},
new Eczane { Id = 754,Enlem = 36.7569900,Boylam = 34.5351300,TelefonNo = "3243571413",Adres = " YENI MH.33180 SK. ESKI APT.ALTI NO:23/A MEZÝTLÝ",AdresTarifiKisa = "MEZÝTLÝ KÝTAPSAN YUKARISI (DAÐA DOÐRU 300MT) ",AdresTarifi ="MEZÝTLÝ KÝTAPSAN YUKARISI (DAÐA DOÐRU 300 MT) 1 NOLU ASM BATI KAPISI MEZÝTLÝ"},
new Eczane { Id = 695,Enlem = 36.7896146,Boylam = 34.5870556,TelefonNo = "3243266690",Adres = "GÜVENEVLER MAHALLESI 20. CADDE NO.19B/2 YENISEHIR MERSIN",AdresTarifiKisa = " ÖZEL MERSÝN FORUM YAÞAM HASTANESÝ KARÞISI",AdresTarifi ="GÜVENEVLER MH. 20 CD. NO:19 B/2 ÖZEL MERSÝN FORUM YAÞAM HASTANESÝ KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 787,Enlem = 36.7344050,Boylam = 34.5132440,TelefonNo = "3242338385",Adres = "Akdeniz Mh. 39608 Sk. Çukurova apt.  Zemin kat No:8 Mezitli",AdresTarifiKisa = " SOLÝ CENTER ARKASI MEZÝTLÝ",AdresTarifi ="AKDENÝZ MH. SOLÝ CENTER ARKASI - BÝM MARKET SOKAÐI -ÇUKUROVA APT. 2 NOLU ASM CÝVARI - MEZÝTLÝ ?"},
new Eczane { Id = 518,Enlem = 36.8452240,Boylam = 34.6335750,TelefonNo = "3242293713",Adres = "CUMHURIYET MAH.M.FEVZI ÇAKMAK CAD.NO:96 TOROSLAR/MERSIN",AdresTarifiKisa = "YALINAYAK SAÐLIK OCAÐI CÝVARI/TOROSLAR",AdresTarifi ="YALINAYAK MH. M.FEVZÝ ÇAKMAK CD. YALINAYAK SAÐLIK OCAÐI CÝVARI/TOROSLAR"},
new Eczane { Id = 621,Enlem = 36.7990240,Boylam = 34.6267150,TelefonNo = "3242333656",Adres = "ÇANKAYA MAH.123.CAD. 11/A AKDENIZ/MERSIN",AdresTarifiKisa = "ZEYTÝNLÝBAHÇE CD BÝTÝMÝ ÝSTÝKLAL CD KAVÞAÐI/AKDENÝ",AdresTarifi ="ZEYTÝNLÝBAHÇE CD BÝTÝMÝ ÝSTÝKLAL CD KAVÞAÐI/AKDENÝZ"},
new Eczane { Id = 788,Enlem = 36.7487271,Boylam = 34.5294383,TelefonNo = "3243584910",Adres = "MEZITLI ILÇESI MERKEZ MH. 52007 SK. ONURSAL AP. NO:10/C",AdresTarifiKisa = "MEZÝTLÝ BELEDÝYESÝ KARÞISI 2 NOLU ASM CÝVARI",AdresTarifi ="MEZÝTLÝ BELEDÝYESÝ KARÞISI 2 NOLU SAÐLIK OCAÐI CÝVARI/MEZÝTLÝ"},
new Eczane { Id = 600,Enlem = 36.8058330,Boylam = 34.6228810,TelefonNo = "3243364432",Adres = "KUVAYI MILLIYE CAD.BILLUR APT.ALTI 13/4 AKDENIZ MERSIN",AdresTarifiKisa = "HASTANE CD. KURUÇEÞME CÝVARI/AKDENÝZ",AdresTarifi ="HASTANE CD. KURUÇEÞME CÝVARI/AKDENÝZ"},
new Eczane { Id = 574,Enlem = 36.8051040,Boylam = 34.6322360,TelefonNo = "3242393272",Adres = "AKDENIZ ILÇESI YENI MH.5315 NO:15/B MERSIN",AdresTarifiKisa = "YENÝ MH.BAÐKUR ÝL MÜD. ARKASI ",AdresTarifi ="YENÝ MH.BAÐKUR ÝL MÜD. ARKASI YENÝ MH.SAÐLIK OCAÐI YANI/AKDENÝZ"},
new Eczane { Id = 696,Enlem = 36.7833500,Boylam = 34.5920150,TelefonNo = "3243367852",Adres = "Güvenevler Mh. 1908 Sk. Ümit Apt. A Blok Zemin Kat No:2",AdresTarifiKisa = "POZCU ÖZEL YENÝÞEHÝR HASTANESÝ ARKASI ",AdresTarifi ="POZCU ÖZEL YENÝÞEHÝR HASTANESÝ ARKASI - FORUM AVM KARÞISI - YENÝÞEHÝR"},
new Eczane { Id = 519,Enlem = 36.8395810,Boylam = 34.6304610,TelefonNo = "3242232104",Adres = "GÜNEYKENT MAH.FARABI CAD.BELEDIYE ÇARSISI NO.1 TOROSLAR",AdresTarifiKisa = "GÜNEYKENT SAÐLIK OCAÐI YANI",AdresTarifi ="GÜNEYKENT SAÐLIK OCAÐI YANI / GÜNEYKENT MERKEZ CAMÝÝ KARÞISI / TOROSLAR"},
new Eczane { Id = 721,Enlem = 36.7911858,Boylam = 34.5658996,TelefonNo = "3243412545",Adres = "MENTES MAH.25137 SOK.MIKDAT APT.ALTI NO:7/B YENISEHIR-MERSIN",AdresTarifiKisa = "METRO MARKET BATISI MOÝL PETROL SOKAÐI",AdresTarifi ="MENTEÞ MH. METRO GROSS MARKET (3. ÇEVREYOLU ÜZERÝ) BATISI MOÝL PETROL SOKAÐI/YENÝÞEHÝR"},
new Eczane { Id = 697,Enlem = 36.7901100,Boylam = 34.5876800,TelefonNo = "3243272275",Adres = "GÜVENEVLER MAHALLESI 20 CADDE 1951 SOKAK NO.27 YENISEHIR MERSIN",AdresTarifiKisa = "FORUM YAÞAM HASTANESÝ ACÝL KARÞISI/YENÝÞEHÝR",AdresTarifi ="GÜVENEVLER MH.1951 SK.FORUM YAÞAM HASTANESÝACÝL KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 698,Enlem = 36.7860530,Boylam = 34.5934990,TelefonNo = "3243263606",Adres = "GÜVENEVLER MAH 1.CAD.NO.5/B YENISEHIR",AdresTarifiKisa = "GÜVEN SÝTESÝ CÝVARI 4 NOLU SAÐLIK OCAÐI KARÞISI",AdresTarifi ="GÜVENEVLER MH. GÜVEN SÝTESÝ CÝVARI - POZCU ÇETÝNKAYA KARÞISI - 4 NOLU SAÐLIK OCAÐI KARÞISI/YENÝÞEHÝR"},
new Eczane { Id = 755,Enlem = 36.7604590,Boylam = 34.5439300,TelefonNo = "3243594747",Adres = " ATATÜRK MH. 31118 SK. BUÐRA ÇAT APT. NO:10/B  MEZÝTLÝ",AdresTarifiKisa = " MEDÝKALPARK HASTANESÝ ARKASI -HAL CAD. MEZÝTLÝ",AdresTarifi ="ATATÜRK MH.  MEDÝKALPARK HASTANESÝ ARKASI - HAL CAD.  MEZÝTLÝ"},
new Eczane { Id = 550,Enlem = 36.8128620,Boylam = 34.6135070,TelefonNo = "3243200208",Adres = "MERSIN TOROSLAR ILÇESI OSMANIYE MH. 81034 SK. NO:33",AdresTarifiKisa = "OSMANÝYE MH. 81034 SK. NO:33 /AKDENÝZ",AdresTarifi ="OSMANÝYE MH. 81034 SK. NO:33 /AKDENÝZ"},
new Eczane { Id = 601,Enlem = 36.8098430,Boylam = 34.6205320,TelefonNo = "3243371975",Adres = "IHSANIYE MAHALLE KUVAIMILLIYE CADDE NO.185 ",AdresTarifiKisa = "ESKÝ DEVLET HASTANESÝ KARÞISI",AdresTarifi ="ESKÝ DEVLET HASTANESÝ KARÞISI NO:185/A/AKDENÝZ"},
new Eczane { Id = 676,Enlem = 36.7926940,Boylam = 34.5828189,TelefonNo = "5464549537",Adres = "AKADEMÝ HASTANESÝ YANI MENTEÞ MH. BARBAROS BULV. GÖKSU PARK APT. NO:107 C  YENÝÞEHÝR",AdresTarifiKisa = " ÖZEL AKADEMÝ HASTANESÝ YANI",AdresTarifi ="MENTEÞ MAH. BARBAROS CADDESÝ GÖKSU PARK APT. ÖZEL AKADEMÝ HASTANESÝ YANI   YENÝÞEHÝR"},
new Eczane { Id = 627,Enlem = 36.7905990,Boylam = 34.6224220,TelefonNo = "3242380863",Adres = "KÜLTÜR MH. ATATÜRK CD. ÇAMLIBEL APT. ALTI NO: 1 64/D",AdresTarifiKisa = "KÜLTÜR MH. ATATÜRK CD. SÝSTEM TIP MERKEZÝ YANI  ",AdresTarifi ="KÜLTÜR MH. ATATÜRK CD. SÝSTEM TIP MERKEZÝ YANI ÇAMLIBEL/AKDENÝZ"},
new Eczane { Id = 612,Enlem = 36.8225170,Boylam = 34.6376870,TelefonNo = "3242342566",Adres = "GÜNEÞ MH. 5822 SK. NO: 6 SELAHATTÝN EYYÜBÝ ASM KARÞISI/AKDENÝZ",AdresTarifiKisa = " SELAHATTÝN EYYÜBÝ ASM KARÞISI/AKDENÝZ",AdresTarifi =" SELAHATTÝN EYYÜBÝ ASM KARÞISI/AKDENÝZ"},
new Eczane { Id = 714,Enlem = 36.7853980,Boylam = 34.5400690,TelefonNo = "3245021100",Adres = "ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. PARADÝSE HOMES SÝTESÝC BLOK NO:7-8-9",AdresTarifiKisa = "MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞIS",AdresTarifi ="ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. (MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI) /YENÝÞEHÝR"},
new Eczane { Id = 766,Enlem = 36.7491943,Boylam = 34.5424573,TelefonNo = "5530761803",Adres = "VÝRANÞEHÝR MAH. CENGÝZ TOPEL CAD. 34323 SK. Z.KAT  NO:19-7A",AdresTarifiKisa = "VÝRANÞEHÝR MAH. ALANYA 2 FIRINI KARÞI SOKAÐI",AdresTarifi ="VÝRANÞEHÝR MAH. CENGÝZ TOPEL CAD. 34323 SOK. ALANYA 2 FIRINI KARÞI SOKAÐI VÝRANÞEHÝR ASM CÝVARI "},
new Eczane { Id = 544,Enlem = 36.8140820,Boylam = 34.6289040,TelefonNo = "3243223346",Adres = "Saðlýk Mh. 86046 Sk. A Blok Bina No: 10/9 ",AdresTarifiKisa = "ÇEVÝKKUVVET CÝVARI TOROSLAR SAÐLIK ASM YANI ",AdresTarifi ="ÇEVÝKKUVVET CÝVARI TOROSLAR SAÐLIK ASM YANI (SAÐLIK MH. 86046 SK.)"},
new Eczane { Id = 784,Enlem = 36.7481541,Boylam = 34.5429888,TelefonNo = "3243589090",Adres = "VÝRANÞEHÝR MH. 34321 SK. KILIÇ 2 APT. NO:8/H MEZÝTLÝ",AdresTarifiKisa = " VÝRANÞEHÝR MH. KILIÇ 2 APT. VÝRANÞEHÝR ASM CÝVARI",AdresTarifi ="VÝRANÞEHÝR MH. 34321 SK. KILIÇ 2 APT. VÝRANÞEHÝR ASM CÝVARI MEZÝTLÝ"},
new Eczane { Id = 783,Enlem = 36.7354200,Boylam = 34.5122000,TelefonNo = "3245021620",Adres = "Akdeniz Mh. Yaþar doðu Cd. no:14/A",AdresTarifiKisa = "SOLÝ CENTER CÝVARI CARREFOURSA YANI PLATÝN PLAZA 2",AdresTarifi ="AKDENÝZ MH. YAÞAR DOÐU CD.  SOLÝ CENTER CÝVARI CARREFOURSA YANI PLATÝN PLAZA 2 ALTI/MEZÝTLÝ"},
new Eczane { Id = 725,Enlem = 36.7644110,Boylam = 34.5481630,TelefonNo = "3243587375",Adres = "ATATÜRK MH. 31039 SK. DAMLA SÝTESÝ A BLOK NO:10/A",AdresTarifiKisa = "ÜNÝVERSÝTE YOLU ÖZEL ORTADOÐU HASTANESÝ KUZEY YANI",AdresTarifi ="ÜNÝVERSÝTE YOLU ÖZEL ORTADOÐU HASTANESÝ KUZEY YANI MEZÝTLÝ"},
new Eczane { Id = 547,Enlem = 36.8194000,Boylam = 34.6090400,TelefonNo = "3243213879",Adres = "ZEKÝ AYAN MH. OKAN MERZECÝ BULVARI NO:347/A",AdresTarifiKisa = "ZEKÝ AYAN MH. ANITTAN MODERN TIP MERKEZÝNE DOÐRU ",AdresTarifi ="ZEKÝ AYAN MH. H. OKAN MERZECÝ BULVARI ANITTAN MODERN TIP MERKEZÝNE DOÐRU 100 MT. ÝLERÝSÝ TOROSLAR/MERSÝN"},
new Eczane { Id = 774,Enlem = 36.7347070,Boylam = 34.5137280,TelefonNo = "3245022156",Adres = "AKDENÝZ MAH. 39606 SOK. ÖZGÜL 4 APT. 7/B MEZÝTLÝ MERSÝN",AdresTarifiKisa = "SOLÝ CENTER CÝV. 2 NOLU SAÐLIK OCAÐI KARÞI ÇAPRAZI",AdresTarifi ="SOLÝ CENTER CÝVARI 2 NOLU SAÐLIK OCAÐI KARÞI ÇAPRAZI - MEZÝTLÝ"},
new Eczane { Id = 602,Enlem = 36.7940191,Boylam = 34.6157456,TelefonNo = "3242379666",Adres = "ÝSTÝKLAL CAD.  TURGUT REÝS MAH. ÞALK APT. ALTI NO: 194/ B AKDENÝZ",AdresTarifiKisa = "TURGUTREÝS MAH. ÝSTÝKLAL CAD. IMC HASTANESÝ YANI ",AdresTarifi ="TURGUTREÝS MAH. ÝSTÝKLAL CAD. IMC HASTANESÝ YANI AKDENÝZ"},
new Eczane { Id = 530,Enlem = 36.8254260,Boylam = 34.6228100,TelefonNo = "3245022838",Adres = "KURDALÝ MH. MERSÝNLÝ AHMET BULV. NO:59/A",AdresTarifiKisa = " MERSÝNLÝ AHMET ASM YANI TOROSLAR ",AdresTarifi ="KURDALÝ MH. MERSÝNLÝ AHMET BULV. NO:59 MERSÝNLÝ AHMET ASM YANI TOROSLAR "},
new Eczane { Id = 705,Enlem = 36.7807420,Boylam = 34.5591400,TelefonNo = "3245022216",Adres = "BATIKENT MH. 2652 SK. NO:16",AdresTarifiKisa = "NECATÝ BOLKAN Ý.Ö.OKULU CÝVARI ",AdresTarifi ="BATIKENT MH. ÞAHÝN TEPESÝ - NECATÝ BOLKAN Ý.Ö.OKULU CÝVARI  - MEVLANA ASM 100 MT. DOÐU"},
new Eczane { Id = 712,Enlem = 36.7917630,Boylam = 34.5647660,TelefonNo = "3243412140",Adres = "Menteþ Mah. 25139 Sk. No:19/A Yeniþehir/Mersin",AdresTarifiKisa = "MENTEÞ AÝLE SAÐLIÐI MERKEZÝ ÇAPRAZI",AdresTarifi ="MENTEÞ AÝLE SAÐLIÐI MERKEZÝ ÇAPRAZI"},
new Eczane { Id = 732,Enlem = 36.7639840,Boylam = 34.5472600,TelefonNo = "8502810315",Adres = "ÜNÝVERSÝTE CAD. ORTADOÐU HASTANESÝ YANI VALÝ ÞENOL ENGÝN CD. EMÝRGAN APT. ALTI NO:19 ",AdresTarifiKisa = "ÜNÝVERSÝTE CAD. ORTADOÐU HASTANESÝ YANI",AdresTarifi ="ÜNÝVERSÝTE CAD. ORTADOÐU HASTANESÝ YANI"},
new Eczane { Id = 729,Enlem = 36.7569200,Boylam = 34.5351940,TelefonNo = "3243585857",Adres = "YENÝ MH. 33180 SK. AKKOYUNLU APT. ALTI 21/B ",AdresTarifiKisa = "MEZÝTLÝ KÝTAPSAN YUKARISI 1 NOLU ASM BATI KAPISI ",AdresTarifi ="MEZÝTLÝ KÝTAPSAN YUKARISI (DAÐA DOÐRU 300 MT) 1 NOLU ASM BATI KAPISI MEZÝTLÝ"},
new Eczane { Id = 748,Enlem = 36.7638820,Boylam = 34.5471730,TelefonNo = "3243573808",Adres = "VALÝ ÞENOL ENGÝN CD. 31067 SK. REYHAN APT. NO:15/A",AdresTarifiKisa = "ÜNÝVERSÝTE CADDESÝ ORTADOÐU HASTANESÝ KARÞISI",AdresTarifi ="ÜNÝVERSÝTE CADDESÝ ORTADOÐU HASTANESÝ KARÞISI"},
new Eczane { Id = 646,Enlem = 36.7945067,Boylam = 34.6004964,TelefonNo = "3249995408",Adres = "HÜRRÝYET MAH. 1742 SOK. NO:39/C",AdresTarifiKisa = "2. ÇEVREYOLU ÜZERÝNDEKÝ MÝGROS ARKASI ",AdresTarifi ="2. ÇEVREYOLU ÜZERÝNDEKÝ MÝGROS ARKASI HÜRRÝYET ASM ÇAPRAZI"},
new Eczane { Id = 636,Enlem = 36.8235510,Boylam = 34.6535490,TelefonNo = "3242343477",Adres = "HAL MH. 6040 SK. NO:3 AKDENÝZ / MERSÝN",AdresTarifiKisa = "YENÝ SEBZE HALÝ CÝVARI, TOPTANCILAR SÝTESÝ KARÞISI",AdresTarifi ="YENÝ SEBZE HALÝ CÝVARI, TOPTANCILAR SÝTESÝ KARÞISI"},
new Eczane { Id = 701,Enlem = 36.7855540,Boylam = 34.5399860,TelefonNo = "3245023779",Adres = "ÇÝFTLÝKKÖY MAH. MÝMAR SÝNAN CAD. PARADÝSE HOMES SÝTESÝ D BLOK NO:24 D/F",AdresTarifiKisa = "MERSÝN ÜNÝ. TIP FAK. HASTANESÝ ACÝL KARÞISI",AdresTarifi ="ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. (MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ ACÝL KARÞISI) /YENÝÞEHÝR"},
new Eczane { Id = 515,Enlem = 36.8491080,Boylam = 34.6103740,TelefonNo = "3243266868",Adres = "ÇAÐDAÞKENT MH. 93130 SK. NO:1",AdresTarifiKisa = "ÇAÐDAÞKENT MH. 93130 SK. ÞEHÝR HASTANESÝ CÝVARI",AdresTarifi ="ÇAÐDAÞKENT MH. 93130 SK. ÞEHÝR HASTANESÝ CÝVARI"},
new Eczane { Id = 709,Enlem = 36.7854270,Boylam = 34.5404510,TelefonNo = "3245024373",Adres = "ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. 20/F",AdresTarifiKisa = "MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜL. HASTANESÝ KARÞISI",AdresTarifi ="MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI YENÝÞEHÝR"},
new Eczane { Id = 752,Enlem = 36.7229513,Boylam = 34.4931215,TelefonNo = "3244813432",Adres = " 75. YIL MH. 40036 SK. NO:1 MEZÝTLÝ",AdresTarifiKisa = "50. YIL MH. 5 NOLU SAÐLIK OCAÐI KARÞISI DAVULTEPE",AdresTarifi ="50. YIL MH. 5 NOLU SAÐLIK OCAÐI KARÞISI DAVULTEPE/MEZÝTLÝ"},
new Eczane { Id = 511,Enlem = 36.8489490,Boylam = 34.6104770,TelefonNo = "3242230888",Adres = "ÇAÐDAÞKENT MH. 93130 SK. NO: 1/A",AdresTarifiKisa = "ÇAÐDAÞKENT MH. 93130 SK. ÞEHÝR HASTANESÝ CÝVARI",AdresTarifi ="ÇAÐDAÞKENT MH. 93130 SK. ÞEHÝR HASTANESÝ CÝVARI"},
new Eczane { Id = 747,Enlem = 36.7519830,Boylam = 34.5362660,TelefonNo = "3249994565",Adres = "YENÝ MH. 33163 SK. GÜL CD. MIZRAK 26 APT. NO:24 MEZÝTLÝ",AdresTarifiKisa = "CÝTY HOSPÝTAL (ESKÝ DUYGU TIP MERKZ) ARKA ÇAPRAZI",AdresTarifi ="YENÝ MH. 33163 SK. CÝTY HOSPÝTAL (ESKÝ DUYGU TIP MERKEZÝ) ARKA ÇAPRAZI MEZÝTLÝ"},
new Eczane { Id = 534,Enlem = 36.8313860,Boylam = 34.6047170,TelefonNo = "3243201410",Adres = "ÇAVUÞLU MH. 89031 SK. NO:22 TOROSLAR",AdresTarifiKisa = "ÇAVUÞLU MH. YELDEÐÝRMENLÝ PARK SOKAÐI TOROSLAR",AdresTarifi ="ÇAVUÞLU MH. YELDEÐÝRMENLÝ PARK SOKAÐI TOROSLAR  4 NOLU SAÐLIK OCAÐI YANI"},
new Eczane { Id = 763,Enlem = 36.7515210,Boylam =  34.521605,TelefonNo = "5354270606",Adres = "MERKEZ MH.FINDIKPINARI CD. NO:46/C MEZÝTLÝ",AdresTarifiKisa = "KUYULUK YOLU BÝM MARKET KARÞISI EMRE KASABI YANI ",AdresTarifi ="KUYULUK YOLU BÝM MARKET KARÞISI EMRE KASABI YANI MEZÝTLÝ"},
new Eczane { Id = 770,Enlem = 36.7161290,Boylam = 34.4778320,TelefonNo = "3244815009",Adres = "Davultepe Mah. Kazým Karabekir Cad. No:6/A",AdresTarifiKisa = "DAVULTEPE ASM KARÞISI",AdresTarifi ="DAVULTEPE ASM KARÞISI"},
new Eczane { Id = 685,Enlem = 36.7839950,Boylam = 34.5800560,TelefonNo = "3249998958",Adres = "BARBAROS MAH. BARBAROS BULV. 2106 SOK. MÝLENYUM 3 APT. NO:1 YENÝÞEHÝR",AdresTarifiKisa = "AYDINLIKEVLER ÝÖO 300M KUZEYÝ BARBAROS BUL.ÜSTÜNDE",AdresTarifi ="AYDINLIKEVLER ÝLKOKULUNUN 300 MT KUZEYÝ BARBAROS BULVARI ÜZERÝ"},
new Eczane { Id = 688,Enlem = 36.7846870,Boylam = 34.5798810,TelefonNo = "3243271171",Adres = "Barbaros Mah.2106 Sk. No:8/3  YENÝÞEHÝR ",AdresTarifiKisa = "ADNAN ÖZÇELÝK ORTAOKULU ÇAPRAZI ",AdresTarifi ="ADNAN ÖZÇELÝK ORTAOKULU ÇAPRAZI YENÝÞEHÝR 1 NOLU ASM KARÞISI (GROSERÝ ÇAPRAZ SOKAÐI)"},
new Eczane { Id = 702,Enlem = 36.7792390,Boylam = 34.5387720,TelefonNo = "3245024439",Adres = "ÇÝFTLÝKKÖY MAH. 32133 SK. ÞAKÝR SON CAD. NO:6/C YENÝÞEHÝR",AdresTarifiKisa = "ESKÝ ÇÝFTLÝKKÖY YOLU YENÝÞEHÝR 3 NOLU ASM KARÞISI",AdresTarifi ="ESKÝ ÇÝFTLÝKKÖY YOLU - A 101 YANI - YENÝÞEHÝR 3 NOLU ASM KARÞISI"},
new Eczane { Id = 542,Enlem = 36.8195300,Boylam = 34.6151200,TelefonNo = "3243212437",Adres = "TOZKOPARAN MH. 87045 SK.NO:21/A-B",AdresTarifiKisa = "ÇUKUROVA ÝLKOKULU VE  TOZKOPARAN ASM KARÞISI",AdresTarifi ="TOZKOPARAN MH. 87045 SK. ÇUKUROVA ÝLKOKULU VE  TOZKOPARAN ASM KARÞISI"},
new Eczane { Id = 648,Enlem = 36.7884967,Boylam = 34.6055884,TelefonNo = "3243259800",Adres = "CUMHURÝYET MH. 1617 SK. MEHMETOÐLU CD. NO:3/A-13 YENÝÞEHÝR",AdresTarifiKisa = "CUMHURÝYET MAH. CÝÐERCÝ HAKAN ARKASI",AdresTarifi ="ÝSMET ÝNÖNÜ BULV. CUMHURÝYET MH. CÝÐERCÝ HAKAN VE DONAT TÝCARET ARKASI YENÝÞEHÝR"},
new Eczane { Id = 501,Enlem = 36.8503990,Boylam = 34.6100370,TelefonNo = "3242233390",Adres = "KORUKENT MH. 96013 SK. NO:13/1 TOROSLAR",AdresTarifiKisa = " MERSÝN ÞEHÝR HASTANESÝ CÝVARI TOKÝ EVLERÝ KARÞISI",AdresTarifi ="KORUKENT MH. 96013 SK. MERSÝN ÞEHÝR HASTANESÝ CÝVARI TOKÝ EVLERÝ, KARÞISI TOKÝ CAMÝÝ ÜZERÝ TOROSLAR"},
new Eczane { Id = 672,Enlem = 36.7789940,Boylam = 34.5752190,TelefonNo = "3243268666",Adres = "BARBOROS MH. 2148 SK. MEHMET EMÝN GÖK SÝTESÝ D BLOK NO:1/A",AdresTarifiKisa = "YENÝÞEHÝR KAYMAKAMLIÐI KARÞISI BARBAROS ASM YANI",AdresTarifi ="YENÝÞEHÝR KAYMAKAMLIÐI KARÞISI BARBAROS ÇOCUK PARKI ARKASI 15 NOLU SAÐLIK OCAÐI YANI/YENÝÞEHÝR"},
new Eczane { Id = 690,Enlem = 36.7900460,Boylam = 34.5877410,TelefonNo = "3243257347",Adres = "GÜVENEVLER MH. 1990 SK. NO:8B",AdresTarifiKisa = "FORUM YAÞAM HASTANESÝ ACÝL YANI YENÝÞEHÝR",AdresTarifi ="FORUM YAÞAM HASTANESÝ ACÝL YANI YENÝÞEHÝR"},
new Eczane { Id = 734,Enlem = 36.7597774,Boylam = 34.5444900,TelefonNo = "3243599250",Adres = "ATATÜRK MH. 31118 SK. EMEK APT. NO:6/A MEZÝTLÝ",AdresTarifiKisa = " GMK BULV. MEDÝCALPARK HASTANESÝ YANI MEZÝTLÝ",AdresTarifi ="GMK BULV. ATATÜRK MH.  MEZÝTLÝ MEDÝKAL PARK HASTANESÝ YANI DERYA OTOMOTÝV HYUNDAÝ KARÞISI"},
new Eczane { Id = 735,Enlem = 36.7585338,Boylam = 34.5435113,TelefonNo = "3245024444",Adres = "ATATÜRK MH. HAL CD. NO:3-F  MEZÝTLÝ",AdresTarifiKisa = "MEDÝKALPARK HASTANESÝ ACÝL HAKRÞISI",AdresTarifi ="MEZÝTLÝ ATATÜRK MAH. HAL CAD. MEDÝKALPARK HASTANESÝ ACÝL KARÞISI - MEZÝTLÝ"},
new Eczane { Id = 772,Enlem = 36.7323574,Boylam =  34.504196,TelefonNo = "3245023303",Adres = "AKDENÝZ MAH. 39760 SOK .NO:4/26 MEZÝTLÝ",AdresTarifiKisa = "MARTI OTEL KUZEYÝ M.AKÝF ERSOY ASM KARÞISI MEZÝTLÝ",AdresTarifi ="SAHÝL MARTI OTEL IÞIKLARI KUZEYÝ ÞEHÝT FATÝH SOYDAN ORTAOKULU DOÐUSU MEHMET  AKÝF ERSOY SAÐLIK OCAÐI KARÞISI MEZÝTLÝ"},
new Eczane { Id = 565,Enlem = 36.8063380,Boylam = 34.6328458,TelefonNo = "3242375663",Adres = "YENÝ MAH. 5328 SOK. NO:19/D AKDENÝZ",AdresTarifiKisa = "YENÝ MH. TOROS DEVLET HASTANESÝ ÝTFAÝYE ÇAPRAZI ",AdresTarifi ="YENÝ MH. TOROS DEVLET HASTANESÝ ÝTFAÝYE ÇAPRAZI ÇINAR TANTUNÝ YANI AKDENÝZ"},
new Eczane { Id = 764,Enlem = 36.7488434,Boylam = 34.5313990,TelefonNo = "3249337207",Adres = "VÝRANÞEHÝR MAH. VÝRANÞEHÝR CAD. YENÝ CEMRE SÝT. A BLOK NO:16/C",AdresTarifiKisa = "MEZÝTLÝ BELEDÝYE ARKASI HASAN USTA TANTUNÝ KARÞISI",AdresTarifi ="MEZÝTLÝ BELEDÝYE ARKASI HASAN USTA TANTUNÝ KARÞISI"},
new Eczane { Id = 771,Enlem = 36.7467232,Boylam = 34.5306394,TelefonNo = "3243369383",Adres = "MENDERES MH. 35427 SK. TEMEL SÝTESÝ NO:3/25-26-27  MEZÝTLÝ",AdresTarifiKisa = "MEZÝTLÝ BELEDÝYE ARKASI - MEZÝTLÝ ZABITA SOKAÐI",AdresTarifi ="MENDERES MH. 35427 SK. MEZÝTLÝ BELEDÝYE ARKASI - MEZÝTLÝ ZABITA SOKAÐI - 15 TEMMUZ ÞEHÝTLERÝ ORTAOKULU KUZEYÝ"},
new Eczane { Id = 506,Enlem = 36.8329375,Boylam = 34.6195625,TelefonNo = "5419739313",Adres = "ÇAÐDAÞKENT MH. GAZÝ OSMAN PAÞA BULV. 93007 SK. NO:4/A  TOROSLAR",AdresTarifiKisa = "ÇAÐDAÞKENT MH. GAZÝ OSMAN PAÞA ASM YANI",AdresTarifi ="ÇAÐDAÞKENT MH. GAZÝ OSMAN PAÞA ASM YANI CELLO ÇEÞMESÝ BATISI - TOROSLAR"},
new Eczane { Id = 723,Enlem = 36.7854310,Boylam = 34.5402370,TelefonNo = "3245021516",Adres = "ÇÝFTLÝKKÖY MAH. MÝMAR SÝNAN CAD. PARADÝSE HOMES SÝT. D BLOK NO:5 ",AdresTarifiKisa = "MERSÝN ÜNÝVERSÝTESÝ TIPFAKÜLTESÝ HASTANESÝ KARÞISI",AdresTarifi ="ÇÝFTLÝKKÖY MH. MÝMAR SÝNAN CD. (MERSÝN ÜNÝVERSÝTESÝ TIP FAKÜLTESÝ HASTANESÝ KARÞISI) /YENÝÞEHÝR"},
new Eczane { Id = 586,Enlem = 36.8083191,Boylam =  34.617970,TelefonNo = "3243373034",Adres = "ÝHSANÝYE MAH. HAVUZLAR CAD. NO:80/A-122",AdresTarifiKisa = "KARAYOLLARI KAVÞ. SU HAST. YANI TÜP BEBEK KARÞISI",AdresTarifi ="KARAYOLLARI KAVÞAÐI SU HASTANESÝ YANI TÜP BEBEK KARÞISI"},
new Eczane { Id = 793,Enlem = 36.7902624,Boylam =  34.588063,TelefonNo = "3240000000",Adres = "BAHÇELÝEVLER MH. 16. CD. IÞIK APT. ALTI NO:57/A YENIÞEHIR",AdresTarifiKisa = "BAHÇELÝEVLER MH. 16. CD. TOROS ÜNIVERSITESI YOLU",AdresTarifi ="BAHÇELÝEVLER MH. 16. CD. TOROS ÜNIVERSITESI YENIÞEHIR KAMPÜSÜ YOLU"},
new Eczane { Id = 796,Enlem = 36.7597494,Boylam = 34.5437798,TelefonNo = "3243574737",Adres = "ATATÜRK MH. 31118. SK. ÇERÝ ÇAT. APT. ALTI NO:13  MEZÝTLÝ",AdresTarifiKisa = "MEDÝCALPARK HASTANESÝ YANI - MEZÝTLÝ",AdresTarifi ="ATATÜRK MH. 31118. SK. ÇERÝ ÇAT. APT. ALTI MEDÝCALPARK HASTANESÝ YANI - MEZÝTLÝ"},
new Eczane { Id = 797,Enlem = 36.7602294,Boylam = 34.5419797,TelefonNo = "5073137171",Adres = "ATATÜRK MH. 31118 SK. BUÐRA ÇAT APT. ALTI 10/A MEZITLI",AdresTarifiKisa = "MEZÝTLÝ MEDÝKAL PARK HASTANESÝ YANI ",AdresTarifi ="ATATÜRK MH.  MEZÝTLÝ MEDÝKAL PARK HASTANESÝ YANI DERYA OTOMOTÝV HYUNDAÝ KARÞISI"},
new Eczane { Id = 795,Enlem = 36.7323850,Boylam = 34.5044080,TelefonNo = "3249990030",Adres = "AKDENÝZ MH. 39709 SK. HEKÝMOÐLU APT. NO:5/B  MEZÝTLÝ",AdresTarifiKisa = "AKDENÝZ MH. MEHMET  AKÝF ERSOY ASM KARÞISI MEZÝTLÝ",AdresTarifi ="SAHÝL MARTI OTEL IÞIKLARI KUZEYÝ ÞEHÝT FATÝH SOYDAN ORTAOKULU DOÐUSU MEHMET  AKÝF ERSOY SAÐLIK OCAÐI KARÞISI MEZÝTLÝ"},
new Eczane { Id = 789,Enlem = 36.7902359,Boylam = 34.5971049,TelefonNo = "3249998133",Adres = "BAHÇELÝEVLER MH. 16. CD. NO:55/E",AdresTarifiKisa = "TOROS ÜNÝVERSÝTESÝ 45 EVLER KAMPÜSÜ CÝVAR CADDESÝ ",AdresTarifi ="TÜRK TELEKOM YUKARISI TOROS ÜNÝVERSÝTESÝ 45 EVLER KAMPÜSÜ CÝVAR CADDESÝ ETÇÝ LOKANTASI KARÞISI"},
new Eczane { Id = 791,Enlem = 36.7926835,Boylam = 34.5938196,TelefonNo = "3243312120",Adres = "BAHÇELÝEVLER MH. HÜSEYÝN OKAN MERZECÝ BULV. 466/B",AdresTarifiKisa = "BAHÇELÝEVLER MH. DEMOKRASÝ KAV.ÇIKIÞI KOSGEB YANI ",AdresTarifi ="BAHÇELÝEVLER MAH. DEMOKRASÝ KAVÞAÐI ÇIKIÞI MEZÝTLÝ YOLU ÜZERÝ KOSGEB BÝNASI YANI"},
new Eczane { Id = 790,Enlem = 36.8182237,Boylam = 34.6264595,TelefonNo = "3243214403",Adres = "SELÇUKLAR MAH. 206. CAD. NO:117/C",AdresTarifiKisa = "ESKÝ FEN LÝSESÝ DURAÐI KARÞISI (AKABE CAMÝ SOKAÐI)",AdresTarifi ="ESKÝ FEN LÝSESÝ DURAÐI KARÞISI (AKABE CAMÝ SOKAÐI)"},
new Eczane { Id = 799,Enlem = 36.7593542,Boylam = 34.5433423,TelefonNo = "3245020009",Adres = "ATATÜRK MAH. HAL CAD. 31089 SOK NO:3/C",AdresTarifiKisa = "MEDÝCAL PARK HASTANESÝ ACÝL KARÞISI",AdresTarifi ="MEDÝCAL PARK HASTANESÝ ACÝL KARÞISI"},
new Eczane { Id = 798,Enlem = 36.7593542,Boylam = 34.5433423,TelefonNo = "3245020018",Adres = "ATATÜRK MAH. HAL CAD. 31089 SOK. NO:3/D",AdresTarifiKisa = "MEDÝCAL PARK HASTANESÝ ACÝL KARÞISI",AdresTarifi ="MEDÝCAL PARK HASTANESÝ ACÝL KARÞISI"},
new Eczane { Id = 803,Enlem = 36.7789507,Boylam = 34.5384343,TelefonNo = "3245023377",Adres = "ÇÝFTLÝKKÖY MH. 32133 SK. NO:6/B",AdresTarifiKisa = "ÇÝFTLÝKKÖY AÝLE SAÐLIÐI MERKEZÝ KARÞISI A101 YANI",AdresTarifi ="ÇÝFTLÝKKÖY AÝLE SAÐLIÐI MERKEZÝ KARÞISI A101 YANI"},
new Eczane { Id = 792,Enlem = 36.7936806,Boylam = 34.6011301,TelefonNo = "3243265363",Adres = "HÜRRÝYET MH. 1742 SK. SAKARYA APT. ZEMÝN KAT NO:35/A  YENIÞEHIR/MERSIN",AdresTarifiKisa = "HÜRRIYET SAÐLIK OCAÐI YANINDAKI BIM KARÞISI",AdresTarifi ="DEMOKRASI KAVÞAÐI ÜZERI MIGROS ARKASI HÜRRIYET SAÐLIK OCAÐI YANINDAKI BIM KARÞISI"},
new Eczane { Id = 818,Enlem = 36.8203125,Boylam = 34.6078738,TelefonNo = "3243200007",Adres = "ZEKI AYAN MAH. KUVAYI MILLIYE CAD. NO:291/A",AdresTarifiKisa = "TOROSLAR ANIT KAVÞAÐI. AKBANK KARÞISI",AdresTarifi ="TOROSLAR ANIT KAVÞAÐI. AKBANK KARÞISI"},
new Eczane { Id = 820,Enlem = 36.7380625,Boylam = 34.5144988,TelefonNo = "3249991577",Adres = "MERKEZ MAH. 52136 SOK. ÜÇBAL SÝTESÝ C BLOK NO:19 (1E)",AdresTarifiKisa = "SOLÝ CENTER CÝVARI MEZÝTLÝ 4 NOLU ASM KARÞISI",AdresTarifi ="SOLÝ CENTER CÝVARI BEÞKARDEÞLER YAPI YANI MEZÝTLÝ 4 NOLU ASM KARÞISI"},


            	#endregion
            };

            //var ekle = false;

            //if (ekle)
            //{
            //    foreach (var eczane in mersinEnlemBoylamlar)
            //    {
            //        var result = context.Eczaneler.SingleOrDefault(w => w.Id == eczane.Id);

            //        result.Boylam = eczane.Boylam;
            //        result.Enlem = eczane.Enlem;
            //        result.TelefonNo = eczane.TelefonNo;
            //        result.Adres = eczane.Adres;
            //        result.AdresTarifiKisa = eczane.AdresTarifiKisa;
            //        result.AdresTarifi = eczane.AdresTarifi;

            //        context.SaveChanges();
            //    }
            //}

            #endregion

            #region mazeret türler
            var mazeretTurler = new List<MazeretTur>()
                            {
                                new MazeretTur(){ Adi="Çok Önemli"},
                                new MazeretTur(){ Adi="Önemli"},
                                new MazeretTur(){ Adi="Az Önemli"}
                            };

            context.MazeretTurler.AddOrUpdate(s => new { s.Adi }, mazeretTurler.ToArray());
            //mazeretTurler.ForEach(d => context.MazeretTurler.Add(d));
            context.SaveChanges();
            #endregion

            #region istek türler
            var istekTurler = new List<IstekTur>()
                            {
                                new IstekTur(){ Adi="Çok Önemli"},
                                new IstekTur(){ Adi="Önemli"},
                                new IstekTur(){ Adi="Az Önemli"}
                            };

            context.IstekTurler.AddOrUpdate(s => new { s.Adi }, istekTurler.ToArray());
            //istekTurler.ForEach(d => context.IstekTurler.Add(d));
            context.SaveChanges();
            #endregion

            #region mazeretler
            var mazeretler = new List<Mazeret>()
                            {
                                new Mazeret(){ Adi="Saðlýk", MazeretTurId=1}
                            };

            context.Mazeretler.AddOrUpdate(s => new { s.Adi }, mazeretler.ToArray());
            //mazeretler.ForEach(d => context.Mazeretler.Add(d));
            context.SaveChanges();
            #endregion

            #region istekler
            var istekler = new List<Istek>()
                            {
                                new Istek(){ Adi="Sýralý Nöbet", IstekTurId=1},
                                new Istek(){ Adi="Saðlýk", IstekTurId=1}
                            };

            context.Istekler.AddOrUpdate(s => new { s.Adi }, istekler.ToArray());
            //istekler.ForEach(d => context.Istekler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet mazeretler
            var eczaneNobetMazeretler = new List<EczaneNobetMazeret>()
            {
                new EczaneNobetMazeret(){ EczaneNobetGrupId=1, MazeretId=1, Aciklama="Deneme", TakvimId=1 },
            };

            context.EczaneNobetMazeretler.AddOrUpdate(s => new { s.EczaneNobetGrupId, s.MazeretId, s.TakvimId }, eczaneNobetMazeretler.ToArray());
            //eczaneNobetMazeretler.ForEach(d => context.EczaneNobetMazeretler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet istekler
            var eczaneNobetIstekler = new List<EczaneNobetIstek>()
                            {
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sýrayla", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sýrayla", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sýrayla", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sýrayla", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sýrayla", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sýrayla", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sýrayla", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sýrayla", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sýrayla", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sýrayla", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sýrayla", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sýrayla", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sýrayla", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sýrayla", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sýrayla", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sýrayla", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sýrayla", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sýrayla", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sýrayla", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sýrayla", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sýrayla", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sýrayla", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sýrayla", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sýrayla", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sýrayla", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sýrayla", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sýrayla", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sýrayla", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sýrayla", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sýrayla", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sýrayla", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sýrayla", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sýrayla", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sýrayla", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sýrayla", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sýrayla", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sýrayla", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sýrayla", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sýrayla", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sýrayla", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sýrayla", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sýrayla", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sýrayla", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sýrayla", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sýrayla", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sýrayla", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sýrayla", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sýrayla", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sýrayla", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sýrayla", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sýrayla", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sýrayla", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sýrayla", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sýrayla", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sýrayla", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sýrayla", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sýrayla", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sýrayla", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sýrayla", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sýrayla", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sýrayla", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sýrayla", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sýrayla", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sýrayla", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sýrayla", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sýrayla", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sýrayla", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sýrayla", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sýrayla", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sýrayla", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sýrayla", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sýrayla", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sýrayla", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sýrayla", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sýrayla", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sýrayla", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sýrayla", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sýrayla", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sýrayla", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sýrayla", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sýrayla", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sýrayla", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sýrayla", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sýrayla", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sýrayla", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sýrayla", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sýrayla", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sýrayla", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sýrayla", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sýrayla", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sýrayla", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sýrayla", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sýrayla", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sýrayla", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sýrayla", TakvimId=665},

                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Deneme", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=665},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=672},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=679},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=686},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=693},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=700},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=707},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=714},

                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=665},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=672},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=679},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=686},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=693},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=700},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=707},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=714},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=721},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=728},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=735},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=742},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=749},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=756}
                            };

            context.EczaneNobetIstekler.AddOrUpdate(s => new { s.EczaneNobetGrupId, s.IstekId, s.TakvimId }, eczaneNobetIstekler.ToArray());
            //eczaneNobetIstekler.ForEach(d => context.EczaneNobetIstekler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet muafiyet
            var eczaneNobetMuafiyetler = new List<EczaneNobetMuafiyet>()
                            {
                                new EczaneNobetMuafiyet(){ EczaneId=1, BaslamaTarihi=new DateTime(2018, 2, 1), BitisTarihi=new DateTime(2018, 2, 1).AddDays(30), Aciklama="deneme için muaftýr" }
                            };

            context.EczaneNobetMuafiyetler.AddOrUpdate(s => new { s.EczaneId, s.BaslamaTarihi }, eczaneNobetMuafiyetler.ToArray());
            //eczaneNobetMuafiyetler.ForEach(d => context.EczaneNobetMuafiyetler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet gruplar
            /*
            var eczaneNobetGruplar = new List<EczaneNobetGrup>()
            {
				#region Alanya
				#region 1
				new EczaneNobetGrup() { EczaneId = 1, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 2, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 3, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 4, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 5, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 6, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 7, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 8, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 9, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 10, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 11, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 12, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 13, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 14, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 15, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 16, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 17, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 18, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 19, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" }, 
				#endregion

				#region 2
	new EczaneNobetGrup() { EczaneId = 20, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 21, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 22, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 23, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 24, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 25, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 26, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 27, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 28, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 29, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 30, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 31, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 32, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 33, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 34, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 35, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 36, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 37, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 38, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 39, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" }, 
#endregion

				#region 3
				new EczaneNobetGrup() { EczaneId = 40, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 41, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 42, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 43, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 44, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 45, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 46, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 47, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 48, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 49, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 50, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 51, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 52, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 53, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 54, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 55, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 56, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 57, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
#endregion 
				#endregion

				#region Antalya

				new EczaneNobetGrup() { EczaneId =58, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =59, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =60, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =61, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =62, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =63, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =64, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =65, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =66, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =67, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =68, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =69, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =70, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =71, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =72, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =73, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =74, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =75, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =76, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =77, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =78, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =79, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =80, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =81, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =82, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =83, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =84, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =85, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =86, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =87, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =88, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =89, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =90, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =91, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =92, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =93, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =94, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =95, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =96, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =97, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =98, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =99, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =100, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =101, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =102, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =103, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =104, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =105, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =106, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =107, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =108, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =109, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =110, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =111, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =112, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =113, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =114, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =115, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =116, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =117, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =118, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =119, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =120, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =121, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =122, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =123, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =124, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =125, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =126, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =127, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =128, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =129, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =130, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =131, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =132, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =133, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =134, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =135, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =136, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =137, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =138, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =139, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =140, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =141, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =142, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =143, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =144, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =145, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =146, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =147, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =148, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =149, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =150, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =151, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =152, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =153, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =154, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =155, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =156, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =157, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =158, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =159, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =160, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =161, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =162, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =163, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =164, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =165, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =166, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =167, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =168, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =169, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =170, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =171, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =172, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =173, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =174, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =175, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =176, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =177, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =178, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =179, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =180, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =181, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =182, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =183, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =184, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =185, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =186, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =187, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =188, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =189, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =190, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =191, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =192, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =193, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =194, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =195, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =196, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =197, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =198, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =199, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =200, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =201, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =202, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =203, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =204, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =205, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =206, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =207, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =208, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =209, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =210, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =211, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =212, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =213, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =214, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =215, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =216, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =217, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =218, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =219, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =220, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =221, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =222, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =223, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =224, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =225, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =226, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =227, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =228, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =229, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =230, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =231, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =232, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =233, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =234, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =235, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =236, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =237, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =238, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =239, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =240, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =241, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =242, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =243, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =244, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =245, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =246, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =247, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =248, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =249, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =250, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =251, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =252, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =253, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =254, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =255, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =256, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =257, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =258, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =259, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =260, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =261, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =262, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =263, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =264, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =265, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =266, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =267, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =268, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =269, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =270, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =271, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =272, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =273, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =274, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =275, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =276, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =277, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =278, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =279, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =280, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =281, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =282, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =283, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =284, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =285, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =286, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =287, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =288, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =289, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =290, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =291, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =292, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =293, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =294, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =295, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =296, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =297, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =298, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =299, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =300, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =301, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =302, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =303, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =304, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =305, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =306, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =307, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =308, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =309, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =310, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =311, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =312, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =313, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =314, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =315, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =316, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =317, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =318, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =319, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =320, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =321, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =322, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =323, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =324, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =325, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =326, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =327, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =328, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =329, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =330, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =331, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =332, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =333, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =334, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =335, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =336, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =337, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =338, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =339, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =340, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =341, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =342, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =343, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =344, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =345, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =346, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =347, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =348, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =349, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =350, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =351, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =352, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =353, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =354, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =355, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =356, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =357, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =358, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =359, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =360, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =361, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =362, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =363, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =364, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =365, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =366, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =367, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =368, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =369, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =370, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =371, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =372, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =373, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =374, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =375, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =376, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =377, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =378, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =379, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =380, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =381, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =382, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =383, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =384, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =385, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =386, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =387, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =388, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =389, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =390, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =391, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =392, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =393, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =394, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =395, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =396, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =397, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =398, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =399, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =400, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =401, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =402, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =403, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =404, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =405, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =406, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =407, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =408, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =409, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =410, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =411, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =412, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =413, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =414, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =415, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =416, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =417, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =418, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =419, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =420, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =421, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =422, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =423, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =424, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =425, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =426, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =427, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =428, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =429, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =430, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =431, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =432, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =433, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =434, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =435, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =436, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =437, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =438, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =439, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =440, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =441, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =442, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =443, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =444, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =445, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =446, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =447, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =448, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =449, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =450, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =451, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =452, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =453, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =454, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =455, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =456, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =457, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =458, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =459, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =460, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =461, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =462, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =463, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =464, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =465, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =466, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =467, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =468, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =469, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =470, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =471, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =472, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =473, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =474, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =475, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =476, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =477, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =478, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =479, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =480, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =481, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =482, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },

	#endregion
			};
            context.EczaneNobetGruplar.AddOrUpdate(s => new { s.EczaneId }, eczaneNobetGruplar.ToArray());
            context.SaveChanges();
            */
            #endregion

            #region eczane nöbet sonuç aktifler
            //var v13 = new List<EczaneNobetSonucAktif>()
            //                {
            //                    new EczaneNobetSonucAktif(){ EczaneNobetGrupId=1, TakvimId=1 }
            //                };

            //v13.ForEach(d => context.EczaneNobetSonucAktifler.Add(d));
            //context.SaveChanges();
            #endregion

            #region eczane nöbet sonuçlar
            //var v6 = new List<EczaneNobetSonuc>()
            //                {
            //                    new EczaneNobetSonuc(){ EczaneNobetGrupId=1, TakvimId=1 }
            //                };

            //v6.ForEach(d => context.EczaneNobetSonuclar.Add(d));
            //context.SaveChanges();
            #endregion

            #region nobet sonuc demo tipler
            var nobetSonucDemoTipler = new List<NobetSonucDemoTip>()
                        {
                        new NobetSonucDemoTip(){ Adi="Gruplar baðýmsýz. Tüm günler daðýlým.", Aciklama="Haftanýn herbir gününün düzgün daðýtýldýðý ve gruplar arasý herhangi bir bað kurulmaksýzýn yapýlan çözüm"},
                        new NobetSonucDemoTip(){ Adi="Gruplar baðýmsýz. Seçili günler daðýlým.", Aciklama="Haftanýn seçili günlerinin düzgün daðýtýldýðý ve gruplar arasý herhangi bir bað kurulmaksýzýn yapýlan çözüm"},
                        new NobetSonucDemoTip(){ Adi="Gruplar baðýmlý. Tüm günler daðýlým.", Aciklama="Haftanýn tüm günlerinin düzgün daðýtýldýðý ve gruplar arasý baðlarýn olduðu çözüm"},
                        new NobetSonucDemoTip(){ Adi="Gruplar baðýmlý. Seçili günler daðýlým.", Aciklama="Haftanýn seçili günlerinin düzgün daðýtýldýðý ve gruplar arasý baðlarýn olduðu çözüm"},
                        };

            context.NobetSonucDemoTipler.AddOrUpdate(s => new { s.Adi }, nobetSonucDemoTipler.ToArray());
            context.SaveChanges();
            #endregion

            #region eczane nöbet sonuç demolar
            var eczaneNobetSonucDemolar = new List<EczaneNobetSonucDemo>()
                            {
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=7, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=33, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=44, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=73, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=83, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=117, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=127, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=140, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=166, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=181, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=211, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=228, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=243, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=257, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=273, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=286, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=311, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=320, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=346, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=380, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=393, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=406, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=416, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=443, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=468, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=1, TakvimId=485, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=14, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=37, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=52, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=80, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=86, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=104, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=147, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=152, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=167, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=174, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=187, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=225, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=241, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=253, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=280, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=296, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=305, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=334, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=349, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=372, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=381, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=413, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=437, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=470, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=477, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=2, TakvimId=484, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=21, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=40, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=58, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=66, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=79, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=113, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=125, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=137, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=154, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=179, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=198, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=213, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=230, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=254, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=287, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=295, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=325, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=335, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=341, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=386, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=394, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=410, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=420, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=431, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=472, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=3, TakvimId=479, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=28, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=46, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=53, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=62, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=89, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=99, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=106, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=142, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=161, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=176, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=191, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=214, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=237, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=255, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=264, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=294, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=306, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=327, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=352, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=359, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=377, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=403, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=423, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=427, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=457, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=4, TakvimId=474, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=4, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=20, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=35, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=60, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=74, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=120, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=122, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=138, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=159, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=168, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=185, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=206, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=223, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=261, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=270, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=301, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=310, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=326, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=342, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=354, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=388, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=411, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=434, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=450, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=463, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=5, TakvimId=480, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=17, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=23, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=42, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=69, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=75, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=100, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=128, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=143, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=164, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=175, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=188, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=221, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=236, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=250, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=260, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=288, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=308, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=337, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=363, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=367, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=379, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=409, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=418, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=441, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=466, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=6, TakvimId=481, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=12, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=31, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=49, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=61, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=90, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=96, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=121, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=141, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=163, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=172, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=182, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=219, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=229, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=272, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=284, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=303, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=315, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=348, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=356, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=369, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=396, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=400, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=408, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=448, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=459, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=7, TakvimId=471, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=1, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=16, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=56, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=67, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=87, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=109, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=118, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=130, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=169, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=180, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=189, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=216, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=232, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=269, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=274, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=298, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=322, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=355, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=362, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=368, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=374, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=419, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=438, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=455, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=8, TakvimId=482, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=3, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=13, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=43, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=63, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=93, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=114, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=132, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=151, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=162, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=177, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=196, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=218, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=239, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=263, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=293, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=302, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=329, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=340, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=361, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=373, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=387, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=417, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=424, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=451, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=462, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=9, TakvimId=475, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=6, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=19, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=54, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=64, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=70, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=116, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=144, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=150, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=156, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=173, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=203, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=215, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=242, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=244, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=267, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=285, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=314, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=336, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=344, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=358, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=376, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=414, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=425, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=453, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=10, TakvimId=469, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=11, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=26, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=55, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=77, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=92, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=107, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=129, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=136, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=158, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=184, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=193, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=210, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=235, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=262, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=279, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=299, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=313, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=332, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=343, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=370, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=389, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=407, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=445, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=454, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=467, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=11, TakvimId=476, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=5, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=22, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=50, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=76, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=84, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=103, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=134, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=148, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=153, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=201, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=212, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=217, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=234, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=246, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=275, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=290, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=317, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=333, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=350, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=382, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=391, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=397, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=436, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=449, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=12, TakvimId=483, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=25, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=34, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=51, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=82, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=91, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=102, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=123, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=135, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=160, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=186, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=197, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=224, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=247, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=268, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=282, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=289, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=309, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=324, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=357, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=366, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=404, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=412, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=426, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=435, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=13, TakvimId=456, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=9, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=24, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=39, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=68, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=78, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=98, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=115, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=146, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=157, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=200, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=207, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=231, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=256, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=265, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=281, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=297, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=312, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=328, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=364, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=375, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=405, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=422, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=428, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=439, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=14, TakvimId=464, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=8, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=47, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=57, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=65, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=72, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=105, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=111, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=145, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=170, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=194, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=209, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=238, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=249, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=271, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=276, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=283, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=307, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=323, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=339, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=371, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=390, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=415, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=429, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=442, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=15, TakvimId=473, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=10, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=41, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=59, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=85, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=94, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=112, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=131, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=149, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=171, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=190, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=202, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=226, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=233, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=245, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=278, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=291, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=321, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=330, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=360, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=378, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=395, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=401, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=430, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=446, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=16, TakvimId=461, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=18, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=30, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=36, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=45, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=81, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=97, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=119, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=124, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=178, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=195, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=205, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=222, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=240, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=252, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=277, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=304, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=316, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=345, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=351, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=385, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=398, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=421, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=433, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=444, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=458, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=17, TakvimId=478, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=15, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=27, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=32, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=38, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=95, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=110, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=126, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=139, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=155, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=199, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=208, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=220, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=227, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=251, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=259, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=300, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=319, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=331, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=347, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=383, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=392, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=402, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=440, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=452, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=18, TakvimId=460, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=2, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=29, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=48, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=71, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=88, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=101, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=108, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=133, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=165, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=183, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=192, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=204, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=248, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=258, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=266, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=292, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=318, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=338, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=353, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=365, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=384, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=399, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=432, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=447, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=19, TakvimId=465, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=7, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=47, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=58, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=60, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=90, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=93, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=137, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=154, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=176, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=190, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=205, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=219, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=244, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=270, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=294, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=303, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=344, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=352, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=370, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=387, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=424, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=434, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=454, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=20, TakvimId=479, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=21, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=37, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=59, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=67, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=76, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=109, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=127, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=135, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=153, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=161, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=199, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=234, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=243, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=271, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=301, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=305, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=317, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=348, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=372, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=390, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=398, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=411, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=441, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=21, TakvimId=459, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=17, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=28, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=38, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=69, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=103, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=115, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=122, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=136, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=168, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=187, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=207, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=221, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=258, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=264, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=297, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=308, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=325, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=339, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=383, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=394, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=412, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=436, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=448, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=22, TakvimId=466, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=24, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=30, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=35, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=68, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=81, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=97, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=134, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=145, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=175, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=197, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=208, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=230, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=256, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=263, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=285, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=296, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=315, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=345, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=354, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=373, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=419, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=439, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=455, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=457, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=23, TakvimId=482, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=10, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=26, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=42, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=83, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=94, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=116, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=123, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=139, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=170, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=182, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=188, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=241, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=247, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=281, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=299, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=313, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=322, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=340, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=367, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=386, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=403, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=417, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=433, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=24, TakvimId=462, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=20, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=49, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=57, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=71, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=82, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=95, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=131, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=169, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=181, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=189, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=198, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=215, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=250, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=269, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=295, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=329, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=341, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=349, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=359, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=381, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=407, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=418, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=437, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=456, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=25, TakvimId=469, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=9, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=25, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=56, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=64, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=85, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=104, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=130, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=141, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=159, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=185, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=196, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=223, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=248, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=268, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=283, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=306, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=333, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=336, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=366, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=380, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=405, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=438, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=445, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=470, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=26, TakvimId=476, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=4, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=13, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=45, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=63, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=107, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=120, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=138, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=151, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=165, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=203, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=216, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=222, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=249, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=262, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=291, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=302, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=320, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=343, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=384, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=395, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=409, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=444, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=452, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=27, TakvimId=483, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=5, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=15, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=55, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=70, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=110, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=117, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=128, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=148, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=178, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=210, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=218, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=237, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=260, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=274, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=304, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=321, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=331, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=350, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=388, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=396, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=408, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=431, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=449, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=28, TakvimId=481, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=1, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=34, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=52, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=77, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=99, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=108, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=143, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=157, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=174, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=204, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=217, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=225, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=254, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=277, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=290, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=311, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=357, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=363, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=374, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=414, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=423, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=426, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=464, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=29, TakvimId=472, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=3, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=41, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=53, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=84, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=92, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=106, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=150, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=167, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=173, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=194, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=224, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=232, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=251, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=276, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=289, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=332, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=358, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=364, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=377, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=415, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=421, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=425, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=465, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=30, TakvimId=474, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=6, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=14, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=54, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=72, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=89, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=91, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=121, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=163, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=179, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=184, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=192, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=231, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=261, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=272, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=292, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=319, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=326, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=342, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=355, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=371, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=397, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=443, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=451, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=31, TakvimId=461, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=8, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=33, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=40, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=80, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=98, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=118, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=142, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=158, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=171, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=183, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=228, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=238, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=253, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=278, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=286, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=309, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=356, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=365, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=378, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=402, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=416, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=428, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=442, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=32, TakvimId=484, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=12, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=22, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=46, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=75, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=88, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=105, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=125, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=149, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=162, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=202, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=211, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=227, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=236, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=245, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=293, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=318, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=330, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=353, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=379, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=385, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=410, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=430, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=450, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=467, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=33, TakvimId=480, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=16, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=29, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=48, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=65, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=96, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=112, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=129, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=164, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=177, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=209, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=213, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=235, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=252, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=275, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=284, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=307, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=316, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=361, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=368, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=392, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=401, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=446, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=453, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=34, TakvimId=460, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=18, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=27, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=39, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=74, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=86, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=119, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=124, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=144, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=166, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=186, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=200, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=220, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=259, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=265, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=282, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=314, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=324, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=338, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=382, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=393, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=399, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=432, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=458, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=471, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=35, TakvimId=477, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=23, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=36, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=43, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=61, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=87, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=102, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=126, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=146, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=180, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=193, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=212, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=226, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=257, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=266, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=300, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=327, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=334, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=346, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=375, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=391, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=406, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=435, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=447, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=36, TakvimId=478, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=2, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=11, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=44, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=66, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=73, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=111, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=133, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=152, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=160, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=191, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=201, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=242, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=267, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=273, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=298, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=312, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=323, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=337, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=347, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=389, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=404, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=413, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=440, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=37, TakvimId=468, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=19, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=50, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=62, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=78, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=101, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=114, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=140, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=156, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=172, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=195, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=214, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=233, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=240, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=280, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=288, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=310, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=328, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=362, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=369, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=400, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=420, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=429, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=475, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=38, TakvimId=485, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=31, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=32, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=51, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=79, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=100, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=113, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=132, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=147, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=155, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=206, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=229, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=239, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=246, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=255, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=279, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=287, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=335, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=351, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=360, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=376, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=422, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=427, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=463, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=39, TakvimId=473, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=7, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=20, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=52, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=75, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=86, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=92, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=99, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=133, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=159, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=167, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=191, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=204, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=230, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=239, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=250, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=259, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=286, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=325, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=332, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=340, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=352, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=385, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=405, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=421, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=431, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=474, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=40, TakvimId=485, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=41, TakvimId=14, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=41, TakvimId=31, NobetGorevTipId=1, NobetSonucDemoTipId=4 },
new EczaneNobetSonucDemo(){ EczaneNobetGrupId=41, TakvimId=34, NobetGorevTipId=1, NobetSonucDemoTipId=4 }

                            };

            context.EczaneNobetSonucDemolar.AddOrUpdate(s => new { s.EczaneNobetGrupId, s.TakvimId, s.NobetGorevTipId, s.NobetSonucDemoTipId }, eczaneNobetSonucDemolar.ToArray());
            context.SaveChanges();
            #endregion

            #region eczane grup tanim tipler
            var eczaneGrupTanimTipler = new List<EczaneGrupTanimTip>()
                            {
                                new EczaneGrupTanimTip(){ Adi="Coðrafi yakýnlýk" },
                                new EczaneGrupTanimTip(){ Adi="Eþ Durumu" }
                            };

            context.EczaneGrupTanimTipler.AddOrUpdate(s => new { s.Adi }, eczaneGrupTanimTipler.ToArray());
            context.SaveChanges();
            #endregion //

            #region eczane grup tanýmlar
            var eczaneGrupTanimlar = new List<EczaneGrupTanim>()
                            {
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_GÜNEÞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_NÝSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_TUÐBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_ÞÝRÝN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_BÝLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_FÝLÝZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_GÜNEÞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_NÝSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_TUÐBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_ÞÝRÝN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_BÝLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_FÝLÝZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_GÜNEÞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_NÝSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_TUÐBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_ÞÝRÝN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_BÝLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_FÝLÝZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_GÜNEÞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_NÝSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_TUÐBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_ÞÝRÝN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_BÝLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_FÝLÝZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_GÜNEÞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_NÝSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_TUÐBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_ÞÝRÝN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_BÝLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_FÝLÝZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="HAYAT_SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="SÝPAHÝOÐLU_SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="SELCEN _SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TEZCAN_SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ÞEKER_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ÞEKER_KASAPOÐLU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ÞEKER_ÞAHÝN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ÞEKER_SU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ÞEKER_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ÞEKER_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAÝYE_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAÝYE_KASAPOÐLU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAÝYE_ÞAHÝN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAÝYE_SU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAÝYE_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAÝYE_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLÝOÐLU_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLÝOÐLU_KASAPOÐLU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLÝOÐLU_ÞAHÝN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLÝOÐLU_SU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLÝOÐLU_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLÝOÐLU_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALTUNBAÞ_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜLERYÜZ_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_SEVÝNDÝ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_ALTUNBAÞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_GÜLERYÜZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_GÜNEÞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"}


                            };

            context.EczaneGrupTanimlar.AddOrUpdate(s => new { s.NobetUstGrupId, s.Adi }, eczaneGrupTanimlar.ToArray());
            //eczaneGrupTanimlar.ForEach(d => context.EczaneGrupTanimlar.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane gruplama
            var eczaneGruplar = new List<EczaneGrup>()
                            {
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=1 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=2 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=3 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=4 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=5 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=6 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=7 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=8 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=9 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=10 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=11 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=12 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=13 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=14 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=15 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=16 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=17 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=18 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=19 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=20 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=21 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=22 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=23 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=24 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=25 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=26 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=27 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=28 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=29 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=30 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=31 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=32 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=33 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=34 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=35 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=36 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=37 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=38 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=39 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=40 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=41 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=42 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=43 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=44 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=45 },
                                new EczaneGrup(){ EczaneId=37, EczaneGrupTanimId=46 },
                                new EczaneGrup(){ EczaneId=36, EczaneGrupTanimId=47 },
                                new EczaneGrup(){ EczaneId=35, EczaneGrupTanimId=48 },
                                new EczaneGrup(){ EczaneId=22, EczaneGrupTanimId=49 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=50 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=51 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=52 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=53 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=54 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=55 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=56 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=57 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=58 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=59 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=60 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=61 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=62 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=63 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=64 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=65 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=66 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=67 },
                                new EczaneGrup(){ EczaneId=49, EczaneGrupTanimId=68 },
                                new EczaneGrup(){ EczaneId=45, EczaneGrupTanimId=69 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=70 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=71 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=72 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=73 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=74 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=75 },

                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=1 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=2 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=3 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=4 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=5 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=6 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=7 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=8 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=9 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=10 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=11 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=12 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=13 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=14 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=15 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=16 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=17 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=18 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=19 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=20 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=21 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=22 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=23 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=24 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=25 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=26 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=27 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=28 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=29 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=30 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=31 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=32 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=33 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=34 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=35 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=36 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=37 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=38 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=39 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=40 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=41 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=42 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=43 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=44 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=45 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=46 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=47 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=48 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=49 },
                                new EczaneGrup(){ EczaneId=33, EczaneGrupTanimId=50 },
                                new EczaneGrup(){ EczaneId=27, EczaneGrupTanimId=51 },
                                new EczaneGrup(){ EczaneId=28, EczaneGrupTanimId=52 },
                                new EczaneGrup(){ EczaneId=31, EczaneGrupTanimId=53 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=54 },
                                new EczaneGrup(){ EczaneId=20, EczaneGrupTanimId=55 },
                                new EczaneGrup(){ EczaneId=33, EczaneGrupTanimId=56 },
                                new EczaneGrup(){ EczaneId=27, EczaneGrupTanimId=57 },
                                new EczaneGrup(){ EczaneId=28, EczaneGrupTanimId=58 },
                                new EczaneGrup(){ EczaneId=31, EczaneGrupTanimId=59 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=60 },
                                new EczaneGrup(){ EczaneId=20, EczaneGrupTanimId=61 },
                                new EczaneGrup(){ EczaneId=33, EczaneGrupTanimId=62 },
                                new EczaneGrup(){ EczaneId=27, EczaneGrupTanimId=63 },
                                new EczaneGrup(){ EczaneId=28, EczaneGrupTanimId=64 },
                                new EczaneGrup(){ EczaneId=31, EczaneGrupTanimId=65 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=66 },
                                new EczaneGrup(){ EczaneId=20, EczaneGrupTanimId=67 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=68 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=69 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=70 },
                                new EczaneGrup(){ EczaneId=20, EczaneGrupTanimId=71 },
                                new EczaneGrup(){ EczaneId=49, EczaneGrupTanimId=72 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=73 },
                                new EczaneGrup(){ EczaneId=45, EczaneGrupTanimId=74 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=75 }

                            };

            context.EczaneGruplar.AddOrUpdate(s => new { s.EczaneId, s.EczaneGrupTanimId }, eczaneGruplar.ToArray());
            //eczaneGruplar.ForEach(d => context.EczaneGruplar.Add(d));
            context.SaveChanges();
            #endregion

            #region user eczane odalar
            var userEczaneOdalar = new List<UserEczaneOda>()
                            {
                                new UserEczaneOda(){ EczaneOdaId =1, UserId=4 }
                            };

            context.UserEczaneOdalar.AddOrUpdate(s => new { s.EczaneOdaId, s.UserId }, userEczaneOdalar.ToArray());
            context.SaveChanges();
            #endregion

            #region user nobet üst gruplar
            var userNobetUstGruplar = new List<UserNobetUstGrup>()
                            {
                                new UserNobetUstGrup(){  NobetUstGrupId=1, UserId=3 },
                                new UserNobetUstGrup(){  NobetUstGrupId=1, UserId=5 },
                                new UserNobetUstGrup(){  NobetUstGrupId=2, UserId=8 }
                            };

            context.UserNobetUstGruplar.AddOrUpdate(s => new { s.NobetUstGrupId, s.UserId }, userNobetUstGruplar.ToArray());
            context.SaveChanges();
            #endregion

            #region user eczaneler
            var userEczaneler = new List<UserEczane>()
                            {
                                new UserEczane(){ EczaneId=1, UserId=6 }
                            };

            context.UserEczaneler.AddOrUpdate(s => new { s.EczaneId, s.UserId }, userEczaneler.ToArray());
            context.SaveChanges();
            #endregion

            #region þehirler
            var vSehirler = new List<Sehir>()
                            {
                                new Sehir(){ Adi="Antalya", EczaneOdaId=1 }
                            };

            context.Roles.AddOrUpdate(s => new { s.Name }, vRole.ToArray());
            //vSehirler.ForEach(d => context.Sehirler.Add(d));
            context.SaveChanges();

            #endregion

            #region ilçeler
            var vIlceler = new List<Ilce>()
                            {
                                new Ilce(){ Adi="Alanya", SehirId=1 },
                                new Ilce(){ Adi="Muratpaþa", SehirId=1 },
                                new Ilce(){ Adi="Konyaaltý", SehirId=1 },
                                new Ilce(){ Adi="Kepez", SehirId=1 }
                            };

            context.Roles.AddOrUpdate(s => new { s.Name }, vRole.ToArray());
            vIlceler.ForEach(d => context.Ilceler.Add(d));
            context.SaveChanges();

            #endregion

            #region eczane ilçeler
            //var vEczaneIlceler = new List<EczaneIlce>()
            //                {
            //                   // new EczaneIlce(){  },
            //                };

            //vEczaneIlceler.ForEach(d => context.EczaneIlceler.Add(d));
            //context.SaveChanges();

            #endregion

            //17.09.2018'de eklendi
            #region gün gruplar

            var gunGruplar = new List<GunGrup>()
                            {
                                new GunGrup(){ Adi="Pazar" },
                                new GunGrup(){ Adi="Bayram" },
                                new GunGrup(){ Adi="Hafta Ýçi" },

                                new GunGrup(){ Adi="Cumartesi" },

                                new GunGrup(){ Adi="Arife" },
                            };

            context.GunGruplar.AddOrUpdate(s => new { s.Adi }, gunGruplar.ToArray());
            context.SaveChanges();
            #endregion

            #region nöbet üst grup gün gruplar

            var nobetUstGrupGunGruplar = new List<NobetUstGrupGunGrup>()
                            {
                                //alanya
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 1, GunGrupId = 1 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 1, GunGrupId = 2 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 1, GunGrupId = 3 },

                                //antalya merkez
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 2, GunGrupId = 1 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 2, GunGrupId = 2 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 2, GunGrupId = 3 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 2, GunGrupId = 5 },

                                //mersin merkez
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 3, GunGrupId = 1 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 3, GunGrupId = 2 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 3, GunGrupId = 3 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = 3, GunGrupId = 4 },
                            };

            context.NobetUstGrupGunGruplar.AddOrUpdate(s => new { s.NobetUstGrupId, s.GunGrupId }, nobetUstGrupGunGruplar.ToArray());
            context.SaveChanges();
            #endregion

            #region nöbet özel günler

            var bayramTurler2 = context.BayramTurler.OrderBy(o => o.Id).ToList();

            var nobetOzelGunler = new List<NobetOzelGun>();

            foreach (var bayramTur in bayramTurler2)
            {
                var nobetOzelGun = new NobetOzelGun() { Adi = bayramTur.Adi };

                nobetOzelGunler.Add(nobetOzelGun);
            }

            context.NobetOzelGunler.AddOrUpdate(s => new { s.Adi }, nobetOzelGunler.ToArray());
            context.SaveChanges();
            #endregion

            #region nöbet grup gün kurallar

            var nobetGrupGunKurallar2 = context.NobetGrupGunKurallar
                //.OrderBy(o => o.Id)
                .ToList();

            var nobetGrupGorevTipGunKurallar = new List<NobetGrupGorevTipGunKural>();

            foreach (var nobetGrupGunKural in nobetGrupGunKurallar2)
            {
                var nobetGrupGorevTipGunKural = new NobetGrupGorevTipGunKural()
                {
                    NobetGrupGorevTipId = nobetGrupGunKural.NobetGrupId,
                    NobetGunKuralId = nobetGrupGunKural.NobetGunKuralId,
                    BaslangicTarihi = nobetGrupGunKural.BaslangicTarihi,
                    BitisTarihi = nobetGrupGunKural.BitisTarihi,
                    NobetUstGrupGunGrupId = GetNobetUstGrupGunGrupId(nobetGrupGunKural.NobetGrup.NobetUstGrupId, nobetGrupGunKural.NobetGunKuralId)
                };

                nobetGrupGorevTipGunKurallar.Add(nobetGrupGorevTipGunKural);
            }

            context.NobetGrupGorevTipGunKurallar.AddOrUpdate(s => new { s.NobetGrupGorevTipId, s.NobetGunKuralId, s.NobetUstGrupGunGrupId }, nobetGrupGorevTipGunKurallar.ToArray());
            context.SaveChanges();

            int GetNobetUstGrupGunGrupId(int nobetUstGrupId, int nobetGunKuralId)
            {
                int nobetUstGrupGunGrupId = 0;

                if (nobetGunKuralId == 1)
                {//pazar
                    var nobetUstGrupGunGrup = context.NobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 1);
                    nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
                }
                else if (nobetGunKuralId > 1 && nobetGunKuralId < 7)
                {//hafta içi
                    var nobetUstGrupGunGrup = context.NobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 3);
                    nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
                }
                else if (nobetGunKuralId == 7)
                {
                    if (nobetUstGrupId == 3)
                    {//cumartesi - mersin için
                        var nobetUstGrupGunGrup = context.NobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 4);
                        nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
                    }
                    else
                    {//hafta içi
                        var nobetUstGrupGunGrup = context.NobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 3);
                        nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
                    }
                }
                else
                {
                    var nobetUstGrupGunGrup = context.NobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 2);
                    nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
                }

                return nobetUstGrupGunGrupId;
            }
            #endregion

            #region nöbet grup görev tip takvim özel Günler

            var bayramlar2 = context.Bayramlar.ToList();

            var nobetGrupGorevTipTakvimOzelGunler = new List<NobetGrupGorevTipTakvimOzelGun>();

            foreach (var bayram in bayramlar2)
            {
                var nobetGrupGorevTipGunKural = context.NobetGrupGorevTipGunKurallar.SingleOrDefault(w => w.NobetGunKuralId == bayram.NobetGunKuralId && w.NobetGrupGorevTipId == bayram.NobetGrupGorevTipId);

                var nobetGrupGorevTipTakvimOzelGun = new NobetGrupGorevTipTakvimOzelGun()
                {
                    TakvimId = bayram.TakvimId,
                    NobetGunKuralId = nobetGrupGorevTipGunKural.NobetGunKuralId,
                    NobetGrupGorevTipGunKuralId = nobetGrupGorevTipGunKural.Id,
                    NobetOzelGunId = bayram.BayramTurId
                };

                nobetGrupGorevTipTakvimOzelGunler.Add(nobetGrupGorevTipTakvimOzelGun);
            }

            context.NobetGrupGorevTipTakvimOzelGunler.AddOrUpdate(s => new { s.TakvimId, s.NobetGunKuralId, s.NobetOzelGunId, s.NobetGrupGorevTipGunKuralId }, nobetGrupGorevTipTakvimOzelGunler.ToArray());
            context.SaveChanges();
            #endregion
        }

        private static void UstGrupPaketiEkleKompakt(GerekliBilgiler b)
        {
            //var baslamaTarihi = new DateTime(2019, 4, 1);

            //var odaId = 5;

            //var nobetUstGrupId = 6;
            //var nobetGrupGorevTipId = 28;

            #region eczaneler            

            var eczaneIdSon = b.EczaneNobetContext.Eczaneler.Max(m => m.Id);

            foreach (var eczane in b.Eczaneler)
            {
                eczane.NobetUstGrupId = b.NobetUstGrupId;
            }

            b.EczaneNobetContext.Eczaneler.AddOrUpdate(s => new { s.Adi, s.AcilisTarihi, s.NobetUstGrupId }, b.Eczaneler.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion

            #region nöbet üst grup gün gruplar

            b.EczaneNobetContext.NobetUstGrupGunGruplar.AddOrUpdate(s => new { s.NobetUstGrupId, s.GunGrupId }, b.NobetUstGrupGunGruplar.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion            

            #region nöbet grup gün kurallar

            NobetGrupGunKuralEkle(b.EczaneNobetContext, b.BaslamaTarihi, b.NobetUstGrupId, b.EczaneNobetContext.NobetGrupGorevTipler.Where(w => w.Id == 61).Select(s => s.Id).ToList(), b.VarsayilanNobetciSayisi);

            #endregion

            #region nöbet grup görev tip takvim özel Günler

            NobetGrupGorevTipTakvimOzelGunEkle(b.EczaneNobetContext, b.NobetGrupGorevTipId);

            #endregion 

            #region eczane nöbet gruplar

            //buraya dikkat. birden çok nöbet grubu varsa ayýrýp eklemek lazým
            var eczaneler = b.EczaneNobetContext.Eczaneler
                .Where(w => w.Id > eczaneIdSon)
                .OrderBy(o => o.Id).ToList();

            var eczaneNobetGruplar = new List<EczaneNobetGrup>();

            var indisEczaneSayisi = 1;

            foreach (var eczane in eczaneler)
            {
                var nobetGrupGorevTipId = b.NobetGrupGorevTipId;

                //birden fazla nöbet grubu olursa ayarla mutlaka

                if (indisEczaneSayisi <= 50)
                {
                    nobetGrupGorevTipId = b.NobetGrupGorevTipId;
                }
                //else if (indisEczaneSayisi <= 42)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 1;
                //}
                //else if (indisEczaneSayisi <= 52)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 2;
                //}
                //else if (indisEczaneSayisi <= 58)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 3;
                //}
                //else if (indisEczaneSayisi <= 67)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 4;
                //}
                //else if (indisEczaneSayisi <= 84)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 5;
                //}

                eczaneNobetGruplar.Add(new EczaneNobetGrup()
                {
                    EczaneId = eczane.Id,
                    NobetGrupGorevTipId = nobetGrupGorevTipId,
                    BaslangicTarihi = b.BaslamaTarihi,
                    Aciklama = "-"
                });

                indisEczaneSayisi++;
            }

            b.EczaneNobetContext.EczaneNobetGruplar.AddOrUpdate(s => new { s.EczaneId, s.NobetGrupGorevTipId, s.BaslangicTarihi }, eczaneNobetGruplar.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion

            #region talepler

            //TalepEkle(context, 28, 2);

            #endregion

            #region nöbet üst grup kýsýtlar

            var nobetUstGrupKisitlar = b.EczaneNobetContext.NobetUstGrupKisitlar
                .Where(w => w.NobetUstGrupId == 2)//antalya - varsayýlan
                .ToList();

            if (b.NobetUstGruplar != null)
            {
                var kisitlar = new List<NobetUstGrupKisit>();

                foreach (var nobetUstGrupKisit in nobetUstGrupKisitlar)
                {
                    var nobetUstGrupKisit2 = new NobetUstGrupKisit()
                    {
                        KisitId = nobetUstGrupKisit.KisitId,
                        NobetUstGrupId = b.NobetUstGrupId,
                        SagTarafDegeri = nobetUstGrupKisit.SagTarafDegeri,
                        SagTarafDegeriVarsayilan = nobetUstGrupKisit.SagTarafDegeriVarsayilan,
                        PasifMi = nobetUstGrupKisit.PasifMi,
                        VarsayilanPasifMi = nobetUstGrupKisit.VarsayilanPasifMi
                    };

                    kisitlar.Add(nobetUstGrupKisit2);
                }

                b.EczaneNobetContext.NobetUstGrupKisitlar.AddOrUpdate(s => new { s.NobetUstGrupId, s.KisitId }, kisitlar.ToArray());
                b.EczaneNobetContext.SaveChanges();
            }
            #endregion
        }

        private static void UstGrupPaketiEkle(GerekliBilgiler b)
        {
            //var baslamaTarihi = new DateTime(2019, 4, 1);

            #region eczane odalar

            if (b.EczaneOdalalar != null)
            {
                b.EczaneNobetContext.EczaneOdalar.AddOrUpdate(s => new { s.Adi }, b.EczaneOdalalar.ToArray());
                b.EczaneNobetContext.SaveChanges();
            }

            #endregion

            //var odaId = 5;

            #region nöbet üst gruplar

            if (b.NobetUstGruplar != null)
            {
                b.EczaneNobetContext.NobetUstGruplar.AddOrUpdate(s => new { s.Adi }, b.NobetUstGruplar.ToArray());
                b.EczaneNobetContext.SaveChanges();
            }

            #endregion

            //var nobetUstGrupId = 6;
            //var nobetGrupGorevTipId = 28;

            #region eczaneler            

            var eczaneIdSon = b.EczaneNobetContext.Eczaneler.Max(m => m.Id);

            foreach (var eczane in b.Eczaneler)
            {
                eczane.NobetUstGrupId = b.NobetUstGrupId;
                eczane.Adi = eczane.Adi.Trim();
            }

            b.EczaneNobetContext.Eczaneler.AddOrUpdate(s => new { s.Adi, s.AcilisTarihi, s.NobetUstGrupId }, b.Eczaneler.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion

            #region nöbet gruplar

            foreach (var nobetGrup in b.NobetGruplar)
            {
                nobetGrup.Adi = nobetGrup.Adi.Trim();
            }

            b.EczaneNobetContext.NobetGruplar.AddOrUpdate(s => new { s.Adi }, b.NobetGruplar.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion

            #region users

            var kullanicilar = b.Kullanicilar;//.Select(s => s.User);

            if (kullanicilar.Count() > 0)
            {
                foreach (var kullanici in kullanicilar)
                {
                    kullanici.Password = SHA256(kullanici.Password);
                }

                b.EczaneNobetContext.Users.AddOrUpdate(s => new { s.Email }, kullanicilar.ToArray());
                b.EczaneNobetContext.SaveChanges();
            }

            string SHA256(string strGiris)
            {
                if (strGiris == "" || strGiris == null)
                {
                    throw new ArgumentNullException("Veri Yok");
                }
                else
                {
                    SHA256Managed sifre = new SHA256Managed();
                    byte[] arySifre = StringToByte(strGiris);
                    byte[] aryHash = sifre.ComputeHash(arySifre);
                    var hash = BitConverter.ToString(aryHash);
                    return hash.Replace("-", "");
                }
            }

            byte[] StringToByte(string deger)
            {
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                return ByteConverter.GetBytes(deger);
            }

            #endregion

            #region user roles    

            var userRoller = new List<UserRole>();

            if (kullanicilar.Count() > 0)
            {
                var i = 0;

                foreach (var kullanici in b.Kullanicilar)
                {
                    i++;

                    var eklenenKullanici = b.EczaneNobetContext.Users.SingleOrDefault(w => w.Email == kullanici.Email);

                    var rolId = i == 1 ? 2 : 3;

                    userRoller.Add(new UserRole() { RoleId = rolId, UserId = eklenenKullanici.Id, BaslamaTarihi = b.BaslamaTarihi });
                };

                b.EczaneNobetContext.UserRoles.AddOrUpdate(s => new { s.RoleId, s.UserId, s.BaslamaTarihi }, userRoller.ToArray());
                b.EczaneNobetContext.SaveChanges();
            }
            #endregion

            #region user eczane odalar

            var userEczaneOdalar = new List<UserEczaneOda>();

            foreach (var userRol in userRoller.Where(w => w.RoleId == 2))
            {
                //var sonEklenenKullanici = b.EczaneNobetContext.Users.SingleOrDefault(w => w.Email == kullanici.User.Email);

                userEczaneOdalar.Add(new UserEczaneOda { EczaneOdaId = b.OdaId, UserId = userRol.UserId, BaslamaTarihi = b.BaslamaTarihi });
            };

            if (userEczaneOdalar.Count > 0)
            {
                b.EczaneNobetContext.UserEczaneOdalar.AddOrUpdate(s => new { s.EczaneOdaId, s.UserId, s.BaslamaTarihi }, userEczaneOdalar.ToArray());
                b.EczaneNobetContext.SaveChanges();
            }

            #endregion

            #region user nobet üst gruplar

            var userNobetUstGruplar = new List<UserNobetUstGrup>();

            foreach (var userRol in userRoller.Where(w => w.RoleId == 3))
            {
                //var sonEklenenKullanici = b.EczaneNobetContext.Users.SingleOrDefault(w => w.Email == kullanici.User.Email);

                userNobetUstGruplar.Add(new UserNobetUstGrup { NobetUstGrupId = b.NobetUstGrupId, UserId = userRol.UserId, BaslamaTarihi = b.BaslamaTarihi });
            };

            if (userNobetUstGruplar.Count > 0)
            {
                b.EczaneNobetContext.UserNobetUstGruplar.AddOrUpdate(s => new { s.NobetUstGrupId, s.UserId, s.BaslamaTarihi }, userNobetUstGruplar.ToArray());
                b.EczaneNobetContext.SaveChanges();
            }

            #endregion

            #region nöbet grup görev tipler

            var nobetGrupGorevTipler = new List<NobetGrupGorevTip>();

            var nobetGrupAdlari = b.NobetGruplar.Select(s => s.Adi);
            var nobetGrupBaslamaTarihileri = b.NobetGruplar.Select(s => s.BaslamaTarihi);

            var nobetGruplar = b.EczaneNobetContext.NobetGruplar
                .Where(w => nobetGrupAdlari.Contains(w.Adi)
                         && nobetGrupBaslamaTarihileri.Contains(w.BaslamaTarihi)).ToList();

            foreach (var nobetGrup in nobetGruplar)
            {
                var gorevTipler = new int[] { 1
                    //, 5 //tam gün nöbetçi
                };

                foreach (var gorevTip in gorevTipler)
                {
                    nobetGrupGorevTipler.Add(new NobetGrupGorevTip() { NobetGrupId = nobetGrup.Id, NobetGorevTipId = gorevTip, BaslamaTarihi = nobetGrup.BaslamaTarihi });
                }
            }

            b.EczaneNobetContext.NobetGrupGorevTipler.AddOrUpdate(s => new { s.NobetGrupId, s.NobetGorevTipId }, nobetGrupGorevTipler.ToArray());
            b.EczaneNobetContext.SaveChanges();

            var nobetGrupGorevTiplerSonradanEklenenler = b.EczaneNobetContext.NobetGrupGorevTipler.Where(w => w.Id >= b.NobetGrupGorevTipId).ToList();

            #endregion

            #region nöbet grup kurallar

            //var sonNobetGrubu = b.EczaneNobetContext.NobetGrupKurallar.ToList().LastOrDefault();

            foreach (var nobetGrupGorevTip in nobetGrupGorevTiplerSonradanEklenenler)
            {
                foreach (var kural in b.NobetKurallar)
                {
                    b.NobetGrupKurallar.Add(new NobetGrupKural
                    {
                        NobetGrupGorevTipId = nobetGrupGorevTip.Id,
                        NobetKuralId = kural.Id,
                        BaslangicTarihi = b.BaslamaTarihi,
                        Deger = kural.Id == 1 //Ardýþýk Boþ Gün Sayýsý
                         ? 4
                         : 1 //Varsayýlan günlük nöbetçi sayýsý
                    });
                }
            }

            b.EczaneNobetContext.NobetGrupKurallar.AddOrUpdate(s => new { s.NobetGrupGorevTipId, s.NobetKuralId, s.BaslangicTarihi }, b.NobetGrupKurallar.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion

            #region nöbet üst grup gün gruplar

            b.EczaneNobetContext.NobetUstGrupGunGruplar.AddOrUpdate(s => new { s.NobetUstGrupId, s.GunGrupId }, b.NobetUstGrupGunGruplar.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion            

            #region nöbet grup gün kurallar

            NobetGrupGunKuralEkle(b.EczaneNobetContext, b.BaslamaTarihi, b.NobetUstGrupId, nobetGrupGorevTipler, b.VarsayilanNobetciSayisi);

            #endregion

            #region nöbet grup görev tip takvim özel Günler

            foreach (var nobetGrupGorevTip in nobetGrupGorevTiplerSonradanEklenenler)
            {
                NobetGrupGorevTipTakvimOzelGunEkle(b.EczaneNobetContext, nobetGrupGorevTip.Id);
            }

            #endregion 

            #region eczane nöbet gruplar

            //buraya dikkat. birden çok nöbet grubu varsa ayýrýp eklemek lazým
            var eczaneler = b.EczaneNobetContext.Eczaneler
                .Where(w => w.Id > eczaneIdSon)
                .OrderBy(o => o.Id).ToList();

            var eczaneNobetGruplar = new List<EczaneNobetGrup>();

            var indisEczaneSayisi = 1;

            foreach (var eczane in eczaneler)
            {
                var nobetGrupGorevTipId = b.NobetGrupGorevTipId;

                //birden fazla nöbet grubu olursa ayarla mutlaka

                if (indisEczaneSayisi <= 40)
                {
                    nobetGrupGorevTipId = b.NobetGrupGorevTipId;
                }
                else if (indisEczaneSayisi <= 80)
                {
                    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 1;
                }
                else if (indisEczaneSayisi <= 120)
                {
                    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 2;
                }
                else //if (indisEczaneSayisi <= 140)
                {
                    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 3;
                }
                //else if (indisEczaneSayisi <= 175)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 4;
                //}
                //else if (indisEczaneSayisi <= 214)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 5;
                //}
                //else if (indisEczaneSayisi <= 239)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 6;
                //}
                //else if (indisEczaneSayisi <= 261)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 7;
                //}
                //else if (indisEczaneSayisi <= 275)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 8;
                //}
                //else if (indisEczaneSayisi <= 301)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 9;
                //}
                //else if (indisEczaneSayisi <= 314)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 10;
                //}
                //else if (indisEczaneSayisi <= 344)
                //{
                //    nobetGrupGorevTipId = b.NobetGrupGorevTipId + 11;
                //}

                eczaneNobetGruplar.Add(new EczaneNobetGrup()
                {
                    EczaneId = eczane.Id,
                    NobetGrupGorevTipId = nobetGrupGorevTipId,
                    BaslangicTarihi = b.BaslamaTarihi,
                    Aciklama = "-"
                });

                indisEczaneSayisi++;
            }

            b.EczaneNobetContext.EczaneNobetGruplar.AddOrUpdate(s => new { s.EczaneId, s.NobetGrupGorevTipId, s.BaslangicTarihi }, eczaneNobetGruplar.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion

            #region talepler

            //TalepEkle(context, 28, 2);

            #endregion

            #region nöbet üst grup kýsýtlar

            var nobetUstGrupKisitlar = b.EczaneNobetContext.NobetUstGrupKisitlar
                .Where(w => w.NobetUstGrupId == 4)//çorum
                .ToList();

            if (b.NobetUstGruplar != null)
            {
                var kisitlar = new List<NobetUstGrupKisit>();

                foreach (var nobetUstGrupKisit in nobetUstGrupKisitlar)
                {
                    var nobetUstGrupKisit2 = new NobetUstGrupKisit()
                    {
                        KisitId = nobetUstGrupKisit.KisitId,
                        NobetUstGrupId = b.NobetUstGrupId,
                        SagTarafDegeri = nobetUstGrupKisit.SagTarafDegeri,
                        SagTarafDegeriVarsayilan = nobetUstGrupKisit.SagTarafDegeriVarsayilan,
                        PasifMi = nobetUstGrupKisit.PasifMi,
                        VarsayilanPasifMi = nobetUstGrupKisit.VarsayilanPasifMi
                    };

                    kisitlar.Add(nobetUstGrupKisit2);
                }

                b.EczaneNobetContext.NobetUstGrupKisitlar.AddOrUpdate(s => new { s.NobetUstGrupId, s.KisitId }, kisitlar.ToArray());
                b.EczaneNobetContext.SaveChanges();
            }

            #endregion
        }

        private static void TalepEkle(Concrete.EntityFramework.Contexts.EczaneNobetContext context, int nobetGrupGorevTipId, int nobetciSayisi)
        {
            var ozelTalepTarihleri = context.Takvimler
                .Where(w => SqlFunctions.DatePart("weekday", w.Tarih) == 7
                        && (w.Tarih.Year >= 2019 && w.Tarih.Month >= 4)).ToList();

            var talepler = new List<NobetGrupTalep>();

            foreach (var tarih in ozelTalepTarihleri)
            {
                talepler.Add(new NobetGrupTalep()
                {
                    NobetciSayisi = nobetciSayisi,
                    NobetGrupGorevTipId = nobetGrupGorevTipId,
                    TakvimId = tarih.Id
                });
            }

            context.NobetGrupTalepler.AddOrUpdate(s => new { s.TakvimId, s.NobetGrupGorevTipId }, talepler.ToArray());
            context.SaveChanges();
        }

        private static void NobetGrupGorevTipTakvimOzelGunEkle(Concrete.EntityFramework.Contexts.EczaneNobetContext context, int nobetGrupGorevTipId)
        {
            var bayramlar2 = context.NobetGrupGorevTipTakvimOzelGunler
                .Where(w => w.NobetGrupGorevTipGunKural.NobetGrupGorevTip.Id == 63
                //&& w.Takvim.Tarih >= new DateTime(2020, 6, 1)
                //&& w.Takvim.Tarih < new DateTime(2020, 10, 1)
                //&& w.NobetOzelGunId != 10 
                //arife
                //&& !(((int)w.Takvim.Tarih.DayOfWeek + 1 == 1 || (int)w.Takvim.Tarih.DayOfWeek + 1 == 6) && w.NobetOzelGunId == 9)
                //&& !(((int)SqlFunctions.DatePart("weekday", w.Takvim.Tarih) == 1 || (int)SqlFunctions.DatePart("weekday", w.Takvim.Tarih) == 7) && w.NobetGunKuralId == 9)
                //cumartesi, pazar hariç olduðunda üst satýr açýlacak
                )
                .ToList();

            var nobetGrupGorevTipTakvimOzelGunler = new List<NobetGrupGorevTipTakvimOzelGun>();

            foreach (var bayram in bayramlar2)
            {
                var nobetGrupGorevTipGunKural = context.NobetGrupGorevTipGunKurallar
                    .SingleOrDefault(w => w.NobetGunKuralId == bayram.NobetGunKuralId
                                       && w.NobetGrupGorevTipId == nobetGrupGorevTipId);

                var nobetGrupGorevTipTakvimOzelGun = new NobetGrupGorevTipTakvimOzelGun()
                {
                    TakvimId = bayram.TakvimId,
                    NobetGunKuralId = nobetGrupGorevTipGunKural.NobetGunKuralId,
                    NobetGrupGorevTipGunKuralId = nobetGrupGorevTipGunKural.Id,
                    NobetOzelGunId = bayram.NobetOzelGunId,
                    NobetOzelGunKategoriId = 1
                };

                nobetGrupGorevTipTakvimOzelGunler.Add(nobetGrupGorevTipTakvimOzelGun);
            }

            context.NobetGrupGorevTipTakvimOzelGunler.AddOrUpdate(s => new { s.TakvimId, s.NobetGunKuralId, s.NobetOzelGunId, s.NobetGrupGorevTipGunKuralId }, nobetGrupGorevTipTakvimOzelGunler.ToArray());
            context.SaveChanges();
        }

        private static void NobetGrupGunKuralEkle(Concrete.EntityFramework.Contexts.EczaneNobetContext context,
            DateTime baslamaTarihi,
            int nobetUstGrupId,
            List<NobetGrupGorevTip> nobetGrupVeGorevTipler,
            int varsayilanNobetciSayisi)
        {
            var nobetGrupGorevTipGunKuralListe = context.NobetGrupGorevTipGunKurallar
                            .Where(w => w.NobetGrupGorevTipId == 49) //antalya 11. grup
                            .ToList();

            var nobetGorevTipler = nobetGrupVeGorevTipler.Select(s => s.NobetGorevTipId);
            var nobetGruplar = nobetGrupVeGorevTipler.Select(s => s.NobetGrupId);

            var nobetGrupGorevTipler = context.NobetGrupGorevTipler
                .Where(w => w.NobetGrup.NobetUstGrupId == nobetUstGrupId
                         && nobetGorevTipler.Contains(w.NobetGorevTipId)
                         && nobetGruplar.Contains(w.NobetGrupId)
                         ).ToList();

            NobetGrupGunKuralEkle2(context, baslamaTarihi, nobetUstGrupId, nobetGrupGorevTipGunKuralListe, nobetGrupGorevTipler, varsayilanNobetciSayisi);
        }

        private static void NobetGrupGunKuralEkle2(Concrete.EntityFramework.Contexts.EczaneNobetContext context,
            DateTime baslamaTarihi,
            int nobetUstGrupId,
            List<NobetGrupGorevTipGunKural> nobetGrupGorevTipGunKuralListe,
            List<NobetGrupGorevTip> nobetGrupGorevTipler,
            int varsayilanNobetciSayisi)
        {
            var nobetGrupGorevTipGunKurallar = new List<NobetGrupGorevTipGunKural>();

            foreach (var nobetGrupGorevTip in nobetGrupGorevTipler)
            {
                //if (nobetGrupGorevTip.Id == 43)
                //{
                //    nobetGrupGorevTipGunKuralListe = nobetGrupGorevTipGunKuralListe.Where(w => w.NobetGunKuralId == 7).ToList();
                //}

                foreach (var nobetGrupGunKural in nobetGrupGorevTipGunKuralListe)
                {
                    var nobetGrupGorevTipGunKural = new NobetGrupGorevTipGunKural()
                    {
                        NobetGrupGorevTipId = nobetGrupGorevTip.Id,
                        NobetGunKuralId = nobetGrupGunKural.NobetGunKuralId,
                        BaslangicTarihi = baslamaTarihi,
                        NobetUstGrupGunGrupId = GetNobetUstGrupGunGrupId(nobetUstGrupId, nobetGrupGunKural.NobetGunKuralId, context.NobetUstGrupGunGruplar.ToList()),
                        NobetciSayisi = varsayilanNobetciSayisi
                    };

                    nobetGrupGorevTipGunKurallar.Add(nobetGrupGorevTipGunKural);
                }

                context.NobetGrupGorevTipGunKurallar.AddOrUpdate(s => new { s.NobetGrupGorevTipId, s.NobetGunKuralId, s.NobetUstGrupGunGrupId }, nobetGrupGorevTipGunKurallar.ToArray());
                context.SaveChanges();
            }
        }

        private static void NobetGrupGunKuralEkle(
            Concrete.EntityFramework.Contexts.EczaneNobetContext context,
            DateTime baslamaTarihi,
            int nobetUstGrupId,
            List<int> nobetGrupGorevTipIdList,
            int varsayilanNobetciSayisi)
        {
            var nobetGrupGorevTipGunKuralListe = context.NobetGrupGorevTipGunKurallar
                            .Where(w => w.NobetGrupGorevTipId == 24) //antalya 11. grup
                            .ToList();

            var nobetGrupGorevTipler = context.NobetGrupGorevTipler
                .Where(w => w.NobetGrup.NobetUstGrupId == nobetUstGrupId
                         && nobetGrupGorevTipIdList.Contains(w.Id)).ToList();

            NobetGrupGunKuralEkle2(context, baslamaTarihi, nobetUstGrupId, nobetGrupGorevTipGunKuralListe, nobetGrupGorevTipler, varsayilanNobetciSayisi);
        }

        private static void NobetGrupGunKuralEkle(Concrete.EntityFramework.Contexts.EczaneNobetContext context,
            DateTime baslamaTarihi,
            int nobetUstGrupId,
            List<int> nobetGrupGorevTipIdList,
            int varsayilanNobetciSayisi,
            int alinacakNobetGrupGorevTipId)
        {
            var nobetGrupGorevTipGunKuralListe = context.NobetGrupGorevTipGunKurallar
                            .Where(w => w.NobetGrupGorevTipId == alinacakNobetGrupGorevTipId
                            //&& (w.NobetGunKuralId == 1 || w.NobetGunKuralId == 7)
                            )
                            .ToList();

            var nobetGrupGorevTipler = context.NobetGrupGorevTipler
                .Where(w => w.NobetGrup.NobetUstGrupId == nobetUstGrupId
                         && nobetGrupGorevTipIdList.Contains(w.Id)).ToList();

            NobetGrupGunKuralEkle2(context, baslamaTarihi, nobetUstGrupId, nobetGrupGorevTipGunKuralListe, nobetGrupGorevTipler, varsayilanNobetciSayisi);
        }

        private static int GetNobetUstGrupGunGrupId(int nobetUstGrupId, int nobetGunKuralId, List<NobetUstGrupGunGrup> nobetUstGrupGunGruplar)
        {
            int nobetUstGrupGunGrupId = 0;

            //if (nobetGunKuralId == 1)
            //{//pazar
            //    var nobetUstGrupGunGrup = nobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 1);
            //    nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
            //}
            //else if (nobetGunKuralId == 7)
            //{//cumartesi
            //    var nobetUstGrupGunGrup = nobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 4);
            //    nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
            //}

            if (nobetGunKuralId == 1)
            {//pazar
                var nobetUstGrupGunGrup = nobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 1);
                nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
            }
            if (nobetGunKuralId == 7)
            {//c.tesi
                var nobetUstGrupGunGrup = nobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 1);
                nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
            }
            if (nobetGunKuralId == 10)
            {//c.tesi
                var nobetUstGrupGunGrup = nobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 5);
                nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
            }
            else if (nobetGunKuralId > 1 && nobetGunKuralId < 7)
            {//hafta içi
                var nobetUstGrupGunGrup = nobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 3);
                nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
            }
            //else if (nobetGunKuralId == 7)
            //{
            //    if (nobetUstGrupId == 3 || nobetUstGrupId == 5 || nobetUstGrupId == 6 || nobetUstGrupId == 11)
            //    {//cumartesi, varsa
            //        var nobetUstGrupGunGrup = nobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 4);
            //        nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
            //    }
            //    else
            //    {//hafta içi
            //        var nobetUstGrupGunGrup = nobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 3);
            //        nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
            //    }
            //}
            else
            {//bayram
                var nobetUstGrupGunGrup = nobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 2);
                nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
            }

            return nobetUstGrupGunGrupId;
        }
    }

    class GerekliBilgiler
    {
        public GerekliBilgiler()
        {

        }

        public GerekliBilgiler(Concrete.EntityFramework.Contexts.EczaneNobetContext context, int odaId, int nobetUstGrupId, int nobetGrupGorevTipId, DateTime baslamaTarihi, int varsayilanNobetciSayisi)
        {
            EczaneNobetContext = context;
            OdaId = odaId;
            NobetUstGrupId = nobetUstGrupId;
            NobetGrupGorevTipId = nobetGrupGorevTipId;
            BaslamaTarihi = baslamaTarihi;
            VarsayilanNobetciSayisi = varsayilanNobetciSayisi;
        }

        public int VarsayilanNobetciSayisi { get; set; }
        public Concrete.EntityFramework.Contexts.EczaneNobetContext EczaneNobetContext { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public int OdaId { get; set; }
        public int NobetUstGrupId { get; set; }
        public List<EczaneOda> EczaneOdalalar { get; set; }
        public List<Eczane> Eczaneler { get; set; }
        public List<NobetGrup> NobetGruplar { get; internal set; }
        public List<NobetUstGrup> NobetUstGruplar { get; internal set; }
        public List<KullaniciRolEkle> KullaniciRoller { get; internal set; }
        public List<User> Kullanicilar { get; internal set; }

        public List<NobetGrupKural> NobetGrupKurallar { get; internal set; }
        public List<NobetUstGrupGunGrup> NobetUstGrupGunGruplar { get; internal set; }
        public int NobetGrupGorevTipId { get; internal set; }
        public List<NobetKural> NobetKurallar { get; internal set; }
        //public List<UserRole> KullaniciRoller { get; internal set; }
    }
}

/*ordu
 
    var gerekliBilgilerOrdu = new GerekliBilgiler(context, odaId, nobetUstGrupId, nobetGrupGorevTipId, baslamaTarihi, varsayilanNobetciSayisi)
            {
                //var baslamaTarihi = new DateTime(2019, 3, 5);
                //var odaId = 6;
                //var nobetUstGrupId = 7;
                //var nobetGrupGorevTipId = 30;

                BaslamaTarihi = baslamaTarihi, // new DateTime(2019, 3, 5),

                EczaneOdalalar = context.EczaneOdalar.Where(w => w.Id == 1).ToList(),

                Eczaneler = new List<Eczane>()
                            {
                                #region manavgat
new Eczane{ Adi="ALTAÞ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ALTINORDU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ARSLAN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="CANDAN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="CANSUDERE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DEMÝR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DEVA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DÝLEK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DÝRÝM", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DOÐA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DURU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ERGÜL", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="GÜNDOÐDU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="GÜNEÞ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ÝBNÝSÝNA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KATIRCIOÐLU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KAYMAK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KOÇAK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="NÝLÜFER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="PARK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="PINAR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="POYRAZ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SANAYÝ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ÞEYMA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ÞÝMÞEK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TUBA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TÜRKMENLER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ACAR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="AYDIN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="BERRÝN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="BOZTEPE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="BÜYÜK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ÇELENK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DEFNE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DEMÝRTAÞ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DENÝZ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DERMAN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="HASTANELER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="HIZIR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ÝTÝMAT", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KARAMANOÐLU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KAYAHAN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="MERKEZ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="MURAT", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ODABAÞ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ÖMÜR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SAÐLIK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SELÝN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SUBAÞI", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TEPE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TUNA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TÜLÝN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="UÐUR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="YENÝÇARÞI", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="YENÝMAHALLE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="YÜRÜR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ARDA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="BAHÇELÝEVLER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ÇAKIR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ELÝF", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ESER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ESÝN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="FATÝH", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="FIRAT", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="FÝLÝZ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="GÜLER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="GÜNAYDIN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="GÜVEN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KARÞIYAKA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KOÇYÝÐÝT", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="MERTGERÇEKER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="NAZ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="NUR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ONUR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="PELÝN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SERDAROÐLU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ÞÝFA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TAYFUN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TÜRKMEN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="UFUK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ULUS", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ZAÝMOÐLU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ZEYNEP", AcilisTarihi=new DateTime(2020,4,1)}

                                #endregion
                            },

                NobetUstGruplar = new List<NobetUstGrup>() {
                                new NobetUstGrup(){ Adi = "Ordu", Aciklama = "Ordu Merkez", EczaneOdaId = odaId, BaslangicTarihi=baslamaTarihi, TimeLimit = 60, Enlem = 40.986166, Boylam = 37.879721 },
                            },

                NobetGruplar = new List<NobetGrup>() {
                                new NobetGrup(){ Adi = "Ordu-1", BaslamaTarihi = baslamaTarihi, NobetUstGrupId = nobetUstGrupId },
                                new NobetGrup(){ Adi = "Ordu-2", BaslamaTarihi = baslamaTarihi, NobetUstGrupId = nobetUstGrupId },
                                new NobetGrup(){ Adi = "Ordu-3", BaslamaTarihi = baslamaTarihi, NobetUstGrupId = nobetUstGrupId },
                            },

                Kullanicilar = new List<User>()
                            {
                                //new User(){ Email="odaDiyarbakir@nobetyaz.com", FirstName="Oda Diyarbakýr", LastName="Oda Diyarbakýr", Password="odaDiyarbakir8", UserName="odaDiyarbakir", BaslamaTarihi = baslamaTarihi},
                                //new User(){ Email="ustGrupDiyarbakir@nobetyaz.com", FirstName="Üst Grup", LastName="Üst grp", Password="ustGrup8", UserName="ustGrupDiyarbakir", BaslamaTarihi = baslamaTarihi},
                                new User(){ Email="ecz.zynp@gmail.com", FirstName="Zeynep", LastName="Ordu", Password="ordu2019", UserName="zeynepHaným", BaslamaTarihi = baslamaTarihi}
                            },

                NobetGrupKurallar = new List<NobetGrupKural>(),
                //{
                //new NobetGrupKural(){ NobetKuralId=1, BaslangicTarihi=baslamaTarihi, Deger=5},//Ardýþýk Boþ Gün Sayýsý
                //new NobetGrupKural(){ NobetGrupGorevTipId=28, NobetKuralId=2, BaslangicTarihi=baslamaTarihi, Deger=5},
                //new NobetGrupKural(){ NobetKuralId=3, BaslangicTarihi=baslamaTarihi, Deger=1}//Varsayýlan günlük nöbetçi sayýsý
                //},

                NobetKurallar = context.NobetKurallar.Where(w => new int[]
                {
                    1, //Ardýþýk Boþ Gün Sayýsý
                    3  //Varsayýlan günlük nöbetçi sayýsý
                }.Contains(w.Id)).ToList(),

                NobetUstGrupGunGruplar = new List<NobetUstGrupGunGrup>()
                            {
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 1, AmacFonksiyonuKatsayisi = 1000 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 2, AmacFonksiyonuKatsayisi = 8000 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 3, AmacFonksiyonuKatsayisi = 900 },
                                new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 4, AmacFonksiyonuKatsayisi = 100 },
                                //new NobetUstGrupGunGrup(){ NobetUstGrupId = nobetUstGrupId, GunGrupId = 5, AmacFonksiyonuKatsayisi = 7000 }
                            }
            };
 */
