using System.Collections.Generic;
using System.Linq;
using AlgoHero.Interface;
using AlgoHero.Interface.Enums;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Player
{
    public class ControladorCancion
    {
        private Cancion cancion;
        private IControladorTeclas controladorTeclas;
        private IIterador<Nota> iteradorCancion;

        /*Crea un nuevo controlador cancion a partir de la cancion y el manager de teclas recibidos.*/
        public ControladorCancion(Cancion cancion, IControladorTeclas controlador)
        {
            this.cancion = cancion;
            this.controladorTeclas = controlador;
            this.iteradorCancion = cancion.Partitura.ObtenerIterador();
        }

        /*Devuelve si se encuentra al final del recorrido de la cancion.*/
        public bool EsFinalCancion
        {
            get { return !this.iteradorCancion.TieneSiguiente; }
        }

        /*Obtiene la siguiente nota de la cancion, siempre que los tonos asociados a la
         * misma esten registrados con alguna tecla. 
         * Si ninguno de los tonos esta registrado con ninguna tecla, devuelve null.*/
        public Nota ObtenerSiguienteNota()
        {
            Nota nota = this.iteradorCancion.Siguiente();
            IEnumerable<ITecla> teclas = controladorTeclas.ObtenerTeclas();
            foreach (var tecla in teclas)
            {
                IEnumerable<Tono> tonosAsociados = tecla.ObtenerTonosAsociados();
                foreach (var tono in nota.Tonos)
                {
                    if (tonosAsociados.Contains(tono))
                    {
                        return nota;
                    }
                }
            }
            return null;
            
        }
    }
}
