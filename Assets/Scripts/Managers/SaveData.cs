using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
	public float HappinessPoints;
	public float PollutionPoints;
	public Dictionary<string, int> Facilities;
	public DateTime exitTime;

	public SaveData()
	{
		HappinessPoints = 0;
		PollutionPoints = 0;
		Facilities = new Dictionary<string, int>();
		exitTime = DateTime.Now;
	}

	public SaveData GetClone()
	{
		SaveData clone = new SaveData();
		clone.HappinessPoints = HappinessPoints;
		clone.PollutionPoints = PollutionPoints;
		clone.Facilities = Facilities;
		clone.exitTime = exitTime;
		return clone;
	}
}
