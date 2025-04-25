using NAudio.Wave;

namespace week3
{
    public class SoundManager
    {
        // ========================================
        // 🔊 타자기 사운드용 재생 콘터럴러
        // ========================================
        private IWavePlayer? typingPlayer;
        private AudioFileReader? typingAudio;

        public void ResumeTypingSound()
        {
            if (typingPlayer == null || typingAudio == null)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "typing.wav");

                if (File.Exists(path))
                {
                    typingAudio = new AudioFileReader(path);
                    typingPlayer = new WaveOutEvent();
                    typingPlayer.Init(typingAudio);
                }
                else
                {
                    Console.WriteLine("[사운드 오류] typing.wav 파일을 찾을 수 없습니다.");
                    return;
                }
            }

            if (typingPlayer?.PlaybackState != PlaybackState.Playing)
            {
                typingPlayer?.Play();
            }
        }

        public void PauseTypingSound()
        {
            if (typingPlayer?.PlaybackState == PlaybackState.Playing)
            {
                typingPlayer.Pause();
            }
        }

        public void StopTypingSound()
        {
            typingPlayer?.Stop();
            typingAudio?.Dispose();
            typingPlayer?.Dispose();
            typingAudio = null;
            typingPlayer = null;
        }

        // ========================================
        // 🚫 효과음 단발성 재생
        // ========================================
        public void PlayOnce(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", fileName);

            if (!File.Exists(path))
            {
                Console.WriteLine($"[사운드 오류] 파일 없음: {fileName}");
                return;
            }

            var audio = new AudioFileReader(path);
            var player = new WaveOutEvent();

            player.Init(audio);
            player.Play();

            player.PlaybackStopped += (sender, args) =>
            {
                audio.Dispose();
                player.Dispose();
            };
        }

        // ========================================
        // 🐦 사운드 - 루프 재생 (페이드 인 포함)
        // ========================================
        private IWavePlayer? birdPlayer;
        private AudioFileReader? birdAudio;

        public void PlayBirdLoop()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "bird.wav");

            if (!File.Exists(path))
            {
                Console.WriteLine("[사운드 오류] bird.wav 파일을 찾을 수 없습니다.");
                return;
            }

            StopBirdSound();

            birdAudio = new AudioFileReader(path);
            birdPlayer = new WaveOutEvent();
            birdPlayer.Init(birdAudio);
            birdPlayer.Play();
        }

        public void StopBirdSound()
        {
            birdPlayer?.Stop();
            birdAudio?.Dispose();
            birdPlayer?.Dispose();
            birdAudio = null;
            birdPlayer = null;
        }

        public async void PlayBirdLoopFadeIn(float targetVolume = 1.0f, int durationMs = 2000)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "bird.wav");

            if (!File.Exists(path))
            {
                Console.WriteLine("[사운드 오류] bird.wav 파일을 찾을 수 없습니다.");
                return;
            }

            StopBirdSound();

            birdAudio = new AudioFileReader(path);
            birdAudio.Volume = 0f;
            birdPlayer = new WaveOutEvent();
            birdPlayer.Init(birdAudio);
            birdPlayer.Play();

            await Task.Run(() =>
            {
                float currentVolume = 0f;
                float step = targetVolume / (durationMs / 50f);

                while (currentVolume < targetVolume)
                {
                    currentVolume += step;
                    if (currentVolume > targetVolume)
                        currentVolume = targetVolume;

                    birdAudio.Volume = currentVolume;
                    Thread.Sleep(50);
                }
            });
        }

        // ========================================
        // 📳 진동 사운드 반복 재생
        // ========================================
        private IWavePlayer? vibrationPlayer;
        private AudioFileReader? vibrationAudio;
        private CancellationTokenSource? vibrationTokenSource;

        public void PlayVibrationLoopUntilEnter(int durationMs = 6000)
        {
            StopVibration();

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "smartphone_vibrating.wav");
            if (!File.Exists(path))
            {
                Console.WriteLine("[사운드 오류] smartphone_vibrating.wav 파일을 찾을 수 없습니다.");
                return;
            }

            vibrationTokenSource = new CancellationTokenSource();
            var token = vibrationTokenSource.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    vibrationAudio = new AudioFileReader(path);
                    vibrationPlayer = new WaveOutEvent();
                    vibrationPlayer.Init(vibrationAudio);
                    vibrationPlayer.Play();

                    await Task.Delay(durationMs, token);

                    vibrationPlayer.Stop();
                    vibrationAudio.Dispose();
                    vibrationPlayer.Dispose();
                }
            }, token);
        }

        public void StopVibration()
        {
            vibrationTokenSource?.Cancel();
            vibrationPlayer?.Stop();
            vibrationAudio?.Dispose();
            vibrationPlayer?.Dispose();
            vibrationAudio = null;
            vibrationPlayer = null;
            vibrationTokenSource = null;
        }

        // ========================================
        // 🔁 배경음 루프 재생 콘터럴 (intro, noise, etc.)
        // ========================================
        private IWavePlayer? loopPlayer;
        private WaveStream? loopAudio;

        public void PlayLoop(string fileName)
        {
            StopCurrentLoop();

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", fileName);
            if (!File.Exists(path))
            {
                Console.WriteLine($"[사운드 오류] 파일 없음: {fileName}");
                return;
            }

            var fileReader = new AudioFileReader(path);
            loopAudio = new LoopStream(fileReader); // 💡 중요: LoopStream으로 래핑
            loopPlayer = new WaveOutEvent();
            loopPlayer.Init(loopAudio); // ✅ 반드시 Init 호출 후
            loopPlayer.Play();          // ✅ Play 실행
        }


        public void StopCurrentLoop()
        {
            loopPlayer?.Stop();
            loopAudio?.Dispose();
            loopPlayer?.Dispose();
            loopAudio = null;
            loopPlayer = null;
        }

        // ========================================
        // 💥 충격 효과음 전용
        // ========================================
        public void PlayBodyEffect()
        {
            PlayOnce("body.wav");
        }

        // ========================================
        // 🎵 공포 배경음 전용
        // ========================================
        public void PlayHorrorLoop()
        {
            PlayLoop("intro.wav");
        }
    }

    // 🔁 반복 재생을 위한 래퍼 클래스
    public class LoopStream : WaveStream
    {
        private readonly WaveStream sourceStream;

        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
            this.EnableLooping = true;
        }

        public bool EnableLooping { get; set; } = true;
        public override WaveFormat WaveFormat => sourceStream.WaveFormat;
        public override long Length => sourceStream.Length;

        public override long Position
        {
            get => sourceStream.Position;
            set => sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping)
                        break;

                    sourceStream.Position = 0;
                }
                totalBytesRead += bytesRead;
            }

            return totalBytesRead;
        }
    }

}