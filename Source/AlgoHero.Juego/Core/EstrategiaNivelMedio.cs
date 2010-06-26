﻿using System.Collections.Generic;
using AlgoHero.Juego.Excepciones;
using AlgoHero.Juego.Intefaces;
using AlgoHero.Interface;
using AlgoHero.Juego.Servicios;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Interface.Enums;

namespace AlgoHero.Juego.Core
{
    public class EstrategiaNivelMedio : IEstrategiaNivel
    {
        private IIterador<Nota> iter;
        private Cancion cancion;
        private bool estado;

        public EstrategiaNivelMedio()
        {
            this.estado = true;
        }

        /* Este metodo recibe por parametro una cancion, la asigna al atributo cancion y crea un iterador para la cancion.*/
        public void AsignarCancion(Cancion cancion)
        {
            this.cancion = cancion;
            this.iter = cancion.Partitura.ObtenerIterador();
        }


        /* Este metodo verifica si la cancion llego al final. */
        public bool EsFinalCancion()
        {
            return (!this.iter.TieneSiguiente);
        }

        /* Este metodo obtiene la siguiente nota de la cancion. */
        public Nota ObtenerSiguienteNota()
        {
            if (this.EsFinalCancion())
            {
                throw new ExcepcionFinalDeCancion();
            }
            else
            {
                if (this.estado)
                {
                    this.estado = (!this.estado);
                    return this.iter.Siguiente();
                }
                else
                {
                    Nota nota = this.iter.Siguiente();
                    this.estado = (!this.estado);
                    return new Nota(Tono.Silencio, nota.Figura);
                }
            }
        }

        /* Este metodo recibe un IControladorTeclas por parametro, y asigna los tonos a las difernetes teclas. */
        public void AsignarTonos(IControladorTeclas controlador)
        {
            foreach (ITecla tecla in controlador.ObtenerTeclas())
            {
                tecla.ResetearTonosAsignados();
            }
            OrganizadorDeTonos organizador = new OrganizadorDeTonos(this.cancion);
            List<Tono> listaOrdenada = organizador.OrdenarNotas(organizador.ContarApacionesDeCadaTono());
            int estado = 0;

            foreach (Tono tono in listaOrdenada)
            {
                if (tono != Tono.Silencio)
                {
                    controlador.ObtenerTecla((estado % 3) + 1).AgregarTonoAsociado(tono);
                    estado += 1;
                }
            }
        }
    }
}