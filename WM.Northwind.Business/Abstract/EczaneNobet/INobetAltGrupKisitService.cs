﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
//using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface INobetAltGrupKisitService
    {
        NobetAltGrupKisit GetById(int nobetAltGrupKisitId);
        List<NobetAltGrupKisit> GetList();
        //List<NobetAltGrupKisit> GetByCategory(int categoryId);
        void Insert(NobetAltGrupKisit nobetAltGrupKisit);
        void Update(NobetAltGrupKisit nobetAltGrupKisit);
        void Delete(int nobetAltGrupKisitId);
                        NobetAltGrupKisitDetay GetDetayById(int nobetAltGrupKisitId);
                                   List <NobetAltGrupKisitDetay> GetDetaylar();
    }
} 