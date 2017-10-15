using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using OxyPlot;
using ServiceModule.Interfaces;
using System;
using System.Collections.ObjectModel;
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
        private Thread cameraMoveThread;
        private Thread calculationThread;
        private int[] delayArray;

        [ImportingConstructor]
        public ChartViewModel(ICalculationService calculationService)
        {
            this.calculationService = calculationService;
            SendDataCommand = new DelegateCommand<StackPanel>(sendDataAction);
            CalculateDelayCommand = new DelegateCommand<StackPanel>(calculateDelay);
            IsBusy = false;
            Delay = new ObservableCollection<DataPoint>();
        }

        #region Commands
        public ICommand SendDataCommand { get; set; }
        public ICommand CalculateDelayCommand { get; set; }
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
            cameraMoveThread = new Thread(() => calculationService.SendDataToArduino());
            cameraMoveThread.Start();

            Thread busyThread = new Thread(() => keepBusyInducator());
            busyThread.Start();
        }

        /// <summary>
        /// Action that execute calculating delays
        /// </summary>
        /// <param name="stackPanel">stack panel with selected combination</param>
        private void calculateDelay(StackPanel stackPanel)
        {
            calculationService.ChoseOption(stackPanel);
            delayArray = Array.ConvertAll(calculationService.CalculateDelay(), int.Parse);
            Delay.Clear();
            int i = 0;
            foreach (var item in delayArray)
            {
                i++;
                Delay.Add(new DataPoint(i, item));
            }
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

        /// <summary>
        /// Array of delays
        /// </summary>
        ObservableCollection<DataPoint> delay;
        public ObservableCollection<DataPoint> Delay
        {
            get { return delay; }
            set
            {
                delay = value;
                OnPropertyChanged(() => Delay);
            }
        }

        #endregion

        /// <summary>
        /// Change IsBusy from true to false when operations end
        /// </summary>
        private void keepBusyInducator()
        {
            while (cameraMoveThread.IsAlive) { }
            IsBusy = false;
        }
    }
}
