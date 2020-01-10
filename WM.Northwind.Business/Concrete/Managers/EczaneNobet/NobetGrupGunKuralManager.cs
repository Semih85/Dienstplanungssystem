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
using WM.Core.Aspects.PostSharp.TranstionAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Core.Aspects.PostSharp.LogAspects;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetGrupGunKuralManager : INobetGrupGunKuralService
    {
        private INobetGrupGunKuralDal _nobetGrupGunKuralDal;

        public NobetGrupGunKuralManager(INobetGrupGunKuralDal nobetGrupGunKuralDal)
        {
            _nobetGrupGunKuralDal = nobetGrupGunKuralDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGrupGunKuralId)
        {
            _nobetGrupGunKuralDal.Delete(new NobetGrupGunKural { Id = nobetGrupGunKuralId });
        }

        public NobetGrupGunKural GetById(int nobetGrupGunKuralId)
        {
            return _nobetGrupGunKuralDal.Get(x => x.Id == nobetGrupGunKuralId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGunKural> GetList()
        {
            return _nobetGrupGunKuralDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGrupGunKural nobetGrupGunKural)
        {
            _nobetGrupGunKuralDal.Insert(nobetGrupGunKural);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGrupGunKural nobetGrupGunKural)
        {
            _nobetGrupGunKuralDal.Update(nobetGrupGunKural);
        }

        public NobetGrupGunKuralDetay GetDetayById(int nobetGrupGunKuralId)
        {
            return _nobetGrupGunKuralDal.GetDetay(x => x.Id == nobetGrupGunKuralId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGunKuralDetay> GetDetaylar()
        {
            return _nobetGrupGunKuralDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGunKuralDetay> GetDetaylar(int nobetGrupId, int nobetGunKuralId)
        {
            return _nobetGrupGunKuralDal.GetDetayList(x => x.NobetGrupId == nobetGrupId && x.NobetGunKuralId == nobetGunKuralId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGunKuralDetay> GetDetaylar(List<int> nobetGrupIdList)
        {
            return _nobetGrupGunKuralDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGunKural> GetList(List<int> nobetGrupIdList)
        {
            return _nobetGrupGunKuralDal.GetList(x => nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGunKural> GetAktifList(List<int> nobetGrupIdList)
        {
            return _nobetGrupGunKuralDal.GetList(x => nobetGrupIdList.Contains(x.NobetGrupId) && x.BitisTarihi == null);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGunKuralDetay> GetDetaylarAktifList(List<int> nobetGrupIdList)
        {
            return _nobetGrupGunKuralDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId) && x.BitisTarihi == null);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluAktifPasifYap(List<NobetGrupGunKuralDetay> nobetGrupGunKuralDetaylar, bool pasifMi)
        {
            _nobetGrupGunKuralDal.CokluAktifPasifYap(nobetGrupGunKuralDetaylar, pasifMi);
        }
    }
}
