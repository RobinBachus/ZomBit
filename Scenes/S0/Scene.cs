using System.Collections.Immutable;
using ZomBit.Misc;

namespace ZomBit.Scenes.S0
{
	internal class Scene : SceneBase
	{
		public Scene()
		{
			SceneJsonScheme.Root? root = SceneJsonScheme.Root.FromJson("Scenes/S0/Scene.json");
			if (root == null) return;

			Views = root.Frames.Select(frame => new View(this, frame)).ToImmutableList();
		}

		public override ImmutableList<View> Views { get; } = ImmutableList<View>.Empty;
	}
}