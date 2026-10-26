using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using HarmonyLib;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class SISClothingDurability_Config
{
	// Token: 0x06000031 RID: 49 RVA: 0x00004967 File Offset: 0x00002B67
	public SISClothingDurability_Config()
	{
		SISClothingDurability_Config.instance = this;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00004978 File Offset: 0x00002B78
	private static void GetAndAddSounds()
	{
		foreach (Story story in GameImpl.Instance.CurrentStories)
		{
			string text = story.Path + SISClothingDurability_Config.ModIdentifier;
			string text2 = story.Path + SISClothingDurability_Config.SettingsFilePath;
			bool flag = File.Exists(text) || File.Exists(text2);
			if (flag)
			{
				foreach (Resource<AudioClip> resource in story.Sounds)
				{
					SISClothingDurability_Config.Log("Added sound " + resource.GetPath());
					SISClothingDurability_Config.SoundsClothingBroken.Add(resource);
				}
			}
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00004A74 File Offset: 0x00002C74
	public static void ResetSettings()
	{
		SISClothingDurability_Config.IsConfigLoaded = false;
		SISClothingDurability_Config.FillSettings(SISClothingDurability_Settings.GetDefault());
		SISClothingDurability_Config.IsConfigLoaded = true;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004A8E File Offset: 0x00002C8E
	public static void ClearSettings()
	{
		SISClothingDurability_Config.IsConfigLoaded = false;
		SISClothingDurability_Config.FillSettings(SISClothingDurability_Settings.GetDefault());
		SISClothingDurability_Config.clothesTypes.Clear();
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00004AB0 File Offset: 0x00002CB0
	public static void LoadSettings()
	{
		try
		{
			bool flag = false;
			foreach (Story story in GameImpl.Instance.CurrentStories)
			{
				string text = story.Path + SISClothingDurability_Config.SettingsFilePath;
				bool flag2 = File.Exists(text);
				if (flag2)
				{
					flag = true;
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISClothingDurability_Settings));
					FileStream fileStream = new FileStream(text, FileMode.Open);
					SISClothingDurability_Settings sisclothingDurability_Settings = (SISClothingDurability_Settings)xmlSerializer.Deserialize(fileStream);
					fileStream.Dispose();
					SISClothingDurability_Config.FillSettings(sisclothingDurability_Settings);
					SISClothingDurability_Config.Log("Loaded settings from path " + text);
				}
			}
			bool flag3 = !flag;
			if (flag3)
			{
				SISClothingDurability_Settings @default = SISClothingDurability_Settings.GetDefault();
				SISClothingDurability_Config.FillSettings(@default);
				SISClothingDurability_Config.Log("Loaded default settings since no settings file was found");
				SISClothingDurability_Config.Log(JsonConvert.SerializeObject(@default));
				foreach (Story story2 in GameImpl.Instance.CurrentStories)
				{
					string text2 = story2.Path + SISClothingDurability_Config.ModIdentifier;
					bool flag4 = File.Exists(text2);
					if (flag4)
					{
						SISClothingDurability_Config.SaveSettings(SISClothingDurability_Settings.GetDefault(), story2.Path);
						SISClothingDurability_Config.Log("Saved settings file at path " + story2.Path);
					}
				}
			}
			try
			{
				SISClothingDurability_Config.LoadClothingTypeSettingsFromOtherMods();
			}
			catch (Exception ex)
			{
				SISClothingDurability_Config.Log("Error loading Weapon Type Addons from other mods: " + ex.Message);
			}
		}
		catch (Exception ex2)
		{
			SISClothingDurability_Config.Log(ex2.ToString());
			SISClothingDurability_Config.Log("Error loading settings");
			SISClothingDurability_Config.FillSettings(SISClothingDurability_Settings.GetDefault());
			throw new Exception("Error loading SISCD settings: " + ex2.Message);
		}
		SISClothingDurability_Config.IsConfigLoaded = true;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00004CF0 File Offset: 0x00002EF0
	private static void SaveSettings(SISClothingDurability_Settings settings, string folderPath)
	{
		string text = folderPath + SISClothingDurability_Config.SettingsFilePath;
		SISClothingDurability_Config.Log("Saving to " + text);
		bool flag = File.Exists(text);
		if (flag)
		{
			File.Delete(text);
		}
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISClothingDurability_Settings));
		FileStream fileStream = File.Create(text);
		xmlSerializer.Serialize(fileStream, settings);
		fileStream.Dispose();
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00004D58 File Offset: 0x00002F58
	public static void LoadClothingTypeSettingsFromOtherMods()
	{
		foreach (Story story in GameImpl.Instance.CurrentStories)
		{
			string text = story.Path + SISClothingDurability_Config.ClothingTypeSettingsOtherMods;
			bool flag = File.Exists(text);
			if (flag)
			{
				SISClothingDurability_Config.Log("Found Addon at " + text);
				try
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISCD_ClothingTypeSettingsAddon));
					FileStream fileStream = new FileStream(text, FileMode.Open);
					SISCD_ClothingTypeSettingsAddon siscd_ClothingTypeSettingsAddon = (SISCD_ClothingTypeSettingsAddon)xmlSerializer.Deserialize(fileStream);
					fileStream.Dispose();
					siscd_ClothingTypeSettingsAddon.ClothingTypes.ForEach(delegate(ClothingTypeSettings x)
					{
						try
						{
							SISClothingDurability_Config.clothesTypes.Add(x.Name, x);
						}
						catch (Exception ex2)
						{
							SISClothingDurability_Config.Log("Error adding " + x.Name + ": " + ex2.Message);
						}
					});
					SISClothingDurability_Config.Log("Loaded ClothingTypeSettingsAddon from path " + text);
				}
				catch (Exception ex)
				{
					SISClothingDurability_Config.Log("Error loading ClothingTypeSettingsAddon from path " + text);
					throw new Exception("Error loading ClothingTypeSettingsAddon at " + text + ";" + ex.Message);
				}
			}
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00004E94 File Offset: 0x00003094
	public static void Log(string msg)
	{
		bool flag = !SISClothingDurability_Config.LoggingActive;
		if (!flag)
		{
			try
			{
				FileLog.Log("[SISCD] " + msg);
			}
			catch
			{
			}
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00004EDC File Offset: 0x000030DC
	public static void ReloadSettings()
	{
		SISClothingDurability_Config.ResetSettings();
		SISClothingDurability_Config.LoadSettings();
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004EEC File Offset: 0x000030EC
	private static void FillSettings(SISClothingDurability_Settings obj)
	{
		FileLog.logPath = SISClothingDurability_Config.GetStoryPath() + SISClothingDurability_Config.LogFilePath;
		bool flag = File.Exists(FileLog.logPath);
		if (flag)
		{
			File.Delete(FileLog.logPath);
		}
		SISClothingDurability_Config.Log(JsonConvert.SerializeObject(obj));
		SISClothingDurability_Config.GetAndAddSounds();
		SISClothingDurability_Config.LoggingActive = obj.LoggingActive;
		SISClothingDurability_Config.EnabledForPlayer = obj.EnabledForPlayer;
		SISClothingDurability_Config.EnabledForNPCs = obj.EnabledForNPCs;
		SISClothingDurability_Config.EnabledForPlayerCommunity = obj.EnabledForPlayerCommunity;
		SISClothingDurability_Config.DurabilityDegradeRate = obj.DurabilityDegradeRate;
		SISClothingDurability_Config.DurabilityDegradeOverTimePcnt = obj.DurabilityDegradeOverTimePcnt;
		SISClothingDurability_Config.ClothingDegradesOnAttack = obj.ClothingDegradesOnAttack;
		SISClothingDurability_Config.ClothingDegradesOnDamaged = obj.ClothingDegradesOnDamaged;
		SISClothingDurability_Config.ClothingDegradesOverTimeForPlayer = obj.ClothingDegradesOverTimeForPlayer;
		SISClothingDurability_Config.ClothingDegradesOverTimeForPlayerCommunity = obj.ClothingDegradesOverTimeForPlayerCommunity;
		SISClothingDurability_Config.ClothingDegradesOverTimeForNPCs = obj.ClothingDegradesOverTimeForNPCs;
		SISClothingDurability_Config.ClothingDegradesOverTimeFrequency = obj.ClothingDegradesOverTimeFrequency;
		SISClothingDurability_Config.ArmorProtectsFromDegradation = obj.ArmorProtectsFromDegradation;
		SISClothingDurability_Config.ClothesSpawnWithLessDurability = obj.ClothesSpawnWithLessDurability;
		SISClothingDurability_Config.MinClothesDurabilityOnSpawnPcnt = obj.MinClothesDurabilityOnSpawnPcnt;
		SISClothingDurability_Config.MaxClothesDurabilityOnSpawnPcnt = obj.MaxClothesDurabilityOnSpawnPcnt;
		SISClothingDurability_Config.CanBackpacksDegrade = obj.CanBackpacksDegrade;
		SISClothingDurability_Config.CanShoesDegrade = obj.CanShoesDegrade;
		SISClothingDurability_Config.CanGlassesDegrade = obj.CanGlassesDegrade;
		SISClothingDurability_Config.ClothingDurabilityBarColor = obj.ClothingDurabilityBarColor;
		SISClothingDurability_Config.ZombiesSpawnWithHeavilyDamagedClothing = obj.ZombiesSpawnWithHeavilyDamagedClothing;
		SISClothingDurability_Config.UseDurabilityDegradeOverTimePcnt = obj.UseDurabilityDegradeOverTimePcnt;
		SISClothingDurability_Config.ShowStatusMsgOnClothingBroken = obj.ShowStatusMsgOnClothingBroken;
		SISClothingDurability_Config.CharactersSpeakOnClothingBroken = obj.CharactersSpeakOnClothingBroken;
		SISClothingDurability_Config.PlaySoundOnClothingBroken = obj.PlaySoundOnClothingBroken;
		SISClothingDurability_Config.ShowNotificationOnClothingBroken = obj.ShowNotificationOnClothingBroken;
		SISClothingDurability_Config.RepairClothingDurabilityIncreasePcnt = obj.RepairClothingDurabilityIncreasePcnt;
		SISClothingDurability_Config.RepairClothingMaxDurabilityDecreasePcnt = obj.RepairClothingMaxDurabilityDecreasePcnt;
		foreach (ClothingTypeSettings clothingTypeSettings in obj.ClothingTypes)
		{
			try
			{
				SISClothingDurability_Config.clothesTypes.Add(clothingTypeSettings.Name, clothingTypeSettings);
			}
			catch (Exception ex)
			{
				SISClothingDurability_Config.Log("Error adding " + clothingTypeSettings.Name + ": " + ex.Message);
			}
		}
		foreach (string text in obj.ExcludedTypes)
		{
			try
			{
				SISClothingDurability_Config.ExcludedTypes.Add(text);
			}
			catch (Exception ex2)
			{
				SISClothingDurability_Config.Log("Error adding " + text + ": " + ex2.Message);
			}
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000517C File Offset: 0x0000337C
	private static string GetStoryPath()
	{
		foreach (Story story in GameImpl.Instance.CurrentStories)
		{
			string text = story.Path + SISClothingDurability_Config.ModIdentifier;
			string text2 = story.Path + SISClothingDurability_Config.SettingsFilePath;
			bool flag = File.Exists(text) || File.Exists(text2);
			if (flag)
			{
				return story.Path;
			}
		}
		return ".";
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00005220 File Offset: 0x00003420
	public static float GetMaxDurabilityForEquipment(string name)
	{
		bool flag = !string.IsNullOrEmpty(name);
		if (flag)
		{
			ClothingTypeSettings clothingTypeSettings;
			bool flag2 = SISClothingDurability_Config.clothesTypes.TryGetValue(name, out clothingTypeSettings);
			if (flag2)
			{
				return clothingTypeSettings.MaxDurability;
			}
		}
		return ClothingTypeSettings.GetDefault().MaxDurability;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00005268 File Offset: 0x00003468
	public static ClothingTypeSettings GetClothingModifiersForEquipment(string name)
	{
		bool flag = !string.IsNullOrEmpty(name);
		if (flag)
		{
			ClothingTypeSettings clothingTypeSettings;
			bool flag2 = SISClothingDurability_Config.clothesTypes.TryGetValue(name, out clothingTypeSettings);
			if (flag2)
			{
				return clothingTypeSettings;
			}
		}
		return ClothingTypeSettings.GetDefault();
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000052A4 File Offset: 0x000034A4
	public static int GetClothingMaxPartsOnBroken(string name)
	{
		bool flag = !string.IsNullOrEmpty(name);
		if (flag)
		{
			ClothingTypeSettings clothingTypeSettings;
			bool flag2 = SISClothingDurability_Config.clothesTypes.TryGetValue(name, out clothingTypeSettings);
			if (flag2)
			{
				return clothingTypeSettings.MaxPartsOnBroken;
			}
		}
		return ClothingTypeSettings.GetDefault().MaxPartsOnBroken;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000052EC File Offset: 0x000034EC
	public static int GetClothingMinPartsOnBroken(string name)
	{
		bool flag = !string.IsNullOrEmpty(name);
		if (flag)
		{
			ClothingTypeSettings clothingTypeSettings;
			bool flag2 = SISClothingDurability_Config.clothesTypes.TryGetValue(name, out clothingTypeSettings);
			if (flag2)
			{
				return clothingTypeSettings.MinPartsOnBroken;
			}
		}
		return ClothingTypeSettings.GetDefault().MinPartsOnBroken;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00005334 File Offset: 0x00003534
	public static void OnSaveGame(string Path)
	{
		bool flag = !SISClothingDurability_Config.SaveInProgress;
		if (flag)
		{
			SISClothingDurability_Config.Log("SAVING");
			SISClothingDurability_Config.Log("Saving to " + Path);
			SISClothingDurability_Config.SaveInProgress = true;
			string text = Path + SISClothingDurability_Config.SaveFilePath;
			bool flag2 = File.Exists(text);
			if (flag2)
			{
				File.Delete(text);
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISClothingDurability_Manager));
			SISClothingDurability_Manager.instance.BuildListFromDictionary();
			FileStream fileStream = File.Create(text);
			xmlSerializer.Serialize(fileStream, SISClothingDurability_Manager.instance);
			fileStream.Dispose();
			SISClothingDurability_Config.Log("SAVED");
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000053D8 File Offset: 0x000035D8
	public static bool OnLoadGame(string Path)
	{
		bool flag = !SISClothingDurability_Config.LoadInProgress;
		bool flag3;
		if (flag)
		{
			SISClothingDurability_Config.Log("LOADING");
			SISClothingDurability_Config.LoadInProgress = true;
			string text = Path + SISClothingDurability_Config.SaveFilePath;
			bool flag2 = !File.Exists(text);
			if (flag2)
			{
				flag3 = false;
			}
			else
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(SISClothingDurability_Manager));
				FileStream fileStream = new FileStream(text, FileMode.Open);
				SISClothingDurability_Manager.instance = (SISClothingDurability_Manager)xmlSerializer.Deserialize(fileStream);
				SISClothingDurability_Manager.instance.BuildDictionaryFromList();
				fileStream.Dispose();
				SISClothingDurability_Config.Log("LOADED");
				flag3 = true;
			}
		}
		else
		{
			flag3 = false;
		}
		return flag3;
	}

	// Token: 0x04000048 RID: 72
	public static SISClothingDurability_Config instance;

	// Token: 0x04000049 RID: 73
	private static bool LoggingActive = true;

	// Token: 0x0400004A RID: 74
	private static string SaveFilePath = "/Save_ClothingDurability.sav";

	// Token: 0x0400004B RID: 75
	private static string SettingsFilePath = "/Settings_ClothingDurability.xml";

	// Token: 0x0400004C RID: 76
	private static string ClothingTypeSettingsOtherMods = "/SISCD_ClothingAddon.xml";

	// Token: 0x0400004D RID: 77
	private static string ModIdentifier = "/SISCLOTHINGDURABILITY.txt";

	// Token: 0x0400004E RID: 78
	private static string LogFilePath = "/log.txt";

	// Token: 0x0400004F RID: 79
	public const string HeavySewingKitNameKey = "SISCD_HeavySewingKit";

	// Token: 0x04000050 RID: 80
	public const string SewingKitNameKey = "SISCD_SewingKit";

	// Token: 0x04000051 RID: 81
	public static string SpeechClothingBrokenUniqueId = "SISCD_NPCClothingBroken";

	// Token: 0x04000052 RID: 82
	public static string SpeechTranslateKey = "MEMORY_SISCD_YourClothingBroke";

	// Token: 0x04000053 RID: 83
	public static string RepairNormalClothingRecipeUniqueId = "SISCD_RepairNormalClothing";

	// Token: 0x04000054 RID: 84
	public static string RepairNormalClothingRecipeNameKey = "RECIPE_SISCD_RepairNormalClothing";

	// Token: 0x04000055 RID: 85
	public static string RepairArmorClothingRecipeUniqueId = "SISCD_RepairArmorClothing";

	// Token: 0x04000056 RID: 86
	public static string RepairArmorClothingRecipeNameKey = "RECIPE_SISCD_RepairArmorClothing";

	// Token: 0x04000057 RID: 87
	public static bool SaveInProgress = false;

	// Token: 0x04000058 RID: 88
	public static bool LoadInProgress = false;

	// Token: 0x04000059 RID: 89
	public static bool IsConfigLoaded = false;

	// Token: 0x0400005A RID: 90
	public static float DurabilityDegradeRate;

	// Token: 0x0400005B RID: 91
	public static float DurabilityDegradeOverTimePcnt;

	// Token: 0x0400005C RID: 92
	public static bool UseDurabilityDegradeOverTimePcnt;

	// Token: 0x0400005D RID: 93
	public static bool ClothingDegradesOverTimeForPlayer;

	// Token: 0x0400005E RID: 94
	public static bool ClothingDegradesOverTimeForPlayerCommunity;

	// Token: 0x0400005F RID: 95
	public static bool ClothingDegradesOverTimeForNPCs;

	// Token: 0x04000060 RID: 96
	public static float ClothingDegradesOverTimeFrequency;

	// Token: 0x04000061 RID: 97
	public static bool ClothingDegradesOnAttack;

	// Token: 0x04000062 RID: 98
	public static bool ClothingDegradesOnDamaged;

	// Token: 0x04000063 RID: 99
	public static bool ClothesSpawnWithLessDurability;

	// Token: 0x04000064 RID: 100
	public static float MinClothesDurabilityOnSpawnPcnt;

	// Token: 0x04000065 RID: 101
	public static float MaxClothesDurabilityOnSpawnPcnt;

	// Token: 0x04000066 RID: 102
	public static bool ZombiesSpawnWithHeavilyDamagedClothing;

	// Token: 0x04000067 RID: 103
	public static Color32 ClothingDurabilityBarColor;

	// Token: 0x04000068 RID: 104
	public static bool ArmorProtectsFromDegradation;

	// Token: 0x04000069 RID: 105
	public static float RepairClothingDurabilityIncreasePcnt;

	// Token: 0x0400006A RID: 106
	public static float RepairClothingMaxDurabilityDecreasePcnt;

	// Token: 0x0400006B RID: 107
	public static bool ShowStatusMsgOnClothingBroken;

	// Token: 0x0400006C RID: 108
	public static bool CharactersSpeakOnClothingBroken;

	// Token: 0x0400006D RID: 109
	public static bool PlaySoundOnClothingBroken;

	// Token: 0x0400006E RID: 110
	public static bool ShowNotificationOnClothingBroken;

	// Token: 0x0400006F RID: 111
	public static bool EnabledForPlayer;

	// Token: 0x04000070 RID: 112
	public static bool EnabledForPlayerCommunity;

	// Token: 0x04000071 RID: 113
	public static bool EnabledForNPCs;

	// Token: 0x04000072 RID: 114
	public static bool EnabledForNormalClothing;

	// Token: 0x04000073 RID: 115
	public static bool EnabledForArmorClothing;

	// Token: 0x04000074 RID: 116
	public static bool CanBackpacksDegrade;

	// Token: 0x04000075 RID: 117
	public static bool CanShoesDegrade;

	// Token: 0x04000076 RID: 118
	public static bool CanGlassesDegrade;

	// Token: 0x04000077 RID: 119
	public static Dictionary<string, ClothingTypeSettings> clothesTypes = new Dictionary<string, ClothingTypeSettings>();

	// Token: 0x04000078 RID: 120
	public static List<string> ExcludedTypes = new List<string>();

	// Token: 0x04000079 RID: 121
	public static List<Resource<AudioClip>> SoundsClothingBroken = new List<Resource<AudioClip>>();
}
