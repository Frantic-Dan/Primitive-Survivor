using System.Collections.Generic;

namespace DoorCrawlyFix;

public static class HeightCheck
{
	[HarmonyPatch(typeof(EntityAlive), "HeadHeightFixedUpdate")]
	static class Patches__EntityAlive__HeadHeightFixedUpdate
	{
		static IEnumerable<CodeInstruction> Transpiler(
				IEnumerable<CodeInstruction> instructions)
		{
			CodeMatcher cm = new(instructions);

			cm.End()
				.SearchBackwards(a => a.operand is FieldInfo { Name: "physicsBaseHeight" })
				.Advance(1)
				.Insert(
					new CodeInstruction(OpCodes.Ldc_R4, .75f),
					new CodeInstruction(OpCodes.Mul));

			return cm.InstructionEnumeration();
		}
	}
}