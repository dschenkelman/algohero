namespace AlgoHero.Interface
{
    public interface IIterador<T>
    {
        bool TieneSiguiente { get; }
        T Siguiente();
    }
}
