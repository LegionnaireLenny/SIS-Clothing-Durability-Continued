using System;
using HarmonyLib;

// Token: 0x0200000D RID: 13
public class Main
{
	// Token: 0x06000020 RID: 32 RVA: 0x0000341B File Offset: 0x0000161B
	public static void Load()
	{
		Main.HarmonyInstance = new Harmony("SISClothingDurability");
		Main.HarmonyInstance.PatchAll();
		Main.SISCDManager = new SISClothingDurability_Manager();
		SISClothingDurability_Config.ReloadSettings();
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00003448 File Offset: 0x00001648
	public static void Unload()
	{
		bool flag = Main.HarmonyInstance != null;
		if (flag)
		{
			Main.HarmonyInstance.UnpatchAll(null);
		}
	}

	// Token: 0x04000005 RID: 5
	public static Harmony HarmonyInstance;

	// Token: 0x04000006 RID: 6
	public static SISClothingDurability_Manager SISCDManager;
}
