using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFreezer : MonoBehaviour {
    [SerializeField]
    private float displayTimeScale = 1f;

    public float restoreSpeed = 4f;
    private static float freezeTime = 0f;

    void Update() {
        if (freezeTime > 0f) {
            freezeTime -= Time.unscaledDeltaTime;
        }
        if (freezeTime <= 0f) {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, restoreSpeed * Time.unscaledDeltaTime);
        }
        displayTimeScale = Time.timeScale;
    }

    public static void Freeze(float scale = 0.3f) {
        Time.timeScale = scale;
        freezeTime = Mathf.Max(0.1f, freezeTime);
    }
}
