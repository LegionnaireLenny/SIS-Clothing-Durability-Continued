using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Recipe), "UseIngredientsAndCreateProduct")]
public class Recipe_UseIngredientsAndCreateProduct__Patch
{
    private static Weapon FindWeaponOfPrototypeInInventory(Character carrier, EquipmentPrototype equipmentPrototype)
    {
        return carrier.Inventory.FindItemOfType(equipmentPrototype) as Weapon;
    }

    private static Equipment CreateProductReturningCreated(Recipe recipe, TileObject carrier, Community community, Equipment usingItem, TerrainCoord destTile, Prop.OrientationType orientation, float ingredientsNutrition, InfectionType ingredientsInfectedWith, out float numProduced, bool addToGatheredItems)
    {
        numProduced = 0f;
        if (recipe.ProductPrototype == null)
        {
            return null;
        }

        Character character = carrier as Character;
        Equipment equipment = null;
        for (int i = 0; i < recipe.ProductAmount; i++)
        {
            Equipment tempEquipment = Equipment.Spawn(recipe.ProductPrototype);
            if (recipe.CanPassInfectionToProduct)
            {
                tempEquipment.Infect(ingredientsInfectedWith);
            }

            if (addToGatheredItems && character != null)
            {
                tempEquipment.SetGathered();
            }

            tempEquipment.ColorVariation = usingItem.ColorVariation;
            tempEquipment.ColorVariation2 = usingItem.ColorVariation2;
            tempEquipment.ColorVariation3 = usingItem.ColorVariation3;
            tempEquipment.MaterialVariation = usingItem.MaterialVariation;
            equipment = carrier.GetInventory().Add(carrier, tempEquipment);
            numProduced += 1f;
        }

        if (character != null && equipment != null)
        {
            NotificationManager.Instance.AddEquipmentNotification(null, character, equipment, 1);
        }
        return equipment;
    }

    private static bool Prefix(ref float __result, Recipe __instance, Character carrier, Community community, Equipment usingItem, TerrainCoord destTile, Prop.OrientationType orientation, bool addToGatheredItems, bool checkEquipmentPolicy)
    {
        if (!__instance.UniqueID.StartsWith("SISCD_Repair") ||
            !__instance.HasAllIngredients(carrier, carrier, usingItem, checkEquipmentPolicy ? carrier : null))
        {
            return true;
        }

        SISClothingDurability_Config.Log($"HERE Equipped item or usingItem: {((usingItem != null) ? usingItem.GetDisplayNameString() : "NoUsingItem")};{((carrier.EquippedItem != null) ? carrier.EquippedItem.GetDisplayNameString() : "NoEquippedItem")}");
        __result = 1f;
        if (usingItem == null)
        {
            return false;
        }

        SISClothingDurability_Config.Log($"Clothing found : {usingItem.GetDisplayNameString()};{usingItem.Id}");
        SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo = SISClothingDurability_Manager.Instance.GetDefaultClothesDurabilityInfo(usingItem);
        if (SISClothingDurability_Manager.Instance.IsEquipmentRegistered(usingItem))
        {
            SISClothingDurability_Config.Log($"Durability: {SISClothingDurability_Manager.Instance.GetDurability(usingItem)}");
            siscd_ClothesDurabilityInfo = SISClothingDurability_Manager.Instance.GetClothesDurabilityInfo(usingItem);
        }

        SISClothingDurability_Manager.Instance.RemoveEquipment(usingItem);
        __instance.UseIngredients(carrier, usingItem, out float nutrition, out InfectionType infectionType, checkEquipmentPolicy);
        Equipment equipment = Recipe_UseIngredientsAndCreateProduct__Patch.CreateProductReturningCreated(__instance, carrier, community, usingItem, destTile, orientation, nutrition, infectionType, out _, addToGatheredItems);
        SISClothingDurability_Config.Log($"Clothing created : {equipment.GetDisplayNameString()};{equipment.Id}");

        float durability = siscd_ClothesDurabilityInfo.Durability;
        float maxDurability = siscd_ClothesDurabilityInfo.MaxDurability;
        if (__instance.UniqueID.StartsWith(SISClothingDurability_Config.RepairNormalClothingRecipeUniqueId))
        {
            durability += SISClothingDurability_Config.RepairClothingDurabilityIncreasePercent * siscd_ClothesDurabilityInfo.MaxDurability;
            maxDurability -= SISClothingDurability_Config.RepairClothingMaxDurabilityDecreasePercent * siscd_ClothesDurabilityInfo.MaxDurability;
            durability = Mathf.Clamp(durability, 0f, maxDurability);
        }
        SISClothingDurability_Manager.Instance.RemoveEquipment(equipment);
        SISClothingDurability_Manager.Instance.AddEquipment(equipment, durability, maxDurability);
        return false;
    }
}
