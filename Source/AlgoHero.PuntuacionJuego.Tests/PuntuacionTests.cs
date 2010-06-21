﻿using AlgoHero.Juego.Core;
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
            EstrategiaNivelMedio estrategiaNivel = new EstrategiaNivelMedio();
            Nivel nivelJuego = new Nivel("medio", estrategiaNivel);
            puntuacionActual = new Puntuacion(nivelJuego);
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

            int puntosAcum = 20; // 10 con multiplier 1 y 5 con multiplier 2
            //suman 20, porq yo habia cambiado algo

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