using System;
using System.Collections.Generic;
using AlgoHero.MusicEntities.Excepciones;

namespace AlgoHero.MusicEntities.Core
{
    public class Partitura
    {
        private readonly TiempoCancion tiempoCancion;

        private List<Compas> compases;
        
        public Partitura(TiempoCancion tiempoCancion)
        {
            this.tiempoCancion = tiempoCancion;
            this.compases = new List<Compas>();
        }

        public int CantidadCompases
        {
            get { return this.compases.Count; }
        }

        public void AgregarCompas(Compas compas)
        {
            if (!compas.EsCompleto)
            {
                throw new ExcepcionCompasInvalido();
            }
            this.compases.Add(compas);
        }

        public Compas ObtenerCompas(int index)
        {
            return this.compases[index];
        }
    }
}
