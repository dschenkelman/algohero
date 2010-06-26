using System.Collections.Generic;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Interface.Enums;
using AlgoHero.Interface;

namespace AlgoHero.Juego.Servicios
{
    public class OrganizadorDeTonos
    {
        private IIterador<Nota> iter;

        /* Constructor. Recibe una Cancion y crea un OrganizadorDeTonos. Crea un iterador para la partitura de la cancion. */
        public OrganizadorDeTonos(Cancion cancion)
        {
            this.iter = cancion.Partitura.ObtenerIterador();
        }

        /* Este tono cuenta las apariciones de cada tono en la cancion, y devuelve una lista con los tonos y la cantidad de apariciones*/
        public List<TonoConCantidad> ContarApacionesDeCadaTono()
        {
            var diccionario = new Dictionary<Tono, int>();
            while (this.iter.TieneSiguiente)
            {
                Nota nota = iter.Siguiente();
                foreach (Tono tono in nota.Tonos)
                {
                    if (diccionario.ContainsKey(tono))
                    {
                        diccionario[tono] += 1;
                    }
                    else
                    {
                        diccionario[tono] = 1;
                    }
                }

            }
            List<TonoConCantidad> tonosConCantidad = new List<TonoConCantidad>();
            foreach (Tono tono in diccionario.Keys)
            {
                tonosConCantidad.Add(new TonoConCantidad(tono, diccionario[tono]));
            }
            return tonosConCantidad;
        }

        /* Este metodo recibe una lista de tonos con su cantidad de apariciones, la ordena, y la devuelve. */
        public List<Tono> OrdenarNotas(List<TonoConCantidad> lista)
        {
            lista.Sort(
                delegate(TonoConCantidad a, TonoConCantidad b)
                    {
                        if (a.Cantidad > b.Cantidad)
                            return -1;
                        else if (a.Cantidad < b.Cantidad)
                            return 1;
                        return 0;
                    });
            List<Tono> salida = new List<Tono>();
            foreach (var tono in lista)
            {
                salida.Add(tono.Tono);
            }
            return salida;
        }

        /* Clase interna. Representa un tono y una cantidad de apariciones del tono. */
        public class TonoConCantidad
        {
            /* Constructor. Crea un nuevo TonoConCantidad. */
            public TonoConCantidad(Tono tono, int cant)
            {
                this.Tono = tono;
                this.Cantidad = cant;
            }
            public Tono Tono { get; set; }
            public int Cantidad { get; set; }
        }

    }
}