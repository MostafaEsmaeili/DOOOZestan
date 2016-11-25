using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Utility
{
    public static class Calculator
    {
        public static double Variance(this IEnumerable<double> numbers )
        {
            try
            {
                var ave = numbers.Average();

                double sigma = numbers.Sum(d => Math.Pow(d - ave, 2));

                return sigma / (numbers.Count() - 1);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public static double CoVariance(IEnumerable<double> x,IEnumerable<double> y)
        {
            try
            {
                if (x.Count()!=y.Count())
                {
                    throw new Exception("Variables[X-Y] Count is not Equal!");
                }
                var aveX = x.Average();
                var aveY = y.Average();

                double sigma = 0;

                for (int i = 0; i < y.Count(); i++)
                {
                    sigma += ( y.ToList()[i] -aveY) * (x.ToList()[i] -aveX);
                }

                return (sigma)/(x.Count()-1);

            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}
