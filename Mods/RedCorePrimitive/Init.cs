using System.Reflection;
using HarmonyLib;

public class Init : IModApi
{
	public void InitMod(Mod _modInstance)
	{
		Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "RedCorePrimitive");
	}
}

public class Patches
{
	[HarmonyPatch(typeof(XUiC_MapArea), "updateMapSection")]
	private static class XUiC_MapArea__updateMapSection
	{
		private static bool Prefix() => false;
	}
}
