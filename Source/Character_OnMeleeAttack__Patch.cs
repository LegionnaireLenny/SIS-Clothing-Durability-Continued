using HarmonyLib;
using System;

[HarmonyPatch(typeof(Character), "OnMeleeAttack", new Type[]
{
    typeof(Character),
    typeof(AttackType),
    typeof(InjuryType),
    typeof(Bone),
    typeof(float),
    typeof(bool),
    typeof(bool)
})]
public class Character_OnMeleeAttack__Patch
{
    private static void Postfix(Character __instance, Character targetCharacter, AttackType attackType, InjuryType injuryType, Bone bone, float damage, bool assassinate, bool stealthy)
    {
        SISClothingDurability_Config.Log($"{__instance?.GetDisplayNameString()} attacked {targetCharacter?.GetDisplayNameString()} with {__instance?.EquippedItem?.GetDisplayNameString() ?? "unarmed attack"};{attackType};{injuryType};{SISClothingDurability_Utils.IsClothingDegradationEnabledForCharacter(__instance)}");
        if (!SISClothingDurability_Utils.IsClothingDegradationEnabledForCharacter(__instance) ||
            !SISClothingDurability_Config.ClothingDegradesOnAttack ||
            (attackType != AttackType.Low &&
            attackType != AttackType.Middle &&
            attackType != AttackType.High &&
            attackType != AttackType.Snap &&
            attackType != AttackType.AttackJumpingZombie &&
            attackType != AttackType.Punch &&
            attackType != AttackType.Kick &&
            attackType != AttackType.BludgeonUnconscious &&
            attackType != AttackType.KillUnconscious &&
            attackType != AttackType.PunchHigh &&
            attackType != AttackType.PunchLow))
        {
            return;
        }

        SISClothingDurability_Config.Log($"Character_OnMeleeAttack__Patch: Iterating over equipment: {__instance.Clothes.Length}");
        foreach (Equipment equipment in __instance.Clothes)
        {
            if (equipment != null && SISClothingDurability_Utils.IsPrototypeDegradableClothing(equipment.GetPrototype()))
            {
                ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
                if (clothingModifiersForEquipment.CanDegradeOnAttack)
                {
                    if (!SISClothingDurability_Manager.Instance.IsEquipmentRegistered(equipment))
                    {
                        SISClothingDurability_Config.Log($"Registering equipment");
                        SISClothingDurability_Manager.Instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
                    }
                    SISClothingDurability_Manager.Instance.ReduceDurability(equipment, clothingModifiersForEquipment.DegradeOverTimePcnt * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
                }
            }
        }
    }
}
