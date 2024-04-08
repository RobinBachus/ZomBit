using System.Windows.Controls;

namespace ZomBit
{
	internal abstract class GameObject()
	{
		public int X { get; set; }
		public int Y { get; set; }
		public (int, int) Position
		{
			get => (X, Y);
			set => (X, Y) = value;
		}

		public int Width { get; set; }
		public int Height { get; set; }
		public (int, int) Size => (Width, Height);

		protected abstract W_Shapes.Shape? StaticDrawable { get; }

		// Return the drawable object
		/// <summary>
		/// The drawable object, generally a shape that can be rendered on the canvas.
		/// </summary>
		public virtual W_Shapes.Shape? Drawable => StaticDrawable;

		public bool IsVisible { get; set; } = true;

		protected GameObject((int, int) position, int width, int height): this()
		{
			Position = position;
			Width = width;
			Height = height;
		}

		/// <summary>
		/// Update the game object
		/// </summary>
		public virtual void Draw()
		{
			if (Game.Frame == null) return;
			if (Drawable == null) return;
			if (!IsVisible) return;

			Canvas.SetLeft(Drawable, X);
			Canvas.SetBottom(Drawable, Y);

			if (!Game.Frame.Children.Contains(Drawable))
				Game.Frame.Children.Add(Drawable);
		}

		public virtual void Update()
		{
			Draw();
		}
	}
}
