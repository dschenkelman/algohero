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
    public class EstrategiaNivelFacil : IEstrategiaNivel
    {
        private IIterador<Nota> iter;
        private Cancion cancion;
        private int estado;
        private int max_estado;

        public EstrategiaNivelFacil(Cancion cancion) 
        {
            this.estado = 0;
            this.cancion = cancion;
            this.max_estado = 3;
            this.iter = cancion.Partitura.ObtenerIterador();
        }        

        public bool EsFinalCancion()
        {
            return (!this.iter.TieneSiguiente);
        }

        public Nota ObtenerSiguienteNota()
        {
            if ((this.estado % this.max_estado) == 0)
            {
                this.estado = 0;
            }
            if (this.EsFinalCancion())
            {
                throw new ExcepcionFinalDeCancion();
            }
            else
            {
                if(this.estado == 0)
                {
                    this.estado += 1;
                    return this.iter.Siguiente();
                }
                else
                {
                    Nota nota = this.iter.Siguiente();
                    this.estado += 1;
                    //return new Nota(Tono.Silencio, nota.Figura);
                    return null;
                }
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
                if (estado == 2)
                {
                    estado = 0;
                }
            }
        }
    }
}
