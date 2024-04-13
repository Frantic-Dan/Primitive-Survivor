using System.Diagnostics;

namespace StandWhenRunning;

public static class Common
{
	public static Mod? ModInst { get; set; }
	public static Harmony? HarmonyInst { get; set; }
	public static bool StandWhenRunning { get; set; } = false;

	[Conditional("DEBUG")]
	public static void Log(string str)
	{
		global::Log.Warning($"StandWhenRunning: {str}");
	}
}