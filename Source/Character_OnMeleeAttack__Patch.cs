using System;
using HarmonyLib;

// Token: 0x02000004 RID: 4
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
	// Token: 0x0600000A RID: 10 RVA: 0x00002A38 File Offset: 0x00000C38
	private static void Postfix(Character __instance, Character targetCharacter, AttackType attackType, InjuryType injuryType, Bone bone, float damage, bool assassinate, bool stealthy)
	{
		bool flag = !SISClothingDurability_Config.ClothingDegradesOnAttack;
		if (!flag)
		{
			bool flag2 = attackType != 1 && attackType != 3 && attackType != 2 && attackType != 4 && attackType != 10 && attackType != 11 && attackType != 5;
			if (!flag2)
			{
				Equipment[] clothes = __instance.Clothes;
				int i = 0;
				while (i < clothes.Length)
				{
					Equipment equipment = clothes[i];
					bool flag3 = equipment != null && SISClothingDurability_Utils.IsPrototypeNormalClothing(equipment.GetPrototype()) && SISClothingDurability_Utils.CanClothingTypeDegrade(equipment.GetClothingType());
					if (flag3)
					{
						ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
						bool flag4 = !clothingModifiersForEquipment.CanDegradeOnAttack;
						if (!flag4)
						{
							bool flag5 = !SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipment);
							if (flag5)
							{
								SISClothingDurability_Manager.instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
							}
							float degradeOverTimePcnt = clothingModifiersForEquipment.DegradeOverTimePcnt;
							SISClothingDurability_Manager.instance.ReduceDurability(equipment, degradeOverTimePcnt * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
						}
					}
					IL_0101:
					i++;
					continue;
					goto IL_0101;
				}
			}
		}
	}
}
