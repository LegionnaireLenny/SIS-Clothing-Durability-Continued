using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SISClothingDurability_Config
{
    public SISClothingDurability_Config()
    {
        SISClothingDurability_Config.Instance = this;
    }

    public static void Initialize()
    {
        StoryPath = GetStoryPath();
        SettingsFilePath = StoryPath + SettingsFilename;
        ClothingTypeSettingsOtherModsFilePath = StoryPath + ClothingTypeSettingsOtherModsFilename;
        ModIdentifierFilePath = StoryPath + ModIdentifierFilename;
        LogFilePath = StoryPath + LogFilename;

        if (File.Exists(LogFilePath))
        {
            string previous = StoryPath + LogFilename.Insert(LogFilename.IndexOf("."), ".previous");
            if (File.Exists(previous))
            {
                File.Delete(previous);
            }
            File.Move(LogFilePath, previous);
        }
    }

    public static string GetStoryPath()
    {
        foreach (Story story in GameImpl.Instance.CurrentStories)
        {
            if (File.Exists(story.Path + ModIdentifierFilename) ||
                File.Exists(story.Path + SettingsFilename))
            {
                return story.Path;
            }
        }
        return ".";
    }

    public static void SetSaveFilePath(string path)
    {
        SaveFilePath = path + SaveFilename;
    }

    public static void ClearSettings()
    {
        IsConfigLoaded = false;
        FillSettings(SISClothingDurability_Settings.GetDefault());
        ClothesTypes.Clear();
    }

    public static void LoadSettings()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                using (FileStream fileStream = new FileStream(SettingsFilePath, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISClothingDurability_Settings));
                    SISClothingDurability_Settings sisclothingDurability_Settings = xmlSerializer.Deserialize(fileStream) as SISClothingDurability_Settings;
                    FillSettings(sisclothingDurability_Settings);
                }
                Log($"Loaded settings from path {SettingsFilePath}");
            }
            else
            {
                SISClothingDurability_Settings @default = SISClothingDurability_Settings.GetDefault();
                FillSettings(@default);
                using (FileStream fileStream = File.Create(SettingsFilePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISClothingDurability_Settings));
                    xmlSerializer.Serialize(fileStream, @default);
                }
                Log("Loaded default settings since no settings file was found");
                Log($"Saved settings to {SettingsFilePath}");
            }
            LoadClothingTypeSettingsFromOtherMods();
        }
        catch (Exception ex)
        {
            Log($"Error loading settings: {ex}");
            FillSettings(SISClothingDurability_Settings.GetDefault());
            throw new Exception($"Error loading SISCD settings: {ex.Message}");
        }
        IsConfigLoaded = true;
    }

    public static void LoadClothingTypeSettingsFromOtherMods()
    {
        if (!File.Exists(ClothingTypeSettingsOtherModsFilePath))
        {
            return;
        }

        Log("Found Addon at " + ClothingTypeSettingsOtherModsFilePath);
        try
        {
            using (FileStream fileStream = new FileStream(ClothingTypeSettingsOtherModsFilePath, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISCD_ClothingTypeSettingsAddon));
                SISCD_ClothingTypeSettingsAddon siscd_ClothingTypeSettingsAddon = (SISCD_ClothingTypeSettingsAddon)xmlSerializer.Deserialize(fileStream);
                siscd_ClothingTypeSettingsAddon.ClothingTypes.ForEach(delegate (ClothingTypeSettings x)
                {
                    try
                    {
                        ClothesTypes.Add(x.Name, x);
                    }
                    catch (Exception ex)
                    {
                        Log($"Error adding {x.Name}: {ex.Message}");
                    }
                });
            }
            Log("Loaded ClothingTypeSettingsAddon from path " + ClothingTypeSettingsOtherModsFilePath);
        }
        catch (Exception ex)
        {
            Log("Error loading ClothingTypeSettingsAddon from path " + ClothingTypeSettingsOtherModsFilePath);
            throw new Exception("Error loading ClothingTypeSettingsAddon at " + ClothingTypeSettingsOtherModsFilePath + ";" + ex.Message);
        }
    }

    public static void Log(string message, bool alwaysLog = false)
    {
        try
        {
            if (LoggingEnabled || alwaysLog)
            {
                using (StreamWriter streamWriter = File.AppendText(LogFilePath))
                {
                    streamWriter.WriteLine($"{new string(FileLog.indentChar, FileLog.indentLevel)}[SISClothingDurability] {message}");
                }
            }
        }
        catch { }
    }

    public static void ReloadSettings()
    {
        IsConfigLoaded = false;
        LoadSettings();
    }

    private static void FillSettings(SISClothingDurability_Settings obj)
    {
        Log(JsonConvert.SerializeObject(obj));
        LoggingEnabled = obj.LoggingEnabled;
        EnabledForPlayer = obj.EnabledForPlayer;
        EnabledForNPCs = obj.EnabledForNPCs;
        EnabledForPlayerCommunity = obj.EnabledForPlayerCommunity;
        DurabilityDegradeRate = obj.DurabilityDegradeRate;
        DurabilityDegradeOverTimePercent = obj.DurabilityDegradeOverTimePercent;
        ClothingDegradesOnAttack = obj.ClothingDegradesOnAttack;
        ClothingDegradesOnDamaged = obj.ClothingDegradesOnDamaged;
        ClothingDegradesOverTimeForPlayer = obj.ClothingDegradesOverTimeForPlayer;
        ClothingDegradesOverTimeForPlayerCommunity = obj.ClothingDegradesOverTimeForPlayerCommunity;
        ClothingDegradesOverTimeForNPCs = obj.ClothingDegradesOverTimeForNPCs;
        ClothingDegradesOverTimeFrequency = obj.ClothingDegradesOverTimeFrequency;
        ArmorProtectsFromDegradation = obj.ArmorProtectsFromDegradation;
        ClothesSpawnWithLessDurability = obj.ClothesSpawnWithLessDurability;
        MinClothesDurabilityOnSpawnPercent = obj.MinClothesDurabilityOnSpawnPercent;
        MaxClothesDurabilityOnSpawnPercent = obj.MaxClothesDurabilityOnSpawnPercent;
        CanBackpacksDegrade = obj.CanBackpacksDegrade;
        CanShoesDegrade = obj.CanShoesDegrade;
        CanGlassesDegrade = obj.CanGlassesDegrade;
        ClothingDurabilityBarColor = obj.ClothingDurabilityBarColor;
        ZombiesSpawnWithHeavilyDamagedClothing = obj.ZombiesSpawnWithHeavilyDamagedClothing;
        UseDurabilityDegradeOverTimePercent = obj.UseDurabilityDegradeOverTimePercent;
        ShowStatusMsgOnClothingBroken = obj.ShowStatusMsgOnClothingBroken;
        CharactersSpeakOnClothingBroken = obj.CharactersSpeakOnClothingBroken;
        PlaySoundOnClothingBroken = obj.PlaySoundOnClothingBroken;
        ShowNotificationOnClothingBroken = obj.ShowNotificationOnClothingBroken;
        RepairClothingDurabilityIncreasePercent = obj.RepairClothingDurabilityIncreasePercent;
        RepairClothingMaxDurabilityDecreasePercent = obj.RepairClothingMaxDurabilityDecreasePercent;
        foreach (ClothingTypeSettings clothingTypeSettings in obj.ClothingTypes)
        {
            try
            {
                ClothesTypes.Add(clothingTypeSettings.Name, clothingTypeSettings);
            }
            catch (Exception ex)
            {
                Log($"Error adding {clothingTypeSettings.Name}: {ex.Message}");
            }
        }
        foreach (string prototypeName in obj.ExcludedTypes)
        {
            try
            {
                ExcludedTypes.Add(prototypeName);
            }
            catch (Exception ex)
            {
                Log($"Error adding {prototypeName}: {ex.Message}");
            }
        }
    }

    public static float GetMaxDurabilityForEquipment(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            if (ClothesTypes.TryGetValue(name, out ClothingTypeSettings clothingTypeSettings))
            {
                return clothingTypeSettings.MaxDurability;
            }
        }
        return ClothingTypeSettings.GetDefault().MaxDurability;
    }

    public static ClothingTypeSettings GetClothingModifiersForEquipment(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            if (ClothesTypes.TryGetValue(name, out ClothingTypeSettings clothingTypeSettings))
            {
                return clothingTypeSettings;
            }
        }
        return ClothingTypeSettings.GetDefault();
    }

    public static int GetClothingMaxPartsOnBroken(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            if (ClothesTypes.TryGetValue(name, out ClothingTypeSettings clothingTypeSettings))
            {
                return clothingTypeSettings.MaxPartsOnBroken;
            }
        }
        return ClothingTypeSettings.GetDefault().MaxPartsOnBroken;
    }

    public static int GetClothingMinPartsOnBroken(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            if (ClothesTypes.TryGetValue(name, out ClothingTypeSettings clothingTypeSettings))
            {
                return clothingTypeSettings.MinPartsOnBroken;
            }
        }
        return ClothingTypeSettings.GetDefault().MinPartsOnBroken;
    }

    public static void OnSaveGame(string Path)
    {
        if (SaveInProgress)
        {
            return;
        }

        SaveInProgress = true;
        Log($"Saving to {SaveFilePath}");
        SISClothingDurability_Manager.Instance.BuildListFromDictionary();
        using (FileStream fileStream = File.Create(SaveFilePath))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISClothingDurability_Manager));
            xmlSerializer.Serialize(fileStream, SISClothingDurability_Manager.Instance);
        }
        Log("Saving complete");
    }

    public static bool OnLoadGame(string Path)
    {
        if (LoadInProgress || !File.Exists(SaveFilePath))
        {
            return false;
        }

        LoadInProgress = true;
        Log($"Loading from {SaveFilePath}");
        using (FileStream fileStream = new FileStream(SaveFilePath, FileMode.Open))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISClothingDurability_Manager));
            SISClothingDurability_Manager.Instance = xmlSerializer.Deserialize(fileStream) as SISClothingDurability_Manager;
            SISClothingDurability_Manager.Instance.BuildDictionaryFromList();
        }
        Log("Loading complete");
        return true;
    }

    public static SISClothingDurability_Config Instance;
    public static string StoryPath = "";
    public static string SaveFilePath = "";
    public static string SettingsFilePath = "";
    public static string ClothingTypeSettingsOtherModsFilePath = "";
    public static string ModIdentifierFilePath = "";
    public static string LogFilePath = "";
    private static readonly string SaveFilename = "/Save_ClothingDurability.sav";
    private static readonly string SettingsFilename = "/Settings_ClothingDurability.xml";
    private static readonly string ClothingTypeSettingsOtherModsFilename = "/Settings_ClothingAddon.xml";
    private static readonly string ModIdentifierFilename = "/SISCLOTHINGDURABILITY.txt";
    private static readonly string LogFilename = "/Log_SISClothingDurability.txt";

    public static readonly string HeavySewingKitNameKey = "SISCD_HeavySewingKit";
    public static readonly string SewingKitNameKey = "SISCD_SewingKit";
    public static readonly string PrototypePartsOnClothingBroken = "Rag";
    public static readonly string SpeechClothingBrokenUniqueId = "SISCD_NPCClothingBroken";
    public static readonly string SpeechTranslateKey = "MEMORY_SISCD_YourClothingBroke";
    public static readonly string RepairNormalClothingRecipeUniqueId = "SISCD_RepairNormalClothing";
    public static readonly string RepairNormalClothingRecipeNameKey = "RECIPE_SISCD_RepairNormalClothing";
    public static readonly string RepairArmorClothingRecipeUniqueId = "SISCD_RepairArmorClothing";
    public static readonly string RepairArmorClothingRecipeNameKey = "RECIPE_SISCD_RepairArmorClothing";

    public static List<Resource<AudioClip>> SoundsClothingBroken = new List<Resource<AudioClip>>();

    public static bool SaveInProgress = false;
    public static bool LoadInProgress = false;
    public static bool IsConfigLoaded = false;

    public static bool LoggingEnabled = false;
    public static bool EnabledForPlayer;
    public static bool EnabledForPlayerCommunity;
    public static bool EnabledForNPCs;
    public static bool CanGlassesDegrade;
    public static bool CanBackpacksDegrade;
    public static bool CanShoesDegrade;

    public static bool ClothingDegradesOnAttack;
    public static bool ClothingDegradesOnDamaged;
    public static bool ArmorProtectsFromDegradation;

    public static bool ZombiesSpawnWithHeavilyDamagedClothing;
    public static bool ClothesSpawnWithLessDurability;
    public static float MinClothesDurabilityOnSpawnPercent;
    public static float MaxClothesDurabilityOnSpawnPercent;

    public static float DurabilityDegradeRate;

    public static bool ClothingDegradesOverTimeForPlayer;
    public static bool ClothingDegradesOverTimeForPlayerCommunity;
    public static bool ClothingDegradesOverTimeForNPCs;
    public static float ClothingDegradesOverTimeFrequency;
    public static bool UseDurabilityDegradeOverTimePercent;
    public static float DurabilityDegradeOverTimePercent;

    public static float RepairClothingDurabilityIncreasePercent;
    public static float RepairClothingMaxDurabilityDecreasePercent;

    public static bool ShowStatusMsgOnClothingBroken;
    public static bool CharactersSpeakOnClothingBroken;
    public static bool PlaySoundOnClothingBroken;
    public static bool ShowNotificationOnClothingBroken;

    public static Color32 ClothingDurabilityBarColor;

    public static Dictionary<string, ClothingTypeSettings> ClothesTypes = new Dictionary<string, ClothingTypeSettings>();
    public static List<string> ExcludedTypes = new List<string>();
}
