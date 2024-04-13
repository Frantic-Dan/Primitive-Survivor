namespace StandWhenRunning;

[HarmonyPatch(typeof(EntityAlive))]
static class Patches__EntityAlive
{
	[HarmonyPatch("get_Crouching"), HarmonyPrefix]
	static void get_Crouching__Prefix(
			EntityAlive __instance,
			ref bool ___bCrouching,
			ref bool ___bInElevator)
	{
		if(ShouldForceToStand(__instance, ___bInElevator)){
			Common.Log("get_Crouching__Prefix: Passed Criteria");
			__instance.Crouching = false;
			___bCrouching = false;
		}
	}

	[HarmonyPatch("set_Crouching"), HarmonyPrefix]
	static void set_Crouching__Prefix(
			EntityAlive __instance,
			ref bool value,
			ref bool ___bInElevator)
	{
		if(ShouldForceToStand(__instance, ___bInElevator)){
			Common.Log("set_Crouching__Prefix: Passed Criteria");
			value = false;
		}
	}

	[HarmonyPatch("get_IsCrouching"), HarmonyPrefix]
	static void get_IsCrouching__Postfix(
			EntityAlive __instance,
			ref bool ___bInElevator,
			ref bool __result)
	{
		if(ShouldForceToStand(__instance, ___bInElevator)){
			Common.Log("get_IsCrouching__Postfix: Passed Criteria");
			__instance.Crouching = false;
			__instance.CrouchingLocked = false;
			__result = false;
			return;
		}
	}

	static bool ShouldForceToStand(EntityAlive ea, bool inElevator)
	{
#if DEBUG
		var epl2 = GameManager.Instance.World.GetPrimaryPlayer();

		if(epl2.MovementRunning){
			Common.Log($"{Common.StandWhenRunning}");
			Common.Log($"{!epl2.IsFlyMode.Value}");
			Common.Log($"{!inElevator}");
			Common.Log($"{epl2.AttachedToEntity == null}");
			Common.Log("---");
		}
#endif
		return ea is EntityPlayerLocal epl
			&& Common.StandWhenRunning
			&& epl.MovementRunning
			&& !epl.IsFlyMode.Value
			&& !inElevator
			&& epl.AttachedToEntity == null;
	}
}