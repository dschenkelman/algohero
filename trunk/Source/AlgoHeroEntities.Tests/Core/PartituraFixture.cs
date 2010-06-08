using AlgoHero.Interface;
using AlgoHero.Interface.Enums;
using AlgoHero.MusicEntities.Core;

namespace AlgoHeroMusic.Entities.Tests.Core
{
    using NUnit.Framework;
    using AlgoHero.MusicEntities.Excepciones;
    using System;

    [TestFixture]
    public class PartituraFixture
    {
        private Compas compasCompleto;
        private Compas compasCompleto2;
        private Compas compasIncompleto;
        private Partitura partitura;
        
        [SetUp]
        public void TestInitialize()
        {
            var tiempoCancion = new TiempoCancion(4, 2);
            this.CrearCompasCompleto(tiempoCancion);
            this.CrearOtroCompasCompleto(tiempoCancion);
            this.CrearCompasIncompleto(tiempoCancion);
            this.partitura = new Partitura(tiempoCancion);
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(ExcepcionCompasInvalido))]
        public void AgregarCompasAPartituraNoAgregaCompasIncompleto()
        {
            this.partitura.AgregarCompas(this.compasIncompleto);
        }

        [Test]
        public void AgregarCompasAPartituraAgregaCompasCompleto()
        {
            this.partitura.AgregarCompas(this.compasCompleto);

            Assert.AreEqual(1, this.partitura.CantidadCompases);
            Assert.AreEqual(this.compasCompleto, this.partitura.ObtenerCompas(0));
        }

        [Test]
        public void IteradorPartituraDevuelveNotasDeUnCompas()
        {
            this.partitura.AgregarCompas(this.compasCompleto);
            IIterador<Nota> iterador = this.partitura.ObtenerIterador();
            Nota nota;
            
            nota = iterador.Siguiente();
            Assert.AreEqual(1, nota.Tonos.Count);
            Assert.IsTrue(nota.Tonos.Contains(Tono.Fa));
            Assert.AreEqual(FiguraMusical.Negra, nota.Figura);

            nota = iterador.Siguiente();
            Assert.AreEqual(1, nota.Tonos.Count);
            Assert.IsTrue(nota.Tonos.Contains(Tono.Fa));
            Assert.AreEqual(FiguraMusical.Negra, nota.Figura);

            nota = iterador.Siguiente();
            Assert.AreEqual(1, nota.Tonos.Count);
            Assert.IsTrue(nota.Tonos.Contains(Tono.Si));
            Assert.AreEqual(FiguraMusical.Blanca, nota.Figura);
            
            Assert.IsFalse(iterador.TieneSiguiente);
        }

        [Test]
        public void IteradorPartituraCambiaDeCompasYDevuelveNotas()
        {
            this.partitura.AgregarCompas(this.compasCompleto);
            this.partitura.AgregarCompas(this.compasCompleto2);
            IIterador<Nota> iterador = this.partitura.ObtenerIterador();
            Nota nota;

            Assert.IsTrue(iterador.TieneSiguiente);
            iterador.Siguiente();
            Assert.IsTrue(iterador.TieneSiguiente);
            iterador.Siguiente();
            Assert.IsTrue(iterador.TieneSiguiente);
            iterador.Siguiente();
            Assert.IsTrue(iterador.TieneSiguiente);
            //voy hasta el cambio de compas

            nota = iterador.Siguiente();
            Assert.IsTrue(iterador.TieneSiguiente);
            Assert.AreEqual(1, nota.Tonos.Count);
            Assert.IsTrue(nota.Tonos.Contains(Tono.Do));
            Assert.AreEqual(FiguraMusical.Negra, nota.Figura);

            nota = iterador.Siguiente();
            Assert.IsTrue(iterador.TieneSiguiente);
            Assert.AreEqual(1, nota.Tonos.Count);
            Assert.IsTrue(nota.Tonos.Contains(Tono.Re));
            Assert.AreEqual(FiguraMusical.Negra, nota.Figura);

            nota = iterador.Siguiente();
            Assert.AreEqual(1, nota.Tonos.Count);
            Assert.IsTrue(nota.Tonos.Contains(Tono.Sol));
            Assert.AreEqual(FiguraMusical.Blanca, nota.Figura);
            
            Assert.IsFalse(iterador.TieneSiguiente);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ObtenerSiguienteEnIteradorQueNoTieneLanzaExcepcion()
        {
            IIterador<Nota> iterador = this.partitura.ObtenerIterador();
            iterador.Siguiente();
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

        private void CrearOtroCompasCompleto(TiempoCancion tiempoCancion)
        {
            this.compasCompleto2 = new Compas(tiempoCancion);
            var nota = new Nota(Tono.Do, FiguraMusical.Negra);
            var nota2 = new Nota(Tono.Re, FiguraMusical.Negra);
            var nota3 = new Nota(Tono.Sol, FiguraMusical.Blanca);
            this.compasCompleto2.AgregarNota(nota);
            this.compasCompleto2.AgregarNota(nota2);
            this.compasCompleto2.AgregarNota(nota3);
        }

        private void CrearCompasIncompleto(TiempoCancion tiempoCancion)
        {
            this.compasIncompleto = new Compas(tiempoCancion);
            var nota = new Nota(Tono.Fa, FiguraMusical.Negra);
            var nota2 = new Nota(Tono.Fa, FiguraMusical.Negra);
            var nota3 = new Nota(Tono.Si, FiguraMusical.Negra);
            this.compasIncompleto.AgregarNota(nota);
            this.compasIncompleto.AgregarNota(nota2);
            this.compasIncompleto.AgregarNota(nota3);
        }
    }
}
