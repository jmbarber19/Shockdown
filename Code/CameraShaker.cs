using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour {

	public class CameraShakeProcess {
		public CameraShakeProcess(Vector2 pDirection, float pIntensity, float pDeltaTimeMultiplier, float startTime) {
			direction = pDirection;
			intensity = pIntensity;
			deltaTimeMultiplier = pDeltaTimeMultiplier;
			currentTime = startTime;
		}
		public Vector2 direction = Vector2.zero;
		public float currentTime = 0f;
		public float intensity = 0f;
		public float deltaTimeMultiplier = 1f;
	}

	public static CameraShaker instance;

	public AnimationCurve offsetBounce;
	public float maxShake = 2f;
	public float globalShakeMultiplier = 1f;

	private static List<CameraShakeProcess> cameraShakes;

	private void Start() {
		instance = this;
		cameraShakes = new List<CameraShakeProcess>();
	}
	
	void Update () {
		transform.localPosition = Vector3.zero;
		for (int i = cameraShakes.Count - 1; i >= 0; i--) {
			cameraShakes[i].currentTime += Time.deltaTime * cameraShakes[i].deltaTimeMultiplier;
			transform.localPosition += new Vector3(
				cameraShakes[i].direction.x * cameraShakes[i].intensity * offsetBounce.Evaluate(cameraShakes[i].currentTime),
				cameraShakes[i].direction.y * cameraShakes[i].intensity * offsetBounce.Evaluate(cameraShakes[i].currentTime),
				0f
			) * globalShakeMultiplier;
			if (offsetBounce.keys.Length > 0 && cameraShakes[i].currentTime > offsetBounce.keys[offsetBounce.keys.Length - 1].time) {
				cameraShakes.RemoveAt(i);
			}
		}
		if (transform.localPosition.magnitude > maxShake) {
			transform.localPosition = transform.localPosition.normalized * maxShake;
		}
	}

	public static void AddCameraShake(Vector2 direction, float intensity = 1f, float deltaTimeMultiplier = 1f, float startTime = 0f) {
		cameraShakes.Add(new CameraShakeProcess(direction.normalized, intensity, deltaTimeMultiplier, startTime));
	}
}
