using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class UsurperBall : MonoBehaviour
{
	public MeshRenderer UsurperBallRenderer;
	public bool IsUsurperAlive { get => _isUsurperAlive; }

	private bool _isUsurperAlive = true;
	private Collider _collider;

	private void Start()
	{
		UsurperBallRenderer = GetComponentInChildren<MeshRenderer>();
		_collider = GetComponent<Collider>();
		_collider.enabled = false;
	}

	private void OnMouseDown()
	{
		if (IsUsurperAlive)
			KillUsurper();
	}

	public void MakeUsurperKillable()
	{
		_collider.enabled = true;
	}

	public void KillUsurper()
	{
		_isUsurperAlive = false;
		UsurperBallRenderer.enabled = false;
	}
}
