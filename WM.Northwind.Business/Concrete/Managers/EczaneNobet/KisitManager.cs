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
    public class KisitManager : IKisitService
    {
        private IKisitDal _kisitDal;

        public KisitManager(IKisitDal kisitDal)
        {
            _kisitDal = kisitDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int kisitId)
        {
            _kisitDal.Delete(new Kisit { Id = kisitId });
        }

        public Kisit GetById(int kisitId)
        {
            return _kisitDal.Get(x => x.Id == kisitId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Kisit> GetList()
        {
            return _kisitDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(Kisit kisit)
        {
            _kisitDal.Insert(kisit);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Kisit kisit)
        {
            _kisitDal.Update(kisit);
        }

        public KisitDetay GetDetayById(int KisitId)
        {
            return _kisitDal.GetDetay(x => x.Id == KisitId);
        }

        public List<KisitDetay> GetDetaylar()
        {
            return _kisitDal.GetDetayList();
        }

        public List<KisitDetay> GetDetaylar(int kisitKategoriId)
        {
            return _kisitDal.GetDetayList(x => x.KisitKategoriId == kisitKategoriId);
        }
    }
}