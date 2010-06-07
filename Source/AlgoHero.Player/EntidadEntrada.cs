namespace AlgoHero.Player
{
    public class EntidadEntrada
    {
        /* Constructor. Crea una instancia de EntidadEntrada y le asigna un codigo recibido por parametro*/
        public EntidadEntrada(int codigo)
        {
            this.Codigo = codigo;
        }

        public int Codigo { get; private set; }

    }
}
