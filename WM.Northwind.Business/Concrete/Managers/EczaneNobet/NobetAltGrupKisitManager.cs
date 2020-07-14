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
    public class NobetAltGrupKisitManager : INobetAltGrupKisitService
    {
        private INobetAltGrupKisitDal _nobetAltGrupKisitDal;

        public NobetAltGrupKisitManager(INobetAltGrupKisitDal nobetAltGrupKisitDal)
        {
            _nobetAltGrupKisitDal = nobetAltGrupKisitDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetAltGrupKisitId)
        {
            _nobetAltGrupKisitDal.Delete(new NobetAltGrupKisit { Id = nobetAltGrupKisitId });
        }

        public NobetAltGrupKisit GetById(int nobetAltGrupKisitId)
        {
            return _nobetAltGrupKisitDal.Get(x => x.Id == nobetAltGrupKisitId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetAltGrupKisit> GetList()
        {
            return _nobetAltGrupKisitDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetAltGrupKisit nobetAltGrupKisit)
        {
            _nobetAltGrupKisitDal.Insert(nobetAltGrupKisit);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetAltGrupKisit nobetAltGrupKisit)
        {
            _nobetAltGrupKisitDal.Update(nobetAltGrupKisit);
        }
                                  public NobetAltGrupKisitDetay GetDetayById(int nobetAltGrupKisitId)
            {
                return _nobetAltGrupKisitDal.GetDetay(x => x.Id == nobetAltGrupKisitId);
            }
            
            [CacheAspect(typeof(MemoryCacheManager))]
            public List<NobetAltGrupKisitDetay> GetDetaylar()
            {
                return _nobetAltGrupKisitDal.GetDetayList();
            }

    } 
}