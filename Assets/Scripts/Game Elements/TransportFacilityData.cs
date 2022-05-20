using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Transport Facility Data", menuName = "Idle Transport Empire/Transport Facility Data", order = 1)]
public class TransportFacilityData : ScriptableObject
{
	public Sprite FacilitySprite;
	public string NameText;
	public double FatigueRate;
	public double MilesPointsRate;
	public double PollutionPointsRate;
	public double BuildCost;
	public double DismantleCost;
}
