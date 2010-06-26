using System;
using System.Collections.ObjectModel;
using AlgoHero.Interface.Enums;
using System.Collections.Generic;
using AlgoHero.Interface;

namespace AlgoHero.Juego.Entrada
{
    public class Tecla : ITecla
    {
        private List<Tono> tonos;

        /* Constructor. Recibe una EntidadEntrada y crea una nueva tecla. */
        public Tecla(EntidadEntrada entidad)
        {
            this.tonos = new List<Tono>();
            this.EntidadEntrada = entidad;
        }

        /* Recibe un Tono y lo agrega a la lista de tonos asociados a la tecla. */
        public void AgregarTonoAsociado(Tono tono)
        {
            if (this.tonos.Contains(tono))
            {
                throw new InvalidOperationException();
            }
            this.tonos.Add(tono);
        }

        /* Borra los tonos asociados a la tecla creando una nueva lista de tonos asociados. */
        public void ResetearTonosAsignados() 
        {
            this.tonos = new List<Tono>();
        }

        public int CantidadTonos
        {
            get
            {
                return this.tonos.Count;
            }
        }

        public EntidadEntrada EntidadEntrada
        {
            get; private set;
        }

        /* Este metodo recibe un indice y devuelve el tono asociado a ese indice. */
        public Tono ObtenerTono(int index)
        {
            return this.tonos[index];
        }

        /* Este metodo devuelve una coleccion con todos los tonos asociados a la tecla.*/
        public ReadOnlyCollection<Tono> ObtenerTonosAsociados()
        {
            return this.tonos.AsReadOnly();
        }
    }
}