using System;
using HarmonyLib;

// Token: 0x02000007 RID: 7
[HarmonyPatch(typeof(EquipmentNotification), "GetNewRecipeText")]
public class EquipmentNotification_GetNewRecipeText
{
	// Token: 0x06000011 RID: 17 RVA: 0x00002F24 File Offset: 0x00001124
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
