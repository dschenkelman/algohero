using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfApplication2
{
    public class WindowViewModel : INotifyPropertyChanged
    {
        public WindowViewModel()
        {
            this.multiplicador = 1;
        }

        private int multiplicador;

        public int Multiplicador 
        {
            get
            {
                return multiplicador;
            }
            set
            {
                if (multiplicador != value)
                {
                    this.multiplicador = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Multiplicador"));
                }
            } 
        }

        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
