using System.Windows.Threading;

namespace ZomBit.Internal
{
	internal class TimingManager
	{
		public static TimeSpan MaxFrameRate { get; } = TimeSpan.FromMilliseconds(1000 / 60.0);

		internal long DeltaTime { get; private set; }

		private readonly Stopwatch _stopwatch = new();

		/// <summary>
		/// The event that gets called every frame
		/// </summary>
		/// <remarks>
		/// Since this is the thread that the game loop runs on, it is important to keep this event as small as possible.
		/// </remarks>
		public event EventHandler Update
		{
			add => _tickTimer.Tick += value;
			remove => _tickTimer.Tick -= value;
		}

		public event EventHandler UpdateFps
		{
			add => _fpsTimer.Tick += value;
			remove => _fpsTimer.Tick -= value;
		}

		public double Fps { get; private set; }

		/// <summary>
		/// The timer that ticks every frame (60 fps)
		/// </summary>
		private readonly DispatcherTimer _tickTimer = new(MaxFrameRate, DispatcherPriority.Send,
			(_, _) => { }, Dispatcher.CurrentDispatcher);

		private readonly DispatcherTimer _fpsTimer = new(TimeSpan.FromSeconds(1), DispatcherPriority.Background,
			(_, _) => { }, Dispatcher.CurrentDispatcher);

		internal TimingManager()
		{
			_stopwatch.Start();
			long lastTime = _stopwatch.ElapsedMilliseconds;

			Update += (_, _) =>
			{
				long currentTime = _stopwatch.ElapsedMilliseconds;
				DeltaTime = currentTime - lastTime;
				lastTime = currentTime;
			};

			UpdateFps += (_, _) => Fps = 1000.0 / DeltaTime;
			
			_tickTimer.Start();
			_fpsTimer.Start();
		}
	}
}
