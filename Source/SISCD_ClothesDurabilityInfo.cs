using System;
using System.ComponentModel;
using System.Xml.Serialization;

[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISCD_ClothesDurabilityInfo
{
	public SISCD_ClothesDurabilityInfo()
	{
		this.Id = -1;
	}

	public SISCD_ClothesDurabilityInfo(int id, float durability, float maxDurability, bool isBroken)
	{
		this.Id = id;
		this.Durability = durability;
		this.MaxDurability = maxDurability;
		this.IsBroken = isBroken;
	}

	public SISCD_ClothesDurabilityInfo(int id, float durability, float maxDurability)
	{
		this.Id = id;
		this.Durability = durability;
		this.MaxDurability = maxDurability;
		this.IsBroken = false;
	}

	public int Id;

	public float Durability = 15f;

	public float MaxDurability = 15f;

	public bool IsBroken = false;
}
