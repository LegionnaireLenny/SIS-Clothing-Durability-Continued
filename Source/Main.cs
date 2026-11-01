using HarmonyLib;

public class Main
{
    public static void Load()
    {
        Main.HarmonyInstance = new Harmony("SISClothingDurability");
        Main.HarmonyInstance.PatchAll();
        Main.SISCDManager = new SISClothingDurability_Manager();
        SISClothingDurability_Config.ReloadSettings();
    }

    public static void Unload()
    {
        bool flag = Main.HarmonyInstance != null;
        if (flag)
        {
            Main.HarmonyInstance.UnpatchAll(null);
        }
    }

    public static Harmony HarmonyInstance;

    public static SISClothingDurability_Manager SISCDManager;
}
