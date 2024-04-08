using System.Collections.Immutable;
using ZomBit.BuiltIn;
using ZomBit.BuiltIn.CollidableShapes;

namespace ZomBit
{
	internal abstract class SceneBase
	{
		private int _currentViewIndex;

		public virtual ImmutableList<View> Views => ImmutableList<View>.Empty;

		public View? CurrentView { get => _currentViewIndex < Views.Count ? Views[_currentViewIndex] : null; }

		private CollidableShape? _objective;

		public CollidableShape? Objective
		{
			get => _objective;
			set
			{
				if (value == null) return;

				_objective = value;
				_objective.Collision += (_, _) => CheckObjectiveReached();
			}
		}

		public event EventHandler<EventArgs>? ObjectiveReached;

		protected void CheckObjectiveReached()
		{
			if (Objective == null) return;
			if (Game.Player == null) return;

			if (Game.Player.Collidable.CollidesWith(Objective))
			{
				ObjectiveReached?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Try to get the next view in the sceneBase.
		/// </summary>
		/// <returns> The next view in the sceneBase, or null if there are no more views. </returns>
		public View? NextView()
		{
			if (_currentViewIndex + 1 >= Views.Count) return null;
			
			_currentViewIndex++;
			return CurrentView;
		}

		/// <summary>
		/// Try to get the previous view in the sceneBase.
		/// </summary>
		/// <returns> The previous view in the sceneBase, or null if there are no more views. </returns>
		public View? PreviousView()
		{
			if (_currentViewIndex - 1 < 0) return null;
			
			_currentViewIndex--;
			return CurrentView;
		}
	}
}
