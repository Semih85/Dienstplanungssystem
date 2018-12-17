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

namespace WM.Optimization.Concrete.IbmCplex.Samples
{
    /*
    public sealed class BlendCplexModelVer2 : IBlendOptModel
    {
        #region local değişkenler

        public string _modelAdi;
        int _a;
        int _nbElements;
        int _nbRaw;
        int _nbScrap;
        int _nbIngot;
        double _alloy;

        double[] _cm;
        double[] _cr;
        double[] _cs;
        double[] _ci;
        double[] _p;
        double[] _P;

        double[][] _PRaw;
        double[][] _PScrap;
        double[][] _PIngot;

        //Karar Değişkenleri
        class MyVar
        {
            public double hm;
            public INumVar h;
        }

        List<MyVar> hh;

        INumVar[] m;
        INumVar[] r;
        INumVar[] s;
        INumVar[] i;
        INumVar[] le;

        //Çözücü
        Cplex cplex;
        #endregion

        #region Sonuç Değişkenleri
        public BlendResultModel ResultModel { get; set; }
        public BlendResultModel ResultNegative { get; set; }
        #endregion

        #region Cplex Çözüm Modeli

        /// <summary>
        /// Blend Optimizasyon Çözüm Modeli
        /// </summary>
        /// <param name="data"></param>
        /// <returns>BlendResultModel</returns>
        private BlendResultModel CozumModel(BlendDataModel data)
        {
            #region Veriler
            _modelAdi = "Blend";

            _a = data.A;
            _nbElements = data.NbElements;
            _nbRaw = data.NbRaw;
            _nbScrap = data.NbScrap;
            _nbIngot = data.NbIngot;
            _alloy = data.Alloy;

            _cm = data.Cm;
            _cr = data.Cr;
            _cs = data.Cs;
            _ci = data.Ci;
            _p = data._p;
            _P = data._P;

            _PRaw = data.PRaw;
            _PScrap = data.PScrap;
            _PIngot = data.PIngot;

            cplex = new Cplex();
            #endregion

            #region Karar Değişkenleri
            //Karar Değişkenleri
            hh = new List<MyVar>()
            {
                new MyVar(){ h =cplex.NumVar(0.0, Double.MaxValue)}
            };

            m = cplex.NumVarArray(_nbElements, 0.0, Double.MaxValue);
            r = cplex.NumVarArray(_nbRaw, 0.0, Double.MaxValue);
            s = cplex.NumVarArray(_nbScrap, 0.0, Double.MaxValue);
            i = cplex.NumVarArray(_nbIngot, 0.0, Double.MaxValue);
            le = new INumVar[_nbElements];

            #endregion

            #region Amaç Fonksiyonu

            // Objective Function: Minimize Cost
           
            ILinearNumExpr k = cplex.LinearNumExpr();
            
            foreach (var v in hh)
            {   
                k.AddTerm(v.h, v.hm);
            }
            //INumExpr numExpr = k;

            cplex.AddMinimize(k);

            cplex.AddMinimize(cplex.Sum(cplex.ScalProd(_cm, m),
                                        cplex.ScalProd(_cr, r),
                                        cplex.ScalProd(_cs, s),
                                        cplex.ScalProd(_ci, i)));
            #endregion

            #region Kısıtlar

            // Min and max quantity of each element in alloy
            for (int j = 0; j < _nbElements; j++)
            {
                le[j] = cplex.NumVar(_p[j] * _alloy, _P[j] * _alloy);
            }

            // Constraint: produce requested quantity of alloy
            cplex.AddEq(cplex.Sum(le), _alloy);

            // Constraints: Satisfy element quantity requirements for alloy
            for (int j = 0; j < _nbElements; j++)
            {
                cplex.AddEq(le[j],
                            cplex.Sum(m[j],
                                      cplex.ScalProd(_PRaw[j], r),
                                      cplex.ScalProd(_PScrap[j], s),
                                      cplex.ScalProd(_PIngot[j], i)));
            }

            #endregion

            try
            {
                if (cplex.Solve())
                {
                    if (cplex.GetStatus().Equals(Cplex.Status.Infeasible))
                    {
                        ResultNegative = new BlendResultModel()
                        {
                            Satatus = cplex.GetStatus().ToString(),
                            ResultMessage = "Çözüm başarısız ...."
                        };

                        return ResultNegative;
                    }

                    #region Sonuçların Alınması
                    ResultModel = new BlendResultModel()
                    {
                        Satatus = cplex.GetStatus().ToString(),
                        ObjectiveValue = cplex.GetObjValue(),
                        ResultMessage = "Çözüm başarılı.",

                        MVals = cplex.GetValues(m),
                        RVals = cplex.GetValues(r),
                        SVals = cplex.GetValues(s),
                        IVals = cplex.GetValues(i),
                        EVals = cplex.GetValues(le)
                    };
                    #endregion

                }
                cplex.End();

                return ResultModel;
            }
            catch (ILOG.Concert.Exception exc)
            {
                Console.WriteLine("Concert exception '" + exc + "' caught");
                Console.ReadKey();
            }

            return ResultModel;
        }

        #endregion

        public BlendResultModel Coz(BlendDataModel data)
        {
            CozumModel(data);

            return ResultModel;
        }

    }
    */
}
