using ZomBit.Enums;
using ZomBit.Misc;

namespace ZomBit.Interfaces
{
	internal interface ICollidable<out TSelf> where TSelf : GameObject
	{
		private TSelf Self =>
			this as TSelf ?? throw new InvalidCastException("This object is not a child of type GameObject");

		public ICollidable<TSelf> Collidable { get; }

		public bool CollisionsEnabled { get; set; }

		public event EventHandler<CollisionEventArgs>? Collision;
		private protected void CheckCollision();

		/// <summary>
		/// Check if the object collides with another object
		/// </summary>
		/// <param name="other">The other object</param>
		/// <returns>True if the objects collide, false otherwise</returns>
		public bool CollidesWith(GameObject other) =>
			other is ICollidable<GameObject> { CollisionsEnabled: true } &&
			Self.X < other.X + other.Width &&
			Self.X + Self.Width > other.X &&
			Self.Y < other.Y + other.Height &&
			Self.Y + Self.Height > other.Y;

		public bool IsTouching(GameObject other) =>
			other is ICollidable<GameObject> { CollisionsEnabled: true } &&
			Self.X <= other.X + other.Width &&
			Self.X + Self.Width >= other.X &&
			Self.Y <= other.Y + other.Height &&
			Self.Y + Self.Height >= other.Y;


		/// <summary>
		/// Check if the object collides with another object
		/// </summary>
		/// <param name="other">The other object</param>
		/// <returns>A CollisionDirection value</returns>
		public CollisionDirection CollidesWithDirection(GameObject other)
		{
			if (!CollidesWith(other)) return CollisionDirection.None;

			double x = Self.X + Self.Width / 2.0 - (other.X + other.Width / 2.0);
			double y = Self.Y + Self.Height / 2.0 - (other.Y + other.Height / 2.0);

			double width = Self.Width / 2.0 + other.Width / 2.0;
			double height = Self.Height / 2.0 + other.Height / 2.0;

			double crossWidth = width * y;
			double crossHeight = height * x;

			if (!(Math.Abs(x) <= width) || !(Math.Abs(y) <= height))
				return CollisionDirection.None;


			if (crossWidth > crossHeight)
				return crossWidth > -crossHeight ? CollisionDirection.Top : CollisionDirection.Left;

			return crossWidth > -crossHeight ? CollisionDirection.Right : CollisionDirection.Bottom;
		}
	}
}