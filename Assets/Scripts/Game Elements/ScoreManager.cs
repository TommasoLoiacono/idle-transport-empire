using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
	public float MilesPoints { get => _milesPoints; }
	public float PollutionPoints { get => _pollutionPoints; }

	private float _milesPoints;
	private float _pollutionPoints;

	public void AddMilesPoints(float points)
	{
		_milesPoints += points;
	}

	public void AddPollutionPoints(float points)
	{
		if (points < 0 && PollutionPoints - points <= 0)
			_pollutionPoints = 0;

		_pollutionPoints += points;
	}

	internal void SetMilesPoints(float points)
	{
		_milesPoints = points;
	}

	internal void SetPollutionPoints(float points)
	{
		_pollutionPoints = points;
	}
}