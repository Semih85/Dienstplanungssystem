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
    public class MobilBildirimManager : IMobilBildirimService
    {
        private IMobilBildirimDal _mobilBildirimDal;

        public MobilBildirimManager(IMobilBildirimDal mobilBildirimDal)
        {
            _mobilBildirimDal = mobilBildirimDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int mobilBildirimId)
        {
            _mobilBildirimDal.Delete(new MobilBildirim { Id = mobilBildirimId });
        }

        public MobilBildirim GetById(int mobilBildirimId)
        {
            return _mobilBildirimDal.Get(x => x.Id == mobilBildirimId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<MobilBildirim> GetList()
        {
            return _mobilBildirimDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(MobilBildirim mobilBildirim)
        {
            _mobilBildirimDal.Insert(mobilBildirim);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(MobilBildirim mobilBildirim)
        {
            _mobilBildirimDal.Update(mobilBildirim);
        }
                                  public MobilBildirimDetay GetDetayById(int mobilBildirimId)
            {
                return _mobilBildirimDal.GetDetay(x => x.Id == mobilBildirimId);
            }
            
            [CacheAspect(typeof(MemoryCacheManager))]
            public List<MobilBildirimDetay> GetDetaylar()
            {
                return _mobilBildirimDal.GetDetayList();
            }

    } 
}