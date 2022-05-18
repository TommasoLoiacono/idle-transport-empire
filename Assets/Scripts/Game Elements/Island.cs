using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Island : MonoBehaviour
{
	[Header("Island Data")]
	public IslandData MyIslandData;
	
	[Header("UI components")]
	public TextMeshProUGUI InfoText;
	public Button BuyButton;
	public TextMeshProUGUI BuyText;

	[Header("Island Status")]
	public bool IsUnlocked;
	public bool IsVisible;

	public string MyUUID { get { return _myUUID.ID; } }

	private List<TransportFacilityCard> MyTransportCards = new List<TransportFacilityCard>();
	private UUID _myUUID;

	private void Awake()
	{
		IsUnlocked = MyIslandData.StartingIsland;
		IsVisible = MyIslandData.StartingIsland;
	}

	private void Start()
	{
		if (!TryGetComponent(out _myUUID))
			_myUUID = this.gameObject.AddComponent<UUID>();

		MyTransportCards.AddRange(this.gameObject.GetComponentsInChildren<TransportFacilityCard>());

		InfoText.text = "You must have less than " + MyIslandData.PollutionCost + " to buy this island";
		BuyText.text = "Buy " + MyIslandData.HappinessCost;
	}

	private void Update()
	{
		if (!IsVisible && CheckVisibilityLimit())
			ShowIsland();

		foreach (TransportFacilityCard tfc in MyTransportCards)
			tfc.gameObject.SetActive(IsUnlocked && IsVisible);

		UpdateUI();
	}

	private void UpdateUI()
	{
		BuyButton.interactable = GameManager.Instance.CanBeBoughtWithHappiness(MyIslandData.HappinessCost) 
			&& GameManager.Instance.GetCurrentPollutionPoints() < MyIslandData.PollutionCost 
			&& !IsUnlocked;

		InfoText.gameObject.SetActive(IsVisible && !IsUnlocked);
		BuyText.gameObject.SetActive(IsVisible && !IsUnlocked);
		BuyButton.gameObject.SetActive(IsVisible && !IsUnlocked);
	}

	private bool CheckVisibilityLimit()
	{
		return GameManager.Instance.GetCurrentHappinessPoints() >= MyIslandData.VisibilityLimit;
	}

	private void ShowIsland()
	{
		IsVisible = true;
	}

	///
	/// Public Methods
	///

	public void UnlockIsland()
	{
		if (GameManager.Instance.TrySpendHappinessPoints(MyIslandData.HappinessCost))
			IsUnlocked = true;
	}
}
