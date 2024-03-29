using System.Windows.Input;
using ZomBit.Interfaces;

namespace ZomBit.GameObjects
{
	internal class Player : GameObject, IMovable
	{
		private readonly W_Shapes.Rectangle _drawable;

		/// <inheritdoc />
		public override W_Shapes.Shape Drawable => _drawable;

		private int jumpHeight = 0;

		public Player() : base((0, 0), 50, 50)
		{
			_drawable = new W_Shapes.Rectangle
			{
				Fill = new SolidColorBrush(Colors.Red),
				Width = 50,
				Height = 50
			};

			if (Game.Frame == null) return;

			Game.Frame.KeyDown += MoveOnClick;
		}

		public void SetStartPosition(int x, int y)
		{
			Position = (x, y);
		}


		/// <inheritdoc />
		/// <summary>
		/// Move the player.
		/// </summary>
		/// <param name="x"> The amount to move on the x-axis. </param>
		/// <param name="y"> The amount to move on the y-axis. </param>
		public void Move(int x, int y)
		{
			(int newX, int newY) = (X + x, Y + y);
			Position = (newX, newY);
		}

		public override void Update()
		{
			if (Y > 0) Move(0, -1);
			base.Update();
		}

		/// <summary>
		/// Move right if a key is pressed.
		/// </summary>
		/// <param name="sender"> The object that raised the event. </param>
		/// <param name="e"> The event arguments. </param>
		private void MoveOnClick(object sender, KeyEventArgs e)
		{
			// ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
			switch (e.Key)
			{
				case Key.W:
					Move(0, 10);
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
	}
}
