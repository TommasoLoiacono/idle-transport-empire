using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIScoreboard : MonoBehaviour
{
	public TextMeshProUGUI MilesPointsText;
	public TextMeshProUGUI PollutionPointsText;

	// Update is called once per frame
	void Update()
	{
		MilesPointsText.text = "<sprite=0>      " + String.Format("{0:0.00}", ((float)GameManager.Instance.GetCurrentMilesPoints()));
		PollutionPointsText.text = "<sprite=0>      " + String.Format("{0:0.00}", ((float)GameManager.Instance.GetCurrentPollutionPoints()));
	}
}
