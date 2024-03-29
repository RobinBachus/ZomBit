namespace ZomBit.BuiltIn
{
	internal abstract class Shape((int, int) position, int width, int height, Color? color = null)
		: GameObject(position, width, height)
	{
		public Color Color { get; set; } = color ?? Color.FromRgb(255, 255, 255);
	}
}
