using HarmonyLib;
using System;

[HarmonyPatch(typeof(Session), "Load")]
public class Session_Load__Patch
{
    private static bool IsFirstLoad = true;

    private static void Finalizer(Session __instance)
    {
        if (SISClothingDurability_Config.SaveInProgress ||
            GameImpl.Instance?.LoadingSessionInfo == null)
        {
            return;
        }

        if (!IsFirstLoad)
        {
            SISClothingDurability_Config.ReloadSettings();
            SISClothingDurability_Config.Log("Settings reloaded!");
        }
        IsFirstLoad = false;

        try
        {
            if (string.IsNullOrEmpty(SISClothingDurability_Config.SaveFilePath))
            {
                SISClothingDurability_Config.SetSaveFilePath($"{GameImpl.Instance.SaveGamePath}/{GameImpl.Instance.LoadingSessionInfo.SaveGame.DirName}");
            }
            SISClothingDurability_Config.OnLoadGame(SISClothingDurability_Config.SaveFilePath);
            SISClothingDurability_Config.LoadInProgress = false;
        }
        catch (Exception ex)
        {
            SISClothingDurability_Config.Log($"Error loading save: {ex}");
        }
    }
}
