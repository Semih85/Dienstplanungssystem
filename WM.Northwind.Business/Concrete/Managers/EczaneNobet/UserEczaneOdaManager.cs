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
    //[SecuredOperation(Roles= "Admin, Oda")]
    public class UserEczaneOdaManager : IUserEczaneOdaService
    {
        private IUserEczaneOdaDal _userEczaneOdaDal;

        public UserEczaneOdaManager(IUserEczaneOdaDal userEczaneOdaDal)
        {
            _userEczaneOdaDal = userEczaneOdaDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<UserEczaneOda> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _userEczaneOdaDal.GetList();
        }

        public UserEczaneOda GetById(int userEczaneOdaId)
        {
            return _userEczaneOdaDal.Get(x => x.Id == userEczaneOdaId);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(UserEczaneOda userEczaneOda)
        {
            _userEczaneOdaDal.Insert(userEczaneOda);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(UserEczaneOda userEczaneOda)
        {
            _userEczaneOdaDal.Update(userEczaneOda);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int userEczaneOdaId)
        {
            _userEczaneOdaDal.Delete(new UserEczaneOda { Id = userEczaneOdaId });
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserEczaneOda> GetListByUserId(int userId)
        {
            return _userEczaneOdaDal.GetList(x => x.UserId == userId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public UserEczaneOdaDetay GetDetayById(int userEczaneOdaId)
        {
            return _userEczaneOdaDal.GetDetay(s => s.Id == userEczaneOdaId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserEczaneOdaDetay> GetDetaylar()
        {
            return _userEczaneOdaDal.GetDetayList();
        }
    }
}
