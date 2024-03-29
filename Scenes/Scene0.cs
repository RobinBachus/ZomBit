using System.Collections.Immutable;

namespace ZomBit.Scenes
{
	internal class Scene0 : Scene
	{
		public override ImmutableList<View> Views { get; } = ImmutableList<View>.Empty
			.Add(new Views_Scene0.View0());
	}
}
