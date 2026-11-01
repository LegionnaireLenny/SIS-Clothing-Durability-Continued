using HarmonyLib;

[HarmonyPatch(typeof(EquipmentNotification), "GetNewRecipeTitle")]
public class EquipmentNotification_GetNewRecipeTitle
{
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
