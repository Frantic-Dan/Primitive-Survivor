public class Init : IModApi
{
	public void InitMod(Mod _modInstance)
	{
		Harmony.CreateAndPatchAll(typeof(Init).Assembly, "ProjectileCollisionFix");
	}
}