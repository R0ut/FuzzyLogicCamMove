namespace ServiceModule.Fuzzy
{
    /// <summary>
    /// Contain input and output terms. Define fuzzification for input and output
    /// </summary>
    public class FuzzyLogic
    {
        private double min, med1, med2, max, off;
        public double[] fuzzificationInput = new double[3]; // array of fuzzy values from slowSpeed/middleSpeed/fastSpeed input terms
        public double[] fuzzificationOutput = new double[3]; // array of fuzzy values from bigPower/middlePower/smalPower output terms

        #region input terms

        /// <summary>
        /// Term for start distance
        /// </summary>
        /// <param name="distance">Actual distance</param>
        private void slowSpeed(double distance)
        {
            min = 0;
            med1 = 100;
            med2 = 400;
            max = 500;


            if (distance <= min)
                off = 0.001;
            else if ((distance >= min) && (distance <= med1))
                off = (distance - min) / (double)(med1 - min);
            else if ((distance >= med1) && (distance <= med2))
                off = 1;
            else if ((distance >= med2) && (distance < max))
                off = (max - distance) / (double)(max - med2);
            else if (distance >= max)
                off = 0;
            fuzzificationInput[0] = off;
        }

        /// <summary>
        /// Term for middle distance
        /// </summary>
        /// <param name="distance">Actual distance</param>
        private void middleSpeed(double distance)
        {
            min = 400;
            med1 = 550;
            med2 = 850;
            max = 1000;


            if (distance < min)
                off = 0;
            else if ((distance >= min) && (distance <= med1))
                off = (distance - min) / (double)(med1 - min);
            else if ((distance >= med1) && (distance < med2))
                off = 1;
            else if ((distance >= med2) && (distance < max))
                off = (max - distance) / (double)(max - med2);
            else if (distance >= max)
                off = 0;
            fuzzificationInput[1] = off;
        }

        /// <summary>
        /// Term for end distance
        /// </summary>
        /// <param name="distance">Actual distance</param>
        private void fastSpeed(double distance)
        {
            min = 900;
            med1 = 1000;
            med2 = 1300;
            max = 1400;

            if (distance <= min)
                off = 0;
            else if ((distance >= min) && (distance <= med1))
                off = (distance - min) / (double)(med1 - min);
            else if ((distance >= med1) && (distance <= med2))
                off = 1;
            else if ((distance >= med2) && (distance < max))
                off = (max - distance) / (double)(max - med2);
            else if (distance >= max)
                off = 0;
            fuzzificationInput[2] = off;
        }
        #endregion

        #region output terms

        /// <summary>
        /// Term for big power
        /// </summary>
        /// <param name="distance">power</param>
        private void bigPower(double power)
        {
            min = 1000;
            med1 = 1500;
            max = 2000;

            if (power < min)
                off = 0;
            else if (power <= med1 && power >= min)
                off = (power - min) / (double)(med1 - min);
            else if (power == med1)
                off = 1;
            else if ((power > med1) && (power < max))
                off = (max - power) / (double)(max - med1);
            else if (power >= max)
                off = 0;
            fuzzificationOutput[0] = off;
        }

        /// <summary>
        /// Term for middle power
        /// </summary>
        /// <param name="distance">power</param>
        private void middlePower(double power)
        {
            min = 2000;
            med1 = 2500;
            max = 3000;


            if (power < min)
                off = 0;
            else if (power <= med1 && power >= min)
                off = (power - min) / (double)(med1 - min);
            else if (power == med1)
                off = 1;
            else if ((power > med1) && (power < max))
                off = (max - power) / (double)(max - med1);
            else if (power >= max)
                off = 0;
            fuzzificationOutput[1] = off;
        }

        /// <summary>
        /// Term for smal power
        /// </summary>
        /// <param name="distance">power</param>
        private void smalPower(double power)
        {
            min = 3000;
            med1 = 3500;
            max = 4000;

            if (power < min)
                off = 0;
            else if (power <= med1 && power >= min)
                off = (power - min) / (double)(med1 - min);
            else if (power == med1)
                off = 1;
            else if ((power > med1) && (power < max))
                off = (max - power) / (double)(max - med1);
            else if (power >= max)
                off = 0;
            fuzzificationOutput[2] = off;
        }
        #endregion

        /// <summary>
        /// Fuzzification of input
        /// </summary>
        /// <param name="para">Value to fuzzify</param>
        public void FuzzificationInput(int para)
        {
            slowSpeed(para);
            middleSpeed(para);
            fastSpeed(para);
        }

        /// <summary>
        /// Fuzzification of output
        /// </summary>
        /// <param name="para">Value to fuzzify</param>
        public void FuzzificationOutput(int para)
        {
            bigPower(para);
            middlePower(para);
            smalPower(para);
        }
    }
}
