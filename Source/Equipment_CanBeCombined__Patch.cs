using System;
using HarmonyLib;

[HarmonyPatch(typeof(Equipment), "CanBeCombined")]
public class Equipment_CanBeCombined__Patch
{
	private static void Postfix(ref bool __result, Equipment __instance)
	{
		bool flag = SISClothingDurability_Utils.IsPrototypeNormalClothing(__instance.GetPrototype());
		if (flag)
		{
			__result = false;
		}
	}
}
