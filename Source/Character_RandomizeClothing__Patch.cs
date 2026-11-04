using HarmonyLib;
using System;

[HarmonyPatch(typeof(Character), "RandomizeClothing", new Type[]
{
    typeof(CustomRandom),
    typeof(bool),
    typeof(bool),
    typeof(bool),
    typeof(bool),
    typeof(bool),
    typeof(bool)
})]
public class Character_RandomizeClothing__Patch
{
    private static void Postfix(Human __instance, CustomRandom rand, bool seasonallyAppropriate, bool isPortrait, bool mustHaveBackpack, bool mustHaveBodyArmor, bool mustHaveHelmet, bool mustHaveLegArmor)
    {
        if (!SISClothingDurability_Config.ZombiesSpawnWithHeavilyDamagedClothing ||
            !__instance.Zombie)
        {
            return;
        }

        SISClothingDurability_Config.Log("Spawned is zombie, damaging all clothing");
        foreach (Equipment equipment in __instance.Clothes)
        {
            if (equipment == null ||
                !SISClothingDurability_Utils.IsPrototypeDegradableClothing(equipment.GetPrototype()))
            {
                continue;
            }

            SISClothingDurability_Config.Log("- clothing: " + equipment.GetPrototype().Name);
            ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
            if (!SISClothingDurability_Manager.Instance.IsEquipmentRegistered(equipment))
            {
                SISClothingDurability_Manager.Instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
            }
            SISCD_ClothesDurabilityInfo clothesDurabilityInfo = SISClothingDurability_Manager.Instance.GetClothesDurabilityInfo(equipment);
            float num = (float)SISClothingDurability_Utils.NextBetweenInts(75, 90) / 100f * clothesDurabilityInfo.Durability;
            SISClothingDurability_Config.Log("--- To degrade: " + num.ToString());
            SISClothingDurability_Manager.Instance.ReduceDurability(equipment, num);
        }
    }
}
