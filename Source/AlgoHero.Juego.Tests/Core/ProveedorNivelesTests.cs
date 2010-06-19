using NUnit.Framework;
using AlgoHero.Juego.Core;
using System.Collections.ObjectModel;

namespace AlgoHero.Juego.Tests.Core
{
    [TestFixture]
    public class ProveedorNivelesTests
    {
        [Test]
        public void CrearProveedorNivelesCreaTresNiveles()
        {
            var proveedorNiveles = new ProveedorNiveles();
            ObservableCollection<Nivel> niveles =  proveedorNiveles.ObtenerNiveles();

            Assert.AreEqual("Facil", niveles[0].Descripcion);
            Assert.AreEqual("Medio", niveles[1].Descripcion);
            Assert.AreEqual("Dificil", niveles[2].Descripcion);
        }
    }
}
