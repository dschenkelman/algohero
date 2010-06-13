using NUnit.Framework;
using AlgoHero.MusicEntities.Core;
using System;

namespace AlgoHeroMusic.Entities.Tests.Core
{
    [TestFixture]
    public class CancionFixture
    {
        [Test]
        public void CrearCancionConNombreYAutorSeteaValoresCorrectos()
        {
            var cancion = new Cancion("Vitaminas", "Soda Stereo");
            Assert.AreEqual("Vitaminas", cancion.Nombre);
            Assert.AreEqual("Soda Stereo", cancion.Autor);
        }

    }
}
