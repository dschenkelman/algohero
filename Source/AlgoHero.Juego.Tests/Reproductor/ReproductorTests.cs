using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AlgoHero.Juego.Reproductor;
using NUnit.Framework;


namespace AlgoHero.Juego.Tests.Reproductor
{
    [TestFixture]
    public class ReproductorTests
    {
        private ReproductorMusica reproductor;

        [SetUp]
        public void TestInitialize()
        {
            this.reproductor = new ReproductorMusica();
        }
        
        [Test]
        public void ReproductorMusicaReproduceCorrectamente()
        {
            Assert.AreEqual(false, this.reproductor.Reproduciendo);
            string pathCancion = Path.Combine(Environment.CurrentDirectory, Path.Combine("Archivos Prueba", "Happy.wav"));
            this.reproductor.ReproducirCancion(pathCancion);
            Assert.AreEqual(true, this.reproductor.Reproduciendo);
        }

        [Test]
        public void ReproductorMusicaDetieneReproduccion()
        {
            this.reproductor.DetenerReproduccion();
            Assert.AreEqual(false, this.reproductor.Reproduciendo);
        }
    }
}
