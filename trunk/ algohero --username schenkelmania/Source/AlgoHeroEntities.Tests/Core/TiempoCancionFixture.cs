using AlgoHero.MusicEntities.Core;
using NUnit.Framework;
using System;

namespace AlgoHeroMusic.Entities.Tests.Core
{
    [TestFixture]
    public class TiempoCancionFixture
    {
        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        [Test]
        public void CrearTiempoCompasConDuracionCompasNegativoLanzaExcepcion()
        {
            var tiempoCancion = new TiempoCancion(-1, 1);
        }

        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        [Test]
        public void CrearTiempoCompasConDuracionCompasCeroLanzaExcepcion()
        {
            var tiempoCancion = new TiempoCancion(0, 1);
        }
        
        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        [Test]
        public void CrearTimpoCompasConCantidadBlancasNegativaLanzaExcepcion()
        {
            var tiempoCancion = new TiempoCancion(2, -1);
        }

        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        [Test]
        public void CrearTiempoCompasConCantidadBlancasCeroLanzaExcepcion()
        {
            var tiempoCancion = new TiempoCancion(3, 0);
        }

        [Test]
        public void CrearTiempoCompasSeteaValoresCorrectamente()
        {
            var tiempoCancion = new TiempoCancion(1.5, 2);
            Assert.AreEqual(tiempoCancion.DuracionCompas, 1.5);
            Assert.AreEqual(tiempoCancion.CantidadBlancas, 2);
        }

    }
}
