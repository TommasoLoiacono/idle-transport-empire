using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
	public double MilesPoints { get => _milesPoints; }
	public double PollutionPoints { get => _pollutionPoints; }

	private double _milesPoints;
	private double _pollutionPoints;

	public void AddMilesPoints(double points)
	{
		_milesPoints += points;
	}

	public void AddPollutionPoints(double points)
	{
		if (points < 0 && PollutionPoints - points <= 0)
			_pollutionPoints = 0;

		_pollutionPoints += points;
	}

	internal void SetMilesPoints(double points)
	{
		_milesPoints = points;
	}

	internal void SetPollutionPoints(double points)
	{
		_pollutionPoints = points;
	}
}