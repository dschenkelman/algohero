using System;
using System.Collections.ObjectModel;
using AlgoHero.Interface.Enums;
using System.Collections.Generic;
using AlgoHero.Interface;

namespace AlgoHero.Player
{
    public class Tecla : ITecla
    {
        private List<Tono> tonos;

        public Tecla(EntidadEntrada entidad)
        {
            this.tonos = new List<Tono>();
            this.EntidadEntrada = entidad;
        }

        public void AgregarTonoAsociado(Tono tono)
        {
            if (this.tonos.Contains(tono))
            {
                throw new InvalidOperationException();
            }
            this.tonos.Add(tono);
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

        public Tono ObtenerTono(int index)
        {
            return this.tonos[index];
        }

        public ReadOnlyCollection<Tono> ObtenerTonosAsociados()
        {
            return this.tonos.AsReadOnly();
        }
    }
}
