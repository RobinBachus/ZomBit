global using W_Shapes = System.Windows.Shapes;
global using System.Windows.Media;

using System.Windows.Controls;
using System.Windows.Threading;

namespace ZomBit
{
	public class Game
	{
		public static Canvas? Frame { get; private set; }

		private readonly DispatcherTimer _tickTimer = new(TimeSpan.FromMilliseconds(1000 / 60.0), DispatcherPriority.Normal,
			(e, s) => { }, Dispatcher.CurrentDispatcher);

		private readonly List<Scene> _scenes = new(); // TODO: Implement scenes system
		private int _currentSceneIndex = 0;
		private Scene CurrentScene => _scenes[_currentSceneIndex];

		public Game(Canvas frame)
		{
			_tickTimer.Tick += Update;
			_tickTimer.Start();
			Frame = frame;
			_scenes.Add(new Scenes.Scene0()); // TODO: Implement scenes system
		}

		private void Update(object? sender, EventArgs e)
		{
			// Update game
			CurrentScene.Views.ForEach(view => view.GameObjects.ForEach(gameObject => gameObject.Draw()));
		}
	}
}