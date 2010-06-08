using System;
using System.Collections.Generic;
using AlgoHero.MusicEntities.Core;
using AlgoHero.MusicEntities.Enums;
using NUnit.Framework;
using AlgoHero.Player.Interfaces;
using System.Windows.Input;
using AlgoHero.Interface;

namespace AlgoHero.Player.Tests
{
    [TestFixture]
    public class ControladorCancionTests
    {
        private Compas compasCompleto;
        private Partitura partitura;
        private Cancion cancion;
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
        public void ObtenerSiguienteNotaDevuelveNotaSiTonosEstanRegistradosConTecla()
        {
            
            this.partitura.AgregarCompas(this.compasCompleto);
            this.cancion = new Cancion("We will rock you", "Queen");
            this.cancion.Partitura = this.partitura;
            
            var controlador = new ControladorCancion(this.cancion, new MockManagerTeclas());
            Assert.IsFalse(controlador.EsFinalCancion);

            Assert.IsTrue(controlador.ObtenerSiguienteNota().Tonos.Contains(Tono.Fa));
            Assert.IsTrue(controlador.ObtenerSiguienteNota().Tonos.Contains(Tono.Fa));
        }

        [Test]
        public void ObtenerSiguienteNotaDevuelveNullSiTonoNoEstaRegistradoConTecla()
        {

            this.partitura.AgregarCompas(this.compasCompleto);
            this.cancion = new Cancion("We will rock you", "Queen");
            this.cancion.Partitura = this.partitura;

            var controlador = new ControladorCancion(this.cancion, new MockManagerTeclas());
            Assert.IsFalse(controlador.EsFinalCancion);

            controlador.ObtenerSiguienteNota().Tonos.Contains(Tono.Fa);
            controlador.ObtenerSiguienteNota().Tonos.Contains(Tono.Fa);
            Assert.IsNull(controlador.ObtenerSiguienteNota());
        }

        [Test]
        public void ObtenerNotaConMuchosTonosDevuelveNotaSiAlgunTonoEstaRegistrado()
        {
            this.partitura.AgregarCompas(this.compasCompletoConAcordes);
            this.cancion = new Cancion("We will rock you", "Queen");
            this.cancion.Partitura = this.partitura;

            var controlador = new ControladorCancion(this.cancion, new MockManagerTeclas());
            Assert.IsFalse(controlador.EsFinalCancion);

            Nota primerAcorde = controlador.ObtenerSiguienteNota();
            
            Assert.IsTrue(primerAcorde.Tonos.Contains(Tono.Fa));
            Assert.IsTrue(primerAcorde.Tonos.Contains(Tono.Do));
            Assert.IsNull(controlador.ObtenerSiguienteNota());
            Assert.IsNull(controlador.ObtenerSiguienteNota());
        }

        private class MockManagerTeclas : IManagerTeclas
        {
            public int CantidadTeclas
            {
                get { throw new NotImplementedException(); }
            }

            public Tecla ObtenerTecla(int index)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Tecla> ObtenerTeclas()
            {
                var tecla = new Tecla(new EntidadEntrada(1));
                tecla.AgregarTonoAsociado(Tono.Fa);
                return new List<Tecla>() {tecla};
            }
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
            var nota2 = new Nota(FiguraMusical.Negra, Tono.Do, Tono.Mi);
            var nota3 = new Nota(Tono.Si, FiguraMusical.Blanca);
            this.compasCompletoConAcordes.AgregarNota(nota);
            this.compasCompletoConAcordes.AgregarNota(nota2);
            this.compasCompletoConAcordes.AgregarNota(nota3);
        }
    }
}
