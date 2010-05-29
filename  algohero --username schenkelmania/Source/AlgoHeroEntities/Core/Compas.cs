using System.Collections.Generic;
using AlgoHero.MusicEntities.Excepciones;
using System;

namespace AlgoHero.MusicEntities.Core
{
    public class Compas
    {
        private List<Nota> notas;
        private TiempoCancion tiempoCancion;
        private double tiempoMaximoCompas;
        private double tiempoActualCompas;


        public Compas(TiempoCancion tiempoCancion)
        {
            this.notas = new List<Nota>();
            this.tiempoCancion = tiempoCancion;
            this.tiempoMaximoCompas = tiempoCancion.DuracionCompas;
            this.tiempoActualCompas = 0;
        }

        public void AgregarNota(Nota nota)
        {
            this.tiempoActualCompas += 
                nota.CalcularTiempoProximaNota(this.tiempoCancion);
            if (tiempoActualCompas > tiempoMaximoCompas)
            {
                throw new ExcepcionCompasInvalido();
            }
            this.notas.Add(nota);
        }

        public int CantidadNotas
        {
            get { return this.notas.Count; }
        }

        public Nota ObtenerNota(int index)
        {
            return this.notas[index];
        }

        public void BorrarNota(Nota nota)
        {
            this.notas.Remove(nota);
        }
    }
}
