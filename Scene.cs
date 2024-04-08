using ZomBit.BuiltIn;
using ZomBit.Misc;
using ZomBit.Scenes;

namespace ZomBit
{
	internal class Scene
	{
		private int _currentViewIndex;

		public ImmutableList<View> Views { get; } = ImmutableList<View>.Empty;

		public View? CurrentView 
		{ 
			get => _currentViewIndex<Views.Count? Views[_currentViewIndex] : null;
			// If the value is null, the current view index should not change
			set =>  _currentViewIndex = value == null ? _currentViewIndex : Views.IndexOf(value);
		}

		private CollidableShape? _objective;

		public CollidableShape? Objective
		{
			get => _objective;
			set
			{
				if (value == null) return;

				_objective = value;
				_objective.Collision += (_, _) => CheckObjectiveReached();
			}
		}

		public event EventHandler<EventArgs>? ObjectiveReached;

		public Scene(int sceneIndex)
		{
			Debug.WriteLine($"Scene location: {GetType().FullName}");
			SceneJsonScheme.Root? root = SceneJsonScheme.Root.FromJson($"Scenes/S{sceneIndex}/Scene.json");
			if (root == null) return;

			Views = root.Frames.Select(frame => new View(this, frame)).ToImmutableList();
			ObjectiveReached += (_, _) => SceneManager.NextScene();
		}

		
		public static Scene operator ++(Scene scene) => SceneManager.NextScene() ?? scene;

		public static Scene operator --(Scene scene) => SceneManager.PreviousScene() ?? scene;


		public View? NextView() =>_currentViewIndex == Views.Count - 1 ? CurrentView : Views[++_currentViewIndex];

		public View? PreviousView() => _currentViewIndex == 0 ? CurrentView : Views[--_currentViewIndex];

		public void SetToStart() => _currentViewIndex = 0;

		protected void CheckObjectiveReached()
		{
			if (Objective == null) return;
			if (Game.Player == null) return;

			if (Game.Player.Collidable.CollidesWith(Objective))
			{
				ObjectiveReached?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
