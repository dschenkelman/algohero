using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using AlgoHero.MusicEntities.Core;
using System.Linq;
using AlgoHero.MusicEntities.Enums;
using AlgoHero.Player.Interfaces;
using AlgoHero.Interface;

namespace AlgoHero.Player
{
    public class ManagerTeclas : IManagerTeclas
    {
        private List<Tecla> teclas;

        public ManagerTeclas(Nivel nivel)
        {
            this.teclas = new List<Tecla>();
            AgregarTeclas(nivel);
        }

        public void AsignarTonosATeclas(Cancion cancion)
        {
            var tonosConCantidades = this.ObtenerDiccionarioDeTodosLosTonos();
            ObtenerCantidadPorNota(cancion, tonosConCantidades);
            List<TonoConCantidad> tonosOrdenados = OrdenarNotas(tonosConCantidades);
            AsignarTonosATeclasInterno(tonosOrdenados);
        }

        public int CantidadTeclas
        {
            get { return this.teclas.Count; }
        }

        public Tecla ObtenerTecla(int index)
        {
            return this.teclas[index];
        }

        public IEnumerable<Tecla> ObtenerTeclas()
        {
            return this.teclas.AsEnumerable();
        }

        private void AgregarTeclas(Nivel nivel)
        {
            switch (nivel)
            {
                case Nivel.Facil:
                    this.AgregarTeclasNivelFacil();
                    break;
                case Nivel.Medio:
                    this.AgregarTeclasNivelMedio();
                    break;
                case Nivel.Dificil:
                    this.AgregarTeclasNivelDificil();
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private void AgregarTeclasNivelFacil()
        {
            this.teclas.Add(new Tecla(Key.A));
            this.teclas.Add(new Tecla(Key.S));
        }

        private void AgregarTeclasNivelMedio()
        {
            this.AgregarTeclasNivelFacil();
            this.teclas.Add(new Tecla(Key.K));
        }

        private void AgregarTeclasNivelDificil()
        {
            this.AgregarTeclasNivelMedio();
            this.teclas.Add(new Tecla(Key.L));
        }

        private Dictionary<Tono, TonoConCantidad> ObtenerDiccionarioDeTodosLosTonos()
        {
            var tonosConCantidades = new Dictionary<Tono, TonoConCantidad>();
            for (int i = 1; i < 12; i++)
            {
                Tono tono = (Tono)i;
                tonosConCantidades.Add(tono, new TonoConCantidad() { Tono = tono, Cantidad = 0 });
            }

            return tonosConCantidades;
        }

        private List<TonoConCantidad> OrdenarNotas(Dictionary<Tono, TonoConCantidad> tonosConCantidades)
        {
            List<TonoConCantidad> tonosOrdenados = tonosConCantidades.Values.ToList();
            tonosOrdenados.Sort(
                delegate(TonoConCantidad a, TonoConCantidad b)
                {
                    if (a.Cantidad > b.Cantidad)
                        return -1;
                    else if (a.Cantidad < b.Cantidad)
                        return 1;
                    return 0;
                });
            return tonosOrdenados;
        }

        private void ObtenerCantidadPorNota(Cancion cancion, Dictionary<Tono, TonoConCantidad> tonosConCantidades)
        {
            IEnumerable<Compas> compases = cancion.Partitura.ObtenerCompases();
            foreach (var compas in compases)
            {
                IEnumerable<Nota> notas = compas.ObtenerNotas();
                foreach (var nota in notas)
                {
                    foreach (var tono in nota.Tonos)
                    {
                        TonoConCantidad t = tonosConCantidades[tono];
                        t.Cantidad += 1;
                    }
                }
            }
        }

        private void AsignarTonosATeclasInterno(List<TonoConCantidad> tonosOrdenados)
        {
            for (int i = 0; i < tonosOrdenados.Count; i++)
            {
                var tonoConCantidad = tonosOrdenados[i];
                if (tonoConCantidad.Cantidad != 0)
                {
                    this.ObtenerTecla(i % this.CantidadTeclas).AgregarTonoAsociado(tonoConCantidad.Tono);
                }
            }
        }
       
        private class TonoConCantidad
        {
            public Tono Tono { get; set; }
            public int Cantidad { get; set; }
        }
    }
}
