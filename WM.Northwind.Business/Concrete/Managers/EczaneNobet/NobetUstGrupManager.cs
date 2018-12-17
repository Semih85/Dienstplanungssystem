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
    //[SecuredOperation(Roles= "Admin,Oda,Üst Grup")]
    public class NobetUstGrupManager : INobetUstGrupService
    {
        #region ctor
        private INobetUstGrupDal _nobetUstGrupDal;
        private IUserService _userService;
        private IEczaneOdaService _eczaneOdaService;
        private IUserNobetUstGrupService _userNobetUstGrupService;

        public NobetUstGrupManager(INobetUstGrupDal nobetUstGrupDal,
                                   IUserService userService,
                                   IEczaneOdaService eczaneOdaService,
                                   IUserNobetUstGrupService userNobetUstGrupService)
        {
            _nobetUstGrupDal = nobetUstGrupDal;
            _userService = userService;
            _eczaneOdaService = eczaneOdaService;
            _userNobetUstGrupService = userNobetUstGrupService;
        }
        #endregion

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int nobetUstGrupId)
        {
            _nobetUstGrupDal.Delete(new NobetUstGrup { Id = nobetUstGrupId });
        }

        public NobetUstGrup GetById(int nobetUstGrupId)
        {
            return _nobetUstGrupDal.Get(x => x.Id == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrup> GetList()
        {
            var liste = _nobetUstGrupDal.GetList();
            return _nobetUstGrupDal.GetList();
        }

        public List<NobetUstGrup> GetListByUser(User user)
        {
            //user roller
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            var nobetUstGruplar = new List<NobetUstGrup>();

            if (rolId == 1)
            {//admin 
                //tüm nöbet üst grupları
                nobetUstGruplar = GetList();
            }
            else if (rolId == 2)
            {//oda
                var eczaneOdalar = _eczaneOdaService.GetListByUser(user);
                //yetkili olduğu nöbet üst gruplar
                nobetUstGruplar = GetList().Where(s => eczaneOdalar.Select(e => e.Id).Contains(s.EczaneOdaId)).ToList();
            }
            else
            {//nöbet üst grup ve diğerleri
                //yetkili olduğu nöbet üst grupları
                var userNobetUstGruplar = _userNobetUstGrupService.GetListByUserId(user.Id);
                nobetUstGruplar = GetList().Where(x => userNobetUstGruplar.Select(s => s.NobetUstGrupId).Contains(x.Id)).ToList();
            }
            return nobetUstGruplar;
        }

        public NobetUstGrupDetay GetDetay(int nobetUstGrupId)
        {
            return _nobetUstGrupDal.GetDetay(x => x.Id == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupDetay> GetDetaylar()
        {
            return _nobetUstGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetUstGrupDal.GetDetayList(x => x.Id == nobetUstGrupId);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(NobetUstGrup nobetUstGrup)
        {
            _nobetUstGrupDal.Insert(nobetUstGrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(NobetUstGrup nobetUstGrup)
        {
            _nobetUstGrupDal.Update(nobetUstGrup);
        }

    }
}
