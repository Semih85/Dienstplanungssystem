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
    //[SecuredOperation(Roles= "Admin,Oda")]
    public class UserNobetUstGrupManager : IUserNobetUstGrupService
    {
        private IUserNobetUstGrupDal _userUstGrupDal;

        //public EczaneNobetSonucModel Results { get; set; }

        public UserNobetUstGrupManager(IUserNobetUstGrupDal userUstGrupDal)
        {
            _userUstGrupDal = userUstGrupDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<UserNobetUstGrup> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _userUstGrupDal.GetList();
        }

        public UserNobetUstGrup GetById(int userUstGrupId)
        {
            return _userUstGrupDal.Get(x => x.Id == userUstGrupId);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(UserNobetUstGrup userUstGrup)
        {
            _userUstGrupDal.Insert(userUstGrup);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(UserNobetUstGrup userUstGrup)
        {
            _userUstGrupDal.Update(userUstGrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int userUstGrupId)
        {
            _userUstGrupDal.Delete(new UserNobetUstGrup { Id = userUstGrupId });
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public UserNobetUstGrup GetByUserId(int userId)
        {
            return _userUstGrupDal.Get(x => x.UserId == userId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserNobetUstGrup> GetListByUserId(int userId)
        {
            return _userUstGrupDal.GetList(x => x.UserId == userId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public UserNobetUstGrupDetay GetDetayById(int userNobetUstGrupId)
        {
            return _userUstGrupDal.GetDetay(s => s.Id == userNobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserNobetUstGrupDetay> GetDetaylar()
        {
            return _userUstGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserNobetUstGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _userUstGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserNobetUstGrupDetay> GetDetaylar(List<int> nobetUstGrupIdList)
        {
            return _userUstGrupDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }
    }
}
