using Microsoft.SolverFoundation.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Optimization.Concrete.MicrosoftSolverFoundation.Examples
{
    public class SimpleSamples
    {
        public void OptMethod1()
        {
            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();

            //decisions
            Decision xs = new Decision(Domain.Real, "Number_of_small_chess_boards");
            Decision xl = new Decision(Domain.Real, "Number_of_large_chess_boards");

            model.AddDecisions(xs, xl);

            //constraints
            model.AddConstraints("limits", 0 <= xs, 0 <= xl);
            model.AddConstraint("BoxWood", 1 * xs + 3 * xl <= 200);
            model.AddConstraint("Lathe", 3 * xs + 2 * xl <= 160);

            //Goals
            model.AddGoal("Profit", GoalKind.Maximize, 5 * xs + 20 * xl);

            // This doesn't work!
            // model.AddGoal("Profit", GoalKind.Maximize, objfunc(xs, xl));

            Solution sol = context.Solve(new SimplexDirective());
            Report report = sol.GetReport();
            Console.WriteLine(report);
        }

        public void OptMethod3()
        {
            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();

            //decisions
            Decision vz = new Decision(Domain.RealNonnegative, "barrels_venezuela");
            Decision sa = new Decision(Domain.RealNonnegative, "barrels_saudiarabia");
            model.AddDecisions(vz, sa);

            //Goals
            model.AddGoal("cost", GoalKind.Minimize,
              20 * sa + 15 * vz);

            //constraints
            model.AddConstraints("limits",
              0 <= vz <= 9000,
              0 <= sa <= 6000);

            model.AddConstraints("production",
              0.3 * sa + 0.4 * vz >= 2000,
              0.4 * sa + 0.2 * vz >= 1500,
              0.2 * sa + 0.3 * vz >= 500);

            // This doesn't work!
            // model.AddGoal("Profit", GoalKind.Maximize, objfunc(xs, xl));

            Solution sol = context.Solve(new SimplexDirective());
            Report report = sol.GetReport();
            Console.WriteLine("vz: {0}, sa: {1}", vz, sa);
            Console.Write("{0}", report);
            Console.ReadLine();

            //Console.WriteLine(report);
        }

        class Urun
        {
            public string Ad;
            public int Maliyet;
            public int Satis;
            public int MaksimumStok;

            public int Kar
            {
                get { return Satis - Maliyet; }
            }
        }

        public void OptMethod4()
        {

            //En fazla 5000 TL üretim yapabiliriz.  
            int UretimButce = 5000;


            //Örnek basit ve anlaşılır olsun diye collection kullanılmadı. 
            //İstenilirse collection türünden nesne kabul eden versiyonlarıda var  
            //Urunlerimiz : Decision
            Urun A_Urun = new Urun
            {
                Ad = "A_Ürünü",
                Maliyet = 5,
                Satis = 8,
                MaksimumStok = 1350
            };
            Urun B_Urun = new Urun
            {
                Ad = "B_Ürünü",
                Maliyet = 8,
                Satis = 13,
                MaksimumStok = 875
            };
            Urun C_Urun = new Urun
            {
                Ad = "C_Ürünü",
                Maliyet = 3,
                Satis = 5,
                MaksimumStok = 1100
            };


            Decision DecisionA = new Decision(Domain.IntegerNonnegative, A_Urun.Ad);
            Decision DecisionB = new Decision(Domain.IntegerNonnegative, B_Urun.Ad);
            Decision DecisionC = new Decision(Domain.IntegerNonnegative, C_Urun.Ad);


            // Çözücü
            var solver = SolverContext.GetContext();

            //Model Tanımı Decision + Constraint
            var model = solver.CreateModel();

            model.AddDecision(DecisionA);
            model.AddDecision(DecisionB);
            model.AddDecision(DecisionC);

            //Kısıtlamalar, Şartlar
            model.AddConstraint("Butce_Kisitlamasi",
                           A_Urun.Maliyet * DecisionA +
                           B_Urun.Maliyet * DecisionB +
                           C_Urun.Maliyet * DecisionC <= UretimButce);
            //Ürettiğimiz ürünlerin toplam maliyeti üretim bütcemizden kücük veya eşit olmalı 

            model.AddConstraint("A_Urunu_Stoklama_Kapasitemiz", DecisionA < A_Urun.MaksimumStok);
            model.AddConstraint("B_Urunu_Stoklama_Kapasitemiz", DecisionB < B_Urun.MaksimumStok);
            model.AddConstraint("C_Urunu_Stoklama_Kapasitemiz", DecisionC < C_Urun.MaksimumStok);
            //Üreteceğimiz ürünler depomuza sığmalı

            //Girdiler tanımlandı.
            //Şimdi sonuç olarak ne istediğimizi tanımlayalım
            //Üreteceğimiz ürünlerin karlığının en fazla olduğu optimizasyonu istiyoruz
            model.AddGoal("En_iyi_uretim_stok", GoalKind.Maximize,
            (A_Urun.Kar * DecisionA) +
            (B_Urun.Kar * DecisionB) +
            (C_Urun.Kar * DecisionC));


            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Lütfen Bekleyiniz");


            // Çöz
            Solution solution = solver.Solve();

            // Get our decisions
            Console.WriteLine("Çözüm Kalitesi : " + solution.Quality.ToString());
            Console.WriteLine("A Ürününden {0} adet ", DecisionA);
            Console.WriteLine("B Ürününden {0} adet ", DecisionB);
            Console.WriteLine("C Ürününden {0} adet ", DecisionC);

            double gerceklesen_toplam_maliyet =
            DecisionA.ToDouble() * A_Urun.Maliyet +
            DecisionB.ToDouble() * B_Urun.Maliyet +
            DecisionC.ToDouble() * C_Urun.Maliyet;

            double toplam_satis_fiyat =
            DecisionA.ToDouble() * A_Urun.Satis +
            DecisionB.ToDouble() * B_Urun.Satis +
            DecisionC.ToDouble() * C_Urun.Satis;


            Console.WriteLine("Üretim: Harcanan : {0} Planlanan: {1}", gerceklesen_toplam_maliyet, UretimButce);
            Console.WriteLine("Satış Değeri : {0} Kar: {1}", toplam_satis_fiyat, toplam_satis_fiyat - gerceklesen_toplam_maliyet);
            Console.WriteLine("Optimizasyon {0} milisaniyede tamamlandı", sw.ElapsedMilliseconds);
            Console.ReadLine();

        }

        /// <summary>
        /// Cplex
        /// </summary>
        //public void OptMethod5()
        //{
        //    SolverContext context = SolverContext.GetContext();
        //    Model model = context.CreateModel();

        //    Decision x1 = new Decision(Domain.RealRange(0, 40), "x1");
        //    Decision x2 = new Decision(Domain.RealRange(0, Rational.PositiveInfinity), "x2");
        //    Decision x3 = new Decision(Domain.RealRange(0, Rational.PositiveInfinity), "x3");
        //    Decision x4 = new Decision(Domain.IntegerRange(2, 3), "x4");

        //    model.AddDecisions(x1, x2, x3, x4);

        //    model.AddConstraint("Row1", -x1 + x2 + x3 + 10 * x4 <= 20);
        //    model.AddConstraint("Row2", x1 - 3 * x2 + x3 <= 30);
        //    model.AddConstraint("Row3", x2 - 3.5 * x4 == 0);

        //    Goal goal = model.AddGoal("Goal", GoalKind.Maximize, x1 + 2 * x2 + 3 * x3 + x4);

        //    // Turn on CPLEX log
        //    CplexDirective cplexDirective = new CplexDirective();
        //    cplexDirective.OutputFunction = Console.Write;

        //    Solution solution = context.Solve(cplexDirective);
        //    Report report = solution.GetReport();
        //    Console.WriteLine("x: {0}, {1}, {2}", x1, x2, x3, x4);
        //    Console.Write("{0}", report);
        //    context.ClearModel();
        //}
    }
}
