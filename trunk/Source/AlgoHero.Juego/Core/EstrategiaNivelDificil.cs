﻿using System.Collections.Generic;
using AlgoHero.Juego.Excepciones;
using AlgoHero.Juego.Intefaces;
using AlgoHero.Interface;
using AlgoHero.Juego.Servicios;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Interface.Enums;

namespace AlgoHero.Juego.Core
{
    public class EstrategiaNivelDificil : IEstrategiaNivel
    {
        private IIterador<Nota> iter;
        private Cancion cancion;

        public EstrategiaNivelDificil()
        {
        }

        public void AsignarCancion(Cancion cancion)
        {
            this.cancion = cancion;
            this.iter = cancion.Partitura.ObtenerIterador();
        }

        public bool EsFinalCancion()
        {
            return (!this.iter.TieneSiguiente);
        }

        public Nota ObtenerSiguienteNota()
        {
            if (this.EsFinalCancion())
            {
                throw new ExcepcionFinalDeCancion();
            }
            else
            {
                return this.iter.Siguiente();
            }
        }

        public void AsignarTonos(IControladorTeclas controlador)
        {
            OrganizadorDeTonos organizador = new OrganizadorDeTonos(this.cancion);
            List<Tono> listaOrdenada = organizador.OrdenarNotas(organizador.ContarApacionesDeCadaTono());
            int estado = 0;

            foreach (Tono tono in listaOrdenada)
            {
                controlador.ObtenerTecla((estado % 4) + 1).AgregarTonoAsociado(tono);
                estado += 1;
            }
        }
    }
}