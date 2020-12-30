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
    public class EczaneNobetGrupKisitManager : IEczaneNobetGrupKisitService
    {
        private IEczaneNobetGrupKisitDal _eczaneNobetGrupKisitDal;

        public EczaneNobetGrupKisitManager(IEczaneNobetGrupKisitDal eczaneNobetGrupKisitDal)
        {
            _eczaneNobetGrupKisitDal = eczaneNobetGrupKisitDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetGrupKisitId)
        {
            _eczaneNobetGrupKisitDal.Delete(new EczaneNobetGrupKisit { Id = eczaneNobetGrupKisitId });
        }

        public EczaneNobetGrupKisit GetById(int eczaneNobetGrupKisitId)
        {
            return _eczaneNobetGrupKisitDal.Get(x => x.Id == eczaneNobetGrupKisitId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupKisit> GetList()
        {
            return _eczaneNobetGrupKisitDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetGrupKisit eczaneNobetGrupKisit)
        {
            _eczaneNobetGrupKisitDal.Insert(eczaneNobetGrupKisit);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetGrupKisit eczaneNobetGrupKisit)
        {
            _eczaneNobetGrupKisitDal.Update(eczaneNobetGrupKisit);
        }
        public EczaneNobetGrupKisitDetay GetDetayById(int eczaneNobetGrupKisitId)
        {
            return _eczaneNobetGrupKisitDal.GetDetay(x => x.Id == eczaneNobetGrupKisitId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupKisitDetay> GetDetaylar()
        {
            return _eczaneNobetGrupKisitDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupKisitDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetGrupKisitDal.GetDetayList(x=> x.NobetUstGrupId == nobetUstGrupId);
        }

    }
}