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
    public class EczaneNobetSanalSonucManager : IEczaneNobetSanalSonucService
    {
        private IEczaneNobetSanalSonucDal _eczaneNobetSanalSonucDal;

        public EczaneNobetSanalSonucManager(IEczaneNobetSanalSonucDal eczaneNobetSanalSonucDal)
        {
            _eczaneNobetSanalSonucDal = eczaneNobetSanalSonucDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetSanalSonucId)
        {
            _eczaneNobetSanalSonucDal.Delete(new EczaneNobetSanalSonuc { EczaneNobetSonucId = eczaneNobetSanalSonucId });
        }

        public EczaneNobetSanalSonuc GetById(int eczaneNobetSanalSonucId)
        {
            return _eczaneNobetSanalSonucDal.Get(x => x.EczaneNobetSonucId == eczaneNobetSanalSonucId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSanalSonuc> GetList()
        {
            return _eczaneNobetSanalSonucDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetSanalSonuc eczaneNobetSanalSonuc)
        {
            _eczaneNobetSanalSonucDal.Insert(eczaneNobetSanalSonuc);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetSanalSonuc eczaneNobetSanalSonuc)
        {
            _eczaneNobetSanalSonucDal.Update(eczaneNobetSanalSonuc);
        }
        public EczaneNobetSanalSonucDetay GetDetayById(int eczaneNobetSanalSonucId)
        {
            return _eczaneNobetSanalSonucDal.GetDetay(x => x.Id == eczaneNobetSanalSonucId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSanalSonucDetay> GetDetaylar()
        {
            return _eczaneNobetSanalSonucDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSanalSonucDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetSanalSonucDal.GetDetayList(x=> x.NobetUstGrupId == nobetUstGrupId);
        }

    }
}