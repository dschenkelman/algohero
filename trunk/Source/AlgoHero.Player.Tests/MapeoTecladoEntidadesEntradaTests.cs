using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Windows.Input;

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

    }
}
