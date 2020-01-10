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
    public class MazeretTurManager : IMazeretTurService
    {
        private IMazeretTurDal _mazeretTurDal;

        public MazeretTurManager(IMazeretTurDal mazeretTurDal)
        {
            _mazeretTurDal = mazeretTurDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int mazeretTurId)
        {
            _mazeretTurDal.Delete(new MazeretTur { Id = mazeretTurId });
        }

        public MazeretTur GetById(int mazeretTurId)
        {
            return _mazeretTurDal.Get(x => x.Id == mazeretTurId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<MazeretTur> GetList()
        {
            return _mazeretTurDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(MazeretTur mazeretTur)
        {
            _mazeretTurDal.Insert(mazeretTur);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(MazeretTur mazeretTur)
        {
            _mazeretTurDal.Update(mazeretTur);
        }
    }
}
