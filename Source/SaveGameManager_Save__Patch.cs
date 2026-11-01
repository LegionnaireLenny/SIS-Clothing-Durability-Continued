using HarmonyLib;

[HarmonyPatch(typeof(SaveGameManager), "Save")]
public class SaveGameManager_Save__Patch
{
    private static void Finalizer(SaveRequest request)
    {
        bool flag = request.SaveGame == null || SISClothingDurability_Config.LoadInProgress;
        if (!flag)
        {
            SISClothingDurability_Config.OnSaveGame(GameImpl.Instance.SaveGamePath + "/" + request.SaveGame.DirName + "/");
            SISClothingDurability_Config.SaveInProgress = false;
        }
    }
}
