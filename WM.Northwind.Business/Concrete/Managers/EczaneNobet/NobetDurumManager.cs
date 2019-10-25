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
    public class NobetDurumManager : INobetDurumService
    {
        private INobetDurumDal _nobetDurumDal;

        public NobetDurumManager(INobetDurumDal nobetDurumDal)
        {
            _nobetDurumDal = nobetDurumDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetDurumId)
        {
            _nobetDurumDal.Delete(new NobetDurum { Id = nobetDurumId });
        }

        public NobetDurum GetById(int nobetDurumId)
        {
            return _nobetDurumDal.Get(x => x.Id == nobetDurumId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetDurum> GetList()
        {
            return _nobetDurumDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetDurum nobetDurum)
        {
            _nobetDurumDal.Insert(nobetDurum);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetDurum nobetDurum)
        {
            _nobetDurumDal.Update(nobetDurum);
        }
        public NobetDurumDetay GetDetayById(int nobetDurumId)
        {
            return _nobetDurumDal.GetDetay(x => x.Id == nobetDurumId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetDurumDetay> GetDetaylar()
        {
            return _nobetDurumDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetDurumDetay> GetDetaylar(List<int> nobetUstGruplar)
        {
            return _nobetDurumDal.GetDetayList(x => nobetUstGruplar.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetDurumDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetDurumDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        //[CacheAspect(typeof(MemoryCacheManager))]
        //public List<NobetDurumDetay> GetDetaylar(int[] nobetGrupGorevTipIdList)
        //{
        //    return _nobetDurumDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetUstGrupId));
        //}

    }
}