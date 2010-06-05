using System.Windows.Input;
using NUnit.Framework;

namespace AlgoHero.Files.Tests
{
    [TestFixture]
    public class ConvertidorStringAKeysTests
    {
        [Test]
        public void ConvertidorAStringsConvierteCorrectamente()
        {
            Assert.AreEqual(Key.A, ConvertidorStringAKeys.ConvertirAKey("a"));
            Assert.AreEqual(Key.B, ConvertidorStringAKeys.ConvertirAKey("b"));
            Assert.AreEqual(Key.K, ConvertidorStringAKeys.ConvertirAKey("k"));
            Assert.AreEqual(Key.Z, ConvertidorStringAKeys.ConvertirAKey("z"));
        }
    }
}
