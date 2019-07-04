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
    public class EczaneNobetDegisimManager : IEczaneNobetDegisimService
    {
        private IEczaneNobetDegisimDal _eczaneNobetDegisimDal;
        //private IEczaneNobetSonucService _eczaneNobetSonucService;

        public EczaneNobetDegisimManager(IEczaneNobetDegisimDal eczaneNobetDegisimDal
            //IEczaneNobetSonucService eczaneNobetSonucService
            )
        {
            _eczaneNobetDegisimDal = eczaneNobetDegisimDal;
            //_eczaneNobetSonucService = eczaneNobetSonucService;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int eczaneNobetDegisimId)
        {
            _eczaneNobetDegisimDal.Delete(new EczaneNobetDegisim { Id = eczaneNobetDegisimId });
        }

        public EczaneNobetDegisim GetById(int eczaneNobetDegisimId)
        {
            return _eczaneNobetDegisimDal.Get(x => x.Id == eczaneNobetDegisimId);
        }

        public EczaneNobetDegisim GetBySonucIdVeNobetGrupId(int eczaneNobetSonucId, int eczaneNobetGrupId)
        {
            return _eczaneNobetDegisimDal.Get(x => x.EczaneNobetSonucId == eczaneNobetSonucId && x.EczaneNobetGrupId == eczaneNobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisim> GetList()
        {
            return _eczaneNobetDegisimDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(EczaneNobetDegisim eczaneNobetDegisim)
        {
            _eczaneNobetDegisimDal.Insert(eczaneNobetDegisim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(EczaneNobetDegisim eczaneNobetDegisim)
        {
            _eczaneNobetDegisimDal.Update(eczaneNobetDegisim);
        }

        public EczaneNobetDegisimDetay GetDetayById(int eczaneNobetDegisimId)
        {
            return _eczaneNobetDegisimDal.GetDetay(x => x.Id == eczaneNobetDegisimId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimDetay> GetDetaylar()
        {
            return _eczaneNobetDegisimDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetDegisimDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }
    }
}