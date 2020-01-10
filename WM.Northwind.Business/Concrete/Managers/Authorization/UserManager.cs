using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Aspects.PostSharp.ValidationAspects;
using WM.Core.DAL;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.DataAccess.Abstract.Authorization;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.ValidationRules.FluentValidation;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;

namespace WM.Northwind.Business.Concrete.Managers.Authorization
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        //[FluentValidationAspect(typeof(LoginItemValidator))]
        [CacheAspect(typeof(MemoryCacheManager))]
        public User GetByEMailAndPassword(LoginItem loginItem)
        {
            return _userDal.Get(u => u.Email.Equals(loginItem.Email)
                                  && u.Password.Equals(loginItem.Password)
                                  //String.Compare(u.Email, loginItem.Email, true) == 0
                                  //&& String.Compare(u.Password, loginItem.Password, true) == 0
                                  //   u.Email.Equals(loginItem.Email, StringComparison.Ordinal)
                                  //&& u.Password.Equals(loginItem.Password, StringComparison.Ordinal)
                                  );
        }

        [LogAspect(typeof(DatabaseLogger))]
        //[FluentValidationAspect(typeof(LoginItemValidator))]
        [CacheAspect(typeof(MemoryCacheManager))]
        public User GetByEMailOrUserNamaAndPassword(LoginItem loginItem)
        {
            return _userDal.Get(u => (u.Email.Equals(loginItem.Email) || u.UserName.Equals(loginItem.Email))
                                  && u.Password.Equals(loginItem.Password)
                                  //String.Compare(u.Email, loginItem.Email, true) == 0
                                  //&& String.Compare(u.Password, loginItem.Password, true) == 0
                                  //   u.Email.Equals(loginItem.Email, StringComparison.Ordinal)
                                  //&& u.Password.Equals(loginItem.Password, StringComparison.Ordinal)
                                  );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public User GetByUserNameAndPassword(string userName, string password)
        {
            return _userDal.Get(u => u.UserName == userName & u.Password == password);
        }

        public User GetById(int id)
        {
            return _userDal.Get(u => u.Id == id);
        }

        public User GetByUserName(string userName)
        {
            return _userDal.Get(u => u.UserName == userName && u.BitisTarihi == null);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<UserRoleItem> GetUserRoles(User user)
        {
            return _userDal.GetUserRoles(user);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [FluentValidationAspect(typeof(UserValidator))]
        public void Insert(User user)
        {
            _userDal.Insert(user);
        }

        [LogAspect(typeof(DatabaseLogger))]
        public void Update(User user)
        {
            _userDal.Update(user);
        }

        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int userId)
        {
            _userDal.Delete(new User { Id = userId });
        }

        public List<User> GetList()
        {
            return _userDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<User> GetList(List<int> userIdList)
        {
            return _userDal.GetList(x => userIdList.Contains(x.Id));
        }
    }
}
