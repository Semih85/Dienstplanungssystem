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
    public class KisitKategoriManager : IKisitKategoriService
    {
        private IKisitKategoriDal _kisitKategoriDal;

        public KisitKategoriManager(IKisitKategoriDal kisitKategoriDal)
        {
            _kisitKategoriDal = kisitKategoriDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int kisitKategoriId)
        {
            _kisitKategoriDal.Delete(new KisitKategori { Id = kisitKategoriId });
        }

        public KisitKategori GetById(int kisitKategoriId)
        {
            return _kisitKategoriDal.Get(x => x.Id == kisitKategoriId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<KisitKategori> GetList()
        {
            return _kisitKategoriDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(KisitKategori kisitKategori)
        {
            _kisitKategoriDal.Insert(kisitKategori);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(KisitKategori kisitKategori)
        {
            _kisitKategoriDal.Update(kisitKategori);
        }

    }
}