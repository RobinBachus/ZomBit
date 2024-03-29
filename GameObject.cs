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


		// Return the drawable object
		public abstract W_Shapes.Shape? Drawable { get; }

		public bool HasCollision { get; set; } = true;
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

			// BUG: Objects are drawn at the same position
			Canvas.SetLeft(Drawable, X);
			Canvas.SetBottom(Drawable, Y);

			if (!Game.Frame.Children.Contains(Drawable))
				Game.Frame.Children.Add(Drawable);
		}

		/// <summary>
		/// Check if the object collides with another object
		/// </summary>
		/// <param name="other">The other object</param>
		/// <returns>True if the objects collide, false otherwise</returns>
		public bool CollidesWith(GameObject other) =>
			HasCollision &&
			other.HasCollision &&
			X < other.X + other.Width &&
			X + Width > other.X &&
			Y < other.Y + other.Height &&
			Y + Height > other.Y;
    }
}
