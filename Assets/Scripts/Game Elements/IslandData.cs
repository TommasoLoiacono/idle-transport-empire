using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Island Data", menuName = "Idle Transport Empire/Island Data", order = 2)]

public class IslandData : ScriptableObject
{
	public bool StartingIsland;
	public int MilesCost;
	public int PollutionCost;
	public int VisibilityLimit;
	public string InfoText;
}
