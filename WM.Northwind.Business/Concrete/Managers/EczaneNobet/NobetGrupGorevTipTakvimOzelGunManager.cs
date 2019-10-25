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
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.Aspects.PostSharp.TranstionAspects;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetGrupGorevTipTakvimOzelGunManager : INobetGrupGorevTipTakvimOzelGunService
    {
        private INobetGrupGorevTipTakvimOzelGunDal _nobetGrupGorevTipTakvimOzelGunDal;

        public NobetGrupGorevTipTakvimOzelGunManager(INobetGrupGorevTipTakvimOzelGunDal nobetGrupGorevTipTakvimOzelGunDal)
        {
            _nobetGrupGorevTipTakvimOzelGunDal = nobetGrupGorevTipTakvimOzelGunDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGrupGorevTipTakvimOzelGunId)
        {
            _nobetGrupGorevTipTakvimOzelGunDal.Delete(new NobetGrupGorevTipTakvimOzelGun { Id = nobetGrupGorevTipTakvimOzelGunId });
        }

        public NobetGrupGorevTipTakvimOzelGun GetById(int nobetGrupGorevTipTakvimOzelGunId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.Get(x => x.Id == nobetGrupGorevTipTakvimOzelGunId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGun> GetList()
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGrupGorevTipTakvimOzelGun nobetGrupGorevTipTakvimOzelGun)
        {
            _nobetGrupGorevTipTakvimOzelGunDal.Insert(nobetGrupGorevTipTakvimOzelGun);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGrupGorevTipTakvimOzelGun nobetGrupGorevTipTakvimOzelGun)
        {
            _nobetGrupGorevTipTakvimOzelGunDal.Update(nobetGrupGorevTipTakvimOzelGun);
        }

        public NobetGrupGorevTipTakvimOzelGunDetay GetDetayById(int nobetGrupGorevTipTakvimOzelGunId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetay(x => x.Id == nobetGrupGorevTipTakvimOzelGunId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar()
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(int nobetUstGrupId, int nobetGrupGorevTipId = 0, int nobetOzelGunId = 0)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId
                         && (x.NobetGrupGorevTipId == nobetGrupGorevTipId || nobetGrupGorevTipId == 0)
                         && (x.NobetOzelGunId == nobetOzelGunId || nobetOzelGunId == 0));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(List<int> nobetUstGrupIdList)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi) && x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar2(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi) && x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarVerilenTarihtenSonrasi(DateTime baslangicTarihi, int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => x.Tarih >= baslangicTarihi && x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarVerilenTarihtenOncesi(DateTime baslangicTarihi, int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => x.Tarih <= baslangicTarihi && x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar2(DateTime? baslangicTarihi, DateTime? bitisTarihi, int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                && (x.Tarih <= bitisTarihi || bitisTarihi == null) 
                                && x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar2ByNobetGrupId(DateTime? baslangicTarihi, DateTime? bitisTarihi, int nobetGrupId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                && (x.Tarih <= bitisTarihi || bitisTarihi == null)
                                && x.NobetGrupId == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => x.Tarih >= baslangicTarihi
            && x.NobetGorevTipId == nobetGorevTipId
            && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi)
            && x.NobetGorevTipId == nobetGorevTipId
            && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler)
        {
            var ss = nobetGrupGorevTipler
                //.Where(w => w.NobetGorevTipId == 2)
                .Select(s => s.Id).ToList();

            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi)
            && ss.Contains(x.NobetGrupGorevTipId)
            //&& ss == x.NobetGrupGorevTipId
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal
                .GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                && (x.Tarih <= bitisTarihi || bitisTarihi == null)
                                && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler)
        {
            var ss = nobetGrupGorevTipler
                //.Where(w => w.NobetGorevTipId == 2)
                .Select(s => s.Id).ToList();

            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => x.Tarih >= baslangicTarihi 
            && ss.Contains(x.NobetGrupGorevTipId)
            //&& ss == x.NobetGrupGorevTipId
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarNobetGrupGorevTipBaslamaTarihindenSonra(List<NobetGrupGorevTipDetay> nobetGrupGorevTipler)
        {
            var ss = nobetGrupGorevTipler
                //.Where(w => w.NobetGorevTipId == 2)
                .Select(s => s.Id).ToList();

            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => x.Tarih >= x.NobetGrupGorevTipBaslamaTarihi
            && ss.Contains(x.NobetGrupGorevTipId)
            //&& ss == x.NobetGrupGorevTipId
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarNobetGrupGorevTipBaslamaTarihindenSonra(NobetGrupGorevTipDetay nobetGrupGorevTip)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => x.Tarih >= x.NobetGrupGorevTipBaslamaTarihi
            && x.NobetGrupGorevTipId == nobetGrupGorevTip.Id
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarNobetGrupGorevTipBaslamaTarihindenSonra(int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipTakvimOzelGunDal.GetDetayList(x => x.Tarih >= x.NobetGrupGorevTipBaslamaTarihi
            && x.NobetGrupGorevTipId == nobetGrupGorevTipId
            );
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<NobetGrupGorevTipTakvimOzelGun> nobetGrupGorevTipTakvimOzelGunler)
        {
            _nobetGrupGorevTipTakvimOzelGunDal.CokluEkle(nobetGrupGorevTipTakvimOzelGunler);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluSil(int[] ids)
        {
            _nobetGrupGorevTipTakvimOzelGunDal.CokluSil(ids);
        }
    }
}