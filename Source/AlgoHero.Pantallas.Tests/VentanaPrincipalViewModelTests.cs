using System.Threading;
using System.Windows.Controls;
using NUnit.Framework;

namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class VentanaPrincipalViewModelTests
    {
        private int idObtenido = 0;
        private int idEsperado = 1;

        /* Al corrector: Es bien feo el formato de este test. El problema es que necesito que cree un control y eso solo se puede 
         * hacer en un STA thread. Como NUnit corre en MTA (no como MSTest), es la forma posible de crear el objeto 
         * y ver que obtener el Id.
         * De todas formas, si se les ocurre alguna forma mejor estaria bueno escucharlo, y sino aunque sea
         * usamos lo que aprendimos de threading.
         */
        [Test]
        public void CrearViewModelConControlSeteaContenido()
        {
            Thread t = new Thread(this.CorrerTestReal);
            t.TrySetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            Assert.AreEqual(idEsperado, idObtenido);
        }

        private void CorrerTestReal()
        {
            Control control = new Control();
            idEsperado = control.GetHashCode();

            idObtenido =  new VentanaPrincipalViewModel(control).Contenido.GetHashCode();
        }

    }
}
