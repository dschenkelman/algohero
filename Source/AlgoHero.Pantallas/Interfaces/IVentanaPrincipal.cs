using System.Windows.Controls;
namespace AlgoHero.Pantallas.Interfaces
{
    public interface IVentanaPrincipal
    {
        object ObtenerContenido();
        void CambiarContenido(object control);
    }
}
