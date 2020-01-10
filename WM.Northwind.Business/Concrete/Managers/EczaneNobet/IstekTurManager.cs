using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.ValidationRules.FluentValidation;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Core.Aspects.PostSharp.ValidationAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Core.Aspects.PostSharp.PerformansCounterAspects;
using System.Threading;
using WM.Core.Aspects.PostSharp.AutorizationAspects;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    //[SecuredOperation(Roles = "Admin")]
    public class IstekTurManager : IIstekTurService
    {
        private IIstekTurDal _istekTurDal;
        
        public IstekTurManager(IIstekTurDal istekTurDal)
        {
            _istekTurDal = istekTurDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<IstekTur> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _istekTurDal.GetList();
        }

        public IstekTur GetById(int istekTurId)
        {
            return _istekTurDal.Get(x => x.Id == istekTurId);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(IstekTur istekTur)
        {
            _istekTurDal.Insert(istekTur);
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(IstekTur istekTur)
        {
            _istekTurDal.Update(istekTur);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int istekTurId)
        {
            _istekTurDal.Delete(new IstekTur { Id = istekTurId });
        }
    }
}
