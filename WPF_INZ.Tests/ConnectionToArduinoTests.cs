using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceModule.Connection;

namespace WPF_INZ.Tests
{
    [TestClass]
    public class ConnectionToArduinoTests
    {
        [TestMethod]
        public void ConnectionToArduinoShouldInit()
        {
            ConnectionToArduino con = new ConnectionToArduino();
            Assert.IsNotNull(con.myPort);
        }
    }
}
