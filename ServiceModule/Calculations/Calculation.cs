using ServiceModule.Connection;
using ServiceModule.Fuzzy;
using ServiceModule.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Ports;
using System.Windows;
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
        string[] delay = new string[1400]; // table with delays to arduino
        int[] choseCombination = new int[3]; //combination of steper speed
        private bool isReConnectNeed = false; //flag to try create new connection

        /// <summary>
        /// Set combination of fuzzy rules, accord to checked radiobutton
        /// </summary>
        private void choseOption(StackPanel stackPanel)
        {
            foreach (object child in stackPanel.Children)
            {
                if ((child as RadioButton != null) && (bool)(child as RadioButton).IsChecked)
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
            double[] firstOutput = new double[4000];
            double[] secondOutput = new double[4000];
            double[] thirdOutput = new double[4000];
            double[] Output = new double[4000];

            for (int k = 0; k < 1400; k++)
            {
                fuzzyLogic.FuzzificationInput(k);
                rules.RulesRun(fuzzyLogic.fuzzificationInput, choseCombination);
                double result = 0;
                double sumOfWeights = 0; // sum of weights

                for (int i = 0; i < 4000; i++) //max z output
                {
                    fuzzyLogic.FuzzificationOutput(i + 1000); // + 1000 bo output min jest 1000
                    firstOutput[i] = Math.Min(rules.rules[0], fuzzyLogic.fuzzificationOutput[0]);
                    secondOutput[i] = Math.Min(rules.rules[1], fuzzyLogic.fuzzificationOutput[1]);
                    thirdOutput[i] = Math.Min(rules.rules[2], fuzzyLogic.fuzzificationOutput[2]);
                    Output[i] = Math.Max(Math.Max(firstOutput[i], secondOutput[i]), thirdOutput[i]);
                    result += (i + 1000) * Output[i];
                    sumOfWeights += Output[i];
                }
                delay[k] = Math.Round((result / sumOfWeights), 0).ToString();
            }
        }

        /// <summary>
        /// Sending the array of delays to arduino, and recive data from arduino when done operation
        /// </summary>
        /// <param name="stackPanel">Stack panel with selected combination of speed</param>
        private void sendData()
        {
            try
            {
                if (isReConnectNeed)
                {
                    connection = new ConnectionToArduino();
                }
                    
                isReConnectNeed = false;

                foreach (var item in delay)
                {
                    if (!connection.myPort.IsOpen)
                        connection.myPort.Open();

                    connection.myPort.Write(item);

                    connection.myPort.ReadLine();
                }
            }
            catch (IOException portEx)
            {
                MessageBox.Show("Connection problem, can`t find device", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                isReConnectNeed = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something goes wrong ...", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Chose combination
        /// </summary>
        public void ChoseOption(StackPanel stackPanel)
        {
            choseOption(stackPanel);
        }

        /// <summary>
        /// Public method to, sending the array of delays to arduino, and recive data from arduino when done operation
        /// </summary>
        public void SendDataToArduino()
        {
            sendData();
        }

        /// <summary>
        /// Calculate delays
        /// </summary>
        /// <returns>Array of delays</returns>
        public string[] CalculateDelay()
        {
            calculateDelay();
            return delay;
        }
    }
}
