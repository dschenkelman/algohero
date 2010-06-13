using System;
using System.Windows.Controls;
using AlgoHero.Juego.Core;
using AlgoHero.MusicEntities.Core;
using NUnit.Framework;
using AlgoHero.Pantallas.PlayerCancion;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.Files.Interfaces;
using AlgoHero.Pantallas.Eventos;
namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class PlayerCancionViewModelTests
    {
        [Test]
        public void HandlearElEventoEmpezarCancionObtieneCancionConPartitura()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(new MockManejadorVentanaPrincipal(), proveedorCancion);

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo"){PathPartitura = "MiPath"}, null));

            Assert.AreEqual("Cancion Recuperada", vm.CancionActual.Nombre);
        }

        [Test]
        public void HandlearElEventoEmpezarCancionGuardaNivel()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(new MockManejadorVentanaPrincipal(), proveedorCancion);

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath" }, new Nivel("Nivel Test", null)));

            Assert.AreEqual("Nivel Test", vm.NivelActual.Descripcion);
        }

        private class MockProveedorCancionXml : IProveedorCancion
        {
            public Cancion ObtenerCancionSinPartitura(string path)
            {
                throw new NotImplementedException();
            }

            public Cancion ObtenerCancionConPartitura(string path)
            {
                if (path == "MiPath")
                {
                    return new Cancion("Cancion Recuperada", "Recuperada");
                }
                return null;
            }
        }

        private class MockManejadorVentanaPrincipal : IManejadorVentanaPrincipal
        {
            public void CambiarContenido(Control control)
            {
                throw new NotImplementedException();
            }
        }
    }
}
