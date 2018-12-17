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
    public class EczaneNobetMuafiyetManager : IEczaneNobetMuafiyetService
    {
        private IEczaneNobetMuafiyetDal _eczaneNobetMuafiyetDal;

        public EczaneNobetMuafiyetManager(IEczaneNobetMuafiyetDal eczaneNobetMuafiyetDal)
        {
            _eczaneNobetMuafiyetDal = eczaneNobetMuafiyetDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetMuafiyetId)
        {
            _eczaneNobetMuafiyetDal.Delete(new EczaneNobetMuafiyet { Id = eczaneNobetMuafiyetId });
        }

        public EczaneNobetMuafiyet GetById(int eczaneNobetMuafiyetId)
        {
            return _eczaneNobetMuafiyetDal.Get(x => x.Id == eczaneNobetMuafiyetId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMuafiyet> GetList()
        {
            return _eczaneNobetMuafiyetDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetMuafiyet eczaneNobetMuafiyet)
        {
            _eczaneNobetMuafiyetDal.Insert(eczaneNobetMuafiyet);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetMuafiyet eczaneNobetMuafiyet)
        {
            _eczaneNobetMuafiyetDal.Update(eczaneNobetMuafiyet);
        }
        public EczaneNobetMuafiyetDetay GetDetayById(int eczaneNobetMuafiyetId)
        {
            return _eczaneNobetMuafiyetDal.GetDetay(x => x.Id == eczaneNobetMuafiyetId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMuafiyetDetay> GetDetaylar()
        {
            return _eczaneNobetMuafiyetDal.GetDetayList();
        }

        public List<EczaneNobetMuafiyetDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            return _eczaneNobetMuafiyetDal.GetDetayList(c => c.BaslamaTarihi >= baslangicTarihi && c.BitisTarihi <= bitisTarihi);
        }

        public List<EczaneNobetMuafiyetDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList)
        {
            return _eczaneNobetMuafiyetDal.GetDetayList(c => (c.BaslamaTarihi >= baslangicTarihi && c.BitisTarihi <= bitisTarihi) && nobetGrupIdList.Contains(c.NobetGrupId));
        }
    }
}