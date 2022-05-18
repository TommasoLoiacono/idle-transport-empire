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

	[HideInInspector]
	public bool IsVisible;
	public bool IsUnlocked;
	public bool IsReclaimed;

	public string MyUUID { get { return _myUUID.ID; } }

	private List<TransportFacilityCard> _myTransportCards = new List<TransportFacilityCard>();
	private List<UsurperBall> _myUsurperBalls = new List<UsurperBall>();
	private UUID _myUUID;
	private int _ballsAlive = 0;


	private void Awake()
	{
		IsUnlocked = MyIslandData.StartingIsland;
		IsVisible = MyIslandData.StartingIsland;
		IsReclaimed = MyIslandData.StartingIsland;
	}

	private void Start()
	{
		if (!TryGetComponent(out _myUUID))
			_myUUID = this.gameObject.AddComponent<UUID>();

		_myTransportCards.AddRange(this.gameObject.GetComponentsInChildren<TransportFacilityCard>());
		_myUsurperBalls.AddRange(this.gameObject.GetComponentsInChildren<UsurperBall>());

		BuyText.text = "Buy " + MyIslandData.HappinessCost;
	}

	private void Update()
	{
		if (!IsVisible && CheckVisibilityLimit())
			ShowIsland();

		foreach (TransportFacilityCard tfc in _myTransportCards)
			tfc.gameObject.SetActive(IsUnlocked && IsVisible && IsReclaimed);

		foreach (UsurperBall b in _myUsurperBalls)
			if (IsReclaimed && b.IsUsurperAlive) b.KillUsurper();

		if (IsVisible && IsUnlocked && !IsReclaimed)
		{
			_ballsAlive = 0;

			foreach (UsurperBall b in _myUsurperBalls)
				if (b.IsUsurperAlive)
					_ballsAlive++;

			IsReclaimed = _ballsAlive == 0;
		}

		UpdateUI();
	}

	private void UpdateUI()
	{
		BuyButton.interactable = GameManager.Instance.CanBeBoughtWithHappiness(MyIslandData.HappinessCost)
			&& GameManager.Instance.GetCurrentPollutionPoints() < MyIslandData.PollutionCost
			&& !IsUnlocked;

		if (IsVisible && !IsUnlocked)
			InfoText.text = "You must have less than " + MyIslandData.PollutionCost + " to buy this island";
		if (IsVisible && IsUnlocked && !IsReclaimed)
		{
			InfoText.text = "You still have to click on " + _ballsAlive + " Usurper Balls to reclaim this island";
		}

		InfoText.gameObject.SetActive(IsVisible && !IsReclaimed);
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
		{
			IsUnlocked = true;

			EnableAllIslandUsurpers();
		}
	}

	public void EnableAllIslandUsurpers()
	{
		foreach (UsurperBall b in _myUsurperBalls)
			b.MakeUsurperKillable();
	}
}
