using System;
using HarmonyLib;

// Token: 0x02000005 RID: 5
[HarmonyPatch(typeof(Character), "SimulateSurvivalFactors")]
public class Character_SimulateSurvivalFactors__Patch
{
	// Token: 0x0600000C RID: 12 RVA: 0x00002B54 File Offset: 0x00000D54
	private static void Postfix(Character __instance, TimeSpan curTime, float dts)
	{
		bool flag = !SISClothingDurability_Config.EnabledForPlayer && __instance == Hud.Instance.LocalControlledCharacter;
		if (!flag)
		{
			bool flag2 = !SISClothingDurability_Config.EnabledForPlayerCommunity && __instance.IsControllableByPlayer() && __instance != Hud.Instance.LocalControlledCharacter;
			if (!flag2)
			{
				bool flag3 = !SISClothingDurability_Config.EnabledForNPCs && !__instance.IsControllableByPlayer();
				if (!flag3)
				{
					bool flag4 = !SISClothingDurability_Config.ClothingDegradesOverTimeForPlayer && __instance == Hud.Instance.LocalControlledCharacter;
					if (!flag4)
					{
						bool flag5 = !SISClothingDurability_Config.ClothingDegradesOverTimeForPlayerCommunity && __instance.Community != null && __instance.Community.CommunityType == null && __instance != Hud.Instance.LocalControlledCharacter;
						if (!flag5)
						{
							bool flag6 = !SISClothingDurability_Config.ClothingDegradesOverTimeForNPCs && !__instance.IsOnPlayersTeam();
							if (!flag6)
							{
								float num = SISClothingDurability_Manager.instance.GetCharacterUpdateInfo(__instance).Dts + dts;
								bool flag7 = num > SISClothingDurability_Config.ClothingDegradesOverTimeFrequency;
								if (flag7)
								{
									SISClothingDurability_Manager.instance.SetCharacterUpdateInfo(__instance, curTime, 0f);
									string[] array = new string[8];
									array[0] = "Character_SimulateSurvivalFactors__Patch;";
									array[1] = __instance.GetDisplayNameString();
									array[2] = ";";
									int num2 = 3;
									TimeSpan timeSpan = curTime;
									array[num2] = timeSpan.ToString();
									array[4] = ";";
									array[5] = dts.ToString();
									array[6] = ";";
									array[7] = num.ToString();
									SISClothingDurability_Config.Log(string.Concat(array));
									Equipment[] clothes = __instance.Clothes;
									int i = 0;
									while (i < clothes.Length)
									{
										Equipment equipment = clothes[i];
										bool flag8 = equipment != null && SISClothingDurability_Utils.IsPrototypeNormalClothing(equipment.GetPrototype()) && SISClothingDurability_Utils.CanClothingTypeDegrade(equipment.GetClothingType());
										if (flag8)
										{
											bool flag9 = (!SISClothingDurability_Config.CanBackpacksDegrade && equipment.GetClothingType() == 5) || (!SISClothingDurability_Config.CanShoesDegrade && equipment.GetClothingType() == 4) || (!SISClothingDurability_Config.CanGlassesDegrade && equipment.GetClothingType() == 1);
											if (!flag9)
											{
												ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
												bool flag10 = !clothingModifiersForEquipment.CanDegradeOverTime;
												if (!flag10)
												{
													SISClothingDurability_Config.Log("ClothingType: " + equipment.GetClothingType().ToString() + ";" + equipment.GetDisplayNameString());
													bool flag11 = !SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipment);
													if (flag11)
													{
														SISClothingDurability_Manager.instance.AddEquipment(equipment, 15f, 15f);
													}
													SISClothingDurability_Manager.instance.ReduceDurabilityByPercentage(equipment, (SISClothingDurability_Config.UseDurabilityDegradeOverTimePcnt ? SISClothingDurability_Config.DurabilityDegradeOverTimePcnt : clothingModifiersForEquipment.DegradeOverTimePcnt) * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
												}
											}
										}
										IL_02BF:
										i++;
										continue;
										goto IL_02BF;
									}
									SISClothingDurability_Manager.instance.SetCharacterUpdateInfo(__instance, curTime, 0f);
								}
								else
								{
									SISClothingDurability_Manager.instance.SetCharacterUpdateInfo(__instance, curTime, num);
								}
							}
						}
					}
				}
			}
		}
	}
}
