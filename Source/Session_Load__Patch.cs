using System;
using HarmonyLib;

// Token: 0x02000010 RID: 16
[HarmonyPatch(typeof(Session), "Load")]
public class Session_Load__Patch
{
	// Token: 0x06000029 RID: 41 RVA: 0x000037EC File Offset: 0x000019EC
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
