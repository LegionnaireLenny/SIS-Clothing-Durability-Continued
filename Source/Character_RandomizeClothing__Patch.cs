using System;
using HarmonyLib;

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
		bool flag = __instance.Zombie && SISClothingDurability_Config.ZombiesSpawnWithHeavilyDamagedClothing;
		if (flag)
		{
			SISClothingDurability_Config.Log("Spawned is zombie, damaging all clothing");
			foreach (Equipment equipment in __instance.Clothes)
			{
				bool flag2 = equipment != null && SISClothingDurability_Utils.IsPrototypeNormalClothing(equipment.GetPrototype()) && SISClothingDurability_Utils.CanClothingTypeDegrade(equipment.GetClothingType());
				if (flag2)
				{
					SISClothingDurability_Config.Log("- clothing: " + equipment.GetPrototype().Name);
					ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
					bool flag3 = !SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipment);
					if (flag3)
					{
						SISClothingDurability_Manager.instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
					}
					SISCD_ClothesDurabilityInfo clothesDurabilityInfo = SISClothingDurability_Manager.instance.GetClothesDurabilityInfo(equipment);
					float num = (float)SISClothingDurability_Utils.NextBetweenInts(75, 90) / 100f * clothesDurabilityInfo.Durability;
					SISClothingDurability_Config.Log("--- To degrade: " + num.ToString());
					SISClothingDurability_Manager.instance.ReduceDurability(equipment, num);
				}
			}
		}
	}
}
