using System.Collections.ObjectModel;
using AlgoHero.MusicEntities.Core;
using NUnit.Framework;
using AlgoHero.MusicEntities.Enums;
using AlgoHero.MusicEntities.Excepciones;

namespace AlgoHeroMusic.Entities.Tests.Core
{
    [TestFixture]
    public class CompasFixture
    {
        private TiempoCancion tiempoCancion;
        private Compas compas;
        
        [SetUp]
        public void TestInitialize()
        {
            this.tiempoCancion = new TiempoCancion(4, 2);
            this.compas = new Compas(this.tiempoCancion);
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(ExcepcionCompasInvalido))]
        public void AgregarNotaACompasCompletoLanzaExcepcion()
        {
            var nota = new Nota(Tono.Fa, FiguraMusical.Redonda);
            this.compas.AgregarNota(nota);

            this.compas.AgregarNota(new Nota(Tono.Si, FiguraMusical.Semicorchea));
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(ExcepcionCompasInvalido))]
        public void AgregarBlancaACompasQueSoloFaltaNegraTiraExcepcion()
        {
            var nota = new Nota(Tono.Fa, FiguraMusical.Blanca);
            var nota2 = new Nota(Tono.Fa, FiguraMusical.Negra);
            this.compas.AgregarNota(nota);
            this.compas.AgregarNota(nota2);

            this.compas.AgregarNota(new Nota(Tono.Si, FiguraMusical.Blanca));
        }

        [Test]
        public void AgregarNotaACompasAumentaCantidadNotas()
        {
            var nota = new Nota(Tono.Mi, FiguraMusical.Semicorchea);
            this.compas.AgregarNota(nota);

            Assert.AreEqual(1, this.compas.CantidadNotas);
            Assert.AreEqual(nota, this.compas.ObtenerNota(0));

            var nota2 = new Nota(Tono.Sol, FiguraMusical.Corchea);
            this.compas.AgregarNota(nota2);
            
            Assert.AreEqual(2, this.compas.CantidadNotas);
            Assert.AreEqual(nota2, this.compas.ObtenerNota(1));
        }

        [Test]
        public void BorrarNotaACompasDisminuyeCantidadNotas()
        {
            var nota = new Nota(Tono.Mi, FiguraMusical.Semicorchea);
            this.compas.AgregarNota(nota);
            this.compas.BorrarNota(nota);

            Assert.AreEqual(0, this.compas.CantidadNotas);
        }

        [Test]
        public void ObtenerNotasDevuelveCollectionSoloLectura()
        {
            var nota = new Nota(Tono.Fa, FiguraMusical.Negra);
            var nota2 = new Nota(Tono.Fa, FiguraMusical.Negra);
            var nota3 = new Nota(Tono.Si, FiguraMusical.Corchea);
            this.compas.AgregarNota(nota);
            this.compas.AgregarNota(nota2);
            this.compas.AgregarNota(nota3);

            ReadOnlyCollection<Nota> notas = this.compas.ObtenerNotas();
            Assert.AreEqual(3, notas.Count);
            Assert.AreEqual(nota, notas[0]);
            Assert.AreEqual(nota2, notas[1]);
            Assert.AreEqual(nota3, notas[2]);
        }

    }
}
