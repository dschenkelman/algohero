using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoHero.Files.Interfaces;
using AlgoHero.MusicEntities.Core;
using System.Xml;

namespace AlgoHero.Files
{
    public class ProveedorCancionXml : IProveedorCancion
    {
        #region IProveedorCancion Members

        public Cancion ObtenerCancion(string path)
        {
            XmlDocument documento = new XmlDocument();
            documento.Load(path);
            XmlNode nodoCancion = documento.FirstChild.FirstChild;
            string nombreCancion = nodoCancion.Attributes["nombre"].Value;
            string autorCancion = nodoCancion.Attributes["autor"].Value;

            return new Cancion(nombreCancion, autorCancion);
        }

        #endregion
    }
}
