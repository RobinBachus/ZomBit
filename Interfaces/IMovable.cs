namespace ZomBit.Interfaces
{
	internal interface IMovable
	{
		/// <summary>
		/// Move the object.
		/// </summary>
		/// <param name="x"> The amount to move on the x-axis. </param>
		/// <param name="y"> The amount to move on the y-axis. </param>
		void Move(int x, int y);

		
	}
}