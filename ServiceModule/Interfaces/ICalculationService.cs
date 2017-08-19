using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ServiceModule.Interfaces
{
    public interface ICalculationService
    {
        void Calculate();
        void SendDataToArduino(StackPanel stackPanel);
    }
}
