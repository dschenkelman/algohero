using System;
using System.Collections.Generic;
using AlgoHero.Nivel.Intefaces;
using AlgoHero.Interface;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Nivel.Excepciones;
using AlgoHero.Nivel.Servicios;
using AlgoHero.Interface.Enums;

namespace AlgoHero.Nivel.Core
{
    public class EstrategiaNivelDificil : IEstrategiaNivel
    {
        private IIterador<Nota> iter;
        private Cancion cancion;
        private bool estado;

        public EstrategiaNivelDificil(Cancion cancion)
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
                controlador.ObtenerTecla(estado).AgregarTonoAsociado(tono);
                estado += 1;
                if (estado == 4)
                {
                    estado = 0;
                }
            }
        }
    }
}

