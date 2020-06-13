using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
//using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
//using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetUstGrupKisitIstisnaGunGrupManager : INobetUstGrupKisitIstisnaGunGrupService
    {
        private INobetUstGrupKisitIstisnaGunGrupDal _nobetUstGrupKisitIstisnaGunGrupDal;

        public NobetUstGrupKisitIstisnaGunGrupManager(INobetUstGrupKisitIstisnaGunGrupDal nobetUstGrupKisitIstisnaGunGrupDal)
        {
            _nobetUstGrupKisitIstisnaGunGrupDal = nobetUstGrupKisitIstisnaGunGrupDal;
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetUstGrupKisitIstisnaGunGrupId)
        {
            _nobetUstGrupKisitIstisnaGunGrupDal.Delete(new NobetUstGrupKisitIstisnaGunGrup { Id = nobetUstGrupKisitIstisnaGunGrupId });
        }

        public NobetUstGrupKisitIstisnaGunGrup GetById(int nobetUstGrupKisitIstisnaGunGrupId)
        {
            return _nobetUstGrupKisitIstisnaGunGrupDal.Get(x => x.Id == nobetUstGrupKisitIstisnaGunGrupId);
        }
        
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupKisitIstisnaGunGrup> GetList()
        {
            return _nobetUstGrupKisitIstisnaGunGrupDal.GetList();
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetUstGrupKisitIstisnaGunGrup nobetUstGrupKisitIstisnaGunGrup)
        {
            _nobetUstGrupKisitIstisnaGunGrupDal.Insert(nobetUstGrupKisitIstisnaGunGrup);
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetUstGrupKisitIstisnaGunGrup nobetUstGrupKisitIstisnaGunGrup)
        {
            _nobetUstGrupKisitIstisnaGunGrupDal.Update(nobetUstGrupKisitIstisnaGunGrup);
        }
        public NobetUstGrupKisitIstisnaGunGrupDetay GetDetayById(int nobetUstGrupKisitIstisnaGunGrupId)
        {
            return _nobetUstGrupKisitIstisnaGunGrupDal.GetDetay(x => x.Id == nobetUstGrupKisitIstisnaGunGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupKisitIstisnaGunGrupDetay> GetDetaylar()
        {
            return _nobetUstGrupKisitIstisnaGunGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupKisitIstisnaGunGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetUstGrupKisitIstisnaGunGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

    }
}