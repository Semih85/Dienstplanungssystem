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
    public class EczaneNobetGrupAltGrupManager : IEczaneNobetGrupAltGrupService
    {
        private IEczaneNobetGrupAltGrupDal _eczaneNobetGrupAltGrupDal;

        public EczaneNobetGrupAltGrupManager(IEczaneNobetGrupAltGrupDal eczaneNobetGrupAltGrupDal)
        {
            _eczaneNobetGrupAltGrupDal = eczaneNobetGrupAltGrupDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetGrupAltGrupId)
        {
            _eczaneNobetGrupAltGrupDal.Delete(new EczaneNobetGrupAltGrup { Id = eczaneNobetGrupAltGrupId });
        }

        public EczaneNobetGrupAltGrup GetById(int eczaneNobetGrupAltGrupId)
        {
            return _eczaneNobetGrupAltGrupDal.Get(x => x.Id == eczaneNobetGrupAltGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupAltGrup> GetList()
        {
            return _eczaneNobetGrupAltGrupDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetGrupAltGrup eczaneNobetGrupAltGrup)
        {
            _eczaneNobetGrupAltGrupDal.Insert(eczaneNobetGrupAltGrup);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetGrupAltGrup eczaneNobetGrupAltGrup)
        {
            _eczaneNobetGrupAltGrupDal.Update(eczaneNobetGrupAltGrup);
        }

        public EczaneNobetGrupAltGrupDetay GetDetayById(int eczaneNobetGrupAltGrupId)
        {
            return _eczaneNobetGrupAltGrupDal.GetDetay(x => x.Id == eczaneNobetGrupAltGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupAltGrupDetay> GetDetaylar()
        {
            return _eczaneNobetGrupAltGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupAltGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetGrupAltGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupAltGrupDetay> GetDetaylar(int nobetUstGrupId, int? nobetAltGrupId)
        {
            return _eczaneNobetGrupAltGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId &&
            (x.NobetAltGrupId == nobetAltGrupId || nobetAltGrupId == 0)
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupAltGrupDetay> GetDetaylar(List<int> nobetUstGrupIdList)
        {
            return _eczaneNobetGrupAltGrupDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupAltGrupDetay> GetDetaylarByNobetGrupId(int nobetGrupId)
        {
            return _eczaneNobetGrupAltGrupDal.GetDetayList(x => x.NobetGrupId == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupAltGrupDetay> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId)
        {
            return _eczaneNobetGrupAltGrupDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupAltGrupDetay> GetDetaylarByNobetGrupGorevTipId(int[] nobetGrupGorevTipIdler)
        {
            return _eczaneNobetGrupAltGrupDal.GetDetayList(x => nobetGrupGorevTipIdler.Contains(x.NobetGrupGorevTipId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public EczaneNobetGrupAltGrupDetay GetDetayByEczaneNobetGrupId(int eczaneNobetGrupId)
        {
            return _eczaneNobetGrupAltGrupDal.GetDetay(x => x.EczaneNobetGrupId == eczaneNobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetAltGruptakiEczane> GetNobetAltGruptakiEczaneSayisi(int nobetUstGrupId)
        {
            var sonuc = _eczaneNobetGrupAltGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId)
                .GroupBy(g => new
                {
                    g.NobetGrupId,
                    g.NobetGrupAdi,
                    g.NobetAltGrupId,
                    g.NobetAltGrupAdi
                })
                .Select(s => new NobetAltGruptakiEczane
                {
                    NobetGrupId = s.Key.NobetGrupId,
                    NobetGrupAdi = s.Key.NobetGrupAdi,
                    NobetAltGrupId = s.Key.NobetAltGrupId,
                    NobetAltGrupAdi = s.Key.NobetAltGrupAdi,
                    EczaneSayisi = s.Count()
                }).ToList();

            return sonuc;
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<EczaneNobetGrupAltGrup> eczaneNobetGrupAltGruplar)
        {
            _eczaneNobetGrupAltGrupDal.CokluEkle(eczaneNobetGrupAltGruplar);
        }
    }
}