using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
	public float MilesPoints;
	public float PollutionPoints;
	public Dictionary<string, int> Facilities;
	public Dictionary<string, bool> IslandVisibilityStatus;
	public Dictionary<string, bool> IslandUnlockStatus;
	public Dictionary<string, bool> IslandReclaimStatus;
	public DateTime exitTime;

	public SaveData()
	{
		MilesPoints = 0;
		PollutionPoints = 0;
		Facilities = new Dictionary<string, int>();
		IslandVisibilityStatus = new Dictionary<string, bool>();
		IslandUnlockStatus = new Dictionary<string, bool>();
		IslandReclaimStatus = new Dictionary<string, bool>();
		exitTime = DateTime.Now;
	}

	public SaveData GetClone()
	{
		SaveData clone = new SaveData();
		clone.MilesPoints = MilesPoints;
		clone.PollutionPoints = PollutionPoints;
		clone.Facilities = Facilities;
		clone.exitTime = exitTime;
		clone.IslandVisibilityStatus = IslandVisibilityStatus;
		clone.IslandUnlockStatus = IslandUnlockStatus;
		clone.IslandReclaimStatus = IslandReclaimStatus;
		return clone;
	}
}
