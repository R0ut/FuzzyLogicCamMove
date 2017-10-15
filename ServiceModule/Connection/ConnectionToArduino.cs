using System;
using System.IO.Ports;
using System.Windows;

namespace ServiceModule.Connection
{
    /// <summary>
    /// Configurate connection to arduino
    /// </summary>
    public class ConnectionToArduino
    {
        /// <summary>
        /// Property to Serial Port
        /// </summary>
        public SerialPort myPort { get; set; } 

        public ConnectionToArduino()
        {
            try
            {
                myPort = new SerialPort();
                myPort.BaudRate = 463611; //transmision speed

                myPort.PortName = SerialPort.GetPortNames()[SerialPort.GetPortNames().Length - 1];
                
                myPort.Parity = Parity.None;
                myPort.DataBits = 8;
                myPort.StopBits = StopBits.One;

                myPort.DtrEnable = true;
            }
            catch (Exception)
            {
            }
        }
    }
}
