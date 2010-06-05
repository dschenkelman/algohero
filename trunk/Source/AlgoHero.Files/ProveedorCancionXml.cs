using System;
using System.Collections.Generic;
using AlgoHero.Files.Interfaces;
using AlgoHero.MusicEntities.Core;
using System.Xml;
using AlgoHero.MusicEntities.Enums;

namespace AlgoHero.Files
{
    public class ProveedorCancionXml : IProveedorCancion
    {
        /*Obtiene una Cancion a partir del archivo pasado como parametro.*/
        public Cancion ObtenerCancion(string path)
        {
            XmlDocument documento = new XmlDocument();
            documento.Load(path);
            XmlNode nodoCancion = this.ObtenerNodoCancion(documento);
            string nombreCancion = nodoCancion.Attributes["nombre"].Value;
            string autorCancion = nodoCancion.Attributes["autor"].Value;

            
            TiempoCancion tiempoCancion = this.CrearTiempoCancion(documento);
            Partitura partitura = new Partitura(tiempoCancion);
            this.AgregarCompases(partitura, tiempoCancion, documento);
            
            var cancion = new Cancion(nombreCancion, autorCancion);
            cancion.Partitura = partitura;

            return cancion;
        }

        /*A partir del documento XML recibido devuelve el tiempo de la cancion.*/
        private TiempoCancion CrearTiempoCancion(XmlDocument documento)
        {
            XmlNode nodoTiempoCancion = documento.SelectSingleNode("/xml/cancion/tiempo");
            double duracionCompas = Convert.ToDouble(nodoTiempoCancion.Attributes["duracionCompasSegundos"].Value);
            int cantidadBlancas = Convert.ToInt32(nodoTiempoCancion.Attributes["cantidadBlancas"].Value);

            return new TiempoCancion(duracionCompas, cantidadBlancas);
        }

        /*Agrega a la partitura los compases leidos a partir del XmlDocument, asignandoles el TiempoCancion.*/
        private void AgregarCompases(Partitura partitura, TiempoCancion tiempoCancion, XmlDocument documento)
        {
            XmlNodeList nodosCompases = documento.SelectNodes("/xml/cancion/compases/compas"); //documento.FirstChild.FirstChild.LastChild.ChildNodes;
            foreach (XmlNode nodo in nodosCompases)
            {
                var compas = new Compas(tiempoCancion);
                this.ComponerCompas(compas, nodo);
                partitura.AgregarCompas(compas);
            }
        }

        /*Recibe un compas y lo compone por notas a partir del XmlNode*/
        private void ComponerCompas(Compas compas, XmlNode nodoCompas)
        {
            //TODO: Refactor this to avoid mock.
            XmlNodeList listaNotas = nodoCompas.SelectNodes("./notas/nota");
            foreach (XmlNode nota in listaNotas)
            {
                List<Tono> tonoNota = new List<Tono>();
                XmlNodeList listaTonos = nota.SelectNodes("./tono");
                foreach (XmlNode tono in listaTonos)
                {
                    tonoNota.Add(ConvertidorStringAEntidadesMusicales.ConvertirATono(tono.Attributes["valor"].Value));
                }
                FiguraMusical figura = ConvertidorStringAEntidadesMusicales.ConvertirAFigura(nota.Attributes["forma"].Value);
                compas.AgregarNota(new Nota(tonoNota, figura));
            }
        }

        /*Obtiene el nodo que representa a la cancion a partir del XmlDocument*/
        private XmlNode ObtenerNodoCancion(XmlDocument documento)
        {
            return documento.FirstChild.FirstChild;
        }
    }
}
