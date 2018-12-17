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
    public class IlceManager : IIlceService
    {
        private IIlceDal _ilceDal;
        
        public IlceManager(IIlceDal ilceDal)
        {
            _ilceDal = ilceDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<Ilce> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _ilceDal.GetList();
        }

        public Ilce GetById(int ilceId)
        {
            return _ilceDal.Get(x => x.Id == ilceId);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(Ilce ilce)
        {
            _ilceDal.Insert(ilce);
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(Ilce ilce)
        {
            _ilceDal.Update(ilce);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int ilceId)
        {
            _ilceDal.Delete(new Ilce { Id = ilceId });
        }

        public IlceDetay GetDetayById(int ilceId)
        {
            return _ilceDal.GetDetay(x => x.Id == ilceId);
        }

        public List<IlceDetay> GetListDetay()
        {
            return _ilceDal.GetListDetay();
        }
    }
}
