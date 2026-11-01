using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISCD_ClothingTypeSettingsAddon
{
	[XmlArray(ElementName = "ClothingTypes")]
	[XmlArrayItem(ElementName = "ClothingSettings")]
	public List<ClothingTypeSettings> ClothingTypes = new List<ClothingTypeSettings>();
}
