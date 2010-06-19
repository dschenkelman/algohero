using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace AlgoHero.PuntuacionJuego.Tests
{
    [TestFixture]
    public class PuntuacionTests
    {
        private Puntuacion puntuacionActual;

        [SetUp]
        public void TestInitialize()
        {
            puntuacionActual = new Puntuacion("medio");
        }

        [Test]
        public void AcertarNotaFuncionaCorrectamente()
        {
            puntuacionActual.AcertarNota();
            Assert.AreEqual(1, puntuacionActual.Multiplicador);
            Assert.AreEqual(1, puntuacionActual.RachaDeNotasAcertadas);
            Assert.AreEqual(1, puntuacionActual.PuntosAcumulados);
        }

        [Test]
        public void ErrarNotaFuncionaCorrectamente()
        {
            puntuacionActual.ErrarNota();
            Assert.AreEqual(1, puntuacionActual.Multiplicador);
            Assert.AreEqual(0, puntuacionActual.RachaDeNotasAcertadas);
            Assert.AreEqual(0, puntuacionActual.PuntosAcumulados);
        }

        [Test]
        public void ErrarNotaDespuesDeAcertarFuncionaCorrectamente()
        {
            for (int i = 0; i < 15; i++)
            {
                puntuacionActual.AcertarNota();
            }

            int puntosAcum = 15;

            puntuacionActual.ErrarNota();

            Assert.AreEqual(1, puntuacionActual.Multiplicador);
            Assert.AreEqual(0, puntuacionActual.RachaDeNotasAcertadas);
            Assert.AreEqual(puntosAcum, puntuacionActual.PuntosAcumulados);
        }

        [Test]
        public void AumentarMultiplicadorFuncionaCorrectamente()
        {
            Assert.AreEqual(1, puntuacionActual.Multiplicador);

            for (int i = 0; i < 9; i++)
            {
                puntuacionActual.AcertarNota();
            }
            Assert.AreEqual(1, puntuacionActual.Multiplicador);

            puntuacionActual.AcertarNota();

            Assert.AreEqual(2, puntuacionActual.Multiplicador);
        }
    }
}
