﻿using System;
using System.Collections.Generic;
using AlgoHero.Interface;
using AlgoHero.MusicEntities.Excepciones;
using System.Collections.ObjectModel;

namespace AlgoHero.MusicEntities.Core
{
    public class Partitura
    {
        private readonly TiempoCancion tiempoCancion;

        private List<Compas> compases;
        
        public Partitura(TiempoCancion tiempoCancion)
        {
            this.tiempoCancion = tiempoCancion;
            this.compases = new List<Compas>();
        }

        public int CantidadCompases
        {
            get { return this.compases.Count; }
        }

        public double DuracionCompas
        {
            get { return this.tiempoCancion.DuracionCompas; }
        }

        public int CantidadBlancas
        {
            get { return this.tiempoCancion.CantidadBlancas; }
        }

        public void AgregarCompas(Compas compas)
        {
            if (!compas.EsCompleto)
            {
                throw new ExcepcionCompasInvalido();
            }
            this.compases.Add(compas);
        }

        public Compas ObtenerCompas(int index)
        {
            return this.compases[index];
        }

        public ReadOnlyCollection<Compas> ObtenerCompases()
        {
            return this.compases.AsReadOnly();
        }

        public IIterador<Nota> ObtenerIterador()
        {
            return new IteradorPartirura(this);
        }

        private class IteradorPartirura : IIterador<Nota>
        {
            private Partitura partitura;
            private Compas compasActual;
            private Nota notaActual;
            private int numeroCompas;
            private int numeroNota;
            private bool tieneSiguiente;
            
            public IteradorPartirura(Partitura partitura)
            {
                this.partitura = partitura;

                if (this.partitura.CantidadCompases > 0)
                {
                    this.tieneSiguiente = true;
                    this.numeroCompas = 0;
                    this.numeroNota = 0;
                    this.compasActual = this.partitura.ObtenerCompas(0);
                    this.notaActual = this.compasActual.ObtenerNota(0);
                }
            }

            public bool TieneSiguiente
            {
                get { return tieneSiguiente; }
            }

            public Nota Siguiente()
            {
                if (!this.tieneSiguiente)
                {
                    throw new InvalidOperationException();
                }
                Nota notaADevolver = this.notaActual;

                
                //TODO: Refactor this
                if (this.numeroNota + 1 < this.compasActual.CantidadNotas)
                {
                    this.numeroNota += 1;
                    this.notaActual = this.compasActual.ObtenerNota(this.numeroNota);
                }
                //se acabo el compas
                else if (this.numeroCompas + 1 < this.partitura.CantidadCompases)
                {
                    //TODO: Move to next compas
                    this.numeroNota = 0;
                    this.numeroCompas += 1;
                    this.compasActual = this.partitura.ObtenerCompas(this.numeroCompas);
                    this.notaActual = this.compasActual.ObtenerNota(this.numeroNota);
                }
                //se acabo la partitura
                else
                {
                    this.tieneSiguiente = false;
                }

                return notaADevolver;
            }
        }
    }
}
