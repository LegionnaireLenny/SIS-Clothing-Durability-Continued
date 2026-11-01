using System;

public class SISClothingDurability_Utils
{
	public static bool IsInjuryLocationBottom(InjuryLocation location)
	{
		return location == InjuryLocation.LeftLeg || location == InjuryLocation.RightLeg;
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

	public static void SetStatusBarMsgWithTimeout(string msg, float timeout)
	{
		HudBehaviour.Instance.StatusBarMsg = msg;
		HudBehaviour.Instance.StatusBarMsgTimeout = timeout;
		HudBehaviour.Instance.StatusBarMsgShowForOneFrame = false;
		HudBehaviour.Instance.StatusBarMsgAction = default;
	}

    public static bool IsPrototypeNormalClothing(EquipmentPrototype equip)
    {
        return !IsPrototypeArmorClothing(equip) && (equip.ClothingType == ClothingType.Hat || 
            (SISClothingDurability_Config.CanGlassesDegrade && equip.ClothingType == ClothingType.Glasses) ||
			equip.ClothingType == ClothingType.Top ||
            equip.ClothingType == ClothingType.Bottom ||
            (SISClothingDurability_Config.CanShoesDegrade && equip.ClothingType == ClothingType.Shoes) ||
            (SISClothingDurability_Config.CanBackpacksDegrade && equip.ClothingType == ClothingType.Backpack) ||
            equip.TypeName == BaseObjectType.Beanie ||
            equip.TypeName == BaseObjectType.Balaklava);
    }

    public static bool IsPrototypeArmorClothing(EquipmentPrototype equip)
	{
		return equip.TypeName == BaseObjectType.Armor || 
			equip.ClothingType == ClothingType.BodyArmor ||
			equip.ClothingType == ClothingType.LegArmor;
	}

	public static bool CanClothingTypeDegrade(ClothingType type)
	{
		bool flag = (!SISClothingDurability_Config.CanBackpacksDegrade && type == ClothingType.Backpack) || 
			(!SISClothingDurability_Config.CanShoesDegrade && type == ClothingType.Shoes) || 
			(!SISClothingDurability_Config.CanGlassesDegrade && type == ClothingType.Glasses);
		bool flag2;
		if (flag)
		{
			SISClothingDurability_Config.Log(type.ToString() + " CANT degrade");
			flag2 = false;
		}
		else
		{
			SISClothingDurability_Config.Log(type.ToString() + " CAN degrade");
			flag2 = true;
		}
		return flag2;
	}

	public static float NextFloat()
	{
		double num = Session.Instance.DeterministicRand.NextDouble();
		return (float)num;
	}

	public static int NextBetweenInts(int min, int max)
	{
		return SISClothingDurability_Utils.random.Next(min, max + 1);
	}

	private static Random random = new Random();
}
