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
	public TextMeshProUGUI NameText;
	public TextMeshProUGUI MilesRateText;
	public TextMeshProUGUI PollutionRateText;
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
		BuildButton.interactable = GameManager.Instance.CanBeBoughtWithMiles(GetCurrentBuildCost());
		DismantleButton.interactable = CurrentlyOwned > 0;

		InfoImage.sprite = MyTransportFacilityData.FacilitySprite;
		MilesRateText.text = "+" + String.Format("{0:0.00}", MyTransportFacilityData.MilesPointsRate) + " <sprite=0>";
		if (MyTransportFacilityData.PollutionPointsRate >= 0)
			PollutionRateText.text = "+" + String.Format("{0:0.00}", MyTransportFacilityData.PollutionPointsRate) + " <sprite=0>";
		else
			PollutionRateText.text = String.Format("{0:0.00}", MyTransportFacilityData.PollutionPointsRate) + " <sprite=0>";

		NameText.text = MyTransportFacilityData.NameText;
		CurrentlyOwnedText.text = "Owned: " + CurrentlyOwned.ToString();
		BuildText.text = "Build<br>" + String.Format("{0:0.00}", GetCurrentBuildCost()) + " <sprite=0>";
		DismantleText.text = "Dismantle<br>" + String.Format("{0:0.00}", GetDismantleCost()) + " <sprite=0>";
	}

	public double GetCurrentMilesPoints()
	{
		return MyTransportFacilityData.MilesPointsRate * CurrentlyOwned;
	}

	public double GetCurrentPollutionPoints()
	{
		return MyTransportFacilityData.PollutionPointsRate * CurrentlyOwned;
	}

	public double GetCurrentBuildCost()
	{
		return (float)(MyTransportFacilityData.BuildCost + MyTransportFacilityData.BuildCost * Math.Pow(CurrentlyOwned, MyTransportFacilityData.FatigueRate));
	}

	public double GetDismantleCost()
	{
		return MyTransportFacilityData.DismantleCost;
	}

	public void BuildFacility()
	{
		if (GameManager.Instance.TrySpendMilesPoints(GetCurrentBuildCost()))
			CurrentlyOwned++;
	}

	public void DismantleFacility()
	{
		if (CurrentlyOwned > 0)
		{
			CurrentlyOwned--;
			GameManager.Instance.GainMilesPoints(MyTransportFacilityData.DismantleCost);
		}
	}
}
