using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroController : MonoBehaviour
{
    public static OutroController I;
    public float finalScore = 0f;

    private float currentTimeTillSceneChange;
    private float timeTillSceneChange = 1f;
    private bool didLoad = false;

    void Awake() {
        currentTimeTillSceneChange = timeTillSceneChange;
        if (I != null) {
            Destroy(this.gameObject);
        } else {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update() {
        if (ScoreController.I != null && !ScoreController.I.IsGameOver()) {
            didLoad = false;
            finalScore = 0f;
            currentTimeTillSceneChange = timeTillSceneChange;
            return;
        }

        finalScore = ScoreController.I.timeSurvived;

        if (currentTimeTillSceneChange > 0f) {
            currentTimeTillSceneChange -= Time.deltaTime;
        } else {
            if (!didLoad) {
                SceneManager.LoadScene("FinalScore");
                didLoad = true;
            }
        }
    }
}
