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
    public class BayramManager : IBayramService
    {
        private IBayramDal _bayramDal;

        public BayramManager(IBayramDal bayramDal)
        {
            _bayramDal = bayramDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int id)
        {
            _bayramDal.Delete(new Bayram { Id = id });
        }

        public Bayram GetById(int takvimId)
        {
            return _bayramDal.Get(x => x.TakvimId == takvimId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Bayram> GetList()
        {
            return _bayramDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(Bayram bayram)
        {
            _bayramDal.Insert(bayram);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Bayram bayram)
        {
            _bayramDal.Update(bayram);
        }

        public BayramDetay GetDetayById(int id)
        {
            return _bayramDal.GetDetay(x => x.Id == id);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar()
        {
            return _bayramDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(List<int> nobetGrupGorevtipIdList)
        {
            return _bayramDal.GetDetayList(x => nobetGrupGorevtipIdList.Contains(x.Id));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _bayramDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(int takvimId, int nobetGrupId, int nobetGorevTipId)
        {
            return _bayramDal.GetDetayList(x => x.TakvimId == takvimId && x.NobetGrupId == nobetGrupId && x.NobetGorevTipId == nobetGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupId, int nobetGorevTipId)
        {
            return _bayramDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi) && x.NobetGorevTipId == nobetGorevTipId && x.NobetGrupId == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            return _bayramDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi) && x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            return _bayramDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi) && x.NobetGorevTipId == nobetGorevTipId && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(DateTime baslangicTarihi, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            return _bayramDal.GetDetayList(x => x.Tarih >= baslangicTarihi && x.NobetGorevTipId == nobetGorevTipId && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(int yil, int ay, int nobetGrupId, int nobetGorevTipId)
        {
            return _bayramDal.GetDetayList(x => (x.Tarih.Year == yil && x.Tarih.Month == ay) && x.NobetGorevTipId == nobetGorevTipId && x.NobetGrupId == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(int yil, int ay, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            return _bayramDal.GetDetayList(x => (x.Tarih.Year == yil && x.Tarih.Month == ay) && x.NobetGorevTipId == nobetGorevTipId && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramDetay> GetDetaylar(int yil, List<int> nobetGrupIdList, int nobetGorevTipId)
        {
            return _bayramDal.GetDetayList(x => x.Tarih.Year == yil && x.NobetGorevTipId == nobetGorevTipId && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<Bayram> bayramlar)
        {
            _bayramDal.CokluEkle(bayramlar);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluSil(int[] ids)
        {
            _bayramDal.CokluSil(ids);
        }
    }
}
