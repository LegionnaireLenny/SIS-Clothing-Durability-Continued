using System;
using HarmonyLib;

// Token: 0x0200000E RID: 14
[HarmonyPatch(typeof(Recipe), "UseIngredientsAndCreateProduct")]
public class Recipe_UseIngredientsAndCreateProduct__Patch
{
	// Token: 0x06000023 RID: 35 RVA: 0x00003470 File Offset: 0x00001670
	private static Weapon FindWeaponOfPrototypeInInventory(Character carrier, EquipmentPrototype equipmentPrototype)
	{
		Equipment equipment = carrier.Inventory.FindItemOfType(equipmentPrototype);
		bool flag = equipment != null;
		Weapon weapon;
		if (flag)
		{
			weapon = (Weapon)equipment;
		}
		else
		{
			weapon = null;
		}
		return weapon;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000034A4 File Offset: 0x000016A4
	private static Equipment CreateProductReturningCreated(Recipe recipe, TileObject carrier, Community community, Equipment usingItem, TerrainCoord destTile, Prop.OrientationType orientation, float ingredientsNutrition, InfectionType ingredientsInfectedWith, out float numProduced, bool addToGatheredItems)
	{
		Character character = carrier as Character;
		numProduced = 1f;
		EquipmentPrototype productPrototype = recipe.ProductPrototype;
		bool flag = productPrototype != null;
		Equipment equipment3;
		if (flag)
		{
			Equipment equipment = null;
			for (int i = 0; i < recipe.ProductAmount; i++)
			{
				Equipment equipment2 = Equipment.Spawn(productPrototype);
				bool canPassInfectionToProduct = recipe.CanPassInfectionToProduct;
				if (canPassInfectionToProduct)
				{
					equipment2.Infect(ingredientsInfectedWith);
				}
				bool flag2 = addToGatheredItems && character != null;
				if (flag2)
				{
					equipment2.SetGathered();
				}
				equipment = carrier.GetInventory().Add(carrier, equipment2);
				numProduced += 1f;
			}
			bool flag3 = character != null && equipment != null;
			if (flag3)
			{
				NotificationManager.Instance.AddEquipmentNotification(null, character, equipment, 1);
			}
			equipment3 = equipment;
		}
		else
		{
			equipment3 = null;
		}
		return equipment3;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00003574 File Offset: 0x00001774
	private static bool Prefix(ref float __result, Recipe __instance, Character carrier, Community community, Equipment usingItem, TerrainCoord destTile, Prop.OrientationType orientation, bool addToGatheredItems, bool checkEquipmentPolicy)
	{
		bool flag = __instance.UniqueID.StartsWith("SISCD_Repair");
		bool flag2 = !flag;
		bool flag3;
		if (flag2)
		{
			flag3 = true;
		}
		else
		{
			bool flag4 = !__instance.HasAllIngredients(carrier, carrier, usingItem, checkEquipmentPolicy ? carrier : null);
			if (flag4)
			{
				flag3 = true;
			}
			else
			{
				__result = 1f;
				SISClothingDurability_Config.Log("HERE Equipped item or usingItem: " + ((usingItem != null) ? usingItem.GetDisplayNameString() : "NoUsingItem") + ";" + ((carrier.EquippedItem != null) ? carrier.EquippedItem.GetDisplayNameString() : "NoEquippedItem"));
				bool flag5 = __instance.UniqueID.StartsWith(SISClothingDurability_Config.RepairNormalClothingRecipeUniqueId);
				float num = 0f;
				bool flag6 = usingItem != null;
				if (flag6)
				{
					SISClothingDurability_Config.Log("Clothing found : " + usingItem.GetDisplayNameString() + ";" + usingItem.Id.ToString());
					float num2 = 0f;
					InfectionType infectionType = 0;
					SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo = SISClothingDurability_Manager.instance.GetDefaultClothesDurabilityInfo(usingItem);
					bool flag7 = SISClothingDurability_Manager.instance.IsEquipmentRegistered(usingItem);
					if (flag7)
					{
						SISClothingDurability_Config.Log("Durability: " + SISClothingDurability_Manager.instance.GetDurability(usingItem).ToString());
						siscd_ClothesDurabilityInfo = SISClothingDurability_Manager.instance.GetClothesDurabilityInfo(usingItem);
					}
					SISClothingDurability_Manager.instance.RemoveEquipment(usingItem);
					__instance.UseIngredients(carrier, usingItem, ref num2, ref infectionType, checkEquipmentPolicy);
					Equipment equipment = Recipe_UseIngredientsAndCreateProduct__Patch.CreateProductReturningCreated(__instance, carrier, community, usingItem, destTile, orientation, num2, infectionType, out num, addToGatheredItems);
					SISClothingDurability_Config.Log("Clothing created : " + equipment.GetDisplayNameString() + ";" + equipment.Id.ToString());
					float num3 = siscd_ClothesDurabilityInfo.Durability;
					float num4 = siscd_ClothesDurabilityInfo.MaxDurability;
					bool flag8 = flag5;
					if (flag8)
					{
						num3 = siscd_ClothesDurabilityInfo.Durability + SISClothingDurability_Config.RepairClothingDurabilityIncreasePcnt * siscd_ClothesDurabilityInfo.MaxDurability;
						num4 = siscd_ClothesDurabilityInfo.MaxDurability - SISClothingDurability_Config.RepairClothingMaxDurabilityDecreasePcnt * siscd_ClothesDurabilityInfo.MaxDurability;
					}
					bool flag9 = num3 > num4;
					if (flag9)
					{
						num3 = num4;
					}
					SISClothingDurability_Manager.instance.RemoveEquipment(equipment);
					SISClothingDurability_Manager.instance.AddEquipment(equipment, num3, num4);
				}
				flag3 = false;
			}
		}
		return flag3;
	}
}
