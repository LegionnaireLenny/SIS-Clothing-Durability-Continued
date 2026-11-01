using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using HarmonyLib;
using Newtonsoft.Json;
using UnityEngine;

public class SISClothingDurability_Config
{
	public SISClothingDurability_Config()
	{
		SISClothingDurability_Config.instance = this;
	}

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

	public static void ResetSettings()
	{
		SISClothingDurability_Config.IsConfigLoaded = false;
		SISClothingDurability_Config.FillSettings(SISClothingDurability_Settings.GetDefault());
		SISClothingDurability_Config.IsConfigLoaded = true;
	}

	public static void ClearSettings()
	{
		SISClothingDurability_Config.IsConfigLoaded = false;
		SISClothingDurability_Config.FillSettings(SISClothingDurability_Settings.GetDefault());
		SISClothingDurability_Config.clothesTypes.Clear();
	}

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

	public static void ReloadSettings()
	{
		SISClothingDurability_Config.ResetSettings();
		SISClothingDurability_Config.LoadSettings();
	}

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

	public static SISClothingDurability_Config instance;

	private static bool LoggingActive = true;

	private static string SaveFilePath = "/Save_ClothingDurability.sav";

	private static string SettingsFilePath = "/Settings_ClothingDurability.xml";

	private static string ClothingTypeSettingsOtherMods = "/SISCD_ClothingAddon.xml";

	private static string ModIdentifier = "/SISCLOTHINGDURABILITY.txt";

	private static string LogFilePath = "/log.txt";

	public const string HeavySewingKitNameKey = "SISCD_HeavySewingKit";

	public const string SewingKitNameKey = "SISCD_SewingKit";

	public static string SpeechClothingBrokenUniqueId = "SISCD_NPCClothingBroken";

	public static string SpeechTranslateKey = "MEMORY_SISCD_YourClothingBroke";

	public static string RepairNormalClothingRecipeUniqueId = "SISCD_RepairNormalClothing";

	public static string RepairNormalClothingRecipeNameKey = "RECIPE_SISCD_RepairNormalClothing";

	public static string RepairArmorClothingRecipeUniqueId = "SISCD_RepairArmorClothing";

	public static string RepairArmorClothingRecipeNameKey = "RECIPE_SISCD_RepairArmorClothing";

	public static bool SaveInProgress = false;

	public static bool LoadInProgress = false;

	public static bool IsConfigLoaded = false;

	public static float DurabilityDegradeRate;

	public static float DurabilityDegradeOverTimePcnt;

	public static bool UseDurabilityDegradeOverTimePcnt;

	public static bool ClothingDegradesOverTimeForPlayer;

	public static bool ClothingDegradesOverTimeForPlayerCommunity;

	public static bool ClothingDegradesOverTimeForNPCs;

	public static float ClothingDegradesOverTimeFrequency;

	public static bool ClothingDegradesOnAttack;

	public static bool ClothingDegradesOnDamaged;

	public static bool ClothesSpawnWithLessDurability;

	public static float MinClothesDurabilityOnSpawnPcnt;

	public static float MaxClothesDurabilityOnSpawnPcnt;

	public static bool ZombiesSpawnWithHeavilyDamagedClothing;

	public static Color32 ClothingDurabilityBarColor;

	public static bool ArmorProtectsFromDegradation;

	public static float RepairClothingDurabilityIncreasePcnt;

	public static float RepairClothingMaxDurabilityDecreasePcnt;

	public static bool ShowStatusMsgOnClothingBroken;

	public static bool CharactersSpeakOnClothingBroken;

	public static bool PlaySoundOnClothingBroken;

	public static bool ShowNotificationOnClothingBroken;

	public static bool EnabledForPlayer;

	public static bool EnabledForPlayerCommunity;

	public static bool EnabledForNPCs;

	public static bool EnabledForNormalClothing;

	public static bool EnabledForArmorClothing;

	public static bool CanBackpacksDegrade;

	public static bool CanShoesDegrade;

	public static bool CanGlassesDegrade;

	public static Dictionary<string, ClothingTypeSettings> clothesTypes = new Dictionary<string, ClothingTypeSettings>();

	public static List<string> ExcludedTypes = new List<string>();

	public static List<Resource<AudioClip>> SoundsClothingBroken = new List<Resource<AudioClip>>();
}
