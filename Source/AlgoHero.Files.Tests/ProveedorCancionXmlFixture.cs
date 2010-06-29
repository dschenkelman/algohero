using System.Collections.Generic;
using System.Linq;
using AlgoHero.Interface.Enums;
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
        public void ObtenerCancionSinPartiruaDeArchivoDevuelveCancionConNombreYAutorCorrectos()
        {
            string pathCancion = Path.Combine(Environment.CurrentDirectory,
            Path.Combine(@"Archivos Prueba\Canciones\Queen", "WeWillRockYou.xml"));
            string directorioCanciones = Path.Combine(Environment.CurrentDirectory, @"Archivos Prueba\Canciones");
            IProveedorCancion proveedor = new ProveedorCancionXml();
            Cancion cancionSinPartitura = proveedor.ObtenerCancionSinPartitura(pathCancion);
            Assert.AreEqual("We will rock you", cancionSinPartitura.Nombre);
            Assert.AreEqual("Queen", cancionSinPartitura.Autor);
        }
        
        [Test]
        public void ObtenerCancionDeArchivoDevuelveCancionConTiempoCorrecto()
        {

            string pathCancion = Path.Combine(Environment.CurrentDirectory,
            Path.Combine(@"Archivos Prueba\Canciones\Queen", "WeWillRockYou.xml"));
            string directorioCanciones = Path.Combine(Environment.CurrentDirectory, @"Archivos Prueba\Canciones");
            IProveedorCancion proveedor = new ProveedorCancionXml();
            Cancion cancion = proveedor.ObtenerCancionConPartitura(pathCancion);

            Assert.AreEqual(4, cancion.Partitura.TiempoCancion.DuracionCompas);
            Assert.AreEqual(4, cancion.Partitura.TiempoCancion.CantidadNegras);
        }

        [Test]
        public void ObtenerCancionDeArchivoDevuelveCancionConCantidadDeCompasesCorrecta()
        {
            string pathCancion = Path.Combine(Environment.CurrentDirectory,
            Path.Combine(@"Archivos Prueba\Canciones\Queen", "WeWillRockYou.xml"));
            string directorioCanciones = Path.Combine(Environment.CurrentDirectory, @"Archivos Prueba\Canciones");
            IProveedorCancion proveedor = new ProveedorCancionXml();
            Cancion cancion = proveedor.ObtenerCancionConPartitura(pathCancion);
            
            Assert.AreEqual(2, cancion.Partitura.CantidadCompases);
        }

        [Test]
        public void ObtenerCancionDeArchivoObtieneNombreDelArchivoDeMusicaAReproducir()
        {
            string pathCancion = Path.Combine(Environment.CurrentDirectory,
            Path.Combine(@"Archivos Prueba\Canciones\Queen", "WeWillRockYou.xml"));
            string directorioCanciones = Path.Combine(Environment.CurrentDirectory, @"Archivos Prueba\Canciones");
            IProveedorCancion proveedor = new ProveedorCancionXml();
            Cancion cancion = proveedor.ObtenerCancionConPartitura(pathCancion);

            Assert.AreEqual(@"Musica\wwry.wav", cancion.PathArchivoMusica);
        }

        [Test]
        public void ObtenerCancionDeArchivoDevuelveCompasesConNotasCorrectas()
        {
            string pathCancion = Path.Combine(Environment.CurrentDirectory,
            Path.Combine(@"Archivos Prueba\Canciones\Queen", "WeWillRockYou.xml"));
            string directorioCanciones = Path.Combine(Environment.CurrentDirectory, @"Archivos Prueba\Canciones");
            IProveedorCancion proveedor = new ProveedorCancionXml();
            Cancion cancion = proveedor.ObtenerCancionConPartitura(pathCancion);
            
            Nota nota;
            //Primer nota primer compas
            nota = cancion.Partitura.ObtenerCompas(0).ObtenerNota(0);
            Assert.AreEqual(Tono.Do, nota.Tonos[0]);
            Assert.AreEqual(Tono.Mi, nota.Tonos[1]);
            //Este ademas prueba un acorde
            Assert.AreEqual(FiguraMusical.Blanca, nota.Figura);

            //Segunda nota primer compas
            nota = cancion.Partitura.ObtenerCompas(0).ObtenerNota(1);
            Assert.AreEqual(Tono.Re, nota.Tonos[0]);
            Assert.AreEqual(FiguraMusical.Negra, nota.Figura);

            //Tercer nota primer compas
            nota = cancion.Partitura.ObtenerCompas(0).ObtenerNota(2);
            Assert.AreEqual(Tono.Silencio, nota.Tonos[0]);
            Assert.AreEqual(FiguraMusical.Negra, nota.Figura);

            //Primeras cuatro notas segundo compas
            for (int i = 0; i < 4; i++)
            {
                nota = cancion.Partitura.ObtenerCompas(1).ObtenerNota(i);
                Assert.AreEqual(Tono.Mi, nota.Tonos[0]);
                Assert.AreEqual(FiguraMusical.Corchea, nota.Figura);
            }

            nota = cancion.Partitura.ObtenerCompas(1).ObtenerNota(4);
            Assert.AreEqual(Tono.DoSostenido, nota.Tonos[0]);
            Assert.AreEqual(FiguraMusical.Blanca, nota.Figura);
        }

        [Test]
        public void ObtenerCancionesDeDirectorioDevuelveCantidadCancionesCorrecta()
        {
            string directorioCanciones = Path.Combine(Environment.CurrentDirectory, @"Archivos Prueba\Canciones");
            IProveedorCancionesDirectorio proveedor = new ProveedorCancionXml();
            IEnumerable<Cancion> canciones = proveedor.ObtenerCancionesDirectorio(directorioCanciones);
            
            Assert.AreEqual(3, canciones.Count());
        }

        [Test]
        public void ObtenerCancionesDeDirectorioDevuelveCancionesConSuPath()
        {
            string directorioCanciones = Path.Combine(Environment.CurrentDirectory, @"Archivos Prueba\Canciones");
            IProveedorCancionesDirectorio proveedor = new ProveedorCancionXml();
            IEnumerable<Cancion> canciones = proveedor.ObtenerCancionesDirectorio(directorioCanciones);

            Cancion cancion;
            string pathCancion;
            
            cancion = canciones.First(c => c.Nombre == "We will rock you");
            pathCancion = Path.Combine(directorioCanciones, @"Queen\WeWillRockYou.xml");
            Assert.AreEqual(pathCancion, cancion.PathPartitura);

            cancion = canciones.First(c => c.Nombre == "Hotel California");
            pathCancion = Path.Combine(directorioCanciones, @"Eagles\HotelCalifornia.xml");
            Assert.AreEqual(pathCancion, cancion.PathPartitura);

            cancion = canciones.First(c => c.Nombre == "Jijiji");
            pathCancion = Path.Combine(directorioCanciones, @"Los Redondos\Jijiji.xml");
            Assert.AreEqual(pathCancion, cancion.PathPartitura);
        }
    }
}
