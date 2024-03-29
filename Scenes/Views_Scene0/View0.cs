using System.Collections.Immutable;
using ZomBit.BuiltIn.Shapes;

namespace ZomBit.Scenes.Views_Scene0
{
	internal class View0: View
	{
		public override ImmutableList<GameObject> GameObjects { get; } = new GameObject[]
		{
			new Rectangle((0, 0), 100, 100),
			new Rectangle((100, 100), 100, 100, Color.FromRgb(255, 0, 0)),
			new Rectangle((200, 200), 100, 100, Color.FromRgb(0, 255, 0)),
			new Rectangle((300, 300), 100, 100, Color.FromRgb(0, 0, 255)),
		}.ToImmutableList();
		
	}
}
