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
    public class EczaneIlceManager : IEczaneIlceService
    {
        private IEczaneIlceDal _eczaneIlceDal;
        
        public EczaneIlceManager(IEczaneIlceDal eczaneIlceDal)
        {
            _eczaneIlceDal = eczaneIlceDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<EczaneIlce> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _eczaneIlceDal.GetList();
        }

        public EczaneIlce GetById(int eczaneIlceId)
        {
            return _eczaneIlceDal.Get(x => x.Id == eczaneIlceId);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(EczaneIlce eczaneIlce)
        {
            _eczaneIlceDal.Insert(eczaneIlce);
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(EczaneIlce eczaneIlce)
        {
            _eczaneIlceDal.Update(eczaneIlce);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int eczaneIlceId)
        {
            _eczaneIlceDal.Delete(new EczaneIlce { Id = eczaneIlceId });
        }
    }
}
