using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.ValidationRules.FluentValidation;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Core.Aspects.PostSharp.ValidationAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Core.Aspects.PostSharp.PerformansCounterAspects;
using System.Threading;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.ExceptionAspects;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Business.Abstract.Authorization;
using System.Globalization;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    //[SecuredOperation(Roles= "Admin,Oda,Üst Grup,Eczane")]
    public class EczaneManager : IEczaneService
    {
        private IEczaneDal _eczaneDal;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupService _nobetGrupService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IUserEczaneService _userEczaneService;

        public EczaneManager(IEczaneDal eczaneDal,
                             IUserService userService,
                             INobetUstGrupService nobetUstGrupService,
                             INobetGrupService nobetGrupService,
                             IEczaneNobetGrupService eczaneNobetGrupService,
                             IUserEczaneService userEczaneService)
        {
            _eczaneDal = eczaneDal;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupService = nobetGrupService;
            _userEczaneService = userEczaneService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<Eczane> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _eczaneDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Eczane> GetList(int nobetUstGrupId)
        {
            return _eczaneDal.GetList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Eczane> GetList(int[] nobetUstGrupIds)
        {
            return _eczaneDal.GetList(x => nobetUstGrupIds.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Eczane> GetListByEczaneIds(int[] eczaneIds)
        {
            return _eczaneDal.GetList(x => eczaneIds.Contains(x.Id));
        }

        public Eczane GetById(int eczaneId)
        {
            return _eczaneDal.Get(x => x.Id == eczaneId);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(Eczane eczane)
        {
            eczane.Adi = eczane.Adi.Trim().ToUpper(new CultureInfo("tr-TR"));
            _eczaneDal.Insert(eczane);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(Eczane eczane)
        {
            eczane.Adi = eczane.Adi.Trim().ToUpper(new CultureInfo("tr-TR"));
            _eczaneDal.Update(eczane);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int eczaneId)
        {
            _eczaneDal.Delete(new Eczane { Id = eczaneId });
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Eczane> GetListByUser(User user)
        {
            //user roller
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            var eczaneler = new List<Eczane>();

            if (rolId == 4)
            {//yetkili olduğu eczaneler
                var userEczaneler = _userEczaneService.GetListByUserId(user.Id);
                eczaneler = GetList(userEczaneler.Select(s => s.EczaneId).ToArray());
                //.Where(x => userEczaneler.Select(s => s.EczaneId).Contains(x.Id)).ToList();
            }
            else
            {//yetkili olduğu nöbet üst gruplar
                var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(g => g.Id).ToArray();

                eczaneler = GetList(nobetUstGruplar);
            }

            return eczaneler;
        }

        public EczaneDetay GetDetayById(int eczaneId)
        {
            return _eczaneDal.GetDetay(x => x.Id == eczaneId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneDetay> GetDetaylar()
        {
            return _eczaneDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneDetay> GetDetaylar(List<int> nobetUstGrupIdList)
        {
            return _eczaneDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }
    }
}
