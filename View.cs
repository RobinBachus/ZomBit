using System.Collections.Immutable;

namespace ZomBit
{
	internal abstract class View(Scene scene)
	{
		public abstract ImmutableList<GameObject> GameObjects { get; }

		private protected Scene Scene = scene;
	}
}
