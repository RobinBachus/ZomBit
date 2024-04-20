namespace ZomBit.BuiltIn.Shapes
{
	internal class Rectangle : Shape
	{
		protected override WpfShapes.Rectangle StaticDrawable { get; }

		public Rectangle((int, int) position, int width, int height, Color? color = null)
			: base(position, width, height, color)
		{
			StaticDrawable = new WpfShapes.Rectangle
			{
				Width = width, Height = height, Fill = new SolidColorBrush(Color)
			};
		}
	}
}
