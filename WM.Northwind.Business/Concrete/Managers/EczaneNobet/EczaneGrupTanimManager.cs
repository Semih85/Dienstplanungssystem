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
    public class EczaneGrupTanimManager : IEczaneGrupTanimService
    {
        private IEczaneGrupTanimDal _eczaneGrupTanimDal;

        public EczaneGrupTanimManager(IEczaneGrupTanimDal eczaneGrupTanimDal)
        {
            _eczaneGrupTanimDal = eczaneGrupTanimDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneGrupTanimId)
        {
            _eczaneGrupTanimDal.Delete(new EczaneGrupTanim { Id = eczaneGrupTanimId });
        }

        public EczaneGrupTanim GetById(int eczaneGrupTanimId)
        {
            return _eczaneGrupTanimDal.Get(x => x.Id == eczaneGrupTanimId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanim> GetList()
        {
            return _eczaneGrupTanimDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneGrupTanim eczaneGrupTanim)
        {
            _eczaneGrupTanimDal.Insert(eczaneGrupTanim);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneGrupTanim eczaneGrupTanim)
        {
            _eczaneGrupTanimDal.Update(eczaneGrupTanim);
        }

        public EczaneGrupTanimDetay GetDetayById(int eczaneGrupTanimId)
        {
            return _eczaneGrupTanimDal.GetDetay(x => x.Id == eczaneGrupTanimId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanimDetay> GetDetaylar()
        {
            return _eczaneGrupTanimDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanimDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneGrupTanimDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanim> GetAktifTanimList(List<int> eczaneGrupTanimIdList)
        {
            return _eczaneGrupTanimDal.GetList(x => eczaneGrupTanimIdList.Contains(x.Id) && (x.BitisTarihi == null && x.PasifMi == false));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanim> GetAktifTanimList(int eczaneGrupTanimId)
        {
            return _eczaneGrupTanimDal.GetList(x => eczaneGrupTanimId == x.Id && (x.BitisTarihi == null && x.PasifMi == false));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanimDetay> GetDetaylar(List<int> nobetUstGrupIdList)
        {
            return _eczaneGrupTanimDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanimDetay> GetDetaylarAktifTanimList(List<int> eczaneGrupTanimIdList)
        {
            return _eczaneGrupTanimDal.GetDetayList(x => eczaneGrupTanimIdList.Contains(x.Id) && (x.BitisTarihi == null && x.PasifMi == false));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanimDetay> GetDetaylarAktifTanimList(int eczaneGrupTanimId)
        {
            return _eczaneGrupTanimDal.GetDetayList(x => eczaneGrupTanimId == x.Id && (x.BitisTarihi == null && x.PasifMi == false));
        }
    }
}