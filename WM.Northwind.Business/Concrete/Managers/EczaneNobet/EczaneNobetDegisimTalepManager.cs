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
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class EczaneNobetDegisimTalepManager : IEczaneNobetDegisimTalepService
    {
        private IEczaneNobetDegisimTalepDal _eczaneNobetDegisimTalepDal;
        //private IEczaneNobetSonucService _eczaneNobetSonucService;

        public EczaneNobetDegisimTalepManager(IEczaneNobetDegisimTalepDal eczaneNobetDegisimTalepDal
            //IEczaneNobetSonucService eczaneNobetSonucService
            )
        {
            _eczaneNobetDegisimTalepDal = eczaneNobetDegisimTalepDal;
            //_eczaneNobetSonucService = eczaneNobetSonucService;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int eczaneNobetDegisimId)
        {
            _eczaneNobetDegisimTalepDal.Delete(new EczaneNobetDegisimTalep { Id = eczaneNobetDegisimId });
        }

        public EczaneNobetDegisimTalep GetById(int eczaneNobetDegisimId)
        {
            return _eczaneNobetDegisimTalepDal.Get(x => x.Id == eczaneNobetDegisimId);
        }

        public EczaneNobetDegisimTalep GetBySonucIdVeNobetGrupId(int eczaneNobetSonucId, int eczaneNobetGrupId)
        {
            return _eczaneNobetDegisimTalepDal.Get(x => x.EczaneNobetSonucId == eczaneNobetSonucId && x.EczaneNobetGrupId == eczaneNobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimTalep> GetList()
        {
            return _eczaneNobetDegisimTalepDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(EczaneNobetDegisimTalep eczaneNobetDegisim)
        {
            _eczaneNobetDegisimTalepDal.Insert(eczaneNobetDegisim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(EczaneNobetDegisimTalep eczaneNobetDegisim)
        {
            _eczaneNobetDegisimTalepDal.Update(eczaneNobetDegisim);
        }

        //public EczaneNobetDegisimTalepDetay GetDetayById(int eczaneNobetDegisimId)
        //{
        //    return _eczaneNobetDegisimTalepDal.GetDetay(x => x.Id == eczaneNobetDegisimId);
        //}

        //[CacheAspect(typeof(MemoryCacheManager))]
        //public List<EczaneNobetDegisimTalepDetay> GetDetaylar()
        //{
        //    return _eczaneNobetDegisimTalepDal.GetDetayList();
        //}

        //[CacheAspect(typeof(MemoryCacheManager))]
        //public List<EczaneNobetDegisimTalepDetay> GetDetaylar(int nobetUstGrupId)
        //{
        //    return _eczaneNobetDegisimTalepDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        //}
    }
}