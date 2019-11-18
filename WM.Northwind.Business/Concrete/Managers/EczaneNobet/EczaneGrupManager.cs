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
using WM.Core.DAL;
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
    //[SecuredOperation(Roles= "Admin,Oda,Üst Grup")]
    public class EczaneGrupManager : IEczaneGrupService
    {
        #region ctor
        private IEczaneGrupDal _eczaneGrupDal;
        private IUserService _userService;
        private IEczaneDal _eczaneDal;
        private IUserEczaneDal _userEczaneDal;
        //IQueryableRepository<EczaneGrup> _queryable;
        private IEczaneNobetGrupDal _eczaneNobetGrupDal;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IUserEczaneService _userEczaneService;

        public EczaneGrupManager(IEczaneGrupDal eczaneGrupDal,
                                    IEczaneDal eczaneDal,
                                    IUserService userService,
                                    IUserEczaneDal userEczaneDal,
                                    INobetGrupGorevTipService nobetGrupGorevTipService,
                                    IEczaneNobetGrupService eczaneNobetGrupService,
                                    IUserEczaneService userEczaneService,
        //IQueryableRepository<EczaneGrup> queryable
                                    IEczaneNobetGrupDal eczaneNobetGrupDal
            )
        {
            _eczaneGrupDal = eczaneGrupDal;
            _eczaneDal = eczaneDal;
            _userService = userService;
            _userEczaneDal = userEczaneDal;
            //_queryable = queryable;      
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _userEczaneService = userEczaneService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneNobetGrupDal = eczaneNobetGrupDal;
        }
        #endregion

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneGrupId)
        {
            _eczaneGrupDal.Delete(new EczaneGrup { Id = eczaneGrupId });
        }

        public List<EczaneGrup> GetListByUser(User user)
        {
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            var eczaneler = new List<Eczane>();

            if (rolId == 4)
            {//yetkili olduğu eczaneler
                var userEczaneler = _userEczaneService.GetListByUserId(user.Id);
                eczaneler = _eczaneDal.GetList().Where(x => userEczaneler.Select(s => s.EczaneId).Contains(x.Id)).ToList();
            }
            else
            {//yetkili olduğu nöbet gruplar
                var nobetGruplar = _nobetGrupGorevTipService.GetListByUser(user).Select(g => g.Id);

                var eczaneNobetGruplar = _eczaneNobetGrupService.GetList()
                    .Where(s => nobetGruplar.Contains(s.NobetGrupGorevTipId)).ToList();
                //.Where(s => s.NobetGrupId == 3).ToList();
                eczaneler = _eczaneDal.GetList().Where(e => eczaneNobetGruplar.Select(n => n.EczaneId).Contains(e.Id)).ToList();
            }
            var eczaneIdler = eczaneler.Select(s => s.Id).ToList();
            return _eczaneGrupDal.GetList(x => eczaneIdler.Contains(x.EczaneId));
        }
        //public IQueryable<EczaneGrupDetay> GetEczaneGrupDetaylar()
        //{
        //    return _queryable.Table.Select(s => new EczaneGrupDetay
        //    {
        //        Id = s.Id,
        //        EczaneAdi = s.Eczane.Adi,
        //        EczaneGrupTanimAdi = s.EczaneGrupTanim.Adi
        //    });
        //}

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylar()
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarByNobetGrupId(int nobetGrupId)
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar(x => x.NobetGrupId == nobetGrupId || nobetGrupId == 0);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar(x => //x.EczaneGrupTanimBitisTarihi == null && 
            x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarAktifGruplar()
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar(x => x.EczaneGrupTanimBitisTarihi == null
                                                         && x.EczaneGrupTanimPasifMi == false
                                                         && x.PasifMi == false);
        }

        public List<EczaneGrupDetay> GetDetayById(int eczaneGrupDetayId)
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar(x => x.Id == eczaneGrupDetayId);
        }

        public EczaneGrup GetById(int eczaneGrupId)
        {
            return _eczaneGrupDal.Get(x => x.Id == eczaneGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrup> GetList()
        {
            return _eczaneGrupDal.GetList();
        }

        [FluentValidationAspect(typeof(EczaneGrupValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(EczaneGrup eczaneGrup)
        {
            _eczaneGrupDal.Insert(eczaneGrup);
        }

        //[FluentValidationAspect(typeof(EczaneGrupValidator))]
        //[CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[LogAspect(typeof(DatabaseLogger))]
        public void Update(EczaneGrup eczaneGrup)
        {
            _eczaneGrupDal.Update(eczaneGrup);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarAktifGruplar(int nobetUstGrupId)
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar(x => x.NobetUstGrupId == nobetUstGrupId
                                                         && (x.EczaneGrupTanimBitisTarihi == null
                                                         && x.EczaneGrupTanimPasifMi == false
                                                         && x.PasifMi == false
                                                         ));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarAktif(int eczaneNobetGrupId)
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar(x => x.EczaneNobetGrupId == eczaneNobetGrupId
                                                         && (x.EczaneGrupTanimBitisTarihi == null
                                                         && x.EczaneGrupTanimPasifMi == false
                                                         && x.PasifMi == false
                                                         ));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarEczaneninEsOlduguEczaneler(int eczaneNobetGrupId)
        {
            var eczaneGruplar = _eczaneGrupDal.GetEczaneGrupDetaylar(x => x.EczaneNobetGrupId == eczaneNobetGrupId
                                                         && (x.EczaneGrupTanimBitisTarihi == null
                                                         && x.EczaneGrupTanimPasifMi == false
                                                         && x.PasifMi == false
                                                         ));

            return GetDetaylarEczaneneGrupTanimdakiler(eczaneGruplar.Select(s => s.EczaneGrupTanimId).ToList());
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarEczaneninEsOlduguEczaneler(List<int> eczaneNobetGrupIdList)
        {
            var eczaneGruplar = _eczaneGrupDal.GetEczaneGrupDetaylar(x => eczaneNobetGrupIdList.Contains(x.EczaneNobetGrupId)
                                                         && (x.EczaneGrupTanimBitisTarihi == null
                                                         && x.EczaneGrupTanimPasifMi == false
                                                         && x.PasifMi == false
                                                         ));

            return GetDetaylarEczaneneGrupTanimdakiler(eczaneGruplar.Select(s => s.EczaneGrupTanimId).ToList());
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupIdList)
        {
            var eczaneGruplar = _eczaneGrupDal.GetEczaneGrupDetaylar(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId)
                                                         && (x.EczaneGrupTanimBitisTarihi == null
                                                         && x.EczaneGrupTanimPasifMi == false
                                                         && x.PasifMi == false
                                                         ));

            return GetDetaylarEczaneneGrupTanimdakiler(eczaneGruplar.Select(s => s.EczaneGrupTanimId).ToList());
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarEczaneneGrupTanimdakiler(List<int> eczaneNobetGrupTanimIdler)
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar(x => eczaneNobetGrupTanimIdler.Contains(x.EczaneGrupTanimId)
                                                         && (x.EczaneGrupTanimBitisTarihi == null
                                                         && x.EczaneGrupTanimPasifMi == false
                                                         && x.PasifMi == false
                                                         ));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupNode> GetNodes()
        {
            var eczaneNodes = (from e in GetDetaylarAktifGruplar()
                               //.Where(w => !(w.EczaneGrupTanimBitisTarihi == null
                               //          || w.EczaneGrupTanimPasifMi == false
                               //          || w.PasifMi == false))
                               .Select(s => new { s.EczaneId, s.EczaneAdi, s.NobetGrupId }).Distinct()
                               select (new EczaneGrupNode
                               {
                                   Id = e.EczaneId,
                                   Label = e.EczaneAdi,
                                   Value = 5,
                                   Level = e.NobetGrupId,
                                   Group = e.NobetGrupId
                               })).ToList();
            return eczaneNodes;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupNode> GetNodes(int[] nobetUstGrupIdler)
        {
            var eczaneNodes = (from e in GetDetaylarAktifGruplar()
                               .Where(w =>
                                          //!(w.EczaneGrupTanimBitisTarihi == null
                                          // || w.EczaneGrupTanimPasifMi == false
                                          // || w.PasifMi == false)
                                          nobetUstGrupIdler.Contains(w.NobetUstGrupId))
                               .Select(s => new { s.EczaneId, s.EczaneAdi, s.NobetGrupId }).Distinct()
                               select (new EczaneGrupNode
                               {
                                   Id = e.EczaneId,
                                   Label = e.EczaneAdi,
                                   Value = 5,
                                   Level = e.NobetGrupId,
                                   Group = e.NobetGrupId
                               })).ToList();
            return eczaneNodes;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupEdge> GetEdges()
        {
            var eczaneGrupDetaylar = GetDetaylarAktifGruplar();

            var eczaneGrupTanimNobetGruplar = (from e in eczaneGrupDetaylar
                                               group e by new
                                               {
                                                   e.EczaneGrupTanimId,
                                                   e.NobetGrupId,
                                                   e.NobetUstGrupId
                                               } into g
                                               select new EczaneGrupTanimNobetGrup
                                               {
                                                   EczaneGrupTanimId = g.Key.EczaneGrupTanimId,
                                                   NobetGrupId = g.Key.NobetGrupId,
                                                   NobetUstGrupId = g.Key.NobetUstGrupId,
                                                   Sayi = g.Count()
                                               }).ToList();

            var eczaneEdgeList = EsGrupluEczanelerinGruplariniBelirle(eczaneGrupDetaylar, eczaneGrupTanimNobetGruplar);

            return eczaneEdgeList;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupEdge> GetEdges(int[] nobetUstGrupIdler)
        {
            var eczaneGrupDetaylar = GetDetaylarAktifGruplar().Where(e => nobetUstGrupIdler.Contains(e.NobetUstGrupId)).ToList();

            var eczaneGrupTanimNobetGruplar = (from e in eczaneGrupDetaylar
                                               group e by new
                                               {
                                                   e.EczaneGrupTanimId,
                                                   e.NobetGrupId,
                                                   e.NobetUstGrupId
                                               } into g
                                               select new EczaneGrupTanimNobetGrup
                                               {
                                                   EczaneGrupTanimId = g.Key.EczaneGrupTanimId,
                                                   NobetGrupId = g.Key.NobetGrupId,
                                                   NobetUstGrupId = g.Key.NobetUstGrupId,
                                                   Sayi = g.Count()
                                               }).Distinct().ToList();

            var eczaneEdgeList = EsGrupluEczanelerinGruplariniBelirle(eczaneGrupDetaylar, eczaneGrupTanimNobetGruplar);

            return eczaneEdgeList;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupEdge> GetEdges(int nobetUstGrupId)
        {
            var eczaneGrupDetaylar = GetDetaylarAktifGruplar(nobetUstGrupId);

            var eczaneGrupTanimNobetGruplar = GetEczaneGrupTanimNobetGrup(eczaneGrupDetaylar);

            var eczaneEdgeList = EsGrupluEczanelerinGruplariniBelirle(eczaneGrupDetaylar, eczaneGrupTanimNobetGruplar);

            return eczaneEdgeList;
        }

        private List<EczaneGrupTanimNobetGrup> GetEczaneGrupTanimNobetGrup(List<EczaneGrupDetay> eczaneGrupDetaylar)
        {
            var liste = (from e in eczaneGrupDetaylar
                         group e by new
                         {
                             e.EczaneGrupTanimId,
                             e.NobetGrupId,
                             e.NobetUstGrupId
                         } into g
                         select new EczaneGrupTanimNobetGrup
                         {
                             EczaneGrupTanimId = g.Key.EczaneGrupTanimId,
                             NobetGrupId = g.Key.NobetGrupId,
                             NobetUstGrupId = g.Key.NobetUstGrupId,
                             Sayi = g.Count()
                         }).Distinct().ToList();

            return liste;
        }

        public List<EczaneGrupEdge> GetEdgesByNobetGrupId(int[] nobetGrupIdler)
        {
            var eczaneGrupDetaylar = GetDetaylarAktifGruplar().Where(e =>
                                                            //!(e.EczaneGrupTanimBitisTarihi == null
                                                            //|| e.EczaneGrupTanimPasifMi == false
                                                            //|| e.PasifMi == false)
                                                            nobetGrupIdler.Contains(e.NobetGrupId)).ToList();

            var eczaneGrupTanimNobetGruplar = (from e in eczaneGrupDetaylar
                                               group e by new
                                               {
                                                   e.EczaneGrupTanimId,
                                                   e.NobetGrupId,
                                                   e.NobetUstGrupId
                                               } into g
                                               select new EczaneGrupTanimNobetGrup
                                               {
                                                   EczaneGrupTanimId = g.Key.EczaneGrupTanimId,
                                                   NobetGrupId = g.Key.NobetGrupId,
                                                   NobetUstGrupId = g.Key.NobetUstGrupId,
                                                   Sayi = g.Count()
                                               }).Distinct().ToList();

            var eczaneEdgeList = EsGrupluEczanelerinGruplariniBelirle(eczaneGrupDetaylar, eczaneGrupTanimNobetGruplar);

            return eczaneEdgeList;
        }

        public List<EczaneGrupEdge> EsGrupluEczanelerinGruplariniBelirle(List<EczaneGrupDetay> eczaneGrupDetaylar, List<EczaneGrupTanimNobetGrup> eczaneGrupTanimNobetGruplar)
        {
            var eczaneGrupKenarlar = new List<EczaneGrupEdge>();
            var grupList = new List<int>();
            var indis = 0;
            var grupTanimlar = eczaneGrupTanimNobetGruplar.Select(s => s.EczaneGrupTanimId).Distinct();

            if (eczaneGrupTanimNobetGruplar.Count > 0)
            {
                foreach (var grupTanimId in grupTanimlar)//1,2..138
                {
                    var ilkGrup = eczaneGrupTanimNobetGruplar
                        .Where(w => w.EczaneGrupTanimId == grupTanimId)
                        .Select(s => s.NobetGrupId)
                        .OrderBy(o => o)
                        .FirstOrDefault();

                    var eczaneGrupTanimGruplar = (from e in eczaneGrupDetaylar
                                                  where e.EczaneGrupTanimId == grupTanimId
                                                  group e by new
                                                  {
                                                      e.EczaneGrupTanimId,
                                                      e.NobetGrupId,
                                                      e.NobetUstGrupId
                                                  } into g
                                                  select new EczaneGrupTanimNobetGrup
                                                  {
                                                      EczaneGrupTanimId = g.Key.EczaneGrupTanimId,
                                                      NobetGrupId = g.Key.NobetGrupId,
                                                      NobetUstGrupId = g.Key.NobetUstGrupId,
                                                      Sayi = g.Count()
                                                  })
                                                  .OrderBy(o => o.NobetGrupId)
                                                  .ToList();

                    foreach (var grup in eczaneGrupTanimGruplar)
                    {
                        if (grup.NobetGrupId > ilkGrup)
                        {
                            var gruplar = grupList.Distinct();
                            if (gruplar.Contains(grup.NobetGrupId))
                                continue;
                        }
                        indis++;

                        var eczaneGrupTanimEczaneler = (from e in eczaneGrupDetaylar
                                                        where e.EczaneGrupTanimId == grup.EczaneGrupTanimId
                                                        && e.NobetGrupId == grup.NobetGrupId
                                                        select new EczaneGrupTanimEczane
                                                        {
                                                            EczaneGrupTanimId = e.EczaneGrupTanimId,
                                                            NobetGrupId = e.NobetGrupId,
                                                            NobetUstGrupId = e.NobetUstGrupId,
                                                            EczaneId = e.EczaneId,
                                                            EczaneGrupTanimTuru = e.EczaneGrupTanimTipAdi,
                                                            NobetGrupAdi = e.NobetGrupAdi,
                                                            EczaneGrupTanimAdi = e.EczaneGrupTanimAdi,
                                                            EczaneAdi = e.EczaneAdi
                                                        }).ToList();

                        foreach (var fromEczane in eczaneGrupTanimEczaneler)
                        {
                            grupList.Add(fromEczane.NobetGrupId);//1,2,3
                            var eczaneGrupTanimEczaneler2 = (from e in eczaneGrupDetaylar
                                                             where e.NobetGrupId != grup.NobetGrupId
                                                             && e.EczaneGrupTanimId == grup.EczaneGrupTanimId
                                                             select new EczaneGrupTanimEczane
                                                             {
                                                                 EczaneGrupTanimId = e.EczaneGrupTanimId,
                                                                 NobetGrupId = e.NobetGrupId,
                                                                 NobetUstGrupId = e.NobetUstGrupId,
                                                                 EczaneId = e.EczaneId,
                                                                 EczaneGrupTanimTuru = e.EczaneGrupTanimTipAdi,
                                                                 NobetGrupAdi = e.NobetGrupAdi,
                                                                 EczaneGrupTanimAdi = e.EczaneGrupTanimAdi,
                                                                 EczaneAdi = e.EczaneAdi
                                                             }).ToList();

                            foreach (var toEczane in eczaneGrupTanimEczaneler2)
                            {
                                grupList.Add(toEczane.NobetGrupId);//1,2,3

                                eczaneGrupKenarlar
                                    .Add(new EczaneGrupEdge
                                    {
                                        NobetUstGrupId = fromEczane.NobetUstGrupId,
                                        From = fromEczane.EczaneId,
                                        To = toEczane.EczaneId,
                                        FromNobetGrupId = fromEczane.NobetGrupId,
                                        FromNobetGrupAdi = fromEczane.NobetGrupAdi,
                                        FromEczaneAdi = fromEczane.EczaneAdi,
                                        ToNobetGrupId = toEczane.NobetGrupId,
                                        ToNobetGrupAdi = toEczane.NobetGrupAdi,
                                        ToEczaneAdi = toEczane.EczaneAdi,
                                        Value = 1,
                                        GrupTanimAdi = fromEczane.EczaneGrupTanimAdi,
                                        GrupTuru = fromEczane.EczaneGrupTanimTuru,
                                        //Label = 1,
                                        Title = $"Nöbet Grubu: {fromEczane.NobetGrupAdi}-{toEczane.NobetGrupAdi}"  // + "->" + "To eczane: " + h.G2EczaneId + ": " + h.AyniGunNobetTutmaSayisi + " adet birlikte nöbet"
                                    });
                            }
                        }
                    }
                }
            }
            return eczaneGrupKenarlar;
        }

        /// <summary>
        /// Birbiri ile ilişkisi/bağı olan grupların gruplanması
        /// </summary>
        /// <param name="eczaneGrupDetaylar"></param>
        /// <param name="esGruplar"></param>
        /// <returns></returns>
        public List<NobetBagGrup> EsGrupluEczanelerinGruplariniBelirle(List<EczaneGrupDetay> eczaneGrupDetaylar, List<int> esNobetGruplar)
        {
            var nobetGrupIdList = new List<NobetBagGrup>();
            var grupList = new List<int>();
            var indis = 0;
            if (esNobetGruplar.Count > 0)
            {
                var ilk = esNobetGruplar.FirstOrDefault();
                foreach (var esNobetGrupId in esNobetGruplar)//1,2,3,4,5
                {
                    if (esNobetGrupId > ilk)
                    {
                        if (grupList.Contains(esNobetGrupId))
                            continue;
                    }
                    indis++;

                    var temp = BagliNobetGruplar(eczaneGrupDetaylar, esNobetGrupId);//1,2,3
                    foreach (var item2 in temp)
                    {
                        grupList.Add(item2);//1,2,3
                        nobetGrupIdList
                            .Add(new NobetBagGrup
                            {
                                Id = indis,
                                NobetGrupId = item2
                            });
                    }
                }
            }

            return nobetGrupIdList;
        }

        public List<NobetBagGrup> EsGrupluEczanelerinGruplariniBelirleTumu(List<EczaneGrupDetay> eczaneGrupDetaylar, List<int> nobetGruplar)
        {
            var esliNobetGruplar = new List<NobetBagGrup>();
            var tumNobetGruplar = new List<NobetBagGrup>();
            var grupList = new List<int>();

            var esNobetGruplar = eczaneGrupDetaylar.Select(s => s.NobetGrupId).Distinct().OrderBy(o => o).ToList();

            if (esNobetGruplar.Count > 0)
            {
                var indis = 0;

                var ilk = esNobetGruplar.FirstOrDefault();

                foreach (var esNobetGrupId in esNobetGruplar)//1,2,3,4,5
                {
                    if (esNobetGrupId > ilk)
                    {
                        if (grupList.Contains(esNobetGrupId))
                            continue;
                    }

                    indis++;

                    var temp = BagliNobetGruplar(eczaneGrupDetaylar, esNobetGrupId);//1,2,3

                    foreach (var item2 in temp)
                    {
                        grupList.Add(item2);//1,2,3

                        esliNobetGruplar
                            .Add(new NobetBagGrup
                            {
                                Id = indis,
                                NobetGrupId = item2
                            });
                    }
                }

                #region başka gruplarla ilişkisi olmayan gruplar

                var tekliNobetGruplar = nobetGruplar //_nobetGrupService.GetList(nobetGruplar)
                    .Where(x => !esNobetGruplar.Contains(x)).ToList();

                #endregion

                #region tüm nöbet gruplar                

                foreach (var esliNobetGrup in esliNobetGruplar.Distinct())
                {
                    tumNobetGruplar.Add(new NobetBagGrup
                    {
                        Id = esliNobetGrup.Id,
                        NobetGrupId = esliNobetGrup.NobetGrupId
                    });
                }

                var indis2 = tumNobetGruplar.Select(s => s.Id).LastOrDefault();

                foreach (var tekliNobetGrup in tekliNobetGruplar)
                {
                    indis2++;

                    tumNobetGruplar.Add(new NobetBagGrup
                    {
                        Id = indis2,
                        NobetGrupId = tekliNobetGrup
                    });
                }

                var nobetGrupTanimlar = tumNobetGruplar.Select(s => s.Id).Distinct().ToList();

                #endregion
            }

            return tumNobetGruplar.Distinct().ToList();
        }

        public List<NobetBagGrup> EsGrupluEczanelerinGruplariniBelirle(List<EczaneGrupDetay> eczaneGrupDetaylar)
        {
            var nobetGrupIdList = new List<NobetBagGrup>();
            var grupList = new List<int>();
            var esNobetGruplar = eczaneGrupDetaylar.Select(s => s.NobetGrupId).Distinct().ToList();

            var indis = 0;
            if (esNobetGruplar.Count > 0)
            {
                var ilk = esNobetGruplar.FirstOrDefault();
                foreach (var esNobetGrupId in esNobetGruplar)//1,2,3,4,5
                {
                    if (esNobetGrupId > ilk)
                    {
                        if (grupList.Contains(esNobetGrupId))
                            continue;
                    }
                    indis++;

                    var temp = BagliNobetGruplar(eczaneGrupDetaylar, esNobetGrupId);//1,2,3
                    foreach (var item2 in temp)
                    {
                        grupList.Add(item2);//1,2,3
                        nobetGrupIdList
                            .Add(new NobetBagGrup
                            {
                                Id = indis,
                                NobetGrupId = item2
                            });
                    }
                }
            }

            return nobetGrupIdList;
        }

        /// <summary>
        /// Eşli grupların gruplanması
        /// </summary>
        /// <param name="eczaneGrupDetaylar"></param>
        /// <param name="esGrup"></param>
        /// <returns></returns>
        private List<int> BagliNobetGruplar(List<EczaneGrupDetay> eczaneGrupDetaylar, int esNobetGrupId)
        {
            var nobetGrupIdList = new List<int>();
            var bakilanNobetGruplar = new List<int>();
            var nobetGrupIdListKontrol = new List<int>
            {
                esNobetGrupId
            };

            do
            {//1. parametre gelen nöbet grubunun ilişkileri
                if (nobetGrupIdListKontrol.Count() > 0)
                {
                    var eczaneGrupTanimIdList = eczaneGrupDetaylar
                        .Where(x => nobetGrupIdListKontrol.Contains(x.NobetGrupId))
                        .Select(t => new EczaneGrupTanimKisa
                        {
                            EczaneGrupTanimId = t.EczaneGrupTanimId,
                            EczaneGrupTanimAdi = t.EczaneGrupTanimAdi
                        }).Distinct().ToList();

                    var ilkBagliGruplar = eczaneGrupDetaylar
                                        .Where(w => eczaneGrupTanimIdList.Select(s => s.EczaneGrupTanimId).Contains(w.EczaneGrupTanimId))
                                        .Select(s => s.NobetGrupId).Distinct()
                                        .ToList();

                    foreach (var grup in ilkBagliGruplar)
                    {
                        if (!nobetGrupIdList.Contains(grup))
                        {
                            nobetGrupIdList.Add(grup);
                        }
                    }

                    var digerBagliGruplar = DigerBagliGruplariGetir(eczaneGrupDetaylar, esNobetGrupId, ilkBagliGruplar);

                    foreach (var grup in digerBagliGruplar)
                    {
                        if (!nobetGrupIdList.Contains(grup))
                        {
                            nobetGrupIdList.Add(grup);
                        }
                    }

                    foreach (var item in nobetGrupIdListKontrol)
                    {
                        if (!bakilanNobetGruplar.Contains(item))
                        {
                            bakilanNobetGruplar.Add(item);
                        }
                    }

                    foreach (var item in ilkBagliGruplar)
                    {
                        if (!bakilanNobetGruplar.Contains(item))
                        {
                            bakilanNobetGruplar.Add(item);
                        }
                    }

                    nobetGrupIdListKontrol.Clear();

                    foreach (var item in digerBagliGruplar.Where(w => !bakilanNobetGruplar.Contains(w)))
                    {
                        nobetGrupIdListKontrol.Add(item);
                    }
                }

            } while (nobetGrupIdListKontrol.Count() > 0);

            var sonuc = nobetGrupIdList.Distinct().OrderBy(o => o).ToList();

            return sonuc;
        }

        private List<int> DigerBagliGruplariGetir(List<EczaneGrupDetay> eczaneGrupDetaylar, int esNobetGrupId, List<int> ilkBagliGruplar)
        {
            var bagliGruplar = new List<int>();

            foreach (var ilkBagliGrup in ilkBagliGruplar.Where(x => x != esNobetGrupId))
            {//1,2,3
                #region MyRegion
                //var eczaneGruplar = eczaneGrupDetaylar
                //                 .Where(x => x.NobetGrupId == ilkBagliGrup)
                //                 .Select(x => new EczaneGrupKisa
                //                 {
                //                     EczaneGrupTanimId = x.EczaneGrupTanimId,
                //                     EczaneGrupTanimAdi = x.EczaneGrupTanimAdi,
                //                     NobetGrupId = x.NobetGrupId,
                //                     NobetGrupAdi = x.NobetGrupAdi
                //                 }).Distinct().ToList(); 
                #endregion

                var eczaneGrupTanimIdList2 = eczaneGrupDetaylar
                                    .Where(x => x.NobetGrupId == ilkBagliGrup)
                                    .Select(t => new EczaneGrupTanimKisa
                                    {
                                        EczaneGrupTanimId = t.EczaneGrupTanimId,
                                        EczaneGrupTanimAdi = t.EczaneGrupTanimAdi
                                    }).Distinct().ToList();

                var digerBagliGruplar = eczaneGrupDetaylar
                                 .Where(w => eczaneGrupTanimIdList2.Select(s => s.EczaneGrupTanimId).Contains(w.EczaneGrupTanimId))
                                 .Select(s => s.NobetGrupId).Distinct()
                                 .ToList();

                foreach (var grup in digerBagliGruplar)
                {
                    if (!bagliGruplar.Contains(grup))
                    {
                        bagliGruplar.Add(grup);
                    }
                }
            }

            return bagliGruplar.Distinct().OrderBy(o => o).ToList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarByEczaneGrupTanimId(List<int> eczaneGrupTanimIdList)
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar(x => eczaneGrupTanimIdList.Contains(x.EczaneGrupTanimId))
                .Where(e => (e.EczaneGrupTanimBitisTarihi == null
                          && e.EczaneGrupTanimPasifMi == false
                          && e.PasifMi == false)).ToList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupDetay> GetDetaylarByEczaneGrupTanimId(int eczaneGrupTanimId)
        {
            return _eczaneGrupDal.GetEczaneGrupDetaylar(x => eczaneGrupTanimId == x.EczaneGrupTanimId)
                .Where(e => (e.EczaneGrupTanimBitisTarihi == null
                          && e.EczaneGrupTanimPasifMi == false
                          && e.PasifMi == false)).ToList();
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<EczaneGrup> eczaneGruplar)
        {
            _eczaneGrupDal.CokluEkle(eczaneGruplar);
        }
    }
}
