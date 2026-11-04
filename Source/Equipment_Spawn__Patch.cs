using HarmonyLib;
using System;

[HarmonyPatch(typeof(Equipment), "Spawn", new Type[]
{
    typeof(EquipmentPrototype),
    typeof(int)
})]
public class Equipment_Spawn__Patch
{
    private static void Postfix(Equipment __result, Equipment __instance, EquipmentPrototype prototype, int amount)
    {
        if (__result == null ||
            !SISClothingDurability_Config.ClothesSpawnWithLessDurability ||
            !SISClothingDurability_Utils.IsPrototypeDegradableClothing(__result.GetPrototype()))
        {
            return;
        }

        ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(__result.GetPrototype().Name);
        float num = (float)SISClothingDurability_Utils.NextBetweenInts((int)(clothingModifiersForEquipment.MaxDurability * SISClothingDurability_Config.MinClothesDurabilityOnSpawnPercent), (int)(SISClothingDurability_Config.MaxClothesDurabilityOnSpawnPercent * clothingModifiersForEquipment.MaxDurability));
        SISClothingDurability_Config.Log($"Spawning {prototype.Name};{num};{clothingModifiersForEquipment.MaxDurability}");
        SISClothingDurability_Manager.Instance.AddEquipment(__result, num, clothingModifiersForEquipment.MaxDurability);
    }
}
