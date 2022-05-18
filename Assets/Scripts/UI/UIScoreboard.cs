using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreboard : MonoBehaviour
{
	public TextMeshProUGUI HappinessPointsText;
	public TextMeshProUGUI PollutionPointsText;

	// Update is called once per frame
	void Update()
	{
		HappinessPointsText.text = "Happiness Points: " + ((int)GameManager.Instance.GetCurrentHappinessPoints()).ToString();
		PollutionPointsText.text = "Pollution Points: " + ((int)GameManager.Instance.GetCurrentPollutionPoints()).ToString();
	}
}
