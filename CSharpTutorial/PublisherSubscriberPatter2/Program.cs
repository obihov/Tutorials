using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PublisherSubscriberPatter2
{
    public class Video
    {
        public string Title { get; set; }
    }

    public class VideoArgs : EventArgs
    {
        public Video Video { get; set; }
    }

    public delegate void VideolayerEventHandler(object source, VideoArgs args);

    public class VideoPlayerPublisher
    {
        private Video Video { get; set; }

        public VideoPlayerPublisher(Video video)
        {
            this.Video = video;
        }

        public event VideolayerEventHandler OnPlay = delegate { };
        public event VideolayerEventHandler OnPause = delegate { };
        public event VideolayerEventHandler OnStop = delegate { };

        public void Player(string playerState)
        {
            switch (playerState.ToLower())
            {
                case "p": { Play(); } break;
                case "spacebar": { Pause(); } break;
                case "s": { Stop(); } break;
                default: { Console.WriteLine("Cant recognize input. Press P to play, Spacebar to Pause, S to Stop."); } break;
            }
        }

        protected virtual void Play()
        {
            OnPlay(this, new VideoArgs { Video = this.Video });
        }

        protected virtual void Pause()
        {
            OnPause(this, new VideoArgs { Video = this.Video });
        }

        protected virtual void Stop()
        {
            OnStop(this, new VideoArgs { Video = this.Video });
        }
    }

    public class VideoPlayerSubscriber
    {
        public void OnPlay(object source, VideoArgs e)
        {
            Console.WriteLine($"Video, {e.Video.Title} is now playing.");
        }

        public void OnPause(object source, VideoArgs e)
        {
            Console.WriteLine($"Video, {e.Video.Title} is now paused.");
        }

        public void OnStop(object source, VideoArgs e)
        {
            Console.WriteLine($"Video, {e.Video.Title} is now stopped.");
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            VideoPlayerPublisher videoPlayer = new VideoPlayerPublisher(new Video { Title = "Titanic" });
            VideoPlayerSubscriber playerSubscriber = new VideoPlayerSubscriber();

            //First subscribe to OnPlay, OnPause, and OnStop events
            videoPlayer.OnPlay += playerSubscriber.OnPlay;
            videoPlayer.OnPause += playerSubscriber.OnPause;
            videoPlayer.OnStop += playerSubscriber.OnStop;

            //Perform tasks that will allow user to pause, play and stop movie.
            string userInput = "p";
            bool applicationAlive = true;
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //CancellationToken token = cancellationTokenSource.Token;
            Task completed = Task.Run(() =>
            {
                while (applicationAlive)
                {
                    videoPlayer.Player(userInput);
                    Thread.Sleep(3000);
                }
                videoPlayer.Player("s");
            });

            Console.WriteLine("Type any of the below keys:\nP:\t\tTo play video.\nSpacebar:\tTo Pause video.\nS:\t\tTo Stop video.");
            Console.WriteLine();

            while (applicationAlive)
            {
                userInput = Console.ReadKey().Key.ToString();
                if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "s")
                {
                    applicationAlive = false;
                    //cancellationTokenSource.Cancel();
                }
            }

            completed.Wait();

            Console.WriteLine("Press any key to exit Video Player.");
            Console.ReadLine();
        }
    }
}
