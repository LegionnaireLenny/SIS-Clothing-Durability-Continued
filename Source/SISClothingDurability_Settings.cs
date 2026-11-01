using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISClothingDurability_Settings
{
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

    public bool LoggingActive = false;
    public bool EnabledForPlayer = true;
    public bool EnabledForPlayerCommunity = true;
    public bool EnabledForNPCs = true;
    public bool ClothesSpawnWithLessDurability = true;
    public float MinClothesDurabilityOnSpawnPcnt = 0.35f;
    public float MaxClothesDurabilityOnSpawnPcnt = 1f;
    public bool ZombiesSpawnWithHeavilyDamagedClothing = false;
    public bool CanBackpacksDegrade = true;
    public bool CanShoesDegrade = true;
    public bool CanGlassesDegrade = true;
    public float DurabilityDegradeRate = 1f;
    public float DurabilityDegradeOverTimePcnt = 0.01f;
    public bool UseDurabilityDegradeOverTimePcnt = false;
    public bool ClothingDegradesOverTimeForPlayer = true;
    public bool ClothingDegradesOverTimeForPlayerCommunity = true;
    public bool ClothingDegradesOverTimeForNPCs = false;
    public float ClothingDegradesOverTimeFrequency = 100f;
    public bool ClothingDegradesOnAttack = true;
    public bool ClothingDegradesOnDamaged = true;
    public bool ArmorProtectsFromDegradation = true;
    public float RepairClothingDurabilityIncreasePcnt = 0.5f;
    public float RepairClothingMaxDurabilityDecreasePcnt = 0.2f;
    public bool ShowStatusMsgOnClothingBroken = true;
    public bool CharactersSpeakOnClothingBroken = true;
    public bool PlaySoundOnClothingBroken = true;
    public bool ShowNotificationOnClothingBroken = true;
    public Color32 ClothingDurabilityBarColor = LiquidPrototype.Urine.Col;
    
    [XmlArray(ElementName = "ClothingTypes")]
    public List<ClothingTypeSettings> ClothingTypes = new List<ClothingTypeSettings>();

    [XmlArray(ElementName = "ExcludedTypes")]
    [XmlArrayItem(ElementName = "PrototypeName")]
    public List<string> ExcludedTypes = new List<string>();
}
