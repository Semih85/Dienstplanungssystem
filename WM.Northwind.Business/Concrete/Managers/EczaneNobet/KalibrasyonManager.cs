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

        public KalibrasyonDetay GetDetay(int eczaneNobetGrupId, int gunGrupId, int kalibrasyonTipId)
        {
            return _kalibrasyonDal.GetDetay(x => x.EczaneNobetGrupId == eczaneNobetGrupId
                                        && x.GunGrupId == gunGrupId
                                        && x.KalibrasyonTipId == kalibrasyonTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<KalibrasyonDetay> GetDetaylar()
        {
            return _kalibrasyonDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<KalibrasyonDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _kalibrasyonDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<KalibrasyonYatay> GetKalibrasyonlarYatay(int nobetUstGrupId)
        {
            var kalibrasyonlar = GetDetaylar(nobetUstGrupId);

            return (from k in kalibrasyonlar
                    group k by new
                    {
                        k.EczaneNobetGrupId,
                        k.EczaneAdi,
                        k.KalibrasyonTipId,
                        k.KalibrasyonTipAdi
                    } into s
                    let klbr = kalibrasyonlar
                                     .Where(w => w.EczaneNobetGrupId == s.Key.EczaneNobetGrupId
                                              && w.KalibrasyonTipId == s.Key.KalibrasyonTipId
                                              && w.KalibrasyonTipId < 7)
                    let klbrToplam = kalibrasyonlar
                           .Where(w => w.EczaneNobetGrupId == s.Key.EczaneNobetGrupId
                                    && w.KalibrasyonTipId == 7)
                    select new KalibrasyonYatay
                    {
                        EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                        EczaneAdi = s.Key.EczaneAdi,
                        KalibrasyonTipId = s.Key.KalibrasyonTipId,
                        KalibrasyonTipAdi = s.Key.KalibrasyonTipAdi,
                        KalibrasyonCumartesi = klbr
                                     .Where(w => w.GunGrupId == 4)
                                     .Select(s1 => s1.Deger).SingleOrDefault(),
                        KalibrasyonPazar = klbr
                                     .Where(w => w.GunGrupId == 1)
                                     .Select(s1 => s1.Deger).SingleOrDefault(),
                        KalibrasyonHaftaIci = klbr
                                     .Where(w => w.GunGrupId == 3)
                                     .Select(s1 => s1.Deger).SingleOrDefault(),
                        KalibrasyonToplamCumartesi = klbrToplam
                                     .Where(w => w.GunGrupId == 4)
                                              .Select(s1 => s1.Deger).SingleOrDefault(),
                        KalibrasyonToplamPazar = klbrToplam
                                     .Where(w => w.GunGrupId == 1)
                                              .Select(s1 => s1.Deger).SingleOrDefault(),
                        KalibrasyonToplamHaftaIci = klbrToplam
                                     .Where(w => w.GunGrupId == 3)
                                     .Select(s1 => s1.Deger).SingleOrDefault()
                    }).ToList();
        }
    }
}