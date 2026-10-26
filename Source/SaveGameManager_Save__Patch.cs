using System;
using HarmonyLib;

// Token: 0x0200000F RID: 15
[HarmonyPatch(typeof(SaveGameManager), "Save")]
public class SaveGameManager_Save__Patch
{
	// Token: 0x06000027 RID: 39 RVA: 0x00003798 File Offset: 0x00001998
	private static void Finalizer(SaveRequest request)
	{
		bool flag = request.SaveGame == null || SISClothingDurability_Config.LoadInProgress;
		if (!flag)
		{
			SISClothingDurability_Config.OnSaveGame(GameImpl.Instance.SaveGamePath + "/" + request.SaveGame.DirName + "/");
			SISClothingDurability_Config.SaveInProgress = false;
		}
	}
}
