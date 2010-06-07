using System.Collections.Generic;
using System.Collections.ObjectModel;
using AlgoHero.Player.Interfaces;
using System.Windows.Input;
using System.Linq;
using AlgoHero.Files;
using AlgoHero.Interface;

namespace AlgoHero.Player
{
    public class MapeoTecladoEntidadesEntrada : IMapeoTecladoEntidadesEntrada
    {
        private Dictionary<Key, EntidadEntrada> diccionarioEntidades;
        private ReadOnlyCollection<EntidadEntrada> entidadesEntrada;
        private int codigo = 1;

        /* Constructor. Agrega las Keys al diccionario de Entidades.*/
        public MapeoTecladoEntidadesEntrada()
        {
            diccionarioEntidades = new Dictionary<Key, EntidadEntrada>();
            AgregarTeclaADiccionario(Key.A);
            AgregarTeclaADiccionario(Key.S);
            AgregarTeclaADiccionario(Key.K);
            AgregarTeclaADiccionario(Key.L);
        }

        /* Constructor. Recibe el path de un archivo XML por parametro y agrega las Keys al diccionario*/
        public MapeoTecladoEntidadesEntrada(string path)
        {
            this.diccionarioEntidades = new Dictionary<Key, EntidadEntrada>();
            ProveedorKeysXml proveedor = new ProveedorKeysXml();
            List<List<Key>> listaKeys = proveedor.ObtenerListaDeKeys(path);
            foreach (List<Key> lista in listaKeys)
            {
                EntidadEntrada entEntrada = new EntidadEntrada(this.codigo);
                foreach (Key key in lista)
                {
                    this.diccionarioEntidades.Add(key, entEntrada);
                }
                this.codigo++;
            }
        }

        /* Recibe una Key por parametro y devuelve la Entidad Entrada que esta relacionada
         * a es Key en el diccionario. Si no se encuentra en el diccionario, devuelve null.*/
        public EntidadEntrada ObtenerEntidadEntrada(Key key)
        {
            if (this.diccionarioEntidades.ContainsKey(key))
            {
                EntidadEntrada entEntrada = this.diccionarioEntidades[key];
                return entEntrada;
            }
            return null;
        }

        /* Recibe una key y se la agrega al diccionario con una nueva Entidad Entrada*/
        private void AgregarTeclaADiccionario(Key key)
        {
            this.diccionarioEntidades.Add(key, new EntidadEntrada(this.codigo));
            this.codigo++;
        }

        /* Devuelve una coleccion con todas las Entidades Entrada del diccionario */
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
