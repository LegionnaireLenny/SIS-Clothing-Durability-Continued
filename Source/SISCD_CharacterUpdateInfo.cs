using System;

// Token: 0x02000016 RID: 22
public class SISCD_CharacterUpdateInfo
{
	// Token: 0x06000043 RID: 67 RVA: 0x00005529 File Offset: 0x00003729
	public SISCD_CharacterUpdateInfo()
	{
		this.Dts = 0f;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0000553E File Offset: 0x0000373E
	public SISCD_CharacterUpdateInfo(TimeSpan time, float dts)
	{
		this.Time = time;
		this.Dts = dts;
	}

	// Token: 0x0400007A RID: 122
	public TimeSpan Time;

	// Token: 0x0400007B RID: 123
	public float Dts;
}
