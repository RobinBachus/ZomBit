using System.Collections.Immutable;

namespace ZomBit.Scenes
{
	internal static class SceneManager
	{
		private static int _currentSceneIndex = 0;
		public static SceneBase CurrentSceneBase => Scenes[_currentSceneIndex];

		public static ImmutableList<SceneBase> Scenes => ImmutableList<SceneBase>.Empty;

		public static void LoadScenes()
		{

		}
	}
}
