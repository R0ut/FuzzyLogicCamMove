using System;
using System.IO.Ports;
using System.Windows;

namespace WPF_INZ.Connection
{
    class ConnectionToArduino
    {
        public SerialPort myPort { get; set; } //property to Serial Port

        public ConnectionToArduino()
        {
            try
            {
                myPort = new SerialPort();
                myPort.BaudRate = 463611; //transmision speed
                myPort.PortName = SerialPort.GetPortNames()[0];   

                myPort.Parity = Parity.None;
                myPort.DataBits = 8;
                myPort.StopBits = StopBits.One;

                myPort.DtrEnable = true;
                // myPort.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Connection problem");
            }
        }
    }
}
