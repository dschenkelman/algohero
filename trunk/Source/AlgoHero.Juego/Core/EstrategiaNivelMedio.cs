using System.Collections.Generic;
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

        public EstrategiaNivelMedio(Cancion cancion)
        {
            this.estado = true;
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
                if (this.estado)
                {
                    this.estado = (!this.estado);
                    return this.iter.Siguiente();
                }
                else
                {
                    Nota nota = this.iter.Siguiente();
                    this.estado = (!this.estado);
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
                if (estado == 3)
                {
                    estado = 0;
                }
            }
        }
    }
}