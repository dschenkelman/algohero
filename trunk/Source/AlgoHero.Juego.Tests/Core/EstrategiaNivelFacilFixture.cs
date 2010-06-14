﻿using System.Collections.ObjectModel;
using AlgoHero.Interface;
using AlgoHero.Interface.Enums;
using AlgoHero.Juego.Core;
using AlgoHero.Juego.Excepciones;
using AlgoHero.Juego.Intefaces;
using AlgoHero.Juego.Tests.Core.Mocks;
using AlgoHero.MusicEntities.Core;
using NUnit.Framework;

namespace AlgoHero.Juego.Tests.Core
{
    [TestFixture]
    public class EstrategiaNivelFacilFixture
    {
        private Cancion cancion;
        private Partitura partitura;
        private Compas compasCompleto;
        private Compas compasCompletoConAcordes;

        [SetUp]
        public void TestInitialize()
        {
            var tiempoCancion = new TiempoCancion(4, 2);
            this.CrearCompasCompleto(tiempoCancion);
            this.CrearCompasCompletoConAcordes(tiempoCancion);
            this.partitura = new Partitura(tiempoCancion);
        }

        [Test]
        public void ObtenerSiguienteNotaDevuelveNota()
        {   
            this.partitura.AgregarCompas(this.compasCompleto);
            this.cancion = new Cancion("We will rock you", "Queen");
            this.cancion.Partitura = this.partitura;
            
            var nivel = new EstrategiaNivelFacil(this.cancion);
            Assert.IsFalse(nivel.EsFinalCancion());
            Assert.IsTrue(nivel.ObtenerSiguienteNota().Tonos.Contains(Tono.Fa));
            Assert.IsFalse(nivel.EsFinalCancion());
        }

        [Test]
        public void ObtenerSiguienteNotaDevuelveNull()
        {
            this.partitura.AgregarCompas(this.compasCompleto);
            this.cancion = new Cancion("We will rock you", "Queen");
            this.cancion.Partitura = this.partitura;

            var nivel = new EstrategiaNivelFacil(this.cancion);
            Assert.IsFalse(nivel.EsFinalCancion());

            Assert.IsTrue(nivel.ObtenerSiguienteNota().Tonos.Contains(Tono.Fa));
            Assert.IsTrue(nivel.ObtenerSiguienteNota().Tonos.Contains(Tono.Silencio));
        }

        [Test]
        public void ObtenerNotaConMuchosTonosDevuelveNotaYLuegoNull()
        {
            this.partitura.AgregarCompas(this.compasCompletoConAcordes);
            this.cancion = new Cancion("We will rock you", "Queen");
            this.cancion.Partitura = this.partitura;

            var nivel = new EstrategiaNivelFacil(this.cancion);
            Assert.IsFalse(nivel.EsFinalCancion());

            Nota primerAcorde = nivel.ObtenerSiguienteNota();

            Assert.IsTrue(primerAcorde.Tonos.Contains(Tono.Fa));
            Assert.IsTrue(primerAcorde.Tonos.Contains(Tono.Do));
            Assert.IsTrue(nivel.ObtenerSiguienteNota().Tonos.Contains(Tono.Silencio));
            Assert.IsTrue(nivel.ObtenerSiguienteNota().Tonos.Contains(Tono.Silencio));
        }


        [Test]
        public void ObtenerIControladorTeclas()
        {
            this.partitura.AgregarCompas(this.compasCompletoConAcordes);
            this.cancion = new Cancion("We will rock you", "Queen");
            this.cancion.Partitura = this.partitura;

            IControladorTeclas control = new MockControladorTeclas();
            IEstrategiaNivel nivel = new EstrategiaNivelFacil(this.cancion);

            nivel.AsignarTonos(control);
            ITecla teclaUno = control.ObtenerTecla(0);
            ITecla teclaDos = control.ObtenerTecla(1);
            ReadOnlyCollection<Tono> listaUno = teclaUno.ObtenerTonosAsociados();
            ReadOnlyCollection<Tono> listaDos = teclaDos.ObtenerTonosAsociados();
            Assert.IsTrue(listaUno.Contains(Tono.Do));
            Assert.IsTrue(listaUno.Contains(Tono.Si));
            Assert.IsTrue(listaDos.Contains(Tono.Fa));
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(ExcepcionFinalDeCancion))]
        public void EstrategiaNivelFacilLanzaExcepcionFinalDeCancion()
        {
            this.partitura.AgregarCompas(this.compasCompletoConAcordes);
            this.cancion = new Cancion("We will rock you", "Queen");
            this.cancion.Partitura = this.partitura;

            var nivel = new EstrategiaNivelFacil(this.cancion);
            nivel.ObtenerSiguienteNota();
            nivel.ObtenerSiguienteNota();
            nivel.ObtenerSiguienteNota();
            nivel.ObtenerSiguienteNota();
        }

        private void CrearCompasCompleto(TiempoCancion tiempoCancion)
        {
            this.compasCompleto = new Compas(tiempoCancion);
            var nota = new Nota(Tono.Fa, FiguraMusical.Negra);
            var nota2 = new Nota(Tono.Fa, FiguraMusical.Negra);
            var nota3 = new Nota(Tono.Si, FiguraMusical.Blanca);
            this.compasCompleto.AgregarNota(nota);
            this.compasCompleto.AgregarNota(nota2);
            this.compasCompleto.AgregarNota(nota3);
        }

        private void CrearCompasCompletoConAcordes(TiempoCancion tiempoCancion)
        {
            this.compasCompletoConAcordes = new Compas(tiempoCancion);
            var nota = new Nota(FiguraMusical.Negra, Tono.Do, Tono.Fa);
            var nota2 = new Nota(FiguraMusical.Negra, Tono.Do, Tono.Fa);
            var nota3 = new Nota(FiguraMusical.Blanca, Tono.Si, Tono.Do);
            this.compasCompletoConAcordes.AgregarNota(nota);
            this.compasCompletoConAcordes.AgregarNota(nota2);
            this.compasCompletoConAcordes.AgregarNota(nota3);
        }

    }
}