﻿namespace ZomBit.BuiltIn.CollidableShapes
{
	/// <summary>
	/// A rectangle that can collide with other objects
	/// </summary>
	internal class CollidableRectangle : CollidableShape
	{
		protected override WpfShapes.Rectangle StaticDrawable { get; }

		/// <inheritdoc cref="CollidableShape"/>
		public CollidableRectangle((int, int) position, int width, int height, Color? color = null, bool collisionEnabled = true)
			: base(position, width, height, color, collisionEnabled)
		{
			StaticDrawable = new WpfShapes.Rectangle
			{
				Width = Width,
				Height = Height,
				Fill = new SolidColorBrush(Color)
			};
		}
	}
}
