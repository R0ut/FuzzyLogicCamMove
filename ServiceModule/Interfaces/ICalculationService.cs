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
        /// Chose combination
        /// </summary>
        /// <param name="stackPanel">Stack panel with selected combination</param>
        void ChoseOption(StackPanel stackPanel);
        /// <summary>
        /// Send data to arduino
        /// </summary>
        
        void SendDataToArduino();
    }
}
