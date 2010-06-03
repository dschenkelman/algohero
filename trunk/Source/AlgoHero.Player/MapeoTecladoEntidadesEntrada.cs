using System;
using System.Collections.Generic;
using AlgoHero.Player.Interfaces;
using System.Windows.Input;

namespace AlgoHero.Player
{
    public class MapeoTecladoEntidadesEntrada : IMapeoTecladoEntidadesEntrada
    {
        private Dictionary<Key, EntidadEntrada> diccionarioEntidades;
        private int codigo = 1;

        public MapeoTecladoEntidadesEntrada()
        {
            diccionarioEntidades = new Dictionary<Key, EntidadEntrada>();
            AgregarTeclaADiccionario(Key.A);
            AgregarTeclaADiccionario(Key.S);
            AgregarTeclaADiccionario(Key.K);
            AgregarTeclaADiccionario(Key.L);
        }

        public EntidadEntrada ObtenerEntidadEntrada(Key key)
        {
            if (this.diccionarioEntidades.ContainsKey(key))
            {
                EntidadEntrada entEntrada = diccionarioEntidades[key];
                return entEntrada;
            }
            return null;
        }

        private void AgregarTeclaADiccionario(Key key)
        {
            this.diccionarioEntidades.Add(key, new EntidadEntrada(this.codigo));
            this.codigo++;
        }

    }
}
