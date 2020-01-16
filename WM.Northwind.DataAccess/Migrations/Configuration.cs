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
            //veri taban�nda de�i�ikli�e izin vermek i�in istendi�i zaman true olacak.
            AutomaticMigrationsEnabled = false;
            //alan silinece�i zaman true olacak. silmede veri kayb�n� �nlemek i�in false 
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts.EczaneNobetContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //  �rne�in yaz d�nemi cts. ile ilgili bir �ey yap�lmak isteniyorsa bunun i�in ayr� bir g�rev tipine gerek olmayabilir. bayramlar gibi �zel g�n olarak eklenip devam edilebilir.
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
            #region �rnek
            //var kisitKategoriler = new List<KisitKategori>()
            //                {
            //                    new KisitKategori(){ Adi="ktg 1 ", Aciklama = "ilk ktg" },
            //                };

            //context.KisitKategoriler.AddOrUpdate(s => new { s.Adi }, kisitKategoriler.ToArray());
            //context.SaveChanges();

            ////�rnek
            //foreach (var item in context.EczaneGrupTanimlar)
            //{
            //    item.AyniGunNobetTutabilecekEczaneSayisi = 1;
            //}
            //context.SaveChanges();

            ////�rnek
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

            #region �rnek
            //var eczaneGrupTanimTipler = new List<EczaneGrupTanimTip>()
            //                {
            //                    new EczaneGrupTanimTip(){ Adi="Co�rafi yak�nl�k" },
            //                    new EczaneGrupTanimTip(){ Adi="E� Durumu" }
            //                };

            //context.EczaneGrupTanimTipler.AddOrUpdate(s => new { s.Adi }, eczaneGrupTanimTipler.ToArray());
            //context.SaveChanges();

            //�rnek
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
new Eczane{ Adi="AK�ZCAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKSARAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKSOY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ALKAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ANTAKYA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ATE�", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="B�LD�REN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CANBOLAT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�IRAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ESENOCAK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ESENTEPE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ESRA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="FAT�H", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="FATMA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="GENCO", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="GONCA (DEFNE)", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HAKAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="IHLAMUR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="I�IK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�REZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KAT�PO�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KORO�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KUDRET", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MET�N", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="OKAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�Z", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="RENK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SAYIN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SEV�M", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="S�NAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�ENOL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="��R�NCE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="TUTAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="T�RKER", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="U�URG�L", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YASEM�N", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YA�AM", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YILMAZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YUSUF", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ZORKUN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKDO�AN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ALEV", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ANDI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="B�Y�K", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CUMHUR�YET", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DARAO�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DER�NK�K", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DERMAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DO�A", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="G�NEN�", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="G�NAL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HALK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HASTANE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HATAY G�K�E", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�PEK��O�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KARAG�Z", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KEMALPA�A", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KURTULU�", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="LEVEND", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MELTEM", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MUHS�N", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MUNA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="M�GE  ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="M�ZEYYEN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="NURSEL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�MERO�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�ZKAYA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="RE�AT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SEL�M", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SEL�N   ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SEV�N�", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="S�NER", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�ANLI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="��FA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="��R�NTEPE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="TAHS�N", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="T�LAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�M�T   ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="YEN� MOZA�K", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKADEM�", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKYOL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ASLAN ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ASLANLAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="A�KAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AYDIN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BA�ARAN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="B�NNUR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CADDE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CEM", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="CEYHUN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="C�LL�", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�ARE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�AR�I", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�INAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DAMLA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DEFNE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="EDA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ERG�N�L", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ESK�OCAK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="G�LBA�", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HATAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HEK�MO�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�MGE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="K.DALYAN MERKEZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KIRKICI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KUR�UN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KUZEYTEPE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="NAR", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="OKAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="OKYANUS", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="OVALI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�ZTOPRAK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SA�LIKPINARI", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SARAYKENT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="S�GORTA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="S�RMEL�", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="UYGUN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="Y�KSEL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ADA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKBAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="AKDEN�Z", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ALTUNAY", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ANIL U�UCU  ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ATIF", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BAYRAMO�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BET�L", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="BURCU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="B�LENT �IRAY   ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�EKMECE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DEMET", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DURU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="DURU FAYSAL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ECE G��MEN", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HALK HAR.", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HATAY HAYAT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HAYAT", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="HAYAT S�NMEZ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�Y�L�K", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KAM�L", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KESK�N", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KUMSAL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="KUNT ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MANSURO�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MEL�S CAN   ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MENEL", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="MENG�LL�O�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="NEVRA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="ORGAN�K", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�ZGE", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�ZLEM  ", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="�ZT�RK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="R�YA", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="R�YA HAR.", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SA�LIK", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SA�LIK HAR.", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SEND�O�LU", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
new Eczane{ Adi="SEV�NG�L G�K�E", AcilisTarihi=new DateTime(2020,2,1), Enlem=1, Boylam=1, NobetUstGrupId=15},
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
                                //new User(){ Email="odaDiyarbakir@nobetyaz.com", FirstName="Oda Diyarbak�r", LastName="Oda Diyarbak�r", Password="odaDiyarbakir8", UserName="odaDiyarbakir", BaslamaTarihi = baslamaTarihi},
                                //new User(){ Email="ustGrupDiyarbakir@nobetyaz.com", FirstName="�st Grup", LastName="�st grp", Password="ustGrup8", UserName="ustGrupDiyarbakir", BaslamaTarihi = baslamaTarihi},
                                new User(){ Email="utkuergonul@yahoo.com", FirstName="Utku", LastName="G�n�l", Password="antakya2020", UserName="Utku", BaslamaTarihi = baslamaTarihi}
                            },

                NobetGrupKurallar = new List<NobetGrupKural>()
                {
                    //new NobetGrupKural() { NobetKuralId = 1, BaslangicTarihi = baslamaTarihi, Deger = 5 },//Ard���k Bo� G�n Say�s�
                    ////new NobetGrupKural() { NobetGrupGorevTipId = 28, NobetKuralId = 2, BaslangicTarihi = baslamaTarihi, Deger = 5 },
                    //new NobetGrupKural() { NobetKuralId = 3, BaslangicTarihi = baslamaTarihi, Deger = 1 }//Varsay�lan g�nl�k n�bet�i say�s�
                },

                NobetKurallar = context.NobetKurallar.Where(w => new int[]
                {
                    1, //Ard���k Bo� G�n Say�s�
                    3  //Varsay�lan g�nl�k n�bet�i say�s�
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
            new User(){ Email="ozdamar85@gmail.com", FirstName="Semih", LastName="�ZDAMAR", Password="123456", UserName="semih"},
            new User(){ Email="atesates2012@gmail.com", FirstName="Ate�", LastName="Ate�", Password="123456", UserName="ates"},
            new User(){ Email="huseyinecz@gmail.com", FirstName="H�seyin", LastName="Eczane", Password="Alanya", UserName="huseyin"},

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
                                new Role(){ Name="�st Grup" },
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

            #region men�ler
            var vMenu = new List<Menu>()
            {
			//Men� Single Items 1,2
			new Menu(){ LinkText="Eczaneler", ActionName="Index", ControllerName="Eczane", AreaName="EczaneNobet", SpanCssClass="fa fa-medkit fa-lg" },
            new Menu(){ LinkText="Eczane Mazeret", ActionName="Index", ControllerName="EczaneNobetMazeret", AreaName="EczaneNobet", SpanCssClass="fa fa-bug fa-lg" },

			//Men� Dropdown Titles 3,4,5,6,7
			new Menu(){ LinkText="N�bet Gruplar", SpanCssClass="fa fa-bars"},
            new Menu(){ LinkText="Eczane Gruplar", SpanCssClass="fa fa-users"},
            new Menu(){ LinkText="Sonu�lar", SpanCssClass="fa fa-cubes" },
            new Menu(){ LinkText="Tan�mlar", SpanCssClass="fa fa-database" },
            new Menu(){ LinkText="Yetki", SpanCssClass="fa fa-shield" },
            new Menu(){ LinkText="N�bet Kural", SpanCssClass="fa fa-cogs" }
            };

            context.Menuler.AddOrUpdate(s => new { s.LinkText }, vMenu.ToArray());
            //vMenu.ForEach(d => context.Menuler.Add(d));
            context.SaveChanges();
            #endregion

            #region men� roller
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
								
								//n�bet �st grup 
								new MenuRole(){ MenuId=1, RoleId=3 },
                                new MenuRole(){ MenuId=2, RoleId=3 },
                                new MenuRole(){ MenuId=3, RoleId=3 },
                                new MenuRole(){ MenuId=4, RoleId=3 },
                                new MenuRole(){ MenuId=5, RoleId=3 },
                                new MenuRole(){ MenuId=7, RoleId=3 },
                                new MenuRole(){ MenuId=8, RoleId=3 },

								//eczac�
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

            #region men� altlar
            var menuAltlar = new List<MenuAlt>()
            {
				//N�bet Gruplar 1,2,3
				new MenuAlt(){ LinkText="N�bet �st Grup", ActionName="Index", ControllerName="NobetUstGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3 },
                new MenuAlt(){ LinkText="N�bet Grup", ActionName="Index", ControllerName="NobetGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3 },
                new MenuAlt(){ LinkText="Eczane N�bet Grup", ActionName="Index", ControllerName="EczaneNobetGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3 },

				//Eczane Gruplar 4,5
				new MenuAlt(){ LinkText="Eczane Grup Tan�m", ActionName="Index", ControllerName="EczaneGrupTanim", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=4 },
                new MenuAlt(){ LinkText="Eczane Grup", ActionName="Index", ControllerName="EczaneGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=4},

				//Sonu�lar 6,7
				new MenuAlt(){ LinkText="Pivot Tablo", ActionName="Index", ControllerName="EczaneNobetSonuc", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=5 },
                new MenuAlt(){ LinkText="G�rsel Sonu�lar", ActionName="GorselSonuclar", ControllerName="EczaneNobetSonuc", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=5},

				//tan�mlar 8,9,10
				new MenuAlt(){ LinkText="Eczanene Oda", ActionName="Index", ControllerName="EczaneOda", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6 },
                new MenuAlt(){ LinkText="Mazeret T�r", ActionName="Index", ControllerName="MazeretTur", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6 },
                new MenuAlt(){ LinkText="Mazeret", ActionName="Index", ControllerName="Mazeret", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},

				//yetkiler 11,12,13,14,15,16,17,18
				new MenuAlt(){ LinkText="Kullan�c�", ActionName="Register", ControllerName="Account", AreaName="", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Men�", ActionName="Index", ControllerName="Menu", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Men� Alt", ActionName="Index", ControllerName="MenuAlt", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Men� Rol", ActionName="Index", ControllerName="MenuRole", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Men� Alt Rol", ActionName="Index", ControllerName="MenuAltRole", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Kullan�c� Oda", ActionName="Index", ControllerName="UserOda", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Kullan�c� N�bet �st Grup", ActionName="Index", ControllerName="UserNobetUstGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Kullan�c� Eczane", ActionName="Index", ControllerName="UserEczane", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},

				//tan�mlar 19,20,21,22,23,24,25,26
				new MenuAlt(){ LinkText="�stek", ActionName="Index", ControllerName="Istek", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="�stek T�r", ActionName="Index", ControllerName="IstekTur", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="N�bet G�rev Tip", ActionName="Index", ControllerName="NobetGorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="�ehir", ActionName="Index", ControllerName="Sehir", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="�l�e", ActionName="Index", ControllerName="Ilce", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Rol", ActionName="Index", ControllerName="Role", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},

                new MenuAlt(){ LinkText="Eczane N�bet Mazeret", ActionName="Index", ControllerName="EczaneNobetMazeret", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=2},
                new MenuAlt(){ LinkText="Eczane N�bet �stek", ActionName="Index", ControllerName="EczaneNobetIstek", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=2},

				//yetkiler 27
				new MenuAlt(){ LinkText="Kullan�c� Rol", ActionName="Index", ControllerName="UserRole", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},

				//n�bet kural 28,29
				new MenuAlt(){ LinkText="N�bet Kural", ActionName="Index", ControllerName="NobetKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},
                new MenuAlt(){ LinkText="N�bet Grup Kural", ActionName="Index", ControllerName="NobetGrupKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},

				//tan�mlar 30,31,32
				new MenuAlt(){ LinkText="G�rev Tip", ActionName="Index", ControllerName="GorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Eczane G�rev Tip", ActionName="Index", ControllerName="EczaneGorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="N�bet Grup G�rev Tip", ActionName="Index", ControllerName="NobetGrupGorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},

				//N�bet Gruplar 33
				new MenuAlt(){ LinkText="N�bet Alt Grup", ActionName="Index", ControllerName="NobetAltGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3},

				//n�bet kural 34
				new MenuAlt(){ LinkText="N�bet Grup Talepler", ActionName="Index", ControllerName="NobetGrupTalep", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},

				//n�bet kural 35,36
				new MenuAlt(){ LinkText="N�bet G�n Kural", ActionName="Index", ControllerName="NobetGunKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},
                new MenuAlt(){ LinkText="N�bet Grup G�n Kural", ActionName="Index", ControllerName="NobetGrupGunKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},
            };

            context.MenuAltlar.AddOrUpdate(s => new { s.LinkText }, menuAltlar.ToArray());
            //vMenuAlt.ForEach(d => context.MenuAltlar.Add(d));
            context.SaveChanges();

            #endregion

            #region men� alt roller

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
								
								#region n�bet �st grup
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
								
								#region eczac�
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

            #region n�bet g�n kurallar
            var nobetGunKurallar = new List<NobetGunKural>()
                            {
                                new NobetGunKural(){ Adi="Pazar", Aciklama="Pazar g�n� e�it da��l�ma dahil olsun"},
                                new NobetGunKural(){ Adi="Pazartesi", Aciklama="Pazartesi g�n� e�it da��l�ma dahil olsun"},
                                new NobetGunKural(){ Adi="Sal�", Aciklama="Sal� g�n� e�it da��l�ma dahil olsun"},
                                new NobetGunKural(){ Adi="�ar�amba", Aciklama="�ar�amba g�n� e�it da��l�ma dahil olsun"},
                                new NobetGunKural(){ Adi="Per�embe", Aciklama="Per�embe g�n� e�it da��l�ma dahil olsun"},
                                new NobetGunKural(){ Adi="Cuma", Aciklama="Cuma g�n� e�it da��l�ma dahil olsun"},
                                new NobetGunKural(){ Adi="Cumartesi", Aciklama="Cumartesi g�n� e�it da��l�ma dahil olsun"},
                                new NobetGunKural(){ Adi="Dini Bayram", Aciklama="Dini Bayram e�it da��l�ma dahil olsun"},
                                new NobetGunKural(){ Adi="Milli Bayram", Aciklama="Milli Bayram e�it da��l�ma dahil olsun"},
                            };

            context.NobetGunKurallar.AddOrUpdate(s => new { s.Adi }, nobetGunKurallar.ToArray());
            //nobetGunKurallar.ForEach(d => context.NobetGunKurallar.Add(d));
            context.SaveChanges();
            #endregion

            #region n�bet kurallar
            var nobetKurallar = new List<NobetKural>()
                            {
                                new NobetKural(){ Adi="Ard���k N�bet Say�s�", Aciklama="Pe�pe�e n�bet yaz�lmayacak ard���k g�n say�s�"},
                                new NobetKural(){ Adi="Birlikte N�bet Say�s�", Aciklama="Ayn� g�ne denk gelen n�bet say�s�"}, // bu say� i�in 4 uygun
								new NobetKural(){ Adi="Varsay�lan N�bet Say�s�", Aciklama="Grubun varsay�lan n�bet say�s�"} // bu say� i�in 1 uygun
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

            #region n�bet g�rev tipler
            var nobbetGorevTipler = new List<NobetGorevTip>()
            {
                                new NobetGorevTip(){Adi = "Tam G�n N�bet�i"},
                                new NobetGorevTip(){Adi = "G�nd�z N�bet�i"}
            };
            context.NobetGorevTipler.AddOrUpdate(s => new { s.Adi }, nobbetGorevTipler.ToArray());
            //nobbetGorevTipler.ForEach(d => context.NobetGorevTipler.Add(d));
            context.SaveChanges();
            #endregion

            #region bayram t�rler
            var bayramTurler = new List<BayramTur>()
                            {
                                new BayramTur(){ Adi="Y�lba��" },
                                new BayramTur(){ Adi="23 Nisan" },
                                new BayramTur(){ Adi="1 May�s Emek ve Dayan��ma G�n�" },
                                new BayramTur(){ Adi="Zafer Bayram�" },
                                new BayramTur(){ Adi="29 Ekim Cumhuriyet Bayram�" },
                                new BayramTur(){ Adi="Ramazan Bayram�" },
                                new BayramTur(){ Adi="Kurban Bayram�" }
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

            #region n�bet �st gruplar
            var nobetUstGruplar = new List<NobetUstGrup>() {
                new NobetUstGrup(){Adi = "Alanya",Aciklama = "Antalya il�esi",EczaneOdaId = 1, BaslangicTarihi=new DateTime(2018,1,1)},
                new NobetUstGrup(){Adi = "Antalya Merkez",Aciklama = "Antalya merkez",EczaneOdaId = 1, BaslangicTarihi=new DateTime(2018,1,1)},
            };

            context.NobetUstGruplar.AddOrUpdate(s => new { s.Adi }, nobetUstGruplar.ToArray());
            context.SaveChanges();
            #endregion

            #region n�bet gruplar

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

            #region n�bet grup kurallar
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

            #region n�bet grup g�rev tipler
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

            #region n�bet grup talepler
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
                new Eczane { Adi = "ALA�YE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "S�NAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "G�LAY", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AK�ALIO�LU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "G�LER", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NAZ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AY", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NUR", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "EY�P", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "��M�EK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AYDO�AN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "�EKER", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "G�NEYL�O�LU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SARE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AYNUR", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "FARUK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "�KS�R", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "H�DAYET", AcilisTarihi = new DateTime(2018,1,1) }, 
				#endregion
				
				#region 2
				new Eczane { Adi = "MARTI", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "DEFNE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TEZCAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "KAMBURO�LU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "Y�KSEK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NOYAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ARIKAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "KASAPO�LU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "�AH�N", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "KO�AK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "G�KSU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ASLI", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AKSU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALANYA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SELCEN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "S�PAH�O�LU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "HAYAT", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TOROS", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALPER", AcilisTarihi = new DateTime(2018,1,1) }, 
				#endregion
				
				#region 3
				new Eczane { Adi = "SA�LIK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ECE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "��KRAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TUNA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TURUN�", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "G�LERY�Z", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "B�KE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BA�AK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BERNA AK�ALIO�LU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALTUNBA�", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TU�BA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "N�SA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "��R�N", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AYY�CE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "G�NE�", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SEV�ND�", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "B�LGE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "F�L�Z", AcilisTarihi = new DateTime(2018,1,1) },
				#endregion
				#endregion

				#region Antalya
		 
				#region 1
				new Eczane { Adi = "SEVDA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� YAPRAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�VEN�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PORTAKAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "���DEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PAST�R", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANSU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "H�LAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BUYRUK�U", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KO�AK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "IRMAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUNAHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NOKTA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DER�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZMEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "RODOPLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SOYAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FUNDA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KIYMET", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� N�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIZ ADEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�AVDIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� MELTEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EL�F �NCE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAFRAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MELTEM MERT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PAMUK �EKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURDUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEFNE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NERG�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZNUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�LAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "S�Z�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�Z�A�LAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� ELMALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "T�RKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�NDER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�KY�Z�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SE�K�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KALE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LOKMAN NUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�NEY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ORU�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "Y���T", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OLGAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FEYZA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "F�DAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "A�LE=�ZG�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZEYT�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAYSAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEN�Z�N", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 2
				new Eczane { Adi = "BAHAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ITIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "D�LA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�REL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� BURDUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�LL�K", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERP�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DURUKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KORAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZLEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASPENDOS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERTEK�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SER�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YAKAMOZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYG�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�L�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�K�EN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SED�R", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERK�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELMALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERK�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�NE�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�M�R", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "VER�ML�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ARAMPOL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELVAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MET�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�OBAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� �ZLEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "B�LGEHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�M�T", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HONAMLI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DO�U�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURCU DURAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MEK�K", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ULE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYKUT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ILGIN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AZ�M", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 3
				new Eczane { Adi = "ET�LER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEV�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN�G�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "T�L�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BEYAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "D�ZG�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TU�TEK�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BATUHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "K�K", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KADIO�LU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AL�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KONUKSEVER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� I�IK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GAMZEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OKTAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EVRE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SE�K�NER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AL� BERK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERTAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�VEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NA�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CEMRE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EZDEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AVDANLIO�LU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GAMZE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MAV�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "D�K�L�TA�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BALCI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SA�LIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MEVLANA", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 4
				new Eczane { Adi = "C�HAN D�N�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MEYDAN ALPER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZEYNEP AKMAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� HAYAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MUZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DO�U GARAJI SA�LIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CUMHUR�YET", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� TUBA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "U�UR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PINAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OLCAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOKU�O�LU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YUNUS EMRE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKDEN�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�K�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�K�EN EFE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MURATPA�A", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MURAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KARAMANLI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "U�URCAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEMET", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�AYBA�I", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MERVE=ANTALYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BEYZA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ULU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "K�RAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERKAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAPLAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "M�JGAN", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 5
				new Eczane { Adi = "KEREM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEZ�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MANDAL�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ANANAS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�D�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZUHAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEREN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PORTAKAL ���E��", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TAR�IN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EVR�M", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GEN��", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BULVAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BENG�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERHAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FULYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PERGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "S�NMEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TERMESSOS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LOTUS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZENCEF�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARZU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "I�IN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DORUK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�AMLILAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "K�Y�M", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�END�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TURUN�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FLORA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�A�LA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PARK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SENTEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ANADOLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIRIM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DO�A", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KESK�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "N�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BALKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YE��LBAH�E", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "M��REN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAH�EL�EVLER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KUMBUL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TALYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EL�FSU", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 6
				new Eczane { Adi = "VOLKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALARA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�BRAH�M �ZER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YAL�INER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SIHH�YE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURCU-M", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERR�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TU�BA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FARMA LARA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GENCA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANDEM�R", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZDEN�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�KAL�PTUS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�K�ZLER=SARIKUM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FERAH", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TURKUAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "��R�NYALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUANA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OKYANUS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DORA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "V�TAM�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "I�ILAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAHADIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEMA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�NAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZKENT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZDEM�R", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�K�E", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAH�EL�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EL�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "POSTACILAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ATLAS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� YILDIZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERTER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BARINAKLAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LARA SA�LIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURCU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�NCESU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZG�R", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DAMLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "U�UR UYSAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SIHHAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERDEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�RNEKK�Y", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AK�EH�R", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 7
				new Eczane { Adi = "G�LENY�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "B�Y�K", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEVA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EL�F", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BA�G�R", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ER��N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�LKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYDINLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERSOY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEL�S", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MASAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�K�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "K�LT�R", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOLUNAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAZLI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CENG�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TEM�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ESRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MERKEZ G�LH�SAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARSLAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ESEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEMT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�A�LAYAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BA�AK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MELDA", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 8
				new Eczane { Adi = "ARAPSUYU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ATILGAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANSEV", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SELM�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KIVAN�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�LNUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KONYAALTI B�LGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AVKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PERA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�ZEL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "L�MAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EZG�M", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZSOY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DURU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EL�T", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AL�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SUN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KURTO�LU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TALYA SU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EGE BORA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�L��FA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEN�Z YILDIZI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALTINKUM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PAPATYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELV�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DA�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "S�MGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ESMELER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKINCI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BI�AK�I", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "T�T�NC�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UYAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TU��E", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SOYT�RK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYSUN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LEYLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DO�ANTAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUNCAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� ARAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ANTALYA MODERN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALPSOY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MATRU�KA=KB", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 9
				new Eczane { Adi = "S���T", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "��ZEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TEZCAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERDO�AN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DUYGU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CERENSU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEHER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KEPEZ ANADOLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�ZDE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AY�EG�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�NANIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKTAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TATLICAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZCAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAYRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "K���KSU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�NSAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�LTALYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAD�REM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ALIM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "G�LEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NEH�R", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TA�EL�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BARI�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "D�L�AD", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KEPEZALTI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERE�L� ANIL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOPKAYA", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 10
				new Eczane { Adi = "S�BEL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UTKU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FREZYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HAZAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEVG�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYL�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TURGAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YE��LIRMAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�KRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEZER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NEZ�H", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIZ R�YA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AS�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EDA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MERKEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EMEK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKYILDIZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "B�LGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LEVENT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALKI�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAKARYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BABACAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEM�RG�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YURTPINAR U�UR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "Y�CEL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "N�LAY", AcilisTarihi = new DateTime(2018,2,1) }, 
				#endregion
				
				#region 11
				new Eczane { Adi = "YEN� VARSAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "Y���TBA�I", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LALE PARK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEN� EGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEM�H", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "F�L�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ONUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DO�ANT�RK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "C�HAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BEREN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DO�RU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KORKUTEL�", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAR ���E��", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "VARSAK G�NEY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAYGILI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DUYGU TOPLUK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EYL�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAH�L", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZERR�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "S�T��LER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERRA BALTA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ATA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GEB�Z", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KALAYCI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YE��LYAYLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "D�NYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SARI�OBAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "S�T��LER SA�LIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASYA KEPEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "B�LLUR �EL�K", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BANU YAL�IN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LOKMAN HEK�M", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HAYAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KARAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TALAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARPEK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CEVHER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUNCALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KURU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�ZYURT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KOLSUZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKT�N", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "�MER YILDIZ", AcilisTarihi = new DateTime(2018,2,1) }, 
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
       
new Eczane { Id =953,Enlem=37.083649,Boylam= 36.265817,Adres ="KAZIM KARABEK�R MAH.6502 SK.9102", AdresTarifiKisa = "Kad�n Do�um Hastanesi Yan�", TelefonNo = "3288141474"},
new Eczane { Id =932,Enlem=37.074959,Boylam= 36.250194,Adres ="RAHIME HATUN MAH. DR. SADIK AHMET CAD. NO:22/A MERKEZ/OSMANIYE", AdresTarifiKisa = "Cumhuriyet Meydan� Kar��s�", TelefonNo = "3288129400"},
new Eczane { Id =918,Enlem=37.0675690,Boylam=36.2490990,Adres ="YED�OCAK MAH.DR.DEVLET BAH�EL� BUL.NO 70/B MERKEZ/OSMANIYE", AdresTarifiKisa = "Dr. Devlet Bah�eli bulvar� 3. Etap 17 nolu aile hekimli�i kar��s�", TelefonNo = "3288142051"},
new Eczane { Id =940,Enlem=37.080859,Boylam= 36.252531,Adres ="RIZA�YE MAH.�SKENDER T�RKMEN CAD.NO:106/A", AdresTarifiKisa = "R�zaiye Mah.�.T�rkmen Cad.Kadirli Yolu �zeri B�M Civar�", TelefonNo = "3284000086"},
new Eczane { Id =903,Enlem=37.063517,Boylam= 36.235955,Adres ="ADNAN MENDERES MAH.19538 SK.NO:6/B", AdresTarifiKisa = "AVM OTOPARK �IKI�I SA�LIK OCA�I KAR�ISI", TelefonNo = "3288257013"},
new Eczane { Id =951,Enlem=37.083783,Boylam= 36.265931,Adres ="KAZIM KARABEK�R MH.6502 SK.NO:11/A", AdresTarifiKisa = "Kad�n Do�um Hastanesi Yan�", TelefonNo = "3288148333"},
new Eczane { Id =926,Enlem=37.076427,Boylam= 36.246032,Adres ="ESENEVLER MH.KAYALAR SK.NO:8/B", AdresTarifiKisa = "Esenevler mahallesi kurtulu� sa�l�k oca�� ilerisi B�y�k PTT arkas� Eski vali kona�� kar��s�", TelefonNo = "3288125678"},
new Eczane { Id =927,Enlem=37.076632,Boylam= 36.246031,Adres ="ESENEVLER MAHALLES� KAYALAR SOKAK NO:8/A", AdresTarifiKisa = "kurtulu� sa�l�k oca�� yan�", TelefonNo = "3287862123"},
new Eczane { Id =900,Enlem=37.063006,Boylam= 36.236542,Adres ="ADNAN MENDERES MAH. 19537 SK.NO:5/E", AdresTarifiKisa = "Park 328 AVM Arkas�, Metin Tamer Sitesi 11.Blok No:34", TelefonNo = "3288260390"},
new Eczane { Id =912,Enlem=37.073745,Boylam= 36.235997,Adres ="YILDIRIM BEYAZIT MAH.14015 SK.NO:1/A", AdresTarifiKisa = "Musa �ahin bulvar� G�lbah�em sitesinden a�a�� 400 m ileride cuma pazar� giri�i sol k��e", TelefonNo = "3288133464"},
new Eczane { Id =907,Enlem=37.062219,Boylam= 36.238711,Adres ="MEHMET AKIF ERSOY MAH. ATAT�RK CAD. NO:475 B-C MERKEZ/OSMANIYE", AdresTarifiKisa = "�ZEL YEN� HAYAT HASTANES� YANI", TelefonNo = "3288250055"},
new Eczane { Id =938,Enlem=37.072128,Boylam= 36.252760,Adres ="HACI OSMANLI MH. DR. AHMET SADIK CD.DANIS APT. NO:86/A MERKEZ/OSMANIYE", AdresTarifiKisa = "Dr.Sad�k Ahmet Cd.Eski Vergi Dairesi Kar��s�", TelefonNo = "3288128405"},
new Eczane { Id =936,Enlem=37.073538,Boylam= 36.251795,Adres ="ATAT�RK CAD. IS BANKASI KARSISI NO:179 MERKEZ/OSMANIYE", AdresTarifiKisa = "ATAT�RK CAD. IS BANKASI KARSISI", TelefonNo = "3288141617"},
new Eczane { Id =954,Enlem=37.059333,Boylam= 36.246000,Adres ="MEHMET AK�F ERSOY MAH.8507 (AZ�Z H�DA�) SK.NO:58/A", AdresTarifiKisa = "", TelefonNo = "3284300020"},
new Eczane { Id =937,Enlem=37.072714,Boylam= 36.252197,Adres ="ISTIKLAL MAH. DR. SADIK AHMET CAD. NO:99 MERKEZ/OSMANIYE", AdresTarifiKisa = "Dr.Sad�k Ahmet Cd.�INARLI KAHVE KAR�ISI", TelefonNo = "3288143420"},
new Eczane { Id =942,Enlem=37.082748,Boylam= 36.258156,Adres ="M. FEVZI �AKMAK MH. HOCA AHMET YESEVI CD. NO:67/c", AdresTarifiKisa = "Nahar Yolu Cumhuriyet Sa�l�k Oca�� Kar��s�", TelefonNo = "3288136722"},
new Eczane { Id =934,Enlem=37.073472,Boylam= 36.250960,Adres ="ALIBEYLI MH. ATAT�RK CD. NO:186 MERKEZ/OSMANIYE", AdresTarifiKisa = "Atat�rk Cad.�AR�I �� Bankas� Civar� Vestel Bayii Yan�", TelefonNo = "3288143848"},
new Eczane { Id =899,Enlem=37.059034,Boylam= 36.256193,Adres ="Karaboyunlu Mah.5550 Sk.No:31/A", AdresTarifiKisa = "KARABOYUNLU SA�LIK OCA�I KAR�ISI", TelefonNo = "3288250065"},
new Eczane { Id =950,Enlem=37.083597,Boylam= 36.266085,Adres ="KAZIM KARABEK�R MH.6502 SK.NO:11/B", AdresTarifiKisa = "Eski Devlet Hastanesi Kar��s�", TelefonNo = "3288142180"},
new Eczane { Id =919,Enlem=37.067596,Boylam= 36.249096,Adres ="YED�OCAK MAH.DR.DEVLET BAH�EL� BULVARI 3.ETAP DR.DEVLET BAH�EL� BULVARI 3.ETAP", AdresTarifiKisa = "DR.DEVLET BAH�EL� BULVARI 3.ETAP", TelefonNo = "3284020002"},
new Eczane { Id =945,Enlem=37.081341,Boylam= 36.262527,Adres ="MARA�AL FEVZ� �AKMAK MH.7520 SK NO:4/A", AdresTarifiKisa = "�BN-� S�NA Hastanesi Arkas� Acil Giri�i", TelefonNo = "3288141199"},
new Eczane { Id =914,Enlem=37.071232,Boylam= 36.244700,Adres ="AL�BEYL� MAHALLES� DR.DEVLET BAH�EL� BULVARI NO 27/B OSMAN�YE/MERKEZ", AdresTarifiKisa = "�ZEL SEVG� HASTANES� KAR�ISI", TelefonNo = "3288133453"},
new Eczane { Id =925,Enlem=37.076404,Boylam= 36.246201,Adres ="ESENEVLER MH. KAYALAR SK. NO:13/E MERKEZ/ OSMANIYE", AdresTarifiKisa = "�stasyon Cad. PTT Arkas� Kurtulu� Sa�l�k Oca�� Kar��s�", TelefonNo = "3288145516"},
new Eczane { Id =922,Enlem=37.072431,Boylam= 36.245920,Adres ="ALIBEYLI MAH.KARAOGLANOGLU CAD. NO:17 MERKEZ/ OSMANIYE", AdresTarifiKisa = "�zel Park Hastanesi Kar��s�", TelefonNo = "3288147262"},
new Eczane { Id =931,Enlem=37.073967,Boylam= 36.249472,Adres ="ALIBEYLI MAH.PALALI S�LEYMAN CD. ZAFER CAMII YANI NO:23 MERKEZ/OSMANIYE", AdresTarifiKisa = "Palal� S�leyman Cd.No:23 Zafer Camii yan�", TelefonNo = "3288126535"},
new Eczane { Id =910,Enlem=37.061515,Boylam= 36.239532,Adres ="MEHMET AKIF ERSOY MAH.8009 SKK.NO:8 MERKEZ/OSMANIYE", AdresTarifiKisa = "�zel Yeni Hayat Hastanesi Acil ��k���", TelefonNo = "3288254004"},
new Eczane { Id =901,Enlem=37.063068,Boylam= 36.236504,Adres ="ADNAN MENDERES MAH.19537 SK.no:5/A MERKEZ/OSMANIYE", AdresTarifiKisa = "Metin Tamer Sitesi Arkas� AVM Taraf� Sa�l�k Oca�� Kar��s� Saray Pastanesi Yan�ndaki Sokak", TelefonNo = "3288258010"},
new Eczane { Id =939,Enlem=37.074076,Boylam= 36.253939,Adres ="ISTIKLAL MAH. SEHT. MEHMET EROGLU CAD. NO:137 MERKEZ/OSMANIYE", AdresTarifiKisa = "�ehit Mehmet Ero�lu Cad. HALK BANKASI Kar��s�", TelefonNo = "3288146599"},
new Eczane { Id =924,Enlem=37.0723410,Boylam=36.2461990,Adres ="AL�BEYL� MAH.KARAO�LANO�LU SOK. 14/A", AdresTarifiKisa = "�zel Park Hastanesi Yan�", TelefonNo = "3288127070"},
new Eczane { Id =898,Enlem=37.039324,Boylam= 36.227989,Adres ="FAKU�A�I MAH.45018 SK.NO:8/34 OSMAN�YE", AdresTarifiKisa = "", TelefonNo = "3288020099"},
new Eczane { Id =928,Enlem=37.074496,Boylam= 36.247938,Adres ="ALIBEYLI MAH. S. MEHMET TATLI SK. CEREN IS HANI ZEMIN KAT NO:10 MERKEZ/OSMANIYE", AdresTarifiKisa = "Ziraat Bankas� Kar�� Soka��", TelefonNo = "3288133666"},
new Eczane { Id =933,Enlem=37.073140,Boylam= 36.250133,Adres ="YEDIOCAK MAH. ATAT�RK CD.NO:311 MERKEZ/OSMANIYE", AdresTarifiKisa = "Atat�rk Cd. Kobaner Pasaj� Yan�", TelefonNo = "3288141064"},
new Eczane { Id =948,Enlem=37.083038,Boylam= 36.266513,Adres ="MARESAL FEVZI �AKMAK MAH. HASTANE KARSISI NO:581 MERKEZ/OSMANIYE", AdresTarifiKisa = "Kad�n Do�um Hastanesi Yan�", TelefonNo = "3288143598"},
new Eczane { Id =913,Enlem=37.0710640,Boylam=36.2447360,Adres ="AL�BEYL� MAH.DR.DEVLET BAH�EL� BULVARI 1.ETAP NO:27/A", AdresTarifiKisa = "�ZEL SEVG� HASTANES� KAR�ISI", TelefonNo = "3288123838"},
new Eczane { Id =915,Enlem=37.070599,Boylam= 36.245437,Adres ="DR.DEVLET BAH�EL� BULVARI.DEM�RPEN �N�AAT APT.NO:33 B/B", AdresTarifiKisa = "Sevgi Hastanesi Kar��s� Dr.Devlet Bah�eli Bulvar� 1.Etap", TelefonNo = "3288137070"},
new Eczane { Id =944,Enlem=37.083360,Boylam= 36.258149,Adres ="RIZAIYE MAH.HOCA AHMET YESEVI CAD.NO:79/B OSMANIYE", AdresTarifiKisa = "Nahar Yolu Cumhuriyet Sa�l�k Oca�� Yan�", TelefonNo = "3288132149"},
new Eczane { Id =943,Enlem=37.083374,Boylam= 36.258133,Adres ="Mare�al Fevzi �akmak mah.Hoca Ahmet Yesevi cd.No:44/B", AdresTarifiKisa = "Nahar yolu, cumhuriyet sa�l�k oca�� kar��s�", TelefonNo = "3288127666"},
new Eczane { Id =909,Enlem=37.061855,Boylam= 36.238855,Adres ="M.AKIF ERSOY MH. 8010 SK. �ZYURT SITESI B BLOK NO : 4", AdresTarifiKisa = "Yeni hayat hastanesi orta kap� kar��s�", TelefonNo = "3288251919"},
new Eczane { Id =935,Enlem=37.073485,Boylam= 36.251060,Adres ="ISTIKLAL MAH. ATAT�RK CAD. NO:182 MERKEZ/OSMANIYE", AdresTarifiKisa = "Atat�rk Cad. Eski Belediye ve �AR�I �� Bankas� aras�", TelefonNo = "3288143601"},
new Eczane { Id =930,Enlem=37.074911,Boylam= 36.248520,Adres ="CEVDET SUNAY CAD. NO:33 OSMANIYE", AdresTarifiKisa = "", TelefonNo = "3288143667"},
new Eczane { Id =920,Enlem=37.072707,Boylam= 36.245708,Adres ="AL�BEYL� MAH.KARAO�LANO�LU SK.ORHUN �T�KEN �N�.APT.NO:19/A OSMAN�YE", AdresTarifiKisa = "�zel Park Hastanesi Kar��s�", TelefonNo = "3288143701"},
new Eczane { Id =952,Enlem=37.083542,Boylam= 36.265977,Adres ="KAZIM KARABEK�R MAH.6502 SK.", AdresTarifiKisa = "Kad�n Do�um Hastanesi Yan�", TelefonNo = "3288131513"},
new Eczane { Id =897,Enlem=37.055776,Boylam= 36.189888,Adres ="Akyar K�y� Hastane Mevkii 111/2", AdresTarifiKisa = "B�Y�K OSMAN�YE OTEL� YANI OPET KIDIK PETROL ���", TelefonNo = "5415689788"},
new Eczane { Id =929,Enlem=37.073829,Boylam= 36.247108,Adres ="ALIBEYLI MAH. SEHIT MEHMET SK. NO:4 MERKEZ/OSMANIYE", AdresTarifiKisa = "Ziraat Bankas� Kar�� Soka�� Toprakkale Garaj� Yan�", TelefonNo = "3288149637"},
new Eczane { Id =949,Enlem=37.083618,Boylam= 36.266187,Adres ="KAZIM KARABEK�R MAH.6542 SK.NO:13/A OSMAN�YE", AdresTarifiKisa = "Kad�n Do�um Hastanesi Kar��s�", TelefonNo = "3288147164"},
new Eczane { Id =947,Enlem=37.074039,Boylam= 36.250443,Adres ="ALIBEYLI MAH. CEVDET SUNAY CAD. NO:44/A MERKEZ/OSMANIYE", AdresTarifiKisa = "�AR�I POL�S KARAKOLU ARKASI, AKBANK G�R�� KAPISI KAR�ISI", TelefonNo = "3288124252"},
new Eczane { Id =917,Enlem=37.067728,Boylam= 36.2489378,Adres ="YED�OCAK MAH.DR.DEVLET BAH�EL� BULVARI.NO:87-A/D MERKEZ OSMAN�YE", AdresTarifiKisa = "Devlet bah�eli bulvar� 3.etap", TelefonNo = "5063864196"},
new Eczane { Id =902,Enlem=37.062298,Boylam= 36.233553,Adres ="ADNAN MENDERES MAH.19535 SK.Z�MR�T APT ZEM�N KAT 3/A", AdresTarifiKisa = "Metin Tamer Sitesi Arkas�, AVM otopark ��k��� taraf�, Sanayi Sa�l�k Oca�� kar��s�, Saray Pastanesi yan�ndaki sokakta", TelefonNo = "3288256676"},
new Eczane { Id =904,Enlem=37.063585,Boylam= 36.235971,Adres ="ADNAN MENDERES MAH.19538:SK NO:8/A", AdresTarifiKisa = "Metin tamer Sitesi Arkas� PARK 328 Otopark ��k���", TelefonNo = "3288120331"},
new Eczane { Id =908,Enlem=37.062033,Boylam= 36.238864,Adres ="Yeni Hayat Hastanesi Kar��s� M AK�F ERSOY MAH.ATAT�RK CAD.8008 SK.NO.3", AdresTarifiKisa = "Yeni Hayat Hastanesi Kar��s�", TelefonNo = "3288255252"},
new Eczane { Id =921,Enlem=37.072501,Boylam= 36.245900,Adres ="ALIBEYLI MH.KARAOLANOGLU CD. NO:17/C MERKEZ/OSMANIYE", AdresTarifiKisa = "�zel Park Hastanesi Yan�", TelefonNo = "3288147000"},
new Eczane { Id =916,Enlem=37.071005,Boylam= 36.244900,Adres ="ALIBEYLI MAH. DR. DEVLET BAH�ELI BULV. NO: 29/B MERKEZ/ OSMANIYE", AdresTarifiKisa = "Sevgi Hastanesi Kar��s�, Devlet Bah�eli Bulvar�, Vatan Bilgisayar Yan�", TelefonNo = "3288136059"},
new Eczane { Id =905,Enlem=37.063544,Boylam= 36.235848,Adres ="ADNAN MENDERES MAHALLESI 19538 SOKAK NO:10/B MERKEZ/ OSMANIYE", AdresTarifiKisa = "Metin tamer Sitesi Arkas� PARK 328 Otopark ��k���", TelefonNo = "3288256618"},
new Eczane { Id =906,Enlem=37.062447,Boylam= 36.238758,Adres ="RAUFBEY MH. ATAT�RK CD. SAFAEVLER SITESI B1 BLOK NO:7 MERKEZ/OSMANIYE", AdresTarifiKisa = "Yenihayat hastanesi kar��s�", TelefonNo = "3288251565"},
new Eczane { Id =946,Enlem=37.080450,Boylam= 36.261678,Adres ="MARE�AL FEVZ� �AKMAK MAHALLES� MUSA �AH�N BULVARI 499/B ZEM�N KAT DA�RE 1", AdresTarifiKisa = "�BN-� S�NA Hastanesi Yan�", TelefonNo = "3288120200"},


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
             //       Adres = "��FTL�KK�Y MH. 32133 SK. NO:6/B",
             //       AdresTarifiKisa = "��FTL�KK�Y A�LE SA�LI�I MERKEZ� KAR�ISI A101 YANI",
             //       AdresTarifi ="��FTL�KK�Y A�LE SA�LI�I MERKEZ� KAR�ISI A101 YANI"
             //   },
new Eczane { Id =5,Enlem=36.5395879643729,Boylam=32.041003704071,Adres = "OBA MAH. F�DANLIK CAD. ALANYA E��T�M ARA�TIRMA HAST. YANI NO: 88/D"},
new Eczane { Id =33,Enlem=36.5474827812,Boylam=32.0073777484,Adres = "HACET MAH. 25 M.LIK YOL HANCI PASTANESI KARSISI"},
new Eczane { Id =2,Enlem=36.551405,Boylam=32.012825,Adres = "G�LLER PINARI MAH. 3 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =34,Enlem=36.5442578091,Boylam=32.007648431,Adres = "G�LLER PINARI MH. ESREFKAHVECIOGLU CD. B�Y�K OTEL KARSISI"},
new Eczane { Id =39,Enlem=36.5474486097,Boylam=31.9973150267,Adres = "SEVKET TOKU� CAD. 25 M.LIK YOL GARANTI BATI �APRAZI"},
new Eczane { Id =49,Enlem=36.5579249180445,Boylam=31.9998854398727,Adres = "K���KHAS BAH�E MAH. 610 SOK. NO:3/B �EVRE YOLU SEY�R TERASI KAV�A�I �ST�KBAL ARKASI ALANYA"},
new Eczane { Id =26,Enlem=36.5476141164,Boylam=32.0021473201,Adres = "SEKERHANE MH.�ZEL YASAM HASTANESI KARSISI"},
new Eczane { Id =32,Enlem=36.5471883084014,Boylam=31.996342241764,Adres = "�EVKET TOKU� CAD. 25 METREL�K YOL �ZER�. TURKCELL PLAZA KAR�ISI. 57/A ALANYA"},
new Eczane { Id =8,Enlem=36.5470913424577,Boylam=32.0266216993331,Adres = "C�KC�LL� MEYDAN PAZARI G�R��� K�PA ARKASI"},
new Eczane { Id =12,Enlem=36.533588185858,Boylam=32.0469689369201,Adres = "OBA MAH. OBA BA�KENT HAST. SEMT POL�KL�N��� KAR�ISI G��MENT�RK YANI"},
new Eczane { Id =16,Enlem=36.5402818742377,Boylam=32.0414704084396,Adres = "OBA MAH. F�DANLIK CAD. NO:5/A ALANYA"},
new Eczane { Id =53,Enlem=36.5469069824,Boylam=31.9938537966,Adres = "SARAY MAH. BA�KENT HASTANES� YANI NO:13 ALANYA"},
new Eczane { Id =47,Enlem=36.5552874688,Boylam=31.9901414311,Adres = "SARAY MH. STAD CD. �ZEL ANADOLU HASTANESI KARSISI"},
new Eczane { Id =48,Enlem=36.5536373479571,Boylam=31.9920426607131,Adres = "SARAY MAH.STAD CAD.NO:9/C ALANYAANADOLU CAN HASTANES� DO�U �APRAZI TADIM TANTUN� YANI"},
new Eczane { Id =56,Enlem=36.5471193548539,Boylam=31.9939175248146,Adres = "SARAY MAH. BA�KENT HASTANES� YANI NO:15 ALANYA"},
new Eczane { Id =46,Enlem=36.5468198371682,Boylam=31.9926059246063,Adres = "SARAY MAH. BA�KENT HASTANES� BATISI M�GROS YANI"},
new Eczane { Id =21,Enlem=36.547494512,Boylam=32.0048767373,Adres = "SEKERHANE MH. SEVKET TOKUS CD TULUKLAR YANI 8/A"},
new Eczane { Id =41,Enlem=36.5523076635,Boylam=31.9940133666,Adres = "SARAY MH. SU G�Z� CD. SIFA TIP MERKEZI KARSISI"},
new Eczane { Id =1,Enlem=36.55661928119,Boylam=32.064419388771,Adres = "CAM� ALANI MH. �AR�AMBA CD. OBA KASABASI"},
new Eczane { Id =10,Enlem=36.5475761709598,Boylam=32.0332118868827,Adres = "C�KC�LL� SA�LIK OCA�I KAR�ISI ALANYUM ALI�VER�� MERKEZ� �ST�"},
new Eczane { Id =17,Enlem=36.5333769782763,Boylam=32.0463573932647,Adres = "OBA MAH. OBA BA�KENT HAST. SEMT POL�KL�N��� KAR�ISI G��MENT�RK ALTI"},
new Eczane { Id =57,Enlem=36.5465779107,Boylam=31.994877266,Adres = "SARAY MH. K�LT�R CD. BASKENT HASTANESI ACIL YANI"},
new Eczane { Id =30,Enlem=36.5474586772,Boylam=31.9980432322,Adres = "SEKERHANE MH. SEVKET TOKU� CD 25 M.LIK YOL GARANTI BANKASI KARSISI"},
new Eczane { Id =4,Enlem=36.5477765658524,Boylam=32.0330321788787,Adres = "C�KC�LL� SA�LIK OCA�I YANI ALANYUM ALI�VER�� MERKEZ� �ST�"},
new Eczane { Id =6,Enlem=36.5407085610625,Boylam=32.0418620109558,Adres = "OBA MAH. F�DANLIK CAD. No:72/A YEN� DEVLET HASTANES� KAR�ISI"},
new Eczane { Id =45,Enlem=36.5546800729,Boylam=31.9740871795,Adres = "KIZLARPINARI MAH. 4 NOLU SAGLIK OCAGI YANI MIGROS KARSISI"},
new Eczane { Id =54,Enlem=36.5445512465,Boylam=31.997729226,Adres = "SEKERHANE MH. CUMAPAZARI GIRISI LCWAIKIKI DOGU YANI"},
new Eczane { Id =14,Enlem=36.5467224977,Boylam=32.0157013817,Adres = "G�LLERPINARI MH. HASANAK�ALIOGLU CD. DAYIOGLU OTO YIKAMA KARSISI"},
new Eczane { Id =37,Enlem=36.5460327654,Boylam=32.0002030715,Adres = "SEKERHANE MH.ECZACILAR CD. 1 NOLU SAGLIK OCAGI ALTI"},
new Eczane { Id =19,Enlem=36.5463694810534,Boylam=31.9987133145332,Adres = "OBA MAH.Y�ZBA�IO�LU SOKAK.NO:17/C-D ALANYA/ANTALYA"},
new Eczane { Id =18,Enlem=36.5379156591102,Boylam=32.0446515083312,Adres = "OBA MAH. FABR�KA CAD. NO:11/A D�� HASTANES� A�A�ISI,MAK� KAR�ISI,YE��L �AM KAHVES� YANI"},
new Eczane { Id =23,Enlem=36.5471970693,Boylam=32.0001889845,Adres = "SEKERHANE MAH. 25M.LIK YOL �ZERI BANK ASYA YANI"},
new Eczane { Id =27,Enlem=36.5471990823876,Boylam=32.0061993598937,Adres = "G�LLERPINARI MH. 25 M.LIK YOL �ZERI HACET K�PR�S� S�VARI GIYIM �APRAZI"},
new Eczane { Id =29,Enlem=36.5470891414,Boylam=31.9975965897,Adres = "SEVKET TOKU� CAD. 25 M.LIK YOL GARANTI BANKASI YANI"},
new Eczane { Id =7,Enlem=36.5403508338841,Boylam=32.0412880182266,Adres = "OBA MAH. F�DANLIK CAD. No:5/A YEN� DEVLET HASTANES� KAR�ISI"},
new Eczane { Id =51,Enlem=36.5473984008621,Boylam=31.9943359494209,Adres = "SARAY MH. YAVUZ SULTAN SEL�M CD. BA�KENT �NV. HASTANES� KAR�ISI"},
new Eczane { Id =25,Enlem=36.5473711197,Boylam=32.001871954,Adres = "SEKERHANE MAH. SEVKET TOKUS CAD. �ZEL YASAM HASTANESI YANI"},
new Eczane { Id =9,Enlem=36.5475158369119,Boylam=32.0330590009689,Adres = "C�KC�LL� SA�LIK OCA�I YANI ALANYUM ALI�VER�� MERKEZ� �ST�"},
new Eczane { Id =827,Enlem=36541957,Boylam=32043492,Adres = "OBA MAH.F�DANLIK CADDES�.��KR� KIR APT. NO:64/C ALANYA/ANTALYA"},
new Eczane { Id =891,Enlem=36.5553036249,Boylam=31.9910755187,Adres = "KADIPASA MH. STAD CD. �ZEL ANADOLU HASTANESI YANI 26/C"},
new Eczane { Id =40,Enlem=36.5527087185,Boylam=31.9961353519,Adres = "KADIPASA MAH. IKIZLER SOK. 2 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =15,Enlem=36.5419842975234,Boylam=32.0440506935119,Adres = "METRO ALI�VER�� MERKEZ� ARKASI, YEN� B�LGE HASTANES� YANI, A�IZ VE D�� SA�LI�I ARKASI."},
new Eczane { Id =35,Enlem=36.5468583508,Boylam=32.0014392466,Adres = "SEKERHANE MH.SEVKET TOKU� CD. K�Y DOLMUSLARI GIRISI"},
new Eczane { Id =55,Enlem=36.5428542212,Boylam=31.9970757529,Adres = "HAYATE HANIM CAD. NO:14/D SEKERBANK KARSISI"},
new Eczane { Id =3,Enlem=36.539989,Boylam=32.041135,Adres = "OBA MAH.F�DANLIK CAD. NO:88/A ALANYA E��T�M VE ARA�TIRMA HASTANES� YANI"},
new Eczane { Id =36,Enlem=36.5455264403,Boylam=32.000176411,Adres = "SEKERHANE MH.ECZACILAR CD. CUMA PAZARI"},
new Eczane { Id =31,Enlem=36547552,Boylam=32011564,Adres = "G�LLERPINARI MAH. HASAN AK�ALIO�LU CAD. NO:33/B ALANYA"},
new Eczane { Id =28,Enlem=36.547251,Boylam=32.006742,Adres = "25 M. YOL HACET K�PR�S� HANCI PASTANESI YANI"},
new Eczane { Id =11,Enlem=36.5399540349172,Boylam=32.041452974081,Adres = "OBA MAH. METRO MARKET ARKASI. YEN� ALANYA E��T�M ARA�TIMA HASTANES� AC�L �IKI�I KAR�ISI"},
new Eczane { Id =52,Enlem=36.5471133972,Boylam=31.9938741276,Adres = "SARAY MH. YUNUS EMRE CD. BASKENT HASTANESI YANI"},
new Eczane { Id =42,Enlem=36.5521194831,Boylam=31.9940777626,Adres = "SARAY MH. SUG�Z� CD. SIFA POLIKLINIGI KARSISI"},
new Eczane { Id =22,Enlem=36.5441392099494,Boylam=31.9998130202293,Adres = "CUMA PAZARI G�NEY G�R��� �EKERC�LER MARKET KAR�ISI 3/B (T�CARET ODASI KAR�ISI)"},
new Eczane { Id =38,Enlem=36.5452579766,Boylam=31.9982906073,Adres = "SEKERHANE MAH. TEVFIKIYE CD. DOLMUS DURAGI I�I"},
new Eczane { Id =50,Enlem=36.5474808167,Boylam=31.9942393444,Adres = "SARAY MH. YUNUS EMRE CD. BASKENT HASTANESI KARSISI"},
new Eczane { Id =43,Enlem=36.5529758775211,Boylam=31.9959023594856,Adres = "KADIPA�A MAH. TEL SOK. 2. NOLU SA�LIK OCA�I KAR�ISI"},
new Eczane { Id =44,Enlem=36.5551163655,Boylam=31.9904135367,Adres = "SARAY MAH. STAD CAD. �ZEL ANADOLU HASTANESI KARSISI"},
new Eczane { Id =24,Enlem=36.5477193615,Boylam=32.0021294428,Adres = "SEKERHANE MH. YUNUS G�C�OGLU SOK. �ZEL YASAM HASTANESI KARSISI"},
       


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
             //       Adres = "��FTL�KK�Y MH. 32133 SK. NO:6/B",
             //       AdresTarifiKisa = "��FTL�KK�Y A�LE SA�LI�I MERKEZ� KAR�ISI A101 YANI",
             //       AdresTarifi ="��FTL�KK�Y A�LE SA�LI�I MERKEZ� KAR�ISI A101 YANI"
             //   },
new Eczane { Id =109,Enlem=36.89657,Boylam=30.67808,Adres = "SO�UKSU MAH. TOROSLAR CAD. NO:26/A SO�UKSU A�LE HEK�ML��� YANI MURATPA�A"},
new Eczane { Id =69,Enlem=36.89058,Boylam=30.68008,Adres = "K.KARABEKIR CD.EGITIM ARASTIRMA HASTANESI ACIL KARSISI"},
new Eczane { Id =111,Enlem=36.89376,Boylam=30.68449,Adres = "YILDIZ MAH.YILDIZ CAD. 220. SOK.NO:25/B MURATPA�A YILDIZ MEDSTAR HASTANES� KAR�ISI"},
new Eczane { Id =88,Enlem=36.89366,Boylam=30.68033,Adres = "SOGUKSU CD. 78/B DEFTERDARLIK KARSISI"},
new Eczane { Id =67,Enlem=36.89385,Boylam=30.68445,Adres = "YILDIZ MAHALLESI 220 SOKAK NO : 25 / A MEDSTAR ANTALYA HASTANESI KARSISI"},
new Eczane { Id =65,Enlem=36.89019,Boylam=30.66844,Adres = "MELTEMDEKI YENI ANTALYA STADININ KUZEY KAPISININ KARSISI(MELTEM MAH.ASYA SITESI D3 BLOK ALTI)"},
new Eczane { Id =82,Enlem=36.89322,Boylam=30.68026,Adres = "KAZIM KARABEKIR CD.YILDIZ MH NO:70/2 DEFTERDARLIK KARSISI"},
new Eczane { Id =62,Enlem=36.89283,Boylam=30.66994,Adres = "EGITIM ARASTIRMA HASTANESI ARKASI MELTEM MAH. MELTEM BALIK �ARSISI KARSISI"},
new Eczane { Id =89,Enlem=36.89209,Boylam=30.67408,Adres = "MELTEM MH.MELTEM CD.�ZLEM SITESI I�INDE DEVLET HAST.YAKINI MEGAPOL SINEMASI KUZEYINDE"},
new Eczane { Id =112,Enlem=36.89231,Boylam=30.68188,Adres = "YILDIZ MAH.YILDIZ CAD.NO:59/A-B MURATPASA ANTALYA EGITIM ARASTIRMA HASTANESINDEN TRT CADDESINE DOGRU 100 MT ILERIDE SAGDA"},
new Eczane { Id =73,Enlem=36.88996,Boylam=30.67999,Adres = "EGITIM ARASTIRMA HASTANESI KARSISI K.KARABEKIR CD."},
new Eczane { Id =84,Enlem=36.89931,Boylam=30.68271,Adres = "G�VENLIK MAH. 259. SOK. NO:20/1 SOGUKSU CAMII KARSI SOKAGI 25 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =107,Enlem=36.89456,Boylam=30.68457,Adres = "YILDIZ MH 220 SK.G��L� APT.NO:26 MEDSTAR ANTALYA HASTANESI KARSISI TRT CAD.ILE EGITIM ARASTIRMA HASTANESI ARASI"},
new Eczane { Id =108,Enlem=36.8997,Boylam=30.68043,Adres = "KAZIM KARABEKIR CAD.NO:144 SO�UKSU CAM� KAR�ISI SARIYAR TAKS� DURA�I C�VARI"},
new Eczane { Id =78,Enlem=36.89411,Boylam=30.68441,Adres = "YILDIZ MAH.YILDIZ CAD.220 SOKAK 27/C MEDSTAR ANTALYA HAST. KARSISI"},
new Eczane { Id =99,Enlem=36.90021,Boylam=30.68216,Adres = "G�VENL�K MAH.261 SOK.NO:5/A DEFTERDARLIKTAN �ALLIYA DO�RU GIDERKEN SO�UKSU B�M KAR�ISI"},
new Eczane { Id =92,Enlem=36.89047,Boylam=30.67537,Adres = "EGITIM ARASTIRMA HAST.BATISI MELTEM MH. �ZLEM SIT. A 17 BLOK NO:1 (YEN� STADYUM YOLU �ZER�)"},
new Eczane { Id =93,Enlem=36.89192,Boylam=30.6753,Adres = "MELTEM MAH.TARIKAKILTOPU CAD. �ZLEM S�T.A-10 BLK.NO:7-B/A E��T�M ARA�TIRMA HAST. KAR�. MEGAPOL S�NEMASI YANI"},
new Eczane { Id =103,Enlem=36.89928,Boylam=30.68248,Adres = "G�VENLIK MAH. 259. SOK. NO:18 SOGUKSU CAMII KARSI SOKAGI 25 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =60,Enlem=36.88948,Boylam=30.67995,Adres = "VARLIK MH. KAZIM KARABEKIR. CD. NO: 26/1 EGITIM ARASTIRMA HASTANESI KARS."},
new Eczane { Id =66,Enlem=36.89431,Boylam=30.68436,Adres = "MEDSTAR ANTALYA (ESKI YILDIZ ANDEVA) HASTANESI KARSISI YILDIZ MAH.220 SK."},
new Eczane { Id =70,Enlem=36.8997,Boylam=30.68043,Adres = "K.KARABEKIR CD. SARIYAR D�G�N SALONU YANI (SOGUKSU)"},
new Eczane { Id =101,Enlem=36.88929,Boylam=30.67994,Adres = "KAZIM KARABEKIR CAD.ANTALYA EGITIM ARASTIRMA HASTANESI ACIL KARSISI"},
new Eczane { Id =79,Enlem=36.89686,Boylam=30.67264,Adres = "EGITIM ARASTIRMA HAST.KUZEYI BAYINDIR MAH.ANTALYA KOLEJI JANDARMA YOLU �ZERI"},
new Eczane { Id =68,Enlem=36.89041,Boylam=30.68004,Adres = "K.KARABEKIR CD.EGITIM ARASTIRMA HASTANESI KARSISI"},
new Eczane { Id =102,Enlem=36.89303,Boylam=30.68025,Adres = "KAZIM KARABEKIR CAD. NO:68 2 NOLU SAGLIK OCAGI KARSISI DEFTERDARLIK KARSISI"},
new Eczane { Id =86,Enlem=36.89273,Boylam=30.66856,Adres = "MELTEM MAH. MELTEM BULV. MELTEM CAM�� YANI ANTALYASPOR S�TES� ALTI MURATPA�A"},
new Eczane { Id =90,Enlem=36.89032,Boylam=30.68001,Adres = "EGITIM ARASTIRMA HASTANESI ACIL �IKISI KARS. ESKI DEVLET HASTANESI"},
new Eczane { Id =72,Enlem=36.89199,Boylam=30.68018,Adres = "EGITIM ARASTIRMA HASTANESI KARSISI DEFTERDARLIK KAVSAGI"},
new Eczane { Id =483,Enlem=36.89436,Boylam=30.68438,Adres = "YILDIZ MAH.220 SOK. NO:27/B MURATPA�A YILDIZ MEDSTAR HASTANES� KAR�ISI"},
new Eczane { Id =106,Enlem=36.90021,Boylam=30.68248,Adres = "G�VENLIK MAH.261 SK.NO:13 SARIYAR TAKS� SOK.�SMET Y�CE CAM�� KAR�ISI"},
new Eczane { Id =104,Enlem=36.89284,Boylam=30.68026,Adres = "EGITIM ARASTIRMA HASTANESI VE DEFTERDARLIK KARSISI"},
new Eczane { Id =98,Enlem=36.89086,Boylam=30.67572,Adres = "MELTEM MH.�ZLEM ST.EGITIM ARASTIRMA HAST.BATI KAPISI KARSISI MEGAPOL SINEMASI YANI"},
new Eczane { Id =95,Enlem=36.89476,Boylam=30.68429,Adres = "YILDIZ MAH. HAMIDIYE CAD.SALIH ERCIYES APT 21/A YILDIZ ANDEVA HAST (MEDSTAR ANTALYA HAST) ACIL KARSISI"},
new Eczane { Id =74,Enlem=36.89722,Boylam=30.66878,Adres = "MELTEM MH.VATAN SITESI JANDARMA KARSISI YENI ADLIYE ARKASI ANTALYA KOLEJI KARSISI"},
new Eczane { Id =91,Enlem=36.89176,Boylam=30.68021,Adres = "K.KARABEKIR CD. YILDIZ MH. NO:54 EGITIM ARASTIRMA HASTANESI KARS."},
new Eczane { Id =87,Enlem=36.88953,Boylam=30.67995,Adres = "VARLIK MAH. KAZIM KARABEKIR CAD. EGITIM ARASTIRMA HAST. YEN� ACIL KAPISI KAR�IS"},
new Eczane { Id =63,Enlem=36.89005,Boylam=30.68001,Adres = "EGITIM ARASTIRMA HASTANESI KARSISI K.KARABEKIR CD."},
new Eczane { Id =61,Enlem=36.88968,Boylam=30.67996,Adres = "MURATPA�A �L�ES� VARLIK MAH.KAZIMKARABEK�R CAD. NO:28/A ANTALYA E��T�M ARA�TIRMA AC�L KAR�ISI"},
new Eczane { Id =75,Enlem=36.8968,Boylam=30.67294,Adres = "BAYINDIR MH.TOROSLAR CAD.JANDARMA VE DOGA KOLEJI YOLU �ZERI"},
new Eczane { Id =85,Enlem=36.89246,Boylam=30.68026,Adres = "YILDIZ MAH. KAZIM KARABEKIR CAD. NO:60/3 (DEFTERDARLIK KARSISI)"},
new Eczane { Id =100,Enlem=36.89286,Boylam=30.66674,Adres = "MELTEM MH. 3. CD. MELTEM CAMII (50 M) ILERISI NO:3 (ANKARA SITESI)"},
new Eczane { Id =58,Enlem=36.89643,Boylam=30.67835,Adres = "SOGUKSU MAH.TOROSLAR CAD.HEDEF ECZA DEPOSU YOLU �ZERI PASABAH�EYE 100 METRE MESAFEDE"},
new Eczane { Id =94,Enlem=36.89977,Boylam=30.6807,Adres = "G�VENLIK MAH.KAZIM KARABEKIR CAD.NO:150/A SARIYAR D�G�N SALONU YANI"},
new Eczane { Id =76,Enlem=36.89711,Boylam=30.6732,Adres = "BAYINDIR MAHALLESI PINAR CADDESI .�AKMAK APT.MANAVOGLU PARK KARSISI"},
new Eczane { Id =64,Enlem=36.88941,Boylam=30.67994,Adres = "VARLIK MAH. KAZIM KARABEK�R CAD.( ANTALYA E��T�M ARA�TIRMA HASTANES� AC�L KAR�ISI ) NO:24 MURATPA�A"},
new Eczane { Id =71,Enlem=36.8932,Boylam=30.67513,Adres = "MELTEM MH.EGITIM ARASTIRMA HAST. MELTEM KAVSAGI ELEKTRIK TRAFOSU YANI"},
new Eczane { Id =97,Enlem=36.89345,Boylam=30.6803,Adres = "YILDIZ MAH. KAZIM KARABEK�R CAD. 74/A DEFTERDARLIK KAR�ISI VAKIFBANK B�T�����"},
new Eczane { Id =806,Enlem=36.89405,Boylam=30.68431,Adres = "YILDIZ MAHALLES�.�AKIRLAR CADDES�.NO:26/A YILDIZ MEDSTAR HASTANES� AC�L KAR�ISI MURATPA�A/ANTALYA"},
new Eczane { Id =96,Enlem=36.89051,Boylam=30.68005,Adres = "KAZIM KARABEKIR CAD. EGITIM ARASTIRMA HASTANESI KARSISI"},
new Eczane { Id =83,Enlem=36.89407,Boylam=30.67356,Adres = "MELTEM MH.BELGEN SITESI 3.BLOK NO:1"},
new Eczane { Id =59,Enlem=36.90093,Boylam=30.68049,Adres = "SO�UKSU MAH. KAZIM KARABEK�R CAD. NO:95/B KIRAL APT. MURATPA�A"},
new Eczane { Id =77,Enlem=36.8921,Boylam=30.68049,Adres = "EGITIM ARASTIRMA-DEFTERDARLIK KVS.DAN TRT YE GIDERKEN 10 METRE ILERDE SAGDA"},
new Eczane { Id =81,Enlem=36.89362,Boylam=30.68471,Adres = "TRT CADDESI ILE DEVLET HASTANESI ARASI MEDSTAR ANTALYA HASTANESI YANI"},
new Eczane { Id =105,Enlem=36.89271,Boylam=30.68225,Adres = "YILDIZ MH.YILDIZ CAD. FATIH I.�.OKULU K�SESI (EGITIM ARS. HAST.-TRT ISTIKAMETI)"},
new Eczane { Id =110,Enlem=36.88953,Boylam=30.67995,Adres = "VARLIK MAH.KAZIM KARABEK�R CAD. NO:22/A ANTALYA E��T�M ARA�TIRMA HASTANES� AC�L KAR�ISI MURATPA�A"},
new Eczane { Id =118,Enlem=36.89652,Boylam=30.6973,Adres = "��GEN MH. TONGU� CD. SAL�H ENER AP. NO:56/C"},
new Eczane { Id =124,Enlem=36.89216,Boylam=30.69165,Adres = "100.YIL ILGIM (TIP)MERK.ARKASI.TRT KARS.ARA SOKAGI 15 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =177,Enlem=36.88588,Boylam=30.69562,Adres = "DEN�Z MAHALLES� 122 SOKAK 4/A SELEKLERDEN KONYAALTI CADDES�NE DO�RU SA�DAN 2. SOKAK"},
new Eczane { Id =128,Enlem=36.89383,Boylam=30.70034,Adres = "SARAMPOL CADDESI ��GEN MAH. 90. SOKAK NO:4/B �ORBACI AL� BABANIN ARKA SOKA�I"},
new Eczane { Id =151,Enlem=36.89947,Boylam=30.68952,Adres = "G�LL�K CD.ATAT�RK DEVLET HAST.(ESKI SSK) OTOPARK �IKISI KARSISI ACIL KARSISI"},
new Eczane { Id =153,Enlem=36.89756,Boylam=30.69079,Adres = "G�LL�K CAD.AKTAS APT.ATAT�RK DEVLET HASTANESI KARSISI"},
new Eczane { Id =113,Enlem=36.89742,Boylam=30.69085,Adres = "G�LL�K CAD.NO:129/2 ATAT�RK DEVLET HASTANESI ACIL KARS."},
new Eczane { Id =144,Enlem=36.89697,Boylam=30.69344,Adres = "SSK ARKASI MERKEZ ORTAOKULU YANI 5 NOLU SAGLIK OCAGI KARS."},
new Eczane { Id =148,Enlem=36.89066,Boylam=30.69391,Adres = "ALTINDAG MH.100 YIL BULV.YAVUZ APT. G�LL�K ILE 100 YIL KAVS.YAPI KREDI BANKASI BATISI (100.YIL AYDIN KANZA PARKI KAR�ISI)"},
new Eczane { Id =142,Enlem=36.89748,Boylam=30.69082,Adres = "G�LL�K CD.ATAT�RK DEVLET HASTANESI ( ESKI SSK HASTANESI) ACIL KARSISI"},
new Eczane { Id =115,Enlem=36.8984,Boylam=30.69031,Adres = "MEMUREVLERI MAH.G�LL�K CAD. NO:139/A ATAT�RK DEVLET HAST.KARS."},
new Eczane { Id =147,Enlem=36.88564,Boylam=30.69647,Adres = "KONYAALTI CD.ATESEN AP.8/C YAVUZ �ZCAN PARKI KARSISI G�LL�K PTT ARKASI"},
new Eczane { Id =121,Enlem=36.89365,Boylam=30.70172,Adres = "SARAMPOL CAD.MARKANTALYA AVM KAR�ISINDAK� B�LGE HAN OTEL� YANI"},
new Eczane { Id =294,Enlem=36.89633,Boylam=30.6997,Adres = "MURATPA�A MAH. 563 SOK.NO:46/9 MURATPA�A"},
new Eczane { Id =133,Enlem=36.89908,Boylam=30.68983,Adres = "G�LL�K CAD.ATAT�RK DEVLET HASTAHANESI KARSISI (ESKI SSK ANTALYA)"},
new Eczane { Id =140,Enlem=36.89721,Boylam=30.69131,Adres = "G�LL�K CD.ATAT�RK DEVLET HAST.(ESKI SSK)"},
new Eczane { Id =132,Enlem=36.89773,Boylam=30.69065,Adres = "ATAT�RK DEVLET HAST.KARSISI G�LL�K CADDESI NO:133/A SSK ACIL KARSISI"},
new Eczane { Id =125,Enlem=36.88963,Boylam=30.68624,Adres = "100. YIL BULVARI 54/C VARLIK MAH."},
new Eczane { Id =130,Enlem=36.90406,Boylam=30.68421,Adres = "SEDIR MH.VATAN BULV.�ALLI �ST GE�IT YANI NO:34"},
new Eczane { Id =129,Enlem=36.8889,Boylam=30.68583,Adres = "Y�Z�NC�YIL CAD. RADON TIP MERKEZ� YANI VARLIK MAH. 172. SOK. NO:15/A-B MURATPA�A"},
new Eczane { Id =119,Enlem=36.89237,Boylam=30.69385,Adres = "G�LL�K CAD. G�LL�K PETROL OFISI KARSISI MURATPASA IL�E EMNIYET YANI"},
new Eczane { Id =135,Enlem=36.89445,Boylam=30.69257,Adres = "TRT (TONGU�) CD. TRT - G�LL�K KAVSAGI KENT PASTANES� KAR�ISI"},
new Eczane { Id =146,Enlem=36.90021,Boylam=30.68915,Adres = "ATAT�RK DEVLET HASTANESI (ESKI SSK) CIVARI NO:155 ORMAN B�LGE M�D.KARSISI"},
new Eczane { Id =152,Enlem=36.89061,Boylam=30.69359,Adres = "100. YIL BULVARI AYDIN KANZA PARKI KARS.(100.YIL-G�LL�K KAVS.) NO : 9/B"},
new Eczane { Id =114,Enlem=36.8991,Boylam=30.69438,Adres = "ABDI IPEK�I CAD. 72/D SARAMPOL �ARSAMBA PAZARI GIRISI ANTALYA RAF YANI"},
new Eczane { Id =122,Enlem=36.89761,Boylam=30.69637,Adres = "ABD� �PEK�� CAD. PTT �ARAMPOL �UBES� KAR�ISI - MURATPA�A"},
new Eczane { Id =219,Enlem=36.89791,Boylam=30.69047,Adres = "MEMUREVLER� MAH.G�LL�K CAD. AKTA� APT.NO.131/B ATAT�RK DEVLET HASTANES� AC�L KAR�ISI"},
new Eczane { Id =149,Enlem=36.89432,Boylam=30.692,Adres = "TRT (TONGU�) CD. SARAY APT. ALTI NO: 28/B TRT KARSISI"},
new Eczane { Id =141,Enlem=36.89844,Boylam=30.69027,Adres = "ATAT�RK DEVLET HASTANESI KARSISI (ESKI SSK HASTANESI) G�LL�K CADDESI"},
new Eczane { Id =139,Enlem=36.8974,Boylam=30.69137,Adres = "G�LL�K CADDESI ATAT�RK DEVLET HASTANESI POLIKLINIK KARSISI NO:2/B (ESKI SSK) KARSISI"},
new Eczane { Id =136,Enlem=36.8982,Boylam=30.6904,Adres = "G�LL�K CAD. NO:137/C VATAN APT.ATAT�RK DEVLET HASTANESI (ESKI SSK) KARS."},
new Eczane { Id =123,Enlem=36.89826,Boylam=30.69034,Adres = "ATAT�RK DEVLET HAST.(ESK� SSK HAST.)KARSISI G�LL�K CADDESI NO:137/D OTOPARK GIRIS KARSISI"},
new Eczane { Id =131,Enlem=36.9039,Boylam=30.68704,Adres = "SED�R MAH. 725.SOK. NO:15/A-B MURATPA�A �ALLI EMN�YET M�D�RL��� KAR�ISINDAK� ARA SOKAK HAL�DE ED�P ADIVAR ANA OKULU KAR�ISI"},
new Eczane { Id =126,Enlem=36.89665,Boylam=30.69362,Adres = "SSK HASTANESI ACIL YANI K�T�PHANE SOKAGI 5 NOLU SAGLIK OCAGI KARS."},
new Eczane { Id =120,Enlem=36.88767,Boylam=30.69854,Adres = "G�LL�K PTT KARS. KATLI OTOPARK ARKASI VALILIK YOLU �ZERI SEHIT CENGIZ TOYTUN� CAD."},
new Eczane { Id =138,Enlem=36.89537,Boylam=30.69942,Adres = "SARAMPOL CD. (ABDI IPEK�I CAD.) �ORBACI ALI BABANIN YANI"},
new Eczane { Id =150,Enlem=36.89878,Boylam=30.69002,Adres = "ANAFARTALAR CAD. ATAT�RK DEVLET HAST. KARSISI (SSK OTOPARK KARSISI) NO:141/B"},
new Eczane { Id =145,Enlem=36.8922,Boylam=30.69182,Adres = "ALTINDAG MH.TRT CAD.151 SK.15 NOLU SAGLIK OCAGI KARS.TRT BINASI KARS.ARA SOKAGI"},
new Eczane { Id =137,Enlem=36.90306,Boylam=30.68535,Adres = "SED�R MAH.VATAN CADDES�. NO:8/B MURATPA�A. �ALLI EMN�YET M�D�RL��� KAR�ISI."},
new Eczane { Id =127,Enlem=36.899,Boylam=30.6899,Adres = "ATAT�RK DEV. HAST. KARSISI (ESKI SSK) G�VENLIK MAH.G�LL�K CAD. 277 SK.NO:143/A"},
new Eczane { Id =117,Enlem=36.89774,Boylam=30.69065,Adres = "G�LL�K CAD.ANTALYA ATAT�RK DEVLET HASTANES�(ESK� SSK) AC�L KAR�ISI MEMUREVLER� MAH."},
new Eczane { Id =143,Enlem=36.89303,Boylam=30.68845,Adres = "ALTINDAG MAH.TURGUT REIS CAD.ESK� G�NEYL�LER RESTAURANT �LE SAMANYOLU PASTANES� ARASI MERVE APT. NO:60 MURATPASA"},
new Eczane { Id =498,Enlem=36.89895,Boylam=30.72447,Adres = "GEB�ZL� MAH. 1115 SOK. KARALAR APT. NO:10/E ASV YA�AM HASTANES� AC�L KAPISI KAR�ISI KARALAR D���N SALONU ALTI / MURATPA�A"},
new Eczane { Id =164,Enlem=36.90683,Boylam=30.68551,Adres = "SEDIR MAH.GAZI BULVARI �NSAL APT.76/A �ALLI MEYDAN TIP MERKEZI YANI"},
new Eczane { Id =171,Enlem=36.90165,Boylam=30.69896,Adres = "CUMHURIYET MH.ESKI SANAYI ALTI OPERA YASAM HASTANESI YANI"},
new Eczane { Id =181,Enlem=36.90294,Boylam=30.72707,Adres = "YENIG�N MAH.YUNUS EMRE CAD. ANTALYA ANADOLU LISESI GIRISI YANI"},
new Eczane { Id =161,Enlem=36.89884,Boylam=30.72406,Adres = "GEB�ZL� MAH.1115 SOKAK NO:6 / A KARALAR D���N SARAYI YANI - ASV YA�AM HAST. KAR�ISI"},
new Eczane { Id =229,Enlem=36.89543,Boylam=30.72521,Adres = "KIZILTOPRAK MAH.AT�LA N�ZAM SEMT POL�KL�N��� KAR�ISI.KARALAR D���N SALONU G�NEY�.YA�AM HAST.AC�L KAR�I SOKA�I."},
new Eczane { Id =158,Enlem=36.90589,Boylam=30.69906,Adres = "ESKI SANAYI MH.FATIH CD.659 SK.9 NOLU SAGLIK OCAGI KARS. (ITFAIYE CIVARI)"},
new Eczane { Id =228,Enlem=36.8992,Boylam=30.70894,Adres = "ET�LER MAH.882 SK.NO:19/A MEVLANA LOKANTISI ARKASI . �ZEL �OCUK TIP MERKEZ� KAR�ISI MURATPA�A"},
new Eczane { Id =494,Enlem=36.90774,Boylam=30.718,Adres = "KIZILARIK MAH. 2769 SOK. NO:11/B KIZILARIK TAKS� DURA�I SA�INDAN G�R�NCE 200 MT �LERDE MURATPA�A"},
new Eczane { Id =175,Enlem=36.90326,Boylam=30.72855,Adres = "GEBIZLI MH.ASIK VEYSEL CD.32 NOLU AILE HEKIMLIGI �APRAZ KARS. ZEYTINK�Y KAVSAGI"},
new Eczane { Id =180,Enlem=36.90711,Boylam=30.69973,Adres = "DUTLUBAH�E MAH. FATIH.CAD.NO 38 REAL - ESKISANAYI ARASI LUKOIL YANI"},
new Eczane { Id =159,Enlem=36.90588,Boylam=30.69929,Adres = "ESK� SANAY� MAH. FAT�H CAD. 659 SOK. 9 NOLU SA�LIK OCA�I KAR�ISI �TFA�YE C�VARI"},
new Eczane { Id =172,Enlem=36.90573,Boylam=30.69949,Adres = "ESKI SANAYI MAH.637 SOKAK ITFAIYE VE 9 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =154,Enlem=36.90288,Boylam=30.7124,Adres = "ETILER MAH.845 SOK.NO:49/A-B ET�LER D���N SALONU ARASI ALTAY FIRIN KAR�ISI"},
new Eczane { Id =169,Enlem=36.89875,Boylam=30.7244,Adres = "GEBIZLI MAH.1115 SOK.KARALAR APT.NO:10/C KARALAR D�G�N SARAYI YANI GEB�ZL� MAH.YA�AM HASTANES� AC�L KAR�ISI"},
new Eczane { Id =176,Enlem=36.90129,Boylam=30.72544,Adres = "YENIG�N MAH.YUNUS EMRE CAD.NO:93A/A ANTALYA ANADOLU LISESI YANI (�OCUK ESIRGEME KURUMU KARSISI)"},
new Eczane { Id =167,Enlem=36.89643,Boylam=30.73334,Adres = "DO�UYAKA MAH.1216 SOK.MOON L�GHT APT.DO�UYAKA ASM �APRAZI TERMESOS BULV. M�GROS ARKASI MURATPA�A"},
new Eczane { Id =173,Enlem=36.90333,Boylam=30.72683,Adres = "YENIG�N MAH. K�ROGLU BULVARI 54/B ANTALYA ANADOLU LISESI MEVKI 32 NOLU SA�LIK OCA�I ARKASI"},
new Eczane { Id =163,Enlem=36.90734,Boylam=30.68742,Adres = "GAZI BULVARI SEDIR MAHALLESI 86/C MURATPASA �ALLI MEYDAN TIP MERKEZI YANI"},
new Eczane { Id =162,Enlem=36.90808,Boylam=30.70983,Adres = "KONUKSEVER MAH. 819 .SOK.NO:46/A-B MURATPA�A 3 NOLU VAL� SA�M �OTUR ASM KAR�ISI"},
new Eczane { Id =179,Enlem=36.90418,Boylam=30.72015,Adres = "YEN�G�N MAH. K�RO�LU BULVARI AYTEM�Z PETROL �LE ZEYT�NK�Y ARASI MURATPA�A"},
new Eczane { Id =183,Enlem=36.8992,Boylam=30.70894,Adres = "ETILER MAH.MEVLANA LOKANTASI YANI ETILER TAKSI ARKASI EVLIYA �ELEBI CD.861 SK.NO:7/A MAVI APT."},
new Eczane { Id =174,Enlem=36.89782,Boylam=30.73432,Adres = "DO�UYAKA MAH. 1216 SOK. NO:5/B MURATPA�A.TERMESOS K�PA ARKASI."},
new Eczane { Id =168,Enlem=36.90398,Boylam=30.72652,Adres = "KIZILARIK MAH. K�RO�LU BULV. YAVUZEVLER� APT. NO:65/B ANADOLU L�SES� MEVK�� 32 NO'LU SA�LIK OCA�I YANI MURATPA�A"},
new Eczane { Id =182,Enlem=36.89887,Boylam=30.72531,Adres = "GEB�ZL� MAH. 1115 SOK. 4/C ANTALYA VAKIF YA�AM HASTANES� KAR�ISI ( KARALAR D���N SALONU YAKINI)"},
new Eczane { Id =170,Enlem=36.90797,Boylam=30.7102,Adres = "KONUKSEVER MAHALLESI 821 SOKAK 3 NOLU SAGLIK OCAGI KARSISI KONUKSEVER KAPALI CUMARTES� PAZARI KAR�ISI"},
new Eczane { Id =155,Enlem=36.89798,Boylam=30.7056,Adres = "REAL ALISVERIS MERKEZI DEPO GIRISI YANI"},
new Eczane { Id =160,Enlem=36.90692,Boylam=30.68602,Adres = "SEDIR MH.GAZI BULV.�NSAL APARTMANI NO:122/4 �ALLI MEYDAN TIP MERKEZI YANI"},
new Eczane { Id =157,Enlem=36.89908,Boylam=30.72375,Adres = "GEB�ZL� MAH. 1115 SOK. VAKIF YA�AM HASTANES� KAR�ISI MURATPA�A"},
new Eczane { Id =166,Enlem=36.90125,Boylam=30.69879,Adres = "CUMHURIYET MAH.640 SK.OPERA YASAM HASTANESI YANI"},
new Eczane { Id =156,Enlem=36.90301,Boylam=30.72683,Adres = "YENIG�N MAH.YUNUS EMRE CAD.32 NOLU ASM ARKASI ANTALYA ANADOLU LISESI YANI"},
new Eczane { Id =198,Enlem=36.88614,Boylam=30.70853,Adres = "ATAT�RK CAD. NO:15/A D�NERCILER �ARSISI ILE ��KAPILAR ARASI"},
new Eczane { Id =493,Enlem=36.89257,Boylam=30.71571,Adres = "MEVLANA CADDESI BP KARSI �APRAZI SALI PAZARI GIRISI 30 NOLU SAGLIK OCAGI �N�"},
new Eczane { Id =186,Enlem=36.88685,Boylam=30.71424,Adres = "�AYBASI MH.1343 SK.NO:23/ A 1 NOLU AILE SAGLIGI MRK. 50 METRE YANI DOGU GARAJI"},
new Eczane { Id =212,Enlem=36.8891,Boylam=30.72189,Adres = "ALI �ETINKAYA CAD. MEYDAN TIP MERKEZI YANI"},
new Eczane { Id =209,Enlem=36.88778,Boylam=30.72024,Adres = "�AYBASI MH.B.ONAT CD.MEYDAN KAVSAGINDAN B.ONATA D�N�STE ANADOLU HAST.GELMEDEN 6. NOTER YANI"},
new Eczane { Id =191,Enlem=36.8867,Boylam=30.70198,Adres = "CUMHUR�YET MEYDANI TOPHANE PARKI KAR�ISI"},
new Eczane { Id =207,Enlem=36.88722,Boylam=30.71399,Adres = "�AYBASI MAH. ALI �ETINKAYA CD. YIKILAN HALK PAZARI DOGUSU 1 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =205,Enlem=36.89179,Boylam=30.7034,Adres = "MARKANTALYA AVM YANI MURATPASA CAMII KARSISI SARAMPOL CAD. 82/C"},
new Eczane { Id =190,Enlem=36.88962,Boylam=30.71092,Adres = "Y�KSEKALAN MAH. FAHRETT�N ALTAY CAD. 1. BENL�O�LU ��HANI NO:8/B MURATPA�A DO�U GARAJI START OTEL YANI"},
new Eczane { Id =200,Enlem=36.89227,Boylam=30.71583,Adres = "MEVLANA CADDESI BP �APRAZ KAR�ISI SALI PAZARI 30 NOLU SA�LIK OCA�I �N�"},
new Eczane { Id =199,Enlem=36.8876,Boylam=30.71371,Adres = "DOGU GARAJI 1 NOLU SAGLIK OCAGI ALTI"},
new Eczane { Id =214,Enlem=36.8925,Boylam=30.71574,Adres = "MEVLANA CD. SALI PAZARI GIRISI NO:41 BP PETROL KARSISI"},
new Eczane { Id =203,Enlem=36.89267,Boylam=30.70145,Adres = "SARAMPOLDEN 100. YIL GIRISI MARK ANTALYA AVM BATISI SIS�I RAMAZAN YANI"},
new Eczane { Id =211,Enlem=36.88693,Boylam=30.71848,Adres = "�AYBASI MAH.BURHANETTIN ONAT CAD.NO:20/A ANADOLU HASTANESI G�R�� KAPISI KARSISI"},
new Eczane { Id =208,Enlem=36.88583,Boylam=30.71909,Adres = "�AYBA�I MAH. BURHANETT�N ONAT CAD. KANARYA APT. NO:30/A-B MURATPA�A"},
new Eczane { Id =185,Enlem=36.88901,Boylam=30.72199,Adres = "KIZILTOPRAK MAH.ALI �ETINKAYA CAD.NO:133/C MEYDAN TIP HAST.YANI"},
new Eczane { Id =202,Enlem=36.88943,Boylam=30.70454,Adres = "�ARAMPOL CAD. 40/� (KAPALI YOL) KI�LAHAN �AR�I YAKINI GARANT� BANKASI KAR�ISI"},
new Eczane { Id =201,Enlem=36.89042,Boylam=30.70251,Adres = "ELMALI MAH.HASAN SUBASI CAD. 24/3 MURATPASA CAMII G�NEYI"},
new Eczane { Id =189,Enlem=36.88723,Boylam=30.70773,Adres = "ATAT�RK CD.VAKIF ISHANI D�NERCILER �ARSISI KARSISI NO:48"},
new Eczane { Id =215,Enlem=36.89288,Boylam=30.70362,Adres = "MARKANTALYA AVM I�I ANA GIRIS KAPISI YANI"},
new Eczane { Id =195,Enlem=36.88896,Boylam=30.71246,Adres = "Y�KSEKALAN MAH.AL� �ET�NKAYA CAD. NO:23/A MURATPA�A"},
new Eczane { Id =194,Enlem=36.88696,Boylam=30.7194,Adres = "MEYDAN ANADOLU HASTANESI G�R��� KAR�ISI DR.BURHANETTIN ONAT CAD. NERG�ZHAN APT.NO:20/B-C"},
new Eczane { Id =213,Enlem=36.88773,Boylam=30.70734,Adres = "BALBEY MAH. ESKI TEKEL BINASI YANI D�NERCILER �ARSISI �APRAZI �� BANKASI KAR�ISI"},
new Eczane { Id =196,Enlem=36.88818,Boylam=30.71361,Adres = "�AYBASI MH.1343 SK.NO:7 1 NOLU SAGLIK OCAGI 50 METRE �ST�"},
new Eczane { Id =193,Enlem=36.88692,Boylam=30.7025,Adres = "CUMHURIYET MEYDANI ATAT�RK ANITI KARSISI ESKI VALILIK YANI NO:1"},
new Eczane { Id =204,Enlem=36.88925,Boylam=30.7211,Adres = "KIZILTOPRAK MAH. MEYDAN KAVSAGI (YAPI KREDI BANKASI VE MEYDAN TIP MERKEZ� YANI)"},
new Eczane { Id =210,Enlem=36.8877,Boylam=30.71369,Adres = "�AYBA�I MAH. 1343.SOK. NO:11/A DO�UGARAJI 1 NOLU SA�LIK OCA�I YANI"},
new Eczane { Id =188,Enlem=36.89234,Boylam=30.71581,Adres = "Y�KSEKALAN MAH.MEVLANA CAD.KEMAL TUNCAY APT. NO:37/C MURATPA�A"},
new Eczane { Id =192,Enlem=36.88717,Boylam=30.71403,Adres = "�AYBASI MAH.1343 SOKAK NO:19/3 DOGU GARAJI 1 NOLU SAGLIK OCAGI 10 METRE YANI"},
new Eczane { Id =197,Enlem=36.88691,Boylam=30.71858,Adres = "�AYBASI MAH. 1352/1 SOK. NO 6/A MEYDAN ANADOLU HASTANESI ACIL KAPISI YANI"},
new Eczane { Id =187,Enlem=36.88316,Boylam=30.71317,Adres = "BURHANETT�N ONAT CADDES� BATISINDAK� CEBESOY CADDES�NDE A 101 TAKS� DURA�I ARASI KUMRUCU �SO YANI"},
new Eczane { Id =248,Enlem=36.86714,Boylam=30.73592,Adres = "SIRINYALI MAH. 1515 SOKAK KIRCAMI C�V�L MA�AZASI ARKASI SIRINYALI AILE SAGLIGI MERKEZI KARSISI"},
new Eczane { Id =237,Enlem=36.88059,Boylam=30.71763,Adres = "BURHANETTIN ONAT CADDESI �EL�KLER OTO GALER� YANI NO:72/B MURATPA�A"},
new Eczane { Id =808,Enlem=36.8634,Boylam=30.73199,Adres = "��R�NYALI MAH. �SMET G�K�EN CADDES�..1487 SOKAK. NO:3/B-C MURATPA�A YA�AM HASTANES� KAR�ISI"},
new Eczane { Id =257,Enlem=36.86316,Boylam=30.73142,Adres = "SIRINYALI MH.1487 SK.NO:8/B YASAM HASTANESI YANI"},
new Eczane { Id =254,Enlem=36.87171,Boylam=30.73226,Adres = "KIRCAM� A�IZ VE D�� SA�LI�I MERKEZ�NDEN LAURA �ST�KAMET�NE G�DERKEN �LK YAYA TRAF�K I�I YANINDA"},
new Eczane { Id =218,Enlem=36.87715,Boylam=30.71118,Adres = "ISIKLAR CADDESI STADYUMU GE�INCE SOLDA TRAMVAY YOLU �ZERINDE CENDER OTELE GELMEDEN"},
new Eczane { Id =245,Enlem=36.88731,Boylam=30.72457,Adres = "�AYBASI MH.1359 SOKAK NO:5 MEYDAN KIZILTOPRAK PTT ILERISI ANTALYA TIP MERKEZI ACIL YANI ANTALYA TIP MERKEZI ACIL YANI"},
new Eczane { Id =240,Enlem=36.88472,Boylam=30.73991,Adres = "TARIM MAH.PERGE BULV.ER�ST APT.25/B - TOP�ULAR MEDSTAR YANI"},
new Eczane { Id =250,Enlem=36.8674,Boylam=30.73608,Adres = "SIRINYALI MH.1515 SK. KIRCAMI BIM VE CARREFOUR ARKASINDAKI SOKAK SIRINYALI ASM KARSISI"},
new Eczane { Id =239,Enlem=36.86896,Boylam=30.72688,Adres = "YESILBAH�E MAH.�INARLI CAD.KIRMIZIGL �I�EK�ILIK DEDEMAN MEVKI ILERISI FIZIKALYA TIP MERKEZI YANI"},
new Eczane { Id =226,Enlem=36.87123,Boylam=30.73117,Adres = "YESILBAH�E MH.1482 SK.D�RIYE KAPSIR APT.KIRCAMII AKDENIZ DONDURMA SOKAGI"},
new Eczane { Id =244,Enlem=36.8785,Boylam=30.71687,Adres = "ZERDAL�L�K MAH. 1387 SOK. G�LPER� ZEYBEK APT. NO:17/B BURHANETT�N ONAT(ESK� KOMA� YEN� M�GROS YANI) SOKA�I B.ONAT ASM KAR�ISI"},
new Eczane { Id =231,Enlem=36.86335,Boylam=30.72998,Adres = "ISMET G�KSEN CADDESI YASAM HASTANESI KARSISI DEGIRMENCI BABA YANI"},
new Eczane { Id =227,Enlem=36.87674,Boylam=30.73489,Adres = "PERGE CAD.MEYDAN KAVA�I MAH.62/A KITIR EKMEK FIRINI YANI"},
new Eczane { Id =238,Enlem=36.88787,Boylam=30.7292,Adres = "KIZILTOPRAK MAH. 920 SOK. 12/B YUNUS EMRE ASM KAR�ISI(L�KYA HAST. VE M.N.�AKALLIKLI ANADOLU L�SES�N�N BATISI)"},
new Eczane { Id =221,Enlem=36.88608,Boylam=30.73968,Adres = "TARIM MAH.PERGE BULV.NO:13/H MURATPA�A MEDSTAR TOP�ULAR HASTANES� YANI"},
new Eczane { Id =251,Enlem=36.88524,Boylam=30.73982,Adres = "TARIM MAH. PERGE CAD NO:21/B MEDSTAR TOP�ULAR HASTANESI YANI"},
new Eczane { Id =216,Enlem=36.86228,Boylam=30.73126,Adres = "SIRINYALI MH.1488 SK. 8 NOLU SAGLIK OCAGI YANI YASAM HASTANESI SIRTI"},
new Eczane { Id =252,Enlem=6.88778,Boylam=30.73312,Adres = "KIZILTOPRAK MAH. 921 SOK. NO:36 �ZEL L�KYA HASTANES� G�R��� KAR�ISI FEST�VAL �AR�ISI KUZEY�"},
new Eczane { Id =241,Enlem=36.87101,Boylam=30.72618,Adres = "YE��LBAH�E MAH.1450 SOK.NO:35/A MURATPA�A DEDEMAN DONK��OT SOKA�I �AR�AMBA PAZARI ARKASI 27 NOLU ASM KAR�ISI"},
new Eczane { Id =259,Enlem=36.88779,Boylam=30.72354,Adres = "ALI �ETINKAYA CD.MEYDAN KIZILTOPRAK PTT KARSISI ANTALYA TIP MERKEZI YANI"},
new Eczane { Id =235,Enlem=36.88509,Boylam=30.7248,Adres = "MEYDAN KAVA�I MAH. DE��RMEN�N� CAD. YILDIZ APT. NO:122/A MURATPA�A GEB�ZL� CAM� VE GEB�ZL� L�SES� 50 MT A�A�ISI"},
new Eczane { Id =256,Enlem=36.88797,Boylam=30.72394,Adres = "KIZILTOPRAK MAH. AL� �ET�NKAYA CAD. NO:145/A MURATPA�A �ZEL ANTALYA TIP MERKEZ� KAR�ISI.MEYDAN POSTANES� H�ZASI ."},
new Eczane { Id =253,Enlem=36.87784,Boylam=30.71071,Adres = "ISIKLAR CD. T�RKAY APT. NO:27/A ESK� STADYUMDAN CENDER OTEL�NE DO�RU �NERKEN 200 MT �LERDE SOLDA TRAMVAY HATTI �ZERINDE"},
new Eczane { Id =246,Enlem=36.8738,Boylam=30.72245,Adres = "YESILBAH�E MAH.PORTAKAL �I�EGI BULVARI NO:21 OLIMPIK Y�ZME HAVUZU KARSISI"},
new Eczane { Id =232,Enlem=36.88094,Boylam=30.72712,Adres = "ISMAIL CEM CAD.(12.CAD) MEYDAN KAVAGI MAH.CARREFOURSA YANI EKSIOGLU BAYRAKTAR SITESI ASAGISI"},
new Eczane { Id =224,Enlem=36.87442,Boylam=30.73234,Adres = "KIRCAMI MH. PERGE BLV. KADIN DOGUM VE �OCUK HAST.YANI TOTAL BENZIN ISTASYONU YANI"},
new Eczane { Id =247,Enlem=36.86319,Boylam=30.73157,Adres = "��R�NYALI MAH.1487 SOK.NO:10/A LARA YA�AM HASTANES� YANI MURATPA�A"},
new Eczane { Id =230,Enlem=36.87112,Boylam=30.71913,Adres = "YESILBAH�E MAH.METIN KASAPOGLU CAD.NO:24/B SAMP� KAV�A�INDAN LARA Y�N�NE 200 MT SONRA �ZS�T YANI"},
new Eczane { Id =217,Enlem=36.86792,Boylam=30.73655,Adres = "SIRINYALI MAH. SINANOGLU CD. (KIRCAMII CARREFOUR-SA MARKET YANI)"},
new Eczane { Id =233,Enlem=36.87808,Boylam=30.71283,Adres = "19 MAYIS CD. ZERDAL�L�K CAM� KAR�ISI YENIKAPI POL�S MERKEZ� YOLU �ZER� NO: 30/A"},
new Eczane { Id =242,Enlem=36.88104,Boylam=30.73112,Adres = "MEYDANKAVAGI MAH. ISMAIL CEM CAD.( 12. CADDE ) NO :36/B EKSIOGLU BAYRAKTAR SITESI KARSISI"},
new Eczane { Id =225,Enlem=36.86214,Boylam=30.73052,Adres = "SIRINYALI MH. ISMET G�KSEN CD.DEDEMAN MC DONALS �APRAZ KAR�ISI YASAM HASTANESI CIVARI"},
new Eczane { Id =234,Enlem=36.89025,Boylam=30.73822,Adres = "MEHMET��K MAH. TERMESSOS BULV. CANPARK S�T. A BLOK NO:12/A-A OYAK S�TES� KAR�ISI"},
new Eczane { Id =243,Enlem=36.88066,Boylam=30.7357,Adres = "MEYDAN KAVAGI MH.1610 SK.TURUNCU SIT.12.CD.ISMAIL CEM CD. G�LGEN MARKET KARSISI"},
new Eczane { Id =258,Enlem=36.86305,Boylam=30.73026,Adres = "SIRINYALI MAH. �SMET G�K�ENCAD.1487 SK.305/2 YASAM HASTANESI YANI"},
new Eczane { Id =255,Enlem=36.86947,Boylam=30.73533,Adres = "KIRCAMI MAH. SINANOGLU CAD. 7/B E BEBEK �LE �ZLEM PASTANES� ARASI, D�LARA YA�AR KAR�ISI"},
new Eczane { Id =249,Enlem=36.87646,Boylam=30.71787,Adres = "B. ONAT CD. BERNA DEDEMAN SAG. MRK. SONRA A101 YANI"},
new Eczane { Id =236,Enlem=36.87028,Boylam=30.72017,Adres = "YE��LBAH�E MAH. MET�N KASAPO�LU CAD. NO:27/C SULTANYAR KAR�ISI"},
new Eczane { Id =222,Enlem=36.86256,Boylam=30.73035,Adres = "SIRINYALI MAH. ISMET G�KSEN CD. 15/B YASAM HASTANESI CIVARI MC DONALDS KARSISI"},
new Eczane { Id =292,Enlem=36.85609,Boylam=30.74723,Adres = "FENER MAH. TEKEL�O�LU CAD. SEDA APT. NO:11/A MURATPA�A MED�KALPARK HASTANES� YANI BOZKAN PETROL KAR�ISI"},
new Eczane { Id =309,Enlem=36.85773,Boylam=30.79332,Adres = "G�ZELOBA MAH.HAVAALANI CAD.NO:27 / A G�ZELOBA PAZARI KAVSAGI A101 MARKET YANI"},
new Eczane { Id =263,Enlem=36.8581,Boylam=30.79282,Adres = "G�ZELOBA MH.HAVAALANI CAD. �ZEL LARA ANADOLU HASTANES� KARSISI NO:33/A"},
new Eczane { Id =220,Enlem=36.85708,Boylam=30.79016,Adres = "G�ZELOBA MAH.2118 SOK.G�ZELOBA PTT KAR�ISINDA BULUNAN AR�EL�KTEN DEN�ZE �NEN SOKA�A �N�NCE SOLDAN 4. D�KKAN MURATPA�A"},
new Eczane { Id =296,Enlem=36.85869,Boylam=30.79431,Adres = "G�ZELOBA MH. 2239 SK.LARA ANADOLU HAST.AC�L G�R�� YANI CUMA PAZARI KAR�ISI"},
new Eczane { Id =286,Enlem=36.85009,Boylam=30.76349,Adres = "�AGLAYAN MAH. BARINAKLAR BULV.2000 SOKAK MIGROS ARKASI 28 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =293,Enlem=36.85825,Boylam=30.74394,Adres = "��R�NYALI MAH. �SMET G�K�EN CAD. 118 A RUMEL� ��KEMBEC�S� YANI"},
new Eczane { Id =299,Enlem=36.8518,Boylam=30.76371,Adres = "BARINAKLAR BULV.G�ZELOBA Y�N�NE TERRA CITY VE BEYAZ D�NYA ILERISI DENIZBANK KARSISI FENER KAVS. 200 MT ILERISI"},
new Eczane { Id =268,Enlem=36.85167,Boylam=30.75853,Adres = "FENER MH.TEKELIOGLU CAD.ASTUR SIT.NO:90/B TERRACITY AVM KARS. MURATPASA BELEDIYESI KARSISI"},
new Eczane { Id =301,Enlem=36.85644,Boylam=30.74635,Adres = "SIRINYALI MAHALLESI TEKELIOGLU CADDESI MEDICAL PARK. HAST. KARSISI ( SHEMALL HASTANE KAPISI KAR�ISI)"},
new Eczane { Id =267,Enlem=36.85762,Boylam=30.7705,Adres = "�A�LAYAN MAH. B�LENT ECEVIT BULVARI BANIO ILERISI PEK�Y� FIRIN �APRAZI"},
new Eczane { Id =273,Enlem=36.85524,Boylam=30.77151,Adres = "�A�LAYAN MAH. BARINAKLAR BULV. 2033 SOK. NO:10/A KAPALI PAZAR PAZARI �APRAZI LARA ASM YANI MURATPA�A"},
new Eczane { Id =184,Enlem=36.85668,Boylam=30.7897,Adres = "BARINAKLAR BULV.G�ZELOBA MAH.2118 SOK. NO:10/A G�ZELOBA PTT �APRAZI KAR�ISI AR�EL�K BAY� SOKA�I MURATPA�A"},
new Eczane { Id =304,Enlem=36.85647,Boylam=30.74631,Adres = "SIRINYALI MAHALLESI TEKELIOGLU CADDESI MEDICALPARK HASTANESI KARSISI"},
new Eczane { Id =283,Enlem=36.85868,Boylam=30.79463,Adres = "G�ZELOBA MAH.2246 SK.�ZEL LARA ANADOLU HASTANESI YAN KAPI KARSISI"},
new Eczane { Id =276,Enlem=36.86024,Boylam=30.78026,Adres = "�AGLAYAN MH. B�LENT ECEVIT BULV.CELAL SAHIN SIT. C BLOK NO:166/B GELECEK T�P BEBEK MERKEZI KARSISI"},
new Eczane { Id =307,Enlem=36.85436,Boylam=30.77717,Adres = "BARINAKLAR BULV.�AGLAYAN OPETI GE�INCE Y�R�KOGLU ALYA MARKET KARSISI"},
new Eczane { Id =270,Enlem=36.85827,Boylam=30.74535,Adres = "SIRINYALI MAH.ISMET G�KSEN CAD.LAURA AVM KAVSAGI VESTEL VE HAYVAN HASTANES� YANI"},
new Eczane { Id =278,Enlem=36.85842,Boylam=30.74651,Adres = "LAURA ALISVERIS MERKEZI GIRIS KAPISI KARS.R�YAMKENT1 SIT B�LENT ECEVIT BULVARI"},
new Eczane { Id =272,Enlem=36.85175,Boylam=30.76882,Adres = "�AGLAYAN MAH.BARINAKLAR BULV.Y�R�K APT. 47 / B YAPIKREDI BANKASI KARSISI"},
new Eczane { Id =291,Enlem=36.86184,Boylam=30.79355,Adres = "G�ZELOBA MAH. RAUF DENKTA� CAD. NO:18 /B K�PA KAV�A�ININ 200 MT �LER�S� LARA UNLU MAM�LLER� YANI"},
new Eczane { Id =285,Enlem=36.858,Boylam=30.74555,Adres = "SIRINYALI MAH.TEKELIOGLU CAD.PEHLIVAN APT NO:2/A MURATPASA LAURA AVM KARSISI BOLULU HASAN USTA YANI"},
new Eczane { Id =264,Enlem=36.85868,Boylam=30.7416,Adres = "SIRINYALI MAH. ISMET G�KSEN CAD. ATLAS APT.97/1 SIRINYALI OCAKBASI KARSI �APRAZI EFE KUYUMCULUK KARSISI"},
new Eczane { Id =277,Enlem=36.8533,Boylam=30.7573,Adres = "FENER MAH. 1968 SOK. LE�ENO�LU APT.NO:5/A TERRAC�TY ARKASI T�CARET BORSASI SEMT POL�K�L�N��� KAR�ISI MURATPA�A"},
new Eczane { Id =302,Enlem=36.85329,Boylam=30.75777,Adres = "TERRACITY AVM ARKA SOKAGI, BORSA SEMT POLIKLINIGI KARSISI FENER MAH. 1968.SOK 7/1 MURATPASA"},
new Eczane { Id =300,Enlem=36.85466,Boylam=30.79356,Adres = "KARPUZ KALDIRAN KAMPI KARSISI G�ZELOBA MAHALLESI 2121 SOKAK 15/B"},
new Eczane { Id =282,Enlem=36.85148,Boylam=30.76206,Adres = "�AGLAYAN MAH.BARINAKLAR BULV. NO:2/B BEYAZ D�NYA AVM.KARS. BURSA MEFRUSAT YANI"},
new Eczane { Id =275,Enlem=36.86204,Boylam=30.76467,Adres = "Z�MR�TOVA MAH. YALI CAD. P�DEX KAR�ISI MAKRO MARKET ARKASI"},
new Eczane { Id =308,Enlem=36.84967,Boylam=30.80314,Adres = "�RNEKK�Y �ARSISI NO:6 �RNEKK�Y HALISAHA VE MESCIDI YANI LARA - �RNEKK�Y LARA - �RNEKK�Y"},
new Eczane { Id =290,Enlem=36.85319,Boylam=30.75822,Adres = "FENER MH. BORSA SEMT POLIK. KARS. 1968 SK. NO:9/1 TERRACITY AVM ARKASI"},
new Eczane { Id =274,Enlem=36.85821,Boylam=30.79262,Adres = "G�ZELOBA MAHALLESI G�ZELOBA PAZARI VE LARA ANADOLU HASTANESI KARS. CARREFOUR- SA MARKET YANI"},
new Eczane { Id =303,Enlem=36.85632,Boylam=30.7464,Adres = "SIRINYALI MAH. TEKELIOGLU CAD. IZCI APT. NO:20/1 MEDICALPARK HASTANESI KARSISI"},
new Eczane { Id =289,Enlem=36.85781,Boylam=30.75657,Adres = "FENER MAH. B�LENT ECEVIT BULVARI 1981 SOK.NO:1 LAURA ILE MAKRO MARKET ARASI KARATA� EKMEK FIRINI KAR�ISI"},
new Eczane { Id =295,Enlem=36.85854,Boylam=30.76363,Adres = "�A�LAYAN MAH.B�LENT ECEV�T BULVARI FENER CAD.(YALI CADDES� YANI MAKRO MARKET ARKASI P�DEX YAKINI)"},
new Eczane { Id =287,Enlem=36.85521,Boylam=30.77144,Adres = "�A�LAYAN MAH. 2033 SOK. NO:12/ B BARINAKLAR MEZARLI�I �LE PEK�Y� EKMEK FIRINI ARASI �BRAH�M ATE� CAM�� KAR�ISI"},
new Eczane { Id =298,Enlem=36.85855,Boylam=30.79392,Adres = "G�ZELOBA MAH. �ZEL LARA ANADOLU HASTANESI YANI"},
new Eczane { Id =306,Enlem=36.85791,Boylam=30.75129,Adres = "B�LENT ECEVIT BULV.�ZG�L KENT SIT.A BLOK NO:22 LAURA AVM 300 MT. �LER�S�"},
new Eczane { Id =266,Enlem=36.85438,Boylam=30.79478,Adres = "G�ZELOBA MAH.NO:517 KARPUZKALDIRAN KAMPI KARSISI LARA"},
new Eczane { Id =280,Enlem=36.85609,Boylam=30.74723,Adres = "FENER MAH. TEKELIOGLU CAD. NO:9/A MURATPA�A MEDICAL PARK HASTANESI YANI"},
new Eczane { Id =499,Enlem=36.85183,Boylam=30.7647,Adres = "BARINAKLAR BULV. G�ZELOBA Y�N�NE TERRAC�TY VE BEYAZ D�NYA �LER�S� DEN�ZBANK KAR�ISI FENER KAV�A�I 200 MT �LER�S� MURATPA�A"},
new Eczane { Id =271,Enlem=36.85365,Boylam=30.75552,Adres = "FENER MAH.BORSA SEMT POLK. BATISI TERRA CITY AVM ARKASI SEDIR RESTAURANT YANI"},
new Eczane { Id =281,Enlem=36.8495,Boylam=30.80682,Adres = "G�ZELOBA MAH. SERA OTELI KARSISI LARA CADDESI �RNEKK�Y"},
new Eczane { Id =269,Enlem=36.85613,Boylam=30.74669,Adres = "SIRINYALI MAH.TEKELIOGLU CAD.NO:24 / 1 MEDICALPARK HASTANESI KARSISI"},
new Eczane { Id =279,Enlem=36.85179,Boylam=30.76409,Adres = "BARINAKLAR BULV.TERRA CITY ILERISI DENIZ BANK �APRAZ KAR�ISI VE BEYAZ D�NYA ILERISI"},
new Eczane { Id =305,Enlem=36.85019,Boylam=30.76347,Adres = "BARINAKLAR BULV.2000 SOK. FENER M�GROS ARKASI 28 NOLU AHMET ATMACA ASM KAR�ISI"},
new Eczane { Id =288,Enlem=36.859,Boylam=30.77648,Adres = "B�LENT ECEVIT BULVARI OPET YANI"},
new Eczane { Id =284,Enlem=36.85857,Boylam=30.73914,Adres = "SIRINYALI MAH. ISMET G�KSEN CAD. SEMIHA KURT APT. 78/A LARA MEMORIAL HAST. YANI"},
new Eczane { Id =262,Enlem=36.85102,Boylam=30.80522,Adres = "�RNEKK�Y M�GROS KARSISI LARA PLAJLARINA INISTE CLUB OTEL SERA YANI SGK ILE ANLASMASI YOKTUR"},
new Eczane { Id =265,Enlem=36.85799,Boylam=30.79026,Adres = "G�ZELOBA MAH. BARINAKLAR BULVARI CAD. NO:230 G�ZELOBA CAMII KARSISI"},
new Eczane { Id =297,Enlem=36.86045,Boylam=30.7409,Adres = "��R�NYALI MAH. 1533 SOK. NO:25/B DEMOKRAS� �EH�TLER� ASM YANI RAMAZAN SAVA� �LKOKULU KAR�ISI MURATPA�A"},
new Eczane { Id =333,Enlem=36.88859,Boylam=30.62883,Adres = "UNCALI 2M MIGROS DOGUSU 35.CD.�ZERI DEFNE 1 KONUTLARI 16/3 KONYAALTI"},
new Eczane { Id =317,Enlem=36.90114,Boylam=30.66076,Adres = "K�LT�R MAH.TIP FAK�LTESI OTOPARK �IKISI KARSISI YENI �AKIRLAR YOLU"},
new Eczane { Id =326,Enlem=36.89198,Boylam=30.62616,Adres = "UNCALI SEMT POLIKLINIGININ YAN TARAFI �EVIK KUVVET KARSISI"},
new Eczane { Id =337,Enlem=36.901,Boylam=30.66037,Adres = "TIP FAK�LTESI ARABA �IKISI KARSISI K�LT�R MAH.12/2 ILLER BANKASI ILERISI"},
new Eczane { Id =314,Enlem=36.88745,Boylam=30.63366,Adres = "UNCALI MAH.�N�VERS�TE CAD. NO:16/CA ATLANT�K TAKS� KAR�ISI KONYAALTI"},
new Eczane { Id =311,Enlem=36.89311,Boylam=30.62129,Adres = "MOLLA YUSUF MAH.1425 SK. UNCALI MEYDAN HASTANESI YANI"},
new Eczane { Id =327,Enlem=36.9099,Boylam=30.6456,Adres = "AKDENIZ SAN.SIT.YOLU �ZERI AHATLI 17 NO'LU SA�LIK OCAGI KARS.TURGUT REIS CAMII KARSISI"},
new Eczane { Id =336,Enlem=36.90111,Boylam=30.6603,Adres = "K�LT�R MAH.TIP FAK�LTESI OTOPARK �IKISI KARSISI YENI �AKIRLAR YOLU"},
new Eczane { Id =312,Enlem=36.90117,Boylam=30.66116,Adres = "K�LT�R MAH.TIP FAK�LTESI OTOPARK �IKISI KARSISI YENI �AKIRLAR YOLU"},
new Eczane { Id =324,Enlem=36.8929,Boylam=30.62152,Adres = "MOLLA YUSUF MAH.1425 SK.�ZEL UNCALI MEYDAN HASTANESI YANI HACI SADIYE AY APT.7/5 KONYAALTI"},
new Eczane { Id =313,Enlem=36.90382,Boylam=30.66179,Adres = "K�LT�R MAH. YENI �GRETMENEVI ARKASI K�LT�R SAGLIK OCAGI YANI"},
new Eczane { Id =315,Enlem=36.90115,Boylam=30.66084,Adres = "AKDENIZ �NI.TIP FAK�LTESI KUZEYI OTOPARK �IKISI KARSISI YENI �AKIRLAR YOLU �ZERI"},
new Eczane { Id =318,Enlem=36.90365,Boylam=30.66205,Adres = "K�LT�R MH. YENI �GRETMENEVI ARK. K�LT�R SAGLIK OCAGI KARS."},
new Eczane { Id =334,Enlem=36.89273,Boylam=30.62145,Adres = "MOLLA YUSUF MH. 1425 SK.UNCALI MEYDAN HAST.YANI"},
new Eczane { Id =329,Enlem=36.90862,Boylam=30.65562,Adres = "AHATLI MAH. 3128 SOK. KA�MAZ APT. NO:3/A ULUSOY CAD. AHATLI FIRINI ARKASI"},
new Eczane { Id =331,Enlem=36.90953,Boylam=30.64643,Adres = "YESILYURT MAH.4301 SOK NO:35/A KEPEZ (Akdeniz Sanayi yolu �zeri, Ahatli Saglik Ocagi karsisi)"},
new Eczane { Id =310,Enlem=36.89315,Boylam=30.6215,Adres = "MOLLA YUSUF MH.1425 SK.�ZEL UNCALI MEYDAN HASTANESI YANI KONYAALTI"},
new Eczane { Id =322,Enlem=36.88856,Boylam=30.62944,Adres = "UNCALI 2M MIGROS DOGUSU 35.CD.�ZERI DEFNE 1 KONUTLARI 16/AB KONYAALTI"},
new Eczane { Id =323,Enlem=36.90116,Boylam=30.66098,Adres = "K�LT�R MH.TIP FAK.HAST.OTO �IKIS KARS.ILLER BANK.SIRASI �AKIRLAR YOLU"},
new Eczane { Id =320,Enlem=36.89527,Boylam=30.63398,Adres = "SITELER MAH. 1327 SOKAK SITELER KAPALI PAZAR YERI YAKINI �ILEM APT NO:28/D MAVITEK SITESI YANI"},
new Eczane { Id =338,Enlem=36.90923,Boylam=30.64747,Adres = "75.YIL CAD.YENI SANAYI YOLU IS BANKASI KARSISI YESILYURT / KEPEZ / ANTALYA"},
new Eczane { Id =332,Enlem=36.88823,Boylam=30.62821,Adres = "UNCALI 2 M MIGROS DOGUSU 35. CAD.SUN CITY KONUTLARI"},
new Eczane { Id =321,Enlem=36.91002,Boylam=30.6457,Adres = "AKDEN�Z SAN.S�T. YOLU �ZER� AHATLI 17 NOLU SA�LIK OCA�I KAR�ISI TURGUT RE�S CAM�� KAR�ISI"},
new Eczane { Id =325,Enlem=36.90355,Boylam=30.66206,Adres = "K�LT�R MAH.3806 SK. 26/6 ILLER BANKASI ARKASI 18 NOLU A�LE SA�LI�I MERKEZ� KAR�ISI"},
new Eczane { Id =330,Enlem=36.91574,Boylam=30.66367,Adres = "YENIDOGAN MAH. ELMALI SITESI S�F�RLER ODASI YANI PAZARYERI KARSISI"},
new Eczane { Id =319,Enlem=36.89191,Boylam=30.6261,Adres = "UNCALI SEMT POLIKLINIGININ YANI �EVIK KUVVET KARSISI"},
new Eczane { Id =335,Enlem=36.90987,Boylam=30.64634,Adres = "AKDENIZ SAN.SIT.YOLU �ZERI AHATLI SAGLIK OCAGI KARS."},
new Eczane { Id =366,Enlem=36.91254,Boylam=30.64322,Adres = "�AFAK MAH. 4256 SOK. NO:24/8 KEPEZ ELEGANT PLAZA ARKASI B�M KAR�ISI"},
new Eczane { Id =328,Enlem=36.90966,Boylam=30.64717,Adres = "AHATLI MAH. (AKDENIZ SANAYI SITESI YOLU �ZERI) AHATLI SAGLIK OCAGI KARSISI"},
new Eczane { Id =316,Enlem=36.90924,Boylam=30.65082,Adres = "AHATLI MAH. ULUSOY CAD.NO:70/C"},
new Eczane { Id =368,Enlem=36.88287,Boylam=30.66046,Adres = "5 M MIGROS ALISVERIS MERKEZI I�I MELTEM MAH. 100.YIL BULVARI"},
new Eczane { Id =805,Enlem=36.87312,Boylam=30.64238,Adres = "KU�KAVA�I MAH.ATAT�RK BULVARI.83/A MEHMET KODAK APT. KONYALTI*AKDEN�Z ��FA HASTANES� YANI .ANTALYA"},
new Eczane { Id =355,Enlem=36.86885,Boylam=30.63002,Adres = "G�RSU MAH. GAZ� MUSTAFA KEMAL BULV.NO:53/A KONYAALTI"},
new Eczane { Id =381,Enlem=36.85386,Boylam=30.61371,Adres = "LIMAN MAH.16 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =362,Enlem=36.87422,Boylam=30.62667,Adres = "GAZI MUSTAFA KEMAL BULV. D�LEK APT.NO:2 SPORLAND YANI MIGROS JET KARSISI"},
new Eczane { Id =380,Enlem=36.85373,Boylam=30.6138,Adres = "L�MAN MAH.BO�A�AYI CAD.NO:36/A KONYAALTI"},
new Eczane { Id =339,Enlem=36.87359,Boylam=30.64691,Adres = "ARAPSUYU MAH. 6.CD.BASEL OTEL YANI"},
new Eczane { Id =340,Enlem=36.85345,Boylam=30.61358,Adres = "LIMAN MAH.BOGA�AYI CAD.32.SOK. YAL�IN APT. NO: 14/C KONYAALTI"},
new Eczane { Id =346,Enlem=36.87077,Boylam=30.63352,Adres = "�ZEL OLIMPOS HASTANESI KARSISI ALTINKUM MH.460 SOKAK NO:18"},
new Eczane { Id =885,Enlem=36.85427,Boylam=30.61465,Adres = "L�MAN MAH. 15. SOK. NO:5/B ALTINYAKA YOLU �ZER� 16 NOLU SA�LIK OCA�I KAR�ISI KONYAALTI"},
new Eczane { Id =375,Enlem=36.87044,Boylam=30.63382,Adres = "ALTINKUM MH.460 SK.NO: 20/B �ZEL OLIMPOS HASTANESI KARSISI"},
new Eczane { Id =495,Enlem=36.88425,Boylam=30.64213,Adres = "TOROS MAH. 836 SOK. �ET�N APT. NO:7/1 KONYAALTI"},
new Eczane { Id =370,Enlem=36.87519,Boylam=30.64079,Adres = "ARAPSUYU CARREFOURSA ARKASI �GRETMENEVLERI MH.18.CD.NO:36/10 KONYAALTI"},
new Eczane { Id =807,Enlem=36.85981,Boylam=30.60507,Adres = "HURMA MAHALLES�.BO�A�AY CADDES�.NO:88/A KONYALTI/ANTALYA"},
new Eczane { Id =341,Enlem=36.87305,Boylam=30.65082,Adres = "ARAPSUYU MAH.BELED�YE CAD.( �L SA�LIK M�D.'DEN DEN�Z �ST�KAMET�NE DO�RU YOL B�T�NCE SA�A D�N�LECEK) KONYAALTI"},
new Eczane { Id =365,Enlem=36.85954,Boylam=30.60483,Adres = "HURMA MAH.BOGA�AY CAD.NO:91/A-B(ESKI SEMAZEN) URFA SULTAN SOFRASI KARSISI"},
new Eczane { Id =361,Enlem=36.8836,Boylam=30.62989,Adres = "UNCALI MAKRO MARKET KARSISI ANTALYA PARK KONUTLARI BANIO YAPI MARKET YAKINI"},
new Eczane { Id =377,Enlem=36.87352,Boylam=30.64261,Adres = "AKDENIZ SIFA KONYAALTI TIP MERKEZI YANI ATAT�RK BULVARI NO 79"},
new Eczane { Id =353,Enlem=36.86434,Boylam=30.63458,Adres = "ALTINKUM MH.G.MUSTAFA KEMAL BULV.AVEA (ESKI T�RKAY OTEL)KVS.50 METRE G�NEYINDE SEALIFE OTEL KUZEYI"},
new Eczane { Id =359,Enlem=36.87249,Boylam=30.64093,Adres = "�GRETMENEVLERI MAH. 924 SK. AKIN APT. 4 / 3 CANET ARKASI KONYAALTI"},
new Eczane { Id =354,Enlem=36.88778,Boylam=30.64537,Adres = "TOROS MAH.�N�VERS�TE CAD. NO:54/A KONYAALTI �N�VERS�TEN�N G�NEY �IKI� KAPISI"},
new Eczane { Id =364,Enlem=36.87076,Boylam=30.62802,Adres = "G�RSU MAH.GAZI MUSTAFA KEMAL BULV.SPOARLAND KAV�A�INDAN DEN�ZE DO�RU 500 MT �LER�DE ANA YOLDA SA�DA"},
new Eczane { Id =367,Enlem=36.88248,Boylam=30.65563,Adres = "PINARBASI MAH. KAMA� APT. B BLOK NO:1 5M MIGROS KARSISI"},
new Eczane { Id =351,Enlem=36.86687,Boylam=30.63232,Adres = "ALTINKUM MAH.GAZ� MUSTAFA KEMAL BULV.ESK� T�RKAY OTEL� (T�RK TELEKOM) YUKARISI T�VAK MARKET KAR�I �APRAZI"},
new Eczane { Id =344,Enlem=36.87077,Boylam=30.63366,Adres = "ALTINKUM MAH.�AMLIK CAD.OLIMPOS HASTANESI KARSISI 100 METRE ILERISI"},
new Eczane { Id =360,Enlem=36.84904,Boylam=30.60634,Adres = "LIMAN MH.3. CD. (BILEYDILER CD) SARMA�IK APT. NO:44/B"},
new Eczane { Id =349,Enlem=36.85927,Boylam=30.60519,Adres = "HURMA MAH. BOGA�AY CAD. NO:87/A-B (ESKI SEMAZEN) URFA SULTAN SOFRASI KARSISI"},
new Eczane { Id =347,Enlem=36.85354,Boylam=30.61343,Adres = "LIMAN MAH. BOGA�AYI CAD. 16 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =343,Enlem=36.88724,Boylam=30.64726,Adres = "PINARBASI MH.14.CD.SONU �N�VERS�TE STAD G�R�� KAPISI KAR�ISI B�M VE A101 MARKET KAR�ISI"},
new Eczane { Id =345,Enlem=36.87598,Boylam=30.64375,Adres = "ATAT�RK BULVARI IL SAGLIK M�D. GE�INCE KONYAALTI SED�R RESTAURANT YANI"},
new Eczane { Id =357,Enlem=36.8778,Boylam=30.65137,Adres = "ARAPSUYU MAH. ARAPSUYU CAD. �L SA�LIK M�D�RL���NDEN SAH�LE DO�RU A101 MARKET YAKINI"},
new Eczane { Id =376,Enlem=36.84981,Boylam=30.61409,Adres = "L�MAN MAH. ATAT�RK BULVARI CAD. NO:230/A KONYAALTI"},
new Eczane { Id =350,Enlem=36.85396,Boylam=30.61941,Adres = "LIMAN MAH.YENI BOGA�AYI K�PR�S� BASI"},
new Eczane { Id =363,Enlem=36.87475,Boylam=30.64186,Adres = "�GRETMENLER MAH. �GRETMENLER CAD.NO: 3/B KASAP SEREF YANI VATAN COMPUTER KARISISI"},
new Eczane { Id =348,Enlem=36.86543,Boylam=30.63479,Adres = "ALTINKUM MH.ATAT�RK BULV.GARANTI BANKASI YANI ESKI T�RKAY OTELI KARSISI"},
new Eczane { Id =342,Enlem=36.86919,Boylam=30.62833,Adres = "G�RSU MAH ANADOLU CAD.NO: 28/A ESKI TURKAY OTELDEN UNCALIYA �IKARKEN CARREFOURSA ARKASI SOK MARKET KARSISI"},
new Eczane { Id =369,Enlem=36.8479,Boylam=30.60752,Adres = "LIMAN MAH.45.SOK.BELLONA VE SEZERLER PETROL ARKASI HURMA AILE SAGLIGI MERKEZI KARSISI"},
new Eczane { Id =374,Enlem=36.87101,Boylam=30.63332,Adres = "ALTINKUM MAH 460 SOKAK AYFER APT 27/B OL�MPOS HASTANES� KAR�ISI"},
new Eczane { Id =356,Enlem=36.87225,Boylam=30.63262,Adres = "ALTINKUM MAH.OLIMPOS HASTANESI YUKARISI ( 50 MT ) �AGLARSOY MARKET KARSISI KONYAALTI"},
new Eczane { Id =358,Enlem=36.85393,Boylam=30.61615,Adres = "L�MAN MAH. 20.SOK. DERYA APT. NO:13/1 KONYAALTI"},
new Eczane { Id =373,Enlem=36.84819,Boylam=30.60686,Adres = "L�MAN MAH. . 33.SOKAK HURMA ASM YANI BELLONA OKYANUS KOLEJ� ARKASI"},
new Eczane { Id =378,Enlem=36.88057,Boylam=30.64004,Adres = "AKKUYU MH. 20. CD. 114/A KONYAALTI NASHIRA PARK ILE �NIVERSITE ARASI"},
new Eczane { Id =371,Enlem=36.88724,Boylam=30.64672,Adres = "TOROS MAH. PINARBA�I CAD. SEV�LAY APT. NO:31/A AKDEN�Z �N�VERS�TES� G�NEY KAPISI ( ESK� STADYUM G�R��� ) YANI"},
new Eczane { Id =372,Enlem=36.87145,Boylam=30.63286,Adres = "ALTINKUM MAH.460 SOKAK �ALIKUSU HUZUREVI ARKASI �ZEL OLIMPOS HASTANESI KARSISI"},
new Eczane { Id =379,Enlem=36.87071,Boylam=30.62788,Adres = "G�RSU MAH.GAZI MUSTAFA KEMAL BULVARI A 101 MARKET KARSISI 16 NOLU SAGLIK OCAGI BIRIMI KARSISI"},
new Eczane { Id =394,Enlem=36.90791,Boylam=30.67422,Adres = "FABRIKALAR MAH. FIKRI ERTEN CAD. BANDOCULAR YOLU KIPA KARSISI"},
new Eczane { Id =392,Enlem=36.9172,Boylam=30.69378,Adres = "ZAFER MH. YILDIRIM BEYAZIT CD. 2615 SK. D�NYA G�Z HAST.YANI MEMOR�AL HAST.AC�L KAR�ISI KEPEZ"},
new Eczane { Id =407,Enlem=36.92562,Boylam=30.68706,Adres = "BARI� MH.2906 SOK. HAL�L EFEND� APT. 24 NOLU BARI� SA�LIK OCA�I �APRAZ KAR�ISI"},
new Eczane { Id =388,Enlem=36.5515,Boylam=30.5513,Adres = "KANAL MAH.NAMIK KEMAL CAD.5.�NAL APT. NO:136/A KEPEZ V�TALE HASTANES� ANTALYA T�P BEBEK MERKEZ� YANI"},
new Eczane { Id =403,Enlem=36.91335,Boylam=30.68818,Adres = "MITHATPASA CAD.NO:58/A OFM HASTANESINDEN �ZDILEKE DOGRU 200 MT ILERDE TRAFIK LAMBALARININ K�SESINDE"},
new Eczane { Id =384,Enlem=36.91714,Boylam=30.68544,Adres = "BARIS TAKSI KARSISI MITHAT PASA CADDESI NO: 118/A DOKUMA-KEPEZ"},
new Eczane { Id =408,Enlem=36.90915,Boylam=30.68361,Adres = "ULUS MH.RASIH KAPLAN CD.EVREN B�FE ASAGISI MEPAS KARSISI DOKUMA"},
new Eczane { Id =387,Enlem=36.914,Boylam=30.683,Adres = "�ZG�RL�K MAH. 2666 SOKAK KAPALI HALK PAZARI YANI DOKUMA"},
new Eczane { Id =386,Enlem=36.91051,Boylam=30.67719,Adres = "�ZDILEK PARK AVM I�I DOKUMA MEVKII"},
new Eczane { Id =410,Enlem=36.90847,Boylam=30.68365,Adres = "�ALLI KAVSAGI DOKUMA ISTIKAMETI 50 MT SAGINDA 14 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =496,Enlem=36.90905,Boylam=30.68354,Adres = "ULUS MAH. RAS�H KAPLAN CAD. EVREN B�FE A�A�ISI MEPA� KAR�ISI - DOKUMA"},
new Eczane { Id =391,Enlem=36.91518,Boylam=30.69206,Adres = "ZAFER MAH. MEHMET AKIF CAD. NO:113/2 KEPEZ - OFM HASTANESI KARSI �APRAZI"},
new Eczane { Id =401,Enlem=36.91475,Boylam=30.69101,Adres = "ZAFER MH.MEHMET AKIF CD.CUMARTESI PAZARI YANI DOKUMA OFM HASTANESI KARSISI"},
new Eczane { Id =393,Enlem=36.9162,Boylam=30.67746,Adres = "YESILTEPE MH. ZIYA G�KALP CAD.�ZKAVAK OTELI YANI SAGLIK BIRIMI YANI - DOKUMA"},
new Eczane { Id =397,Enlem=36.91718,Boylam=30.69432,Adres = "ZAFER MAH.YILDIRIM BEYAZIT CAD. NO: 8/1 MEMOR�AL HASTANES� VE D�NYA G�Z HASTANESI YANI"},
new Eczane { Id =390,Enlem=36.91794,Boylam=30.69449,Adres = "KARSIYAKA MAH. YILDIRIM BEYAZIT CAD. NO:88/7 KEPEZ - D�NYAG�Z VE MEMORIAL HASTANELERI KARSISI"},
new Eczane { Id =409,Enlem=36.91847,Boylam=30.67563,Adres = "PIL FABRIKASI KARSISI ANTALYA T�P BEBEK MERKEZI YAKINI SUISLERI YOLU(OR�S KARS.)DOKUMA"},
new Eczane { Id =399,Enlem=36.91672,Boylam=30.69452,Adres = "ZAFER MAH.YILDIRIM BEYAZIT CAD.KARDI� APT. D�NYA G�Z HASTANESI YANI"},
new Eczane { Id =402,Enlem=36.90704,Boylam=30.67204,Adres = "FABR�KALAR MAH. 3024 SOK. F�KR� ERTEN CAD.NO:10-B/5 KEPEZ �ZD�LEK K�PA 150 MT. �LER�S�"},
new Eczane { Id =405,Enlem=36.91803,Boylam=30.6754,Adres = "PIL FABRIKASI KARSISI ANTALYA T�P BEBEKMERKEZI YANI SUISLERI YOLU(OR�S KARS.)DOKUMA"},
new Eczane { Id =396,Enlem=36.9165,Boylam=30.68556,Adres = "MITHATPASA CAD.BARIS TAKSI ASAGISI NO: 109/B ESKI DOKUMA AN-DEVA HASTANESI BITISIGI"},
new Eczane { Id =389,Enlem=36.9162,Boylam=30.67746,Adres = "YE��LTEPE MAH. Z�YA G�KALP CAD. NO:24/B KEPEZ - �ZKAVAK OTELI YANI ASM YANI - DOKUMA"},
new Eczane { Id =383,Enlem=36.9147,Boylam=30.69094,Adres = "ZAFER MAH.MEHMET AKIF CAD.CUMARTESI PAZARI YANI OFM HASTANESI KARSISI"},
new Eczane { Id =406,Enlem=36.90704,Boylam=30.67204,Adres = "FABR�KALAR MAH.3022 SOK. NO:4-A/7 �ZD�LEK-K�PA AVM ARKASI ��HRETLER HALISAHA KAR�ISI 07STAR D���N SALONU YANI KEPEZ"},
new Eczane { Id =395,Enlem=36.91911,Boylam=30.68751,Adres = "SAKARYA BULV.D�NYA G�Z HASTANESINDEN OTOGAR Y�N�NE 200 MT.ILERI ANTKOOP KARS.DOKUMA"},
new Eczane { Id =385,Enlem=36.91434,Boylam=30.69169,Adres = "Y�KSELIS MAH.2121 SOKAK OFM HAST.ACIL �IKISI KARSISI CUMARTESI PAZARI KARS. DOKUMA"},
new Eczane { Id =398,Enlem=36.91469,Boylam=30.69085,Adres = "ZAFER MAH.MEHMET AKIF CAD.CUMARTESI PAZARI YANI DOKUMA OFM HASTANESI KARSISI"},
new Eczane { Id =411,Enlem=36.90832,Boylam=30.68392,Adres = "�ALLI KAVSAGI DOKUMA Y�N� YAPIKREDI BANKASI ARKASI 14 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =400,Enlem=36.91129,Boylam=30.68321,Adres = "�ZG�RL�K MAH. M.AKIF CAD. ESKI CUMARTESI PAZARI CADDESI EVREN B�FE YAKINI"},
new Eczane { Id =429,Enlem=36.92002,Boylam=30.78609,Adres = "DEEPO - MALL OF ANTALYA AVM ��� HAVAALANI YOLU �ZER�"},
new Eczane { Id =826,Enlem=36.9155,Boylam=30.77161,Adres = "ALTINOVA S�NAN MAH.ANTALYA CADDES�.AGORA A.V.M.FAZ-2NO:14/23 KEPEZ"},
new Eczane { Id =424,Enlem=36.91718,Boylam=30.7016,Adres = "KAR�IYAKA MAH.3933 SOK.SAKARYA BULVARI ERDEM BEYAZIT K�LT�R MERKEZ� KAR�ISI LA��N FIRIN ARKASI KEPEZ"},
new Eczane { Id =417,Enlem=36.91755,Boylam=30.70905,Adres = "EMEK MAH.2184 SK.I �EH�TLER PARKI A�A�ISI 34 NOLU SAGLIK OCAGI KAR�ISI"},
new Eczane { Id =434,Enlem=36.91388,Boylam=30.72037,Adres = "TEOMANPASA MH.GAZI BULV.SEMA YAZAR DEVLET HAST.YANI"},
new Eczane { Id =430,Enlem=36.91388,Boylam=30.72058,Adres = "�EVREYOLU GAZI BULVARI SEMA YAZAR POLIKLINIGI YANI NO:327"},
new Eczane { Id =426,Enlem=36.9188,Boylam=30.73267,Adres = "G�NES MAH. 6025 SOK. EKIN SIT. A3 BLOK NO:1( DEMIRG�L M�GROS YANI (ESKI MAKRO )"},
new Eczane { Id =435,Enlem=36.9193,Boylam=30.7324,Adres = "G�NES MAH. 6022 SOK. SINEM SITESI DEMIRG�L MAKRO MARKET(ESK� GENPA) YANI 7 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =425,Enlem=36.92264,Boylam=30.70243,Adres = "YENI MH.1 NOLU ANA �OCUK SAGLIGI MERK.KEPEZ BELEDIYE FIRINI PAZAR PAZARI KARSISI"},
new Eczane { Id =428,Enlem=36.91387,Boylam=30.71988,Adres = "SEMA YAZAR HASTANESI YANI GAZI BULVARI NO:319"},
new Eczane { Id =414,Enlem=36.91753,Boylam=30.70868,Adres = "EMEK MH.SAKARYA PARKI KARSISI 34 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =415,Enlem=36.91894,Boylam=30.72765,Adres = "G�NES MH. G�NES CD. NO:66 KEPEZ DEVLET HASTANES� 800 M G�NEY� 15 KATLI B�NA �APRAZI"},
new Eczane { Id =420,Enlem=36.9222,Boylam=30.71114,Adres = "YEN� MAH. AL�YE �ZZETBEGOV�� CAD. NO:48/A KEPEZ MOB�L �LK��RET�M OKULU �APRAZI"},
new Eczane { Id =431,Enlem=36.91759,Boylam=30.73406,Adres = "D�DENBASI MAH.2354 SOKAK G�NESEVLER SIT. A BLOK NO:5 DEMIRG�L GENPA KARSISI"},
new Eczane { Id =427,Enlem=36.91777,Boylam=30.70968,Adres = "EMEK MAH. SAKARYA PARKI KARSISI 34 NOLU SAGLIK OCAGI YANI"},
new Eczane { Id =422,Enlem=36.92313,Boylam=30.69723,Adres = "YENI EMEK MAH. 2566 SOKAK NO:71 6 NOLU SAGLIK OCAGI KARSISI KEPEZ M�FT�L��� KUZEY�NDEK� MODA D���N SALONU ARKASI, CUMA PAZARI YANI"},
new Eczane { Id =438,Enlem=36.91756,Boylam=30.70936,Adres = "EMEK MAH.2184 SOKAK NO: 16 �EH�TLER PARKI A�A�ISI 34 NOLU SA�LIK OCA�I KAR�ISI"},
new Eczane { Id =433,Enlem=36.91735,Boylam=30.71485,Adres = "EMEK MH.YESILIRMAK CD.41/2 SAKARYA PARKI KARSISI SELALE TIP MERKEZI YANI VAN G�L� LOKANTASI KAR�ISI"},
new Eczane { Id =416,Enlem=36.91973,Boylam=30.73353,Adres = "G�NES MAH. EKIN SITESI DEMIRG�L M�GROS (ESKI GENPA) ARKASI 7 NOLU ASM YANI"},
new Eczane { Id =421,Enlem=36.91381,Boylam=30.70849,Adres = "EMEK MAH. 2179 SOK. 32/1 SAKARYA SAGLIK OCAGI YANI GENERAL SADI �ETINKAYA OKULU KARSISI"},
new Eczane { Id =412,Enlem=36.91833,Boylam=30.72833,Adres = "G�NES MAH.NECIP FAZIL KISAK�REK TURAN APT. CAD.95/A ( TEOMANPASA CIGERCI EMIN USTA YANI )"},
new Eczane { Id =418,Enlem=36.9185,Boylam=30.72746,Adres = "TEOMANPA�A MAH.G�NE� CAD.C��ERC� EM�N USTA �APRAZI NO:65/A G�NE� SA�LIK OCA�I KAR�ISI"},
new Eczane { Id =413,Enlem=36.92301,Boylam=30.703,Adres = "YENI MH.1 NOLU ANA �OCUK SAGLIGI MERK.KEPEZ BELEDIYE FIRINI PAZAR PAZARI KARSISI (KARATAY L�SES� B�LGES�)"},
new Eczane { Id =419,Enlem=36.91775,Boylam=30.71604,Adres = "TEOMANPASA MAH.NEC�P FAZIL KISAK�REK CAD. NO:12/B �ELALE TIP MERKEZ� KAR�ISI TEOMANPA�A G�R��� �OK MARKET KAR�ISI"},
new Eczane { Id =423,Enlem=36.91391,Boylam=30.72069,Adres = "TEOMANPASA MAH.GAZI BULVARI SEMA YAZAR DEVLET HASTANESI YANI"},
new Eczane { Id =436,Enlem=36.91382,Boylam=30.71953,Adres = "GAZI BULV. TEOMANPASA MAH.SEMA YAZAR DEVLET HAST.YANI"},
new Eczane { Id =437,Enlem=36.91707,Boylam=30.72398,Adres = "TEOMANPA�A MAH. 2230 SOK. NO:5 KEPEZ NEC�P FAZIL KISAK�REK ASM YANI KAPALI CUMA PAZARI YANI"},
new Eczane { Id =481,Enlem=36.93155,Boylam=30.72391,Adres = "S�T��LER MAH.HASTANE CAD. NO:31/F KEPEZ DEVLET HASTANES� KAR�ISI"},
new Eczane { Id =474,Enlem=36.93391,Boylam=30.72538,Adres = "S�T��LER MAH. G�NE� CAD. 127-A - KEPEZ DEVLET HAST. KAR�ISI"},
new Eczane { Id =467,Enlem=36.93327,Boylam=30.72523,Adres = "S�T��LER MAH. G�NE� CAD. 125/A NO:7 KEPEZ DEVLET HASTANES� YANI"},
new Eczane { Id =460,Enlem=36.93367,Boylam=30.72594,Adres = "H�SN� KARAKA� MAH.G�NE� CAD. DERYAM APT. NO:126/B KEPEZ DEVLET HASTANES� YANI KEPEZ"},
new Eczane { Id =469,Enlem=36.92352,Boylam=30.71685,Adres = "MEHMET AK�F ERSOY MAH. MEHMET ATAY CAD. 6758 SOK.NO:5/7 D�DEN ASM YANI"},
new Eczane { Id =448,Enlem=36.93405,Boylam=30.70714,Adres = "KUZEYYAKA MAH.G�KSU CAD.NO:93/A GOLD D���N SARAYININ 500 METRE KUZEY� KUZEYYAKA SA�LIK OCA�I KAR�ISI"},
new Eczane { Id =478,Enlem=36.95052,Boylam=30.71813,Adres = "HABIBLER MAH.5603 SOKAK NO:7 35 NOLU SAGLIK OCAGI KARSISI"},
new Eczane { Id =468,Enlem=36.98143,Boylam=30.71528,Adres = "VARSAK DEM�REL MAH. S�LEYMAN DEM�REL BULVARI VARSAK SA�LIK OCA�I KAR�ISI NO:224/A KEPEZ (MASAL PARKA GELMEDEN 300 MT GER�DE)"},
new Eczane { Id =828,Enlem=36.92323,Boylam=30.71402,Adres = "MEHMET AKIF ERSOY MAH. YESILIRMAK CAD. NO:48 MOB�L KAVSA�I ESK� ANADOLU REKLAM KAR�I �APRAZI 06 ASPAVA KOKORE� YANI"},
new Eczane { Id =475,Enlem=36.93362,Boylam=30.7258,Adres = "H�SN� KARAKA� MAH. G�NE� CAD. NO:126/F KEPEZ DEVLET HASTANES� YANI"},
new Eczane { Id =447,Enlem=36.9397,Boylam=30.72364,Adres = "HABIBLER MH.S�TC�LER CD.NO:8 11 NOLU ZEHRA KOZ SAGLIK OCAGI YANI"},
new Eczane { Id =809,Enlem=36.93111,Boylam=30.72603,Adres = "G�NE� MAH.�EH�T AST.�MER HAL�S DEM�R CAD. NO:42/11 KEPEZ DEVLET HASTANES� YANI KEPEZ / ANTALYA"},
new Eczane { Id =446,Enlem=36.98122,Boylam=30.71413,Adres = "VARSAK KAR�IYAKA MAH. 1161 SOKAK NO:3-B ( VARSAK SA�LIK OCA�I / D�� SA�LI�I MERKEZ� YANI) - KEPEZ"},
new Eczane { Id =449,Enlem=36.93075,Boylam=30.72602,Adres = "S�T��LER MAH. G�NE� CAD. NO:125/6 KEPEZ DEVLET HASTANES� KAR�ISI"},
new Eczane { Id =454,Enlem=36.9309,Boylam=30.72548,Adres = "S�T��LER MAH.G�NE� CAD.NO:121/F KEPEZ DEVLET HASTANES� G�R��� KAR�ISI"},
new Eczane { Id =464,Enlem=36.93092,Boylam=30.72539,Adres = "S�T��LER MAH.G�NES CAD. NO:121/D KEPEZ DEVLET HASTANESI �APRAZI"},
new Eczane { Id =455,Enlem=36.98138,Boylam=30.71525,Adres = "S�LEYMAN DEMIREL BULVARI VARSAK SAGLIK OCAGI KARSISI VARSAK"},
new Eczane { Id =461,Enlem=36.93059,Boylam=30.72744,Adres = "G�NE� MAH.HASTANE CAD. �BRAH�M �AVU� �� MERKEZ� NO:52/2 KEPEZ DEVLET HASTANES� KAR�ISI"},
new Eczane { Id =471,Enlem=36.94383,Boylam=30.70907,Adres = "KUZEYYAKA MH.YESILIRMAK CD.VARSAK K�PR�S� ALTI AKDENIZ SIFA HASTANESI YANI"},
new Eczane { Id =462,Enlem=36.93921,Boylam=30.72224,Adres = "HABIBLER MH.S�TC�LER CD.NO:75/B 11 NOLU ZEHRA KOZ SAGLIK OCAGI KARSISI"},
new Eczane { Id =472,Enlem=36.98076,Boylam=30.71454,Adres = "VARSAK KARSIYAKA MAH.1161 SOK. NO:3 KEPEZ - VARSAK DIS SAGLIGI MERKEZI YANI"},
new Eczane { Id =480,Enlem=36.94155,Boylam=30.70395,Adres = "FEVZI �AKMAK MAH.6168 SOKAK NO:20 AILE SAGLIGI MERKEZI KARSISI"},
new Eczane { Id =477,Enlem=36.96761,Boylam=30.72832,Adres = "SELALE MAH.21.CAD.KEPEZ D�DEN SELALESI GIRISI ILERISI"},
new Eczane { Id =441,Enlem=36.94462,Boylam=30.70963,Adres = "GAZI MH. 6615 SK. AKDENIZ SIFA HASTANESI KARSISI LALEPARK EVLERI A3-BLOK NO:12 KEPEZ"},
new Eczane { Id =470,Enlem=36.94401,Boylam=30.70904,Adres = "KUZEYYAKA MH.YESILIRMAK CD.VARSAK K�PR�S� ALTI AKDENIZ SIFA HASTANESI YANI"},
new Eczane { Id =451,Enlem=36.97213,Boylam=30.70174,Adres = "VARSAK KAR�IYAKA MAH. T�RKO�LU CAD. VARSAK P�DEDEN YUKARI 3. KAV�AKTAN SOLA D�N�NCE 37 NOLU SA�LIK OCA�I KAR�ISI"},
new Eczane { Id =445,Enlem=36.93044,Boylam=30.73165,Adres = "H�SN� KARAKA� MAH. HASTANE CAD.NO:86/7-8 KEPEZ"},
new Eczane { Id =482,Enlem=36.93342,Boylam=30.72586,Adres = "H�SN� KARAKA� MAH. G�NE� CAD. NO:126/D KEPEZ DEVLET HASTANES� YANI"},
new Eczane { Id =352,Enlem=36.93092,Boylam=30.72619,Adres = "G�NE� MAH. �HT. AST.�MER HAL�SDEM�R CAD. 42/14 KEPEZ DEVLET HAST.YANI"},
new Eczane { Id =479,Enlem=36.92439,Boylam=30.72125,Adres = "MEHMET AK�F ERSOY MAH .6752 SOK. NO: 22/B KEPEZ (MEHMET ATAY CAD.)15 KATLI B�NADAN 500 MT BATIDA B�M ARKASI"},
new Eczane { Id =456,Enlem=36.9331,Boylam=30.72622,Adres = "S�T��LER MAH.KEPEZ DEVLET HASTANES� KAR�ISI KEPEZ"},
new Eczane { Id =465,Enlem=36.93524,Boylam=30.70743,Adres = "KUZEY YAKA MAH. G�KSU CAD. NO:95/5 GOLD D�G�N SARAYININ 500 MT KUZEYI KUZEYYAKA ASM KARSISI"},
new Eczane { Id =453,Enlem=36.93972,Boylam=30.72257,Adres = "HABIBLER MH.S�TC�LER CD.5500 SK.NO:8/1 11 NOLU ZEHRA KOZ SAGLIK OCAGI KARSISI"},
new Eczane { Id =443,Enlem=36.93073,Boylam=30.72643,Adres = "G�NE� MAH. HASTANE CAD. NO:42/12 KEPEZ KEPEZ DEVLET HASTANES� KAR�ISI"},
new Eczane { Id =459,Enlem=36.93073,Boylam=30.72583,Adres = "G�NE� MAH.HASTANE CAD.NO:42/13 KEPEZ DEVLET HASTANES� YANI"},
new Eczane { Id =458,Enlem=36.92824,Boylam=30.71472,Adres = "MEHMET AKIF ERSOY MAH. S�T��LER CAD. NO:16 D�DEN SELALESI YOLU"},
new Eczane { Id =466,Enlem=36.93042,Boylam=30.73156,Adres = "H�SN� KARAKA� MAH. HASTANE CAD. NO:86 D-10 KEPEZ S�T��LER ASM YANI"},
new Eczane { Id =473,Enlem=36.93852,Boylam=30.70518,Adres = "KUZEYYAKA MAH. 2546 SOK. NO:12/1-2 KEPEZ YE��LIRMAK ASM YANI"},
new Eczane { Id =497,Enlem=36.97258,Boylam=30.70151,Adres = "VARSAK-KAR�IYAKA MAH. T�RKO�LU CAD. VARSAK TAHTAKALE'DEN YUKARI 37 NOLU ASM KAR�ISI ( A101 VE �OK MARKET ARASINDA) NO:44B/1 KEPEZ"},
new Eczane { Id =476,Enlem=36.93056,Boylam=30.72835,Adres = "G�NES MAH.HASTANE CAD.�BRAH�M �AVU� �� MERKEZ� NO:52/1 KEPEZ DEVLET HASTANES� KAR�ISI"},
new Eczane { Id =452,Enlem=36.98133,Boylam=30.71433,Adres = "S�LEYMAN DEMIREL BULVARI VARSAK SAGLIK OCAGI YANI VARSAK"},
new Eczane { Id =442,Enlem=36.9441,Boylam=30.70837,Adres = "KUZEYYAKA MAH. VARSAK K�PR�S� ALTI AKDENIZ SIFA HASTANESI ACIL �IKISI YANI"},
new Eczane { Id =439,Enlem=36.9813,Boylam=30.71524,Adres = "S�LEYMAN DEMIREL BULV.VARSAK SAGLIK OCAGI KARSISI"},
new Eczane { Id =463,Enlem=36.93301,Boylam=30.72524,Adres = "S�T��LER MAH.G�NE� CAD. NO:125/8 KEPEZ DEVLET HASTANES� KAR�ISI"},
new Eczane { Id =440,Enlem=36.93919,Boylam=30.72384,Adres = "S�T��LER CAD. D�DEN SELALESI YOLU �ZERI 11 NOLU ZEHRA KOZ SAGLIK OCAGI YANI"},
new Eczane { Id =457,Enlem=36.98171,Boylam=30.71541,Adres = "DEM�REL MAH. 14.CAD. NO:19/A M�MAR S�NAN A�LE MERKEZ� YANI KEPEZ VARSAK"},             


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
             //       Adres = "��FTL�KK�Y MH. 32133 SK. NO:6/B",
             //       AdresTarifiKisa = "��FTL�KK�Y A�LE SA�LI�I MERKEZ� KAR�ISI A101 YANI",
             //       AdresTarifi ="��FTL�KK�Y A�LE SA�LI�I MERKEZ� KAR�ISI A101 YANI"
             //   },

                new Eczane { Id = 608,Enlem = 36.8178860,Boylam = 34.6315580,TelefonNo = "3242344480",Adres = "G�NDOGDU MAH.5790 SK.NO:17 AKDENIZ/MERSIN",AdresTarifiKisa = "MERS�N FEN L�SES� YANI, 1 NO LU SA�LIK OCA�I KAR�I",AdresTarifi ="G�NDO�DU MAH,KURDAL� YOLU,MERS�N FEN L�SES� YANI, 1 NO LU SA�LIK OCA�I KAR�ISI/AKDEN�Z"},
new Eczane { Id = 575,Enlem = 36.8097244,Boylam = 34.6222612,TelefonNo = "3243371489",Adres = "NUSRATIYE MAH.5026 SK. NO:44 AKDENIZ/MERSIN",AdresTarifiKisa = "ESK� DEVLET HASTANES� ARKA SOKA�I ",AdresTarifi ="ESK� DEVLET HASTANES� ARKA SOKA�I/AKDEN�Z"},
new Eczane { Id = 500,Enlem = 36.8361900,Boylam = 34.6107400,TelefonNo = "3242241600",Adres = "�AGDASKENT MAH. ERZ�NCANLILAR S�TES� C�VARI VEREM SAVA� D�SPANSER� KAR�ISI TOROSLAR-MERS�N",AdresTarifiKisa = "�A�DA�KENT MAH. S�LEYMAN G�K ASM KAR�ISI ",AdresTarifi ="�A�DA�KENT MAH. S�LEYMAN G�K ASM KAR�ISI ERZ�NCANLILAR S�TES� YANI/TOROSLAR"},
new Eczane { Id = 661,Enlem = 36.7823977,Boylam = 34.5880877,TelefonNo = "3243254600",Adres = "G�VENLER MH. 1914 SK. DERSANELER SOKA�I ARKASI - GMK BULVARI GROSER� MARKET ARKASI ",AdresTarifiKisa = " G�VENEVLER MH. 1914 SK. 5/AB NO:20",AdresTarifi =" G�VENEVLER MH. 1914 SK. 5/AB NO:20"},
new Eczane { Id = 726,Enlem = 36.7652420,Boylam = 34.5472450,TelefonNo = "3243585008",Adres = "ATAT�RK MH. 31045 SK. NO:23",AdresTarifiKisa = "Ortado�u Hastanesi Civar�",AdresTarifi ="Ortado�u Hastanesi Civar� Mezitli Belediye �lk��retim Okulu kar��s� �niversite caddesi civar�/MEZ�TL�"},
new Eczane { Id = 551,Enlem = 36.8053340,Boylam = 34.6337520,TelefonNo = "3243365144",Adres = "YEN� MH. 5328 SK. NO:5/C",AdresTarifiKisa = "TOROS DEVLET HASTANES� AC�L KAR�ISI",AdresTarifi ="TOROS DEVLET HASTANES� AC�L KAR�ISI"},
new Eczane { Id = 609,Enlem = 36.8222380,Boylam = 34.6375961,TelefonNo = "3242344445",Adres = "G�NE� MH. 5822 SK. NO:11/B",AdresTarifiKisa = "G�NE� MAH. 5822 SK. SELAHATT�N EYY�B� ASM YANI",AdresTarifi ="G�NE� MAH. 5822 SK. SELAHATT�N EYY�B� ASM YANI/AKDEN�Z"},
new Eczane { Id = 699,Enlem = 36.7797360,Boylam = 34.5566010,TelefonNo = "3242904494",Adres = "BATIKENT MH. YA�MUR SULTAN KONUTLARI NO:57/AB",AdresTarifiKisa = "BATIKENT MH. �AH�N TEPES�  NECAT� BOLKAN OKULU YAN",AdresTarifi ="BATIKENT MH. �AH�N TEPES�  NECAT� BOLKAN �.�.OKULU YANI �L �ZEL �DARES� KUZEY�  YEN��EH�R 3 NOLU ASM C�VARI/YEN��EH�R"},
new Eczane { Id = 727,Enlem = 36.7635190,Boylam = 34.5481180,TelefonNo = "3243575957",Adres = "ATAT�RK MAH.35.SK.NO:24/B MEZITLI/MERSIN",AdresTarifiKisa = "MERS�N ORTADO�U HASTANES� AC�L YANI/MEZ�TL�",AdresTarifi ="�N�VERS�TE YOLU �ZEL MERS�N ORTADO�U HASTANES� AC�L YANI/MEZ�TL�"},
new Eczane { Id = 728,Enlem = 36.7572490,Boylam = 34.5350140,TelefonNo = "3243599990",Adres = "MEZITLI IL�ESI YENI MH.33180 SK.NO:27/AMERSIN",AdresTarifiKisa = "YEN� MH. KALE YOLU ASM 1 NOLU SA�LIK OCA�I KAR�ISI",AdresTarifi ="YEN� MH. KALE YOLU ASM (1 NOLU SA�LIK OCA�I KAR�ISI) MEZ�TL�/MEZ�TL�"},
new Eczane { Id = 662,Enlem = 36.7840490,Boylam = 34.5856750,TelefonNo = "3243260990",Adres = "G�venevler Mh. Dumlup�nar Cd. No:36/A",AdresTarifiKisa = "FORUM AVM C�VARI YEN��EH�R �TFA�YE 100 MT. KUZEY� ",AdresTarifi ="FORUM AVM C�VARI YEN��EH�R �TFA�YE 100 MT. KUZEY� M�RE� MARKET KAR�ISI/YEN��EH�R"},
new Eczane { Id = 756,Enlem = 36.7401684,Boylam = 34.5261162,TelefonNo = "3243583037",Adres = "MENDERES MAHALLESI CEVAT KUTLU NO.15/A MEZITLI MERSIN",AdresTarifiKisa = "DEN�ZHAN-2 KAR�ISI, NAF�Z �OLAK SA�LIK OCA�I KAR�I",AdresTarifi ="DEN�ZHAN-2 KAR�ISI, NAF�Z �OLAK SA�LIK OCA�I KAR�ISI/MEZ�TL�"},
new Eczane { Id = 700,Enlem = 36.7706280,Boylam = 34.5620680,TelefonNo = "3243416690",Adres = "EGRI�AM MAHALLESI GMK BULVARI.NO.3/C YENISEHIR MERSIN",AdresTarifiKisa = "GMK BULV.DS� YANI SULTA�A OTEL C�VARI.",AdresTarifi ="GMK BULV.E�R��AM MH.DS� YANI SULTA�A OTEL C�VARI."},
new Eczane { Id = 663,Enlem = 36.7862290,Boylam = 34.5932560,TelefonNo = "3243278020",Adres = "G�VENEVLER MAH.1.CAD.NO:112/3 YENISEHIR/MERSIN",AdresTarifiKisa = "G�VENEVLER MH. 1.CD. POZCU �ET�NKAYA B�T�����/YEN�",AdresTarifi ="G�VENEVLER MH. 1.CD. POZCU �ET�NKAYA B�T�����/YEN��EH�R"},
new Eczane { Id = 576,Enlem = 36.8106930,Boylam = 34.6243060,TelefonNo = "3243371421",Adres = "NUSRATIYE MAH.5021 SOK.NO.38.AKDENIZ",AdresTarifiKisa = "NUSRAT�YE MH ANA �OCUK SA�LI�I KAR�ISI ",AdresTarifi ="NUSRAT�YE MH ANA �OCUK SA�LI�I KAR�ISI 23 EVLER KAV�A�I/AKDEN�Z"},
new Eczane { Id = 623,Enlem = 36.7950930,Boylam = 34.6235790,TelefonNo = "3242336845",Adres = "Kiremithane Mh. 4406 Sk. No:4",AdresTarifiKisa = " �ZEL DO�U� HASTANES� YANI/AKDEN�Z",AdresTarifi ="�ST�KLAL CD. �ZG�R �OCUK PARKI ARKASI �ZEL DO�U� HASTANES� YANI/AKDEN�Z"},
new Eczane { Id = 703,Enlem = 36.7781860,Boylam = 34.5340880,TelefonNo = "3243364250",Adres = "��FTL�KK�Y MH. �N�VERS�TE CD. 81 SK. NO: 12/A",AdresTarifiKisa = " �N�VERS�TE CADDES� KAMP�S  G�R�� YANI",AdresTarifi ="��FTL�KK�Y MAH. �N�VERS�TE CADDES� KAMP�S G�R�� KAPISI �N�/YEN��EH�R"},
new Eczane { Id = 625,Enlem = 36.7999120,Boylam = 34.6199050,TelefonNo = "3243371353",Adres = "BAH�E MAH.SOGUKSU 124 CAD.125/D AKDENIZ-MERSIN",AdresTarifiKisa = "BURHANFELEK CD ERC�YES TAKS� DURA�I/AKDEN�Z",AdresTarifi ="BURHANFELEK CD ERC�YES TAKS� DURA�I/AKDEN�Z"},
new Eczane { Id = 664,Enlem = 36.7896146,Boylam = 34.5870556,TelefonNo = "3243311407",Adres = "G�VENEVLER MH.20.CAD.NO:19/C",AdresTarifiKisa = "FORUM YA�AM HAS. KAR�ISI",AdresTarifi ="FORUM YA�AM HASTANES� KAR�ISI G�VENEVLER MH. 20 CD. NO:19/C/YEN��EH�R"},
new Eczane { Id = 610,Enlem = 36.8238940,Boylam = 34.6542330,TelefonNo = "3242352726",Adres = "HAL MAH.6025 SK.NO:39 8 NOLU SAG.OC.YANI AKDENIZ IL�ESI MERSIN",AdresTarifiKisa = "HAL MAH. 6025 SK. NO:39 8 NOLU SA�LIK OCA�I B�T���",AdresTarifi ="HAL MAH. 6025 SK. NO:39 8 NOLU SA�LIK OCA�I B�T�����/AKDEN�Z"},
new Eczane { Id = 757,Enlem = 36.7151070,Boylam = 34.4770510,TelefonNo = "3244815448",Adres = "SILIFKE   CAD.NO:86/DAVULTEPE MEZITLI-MERSIN",AdresTarifiKisa = "DAVULTEPE SA�LIK OCA�I KAR�ISI - DAVULTEPE",AdresTarifi ="GMK BULV. KAZIM KARABEK�R CD. 1110-A DAVULTEPE SA�LIK OCA�I KAR�ISI - DAVULTEPE"},
new Eczane { Id = 758,Enlem = 36.7406600,Boylam = 34.5256200,TelefonNo = "3243571142",Adres = "MENDERES MAHALLSI CEVAT KUTLU CADDESI N0.15/D MEZITLI/MERSIN",AdresTarifiKisa = "NAF�Z �OLAK SA�LIK OCA�I KAR�ISI ",AdresTarifi ="NAF�Z �OLAK SA�LIK OCA�I KAR�ISI FLORYA L�FE EVLER� ALTI/MEZ�TL�"},
new Eczane { Id = 520,Enlem = 36.8376547,Boylam = 34.5975522,TelefonNo = "3243243042",Adres = "�UKUROVA MAH.85119 SK.NO:130/A TOROSLAR-MERSIN",AdresTarifiKisa = "�UKUROVA MH.  �UKUROVA SA�LIK OCA�I KAR�ISI",AdresTarifi ="�UKUROVA MAH 85119 SK NO 130 �UKUROVA SA�LIK OCA�I KAR�ISI/TOROSLAR"},
new Eczane { Id = 637,Enlem = 36.7835680,Boylam = 34.6038540,TelefonNo = "3243270492",Adres = "GAZI MAH.1304 SK.INCI SIT.12/C YENISEHIR MERSIN",AdresTarifiKisa = " MU�DAT CAM�� DO�USU B�M ARKASI GAZ� ASM KAR�ISI",AdresTarifi ="MU�DAT CAM�� DO�USU B�M MARKET ARKASI GAZ� A�LE SA�LI�I MERKEZ� KAR�ISI POZCU/YEN��EH�R"},
new Eczane { Id = 521,Enlem = 36.8192690,Boylam = 34.6148440,TelefonNo = "3243202026",Adres = "TOZKOPARAN MH. 87045 SK. NO:19/A",AdresTarifiKisa = " TOZKOPARAN SA�LIK OCA�I KAR�ISI TOROSLAR",AdresTarifi ="TOZKOPARAN MAH 87045 SOK. TOZKOPARAN SA�LIK OCA�I KAR�ISI (PALM�YE D���N SALONU KAR�ISI)/TOROSLAR"},
new Eczane { Id = 730,Enlem = 36.7561180,Boylam = 34.5377100,TelefonNo = "3243592200",Adres = "MEZITLI IL�ESI YENI MAH.KELVELI CD. ZIYA �NSAL APT. A BLOK ALTI. NO:3/D             MERSIN",AdresTarifiKisa = "YEN� MH. KELVEL� CD. MEZ�TL� SEMT POL�K. KAR�ISI",AdresTarifi ="YEN� MH. KELVEL� CD. Z�YA �NSAL APT A BLOK - KALE YOLU - TOROS DEVLET HAST. MEZ�TL� SEMT POL�KL�N��� KAR�ISI   MEZ�TL�/MEZ�TL�"},
new Eczane { Id = 522,Enlem = 36.8081280,Boylam = 34.6121810,TelefonNo = "3243204178",Adres = "TURGUT T�RKALP MH.79028SK.NO:6 TOROSLAR/MERSIN",AdresTarifiKisa = "TURGUT T�RKALP MAH 10 NOLU SA�LIK OCA�I YANI",AdresTarifi ="TURGUT T�RKALP MAH 10 NOLU SA�LIK OCA�I YANI/TOROSLAR"},
new Eczane { Id = 731,Enlem = 36.7577930,Boylam = 34.5426580,TelefonNo = "3243592666",Adres = "BABIL KAVSAGI MEZITLI/MERSIN",AdresTarifiKisa = "GMK.BLV.BAB�L KAV�A�I SANAY� G�R���/MEZ�TL�",AdresTarifi ="GMK.BLV.BAB�L KAV�A�I SANAY� G�R���/MEZ�TL�"},
new Eczane { Id = 704,Enlem = 36.7711900,Boylam = 34.5627800,TelefonNo = "3243414101",Adres = "E�ri�am Mh. GMK Bulv. No:564",AdresTarifiKisa = "GMK BULV. UZMAN ATA TIP MERKEZ� KAR�ISI ",AdresTarifi ="E�R��AM MH. GMK BULV. UZMAN ATA TIP MERKEZ� KAR�ISI SULTA�A OTEL� KAR�ISI/YEN��EH�R"},
new Eczane { Id = 552,Enlem = 36.8062000,Boylam = 34.6359500,TelefonNo = "3242376797",Adres = "YENI MH.5314SK.G�ND�Z APT.ALTI NO:2/A AKDENIZ/MERSIN",AdresTarifiKisa = "TOROS DEVLET HASTANES� KAR�ISI ( ESK� SSK )/AKDEN�",AdresTarifi ="TOROS DEVLET HASTANES� KAR�ISI ( ESK� SSK )/AKDEN�Z"},
new Eczane { Id = 611,Enlem = 36.8149150,Boylam = 34.6384100,TelefonNo = "3242343311",Adres = "MERSIN ILI AKDENIZ IL�ESI SITELER MH. 5651 SK. NO:8 MERSIN",AdresTarifiKisa = "S�TELER MAH MUHTARLI�I ARKASI �EH�T CENG�Z KIR ASM",AdresTarifi ="S�TELER MH. S�TELER MAHALLE MUHTARLI�I ARKASI �EH�T CENG�Z KIR ASM KAR�ISI/AKDEN�Z"},
new Eczane { Id = 638,Enlem = 36.7901034,Boylam = 34.6022923,TelefonNo = "3243290055",Adres = "YENISEHIR IL�ESI CUMHURIYET MH.ISMET IN�N� BLV.93/B",AdresTarifiKisa = "ESK� G�LERY�Z PAST-��LEK MOB�LYA �ARPRAZ KAR�ISI",AdresTarifi ="ESK� G�LERY�Z PASTANES� 100 MT G�NEY� ��LEK MOB�LYA �APRAZ KAR�ISI YEN��EH�R"},
new Eczane { Id = 577,Enlem = 36.8103850,Boylam = 34.6274350,TelefonNo = "3243369032",Adres = "NUSRAT�YE MH 5004 SK NO:5",AdresTarifiKisa = "NUSRAT�YE MH D�ABET HASTANES� YANI ",AdresTarifi ="NUSRAT�YE MH D�ABET HASTANES� YANI ATAT�RK L�SES� ARKASI/AKDEN�Z"},
new Eczane { Id = 666,Enlem = 36.779475,Boylam = 34.5749355,TelefonNo = "3243255740",Adres = "BARBAROS MH. G��MEN SEMT�  2148 SOK. NO:22/A YENISEHIR/MERSIN",AdresTarifiKisa = " YEN��EH�R KAYMAKAMLI�I KAR�.�OCUK PARKI ARKASI.",AdresTarifi =" BARBAROS MAH.YEN��EH�R KAYMAKAMLI�I KAR�.�OCUK  PARKI ARKASI SA�LIK OCA�I KAR�ISI G��MENK�Y/MERS�N"},
new Eczane { Id = 578,Enlem = 36.8053090,Boylam = 34.6297930,TelefonNo = "3242312745",Adres = "�AKMAK CAD.NO:51/B-AKDENIZ/MERSIN",AdresTarifiKisa = "�AKMAK CD YAPI KRED� BANK VE HASIRCI CAM� ARASI",AdresTarifi ="�AKMAK CADDES� YAPI KRED� BANKASI VE HASIRCI CAM� ARASI SEMT PAZARI YANI/AKDEN�Z"},
new Eczane { Id = 736,Enlem = 36.7568890,Boylam = 34.5351060,TelefonNo = "3243595474",Adres = " YENI MH.Y�KSEK HARMAN CD. 180 SK. AKKOYUNCU AP. NO:12 MEZ�TL",AdresTarifiKisa = "",AdresTarifi ="MEZ�TL� K�TAPSAN (300 MT YUKARISI) KALEYOLU SA�LIK OCA�I KAR�ISI/MEZ�TL�"},
new Eczane { Id = 737,Enlem = 36.7541709,Boylam =  34.544972,TelefonNo = "3243599733",Adres = "FATIH MAH.BABIL CAD. ARTEMIS SITESI NO:27 MEZITLI/MERSIN",AdresTarifiKisa = "FAT�H MH. BAB�L CD.MEZ�TL� MADO KAR�ISI/MEZ�TL�",AdresTarifi ="FAT�H MH. BAB�L CD.MEZ�TL� MADO KAR�ISI/MEZ�TL�"},
new Eczane { Id = 523,Enlem = 36.8260160,Boylam = 34.6227100,TelefonNo = "3243206383",Adres = "KURDAL� MH. MERS�NL� AHMET BULV. NO:63",AdresTarifiKisa = "1 NOLU ASM YANI /TOROSLAR",AdresTarifi ="KURDAL� MH.MERS�NL� AHMET CD.NO:63 TOROSLAR 1 NOLU ASM YANI /TOROSLAR"},
new Eczane { Id = 667,Enlem = 36.7807260,Boylam = 34.5849790,TelefonNo = "3243294060",Adres = "Ayd�nl�kevler Mh. 2001 Sk. No:18/B �lyas Apt. Alt�  Pozcu /YEN��EH�R",AdresTarifiKisa = "POZCU ADESE KAR�ISI B�M MARKET YANI",AdresTarifi ="POZCU ADESE KAR�ISI B�M MARKET YANI"},
new Eczane { Id = 579,Enlem = 36.8044300,Boylam = 34.6237100,TelefonNo = "3243371017",Adres = "IHSANIYE MH. KUVAAI MILLIYE CD. AKARSU PLAZA NO:6",AdresTarifiKisa = "HASTANE CD. AKDEN�Z BELED�YES� ARKASI ",AdresTarifi ="HASTANE CD. AKDEN�Z BELED�YES� ARKASI Z�RAAT BANKASI KURU�E�ME �UBES� KAR�ISI /AKDEN�Z"},
new Eczane { Id = 553,Enlem = 36.8064457,Boylam = 34.6356586,TelefonNo = "3243366620",Adres = "YEN� MH. 5314 SK. NO:4",AdresTarifiKisa = "TOROS DEVLET HASTANES� (ESK� SSK) KAR�ISI",AdresTarifi ="TOROS DEVLET HASTANES� (ESK� SSK) KAR�ISI"},
new Eczane { Id = 639,Enlem = 36.7892857,Boylam = 34.6065127,TelefonNo = "3243266662",Adres = "CUMHUR�YET MH. 1628 SK. NO: 6/A-B",AdresTarifiKisa = "P�R�RE�S A�LE SA�LI�I MERKEZ� KAR�ISI YEN��EH�R",AdresTarifi ="�SMET �N�N� BULV.YURT ��� KARGO-AB�D�N TANTUN� ARKA SOKA�I P�R�RE�S A�LE SA�LI�I MERKEZ� KAR�ISI"},
new Eczane { Id = 640,Enlem = 36.784282,Boylam = 34.5981303,TelefonNo = "3243257130",Adres = "GMK BULV. NO:330/A TUBA APT. ALTI",AdresTarifiKisa = "GMK BLV.T�RK TELEKOM BATISI DOM�NOS P�ZZA  KAR�ISI",AdresTarifi ="GMK BULVARI T�RK TELEKOM BATISI DOM�NOS P�ZZA KAR�ISI POZCU/YEN��EH�R"},
new Eczane { Id = 759,Enlem = 36.7499690,Boylam = 34.5197960,TelefonNo = "3243571992",Adres = "Merkez Mh. 52081 Sk. Lider Konutlar� Alt� No:5/A",AdresTarifiKisa = "KUYULUK YOLU - B�M MARKET ARKASI",AdresTarifi ="KUYULUK YOLU - B�M MARKET ARKASI L�DER KONUTLARI ALTI OSMANGAZ� ASM C�VARI - MEZ�TL�"},
new Eczane { Id = 628,Enlem = 36.7959509,Boylam = 34.6266830,TelefonNo = "3242324168",Adres = "SILIFKE CAD.BAHRI OK ISHANI NO:51/B AKDENIZ/MERSIN",AdresTarifiKisa = "S�L�FKE CD ESK� ��RETMENEV� KAR�ISI/AKDEN�Z",AdresTarifi ="S�L�FKE CD ESK� ��RETMENEV� KAR�ISI/AKDEN�Z"},
new Eczane { Id = 580,Enlem = 36.8069150,Boylam = 34.6179420,TelefonNo = "3243361533",Adres = "�HSAN�YE MAH.SA�T ��FT�� CAD.NO:36 �ZEL SU HASTANES� AC�L KAR�ISI AKDEN�Z/MERS�N",AdresTarifiKisa = "�ZEL SU HASTANES� AC�L KAR�ISI AKDEN�Z",AdresTarifi ="�ZEL SU HASTANES� AC�L KAR�ISI AKDEN�Z/MERS�N"},
new Eczane { Id = 502,Enlem = 36.8357010,Boylam = 34.6110080,TelefonNo = "3242233636",Adres = "MERSIN ILI TOROSLAR IL�ESI �AGDASKENT MH. 229 CD.10/C",AdresTarifiKisa = "ERZ�NCANLILAR S�T KAR�ISI S�LEYMAN G�K ASM C�VARI",AdresTarifi ="�A�DA�KENT MH. 229. CD. ERZ�NCANLILAR S�TES� KAR�ISI S�LEYMAN G�K ASM C�VARI/TOROSLAR"},
new Eczane { Id = 760,Enlem = 36.7489010,Boylam = 34.5292870,TelefonNo = "3243574585",Adres = "MERKEZ MAH.GMK BUL.MEZITLI �ARS.C BLOK NO:192 MEZITLI/MERSIN",AdresTarifiKisa = "MEZ�TL� BLD.�APRAZ KAR�ISI 2 NOLU SA�LIK OCA�I YAN",AdresTarifi ="MEZ�TL� BLD.�APRAZ KAR�ISI 2 NOLU SA�LIK OCA�I YANI/MEZ�TL�"},
new Eczane { Id = 668,Enlem = 36.7936810,Boylam = 34.5844320,TelefonNo = "3243287747",Adres = "L�MONLUK MH. KAN�IRAY APT. ZEM�NKAT NO: 48/A",AdresTarifiKisa = "CEMEV� KAR�ISI M�F�DE �LHAN �LK��RET�M OKULU YANI",AdresTarifi ="L�MONLUK MH. 9. CD. CEMEV� KAR�ISI M�F�DE �LHAN �LK��RET�M OKULU YANI/YEN��EH�R"},
new Eczane { Id = 503,Enlem = 36.8320303,Boylam = 34.6139739,TelefonNo = "3242240102",Adres = "GAZ� OSMAN PA�A CAD. 93016 SK. NO:16/A",AdresTarifiKisa = " GAZ� OSMAN PA�A CAD. EMEK S�T. YANI /TOROSLAR",AdresTarifi ="GAZ� OSMAN PA�A 93003 SK. CELLO �E�MES� BATISI EMEK S�TES� YANI GAZ� OSMAN PA�A ASM KAR�ISI /TOROSLAR"},
new Eczane { Id = 554,Enlem = 36.8045130,Boylam = 34.6326180,TelefonNo = "3242373799",Adres = "YENI MAH.CEMALPASA CAD. 74/A-AKDENIZ",AdresTarifiKisa = "TOROS DEVLET HASTANES� TREN �STASYONU ARASI",AdresTarifi ="MERS�N TOROS DEVLET HASTANES�NDEN TREN �STASYONUNA G�DEN G�ZERGAHTA SA�DA PETROL OF�S� KAR�ISI"},
new Eczane { Id = 524,Enlem = 36.8379040,Boylam = 34.5987590,TelefonNo = "3243244060",Adres = "�ukurova Mh. 85119 Sk. No:134 A1-2",AdresTarifiKisa = "�UKUROVA SA�LIK OCA�I KAR�ISI/TOROSLAR",AdresTarifi ="�UKUROVA MH. 85119 SK.�UKUROVA SA�LIK OCA�I KAR�ISI/TOROSLAR"},
new Eczane { Id = 525,Enlem = 36.8249017,Boylam = 34.6008625,TelefonNo = "3243413434",Adres = "AKBELEN MH. TOROS CD. NO:6 TOROSLAR",AdresTarifiKisa = "AKBELEN SA�LIK OCA�I KAR�ISI ",AdresTarifi ="AKBELEN MH. AKBELEN SA�LIK OCA�I KAR�ISI KORAY AYDIN STADI ALTI/TOROSLAR"},
new Eczane { Id = 641,Enlem = 36.7939650,Boylam = 34.6009070,TelefonNo = "3243287355",Adres = "H�RRIYET MAHALLESI 1742 SOKAK NO.31 YENISEHIR.MERSIN",AdresTarifiKisa = " CARREFOUR YOLU �ZER� M�GROS MARKET ARKASI ",AdresTarifi ="H�RR�YET MH CARREFOUR YOLU �ZER� M�GROS MARKET ARKASI SA�LIK OC. KR�/YEN��EH�R"},
new Eczane { Id = 629,Enlem = 36.7926482,Boylam = 34.6238667,TelefonNo = "3242379310",Adres = "K�LT�R MH.4303 SK.�AMLIBEL AP.23/D AKDENIZ MERSIN",AdresTarifiKisa = "S�STEM TIP AC�L G�R��� KAR�ISI/AKDEN�Z",AdresTarifi ="K�LT�R MH. 4303 SK. 23/D  S�STEM TIP AC�L G�R��� KAR�ISI/AKDEN�Z"},
new Eczane { Id = 738,Enlem = 36.7527600,Boylam = 34.5357940,TelefonNo = "3243291918",Adres = "GMK BULVARI G�NCEL AP. ALTI 675 /A NO:9 MEZ�TL� OPET VE MC DONALT KAR�ISI MC�TY HOSP�TAL YANI",AdresTarifiKisa = "GMK BUL. OPET KAR�ISI M C�TY HOSP�TAL YANI",AdresTarifi ="GMK BULVARI G�NCEL AP. ALTI 675 /A NO:9 MEZ�TL� OPET VE MC DONALT KAR�ISI MC�TY HOSP�TAL YANI"},
new Eczane { Id = 761,Enlem = 36.7444300,Boylam = 34.5236880,TelefonNo = "3243582210",Adres = "MENDERES MH. GMK BULV. 35433 SK. T�GR�S 5 APT ALTI NO:1",AdresTarifiKisa = "MEZ�TL� GMK BULV. 11. NOTER YANI - LCW KAR�ISI",AdresTarifi ="GMK BULV. MEZ�TL�  11. NOTER YANI VE �ZEL MERS�N �OCUK HASTALIKLARI MERKEZ� YANI - LCW KAR�ISI MEZ�TL�"},
new Eczane { Id = 581,Enlem = 36.8009530,Boylam = 34.6269220,TelefonNo = "3242393722",Adres = "CAMI SERIF MAH.170.CAD. CEMALPASA CADDE NO.93/C. AKDENIZ",AdresTarifiKisa = "CAM��ER�F MH. EVKUR ALI�VER�� MERKEZ� YANI",AdresTarifi ="CAM��ER�F MH. FABR�KALAR CD. EVKUR ALI�VER�� MERKEZ� YANI / AKDEN�Z"},
new Eczane { Id = 706,Enlem = 36.7854140,Boylam = 34.5400070,TelefonNo = "3243363915",Adres = "M�MAR S�NAN CD. ��FTL�KK�Y MH. 32231 SK. NO:2D BLOK ZEM�N NO:4",AdresTarifiKisa = "MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�IS",AdresTarifi ="��FTL�KK�Y MH. M�MAR S�NAN CD. (MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI) YEN��EH�R"},
new Eczane { Id = 739,Enlem = 36.7567710,Boylam = 34.5376750,TelefonNo = "3243580664",Adres = "KALE YOLUZIRAATCILAR SITESI A.BLOKNO.1 MEZITLI",AdresTarifiKisa = "TOROS DEVLET HAST.SEMT POLKL�N��� KAR�ISI ",AdresTarifi ="TOROS DEVLET HAST.SEMT POLKL�N��� KAR�ISI (KALEYOLU)/MEZ�TL�"},
new Eczane { Id = 582,Enlem = 36.8105690,Boylam = 34.6244940,TelefonNo = "3243372588",Adres = "NUSRATITE MAH. M.AKIF ERSOY CAD.5021.SOK.NO.33/1 AKDENIZ .MERSIN",AdresTarifiKisa = "MERS�N DEVLET HASTANES� ESK� AC�L C�VARI",AdresTarifi ="MERS�N DEVLET HASTANES� ESK� AC�L C�VARI 23 EVLER KAV�A�I/AKDEN�Z"},
new Eczane { Id = 679,Enlem = 36.7891370,Boylam = 34.5881090,TelefonNo = "3249991151",Adres = "G�VENEVLER MH. 1949 SK. NO:14/A",AdresTarifiKisa = "FORUM YA�AM HASTANES� YANI",AdresTarifi =" G�VENEVLER MH. 1949 SK. NO:23 FORUM YA�AM HASTANES� YANI/YEN��EH�R"},
new Eczane { Id = 555,Enlem = 36.8053340,Boylam = 34.6337520,TelefonNo = "3243365152",Adres = "YEN�  MH. 5328 SK. NO:7/C",AdresTarifiKisa = "TOROS DEVLET HASTANES� AC�L  KAR�ISI ",AdresTarifi ="TOROS DEVLET HASTANES� AC�L  KAR�ISI "},
new Eczane { Id = 762,Enlem = 36.7493960,Boylam = 34.5288860,TelefonNo = "3243581741",Adres = "MERKEZ MAH.KARAOGLAN CAD. �ZT�RK APT NO/5A/5B MEZITLI",AdresTarifiKisa = "MEZ�TL� BELED�YES� KAR�ISI 2.NOLIK OCA�I YANI ",AdresTarifi ="MERKEZLU SA� MH. MEZ�TL� BELED�YES� KAR�ISI EKMEK FIRINLARI ARKASI 2.NOLIK OCA�I YANI MEZ�TL�/MEZ�TL�"},
new Eczane { Id = 642,Enlem = 36.7942510,Boylam = 34.6009570,TelefonNo = "3243292452",Adres = "H�RRIYET MH.1747SK.I.SINCAR APT.ALTI NO:6/17 YENISEHIR/MERSIN",AdresTarifiKisa = "CARREFOUR YOLU �ZER� H�RR�YET ASM YANI",AdresTarifi ="H�RR�YET MH. CARREFOUR YOLU �ZER� M�GROSMARKET ARKA SOKA�I H�RR�YET ASM YANI/YEN��EH�R"},
new Eczane { Id = 526,Enlem = 36.8195340,Boylam = 34.6153040,TelefonNo = "3243215850",Adres = "TOSKOPARAN MAH.87045.SOK NO.14.TOROSLAR",AdresTarifiKisa = " TOZKOPARAN SA�LIK OCA�I YANI/TOROSLAR",AdresTarifi ="TOZKOPARAN MAH 87045 SOK. NO 24 TOZKOPARAN SA�LIK OCA�I YANI/TOROSLAR"},
new Eczane { Id = 505,Enlem = 36.8425510,Boylam = 34.6235280,TelefonNo = "3242235252",Adres = "HALKKENT MH. FAT�H SULTAN MEHMET BULV.NO:25",AdresTarifiKisa = "ESK� KADIN DO�UM VE �OCUK HASTANES�  KAR�",AdresTarifi ="ESK� KADIN DO�UM VE �OCUK HASTANES�  KAR�ISI/TOROSLAR"},
new Eczane { Id = 669,Enlem = 36.7857040,Boylam = 34.5924850,TelefonNo = "3243285493",Adres = "G�VENEVLER MAH..CAD.CIHAN SAFAK APT.NO:27-2/B YENISEHIR",AdresTarifiKisa = "G�VEN S�TES� KAR�ISI  4 NOLU SA�LIK OCA�I KAR�ISI",AdresTarifi ="G�VENEVLER MAH.G�VEN S�TES� KAR�ISI FORUM DO�U TRF 1.CD. 4 NOLU SA�LIK OCA�I KAR�ISI/YEN��EH�R"},
new Eczane { Id = 603,Enlem = 36.8008300,Boylam = 34.6233490,TelefonNo = "3242372202",Adres = "MAHMUD�YE MAH. 4812 SOK. NO:28/A AKDEN�Z",AdresTarifiKisa = "MAHMUD�YE MH. END�STR� MESLEK L�SES� C�VARI",AdresTarifi ="NOBEL OTEL� KAR�I SOKA�I YEN� ADL�YEYE DO�RU END�STR� MESLEK L�SES� C�VARI"},
new Eczane { Id = 765,Enlem = 36.7492090,Boylam = 34.5292890,TelefonNo = "3243599385",Adres = "MERKEZ MH. 2030 SK.MEZITLI 2 NO LU SAGLIK OCAGI YANI MEZITLI/MERSIN",AdresTarifiKisa = "MEZ�TL� BELED�YES� KAR�ISI ALDA MARKET ARKASI ",AdresTarifi ="MEZ�TL� BELED�YES� KAR�ISI ALDA MARKET ARKASI MERKEZ MH. ASM YANI/MEZ�TL�"},
new Eczane { Id = 670,Enlem = 36.7832833,Boylam = 34.5920555,TelefonNo = "3243597755",Adres = "G�venevler Mh. 1908 Sk. Serpil Apt. Alt� No: 8/A   YEN��EH�R",AdresTarifiKisa = " FORUM AVM C�VARI �ZEL YEN��EH�R HASTANES� ARKASI",AdresTarifi =" FORUM AVM C�VARI �ZEL YEN��EH�R HASTANES� ARKASI - POZCU"},
new Eczane { Id = 713,Enlem = 36.7741661,Boylam = 34.5677681,TelefonNo = "3243270483",Adres = "EGRI�AM MAH. GMK BUL.NO:458 YENISEHIR MERSIN",AdresTarifiKisa = "GMK BULVARI VATAN B�LG�SAYAR - 48 ELEKTR�K  ARASI ",AdresTarifi ="GMK BULVARI E�R��AM MH VATAN B�LG�SAYAR - 48 ELEKTR�K ARASI/YEN��EH�R"},
new Eczane { Id = 643,Enlem = 36.7856220,Boylam = 34.6032450,TelefonNo = "3243273353",Adres = "GMK BULV. ALDO S�TES� 300/B",AdresTarifiKisa = "GMK BULV POZCU ET BALIK KURUMU KAR�ISI GRAT�S YANI",AdresTarifi ="GMK BULV. ANA CADDE �ZER� POZCU ET BALIK KURUMU KAR�ISI GRAT�S YANI YEN��EH�R"},
new Eczane { Id = 556,Enlem = 36.8073300,Boylam = 34.6351400,TelefonNo = "3242388215",Adres = "YEN� MH. 5314 SK. NO:12",AdresTarifiKisa = "TOROS DEVLET HASTANES� KAR�ISI ( ESK� SSK)/AKDEN�Z",AdresTarifi ="TOROS DEVLET HASTANES� KAR�ISI ( ESK� SSK)/AKDEN�Z"},
new Eczane { Id = 689,Enlem = 36.7834040,Boylam =  34.587422,TelefonNo = "3245020304",Adres = "G�VENEVLER  MAHALLES� 1.CADDE NO:9 YEN��EH�R/MERS�N",AdresTarifiKisa = "FORUM ALI�VER�� MERKEZ� �LE �TFA�YE ARASI",AdresTarifi ="FORUM ALI�VER�� MERKEZ� �LE �TFA�YE ARASI "},
new Eczane { Id = 707,Enlem = 36.7854310,Boylam = 34.5402370,TelefonNo = "3243372522",Adres = "MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI",AdresTarifiKisa = "MERS�N �N�VERS�TES� TIPFAK�LTES� HASTANES� KAR�ISI",AdresTarifi ="��FTL�KK�Y MH. M�MAR S�NAN CD. (MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI) /YEN��EH�R"},
new Eczane { Id = 644,Enlem = 36.7941148,Boylam = 34.600818,TelefonNo = "3243262599",Adres = "H�RRIYET MAHALLESI 1753 SOKAK NO.11/C YENISEHIR.MERSIN",AdresTarifiKisa = " B�Y�K CARREFOUR YOLU �ZER� M�GROS MARKET ARKASI",AdresTarifi ="H�RR�YET MH. B�Y�K CARREFOUR YOLU �ZER� M�GROS MARKET ARKA SOKA�I H�RR�YET ASM KAR�ISI/YEN��EH�R"},
new Eczane { Id = 740,Enlem = 36.7563370,Boylam = 34.5373640,TelefonNo = "3243595175",Adres = "KALEYOLU MEZ�TL� SEMT POL�KL�N��� KAR�ISI NO : 23/B YEN�MAHALLE / MEZ�TL� / MERS�N",AdresTarifiKisa = "KALE YOLU MEZ�TL� TOROS SEMT POL�KL�N��� KAR�ISI",AdresTarifi ="KALE YOLU MEZ�TL� TOROS SEMT POL�KL�N��� KAR�ISI/MEZ�TL�"},
new Eczane { Id = 557,Enlem = 36.8058940,Boylam = 34.6332380,TelefonNo = "3242335948",Adres = "YEN� MH. 5328 SK NO: 5/A",AdresTarifiKisa = "TOROS DEVLET HASTANES� AC�L KAPISI KAR�ISI ",AdresTarifi ="TOROS DEVLET HASTANES� AC�L KAPISI KAR�ISI YEN� MH. 5328 SK /AKDEN�Z"},
new Eczane { Id = 671,Enlem = 36.7823700,Boylam = 34.5954880,TelefonNo = "3243259717",Adres = "KUSH�MOTO SOKA�I POZCU AKBANK SOKA�I NO:17/B YEN��EH�R ",AdresTarifiKisa = "KUSH�MOTO SOKA�I POZCU AKBANK SOKA�I ",AdresTarifi ="KUSH�MOTO SOKA�I POZCU AKBANK SOKA�I NO:17/B YEN��EH�R "},
new Eczane { Id = 741,Enlem = 36.7635770,Boylam = 34.5487250,TelefonNo = "3243575524",Adres = "ATAT�RK MAHALLESI 35 SOKAK 23/E MEZITLI MERSIN",AdresTarifiKisa = "�N�V. YOLU �ZEL MERS�N ORTADO�U HASTANES� YANI",AdresTarifi ="�N�VERS�TE YOLU �ZEL MERS�N ORTADO�U HASTANES� YANI MEZ�TL�/MEZ�TL�"},
new Eczane { Id = 645,Enlem = 36.7878990,Boylam = 34.5987870,TelefonNo = "3243259805",Adres = "CUMHURIYET MH.16.CD.NO:22/B YENISEHIR/MERSIN",AdresTarifiKisa = "45 EVLER YOLU ADA ET�D KAR�ISI YEN��EH�R/MERS�N",AdresTarifi ="CUMHUR�YET MH 45 EVLER YOLU 16. CD NO 22/B VEN�S PASTANES� YANI/YEN��EH�R"},
new Eczane { Id = 583,Enlem = 36.8042160,Boylam = 34.6308310,TelefonNo = "3242315880",Adres = "MESUDIYE MAHALLESI �AKMAK CADDESI S.G.K. IL M�D.KARSISI NO 31/A AKDENIZ MERSIN",AdresTarifiKisa = "�AKMAK CADDES� BA� KUR KAR�ISI",AdresTarifi ="�AKMAK CADDES� BA� KUR KAR�ISI/AKDEN�Z"},
new Eczane { Id = 767,Enlem = 36.7601200,Boylam = 34.5119440,TelefonNo = "3243588451",Adres = "�ANKAYA MAHALLES� 202 SOK. NO: 17 / A KUYULUK/ MEZ�TL� /MERS�N",AdresTarifiKisa = "FINDIKPINARI CADDES� KUYULUK ASM KAR�ISI ",AdresTarifi ="FINDIKPINARI CADDES� KUYULUK ASM KAR�ISI KUYULUK/ MEZ�TL�/MERS�N"},
new Eczane { Id = 507,Enlem = 36.8456810,Boylam =  34.633806,TelefonNo = "3242292696",Adres = " YALINAYAK MH. ATAT�RK CD. NO:1/A",AdresTarifiKisa = "YALINAYAK SA�LIK OCA�I YANI/TOROSLAR",AdresTarifi ="YALINAYAK MH. ATAT�RK CAD. YALINAYAK SA�LIK OCA�I YANI/TOROSLAR"},
new Eczane { Id = 708,Enlem = 36.7860620,Boylam = 34.5397650,TelefonNo = "3242902206",Adres = "��FTL�KK�Y MH. M�MAR S�NAN CD. PARAD�SE HOMES C BLOK NO: 24 CG",AdresTarifiKisa = "MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�IS",AdresTarifi ="MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI/YEN��EH�R"},
new Eczane { Id = 613,Enlem = 36.8222380,Boylam = 34.6375960,TelefonNo = "3242357656",Adres = "G�NE� MAH. 5822 SOK. NO:5/A AKDEN�Z/MERS�N",AdresTarifiKisa = "G�NE� MH.SELAHATT�N EYYUB� SA�LIK OCA�I YANI ",AdresTarifi ="G�NE� MAH. SELAHATT�N EYYUB� SA�LIK OCA�I YANI KURDAL� CAM�S� ARKASI"},
new Eczane { Id = 647,Enlem = 36.7833990,Boylam = 34.6039080,TelefonNo = "3243260979",Adres = "GAZ� MH. 1304 SK. NO:10",AdresTarifiKisa = "MU�DAT CAM�� DO�USU GAZ� SA�LIK OCA�I KAR�ISI ",AdresTarifi ="MU�DAT CAM�� DO�USU B�M MARKET ARKASI GAZ� SA�LIK OCA�I KAR�ISI - POZCU/YEN��EH�R"},
new Eczane { Id = 604,Enlem = 36.7896080,Boylam = 34.6173200,TelefonNo = "3242388573",Adres = "HAM�D�YE MH. 4209 SK. NO:20/B",AdresTarifiKisa = "MERS�N HAMAMI SOKA�I HAM�D�YE ASM YANI ",AdresTarifi ="HAM�D�YE MH 4209 SK NO: 20/B MERS�N HAMAMI SOKA�I HAM�D�YE ASM YANI  YANI / AKDEN�Z"},
new Eczane { Id = 527,Enlem = 36.8240273,Boylam = 34.6036559,TelefonNo = "3243225650",Adres = "YUSUF KILI� MAH.217.CAD NO.76.TOROSLAR",AdresTarifiKisa = "AYI�I�I D���N SALONU KAR�ISI KAR�ISI AKBELEN",AdresTarifi ="YUSUF KILI� MH 217 CAD AYI�I�I D���N SALONU KAR�ISI AKBELEN/TOROSLAR"},
new Eczane { Id = 673,Enlem = 36.7857569,Boylam = 34.5930935,TelefonNo = "3243258804",Adres = "G�venevler Mh. 1. Cd. No:103/B",AdresTarifiKisa = " G�VEN S�TES� POZCU �ET�NKAYA KAR�ISI/YEN��EH�R",AdresTarifi ="G�VENEVLER MAH. 1 CAD. G�VEN S�TES� POZCU �ET�NKAYA KAR�ISI/YEN��EH�R"},
new Eczane { Id = 674,Enlem = 36.7788175,Boylam = 34.5751753,TelefonNo = "3243260767",Adres = "BARBAROS MH.2148SK.NO:22 YENISEHIR/MERSIN",AdresTarifiKisa = " YEN��EH�R KAYMAKAMLI�I KAR�ISI. ",AdresTarifi ="BARBAROS MAH. YEN��EH�R KAYMAKAMLI�I KAR�ISI. BARBAROS �OCUK PARKI ARKASI. 15 NOLU SA�LIK OCA�I KAR�ISI/YEN��EH�R"},
new Eczane { Id = 584,Enlem = 36.8090630,Boylam = 34.6210450,TelefonNo = "3243366180",Adres = "IHSANIYE MAH.SALT �IFCI CAD.113.CAD.4903 SOK.NO.5 AKDENIZ",AdresTarifiKisa = "ESK� DEVLET HASTANES� KAR�ISI ARA SOKAK/AKDEN�Z",AdresTarifi ="ESK� DEVLET HASTANES� KAR�ISI ARA SOKAK/AKDEN�Z"},
new Eczane { Id = 649,Enlem = 36.7937800,Boylam = 34.6011220,TelefonNo = "3243276507",Adres = "H�RR�YET MH. 1742 SK",AdresTarifiKisa = "CARREFOUR YOLU �ZER� M�GROS MARKET ARKA SOKA�I ",AdresTarifi ="H�RR�YET MH. 1742 SK. CARREFOUR YOLU �ZER� M�GROS MARKET ARKA SOKA�I 11 NOLU SA�LIK OCA�I KAR�ISI/YEN��EH�R"},
new Eczane { Id = 606,Enlem = 36.7951053,Boylam = 34.6231860,TelefonNo = "3242330101",Adres = "Kiremithane Mh. 4409 Sk. No: 1/D-E-F / AKDEN�Z",AdresTarifiKisa = "�ZEL DO�U� HASTANES� YEN� B�NA YANI ",AdresTarifi ="K�REM�THANE MH. 4409 SK. �ZEL DO�U� HASTANES� YEN� B�NA YANI - AKDEN�Z"},
new Eczane { Id = 650,Enlem = 36.7868565,Boylam = 34.6099304,TelefonNo = "3243283220",Adres = "PIRIREIS MH.SILIFKE CD.NO:22/A YENISEHIR/MERSIN",AdresTarifiKisa = "P�R�RE�S MH. KAPALI SPOR SAL. ARK.BATTI �IKTI  C�V",AdresTarifi ="P�R�RE�S MAH. KAPALI SPOR SALONU ARKASI S�L�FKE CAD. SONU TULUMBA DURA�I BATTI �IKTIYA G�RMEDEN SA�  YOLUN SONU "},
new Eczane { Id = 768,Enlem = 36.7324059,Boylam = 34.5073086,TelefonNo = "3243582896",Adres = "AKDEN�Z MH. CUMHUR�YET CD. MERS�NE PARK 3 APT. ALTI NO:12/F  MEZ�TL�",AdresTarifiKisa = "YA�ARDO�U CD-CUMHUR�YET CD. KAV�A�I  ",AdresTarifi ="AKDEN�Z MH. CUMHUR�YET CD.- YA�ARDO�U CD. KAV�A�I �OK MARKET KAR�ISI MERS�NE PARK 3 APT. ALTI   /MEZ�TL�"},
new Eczane { Id = 675,Enlem = 36.7856610,Boylam = 34.5923670,TelefonNo = "3243294406",Adres = "G�VENEVLER MAH 1.CAD.113/A YENISEHIR",AdresTarifiKisa = "G�VENEVLER MAH 1. CAD. G�VEN S�TES� KAR�ISI/YEN��E",AdresTarifi ="G�VENEVLER MAH 1. CAD. G�VEN S�TES� KAR�ISI/YEN��EH�R"},
new Eczane { Id = 742,Enlem = 36.7654070,Boylam = 34.5472560,TelefonNo = "3242215186",Adres = "ATAT�RK MH. 31045 SK. NO:18/A",AdresTarifiKisa = " �N�VERS�TE YOLU �ZEL ORTADO�U HASTANES� C�VARI ",AdresTarifi =" �N�VERS�TE YOLU �ZEL ORTADO�U HASTANES� C�VARI MEZ�TL� BELED�YE �LK ��RET�M OKULU YANI  MEZ�TL�"},
new Eczane { Id = 528,Enlem = 36.8512597,Boylam = 34.6072272,TelefonNo = "3243211203",Adres = "KORUKENT MH. 96004SK. NO:17/4 (B)",AdresTarifiKisa = "�EH�R HASTANES� YANI KORUKENT MAH. 96004 SOK. NO: ",AdresTarifi ="�EH�R HASTANES� YANI KORUKENT MAH. 96004 SOK. NO: 17/A"},
new Eczane { Id = 631,Enlem = 36.8176090,Boylam = 34.6315660,TelefonNo = "3242359602",Adres = "G�NDOGDU MAH.5790 SK.NO:15 AKDENIZ IL�ESI-MERSIN",AdresTarifiKisa = "G�NDO�DU MAH  1 NOLU SA�LIK OCA�I YANI/AKDEN�Z",AdresTarifi ="G�NDO�DU MAH 5790 SOK. NO 12/A 1 NOLU SA�LIK OCA�I YANI/AKDEN�Z"},
new Eczane { Id = 585,Enlem = 36.8089900,Boylam = 34.6211470,TelefonNo = "3243375235",Adres = "IHSANIYE MAHALLESI KUVAIMILIYE CADDESI .NO.173 AKDENIZ",AdresTarifiKisa = "ESK� DEVLET HASTANES� KAR�ISI/AKDEN�Z",AdresTarifi ="ESK� DEVLET HASTANES� KAR�ISI/AKDEN�Z"},
new Eczane { Id = 529,Enlem = 36.8264900,Boylam = 34.6254140,TelefonNo = "3243202177",Adres = "Kurdali Mh. 100075 sk. No:33/A",AdresTarifiKisa = "SELAHATT�N YANPAR ASM KAR�ISI ",AdresTarifi ="SELAHATT�N YANPAR ASM KAR�ISI  KURDAL� MAH. 100075 SK. NO:33 TOROSLAR/MERS�N"},
new Eczane { Id = 632,Enlem = 36.8257230,Boylam = 34.6418280,TelefonNo = "3242355600",Adres = "SEVKET S�MER MAH.156.CAD.NO:27AKDENIZ/MERSIN",AdresTarifiKisa = "�EVKET S�MER MAH 156 CAD S�TELER KARAKOLU KAR�ISI/",AdresTarifi ="�EVKET S�MER MAH 156 CAD S�TELER KARAKOLU KAR�ISI/AKDEN�Z"},
new Eczane { Id = 769,Enlem = 36.7404360,Boylam = 34.5260331,TelefonNo = "3243583818",Adres = "MENDERES MH. CEVAT KUTLU CD. 35415 SK. NO:29/B",AdresTarifiKisa = "NAF�Z �OLAK SA�LIK OCA�I C�VARI MEZ�TL�",AdresTarifi ="DEN�ZHAN-2 KAR�ISINDAK� CADDE �ZER�, NAF�Z �OLAK SA�LIK OCA�I C�VARI MEZ�TL�/MEZ�TL�"},
new Eczane { Id = 677,Enlem = 36.7844320,Boylam = 34.5958580,TelefonNo = "3243283549",Adres = "G�VENEVLER MAH 18.CAD.CAD.1/B YENISEHIR",AdresTarifiKisa = "D�KENL� YOLDEN�ZKIZI PASTAHANES� KAR�ISI/YEN��EH�R",AdresTarifi ="G�VENEVLER MAH.18.CAD.3/B D�KENL� YOLDEN�ZKIZI PASTAHANES� KAR�ISI/YEN��EH�R"},
new Eczane { Id = 560,Enlem = 36.8074310,Boylam = 34.6350570,TelefonNo = "3242371874",Adres = "YENIMAH.54 SOK.UGUR APT.NO.146 AKDENIZ",AdresTarifiKisa = "TOROS DEVLET HASTANES� (ESK� SSK HAST.)KAR�ISI",AdresTarifi ="TOROS DEVLET HASTANES� (ESK� SSK HASTANES�) KAR�ISI/AKDEN�Z"},
new Eczane { Id = 633,Enlem = 36.8313738,Boylam = 34.6355564,TelefonNo = "3242349959",Adres = "AKDENIZ IL�ESI G�NES MH. �IFT�ILER CD. NO:212 MERSIN",AdresTarifiKisa = "G�NE� MH.KURDAL� KAV�A�I TUZ DEPOSU C�VARI/AKDEN�Z",AdresTarifi ="G�NE� MH. KURDAL� KAV�A�I TUZ DEPOSU C�VARI/AKDEN�Z"},
new Eczane { Id = 607,Enlem = 36.7942800,Boylam = 34.6155800,TelefonNo = "3242382023",Adres = "TURGUT REIS MAH 4136 SOKAK NO.3 AKDENIZ",AdresTarifiKisa = "TURGUT RE�S MH. IMC HASTANES� AC�L KAR�ISI/AKDEN�Z",AdresTarifi ="TURGUT RE�S MH. IMC HASTANES� AC�L KAR�ISI/AKDEN�Z"},
new Eczane { Id = 744,Enlem = 36.7580250,Boylam = 34.5455410,TelefonNo = "3243574767",Adres = "FATIH MAH.28.SK.NO:7/A MEZITLI-MERSIN",AdresTarifiKisa = "HUNDA� ARKASI SAH�L SA�LIK OCA�I YANI/MEZ�TL�",AdresTarifi ="HUNDA� ARKASI SAH�L SA�LIK OCA�I YANI/MEZ�TL�"},
new Eczane { Id = 743,Enlem = 36.7571577,Boylam = 34.5363259,TelefonNo = "3243570787",Adres = "YENI MAH.KELVELI CAD.ZIYA �NSAL APT.MEZITLI/MERSIN",AdresTarifiKisa = "TOROS DEVLET HAS.MEZ�TL� SEMT POL�KL�N��� KAR�ISI",AdresTarifi ="MEZ�TL� YEN� MH. KALE YOLU TOROS DEVLET HASTANES� MEZ�TL� SEMT POL�KL�N��� KAR�ISI MEZ�TL�"},
new Eczane { Id = 587,Enlem = 36.8089230,Boylam =  34.621192,TelefonNo = "3243366817",Adres = "KUVAYIMILLIYE CAD.NO:235/A NO:40 AKDENIZ/MERSIN",AdresTarifiKisa = "ESK� DEVLET HASTANES� KAR�ISI/AKDEN�Z",AdresTarifi ="ESK� DEVLET HASTANES� KAR�ISI/AKDEN�Z"},
new Eczane { Id = 561,Enlem = 36.8066500,Boylam = 34.6355270,TelefonNo = "3242379900",Adres = "YENI MAH.5314SK.NO:6/A AKDENIZ/MERSIN",AdresTarifiKisa = "TOROS DEVLET HST KAR�ISI (ESK� SSK HAS.)/AKDEN�Z",AdresTarifi ="TOROS DEVLET HST KAR�ISI (ESK� SSK HAS.)/AKDEN�Z"},
new Eczane { Id = 634,Enlem = 36.8254870,Boylam = 34.6422460,TelefonNo = "3242342078",Adres = "�EVKET S�MER MH. 5975 SK. NO:59/A",AdresTarifiKisa = "�EVKET S�MER MH. S�TELER KARAKOLU YANI/AKDEN�Z",AdresTarifi ="�EVKET S�MER MH. S�TELER KARAKOLU YANI/AKDEN�Z"},
new Eczane { Id = 652,Enlem = 36.7827593,Boylam = 34.6042718,TelefonNo = "3243268594",Adres = "GAZI MAHALLESI 1304 SOKAK NO.6-B YENISEHIR MERSIN",AdresTarifiKisa = "MU�DAT CAM� ALTI MAKRO MARKET KAR�I SOKA�I",AdresTarifi ="Mu�dat Camii  Alt� Makro Market Kar�� Soka��ndan 100 Metre �lerde �lk Sokaktan Sola D�n�nce?"},
new Eczane { Id = 745,Enlem = 36.7575100,Boylam = 34.5368350,TelefonNo = "3243592067",Adres = "YEN� MH. KELVEL� CD. NO: 11/A",AdresTarifiKisa = "TOROS DEVLET HAST. MEZ�TL� SEMT POL�KL�N��� C�VARI",AdresTarifi ="YEN� MH. KELVEL� CD. - KALE YOLU - TOROS DEVLET HAST. MEZ�TL� SEMT POL�KL�N��� C�VARI YEN� MH. MUHTARLI�I KAR�ISI /MEZ�TL�"},
new Eczane { Id = 588,Enlem = 36.8090990,Boylam =  34.621091,TelefonNo = "3243364676",Adres = " IHSANIYE MH. KUVAI MILLIYE CAD.173/C",AdresTarifiKisa = "ESK� DEVLET HASTANES� KAR�ISI/AKDEN�Z",AdresTarifi ="ESK� DEVLET HASTANES� KAR�ISI/AKDEN�Z"},
new Eczane { Id = 531,Enlem = 36.8250450,Boylam = 34.6313610,TelefonNo = "3243210990",Adres = "SEL�ULAR MAH. 212CD.206/A TOROSLAR/MERSIN",AdresTarifiKisa = "SEL�UKLAR MH.KURDAL� CAM�� KAR�ISI",AdresTarifi ="SEL�UKLAR MH. 212. CD. NO:206/C KURDAL� CAM�� KAR�ISI/TOROSLAR"},
new Eczane { Id = 589,Enlem = 36.8081990,Boylam = 34.6214920,TelefonNo = "3243361952",Adres = "�HSAN�YE MH. 121.CAD NO:159/A (KUVAYI MILLIYE) ",AdresTarifiKisa = "ESK� DEVLET HASTANES� KAR�ISI/AKDEN�Z",AdresTarifi ="ESK� DEVLET HASTANES� KAR�ISI/AKDEN�Z"},
new Eczane { Id = 562,Enlem = 36.8048610,Boylam = 34.6322300,TelefonNo = "3242388684",Adres = "YENI MH. 5334 SK. NO:6",AdresTarifiKisa = "YEN� MAH.SA�LIK OCA�I KAR�ISI ",AdresTarifi ="YEN� MAH.SA�LIK OCA�I KAR�ISI BA� KUR �L M�D ARKASI/AKDEN�Z"},
new Eczane { Id = 563,Enlem = 36.8063540,Boylam = 34.6327040,TelefonNo = "3242380380",Adres = "YEN� MH. 5319 SK. NO:38/A",AdresTarifiKisa = "TOROS DEVLET HASTANES� AC�L SERV�S KUZEY�",AdresTarifi ="TOROS DEVLET HASTANES� AC�L C�VARI �TFA�YE YANI/AKDEN�Z"},
new Eczane { Id = 678,Enlem = 36.7840370,Boylam = 34.5895280,TelefonNo = "3243315411",Adres = "G�VENEVLER.MAH.FORUM.YENISEHIR.MERSIN",AdresTarifiKisa = "FORUM MERS�N AVM A BLOK 28/A/YEN��EH�R",AdresTarifi ="FORUM MERS�N AVM A BLOK 28/A/YEN��EH�R"},
new Eczane { Id = 710,Enlem = 36.7856132,Boylam = 34.5399163,TelefonNo = "3243412510",Adres = "MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI ��FTL�KK�Y MAH. M�MAR S�NAN CADDES�",AdresTarifiKisa = "MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�IS",AdresTarifi ="MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI ��FTL�KK�Y MAH. M�MAR S�NAN CADDES�/YEN��EH�R"},
new Eczane { Id = 508,Enlem = 36.8397220,Boylam = 34.6305740,TelefonNo = "3242240499",Adres = "G�NEYKENT MAH.FARABI CAD.NO.14.TOROSLAR",AdresTarifiKisa = "7 NOLU SA�LIK OCA�I YANI/TOROSLAR",AdresTarifi ="FARAB� CAD BELED�YE D�KKANLARI G�NEYKENT 7 NOLU SA�LIK OCA�I YANI/TOROSLAR"},
new Eczane { Id = 773,Enlem = 36.7351330,Boylam = 34.5138130,TelefonNo = "3243582500",Adres = "AKDEN�Z MH. 39607 SK. �EV�K-1 PLAZA ALTI NO:19",AdresTarifiKisa = "SOL� CENTER ARKASI 2 NOLU ASM KAR�ISI",AdresTarifi ="AKDEN�Z MH. SOL� CENTER ARKASI - B�M MARKET SOKA�I - �EV�K 1 PLAZA - 2 NOLU ASM KAR�ISI - MEZ�TL�/MEZ�TL�"},
new Eczane { Id = 614,Enlem = 36.7939993,Boylam = 34.6174615,TelefonNo = "3242370102",Adres = "ISTIKLAL CAD.�ELIKLER APT.ALTI NO.199/18-19",AdresTarifiKisa = " IMC HASTANES� C�VARI �ZKAN �ALGAM KAR�ISI/AKDEN�Z",AdresTarifi ="�ST�KLAL CD �ZEL IMC HASTANES� C�VARI �ZKAN �ALGAM KAR�ISI/AKDEN�Z"},
new Eczane { Id = 711,Enlem = 36.7859270,Boylam = 34.5398470,TelefonNo = "3242904635",Adres = "��FTL�KK�Y MH. M�MAR S�NAN CD. PARAD�SE HOMES S�TES� C BLOK NO: 24/CD",AdresTarifiKisa = "MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�IS",AdresTarifi ="��FTL�KK�Y MH. M�MAR S�NAN CD. (MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI) /YEN��EH�R"},
new Eczane { Id = 509,Enlem = 36.8426390,Boylam = 34.6234960,TelefonNo = "3242372817",Adres = "HALKKENT MH. F.SULTAN MEHMET BULV. 25B/3",AdresTarifiKisa = "ESK� KADIN DO�UM VE �OCUK HASTANES� KAR�ISI",AdresTarifi ="HALKKENT MAH MER�� �LER�S� ESK� KADIN DO�UM VE �OCUK HASTANES� KAR�ISI/TOROSLAR"},
new Eczane { Id = 566,Enlem = 36.8063230,Boylam = 34.6328370,TelefonNo = "3242375070",Adres = "YEN� MH. 5328 SK. NO:11/A-B",AdresTarifiKisa = "TOROS DEVLET HASTANES�  YEN� AC�L KAPISI KAR�ISI",AdresTarifi ="YEN� MH. TOROS DEVLET HASTANES�  YEN� AC�L KAPISI KAR�ISI/AKDEN�Z"},
new Eczane { Id = 680,Enlem = 36.7790720,Boylam =  34.575277,TelefonNo = "3243269184",Adres = "BARBAROS MAH.2148 SOK.NO20 YENISEHIR",AdresTarifiKisa = "YEN��EH�R KAYMAKAMLI�I KAR�ISI �OCUK PARKI ARKASI ",AdresTarifi ="YEN��EH�R KAYMAKAMLI�I KAR�ISI �OCUK PARKI ARKASI BARBAROS ASM KAR�ISI/YEN��EH�R"},
new Eczane { Id = 532,Enlem = 36.8139610,Boylam = 34.6283680,TelefonNo = "3243208960",Adres = "MERSIN ILI TOROSLAR IL�ESI SAGLIK MH.86019 SK.ISTANBUL AP.ALTI NO:6/A-B",AdresTarifiKisa = "TA�G�N HALISAHA B�T����� �EV�K KUVVET C�VARI",AdresTarifi ="SA�LIK MH. 86019 SK TA�G�N HALISAHA B�T����� �EV�K KUVVET C�VARI/TOROSLAR"},
new Eczane { Id = 564,Enlem = 36.8064457,Boylam = 34.6356586,TelefonNo = "3242312080",Adres = "YEN� MH. 5314 SK. NO:2",AdresTarifiKisa = "TOROS DEVLET HASTANES� KAR�ISI (ESK� SSK)",AdresTarifi ="TOROS DEVLET HASTANES� KAR�ISI (ESK� SSK)"},
new Eczane { Id = 746,Enlem = 36.7573520,Boylam = 34.5384730,TelefonNo = "3243411412",Adres = "YEN� MH. 33167 SK. NO:10/B -  MEZ�TL�",AdresTarifiKisa = "D�MAX MOB�LYA - TOYOTA PLAZA ARA SOKA�I - MEZ�TL�",AdresTarifi ="YEN� MH. 33167 SK. D�MAX MOB�LYA - TOYOTA PLAZA ARA SOKA�I - MEZ�TL�"},
new Eczane { Id = 653,Enlem = 36.7852110,Boylam = 34.6146820,TelefonNo = "3243284375",Adres = "PIRIREIS MAH.1103.SOK.18/A YENISEHIR",AdresTarifiKisa = " ESK� PLAJ YOLU, 24 KASIM �LKOKULU ARKA SOKA�I",AdresTarifi ="P�R�RE�S MH 1103 SK. ESK� PLAJ YOLU, 24 KASIM �LKOKULU ARKA SOKA�I"},
new Eczane { Id = 775,Enlem = 36.7416020,Boylam = 34.5250720,TelefonNo = "3243580998",Adres = "MENDERES MAHALLESI CEVAT KUTLU CADDESI NO.11/E MEZILI MERSIN",AdresTarifiKisa = "NAF�Z �OLAK ASM C�VARI  MEZ�TL� ",AdresTarifi ="11. NOTER�N DEN�ZE DO�RU 200 METRE  �LER�S� NAF�Z �OLAK ASM C�VARI  MEZ�TL� /MEZ�TL�"},
new Eczane { Id = 567,Enlem = 36.8059820,Boylam = 34.6331660,TelefonNo = "3242379733",Adres = "YEN� MH. 5328 SK. OSMAN BEY APT. NO:7/A",AdresTarifiKisa = "TOROS DEVLET HAST. ARKA SOK. YEN� AC�L KAPIS� KAR�",AdresTarifi ="YEN� MH. TOROS DEVLET HASTANES� ARKA SOKA�I YEN� AC�L KAPISI KAR�ISI/AKDEN�Z"},
new Eczane { Id = 715,Enlem = 36.7862000,Boylam = 34.5395670,TelefonNo = "3245020010",Adres = "��FTL�K K�Y MAHALLES� M�MAR S�NAN CADDES� PARAD�SE HOMES C BLOK NO:24CH",AdresTarifiKisa = "MERS�N �N�VERS�TES� TIP FAK�TSL. HASTANES� KAR�ISI",AdresTarifi ="MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI ��FTL�KK�Y MAH. M�MAR S�NAN CAD. PARAD�SE HOMES S�T. C BLOK NO:24 CH / YEN��EH�R"},
new Eczane { Id = 533,Enlem = 36.8261570,Boylam = 34.6252830,TelefonNo = "3243218998",Adres = "KURDALI MH.100041 SK.6/B",AdresTarifiKisa = "KURDAL� CAM�� C�VARI SELAHATT�N YANPAR ASM KAR�ISI",AdresTarifi ="KURDAL� MH. 100041 SK. KURDAL� CAM�� C�VARI SELAHATT�N YANPAR ASM KAR�ISI/TOROSLAR"},
new Eczane { Id = 590,Enlem = 36.8096870,Boylam = 34.6222990,TelefonNo = "3243362660",Adres = "NUSRATIYE MH. 5026 SK. NO:44/C AKDENIZ MERSIN",AdresTarifiKisa = "ESK� DEVLET HASTANES� ARKA SOKA�I",AdresTarifi ="ESK� DEVLET HASTANES� ARKA SOKA�I  SA�LIK KURULU - KAN BANKASI KAR�ISI/AKDEN�Z"},
new Eczane { Id = 776,Enlem = 36.7492830,Boylam = 34.5289610,TelefonNo = "3243583685",Adres = "MERKEZ MAHALESI KARAOGLAN CADDESI NO.5/A MEZITLI MERSIN",AdresTarifiKisa = "MEZ�TL� MERKEZ MH. 2.NOLU ASM KAR�ISI ",AdresTarifi ="MEZ�TL� MERKEZ MH. 2.NOLU ASM KAR�ISI MEZ�TL� BELED�YES� C�VARI"},
new Eczane { Id = 568,Enlem = 36.8068940,Boylam = 34.6353930,TelefonNo = "3242324094",Adres = "YENI MAH.5314 SK.MELTEM APT.ALTI NO:8/2 AKDENIZ/MERSIN",AdresTarifiKisa = "TOROS DEVLET HASTANES� KAR�ISI (ESK� SSK )/AKDEN�Z",AdresTarifi ="TOROS DEVLET HASTANES� KAR�ISI ( ESK� SSK )/AKDEN�Z"},
new Eczane { Id = 615,Enlem = 36.7977990,Boylam = 34.6259700,TelefonNo = "3242373086",Adres = "ISTIKLAL CD. NO:75/A AKDENIZ/MERSIN",AdresTarifiKisa = "�ST�KLAL CD NO 75/A  NOBEL OTEL� YANI/AKDEN�Z",AdresTarifi ="�ST�KLAL CD NO 75/A  NOBEL OTEL� YANI/AKDEN�Z"},
new Eczane { Id = 535,Enlem = 36.8196334,Boylam = 34.6152043,TelefonNo = "3243227343",Adres = "TOZKOPARAN MH. 87045 SK. NO:23/B",AdresTarifiKisa = "TOZKOPARAN SA�LIK OCA�I KAR�ISI ",AdresTarifi ="TOZKOPARAN MH. TOZKOPARAN SA�LIK OCA�I KAR�ISI PALM�YE D���N SALONU KAR�I SOKA�I �UKUROVA �LK��RET�M OKULU C�VARI / TOROSLAR"},
new Eczane { Id = 635,Enlem = 36.8180620,Boylam = 34.6315320,TelefonNo = "3242357974",Adres = "G�NDOGDU MAH.1 NOLU SAG.OC.KARS.5790 SK.NO:19 AKDENIZ/MERSIN",AdresTarifiKisa = "FEN L�SES� C�VARI 1 NOLU SA�LIK OCA�I KR�/AKDEN�Z",AdresTarifi ="FEN L�SES� C�VARI 1 NOLU SA�LIK OCA�I KR�/AKDEN�Z"},
new Eczane { Id = 536,Enlem = 36.8250076,Boylam = 34.6004830,TelefonNo = "3243222255",Adres = "AKBELEN MH.TOROSLAR CD. NO:6/E TOROSLAR/MERSIN",AdresTarifiKisa = "AKBELEN SA�LIK OCA�I KAR�SI KORAY AYDIN STADI ALT�",AdresTarifi ="AKBELEN MH. AKBELEN SA�LIK OCA�I KAR�ISI KORAY AYDIN STADI ALTI /TOROSLAR"},
new Eczane { Id = 616,Enlem = 36.7950770,Boylam = 34.6237710,TelefonNo = "3242393444",Adres = "KIREMITHANE MH.4406 DR. CEMALETTIN TANRI�VER SK. FAKS AP. ALTI NO:10/C",AdresTarifiKisa = "�ZG�R �OCUK PARKI ARKASI  �ZEL DO�U� HAST. KAR�ISI",AdresTarifi ="K�REM�THANE MH. �ZG�R �OCUK PARKI ARKASI  �ZEL DO�U� HASTANES� KAR�ISI/AKDEN�Z"},
new Eczane { Id = 591,Enlem = 36.8099035,Boylam = 34.6226827,TelefonNo = "3243364906",Adres = "NUSRAT�YE MH. 5021 SK. NO:15/3",AdresTarifiKisa = "ESK� DEVLET HASTANES� ARKA SOKA�I /AKDEN�Z",AdresTarifi ="ESK� DEVLET HASTANES� ARKA SOKA�I /AKDEN�Z"},
new Eczane { Id = 617,Enlem = 36.7916780,Boylam = 34.6238340,TelefonNo = "3242315854",Adres = "K�LT�R MH. ATAT�RK CD. NO:35",AdresTarifiKisa = "ATAT�RK CD. ALTINANAHTAR APARTMAN ALTI �AMLIBEL",AdresTarifi ="ATAT�RK CD. ALTINANAHTAR APARTMAN ALTI �AMLIBEL/AKDEN�Z"},
new Eczane { Id = 654,Enlem = 36.7828734,Boylam = 34.6061372,TelefonNo = "3243254191",Adres = "PALMIYE MAH.1207.SOK.YAL�IN�Z.APT.ALTI.NO.16/A. YENISEHIR.MERSIN",AdresTarifiKisa = "PALM�YE MH P�R�RE�S �LK��RET�M OKULU YOLU �LER�S� ",AdresTarifi ="PALM�YE MH P�R�RE�S �LK��RET�M OKULU YOLU �LER�S� SAH�LDEN BE��KTA� KAPISI KAR�I YOLU �LER�S� YEN��EH�R"},
new Eczane { Id = 537,Enlem = 36.8179110,Boylam = 34.6298850,TelefonNo = "3243207473",Adres = "�IFT�ILER CAD.97/ASEL�UKLAR TOROSLAR/MERSIN",AdresTarifiKisa = "(KURDAL� YOLU) T�RK�YE PETROLLER� KAR�.TOROSLAR",AdresTarifi ="SEL�UKLAR MAH ��FT��LER CAD (KURDAL� YOLU) T�RK�YE PETROLLER� KAR�ISI �EV�K KUVVET C�VARI/TOROSLAR"},
new Eczane { Id = 655,Enlem = 36.7858036,Boylam = 34.6005056,TelefonNo = "3243285802",Adres = "CUMHURIYET MH. 16 CD.4/B",AdresTarifiKisa = "POZCU TELEKOM  C�VARI 45 EVLER YOLU",AdresTarifi ="CUMHUR�YET MH. 16. CD. �L TELEKOM M�D�RL��� YANI - 45 EVLER YOLU SUPH� �ALGAM B�T����� YEN��EH�R"},
new Eczane { Id = 716,Enlem = 36.7982060,Boylam = 34.5714440,TelefonNo = "3243284282",Adres = "50. YIL MH. 27140 SK. NO:2/A",AdresTarifiKisa = "METRO ALI� VER�� MERKEZ� ARKASI - 50. YIL",AdresTarifi ="METRO ALI� VER�� MERKEZ� ARKASI - 50. YIL POL�S KARAKOLU C�VARI 50. YIL ASM KAR�ISI YEN��EH�R/YEN��EH�R"},
new Eczane { Id = 749,Enlem = 36.7579230,Boylam = 34.5458420,TelefonNo = "3243587867",Adres = "MEZITLI IL�ESI FATIH MH.28 SK.MELTEM AP. ALTI.NO:2",AdresTarifiKisa = "MEZ�TL� HUNDA� ARKASI SAH�L SA�LIK OCA�I C�VARI",AdresTarifi ="FAT�H MH. MEZ�TL� HUNDA� ARKASI SAH�L SA�LIK OCA�I C�VARI/MEZ�TL�"},
new Eczane { Id = 750,Enlem = 36.7631620,Boylam = 34.5478980,TelefonNo = "3243575811",Adres = "ATAT�RK MAH.VAL� �ENOL ENG�N CAD.NO:8 A/20 MEZITLI/MERSIN",AdresTarifiKisa = " VAL� �ENOL ENG�N CD.ORTADO�U HAST. K��ES� MEZ�TL�",AdresTarifi ="VAL� �ENOL ENG�N CD.�ZEL ORTADO�U HASTANES� K��ES� MEZ�TL�"},
new Eczane { Id = 751,Enlem = 36.7560770,Boylam = 34.5377100,TelefonNo = "3243570105",Adres = "YEN� MH. KELVEL� CD. Z�YA �NSAL APT. ALTI NO:: 23/A   MEZ�TL�",AdresTarifiKisa = "MEZ�TL� TOROS DEVLET HAST.SEMT POL�KL�N��� KAR�ISI",AdresTarifi ="TOROS DEVLET HASTANES� SEMT POL�KL�N��� KAR�ISI - KALEYOLU MEZ�TL�"},
new Eczane { Id = 778,Enlem = 36.7493360,Boylam = 34.5291970,TelefonNo = "3245020016",Adres = "MERKEZ MH. 52030 SK. DURDU ULU� APT. NO.6 Z13",AdresTarifiKisa = "MEZ�TL� BELED�YES� KAR�ISI MERKEZ ASM YANI",AdresTarifi ="MEZ�TL� BELED�YES� KAR�ISI ALDA MARKET ARKASI MERKEZ ASM YANI - MEZ�TL�/MEZ�TL�"},
new Eczane { Id = 510,Enlem = 36.8489600,Boylam = 34.6075910,TelefonNo = "3242230207",Adres = "KORUKENT MH. 96017 SK. NO:20",AdresTarifiKisa = "�EH�R HAST. C�VARI KORUKENT MH. MUHTARLI�I KAR�ISI",AdresTarifi ="KORUKENT MH. 96017 SK. �EH�R HASTANES� C�VARI KORUKENT MH. MUHTARLI�I KAR�ISI"},
new Eczane { Id = 656,Enlem = 36.7833990,Boylam = 34.6039080,TelefonNo = "3243290585",Adres = "GAZI MAH.1304.SOK.NO.12/A YENISEHIR",AdresTarifiKisa = "MU�DAT CAM� C�VARI, GAZ� ASM KAR�ISI",AdresTarifi ="MU�DAT CAM� C�VARI, GAZ� ASM KAR�ISI - POZCU'"},
new Eczane { Id = 538,Enlem = 36.8122550,Boylam = 34.6195570,TelefonNo = "3243220704",Adres = "K�VAYI MILLIYE CADDESI NO.154.TOROSLAR MERSIN",AdresTarifiKisa = "HASTANE CD ESK� TOROSLAR BELED�YES� KAR�ISI.",AdresTarifi ="HASTANE CD ESK� TOROSLAR BELED�YES� KAR�ISI/TOROSLAR"},
new Eczane { Id = 592,Enlem = 36.8100900,Boylam = 34.6222500,TelefonNo = "3243363634",Adres = "NUSRAT�YE MH. 5021 SK. �AH�N APT ALTI NO:53/1",AdresTarifiKisa = "ESK� DEVLET HASTANES� ESK� AC�L KAR�ISI",AdresTarifi ="ESK� DEVLET HASTANES� ESK� AC�L KAR�ISI/AKDEN�Z"},
new Eczane { Id = 681,Enlem = 36.7858300,Boylam = 34.5928860,TelefonNo = "3243250192",Adres = "G�VENEVLER MAH. CADDE NO:13 YENISEHIR MERSIN",AdresTarifiKisa = "G�VENEVLER MH1. CAD. G�VEN S�TES� KAR�ISI POZCU",AdresTarifi ="G�VENEVLER MH1. CAD. G�VEN S�TES� KAR�ISI POZCU/YEN��EH�R"},
new Eczane { Id = 779,Enlem = 36.7411090,Boylam = 34.5253560,TelefonNo = "3243588856",Adres = "MENDERES MAHALLESI CEVAT KUTLU CADDESI YALI KENT AP.NO.37/22/C MEZITLI MERSIN",AdresTarifiKisa = " 11. NOTER SOKA�I NAF�Z �OLAK SAG.OC.YANI.MEZ�TL�",AdresTarifi ="L�DER GED�KLER KAR�ISI 11. NOTER SOKA�I SAH�LDEN DEN�ZHAN 2 KAR�I CD. �ZER�/MEZ�TL�"},
new Eczane { Id = 825,Enlem = 36.7990250,Boylam = 34.6251280,TelefonNo = "3242385119",Adres = "�ANKAYA MAHALLE 4738 SOKAK NO 22. AKDENIZ",AdresTarifiKisa = "�ANKAYA MH 4738 SK NOBEL OTEL� KR� SOKA�I  AKDEN�Z",AdresTarifi ="�ANKAYA MH 4738 SK NOBEL OTEL� KR� SOKA�I NO 22/AKDEN�Z"},
new Eczane { Id = 657,Enlem = 36.7860200,Boylam = 34.6044200,TelefonNo = "3243294690",Adres = "GMK BULVARI PALMIYE MAH.EREN AP.NO.263/32. YENISEHIR",AdresTarifiKisa = "ESK� DUYGU TIP MERKEZ� YANI TOP�ULAR DURA�I",AdresTarifi ="ESK� DUYGU TIP MERKEZ� YANI TOP�ULAR DURA�I/YEN��EH�R"},
new Eczane { Id = 682,Enlem = 36.7900090,Boylam = 34.5875560,TelefonNo = "3243363569",Adres = "G�VENEVLER MH. 1953 SK. NO:14 A/B",AdresTarifiKisa = "FORUM YA�AM HAST. AC�L KAR�ISI",AdresTarifi ="�ZEL FORUM HASTANES� AC�L SERV�S� KAR�ISI"},
new Eczane { Id = 569,Enlem = 36.8060730,Boylam = 34.6330850,TelefonNo = "3242380016",Adres = "Yeni Mh. 5328 Sk. No: 9/A",AdresTarifiKisa = "TOROS DEVLET HAST. ARKA SOKA�I YEN� AC�L KAR�ISI",AdresTarifi ="YEN� MH. TOROS DEVLET HASTANES� ARKA SOKA�I YEN� AC�L KAPISI KAR�ISI/AKDEN�Z"},
new Eczane { Id = 593,Enlem = 36.8078420,Boylam = 34.6239200,TelefonNo = "3243575857",Adres = "NUSRAT�YE MH. 5020 SK. NO:17/A",AdresTarifiKisa = "ESK� DEVLET HASTANES� C�VARI OPET ARKASI AKDEN�Z",AdresTarifi ="ESK� DEVLET HASTANES� C�VARI OPET ARKASI AKDEN�Z"},
new Eczane { Id = 512,Enlem = 36.8396980,Boylam = 34.6303550,TelefonNo = "3242233030",Adres = "FARAB� CD. NO:29 G�NEYKENT",AdresTarifiKisa = "G�NEYKENT �AR�ISI NO:29 - 7 NOLU SA�LIK OCA�I YANI",AdresTarifi ="FARAB� CD G�NEYKENT �AR�ISI NO:29 - 7 NOLU SA�LIK OCA�I YANI/AKDEN�Z"},
new Eczane { Id = 513,Enlem = 36.8428770,Boylam = 34.6232710,TelefonNo = "3242230818",Adres = "HALKKENT MAH.2996 ADA.L BLOK NO:15 TOROSLAR ",AdresTarifiKisa = "ESK� KADIN DO�UM VE �OCUK HASTANES�  KAR�ISI",AdresTarifi ="ESK� KADIN DO�UM VE �OCUK HASTANES� �APRAZ KAR�ISI HALKKENT MH. TOROSLAR"},
new Eczane { Id = 594,Enlem = 36.8062440,Boylam = 34.6279400,TelefonNo = "3242371598",Adres = "�AKMAK CAD. NO : 77",AdresTarifiKisa = "�AKMAK CADDES�  BOLKEP�E LOKANTASI KAR�ISI",AdresTarifi ="�AKMAK CADDES� NO:79 BE�YOL / BOLKEP�E LOKANTASI KAR�ISI/AKDEN�Z"},
new Eczane { Id = 658,Enlem = 36.7894770,Boylam = 34.6066050,TelefonNo = "3243275848",Adres = "CUMHUR�YET MH. 1617 SK. MEHMETO�LU APT. NO:3/B",AdresTarifiKisa = "CUMHUR�YET MH. TULUMBA KAV�A�I 50 METRE KUZEY�",AdresTarifi ="CUMHUR�YET MH. 1617 SK. TULUMBA KAV�A�I 50 METRE KUZEY� C��ERC� HAKAN VE DONAT T�CARET ARKASI - POZCU/YEN��EH�R"},
new Eczane { Id = 781,Enlem = 36.7469908,Boylam = 34.5265523,TelefonNo = "3243593233",Adres = "MENDERES MH. GMK BULV. BEKO APT. ALTI 725/C",AdresTarifiKisa = "KUYULUK KAV�A�I KAR�ISI VAKIFBANK YANI MEZ�TL�/MEZ",AdresTarifi ="KUYULUK KAV�A�I KAR�ISI VAKIFBANK YANI MEZ�TL�/MEZ�TL�"},
new Eczane { Id = 570,Enlem = 36.8068280,Boylam = 34.6355220,TelefonNo = "3242335072",Adres = "YENI MAH.5314 SOK. MELTEM APT. ALTI NO:8 AKDEN�Z",AdresTarifiKisa = "TOROS DEVLET HASTANES� (ESK� SSK) KAR�ISI/AKDEN�Z",AdresTarifi ="TOROS DEVLET HASTANES� (ESK� SSK) KAR�ISI/AKDEN�Z"},
new Eczane { Id = 595,Enlem = 36.8056810,Boylam = 34.6230680,TelefonNo = "3243361048",Adres = "KUVAIMIILLIYE CADDESI NO.124 AKDENIZ.MERSIN",AdresTarifiKisa = "HASTANE CADDES� KURU�E�ME KAR�ISI/AKDEN�Z",AdresTarifi ="HASTANE CADDES� KURU�E�ME KAR�ISI/AKDEN�Z"},
new Eczane { Id = 665,Enlem = 36.7925630,Boylam = 34.5826280,TelefonNo = "3243369980",Adres = "MENTE� MH.25161 SK. NO:27 AKADEM� HASTANES� YANI",AdresTarifiKisa = "MENTE� MAHALLES� �ZEL AKADEM� HASTANES� YANI ",AdresTarifi ="MENTE� MAHALLES� �ZEL AKADEM� HASTANES� YANI "},
new Eczane { Id = 659,Enlem = 36.7835080,Boylam = 34.6029690,TelefonNo = "3243265620",Adres = "GAZI MAH.1307 SOK.NO.16/B                YENISEHIR",AdresTarifiKisa = "GAZ� MH. 1307 SK MU�DAT CAM� KAR�ISI",AdresTarifi ="GAZ� MH. 1307 SK MU�DAT CAM� KAR�ISI B�M MARKET ARKASI/YEN��EH�R"},
new Eczane { Id = 539,Enlem = 36.8185790,Boylam = 34.6294650,TelefonNo = "3243200202",Adres = "SEL�UKLAR MH.206 CD.NO:146/TOROSLAR MERSIN",AdresTarifiKisa = "SEL�UKLAR MH. 206. CD.SEL�UKLAR ASM KAR�ISI ",AdresTarifi ="SEL�UKLAR MH. 206. CD.SEL�UKLAR ASM KAR�ISI AKABE CAM�� C�VARI/TOROSLAR"},
new Eczane { Id = 540,Enlem = 36.8381480,Boylam = 34.5984400,TelefonNo = "3243251404",Adres = "�UKUROVA MH. 85119 SK. NO.132",AdresTarifiKisa = " �UKUROVA SA�LIK OCA�I KAR�ISI/TOROSLAR",AdresTarifi ="�UKUROVA MH. 85119 SK. - �UKUROVA SA�LIK OCA�I KAR�ISI/TOROSLAR"},
new Eczane { Id = 597,Enlem = 36.8080099,Boylam = 34.6180385,TelefonNo = "3243365717",Adres = "IHSANIYE MAHALLESI HAVUZLAR CADDESI NO.74",AdresTarifiKisa = "ESK� DEVLET HAST. KAR�I SOKA�I G�ZDE TIP MRK YAN",AdresTarifi ="ESK� DEVLET HASTANES� KAR�I SOKA�I  �HSAN�YE MH HAVUZLAR CD. G�ZDE TIP MRK YANI/AKDEN�Z"},
new Eczane { Id = 683,Enlem = 36.7925642,Boylam = 34.5827809,TelefonNo = "3249999745",Adres = "MENTE� MH. BARBAROS BULV. NO:107/B",AdresTarifiKisa = "",AdresTarifi ="�ZEL AKADEM� HASTANES� YANI"},
new Eczane { Id = 541,Enlem = 36.8249269,Boylam = 34.6003380,TelefonNo = "3243201120",Adres = "AKBELEN MH. TOROSLAR CD. NO:6/B1-B2-B3",AdresTarifiKisa = "AKBELEN MH. TOROSLAR CD. NO:6",AdresTarifi ="AKBELEN KONUTEVLER� KORAY AYDIN STADI ALTI AKBELEN SA�LIK OCA�I KAR�ISI/TOROSLAR"},
new Eczane { Id = 619,Enlem = 36.7960134,Boylam = 34.6240593,TelefonNo = "3242388985",Adres = "ISTIKLAL CADDESI KIREMITHANE MAHALLESI 4413 SOKAK.NO.12/34 AKDENIZ.MERSIN",AdresTarifiKisa = "�ST�KLAL CD �ZG�R �OCUK PARKI YANI/AKDEN�Z",AdresTarifi ="�ST�KLAL CD �ZG�R �OCUK PARKI YANI/AKDEN�Z"},
new Eczane { Id = 782,Enlem = 36.7464175,Boylam = 34.5376006,TelefonNo = "3243597880",Adres = "Viran�ehir Mh. Viran�ehir Cd. No:39/D",AdresTarifiKisa = "V�RAN�EH�R MH.V�RAN�EH�R CD NO:39/D /MEZ�TL�",AdresTarifi ="V�RAN�EH�R MH.V�RAN�EH�R CD NO:39/D /MEZ�TL�"},
new Eczane { Id = 571,Enlem = 36.8057740,Boylam = 34.6333050,TelefonNo = "3243289050",Adres = "YEN� MH. 5328 SK. �ZBEY APT. NO:1/A",AdresTarifiKisa = "TOROS DEVLET HASTANES� AC�L KAPISI KAR�ISI ",AdresTarifi ="TOROS DEVLET HASTANES� AC�L KAPISI KAR�ISI  SOL �APRAZ K��E /AKDEN�Z"},
new Eczane { Id = 514,Enlem = 36.8512210,Boylam = 34.6110120,TelefonNo = "3242233770",Adres = "KORUKENT MH. 96004 SK. NO:13/1",AdresTarifiKisa = " MERS�N �EH�R HASTANES� C�VARI TOK� EVLER�,KAR�ISI",AdresTarifi ="KORUKENT MH. 96004 SK. MERS�N �EH�R HASTANES� C�VARI TOK� EVLER�, KAR�ISI TOK� CAM�� �ZER�"},
new Eczane { Id = 684,Enlem = 36.7854340,Boylam = 34.5928900,TelefonNo = "3243254891",Adres = "G�VENEVLER MAH.1911 SK.�ALIKOGLU APT.NO:25/12-YENISEHIR-MERSIN",AdresTarifiKisa = "G�VEN S�TES� C�VARI ALANYA FIRINI SOKA�I/YEN��EH�R",AdresTarifi ="G�VENEVLER MH 1902 SOK. NO 25 G�VEN S�TES� C�VARI ALANYA FIRINI SOKA�I/YEN��EH�R"},
new Eczane { Id = 545,Enlem = 36.8262290,Boylam = 34.6250660,TelefonNo = "3243205590",Adres = "KURDALI MAH. 10041 SOK. NO:5 TOROSLAR",AdresTarifiKisa = "SELAHATT�N YANPAR SA�LIK OCA�I YANI/TOROSLAR",AdresTarifi ="KURDAL� MAH 100041 SK NO 5 SELAHATT�N YANPAR SA�LIK OCA�I YANI/TOROSLAR"},
new Eczane { Id = 717,Enlem = 36.7804870,Boylam = 34.5574340,TelefonNo = "3242904485",Adres = "BATIKENT MH. 2635 SK. YA�MUR SULTAN KONUTLARI C BLOK ALTI NO:25",AdresTarifiKisa = " MERS�N VAL�L�K B�NASI KUZEY� - MEVLANA ASM C�VARI",AdresTarifi ="BATIKENT MH. �AH�N TEPES� - NECAT� BOLKAN �.�.OKULU YANI MERS�N VAL�L�K B�NASI KUZEY� - MEVLANA ASM C�VARI YEN��EH�R"},
new Eczane { Id = 687,Enlem = 36.7793040,Boylam = 34.5748770,TelefonNo = "3243254457",Adres = "BARBAROS MAH MAH.2121 SOKAK NO.2/A YENISEHIR",AdresTarifiKisa = "YEN��EH�R KAYMAKAMLI�I KAR�ISI BARBAROS ASM YANI",AdresTarifi ="YEN��EH�R KAYMAKAMLI�I KAR�ISI BARBAROS �OCUK PARKI ARKASI 15 NOLU SA�LIK OCA�I YANI/YEN��EH�R"},
new Eczane { Id = 546,Enlem = 36.8168600,Boylam = 34.6195440,TelefonNo = "3243233383",Adres = "TOZKOPARAN MH. 87002 SK. NO:33 TOROSLAR",AdresTarifiKisa = "TOZKOPARAN MH. HASAN AKEL L�SES� C�VARI",AdresTarifi =" TOZKOPARAN MH. HASAN AKEL L�SES� C�VARI �AR�AMBA PAZARI SOKA�I"},
new Eczane { Id = 596,Enlem = 36.8108630,Boylam = 34.6223590,TelefonNo = "3243368218",Adres = "NUSRATIYE MAH.GMK BULV.M.MUSTAFA EFENDI SIT.C BLOK 119/E AKDENIZ/MERSIN",AdresTarifiKisa = "ESK� DEVLET HASTANES� B�T����� �� BANKASI KAR�ISI",AdresTarifi ="ESK� DEVLET HASTANES� B�T����� �EVRE YOLU (GMK) TARAFI ��BANKASI KAR�ISI - YAYA �ST GE��D� YANI/AKDEN�Z"},
new Eczane { Id = 516,Enlem = 36.8426390,Boylam = 34.6234960,TelefonNo = "3242237061",Adres = "Halkkent Mh. Fatih Sultan Mehmet Bulv. A Blok No:1",AdresTarifiKisa = "ESK� KADIN DO�UM VE �OCUK HAS. KAR�ISI/TOROSLAR",AdresTarifi ="HALKKENT MAH. ESK�  KADIN DO�UM VE �OCUK HAS. KAR�ISI/TOROSLAR"},
new Eczane { Id = 691,Enlem = 36.7832833,Boylam = 34.5920555,TelefonNo = "3243268033",Adres = " YEN��EH�R HASTANES� ARKASI (G�VENEVLER MH.) YEN��EH�R",AdresTarifiKisa = "�ZEL YEN��EH�R HASTANES� ARKASI (FORUM AVM C�VARI)",AdresTarifi ="�ZEL YEN��EH�R HASTANES� ARKASI (FORUM AVM C�VARI)"},
new Eczane { Id = 718,Enlem = 36.7709311,Boylam = 34.5628202,TelefonNo = "3243412144",Adres = "G.M.K. BULVARI EGRI�AM MAH.NO�.568/A YENISEHIR",AdresTarifiKisa = "UZMAN ATA TIP MRKZ VE SULTA�A OTEL� KAR�ISI",AdresTarifi ="GMK BULV. UZMAN ATA TIP MRKZ VE SULTA�A OTEL� KAR�ISI E�R��AM/YEN��EH�R"},
new Eczane { Id = 572,Enlem = 36.8063230,Boylam = 34.6328370,TelefonNo = "3242370500",Adres = "YEN�MAHALLE 5328 SOKAK FAHR�YE ABLA APT. 11/B AKDEN�Z MERS�N",AdresTarifiKisa = "TOROS DEVLET HASTANES�  AC�L KAR�ISI",AdresTarifi ="TOROS DEVLET HASTANES�  AC�L KAR�ISI"},
new Eczane { Id = 692,Enlem = 36.7792690,Boylam = 34.5750540,TelefonNo = "3243282333",Adres = "BARBAROS MAHALLES� 2148 SOKAK NO:22/B",AdresTarifiKisa = " BARBAROS ASM KAR�ISI / YEN��EH�R",AdresTarifi ="BARBAROS MH. YEN��EH�R KAYMAKAMLI�I KAR�ISI - �OCUK PARKI ARKASI - BARBAROS ASM KAR�ISI / YEN��EH�R"},
new Eczane { Id = 517,Enlem = 36.8367167,Boylam = 34.6102022,TelefonNo = "3242241809",Adres = "MERSIN ILI TOROSLAR IL�ESI �AGDASKENT MH. 229 CD. NO:10/7",AdresTarifiKisa = "ERZ�NCANLILAR S�TES� KAR�ISI / TOROSLAR",AdresTarifi ="�A�DA�KENT MH 229. CD. MAKRO MARKET C�VARI ERZ�NCANLILAR S�TES� KAR�ISI / TOROSLAR/TOROSLAR"},
new Eczane { Id = 660,Enlem = 36.7827040,Boylam = 34.6043000,TelefonNo = "3243274541",Adres = "GAZI MAH.1304 SOK.SELIM BEY AP.NO.6/A.YENISEHIR",AdresTarifiKisa = "MU�DAT CAM�� C�VARI 9 NOLU SA�LIK OCA�I KAR�ISI ",AdresTarifi ="MU�DAT CAM�� C�VARI 9 NOLU SA�LIK OCA�I KAR�ISI B�M MARKET ARKASI POZCU/YEN��EH�R"},
new Eczane { Id = 548,Enlem = 36.8261510,Boylam = 34.6252270,TelefonNo = "3243370084",Adres = "KURDAL� MH. 100041 SK. NO:6/A",AdresTarifiKisa = " SELAHATT�N YANPAR ASM KAR�ISI/TOROSLAR",AdresTarifi ="KURDAL� MH. 100041 SK. SELAHATT�N YANPAR ASM KAR�ISI/TOROSLAR"},
new Eczane { Id = 573,Enlem = 36.8070710,Boylam = 34.6352780,TelefonNo = "3242335624",Adres = "YENI MAH.5314 SOK.NO.10 AKDENIZ",AdresTarifiKisa = "TOROS DEVLET HASTANES� KAR�ISI ( ESK� SSK )/AKDEN�",AdresTarifi ="TOROS DEVLET HASTANES� KAR�ISI ( ESK� SSK )/AKDEN�Z"},
new Eczane { Id = 630,Enlem = 36.8254350,Boylam = 34.6419190,TelefonNo = "3242355581",Adres = "�EVKET S�MER MAH. 156. CAD. NO:21  AKDEN�Z",AdresTarifiKisa = "�EVKET S�MER MH S�TELER KARAKOLU KAR�ISI / AKDEN�Z",AdresTarifi ="�EVKET S�MER MH S�TELER KARAKOLU KAR�ISI / AKDEN�Z"},
new Eczane { Id = 785,Enlem = 36.7190860,Boylam = 34.4860625,TelefonNo = "3244814797",Adres = "GMK BULV. EBRU S�TES� B BLOK NO:14 DAVULTEPE",AdresTarifiKisa = "GMK BULV. DAVULTEPE B�M YANI  - DAVULTEPE/MEZ�TL�",AdresTarifi ="GMK BULV. DAVULTEPE B�M YANI  - DAVULTEPE/MEZ�TL�"},
new Eczane { Id = 693,Enlem = 36.7833349,Boylam = 34.5919858,TelefonNo = "3243363079",Adres = "G�VENEVLER MH. 1908 SK. �M�T ATP. NO:8/A",AdresTarifiKisa = "�ZEL YEN��EH�R HASTANES� ARKA SOKA�I/YEN��EH�R",AdresTarifi ="G�VENEVLER MH. 1908 SK. NO:8 �ZEL YEN��EH�R HASTANES� ARKA SOKA�I/YEN��EH�R"},
new Eczane { Id = 753,Enlem = 36.7580250,Boylam = 34.5456210,TelefonNo = "3243598636",Adres = "FATIH MAH.HUNDAI ARKASI 28.SOK.NO MELTEM AP.MEZITLI",AdresTarifiKisa = "HYUNDA� ARKASI SAH�L SA�LIK OCA�I B�T�����/MEZ�TL�",AdresTarifi ="HYUNDA� ARKASI SAH�L SA�LIK OCA�I B�T�����/MEZ�TL�"},
new Eczane { Id = 598,Enlem = 36.8075860,Boylam = 34.6184420,TelefonNo = "3243363590",Adres = "�hsaniye Mahallesi Sait �ift�i Cd.  No:19/E   Akdeniz /MERS�N",AdresTarifiKisa = "Su Hastanesi (Eski G�zde Hastanesi) Kar��s�",AdresTarifi ="Su Hastanesi (Eski G�zde Hastanesi) kar��s�"},
new Eczane { Id = 694,Enlem = 36.7897859,Boylam = 34.5875678,TelefonNo = "3243252211",Adres = "YENISEHIR IL�ESI G�VENEVLER MH.20 CD.1953 SK.NO:25 MERSIN",AdresTarifiKisa = "FORUM YA�AM HASTANES� AC�L KAR�ISI ",AdresTarifi ="FORUM YA�AM HASTANES� AC�L KAR�ISI 1953 SK. NO:25  YEN��EH�R/YEN��EH�R"},
new Eczane { Id = 549,Enlem = 36.8240273,Boylam = 34.6036559,TelefonNo = "3243225270",Adres = "YUSUF KILI� MH.641 SK.NO:53/1 TOROSLAR/MERSIN",AdresTarifiKisa = "YUSUF KILI� MAH. 217.CAD AYI�I�I D���N SALONU YANI",AdresTarifi ="YUSUF KILI� MAH. 217. CAD AYI�I�I D���N SALONU YANI"},
new Eczane { Id = 819,Enlem = 36.7704810,Boylam = 34.5623980,TelefonNo = "3243370045",Adres = "GMK BULVARI NO 529/A SULTA�A OTEL VE UZMAN ATA TIP MERKEZ� YAKINI DS� KAR�ISI /YEN��EH�R",AdresTarifiKisa = "GMK BULV. TOLGA S�TES� A BLOK NO:20",AdresTarifi ="GMK BULV. TOLGA S�TES� A BLOK NO:20"},
new Eczane { Id = 620,Enlem = 36.7908560,Boylam = 34.6193880,TelefonNo = "3242316273",Adres = "HAMIDIYE MAH.CENGIZ TOPEL CAD.17/B AKDENIZ IL�ESI-MERSIN",AdresTarifiKisa = "�AMLIBEL �OK MARKET C�VARI ",AdresTarifi ="CENG�Z TOPEL CD �AMLIBEL �OK MARKET C�VARI SAL�M YILMAZ L.K��E/AKDEN�Z"},
new Eczane { Id = 720,Enlem = 36.7756830,Boylam = 34.5377920,TelefonNo = "3242905590",Adres = "��FTL�KK�Y MH. �N�VERS�TE CD. �ZLEM �N�VERS�TE KONUTLARI NO:36/BK",AdresTarifiKisa = "�N�VERS�TE CD. VE �STEM�HAN TALAY BULVARI KAV�A�I",AdresTarifi ="��FTL�KK�Y MH. �ZLEM �N�VERS�TE KONUTLARI 2 - �N�VERS�TE CD. VE �STEM�HAN TALAY BULVARI KAV�A�I/YEN��EH�R"},
new Eczane { Id = 599,Enlem = 36.8094490,Boylam = 34.6208790,TelefonNo = "3243375354",Adres = "�HSAN�YE MH. KUVAY�� M�LL�YE CD. NO:175/C",AdresTarifiKisa = "�HSAN�YE MAH. ESK� DEVLET HASTANES� KAR�ISI ",AdresTarifi ="�HSAN�YE MAH. ESK� DEVLET HASTANES� KAR�ISI "},
new Eczane { Id = 786,Enlem = 36.7490039,Boylam = 34.5316538,TelefonNo = "3243581130",Adres = "MENDERES MH.MILLI EGEMENLIK CD. MIZRAK 5 AP./B",AdresTarifiKisa = "MEZ�TL� BELED�YE ARKASI TEDA� �DEME NOKTASI KAR�IS",AdresTarifi ="MEZ�TL� BELED�YE ARKASI TEDA� �DEME NOKTASI KAR�ISI/MEZ�TL�"},
new Eczane { Id = 722,Enlem = 36.7708620,Boylam = 34.5621910,TelefonNo = "3245021112",Adres = "GMK BULVARI  570/A KILI� APT.ALTI",AdresTarifiKisa = "SULTA�A OTEL KAR�ISI",AdresTarifi ="GMK BULV. E�R��AM MH. ADNAN �Z�EL�K ASM YANI - SULTA�A OTEL KAR�ISI - DS� B�T����� /YEN��EH�R"},
new Eczane { Id = 754,Enlem = 36.7569900,Boylam = 34.5351300,TelefonNo = "3243571413",Adres = " YENI MH.33180 SK. ESKI APT.ALTI NO:23/A MEZ�TL�",AdresTarifiKisa = "MEZ�TL� K�TAPSAN YUKARISI (DA�A DO�RU 300MT) ",AdresTarifi ="MEZ�TL� K�TAPSAN YUKARISI (DA�A DO�RU 300 MT) 1 NOLU ASM BATI KAPISI MEZ�TL�"},
new Eczane { Id = 695,Enlem = 36.7896146,Boylam = 34.5870556,TelefonNo = "3243266690",Adres = "G�VENEVLER MAHALLESI 20. CADDE NO.19B/2 YENISEHIR MERSIN",AdresTarifiKisa = " �ZEL MERS�N FORUM YA�AM HASTANES� KAR�ISI",AdresTarifi ="G�VENEVLER MH. 20 CD. NO:19 B/2 �ZEL MERS�N FORUM YA�AM HASTANES� KAR�ISI/YEN��EH�R"},
new Eczane { Id = 787,Enlem = 36.7344050,Boylam = 34.5132440,TelefonNo = "3242338385",Adres = "Akdeniz Mh. 39608 Sk. �ukurova apt.  Zemin kat No:8 Mezitli",AdresTarifiKisa = " SOL� CENTER ARKASI MEZ�TL�",AdresTarifi ="AKDEN�Z MH. SOL� CENTER ARKASI - B�M MARKET SOKA�I -�UKUROVA APT. 2 NOLU ASM C�VARI - MEZ�TL� ?"},
new Eczane { Id = 518,Enlem = 36.8452240,Boylam = 34.6335750,TelefonNo = "3242293713",Adres = "CUMHURIYET MAH.M.FEVZI �AKMAK CAD.NO:96 TOROSLAR/MERSIN",AdresTarifiKisa = "YALINAYAK SA�LIK OCA�I C�VARI/TOROSLAR",AdresTarifi ="YALINAYAK MH. M.FEVZ� �AKMAK CD. YALINAYAK SA�LIK OCA�I C�VARI/TOROSLAR"},
new Eczane { Id = 621,Enlem = 36.7990240,Boylam = 34.6267150,TelefonNo = "3242333656",Adres = "�ANKAYA MAH.123.CAD. 11/A AKDENIZ/MERSIN",AdresTarifiKisa = "ZEYT�NL�BAH�E CD B�T�M� �ST�KLAL CD KAV�A�I/AKDEN�",AdresTarifi ="ZEYT�NL�BAH�E CD B�T�M� �ST�KLAL CD KAV�A�I/AKDEN�Z"},
new Eczane { Id = 788,Enlem = 36.7487271,Boylam = 34.5294383,TelefonNo = "3243584910",Adres = "MEZITLI IL�ESI MERKEZ MH. 52007 SK. ONURSAL AP. NO:10/C",AdresTarifiKisa = "MEZ�TL� BELED�YES� KAR�ISI 2 NOLU ASM C�VARI",AdresTarifi ="MEZ�TL� BELED�YES� KAR�ISI 2 NOLU SA�LIK OCA�I C�VARI/MEZ�TL�"},
new Eczane { Id = 600,Enlem = 36.8058330,Boylam = 34.6228810,TelefonNo = "3243364432",Adres = "KUVAYI MILLIYE CAD.BILLUR APT.ALTI 13/4 AKDENIZ MERSIN",AdresTarifiKisa = "HASTANE CD. KURU�E�ME C�VARI/AKDEN�Z",AdresTarifi ="HASTANE CD. KURU�E�ME C�VARI/AKDEN�Z"},
new Eczane { Id = 574,Enlem = 36.8051040,Boylam = 34.6322360,TelefonNo = "3242393272",Adres = "AKDENIZ IL�ESI YENI MH.5315 NO:15/B MERSIN",AdresTarifiKisa = "YEN� MH.BA�KUR �L M�D. ARKASI ",AdresTarifi ="YEN� MH.BA�KUR �L M�D. ARKASI YEN� MH.SA�LIK OCA�I YANI/AKDEN�Z"},
new Eczane { Id = 696,Enlem = 36.7833500,Boylam = 34.5920150,TelefonNo = "3243367852",Adres = "G�venevler Mh. 1908 Sk. �mit Apt. A Blok Zemin Kat No:2",AdresTarifiKisa = "POZCU �ZEL YEN��EH�R HASTANES� ARKASI ",AdresTarifi ="POZCU �ZEL YEN��EH�R HASTANES� ARKASI - FORUM AVM KAR�ISI - YEN��EH�R"},
new Eczane { Id = 519,Enlem = 36.8395810,Boylam = 34.6304610,TelefonNo = "3242232104",Adres = "G�NEYKENT MAH.FARABI CAD.BELEDIYE �ARSISI NO.1 TOROSLAR",AdresTarifiKisa = "G�NEYKENT SA�LIK OCA�I YANI",AdresTarifi ="G�NEYKENT SA�LIK OCA�I YANI / G�NEYKENT MERKEZ CAM�� KAR�ISI / TOROSLAR"},
new Eczane { Id = 721,Enlem = 36.7911858,Boylam = 34.5658996,TelefonNo = "3243412545",Adres = "MENTES MAH.25137 SOK.MIKDAT APT.ALTI NO:7/B YENISEHIR-MERSIN",AdresTarifiKisa = "METRO MARKET BATISI MO�L PETROL SOKA�I",AdresTarifi ="MENTE� MH. METRO GROSS MARKET (3. �EVREYOLU �ZER�) BATISI MO�L PETROL SOKA�I/YEN��EH�R"},
new Eczane { Id = 697,Enlem = 36.7901100,Boylam = 34.5876800,TelefonNo = "3243272275",Adres = "G�VENEVLER MAHALLESI 20 CADDE 1951 SOKAK NO.27 YENISEHIR MERSIN",AdresTarifiKisa = "FORUM YA�AM HASTANES� AC�L KAR�ISI/YEN��EH�R",AdresTarifi ="G�VENEVLER MH.1951 SK.FORUM YA�AM HASTANES�AC�L KAR�ISI/YEN��EH�R"},
new Eczane { Id = 698,Enlem = 36.7860530,Boylam = 34.5934990,TelefonNo = "3243263606",Adres = "G�VENEVLER MAH 1.CAD.NO.5/B YENISEHIR",AdresTarifiKisa = "G�VEN S�TES� C�VARI 4 NOLU SA�LIK OCA�I KAR�ISI",AdresTarifi ="G�VENEVLER MH. G�VEN S�TES� C�VARI - POZCU �ET�NKAYA KAR�ISI - 4 NOLU SA�LIK OCA�I KAR�ISI/YEN��EH�R"},
new Eczane { Id = 755,Enlem = 36.7604590,Boylam = 34.5439300,TelefonNo = "3243594747",Adres = " ATAT�RK MH. 31118 SK. BU�RA �AT APT. NO:10/B  MEZ�TL�",AdresTarifiKisa = " MED�KALPARK HASTANES� ARKASI -HAL CAD. MEZ�TL�",AdresTarifi ="ATAT�RK MH.  MED�KALPARK HASTANES� ARKASI - HAL CAD.  MEZ�TL�"},
new Eczane { Id = 550,Enlem = 36.8128620,Boylam = 34.6135070,TelefonNo = "3243200208",Adres = "MERSIN TOROSLAR IL�ESI OSMANIYE MH. 81034 SK. NO:33",AdresTarifiKisa = "OSMAN�YE MH. 81034 SK. NO:33 /AKDEN�Z",AdresTarifi ="OSMAN�YE MH. 81034 SK. NO:33 /AKDEN�Z"},
new Eczane { Id = 601,Enlem = 36.8098430,Boylam = 34.6205320,TelefonNo = "3243371975",Adres = "IHSANIYE MAHALLE KUVAIMILLIYE CADDE NO.185 ",AdresTarifiKisa = "ESK� DEVLET HASTANES� KAR�ISI",AdresTarifi ="ESK� DEVLET HASTANES� KAR�ISI NO:185/A/AKDEN�Z"},
new Eczane { Id = 676,Enlem = 36.7926940,Boylam = 34.5828189,TelefonNo = "5464549537",Adres = "AKADEM� HASTANES� YANI MENTE� MH. BARBAROS BULV. G�KSU PARK APT. NO:107 C  YEN��EH�R",AdresTarifiKisa = " �ZEL AKADEM� HASTANES� YANI",AdresTarifi ="MENTE� MAH. BARBAROS CADDES� G�KSU PARK APT. �ZEL AKADEM� HASTANES� YANI   YEN��EH�R"},
new Eczane { Id = 627,Enlem = 36.7905990,Boylam = 34.6224220,TelefonNo = "3242380863",Adres = "K�LT�R MH. ATAT�RK CD. �AMLIBEL APT. ALTI NO: 1 64/D",AdresTarifiKisa = "K�LT�R MH. ATAT�RK CD. S�STEM TIP MERKEZ� YANI  ",AdresTarifi ="K�LT�R MH. ATAT�RK CD. S�STEM TIP MERKEZ� YANI �AMLIBEL/AKDEN�Z"},
new Eczane { Id = 612,Enlem = 36.8225170,Boylam = 34.6376870,TelefonNo = "3242342566",Adres = "G�NE� MH. 5822 SK. NO: 6 SELAHATT�N EYY�B� ASM KAR�ISI/AKDEN�Z",AdresTarifiKisa = " SELAHATT�N EYY�B� ASM KAR�ISI/AKDEN�Z",AdresTarifi =" SELAHATT�N EYY�B� ASM KAR�ISI/AKDEN�Z"},
new Eczane { Id = 714,Enlem = 36.7853980,Boylam = 34.5400690,TelefonNo = "3245021100",Adres = "��FTL�KK�Y MH. M�MAR S�NAN CD. PARAD�SE HOMES S�TES�C BLOK NO:7-8-9",AdresTarifiKisa = "MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�IS",AdresTarifi ="��FTL�KK�Y MH. M�MAR S�NAN CD. (MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI) /YEN��EH�R"},
new Eczane { Id = 766,Enlem = 36.7491943,Boylam = 34.5424573,TelefonNo = "5530761803",Adres = "V�RAN�EH�R MAH. CENG�Z TOPEL CAD. 34323 SK. Z.KAT  NO:19-7A",AdresTarifiKisa = "V�RAN�EH�R MAH. ALANYA 2 FIRINI KAR�I SOKA�I",AdresTarifi ="V�RAN�EH�R MAH. CENG�Z TOPEL CAD. 34323 SOK. ALANYA 2 FIRINI KAR�I SOKA�I V�RAN�EH�R ASM C�VARI "},
new Eczane { Id = 544,Enlem = 36.8140820,Boylam = 34.6289040,TelefonNo = "3243223346",Adres = "Sa�l�k Mh. 86046 Sk. A Blok Bina No: 10/9 ",AdresTarifiKisa = "�EV�KKUVVET C�VARI TOROSLAR SA�LIK ASM YANI ",AdresTarifi ="�EV�KKUVVET C�VARI TOROSLAR SA�LIK ASM YANI (SA�LIK MH. 86046 SK.)"},
new Eczane { Id = 784,Enlem = 36.7481541,Boylam = 34.5429888,TelefonNo = "3243589090",Adres = "V�RAN�EH�R MH. 34321 SK. KILI� 2 APT. NO:8/H MEZ�TL�",AdresTarifiKisa = " V�RAN�EH�R MH. KILI� 2 APT. V�RAN�EH�R ASM C�VARI",AdresTarifi ="V�RAN�EH�R MH. 34321 SK. KILI� 2 APT. V�RAN�EH�R ASM C�VARI MEZ�TL�"},
new Eczane { Id = 783,Enlem = 36.7354200,Boylam = 34.5122000,TelefonNo = "3245021620",Adres = "Akdeniz Mh. Ya�ar do�u Cd. no:14/A",AdresTarifiKisa = "SOL� CENTER C�VARI CARREFOURSA YANI PLAT�N PLAZA 2",AdresTarifi ="AKDEN�Z MH. YA�AR DO�U CD.  SOL� CENTER C�VARI CARREFOURSA YANI PLAT�N PLAZA 2 ALTI/MEZ�TL�"},
new Eczane { Id = 725,Enlem = 36.7644110,Boylam = 34.5481630,TelefonNo = "3243587375",Adres = "ATAT�RK MH. 31039 SK. DAMLA S�TES� A BLOK NO:10/A",AdresTarifiKisa = "�N�VERS�TE YOLU �ZEL ORTADO�U HASTANES� KUZEY YANI",AdresTarifi ="�N�VERS�TE YOLU �ZEL ORTADO�U HASTANES� KUZEY YANI MEZ�TL�"},
new Eczane { Id = 547,Enlem = 36.8194000,Boylam = 34.6090400,TelefonNo = "3243213879",Adres = "ZEK� AYAN MH. OKAN MERZEC� BULVARI NO:347/A",AdresTarifiKisa = "ZEK� AYAN MH. ANITTAN MODERN TIP MERKEZ�NE DO�RU ",AdresTarifi ="ZEK� AYAN MH. H. OKAN MERZEC� BULVARI ANITTAN MODERN TIP MERKEZ�NE DO�RU 100 MT. �LER�S� TOROSLAR/MERS�N"},
new Eczane { Id = 774,Enlem = 36.7347070,Boylam = 34.5137280,TelefonNo = "3245022156",Adres = "AKDEN�Z MAH. 39606 SOK. �ZG�L 4 APT. 7/B MEZ�TL� MERS�N",AdresTarifiKisa = "SOL� CENTER C�V. 2 NOLU SA�LIK OCA�I KAR�I �APRAZI",AdresTarifi ="SOL� CENTER C�VARI 2 NOLU SA�LIK OCA�I KAR�I �APRAZI - MEZ�TL�"},
new Eczane { Id = 602,Enlem = 36.7940191,Boylam = 34.6157456,TelefonNo = "3242379666",Adres = "�ST�KLAL CAD.  TURGUT RE�S MAH. �ALK APT. ALTI NO: 194/ B AKDEN�Z",AdresTarifiKisa = "TURGUTRE�S MAH. �ST�KLAL CAD. IMC HASTANES� YANI ",AdresTarifi ="TURGUTRE�S MAH. �ST�KLAL CAD. IMC HASTANES� YANI AKDEN�Z"},
new Eczane { Id = 530,Enlem = 36.8254260,Boylam = 34.6228100,TelefonNo = "3245022838",Adres = "KURDAL� MH. MERS�NL� AHMET BULV. NO:59/A",AdresTarifiKisa = " MERS�NL� AHMET ASM YANI TOROSLAR ",AdresTarifi ="KURDAL� MH. MERS�NL� AHMET BULV. NO:59 MERS�NL� AHMET ASM YANI TOROSLAR "},
new Eczane { Id = 705,Enlem = 36.7807420,Boylam = 34.5591400,TelefonNo = "3245022216",Adres = "BATIKENT MH. 2652 SK. NO:16",AdresTarifiKisa = "NECAT� BOLKAN �.�.OKULU C�VARI ",AdresTarifi ="BATIKENT MH. �AH�N TEPES� - NECAT� BOLKAN �.�.OKULU C�VARI  - MEVLANA ASM 100 MT. DO�U"},
new Eczane { Id = 712,Enlem = 36.7917630,Boylam = 34.5647660,TelefonNo = "3243412140",Adres = "Mente� Mah. 25139 Sk. No:19/A Yeni�ehir/Mersin",AdresTarifiKisa = "MENTE� A�LE SA�LI�I MERKEZ� �APRAZI",AdresTarifi ="MENTE� A�LE SA�LI�I MERKEZ� �APRAZI"},
new Eczane { Id = 732,Enlem = 36.7639840,Boylam = 34.5472600,TelefonNo = "8502810315",Adres = "�N�VERS�TE CAD. ORTADO�U HASTANES� YANI VAL� �ENOL ENG�N CD. EM�RGAN APT. ALTI NO:19 ",AdresTarifiKisa = "�N�VERS�TE CAD. ORTADO�U HASTANES� YANI",AdresTarifi ="�N�VERS�TE CAD. ORTADO�U HASTANES� YANI"},
new Eczane { Id = 729,Enlem = 36.7569200,Boylam = 34.5351940,TelefonNo = "3243585857",Adres = "YEN� MH. 33180 SK. AKKOYUNLU APT. ALTI 21/B ",AdresTarifiKisa = "MEZ�TL� K�TAPSAN YUKARISI 1 NOLU ASM BATI KAPISI ",AdresTarifi ="MEZ�TL� K�TAPSAN YUKARISI (DA�A DO�RU 300 MT) 1 NOLU ASM BATI KAPISI MEZ�TL�"},
new Eczane { Id = 748,Enlem = 36.7638820,Boylam = 34.5471730,TelefonNo = "3243573808",Adres = "VAL� �ENOL ENG�N CD. 31067 SK. REYHAN APT. NO:15/A",AdresTarifiKisa = "�N�VERS�TE CADDES� ORTADO�U HASTANES� KAR�ISI",AdresTarifi ="�N�VERS�TE CADDES� ORTADO�U HASTANES� KAR�ISI"},
new Eczane { Id = 646,Enlem = 36.7945067,Boylam = 34.6004964,TelefonNo = "3249995408",Adres = "H�RR�YET MAH. 1742 SOK. NO:39/C",AdresTarifiKisa = "2. �EVREYOLU �ZER�NDEK� M�GROS ARKASI ",AdresTarifi ="2. �EVREYOLU �ZER�NDEK� M�GROS ARKASI H�RR�YET ASM �APRAZI"},
new Eczane { Id = 636,Enlem = 36.8235510,Boylam = 34.6535490,TelefonNo = "3242343477",Adres = "HAL MH. 6040 SK. NO:3 AKDEN�Z / MERS�N",AdresTarifiKisa = "YEN� SEBZE HAL� C�VARI, TOPTANCILAR S�TES� KAR�ISI",AdresTarifi ="YEN� SEBZE HAL� C�VARI, TOPTANCILAR S�TES� KAR�ISI"},
new Eczane { Id = 701,Enlem = 36.7855540,Boylam = 34.5399860,TelefonNo = "3245023779",Adres = "��FTL�KK�Y MAH. M�MAR S�NAN CAD. PARAD�SE HOMES S�TES� D BLOK NO:24 D/F",AdresTarifiKisa = "MERS�N �N�. TIP FAK. HASTANES� AC�L KAR�ISI",AdresTarifi ="��FTL�KK�Y MH. M�MAR S�NAN CD. (MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� AC�L KAR�ISI) /YEN��EH�R"},
new Eczane { Id = 515,Enlem = 36.8491080,Boylam = 34.6103740,TelefonNo = "3243266868",Adres = "�A�DA�KENT MH. 93130 SK. NO:1",AdresTarifiKisa = "�A�DA�KENT MH. 93130 SK. �EH�R HASTANES� C�VARI",AdresTarifi ="�A�DA�KENT MH. 93130 SK. �EH�R HASTANES� C�VARI"},
new Eczane { Id = 709,Enlem = 36.7854270,Boylam = 34.5404510,TelefonNo = "3245024373",Adres = "��FTL�KK�Y MH. M�MAR S�NAN CD. 20/F",AdresTarifiKisa = "MERS�N �N�VERS�TES� TIP FAK�L. HASTANES� KAR�ISI",AdresTarifi ="MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI YEN��EH�R"},
new Eczane { Id = 752,Enlem = 36.7229513,Boylam = 34.4931215,TelefonNo = "3244813432",Adres = " 75. YIL MH. 40036 SK. NO:1 MEZ�TL�",AdresTarifiKisa = "50. YIL MH. 5 NOLU SA�LIK OCA�I KAR�ISI DAVULTEPE",AdresTarifi ="50. YIL MH. 5 NOLU SA�LIK OCA�I KAR�ISI DAVULTEPE/MEZ�TL�"},
new Eczane { Id = 511,Enlem = 36.8489490,Boylam = 34.6104770,TelefonNo = "3242230888",Adres = "�A�DA�KENT MH. 93130 SK. NO: 1/A",AdresTarifiKisa = "�A�DA�KENT MH. 93130 SK. �EH�R HASTANES� C�VARI",AdresTarifi ="�A�DA�KENT MH. 93130 SK. �EH�R HASTANES� C�VARI"},
new Eczane { Id = 747,Enlem = 36.7519830,Boylam = 34.5362660,TelefonNo = "3249994565",Adres = "YEN� MH. 33163 SK. G�L CD. MIZRAK 26 APT. NO:24 MEZ�TL�",AdresTarifiKisa = "C�TY HOSP�TAL (ESK� DUYGU TIP MERKZ) ARKA �APRAZI",AdresTarifi ="YEN� MH. 33163 SK. C�TY HOSP�TAL (ESK� DUYGU TIP MERKEZ�) ARKA �APRAZI MEZ�TL�"},
new Eczane { Id = 534,Enlem = 36.8313860,Boylam = 34.6047170,TelefonNo = "3243201410",Adres = "�AVU�LU MH. 89031 SK. NO:22 TOROSLAR",AdresTarifiKisa = "�AVU�LU MH. YELDE��RMENL� PARK SOKA�I TOROSLAR",AdresTarifi ="�AVU�LU MH. YELDE��RMENL� PARK SOKA�I TOROSLAR  4 NOLU SA�LIK OCA�I YANI"},
new Eczane { Id = 763,Enlem = 36.7515210,Boylam =  34.521605,TelefonNo = "5354270606",Adres = "MERKEZ MH.FINDIKPINARI CD. NO:46/C MEZ�TL�",AdresTarifiKisa = "KUYULUK YOLU B�M MARKET KAR�ISI EMRE KASABI YANI ",AdresTarifi ="KUYULUK YOLU B�M MARKET KAR�ISI EMRE KASABI YANI MEZ�TL�"},
new Eczane { Id = 770,Enlem = 36.7161290,Boylam = 34.4778320,TelefonNo = "3244815009",Adres = "Davultepe Mah. Kaz�m Karabekir Cad. No:6/A",AdresTarifiKisa = "DAVULTEPE ASM KAR�ISI",AdresTarifi ="DAVULTEPE ASM KAR�ISI"},
new Eczane { Id = 685,Enlem = 36.7839950,Boylam = 34.5800560,TelefonNo = "3249998958",Adres = "BARBAROS MAH. BARBAROS BULV. 2106 SOK. M�LENYUM 3 APT. NO:1 YEN��EH�R",AdresTarifiKisa = "AYDINLIKEVLER ��O 300M KUZEY� BARBAROS BUL.�ST�NDE",AdresTarifi ="AYDINLIKEVLER �LKOKULUNUN 300 MT KUZEY� BARBAROS BULVARI �ZER�"},
new Eczane { Id = 688,Enlem = 36.7846870,Boylam = 34.5798810,TelefonNo = "3243271171",Adres = "Barbaros Mah.2106 Sk. No:8/3  YEN��EH�R ",AdresTarifiKisa = "ADNAN �Z�EL�K ORTAOKULU �APRAZI ",AdresTarifi ="ADNAN �Z�EL�K ORTAOKULU �APRAZI YEN��EH�R 1 NOLU ASM KAR�ISI (GROSER� �APRAZ SOKA�I)"},
new Eczane { Id = 702,Enlem = 36.7792390,Boylam = 34.5387720,TelefonNo = "3245024439",Adres = "��FTL�KK�Y MAH. 32133 SK. �AK�R SON CAD. NO:6/C YEN��EH�R",AdresTarifiKisa = "ESK� ��FTL�KK�Y YOLU YEN��EH�R 3 NOLU ASM KAR�ISI",AdresTarifi ="ESK� ��FTL�KK�Y YOLU - A 101 YANI - YEN��EH�R 3 NOLU ASM KAR�ISI"},
new Eczane { Id = 542,Enlem = 36.8195300,Boylam = 34.6151200,TelefonNo = "3243212437",Adres = "TOZKOPARAN MH. 87045 SK.NO:21/A-B",AdresTarifiKisa = "�UKUROVA �LKOKULU VE  TOZKOPARAN ASM KAR�ISI",AdresTarifi ="TOZKOPARAN MH. 87045 SK. �UKUROVA �LKOKULU VE  TOZKOPARAN ASM KAR�ISI"},
new Eczane { Id = 648,Enlem = 36.7884967,Boylam = 34.6055884,TelefonNo = "3243259800",Adres = "CUMHUR�YET MH. 1617 SK. MEHMETO�LU CD. NO:3/A-13 YEN��EH�R",AdresTarifiKisa = "CUMHUR�YET MAH. C��ERC� HAKAN ARKASI",AdresTarifi ="�SMET �N�N� BULV. CUMHUR�YET MH. C��ERC� HAKAN VE DONAT T�CARET ARKASI YEN��EH�R"},
new Eczane { Id = 501,Enlem = 36.8503990,Boylam = 34.6100370,TelefonNo = "3242233390",Adres = "KORUKENT MH. 96013 SK. NO:13/1 TOROSLAR",AdresTarifiKisa = " MERS�N �EH�R HASTANES� C�VARI TOK� EVLER� KAR�ISI",AdresTarifi ="KORUKENT MH. 96013 SK. MERS�N �EH�R HASTANES� C�VARI TOK� EVLER�, KAR�ISI TOK� CAM�� �ZER� TOROSLAR"},
new Eczane { Id = 672,Enlem = 36.7789940,Boylam = 34.5752190,TelefonNo = "3243268666",Adres = "BARBOROS MH. 2148 SK. MEHMET EM�N G�K S�TES� D BLOK NO:1/A",AdresTarifiKisa = "YEN��EH�R KAYMAKAMLI�I KAR�ISI BARBAROS ASM YANI",AdresTarifi ="YEN��EH�R KAYMAKAMLI�I KAR�ISI BARBAROS �OCUK PARKI ARKASI 15 NOLU SA�LIK OCA�I YANI/YEN��EH�R"},
new Eczane { Id = 690,Enlem = 36.7900460,Boylam = 34.5877410,TelefonNo = "3243257347",Adres = "G�VENEVLER MH. 1990 SK. NO:8B",AdresTarifiKisa = "FORUM YA�AM HASTANES� AC�L YANI YEN��EH�R",AdresTarifi ="FORUM YA�AM HASTANES� AC�L YANI YEN��EH�R"},
new Eczane { Id = 734,Enlem = 36.7597774,Boylam = 34.5444900,TelefonNo = "3243599250",Adres = "ATAT�RK MH. 31118 SK. EMEK APT. NO:6/A MEZ�TL�",AdresTarifiKisa = " GMK BULV. MED�CALPARK HASTANES� YANI MEZ�TL�",AdresTarifi ="GMK BULV. ATAT�RK MH.  MEZ�TL� MED�KAL PARK HASTANES� YANI DERYA OTOMOT�V HYUNDA� KAR�ISI"},
new Eczane { Id = 735,Enlem = 36.7585338,Boylam = 34.5435113,TelefonNo = "3245024444",Adres = "ATAT�RK MH. HAL CD. NO:3-F  MEZ�TL�",AdresTarifiKisa = "MED�KALPARK HASTANES� AC�L HAKR�ISI",AdresTarifi ="MEZ�TL� ATAT�RK MAH. HAL CAD. MED�KALPARK HASTANES� AC�L KAR�ISI - MEZ�TL�"},
new Eczane { Id = 772,Enlem = 36.7323574,Boylam =  34.504196,TelefonNo = "3245023303",Adres = "AKDEN�Z MAH. 39760 SOK .NO:4/26 MEZ�TL�",AdresTarifiKisa = "MARTI OTEL KUZEY� M.AK�F ERSOY ASM KAR�ISI MEZ�TL�",AdresTarifi ="SAH�L MARTI OTEL I�IKLARI KUZEY� �EH�T FAT�H SOYDAN ORTAOKULU DO�USU MEHMET  AK�F ERSOY SA�LIK OCA�I KAR�ISI MEZ�TL�"},
new Eczane { Id = 565,Enlem = 36.8063380,Boylam = 34.6328458,TelefonNo = "3242375663",Adres = "YEN� MAH. 5328 SOK. NO:19/D AKDEN�Z",AdresTarifiKisa = "YEN� MH. TOROS DEVLET HASTANES� �TFA�YE �APRAZI ",AdresTarifi ="YEN� MH. TOROS DEVLET HASTANES� �TFA�YE �APRAZI �INAR TANTUN� YANI AKDEN�Z"},
new Eczane { Id = 764,Enlem = 36.7488434,Boylam = 34.5313990,TelefonNo = "3249337207",Adres = "V�RAN�EH�R MAH. V�RAN�EH�R CAD. YEN� CEMRE S�T. A BLOK NO:16/C",AdresTarifiKisa = "MEZ�TL� BELED�YE ARKASI HASAN USTA TANTUN� KAR�ISI",AdresTarifi ="MEZ�TL� BELED�YE ARKASI HASAN USTA TANTUN� KAR�ISI"},
new Eczane { Id = 771,Enlem = 36.7467232,Boylam = 34.5306394,TelefonNo = "3243369383",Adres = "MENDERES MH. 35427 SK. TEMEL S�TES� NO:3/25-26-27  MEZ�TL�",AdresTarifiKisa = "MEZ�TL� BELED�YE ARKASI - MEZ�TL� ZABITA SOKA�I",AdresTarifi ="MENDERES MH. 35427 SK. MEZ�TL� BELED�YE ARKASI - MEZ�TL� ZABITA SOKA�I - 15 TEMMUZ �EH�TLER� ORTAOKULU KUZEY�"},
new Eczane { Id = 506,Enlem = 36.8329375,Boylam = 34.6195625,TelefonNo = "5419739313",Adres = "�A�DA�KENT MH. GAZ� OSMAN PA�A BULV. 93007 SK. NO:4/A  TOROSLAR",AdresTarifiKisa = "�A�DA�KENT MH. GAZ� OSMAN PA�A ASM YANI",AdresTarifi ="�A�DA�KENT MH. GAZ� OSMAN PA�A ASM YANI CELLO �E�MES� BATISI - TOROSLAR"},
new Eczane { Id = 723,Enlem = 36.7854310,Boylam = 34.5402370,TelefonNo = "3245021516",Adres = "��FTL�KK�Y MAH. M�MAR S�NAN CAD. PARAD�SE HOMES S�T. D BLOK NO:5 ",AdresTarifiKisa = "MERS�N �N�VERS�TES� TIPFAK�LTES� HASTANES� KAR�ISI",AdresTarifi ="��FTL�KK�Y MH. M�MAR S�NAN CD. (MERS�N �N�VERS�TES� TIP FAK�LTES� HASTANES� KAR�ISI) /YEN��EH�R"},
new Eczane { Id = 586,Enlem = 36.8083191,Boylam =  34.617970,TelefonNo = "3243373034",Adres = "�HSAN�YE MAH. HAVUZLAR CAD. NO:80/A-122",AdresTarifiKisa = "KARAYOLLARI KAV�. SU HAST. YANI T�P BEBEK KAR�ISI",AdresTarifi ="KARAYOLLARI KAV�A�I SU HASTANES� YANI T�P BEBEK KAR�ISI"},
new Eczane { Id = 793,Enlem = 36.7902624,Boylam =  34.588063,TelefonNo = "3240000000",Adres = "BAH�EL�EVLER MH. 16. CD. I�IK APT. ALTI NO:57/A YENI�EHIR",AdresTarifiKisa = "BAH�EL�EVLER MH. 16. CD. TOROS �NIVERSITESI YOLU",AdresTarifi ="BAH�EL�EVLER MH. 16. CD. TOROS �NIVERSITESI YENI�EHIR KAMP�S� YOLU"},
new Eczane { Id = 796,Enlem = 36.7597494,Boylam = 34.5437798,TelefonNo = "3243574737",Adres = "ATAT�RK MH. 31118. SK. �ER� �AT. APT. ALTI NO:13  MEZ�TL�",AdresTarifiKisa = "MED�CALPARK HASTANES� YANI - MEZ�TL�",AdresTarifi ="ATAT�RK MH. 31118. SK. �ER� �AT. APT. ALTI MED�CALPARK HASTANES� YANI - MEZ�TL�"},
new Eczane { Id = 797,Enlem = 36.7602294,Boylam = 34.5419797,TelefonNo = "5073137171",Adres = "ATAT�RK MH. 31118 SK. BU�RA �AT APT. ALTI 10/A MEZITLI",AdresTarifiKisa = "MEZ�TL� MED�KAL PARK HASTANES� YANI ",AdresTarifi ="ATAT�RK MH.  MEZ�TL� MED�KAL PARK HASTANES� YANI DERYA OTOMOT�V HYUNDA� KAR�ISI"},
new Eczane { Id = 795,Enlem = 36.7323850,Boylam = 34.5044080,TelefonNo = "3249990030",Adres = "AKDEN�Z MH. 39709 SK. HEK�MO�LU APT. NO:5/B  MEZ�TL�",AdresTarifiKisa = "AKDEN�Z MH. MEHMET  AK�F ERSOY ASM KAR�ISI MEZ�TL�",AdresTarifi ="SAH�L MARTI OTEL I�IKLARI KUZEY� �EH�T FAT�H SOYDAN ORTAOKULU DO�USU MEHMET  AK�F ERSOY SA�LIK OCA�I KAR�ISI MEZ�TL�"},
new Eczane { Id = 789,Enlem = 36.7902359,Boylam = 34.5971049,TelefonNo = "3249998133",Adres = "BAH�EL�EVLER MH. 16. CD. NO:55/E",AdresTarifiKisa = "TOROS �N�VERS�TES� 45 EVLER KAMP�S� C�VAR CADDES� ",AdresTarifi ="T�RK TELEKOM YUKARISI TOROS �N�VERS�TES� 45 EVLER KAMP�S� C�VAR CADDES� ET�� LOKANTASI KAR�ISI"},
new Eczane { Id = 791,Enlem = 36.7926835,Boylam = 34.5938196,TelefonNo = "3243312120",Adres = "BAH�EL�EVLER MH. H�SEY�N OKAN MERZEC� BULV. 466/B",AdresTarifiKisa = "BAH�EL�EVLER MH. DEMOKRAS� KAV.�IKI�I KOSGEB YANI ",AdresTarifi ="BAH�EL�EVLER MAH. DEMOKRAS� KAV�A�I �IKI�I MEZ�TL� YOLU �ZER� KOSGEB B�NASI YANI"},
new Eczane { Id = 790,Enlem = 36.8182237,Boylam = 34.6264595,TelefonNo = "3243214403",Adres = "SEL�UKLAR MAH. 206. CAD. NO:117/C",AdresTarifiKisa = "ESK� FEN L�SES� DURA�I KAR�ISI (AKABE CAM� SOKA�I)",AdresTarifi ="ESK� FEN L�SES� DURA�I KAR�ISI (AKABE CAM� SOKA�I)"},
new Eczane { Id = 799,Enlem = 36.7593542,Boylam = 34.5433423,TelefonNo = "3245020009",Adres = "ATAT�RK MAH. HAL CAD. 31089 SOK NO:3/C",AdresTarifiKisa = "MED�CAL PARK HASTANES� AC�L KAR�ISI",AdresTarifi ="MED�CAL PARK HASTANES� AC�L KAR�ISI"},
new Eczane { Id = 798,Enlem = 36.7593542,Boylam = 34.5433423,TelefonNo = "3245020018",Adres = "ATAT�RK MAH. HAL CAD. 31089 SOK. NO:3/D",AdresTarifiKisa = "MED�CAL PARK HASTANES� AC�L KAR�ISI",AdresTarifi ="MED�CAL PARK HASTANES� AC�L KAR�ISI"},
new Eczane { Id = 803,Enlem = 36.7789507,Boylam = 34.5384343,TelefonNo = "3245023377",Adres = "��FTL�KK�Y MH. 32133 SK. NO:6/B",AdresTarifiKisa = "��FTL�KK�Y A�LE SA�LI�I MERKEZ� KAR�ISI A101 YANI",AdresTarifi ="��FTL�KK�Y A�LE SA�LI�I MERKEZ� KAR�ISI A101 YANI"},
new Eczane { Id = 792,Enlem = 36.7936806,Boylam = 34.6011301,TelefonNo = "3243265363",Adres = "H�RR�YET MH. 1742 SK. SAKARYA APT. ZEM�N KAT NO:35/A  YENI�EHIR/MERSIN",AdresTarifiKisa = "H�RRIYET SA�LIK OCA�I YANINDAKI BIM KAR�ISI",AdresTarifi ="DEMOKRASI KAV�A�I �ZERI MIGROS ARKASI H�RRIYET SA�LIK OCA�I YANINDAKI BIM KAR�ISI"},
new Eczane { Id = 818,Enlem = 36.8203125,Boylam = 34.6078738,TelefonNo = "3243200007",Adres = "ZEKI AYAN MAH. KUVAYI MILLIYE CAD. NO:291/A",AdresTarifiKisa = "TOROSLAR ANIT KAV�A�I. AKBANK KAR�ISI",AdresTarifi ="TOROSLAR ANIT KAV�A�I. AKBANK KAR�ISI"},
new Eczane { Id = 820,Enlem = 36.7380625,Boylam = 34.5144988,TelefonNo = "3249991577",Adres = "MERKEZ MAH. 52136 SOK. ��BAL S�TES� C BLOK NO:19 (1E)",AdresTarifiKisa = "SOL� CENTER C�VARI MEZ�TL� 4 NOLU ASM KAR�ISI",AdresTarifi ="SOL� CENTER C�VARI BE�KARDE�LER YAPI YANI MEZ�TL� 4 NOLU ASM KAR�ISI"},


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

            #region mazeret t�rler
            var mazeretTurler = new List<MazeretTur>()
                            {
                                new MazeretTur(){ Adi="�ok �nemli"},
                                new MazeretTur(){ Adi="�nemli"},
                                new MazeretTur(){ Adi="Az �nemli"}
                            };

            context.MazeretTurler.AddOrUpdate(s => new { s.Adi }, mazeretTurler.ToArray());
            //mazeretTurler.ForEach(d => context.MazeretTurler.Add(d));
            context.SaveChanges();
            #endregion

            #region istek t�rler
            var istekTurler = new List<IstekTur>()
                            {
                                new IstekTur(){ Adi="�ok �nemli"},
                                new IstekTur(){ Adi="�nemli"},
                                new IstekTur(){ Adi="Az �nemli"}
                            };

            context.IstekTurler.AddOrUpdate(s => new { s.Adi }, istekTurler.ToArray());
            //istekTurler.ForEach(d => context.IstekTurler.Add(d));
            context.SaveChanges();
            #endregion

            #region mazeretler
            var mazeretler = new List<Mazeret>()
                            {
                                new Mazeret(){ Adi="Sa�l�k", MazeretTurId=1}
                            };

            context.Mazeretler.AddOrUpdate(s => new { s.Adi }, mazeretler.ToArray());
            //mazeretler.ForEach(d => context.Mazeretler.Add(d));
            context.SaveChanges();
            #endregion

            #region istekler
            var istekler = new List<Istek>()
                            {
                                new Istek(){ Adi="S�ral� N�bet", IstekTurId=1},
                                new Istek(){ Adi="Sa�l�k", IstekTurId=1}
                            };

            context.Istekler.AddOrUpdate(s => new { s.Adi }, istekler.ToArray());
            //istekler.ForEach(d => context.Istekler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane n�bet mazeretler
            var eczaneNobetMazeretler = new List<EczaneNobetMazeret>()
            {
                new EczaneNobetMazeret(){ EczaneNobetGrupId=1, MazeretId=1, Aciklama="Deneme", TakvimId=1 },
            };

            context.EczaneNobetMazeretler.AddOrUpdate(s => new { s.EczaneNobetGrupId, s.MazeretId, s.TakvimId }, eczaneNobetMazeretler.ToArray());
            //eczaneNobetMazeretler.ForEach(d => context.EczaneNobetMazeretler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane n�bet istekler
            var eczaneNobetIstekler = new List<EczaneNobetIstek>()
                            {
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="S�rayla", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="S�rayla", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="S�rayla", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="S�rayla", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="S�rayla", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="S�rayla", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="S�rayla", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="S�rayla", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="S�rayla", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="S�rayla", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="S�rayla", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="S�rayla", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="S�rayla", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="S�rayla", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="S�rayla", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="S�rayla", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="S�rayla", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="S�rayla", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="S�rayla", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="S�rayla", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="S�rayla", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="S�rayla", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="S�rayla", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="S�rayla", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="S�rayla", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="S�rayla", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="S�rayla", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="S�rayla", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="S�rayla", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="S�rayla", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="S�rayla", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="S�rayla", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="S�rayla", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="S�rayla", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="S�rayla", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="S�rayla", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="S�rayla", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="S�rayla", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="S�rayla", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="S�rayla", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="S�rayla", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="S�rayla", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="S�rayla", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="S�rayla", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="S�rayla", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="S�rayla", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="S�rayla", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="S�rayla", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="S�rayla", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="S�rayla", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="S�rayla", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="S�rayla", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="S�rayla", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="S�rayla", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="S�rayla", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="S�rayla", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="S�rayla", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="S�rayla", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="S�rayla", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="S�rayla", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="S�rayla", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="S�rayla", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="S�rayla", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="S�rayla", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="S�rayla", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="S�rayla", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="S�rayla", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="S�rayla", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="S�rayla", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="S�rayla", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="S�rayla", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="S�rayla", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="S�rayla", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="S�rayla", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="S�rayla", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="S�rayla", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="S�rayla", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="S�rayla", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="S�rayla", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="S�rayla", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="S�rayla", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="S�rayla", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="S�rayla", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="S�rayla", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="S�rayla", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="S�rayla", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="S�rayla", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="S�rayla", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="S�rayla", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="S�rayla", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="S�rayla", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="S�rayla", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="S�rayla", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="S�rayla", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="S�rayla", TakvimId=665},

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

            #region eczane n�bet muafiyet
            var eczaneNobetMuafiyetler = new List<EczaneNobetMuafiyet>()
                            {
                                new EczaneNobetMuafiyet(){ EczaneId=1, BaslamaTarihi=new DateTime(2018, 2, 1), BitisTarihi=new DateTime(2018, 2, 1).AddDays(30), Aciklama="deneme i�in muaft�r" }
                            };

            context.EczaneNobetMuafiyetler.AddOrUpdate(s => new { s.EczaneId, s.BaslamaTarihi }, eczaneNobetMuafiyetler.ToArray());
            //eczaneNobetMuafiyetler.ForEach(d => context.EczaneNobetMuafiyetler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane n�bet gruplar
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

            #region eczane n�bet sonu� aktifler
            //var v13 = new List<EczaneNobetSonucAktif>()
            //                {
            //                    new EczaneNobetSonucAktif(){ EczaneNobetGrupId=1, TakvimId=1 }
            //                };

            //v13.ForEach(d => context.EczaneNobetSonucAktifler.Add(d));
            //context.SaveChanges();
            #endregion

            #region eczane n�bet sonu�lar
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
                        new NobetSonucDemoTip(){ Adi="Gruplar ba��ms�z. T�m g�nler da��l�m.", Aciklama="Haftan�n herbir g�n�n�n d�zg�n da��t�ld��� ve gruplar aras� herhangi bir ba� kurulmaks�z�n yap�lan ��z�m"},
                        new NobetSonucDemoTip(){ Adi="Gruplar ba��ms�z. Se�ili g�nler da��l�m.", Aciklama="Haftan�n se�ili g�nlerinin d�zg�n da��t�ld��� ve gruplar aras� herhangi bir ba� kurulmaks�z�n yap�lan ��z�m"},
                        new NobetSonucDemoTip(){ Adi="Gruplar ba��ml�. T�m g�nler da��l�m.", Aciklama="Haftan�n t�m g�nlerinin d�zg�n da��t�ld��� ve gruplar aras� ba�lar�n oldu�u ��z�m"},
                        new NobetSonucDemoTip(){ Adi="Gruplar ba��ml�. Se�ili g�nler da��l�m.", Aciklama="Haftan�n se�ili g�nlerinin d�zg�n da��t�ld��� ve gruplar aras� ba�lar�n oldu�u ��z�m"},
                        };

            context.NobetSonucDemoTipler.AddOrUpdate(s => new { s.Adi }, nobetSonucDemoTipler.ToArray());
            context.SaveChanges();
            #endregion

            #region eczane n�bet sonu� demolar
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
                                new EczaneGrupTanimTip(){ Adi="Co�rafi yak�nl�k" },
                                new EczaneGrupTanimTip(){ Adi="E� Durumu" }
                            };

            context.EczaneGrupTanimTipler.AddOrUpdate(s => new { s.Adi }, eczaneGrupTanimTipler.ToArray());
            context.SaveChanges();
            #endregion //

            #region eczane grup tan�mlar
            var eczaneGrupTanimlar = new List<EczaneGrupTanim>()
                            {
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_G�NE�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_N�SA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_TU�BA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_��R�N", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_B�LGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_AYY�CE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_F�L�Z", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_B�KE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_G�NE�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_N�SA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_TU�BA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_��R�N", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_B�LGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_AYY�CE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_F�L�Z", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_B�KE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�KSU_G�NE�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�KSU_SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�KSU_N�SA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�KSU_TU�BA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�KSU_��R�N", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�KSU_B�LGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�KSU_AYY�CE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�KSU_F�L�Z", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�KSU_B�KE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KO�AK_G�NE�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KO�AK_SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KO�AK_N�SA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KO�AK_TU�BA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KO�AK_��R�N", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KO�AK_B�LGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KO�AK_AYY�CE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KO�AK_F�L�Z", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KO�AK_B�KE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_G�NE�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_N�SA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_TU�BA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_��R�N", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_B�LGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_AYY�CE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_F�L�Z", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_B�KE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="HAYAT_SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="S�PAH�O�LU_SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="SELCEN _SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TEZCAN_SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="�EKER_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="�EKER_KASAPO�LU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="�EKER_�AH�N", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="�EKER_SU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="�EKER_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="�EKER_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALA�YE_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALA�YE_KASAPO�LU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALA�YE_�AH�N", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALA�YE_SU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALA�YE_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALA�YE_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�NEYL�O�LU_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�NEYL�O�LU_KASAPO�LU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�NEYL�O�LU_�AH�N", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�NEYL�O�LU_SU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�NEYL�O�LU_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�NEYL�O�LU_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALTUNBA�_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="G�LERY�Z_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_SEV�ND�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_ALTUNBA�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_G�LERY�Z", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_G�NE�", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"}


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

            #region user nobet �st gruplar
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

            #region �ehirler
            var vSehirler = new List<Sehir>()
                            {
                                new Sehir(){ Adi="Antalya", EczaneOdaId=1 }
                            };

            context.Roles.AddOrUpdate(s => new { s.Name }, vRole.ToArray());
            //vSehirler.ForEach(d => context.Sehirler.Add(d));
            context.SaveChanges();

            #endregion

            #region il�eler
            var vIlceler = new List<Ilce>()
                            {
                                new Ilce(){ Adi="Alanya", SehirId=1 },
                                new Ilce(){ Adi="Muratpa�a", SehirId=1 },
                                new Ilce(){ Adi="Konyaalt�", SehirId=1 },
                                new Ilce(){ Adi="Kepez", SehirId=1 }
                            };

            context.Roles.AddOrUpdate(s => new { s.Name }, vRole.ToArray());
            vIlceler.ForEach(d => context.Ilceler.Add(d));
            context.SaveChanges();

            #endregion

            #region eczane il�eler
            //var vEczaneIlceler = new List<EczaneIlce>()
            //                {
            //                   // new EczaneIlce(){  },
            //                };

            //vEczaneIlceler.ForEach(d => context.EczaneIlceler.Add(d));
            //context.SaveChanges();

            #endregion

            //17.09.2018'de eklendi
            #region g�n gruplar

            var gunGruplar = new List<GunGrup>()
                            {
                                new GunGrup(){ Adi="Pazar" },
                                new GunGrup(){ Adi="Bayram" },
                                new GunGrup(){ Adi="Hafta ��i" },

                                new GunGrup(){ Adi="Cumartesi" },

                                new GunGrup(){ Adi="Arife" },
                            };

            context.GunGruplar.AddOrUpdate(s => new { s.Adi }, gunGruplar.ToArray());
            context.SaveChanges();
            #endregion

            #region n�bet �st grup g�n gruplar

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

            #region n�bet �zel g�nler

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

            #region n�bet grup g�n kurallar

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
                {//hafta i�i
                    var nobetUstGrupGunGrup = context.NobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 3);
                    nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
                }
                else if (nobetGunKuralId == 7)
                {
                    if (nobetUstGrupId == 3)
                    {//cumartesi - mersin i�in
                        var nobetUstGrupGunGrup = context.NobetUstGrupGunGruplar.SingleOrDefault(x => x.NobetUstGrupId == nobetUstGrupId && x.GunGrupId == 4);
                        nobetUstGrupGunGrupId = nobetUstGrupGunGrup.Id;
                    }
                    else
                    {//hafta i�i
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

            #region n�bet grup g�rev tip takvim �zel G�nler

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

            #region n�bet �st grup g�n gruplar

            b.EczaneNobetContext.NobetUstGrupGunGruplar.AddOrUpdate(s => new { s.NobetUstGrupId, s.GunGrupId }, b.NobetUstGrupGunGruplar.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion            

            #region n�bet grup g�n kurallar

            NobetGrupGunKuralEkle(b.EczaneNobetContext, b.BaslamaTarihi, b.NobetUstGrupId, b.EczaneNobetContext.NobetGrupGorevTipler.Where(w => w.Id == 61).Select(s => s.Id).ToList(), b.VarsayilanNobetciSayisi);

            #endregion

            #region n�bet grup g�rev tip takvim �zel G�nler

            NobetGrupGorevTipTakvimOzelGunEkle(b.EczaneNobetContext, b.NobetGrupGorevTipId);

            #endregion 

            #region eczane n�bet gruplar

            //buraya dikkat. birden �ok n�bet grubu varsa ay�r�p eklemek laz�m
            var eczaneler = b.EczaneNobetContext.Eczaneler
                .Where(w => w.Id > eczaneIdSon)
                .OrderBy(o => o.Id).ToList();

            var eczaneNobetGruplar = new List<EczaneNobetGrup>();

            var indisEczaneSayisi = 1;

            foreach (var eczane in eczaneler)
            {
                var nobetGrupGorevTipId = b.NobetGrupGorevTipId;

                //birden fazla n�bet grubu olursa ayarla mutlaka

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

            #region n�bet �st grup k�s�tlar

            var nobetUstGrupKisitlar = b.EczaneNobetContext.NobetUstGrupKisitlar
                .Where(w => w.NobetUstGrupId == 2)//antalya - varsay�lan
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

            #region n�bet �st gruplar

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

            #region n�bet gruplar

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

            #region user nobet �st gruplar

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

            #region n�bet grup g�rev tipler

            var nobetGrupGorevTipler = new List<NobetGrupGorevTip>();

            var nobetGrupAdlari = b.NobetGruplar.Select(s => s.Adi);
            var nobetGrupBaslamaTarihileri = b.NobetGruplar.Select(s => s.BaslamaTarihi);

            var nobetGruplar = b.EczaneNobetContext.NobetGruplar
                .Where(w => nobetGrupAdlari.Contains(w.Adi)
                         && nobetGrupBaslamaTarihileri.Contains(w.BaslamaTarihi)).ToList();

            foreach (var nobetGrup in nobetGruplar)
            {
                var gorevTipler = new int[] { 1
                    //, 5 //tam g�n n�bet�i
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

            #region n�bet grup kurallar

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
                        Deger = kural.Id == 1 //Ard���k Bo� G�n Say�s�
                         ? 4
                         : 1 //Varsay�lan g�nl�k n�bet�i say�s�
                    });
                }
            }

            b.EczaneNobetContext.NobetGrupKurallar.AddOrUpdate(s => new { s.NobetGrupGorevTipId, s.NobetKuralId, s.BaslangicTarihi }, b.NobetGrupKurallar.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion

            #region n�bet �st grup g�n gruplar

            b.EczaneNobetContext.NobetUstGrupGunGruplar.AddOrUpdate(s => new { s.NobetUstGrupId, s.GunGrupId }, b.NobetUstGrupGunGruplar.ToArray());
            b.EczaneNobetContext.SaveChanges();

            #endregion            

            #region n�bet grup g�n kurallar

            NobetGrupGunKuralEkle(b.EczaneNobetContext, b.BaslamaTarihi, b.NobetUstGrupId, nobetGrupGorevTipler, b.VarsayilanNobetciSayisi);

            #endregion

            #region n�bet grup g�rev tip takvim �zel G�nler

            foreach (var nobetGrupGorevTip in nobetGrupGorevTiplerSonradanEklenenler)
            {
                NobetGrupGorevTipTakvimOzelGunEkle(b.EczaneNobetContext, nobetGrupGorevTip.Id);
            }

            #endregion 

            #region eczane n�bet gruplar

            //buraya dikkat. birden �ok n�bet grubu varsa ay�r�p eklemek laz�m
            var eczaneler = b.EczaneNobetContext.Eczaneler
                .Where(w => w.Id > eczaneIdSon)
                .OrderBy(o => o.Id).ToList();

            var eczaneNobetGruplar = new List<EczaneNobetGrup>();

            var indisEczaneSayisi = 1;

            foreach (var eczane in eczaneler)
            {
                var nobetGrupGorevTipId = b.NobetGrupGorevTipId;

                //birden fazla n�bet grubu olursa ayarla mutlaka

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

            #region n�bet �st grup k�s�tlar

            var nobetUstGrupKisitlar = b.EczaneNobetContext.NobetUstGrupKisitlar
                .Where(w => w.NobetUstGrupId == 4)//�orum
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
                //cumartesi, pazar hari� oldu�unda �st sat�r a��lacak
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
            {//hafta i�i
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
            //    {//hafta i�i
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
new Eczane{ Adi="ALTA�", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ALTINORDU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ARSLAN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="CANDAN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="CANSUDERE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DEM�R", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DEVA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="D�LEK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="D�R�M", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DO�A", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DURU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ERG�L", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="G�NDO�DU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="G�NE�", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="�BN�S�NA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KATIRCIO�LU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KAYMAK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KO�AK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="N�L�FER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="PARK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="PINAR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="POYRAZ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SANAY�", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="�EYMA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="��M�EK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TUBA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="T�RKMENLER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ACAR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="AYDIN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="BERR�N", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="BOZTEPE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="B�Y�K", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="�ELENK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DEFNE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DEM�RTA�", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DEN�Z", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="DERMAN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="HASTANELER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="HIZIR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="�T�MAT", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KARAMANO�LU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KAYAHAN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="MERKEZ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="MURAT", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ODABA�", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="�M�R", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SA�LIK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SEL�N", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SUBA�I", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TEPE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TUNA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="T�L�N", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="U�UR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="YEN��AR�I", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="YEN�MAHALLE", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="Y�R�R", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ARDA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="BAH�EL�EVLER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="�AKIR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="EL�F", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ESER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ES�N", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="FAT�H", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="FIRAT", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="F�L�Z", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="G�LER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="G�NAYDIN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="G�VEN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KAR�IYAKA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="KO�Y���T", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="MERTGER�EKER", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="NAZ", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="NUR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ONUR", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="PEL�N", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="SERDARO�LU", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="��FA", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="TAYFUN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="T�RKMEN", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="UFUK", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ULUS", AcilisTarihi=new DateTime(2020,4,1)},
new Eczane{ Adi="ZA�MO�LU", AcilisTarihi=new DateTime(2020,4,1)},
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
                                //new User(){ Email="odaDiyarbakir@nobetyaz.com", FirstName="Oda Diyarbak�r", LastName="Oda Diyarbak�r", Password="odaDiyarbakir8", UserName="odaDiyarbakir", BaslamaTarihi = baslamaTarihi},
                                //new User(){ Email="ustGrupDiyarbakir@nobetyaz.com", FirstName="�st Grup", LastName="�st grp", Password="ustGrup8", UserName="ustGrupDiyarbakir", BaslamaTarihi = baslamaTarihi},
                                new User(){ Email="ecz.zynp@gmail.com", FirstName="Zeynep", LastName="Ordu", Password="ordu2019", UserName="zeynepHan�m", BaslamaTarihi = baslamaTarihi}
                            },

                NobetGrupKurallar = new List<NobetGrupKural>(),
                //{
                //new NobetGrupKural(){ NobetKuralId=1, BaslangicTarihi=baslamaTarihi, Deger=5},//Ard���k Bo� G�n Say�s�
                //new NobetGrupKural(){ NobetGrupGorevTipId=28, NobetKuralId=2, BaslangicTarihi=baslamaTarihi, Deger=5},
                //new NobetGrupKural(){ NobetKuralId=3, BaslangicTarihi=baslamaTarihi, Deger=1}//Varsay�lan g�nl�k n�bet�i say�s�
                //},

                NobetKurallar = context.NobetKurallar.Where(w => new int[]
                {
                    1, //Ard���k Bo� G�n Say�s�
                    3  //Varsay�lan g�nl�k n�bet�i say�s�
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
