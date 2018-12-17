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
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Core.Aspects.PostSharp.TranstionAspects;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class EczaneNobetSonucPlanlananManager : IEczaneNobetSonucPlanlananService
    {
        private IEczaneNobetSonucPlanlananDal _eczaneNobetSonucPlanlananDal;
        private IEczaneNobetSonucService _eczaneNobetSonucService;

        public EczaneNobetSonucPlanlananManager(IEczaneNobetSonucPlanlananDal eczaneNobetSonucPlanlananDal,
            IEczaneNobetSonucService eczaneNobetSonucService)
        {
            _eczaneNobetSonucPlanlananDal = eczaneNobetSonucPlanlananDal;
            _eczaneNobetSonucService = eczaneNobetSonucService;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetSonucPlanlananId)
        {
            _eczaneNobetSonucPlanlananDal.Delete(new EczaneNobetSonucPlanlanan { Id = eczaneNobetSonucPlanlananId });
        }

        public EczaneNobetSonucPlanlanan GetById(int eczaneNobetSonucPlanlananId)
        {
            return _eczaneNobetSonucPlanlananDal.Get(x => x.Id == eczaneNobetSonucPlanlananId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucPlanlanan> GetList()
        {
            return _eczaneNobetSonucPlanlananDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan)
        {
            _eczaneNobetSonucPlanlananDal.Insert(eczaneNobetSonucPlanlanan);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan)
        {
            _eczaneNobetSonucPlanlananDal.Update(eczaneNobetSonucPlanlanan);
        }

        public EczaneNobetSonucDetay2 GetDetayById(int eczaneNobetSonucPlanlananId)
        {
            return _eczaneNobetSonucPlanlananDal.GetDetay(x => x.Id == eczaneNobetSonucPlanlananId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylar()
        {
            return _eczaneNobetSonucPlanlananDal.GetDetayList();
        }


        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDetay2> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetSonucPlanlananDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> SiraliNobetYaz(int nobetUstGrupId)
        {
            var sonuclar = _eczaneNobetSonucService.GetDetaylarUstGrupBaslamaTarihindenOnce(nobetUstGrupId);
            var sonuclarSirali = _eczaneNobetSonucService.GetSonuclar(sonuclar, nobetUstGrupId)
                .OrderBy(o => o.GunGrup)
                .ThenBy(o => o.Tarih)
                .ToList();

            return sonuclarSirali;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluSil(int[] ids)
        {
            _eczaneNobetSonucPlanlananDal.CokluSil(ids);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler)
        {
            _eczaneNobetSonucPlanlananDal.CokluEkle(eczaneNobetCozumler);
        }
    }
}