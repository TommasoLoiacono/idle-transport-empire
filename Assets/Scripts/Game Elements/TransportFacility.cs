using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportFacilityCard : MonoBehaviour
{
	public TransportFacilityData myTransportFacilityData;

	public int CurrentlyOwned;

	public int GetCurrentHappinessPoints()
	{
		return myTransportFacilityData.HappinessPointsRate * CurrentlyOwned;
	}

	public int GetCurrentPollutionPoints()
	{
		return myTransportFacilityData.PollutionPointsRate * CurrentlyOwned;
	}

	public int GetCurrentBuildCost()
	{
		return myTransportFacilityData.BuildCost + myTransportFacilityData.BuildCost * myTransportFacilityData.BuildCostMultiplier * CurrentlyOwned;
	}

	public int GetDismantleCost()
	{
		return myTransportFacilityData.DismantleCost;
	}

	public void BuildFacility()
	{
		CurrentlyOwned++;
	}

	public void DismantleFacility()
	{
		if (CurrentlyOwned > 0)
			CurrentlyOwned--;
	}
}
