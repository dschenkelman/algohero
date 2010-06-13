using System.Threading;
using System.Windows.Controls;
using NUnit.Framework;
using AlgoHero.Pantallas.Interfaces;
namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class ManejadorVentanaPrincipalTests
    {
        private int idContenido;
        private int idControl;

        [Test]
        public void CambiarContenidoVentanaModificaPropiedadContenido()
        {
            Thread t = new Thread(this.CorrerTestReal);
            t.TrySetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            Assert.AreEqual(idControl, idContenido);
        }

        private void CorrerTestReal()
        {
            IVentanaPrincipal vista = new VentanaPrincipal();
            IManejadorVentanaPrincipal manejador = new ManejadorVentanaPrincipal(vista);

            Control c = new Control();

            idControl = c.GetHashCode();

            manejador.CambiarContenido(c);

            idContenido = vista.ObtenerContenido().GetHashCode();
        }
    }
}
