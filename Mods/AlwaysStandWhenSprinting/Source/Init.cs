using StandWhenRunning;
#if DEBUG
using Logger = HarmonyLib.Tools.Logger;
#endif

public class Init : IModApi
{
	public void InitMod(Mod _modInstance)
	{
#if DEBUG
		Logger.ChannelFilter = Logger.LogChannel.All ^ Logger.LogChannel.IL;
#endif

		Common.ModInst = _modInstance;
		Common.HarmonyInst = Harmony.CreateAndPatchAll(
			typeof(Init).Assembly, "StandWhenRunning");
		Persistence.Load();
	}
}