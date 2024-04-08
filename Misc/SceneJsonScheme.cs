using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZomBit.Misc
{
	internal class SceneJsonScheme
	{
		// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
		public class Frame
		{
			[JsonPropertyName("GameObjects")] public List<GameObject> GameObjects { get; set; }

			public Frame()
			{
				GameObjects = new List<GameObject>();
			}
		}

		public class GameObject
		{
			[JsonPropertyName("Type")] public string Type { get; set; }

			[JsonPropertyName("Position")] public List<int> Position { get; set; }

			[JsonPropertyName("Width")] public int Width { get; set; }

			[JsonPropertyName("Height")] public int Height { get; set; }

			[JsonPropertyName("Color")] public List<byte?> Color { get; set; }

			[JsonPropertyName("IsCollidable")] public bool? IsCollidable { get; set; } = true;

			[JsonPropertyName("IsObjective")] public bool? IsObjective { get; set; }

			public GameObject()
			{
				Type = "Rectangle";
				Position = new List<int>();
				Color = new List<byte?>() { null, null, null};
			}
		}

		public class Root
		{
			[JsonPropertyName("Frames")] public List<Frame> Frames { get; set; }

			public Root()
			{
				Frames = new List<Frame>();
			}

			internal static Root? FromJson(string path)
			{
				try
				{
					string json = File.ReadAllText(path);
					return JsonSerializer.Deserialize<Root>(json);
				}
				catch (JsonException e)
				{
					Debug.WriteLine("Invalid JSON");
					Debug.WriteLine(e.Message);
					return null;
				}
				catch (FileNotFoundException e)
				{
					Debug.WriteLine("File not found");
					Debug.WriteLine(e.Message);
					return null;
				}
				catch (FileLoadException e)
				{
					Debug.WriteLine("File load error");
					Debug.WriteLine(e.Message);
					return null;
				}
				catch (IOException e)
				{
					Debug.WriteLine("IO error");
					Debug.WriteLine(e.Message);
					return null;
				}
				catch (UnauthorizedAccessException e)
				{
					Debug.WriteLine("Unauthorized access");
					Debug.WriteLine(e.Message);
					return null;
				}
				catch (Exception e)
				{
					Debug.WriteLine("Unknown error");
					Debug.WriteLine(e.Message);
					throw;
				}
			}
		}
	}
}