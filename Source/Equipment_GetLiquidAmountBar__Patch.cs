using System;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Equipment), "GetLiquidAmountBar")]
public class Equipment_GetLiquidAmountBar__Patch
{
	private static void Postfix(ref bool __result, Equipment __instance, ref float amount, ref Color col)
	{
		bool flag = SISClothingDurability_Utils.IsPrototypeNormalClothing(__instance.GetPrototype());
		if (flag)
		{
			bool flag2 = SISClothingDurability_Manager.instance.IsEquipmentRegistered(__instance) && SISClothingDurability_Config.ExcludedTypes.IndexOf(__instance.GetPrototype().Name) == -1;
			if (flag2)
			{
				bool flag3 = __instance.InfectedWith == 0;
				if (flag3)
				{
					amount = SISClothingDurability_Manager.instance.GetDurability(__instance) / SISClothingDurability_Manager.instance.GetMaxDurability(__instance);
					col = SISClothingDurability_Config.ClothingDurabilityBarColor;
					__result = true;
				}
				else
				{
					SISClothingDurability_Config.Log("Showing infect bar");
				}
			}
		}
	}
}
