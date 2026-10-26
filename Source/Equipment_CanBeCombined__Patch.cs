using System;
using HarmonyLib;

// Token: 0x02000009 RID: 9
[HarmonyPatch(typeof(Equipment), "CanBeCombined")]
public class Equipment_CanBeCombined__Patch
{
	// Token: 0x06000015 RID: 21 RVA: 0x00002F74 File Offset: 0x00001174
	private static void Postfix(ref bool __result, Equipment __instance)
	{
		bool flag = SISClothingDurability_Utils.IsPrototypeNormalClothing(__instance.GetPrototype());
		if (flag)
		{
			__result = false;
		}
	}
}
