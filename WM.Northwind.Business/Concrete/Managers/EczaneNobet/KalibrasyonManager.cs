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

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class KalibrasyonManager : IKalibrasyonService
    {
        private IKalibrasyonDal _kalibrasyonDal;

        public KalibrasyonManager(IKalibrasyonDal kalibrasyonDal)
        {
            _kalibrasyonDal = kalibrasyonDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int kalibrasyonId)
        {
            _kalibrasyonDal.Delete(new Kalibrasyon { Id = kalibrasyonId });
        }

        public Kalibrasyon GetById(int kalibrasyonId)
        {
            return _kalibrasyonDal.Get(x => x.Id == kalibrasyonId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Kalibrasyon> GetList()
        {
            return _kalibrasyonDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(Kalibrasyon kalibrasyon)
        {
            _kalibrasyonDal.Insert(kalibrasyon);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Kalibrasyon kalibrasyon)
        {
            _kalibrasyonDal.Update(kalibrasyon);
        }

        public KalibrasyonDetay GetDetayById(int kalibrasyonId)
        {
            return _kalibrasyonDal.GetDetay(x => x.Id == kalibrasyonId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<KalibrasyonDetay> GetDetaylar()
        {
            return _kalibrasyonDal.GetDetayList();
        }

    }
}