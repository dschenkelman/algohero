using System;
using AlgoHero.Juego.Core;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.Eventos
{
    public class EmpezarCancionLlamadoEventArgs : EventArgs
    {
        public EmpezarCancionLlamadoEventArgs(Cancion cancion, Nivel nivel)
        {
            Cancion = cancion;
            Nivel = nivel;
            //Nivel.AsignarCancion(Cancion);
        }

        public Cancion Cancion
        {
            get; private set;
        }

        public Nivel Nivel
        {
            get; private set;
        }
    }
}
