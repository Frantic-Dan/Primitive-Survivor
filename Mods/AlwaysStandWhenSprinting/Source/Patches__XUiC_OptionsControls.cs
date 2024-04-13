namespace StandWhenRunning;

[HarmonyPatch(typeof(XUiC_OptionsControls))]
public static class Patches__XUiC_OptionsControls
{
	static XUiC_ComboBoxBool? combo;
	static XUiC_OptionsControls? view;

	[HarmonyPatch("Init"), HarmonyPostfix]
	static void Init__Postfix(XUiC_OptionsControls __instance)
	{
		if(!Persistence.Load()){
			Persistence.Save();
		}

		combo = __instance.GetChildById("StandWhenRunning").
			GetChildByType<XUiC_ComboBoxBool>();
		view = __instance;
		combo.OnValueChangedGeneric += RunValueChangedRevPatch;
	}

	static void RunValueChangedRevPatch(XUiController sender)
	{
		if(view is {}){
			Combo_OnValueChangedGeneric__ReversePatch(view, sender);
		}
	}

	[HarmonyPatch("Combo_OnValueChangedGeneric"), HarmonyReversePatch]
	static void Combo_OnValueChangedGeneric__ReversePatch(
			XUiC_OptionsControls inst,
			XUiController _sender)
	{
		// Run original.
	}

	[HarmonyPatch("OnOpen"), HarmonyPostfix]
	static void OnOpen__Postfix()
	{
		if(combo is {}){
			combo.Value = Common.StandWhenRunning;
		}
	}

	[HarmonyPatch("applyChanges"), HarmonyPrefix]
	static void applyChanges__Prefix()
	{
		if(combo is {}){
			Common.Log("Saving changes...");
			Common.StandWhenRunning = combo.Value;
			Persistence.Save();
			return;
		}

		Log.Warning("StandWhenRunning failed to locate its ComboBox when trying to save settings.");
	}
}