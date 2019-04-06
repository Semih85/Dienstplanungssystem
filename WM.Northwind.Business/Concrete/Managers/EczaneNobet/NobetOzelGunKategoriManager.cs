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
    public class NobetOzelGunKategoriManager : INobetOzelGunKategoriService
    {
        private INobetOzelGunKategoriDal _nobetOzelGunKategoriDal;

        public NobetOzelGunKategoriManager(INobetOzelGunKategoriDal nobetOzelGunKategoriDal)
        {
            _nobetOzelGunKategoriDal = nobetOzelGunKategoriDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetOzelGunKategoriId)
        {
            _nobetOzelGunKategoriDal.Delete(new NobetOzelGunKategori { Id = nobetOzelGunKategoriId });
        }

        public NobetOzelGunKategori GetById(int nobetOzelGunKategoriId)
        {
            return _nobetOzelGunKategoriDal.Get(x => x.Id == nobetOzelGunKategoriId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetOzelGunKategori> GetList()
        {
            return _nobetOzelGunKategoriDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetOzelGunKategori nobetOzelGunKategori)
        {
            _nobetOzelGunKategoriDal.Insert(nobetOzelGunKategori);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetOzelGunKategori nobetOzelGunKategori)
        {
            _nobetOzelGunKategoriDal.Update(nobetOzelGunKategori);
        }
                        
    }
}