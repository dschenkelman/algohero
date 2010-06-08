using System;
using System.Windows.Input;
using AlgoHero.MusicEntities.Core;
using NUnit.Framework;
using AlgoHero.MusicEntities.Enums;
using AlgoHero.Interface;

namespace AlgoHero.Player.Tests
{
    [TestFixture]
    public class ManagerTeclasTests
    {
        [Test]
        public void CrearControladorEnNivelFacilCreaConDosTeclas()
        {
            var controlador = new ManagerTeclas(Nivel.Facil);
            Assert.AreEqual(2, controlador.CantidadTeclas);
        }
        
        [Test]
        public void CrearControladorEnNivelFacilCreaConTresTeclas()
        {
            var controlador = new ManagerTeclas(Nivel.Medio);
            Assert.AreEqual(3, controlador.CantidadTeclas);
        }

        [Test]
        public void CrearControladorEnNivelFacilCreaConCuatroTeclas()
        {
            var controlador = new ManagerTeclas(Nivel.Dificil);
            Assert.AreEqual(4, controlador.CantidadTeclas);
        }

        [Test]
        public void CrearControladorEnNivelFacilCreaTeclasAyS()
        {
            var controlador = new ManagerTeclas(Nivel.Facil);
            Assert.AreEqual(1, controlador.ObtenerTecla(0).EntidadEntrada.Codigo);
            Assert.AreEqual(2, controlador.ObtenerTecla(1).EntidadEntrada.Codigo);
        }

        [Test]
        public void CrearControladorEnNivelFacilCreaTeclaK()
        {
            var controlador = new ManagerTeclas(Nivel.Medio);
            Assert.AreEqual(3, controlador.ObtenerTecla(2).EntidadEntrada.Codigo);
        }

        [Test]
        public void CrearControladorEnNivelFacilCreaTeclaL()
        {
            var controlador = new ManagerTeclas(Nivel.Dificil);
            Assert.AreEqual(4, controlador.ObtenerTecla(3).EntidadEntrada.Codigo);
        }

        [Test]
        public void AsignarTonosPorTeclaEnNivelFacilAsignaCorrectamente()
        {
            var controlador = new ManagerTeclas(Nivel.Facil);
            Cancion cancion = ObtenerCancionMock();

            controlador.AsignarTonosATeclas(cancion);

            Assert.Contains(Tono.Do, controlador.ObtenerTecla(0).ObtenerTonosAsociados());
            Assert.Contains(Tono.Mi, controlador.ObtenerTecla(0).ObtenerTonosAsociados());
            Assert.Contains(Tono.Re, controlador.ObtenerTecla(0).ObtenerTonosAsociados());
            Assert.Contains(Tono.Sol, controlador.ObtenerTecla(0).ObtenerTonosAsociados());

            Assert.Contains(Tono.Fa, controlador.ObtenerTecla(1).ObtenerTonosAsociados());
            Assert.Contains(Tono.La, controlador.ObtenerTecla(1).ObtenerTonosAsociados());
            Assert.Contains(Tono.Si, controlador.ObtenerTecla(1).ObtenerTonosAsociados());
            
        }

        [Test]
        public void AsignarTonosPorTeclaEnNivelMedioAsignaCorrectamente()
        {
            var controlador = new ManagerTeclas(Nivel.Medio);
            Cancion cancion = ObtenerCancionMock();

            controlador.AsignarTonosATeclas(cancion);

            Assert.Contains(Tono.Do, controlador.ObtenerTecla(0).ObtenerTonosAsociados());
            Assert.Contains(Tono.Si, controlador.ObtenerTecla(0).ObtenerTonosAsociados());
            Assert.Contains(Tono.Sol, controlador.ObtenerTecla(0).ObtenerTonosAsociados());
            
            Assert.Contains(Tono.La, controlador.ObtenerTecla(1).ObtenerTonosAsociados());
            Assert.Contains(Tono.Mi, controlador.ObtenerTecla(1).ObtenerTonosAsociados());

            Assert.Contains(Tono.Re, controlador.ObtenerTecla(2).ObtenerTonosAsociados());
            Assert.Contains(Tono.Fa, controlador.ObtenerTecla(2).ObtenerTonosAsociados());
        }

        [Test]
        public void AsignarTonosPorTeclaEnNivelDificilAsignaCorrectamente()
        {
            var controlador = new ManagerTeclas(Nivel.Dificil);
            Cancion cancion = ObtenerCancionMock();

            controlador.AsignarTonosATeclas(cancion);

            Assert.Contains(Tono.Do, controlador.ObtenerTecla(0).ObtenerTonosAsociados());
            Assert.Contains(Tono.Mi, controlador.ObtenerTecla(0).ObtenerTonosAsociados());

            Assert.Contains(Tono.La, controlador.ObtenerTecla(1).ObtenerTonosAsociados());
            Assert.Contains(Tono.Fa, controlador.ObtenerTecla(1).ObtenerTonosAsociados());

            Assert.Contains(Tono.Re, controlador.ObtenerTecla(2).ObtenerTonosAsociados());
            Assert.Contains(Tono.Sol, controlador.ObtenerTecla(2).ObtenerTonosAsociados());
            
            Assert.Contains(Tono.Si, controlador.ObtenerTecla(3).ObtenerTonosAsociados());
        }

        [Test]
        public void AsignarTonosPorTeclaNoAsignaTeclasConNingunaAparicion()
        {
            var controlador = new ManagerTeclas(Nivel.Facil);
            Cancion cancion = ObtenerCancionMock();

            controlador.AsignarTonosATeclas(cancion);

            for (int i = 0; i < 2; i++)
            {
                Assert.IsFalse(controlador.ObtenerTecla(i).ObtenerTonosAsociados().Contains(Tono.DoSostenido));
                Assert.IsFalse(controlador.ObtenerTecla(i).ObtenerTonosAsociados().Contains(Tono.FaSostenido));
                Assert.IsFalse(controlador.ObtenerTecla(i).ObtenerTonosAsociados().Contains(Tono.ReSostenido));
                Assert.IsFalse(controlador.ObtenerTecla(i).ObtenerTonosAsociados().Contains(Tono.LaSostenido));
            }
        }

        private static Cancion ObtenerCancionMock()
        {
            Cancion cancion = new Cancion("Mock", "Autor Mock");
            TiempoCancion tiempo = new TiempoCancion(4, 2);
            Partitura partitura = new Partitura(tiempo);
            //Do 16, La 10, Re 8, Si 6, Mi 4, Fa 2, Sol 1
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 16, Tono.Do, FiguraMusical.Semicorchea));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 8, Tono.Re, FiguraMusical.Corchea));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 4, Tono.Mi, FiguraMusical.Negra));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 2, Tono.Fa, FiguraMusical.Blanca));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 1, Tono.Sol, FiguraMusical.Redonda));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 2, Tono.La, FiguraMusical.Blanca));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 4, Tono.Si, FiguraMusical.Negra));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 2, Tono.Si, FiguraMusical.Blanca));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 8, Tono.La, FiguraMusical.Corchea));

            cancion.Partitura = partitura;
            return cancion;
        }

        private static Compas ObtenerMockCompas(TiempoCancion tiempo, int cantidad, Tono tono, FiguraMusical figura)
        {
            Compas compas = new Compas(tiempo);
            for (int i = 0; i < cantidad; i++)
            {
                compas.AgregarNota(new Nota(tono, figura));
            }
            return compas;
        }
    }
}
