using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract.EczaneNobet;

namespace ConsoleApp1
{
    class Program
    {
        private static INobetGrupService _nobetGrupService;

        public Program(INobetGrupService nobetGrupService)
        {
            _nobetGrupService = nobetGrupService;
        }

        static void Main(string[] args)
        {
            var c = _nobetGrupService.GetDetaylar();

            Console.WriteLine("s");
            Console.ReadLine();
        }
    }
}
