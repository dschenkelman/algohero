using System;
using AlgoHero.Interface;
using AlgoHero.Juego.Intefaces;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.Tests.Mocks
{
    public class MockEstrategiaNivel : IEstrategiaNivel
    {
        public Nota ObtenerSiguienteNota()
        {
            throw new NotImplementedException();
        }

        public void AsignarCancion(Cancion cancion)
        {
            throw new NotImplementedException();
        }

        public bool EsFinalCancion()
        {
            throw new NotImplementedException();
        }

        public void AsignarTonos(IControladorTeclas controlador)
        {
            throw new NotImplementedException();
        }
    }
}
