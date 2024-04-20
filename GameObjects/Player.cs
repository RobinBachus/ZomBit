using System.Windows.Input;
using ZomBit.Enums;
using ZomBit.Interfaces;

namespace ZomBit.GameObjects
{
	internal class Player : GameObject, IMovable, ICollidable<Player>
	{
		protected override WpfShapes.Rectangle StaticDrawable { get; }

		public ICollidable<Player> Collidable => this;

		public bool CollisionsEnabled { get; set; } = true;

		public event EventHandler<CollisionEventArgs>? Collision;

		private const int GRAVITY = -10;

		private int _jumpHeight;

		public Player() : base((25, 200), 50, 50)
		{
			StaticDrawable = new WpfShapes.Rectangle
			{
				Fill = new SolidColorBrush(Colors.SandyBrown),
				Width = Width,
				Height = Height
			};

			if (Game.Frame == null) return;

			Game.Frame.KeyDown += MoveOnClick;
			Collision += OnCollision;
		}

		private void OnCollision(object? sender, CollisionEventArgs args)
		{
			switch (args.CollisionDirection)
			{
				case CollisionDirection.Top:
					Y = args.CollidedObject.Y + args.CollidedObject.Height + 1;
					break;
				case CollisionDirection.Bottom:
					Y = args.CollidedObject.Y - Height - 1;
					break;
				case CollisionDirection.Right:
					X = args.CollidedObject.X + args.CollidedObject.Width + 1;
					break;
				case CollisionDirection.Left:
					X = args.CollidedObject.X - Width - 1;
					break;
				case CollisionDirection.None:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(args), args.CollisionDirection, "Invalid collision direction.");
			}
		}

		/// <summary>
		/// Move the player.
		/// </summary>
		/// <param name="x"> The amount to move on the x-axis. </param>
		/// <param name="y"> The amount to move on the y-axis. </param>
		/// <inheritdoc />
		public void Move(int x, int y)
		{
			(int newX, int newY) = (X + x, Y + y);
			Position = (newX, newY);
		}

		public override void Update()
		{
			Jump();
			if (Y > 0) Move(0, GRAVITY + _jumpHeight);
			CheckCollision();
			base.Update();
		}

		/// <summary>
		/// Move right if a key is pressed.
		/// </summary>
		/// <param name="sender"> The object that raised the event. </param>
		/// <param name="e"> The event arguments. </param>
		private void MoveOnClick(object sender, KeyEventArgs e)
		{
			// ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault -- We only care about these keys
			switch (e.Key)
			{
				case Key.W:
					_jumpHeight = 10;
					break;
				case Key.A:
					Move(-10, 0);
					break;
				case Key.S:
					Move(0, -10);
					break;
				case Key.D:
					Move(10, 0);
					break;
				default:
					return;
			}
		}

		private void Jump()
		{
			if (_jumpHeight <= 0) return;

			Move(0, 10);
			_jumpHeight--;
		}

		/// <summary>
		/// Check if the player collides with another object.
		/// </summary>
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
