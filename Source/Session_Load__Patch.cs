using System;
using HarmonyLib;

[HarmonyPatch(typeof(Session), "Load")]
public class Session_Load__Patch
{
	private static void Finalizer(Session __instance)
	{
		SISClothingDurability_Config.ReloadSettings();
		SISClothingDurability_Config.Log("Loaded!");
		object value = AccessTools.Field(typeof(GameImpl), "LoadingSessionInfo").GetValue(GameImpl.Instance);
		bool flag = value != null && !SISClothingDurability_Config.SaveInProgress;
		if (flag)
		{
			SaveGame saveGame = (SaveGame)AccessTools.Field(AccessTools.TypeByName("LoadSessionInfo"), "SaveGame").GetValue(value);
			bool flag2 = saveGame != null;
			if (flag2)
			{
				SISClothingDurability_Config.OnLoadGame(GameImpl.Instance.SaveGamePath + "/" + saveGame.DirName + "/");
				SISClothingDurability_Config.LoadInProgress = false;
			}
		}
	}
}
