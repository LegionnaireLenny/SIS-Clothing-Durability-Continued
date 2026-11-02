using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISClothingDurability_Manager
{
    public SISClothingDurability_Manager()
    {
        SISClothingDurability_Manager.instance = this;
        this.CharacterIdsTimeUpdate = new Dictionary<int, SISCD_CharacterUpdateInfo>();
        this.equipments = new List<SISCD_ClothesDurabilityInfo>();
        this.Equipments = new Dictionary<int, SISCD_ClothesDurabilityInfo>();
    }

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

    public void BuildListFromDictionary()
    {
        this.equipments.Clear();
        foreach (SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo in this.Equipments.Values)
        {
            this.equipments.Add(siscd_ClothesDurabilityInfo);
        }
    }

    public TimeSpan GetCharacterUpdateTime(Character character)
    {
        SISCD_CharacterUpdateInfo siscd_CharacterUpdateInfo;
        this.CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
        TimeSpan timeSpan;
        if (siscd_CharacterUpdateInfo != null)
        {
            timeSpan = siscd_CharacterUpdateInfo.Time;
        }
        else
        {
            timeSpan = TimeSpan.Zero;
        }
        return timeSpan;
    }

    public void SetCharacterUpdateInfo(Character character, TimeSpan time, float dts = 0f)
    {
        if (!this.CharacterIdsTimeUpdate.ContainsKey(character.Id))
        {
            this.CharacterIdsTimeUpdate.Add(character.Id, new SISCD_CharacterUpdateInfo(time, dts));
        }
        else
        {
            this.CharacterIdsTimeUpdate[character.Id].Time = time;
            this.CharacterIdsTimeUpdate[character.Id].Dts = dts;
        }
    }

    public bool AddEquipment(Equipment equip, float durability = 15f, float maxDurability = 15f)
    {
        bool flag2;
        if (equip == null || durability < 0f || maxDurability < 0f)
        {
            flag2 = false;
        }
        else
        {
            durability = Mathf.Clamp(durability, 0f, maxDurability);
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

    public bool AddEquipment(Equipment equip, float maxDurability)
    {
        return this.AddEquipment(equip, maxDurability, maxDurability);
    }

    public bool IsEquipmentRegistered(Equipment equip)
    {
        return this.IsEquipmentRegistered(equip.Id);
    }

    public SISCD_ClothesDurabilityInfo GetClothesDurabilityInfo(int id)
    {
        for (int i = 0; i < this.equipments.Count; i++)
        {
            if (this.equipments[i].Id == id)
            {
                return this.equipments[i];
            }
        }
        return null;
    }

    public SISCD_CharacterUpdateInfo GetCharacterUpdateInfo(Character character, TimeSpan time, float dts = 0f)
    {
        SISCD_CharacterUpdateInfo siscd_CharacterUpdateInfo;
        this.CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
        if (siscd_CharacterUpdateInfo == null)
        {
            this.CharacterIdsTimeUpdate.Add(character.Id, new SISCD_CharacterUpdateInfo(time, dts));
        }
        return siscd_CharacterUpdateInfo;
    }

    public SISCD_CharacterUpdateInfo GetCharacterUpdateInfo(Character character)
    {
        SISCD_CharacterUpdateInfo siscd_CharacterUpdateInfo;
        this.CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
        if (siscd_CharacterUpdateInfo == null)
        {
            this.CharacterIdsTimeUpdate.Add(character.Id, new SISCD_CharacterUpdateInfo());
            this.CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
        }
        return siscd_CharacterUpdateInfo;
    }

    public SISCD_ClothesDurabilityInfo GetDefaultClothesDurabilityInfo(Equipment equip)
    {
        ClothingTypeSettings clothingModifiersForEquipment = SISClothingDurability_Config.GetClothingModifiersForEquipment(equip.GetPrototype().Name);
        return new SISCD_ClothesDurabilityInfo(equip.Id, clothingModifiersForEquipment.MaxDurability, clothingModifiersForEquipment.MaxDurability);
    }

    public SISCD_ClothesDurabilityInfo GetClothesDurabilityInfo(Equipment equip)
    {
        SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
        this.Equipments.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
        return siscd_ClothesDurabilityInfo;
    }

    public void SetClothesDurabilityInfo(Equipment equip, SISCD_ClothesDurabilityInfo equipDur)
    {
        if (this.Equipments.ContainsKey(equip.Id))
        {
            this.Equipments[equip.Id] = equipDur;
        }
    }

    public float IncreaseDurability(Equipment equip, float amount = 1f)
    {
        float num;
        if (amount <= 0f || equip == null)
        {
            num = -1f;
        }
        else
        {
            float num2 = -1f;
            SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
            this.Equipments.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
            if (siscd_ClothesDurabilityInfo != null)
            {
                num2 = Mathf.Min(siscd_ClothesDurabilityInfo.Durability + amount, siscd_ClothesDurabilityInfo.MaxDurability);
                SISClothingDurability_Config.Log("Increased durability for " + equip.GetPrototype().Name + ", dur = " + num2.ToString());
                this.Equipments[equip.Id].Durability = num2;
            }
            num = num2;
        }
        return num;
    }

    public float ReduceDurabilityByPercentage(Equipment equip, float pcnt)
    {
        float num = pcnt * this.GetMaxDurability(equip);
        return this.ReduceDurability(equip, num);
    }

    public float ReduceDurability(Equipment equip, float amount = 1f)
    {
        SISClothingDurability_Config.Log("Reducing durability for " + equip.GetPrototype().Name + ", amount = " + amount.ToString());
        float num = -1f;
        float num2;
        if (amount <= 0f || equip == null)
        {
            num2 = num;
        }
        else
        {
            SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
            this.Equipments.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
            bool flag2 = false;
            if (siscd_ClothesDurabilityInfo != null)
            {
                if (siscd_ClothesDurabilityInfo.IsBroken)
                {
                    return siscd_ClothesDurabilityInfo.Durability;
                }
                num = Mathf.Max(siscd_ClothesDurabilityInfo.Durability - amount, 0f);
                SISClothingDurability_Config.Log("Reduced durability for " + equip.GetPrototype().Name + ", dur = " + num.ToString());
                this.Equipments[equip.Id].Durability = num;
                if (num == 0f)
                {
                    flag2 = true;
                }
            }
            if (flag2)
            {
                this.Equipments[equip.Id].IsBroken = true;
                if (!equip.GetAsCharacter().Zombie && !equip.GetAsCharacter().IsSleepingOrUnconscious() && equip.GetAsCharacter().Alive)
                {
                    if (equip.GetAsCharacter() == Hud.Instance.LocalControlledCharacter)
                    {
                        if (SISClothingDurability_Config.ShowStatusMsgOnClothingBroken)
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
                        if (SISClothingDurability_Config.CharactersSpeakOnClothingBroken)
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
                        if ((equip.GetAsCharacter().IsOnPlayersTeam() && SISClothingDurability_Config.EnabledForPlayerCommunity) || (SISClothingDurability_Config.EnabledForNPCs && !equip.GetAsCharacter().IsControllableByPlayer()))
                        {
                            if (SISClothingDurability_Config.CharactersSpeakOnClothingBroken)
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
                if (SISClothingDurability_Config.PlaySoundOnClothingBroken)
                {
                    try
                    {
                        SoundManager.PlaySound3DFromList(SISClothingDurability_Config.SoundsClothingBroken, equip.GetAsCharacter().Pos);
                    }
                    catch
                    {
                    }
                }
                if (SISClothingDurability_Config.ShowNotificationOnClothingBroken)
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
                if (equipmentPrototype != null)
                {
                    int clothingMinPartsOnBroken = SISClothingDurability_Config.GetClothingMinPartsOnBroken(equip.GetPrototype().Name);
                    int clothingMaxPartsOnBroken = SISClothingDurability_Config.GetClothingMaxPartsOnBroken(equip.GetPrototype().Name);
                    int num3 = SISClothingDurability_Utils.NextBetweenInts(clothingMinPartsOnBroken, clothingMaxPartsOnBroken);
                    if (num3 > 0)
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

    public bool RemoveEquipment(Equipment equip)
    {
        bool flag3;
        if (equip != null)
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

    private bool RemoveEquipment(int id)
    {
        return this.Equipments.Remove(id);
    }

    private bool IsEquipmentRegistered(int id)
    {
        return this.Equipments.ContainsKey(id);
    }

    public float GetDurability(Equipment equip)
    {
        float num;
        if (equip == null)
        {
            num = 0f;
        }
        else
        {
            num = this.GetDurability(equip.Id);
        }
        return num;
    }

    private float GetDurability(int id)
    {
        SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
        this.Equipments.TryGetValue(id, out siscd_ClothesDurabilityInfo);
        float num;
        if (siscd_ClothesDurabilityInfo != null)
        {
            num = siscd_ClothesDurabilityInfo.Durability;
        }
        else
        {
            num = -1f;
        }
        return num;
    }

    public float GetMaxDurability(Equipment equip)
    {
        if (equip != null)
        {
            SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo;
            this.Equipments.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
            if (siscd_ClothesDurabilityInfo != null)
            {
                return siscd_ClothesDurabilityInfo.MaxDurability;
            }
        }
        return SISClothingDurability_Config.GetMaxDurabilityForEquipment(equip.GetPrototype().Name);
    }

    public void OnAddedEquipment(Equipment equipment, SISCD_ClothesDurabilityInfo equipmentDurability) { }

    public void OnRemovedEquipment(Equipment equipment, bool success) { }

    public void OnBrokenEquipment(Equipment equipment) { }

    public void OnReducedDurability(Equipment equipment, float newDurability) { }

    [XmlIgnore]
    public static SISClothingDurability_Manager instance;

    [XmlIgnore]
    public Dictionary<int, SISCD_CharacterUpdateInfo> CharacterIdsTimeUpdate;

    [XmlArray]
    public List<SISCD_ClothesDurabilityInfo> equipments;

    [XmlIgnore]
    public Dictionary<int, SISCD_ClothesDurabilityInfo> Equipments;
}
