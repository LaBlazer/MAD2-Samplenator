using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samplenator
{
    internal static class Statistics
    {
        // https://www.itl.nist.gov/div898/handbook/eda/section3/eda35f.htm

        public static (double[], double[]) Normalize(List<int> a, List<int> b)
        {
            // make same size
            if (a.Count != b.Count)
            {
                if (a.Count < b.Count)
                {
                    b.RemoveRange(a.Count, b.Count - a.Count);
                }
                else
                {
                    a.RemoveRange(b.Count, a.Count - b.Count);
                }
            }

            // make same range

            int maxA = 0, maxB = 0;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] > maxA) maxA = a[i];
                if (b[i] > maxB) maxB = b[i];
            }

            double ratio = (double)maxA / (double)maxB;

            double[] da = new double[a.Count];
            double[] db = new double[b.Count];

            for (int i = 0; i < b.Count; i++)
            {
                da[i] = a[i] / (double)maxA;
                db[i] = b[i] / (double)maxB;
            }

            return (da, db);
        }

        public static double CalculateDval(List<int> observed, List<int> expected)
        {

            var (obs, exp) = Normalize(observed, expected);


            return KolmogorovSmirnov(obs, exp);
            //return ChiSquarePval(ChiFromFreqs(obs, exp), obs.Length - 1);
            //return ChiFromFreqs(obs, exp);
        }

        private static double KolmogorovSmirnov(double[] observed, double[] expected)
        {
            double max = 0.0;
            for (int i = 0; i < observed.Length; ++i)
            {
                var dist = Math.Abs(observed[i] - expected[i]);
                if(dist > max)
                {
                    max = dist;
                }
            }
            return max;
        }

    }
}
