using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_INZ.Connection;
using WPF_INZ.Fuzzy;

namespace WPF_INZ.Calculations
{
    public class Calculation
    {
        FuzzyLogic fuzzyLogic;
        Rules rules;
        ConnectionToArduino connection;
        StackPanel stackPanel;
        string[] delay = new string[1000];// table with delays to arduino
        int[] choseCombination = new int[3];

        
        public Calculation(StackPanel stackPanel)
        {
            this.stackPanel = stackPanel;
            fuzzyLogic = new FuzzyLogic();
            rules = new Rules();
            connection = new ConnectionToArduino();
        }

        private void choseOption()
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
                rules.rulesRun(fuzzyLogic.fuzzificationInput, choseCombination);
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
        public void SendData()
        {
            choseOption();
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
    }
}
