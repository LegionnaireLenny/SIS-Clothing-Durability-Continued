using System;
using HarmonyLib;

[HarmonyPatch(typeof(Equipment), "Spawn", new Type[]
{
	typeof(EquipmentPrototype),
	typeof(int)
})]
public class Equipment_Spawn__Patch
{
	private static void Postfix(Equipment __result, Equipment __instance, EquipmentPrototype prototype, int amount)
	{
		bool flag = __result == null;
		if (!flag)
		{
			bool flag2 = !SISClothingDurability_Config.ClothesSpawnWithLessDurability;
			if (!flag2)
			{
				bool flag3 = SISClothingDurability_Utils.IsPrototypeNormalClothing(__result.GetPrototype()) && SISClothingDurability_Utils.CanClothingTypeDegrade(__result.GetClothingType());
				if (flag3)
				{
					ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(__result.GetPrototype().Name);
					float num = (float)SISClothingDurability_Utils.NextBetweenInts((int)(clothingModifiersForEquipment.MaxDurability * SISClothingDurability_Config.MinClothesDurabilityOnSpawnPcnt), (int)(SISClothingDurability_Config.MaxClothesDurabilityOnSpawnPcnt * clothingModifiersForEquipment.MaxDurability));
					SISClothingDurability_Config.Log(string.Concat(new string[]
					{
						"Spawning ",
						prototype.Name,
						";",
						num.ToString(),
						";",
						clothingModifiersForEquipment.MaxDurability.ToString()
					}));
					SISClothingDurability_Manager.instance.AddEquipment(__result, num, clothingModifiersForEquipment.MaxDurability);
				}
			}
		}
	}
}
