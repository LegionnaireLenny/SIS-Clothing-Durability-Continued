using HarmonyLib;

[HarmonyPatch(typeof(EquipmentNotification), "GetNewRecipeText")]
public class EquipmentNotification_GetNewRecipeText
{
    private static bool Prefix(ref string __result, EquipmentNotification __instance, Recipe recipe)
    {
        if (recipe == null)
        {
            __result = "";
            return false;
        }
        return true;
    }
}
