using System;
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
        
        /*Crea una nueva partitura con el tiempo de cancion recibido.*/
        public Partitura(TiempoCancion tiempoCancion)
        {
            this.tiempoCancion = tiempoCancion;
            this.compases = new List<Compas>();
        }

        /*Devuelve la cantidad de compases de la partitura.*/
        public int CantidadCompases
        {
            get { return this.compases.Count; }
        }

        public TiempoCancion TiempoCancion
        {
            get { return this.tiempoCancion; }
        }

        /*Agrega un compas a la partitura. Si el compas no esta completo lanza una excepcion ExcepcionCompasInvalido.*/
        public void AgregarCompas(Compas compas)
        {
            if (!compas.EsCompleto)
            {
                throw new ExcepcionCompasInvalido();
            }
            this.compases.Add(compas);
        }

        /*Devuelve un compas que esta en la posicion que recibe como parametro.*/
        public Compas ObtenerCompas(int index)
        {
            return this.compases[index];
        }

        /*Devuleve todos los compases de la partitura.*/
        public ReadOnlyCollection<Compas> ObtenerCompases()
        {
            return this.compases.AsReadOnly();
        }

        /*Devuelve un iterador para las notas de la partitura.*/
        public IIterador<Nota> ObtenerIterador()
        {
            return new IteradorPartitura(this);
        }

        private class IteradorPartitura : IIterador<Nota>
        {

            #region Atributos
            private Partitura partitura;
            private Compas compasActual;
            private Nota notaActual;
            private int numeroCompas;
            private int numeroNota;
            private bool tieneSiguiente;
            #endregion

            public IteradorPartitura(Partitura partitura)
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

            #region Interfaz Iterador
            /*Devuelve si la partitura tiene una nota siguiente.*/
            public bool TieneSiguiente
            {
                get { return tieneSiguiente; }
            }

            /*Devuelve la siguiente nota de la partitura y avanza.*/
            public Nota Siguiente()
            {
                if (!this.tieneSiguiente)
                {
                    throw new InvalidOperationException();
                }
                Nota notaADevolver = this.notaActual;

                AvanzarNota();

                return notaADevolver;
            }
            #endregion

            #region Private
            private void AvanzarNota()
            {
                if (!this.EnFinDeCompas())
                {
                    AvanzarNotaEnMismoCompas();
                }
                else if (!this.EnFinDePartitura())
                {
                    //TODO: Move to next compas
                    AvanzarNotaEnCompasSiguiente();
                }
                else
                {
                    this.tieneSiguiente = false;
                }
            }

            private bool EnFinDePartitura()
            {
                return this.numeroCompas + 1 >= this.partitura.CantidadCompases;
            }

            private bool EnFinDeCompas()
            {
                return this.numeroNota + 1 >= this.compasActual.CantidadNotas;
            }

            private void AvanzarNotaEnCompasSiguiente()
            {
                this.numeroNota = 0;
                this.numeroCompas += 1;
                this.compasActual = this.partitura.ObtenerCompas(this.numeroCompas);
                this.notaActual = this.compasActual.ObtenerNota(this.numeroNota);
            }

            private void AvanzarNotaEnMismoCompas()
            {
                this.numeroNota += 1;
                this.notaActual = this.compasActual.ObtenerNota(this.numeroNota);
            }
            #endregion

        }
    }
}
