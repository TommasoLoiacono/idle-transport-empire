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
		HappinessPointsText.text = GameManager.Instance.MyScoreManager.HappinessPoints.ToString();
		PollutionPointsText.text = GameManager.Instance.MyScoreManager.HappinessPoints.ToString();
	}
}
