using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreboard : MonoBehaviour
{
	public TextMeshProUGUI MilesPointsText;
	public TextMeshProUGUI PollutionPointsText;

	// Update is called once per frame
	void Update()
	{
		MilesPointsText.text = "<sprite=0>      " + ((int)GameManager.Instance.GetCurrentMilesPoints()).ToString();
		PollutionPointsText.text = "<sprite=0>      " + ((int)GameManager.Instance.GetCurrentPollutionPoints()).ToString();
	}
}
