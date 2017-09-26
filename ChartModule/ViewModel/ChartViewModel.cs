using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using ServiceModule.Interfaces;
using System.ComponentModel.Composition;
using System.Threading;
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
        private Thread thread;

        [ImportingConstructor]
        public ChartViewModel(ICalculationService calculationService)
        {
            this.calculationService = calculationService;
            SendDataCommand = new DelegateCommand<StackPanel>(sendDataAction);
            IsBusy = false;
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
            IsBusy = true;
            calculationService.ChoseOption(stackPanel);
            thread = new Thread(() => calculationService.SendDataToArduino());
            thread.Start();

            Thread busyThread = new Thread(() => keepBusyInducator());
            busyThread.Start();
        }
        #endregion
       
        #region Properties

        /// <summary>
        /// Busy property
        /// </summary>
        bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged(() => IsBusy);
            }
        }

        #endregion

        /// <summary>
        /// Change IsBusy from true to false when operations end
        /// </summary>
        private void keepBusyInducator()
        {
            while (thread.IsAlive) { }
            IsBusy = false;
        }
    }
}
