﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using AlgoHero.Files.Interfaces;
using AlgoHero.Juego.Core;
using AlgoHero.Juego.Intefaces;
using AlgoHero.MusicEntities.Core;
using System.IO;
using System;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace AlgoHero.Pantallas.MenuPrincipal
{
    public class MenuPrincipalViewModel
    {
        private readonly IProveedorCancionesDirectorio proveedorCanciones;
        private readonly IProveedorNiveles proveedorNiveles;

        public MenuPrincipalViewModel(IProveedorCancionesDirectorio proveedorCanciones, IProveedorNiveles proveedorNiveles)
        {
            this.proveedorCanciones = proveedorCanciones;
            this.proveedorNiveles = proveedorNiveles;
            
            this.Canciones = new ObservableCollection<Cancion>();
            this.Niveles = new ObservableCollection<Nivel>();
            this.ComandoEmpezarCancion = new DelegateCommand<object>(this.EmpezarCancion, this.PuedeEmpezarCancion);

            AgregarCancionesDeProveedor();
            AgregarNiveles();
        }

        private Cancion cancionActual;

        public Cancion CancionActual
        {
            get { return cancionActual; }
            set
            {
                if (value != cancionActual)
                {
                    cancionActual = value;
                    this.ComandoEmpezarCancion.RaiseCanExecuteChanged();
                }
            }
        }

        private Nivel nivelActual;

        public Nivel NivelActual
        {
            get { return nivelActual; }
            set
            {
                if (value != nivelActual)
                {
                    nivelActual = value;
                    this.ComandoEmpezarCancion.RaiseCanExecuteChanged();
                }
                
            }
        }

        public DelegateCommand<object> ComandoEmpezarCancion
        {
            get; private set;
        }

        public ObservableCollection<Cancion> Canciones
        {
            get;
            private set;
        }

        public ObservableCollection<Nivel> Niveles
        {
            get;
            private set;
        }

        private void AgregarNiveles()
        {
            this.Niveles = this.proveedorNiveles.ObtenerNiveles(); 
        }

        private void AgregarCancionesDeProveedor()
        {
            //TODO: Cambiar esto para poder configurar de donde vienen las canciones
            string pathCanciones = Path.Combine(Environment.CurrentDirectory, "Canciones");
            IEnumerable<Cancion> canciones = this.proveedorCanciones.ObtenerCancionesDirectorio(pathCanciones);
            foreach (var cancion in canciones)
            {
                this.Canciones.Add(cancion);
            }
        }

        private bool PuedeEmpezarCancion(object obj)
        {
            return (this.NivelActual != null && this.CancionActual != null);
        }

        private void EmpezarCancion(object obj)
        {
            //MessageBox.Show("Empezar cancion");
        }

    }
}
