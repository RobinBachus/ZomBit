using System.Collections.Immutable;

namespace ZomBit
{
	internal abstract class View
	{
		public abstract ImmutableList<GameObject> GameObjects { get; }
	}
}
