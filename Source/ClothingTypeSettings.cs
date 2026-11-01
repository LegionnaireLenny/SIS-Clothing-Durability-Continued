using System;

[Serializable]
public class ClothingTypeSettings
{
	public ClothingTypeSettings()
	{
	}

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

	public static ClothingTypeSettings GetDefault()
	{
		return new ClothingTypeSettings();
	}

	public string Name;

	public bool CanDegradeOnAttack = true;

	public bool CanDegradeOverTime = true;

	public bool CanDegradeOnDamaged = true;

	public float DegradeRate = 1f;

	public float MaxDurability = 15f;

	public int MinPartsOnBroken = 0;

	public int MaxPartsOnBroken = 1;

	public float DegradeOverTimePcnt = 0.01f;

	public float DegradeOnAttackPcnt = 0.01f;

	public float DegradeOnExplosionDamagePcnt = 0.95f;

	public float DegradeOnBulletDamagePcnt = 0.35f;

	public float DegradeOnArrowDamagePcnt = 0.3f;

	public float DegradeOnBluntDamagePcnt = 0.15f;

	public float DegradeOnSharpDamagePcnt = 0.25f;

	public float DegradeOnVehicleImpactDamagePcnt = 0.5f;

	public float DegradeOnFireDamagePcnt = 0.6f;

	public float DegradeOnTrapExplosionDamagePcnt = 0.6f;

	public float DegradeOnZombieBiteDamagePcnt = 0.2f;
}
