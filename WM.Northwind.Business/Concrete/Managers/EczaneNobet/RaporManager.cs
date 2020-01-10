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
    public class RaporManager : IRaporService
    {
        private IRaporDal _eczaneNobetRaporDal;

        public RaporManager(IRaporDal eczaneNobetRaporDal)
        {
            _eczaneNobetRaporDal = eczaneNobetRaporDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetRaporId)
        {
            _eczaneNobetRaporDal.Delete(new Rapor { Id = eczaneNobetRaporId });
        }

        public Rapor GetById(int eczaneNobetRaporId)
        {
            return _eczaneNobetRaporDal.Get(x => x.Id == eczaneNobetRaporId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Rapor> GetList()
        {
            return _eczaneNobetRaporDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(Rapor eczaneNobetRapor)
        {
            _eczaneNobetRaporDal.Insert(eczaneNobetRapor);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Rapor eczaneNobetRapor)
        {
            _eczaneNobetRaporDal.Update(eczaneNobetRapor);
        }
                          public RaporDetay GetDetayById(int eczaneNobetRaporId)
    {
        return _eczaneNobetRaporDal.GetDetay(x => x.Id == eczaneNobetRaporId);
    }
    
    [CacheAspect(typeof(MemoryCacheManager))]
    public List<RaporDetay> GetDetaylar()
    {
        return _eczaneNobetRaporDal.GetDetayList();
    }

    }
}