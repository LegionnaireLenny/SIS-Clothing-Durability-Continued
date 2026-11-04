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
        SISClothingDurability_Manager.Instance = this;
        CharacterIdsTimeUpdate = new Dictionary<int, SISCD_CharacterUpdateInfo>();
        EquipmentList = new List<SISCD_ClothesDurabilityInfo>();
        EquipmentDict = new Dictionary<int, SISCD_ClothesDurabilityInfo>();
    }

    public void BuildDictionaryFromList()
    {
        EquipmentDict.Clear();
        foreach (SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo in EquipmentList)
        {
            try
            {
                EquipmentDict.Add(siscd_ClothesDurabilityInfo.Id, siscd_ClothesDurabilityInfo);
            }
            catch
            {
            }
        }
    }

    public void BuildListFromDictionary()
    {
        EquipmentList.Clear();
        foreach (SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo in EquipmentDict.Values)
        {
            EquipmentList.Add(siscd_ClothesDurabilityInfo);
        }
    }

    public TimeSpan GetCharacterUpdateTime(Character character)
    {
        SISCD_CharacterUpdateInfo siscd_CharacterUpdateInfo;
        CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
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
        if (!CharacterIdsTimeUpdate.ContainsKey(character.Id))
        {
            CharacterIdsTimeUpdate.Add(character.Id, new SISCD_CharacterUpdateInfo(time, dts));
        }
        else
        {
            CharacterIdsTimeUpdate[character.Id].Time = time;
            CharacterIdsTimeUpdate[character.Id].Dts = dts;
        }
    }

    public bool AddEquipment(Equipment equip, float durability = 15f, float maxDurability = 15f)
    {
        if (equip == null || durability < 0f || maxDurability < 0f)
        {
            return false;
        }

        durability = Mathf.Clamp(durability, 0f, maxDurability);
        SISCD_ClothesDurabilityInfo siscd_ClothesDurabilityInfo = new SISCD_ClothesDurabilityInfo(equip.Id, durability, maxDurability);
        EquipmentDict.Add(equip.Id, siscd_ClothesDurabilityInfo);
        SISClothingDurability_Config.Log($"Added equipment with ID {equip.Id} {equip.GetPrototype().Name};{durability};{maxDurability}");
        OnAddedEquipment(equip, siscd_ClothesDurabilityInfo);
        return true;
    }

    public bool AddEquipment(Equipment equip, float maxDurability)
    {
        return AddEquipment(equip, maxDurability, maxDurability);
    }

    public bool IsEquipmentRegistered(Equipment equip)
    {
        return IsEquipmentRegistered(equip.Id);
    }

    public SISCD_ClothesDurabilityInfo GetClothesDurabilityInfo(int id)
    {
        for (int i = 0; i < EquipmentList.Count; i++)
        {
            if (EquipmentList[i].Id == id)
            {
                return EquipmentList[i];
            }
        }
        return null;
    }

    public SISCD_CharacterUpdateInfo GetCharacterUpdateInfo(Character character, TimeSpan time, float dts = 0f)
    {
        SISCD_CharacterUpdateInfo siscd_CharacterUpdateInfo;
        CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
        if (siscd_CharacterUpdateInfo == null)
        {
            CharacterIdsTimeUpdate.Add(character.Id, new SISCD_CharacterUpdateInfo(time, dts));
        }
        return siscd_CharacterUpdateInfo;
    }

    public SISCD_CharacterUpdateInfo GetCharacterUpdateInfo(Character character)
    {
        SISCD_CharacterUpdateInfo siscd_CharacterUpdateInfo;
        CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
        if (siscd_CharacterUpdateInfo == null)
        {
            CharacterIdsTimeUpdate.Add(character.Id, new SISCD_CharacterUpdateInfo());
            CharacterIdsTimeUpdate.TryGetValue(character.Id, out siscd_CharacterUpdateInfo);
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
        EquipmentDict.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
        return siscd_ClothesDurabilityInfo;
    }

    public void SetClothesDurabilityInfo(Equipment equip, SISCD_ClothesDurabilityInfo equipDur)
    {
        if (EquipmentDict.ContainsKey(equip.Id))
        {
            EquipmentDict[equip.Id] = equipDur;
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
            EquipmentDict.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
            if (siscd_ClothesDurabilityInfo != null)
            {
                num2 = Mathf.Min(siscd_ClothesDurabilityInfo.Durability + amount, siscd_ClothesDurabilityInfo.MaxDurability);
                SISClothingDurability_Config.Log("Increased durability for " + equip.GetPrototype().Name + ", dur = " + num2.ToString());
                EquipmentDict[equip.Id].Durability = num2;
            }
            num = num2;
        }
        return num;
    }

    public float ReduceDurabilityByPercentage(Equipment equip, float pcnt)
    {
        float num = pcnt * GetMaxDurability(equip);
        return ReduceDurability(equip, num);
    }

    public float ReduceDurability(Equipment equip, float amount = 1f)
    {
        if (amount <= 0f || 
            equip == null ||
            !(equip.GetAsCharacter() is Character owner) ||
            !EquipmentDict.TryGetValue(equip.Id, out SISCD_ClothesDurabilityInfo equipInfo))
        {
            return -1f;
        }

        SISClothingDurability_Config.Log($"Reducing durability for {equip.GetPrototype().Name}; {equipInfo.Durability} - {amount} = {equipInfo.Durability - amount}");
        equipInfo.Durability -= amount;
        if (equipInfo.Durability <= 0)
        {
            equipInfo.IsBroken = true;
            if (owner.AliveAndNotZombie && !owner.IsSleepingOrUnconscious())
            {
                if (SISClothingDurability_Utils.IsSpeakingOnItemBrokenEnabledForCharacter(owner))
                {
                    try
                    {
                        owner.Speak(GameImpl.Instance.FindSpeechByUniqueID(SISClothingDurability_Config.SpeechClothingBrokenUniqueId));
                    }
                    catch { }
                }

                if (SISClothingDurability_Config.ShowStatusMsgOnClothingBroken &&
                    owner == Hud.Instance.LocalControlledCharacter)
                {
                    try
                    {
                        string text = GameImpl.Translate(SISClothingDurability_Config.SpeechTranslateKey).Replace("%1", GameImpl.Translate(equip.GetPrototype().GetNameKey()));
                        HudBehaviour.Instance.SetStatusBarMsg(text);
                    }
                    catch { }
                }
            }

            if (SISClothingDurability_Config.PlaySoundOnClothingBroken)
            {
                try
                {
                    SoundManager.PlaySound3DFromList(SISClothingDurability_Config.SoundsClothingBroken, equip.GetAsCharacter().Pos);
                }
                catch { }
            }

            if (SISClothingDurability_Config.ShowNotificationOnClothingBroken)
            {
                try
                {
                    NotificationManager.Instance.AddEquipmentNotification(owner, owner, equip, 1);
                }
                catch { }
            }

            RemoveEquipment(equip.Id);
            owner.Inventory.Remove(owner, equip);
            EquipmentPrototype scrapParts = GameImpl.Instance.FindEquipmentPrototypeByName(SISClothingDurability_Config.PrototypePartsOnClothingBroken);
            if (scrapParts != null)
            {
                int clothingMinPartsOnBroken = SISClothingDurability_Config.GetClothingMinPartsOnBroken(equip.GetPrototype().Name);
                int clothingMaxPartsOnBroken = SISClothingDurability_Config.GetClothingMaxPartsOnBroken(equip.GetPrototype().Name);
                int num3 = SISClothingDurability_Utils.NextBetweenInts(clothingMinPartsOnBroken, clothingMaxPartsOnBroken);
                if (num3 > 0)
                {
                    Equipment equipment = Equipment.Spawn(scrapParts, num3);
                    owner.Inventory.Add(owner, equipment);
                }
            }
            OnBrokenEquipment(equip);
        }
        else
        {
            OnReducedDurability(equip, equipInfo.Durability);
        }
        return equipInfo.Durability;
    }

    public bool RemoveEquipment(Equipment equip)
    {
        if (equip == null)
        {
            return false;
        }

        bool success = RemoveEquipment(equip.Id);
        OnRemovedEquipment(equip, success);
        return success;
    }

    private bool RemoveEquipment(int id)
    {
        return EquipmentDict.Remove(id);
    }

    private bool IsEquipmentRegistered(int id)
    {
        return EquipmentDict.ContainsKey(id);
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
        EquipmentDict.TryGetValue(id, out siscd_ClothesDurabilityInfo);
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
            EquipmentDict.TryGetValue(equip.Id, out siscd_ClothesDurabilityInfo);
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
    public static SISClothingDurability_Manager Instance;

    [XmlIgnore]
    public Dictionary<int, SISCD_CharacterUpdateInfo> CharacterIdsTimeUpdate;

    [XmlArray]
    public List<SISCD_ClothesDurabilityInfo> EquipmentList;

    [XmlIgnore]
    public Dictionary<int, SISCD_ClothesDurabilityInfo> EquipmentDict;
}
