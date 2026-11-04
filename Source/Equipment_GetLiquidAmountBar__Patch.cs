using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Equipment), "GetLiquidAmountBar")]
public class Equipment_GetLiquidAmountBar__Patch
{
    private static void Postfix(ref bool __result, Equipment __instance, ref float amount, ref Color col)
    {
        if (!SISClothingDurability_Utils.IsPrototypeDegradableClothing(__instance.GetPrototype()) ||
            !SISClothingDurability_Manager.Instance.IsEquipmentRegistered(__instance) || 
            SISClothingDurability_Config.ExcludedTypes.IndexOf(__instance.GetPrototype().Name) != -1)
        {
            return;
        }

        if (__instance.InfectedWith == 0)
        {
            amount = SISClothingDurability_Manager.Instance.GetDurability(__instance) / SISClothingDurability_Manager.Instance.GetMaxDurability(__instance);
            col = SISClothingDurability_Config.ClothingDurabilityBarColor;
            __result = true;
        }
        else
        {
            SISClothingDurability_Config.Log("Showing infect bar");
        }
    }
}
