using System.Collections.Generic;
using AlgoHero.Interface;

namespace AlgoHero.Juego.Tests.Core.Mocks
{
    public class MockControladorTeclas : IControladorTeclas
    {
        private List<ITecla> Teclas;

        public MockControladorTeclas()
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
            return this.Teclas[index - 1];
        }

        public IEnumerable<ITecla> ObtenerTeclas()
        {
            return this.Teclas;
        }

        public ITecla ObtenerTecla(EntidadEntrada entidadEntrada)
        {
            return this.Teclas[entidadEntrada.Codigo];
        }
    }
}