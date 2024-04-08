namespace ZomBit.BuiltIn.Shapes
{
	internal class Rectangle : Shape
	{
		protected override W_Shapes.Rectangle StaticDrawable { get; }

		public Rectangle((int, int) position, int width, int height, Color? color = null) 
			: base(position, width, height, color)
		{
			StaticDrawable = new W_Shapes.Rectangle
			{
				Width = Width,
				Height = Height,
				Fill = new SolidColorBrush(Color)
			};
		}

	}
}
