using HarmonyLib;
using System;

[HarmonyPatch(typeof(SaveGameManager), "Save")]
public class SaveGameManager_Save__Patch
{
    private static void Finalizer(SaveRequest request)
    {
        if (request.SaveGame == null || SISClothingDurability_Config.LoadInProgress)
        {
            return;
        }

        try
        {
            if (string.IsNullOrEmpty(SISClothingDurability_Config.SaveFilePath))
            {
                SISClothingDurability_Config.SetSaveFilePath($"{GameImpl.Instance.SaveGamePath}/{request.SaveGame.DirName}");
            }
            SISClothingDurability_Config.OnSaveGame(SISClothingDurability_Config.SaveFilePath);
            SISClothingDurability_Config.SaveInProgress = false;
        }
        catch (Exception ex)
        {
            SISClothingDurability_Config.Log($"Error saving: {ex}");
        }
    }
}
