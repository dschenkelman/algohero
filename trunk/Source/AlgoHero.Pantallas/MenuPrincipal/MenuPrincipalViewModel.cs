using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AlgoHero.Files.Interfaces;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.MenuPrincipal
{
    public class MenuPrincipalViewModel
    {
        private readonly IProveedorCancion proveedorCancion;

        public MenuPrincipalViewModel(IProveedorCancion proveedorCancion)
        {
            this.proveedorCancion = proveedorCancion;
            this.Canciones = new ObservableCollection<Cancion>();
            AgregarCancionesDeProveedor();
        }

        private void AgregarCancionesDeProveedor()
        {
            IEnumerable<Cancion> canciones = this.proveedorCancion.ObtenerCancionesDirectorio(@"C:\");
            foreach (var cancion in canciones)
            {
                this.Canciones.Add(cancion);
            }
        }

        public ObservableCollection<Cancion> Canciones
        {
            get; private set;
        }
    }
}
