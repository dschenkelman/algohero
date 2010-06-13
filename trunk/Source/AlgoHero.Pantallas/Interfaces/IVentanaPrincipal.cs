using System.Windows.Controls;
namespace AlgoHero.Pantallas.Interfaces
{
    public interface IVentanaPrincipal
    {
        Control ObtenerContenido();
        void CambiarContenido(Control control);
    }
}
