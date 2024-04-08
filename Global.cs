// These are usings that are used in every file in the project
global using System.Windows.Media;
global using System.Collections.Immutable;
global using System.Diagnostics;
global using WinShapes = System.Windows.Shapes;

namespace ZomBit
{
	/// <summary>
	/// Global namespace for ZomBit
	/// Declares global usings and other global variables
	/// Also useful for miscellaneous information
	/// </summary>
	internal static class Global
	{
		public const string Creator = "Robin Bachus";
		public const string GameName = "ZomBit";
		public const string GameDescription = "ZomBit is a project that includes a game engine and a game that uses that engine.";

		public static new string ToString() => $"{GameName} by {Creator} \n- {GameDescription}";
	}
}
