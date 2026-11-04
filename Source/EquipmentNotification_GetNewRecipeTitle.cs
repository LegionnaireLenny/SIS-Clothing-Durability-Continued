using HarmonyLib;

[HarmonyPatch(typeof(EquipmentNotification), "GetNewRecipeTitle")]
public class EquipmentNotification_GetNewRecipeTitle
{
    private static bool Prefix(ref string __result, EquipmentNotification __instance, Recipe recipe)
    {
        SISClothingDurability_Config.Log($"GetNewRecipeTitle - {__instance?.NotificationType};{__instance?.Item?.GetDisplayNameString()};{recipe?.UniqueID}");
        if (recipe == null)
        {
            __result = "";
            return false;
        }
        return true;
    }
}
