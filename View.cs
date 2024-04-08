using ZomBit.BuiltIn;
using ZomBit.BuiltIn.CollidableShapes;
using ZomBit.BuiltIn.Shapes;
using ZomBit.Misc;
using ZomBit.Scenes;

namespace ZomBit
{
	internal class View
	{
		public View(Scene scene, SceneJsonScheme.Frame frame)
		{
			GameObjects = ImmutableList<GameObject>.Empty;
			foreach (SceneJsonScheme.GameObject gameObject in frame.GameObjects)
			{
				(int, int) position = (gameObject.Position[0], gameObject.Position[1]);

				Color? color = null;
				List<byte> colors = gameObject.Color.Select(c => c ?? 0).ToList();
				if (gameObject.Color.All(c => c != null))
					color = Color.FromRgb(colors[0], colors[1], colors[2]);

				GameObjects = gameObject.Type switch
				{
					"CollidableRectangle" => GameObjects.Add(new CollidableRectangle(position, gameObject.Width,
						gameObject.Height, color, gameObject.IsCollidable ?? false)),
					"Rectangle" => GameObjects.Add(new Rectangle(position, gameObject.Width, gameObject.Height, color)),
					_ => GameObjects
				};

				if (gameObject.IsObjective is true)
					scene.Objective = GameObjects[^1] as CollidableShape;
			}
		}

		public static View operator ++(View view) => SceneManager.NextView() ?? view;

		public static View operator --(View view) => SceneManager.PreviousView() ?? view;

		public ImmutableList<GameObject> GameObjects { get; }
	}
}
