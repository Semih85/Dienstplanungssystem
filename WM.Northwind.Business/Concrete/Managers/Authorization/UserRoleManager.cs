using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.Aspects.PostSharp.PerformansCounterAspects;
using WM.Core.Aspects.PostSharp.ValidationAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.ValidationRules.FluentValidation;
using WM.Northwind.DataAccess.Abstract.Authorization;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.Managers.Authorization
{
    public class UserRoleManager : IUserRoleService
    {
        private IUserRoleDal _userRoleDal;

        public EczaneNobetSonucModel Results { get; set; }

        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<UserRole> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _userRoleDal.GetList();
        }

        public UserRole GetById(int userRoleId)
        {
            return _userRoleDal.Get(x => x.Id == userRoleId);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(UserRole userRole)
        {
            _userRoleDal.Insert(userRole);
        }

        [FluentValidationAspect(typeof(EczaneValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(UserRole userRole)
        {
            _userRoleDal.Update(userRole);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int userRoleId)
        {
            _userRoleDal.Delete(new UserRole { Id = userRoleId });
        }

        public List<UserRole> GetListByUserId(int userId)
        {
            return _userRoleDal.GetList(w => w.UserId == userId);
        }

        public UserRoleDetay GetDetayById(int userRoleId)
        {
            return _userRoleDal.GetDetay(x => x.Id == userRoleId);
        }

        public List<UserRoleDetay> GetDetaylar()
        {
            return _userRoleDal.GetDetayList();
        }
    }
}
