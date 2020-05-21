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
    public class EczaneNobetDegisimArzManager : IEczaneNobetDegisimArzService
    {
        private IEczaneNobetDegisimArzDal _eczaneNobetDegisimTalepDal;
        //private IEczaneNobetSonucService _eczaneNobetSonucService;

        public EczaneNobetDegisimArzManager(IEczaneNobetDegisimArzDal eczaneNobetDegisimTalepDal
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
            _eczaneNobetDegisimTalepDal.Delete(new EczaneNobetDegisimArz { Id = eczaneNobetDegisimId });
        }

        public EczaneNobetDegisimArz GetById(int eczaneNobetDegisimId)
        {
            return _eczaneNobetDegisimTalepDal.Get(x => x.Id == eczaneNobetDegisimId);
        }

        public EczaneNobetDegisimArz GetBySonucIdVeNobetGrupId(int eczaneNobetSonucId, int eczaneNobetGrupId)
        {
            return _eczaneNobetDegisimTalepDal.Get(x => x.EczaneNobetSonucId == eczaneNobetSonucId && x.EczaneNobetGrupId == eczaneNobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimArz> GetList()
        {
            return _eczaneNobetDegisimTalepDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(EczaneNobetDegisimArz eczaneNobetDegisim)
        {
            _eczaneNobetDegisimTalepDal.Insert(eczaneNobetDegisim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(EczaneNobetDegisimArz eczaneNobetDegisim)
        {
            _eczaneNobetDegisimTalepDal.Update(eczaneNobetDegisim);
        }

        //public EczaneNobetDegisimArzDetay GetDetayById(int eczaneNobetDegisimId)
        //{
        //    return _eczaneNobetDegisimTalepDal.GetDetay(x => x.Id == eczaneNobetDegisimId);
        //}

        //[CacheAspect(typeof(MemoryCacheManager))]
        //public List<EczaneNobetDegisimArzDetay> GetDetaylar()
        //{
        //    return _eczaneNobetDegisimTalepDal.GetDetayList();
        //}

        //[CacheAspect(typeof(MemoryCacheManager))]
        //public List<EczaneNobetDegisimArzDetay> GetDetaylar(int nobetUstGrupId)
        //{
        //    return _eczaneNobetDegisimTalepDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        //}
    }
}