using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
	public float HappinessPoints { get => _happinessPoints; }
	public float PollutionPoints { get => _pollutionPoints; }

	private float _happinessPoints;
	private float _pollutionPoints;

	public void AddHappinessPoints(float points)
	{
		_happinessPoints += points;
	}

	public void AddPollutionPoints(float points)
	{
		if (points < 0 && PollutionPoints - points <= 0)
			_pollutionPoints = 0;

		_pollutionPoints += points;
	}

	internal void SetHappinessPoints(float points)
	{
		_happinessPoints = points;
	}

	internal void SetPollutionPoints(float points)
	{
		_pollutionPoints = points;
	}
}