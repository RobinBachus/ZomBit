using System.Windows.Controls;
using System.Windows.Threading;
using ZomBit.GameObjects;
using ZomBit.Scenes;

namespace ZomBit
{
	public class Game
	{
		public static Canvas? Frame { get; private set; }
		internal static Player? Player { get; private set; }
		internal static ImmutableList<GameObject> GameObjectsInFrame
		{
			get
			{
				List<GameObject>? gameObjects = SceneManager.CurrentView?.GameObjects.ToList();
				if (gameObjects == null) return ImmutableList<GameObject>.Empty;
				if (Player != null) gameObjects.Add(Player);
				
				return gameObjects.ToImmutableList();
			}
		}

		/// <summary>
		/// The timer that ticks every frame (60 fps)
		/// </summary>
		private static readonly DispatcherTimer _tickTimer = new(TimeSpan.FromMilliseconds(1000 / 60.0), DispatcherPriority.Normal,
			(e, s) => { }, Dispatcher.CurrentDispatcher);

		/// <summary>
		/// The event that gets called every frame
		/// </summary>
		/// <remarks>
		/// Since this is the thread that the game loop runs on, it is important to keep this event as small as possible.
		/// </remarks>
		public static event EventHandler Update
		{
			add => _tickTimer.Tick += value;
			remove => _tickTimer.Tick -= value;
		}

		/// <summary>
		/// Sets the frame and starts the game loop
		/// </summary>
		/// <param name="frame"> The canvas to render the game on. </param>
		public static void Initialize(Canvas frame)
		{
			Frame = frame;
			SceneManager.LoadScenes();

			Player = new Player();
			Frame.Focus();

			Update += OnUpdate;
			_tickTimer.Start();
		}

		private static void OnUpdate(object? sender, EventArgs e)
		{
			// Update game
			GameObjectsInFrame.ForEach(go => go.Update());
		}
	}
}