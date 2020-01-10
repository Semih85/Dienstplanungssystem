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
using System.Text.RegularExpressions;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class LogManager : ILogService
    {
        private ILogDal _ILogDal;

        public LogManager(ILogDal ILogDal)
        {
            _ILogDal = ILogDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int lId)
        {
            _ILogDal.Delete(new Log { Id = lId });
        }

        public Log GetById(int lId)
        {
            return _ILogDal.Get(x => x.Id == lId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Log> GetList()
        {
            return _ILogDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Log> GetList(params string[] icerik)
        {

            //Regex regex = new Regex("^/(?<Controller>[^/]*)(/(?<Action>[^/]*)(/(?<id>[^?]*)(/?(?<QueryString>.*))?)?)?$");
            //Match match = regex.Match(icerik);

            // match.Groups["Controller"].Value is the controller, 
            // match.Groups["Action"].Value is the action,
            // match.Groups["id"].Value is the id
            // match.Groups["QueryString"].Value are the other parameters
            //var sorguIcerik = $"{icerik}";

            return _ILogDal.GetList(x => icerik.Contains(x.Detail));
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(Log log)
        {
            _ILogDal.Insert(log);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Log log)
        {
            _ILogDal.Update(log);
        }

    }
}