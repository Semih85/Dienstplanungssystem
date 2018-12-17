using GridMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.UI.Mvc.HtmlHelpers;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Controllers
{
    public class GridMvcController : Controller
    {
        private static List<Kisi> _SampleKisiler = null;
        //private static IQueryable<Kisi> _Kisiler = null;
        private IGridMvcHelper gridMvcHelper;
        private IDemoData data;

        public GridMvcController()
        {
            this.gridMvcHelper = new GridMvcHelper();
            this.data = new FootballersData();
        }
        // GET: GridMvc
        public ActionResult Index()
        {
            if (_SampleKisiler == null)
                _SampleKisiler = Kisi.GetSampleKisiler(20);

            return View(_SampleKisiler);
        }

        [ChildActionOnly]
        public ActionResult GridAjaxPartial()
        {
            var items = this.data.GetFootballers().OrderBy(f => f.FirstName);
            var grid = this.gridMvcHelper.GetAjaxGrid(items);

            return PartialView("GridAjaxPartial", grid);
        }

        [HttpGet]
        public ActionResult GridAjaxPartialPager(int? page)
        {
            var items = this.data.GetFootballers().OrderBy(f => f.FirstName);
            var grid = this.gridMvcHelper.GetAjaxGrid(items, page);
            object jsonData = this.gridMvcHelper.GetGridJsonData(grid, "GridAjaxPartial", this);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

    }

    public class Kisi
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }

        public static List<Kisi> GetSampleKisiler(int count = 10)
        {
            Random rnd = new Random();
            List<Kisi> kisiler = new List<Kisi>();

            for (int i = 0; i < count; i++)
            {
                kisiler.Add(new Kisi()
                {
                    Id = i,
                    FullName = FakeData.NameData.GetFullName(),
                    Age = rnd.Next(10, 99),
                    IsActive = (i % 2 == 0) ? true : false,
                    BirthDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-25), DateTime.Now.AddYears(-2))
                });
            }

            return kisiler;
        }
    }

    public class FootballersData : IDemoData
    {
        public IQueryable<Footballer> GetFootballers()
        {
            var footballers = new List<Footballer>();
            footballers.Add(new Footballer("Jon", "Doe2", 20));
            footballers.Add(new Footballer("Jon1", "Doe1", 18));
            footballers.Add(new Footballer("Jon7", "Doe2", 19));
            footballers.Add(new Footballer("Jon6", "Doe5", 31));
            footballers.Add(new Footballer("Jon", "Doe3", 33));
            footballers.Add(new Footballer("Jon", "Doe2", 20));
            footballers.Add(new Footballer("Jon2", "Doe", 28));
            footballers.Add(new Footballer("Jon", "Doe1", 26));
            footballers.Add(new Footballer("Jon8", "Doe4", 25));
            footballers.Add(new Footballer("Jon9", "Doe6", 25));
            footballers.Add(new Footballer("Jon3", "Doe4", 25));
            footballers.Add(new Footballer("Jon10", "Do4e", 21));
            footballers.Add(new Footballer("Jon4", "Doe2", 18));
            footballers.Add(new Footballer("Jon22", "Doe3", 44));
            footballers.Add(new Footballer("Jon5", "Doe15", 33));
            footballers.Add(new Footballer("Jon13", "Doe2", 21));

            return footballers.AsQueryable();
        }
    }

    public class Footballer
    {
        public Footballer(string firstName, string lastName, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }

    public interface IDemoData
    {
        IQueryable<Footballer> GetFootballers();
    }
}