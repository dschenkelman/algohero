using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AlgoHero.Juego.Reproductor
{
    public class ReproductorMusica
    {
        private string pathCancion;

        [System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        private static extern bool Play(string szSound, System.IntPtr hMod, PlaySoundFlags flags);

        [System.Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }

        /* Este metodo recibe un string con el path de la cancion y lo asigna al atributo pathCancion de la clase*/
        public void ElegirCancion(string pathArchivo)
        {
            this.pathCancion = pathArchivo;
        }

        /* Este metodo comienza a reproducir la cancion*/
        public void ReproducirCancion()
        {
            if(this.pathCancion == null)
            {
                throw new ArgumentException();
            }

            Play(this.pathCancion, new System.IntPtr(), PlaySoundFlags.SND_ASYNC);
        }

        /* Este metodo detiene la reproduccion*/
        public void DetenerReproduccion()
        {
            Play(null, new System.IntPtr(), PlaySoundFlags.SND_ASYNC);
        }

    }
}
