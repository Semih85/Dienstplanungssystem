using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.Optimization.EczaneNobet
{
    public interface IMersinMerkezOptimizationServiceV2
    {
        EczaneNobetSonucModel EczaneNobetCozAktifiGuncelle(MersinMerkezDataModelV2 data);
        EczaneNobetSonucModel ModelCoz(EczaneNobetModelCoz eczaneNobetModelCoz);
        void Kesinlestir(int nobetUstGrupId);
        void ModeliKapat();
    }
}
