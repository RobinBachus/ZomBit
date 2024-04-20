using System.Windows.Controls;
using System.Windows.Shell;
using ZomBit.BuiltIn.UI;
using ZomBit.GameObjects;
using ZomBit.Internal;
using ZomBit.Scenes;

namespace ZomBit
{
	public static class Game
	{
		/// <summary>
		/// The canvas that the game is rendered on
		/// </summary>
		public static Canvas? Frame { get; private set; }

		public static double FrameHeight => Frame?.ActualHeight ?? 0;
		public static double FrameWidth => Frame?.ActualWidth ?? 0;

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
				if (_fpsDisplay != null) gameObjects.Add(_fpsDisplay);
				
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

		// TODO: TEMPORARY, REMOVE
		private static TextLabel? _fpsDisplay;

		/// <summary>
		/// The last view that was rendered
		/// </summary>
		private static View? _lastView;
		private static readonly TimingManager _timingManager = new();

		/// <inheritdoc cref="TimingManager.Update"/>
		public static event EventHandler Update
		{
			add => _timingManager.Update += value; 
			remove => _timingManager.Update -= value;
		}

		/// <summary>
		/// Sets the frame and starts the game loop
		/// </summary>
		/// <param name="frame"> The canvas to render the game on. </param>
		public static void Initialize(Canvas frame)
		{
			Frame = frame;
			SceneManager.LoadScenes();

			frame.Loaded += (_, _) =>
				_fpsDisplay = new TextLabel((0, 0), 10, 10, "Fps");

			Player = new Player();
			Frame.Focus();

			Update += OnUpdate;
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
			if (_fpsDisplay != null)
				_fpsDisplay.Text = $"Fps: {Math.Floor(_timingManager.Fps)}";
			

			// If the scale has changed, update the layout
			if (_scaleChanged) Frame.LayoutTransform = new ScaleTransform(Scale, Scale);
			
			// Update all game objects
			GameObjectsInFrame.ForEach(go => go.Update());
			SceneManager.CurrentScene.Objective?.CheckCollision();
		}

		/// <summary>
		/// Clears all objects from the frame
		/// </summary>
		public static void ClearFrame() => Frame?.Children.Clear();
	}
}