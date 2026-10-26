using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

// Token: 0x02000013 RID: 19
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISCD_ClothingTypeSettingsAddon
{
	// Token: 0x04000029 RID: 41
	[XmlArray(ElementName = "ClothingTypes")]
	[XmlArrayItem(ElementName = "ClothingSettings")]
	public List<ClothingTypeSettings> ClothingTypes = new List<ClothingTypeSettings>();
}
