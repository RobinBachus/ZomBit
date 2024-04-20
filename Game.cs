using System.Windows.Controls;
using ZomBit.GameObjects;
using ZomBit.Internal;
using ZomBit.Scenes;

namespace ZomBit
{
	public static class Game
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

		private static readonly TimingManager _timingManager = new();

		/// <inheritdoc cref="TimingManager.Update"/>
		public static event EventHandler Update
		{
			add => _timingManager.Update += value; 
			remove => _timingManager.Update -= value;
		}

		private static View? _lastView;

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
		}

		private static void OnUpdate(object? sender, EventArgs e)
		{
			if (Frame == null) return;
			if (Player == null) return;
			if (SceneManager.CurrentScene == null) return;
			if (SceneManager.CurrentView == null) return;

			if (SceneManager.CurrentView != _lastView)
			{
				ClearFrame();
				Player.Position = SceneManager.CurrentView.PlayerStartPosition;
			}

			_lastView = SceneManager.CurrentView;

			GameObjectsInFrame.ForEach(go => go.Update());
			SceneManager.CurrentScene.Objective?.CheckCollision();
		}

		public static void ClearFrame() => Frame?.Children.Clear();
	}
}