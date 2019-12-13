using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusHeuristicAnalize
{
    class P
    {
        public double infected { get; } = 0.0;
        public double uninfected { get; } = 1.0;
        private double _weight = 1;
        
        public double Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if (value <= 1 && value >= 0)
                    _weight = value;
            }
        }
        
        public P()
        {
            infected = 0.0;
            uninfected = 1.0;
            _weight = 1;
        }
        public P(double infected, double uninfected, double weight)
        {
            double sum = infected + uninfected;
            this.infected = infected / sum;
            this.uninfected = uninfected / sum;
            _weight = weight;
        }

        public static P operator +(P prob1, P prob2)
        {
            return new P(prob1.infected * prob1._weight + prob2.infected * prob2._weight,
                        prob1.uninfected * prob1._weight + prob2.uninfected * prob2._weight,
                        1);
        }

        public static P SumArray (P[] array)
        {
            P res = new P();
            foreach (P prob in array)
            {
                if (prob != null)
                {
                    res += prob;
                }
            }
            return res;
        }
    }
}
