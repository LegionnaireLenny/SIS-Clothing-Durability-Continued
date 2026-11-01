using System;

public class SISCD_CharacterUpdateInfo
{
	public SISCD_CharacterUpdateInfo()
	{
		this.Dts = 0f;
	}

	public SISCD_CharacterUpdateInfo(TimeSpan time, float dts)
	{
		this.Time = time;
		this.Dts = dts;
	}

	public TimeSpan Time;

	public float Dts;
}
