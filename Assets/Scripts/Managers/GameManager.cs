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

		foreach (TransportFacilityCard tfc in _facilityCards)
			if (MySaveDataManager.MySaveData.Facilities.ContainsKey(tfc.MyUUID))
				tfc.CurrentlyOwned = MySaveDataManager.MySaveData.Facilities.GetValueOrDefault(tfc.MyUUID);

		foreach (TransportFacilityCard tfc in _facilityCards)
		{
			MyScoreManager.AddHappinessPoints(tfc.GetCurrentHappinessPoints() * (float)(DateTime.Now - MySaveDataManager.MySaveData.exitTime).TotalSeconds * GameSpeed);
			MyScoreManager.AddPollutionPoints(tfc.GetCurrentPollutionPoints() * (float)(DateTime.Now - MySaveDataManager.MySaveData.exitTime).TotalSeconds * GameSpeed);
		}
	}

	// Update is called once per frame
	private void Update()
	{
		UpdateGameStatus();
		DoDebugActions();
	}

	private void UpdateGameStatus()
	{
		foreach (TransportFacilityCard tfc in _facilityCards)
		{
			MyScoreManager.AddHappinessPoints(tfc.GetCurrentHappinessPoints() * Time.deltaTime * GameSpeed);
			MyScoreManager.AddPollutionPoints(tfc.GetCurrentPollutionPoints() * Time.deltaTime * GameSpeed);
		}
	}

	private void DoDebugActions()
	{
		if (Input.GetKeyDown(KeyCode.F12))
		{
			MySaveDataManager.DeleteSaveDataFile();
			MySaveDataManager.CreateSaveDataFile();
		}
	}

	private void OnApplicationQuit()
	{
		MySaveDataManager.MySaveData.HappinessPoints = MyScoreManager.HappinessPoints;
		MySaveDataManager.MySaveData.PollutionPoints = MyScoreManager.PollutionPoints;
		
		MySaveDataManager.MySaveData.Facilities.Clear();

		foreach (TransportFacilityCard tfc in _facilityCards)
			MySaveDataManager.MySaveData.Facilities.Add(tfc.MyUUID, tfc.CurrentlyOwned);

		MySaveDataManager.SaveSaveDataFile();
	}
}
