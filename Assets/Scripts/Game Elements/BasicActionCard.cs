using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicActionCard : MonoBehaviour
{
	public void GoForAWalk()
	{
		GameManager.Instance.GainMilesPoints(0.01f);
	}
}
