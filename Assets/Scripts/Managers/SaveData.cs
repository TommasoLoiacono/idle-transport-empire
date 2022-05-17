using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public float HappinessPoints;
	public float PollutionPoints;
	public Dictionary<int, string> Facilities;

	public SaveData()
	{
		HappinessPoints = 0;
		PollutionPoints = 0;
		Facilities = new Dictionary<int, string>();
	}

	public SaveData GetClone()
	{
		SaveData clone = new SaveData();
		clone.HappinessPoints = HappinessPoints;
		clone.PollutionPoints = PollutionPoints;
		clone.Facilities = Facilities;
		return clone;
	}
}
