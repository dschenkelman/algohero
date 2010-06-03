using System;
using System.Collections.Generic;
using AlgoHero.Player.Interfaces;
using System.Windows.Input;

namespace AlgoHero.Player
{
    public class Mapeo : IObtenerEntidades
    {
        private Dictionary<Key, EntidadEntrada> diccionarioEntidades;
        private int codigo = 1;

        public Mapeo()
        {
            diccionarioEntidades = new Dictionary<Key, EntidadEntrada>();
            diccionarioEntidades.Add(Key.A, new EntidadEntrada(codigo));
            this.codigo++;
            diccionarioEntidades.Add(Key.S, new EntidadEntrada(codigo));
            this.codigo++;
            diccionarioEntidades.Add(Key.K, new EntidadEntrada(codigo));
            this.codigo++;
            diccionarioEntidades.Add(Key.L, new EntidadEntrada(codigo));
            this.codigo++;
        }

        public EntidadEntrada ObtenerEntidadEntrada(Key key)
        {
            if (this.diccionarioEntidades.ContainsKey(key))
            {
                EntidadEntrada entEntrada;
                diccionarioEntidades.TryGetValue(key, out entEntrada);
                return entEntrada;
            }
            return null;
        }

        public Dictionary<Key, EntidadEntrada> getDiccionarioEntidades()
        {
            return this.diccionarioEntidades;
        }

    }
}
