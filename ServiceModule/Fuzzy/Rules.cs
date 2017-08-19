
namespace ServiceModule.Fuzzy
{
    public class Rules
    {
        public double[] rules = new double[3];

        /// <summary>
        /// Execute rules
        /// </summary>
        /// <param name="fuzzification">fuzzy input</param>
        /// <param name="choseCombination">Combination of rules</param>
        public void RulesRun(double[] fuzzification, int[] choseCombination)
        {
            if (fuzzification[0] >= 0) rules[choseCombination[0]] = fuzzification[0];
            if (fuzzification[1] >= 0) rules[choseCombination[1]] = fuzzification[1];
            if (fuzzification[2] >= 0) rules[choseCombination[2]] = fuzzification[2];
        }
    }
}
