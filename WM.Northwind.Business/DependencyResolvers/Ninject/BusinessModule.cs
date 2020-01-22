using Ninject.Modules;
using System.Data.Entity;
using WM.Core.DAL;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.Abstract.Transport;
using WM.Northwind.Business.Concrete.Managers;
using WM.Northwind.Business.Concrete.Managers.Authorization;
using WM.Northwind.Business.Concrete.Managers.EczaneNobet;
using WM.Northwind.Business.Concrete.Managers.Transport;
using WM.Northwind.Business.Concrete.OptimizationModels;
using WM.Northwind.Business.Concrete.OptimizationModels.Samples;
using WM.Northwind.DataAccess.Abstract;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.Authorization;
using WM.Northwind.DataAccess.Abstract.Transport;
using WM.Northwind.DataAccess.Concrete.EntityFramework;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Authorization;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Transport;
using WM.Optimization.Abstract.Samples;
using WM.Optimization.Concrete.IbmCplex.Samples;
using WM.Optimization.Concrete.Optano.Samples;
using WM.Optimization.Abstract.Health;
using WM.Optimization.Concrete.Optano.Health.EczaneNobet;
using WM.Northwind.Business.Abstract.Optimization;
using WM.Northwind.Business.Concrete.OptimizationManagers.Health.EczaneNobet;
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;
using WM.Optimization.Concrete.Optano.Health.EczaneNobet.HedefProgramlama;
using WM.Optimization.Concrete.Optano.Health.EczaneNobet.Eski;
using WM.Northwind.Business.Concrete.OptimizationManagers.Health.EczaneNobet.Eski;

namespace WM.BLL.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            #region Eczane Nöbet
            Bind<IAdminService>().To<AdminManager>().InSingletonScope();

            Bind<IUserRoleService>().To<UserRoleManager>().InSingletonScope();
            Bind<IUserRoleDal>().To<EfUserRoleDal>();

            Bind<IMenuService>().To<MenuManager>().InSingletonScope();
            Bind<IMenuDal>().To<EfMenuDal>();

            Bind<IMenuRoleService>().To<MenuRoleManager>().InSingletonScope();
            Bind<IMenuRoleDal>().To<EfMenuRoleDal>();

            Bind<IMenuAltService>().To<MenuAltManager>().InSingletonScope();
            Bind<IMenuAltDal>().To<EfMenuAltDal>();

            Bind<IMenuAltRoleService>().To<MenuAltRoleManager>().InSingletonScope();
            Bind<IMenuAltRoleDal>().To<EfMenuAltRoleDal>();

            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<IUserDal>().To<EfUserDal>();

            Bind<IRoleService>().To<RoleManager>().InSingletonScope();
            Bind<IRoleDal>().To<EfRoleDal>();

            Bind<IUserEczaneOdaService>().To<UserEczaneOdaManager>().InSingletonScope();
            Bind<IUserEczaneOdaDal>().To<EfUserEczaneOdaDal>();

            Bind<IUserNobetUstGrupService>().To<UserNobetUstGrupManager>().InSingletonScope();
            Bind<IUserNobetUstGrupDal>().To<EfUserNobetUstGrupDal>();

            Bind<IUserEczaneService>().To<UserEczaneManager>().InSingletonScope();
            Bind<IUserEczaneDal>().To<EfUserEczaneDal>();

            Bind<ISehirService>().To<SehirManager>().InSingletonScope();
            Bind<ISehirDal>().To<EfSehirDal>();

            Bind<IIlceService>().To<IlceManager>().InSingletonScope();
            Bind<IIlceDal>().To<EfIlceDal>();

            Bind<IEczaneIlceService>().To<EczaneIlceManager>().InSingletonScope();
            Bind<IEczaneIlceDal>().To<EfEczaneIlceDal>();

            Bind<INobetGrupTalepService>().To<NobetGrupTalepManager>().InSingletonScope();
            Bind<INobetGrupTalepDal>().To<EfNobetGrupTalepDal>();

            Bind<INobetKuralService>().To<NobetKuralManager>().InSingletonScope();
            Bind<INobetKuralDal>().To<EfNobetKuralDal>();

            Bind<INobetGrupKuralService>().To<NobetGrupKuralManager>().InSingletonScope();
            Bind<INobetGrupKuralDal>().To<EfNobetGrupKuralDal>();

            Bind<INobetGrupGunKuralService>().To<NobetGrupGunKuralManager>().InSingletonScope();
            Bind<INobetGrupGunKuralDal>().To<EfNobetGrupGunKuralDal>();

            Bind<INobetGunKuralService>().To<NobetGunKuralManager>().InSingletonScope();
            Bind<INobetGunKuralDal>().To<EfNobetGunKuralDal>();

            Bind<INobetGrupGorevTipService>().To<NobetGrupGorevTipManager>().InSingletonScope();
            Bind<INobetGrupGorevTipDal>().To<EfNobetGrupGorevTipDal>();

            Bind<INobetGorevTipService>().To<NobetGorevTipManager>().InSingletonScope();
            Bind<INobetGorevTipDal>().To<EfNobetGorevTipDal>();

            Bind<IEczaneService>().To<EczaneManager>().InSingletonScope();
            Bind<IEczaneDal>().To<EfEczaneDal>();

            Bind<IEczaneOdaService>().To<EczaneOdaManager>().InSingletonScope();
            Bind<IEczaneOdaDal>().To<EfEczaneOdaDal>();

            Bind<ITakvimService>().To<TakvimManager>().InSingletonScope();
            Bind<ITakvimDal>().To<EfTakvimDal>();

            Bind<IBayramService>().To<BayramManager>().InSingletonScope();
            Bind<IBayramDal>().To<EfBayramDal>();

            Bind<IBayramTurService>().To<BayramTurManager>().InSingletonScope();
            Bind<IBayramTurDal>().To<EfBayramTurDal>();

            Bind<INobetUstGrupService>().To<NobetUstGrupManager>().InSingletonScope();
            Bind<INobetUstGrupDal>().To<EfNobetUstGrupDal>();

            Bind<INobetAltGrupService>().To<NobetAltGrupManager>().InSingletonScope();
            Bind<INobetAltGrupDal>().To<EfNobetAltGrupDal>();

            Bind<IEczaneGrupService>().To<EczaneGrupManager>().InSingletonScope();
            Bind<IEczaneGrupDal>().To<EfEczaneGrupDal>();

            Bind<IEczaneGrupTanimService>().To<EczaneGrupTanimManager>().InSingletonScope();
            Bind<IEczaneGrupTanimDal>().To<EfEczaneGrupTanimDal>();

            Bind<IEczaneGrupTanimTipService>().To<EczaneGrupTanimTipManager>().InSingletonScope();
            Bind<IEczaneGrupTanimTipDal>().To<EfEczaneGrupTanimTipDal>();

            Bind<INobetGrupService>().To<NobetGrupManager>().InSingletonScope();
            Bind<INobetGrupDal>().To<EfNobetGrupDal>();

            Bind<IEczaneNobetOrtakService>().To<EczaneNobetOrtakManager>().InSingletonScope();

            Bind<IEczaneNobetMuafiyetService>().To<EczaneNobetMuafiyetManager>().InSingletonScope();
            Bind<IEczaneNobetMuafiyetDal>().To<EfEczaneNobetMuafiyetDal>();

            Bind<IEczaneNobetSonucDemoService>().To<EczaneNobetSonucDemoManager>().InSingletonScope();
            Bind<IEczaneNobetSonucDemoDal>().To<EfEczaneNobetSonucDemoDal>();

            Bind<IEczaneNobetSonucPlanlananService>().To<EczaneNobetSonucPlanlananManager>().InSingletonScope();
            Bind<IEczaneNobetSonucPlanlananDal>().To<EfEczaneNobetSonucPlanlananDal>();

            Bind<IAyniGunTutulanNobetService>().To<AyniGunTutulanNobetManager>().InSingletonScope();
            Bind<IAyniGunTutulanNobetDal>().To<EfAyniGunTutulanNobetDal>();

            Bind<INobetSonucDemoTipService>().To<NobetSonucDemoTipManager>().InSingletonScope();
            Bind<INobetSonucDemoTipDal>().To<EfNobetSonucDemoTipDal>();

            Bind<IEczaneNobetSonucAktifService>().To<EczaneNobetSonucAktifManager>().InSingletonScope();
            Bind<IEczaneNobetSonucAktifDal>().To<EfEczaneNobetSonucAktifDal>();

            Bind<IEczaneNobetSonucService>().To<EczaneNobetSonucManager>().InSingletonScope();
            Bind<IEczaneNobetSonucDal>().To<EfEczaneNobetSonucDal>();

            Bind<IEczaneNobetSanalSonucService>().To<EczaneNobetSanalSonucManager>().InSingletonScope();
            Bind<IEczaneNobetSanalSonucDal>().To<EfEczaneNobetSanalSonucDal>();

            Bind<IEczaneNobetDegisimService>().To<EczaneNobetDegisimManager>().InSingletonScope();
            Bind<IEczaneNobetDegisimDal>().To<EfEczaneNobetDegisimDal>();

            Bind<IEczaneNobetFeragatService>().To<EczaneNobetFeragatManager>().InSingletonScope();
            Bind<IEczaneNobetFeragatDal>().To<EfEczaneNobetFeragatDal>();

            Bind<IEczaneNobetMazeretService>().To<EczaneNobetMazeretManager>().InSingletonScope();
            Bind<IEczaneNobetMazeretDal>().To<EfEczaneNobetMazeretDal>();

            Bind<IEczaneNobetIstekService>().To<EczaneNobetIstekManager>().InSingletonScope();
            Bind<IEczaneNobetIstekDal>().To<EfEczaneNobetIstekDal>();

            Bind<IMazeretService>().To<MazeretManager>().InSingletonScope();
            Bind<IMazeretDal>().To<EfMazeretDal>();

            Bind<IMazeretTurService>().To<MazeretTurManager>().InSingletonScope();
            Bind<IMazeretTurDal>().To<EfMazeretTurDal>();

            Bind<IIstekService>().To<IstekManager>().InSingletonScope();
            Bind<IIstekDal>().To<EfIstekDal>();

            Bind<IIstekTurService>().To<IstekTurManager>().InSingletonScope();
            Bind<IIstekTurDal>().To<EfIstekTurDal>();

            Bind<IGunDegerService>().To<GunDegerManager>().InSingletonScope();
            Bind<IGunDegerDal>().To<EfGunDegerDal>();

            Bind<IEczaneNobetGrupService>().To<EczaneNobetGrupManager>().InSingletonScope();
            Bind<IEczaneNobetGrupDal>().To<EfEczaneNobetGrupDal>();

            Bind<IEczaneNobetGrupAltGrupService>().To<EczaneNobetGrupAltGrupManager>().InSingletonScope();
            Bind<IEczaneNobetGrupAltGrupDal>().To<EfEczaneNobetGrupAltGrupDal>();

            Bind<IKisitService>().To<KisitManager>().InSingletonScope();
            Bind<IKisitDal>().To<EfKisitDal>();

            Bind<IKisitKategoriService>().To<KisitKategoriManager>().InSingletonScope();
            Bind<IKisitKategoriDal>().To<EfKisitKategoriDal>();

            Bind<INobetUstGrupKisitService>().To<NobetUstGrupKisitManager>().InSingletonScope();
            Bind<INobetUstGrupKisitDal>().To<EfNobetUstGrupKisitDal>();

            Bind<INobetGrupGorevTipTakvimOzelGunService>().To<NobetGrupGorevTipTakvimOzelGunManager>().InSingletonScope();
            Bind<INobetGrupGorevTipTakvimOzelGunDal>().To<EfNobetGrupGorevTipTakvimOzelGunDal>();

            Bind<INobetGrupGorevTipGunKuralService>().To<NobetGrupGorevTipGunKuralManager>().InSingletonScope();
            Bind<INobetGrupGorevTipGunKuralDal>().To<EfNobetGrupGorevTipGunKuralDal>();

            Bind<INobetOzelGunService>().To<NobetOzelGunManager>().InSingletonScope();
            Bind<INobetOzelGunDal>().To<EfNobetOzelGunDal>();

            Bind<INobetUstGrupGunGrupService>().To<NobetUstGrupGunGrupManager>().InSingletonScope();
            Bind<INobetUstGrupGunGrupDal>().To<EfNobetUstGrupGunGrupDal>();

            Bind<INobetDurumService>().To<NobetDurumManager>().InSingletonScope();
            Bind<INobetDurumDal>().To<EfNobetDurumDal>();

            Bind<INobetDurumTipService>().To<NobetDurumTipManager>().InSingletonScope();
            Bind<INobetDurumTipDal>().To<EfNobetDurumTipDal>();

            Bind<IKalibrasyonService>().To<KalibrasyonManager>().InSingletonScope();
            Bind<IKalibrasyonDal>().To<EfKalibrasyonDal>();

            Bind<IKalibrasyonTipService>().To<KalibrasyonTipManager>().InSingletonScope();
            Bind<IKalibrasyonTipDal>().To<EfKalibrasyonTipDal>();

            Bind<IEczaneUzaklikMatrisService>().To<EczaneUzaklikMatrisManager>().InSingletonScope();
            Bind<IEczaneUzaklikMatrisDal>().To<EfEczaneUzaklikMatrisDal>();

            Bind<INobetFeragatTipService>().To<NobetFeragatTipManager>().InSingletonScope();
            Bind<INobetFeragatTipDal>().To<EfNobetFeragatTipDal>();

            Bind<INobetGrupGorevTipKisitService>().To<NobetGrupGorevTipKisitManager>().InSingletonScope();
            Bind<INobetGrupGorevTipKisitDal>().To<EfNobetGrupGorevTipKisitDal>();

            Bind<IAyniGunNobetTakipGrupAltGrupService>().To<AyniGunNobetTakipGrupAltGrupManager>().InSingletonScope();
            Bind<IAyniGunNobetTakipGrupAltGrupDal>().To<EfAyniGunNobetTakipGrupAltGrupDal>();

            Bind<IGunGrupService>().To<GunGrupManager>().InSingletonScope();
            Bind<IGunGrupDal>().To<EfGunGrupDal>();

            Bind<INobetOzelGunKategoriService>().To<NobetOzelGunKategoriManager>().InSingletonScope();
            Bind<INobetOzelGunKategoriDal>().To<EfNobetOzelGunKategoriDal>();

            Bind<IRaporService>().To<RaporManager>().InSingletonScope();
            Bind<IRaporDal>().To<EfRaporDal>();

            Bind<IRaporKategoriService>().To<RaporKategoriManager>().InSingletonScope();
            Bind<IRaporKategoriDal>().To<EfRaporKategoriDal>();

            Bind<IRaporRolService>().To<RaporRolManager>().InSingletonScope();
            Bind<IRaporRolDal>().To<EfRaporRolDal>();

            Bind<IRaporNobetUstGrupService>().To<RaporNobetUstGrupManager>().InSingletonScope();
            Bind<IRaporNobetUstGrupDal>().To<EfRaporNobetUstGrupDal>();

            Bind<ILogService>().To<LogManager>().InSingletonScope();
            Bind<ILogDal>().To<EfLogDal>();

            Bind<IDebugEczaneService>().To<DebugEczaneManager>().InSingletonScope();
            Bind<IDebugEczaneDal>().To<EfDebugEczaneDal>();

            #endregion

            #region Optimization

            Bind<IEczaneNobetOptimizationService>().To<EczaneNobetOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetOptimization>().To<EczaneNobetOptano>().InSingletonScope();
            Bind<IEczaneNobetKisit>().To<EczaneNobetKisit>().InSingletonScope();

            Bind<IAlanyaOptimizationServiceEski>().To<AlanyaOptimizationManagerEski>().InSingletonScope();
            Bind<IEczaneNobetAlanyaOptimization>().To<AlanyaOptanoEski>().InSingletonScope();

            Bind<IAlanyaOptimizationService>().To<AlanyaOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetAlanyaOptimizationV2>().To<AlanyaOptano>().InSingletonScope();

            Bind<IAntalyaMerkezOptimizationService>().To<AntalyaMerkezOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetAntalyaMerkezOptimization>().To<AntalyaMerkezOptano>().InSingletonScope();

            Bind<IMersinMerkezOptimizationServiceV2>().To<MersinMerkezOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetMersinMerkezOptimizationV2>().To<MersinMerkezOptano>().InSingletonScope();

            Bind<IGiresunOptimizationService>().To<GiresunOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetGiresunOptimization>().To<GiresunOptano>().InSingletonScope();

            Bind<IOsmaniyeOptimizationService>().To<OsmaniyeOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetOsmaniyeOptimization>().To<OsmaniyeOptano>().InSingletonScope();

            Bind<IBartinOptimizationService>().To<BartinOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetBartinOptimization>().To<BartinOptano>().InSingletonScope();

            Bind<ICorumOptimizationService>().To<CorumOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetCorumOptimization>().To<CorumOptano>().InSingletonScope();

            Bind<IIskenderunOptimizationService>().To<IskenderunOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetIskenderunOptimization>().To<IskenderunOptano>().InSingletonScope();

            Bind<IKirikhanOptimizationService>().To<KirikhanOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetKirikhanOptimization>().To<KirikhanOptano>().InSingletonScope();

            Bind<IDiyarbakirOptimizationService>().To<DiyarbakirOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetDiyarbakirOptimization>().To<DiyarbakirOptano>().InSingletonScope();

            Bind<IManavgatOptimizationService>().To<ManavgatOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetManavgatOptimization>().To<ManavgatOptano>().InSingletonScope();

            Bind<IOrduMerkezOptimizationService>().To<OrduMerkezOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetOrduMerkezOptimization>().To<OrduMerkezOptano>().InSingletonScope();

            Bind<IKayseriOptimizationService>().To<KayseriOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetKayseriOptimization>().To<KayseriOptano>().InSingletonScope();

            Bind<IAntakyaOptimizationService>().To<AntakyaOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetAntakyaOptimization>().To<AntakyaOptano>().InSingletonScope();

            Bind<IZonguldakOptimizationService>().To<ZonguldakOptimizationManager>().InSingletonScope();
            Bind<IEczaneNobetZonguldakOptimization>().To<ZonguldakOptano>().InSingletonScope();


            Bind<ITransportService>().To<TransportManager>().InSingletonScope();
            Bind<ITransportOptimization>().To<TransportOptano>();

            Bind<IBlendService>().To<BlendManager>().InSingletonScope();
            Bind<IBlendOptimization>().To<BlendCplex>().InSingletonScope();

            #region eski
            //Bind<IEczaneNobetService>().To<EczaneNobetManager>().InSingletonScope();
            Bind<IEczaneNobetTekGrupOptimization>().To<EczaneNobetTekGrupOptano>().InSingletonScope();
            Bind<IEczaneNobetTekGrupAltOptimization>().To<EczaneNobetTekGrupAltOptano>().InSingletonScope();
            Bind<IEczaneNobetCokGrupOptimization>().To<EczaneNobetCokGrupOptano>().InSingletonScope();
            Bind<IEczaneNobetCokGrupAltOptimization>().To<EczaneNobetCokGrupAltOptano>().InSingletonScope();
            #endregion
            #endregion

            #region Transport
            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IProductDal>().To<EfProductDal>();

            Bind<IFabrikaService>().To<FabrikaManager>().InSingletonScope();
            Bind<IFabrikaDal>().To<EfFabrikaDal>();

            Bind<IDepoService>().To<DepoManager>().InSingletonScope();
            Bind<IDepoDal>().To<EfDepoDal>();

            Bind<ITransportMaliyetService>().To<TransportMaliyetManager>().InSingletonScope();
            Bind<ITransportMaliyetDal>().To<EfTransportMaliyetDal>();

            Bind<ITransportSonucService>().To<TransportSonucManager>().InSingletonScope();
            Bind<ITransportSonucDal>().To<EfTransportSonucDal>();

            Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();
            Bind<ICategoryDal>().To<EfCategoryDal>();
            #endregion

            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQueryableRepository<>));
            Bind<DbContext>().To<EczaneNobetContext>();
        }
    }
}
