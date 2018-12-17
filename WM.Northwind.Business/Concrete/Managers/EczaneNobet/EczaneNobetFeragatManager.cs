﻿using System;
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
    public class EczaneNobetFeragatManager : IEczaneNobetFeragatService
    {
        private IEczaneNobetFeragatDal _eczaneNobetFeragatDal;

        public EczaneNobetFeragatManager(IEczaneNobetFeragatDal eczaneNobetFeragatDal)
        {
            _eczaneNobetFeragatDal = eczaneNobetFeragatDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetFeragatId)
        {
            _eczaneNobetFeragatDal.Delete(new EczaneNobetFeragat { EczaneNobetSonucId = eczaneNobetFeragatId });
        }

        public EczaneNobetFeragat GetById(int eczaneNobetFeragatId)
        {
            return _eczaneNobetFeragatDal.Get(x => x.EczaneNobetSonucId == eczaneNobetFeragatId);
        }

        public EczaneNobetFeragatDetay GetDetayById(int eczaneNobetFeragatId)
        {
            return _eczaneNobetFeragatDal.GetDetay(x => x.EczaneNobetSonucId == eczaneNobetFeragatId);
        }

        public List<EczaneNobetFeragatDetay> GetDetaylar()
        {
            return _eczaneNobetFeragatDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetFeragat> GetList()
        {
            return _eczaneNobetFeragatDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetFeragat eczaneNobetFeragat)
        {
            _eczaneNobetFeragatDal.Insert(eczaneNobetFeragat);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetFeragat eczaneNobetFeragat)
        {
            _eczaneNobetFeragatDal.Update(eczaneNobetFeragat);
        }


    }
}