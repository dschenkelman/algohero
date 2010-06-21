using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AlgoHero.Interface;
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

namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class PlayerCancionViewModelTests
    {
        [Test]
        public void HandlearElEventoEmpezarCancionObtieneCancionConPartitura()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(null,
                new MockManejadorVentanaPrincipal(),
                proveedorCancion,
                new MockCalculadorDuracionNotas(),
                new MockMapeoTecladoEntidadesEntrada());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath" }
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.AreEqual("Cancion Recuperada", vm.CancionActual.Nombre);
        }


        [Test]
        public void HandlearElEventoEmpezarCancionGuardaNivel()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(null,
                new MockManejadorVentanaPrincipal(),
                proveedorCancion,
                new MockCalculadorDuracionNotas(),
                new MockMapeoTecladoEntidadesEntrada());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath" }
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.AreEqual("Nivel Test", vm.NivelActual.Descripcion);
        }

        [Test]
        public void HandlearElEventoEmpezarCancionLlamaAMetodosNivel()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(null,
                new MockManejadorVentanaPrincipal(),
                proveedorCancion,
                new MockCalculadorDuracionNotas(),
                new MockMapeoTecladoEntidadesEntrada());

            MockEstrategiaNivelCancionFinita estrategiaNivelCancionFinita =  new MockEstrategiaNivelCancionFinita();
            
            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath" }
                , new Nivel("Nivel Test", estrategiaNivelCancionFinita)));

            Assert.IsTrue(estrategiaNivelCancionFinita.AsignarCancionLlamado);
            Assert.IsTrue(estrategiaNivelCancionFinita.AsignarTonosLlamado);
        }

        [Test]
        public void AlEmpezarCancionCambiaContenidoDeVentanaPrincipal()
        {
            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion,
                manejadorVentanaPrincipal,
                proveedorCancion,
                calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada());

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
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada());

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
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada());

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
        public void ActualizarEstadoAgregaNotaANotasVisualesDeVista()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath" }
                , nivel));

            //loopear mucho para que se acaben las notas
            vm.ActualizarEstado(this, null);
            

            Assert.IsTrue(vistaPlayerCancion.AgregarNotaVisualFueLlamado);
        }


        [Test]
        public void ActualizarEstadoActualizarVista()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath" }
                , nivel));

            //loopear mucho para que se acaben las notas
            vm.ActualizarEstado(this, null);

            Assert.IsTrue(vistaPlayerCancion.ActualizarFueLlamado);
        }


        //Hay que correr el test separado de los demas
        [Test]
        public void ActualizarEstadoPideSiguienteNotaCuandoTiempoEntreNotasEsIgualATiempoDesdeNotaAnterior()
        {
            MockEstrategiaNivelCancionInfinita mockEstrategiaNivelCancionInfinita = new MockEstrategiaNivelCancionInfinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionInfinita);

            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath" }
                , nivel));
            
            Thread.Sleep(2100);

            Assert.GreaterOrEqual(mockEstrategiaNivelCancionInfinita.LlamadosObtenerNotas ,2);
            Assert.LessOrEqual(mockEstrategiaNivelCancionInfinita.LlamadosObtenerNotas, 4);
        }
        
        //Hay que correr el test separado de los demas. Para hacerlo sacar el atributo ignore.
        [Test]
        public void ActualizarEstadoPideSiguienteNotaCuandoTiempoEntreNotasEsIgualATiempoDesdeNotaAnterior2()
        {
            MockEstrategiaNivelCancionInfinita mockEstrategiaNivelCancionInfinita = new MockEstrategiaNivelCancionInfinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionInfinita);

            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath" }
                , nivel));

            Thread.Sleep(5100);

            Assert.GreaterOrEqual(mockEstrategiaNivelCancionInfinita.LlamadosObtenerNotas, 5);
            Assert.LessOrEqual(mockEstrategiaNivelCancionInfinita.LlamadosObtenerNotas, 7);
        }



        private class MockMapeoTecladoEntidadesEntrada : IMapeoTecladoEntidadesEntrada
        {
            public EntidadEntrada ObtenerEntidadEntrada(Key key)
            {
                return new EntidadEntrada(1);
            }

            public ReadOnlyCollection<EntidadEntrada> ObtenerEntidadesEntrada()
            {
                return new ReadOnlyCollection<EntidadEntrada>(new List<EntidadEntrada>() { new EntidadEntrada(1) });
            }
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

            public void AsignarTonos(IControladorTeclas controlador)
            {
                
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

            public bool AsignarTonosLlamado
            {
                get; set;
            }

            public bool AsignarCancionLlamado
            {
                get; set;
            }

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

            public void AsignarTonos(IControladorTeclas controlador)
            {
                this.AsignarCancionLlamado = true;
            }

            public void AsignarCancion(Cancion cancion)
            {
                this.AsignarTonosLlamado = true;
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
            public bool AgregarNotaVisualFueLlamado
            {
                get; set;
            }

            public bool ActualizarFueLlamado
            {
                get; set;
            }

            public void AgregarNotaVisual(Nota nota, IEnumerable<ITecla> teclas)
            {
                this.AgregarNotaVisualFueLlamado = true;
            }

            public void Actualizar()
            {
                this.ActualizarFueLlamado = true;
            }
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
