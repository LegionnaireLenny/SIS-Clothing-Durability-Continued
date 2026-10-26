using System;

// Token: 0x02000012 RID: 18
[Serializable]
public class ClothingTypeSettings
{
	// Token: 0x0600002B RID: 43 RVA: 0x00003894 File Offset: 0x00001A94
	public ClothingTypeSettings()
	{
	}

	// Token: 0x0600002C RID: 44 RVA: 0x0000395C File Offset: 0x00001B5C
	public ClothingTypeSettings(string name, float degradeRate = 1f, float maxDurability = 15f, int minPartsOnBroken = 0, int maxPartsOnBroken = 1, float degradeOverTimePcnt = 0.01f, float degradeOnExplosionDamagePcnt = 0.6f, float degradeOnBulletDamagePcnt = 0.35f, float degradeOnArrowDamagePcnt = 0.3f, float degradeOnBluntDamagePcnt = 0.15f, float degradeOnSharpDamagePcnt = 0.25f, float degradeOnVehicleImpactDamagePcnt = 0.5f, float degradeOnFireDamagePcnt = 0.6f, float degradeOnTrapExplosionDamagePcnt = 0.6f, float degradeOnZombieBiteDamagePcnt = 0.2f, float probabilityDegradeOnAttackPcnted = 1f, bool canDegradeOnAttackPcnt = true, bool canDegradeOverTime = true, bool canDegradeOnDamaged = true, float degradeOnAttackPcnt = 0.01f)
	{
		this.Name = name;
		this.DegradeRate = degradeRate;
		this.MaxDurability = maxDurability;
		this.MinPartsOnBroken = minPartsOnBroken;
		this.MaxPartsOnBroken = maxPartsOnBroken;
		this.DegradeOverTimePcnt = degradeOverTimePcnt;
		this.DegradeOnAttackPcnt = degradeOnAttackPcnt;
		this.DegradeOnExplosionDamagePcnt = degradeOnExplosionDamagePcnt;
		this.DegradeOnBulletDamagePcnt = degradeOnBulletDamagePcnt;
		this.DegradeOnArrowDamagePcnt = degradeOnArrowDamagePcnt;
		this.DegradeOnBluntDamagePcnt = degradeOnBluntDamagePcnt;
		this.DegradeOnSharpDamagePcnt = degradeOnSharpDamagePcnt;
		this.DegradeOnVehicleImpactDamagePcnt = degradeOnVehicleImpactDamagePcnt;
		this.DegradeOnFireDamagePcnt = degradeOnFireDamagePcnt;
		this.DegradeOnTrapExplosionDamagePcnt = degradeOnTrapExplosionDamagePcnt;
		this.DegradeOnZombieBiteDamagePcnt = degradeOnZombieBiteDamagePcnt;
		this.CanDegradeOnAttack = canDegradeOnAttackPcnt;
		this.CanDegradeOverTime = canDegradeOverTime;
		this.CanDegradeOnDamaged = canDegradeOnDamaged;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003AB8 File Offset: 0x00001CB8
	public static ClothingTypeSettings GetDefault()
	{
		return new ClothingTypeSettings();
	}

	// Token: 0x04000016 RID: 22
	public string Name;

	// Token: 0x04000017 RID: 23
	public bool CanDegradeOnAttack = true;

	// Token: 0x04000018 RID: 24
	public bool CanDegradeOverTime = true;

	// Token: 0x04000019 RID: 25
	public bool CanDegradeOnDamaged = true;

	// Token: 0x0400001A RID: 26
	public float DegradeRate = 1f;

	// Token: 0x0400001B RID: 27
	public float MaxDurability = 15f;

	// Token: 0x0400001C RID: 28
	public int MinPartsOnBroken = 0;

	// Token: 0x0400001D RID: 29
	public int MaxPartsOnBroken = 1;

	// Token: 0x0400001E RID: 30
	public float DegradeOverTimePcnt = 0.01f;

	// Token: 0x0400001F RID: 31
	public float DegradeOnAttackPcnt = 0.01f;

	// Token: 0x04000020 RID: 32
	public float DegradeOnExplosionDamagePcnt = 0.95f;

	// Token: 0x04000021 RID: 33
	public float DegradeOnBulletDamagePcnt = 0.35f;

	// Token: 0x04000022 RID: 34
	public float DegradeOnArrowDamagePcnt = 0.3f;

	// Token: 0x04000023 RID: 35
	public float DegradeOnBluntDamagePcnt = 0.15f;

	// Token: 0x04000024 RID: 36
	public float DegradeOnSharpDamagePcnt = 0.25f;

	// Token: 0x04000025 RID: 37
	public float DegradeOnVehicleImpactDamagePcnt = 0.5f;

	// Token: 0x04000026 RID: 38
	public float DegradeOnFireDamagePcnt = 0.6f;

	// Token: 0x04000027 RID: 39
	public float DegradeOnTrapExplosionDamagePcnt = 0.6f;

	// Token: 0x04000028 RID: 40
	public float DegradeOnZombieBiteDamagePcnt = 0.2f;
}
