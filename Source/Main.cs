using HarmonyLib;

public class Main
{
    public static Harmony HarmonyInstance;
    public static SISClothingDurability_Manager SISCDManager;

    public static void Load()
    {
        SISCDManager = new SISClothingDurability_Manager();
        SISClothingDurability_Config.Initialize();
        SISClothingDurability_Config.LoadSettings();
        HarmonyInstance = new Harmony("SISClothingDurability");
        HarmonyInstance.PatchAll();
    }

    public static void Unload()
    {
        if (HarmonyInstance != null)
        {
            HarmonyInstance.UnpatchAll(null);
        }
    }
}
