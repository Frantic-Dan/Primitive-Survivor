using System.Collections.Generic;

namespace ProjectileCollisionFix;

/// <summary>
/// Attempts to fix the issue where projectiles phase through targets on
/// occasion by just moving the raycast position back one unit and increasing
/// the raycast distance/magnitude by one to compensate, thereby giving the
/// raycast a longer "tail".
/// </summary>
public static class ProjectileColliders
{
	public static bool Enabled { get; set; } = true;

	[HarmonyPatch(typeof(ItemActionLauncher), "ItemActionEffects")]
	static class Patches__ItemActionLauncher__ItemActionEffects
	{
		static IEnumerable<CodeInstruction> Transpiler(
				IEnumerable<CodeInstruction> instructions)
		{
			CodeMatcher cm = new(instructions);

			cm.Start()
				.SearchForward(a => a.operand is MethodInfo { Name: "Fire" })
				.Advance(-1)
				.SetOperandAndAdvance(.025f);
			
			return cm.InstructionEnumeration();
		}
	}

	[HarmonyPatch(typeof(ProjectileMoveScript), "checkCollision")]
	static class Patches__ProjectileMoveScript__checkCollision
	{
		static IEnumerable<CodeInstruction> Transpiler(
				IEnumerable<CodeInstruction> instructions)
		{
			CodeMatcher cm = new(instructions);
			
			cm.Start().
				SearchForward(a => a.operand is MethodInfo { Name: "get_magnitude" }).
				Advance(1).
				Insert(Transpilers.EmitDelegate((float f) => Enabled ? f + 1f : f)).
				SearchForward(a => a.operand is FieldInfo { Name: "previousPosition" }).
				Advance(1).
				Insert(
					new CodeInstruction(OpCodes.Ldloc_2, 2),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld,
						AccessTools.Field(typeof(ProjectileMoveScript), "firingEntity")),
					Transpilers.EmitDelegate((
								Vector3 pos,
								Vector3 dir,
								Entity firingEntity) => {
							if(Enabled){
								float halfHeight = firingEntity.height / 2;
								float dist = Mathf.Min(
									(firingEntity.position + (Vector3.up * halfHeight) - pos)
										.sqrMagnitude * .5f, 1f);
								return pos - (dir.normalized * dist);
							}

							return pos;
						}
					)
				);

			return cm.InstructionEnumeration();
		}
	}
}