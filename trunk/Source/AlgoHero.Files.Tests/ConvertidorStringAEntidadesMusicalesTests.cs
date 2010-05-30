using AlgoHero.MusicEntities.Enums;
using NUnit.Framework;

namespace AlgoHero.Files.Tests
{
    [TestFixture]
    public class ConvertidorStringAEntidadesMusicalesTests
    {
        [Test]
        public void ConvertirCadenaTonoAEnumConvierteCorrectamente()
        {
            Assert.AreEqual(Tono.Do, ConvertidorStringAEntidadesMusicales.ConvertirATono("do"));
            Assert.AreEqual(Tono.Do, ConvertidorStringAEntidadesMusicales.ConvertirATono("Do"));
            Assert.AreEqual(Tono.DoSostenido, ConvertidorStringAEntidadesMusicales.ConvertirATono("Do#"));
            Assert.AreEqual(Tono.Re, ConvertidorStringAEntidadesMusicales.ConvertirATono("Re"));
            Assert.AreEqual(Tono.ReSostenido, ConvertidorStringAEntidadesMusicales.ConvertirATono("Re#"));
            Assert.AreEqual(Tono.Mi, ConvertidorStringAEntidadesMusicales.ConvertirATono("Mi"));
            Assert.AreEqual(Tono.Fa, ConvertidorStringAEntidadesMusicales.ConvertirATono("Fa"));
            Assert.AreEqual(Tono.FaSostenido, ConvertidorStringAEntidadesMusicales.ConvertirATono("Fa#"));
            Assert.AreEqual(Tono.Sol, ConvertidorStringAEntidadesMusicales.ConvertirATono("Sol"));
            Assert.AreEqual(Tono.La, ConvertidorStringAEntidadesMusicales.ConvertirATono("La"));
            Assert.AreEqual(Tono.LaSostenido, ConvertidorStringAEntidadesMusicales.ConvertirATono("La#"));
            Assert.AreEqual(Tono.Si, ConvertidorStringAEntidadesMusicales.ConvertirATono("Si"));
        }

        [Test]
        public void ConvertirCadenaFiguraAEnumConvierteCorrectamente()
        {
            Assert.AreEqual(FiguraMusical.Redonda, ConvertidorStringAEntidadesMusicales.ConvertirAFigura("Redonda"));
            Assert.AreEqual(FiguraMusical.Blanca, ConvertidorStringAEntidadesMusicales.ConvertirAFigura("blanca"));
            Assert.AreEqual(FiguraMusical.Blanca, ConvertidorStringAEntidadesMusicales.ConvertirAFigura("Blanca"));
            Assert.AreEqual(FiguraMusical.Negra, ConvertidorStringAEntidadesMusicales.ConvertirAFigura("Negra"));
            Assert.AreEqual(FiguraMusical.Corchea, ConvertidorStringAEntidadesMusicales.ConvertirAFigura("Corchea"));
            Assert.AreEqual(FiguraMusical.Semicorchea, ConvertidorStringAEntidadesMusicales.ConvertirAFigura("Semicorchea"));
        }
    }
}
