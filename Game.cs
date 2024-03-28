using System.Windows.Threading;

namespace ZomBit
{
	internal class Game
	{
		private readonly DispatcherTimer _tickTimer = new(TimeSpan.FromMilliseconds(1000 / 60.0), DispatcherPriority.Normal,
			(e, s) => { }, Dispatcher.CurrentDispatcher);

		private readonly List<Scene> _scenes = new(); // TODO: Implement scenes system

		public Game()
		{
			_tickTimer.Tick += Update;
			_tickTimer.Start();
		}

		private void Update(object? sender, EventArgs e)
		{
			// Update game logic
		}
	}
}