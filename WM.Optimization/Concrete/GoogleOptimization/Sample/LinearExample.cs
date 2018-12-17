using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Optimization.Concrete.GoogleOptimization.Sample
{
    class LinearExample
    {
        //private static void RunLinearExample(String solverType)
        //{
        //    Solver solver = Solver.CreateSolver("LinearExample", solverType);
        //    // x and y are continuous non-negative variables.
        //    Variable x = solver.MakeNumVar(0.0, double.PositiveInfinity, "x");
        //    Variable y = solver.MakeNumVar(0.0, double.PositiveInfinity, "y");
        //    // Objective function: 3x + 4y.
        //    Objective objective = solver.Objective();
        //    objective.SetCoefficient(x, 3);
        //    objective.SetCoefficient(y, 4);
        //    objective.SetMaximization();
        //    // x + 2y <= 14.
        //    Constraint c0 = solver.MakeConstraint(double.NegativeInfinity, 14.0);
        //    c0.SetCoefficient(x, 1);
        //    c0.SetCoefficient(y, 2);

        //    // 3x - y >= 0.
        //    Constraint c1 = solver.MakeConstraint(0.0, double.PositiveInfinity);
        //    c1.SetCoefficient(x, 3);
        //    c1.SetCoefficient(y, -1);

        //    // x - y <= 2.
        //    Constraint c2 = solver.MakeConstraint(double.NegativeInfinity, 2.0);
        //    c2.SetCoefficient(x, 1);
        //    c2.SetCoefficient(y, -1);
        //    Console.WriteLine("Number of variables = " + solver.NumVariables());
        //    Console.WriteLine("Number of constraints = " + solver.NumConstraints());
        //    solver.Solve();
        //    // The value of each variable in the solution.
        //    Console.WriteLine("Solution:");
        //    Console.WriteLine("x = " + x.SolutionValue());
        //    Console.WriteLine("y = " + y.SolutionValue());
        //    // The objective value of the solution.
        //    Console.WriteLine("Optimal objective value = " +
        //                      solver.Objective().Value());
        //}

        //static void Main()
        //{
        //    RunLinearExample("GLOP_LINEAR_PROGRAMMING");
        //}
    }
}
