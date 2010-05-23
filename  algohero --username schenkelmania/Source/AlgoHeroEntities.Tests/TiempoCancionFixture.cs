namespace AlgoHero.Entities.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class TiempoCancionFixture
    {
        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        [Test]
        public void CrearTiempoCompasConDuracionCompasNegativoLanzaExcepcion()
        {
            TiempoCancion tCancion = new TiempoCancion(-1, 1);
        }

        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        [Test]
        public void CrearTiempoCompasConDuracionCompasCeroLanzaExcepcion()
        {
            TiempoCancion tCancion = new TiempoCancion(0, 1);
        }
        
        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        [Test]
        public void CrearTimpoCompasConCantidadBlancasNegativaLanzaExcepcion()
        {
            TiempoCancion tCancion = new TiempoCancion(2, -1);
        }

        [ExpectedException(ExceptionType = typeof(ArgumentException))]
        [Test]
        public void CrearTiempoCompasConCantidadBlancasCeroLanzaExcepcion()
        {
            TiempoCancion tCancion = new TiempoCancion(3, 0);
        }

    }
}
