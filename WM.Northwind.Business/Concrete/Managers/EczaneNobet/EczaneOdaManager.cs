using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    //[SecuredOperation(Roles= "Admin, Oda")]
    public class EczaneOdaManager : IEczaneOdaService
    {
        #region ctor
        private IEczaneOdaDal _eczaneOdaDal;
        private IUserService _userService;
        private IUserEczaneOdaService _userEczaneOdaService;

        public EczaneOdaManager(IEczaneOdaDal eczaneOdaDal,
                                IUserService userService,
                                IUserEczaneOdaService userEczaneOdaService)
        {
            _eczaneOdaDal = eczaneOdaDal;
            _userService = userService;
            _userEczaneOdaService = userEczaneOdaService;
        } 
        #endregion

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int eczaneGrupId)
        {
            _eczaneOdaDal.Delete(new EczaneOda { Id = eczaneGrupId });
        }

        public EczaneOda GetById(int eczaneGrupId)
        {
            return _eczaneOdaDal.Get(x => x.Id == eczaneGrupId);
        }

        public List<EczaneOda> GetListByUser(User user)
        {
            //user roller
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            var eczaneOdalar = new List<EczaneOda>();

            if (rolId == 1)
            {//admin 
                //tüm odalar
                eczaneOdalar = GetList();
            }
            else
            {//oda
                var userOdalar = _userEczaneOdaService.GetListByUserId(user.Id);
                //yetkili olduğu odalar
                eczaneOdalar = GetList().Where(x => userOdalar.Select(s => s.EczaneOdaId).Contains(x.Id)).ToList();
            }

            return eczaneOdalar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneOda> GetList()
        {
            return _eczaneOdaDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(EczaneOda eczaneGrup)
        {
            _eczaneOdaDal.Insert(eczaneGrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(EczaneOda eczaneGrup)
        {
            _eczaneOdaDal.Update(eczaneGrup);
        }
    }
}
