using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
	public int HappinessPoints { get => _happinessPoints; }
	public int PollutionPoints { get => _pollutionPoints; }

	private int _happinessPoints;
	private int _pollutionPoints;

	public void AddHappinessPoints(int points)
	{
		_happinessPoints += points;
	}

	public void AddPollutionPoints(int points)
	{
		_pollutionPoints = points;
	}
}