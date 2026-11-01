using System;
using System.Collections.Generic;
using HarmonyLib;
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
	private static Equipment GetEquipmentDamagedByInjuryLocation(Character character, InjuryLocation injuryLocation)
	{
		Equipment equipment = null;
		switch (injuryLocation)
		{
		case InjuryLocation.Torso:
		case InjuryLocation.LeftArm:
		case InjuryLocation.RightArm:
		{
			bool flag = character.Clothes[2] != null;
			if (flag)
			{
				equipment = character.Clothes[2];
			}
			break;
		}
		case InjuryLocation.Head:
		{
			bool flag2 = character.Clothes[0] != null;
			if (flag2)
			{
				equipment = character.Clothes[0];
			}
			break;
		}
		case InjuryLocation.LeftLeg:
		case InjuryLocation.RightLeg:
		{
			bool flag3 = character.Clothes[3] != null;
			if (flag3)
			{
				equipment = character.Clothes[3];
			}
			break;
		}
		}
		return equipment;
	}

	private static List<Equipment> GetEquipmentsDamagedByInjuryLocation(Character character, InjuryLocation injuryLocation)
	{
		List<Equipment> list = new List<Equipment>();
		switch (injuryLocation)
		{
		case InjuryLocation.Torso:
		case InjuryLocation.LeftArm:
		case InjuryLocation.RightArm:
		{
			bool flag = character.Clothes[2] != null;
			if (flag)
			{
				list.Add(character.Clothes[2]);
			}
			break;
		}
		case InjuryLocation.Head:
		{
			bool flag2 = character.Clothes[0] != null;
			if (flag2)
			{
				list.Add(character.Clothes[0]);
			}
			bool flag3 = character.Clothes[0] != null;
			if (flag3)
			{
				list.Add(character.Clothes[1]);
			}
			break;
		}
		case InjuryLocation.LeftLeg:
		case InjuryLocation.RightLeg:
		{
			bool flag4 = character.Clothes[3] != null;
			if (flag4)
			{
				list.Add(character.Clothes[3]);
			}
			bool flag5 = character.Clothes[4] != null;
			if (flag5)
			{
				list.Add(character.Clothes[4]);
			}
			break;
		}
		}
		return list;
	}

	private static void ApplyFireDamage(Character __instance, InjuryLocation injuryLocation)
	{
		SISClothingDurability_Config.Log("Injury is Fire, damaging all clothes");
		Equipment equipmentDamagedByInjuryLocation = Character_OnDamaged__Patch.GetEquipmentDamagedByInjuryLocation(__instance, injuryLocation);
		Equipment[] clothes = __instance.Clothes;
		int i = 0;
		while (i < clothes.Length)
		{
			Equipment equipment = clothes[i];
			bool flag = equipment != null && SISClothingDurability_Utils.IsPrototypeNormalClothing(equipment.GetPrototype()) && SISClothingDurability_Utils.CanClothingTypeDegrade(equipment.GetClothingType());
			if (flag)
			{
				bool flag2 = SISClothingDurability_Config.ArmorProtectsFromDegradation && equipment.GetClothingType() == ClothingType.Top && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.BodyArmor);
				if (!flag2)
				{
					bool flag3 = SISClothingDurability_Config.ArmorProtectsFromDegradation && equipment.GetClothingType() == ClothingType.Bottom && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.LegArmor);
					if (!flag3)
					{
						bool flag4 = equipmentDamagedByInjuryLocation == null || equipment == equipmentDamagedByInjuryLocation;
						ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
						bool flag5 = !clothingModifiersForEquipment.CanDegradeOnDamaged;
						if (!flag5)
						{
							bool flag6 = !SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipment);
							if (flag6)
							{
								SISClothingDurability_Manager.instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
							}
							float num = (flag4 ? clothingModifiersForEquipment.DegradeOnFireDamagePcnt : (clothingModifiersForEquipment.DegradeOnFireDamagePcnt / 1.5f));
							SISClothingDurability_Manager.instance.ReduceDurabilityByPercentage(equipment, num * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
						}
					}
				}
			}
			i++;
		}
	}

	private static void ApplyExplosionDamage(Character __instance, InjuryLocation injuryLocation)
	{
		Equipment equipmentDamagedByInjuryLocation = Character_OnDamaged__Patch.GetEquipmentDamagedByInjuryLocation(__instance, injuryLocation);
		Equipment[] clothes = __instance.Clothes;
		int i = 0;
		while (i < clothes.Length)
		{
			Equipment equipment = clothes[i];
			bool flag = equipment != null && SISClothingDurability_Utils.IsPrototypeNormalClothing(equipment.GetPrototype()) && SISClothingDurability_Utils.CanClothingTypeDegrade(equipment.GetClothingType());
			if (flag)
			{
				bool flag2 = SISClothingDurability_Config.ArmorProtectsFromDegradation && equipment.GetClothingType() == ClothingType.Top && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.BodyArmor);
				if (!flag2)
				{
					bool flag3 = SISClothingDurability_Config.ArmorProtectsFromDegradation && equipment.GetClothingType() == ClothingType.Bottom && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.LegArmor);
					if (!flag3)
					{
						bool flag4 = equipmentDamagedByInjuryLocation == null || equipment == equipmentDamagedByInjuryLocation;
						ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
						bool flag5 = !clothingModifiersForEquipment.CanDegradeOnDamaged;
						if (!flag5)
						{
							bool flag6 = !SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipment);
							if (flag6)
							{
								SISClothingDurability_Manager.instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
							}
							float num = (flag4 ? clothingModifiersForEquipment.DegradeOnExplosionDamagePcnt : (clothingModifiersForEquipment.DegradeOnExplosionDamagePcnt / 1.5f));
							SISClothingDurability_Manager.instance.ReduceDurabilityByPercentage(equipment, num * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
						}
					}
				}
			}
			i++;
		}
	}

	private static void ApplyVehicleImpactDamage(Character __instance, InjuryLocation injuryLocation)
	{
		Equipment equipmentDamagedByInjuryLocation = Character_OnDamaged__Patch.GetEquipmentDamagedByInjuryLocation(__instance, injuryLocation);
		Equipment[] clothes = __instance.Clothes;
		int i = 0;
		while (i < clothes.Length)
		{
			Equipment equipment = clothes[i];
			bool flag = equipment != null && SISClothingDurability_Utils.IsPrototypeNormalClothing(equipment.GetPrototype()) && SISClothingDurability_Utils.CanClothingTypeDegrade(equipment.GetClothingType());
			if (flag)
			{
				bool flag2 = SISClothingDurability_Config.ArmorProtectsFromDegradation && equipment.GetClothingType() == ClothingType.Top && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.BodyArmor);
				if (!flag2)
				{
					bool flag3 = SISClothingDurability_Config.ArmorProtectsFromDegradation && equipment.GetClothingType() == ClothingType.Bottom && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.LegArmor);
					if (!flag3)
					{
						bool flag4 = equipmentDamagedByInjuryLocation == null || equipment == equipmentDamagedByInjuryLocation;
						ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipment.GetPrototype().Name);
						bool flag5 = !clothingModifiersForEquipment.CanDegradeOnDamaged;
						if (!flag5)
						{
							bool flag6 = !SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipment);
							if (flag6)
							{
								SISClothingDurability_Manager.instance.AddEquipment(equipment, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
							}
							float num = (flag4 ? clothingModifiersForEquipment.DegradeOnVehicleImpactDamagePcnt : (clothingModifiersForEquipment.DegradeOnVehicleImpactDamagePcnt / 1.5f));
							SISClothingDurability_Manager.instance.ReduceDurabilityByPercentage(equipment, num * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
						}
					}
				}
			}
			i++;
		}
	}

	private static void Postfix(Character __instance, Character source, TileObject intendedTarget, ref InjuryType injuryType, InjuryLocation injuryLocation, InfectionType infectionType, EquipmentPrototype ammoType, SkillType skillType, ref float damage, float hitRadius, Vector3 nonDeterministicHitPos, Vector3 nonDeterministicHitForce, Bone bone, Vector3 posInBoneSpace, bool dontReact, bool assassinate, SecrecyMode secret, TargettableBodyLocation targetBodyLocation, float fuel, out bool absorbedByVest)
	{
		absorbedByVest = false;
		bool flag = __instance == null;
		if (!flag)
		{
			bool flag2 = !SISClothingDurability_Config.ClothingDegradesOnDamaged;
			if (!flag2)
			{
				bool flag3 = !SISClothingDurability_Config.EnabledForPlayer && __instance == Hud.Instance.LocalControlledCharacter;
				if (!flag3)
				{
					bool flag4 = !SISClothingDurability_Config.EnabledForPlayerCommunity && __instance.Community != null && __instance.Community.CommunityType == null && __instance != Hud.Instance.LocalControlledCharacter;
					if (!flag4)
					{
						bool flag5 = !SISClothingDurability_Config.EnabledForNPCs && !__instance.IsControllableByPlayer();
						if (!flag5)
						{
							SISClothingDurability_Config.Log(string.Concat(new string[]
							{
								"OnDamaged;",
								__instance.GetDisplayNameString(),
								";",
								injuryLocation.ToString(),
								";",
								injuryType.ToString(),
								";",
								damage.ToString(),
								";",
								(source != null) ? source.GetDisplayNameString() : "NoSource"
							}));
							bool flag6 = injuryType == InjuryType.Fire;
							if (flag6)
							{
								SISClothingDurability_Config.Log("Injury is Fire, damaging all clothes");
								Character_OnDamaged__Patch.ApplyFireDamage(__instance, injuryLocation);
							}
							else
							{
								bool flag7 = injuryType == InjuryType.Explosion;
								if (flag7)
								{
									SISClothingDurability_Config.Log("Injury is Explosion, damaging all clothes");
									Character_OnDamaged__Patch.ApplyExplosionDamage(__instance, injuryLocation);
								}
								else
								{
									bool flag8 = injuryType == InjuryType.VehicleImpact;
									if (flag8)
									{
										SISClothingDurability_Config.Log("Injury is Vehicle impact, damaging all clothes");
										Character_OnDamaged__Patch.ApplyVehicleImpactDamage(__instance, injuryLocation);
									}
									else
									{
										Equipment equipmentDamagedByInjuryLocation = Character_OnDamaged__Patch.GetEquipmentDamagedByInjuryLocation(__instance, injuryLocation);
										bool flag9 = equipmentDamagedByInjuryLocation != null;
										if (flag9)
										{
											SISClothingDurability_Config.Log("OnDamaged Damaged Clothing " + equipmentDamagedByInjuryLocation.GetDisplayNameString());
											ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equipmentDamagedByInjuryLocation.GetPrototype().Name);
											bool flag10 = !SISClothingDurability_Manager.instance.IsEquipmentRegistered(equipmentDamagedByInjuryLocation);
											if (flag10)
											{
												SISClothingDurability_Manager.instance.AddEquipment(equipmentDamagedByInjuryLocation, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
											}
											float num = 0f;
											bool flag11 = source != null && source.EquippedItem == null && injuryType == InjuryType.Punch;
											if (flag11)
											{
												SISClothingDurability_Manager.instance.ReduceDurabilityByPercentage(equipmentDamagedByInjuryLocation, 0.05f * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
												return;
											}
											switch (injuryType)
											{
											case InjuryType.Bullet:
												num = clothingModifiersForEquipment.DegradeOnBulletDamagePcnt + clothingModifiersForEquipment.DegradeOnBulletDamagePcnt * damage * 2f;
												break;
											case InjuryType.Arrow:
												num = clothingModifiersForEquipment.DegradeOnArrowDamagePcnt + clothingModifiersForEquipment.DegradeOnArrowDamagePcnt * damage * 2f;
												break;
											case InjuryType.ZombieBite:
												num = clothingModifiersForEquipment.DegradeOnZombieBiteDamagePcnt;
												break;
											case InjuryType.SharpObject:
												num = clothingModifiersForEquipment.DegradeOnSharpDamagePcnt + clothingModifiersForEquipment.DegradeOnSharpDamagePcnt * damage * 2f;
												break;
											case InjuryType.BluntObject:
												num = clothingModifiersForEquipment.DegradeOnBluntDamagePcnt + clothingModifiersForEquipment.DegradeOnBluntDamagePcnt * damage * 2f;
												break;
											case InjuryType.TrapExplosion:
												num = clothingModifiersForEquipment.DegradeOnTrapExplosionDamagePcnt;
												break;
											}
											SISClothingDurability_Manager.instance.ReduceDurabilityByPercentage(equipmentDamagedByInjuryLocation, num * clothingModifiersForEquipment.DegradeRate * SISClothingDurability_Config.DurabilityDegradeRate);
										}
										bool flag12 = injuryLocation == InjuryLocation.Head && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.Glasses);
										if (!flag12)
										{
											bool flag13 = SISClothingDurability_Utils.IsInjuryLocationBottom(injuryLocation) && SISClothingDurability_Utils.IsWearingClothingType(__instance, ClothingType.Shoes);
											if (flag13)
											{
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
