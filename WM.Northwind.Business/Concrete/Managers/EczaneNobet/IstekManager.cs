using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    //[SecuredOperation(Roles= "Admin,Oda")]
    public class IstekManager : IIstekService
    {
        private IIstekDal _istekDal;

        public IstekManager(IIstekDal istekDal)
        {
            _istekDal = istekDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int istekId)
        {
            _istekDal.Delete(new Istek { Id = istekId });
        }

        public Istek GetById(int istekId)
        {
            return _istekDal.Get(x => x.Id == istekId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Istek> GetList()
        {
            return _istekDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(Istek istek)
        {
            _istekDal.Insert(istek);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(Istek istek)
        {
            _istekDal.Update(istek);
        }
    }
}
