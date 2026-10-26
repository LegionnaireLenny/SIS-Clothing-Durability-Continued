using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x02000014 RID: 20
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISClothingDurability_Settings
{
	// Token: 0x0600002F RID: 47 RVA: 0x00003AE4 File Offset: 0x00001CE4
	public static SISClothingDurability_Settings GetDefault()
	{
		return new SISClothingDurability_Settings
		{
			ClothingTypes = 
			{
				new ClothingTypeSettings("Jacket", 1f, 20f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("SleevelessJacket", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("LongSleevedTShirt", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Jacket_Camo", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("TracksuitTop", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Vest_Camo", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("TrenchCoat", 1f, 30f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Shirt", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Vest", 1f, 15f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Hoodie", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("LuckyCamoJacket", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("DoctorCoat", 1f, 20f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Trousers", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("CombatTrousers", 1f, 15f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("CombatTrousers_Camo", 1f, 15f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("CowboyHat", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("MilitaryCap", 1f, 12f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("BaseballCap", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("MilitaryCap_Camo", 1f, 12f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("BaseballCap_Camo", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Beanie_Camo", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Balaklava_Camo", 1f, 15f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Beanie", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Balaklava", 1f, 15f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("CowboyHat_Camo", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("DrillSargeantsCap", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("SheriffHat", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Sunglasses", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Sunglasses2", 1f, 10f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("BuckledBoots", 1f, 20f, 0, 1, 0.005f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, false, true, true, 0.01f),
				new ClothingTypeSettings("CombatBoots", 1f, 30f, 0, 1, 0.005f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, false, true, true, 0.01f),
				new ClothingTypeSettings("Backpack_Large", 1f, 45f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Backpack_Large_Camo", 1f, 45f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Backpack_Medium", 1f, 30f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f),
				new ClothingTypeSettings("Backpack_Small", 1f, 15f, 0, 1, 0.01f, 0.6f, 0.35f, 0.3f, 0.15f, 0.25f, 0.5f, 0.6f, 0.6f, 0.2f, 1f, true, true, true, 0.01f)
			},
			ExcludedTypes = { "ExcludedClothingPrototypeName" }
		};
	}

	// Token: 0x0400002A RID: 42
	public bool LoggingActive = false;

	// Token: 0x0400002B RID: 43
	public bool EnabledForPlayer = true;

	// Token: 0x0400002C RID: 44
	public bool EnabledForPlayerCommunity = true;

	// Token: 0x0400002D RID: 45
	public bool EnabledForNPCs = true;

	// Token: 0x0400002E RID: 46
	public bool ClothesSpawnWithLessDurability = true;

	// Token: 0x0400002F RID: 47
	public float MinClothesDurabilityOnSpawnPcnt = 0.35f;

	// Token: 0x04000030 RID: 48
	public float MaxClothesDurabilityOnSpawnPcnt = 1f;

	// Token: 0x04000031 RID: 49
	public bool ZombiesSpawnWithHeavilyDamagedClothing = false;

	// Token: 0x04000032 RID: 50
	public bool CanBackpacksDegrade = true;

	// Token: 0x04000033 RID: 51
	public bool CanShoesDegrade = true;

	// Token: 0x04000034 RID: 52
	public bool CanGlassesDegrade = true;

	// Token: 0x04000035 RID: 53
	public float DurabilityDegradeRate = 1f;

	// Token: 0x04000036 RID: 54
	public float DurabilityDegradeOverTimePcnt = 0.01f;

	// Token: 0x04000037 RID: 55
	public bool UseDurabilityDegradeOverTimePcnt = false;

	// Token: 0x04000038 RID: 56
	public bool ClothingDegradesOverTimeForPlayer = true;

	// Token: 0x04000039 RID: 57
	public bool ClothingDegradesOverTimeForPlayerCommunity = true;

	// Token: 0x0400003A RID: 58
	public bool ClothingDegradesOverTimeForNPCs = false;

	// Token: 0x0400003B RID: 59
	public float ClothingDegradesOverTimeFrequency = 100f;

	// Token: 0x0400003C RID: 60
	public bool ClothingDegradesOnAttack = true;

	// Token: 0x0400003D RID: 61
	public bool ClothingDegradesOnDamaged = true;

	// Token: 0x0400003E RID: 62
	public bool ArmorProtectsFromDegradation = true;

	// Token: 0x0400003F RID: 63
	public float RepairClothingDurabilityIncreasePcnt = 0.5f;

	// Token: 0x04000040 RID: 64
	public float RepairClothingMaxDurabilityDecreasePcnt = 0.2f;

	// Token: 0x04000041 RID: 65
	public bool ShowStatusMsgOnClothingBroken = true;

	// Token: 0x04000042 RID: 66
	public bool CharactersSpeakOnClothingBroken = true;

	// Token: 0x04000043 RID: 67
	public bool PlaySoundOnClothingBroken = true;

	// Token: 0x04000044 RID: 68
	public bool ShowNotificationOnClothingBroken = true;

	// Token: 0x04000045 RID: 69
	public Color32 ClothingDurabilityBarColor = LiquidPrototype.Urine.Col;

	// Token: 0x04000046 RID: 70
	[XmlArray(ElementName = "ClothingTypes")]
	public List<ClothingTypeSettings> ClothingTypes = new List<ClothingTypeSettings>();

	// Token: 0x04000047 RID: 71
	[XmlArray(ElementName = "ExcludedTypes")]
	[XmlArrayItem(ElementName = "PrototypeName")]
	public List<string> ExcludedTypes = new List<string>();
}
