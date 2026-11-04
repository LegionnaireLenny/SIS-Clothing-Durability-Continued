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
        if (!SISClothingDurability_Config.ClothingDegradesOnAttack ||
            (attackType != AttackType.Low &&
            attackType != AttackType.High &&
            attackType != AttackType.Middle &&
            attackType != AttackType.Snap &&
            attackType != AttackType.BludgeonUnconscious &&
            attackType != AttackType.KillUnconscious &&
            attackType != AttackType.AttackJumpingZombie))
        {
            return;
        }

        foreach (Equipment equipment in __instance.Clothes)
        {
            if (equipment != null && SISClothingDurability_Utils.IsPrototypeDegradableClothing(equipment.GetPrototype()))
            {
                ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
                if (clothingModifiersForEquipment.CanDegradeOnAttack)
                {
                    if (!SISClothingDurability_Manager.Instance.IsEquipmentRegistered(equipment))
                    {
                        SISClothingDurability_Manager.Instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
                    }
                    float degradeOverTimePcnt = clothingModifiersForEquipment.DegradeOverTimePcnt;
                    SISClothingDurability_Manager.Instance.ReduceDurability(equipment, degradeOverTimePcnt * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
                }
            }
        }
    }
}
