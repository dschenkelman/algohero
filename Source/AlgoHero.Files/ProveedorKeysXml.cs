using System.Collections.Generic;
using System.Windows.Input;
using System.Xml;

namespace AlgoHero.Files
{
    public class ProveedorKeysXml
    {
        /* Obtiene una lista de listas de Key a partir de un archivo XML pasado por parametro*/
        public List<List<Key>> ObtenerListaDeKeys(string path)
        {
            XmlDocument documento = new XmlDocument();
            documento.Load(path);
            XmlNode nodoEntradas = this.ObtenerNodoEntradas(documento);
            XmlNodeList listaEntradas = this.ObtenerListaNodos(nodoEntradas);
            return ConvertirListaNodosAListaKeys(listaEntradas);
        }

        /* Recibe el documento XML y devuelve el nodo de las entradas */
        private XmlNode ObtenerNodoEntradas(XmlDocument documento)
        {
            return documento.FirstChild.FirstChild;
        }

        /* Recibe un nodo de entradas y devuelve una lista de nodos con todas las entradas */
        private XmlNodeList ObtenerListaNodos(XmlNode nodo)
        {
            return nodo.ChildNodes;
        }

        /* Recibe una lista de nodos, la convierte a una lista de listas de Key y la devuelve */
        private List<List<Key>>ConvertirListaNodosAListaKeys(XmlNodeList listaNodos)
        {
            List<List<Key>> listaKeys = new List<List<Key>>();
            foreach (XmlNode nodoEntrada in listaNodos)
            {
                List<Key> lista = this.CrearKey(nodoEntrada);
                listaKeys.Add(lista);
            }
            return listaKeys;
        }

        /* Recibe un nodo del documento XML y a partir de los datos de dicho nodo crea una Key y la devuelve */
        private List<Key> CrearKey(XmlNode nodo)
        {
            XmlNodeList listaTeclas = nodo.ChildNodes;
            List<Key> listaKey = new List<Key>();
            foreach (XmlNode nodoTecla in listaTeclas)
            {
                Key key = ConvertidorStringAKeys.ConvertirAKey(nodoTecla.Attributes["letra"].Value);
                listaKey.Add(key);
            }
            return listaKey;
        }
    }
}
