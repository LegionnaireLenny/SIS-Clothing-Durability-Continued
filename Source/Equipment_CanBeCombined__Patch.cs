using HarmonyLib;

[HarmonyPatch(typeof(Equipment), "CanBeCombined")]
public class Equipment_CanBeCombined__Patch
{
    private static void Postfix(ref bool __result, Equipment __instance)
    {
        if (SISClothingDurability_Utils.IsPrototypeDegradableClothing(__instance.GetPrototype()))
        {
            __result = false;
        }
    }
}
