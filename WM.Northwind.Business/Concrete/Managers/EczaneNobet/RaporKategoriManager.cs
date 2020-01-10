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
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class RaporKategoriManager : IRaporKategoriService
    {
        private IRaporKategoriDal _raporKategoriDal;

        public RaporKategoriManager(IRaporKategoriDal raporKategoriDal)
        {
            _raporKategoriDal = raporKategoriDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int raporKategoriId)
        {
            _raporKategoriDal.Delete(new RaporKategori { Id = raporKategoriId });
        }

        public RaporKategori GetById(int raporKategoriId)
        {
            return _raporKategoriDal.Get(x => x.Id == raporKategoriId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<RaporKategori> GetList()
        {
            return _raporKategoriDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(RaporKategori raporKategori)
        {
            _raporKategoriDal.Insert(raporKategori);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(RaporKategori raporKategori)
        {
            _raporKategoriDal.Update(raporKategori);
        }
                        
    }
}