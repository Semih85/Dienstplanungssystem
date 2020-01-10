using System.Collections.Generic;
using System.Linq;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetGrupManager : INobetGrupService
    {
        private INobetGrupDal _nobetGrupDal;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;

        public NobetGrupManager(INobetGrupDal nobetGrupDal,
                                IUserService userService,
                                INobetUstGrupService nobetUstGrupService)
        {
            _nobetGrupDal = nobetGrupDal;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGrupId)
        {
            _nobetGrupDal.Delete(new NobetGrup { Id = nobetGrupId });
        }

        public NobetGrup GetById(int nobetGrupId)
        {
            return _nobetGrupDal.Get(x => x.Id == nobetGrupId);
        }
        
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrup> GetList()
        {
            return _nobetGrupDal.GetList();
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGrup nobetGrup)
        {
            _nobetGrupDal.Insert(nobetGrup);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGrup nobetGrup)
        {
            _nobetGrupDal.Update(nobetGrup);
        }
        public NobetGrupDetay GetDetayById(int nobetGrupId)
        {
            return _nobetGrupDal.GetDetay(x => x.Id == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        //[SecuredOperation(Roles = "Admin,Oda,Üst Grup,Eczane")]
        public List<NobetGrupDetay> GetDetaylar()
        {
            return _nobetGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        //[SecuredOperation(Roles = "Admin,Oda,Üst Grup,Eczane")]
        public List<NobetGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrup> GetListByUser(User user)
        {
            //yetkili olduğu nöbet üst gruplar
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(e => e.Id);
            //yetkili olduğu nöbet gruplar
            var nobetGruplar = GetList()
                       .Where(s => // s.BitisTarihi == null &&
                                      nobetUstGruplar.Contains(s.NobetUstGrupId)).ToList();
            return nobetGruplar;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrup> GetListByNobetUstGrupId(int nobetUstGrupId)
        {
            return _nobetGrupDal.GetList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrup> GetList(List<int> nobetGrupIdList)
        {
            return _nobetGrupDal.GetList(x => nobetGrupIdList.Contains(x.Id));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupDetay> GetDetaylar(List<int> nobetGrupIdList)
        {
            return _nobetGrupDal.GetDetayList(x => nobetGrupIdList.Contains(x.Id));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupIdList)
        {
            return _nobetGrupDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }
    }
}
