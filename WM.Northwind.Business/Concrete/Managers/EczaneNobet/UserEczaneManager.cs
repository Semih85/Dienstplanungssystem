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

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    //[SecuredOperation(Roles= "Admin,Oda,Üst Grup")]
    public class UserEczaneManager : IUserEczaneService
    {
        private IUserEczaneDal _userEczaneDal;

        public EczaneNobetSonucModel Results { get; set; }

        public UserEczaneManager(IUserEczaneDal userEczaneDal)
        {
            _userEczaneDal = userEczaneDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<UserEczane> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _userEczaneDal.GetList();
        }

        public UserEczane GetById(int userEczaneId)
        {
            return _userEczaneDal.Get(x => x.Id == userEczaneId);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(UserEczane userEczane)
        {
            _userEczaneDal.Insert(userEczane);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(UserEczane userEczane)
        {
            _userEczaneDal.Update(userEczane);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int userEczaneId)
        {
            _userEczaneDal.Delete(new UserEczane { Id = userEczaneId });
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserEczane> GetListByUserId(int userId)
        {
            return _userEczaneDal.GetList(w => w.UserId == userId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserEczaneDetay> GetDetaylarByUserId(int userId)
        {
            return _userEczaneDal.GetDetayList(w => w.UserId == userId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public UserEczaneDetay GetDetayById(int userEczaneId)
        {
            return _userEczaneDal.GetDetay(x => x.Id == userEczaneId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserEczaneDetay> GetDetaylar()
        {
            return _userEczaneDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserEczaneDetay> GetDetaylar(List<int> nobetUstGrupIdList)
        {
            return _userEczaneDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserEczaneDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _userEczaneDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }
    }
}
