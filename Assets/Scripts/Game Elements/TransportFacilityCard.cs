using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TransportFacilityCard : MonoBehaviour
{
	[Header("Facility data")]
	public TransportFacilityData myTransportFacilityData;

	[Header("UI components")]
	public Image InfoImage;
	public TextMeshProUGUI HappinessRateText;
	public TextMeshProUGUI PollutionRateText;
	public TextMeshProUGUI InfoText;
	public Button BuildButton;
	public TextMeshProUGUI BuildText;
	public Button DismantleButton;
	public TextMeshProUGUI DismantleText;

	public int CurrentlyOwned;

	public string MyUUID {  get { return _myUUID.ID; } }

	private UUID _myUUID;

	public void Start()
	{
		if (!TryGetComponent(out _myUUID))
			_myUUID = this.gameObject.AddComponent<UUID>();

		SetupUI();
	}

	private void Update()
	{
		BuildButton.interactable = GameManager.Instance.CanBeBoughtWithHappiness(GetCurrentBuildCost());
		DismantleButton.interactable = CurrentlyOwned > 0;
	}

	private void SetupUI()
	{
		InfoImage.sprite = myTransportFacilityData.FacilitySprite;
		HappinessRateText.text = myTransportFacilityData.HappinessPointsRate.ToString();
		PollutionRateText.text = myTransportFacilityData.PollutionPointsRate.ToString();
		InfoText.text = myTransportFacilityData.InfoText;
		BuildText.text = "Build " + GetCurrentBuildCost().ToString();
		DismantleText.text = "Dismantle " + GetDismantleCost().ToString();
	}

	public int GetCurrentHappinessPoints()
	{
		return myTransportFacilityData.HappinessPointsRate * CurrentlyOwned;
	}

	public int GetCurrentPollutionPoints()
	{
		return myTransportFacilityData.PollutionPointsRate * CurrentlyOwned;
	}

	public float GetCurrentBuildCost()
	{
		return myTransportFacilityData.BuildCost + myTransportFacilityData.BuildCost * myTransportFacilityData.BuildCostMultiplier * CurrentlyOwned;
	}

	public int GetDismantleCost()
	{
		return myTransportFacilityData.DismantleCost;
	}

	public void BuildFacility()
	{
		if (GameManager.Instance.TrySpendHappinessPoints(GetCurrentBuildCost()))
			CurrentlyOwned++;
	}

	public void DismantleFacility()
	{
		if (CurrentlyOwned > 0)
			CurrentlyOwned--;
	}
}
