using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using ServiceModule.Interfaces;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChartModule.ViewModel
{
    [Export]
    public class ChartViewModel : BindableBase
    {
        private ICalculationService calculationService;
        
        [ImportingConstructor]
        public ChartViewModel(ICalculationService calculationService)
        {
            this.calculationService = calculationService;
            SendDataCommand = new DelegateCommand<StackPanel>(sendDataAction);
        }

        
       
        #region Commands
        public ICommand SendDataCommand { get; set; }
        #endregion

        #region Actions
        private void sendDataAction(StackPanel stackPanel)
        {
            calculationService.SendDataToArduino(stackPanel);
        }
        #endregion
    }
}
