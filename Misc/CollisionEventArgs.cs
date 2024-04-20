using ZomBit.Enums;

namespace ZomBit.Misc
{
	internal class CollisionEventArgs(GameObject thisObject, GameObject otherObject, CollisionDirection direction) : EventArgs
	{
		public GameObject ThisObject { get; } = thisObject;
		public GameObject CollidedObject { get; } = otherObject;
		public CollisionDirection CollisionDirection { get; set; } = direction;

		public int CollisionDistance
		{
			get
			{
				return CollisionDirection switch
				{
					CollisionDirection.Top => ThisObject.Y - CollidedObject.Y - CollidedObject.Height,
					CollisionDirection.Bottom => CollidedObject.Y - ThisObject.Y - ThisObject.Height,
					CollisionDirection.Left => ThisObject.X - CollidedObject.X - CollidedObject.Width,
					CollisionDirection.Right => CollidedObject.X - ThisObject.X - ThisObject.Width,
					CollisionDirection.None => 0,
					_ => throw new ArgumentOutOfRangeException(nameof(CollisionDirection), CollisionDirection, "Invalid collision direction.")
				};
			}
		}
	}
}