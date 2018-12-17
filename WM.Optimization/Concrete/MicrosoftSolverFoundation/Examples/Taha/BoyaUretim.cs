using Microsoft.SolverFoundation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Optimization.Concrete.MicrosoftSolverFoundation.Examples.Taha
{
    public class BoyaUretim
    {
        public void OptMethod2()
        {
            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();

            //decisions
            Decision x1 = new Decision(Domain.RealNonnegative, "dis_boyanin_gunluk_uretim_miktari");
            Decision x2 = new Decision(Domain.RealNonnegative, "ic_boyanin_gunluk_uretim_miktari");

            model.AddDecisions(x1, x2);

            //Goals
            model.AddGoal("Profit", GoalKind.Maximize, 5 * x2 + 4 * x2);

            //constraints
            model.AddConstraint("M1_Hammaddesi", 6 * x1 + 4 * x2 <= 24);
            model.AddConstraint("M2_Hammaddesi", x1 + 2 * x2 <= 6);
            model.AddConstraint("ic_boya_gun_fazlasi", -x1 + x2 <= 1);
            model.AddConstraint("ic_boya_en_çok", x2 <= 2);

            // This doesn't work!
            // model.AddGoal("Profit", GoalKind.Maximize, objfunc(xs, xl));

            Solution sol = context.Solve(new SimplexDirective());
            Report report = sol.GetReport();
            Console.WriteLine(report);
        }
    }
}
