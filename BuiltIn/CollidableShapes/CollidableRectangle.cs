using ZomBit.Enums;
using ZomBit.GameObjects;
using ZomBit.Interfaces;

namespace ZomBit.BuiltIn.CollidableShapes
{
	internal class CollidableRectangle : Shape, ICollidable<CollidableRectangle>
	{
		private readonly W_Shapes.Rectangle _rectangle;

		public override W_Shapes.Shape Drawable => _rectangle;

		public ICollidable<CollidableRectangle> Collidable => this;
		public event EventHandler<CollisionEventArgs>? Collision;

		public CollidableRectangle((int, int) position, int width, int height, Color? color = null)
			: base(position, width, height, color)
		{
			_rectangle = new W_Shapes.Rectangle
			{
				Width = Width,
				Height = Height,
				Fill = new SolidColorBrush(Color)
			};
		}

		public void CheckCollision()
		{
			foreach (GameObject gameObject in Game.GameObjectsInFrame)
			{
				if (gameObject == this) continue;

				CollisionDirection collisionDirection = Collidable.CollidesWithDirection(gameObject);
				if (collisionDirection is CollisionDirection.None) continue;

				Collision?.Invoke(this, new CollisionEventArgs(this, gameObject, collisionDirection));
			}
		}
	}
}
