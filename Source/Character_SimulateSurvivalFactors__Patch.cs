using System;
using HarmonyLib;

[HarmonyPatch(typeof(Character), "SimulateSurvivalFactors")]
public class Character_SimulateSurvivalFactors__Patch
{
    private static void Postfix(Character __instance, TimeSpan curTime, float dts)
    {
        if ((!SISClothingDurability_Config.EnabledForPlayer && __instance == Hud.Instance.LocalControlledCharacter) ||
                (!SISClothingDurability_Config.EnabledForPlayerCommunity && __instance.IsControllableByPlayer() && __instance != Hud.Instance.LocalControlledCharacter) ||
                (!SISClothingDurability_Config.EnabledForNPCs && !__instance.IsControllableByPlayer()) ||
                (!SISClothingDurability_Config.ClothingDegradesOverTimeForPlayer && __instance == Hud.Instance.LocalControlledCharacter) ||
                (!SISClothingDurability_Config.ClothingDegradesOverTimeForPlayerCommunity && __instance.IsControllableByPlayer() && __instance != Hud.Instance.LocalControlledCharacter) ||
                (!SISClothingDurability_Config.ClothingDegradesOverTimeForNPCs && !__instance.IsOnPlayersTeam()))
        {
            return;
        }

        float num = SISClothingDurability_Manager.instance.GetCharacterUpdateInfo(__instance).Dts + dts;
        if (num > SISClothingDurability_Config.ClothingDegradesOverTimeFrequency)
        {
            SISClothingDurability_Manager.instance.SetCharacterUpdateInfo(__instance, curTime, 0f);
            SISClothingDurability_Config.Log($"Character_SimulateSurvivalFactors__Patch;{__instance.GetDisplayNameString()};{curTime};{dts};{num}");

            foreach (Equipment equipment in __instance.Clothes)
            {
                if (equipment != null && SISClothingDurability_Utils.IsPrototypeDegradableClothing(equipment.GetPrototype()))
                {
                    ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
                    if (clothingModifiersForEquipment.CanDegradeOverTime)
                    {
                        SISClothingDurability_Config.Log("ClothingType: " + equipment.GetClothingType().ToString() + ";" + equipment.GetDisplayNameString());
                        if (!SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipment))
                        {
                            SISClothingDurability_Manager.instance.AddEquipment(equipment, 15f, 15f);
                        }
                        SISClothingDurability_Manager.instance.ReduceDurabilityByPercentage(equipment, (SISClothingDurability_Config.UseDurabilityDegradeOverTimePcnt ? SISClothingDurability_Config.DurabilityDegradeOverTimePcnt : clothingModifiersForEquipment.DegradeOverTimePcnt) * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
                    }
                }

            }
            SISClothingDurability_Manager.instance.SetCharacterUpdateInfo(__instance, curTime, 0f);
        }
        else
        {
            SISClothingDurability_Manager.instance.SetCharacterUpdateInfo(__instance, curTime, num);
        }
    }
}
