using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Transport Facility Data", menuName = "Idle Transport Empire/Transport Facility Data", order = 1)]
public class TransportFacilityData : ScriptableObject
{
	public Sprite FacilitySprite;
	public int HappinessPointsRate;
	public int PollutionPointsRate;
	public string InfoText;
	public int BuildCost;
	public float BuildCostMultiplier;
	public int DismantleCost;
}
