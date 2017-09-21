using ServiceModule.Connection;
using ServiceModule.Fuzzy;
using ServiceModule.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ServiceModule.Calculations
{
    /// <summary>
    /// This class do fuzzy logic, and send data to arduino
    /// </summary>
    [Export(typeof(ICalculationService))]
    public class Calculation : ICalculationService
    {
        FuzzyLogic fuzzyLogic = new FuzzyLogic();
        Rules rules = new Rules();
        ConnectionToArduino connection = new ConnectionToArduino();
        string[] delay = new string[1000]; // table with delays to arduino
        int[] choseCombination = new int[3]; //combination of steper speed

        /// <summary>
        /// Set combination of fuzzy rules, accord to checked radiobutton
        /// </summary>
        private void choseOption(StackPanel stackPanel)
        {
            foreach (object child in stackPanel.Children)
            {
                if ((bool)(child as RadioButton).IsChecked)
                {
                    if ((child as RadioButton).Name == "radioButtonSlowMiddleFast")
                    {
                        choseCombination[0] = 2;
                        choseCombination[1] = 1;
                        choseCombination[2] = 0;
                    }
                    else if ((child as RadioButton).Name == "radioButtonSlowFastMiddle")
                    {
                        choseCombination[0] = 2;
                        choseCombination[1] = 0;
                        choseCombination[2] = 1;
                    }
                    else if ((child as RadioButton).Name == "radioButtonMiddleSlowFast")
                    {
                        choseCombination[0] = 1;
                        choseCombination[1] = 2;
                        choseCombination[2] = 0;
                    }
                    else if ((child as RadioButton).Name == "radioButtonMiddleFastSlow")
                    {
                        choseCombination[0] = 1;
                        choseCombination[1] = 0;
                        choseCombination[2] = 2;
                    }
                    else if ((child as RadioButton).Name == "radioButtonFastSlowMiddle")
                    {
                        choseCombination[0] = 0;
                        choseCombination[1] = 2;
                        choseCombination[2] = 1;
                    }
                    else if ((child as RadioButton).Name == "radioButtonFastMiddleSlow")
                    {
                        choseCombination[0] = 0;
                        choseCombination[1] = 1;
                        choseCombination[2] = 2;
                    }
                }
            }
        }

        /// <summary>
        /// Fuzzy logic calcualtions. Result is in delay array
        /// </summary>
        private void calculateDelay()
        {
            double[] firstOutput = new double[3000];
            double[] secondOutput = new double[3000];
            double[] thirdOutput = new double[3000];
            double[] Output = new double[3000];
           
            for (int k = 0; k <= 999; k++)
            {
                fuzzyLogic.FuzzificationInput(k);
                rules.RulesRun(fuzzyLogic.fuzzificationInput, choseCombination);
                double result = 0;
                double sumOfWeights = 0; // sum of weights

                for (int i = 0; i <= 2600; i++) // +950 aby było 3550
                {
                    fuzzyLogic.FuzzificationOutput(i+950);
                    firstOutput[i] = Math.Min(rules.rules[0], fuzzyLogic.fuzzificationOutput[0]);
                    secondOutput[i] = Math.Min(rules.rules[1], fuzzyLogic.fuzzificationOutput[1]);
                    thirdOutput[i] = Math.Min(rules.rules[2], fuzzyLogic.fuzzificationOutput[2]);
                    Output[i] = Math.Max(Math.Max(firstOutput[i], secondOutput[i]), thirdOutput[i]);
                    result += (i + 950) * Output[i];
                    sumOfWeights += Output[i];
                }

                Math.Round((result / sumOfWeights), 0).ToString();
                delay[k] = Math.Round((result / sumOfWeights), 0).ToString();
            }
        }

        /// <summary>
        /// Sending the array of delays to arduino, and recive data from arduino when done operation
        /// </summary>
        /// <param name="stackPanel">Stack panel with selected combination of speed</param>
        private void sendData(StackPanel stackPanel)
        {
            choseOption(stackPanel);
            calculateDelay();

            foreach (var item in delay)
            {
                if (!connection.myPort.IsOpen)
                    connection.myPort.Open();

                var watch = System.Diagnostics.Stopwatch.StartNew();

                connection.myPort.Write(item);
              
                connection.myPort.ReadLine();
                watch.Stop();
                Console.WriteLine(watch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Public method to, Fuzzy logic calcualtions. Result is in delay array
        /// </summary>
        public void Calculate()
        {
            calculateDelay();
        }

        /// <summary>
        /// Public method to, sending the array of delays to arduino, and recive data from arduino when done operation
        /// </summary>
        /// <param name="stackPanel">Stack panel with selected combination of speed</param>
        public void SendDataToArduino(StackPanel stackPanel)
        {
            sendData(stackPanel);
        }
    }
}
