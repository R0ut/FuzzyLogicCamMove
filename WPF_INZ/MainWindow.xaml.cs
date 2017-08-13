using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Documents;
using WPF_INZ.Calculations;
using WPF_INZ.Connection;
using WPF_INZ.Fuzzy;

namespace WPF_INZ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculation calculation;
        public MainWindow()
        {
            InitializeComponent();
            calculation = new Calculation(this.RadioButtonGroup);
        }

        private void SendData(object sender, RoutedEventArgs e)
        {
            calculation.SendData();
        }
    }
}
