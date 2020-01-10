using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.Aspects.PostSharp.TranstionAspects;
using WM.Core.Aspects.PostSharp.ValidationAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.ValidationRules.FluentValidation;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{

    public class EczaneNobetGrupManager : IEczaneNobetGrupService
    {
        private IEczaneNobetGrupDal _eczaneNobetGrupDal;

        public EczaneNobetGrupManager(IEczaneNobetGrupDal eczaneNobetGrupDal)
        {
            _eczaneNobetGrupDal = eczaneNobetGrupDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int eczaneNobetGrupId)
        {
            _eczaneNobetGrupDal.Delete(new EczaneNobetGrup { Id = eczaneNobetGrupId });
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrup> GetListByNobetUstGrupId(int nobetUstGrupId)
        {
            return _eczaneNobetGrupDal.GetList(x => x.NobetGrupGorevTip.NobetGrup.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrup> GetList(int nobetGrupGorevTipId)
        {
            return _eczaneNobetGrupDal.GetList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrup> GetList()
        {
            return _eczaneNobetGrupDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrup> GetList(List<int> nobetGrupGorevTipIdList)
        {
            return _eczaneNobetGrupDal.GetList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrup> GetList(List<int> nobetGrupGorevTipIdList, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            return _eczaneNobetGrupDal.GetList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
            && (bitisTarihi >= x.BaslangicTarihi && (bitisTarihi <= x.BitisTarihi || x.BitisTarihi == null)));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrup> GetList(int nobetGrupGorevTipId, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            return _eczaneNobetGrupDal.GetList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId
            && (bitisTarihi >= x.BaslangicTarihi && (bitisTarihi <= x.BitisTarihi || x.BitisTarihi == null)));
        }

        public EczaneNobetGrup GetById(int nobetGrupId)
        {
            return _eczaneNobetGrupDal.Get(x => x.Id == nobetGrupId);
        }

        public EczaneNobetGrupDetay GetDetayById(int eczaneNobetGrupId)
        {
            return _eczaneNobetGrupDal.GetDetay(x => x.Id == eczaneNobetGrupId);
        }

        public EczaneNobetGrupDetay GetDetayByEczaneId(int eczaneId)
        {
            return _eczaneNobetGrupDal.GetDetay(x => x.EczaneId == eczaneId
                                                  && x.BitisTarihi == null);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylar(int[] eczaneNobetGrupIdList)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => eczaneNobetGrupIdList.Contains(x.Id));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylar(List<int> eczaneIdList, int nobetGrupGorevTipId)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => eczaneIdList.Contains(x.EczaneId)
            && x.BitisTarihi == null
            && x.NobetGrupGorevTipId == nobetGrupGorevTipId
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public EczaneNobetGrupDetay GetEczaneninOncekiNobetGrubu(int eczaneId)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => x.EczaneId == eczaneId).OrderByDescending(o => o.Id).Skip(1).Take(1).FirstOrDefault() ?? new EczaneNobetGrupDetay();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylar()
        {
            return _eczaneNobetGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId || nobetUstGrupId == 0);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylar(List<int> nobetGrupIdList)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylarByNobetGrupGorevTipler(List<int> nobetGrupGorevTipIdList)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylarByNobetGrupGorevTipler(List<int> nobetGrupGorevTipIdList, bool tumEczaneler)
        {
            if (tumEczaneler)
            {
                return _eczaneNobetGrupDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId));
            }
            else
            {
                return _eczaneNobetGrupDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId) && x.BitisTarihi == null);
            }
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylarByNobetGrupGorevTipler(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupGorevTipIdList)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
                    && (bitisTarihi >= x.BaslangicTarihi && (bitisTarihi <= x.BitisTarihi || x.BitisTarihi == null)));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylarByNobetGrupGorevTipler(int nobetGrupGorevTipId)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupIdList)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<MyDrop> GetMyDrop(List<EczaneNobetGrupDetay> czaneNobetGrupDetaylar)
        {
            var nobetGruplar = czaneNobetGrupDetaylar.Select(s => s.NobetGrupId).Distinct().ToList();
            var nobetGorevTipler = czaneNobetGrupDetaylar.Select(s => s.NobetGorevTipAdi).Distinct().ToList();

            var liste = new List<MyDrop>();

            var eczaneNobetGruplar = czaneNobetGrupDetaylar
              .OrderBy(s => s.EczaneAdi)
              .ThenBy(t => t.NobetGrupAdi)
              //.Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi} ({s.NobetGrupAdi}, {s.NobetGorevTipAdi})" })
              .ToList();
            if (nobetGorevTipler.Count > 1 && nobetGruplar.Count > 1)
            {
                liste = eczaneNobetGruplar
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}, {s.NobetGorevTipAdi}"
                }).ToList();
            }
            else if (nobetGorevTipler.Count > 1)
            {
                liste = eczaneNobetGruplar
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.EczaneAdi}, {s.NobetGorevTipAdi}"
                }).ToList();
            }
            else if (nobetGruplar.Count > 1)
            {
                liste = eczaneNobetGruplar
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}"
                }).ToList();
            }
            else
            {
                liste = eczaneNobetGruplar
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.EczaneAdi}"
                }).ToList();
            }

            //liste = liste
            //    .OrderBy(s => s.EczaneAdi)
            //    .ThenBy(t => t.NobetGrupAdi)
            //    //.Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi} ({s.NobetGrupAdi}, {s.NobetGorevTipAdi})" })
            //    .ToList();

            return liste;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylar(int nobetGrupId, int eczaneId)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => (x.EczaneId == eczaneId || eczaneId == 0)
            && (x.NobetGrupId == nobetGrupId || nobetGrupId == 0)
            && x.BitisTarihi == null
            );
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylar(List<int> nobetGrupIdList, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId)
                                                     && (bitisTarihi >= x.BaslangicTarihi && (bitisTarihi <= x.BitisTarihi || x.BitisTarihi == null)));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylarTarihAralik(List<int> nobetGrupIdList, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId)
                                                     && (bitisTarihi >= x.BaslangicTarihi && bitisTarihi <= x.BitisTarihi));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylar(int nobetGrupId, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => x.NobetGrupId == nobetGrupId
            && (bitisTarihi >= x.BaslangicTarihi && (bitisTarihi <= x.BitisTarihi || x.BitisTarihi == null)));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupGorevTipId)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId
            && (bitisTarihi >= x.BaslangicTarihi && (bitisTarihi <= x.BitisTarihi || x.BitisTarihi == null)));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylarNobetUstGrupId(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId
            && (bitisTarihi >= x.BaslangicTarihi && (bitisTarihi <= x.BitisTarihi || x.BitisTarihi == null)));
        }

        [FluentValidationAspect(typeof(EczaneNobetGrupValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        [SecuredOperation(Roles = "Admin,Oda,Üst Grup")]
        public void Insert(EczaneNobetGrup eczaneNobetGrup)
        {
            _eczaneNobetGrupDal.Insert(eczaneNobetGrup);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(EczaneNobetGrup eczaneNobetGrup)
        {
            _eczaneNobetGrupDal.Update(eczaneNobetGrup);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrup> GetAktifEczaneGrupList(List<int> nobetGrupGorevTipIdList)
        {
            return _eczaneNobetGrupDal.GetList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId) && x.BitisTarihi == null);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetAktifEczaneGrupListByNobetGrupGorevTipIdList(List<int> nobetGrupGorevTipIdList)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId) && x.BitisTarihi == null);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetAktifEczaneNobetGrupList(List<int> nobetGrupIdList)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId) && x.BitisTarihi == null);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public int GetEczaneninGrubaGirdigiTarihtekiEczaneSayisi(int eczaneNobetGrupId)
        {
            var eczaneNobetGrupBilgileri = GetDetayById(eczaneNobetGrupId);

            var baslangicTarihi = eczaneNobetGrupBilgileri.BaslangicTarihi < eczaneNobetGrupBilgileri.NobetGrupGorevTipBaslamaTarihi
                ? eczaneNobetGrupBilgileri.NobetGrupGorevTipBaslamaTarihi
                : eczaneNobetGrupBilgileri.BaslangicTarihi;

            return _eczaneNobetGrupDal.GetDetayList(w => w.NobetGrupGorevTipId == eczaneNobetGrupBilgileri.NobetGrupGorevTipId
                && (w.BaslangicTarihi <= baslangicTarihi && (baslangicTarihi <= w.BitisTarihi || w.BitisTarihi == null))
            ).Count();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetAktifEczaneNobetGrup(int nobetGrupId)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => x.NobetGrupId == nobetGrupId && x.BitisTarihi == null);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupDetay> GetDetaylarByEczaneIdList(List<int> eczaneIdList)
        {
            return _eczaneNobetGrupDal.GetDetayList(x => eczaneIdList.Contains(x.EczaneId) && x.BitisTarihi == null);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetGrupIstatistik> NobetGruplarDDL(int nobetUstGrupId)
        {
            var nobetGruplar = GetDetaylar(nobetUstGrupId)
                .Where(x => x.BitisTarihi == null)
                .GroupBy(g => new
                {
                    g.NobetUstGrupId,
                    g.NobetGrupId,
                    g.NobetGrupAdi,
                    g.NobetGrupGorevTipId,
                    g.NobetGorevTipId,
                    g.NobetGorevTipAdi,
                    g.NobetGrupGorevTipAdi
                })
                .Select(s => new EczaneNobetGrupIstatistik
                {
                    NobetUstGrupId = s.Key.NobetUstGrupId,
                    NobetGrupGorevTipAdi = s.Key.NobetGrupGorevTipAdi,
                    NobetGrupId = s.Key.NobetGrupId,
                    NobetGrupAdi = s.Key.NobetGrupAdi,
                    NobetGrupGorevTipId = s.Key.NobetGrupGorevTipId,
                    NobeGorevTipAdi = s.Key.NobetGorevTipAdi,
                    NobeGorevTipId = s.Key.NobetGorevTipId,
                    EczaneSayisi = s.Count()
                })
                .OrderBy(o => o.NobetGrupId).ToList();

            return nobetGruplar;
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<EczaneNobetGrup> eczaneNobetGruplar)
        {
            var nobetGrupGorevTipId = eczaneNobetGruplar.Select(s => s.NobetGrupGorevTipId).Distinct().FirstOrDefault();

            var gruptakiEczaneler = GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipId);

            var dahaOnceGruptaAyniGruptaOlanEczaneler = gruptakiEczaneler.Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            if (dahaOnceGruptaAyniGruptaOlanEczaneler.Count == 0)
            {
                _eczaneNobetGrupDal.CokluEkle(eczaneNobetGruplar);
            }
            else
            {
                foreach (var dahaOnceGruptaAyniGruptaOlanEczane in dahaOnceGruptaAyniGruptaOlanEczaneler)
                {
                    var eklenecekEczane = eczaneNobetGruplar.SingleOrDefault(x => x.EczaneId == dahaOnceGruptaAyniGruptaOlanEczane.EczaneId);

                    //if (dahaOnceGruptaAyniGruptaOlanEczane.BitisTarihi == null 
                    //    && eklenecekEczane.BitisTarihi == null
                    //    )
                    //{
                    throw new Exception($"'{dahaOnceGruptaAyniGruptaOlanEczane.EczaneAdi}' eczanesi " +
                        $"{dahaOnceGruptaAyniGruptaOlanEczane.NobetGrupAdi},'{dahaOnceGruptaAyniGruptaOlanEczane.NobetGorevTipAdi}' nöbet grubuna " +
                        $"2 kez eklenemez.");
                    //}
                }

                _eczaneNobetGrupDal.CokluEkle(eczaneNobetGruplar);
            }
        }
    }
}

