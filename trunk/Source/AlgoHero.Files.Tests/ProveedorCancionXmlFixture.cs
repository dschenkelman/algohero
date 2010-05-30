using NUnit.Framework;
using AlgoHero.Files.Interfaces;
using AlgoHero.MusicEntities.Core;
using System.IO;
using System;

namespace AlgoHero.Files.Tests
{
    /// <summary>
    /// Summary description for ProveedorCancionXmlFixture
    /// </summary>
    [TestFixture]
    public class ProveedorCancionXmlFixture
    {
        [Test]
        public void ObtenerCancionDeArchivoDevuelveCancionConNombreYAutorCorrectos() 
        {
            string pathCancion = Path.Combine(Environment.CurrentDirectory, 
            Path.Combine("Archivos Prueba", "WeWillRockYou.xml"));
            IProveedorCancion proveedor = new ProveedorCancionXml();
            Cancion cancion = proveedor.ObtenerCancion(pathCancion);

            Assert.AreEqual("We will rock you", cancion.Nombre);
            Assert.AreEqual("Queen", cancion.Autor);
        }
    }
}
