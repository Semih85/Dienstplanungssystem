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
    public class AyniGunNobetTakipGrupAltGrupManager : IAyniGunNobetTakipGrupAltGrupService
    {
        private IAyniGunNobetTakipGrupAltGrupDal _ayniGunNobetTakipGrupAltGrupDal;

        public AyniGunNobetTakipGrupAltGrupManager(IAyniGunNobetTakipGrupAltGrupDal ayniGunNobetTakipGrupAltGDal)
        {
            _ayniGunNobetTakipGrupAltGrupDal = ayniGunNobetTakipGrupAltGDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int ayniGunNobetTakipGrupAltGId)
        {
            _ayniGunNobetTakipGrupAltGrupDal.Delete(new AyniGunNobetTakipGrupAltGrup { Id = ayniGunNobetTakipGrupAltGId });
        }

        public AyniGunNobetTakipGrupAltGrup GetById(int ayniGunNobetTakipGrupAltGId)
        {
            return _ayniGunNobetTakipGrupAltGrupDal.Get(x => x.Id == ayniGunNobetTakipGrupAltGId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunNobetTakipGrupAltGrup> GetList()
        {
            return _ayniGunNobetTakipGrupAltGrupDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(AyniGunNobetTakipGrupAltGrup ayniGunNobetTakipGrupAltG)
        {
            _ayniGunNobetTakipGrupAltGrupDal.Insert(ayniGunNobetTakipGrupAltG);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(AyniGunNobetTakipGrupAltGrup ayniGunNobetTakipGrupAltG)
        {
            _ayniGunNobetTakipGrupAltGrupDal.Update(ayniGunNobetTakipGrupAltG);
        }
        public AyniGunNobetTakipGrupAltGrupDetay GetDetayById(int ayniGunNobetTakipGrupAltGId)
        {
            return _ayniGunNobetTakipGrupAltGrupDal.GetDetay(x => x.Id == ayniGunNobetTakipGrupAltGId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunNobetTakipGrupAltGrupDetay> GetDetaylar()
        {
            return _ayniGunNobetTakipGrupAltGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunNobetTakipGrupAltGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _ayniGunNobetTakipGrupAltGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<AyniGunNobetTakipGrupAltGrupDetay> GetDetaylar(List<int> nobetUstGrupIdList)
        {
            return _ayniGunNobetTakipGrupAltGrupDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }
    }
}