using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface INobetGrupGorevTipTakvimOzelGunService
    {
        NobetGrupGorevTipTakvimOzelGun GetById(int nobetGrupGorevTipTakvimOzelGunId);
        List<NobetGrupGorevTipTakvimOzelGun> GetList();
        //List<NobetGrupGorevTipTakvimOzelGun> GetByCategory(int categoryId);
        void Insert(NobetGrupGorevTipTakvimOzelGun nobetGrupGorevTipTakvimOzelGun);
        void Update(NobetGrupGorevTipTakvimOzelGun nobetGrupGorevTipTakvimOzelGun);
        void Delete(int nobetGrupGorevTipTakvimOzelGunId);
        void CokluEkle(List<NobetGrupGorevTipTakvimOzelGun> nobetGrupGorevTipTakvimOzelGunler);
        void CokluSil(int[] ids);

        NobetGrupGorevTipTakvimOzelGunDetay GetDetayById(int nobetGrupGorevTipTakvimOzelGunId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar();
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(int nobetUstGrupId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(int nobetUstGrupId, int nobetGrupGorevTipId = 0, int nobetOzelGunId = 0);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(List<int> nobetUstGrupIdList);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar(DateTime baslangicTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar2(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupGorevTipId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar2(DateTime? baslangicTarihi, DateTime? bitisTarihi, int nobetGrupGorevTipId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylar2ByNobetGrupId(DateTime? baslangicTarihi, DateTime? bitisTarihi, int nobetGrupId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarNobetGrupGorevTipBaslamaTarihindenSonra(List<NobetGrupGorevTipDetay> nobetGrupGorevTipler);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarNobetGrupGorevTipBaslamaTarihindenSonra(NobetGrupGorevTipDetay nobetGrupGorevTip);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarVerilenTarihtenSonrasi(DateTime baslangicTarihi, int nobetGrupGorevTipId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarVerilenTarihtenOncesi(DateTime baslangicTarihi, int nobetGrupGorevTipId);
        List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetaylarNobetGrupGorevTipBaslamaTarihindenSonra(int nobetGrupGorevTipId);
    }
}