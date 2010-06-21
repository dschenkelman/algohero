
using System.Collections.Generic;
using AlgoHero.Interface.Enums;
using AlgoHero.Juego.Servicios;
using AlgoHero.MusicEntities.Core;
using NUnit.Framework;

namespace AlgoHero.Juego.Tests.Servicios
{
    [TestFixture]
    public class OrganizadorDeTonosFixture
    {
        private Cancion cancion;
        private Partitura partitura;
        private Compas compasCompleto;
        
        [SetUp]
        public void TestInitialize()
        {
            var tiempoCancion = new TiempoCancion(4, 4);
            this.CrearCompasCompleto(tiempoCancion);
            this.cancion = new Cancion("Cancion", "Autor");
            this.partitura = new Partitura(tiempoCancion);
            this.partitura.AgregarCompas(this.compasCompleto);
            this.cancion.Partitura = this.partitura;
        }

        [Test]
        public void ContarApacionesDeCadaTonoDevuelveLista() 
        {
            var organizadorDeTonos = new OrganizadorDeTonos(this.cancion);
            List<OrganizadorDeTonos.TonoConCantidad> lista = organizadorDeTonos.ContarApacionesDeCadaTono();
            foreach (OrganizadorDeTonos.TonoConCantidad tonoConCant in lista)
            { 
                if (tonoConCant.Cantidad == 3)
                {
                    Assert.AreEqual(tonoConCant.Tono, Tono.Mi);
                }
                if (tonoConCant.Cantidad == 2)
                {
                    Assert.AreEqual(tonoConCant.Tono, Tono.Do);
                }
                if (tonoConCant.Cantidad == 1)
                {
                    Assert.AreEqual(tonoConCant.Tono, Tono.Si);
                }
            }
        }

        [Test]
        public void OrdenarNotasDevuelveListaOrdenada() 
        {
            var organizadorDeTonos = new OrganizadorDeTonos(this.cancion);
            var lista = organizadorDeTonos.ContarApacionesDeCadaTono();
            List<Tono> listaOrdenada = organizadorDeTonos.OrdenarNotas(lista);
            Assert.AreEqual(listaOrdenada[0], Tono.Mi);
            Assert.AreEqual(listaOrdenada[1], Tono.Do);
            Assert.AreEqual(listaOrdenada[2], Tono.Si);
        }

        private void CrearCompasCompleto(TiempoCancion tiempoCancion)
        {
            this.compasCompleto = new Compas(tiempoCancion);
            var nota = new Nota(FiguraMusical.Negra, Tono.Do, Tono.Mi);
            var nota2 = new Nota(FiguraMusical.Negra, Tono.Do, Tono.Mi);
            var nota3 = new Nota(FiguraMusical.Blanca, Tono.Si, Tono.Mi);
            this.compasCompleto.AgregarNota(nota);
            this.compasCompleto.AgregarNota(nota2);
            this.compasCompleto.AgregarNota(nota3);
        }

        
    }
}