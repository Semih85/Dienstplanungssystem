using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    //[SecuredOperation(Roles= "Admin,Oda,Üst Grup,Eczane")]
    public class EczaneNobetIstekManager : IEczaneNobetIstekService
    {
        private IEczaneNobetIstekDal _eczaneNobetIstekDal;

        public EczaneNobetIstekManager(IEczaneNobetIstekDal eczaneNobetIstekDal)
        {
            _eczaneNobetIstekDal = eczaneNobetIstekDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int Id)
        {
            _eczaneNobetIstekDal.Delete(new EczaneNobetIstek { Id = Id });
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public EczaneNobetIstekDetay GetDetayById(int eczaneNobetIstekId)
        {
            return _eczaneNobetIstekDal.GetDetay(x => x.Id == eczaneNobetIstekId);
        }
        public List<EczaneNobetIstek> GetByCategory(int ustGrupId)
        {
            throw new System.NotImplementedException();
        }

        public EczaneNobetIstek GetById(int eczaneNobetIstekId)
        {
            return _eczaneNobetIstekDal.Get(x => x.Id == eczaneNobetIstekId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstekDetay> GetDetaylar()
        {
            return _eczaneNobetIstekDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstekDetay> GetDetaylar(int yil, int ay, int nobetGrupId)
        {
            return _eczaneNobetIstekDal.GetDetayList(x => x.Yil == yil && x.Ay == ay && x.NobetGrupId == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstekDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetIstekDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstekDetay> GetDetaylar(int yil, int ayBaslangic, int ayBitis, int nobetGrupId)
        {
            return _eczaneNobetIstekDal.GetDetayList(x => x.Yil == yil && (x.Ay >= ayBaslangic && x.Ay <= ayBitis) && x.NobetGrupId == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstekDetay> GetDetaylar(int yil, int ay)
        {
            return _eczaneNobetIstekDal.GetDetayList(x => x.Yil == yil && x.Ay == ay);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstekDetay> GetDetaylar(int yil, int ay, List<int> ecznaneIdList)
        {
            return _eczaneNobetIstekDal.GetDetayList(x => x.Yil == yil && x.Ay == ay && ecznaneIdList.Contains(x.EczaneId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstekDetay> GetDetaylarByNobetGrupIdList(int yil, int ay, List<int> nobetGrupIdList)
        {
            return _eczaneNobetIstekDal.GetDetayList(x => x.Yil == yil && x.Ay == ay && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstekDetay> GetDetaylarByNobetGrupIdList(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList)
        {
            return _eczaneNobetIstekDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi) && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstekDetay> GetDetaylarByTakvimId(int takvimId, int nobetUstGrupId)
        {
            return _eczaneNobetIstekDal.GetDetayList(x => x.TakvimId == takvimId && x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylar(List<EczaneNobetIstekDetay> eczaneNobetIstekDetaylar, List<EczaneGrupDetay> eczaneGrupDetayalar)
        {
            //var istekGirilenEczaneninEsGrubundakiEczaneler = eczaneNobetIstekDetaylar.Where(x => eczaneGrupDetayalar.Select(s => s.EczaneNobetGrupId).Contains(x.EczaneNobetGrupId)).ToList();
            var istekGirilenEczaneninEsGrubundakiEczaneler = eczaneGrupDetayalar.Where(x => eczaneNobetIstekDetaylar.Select(s => s.EczaneNobetGrupId).Contains(x.EczaneNobetGrupId)).ToList();

            return istekGirilenEczaneninEsGrubundakiEczaneler;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstek> GetList()
        {
            return _eczaneNobetIstekDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(EczaneNobetIstek sonuc)
        {
            _eczaneNobetIstekDal.Insert(sonuc);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(EczaneNobetIstek sonuc)
        {
            _eczaneNobetIstekDal.Update(sonuc);
        }

        //public List<EczaneNobetIstekListe> GetListeByNobetGrupIdList(int yil, int ay, List<int> nobetGrupIdList)
        //{
        //    return _eczaneNobetIstekDal.GetEczaneNobetIstekDetaylar(yil, ay, nobetGrupIdList);
        //}
    }
}
