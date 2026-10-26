using System;
using HarmonyLib;

// Token: 0x0200000B RID: 11
[HarmonyPatch(typeof(Equipment), "Spawn", new Type[]
{
	typeof(EquipmentPrototype),
	typeof(int)
})]
public class Equipment_Spawn__Patch
{
	// Token: 0x06000019 RID: 25 RVA: 0x00003030 File Offset: 0x00001230
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
