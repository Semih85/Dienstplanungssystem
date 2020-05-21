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
    public class MobilUygulamaYetkiManager : IMobilUygulamaYetkiService
    {
        private IMobilUygulamaYetkiDal _mobilUygulamaYetkiDal;

        public MobilUygulamaYetkiManager(IMobilUygulamaYetkiDal mobilUygulamaYetkiDal)
        {
            _mobilUygulamaYetkiDal = mobilUygulamaYetkiDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int mobilUygulamaYetkiId)
        {
            _mobilUygulamaYetkiDal.Delete(new MobilUygulamaYetki { Id = mobilUygulamaYetkiId });
        }

        public MobilUygulamaYetki GetById(int mobilUygulamaYetkiId)
        {
            return _mobilUygulamaYetkiDal.Get(x => x.Id == mobilUygulamaYetkiId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<MobilUygulamaYetki> GetList()
        {
            return _mobilUygulamaYetkiDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(MobilUygulamaYetki mobilUygulamaYetki)
        {
            _mobilUygulamaYetkiDal.Insert(mobilUygulamaYetki);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(MobilUygulamaYetki mobilUygulamaYetki)
        {
            _mobilUygulamaYetkiDal.Update(mobilUygulamaYetki);
        }
                        

    } 
}