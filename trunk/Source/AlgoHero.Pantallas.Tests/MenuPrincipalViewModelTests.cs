using AlgoHero.MusicEntities.Core;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.Pantallas.MenuPrincipal;
using AlgoHero.Pantallas.Tests.Mocks;
using NUnit.Framework;
using AlgoHero.Juego.Core;

namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class MenuPrincipalViewModelTests
    {
        [Test]
        public void CrearViewModelConProveedorCancionObtieneCantidadCancionesCorrecta()
        {
            MenuPrincipalViewModel vm = new MenuPrincipalViewModel(new MockProveedorCanciones(), new MockProveedorNiveles(), null);
            Assert.AreEqual(vm.Canciones.Count, 3);
        }

        [Test]
        public void CrearViewModelAsignaNivelesDeJuego()
        {
            MenuPrincipalViewModel vm = new MenuPrincipalViewModel(new MockProveedorCanciones(), new MockProveedorNiveles(), null);
            Assert.AreEqual(3, vm.Niveles.Count);
            
            Assert.AreEqual("Facil", vm.Niveles[0].Descripcion);
            
            Assert.AreEqual("Medio", vm.Niveles[1].Descripcion);
            
            Assert.AreEqual("Dificil", vm.Niveles[2].Descripcion);
        }

        [Test]
        public void EmpezarCancionPublicaEventoConCancionYNivelEnArgs()
        {
            MenuPrincipalViewModel vm = new MenuPrincipalViewModel(new MockProveedorCanciones(), new MockProveedorNiveles(), null);
            var cancion = new Cancion("We will rock you", "Queen");
            
            vm.CancionActual = cancion;
            vm.NivelActual = vm.Niveles[0];

            Cancion cancionPublicada = null;
            Nivel nivelPublicado = null;

            vm.EmpezarCancionLlamado += delegate(object o, EmpezarCancionLlamadoEventArgs e)
                                            {
                                                cancionPublicada = e.Cancion;
                                                nivelPublicado = e.Nivel;
                                            };

            vm.EmpezarCancion(null);

            Assert.AreEqual(vm.CancionActual, cancionPublicada);
            Assert.AreEqual(vm.NivelActual, nivelPublicado);
        }
    }
}
