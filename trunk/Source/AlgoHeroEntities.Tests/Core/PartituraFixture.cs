using AlgoHero.MusicEntities.Core;
using AlgoHero.MusicEntities.Enums;

namespace AlgoHeroMusic.Entities.Tests.Core
{
    using NUnit.Framework;
    using AlgoHero.MusicEntities.Excepciones;

    [TestFixture]
    public class PartituraFixture
    {
        private Compas compasCompleto;
        private Compas compasIncompleto;
        private Partitura partitura;
        
        [SetUp]
        public void TestInitialize()
        {
            var tiempoCancion = new TiempoCancion(4, 2);
            this.CrearCompasCompleto(tiempoCancion);
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
