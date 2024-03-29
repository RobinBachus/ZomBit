using System.Collections.Immutable;

namespace ZomBit
{
	internal abstract class Scene
	{
		public abstract ImmutableList<View> Views { get; }
	}
}
