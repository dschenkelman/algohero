using NUnit.Framework;
using AlgoHero.Juego.Intefaces;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Interface;
using AlgoHero.Juego.Core;
using AlgoHero.Juego.Tests.Core.Mocks;

namespace AlgoHero.Juego.Tests.Core
{
    [TestFixture]
    public class NivelTests
    {
        [Test]
        public void CrearNivelSeteaDescripcion()
        {
            Nivel nivel = new Nivel("Muy Dificil", null);
            Assert.AreEqual("Muy Dificil", nivel.Descripcion);
        }

        [Test]
        public void ObtenerSiguienteNotaLlamaEstrategiaNivel()
        {
            MockEstrategiaNivel estrategiaNivel = new MockEstrategiaNivel();
            Nivel nivel = new Nivel("Mock", estrategiaNivel);

            Nota nota = nivel.ObtenerSiguienteNota();

            Assert.IsTrue(estrategiaNivel.ObtenerSiguienteNotaFueLlamado);
        }

        [Test]
        public void EsFinalDeCancionLlamaEstrategiaNivel()
        {
            MockEstrategiaNivel estrategiaNivel = new MockEstrategiaNivel();
            Nivel nivel = new Nivel("Mock", estrategiaNivel);

            bool esFinal = nivel.EsFinalCancion;

            Assert.IsTrue(estrategiaNivel.EsFinalCancionFueLlamado);
        }

        [Test]
        public void AsignarTeclasLlamaEstrategiaNivel()
        {
            MockEstrategiaNivel estrategiaNivel = new MockEstrategiaNivel();
            Nivel nivel = new Nivel("Mock", estrategiaNivel);

            nivel.AsignarTeclas(new MockControladorTeclas());

            Assert.IsTrue(estrategiaNivel.AsignarTeclasFueLlamado);
        }

        private class MockEstrategiaNivel : IEstrategiaNivel
        {
            #region IEstrategiaNivel Members

            public Nota ObtenerSiguienteNota()
            {
                this.ObtenerSiguienteNotaFueLlamado = true;
                return null;
            }

            public bool EsFinalCancion()
            {
                this.EsFinalCancionFueLlamado = true;
                return true;
            }

            public void AsignarTonos(IControladorTeclas controlador)
            {
                this.AsignarTeclasFueLlamado = true;
            }

            public void AsignarCancion(Cancion cancion)
            {
                throw new System.NotImplementedException();
            }

            #endregion

            public bool ObtenerSiguienteNotaFueLlamado
            {
                get; set;
            }

            public bool EsFinalCancionFueLlamado
            { 
                get; set;
            }

            public bool AsignarTeclasFueLlamado
            {
                get; set;
            }
        }
    }

    

}
