using System;
using System.Collections.ObjectModel;
using AlgoHero.Juego.Core;
using AlgoHero.Juego.Intefaces;

namespace AlgoHero.Pantallas.Tests.Mocks
{
    public class MockProveedorNiveles : IProveedorNiveles
    {
        private ObservableCollection<Nivel> niveles;

        public MockProveedorNiveles()
        {
            Nivel facil = new Nivel("Facil", new MockEstrategiaNivel());
            Nivel medio = new Nivel("Medio", new MockEstrategiaNivel());
            Nivel dificil = new Nivel("Dificil", new MockEstrategiaNivel());
            niveles = new ObservableCollection<Nivel>();

            this.niveles.Add(facil);
            this.niveles.Add(medio);
            this.niveles.Add(dificil);
        }
        
        public ObservableCollection<Nivel> ObtenerNiveles()
        {
            return this.niveles;
        }
    }
}
