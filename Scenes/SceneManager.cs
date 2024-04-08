using System.IO;

namespace ZomBit.Scenes
{
	internal static class SceneManager
	{
		private static int _currentSceneIndex;

		public static Scene? CurrentScene => _currentSceneIndex < Scenes.Count ? Scenes[_currentSceneIndex] : null;

		public static View? CurrentView
		{
			get => CurrentScene?.CurrentView;
			set
			{
				if (CurrentScene != null) CurrentScene.CurrentView = value;
			}
		}

		public static ImmutableList<Scene> Scenes { get; private set; } = ImmutableList<Scene>.Empty;

		public static void LoadScenes()
		{
			// load all scenes from the Scenes folder
			string[] sceneDirectories = Directory.GetDirectories("Scenes");
			for (int i = 0; i < sceneDirectories.Length; i++) 
				AddScene(new Scene(i));
		}

		private static void AddScene(Scene scene) => Scenes = Scenes.Add(scene);

		public static Scene? NextScene()
		{
			if (_currentSceneIndex == Scenes.Count - 1) 
				return CurrentScene;

			_currentSceneIndex++;
			CurrentScene?.SetToStart();
			return CurrentScene;

		}

		public static Scene? PreviousScene()
		{
			if (_currentSceneIndex == 0) 
				return CurrentScene;

			_currentSceneIndex--;
			CurrentScene?.SetToStart();
			return CurrentScene;
		}

		public static View? NextView() => CurrentScene?.NextView();

		public static View? PreviousView() => CurrentScene?.PreviousView();
	}
}
