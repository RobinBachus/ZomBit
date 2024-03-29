namespace ZomBit.BuiltIn.Shapes
{
	internal class Rectangle : Shape
	{
		private readonly W_Shapes.Rectangle _rectangle;
		public override W_Shapes.Shape Drawable => _rectangle;

		public Rectangle((int, int) position, int width, int height, Color? color = null) 
			: base(position, width, height, color)
		{
			_rectangle = new W_Shapes.Rectangle
			{
				Width = Width,
				Height = Height,
				Fill = new SolidColorBrush(Color)
			};
		}

	}
}
