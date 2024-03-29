using System.Collections.Immutable;

namespace ZomBit
{
	internal abstract class Scene
	{
		private int _currentViewIndex;

		public abstract ImmutableList<View> Views { get; }

		public View CurrentView { get => Views[_currentViewIndex]; }

		/// <summary>
		/// Try to get the next view in the scene.
		/// </summary>
		/// <returns> The next view in the scene, or null if there are no more views. </returns>
		public View? NextView()
		{
			if (_currentViewIndex + 1 >= Views.Count) return null;
			
			_currentViewIndex++;
			return CurrentView;
		}

		/// <summary>
		/// Try to get the previous view in the scene.
		/// </summary>
		/// <returns> The previous view in the scene, or null if there are no more views. </returns>
		public View? PreviousView()
		{
			if (_currentViewIndex - 1 < 0) return null;
			
			_currentViewIndex--;
			return CurrentView;
		}
	}
}
