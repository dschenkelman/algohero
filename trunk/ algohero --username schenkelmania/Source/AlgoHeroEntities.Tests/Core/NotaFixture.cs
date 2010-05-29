
namespace AlgoHeroMusic.Entities.Tests.Core
{       
    using NUnit.Framework;
    using AlgoHero.MusicEntities.Core;
    using AlgoHero.MusicEntities.Enums;

    [TestFixture]
    public class NotaFixture
    {
        [Test]
        public void CrearNotaMusicalAsignaTonoYFigura()
        {
            var nota = new Nota(Tono.Do, FiguraMusical.Negra);
            Assert.AreEqual(nota.Tono, Tono.Do);
            Assert.AreEqual(FiguraMusical.Negra, nota.Figura);
        }

        [Test]
        public void CalcularDuracionNotaMusicalRedondaDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 2);
            var nota = new Nota(Tono.Do, FiguraMusical.Redonda);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(4,segundos);
        }

        [Test]
        public void CalcularDuracionNotaMusicalBlancaDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 2);
            var nota = new Nota(Tono.Do, FiguraMusical.Blanca);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(2, segundos);
        }

        [Test]
        public void CalcularDuracionNotaMusicalNegraDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 2);
            var nota = new Nota(Tono.Do, FiguraMusical.Negra);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(1, segundos);
        }

        [Test]
        public void CalcularDuracionNotaMusicalCorcheaDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 2);
            var nota = new Nota(Tono.Do, FiguraMusical.Corchea);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(0.5, segundos);
        }

        [Test]
        public void CalcularDuracionNotaMusicalSemicorcheaDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 2);
            var nota = new Nota(Tono.Do, FiguraMusical.Semicorchea);
            double segundos = nota.CalcularTiempoProximaNota(tiempoCancion);
            Assert.AreEqual(0.25, segundos);
        }
    }
}
