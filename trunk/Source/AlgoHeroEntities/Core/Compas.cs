using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private double tiempoEnNegras;


        /*Crea un nuevo compas asignando con el tiempo de cancion recibido como parametro.*/
        public Compas(TiempoCancion tiempoCancion)
        {
            this.notas = new List<Nota>();
            this.tiempoCancion = tiempoCancion;
            this.tiempoEnNegras = 0;
            this.tiempoMaximoCompas = tiempoCancion.DuracionCompas;
            this.tiempoActualCompas = 0;
        }

        /*Recibe una nota como parametro y la agrega al compas. Si agregar la nota llena al compas
         mas de lo permitido por el tiempo de la cancion lanza una excepcion ExcepcionCompasInvalido.*/
        public void AgregarNota(Nota nota)
        {
            this.tiempoActualCompas += 
                nota.CalcularTiempoProximaNota(this.tiempoCancion);
            this.tiempoEnNegras += nota.ProporcionFigura;

            if (this.tiempoEnNegras > this.tiempoCancion.CantidadNegras)
            {
                throw new ExcepcionCompasInvalido();
            }
            this.notas.Add(nota);
        }

        /*Devuelve la cantidad de notas del compas.*/
        public int CantidadNotas
        {
            get { return this.notas.Count; }
        }
      
        /*Devuelve si el compsa esta completo (segun el tiempo de la cancion).*/
        public bool EsCompleto
        {
            get { return (this.tiempoEnNegras == this.tiempoCancion.CantidadNegras); }
        }

        /*Devuelve la nota del compas en la posicion recibida como parametro.*/
        public Nota ObtenerNota(int index)
        {
            return this.notas[index];
        }

        /*Recibe una nota y la borra del compas.*/
        public void BorrarNota(Nota nota)
        {
            this.notas.Remove(nota);
        }

        /*Devuelve una coleccion de notas del compas.*/
        public ReadOnlyCollection<Nota> ObtenerNotas()
        {
            return this.notas.AsReadOnly();
        }
    }
}
