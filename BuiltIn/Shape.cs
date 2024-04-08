namespace ZomBit.BuiltIn
{
	/// <summary>
	/// A shape that can be drawn on the screen
	/// </summary>
	/// <param name="position"> The position of the shape. </param>
	/// <param name="width"> The width of the shape. </param>
	/// <param name="height"> The height of the shape. </param>
	/// <param name="color"> The color of the shape. </param>
	internal abstract class Shape((int, int) position, int width, int height, Color? color = null)
		: GameObject(position, width, height)
	{
		public Color Color { get; set; } = color ?? Color.FromRgb(255, 255, 255);
	}
}
