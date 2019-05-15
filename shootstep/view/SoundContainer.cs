using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shootstep.view
{
    class SoundContainer
    {
        private static Stream[] _audioContainer;

        public static void Init(params Stream[] audio)
        {
            _audioContainer = audio;
        }

        public static Stream GetNext()
        {
            return _audioContainer[(new Random()).Next(0, _audioContainer.Length)];
        }
    }
}
