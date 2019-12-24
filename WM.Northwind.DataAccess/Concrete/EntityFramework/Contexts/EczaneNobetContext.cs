using System.Data.Entity;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Initializers;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Mapping.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using System.Collections.Generic;
using WM.Northwind.DataAccess.Migrations;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts
{
    public class EczaneNobetContext : DbContext
    {
        static EczaneNobetContext()
        {
            //Database.SetInitializer(new EczaneNobetInitializer());
            //Database.SetInitializer(new EczaneNobetInitializerAlanya());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EczaneNobetContext>());

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EczaneNobetContext, Configuration>());
        }

        public EczaneNobetContext() : base("Name=EczaneNobetContext")
        {
        }

        #region Yetki
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<MenuAltRole> MenuAltRoles { get; set; }

        public DbSet<Menu> Menuler { get; set; }
        public DbSet<MenuAlt> MenuAltlar { get; set; }
        #endregion

        public DbSet<UserEczaneOda> UserEczaneOdalar { get; set; }
        public DbSet<UserNobetUstGrup> UserNobetUstGruplar { get; set; }
        public DbSet<UserEczane> UserEczaneler { get; set; }

        public DbSet<EczaneGrupTanimTip> EczaneGrupTanimTipler { get; set; }
        public DbSet<Kisit> Kisitlar { get; set; }
        public DbSet<KisitKategori> KisitKategoriler { get; set; }
        public DbSet<NobetUstGrupKisit> NobetUstGrupKisitlar { get; set; }
        public DbSet<EczaneNobetFeragat> EczaneNobetFeragatlar { get; set; }
        public DbSet<EczaneNobetDegisim> EczaneNobetDegisimler { get; set; }

        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<EczaneIlce> EczaneIlceler { get; set; }

        public DbSet<GorevTip> GorevTipler { get; set; }
        public DbSet<NobetGorevTip> NobetGorevTipler { get; set; }
        public DbSet<NobetGrupGorevTip> NobetGrupGorevTipler { get; set; }//
        public DbSet<NobetGrupTalep> NobetGrupTalepler { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<GunGrup> GunGruplar { get; set; }
        public DbSet<NobetGrupGorevTipGunKural> NobetGrupGorevTipGunKurallar { get; set; }
        public DbSet<NobetUstGrupGunGrup> NobetUstGrupGunGruplar { get; set; }
        public DbSet<NobetGrupGorevTipTakvimOzelGun> NobetGrupGorevTipTakvimOzelGunler { get; set; }
        public DbSet<NobetOzelGun> NobetOzelGunler { get; set; }


        #region Eczane Nöbet
        public DbSet<NobetKural> NobetKurallar { get; set; }
        public DbSet<NobetGrupKural> NobetGrupKurallar { get; set; }

        public DbSet<NobetGunKural> NobetGunKurallar { get; set; }
        public DbSet<NobetGrupGunKural> NobetGrupGunKurallar { get; set; }

        public DbSet<Bayram> Bayramlar { get; set; }
        public DbSet<BayramTur> BayramTurler { get; set; }
        public DbSet<Eczane> Eczaneler { get; set; }
        public DbSet<EczaneGrupTanim> EczaneGrupTanimlar { get; set; }
        public DbSet<EczaneGrup> EczaneGruplar { get; set; }
        public DbSet<EczaneNobetGrup> EczaneNobetGruplar { get; set; }
        public DbSet<EczaneNobetGrupAltGrup> EczaneNobetGrupAltGruplar { get; set; }
        public DbSet<EczaneNobetIstek> EczaneNobetIstekler { get; set; }
        public DbSet<EczaneNobetMazeret> EczaneNobetMazeretler { get; set; }
        public DbSet<EczaneNobetMuafiyet> EczaneNobetMuafiyetler { get; set; }
        public DbSet<EczaneNobetSonuc> EczaneNobetSonuclar { get; set; }
        public DbSet<EczaneNobetSonucPlanlanan> EczaneNobetSonucPlanlananlar { get; set; }
        public DbSet<EczaneNobetSonucAktif> EczaneNobetSonucAktifler { get; set; }
        public DbSet<EczaneNobetSonucDemo> EczaneNobetSonucDemolar { get; set; }
        public DbSet<NobetSonucDemoTip> NobetSonucDemoTipler { get; set; }
        public DbSet<AyniGunTutulanNobet> AyniGunTutulanNobetler { get; set; }

        public DbSet<EczaneOda> EczaneOdalar { get; set; }
        public DbSet<Istek> Istekler { get; set; }
        public DbSet<Mazeret> Mazeretler { get; set; }
        public DbSet<MazeretTur> MazeretTurler { get; set; }
        public DbSet<IstekTur> IstekTurler { get; set; }
        public DbSet<NobetUstGrup> NobetUstGruplar { get; set; }
        public DbSet<NobetGrup> NobetGruplar { get; set; }
        public DbSet<NobetAltGrup> NobetAltGruplar { get; set; }
        public DbSet<Takvim> Takvimler { get; set; }
        public DbSet<EczaneGorevTip> EczaneGorevTipler { get; set; }
        public DbSet<NobetDurumTip> NobetDurumTipler { get; set; }
        public DbSet<NobetDurum> NobetDurumlar { get; set; }
        public DbSet<KalibrasyonTip> KalibrasyonTipler { get; set; }
        public DbSet<Kalibrasyon> Kalibrasyonlar { get; set; }
        public DbSet<EczaneUzaklikMatris> EczaneUzaklikMatrisler { get; set; }
        public DbSet<NobetFeragatTip> NobetFeragatTipler { get; set; }
        public DbSet<NobetGrupGorevTipKisit> NobetGrupGorevTipKisitlar { get; set; }
        public DbSet<AyniGunNobetTakipGrupAltGrup> AyniGunNobetTakipGrupAltGruplar { get; set; }
        public DbSet<NobetOzelGunKategori> NobetOzelGunKategoriler { get; set; }
        public DbSet<Rapor> Raporlar { get; set; }
        public DbSet<RaporKategori> RaporKategoriler { get; set; }
        public DbSet<RaporRol> RaporRoller { get; set; }
        public DbSet<RaporNobetUstGrup> RaporNobetUstGruplar { get; set; }
        public DbSet<EczaneNobetSanalSonuc> EczaneNobetSanalSonuclar { get; set; }
        public DbSet<DebugEczane> DebugEczaneler { get; set; }

        #endregion

        #region Mapping
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("EczaneNobet");

            #region Yetki
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserRoleMap());

            modelBuilder.Configurations.Add(new MenuRoleMap());
            modelBuilder.Configurations.Add(new MenuAltRoleMap());

            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new MenuAltMap());
            #endregion

            modelBuilder.Configurations.Add(new UserEczaneOdaMap());
            modelBuilder.Configurations.Add(new UserNobetUstGrupMap());
            modelBuilder.Configurations.Add(new UserEczaneMap());

            modelBuilder.Configurations.Add(new SehirMap());
            modelBuilder.Configurations.Add(new IlceMap());
            modelBuilder.Configurations.Add(new EczaneIlceMap());

            modelBuilder.Configurations.Add(new GorevTipMap());
            modelBuilder.Configurations.Add(new NobetGorevTipMap());
            modelBuilder.Configurations.Add(new NobetGrupGorevTipMap());
            modelBuilder.Configurations.Add(new NobetGrupTalepMap());

            modelBuilder.Configurations.Add(new LogMap());

            modelBuilder.Configurations.Add(new GunGrupMap());
            modelBuilder.Configurations.Add(new NobetGrupGorevTipGunKuralMap());
            modelBuilder.Configurations.Add(new NobetUstGrupGunGrupMap());
            modelBuilder.Configurations.Add(new NobetGrupGorevTipTakvimOzelGunMap());
            modelBuilder.Configurations.Add(new NobetOzelGunMap());


            #region Eczane Nöbet
            modelBuilder.Configurations.Add(new BayramMap());
            modelBuilder.Configurations.Add(new NobetKuralMap());
            modelBuilder.Configurations.Add(new NobetGunKuralMap());
            modelBuilder.Configurations.Add(new NobetGrupKuralMap());
            modelBuilder.Configurations.Add(new NobetGrupGunKuralMap());
            modelBuilder.Configurations.Add(new BayramTurMap());
            modelBuilder.Configurations.Add(new EczaneMap());
            modelBuilder.Configurations.Add(new EczaneGrupTanimMap());
            modelBuilder.Configurations.Add(new EczaneGrupMap());
            modelBuilder.Configurations.Add(new EczaneNobetGrupMap());
            modelBuilder.Configurations.Add(new EczaneNobetGrupAltGrupMap());
            modelBuilder.Configurations.Add(new EczaneNobetIstekMap());
            modelBuilder.Configurations.Add(new EczaneNobetMazeretMap());
            modelBuilder.Configurations.Add(new EczaneNobetMuafiyetMap());
            modelBuilder.Configurations.Add(new EczaneNobetSonucMap());
            modelBuilder.Configurations.Add(new EczaneNobetSonucPlanlananMap());
            modelBuilder.Configurations.Add(new EczaneNobetSonucAktifMap());
            modelBuilder.Configurations.Add(new EczaneNobetSonucDemoMap());
            modelBuilder.Configurations.Add(new AyniGunTutulanNobetMap());

            modelBuilder.Configurations.Add(new NobetSonucDemoTipMap());
            modelBuilder.Configurations.Add(new EczaneOdaMap());
            modelBuilder.Configurations.Add(new MazeretTurMap());
            modelBuilder.Configurations.Add(new IstekTurMap());
            modelBuilder.Configurations.Add(new IstekMap());
            modelBuilder.Configurations.Add(new MazeretMap());
            modelBuilder.Configurations.Add(new NobetUstGrupMap());
            modelBuilder.Configurations.Add(new NobetGrupMap());
            modelBuilder.Configurations.Add(new NobetAltGrupMap());
            modelBuilder.Configurations.Add(new TakvimMap());
            modelBuilder.Configurations.Add(new EczaneGorevTipMap());

            modelBuilder.Configurations.Add(new EczaneGrupTanimTipMap());
            modelBuilder.Configurations.Add(new KisitMap());
            modelBuilder.Configurations.Add(new KisitKategoriMap());
            modelBuilder.Configurations.Add(new NobetUstGrupKisitMap());
            modelBuilder.Configurations.Add(new EczaneNobetFeragatMap());
            modelBuilder.Configurations.Add(new EczaneNobetDegisimMap());
            modelBuilder.Configurations.Add(new EczaneNobetSonucEskiMap());
            modelBuilder.Configurations.Add(new NobetDurumTipMap());
            modelBuilder.Configurations.Add(new NobetDurumMap());
            modelBuilder.Configurations.Add(new KalibrasyonTipMap());
            modelBuilder.Configurations.Add(new KalibrasyonMap());
            modelBuilder.Configurations.Add(new EczaneUzaklikMatrisMap());
            modelBuilder.Configurations.Add(new NobetFeragatTipMap());
            modelBuilder.Configurations.Add(new NobetGrupGorevTipKisitMap());
            modelBuilder.Configurations.Add(new AyniGunNobetTakipGrupAltGrupMap());
            modelBuilder.Configurations.Add(new NobetOzelGunKategoriMap());
            modelBuilder.Configurations.Add(new RaporMap());
            modelBuilder.Configurations.Add(new RaporKategoriMap());
            modelBuilder.Configurations.Add(new RaporRolMap());
            modelBuilder.Configurations.Add(new RaporNobetUstGrupMap());
            modelBuilder.Configurations.Add(new EczaneNobetSanalSonucMap());
            modelBuilder.Configurations.Add(new DebugEczaneMap());

        #endregion
    }
    #endregion
}
}