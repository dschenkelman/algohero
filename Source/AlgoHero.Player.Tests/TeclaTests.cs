using System.Windows.Input;
using AlgoHero.MusicEntities.Enums;
using NUnit.Framework;
using System;

namespace AlgoHero.Player.Tests
{
    [TestFixture]
    public class TeclaTests
    {
        [Test]
        public void CrearTeclaSeteaKeyAsociada()
        {
            Tecla tecla = new Tecla(Key.A);
            Assert.AreEqual(Key.A, tecla.Key);
        }

        [Test]
        public void AgregarTonoAsociadoAumentaCantidad()
        {
            var tecla = new Tecla(Key.A);
            tecla.AgregarTonoAsociado(Tono.Do);

            Assert.AreEqual(1, tecla.CantidadTonos);
            Assert.AreEqual(Tono.Do, tecla.ObtenerTono(0));

            tecla.AgregarTonoAsociado(Tono.DoSostenido);
            Assert.AreEqual(2, tecla.CantidadTonos);
            Assert.AreEqual(Tono.DoSostenido, tecla.ObtenerTono(1));
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(InvalidOperationException))]
        public void AgregarTonoDosVecesLanzaExcepcion()
        {
            var tecla = new Tecla(Key.A);
            tecla.AgregarTonoAsociado(Tono.Do);
            tecla.AgregarTonoAsociado(Tono.Do);
        }
    }
}
