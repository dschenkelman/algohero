using System;
using System.Collections.Generic;
using System.Linq;
using AlgoHero.Interface;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Player.Interfaces;
using AlgoHero.MusicEntities.Enums;

namespace AlgoHero.Player
{
    public class ControladorCancion
    {
        private Cancion cancion;
        private IManagerTeclas managerTeclas;
        private IIterador<Nota> iteradorCancion;

        /*Crea un nuevo controlador cancion a partir de la cancion y el manager de teclas recibidos.*/
        public ControladorCancion(Cancion cancion, IManagerTeclas manager)
        {
            this.cancion = cancion;
            this.managerTeclas = manager;
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
            IEnumerable<Tecla> teclas = managerTeclas.ObtenerTeclas();
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
