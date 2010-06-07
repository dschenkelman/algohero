using System;
using System.Collections.ObjectModel;
using System.IO;
using NUnit.Framework;
using System.Windows.Input;
using System.Linq;

namespace AlgoHero.Player.Tests
{
    [TestFixture]
    public class MapeoTecladoEntidadesEntradaTests
    {

        [Test]
        public void ObtenerEntidadMapeoFuncionaCorrectamente()
        {
            MapeoTecladoEntidadesEntrada map = new MapeoTecladoEntidadesEntrada();
            EntidadEntrada entEntradaUno = map.ObtenerEntidadEntrada(Key.A);
            EntidadEntrada entEntradaDos = map.ObtenerEntidadEntrada(Key.S);
            EntidadEntrada entEntradaTres = map.ObtenerEntidadEntrada(Key.K);
            EntidadEntrada entEntradaCuatro = map.ObtenerEntidadEntrada(Key.L);
            Assert.AreEqual(1, entEntradaUno.Codigo);
            Assert.AreEqual(2, entEntradaDos.Codigo);
            Assert.AreEqual(3, entEntradaTres.Codigo);
            Assert.AreEqual(4, entEntradaCuatro.Codigo);
        }

        [Test]
        public void ObtenerEntidadMapeoConKeyIncorrectaDevuelveNull()
        {
            MapeoTecladoEntidadesEntrada map = new MapeoTecladoEntidadesEntrada();
            Assert.AreEqual(null, map.ObtenerEntidadEntrada(Key.G));
        }

        [Test]
        public void ObtenerTodasLasEntidadesDevuelveValoresCorrectos()
        {
            var map = new MapeoTecladoEntidadesEntrada();

            ReadOnlyCollection<EntidadEntrada> entidades = map.ObtenerEntidadesEntrada();

            Assert.AreEqual(4, entidades.Count);
            Assert.IsTrue(entidades.Any(e => e.Codigo == 1));
            Assert.IsTrue(entidades.Any(e => e.Codigo == 2));
            Assert.IsTrue(entidades.Any(e => e.Codigo == 3));
            Assert.IsTrue(entidades.Any(e => e.Codigo == 4));
        }

        [Test]
        public void MapeoDesdeArchivoAsignaKeysCorrectamente()
        {
            const string pathArchivo = "C:\\Gonzalo\\UBA-UNLP\\Materias (UBA)\\Programacion III\\tp2\\Source\\AlgoHero.Files.Tests\\bin\\Debug\\Archivos Prueba\\Entradas.xml";
            var map = new MapeoTecladoEntidadesEntrada(pathArchivo);
            EntidadEntrada entEntradaUno = map.ObtenerEntidadEntrada(Key.A);
            EntidadEntrada entEntradaDos = map.ObtenerEntidadEntrada(Key.S);
            EntidadEntrada entEntradaTres = map.ObtenerEntidadEntrada(Key.D);
            EntidadEntrada entEntradaCuatro = map.ObtenerEntidadEntrada(Key.F);
            Assert.AreSame(entEntradaUno, entEntradaDos);
            Assert.AreEqual(1, entEntradaUno.Codigo);
            Assert.AreEqual(1, entEntradaDos.Codigo);
            Assert.AreEqual(2, entEntradaTres.Codigo);
            Assert.AreEqual(3, entEntradaCuatro.Codigo);
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(FileNotFoundException))]
        public void MapeoDesdeArchivoNoExistenteLanzaExcepcion()
        {
            var map = new MapeoTecladoEntidadesEntrada("Entrada.xml");
        }

    }
}
