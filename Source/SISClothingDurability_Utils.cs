using System;
using static SISClothingDurability_Config;

public class SISClothingDurability_Utils
{
    public static void SetStatusBarMsgWithTimeout(string msg, float timeout)
    {
        HudBehaviour.Instance.StatusBarMsg = msg;
        HudBehaviour.Instance.StatusBarMsgTimeout = timeout;
        HudBehaviour.Instance.StatusBarMsgShowForOneFrame = false;
        HudBehaviour.Instance.StatusBarMsgAction = default;
    }

    public static bool IsInjuryLocationBottom(InjuryLocation location)
    {
        return location == InjuryLocation.LeftLeg ||
            location == InjuryLocation.RightLeg;
    }

    public static bool IsInjuryLocationTop(InjuryLocation location)
    {
        return location == InjuryLocation.Torso ||
            location == InjuryLocation.LeftArm ||
            location == InjuryLocation.RightArm;
    }

    public static bool IsWearingClothingType(Character character, ClothingType clothingType)
    {
        return character.Clothes[(int)clothingType] != null;
    }

    public static bool IsLocationProtected(Character character, Equipment equipment)
    {
        if (!ArmorProtectsFromDegradation)
        {
            return false;
        }

        return (equipment.GetClothingType() == ClothingType.Top && IsWearingClothingType(character, ClothingType.BodyArmor)) ||
            (equipment.GetClothingType() == ClothingType.Bottom && IsWearingClothingType(character, ClothingType.LegArmor));
    }

    public static bool IsPrototypeDegradableClothing(EquipmentPrototype equip)
    {
        return !IsPrototypeArmorClothing(equip) && 
            (equip.ClothingType == ClothingType.Hat ||
            (CanGlassesDegrade && equip.ClothingType == ClothingType.Glasses) ||
            equip.ClothingType == ClothingType.Top ||
            equip.ClothingType == ClothingType.Bottom ||
            (CanShoesDegrade && equip.ClothingType == ClothingType.Shoes) ||
            (CanBackpacksDegrade && equip.ClothingType == ClothingType.Backpack) ||
            equip.TypeName == BaseObjectType.Beanie ||
            equip.TypeName == BaseObjectType.Balaklava);
    }

    public static bool IsPrototypeArmorClothing(EquipmentPrototype equip)
    {
        return equip.TypeName == BaseObjectType.Armor ||
            equip.ClothingType == ClothingType.BodyArmor ||
            equip.ClothingType == ClothingType.LegArmor;
    }

    public static bool IsClothingDegradationEnabledForCharacter(Character character)
    {
        return (EnabledForNPCs && !character.IsOnPlayersTeam()) ||
            (EnabledForPlayerCommunity && character.IsOnPlayersTeam() && character != Hud.Instance.LocalControlledCharacter) ||
            (EnabledForPlayer && character.IsOnPlayersTeam() && character == Hud.Instance.LocalControlledCharacter);
    }

    public static bool IsSpeakingOnItemBrokenEnabledForCharacter(Character character)
    {
        return CharactersSpeakOnClothingBroken && 
            ((EnabledForNPCs && !character.IsOnPlayersTeam()) ||
            (EnabledForPlayerCommunity && character.IsOnPlayersTeam() && character != Hud.Instance.LocalControlledCharacter) ||
            (EnabledForPlayer && character.IsOnPlayersTeam() && character == Hud.Instance.LocalControlledCharacter));
    }


    public static float NextFloat()
    {
        double num = Session.Instance.DeterministicRand.NextDouble();
        return (float)num;
    }

    public static int NextBetweenInts(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    private static Random random = new Random();
}
