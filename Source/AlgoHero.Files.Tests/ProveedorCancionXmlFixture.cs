﻿using NUnit.Framework;
using AlgoHero.Files.Interfaces;
using AlgoHero.MusicEntities.Core;
using System.IO;
using System;
using AlgoHero.MusicEntities.Enums;

namespace AlgoHero.Files.Tests
{
    /// <summary>
    /// Summary description for ProveedorCancionXmlFixture
    /// </summary>
    [TestFixture]
    public class ProveedorCancionXmlFixture
    {
        private Cancion cancion;

        [SetUp]
        public void TestInitialize()
        {
            string pathCancion = Path.Combine(Environment.CurrentDirectory,
            Path.Combine("Archivos Prueba", "WeWillRockYou.xml"));
            IProveedorCancion proveedor = new ProveedorCancionXml();
            this.cancion = proveedor.ObtenerCancion(pathCancion);
        }

        [Test]
        public void ObtenerCancionDeArchivoDevuelveCancionConNombreYAutorCorrectos() 
        {
            Assert.AreEqual("We will rock you", cancion.Nombre);
            Assert.AreEqual("Queen", cancion.Autor);
        }

        [Test]
        public void ObtenerCancionDeArchivoDevuelveCancionConTiempoCorrecto()
        {
            Assert.AreEqual(4, cancion.Partitura.DuracionCompas);
            Assert.AreEqual(2, cancion.Partitura.CantidadBlancas);
        }

        [Test]
        public void ObtenerCancionDeArchivoDevuelveCancionConCantidadDeCompasesCorrecta()
        {
            Assert.AreEqual(2, cancion.Partitura.CantidadCompases);
        }

        [Test]
        public void ObtenerCancionDeArchivoDevuelveCompasesConNotasCorrectas()
        {
            Nota nota;
            //Primer nota primer compas
            nota = cancion.Partitura.ObtenerCompas(0).ObtenerNota(0);
            Assert.AreEqual(Tono.Do, nota.Tono);
            Assert.AreEqual(FiguraMusical.Blanca, nota.Figura);

            //Segunda nota primer compas
            nota = cancion.Partitura.ObtenerCompas(0).ObtenerNota(1);
            Assert.AreEqual(Tono.Re, nota.Tono);
            Assert.AreEqual(FiguraMusical.Negra, nota.Figura);

            //Tercer nota primer compas
            nota = cancion.Partitura.ObtenerCompas(0).ObtenerNota(2);
            Assert.AreEqual(Tono.La, nota.Tono);
            Assert.AreEqual(FiguraMusical.Negra, nota.Figura);

            //Primeras cuatro notas segundo compas
            for (int i = 0; i < 4; i++)
            {
                nota = cancion.Partitura.ObtenerCompas(1).ObtenerNota(i);
                Assert.AreEqual(Tono.Mi, nota.Tono);
                Assert.AreEqual(FiguraMusical.Corchea, nota.Figura);
            }

            nota = cancion.Partitura.ObtenerCompas(1).ObtenerNota(4);
            Assert.AreEqual(Tono.Do, nota.Tono);
            Assert.AreEqual(FiguraMusical.Blanca, nota.Figura);
        }

    }
}
