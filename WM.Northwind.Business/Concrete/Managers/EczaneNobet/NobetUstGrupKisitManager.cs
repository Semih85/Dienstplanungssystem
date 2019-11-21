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
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Core.Aspects.PostSharp.TranstionAspects;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetUstGrupKisitManager : INobetUstGrupKisitService
    {
        private INobetUstGrupKisitDal _nobetUstGrupKisitDal;

        public NobetUstGrupKisitManager(INobetUstGrupKisitDal nobetUstGrupKisitDal)
        {
            _nobetUstGrupKisitDal = nobetUstGrupKisitDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin")]
        public void Delete(int nobetUstGrupKisitId)
        {
            _nobetUstGrupKisitDal.Delete(new NobetUstGrupKisit { Id = nobetUstGrupKisitId });
        }

        public NobetUstGrupKisit GetById(int nobetUstGrupKisitId)
        {
            return _nobetUstGrupKisitDal.Get(x => x.Id == nobetUstGrupKisitId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupKisit> GetList()
        {
            return _nobetUstGrupKisitDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin")]
        public void Insert(NobetUstGrupKisit nobetUstGrupKisit)
        {
            _nobetUstGrupKisitDal.Insert(nobetUstGrupKisit);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetUstGrupKisit nobetUstGrupKisit)
        {
            _nobetUstGrupKisitDal.Update(nobetUstGrupKisit);
        }
        public NobetUstGrupKisitDetay GetDetayById(int nobetUstGrupKisitId)
        {
            return _nobetUstGrupKisitDal.GetDetay(x => x.Id == nobetUstGrupKisitId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupKisitDetay> GetDetaylar()
        {
            return _nobetUstGrupKisitDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupKisitDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetUstGrupKisitDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        public List<NobetUstGrupKisitDetay> GetVarsayilandanFarkliOlanlar(int nobetUstGrupId)
        {
            return _nobetUstGrupKisitDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId
            && (x.SagTarafDegeri != x.SagTarafDegeriVarsayilan
             || x.PasifMi != x.VarsayilanPasifMi
             || x.NobetGrupGorevtipKisitSayisi > 0
             )
            );
        }

        public bool GetVarsayilandanFarkliMi(int nobetUstGrupKisitId)
        {
            var sonuc = _nobetUstGrupKisitDal.GetDetay(x => x.Id == nobetUstGrupKisitId
            && (x.SagTarafDegeri != x.SagTarafDegeriVarsayilan
             || x.PasifMi != x.VarsayilanPasifMi
             || x.NobetGrupGorevtipKisitSayisi > 0
             ));

            return sonuc != null ? true : false;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetUstGrupKisitDetay> GetAktifKisitlar(int nobetUstGrupId)
        {
            return _nobetUstGrupKisitDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId
                                                        && x.PasifMi == false);
        }

        public bool GetKisitPasifMi(string kisitAdi, int nobetUstGrupId)
        {
            return _nobetUstGrupKisitDal.GetDetay(x => x.KisitAdi == kisitAdi
                                                    && x.NobetUstGrupId == nobetUstGrupId).PasifMi == false;
        }

        public NobetUstGrupKisitDetay GetDetay(string kisitAdi, int nobetUstGrupId)
        {
            return _nobetUstGrupKisitDal.GetDetay(x => x.KisitAdi == kisitAdi
                                                    && x.NobetUstGrupId == nobetUstGrupId)
                                                    ?? new NobetUstGrupKisitDetay()
                                                    //throw new Exception($"{nobetUstGrupId} üst grubuna {kisitAdi} kısıtı tanımlı değil.")
                                                    ;
        }

        public NobetUstGrupKisitDetay GetDetay(int kisitId, int nobetUstGrupId)
        {
            return _nobetUstGrupKisitDal.GetDetay(x => x.KisitId == kisitId
                                                    && x.NobetUstGrupId == nobetUstGrupId)
                                                    ?? new NobetUstGrupKisitDetay()
                                                    //throw new Exception($"{nobetUstGrupId} üst grubuna {kisitAdi} kısıtı tanımlı değil.")
                                                    ;
        }

        public int GetDegisenKisitlar(int nobetUstGrupId)
        {
            var sayi = GetVarsayilandanFarkliOlanlar(nobetUstGrupId).Count;

            //_nobetUstGrupKisitDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId
            //                                        && (x.PasifMi != x.VarsayilanPasifMi 
            //                                        || x.SagTarafDegeri != x.SagTarafDegeriVarsayilan
            //                                        || x.NobetGrupGorevtipKisitSayisi > 0
            //                                        )).Count();

            if (sayi > 0)
            {
                return sayi;
            }
            else
            {
                return 0;
            }
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<NobetUstGrupKisit> nobetUstGrupKisitlar)
        {
            _nobetUstGrupKisitDal.CokluEkle(nobetUstGrupKisitlar);
        }
    }
}