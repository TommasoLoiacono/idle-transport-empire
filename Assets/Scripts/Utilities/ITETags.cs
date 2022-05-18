using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITETags : MonoBehaviour
{
	public enum Tag
	{
		FacilityCard,
		Island		
	}

	public static string FacilityCard = nameof(Tag.FacilityCard);
	public static string Island = nameof(Tag.Island);

	public static string[] Tags = new string[]
	{
		FacilityCard,
		Island
	};
}
