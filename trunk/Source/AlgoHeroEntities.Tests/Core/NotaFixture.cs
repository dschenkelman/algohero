using System.Collections.Generic;
using AlgoHero.Interface.Enums;

namespace AlgoHeroMusic.Entities.Tests.Core
{
    using NUnit.Framework;
    using AlgoHero.MusicEntities.Core;

    [TestFixture]
    public class NotaFixture
    {
        private List<Tono> tonos;
        private Nota acorde;

        [SetUp]
        public void TestInitialize()
        {
            this.tonos = new List<Tono>();
            this.tonos.Add(Tono.Do);
            this.tonos.Add(Tono.Mi);
            this.tonos.Add(Tono.Sol);
            // tonos es el acorde Do mayor
            this.acorde = new Nota(tonos, FiguraMusical.Negra);
        }

        [Test]
        public void CrearAcordeAsignarTonosYFigura()
        {
            Assert.AreEqual(FiguraMusical.Negra, this.acorde.Figura);
            int indice = 0;
            foreach (Tono tono in this.acorde.Tonos)
            {
                Assert.AreEqual(tono, this.tonos[indice]);
                indice += 1;
            }
            
        }

        [Test]
        public void CrearAcordeDeUnaNotaAsignarTonoYFigura()
        {
            var nota = new Nota(Tono.Si, FiguraMusical.Redonda);
            Assert.AreEqual(FiguraMusical.Redonda, nota.Figura);
            Assert.AreEqual(Tono.Si, nota.Tonos[0]);

        }

        [Test]
        public void CalcularDuracionNotaMusicalRedondaDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 4);
            var nota = new Nota(this.tonos, FiguraMusical.Redonda);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(4, segundos);
        }

        [Test]
        public void CalcularDuracionNotaMusicalBlancaDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 4);
            var nota = new Nota(this.tonos, FiguraMusical.Blanca);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(2, segundos);
        }

        [Test]
        public void CalcularDuracionNotaMusicalNegraDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 4);
            var nota = new Nota(this.tonos, FiguraMusical.Negra);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(1, segundos);
        }

        [Test]
        public void CalcularDuracionNotaMusicalCorcheaDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 4);
            var nota = new Nota(this.tonos, FiguraMusical.Corchea);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(0.5, segundos);
        }

        [Test]
        public void CalcularDuracionNotaMusicalSemicorcheaDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 4);
            var nota = new Nota(this.tonos, FiguraMusical.Semicorchea);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(0.25, segundos);
        }
    }
}
