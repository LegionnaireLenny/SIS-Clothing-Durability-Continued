using System;

// Token: 0x02000018 RID: 24
public class SISClothingDurability_Utils
{
	// Token: 0x06000060 RID: 96 RVA: 0x00005F90 File Offset: 0x00004190
	public static bool IsInjuryLocationBottom(InjuryLocation location)
	{
		return location == 4 || location == 5;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00005FB0 File Offset: 0x000041B0
	public static bool IsInjuryLocationTop(InjuryLocation location)
	{
		return location == null || location == 2 || location == 3;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00005FD0 File Offset: 0x000041D0
	public static bool IsWearingClothingType(Character character, ClothingType clothingType)
	{
		return character.Clothes[clothingType] != null;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00005FED File Offset: 0x000041ED
	public static void SetStatusBarMsgWithTimeout(string msg, float timeout)
	{
		HudBehaviour.Instance.StatusBarMsg = msg;
		HudBehaviour.Instance.StatusBarMsgTimeout = timeout;
		HudBehaviour.Instance.StatusBarMsgShowForOneFrame = false;
		HudBehaviour.Instance.StatusBarMsgAction = default(AvailableAction);
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00006024 File Offset: 0x00004224
	public static bool IsPrototypeNormalClothing(EquipmentPrototype equip)
	{
		return (equip.ClothingType == 2 || equip.ClothingType == 3 || equip.ClothingType == null || (SISClothingDurability_Config.CanShoesDegrade && equip.ClothingType == 4) || (SISClothingDurability_Config.CanGlassesDegrade && equip.ClothingType == 1) || equip.TypeName == 114 || equip.TypeName == 115 || (SISClothingDurability_Config.CanBackpacksDegrade && equip.ClothingType == 5)) && equip.TypeName != 11;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000060A4 File Offset: 0x000042A4
	public static bool IsPrototypeArmorClothing(EquipmentPrototype equip)
	{
		return equip.ClothingType == 6 || equip.ClothingType == 7;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000060CC File Offset: 0x000042CC
	public static bool CanClothingTypeDegrade(ClothingType type)
	{
		bool flag = (!SISClothingDurability_Config.CanBackpacksDegrade && type == 5) || (!SISClothingDurability_Config.CanShoesDegrade && type == 4) || (!SISClothingDurability_Config.CanGlassesDegrade && type == 1);
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

	// Token: 0x06000067 RID: 103 RVA: 0x0000614C File Offset: 0x0000434C
	public static float NextFloat()
	{
		double num = Session.Instance.DeterministicRand.NextDouble();
		return (float)num;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00006170 File Offset: 0x00004370
	public static int NextBetweenInts(int min, int max)
	{
		return SISClothingDurability_Utils.random.Next(min, max + 1);
	}

	// Token: 0x04000080 RID: 128
	private static Random random = new Random();
}
