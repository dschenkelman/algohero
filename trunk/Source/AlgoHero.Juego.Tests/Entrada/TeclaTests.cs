﻿using AlgoHero.Interface.Enums;
using AlgoHero.Juego.Entrada;
using NUnit.Framework;
using System;
using AlgoHero.Interface;

namespace AlgoHero.Juego.Tests.Entrada
{
    [TestFixture]
    public class TeclaTests
    {
        [Test]
        public void CrearTeclaSeteaKeyAsociada()
        {
            Tecla tecla = new Tecla(new EntidadEntrada(1));
            Assert.AreEqual(1, tecla.EntidadEntrada.Codigo);
        }

        [Test]
        public void AgregarTonoAsociadoAumentaCantidad()
        {
            var tecla = new Tecla(new EntidadEntrada(1));
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
            var tecla = new Tecla(new EntidadEntrada(1));
            tecla.AgregarTonoAsociado(Tono.Do);
            tecla.AgregarTonoAsociado(Tono.Do);
        }

        [Test]
        public void ResetearTodosLosTonosDeUnaTecla()
        {
            var tecla = new Tecla(new EntidadEntrada(1));
            tecla.AgregarTonoAsociado(Tono.Do);
            tecla.AgregarTonoAsociado(Tono.Re);
            tecla.ResetearTonosAsignados();
            Assert.IsEmpty(tecla.ObtenerTonosAsociados());
        }
    }
}