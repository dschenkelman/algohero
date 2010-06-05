using System.Collections.Generic;
using System.Windows.Input;
using System.Xml;

namespace AlgoHero.Files
{
    public class ProveedorKeysXml
    {
        public List<List<Key>> ObtenerListaDeKeys(string path)
        {
            XmlDocument documento = new XmlDocument();
            documento.Load(path);
            XmlNode nodoEntradas = this.ObtenerNodoEntradas(documento);
            XmlNodeList listaEntradas = this.ObtenerListaNodos(nodoEntradas);
            return ConvertirListaNodosAListaKeys(listaEntradas);
        }

        private XmlNode ObtenerNodoEntradas(XmlDocument documento)
        {
            return documento.FirstChild.FirstChild;
        }

        private XmlNodeList ObtenerListaNodos(XmlNode nodo)
        {
            return nodo.ChildNodes;
        }

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
