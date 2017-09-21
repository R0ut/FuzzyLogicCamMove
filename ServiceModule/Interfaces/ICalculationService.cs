using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ServiceModule.Interfaces
{
    /// <summary>
    /// Main fuzzylogic, and sending/reciving data from arduino
    /// </summary>
    public interface ICalculationService
    {
        /// <summary>
        /// Fuzzy logic calcualtions
        /// </summary>
        void Calculate();
        /// <summary>
        /// Send data to arduino
        /// </summary>
        /// <param name="stackPanel">Stack panel with selected combination</param>
        void SendDataToArduino(StackPanel stackPanel);
    }
}
