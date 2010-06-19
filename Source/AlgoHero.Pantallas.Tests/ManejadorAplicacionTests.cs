using System.ComponentModel;
using System.Threading;
using NUnit.Framework;

namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class ManejadorAplicacionTests
    {
        private bool aplicacionCorriendo;

        [Test]
        public void IniciarAplicacionMarcaAplicacionComoCorriendo()
        {
            Thread t = new Thread(this.IniciarAplicacionMarcaAplicacionComoCorriendoReal);
            t.TrySetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            Assert.AreEqual(true, aplicacionCorriendo);
        }

        private void IniciarAplicacionMarcaAplicacionComoCorriendoReal()
        {
            var manejador = new ManejadorAplicacion();
            manejador.Iniciar();

            aplicacionCorriendo = manejador.AplicacionCorriendo;
        }

        [Test]
        public void IniciarAplicacionYAvisoCerradoVentanaPrincipalMarcaAplicacionComoNoCorriendo()
        {
            Thread t = new Thread(this.IniciarAplicacionYAvisoCerradoVentanaPrincipalMarcaAplicacionComoNoCorriendoReal);
            t.TrySetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            Assert.AreEqual(false, aplicacionCorriendo);
        }

        private void IniciarAplicacionYAvisoCerradoVentanaPrincipalMarcaAplicacionComoNoCorriendoReal()
        {
            var manejador = new ManejadorAplicacion();
            manejador.Iniciar();
            manejador.CerrandoVentanaPrincipal(null, new CancelEventArgs());

            aplicacionCorriendo = manejador.AplicacionCorriendo;
        }
    }
}
