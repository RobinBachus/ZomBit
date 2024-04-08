global using System.Windows.Media;
global using W_Shapes = System.Windows.Shapes;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Threading;
using ZomBit.GameObjects;

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
				List<GameObject>? gameObjects = CurrentSceneBase.CurrentView?.GameObjects.ToList();
				if (gameObjects == null) return ImmutableList<GameObject>.Empty;
				if (Player != null) gameObjects.Add(Player);
				
				return gameObjects.ToImmutableList();
			}
		}

		/// <summary>
		/// The timer that ticks every frame
		/// </summary>
		private readonly DispatcherTimer _tickTimer = new(TimeSpan.FromMilliseconds(1000 / 60.0), DispatcherPriority.Normal,
			(e, s) => { }, Dispatcher.CurrentDispatcher);

		private static readonly List<SceneBase> Scenes = new(); // TODO: Implement scenes system
		private static int _currentSceneIndex;

		internal static SceneBase CurrentSceneBase => Scenes[_currentSceneIndex];

		public Game(Canvas frame)
		{
			_tickTimer.Tick += Update;
			_tickTimer.Start();
			Frame = frame;
			Scenes.Add(new Scenes.S0.Scene()); // TODO: Implement scenes system
			CurrentSceneBase.ObjectiveReached += (_, _) => Debug.WriteLine("Objective reached!");
			Player = new Player();
			Frame.Focus();
		}

		private static void Update(object? sender, EventArgs e)
		{
			// Update game
			GameObjectsInFrame.ForEach(go => go.Update());
		}

		public static void SetScene(int sceneIndex) => _currentSceneIndex = sceneIndex;

		public static void NextScene() => _currentSceneIndex++;
	}
}