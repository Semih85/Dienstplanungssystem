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
//using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
//using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class EczaneNobetDegisimTalepManager : IEczaneNobetDegisimTalepService
    {
        private IEczaneNobetDegisimTalepDal _eczaneNobetDegisimTalepDal;

        public EczaneNobetDegisimTalepManager(IEczaneNobetDegisimTalepDal eczaneNobetDegisimTalepDal)
        {
            _eczaneNobetDegisimTalepDal = eczaneNobetDegisimTalepDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetDegisimTalepId)
        {
            _eczaneNobetDegisimTalepDal.Delete(new EczaneNobetDegisimTalep { Id = eczaneNobetDegisimTalepId });
        }

        public EczaneNobetDegisimTalep GetById(int eczaneNobetDegisimTalepId)
        {
            return _eczaneNobetDegisimTalepDal.Get(x => x.Id == eczaneNobetDegisimTalepId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimTalep> GetList()
        {
            return _eczaneNobetDegisimTalepDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetDegisimTalep eczaneNobetDegisimTalep)
        {
            _eczaneNobetDegisimTalepDal.Insert(eczaneNobetDegisimTalep);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetDegisimTalep eczaneNobetDegisimTalep)
        {
            _eczaneNobetDegisimTalepDal.Update(eczaneNobetDegisimTalep);
        }
        public EczaneNobetDegisimTalepDetay GetDetayById(int eczaneNobetDegisimTalepId)
        {
            return _eczaneNobetDegisimTalepDal.GetDetay(x => x.Id == eczaneNobetDegisimTalepId);
        }
            
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimTalepDetay> GetDetaylar()
        {
            return _eczaneNobetDegisimTalepDal.GetDetayList();
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimTalepDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetDegisimTalepDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }
    } 
}