using System;
using System.ComponentModel;
using System.Xml.Serialization;

// Token: 0x02000006 RID: 6
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISCD_ClothesDurabilityInfo
{
	// Token: 0x0600000E RID: 14 RVA: 0x00002E56 File Offset: 0x00001056
	public SISCD_ClothesDurabilityInfo()
	{
		this.Id = -1;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002E84 File Offset: 0x00001084
	public SISCD_ClothesDurabilityInfo(int id, float durability, float maxDurability, bool isBroken)
	{
		this.Id = id;
		this.Durability = durability;
		this.MaxDurability = maxDurability;
		this.IsBroken = isBroken;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002ED4 File Offset: 0x000010D4
	public SISCD_ClothesDurabilityInfo(int id, float durability, float maxDurability)
	{
		this.Id = id;
		this.Durability = durability;
		this.MaxDurability = maxDurability;
		this.IsBroken = false;
	}

	// Token: 0x04000001 RID: 1
	public int Id;

	// Token: 0x04000002 RID: 2
	public float Durability = 15f;

	// Token: 0x04000003 RID: 3
	public float MaxDurability = 15f;

	// Token: 0x04000004 RID: 4
	public bool IsBroken = false;
}
