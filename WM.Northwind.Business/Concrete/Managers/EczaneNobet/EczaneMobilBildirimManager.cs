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
    public class EczaneMobilBildirimManager : IEczaneMobilBildirimService
    {
        private IEczaneMobilBildirimDal _eczaneMobilBildirimDal;

        public EczaneMobilBildirimManager(IEczaneMobilBildirimDal eczaneMobilBildirimDal)
        {
            _eczaneMobilBildirimDal = eczaneMobilBildirimDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneMobilBildirimId)
        {
            _eczaneMobilBildirimDal.Delete(new EczaneMobilBildirim { Id = eczaneMobilBildirimId });
        }

        public EczaneMobilBildirim GetById(int eczaneMobilBildirimId)
        {
            return _eczaneMobilBildirimDal.Get(x => x.Id == eczaneMobilBildirimId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneMobilBildirim> GetList()
        {
            return _eczaneMobilBildirimDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneMobilBildirim eczaneMobilBildirim)
        {
            _eczaneMobilBildirimDal.Insert(eczaneMobilBildirim);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneMobilBildirim eczaneMobilBildirim)
        {
            _eczaneMobilBildirimDal.Update(eczaneMobilBildirim);
        }
        public EczaneMobilBildirimDetay GetDetayById(int eczaneMobilBildirimId)
        {
            return _eczaneMobilBildirimDal.GetDetay(x => x.Id == eczaneMobilBildirimId);
        }
            
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneMobilBildirimDetay> GetDetaylar()
        {
           return _eczaneMobilBildirimDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneMobilBildirimDetay> GetDetaylar(int mobilBildirimId)
        {
            return _eczaneMobilBildirimDal.GetDetayList(x => x.MobilBildirimId == mobilBildirimId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneMobilBildirimDetay> GetDetaylar(int mobilBildirimId, int eczaneId)
        {
            return _eczaneMobilBildirimDal.GetDetayList(x => x.MobilBildirimId == mobilBildirimId && x.EczaneId == eczaneId);
        }
    } 
}