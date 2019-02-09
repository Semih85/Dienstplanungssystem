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
    public class EczaneUzaklikMatrisManager : IEczaneUzaklikMatrisService
    {
        private IEczaneUzaklikMatrisDal _eczaneUzaklikMatrisDal;

        public EczaneUzaklikMatrisManager(IEczaneUzaklikMatrisDal eczaneUzaklikMatrisDal)
        {
            _eczaneUzaklikMatrisDal = eczaneUzaklikMatrisDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneUzaklikMatrisId)
        {
            _eczaneUzaklikMatrisDal.Delete(new EczaneUzaklikMatris { Id = eczaneUzaklikMatrisId });
        }

        public EczaneUzaklikMatris GetById(int eczaneUzaklikMatrisId)
        {
            return _eczaneUzaklikMatrisDal.Get(x => x.Id == eczaneUzaklikMatrisId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneUzaklikMatris> GetList()
        {
            return _eczaneUzaklikMatrisDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneUzaklikMatris eczaneUzaklikMatris)
        {
            _eczaneUzaklikMatrisDal.Insert(eczaneUzaklikMatris);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneUzaklikMatris eczaneUzaklikMatris)
        {
            _eczaneUzaklikMatrisDal.Update(eczaneUzaklikMatris);
        }

        public EczaneUzaklikMatrisDetay GetDetayById(int eczaneUzaklikMatrisId)
        {
            return _eczaneUzaklikMatrisDal.GetDetay(x => x.Id == eczaneUzaklikMatrisId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneUzaklikMatrisDetay> GetDetaylar()
        {
            return _eczaneUzaklikMatrisDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneUzaklikMatrisDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneUzaklikMatrisDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

    }
}