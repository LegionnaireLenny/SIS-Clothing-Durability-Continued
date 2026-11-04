using HarmonyLib;

[HarmonyPatch(typeof(EquipmentNotification), "GetNewRecipeText")]
public class EquipmentNotification_GetNewRecipeText
{
    private static bool Prefix(ref string __result, EquipmentNotification __instance, Recipe recipe)
    {
        SISClothingDurability_Config.Log($"GetNewRecipeText - {__instance?.NotificationType};{__instance?.Item?.GetDisplayNameString()};{recipe?.UniqueID}");
        if (recipe == null)
        {
            __result = "";
            return false;
        }
        return true;
    }
}
