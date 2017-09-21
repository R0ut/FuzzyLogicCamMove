using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using ServiceModule.Interfaces;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChartModule.ViewModel
{
    /// <summary>
    /// ViewModel to chart module
    /// </summary>
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
        /// <summary>
        /// Action that execute sending data to arduino
        /// </summary>
        /// <param name="stackPanel">stack panel with selected combination</param>
        private void sendDataAction(StackPanel stackPanel)
        {
            calculationService.SendDataToArduino(stackPanel);
        }
        #endregion
    }
}
