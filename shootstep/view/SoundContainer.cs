using System;
using System.IO;
using NAudio.Wave;

namespace shootstep.view
{
    public class SoundContainer
    {
        private static Stream[] _audioContainer;
        private WaveOutEvent _soundOut;
        private Stream _currentSound;

        public void Init(params Stream[] audio)
        {
            _audioContainer = audio;
            _soundOut = new WaveOutEvent();
        }

        public void Play()
        {
            _currentSound = GetNext();
            _soundOut.Init(new WaveFileReader(_currentSound));
            _soundOut.Play();
        }

        public void PlayLooping()
        {
            _currentSound = GetNext();
            _soundOut.Init(new LoopStream(new WaveFileReader(_currentSound)));
            _soundOut.Play();
        }

        public void Stop()
        {
            _soundOut.Stop();
            _currentSound.Position = 0;
        }

        public void Pause()
        {
            _soundOut.Pause();

            if (_currentSound.Length == _currentSound.Position)
                _currentSound.Position = 0;
        }

        private Stream GetNext()
        {
            return _audioContainer[(new Random()).Next(0, _audioContainer.Length)];
        }
    }

    class LoopStream : WaveStream
    {
        WaveStream sourceStream;

        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
            this.EnableLooping = true;
        }

        public bool EnableLooping { get; set; }

        public override WaveFormat WaveFormat => sourceStream.WaveFormat;

        public override long Length => sourceStream.Length;

        public override long Position
        {
            get => sourceStream.Position;
            set => sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                var bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping)
                        break;
                    // loop
                    sourceStream.Position = 0;
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }

}
