using System.Collections.Generic;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Interface.Enums;
using AlgoHero.Interface;

namespace AlgoHero.Juego.Servicios
{
    public class OrganizadorDeTonos
    {
        private IIterador<Nota> iter;

        public OrganizadorDeTonos(Cancion cancion)
        {
            this.iter = cancion.Partitura.ObtenerIterador();
        }

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

        public class TonoConCantidad
        {
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