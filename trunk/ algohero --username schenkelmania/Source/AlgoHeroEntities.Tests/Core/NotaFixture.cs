
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
            Assert.AreEqual(nota.Figura, FiguraMusical.Negra);
        }
    }
}
