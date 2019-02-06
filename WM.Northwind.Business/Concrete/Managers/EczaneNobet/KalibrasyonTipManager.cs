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
    public class KalibrasyonTipManager : IKalibrasyonTipService
    {
        private IKalibrasyonTipDal _kalibrasyonTipDal;

        public KalibrasyonTipManager(IKalibrasyonTipDal kalibrasyonTipDal)
        {
            _kalibrasyonTipDal = kalibrasyonTipDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int kalibrasyonTipId)
        {
            _kalibrasyonTipDal.Delete(new KalibrasyonTip { Id = kalibrasyonTipId });
        }

        public KalibrasyonTip GetById(int kalibrasyonTipId)
        {
            return _kalibrasyonTipDal.Get(x => x.Id == kalibrasyonTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<KalibrasyonTip> GetList()
        {
            return _kalibrasyonTipDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(KalibrasyonTip kalibrasyonTip)
        {
            _kalibrasyonTipDal.Insert(kalibrasyonTip);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(KalibrasyonTip kalibrasyonTip)
        {
            _kalibrasyonTipDal.Update(kalibrasyonTip);
        }
        public KalibrasyonTipDetay GetDetayById(int kalibrasyonTipId)
        {
            return _kalibrasyonTipDal.GetDetay(x => x.Id == kalibrasyonTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<KalibrasyonTipDetay> GetDetaylar()
        {
            return _kalibrasyonTipDal.GetDetayList();
        }

    }
}