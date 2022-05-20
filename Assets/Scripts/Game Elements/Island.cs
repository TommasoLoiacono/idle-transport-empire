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

	[Header("Island Animator")]
	public Animator IslandAnimator;

	[Header("UI components")]
	public TextMeshProUGUI InfoText;
	public GameObject InfoGameObject;
	public Button BuyButton;
	public TextMeshProUGUI BuyText;
	public TextMeshProUGUI PollutionText;

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

		BuyText.text = "Buy<br>" + String.Format("{0:0.00}", MyIslandData.MilesCost) + " <sprite=0>";
		PollutionText.text = "<sprite=0>   must be < " + String.Format("{0:0.00}", MyIslandData.PollutionCost);
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
		UpdateAnimator();
	}

	private void UpdateUI()
	{
		BuyButton.interactable = GameManager.Instance.CanBeBoughtWithMiles(MyIslandData.MilesCost)
			&& GameManager.Instance.GetCurrentPollutionPoints() < MyIslandData.PollutionCost
			&& !IsUnlocked;

		if (IsVisible && !IsUnlocked)
			InfoText.text = "";
		if (IsVisible && IsUnlocked && !IsReclaimed)
		{
			InfoText.text = "You still have to click on " + _ballsAlive + " Usurper Balls to reclaim this island";
		}

		InfoText.gameObject.SetActive(IsVisible && !IsReclaimed);
		InfoGameObject.gameObject.SetActive(IsVisible && IsUnlocked && !IsReclaimed);
		BuyText.gameObject.SetActive(IsVisible && !IsUnlocked);
		PollutionText.gameObject.SetActive(IsVisible && !IsUnlocked);
		BuyButton.gameObject.SetActive(IsVisible && !IsUnlocked);
	}

	private void UpdateAnimator()
	{
		IslandAnimator.SetBool("IsVisible", IsVisible);
		IslandAnimator.SetBool("IsUnlocked", IsUnlocked);
		IslandAnimator.SetBool("IsReclaimed", IsReclaimed);
	}

	private bool CheckVisibilityLimit()
	{
		return GameManager.Instance.GetCurrentMilesPoints() >= MyIslandData.VisibilityLimit;
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
		if (GameManager.Instance.TrySpendMilesPoints(MyIslandData.MilesCost))
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
