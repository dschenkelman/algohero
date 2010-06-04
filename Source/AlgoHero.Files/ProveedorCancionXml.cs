using System;
using AlgoHero.Files.Interfaces;
using AlgoHero.MusicEntities.Core;
using System.Xml;
using AlgoHero.MusicEntities.Enums;

namespace AlgoHero.Files
{
    public class ProveedorCancionXml : IProveedorCancion
    {
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

        private TiempoCancion CrearTiempoCancion(XmlDocument documento)
        {
            XmlNode nodoTiempoCancion = documento.SelectSingleNode("/xml/cancion/tiempo");
            double duracionCompas = Convert.ToDouble(nodoTiempoCancion.Attributes["duracionCompasSegundos"].Value);
            int cantidadBlancas = Convert.ToInt32(nodoTiempoCancion.Attributes["cantidadBlancas"].Value);

            return new TiempoCancion(duracionCompas, cantidadBlancas);
        }

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

        private void ComponerCompas(Compas compas, XmlNode nodoCompas)
        {
            //TODO: Refactor this to avoid mock.
            XmlNodeList listaNotas = nodoCompas.SelectNodes("./notas/nota");
            foreach (XmlNode nota in listaNotas)
            {
                Tono tonoNota = ConvertidorStringAEntidadesMusicales.ConvertirATono(nota.Attributes["tono"].Value);
                FiguraMusical figura = ConvertidorStringAEntidadesMusicales.ConvertirAFigura(nota.Attributes["forma"].Value);
                compas.AgregarNota(new Nota(tonoNota, figura));
            }
        }

        private XmlNode ObtenerNodoCancion(XmlDocument documento)
        {
            return documento.FirstChild.FirstChild;
        }
    }
}
