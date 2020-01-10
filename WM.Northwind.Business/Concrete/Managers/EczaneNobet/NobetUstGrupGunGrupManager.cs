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
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetUstGrupGunGrupManager : INobetUstGrupGunGrupService
    {
        private INobetUstGrupGunGrupDal _nobetUstGrupGunGrupDal;

        public NobetUstGrupGunGrupManager(INobetUstGrupGunGrupDal nobetUstGrupGunGrupDal)
        {
            _nobetUstGrupGunGrupDal = nobetUstGrupGunGrupDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetUstGrupGunGrupId)
        {
            _nobetUstGrupGunGrupDal.Delete(new NobetUstGrupGunGrup { Id = nobetUstGrupGunGrupId });
        }

        public NobetUstGrupGunGrup GetById(int nobetUstGrupGunGrupId)
        {
            return _nobetUstGrupGunGrupDal.Get(x => x.Id == nobetUstGrupGunGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupGunGrup> GetList()
        {
            return _nobetUstGrupGunGrupDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetUstGrupGunGrup nobetUstGrupGunGrup)
        {
            _nobetUstGrupGunGrupDal.Insert(nobetUstGrupGunGrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetUstGrupGunGrup nobetUstGrupGunGrup)
        {
            _nobetUstGrupGunGrupDal.Update(nobetUstGrupGunGrup);
        }

        public NobetUstGrupGunGrupDetay GetDetayById(int nobetUstGrupGunGrupId)
        {
            return _nobetUstGrupGunGrupDal.GetDetay(x => x.Id == nobetUstGrupGunGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupGunGrupDetay> GetDetaylar()
        {
            return _nobetUstGrupGunGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupGunGrupDetay> GetDetaylar(int nobetUstGupId)
        {
            return _nobetUstGrupGunGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupGunGrupDetay> GetDetaylarByNobetUstGupIdList(List<int> nobetUstGupIdList)
        {
            return _nobetUstGrupGunGrupDal.GetDetayList(x => nobetUstGupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<MyDrop> GetMyDrop(List<NobetUstGrupGunGrupDetay> nobetUstGrupGunGrupDetaylar)
        {
            return nobetUstGrupGunGrupDetaylar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.GunGrupAdi}" }).ToList();
        }

    }
}