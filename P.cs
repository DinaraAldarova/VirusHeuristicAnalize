using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusHeuristicAnalize
{
    class P
    {
        private double _infected = 0;
        private double _uninfected = 0;
        private double _weight = 1;
        
        public double Infected
        {
            get
            {
                return _infected;
            }
            set
            {
                if (/*value <= 1 &&*/ value >= 0)
                    _infected = value;
            }
        }
        public double Uninfected
        {
            get
            {
                return _uninfected;
            }
            set
            {
                if (/*value <= 1 &&*/ value >= 0)
                    _uninfected = value;
            }
        }
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
            _infected = 0;
            _uninfected = 0;
            _weight = 1;
        }
        public P(double infected, double uninfected, double weight)
        {
            _infected = infected;
            _uninfected = uninfected;
            _weight = weight;
        }

        public static P operator +(P prob1, P prob2)
        {
            return new P(prob1._infected * prob1._weight + prob2._infected * prob2._weight, 
                        prob1._uninfected * prob1._weight + prob2._uninfected * prob2._weight, 
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

        public bool isNull()
        {
            return _infected == 0 && _uninfected == 0;
        }
    }
}
