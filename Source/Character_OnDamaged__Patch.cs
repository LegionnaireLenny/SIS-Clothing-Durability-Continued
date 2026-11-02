using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

[HarmonyPatch(typeof(Character), "OnDamaged", new Type[]
{
    typeof(Character),
    typeof(TileObject),
    typeof(InjuryType),
    typeof(InjuryLocation),
    typeof(InfectionType),
    typeof(EquipmentPrototype),
    typeof(SkillType),
    typeof(float),
    typeof(float),
    typeof(Vector3),
    typeof(Vector3),
    typeof(Bone),
    typeof(Vector3),
    typeof(bool),
    typeof(bool),
    typeof(SecrecyMode),
    typeof(TargettableBodyLocation),
    typeof(float),
    typeof(bool)
}, new ArgumentType[]
{
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Ref,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Ref,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Normal,
    ArgumentType.Out
})]
public class Character_OnDamaged__Patch
{
    private static Equipment GetEquipmentAtInjuryLocation(Character character, InjuryLocation injuryLocation)
    {
        Equipment equipment = null;
        switch (injuryLocation)
        {
            case InjuryLocation.Torso:
            case InjuryLocation.LeftArm:
            case InjuryLocation.RightArm:
                {
                    if (character.Clothes[2] != null)
                    {
                        equipment = character.Clothes[2];
                    }
                    break;
                }
            case InjuryLocation.Head:
                {
                    if (character.Clothes[0] != null)
                    {
                        equipment = character.Clothes[0];
                    }
                    break;
                }
            case InjuryLocation.LeftLeg:
            case InjuryLocation.RightLeg:
                {
                    if (character.Clothes[3] != null)
                    {
                        equipment = character.Clothes[3];
                    }
                    break;
                }
        }
        return equipment;
    }

    private static List<Equipment> GetEquipmentsAtInjuryLocation(Character character, InjuryLocation injuryLocation)
    {
        List<Equipment> list = new List<Equipment>();
        switch (injuryLocation)
        {
            case InjuryLocation.Torso:
            case InjuryLocation.LeftArm:
            case InjuryLocation.RightArm:
                {
                    if (character.Clothes[2] != null)
                    {
                        list.Add(character.Clothes[2]);
                    }
                    break;
                }
            case InjuryLocation.Head:
                {
                    if (character.Clothes[0] != null)
                    {
                        list.Add(character.Clothes[0]);
                    }
                    if (character.Clothes[0] != null)
                    {
                        list.Add(character.Clothes[1]);
                    }
                    break;
                }
            case InjuryLocation.LeftLeg:
            case InjuryLocation.RightLeg:
                {
                    if (character.Clothes[3] != null)
                    {
                        list.Add(character.Clothes[3]);
                    }
                    if (character.Clothes[4] != null)
                    {
                        list.Add(character.Clothes[4]);
                    }
                    break;
                }
        }
        return list;
    }

    private static float GetDegradationPercent(Equipment equipment, InjuryType injuryType, float damage = 0f)
    {
        ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);

        float degradationPercent = 0f;
        switch (injuryType)
        {
            case InjuryType.Fire:
                degradationPercent = clothingModifiersForEquipment.DegradeOnFireDamagePcnt;
                break;
            case InjuryType.Explosion:
            case InjuryType.TrapExplosion:
                degradationPercent = clothingModifiersForEquipment.DegradeOnExplosionDamagePcnt;
                break;
            case InjuryType.VehicleImpact:
                degradationPercent = clothingModifiersForEquipment.DegradeOnVehicleImpactDamagePcnt;
                break;
            case InjuryType.Bullet:
                degradationPercent = clothingModifiersForEquipment.DegradeOnBulletDamagePcnt + clothingModifiersForEquipment.DegradeOnBulletDamagePcnt * damage * 2f;
                break;
            case InjuryType.Arrow:
                degradationPercent = clothingModifiersForEquipment.DegradeOnArrowDamagePcnt + clothingModifiersForEquipment.DegradeOnArrowDamagePcnt * damage * 2f;
                break;
            case InjuryType.ZombieBite:
                degradationPercent = clothingModifiersForEquipment.DegradeOnZombieBiteDamagePcnt;
                break;
            case InjuryType.SharpObject:
                degradationPercent = clothingModifiersForEquipment.DegradeOnSharpDamagePcnt + clothingModifiersForEquipment.DegradeOnSharpDamagePcnt * damage * 2f;
                break;
            case InjuryType.BluntObject:
                degradationPercent = clothingModifiersForEquipment.DegradeOnBluntDamagePcnt + clothingModifiersForEquipment.DegradeOnBluntDamagePcnt * damage * 2f;
                break;
            case InjuryType.Punch:
                degradationPercent = 0.05f;
                break;
            case InjuryType.Swallowed:
            case InjuryType.Invalid:
                break;
        }

        return degradationPercent;
    }

    private static void DamageSingleEquipment(Character __instance, InjuryType injuryType, Equipment equipment, float damage)
    {
        if (equipment == null ||
            !SISClothingDurability_Utils.IsPrototypeDegradableClothing(equipment.GetPrototype()) ||
            !SISClothingDurability_Utils.IsLocationProtected(__instance, equipment))
        {
            return;
        }

        ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
        if (!clothingModifiersForEquipment.CanDegradeOnDamaged)
        {
            return;
        }

        if (!SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipment))
        {
            SISClothingDurability_Manager.instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
        }
        SISClothingDurability_Manager.instance.ReduceDurabilityByPercentage(equipment, GetDegradationPercent(equipment, injuryType, damage) * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
    }

    private static void DamageAllEquipment(Character __instance, InjuryLocation injuryLocation, InjuryType injuryType)
    {
        List<Equipment> equipmentAtInjuryLocation = GetEquipmentsAtInjuryLocation(__instance, injuryLocation);
        foreach (Equipment equipment in __instance.Clothes)
        {
            if (equipment == null ||
                !SISClothingDurability_Utils.IsPrototypeDegradableClothing(equipment.GetPrototype()) ||
                !SISClothingDurability_Utils.IsLocationProtected(__instance, equipment))
            {
                continue;
            }

            ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
            if (!clothingModifiersForEquipment.CanDegradeOnDamaged)
            {
                continue;
            }

            if (!SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipment))
            {
                SISClothingDurability_Manager.instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
            }

            float degradationPercent = GetDegradationPercent(equipment, injuryType);
            bool isEquipmentAtInjuryLocation = equipmentAtInjuryLocation == null || equipmentAtInjuryLocation.Contains(equipment);
            // TODO: It looks like equipment not at the injury location is being damaged more, which seems backwards
            float num = isEquipmentAtInjuryLocation ? degradationPercent : (degradationPercent / 1.5f);
            SISClothingDurability_Manager.instance.ReduceDurabilityByPercentage(equipment, num * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
        }
    }

    private static void Postfix(Character __instance, Character source, TileObject intendedTarget, ref InjuryType injuryType, InjuryLocation injuryLocation, InfectionType infectionType, EquipmentPrototype ammoType, SkillType skillType, ref float damage, float hitRadius, Vector3 nonDeterministicHitPos, Vector3 nonDeterministicHitForce, Bone bone, Vector3 posInBoneSpace, bool dontReact, bool assassinate, SecrecyMode secret, TargettableBodyLocation targetBodyLocation, float fuel, out bool absorbedByVest)
    {
        absorbedByVest = false;
        if (__instance == null || 
            !SISClothingDurability_Config.ClothingDegradesOnDamaged ||
            (!SISClothingDurability_Config.EnabledForPlayer && __instance == Hud.Instance.LocalControlledCharacter) ||
            (!SISClothingDurability_Config.EnabledForPlayerCommunity && __instance.IsOnPlayersTeam() && __instance != Hud.Instance.LocalControlledCharacter) || 
            (!SISClothingDurability_Config.EnabledForNPCs && !__instance.IsControllableByPlayer()))
        {
            return;
        }

        SISClothingDurability_Config.Log($"OnDamaged; {__instance.GetDisplayNameString()};{injuryLocation};{injuryType};{damage} {(source != null ? source.GetDisplayNameString() : "NoSource")}");
        
        if (injuryType == InjuryType.Fire)
        {
            SISClothingDurability_Config.Log("Injury is Fire, damaging all clothes");
            DamageAllEquipment(__instance, injuryLocation, InjuryType.Fire);
        }
        else if (injuryType == InjuryType.Explosion)
        {
            SISClothingDurability_Config.Log("Injury is Explosion, damaging all clothes");
            DamageAllEquipment(__instance, injuryLocation, InjuryType.Explosion);
        }
        else if (injuryType == InjuryType.VehicleImpact)
        {
            SISClothingDurability_Config.Log("Injury is Vehicle impact, damaging all clothes");
            DamageAllEquipment(__instance, injuryLocation, InjuryType.VehicleImpact);
        }
        else
        {
            List<Equipment> equipmentDamagedByInjuryLocation = GetEquipmentsAtInjuryLocation(__instance, injuryLocation);
            foreach (Equipment equipment in equipmentDamagedByInjuryLocation)
            {
                DamageSingleEquipment(__instance, injuryType, equipment, damage);
            }
            //if (!(injuryLocation == InjuryLocation.Head && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.Glasses)))
            //{
            //    if (SISClothingDurability_Utils.IsInjuryLocationBottom(injuryLocation) && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.Shoes))
            //    {
            //    }
            //}
        }
    }

}
