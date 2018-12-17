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
    public class SehirManager : ISehirService
    {
        private ISehirDal _sehirDal;
        
        public SehirManager(ISehirDal sehirDal)
        {
            _sehirDal = sehirDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<Sehir> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _sehirDal.GetList();
        }

        public Sehir GetById(int sehirId)
        {
            return _sehirDal.Get(x => x.Id == sehirId);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(Sehir sehir)
        {
            _sehirDal.Insert(sehir);
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(Sehir sehir)
        {
            _sehirDal.Update(sehir);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int sehirId)
        {
            _sehirDal.Delete(new Sehir { Id = sehirId });
        }

        public SehirDetay GetDetayById(int sehirId)
        {
            return _sehirDal.GetDetay(x => x.Id == sehirId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<SehirDetay> GetDetaylar()
        {
            return _sehirDal.GetDetayList();
        }
    }
}
