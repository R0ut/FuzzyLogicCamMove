﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_INZ.Fuzzy
{
    class Rules
    {
        public double[] rules = new double[3];

        //moc z jaką odpaliła się dana regóła
        public void rulesRun(double[] fuzzification, int[] choseCombination)
        {
            if (fuzzification[0] >= 0) rules[choseCombination[0]] = fuzzification[0];
            if (fuzzification[1] >= 0) rules[choseCombination[1]] = fuzzification[1];
            if (fuzzification[2] >= 0) rules[choseCombination[2]] = fuzzification[2];
        }
    }
}
