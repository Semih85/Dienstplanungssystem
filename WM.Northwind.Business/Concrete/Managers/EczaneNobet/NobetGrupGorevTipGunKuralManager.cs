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
    public class NobetGrupGorevTipGunKuralManager : INobetGrupGorevTipGunKuralService
    {
        private INobetGrupGorevTipGunKuralDal _nobetGrupGorevTipGunKuralDal;

        public NobetGrupGorevTipGunKuralManager(INobetGrupGorevTipGunKuralDal nobetGrupGorevTipGunKuralDal)
        {
            _nobetGrupGorevTipGunKuralDal = nobetGrupGorevTipGunKuralDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGrupGorevTipGunKuralId)
        {
            _nobetGrupGorevTipGunKuralDal.Delete(new NobetGrupGorevTipGunKural { Id = nobetGrupGorevTipGunKuralId });
        }

        public NobetGrupGorevTipGunKural GetById(int nobetGrupGorevTipGunKuralId)
        {
            return _nobetGrupGorevTipGunKuralDal.Get(x => x.Id == nobetGrupGorevTipGunKuralId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipGunKural> GetList()
        {
            return _nobetGrupGorevTipGunKuralDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGrupGorevTipGunKural nobetGrupGorevTipGunKural)
        {
            _nobetGrupGorevTipGunKuralDal.Insert(nobetGrupGorevTipGunKural);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGrupGorevTipGunKural nobetGrupGorevTipGunKural)
        {
            _nobetGrupGorevTipGunKuralDal.Update(nobetGrupGorevTipGunKural);
        }

        public NobetGrupGorevTipGunKuralDetay GetDetayById(int nobetGrupGorevTipGunKuralId)
        {
            return _nobetGrupGorevTipGunKuralDal.GetDetay(x => x.Id == nobetGrupGorevTipGunKuralId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipGunKuralDetay> GetDetaylar()
        {
            return _nobetGrupGorevTipGunKuralDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipGunKuralDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetGrupGorevTipGunKuralDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipGunKuralDetay> GetDetaylar(List<int> nobetUstGrupIdList)
        {
            return _nobetGrupGorevTipGunKuralDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipGunKuralDetay> GetDetaylarAktifList(List<int> nobetGrupGorevTipIdList)
        {
            return _nobetGrupGorevTipGunKuralDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId) && x.BitisTarihi == null);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipGunKuralDetay> GetDetaylarByNobetGrupGorevTipIdList(List<int> nobetGrupGorevTipIdList)
        {
            return _nobetGrupGorevTipGunKuralDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipGunKuralDetay> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipGunKuralDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluAktifPasifYap(List<NobetGrupGorevTipGunKuralDetay> nobetGrupGorevTipGunKurallar, bool pasifMi)
        {
            foreach (var nobetGrupGorevTipGunKuralDetay in nobetGrupGorevTipGunKurallar)
            {
                if (pasifMi)
                {
                    var nobetGrupGorevTipGunKural = GetById(nobetGrupGorevTipGunKuralDetay.Id);

                    nobetGrupGorevTipGunKural.BitisTarihi = DateTime.Today;

                    Update(nobetGrupGorevTipGunKural);
                }
                else
                {
                    var nobetGrupGorevTipGunKural = GetById(nobetGrupGorevTipGunKuralDetay.Id);

                    nobetGrupGorevTipGunKural.BitisTarihi = null;

                    Update(nobetGrupGorevTipGunKural);
                }
            }
        }

    }
}