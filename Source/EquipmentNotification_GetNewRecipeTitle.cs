using System;
using HarmonyLib;

// Token: 0x02000008 RID: 8
[HarmonyPatch(typeof(EquipmentNotification), "GetNewRecipeTitle")]
public class EquipmentNotification_GetNewRecipeTitle
{
	// Token: 0x06000013 RID: 19 RVA: 0x00002F4C File Offset: 0x0000114C
	private static bool Prefix(ref string __result, EquipmentNotification __instance, Recipe recipe)
	{
		bool flag = recipe == null;
		bool flag2;
		if (flag)
		{
			__result = "";
			flag2 = false;
		}
		else
		{
			flag2 = true;
		}
		return flag2;
	}
}
