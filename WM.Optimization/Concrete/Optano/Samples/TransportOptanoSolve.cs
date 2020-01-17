using OPTANO.Modeling.Common;
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Configuration;
using OPTANO.Modeling.Optimization.Solver.Cplex128;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;

namespace WM.Optimization.Concrete.Optano.Samples
{
    public class TransportOptanoSolve
    {

        //public List<TransportResultModel> Results { get; set; }

        //#region Transport Solve Model

        //public List<TransportResultModel> Coz(TransportDataModel data)
        //{
        //    var config = new Configuration
        //    {
        //        NameHandling = NameHandlingStyle.UniqueLongNames,
        //        ComputeRemovedVariables = true
        //    };

        //    using (var scope = new ModelScope(config))
        //    {
        //        // create a model, based on given data and the model scope
        //        var designModel = new TransportOptanoModel(data);

        //        // Get a solver instance, change your solver
        //        var solver = new CplexSolver();

        //        // solve the model
        //        var solution = solver.Solve(designModel._model);

        //        // import the results back into the model 
        //        designModel._model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));

        //        // print objective and variable decisions
        //        var objValue = solution.ObjectiveValues.Single();

        //        Results = new List<TransportResultModel>();

        //        foreach (var r in designModel.DataModel.Maliyetler.Where(s => designModel.x[s].Value > 0))
        //        {
        //            Results.Add(new TransportResultModel()
        //            {
        //                DepoId = r.DepoId,
        //                FabrikaId = r.FabrikaId,
        //                Sonuc = designModel.x[r].Value
        //            });
        //        }
        //    }

        //    return Results;
        //}
        //#endregion
    }
}
