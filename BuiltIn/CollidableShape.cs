using ZomBit.Enums;
using ZomBit.Interfaces;

namespace ZomBit.BuiltIn
{
	/// <summary>
	/// A shape that can be drawn on the screen and can collide with other objects
	/// </summary>
	/// <param name="collisionEnabled"> Whether collisions are enabled for this object. </param>
	/// <inheritdoc cref="Shape"/>
	internal abstract class CollidableShape
		((int, int) position, int width, int height, Color? color = null, bool collisionEnabled = true) 
		: Shape(position, width, height, color), ICollidable<CollidableShape>
	{
		public ICollidable<CollidableShape> Collidable => this;
		public bool CollisionsEnabled { get; set; } = collisionEnabled;

		public event EventHandler<CollisionEventArgs>? Collision;
		public void CheckCollision()
		{
			if (!CollisionsEnabled) return;
			foreach (GameObject gameObject in Game.GameObjectsInFrame)
			{
				if (gameObject == this) continue;

				CollisionDirection collisionDirection = Collidable.CollidesWithDirection(gameObject);
				if (collisionDirection is CollisionDirection.None) continue;

				Collision?.Invoke(this, new CollisionEventArgs(this, gameObject, collisionDirection));
			}
		}

		public override void Update()
		{
			CheckCollision();
			base.Update();
		}
	}
}
