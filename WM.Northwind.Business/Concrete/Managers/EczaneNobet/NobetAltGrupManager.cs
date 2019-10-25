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
    public class NobetAltGrupManager : INobetAltGrupService
    {
        private INobetAltGrupDal _nobetAltGrupDal;

        public NobetAltGrupManager(INobetAltGrupDal nobetAltGrupDal)
        {
            _nobetAltGrupDal = nobetAltGrupDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetAltGrupId)
        {
            _nobetAltGrupDal.Delete(new NobetAltGrup { Id = nobetAltGrupId });
        }

        public NobetAltGrup GetById(int nobetAltGrupId)
        {
            return _nobetAltGrupDal.Get(x => x.Id == nobetAltGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetAltGrup> GetList()
        {
            return _nobetAltGrupDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetAltGrup nobetAltGrup)
        {
            _nobetAltGrupDal.Insert(nobetAltGrup);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetAltGrup nobetAltGrup)
        {
            _nobetAltGrupDal.Update(nobetAltGrup);
        }

        public NobetAltGrupDetay GetDetayById(int nobetAltGrupId)
        {
            return _nobetAltGrupDal.GetDetay(x => x.Id == nobetAltGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetAltGrupDetay> GetDetaylar()
        {
            return _nobetAltGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetAltGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetAltGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetAltGrupDetay> GetDetaylarByNobetUstGrup(List<int> nobetUstGrupIdList)
        {
            return _nobetAltGrupDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetAltGrupDetay> GetDetaylarByNobetGrupId(int nobetGrupId)
        {
            return _nobetAltGrupDal.GetDetayList(x => x.NobetGrupId == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetAltGrupDetay> GetDetaylar(List<int> nobetGrupGorevTipIdList)
        {
            return _nobetAltGrupDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGruptakiAltGrup> GetNobetGruptakiAltGrupSayisi(int nobetUstGrupId)
        {
            var sonuc = _nobetAltGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId)
                .GroupBy(g => new
                {
                    g.NobetGrupId,
                    g.NobetGrupAdi
                })
                .Select(s => new NobetGruptakiAltGrup
                {
                    NobetGrupId = s.Key.NobetGrupId,
                    NobetGrupAdi = s.Key.NobetGrupAdi,
                    AltGrupSayisi = s.Count()
                }).ToList();

            return sonuc;
        }
    }
}