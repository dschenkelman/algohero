using System.Collections.Generic;
using NUnit.Framework;
using System.Windows.Input;
using System.IO;
using System;

namespace AlgoHero.Files.Tests
{
    [TestFixture]
    public class ProveedorKeysXmlTests
    {
        private List<List<Key>> listaKeys;

        [SetUp]
        public void TestInitialize()
        {
            string pathArchivo = Path.Combine(Environment.CurrentDirectory,
            Path.Combine("Archivos Prueba", "Entradas.xml"));
            ProveedorKeysXml proveedor = new ProveedorKeysXml();
            this.listaKeys = proveedor.ObtenerListaDeKeys(pathArchivo);
        }

          [Test]
        public void ObtenerListaKeysObtieneCantidadCorrectaDeEntradas()
        {
            Assert.AreEqual(3, listaKeys.Count);
        }

        [Test]
        public void ObtenerListaKeysObtieneEntradasConCantidadCorrectaDeTeclas()
        {
            List<Key> listaUno = listaKeys[0];
            Assert.AreEqual(2, listaUno.Count);
            List<Key> listaDos = listaKeys[1];
            Assert.AreEqual(1, listaDos.Count);
            List<Key> listaTres = listaKeys[2];
            Assert.AreEqual(1, listaTres.Count);
        }

        [Test]
        public void ObtenerListaDeKeysObtieneEntradasConTeclasCorrectas()
        {
            List<Key> listaUno = listaKeys[0];
            Assert.AreEqual(Key.A, listaUno[0]);
            Assert.AreEqual(Key.S, listaUno[1]);
            List<Key> listaDos = listaKeys[1];
            Assert.AreEqual(Key.D, listaDos[0]);
            List<Key> listaTres = listaKeys[2];
            Assert.AreEqual(Key.F, listaTres[0]);
        }

    }
}
