using OPTANO.Modeling.Common;
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Configuration;
using OPTANO.Modeling.Optimization.Enums;
using OPTANO.Modeling.Optimization.Solver.Cplex128;
using System.Collections.Generic;
using System.Linq;
using WM.Northwind.Entities.Concrete.Optimization.Transport;
using WM.Optimization.Abstract.Samples;

namespace WM.Optimization.Concrete.Optano.Samples
{
    public class TransportOptano : ITransportOptimization
    {
        public Model _model { get; private set; }
        public TransportDataModel DataModel { get; set; }
        public TransportSonucModel Results { get; set; }

        //Karar Değişkeni
        public VariableCollection<TransportMaliyet> x { get; private set; }
        
        #region Transportation Model
        public void Model()
        {
            _model = new Model();

            x = new VariableCollection<TransportMaliyet>(
                _model,
                DataModel.Maliyetler,
                "x",
                h => $" {h.Id}, Fabrika: {h.FabrikaId}'dan , Depoya: {h.DepoId} gönderilen malzeme miktarı.",
                h => DataModel.LowerBound,
                h => DataModel.UpperBound,
                h => VariableType.Continuous);

            // Add the objective
            _model.AddObjective(
                    new Objective(Expression.Sum(DataModel.Maliyetler
                                                    .Select(i => (x[i] * i.Deger))),
                                                    "Sum of all item-values: ",
                                                    ObjectiveSense.Minimize)
                                                    );
            // Create Constraints

            //Talep Kısıtları
            foreach (var d in DataModel.Depolar)
            {
                _model.AddConstraint(
                               Expression.Sum(DataModel.Maliyetler
                                                .Where(k => k.DepoId == d.Id)
                                                .Select(m => x[m] )) == d.Talep,
                                                $"her fabrika bir depoya atanmalı, {d}");
            }

            //Arz Kısıtları
            foreach (var f in DataModel.Fabrikalar)
            {
                _model.AddConstraint(
                               Expression.Sum(DataModel.Maliyetler
                                                .Where(l => l.FabrikaId == f.Id)
                                                .Select(m => x[m] )) == f.Kapasite,
                                                                $"her fabrika bir depoya atanmalı, {f}");
            }
        }
        #endregion

        #region Transport Solve Model
        public TransportSonucModel Solve(TransportDataModel data)
        {
            var config = new Configuration
            {
                NameHandling = NameHandlingStyle.UniqueLongNames,
                ComputeRemovedVariables = true
            };

            using (var scope = new ModelScope(config))
            {
                DataModel = data;

                Model();

                // Get a solver instance, change your solver
                var solver = new CplexSolver();

                // solve the model
                var solution = solver.Solve(_model);

                // import the results back into the model 
                _model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));

                // print objective and variable decisions
                var objective = solution.ObjectiveValues.Single();

                Results = new TransportSonucModel
                {
                    ObjectiveValue = objective.Value,
                    ResultModel = new List<TransportSonuc>()
                };

                foreach (var r in DataModel.Maliyetler.Where(s => x[s].Value > 0))
                {
                    Results.ResultModel.Add(new TransportSonuc()
                    {
                        DepoId = r.DepoId,
                        FabrikaId = r.FabrikaId,
                        Sonuc = x[r].Value
                    });
                }
            }

            return Results;
        }
        #endregion
    }
}
