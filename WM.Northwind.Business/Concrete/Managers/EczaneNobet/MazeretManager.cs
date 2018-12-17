using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    //[SecuredOperation(Roles= "Admin,Oda")]
    public class MazeretManager : IMazeretService
    {
        private IMazeretDal _mazeretDal;

        public MazeretManager(IMazeretDal mazeretDal)
        {
            _mazeretDal = mazeretDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int mazeretId)
        {
            _mazeretDal.Delete(new Mazeret { Id = mazeretId });
        }

        public Mazeret GetById(int mazeretId)
        {
            return _mazeretDal.Get(x => x.Id == mazeretId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Mazeret> GetList()
        {
            return _mazeretDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(Mazeret mazeret)
        {
            _mazeretDal.Insert(mazeret);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(Mazeret mazeret)
        {
            _mazeretDal.Update(mazeret);
        }
    }
}
