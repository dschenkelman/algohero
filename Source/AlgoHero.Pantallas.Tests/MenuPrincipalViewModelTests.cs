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
            MenuPrincipalViewModel vm = new MenuPrincipalViewModel(new MockProveedorCancionXml());
            Assert.AreEqual(vm.Canciones.Count, 3);
        }
    }
}
