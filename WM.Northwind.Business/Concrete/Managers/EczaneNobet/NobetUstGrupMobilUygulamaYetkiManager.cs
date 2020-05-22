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
    public class NobetUstGrupMobilUygulamaYetkiManager : INobetUstGrupMobilUygulamaYetkiService
    {
        private INobetUstGrupMobilUygulamaYetkiDal _nobetUstGrupMobilUygulamaYetkiDal;

        public NobetUstGrupMobilUygulamaYetkiManager(INobetUstGrupMobilUygulamaYetkiDal nobetUstGrupMobilUygulamaYetkiDal)
        {
            _nobetUstGrupMobilUygulamaYetkiDal = nobetUstGrupMobilUygulamaYetkiDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetUstGrupMobilUygulamaYetkiId)
        {
            _nobetUstGrupMobilUygulamaYetkiDal.Delete(new NobetUstGrupMobilUygulamaYetki { Id = nobetUstGrupMobilUygulamaYetkiId });
        }

        public NobetUstGrupMobilUygulamaYetki GetById(int nobetUstGrupMobilUygulamaYetkiId)
        {
            return _nobetUstGrupMobilUygulamaYetkiDal.Get(x => x.Id == nobetUstGrupMobilUygulamaYetkiId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupMobilUygulamaYetki> GetList()
        {
            return _nobetUstGrupMobilUygulamaYetkiDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetUstGrupMobilUygulamaYetki nobetUstGrupMobilUygulamaYetki)
        {
            _nobetUstGrupMobilUygulamaYetkiDal.Insert(nobetUstGrupMobilUygulamaYetki);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetUstGrupMobilUygulamaYetki nobetUstGrupMobilUygulamaYetki)
        {
            _nobetUstGrupMobilUygulamaYetkiDal.Update(nobetUstGrupMobilUygulamaYetki);
        }
        public NobetUstGrupMobilUygulamaYetkiDetay GetDetayById(int nobetUstGrupMobilUygulamaYetkiId)
        {
            return _nobetUstGrupMobilUygulamaYetkiDal.GetDetay(x => x.Id == nobetUstGrupMobilUygulamaYetkiId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupMobilUygulamaYetkiDetay> GetDetaylar()
        {
            return _nobetUstGrupMobilUygulamaYetkiDal.GetDetayList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupMobilUygulamaYetkiDetay> GetDetayListByNobetUstGrupId(int nobetUstGrupId)
        {
            return _nobetUstGrupMobilUygulamaYetkiDal.GetDetayList(w => w.NobetUstGrupId == nobetUstGrupId);
        }
    }
}