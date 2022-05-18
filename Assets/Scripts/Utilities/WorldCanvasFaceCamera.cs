using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldCanvasFaceCamera : MonoBehaviour
{
	Transform MyCamera;
	Canvas MyCanvas;

	void Awake()
	{
		MyCamera = Camera.main.transform;
	}

	private void Start()
	{
		if (!TryGetComponent(out MyCanvas))
			Debug.LogError("This component doesn't have a canvas: " + this.name);

		MyCanvas.renderMode = RenderMode.WorldSpace;
		MyCanvas.worldCamera = Camera.main;
	}

	void Update() // update works better than LateUpdate, but It should be done in LateUpdate...
	{
		transform.forward = MyCamera.forward;
		//transform.rotation = Quaternion.LookRotation( transform.position - cam.position );
	}
}