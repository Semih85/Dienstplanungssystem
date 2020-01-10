using ILOG.Concert;
using ILOG.CPLEX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Optimization.Abstract.Samples;
using WM.Core.Optimization;
using WM.Northwind.Entities.Concrete.Optimization.Blend;

namespace WM.Optimization.Concrete.IbmCplex.Samples
{
    public sealed class BlendCplex : IBlendOptimization
    {
        #region değişkenler

        public string _modelAdi;
        BlendDataModel _dataModel;

        //Karar Değişkenleri
        INumVar[] m;
        INumVar[] r;
        INumVar[] s;
        INumVar[] i;
        INumVar[] le;

        //Çözücü
        Cplex cplex;

        // Sonuç Değişkenleri
        //public BlendResultModel ResultModel { get; set; }
        public BlendResultModel Results { get; set; }

        #endregion

        /// <summary>
        /// Blend Optimizasyon Çözüm Modeli
        /// </summary>
        /// <param name="data"></param>
        /// <returns>BlendResultModel</returns>
        private BlendResultModel Model(BlendDataModel data)
        {

            #region Veriler

            //parametrelerin local değişkenlere aktarılması.

            _modelAdi = "Blend";
            _dataModel = data;

            cplex = new Cplex();
            #endregion

            #region Karar Değişkenleri

            m = cplex.NumVarArray(_dataModel.NbElements, 0.0, Double.MaxValue);
            r = cplex.NumVarArray(_dataModel.NbRaw, 0.0, Double.MaxValue);
            s = cplex.NumVarArray(_dataModel.NbScrap, 0.0, Double.MaxValue);
            i = cplex.NumVarArray(_dataModel.NbIngot, 0.0, Double.MaxValue);
            le = new INumVar[_dataModel.NbElements];

            #endregion

            #region Amaç Fonksiyonu

            // Objective Function: Minimize Cost

            cplex.AddMinimize(cplex.Sum(cplex.ScalProd(_dataModel.Cm, m),
                                        cplex.ScalProd(_dataModel.Cr, r),
                                        cplex.ScalProd(_dataModel.Cs, s),
                                        cplex.ScalProd(_dataModel.Ci, i)));
            #endregion

            #region Kısıtlar

            // Min and max quantity of each element in alloy
            for (int j = 0; j < _dataModel.NbElements; j++)
            {
                le[j] = cplex.NumVar(_dataModel._p[j] * _dataModel.Alloy, _dataModel._P[j] * _dataModel.Alloy);
            }

            // Constraint: produce requested quantity of alloy
            cplex.AddEq(cplex.Sum(le), _dataModel.Alloy);

            // Constraints: Satisfy element quantity requirements for alloy
            for (int j = 0; j < _dataModel.NbElements; j++)
            {
                cplex.AddEq(le[j],
                            cplex.Sum(m[j],
                                      cplex.ScalProd(_dataModel.PRaw[j], r),
                                      cplex.ScalProd(_dataModel.PScrap[j], s),
                                      cplex.ScalProd(_dataModel.PIngot[j], i)));
            }

            #endregion

            #region Çözümün işlenmesi 
            try
            {
                if (cplex.Solve())
                {
                    if (cplex.GetStatus().Equals(Cplex.Status.Infeasible))
                    {
                        throw new ILOG.Concert.Exception("No feasible solution found!");
                    }
                    #region Sonuçların Alınması

                    Results = new BlendResultModel()
                    {
                        Satatus = cplex.GetStatus().ToString(),
                        ObjectiveValue = cplex.GetObjValue(),
                        ResultMessage = "Çözüm başarılı.",

                        BlendResults = new BlendResult()
                        {
                            MVals = cplex.GetValues(m),
                            RVals = cplex.GetValues(r),
                            SVals = cplex.GetValues(s),
                            IVals = cplex.GetValues(i),
                            EVals = cplex.GetValues(le)
                        }

                    };                    
                    #endregion

                }
                cplex.End();

                return Results;
            }
            catch (ILOG.Concert.Exception exc)
            {
                //exc.GetBaseException();
                Console.WriteLine("Concert exception '" + exc + "' caught");
                Console.ReadKey();
            }

            return Results;
            #endregion
        }

        public BlendResultModel Solve(BlendDataModel data)
        {
            Model(data);
            return Results;
        }

    }
}


//_dataModel = new BlendDataModel()
//{
//    A = data.A,
//    NbElements = data.NbElements,
//    NbRaw = data.NbRaw,
//    NbScrap = data.NbScrap,
//    NbIngot = data.NbIngot,
//    Alloy = data.Alloy,

//    Cm = data.Cm,
//    Cr = data.Cr,
//    Cs = data.Cs,
//    Ci = data.Ci,
//    _p = data._p,
//    _P = data._P,

//    PRaw = data.PRaw,
//    PScrap = data.PScrap,
//    PIngot = data.PIngot
//};