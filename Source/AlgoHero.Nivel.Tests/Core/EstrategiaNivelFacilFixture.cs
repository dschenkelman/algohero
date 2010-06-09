using System;
using System.Collections.Generic;
using AlgoHero.Interface;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Player;

namespace AlgoHero.Nivel.Tests.Core
{
    using NUnit.Framework;
    using AlgoHero.Nivel.Core;
    using AlgoHero.Nivel.Excepciones;
    using AlgoHero.Interface.Enums;
    using System.Collections.ObjectModel;
    using AlgoHero.Nivel.Intefaces;

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
            Assert.IsNull(nivel.ObtenerSiguienteNota());
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
            Assert.IsNull(nivel.ObtenerSiguienteNota());
            Assert.IsNull(nivel.ObtenerSiguienteNota());
        }


        [Test]
        public void ObtenerIControladorTeclas()
        {
            this.partitura.AgregarCompas(this.compasCompletoConAcordes);
            this.cancion = new Cancion("We will rock you", "Queen");
            this.cancion.Partitura = this.partitura;

            IControladorTeclas control = new MockManagerTeclas();
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



        private class MockManagerTeclas : IControladorTeclas
        {
            private List<ITecla> Teclas;

            public MockManagerTeclas() 
            {
                this.Teclas = new List<ITecla>();
                this.Teclas.Add(new MockTecla(1));
                this.Teclas.Add(new MockTecla(2));
                this.Teclas.Add(new MockTecla(3));
                this.Teclas.Add(new MockTecla(4));
            }

            public int CantidadTeclas
            {
                get { return 4; }
            }

            public ITecla ObtenerTecla(int index)
            {
                return this.Teclas[index];
            }

            public IEnumerable<ITecla> ObtenerTeclas()
            {
                return this.Teclas;
            }

            public ITecla ObtenerTecla(EntidadEntrada entidadEntrada)
            {
                return this.Teclas[entidadEntrada.Codigo - 1];
            }
        }

        private class MockTecla : ITecla
        {
            private List<Tono> tonosAsociados;
            private int Codigo;

            public MockTecla(int codigo)
            {
                this.Codigo = codigo;
                this.EntidadEntrada = new EntidadEntrada(codigo);
                this.tonosAsociados = new List<Tono>();
                this.CantidadTonos = 0;
            }

            public void AgregarTonoAsociado(Tono tono) 
            {
                if (this.tonosAsociados.Contains(tono))
                {
                    throw new InvalidOperationException();
                }
                this.CantidadTonos += 1;
                this.tonosAsociados.Add(tono);
            }

            public Tono ObtenerTono(int index)
            {
                return this.tonosAsociados[index];
            }

            public ReadOnlyCollection<Tono> ObtenerTonosAsociados()
            {
                return this.tonosAsociados.AsReadOnly();
            }

            public int CantidadTonos { get; private set; }
            public EntidadEntrada EntidadEntrada { get; private set; }   
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
