using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IEczaneGrupService
    {
        EczaneGrup GetById(int eczaneGrupId);
        List<EczaneGrup> GetList();
        //List<EczaneGrup> GetByCategory(int categoryId);
        void Insert(EczaneGrup eczaneGrup);
        void Update(EczaneGrup eczaneGrup);
        void Delete(int eczaneGrupId);
        void CokluEkle(List<EczaneGrup> eczaneGruplar);

        List<EczaneGrupDetay> GetDetaylar();
        List<EczaneGrupDetay> GetDetayById(int eczaneGrupDetayId);
        List<EczaneGrupDetay> GetDetaylar(int nobetUstGrupId);
        List<EczaneGrupDetay> GetDetaylarByEczaneGrupTanimId(List<int> eczaneGrupTanimIdList);
        List<EczaneGrupDetay> GetDetaylarByEczaneGrupTanimId(int eczaneGrupTanimId);

        List<EczaneGrupDetay> GetDetaylarByNobetGrupId(int nobetGrupId);
        List<EczaneGrupDetay> GetDetaylarAktifGruplar();
        List<EczaneGrupDetay> GetDetaylarAktifGruplar(int nobetUstGrupId);
        List<EczaneGrupDetay> GetDetaylarAktif(int eczaneNobetGrupId);
        List<EczaneGrupDetay> GetDetaylarEczaneninEsOlduguEczaneler(int eczaneNobetGrupId);
        List<EczaneGrupDetay> GetDetaylarEczaneninEsOlduguEczaneler(List<int> eczaneNobetGrupIdList);
        List<EczaneGrupDetay> GetDetaylarEczaneneGrupTanimdakiler(List<int> eczaneNobetGrupTanimIdler);

        List<EczaneGrupNode> GetNodes();
        List<EczaneGrupNode> GetNodes(int[] nobetUstGrupIdler);
        List<EczaneGrupEdge> GetEdges();
        List<EczaneGrupEdge> GetEdges(int[] nobetUstGrupIdler);
        List<EczaneGrupEdge> GetEdgesByNobetGrupId(int[] nobetGrupIdler);
        List<EczaneGrupEdge> GetEdges(int nobetUstGrupId);

        List<NobetBagGrup> EsGrupluEczanelerinGruplariniBelirle(List<EczaneGrupDetay> eczaneGrupDetaylar, List<int> esGruplar);
        List<NobetBagGrup> EsGrupluEczanelerinGruplariniBelirleTumu(List<EczaneGrupDetay> eczaneGrupDetaylar, List<int> nobetGruplar);
        List<NobetBagGrup> EsGrupluEczanelerinGruplariniBelirle(List<EczaneGrupDetay> eczaneGrupDetaylar);
        List<EczaneGrup> GetListByUser(User user);

        List<EczaneGrupDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupIdList);
    }
}
