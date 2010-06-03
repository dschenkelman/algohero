using System;
using NUnit.Framework;


namespace AlgoHero.Player.Tests
{
    [TestFixture]
    public class EntidadEntradaTests
    {
        [Test]
        public void CrearEntidadEntradaCreaCorrectamente()
        {
            int codigo = 1;
            EntidadEntrada entEntrada = new EntidadEntrada(codigo);
            Assert.AreEqual(1, entEntrada.Codigo);
        }
    }
}   
