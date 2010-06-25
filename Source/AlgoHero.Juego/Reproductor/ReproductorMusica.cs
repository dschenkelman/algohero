using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AlgoHero.Juego.Intefaces;

namespace AlgoHero.Juego.Reproductor
{
    public class ReproductorMusica : IReproductorMusica
    {

        public ReproductorMusica()
        {
            this.Reproduciendo = false;
        }

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

        /* Este metodo comienza a reproducir la cancion*/
        public void ReproducirCancion(string pathCancion)
        {
            if(!File.Exists(pathCancion))
            {
                throw new ArgumentException();
            }

            Play(pathCancion, new System.IntPtr(), PlaySoundFlags.SND_ASYNC);
            this.Reproduciendo = true;
        }

        /* Este metodo detiene la reproduccion*/
        public void DetenerReproduccion()
        {
            Play(null, new System.IntPtr(), PlaySoundFlags.SND_ASYNC);
            this.Reproduciendo = false;
        }

        public bool Reproduciendo { get; private set; }

    }
}
