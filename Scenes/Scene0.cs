using System.Collections.Immutable;

namespace ZomBit.Scenes
{
	internal class Scene0 : Scene
	{
		public Scene0()
		{
			Views = ImmutableList<View>.Empty
				.Add(new Views_Scene0.View0(this));
		}
		
		public override ImmutableList<View> Views { get; }
	}
}