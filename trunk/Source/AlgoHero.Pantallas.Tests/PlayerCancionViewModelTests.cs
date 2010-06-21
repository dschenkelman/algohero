﻿using System;
using AlgoHero.Interface.Enums;
using AlgoHero.Juego.Core;
using AlgoHero.MusicEntities.Core;
using NUnit.Framework;
using AlgoHero.Pantallas.PlayerCancion;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.Files.Interfaces;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.MusicEntities.Servicios.Interfaces;
using AlgoHero.Juego.Intefaces;
using System.Threading;
using System.Diagnostics;

namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class PlayerCancionViewModelTests
    {
        [Test]
        public void HandlearElEventoEmpezarCancionObtieneCancionConPartitura()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(null, new MockManejadorVentanaPrincipal(), proveedorCancion, new MockCalculadorDuracionNotas());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath" }
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.AreEqual("Cancion Recuperada", vm.CancionActual.Nombre);
        }

        [Test]
        public void HandlearElEventoEmpezarCancionGuardaNivel()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(null, new MockManejadorVentanaPrincipal(), proveedorCancion, new MockCalculadorDuracionNotas());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath" }
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.AreEqual("Nivel Test", vm.NivelActual.Descripcion);
        }

        [Test]
        public void AlEmpezarCancionCambiaContenidoDeVentanaPrincipal()
        {
            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(vistaPlayerCancion, manejadorVentanaPrincipal, proveedorCancion, calculadorDuracionNotas);

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath" }
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.AreEqual(vistaPlayerCancion, manejadorVentanaPrincipal.Contenido);
        }

        [Test]
        public void AlEmpezarCancionCalculaDuracionNotas()
        {
            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(vistaPlayerCancion, manejadorVentanaPrincipal, proveedorCancion, calculadorDuracionNotas);

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo"){PathPartitura = "MiPath"}
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.IsTrue(calculadorDuracionNotas.CalcularDuracionFueLlamado);
            Assert.AreEqual(FiguraMusical.Semicorchea, calculadorDuracionNotas.FiguraLlamado);
        }

        [Test]
        public void ActualizarEstadoPideSiguienteNotaSoloSiNoEstaAlFinalDeLaCancion()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionFinita);
            
            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(vistaPlayerCancion, manejadorVentanaPrincipal, proveedorCancion, calculadorDuracionNotas);

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos"){PathPartitura = "MiPath"}
                , nivel));

            //loopear mucho para que se acaben las notas
            for (int i = 0; i < 1000; i++)
            {
                vm.ActualizarEstado(this, null);
            }
            
            Assert.AreEqual(10, mockEstrategiaNivelCancionFinita.LlamadosObtenerNotas);
            Assert.IsTrue(mockEstrategiaNivelCancionFinita.EsFinalCancion());
        }

        [Test]
        public void ActualizarEstadoPideSiguienteNotaCuandoTiempoEntreNotasEsIgualATiempoDesdeNotaAnterior()
        {
            MockEstrategiaNivelCancionInfinita mockEstrategiaNivelCancionInfinita = new MockEstrategiaNivelCancionInfinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionInfinita);

            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(vistaPlayerCancion, manejadorVentanaPrincipal, proveedorCancion, calculadorDuracionNotas);

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath" }
                , nivel));
            
            Thread.Sleep(2100);

            Assert.AreEqual(3, mockEstrategiaNivelCancionInfinita.LlamadosObtenerNotas);
        }

        private class MockEstrategiaNivelCancionInfinita : IEstrategiaNivel
        {

            public int LlamadosObtenerNotas { get; set; }

            #region IEstrategiaNivel Members

            public Nota ObtenerSiguienteNota()
            {
                LlamadosObtenerNotas++;
                return new Nota(Tono.Do, FiguraMusical.Negra);
            }

            public bool EsFinalCancion()
            {
                return false;
            }

            public void AsignarTonos(AlgoHero.Interface.IControladorTeclas controlador)
            {
                throw new NotImplementedException();
            }

            public void AsignarCancion(Cancion cancion)
            {

            }

            #endregion
        }

        private class MockEstrategiaNivelCancionFinita : IEstrategiaNivel
        {
            private int contador = 0;

            public int LlamadosObtenerNotas { get; set; }

            #region IEstrategiaNivel Members

            public Nota ObtenerSiguienteNota()
            {
                LlamadosObtenerNotas++;
                contador++;
                if (contador % 2 == 0)
                {
                    return new Nota(Tono.Do, FiguraMusical.Negra);
                }
                else
                {
                    return new Nota(Tono.Re, FiguraMusical.Semicorchea);
                }
            }

            public bool EsFinalCancion()
            {
                if (contador >= 10)
                {
                    return true;
                }

                return false;
            }

            public void AsignarTonos(AlgoHero.Interface.IControladorTeclas controlador)
            {
                throw new NotImplementedException();
            }

            public void AsignarCancion(Cancion cancion)
            {
                
            }

            #endregion
        }
        
        private class MockCalculadorDuracionNotas : ICalculadorDuracionNotas
        {
            public double CalcularDuracion(TiempoCancion tiempoCancion, Nota nota)
            {
                throw new NotImplementedException();
            }

            public double CalcularDuracion(TiempoCancion tiempoCancion, FiguraMusical figuraMusical)
            {
                this.CalcularDuracionFueLlamado = true;
                this.FiguraLlamado = figuraMusical;
                if (figuraMusical == FiguraMusical.Negra)
                {
                    return 1;
                }
                else if(figuraMusical == FiguraMusical.Semicorchea)
                {
                    return 0.25;
                }
                return 0;
            }

            public bool CalcularDuracionFueLlamado
            {
                get; set;
            }

            public FiguraMusical FiguraLlamado
            {
                get; set;
            }
        }
        
        private class MockVistaPlayerCancion : IVistaPlayerCancion
        {

        }

        private class MockProveedorCancionXml : IProveedorCancion
        {
            public Cancion ObtenerCancionSinPartitura(string path)
            {
                throw new NotImplementedException();
            }

            public Cancion ObtenerCancionConPartitura(string path)
            {
                if (path == "MiPath")
                {
                    return new Cancion("Cancion Recuperada", "Recuperada")
                               {
                                   Partitura = new Partitura(new TiempoCancion(4, 2))
                               };
                }
                return null;
            }
        }

        private class MockManejadorVentanaPrincipal : IManejadorVentanaPrincipal
        {
            public object Contenido
            {
                get; set;
            }

            public void CambiarContenido(object control)
            {
                this.Contenido = control;
            }
        }
    }
}
