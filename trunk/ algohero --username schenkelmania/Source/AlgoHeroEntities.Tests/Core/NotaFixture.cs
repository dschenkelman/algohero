
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
            double segundos = nota.CalcularDuracion(tiempoCancion);
            Assert.AreEqual(4,segundos);
        }

        [Test]
        public void CalcularDuracionNotaMusicalBlancaDevuelveValorCorrecto()
        {
            var tiempoCancion = new TiempoCancion(4, 2);
            var nota = new Nota(Tono.Do, FiguraMusical.Blanca);
            double segundos = nota.CalcularDuracion(tiempoCancion);
            Assert.AreEqual(2, segundos);
        }
    }
}
