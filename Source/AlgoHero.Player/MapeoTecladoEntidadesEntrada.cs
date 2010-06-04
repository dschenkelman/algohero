using System.Collections.Generic;
using System.Collections.ObjectModel;
using AlgoHero.Player.Interfaces;
using System.Windows.Input;
using System.Linq;

namespace AlgoHero.Player
{
    public class MapeoTecladoEntidadesEntrada : IMapeoTecladoEntidadesEntrada
    {
        private Dictionary<Key, EntidadEntrada> diccionarioEntidades;
        private ReadOnlyCollection<EntidadEntrada> entidadesEntrada;
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
                EntidadEntrada entEntrada = this.diccionarioEntidades[key];
                return entEntrada;
            }
            return null;
        }

        private void AgregarTeclaADiccionario(Key key)
        {
            this.diccionarioEntidades.Add(key, new EntidadEntrada(this.codigo));
            this.codigo++;
        }

        public ReadOnlyCollection<EntidadEntrada> ObtenerEntidadesEntrada()
        {
            //performance
            if(this.entidadesEntrada == null)
            {
                this.entidadesEntrada = this.diccionarioEntidades.Values.ToList().AsReadOnly();
            }
            return entidadesEntrada;
        }
    }
}
