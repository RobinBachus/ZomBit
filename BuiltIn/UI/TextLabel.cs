namespace ZomBit.BuiltIn.UI
{
	internal class TextLabel((int, int) position, int width, int height) : GameObject(position, width, height)
	{
		protected override WpfControls.TextBlock StaticDrawable { get; } = new();

		public string Text
		{
			get => StaticDrawable.Text;
			set => StaticDrawable.Text = value;
		}

		public TextLabel((int, int) position, int width, int height, string text)
			: this(position, width, height)
		{
			Text = text;
		}
	}
}
