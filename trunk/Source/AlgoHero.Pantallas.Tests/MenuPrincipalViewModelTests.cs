using AlgoHero.Pantallas.MenuPrincipal;
using AlgoHero.Pantallas.Tests.Mocks;
using NUnit.Framework;

namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class MenuPrincipalViewModelTests
    {
        [Test]
        public void CrearViewModelConProveedorCancionObtieneCantidadCancionesCorrecta()
        {
            MenuPrincipalViewModel vm = new MenuPrincipalViewModel(new MockProveedorCanciones(), new MockProveedorNiveles());
            Assert.AreEqual(vm.Canciones.Count, 3);
        }

        [Test]
        public void CrearViewModelAsignaNivelesDeJuego()
        {
            MenuPrincipalViewModel vm = new MenuPrincipalViewModel(new MockProveedorCanciones(), new MockProveedorNiveles());
            Assert.AreEqual(3, vm.Niveles.Count);
            
            Assert.AreEqual("Facil", vm.Niveles[0].Descripcion);
            
            Assert.AreEqual("Medio", vm.Niveles[1].Descripcion);
            
            Assert.AreEqual("Dificil", vm.Niveles[2].Descripcion);
        }
    }
}
