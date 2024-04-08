namespace ZomBit.BuiltIn.Shapes
{
	internal class Rectangle : Shape
	{
		protected override WinShapes.Rectangle StaticDrawable { get; }

		public Rectangle((int, int) position, int width, int height, Color? color = null)
			: base(position, width, height, color)
		{
			StaticDrawable = new WinShapes.Rectangle
			{
				Width = width, Height = height, Fill = new SolidColorBrush(Color)
			};
		}
	}
}
