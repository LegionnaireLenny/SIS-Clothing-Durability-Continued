using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x02000017 RID: 23
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISClothingDurability_Manager
{
	// Token: 0x06000045 RID: 69 RVA: 0x00005556 File Offset: 0x00003756
	public SISClothingDurability_Manager()
	{
		SISClothingDurability_Manager.instance = this;
		this.CharacterIdsTimeUpdate = new Dictionary<int, SISCD_CharacterUpdateInfo>();
		this.equipments = new List<SISCD_ClothesDurabilityInfo>();
		this.Equipments = new Dictionary<int, SISCD_ClothesDurabilityInfo>();
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00005588 File Offset: 0x00003788
	public void BuildDictionaryFromList()
	{
		this.Equipments.Clear();
		foreach (SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo in this.equipments)
		{
			try
			{
				this.Equipments.Add(siscd_ClothesDurabilityInfo.Id, siscd_ClothesDurabilityInfo);
			}
			catch
			{
			}
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00005610 File Offset: 0x00003810
	public void BuildListFromDictionary()
	{
		this.equipments.Clear();
		foreach (SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo in this.Equipments.Values)
		{
			this.equipments.Add(siscd_ClothesDurabilityInfo);
		}
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00005680 File Offset: 0x00003880
	public TimeSpan GetCharacterUpdateTime(Character character)
	{
		SISCD_CharacterUpdateInfo siscd_CharacterUpdateInfo;
		this.CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
		bool flag = siscd_CharacterUpdateInfo != null;
		TimeSpan timeSpan;
		if (flag)
		{
			timeSpan = siscd_CharacterUpdateInfo.Time;
		}
		else
		{
			timeSpan = TimeSpan.Zero;
		}
		return timeSpan;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000056C0 File Offset: 0x000038C0
	public void SetCharacterUpdateInfo(Character character, TimeSpan time, float dts = 0f)
	{
		bool flag = !this.CharacterIdsTimeUpdate.ContainsKey(character.Id);
		if (flag)
		{
			this.CharacterIdsTimeUpdate.Add(character.Id, new SISCD_CharacterUpdateInfo(time, dts));
		}
		else
		{
			this.CharacterIdsTimeUpdate[character.Id].Time = time;
			this.CharacterIdsTimeUpdate[character.Id].Dts = dts;
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00005734 File Offset: 0x00003934
	public bool AddEquipment(Equipment equip, float durability = 15f, float maxDurability = 15f)
	{
		bool flag = equip == null || durability < 0f || maxDurability < 0f;
		bool flag2;
		if (flag)
		{
			flag2 = false;
		}
		else
		{
			bool flag3 = durability > maxDurability;
			if (flag3)
			{
				durability = maxDurability;
			}
			SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo = new SISCD_ClothesDurabilityInfo(equip.Id, durability, maxDurability);
			this.Equipments.Add(equip.Id, siscd_ClothesDurabilityInfo);
			SISClothingDurability_Config.Log(string.Concat(new string[]
			{
				"Added equipment with ID ",
				equip.Id.ToString(),
				" ",
				equip.GetPrototype().Name,
				";",
				durability.ToString(),
				";",
				maxDurability.ToString()
			}));
			this.OnAddedEquipment(equip, siscd_ClothesDurabilityInfo);
			flag2 = true;
		}
		return flag2;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00005804 File Offset: 0x00003A04
	public bool AddEquipment(Equipment equip, float maxDurability)
	{
		return this.AddEquipment(equip, maxDurability, maxDurability);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00005820 File Offset: 0x00003A20
	public bool IsEquipmentRegistered(Equipment equip)
	{
		return this.IsEquipmentRegistered(equip.Id);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00005840 File Offset: 0x00003A40
	public SISCD_ClothesDurabilityInfo GetClothesDurabilityInfo(int id)
	{
		for (int i = 0; i < this.equipments.Count; i++)
		{
			bool flag = this.equipments[i].Id == id;
			if (flag)
			{
				return this.equipments[i];
			}
		}
		return null;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00005898 File Offset: 0x00003A98
	public SISCD_CharacterUpdateInfo GetCharacterUpdateInfo(Character character, TimeSpan time, float dts = 0f)
	{
		SISCD_CharacterUpdateInfo siscd_CharacterUpdateInfo;
		this.CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
		bool flag = siscd_CharacterUpdateInfo == null;
		if (flag)
		{
			this.CharacterIdsTimeUpdate.Add(character.Id, new SISCD_CharacterUpdateInfo(time, dts));
		}
		return siscd_CharacterUpdateInfo;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000058E4 File Offset: 0x00003AE4
	public SISCD_CharacterUpdateInfo GetCharacterUpdateInfo(Character character)
	{
		SISCD_CharacterUpdateInfo siscd_CharacterUpdateInfo;
		this.CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
		bool flag = siscd_CharacterUpdateInfo == null;
		if (flag)
		{
			this.CharacterIdsTimeUpdate.Add(character.Id, new SISCD_CharacterUpdateInfo());
			this.CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
		}
		return siscd_CharacterUpdateInfo;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00005940 File Offset: 0x00003B40
	public SISCD_ClothesDurabilityInfo GetDefaultClothesDurabilityInfo(Equipment equip)
	{
		ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equip.GetPrototype().Name);
		return new SISCD_ClothesDurabilityInfo(equip.Id, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x0000597C File Offset: 0x00003B7C
	public SISCD_ClothesDurabilityInfo GetClothesDurabilityInfo(Equipment equip)
	{
		SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
		this.Equipments.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
		return siscd_ClothesDurabilityInfo;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000059A4 File Offset: 0x00003BA4
	public void SetClothesDurabilityInfo(Equipment equip, SISCD_ClothesDurabilityInfo equipDur)
	{
		bool flag = this.Equipments.ContainsKey(equip.Id);
		if (flag)
		{
			this.Equipments[equip.Id] = equipDur;
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000059DC File Offset: 0x00003BDC
	public float IncreaseDurability(Equipment equip, float amount = 1f)
	{
		bool flag = amount <= 0f || equip == null;
		float num;
		if (flag)
		{
			num = -1f;
		}
		else
		{
			float num2 = -1f;
			SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
			this.Equipments.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
			bool flag2 = siscd_ClothesDurabilityInfo != null;
			if (flag2)
			{
				num2 = Mathf.Min(siscd_ClothesDurabilityInfo.Durability + amount, siscd_ClothesDurabilityInfo.MaxDurability);
				SISClothingDurability_Config.Log("Increased durability for " + equip.GetPrototype().Name + ", dur = " + num2.ToString());
				this.Equipments[equip.Id].Durability = num2;
			}
			num = num2;
		}
		return num;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00005A84 File Offset: 0x00003C84
	public float ReduceDurabilityByPercentage(Equipment equip, float pcnt)
	{
		float num = pcnt * this.GetMaxDurability(equip);
		return this.ReduceDurability(equip, num);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00005AA8 File Offset: 0x00003CA8
	public float ReduceDurability(Equipment equip, float amount = 1f)
	{
		SISClothingDurability_Config.Log("Reducing durability for " + equip.GetPrototype().Name + ", amount = " + amount.ToString());
		float num = -1f;
		bool flag = amount <= 0f || equip == null;
		float num2;
		if (flag)
		{
			num2 = num;
		}
		else
		{
			SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
			this.Equipments.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
			bool flag2 = false;
			bool flag3 = siscd_ClothesDurabilityInfo != null;
			if (flag3)
			{
				bool isBroken = siscd_ClothesDurabilityInfo.IsBroken;
				if (isBroken)
				{
					return siscd_ClothesDurabilityInfo.Durability;
				}
				num = Mathf.Max(siscd_ClothesDurabilityInfo.Durability - amount, 0f);
				SISClothingDurability_Config.Log("Reduced durability for " + equip.GetPrototype().Name + ", dur = " + num.ToString());
				this.Equipments[equip.Id].Durability = num;
				bool flag4 = num == 0f;
				if (flag4)
				{
					flag2 = true;
				}
			}
			bool flag5 = flag2;
			if (flag5)
			{
				this.Equipments[equip.Id].IsBroken = true;
				bool flag6 = !equip.GetAsCharacter().Zombie && !equip.GetAsCharacter().IsSleepingOrUnconscious() && equip.GetAsCharacter().Alive;
				if (flag6)
				{
					bool flag7 = equip.GetAsCharacter() == Hud.Instance.LocalControlledCharacter;
					bool flag8 = flag7;
					if (flag8)
					{
						bool showStatusMsgOnClothingBroken = SISClothingDurability_Config.ShowStatusMsgOnClothingBroken;
						if (showStatusMsgOnClothingBroken)
						{
							try
							{
								string text = GameImpl.Translate(SISClothingDurability_Config.SpeechTranslateKey).Replace("%1", GameImpl.Translate(equip.GetPrototype().GetNameKey()));
								HudBehaviour.Instance.SetStatusBarMsg(text);
							}
							catch
							{
							}
						}
						bool charactersSpeakOnClothingBroken = SISClothingDurability_Config.CharactersSpeakOnClothingBroken;
						if (charactersSpeakOnClothingBroken)
						{
							try
							{
								Speech speech = GameImpl.Instance.FindSpeechByUniqueID(SISClothingDurability_Config.SpeechClothingBrokenUniqueId);
								equip.GetAsCharacter().Speak(speech);
							}
							catch
							{
							}
						}
					}
					else
					{
						bool flag9 = (equip.GetAsCharacter().IsOnPlayersTeam() && SISClothingDurability_Config.EnabledForPlayerCommunity) || (SISClothingDurability_Config.EnabledForNPCs && !equip.GetAsCharacter().IsControllableByPlayer());
						if (flag9)
						{
							bool charactersSpeakOnClothingBroken2 = SISClothingDurability_Config.CharactersSpeakOnClothingBroken;
							if (charactersSpeakOnClothingBroken2)
							{
								try
								{
									Speech speech2 = GameImpl.Instance.FindSpeechByUniqueID(SISClothingDurability_Config.SpeechClothingBrokenUniqueId);
									equip.GetAsCharacter().Speak(speech2);
								}
								catch
								{
								}
							}
						}
					}
				}
				bool playSoundOnClothingBroken = SISClothingDurability_Config.PlaySoundOnClothingBroken;
				if (playSoundOnClothingBroken)
				{
					try
					{
						SoundManager.PlaySound3DFromList(SISClothingDurability_Config.SoundsClothingBroken, equip.GetAsCharacter().Pos);
					}
					catch
					{
					}
				}
				bool showNotificationOnClothingBroken = SISClothingDurability_Config.ShowNotificationOnClothingBroken;
				if (showNotificationOnClothingBroken)
				{
					try
					{
						NotificationManager.Instance.AddEquipmentNotification(equip.GetAsCharacter(), equip.GetAsCharacter(), equip, 1);
					}
					catch
					{
					}
				}
				Character asCharacter = equip.GetAsCharacter();
				this.RemoveEquipment(equip.Id);
				asCharacter.Inventory.Remove(equip.GetAsCharacter(), equip);
				EquipmentPrototype equipmentPrototype = GameImpl.Instance.FindEquipmentPrototypeByName("Rag");
				bool flag10 = equipmentPrototype != null;
				if (flag10)
				{
					int clothingMinPartsOnBroken = SISClothingDurability_Config.GetClothingMinPartsOnBroken(equip.GetPrototype().Name);
					int clothingMaxPartsOnBroken = SISClothingDurability_Config.GetClothingMaxPartsOnBroken(equip.GetPrototype().Name);
					int num3 = SISClothingDurability_Utils.NextBetweenInts(clothingMinPartsOnBroken, clothingMaxPartsOnBroken);
					bool flag11 = num3 > 0;
					if (flag11)
					{
						Equipment equipment = Equipment.Spawn(equipmentPrototype, num3);
						asCharacter.Inventory.Add(asCharacter, equipment);
					}
				}
				this.OnBrokenEquipment(equip);
			}
			else
			{
				this.OnReducedDurability(equip, num);
			}
			num2 = num;
		}
		return num2;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00005E58 File Offset: 0x00004058
	public bool RemoveEquipment(Equipment equip)
	{
		bool flag = equip != null;
		bool flag3;
		if (flag)
		{
			bool flag2 = this.RemoveEquipment(equip.Id);
			this.OnRemovedEquipment(equip, flag2);
			flag3 = flag2;
		}
		else
		{
			flag3 = false;
		}
		return flag3;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00005E90 File Offset: 0x00004090
	private bool RemoveEquipment(int id)
	{
		return this.Equipments.Remove(id);
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00005EB0 File Offset: 0x000040B0
	private bool IsEquipmentRegistered(int id)
	{
		return this.Equipments.ContainsKey(id);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00005ED0 File Offset: 0x000040D0
	public float GetDurability(Equipment equip)
	{
		bool flag = equip == null;
		float num;
		if (flag)
		{
			num = 0f;
		}
		else
		{
			num = this.GetDurability(equip.Id);
		}
		return num;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00005F00 File Offset: 0x00004100
	private float GetDurability(int id)
	{
		SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
		this.Equipments.TryGetValue(id, out siscd_ClothesDurabilityInfo);
		bool flag = siscd_ClothesDurabilityInfo != null;
		float num;
		if (flag)
		{
			num = siscd_ClothesDurabilityInfo.Durability;
		}
		else
		{
			num = -1f;
		}
		return num;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00005F38 File Offset: 0x00004138
	public float GetMaxDurability(Equipment equip)
	{
		bool flag = equip != null;
		if (flag)
		{
			SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
			this.Equipments.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
			bool flag2 = siscd_ClothesDurabilityInfo != null;
			if (flag2)
			{
				return siscd_ClothesDurabilityInfo.MaxDurability;
			}
		}
		return SISClothingDurability_Config.GetMaxDurabilityForEquipment(equip.GetPrototype().Name);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00005F8A File Offset: 0x0000418A
	public void OnAddedEquipment(Equipment equipment, SISCD_ClothesDurabilityInfo equipmentDurability)
	{
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00005F8A File Offset: 0x0000418A
	public void OnRemovedEquipment(Equipment equipment, bool success)
	{
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00005F8A File Offset: 0x0000418A
	public void OnBrokenEquipment(Equipment equipment)
	{
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00005F8A File Offset: 0x0000418A
	public void OnReducedDurability(Equipment equipment, float newDurability)
	{
	}

	// Token: 0x0400007C RID: 124
	[XmlIgnore]
	public static SISClothingDurability_Manager instance;

	// Token: 0x0400007D RID: 125
	[XmlIgnore]
	public Dictionary<int, SISCD_CharacterUpdateInfo> CharacterIdsTimeUpdate;

	// Token: 0x0400007E RID: 126
	[XmlArray]
	public List<SISCD_ClothesDurabilityInfo> equipments;

	// Token: 0x0400007F RID: 127
	[XmlIgnore]
	public Dictionary<int, SISCD_ClothesDurabilityInfo> Equipments;
}
