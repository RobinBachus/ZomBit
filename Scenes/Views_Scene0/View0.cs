using System.Collections.Immutable;
using ZomBit.BuiltIn.CollidableShapes;
using ZomBit.BuiltIn.Shapes;

namespace ZomBit.Scenes.Views_Scene0
{
	internal class View0 : View
	{
		public View0(Scene scene) : base(scene)
		{
			Scene.Objective = new CollidableRectangle((400, 0), 100, 100, Color.FromRgb(255, 255, 0));

			GameObjects = new GameObject[]
			{
				new CollidableRectangle((0, 0), 100, 100),
				new Rectangle((100, 100), 100, 100, Color.FromRgb(255, 0, 0)),
				new CollidableRectangle((200, 200), 100, 100, Color.FromRgb(0, 255, 0), false),
				new Rectangle((300, 300), 100, 100, Color.FromRgb(0, 0, 255)),
				Scene.Objective
			}.ToImmutableList();
		}

		public override ImmutableList<GameObject> GameObjects { get; } 
	}
}
