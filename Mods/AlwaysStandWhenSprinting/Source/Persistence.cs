namespace StandWhenRunning;

public static class Persistence
{
	const string ConfigFileName = "data";

	public static bool Load()
	{
		if(Common.ModInst is not {} modInst
				|| Path.Combine(modInst.Path, ConfigFileName) is not {} path
				|| string.IsNullOrWhiteSpace(path)
				|| !File.Exists(path)){
			return false;
		}

		byte[] bytes = File.ReadAllBytes(path);
		Common.StandWhenRunning = bytes[0] != 0;
		return true;
	}

	public static void Save()
	{
		if(Common.ModInst is not {} modInst){
			return;
		}

		string path = Path.Combine(modInst.Path, ConfigFileName);
		byte[] bytes = new byte[1];
		bytes[0] = (byte)(Common.StandWhenRunning ? 1 : 0);
		File.WriteAllBytes(path, bytes);
	}
}