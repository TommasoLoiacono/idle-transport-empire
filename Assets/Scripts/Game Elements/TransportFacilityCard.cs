using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TransportFacilityCard : MonoBehaviour
{
	[Header("Facility data")]
	public TransportFacilityData MyTransportFacilityData;

	[Header("UI components")]
	public Image InfoImage;
	public TextMeshProUGUI HappinessRateText;
	public TextMeshProUGUI PollutionRateText;
	public TextMeshProUGUI InfoText;
	public TextMeshProUGUI CurrentlyOwnedText;
	public Button BuildButton;
	public TextMeshProUGUI BuildText;
	public Button DismantleButton;
	public TextMeshProUGUI DismantleText;

	[Header("Card Status")]
	public int CurrentlyOwned;

	public string MyUUID {  get { return _myUUID.ID; } }

	private UUID _myUUID;

	public void Start()
	{
		if (!TryGetComponent(out _myUUID))
			_myUUID = this.gameObject.AddComponent<UUID>();
	}

	private void Update()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		BuildButton.interactable = GameManager.Instance.CanBeBoughtWithHappiness(GetCurrentBuildCost());
		DismantleButton.interactable = CurrentlyOwned > 0;

		InfoImage.sprite = MyTransportFacilityData.FacilitySprite;
		HappinessRateText.text = MyTransportFacilityData.HappinessPointsRate.ToString();
		PollutionRateText.text = MyTransportFacilityData.PollutionPointsRate.ToString();
		InfoText.text = MyTransportFacilityData.InfoText;
		CurrentlyOwnedText.text = CurrentlyOwned.ToString();
		BuildText.text = "Build " + GetCurrentBuildCost().ToString();
		DismantleText.text = "Dismantle " + GetDismantleCost().ToString();
	}

	public int GetCurrentHappinessPoints()
	{
		return MyTransportFacilityData.HappinessPointsRate * CurrentlyOwned;
	}

	public int GetCurrentPollutionPoints()
	{
		return MyTransportFacilityData.PollutionPointsRate * CurrentlyOwned;
	}

	public float GetCurrentBuildCost()
	{
		return MyTransportFacilityData.BuildCost + MyTransportFacilityData.BuildCost * MyTransportFacilityData.BuildCostMultiplier * CurrentlyOwned;
	}

	public int GetDismantleCost()
	{
		return MyTransportFacilityData.DismantleCost;
	}

	public void BuildFacility()
	{
		if (GameManager.Instance.TrySpendHappinessPoints(GetCurrentBuildCost()))
			CurrentlyOwned++;
	}

	public void DismantleFacility()
	{
		if (CurrentlyOwned > 0)
		{
			CurrentlyOwned--;
			GameManager.Instance.GainHappinessPoints(MyTransportFacilityData.DismantleCost);
		}
	}
}
