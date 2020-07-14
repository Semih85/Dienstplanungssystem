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
    public class EczaneNobetSonucAnahtarListeManager : IEczaneNobetSonucAnahtarListeService
    {
        private IEczaneNobetSonucAnahtarListeDal _eczaneNobetSonucAnahtarListeDal;

        public EczaneNobetSonucAnahtarListeManager(IEczaneNobetSonucAnahtarListeDal eczaneNobetSonucAnahtarListeDal)
        {
            _eczaneNobetSonucAnahtarListeDal = eczaneNobetSonucAnahtarListeDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetSonucAnahtarListeId)
        {
            _eczaneNobetSonucAnahtarListeDal.Delete(new EczaneNobetSonucAnahtarListe { Id = eczaneNobetSonucAnahtarListeId });
        }

        public EczaneNobetSonucAnahtarListe GetById(int eczaneNobetSonucAnahtarListeId)
        {
            return _eczaneNobetSonucAnahtarListeDal.Get(x => x.Id == eczaneNobetSonucAnahtarListeId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucAnahtarListe> GetList()
        {
            return _eczaneNobetSonucAnahtarListeDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetSonucAnahtarListe eczaneNobetSonucAnahtarListe)
        {
            _eczaneNobetSonucAnahtarListeDal.Insert(eczaneNobetSonucAnahtarListe);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetSonucAnahtarListe eczaneNobetSonucAnahtarListe)
        {
            _eczaneNobetSonucAnahtarListeDal.Update(eczaneNobetSonucAnahtarListe);
        }
                                  public EczaneNobetSonucAnahtarListeDetay GetDetayById(int eczaneNobetSonucAnahtarListeId)
            {
                return _eczaneNobetSonucAnahtarListeDal.GetDetay(x => x.Id == eczaneNobetSonucAnahtarListeId);
            }
            
            [CacheAspect(typeof(MemoryCacheManager))]
            public List<EczaneNobetSonucAnahtarListeDetay> GetDetaylar()
            {
                return _eczaneNobetSonucAnahtarListeDal.GetDetayList();
            }

    } 
}