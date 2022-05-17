using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public ScoreManager MyScoreManager;
	public SaveDataManager MySaveDataManager;

	public int GameSpeed = 1;

	private List<TransportFacilityCard> _facilityCards = new List<TransportFacilityCard>();

	private void Awake()
	{
		//Instantiate game manager
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		MyScoreManager = new ScoreManager();
	}

	private void Start()
	{
		if (!TryGetComponent(out MySaveDataManager))
			MySaveDataManager = this.gameObject.AddComponent<SaveDataManager>();

		GameObject[] lgo = GameObject.FindGameObjectsWithTag("FacilityCard");

		foreach (GameObject go in lgo)
			if (go.TryGetComponent(out TransportFacilityCard tfc))
				_facilityCards.Add(tfc);

		InitializeFromSave();
	}

	private void InitializeFromSave()
	{
		MySaveDataManager.LoadSaveDataFile();

		if (MySaveDataManager.MySaveData == null)
		{
			MySaveDataManager.CreateSaveDataFile(true);
			return;
		}

		MyScoreManager.SetHappinessPoints(MySaveDataManager.MySaveData.HappinessPoints);
		MyScoreManager.SetPollutionPoints(MySaveDataManager.MySaveData.PollutionPoints);
	}

	// Update is called once per frame
	private void Update()
	{
		StatusUpdate();
	}

	private void StatusUpdate()
	{
		foreach (TransportFacilityCard tfc in _facilityCards)
		{
			MyScoreManager.AddHappinessPoints(tfc.GetCurrentHappinessPoints() * Time.deltaTime * GameSpeed);
			MyScoreManager.AddPollutionPoints(tfc.GetCurrentPollutionPoints() * Time.deltaTime * GameSpeed);
		}
	}

	private void OnApplicationQuit()
	{
		MySaveDataManager.MySaveData.HappinessPoints = MyScoreManager.HappinessPoints;
		MySaveDataManager.MySaveData.PollutionPoints = MyScoreManager.PollutionPoints;

		MySaveDataManager.SaveSaveDataFile();
	}
}
