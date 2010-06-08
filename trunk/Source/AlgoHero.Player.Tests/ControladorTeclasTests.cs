using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AlgoHero.Interface;
using NUnit.Framework;

namespace AlgoHero.Player.Tests
{
    [TestFixture]
    public class ControladorTeclasTests
    {
        [Test]
        public void CrearControladorTeclasCreaEntidadesEntradaDeMapeo()
        {
            ControladorTeclas controlador = new ControladorTeclas(new MapeoTecladoMock());
            Assert.AreEqual(4, controlador.CantidadTeclas);

            for (int i = 1; i < 5; i++)
            {
                Assert.AreEqual(i ,controlador.ObtenerTecla(i).EntidadEntrada.Codigo);
            }
        }

        [Test]
        public void ObtenerTeclasDevuelveTodasLasTeclas()
        {
            ControladorTeclas controlador = new ControladorTeclas(new MapeoTecladoMock());
            Assert.AreEqual(4, controlador.CantidadTeclas);

            int cont = 1;
            foreach (Tecla tecla in controlador.ObtenerTeclas())
            {
                Assert.AreEqual(cont, tecla.EntidadEntrada.Codigo);
                cont++;
            }
        }

        private class MapeoTecladoMock : IMapeoTecladoEntidadesEntrada
        {
            public EntidadEntrada ObtenerEntidadEntrada(Key key)
            {
                throw new NotImplementedException();
            }

            public ReadOnlyCollection<EntidadEntrada> ObtenerEntidadesEntrada()
            {
                return new List<EntidadEntrada>()
                           {new EntidadEntrada(1), new EntidadEntrada(2), new EntidadEntrada(3), new EntidadEntrada(4)}.AsReadOnly();
            }
        }
    }
}
