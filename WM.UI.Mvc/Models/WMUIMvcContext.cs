using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.Transport;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.UI.Mvc.Models
{
    public class WMUIMvcContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public WMUIMvcContext() : base("name=WMUIMvcContext")
        {
        }

        public DbSet<Fabrika> Fabrikas { get; set; }

        public DbSet<Depo> Depoes { get; set; }

        public DbSet<TransportMaliyet> TransportMaliyets { get; set; }

        public DbSet<TransportSonuc> TransportSonucs { get; set; }

        public DbSet<EczaneNobetSonucAktif> EczaneNobetSonucAktifs { get; set; }

        public DbSet<EczaneNobetGrup> EczaneNobetGrups { get; set; }

        public DbSet<Takvim> Takvims { get; set; }

        public DbSet<Bayram> Bayrams { get; set; }

        public DbSet<EczaneNobetSonuc> EczaneNobetSonucs { get; set; }

        public DbSet<EczaneOda> EczaneOdas { get; set; }

        public DbSet<Eczane> Eczanes { get; set; }

        public DbSet<NobetUstGrup> NobetUstGrups { get; set; }

        public DbSet<NobetGrup> NobetGrups { get; set; }

        public DbSet<EczaneGrup> EczaneGrups { get; set; }

        public DbSet<EczaneGrupTanim> EczaneGrupTanims { get; set; }

        public DbSet<EczaneNobetMazeret> EczaneNobetMazerets { get; set; }

        public DbSet<Mazeret> Mazerets { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<MenuAlt> MenuAlts { get; set; }

        public DbSet<MazeretTur> MazeretTurs { get; set; }

        public DbSet<Istek> Isteks { get; set; }

        public DbSet<IstekTur> IstekTurs { get; set; }

        public DbSet<MenuAltRole> MenuAltRoles { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<MenuRole> MenuRoles { get; set; }

        public DbSet<EczaneNobetIstek> EczaneNobetIsteks { get; set; }

        public DbSet<NobetGorevTip> NobetGorevTips { get; set; }

        public DbSet<Sehir> Sehirs { get; set; }

        public DbSet<Ilce> Ilces { get; set; }

        public DbSet<NobetKural> NobetKurals { get; set; }

        public DbSet<NobetGrupKural> NobetGrupKurals { get; set; }

        public DbSet<UserEczaneOda> UserEczaneOdas { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<UserEczane> UserEczanes { get; set; }

        public DbSet<UserNobetUstGrup> UserNobetUstGrups { get; set; }

        public DbSet<EczaneNobetSonucDemo> EczaneNobetSonucDemoes { get; set; }

        public DbSet<NobetGrupTalep> NobetGrupTaleps { get; set; }

        public DbSet<NobetGrupGorevTip> NobetGrupGorevTips { get; set; }

        public DbSet<NobetGunKural> NobetGunKurals { get; set; }

        public DbSet<NobetGrupGunKural> NobetGrupGunKurals { get; set; }

        public DbSet<Kisit> Kisits { get; set; }

        public DbSet<NobetUstGrupKisit> NobetUstGrupKisits { get; set; }

        public DbSet<EczaneNobetFeragat> EczaneNobetFeragats { get; set; }

        public DbSet<NobetSonucDemoTip> NobetSonucDemoTips { get; set; }

        public DbSet<EczaneNobetMuafiyet> EczaneNobetMuafiyets { get; set; }

        public DbSet<BayramTur> BayramTurs { get; set; }

        public DbSet<NobetAltGrup> NobetAltGrups { get; set; }

        public DbSet<EczaneNobetGrupAltGrup> EczaneNobetGrupAltGrups { get; set; }

        public DbSet<EczaneNobetSonucDetay2> EczaneNobetSonucDetay2 { get; set; }

        public DbSet<EczaneNobetDegisim> EczaneNobetDegisims { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.NobetOzelGun> NobetOzelGuns { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.NobetGrupGorevTipTakvimOzelGun> NobetGrupGorevTipTakvimOzelGuns { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.NobetGrupGorevTipGunKural> NobetGrupGorevTipGunKurals { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.NobetUstGrupGunGrup> NobetUstGrupGunGrups { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.GunGrup> GunGrups { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.AyniGunTutulanNobet> AyniGunTutulanNobets { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.NobetDurum> NobetDurums { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.NobetDurumTip> NobetDurumTips { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.EczaneUzaklikMatris> EczaneUzaklikMatris { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.NobetGrupGorevTipKisit> NobetGrupGorevTipKisits { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.Kalibrasyon> Kalibrasyons { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.KalibrasyonTip> KalibrasyonTips { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.AyniGunNobetTakipGrupAltGrup> AyniGunNobetTakipGrupAltGrups { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.EczaneNobetSonucPlanlanan> EczaneNobetSonucPlanlanans { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.EczaneNobetSanalSonuc> EczaneNobetSanalSonucs { get; set; }

        public System.Data.Entity.DbSet<WM.Northwind.Entities.Concrete.EczaneNobet.DebugEczane> DebugEczanes { get; set; }
    }
}
