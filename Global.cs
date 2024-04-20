// These are usings that are used in every file in the project
global using System.Windows.Media;
global using System.Collections.Immutable;
global using System.Diagnostics;
global using WpfShapes = System.Windows.Shapes;
global using WpfControls = System.Windows.Controls;

namespace ZomBit
{
	/// <summary>
	/// Global namespace for ZomBit
	/// Declares global usings and other global variables
	/// Also useful for miscellaneous information
	/// </summary>
	internal static class Global
	{
		public const string CREATOR = "Robin Bachus";
		public const string GAME_NAME = "ZomBit";
		public const string GAME_DESCRIPTION = "ZomBit is a project that includes a game engine and a game that uses that engine.";

		public static new string ToString() => $"{GAME_NAME} by {CREATOR} \n- {GAME_DESCRIPTION}";
	}
}
