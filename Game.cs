using System.Windows.Controls;
using System.Windows.Threading;
using ZomBit.GameObjects;
using ZomBit.Scenes;

namespace ZomBit
{
	public static class Game
	{
		/// <summary>
		/// The canvas that the game is rendered on
		/// </summary>
		public static Canvas? Frame { get; private set; }

		/// <summary>
		/// The player object that is used by all scenes
		/// </summary>
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

		private static bool _scaleChanged;

		private static double _scale = 1;
		internal static double Scale
		{
			get
			{
				_scaleChanged = false;
				return _scale;
			}
			set 
			{ 
				_scaleChanged = true;
				_scale = value;
			}
		}

		/// <summary>
		/// The last view that was rendered
		/// </summary>
		private static View? _lastView;

		/// <summary>
		/// The timer that ticks every frame (60 fps)
		/// </summary>
		private static readonly DispatcherTimer _tickTimer = new(TimeSpan.FromMilliseconds(1000 / 60.0), DispatcherPriority.Normal,
			(_, _) => { }, Dispatcher.CurrentDispatcher);


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

		/// <summary>
		/// The method that gets called every frame
		/// </summary>
		/// <param name="sender"> The object that called the event. </param>
		/// <param name="e"> The event arguments. </param>
		private static void OnUpdate(object? sender, EventArgs e)
		{
			if (Frame == null) return;
			if (Player == null) return;
			if (SceneManager.CurrentScene == null) return;
			if (SceneManager.CurrentView == null) return;

			// If the view has changed, clear the frame and set the player's position to the new start position
			if (SceneManager.CurrentView != _lastView)
			{
				ClearFrame();
				Player.Position = SceneManager.CurrentView.PlayerStartPosition;
			}

			_lastView = SceneManager.CurrentView;

			// If the scale has changed, update the layout
			if (_scaleChanged) Frame.LayoutTransform = new ScaleTransform(Scale, Scale);
			
			// Update all game objects
			GameObjectsInFrame.ForEach(go => go.Update());
		}

		/// <summary>
		/// Clears all objects from the frame
		/// </summary>
		public static void ClearFrame() => Frame?.Children.Clear();
	}
}