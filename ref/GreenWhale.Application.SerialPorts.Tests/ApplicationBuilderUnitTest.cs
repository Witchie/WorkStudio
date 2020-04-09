using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using GreenWhale.Extensions.DependencyInjection;
namespace GreenWhale.Application.SerialPorts.Tests
{
    [TestClass]
    public class ApplicationBuilderUnitTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var builder = new Application.SerialPorts.ApplicationBuilder(null);
            builder.Use(async (context,next)=> 
            {
                await next?.Invoke();
            });
            var app=  builder.Build();
            await app.Invoke(DerfaultSerialPort());
        }
        private SerialPortContext DerfaultSerialPort()
        {
            var sp = new System.IO.Ports.SerialPort("COM2");
            sp.Open();
            return new SerialPortContext(sp);
        }
    }
}
