using System;
using System.ComponentModel;
using System.Windows.Controls;
using AlgoHero.Pantallas.MenuPrincipal;

namespace AlgoHero.Pantallas
{
    public class VentanaPrincipalViewModel : INotifyPropertyChanged
    {
        public VentanaPrincipalViewModel()
        {
            
        }

        public VentanaPrincipalViewModel(Control contenido)
        {
            this.contenido = contenido;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private Control contenido;
       

        public Control Contenido
        {
            get { return contenido; }
            set 
            {
                if (value != contenido)
                {
                    this.contenido = value;
                    PropertyChangedEventHandler handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("Contenido"));
                    }
                }
            }

        }
        
        
    }
}
