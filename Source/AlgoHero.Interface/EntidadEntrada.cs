namespace AlgoHero.Interface
{
    public class EntidadEntrada
    {
        /* Constructor. Crea una entida de entrada y le asigna un codigo. */
        public EntidadEntrada(int codigo)
        {
            this.Codigo = codigo;
        }

        public int Codigo { get; private set; }

    }
}
