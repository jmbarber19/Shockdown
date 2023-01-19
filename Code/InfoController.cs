using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoController : MonoBehaviour
{
    public RectTransform screen1;
    public RectTransform screen2;
    public RectTransform screen3;
    public RectTransform screen4;

    private Vector3 screen1goal;
    private Vector3 screen2goal;
    private Vector3 screen3goal;
    private Vector3 screen4goal;

    public float changeSpeed = 3f;

    void Awake() {
        GotoScreen1(false);
    }

    void Start() {
        SoundManager.I.Spawn_music2();
    }

    public void GotoScreen1(bool playSound = true) {
        if (playSound) {
            SoundManager.I.Spawn_ui2();
        }
        screen1goal = new Vector3(0, 0, 0);
        screen2goal = new Vector3(700, 0, 0);
        screen3goal = new Vector3(700, 0, 0);
        screen4goal = new Vector3(700, 0, 0);
    }

    public void GotoScreen2() {
        SoundManager.I.Spawn_ui2();
        screen1goal = new Vector3(-700, 0, 0);
        screen2goal = new Vector3(0, 0, 0);
        screen3goal = new Vector3(700, 0, 0);
        screen4goal = new Vector3(700, 0, 0);
    }

    public void GotoScreen3() {
        SoundManager.I.Spawn_ui2();
        screen1goal = new Vector3(-700, 0, 0);
        screen2goal = new Vector3(-700, 0, 0);
        screen3goal = new Vector3(0, 0, 0);
        screen4goal = new Vector3(700, 0, 0);
    }

    public void GotoScreen4() {
        SoundManager.I.Spawn_ui2();
        screen1goal = new Vector3(-700, 0, 0);
        screen2goal = new Vector3(-700, 0, 0);
        screen3goal = new Vector3(-700, 0, 0);
        screen4goal = new Vector3(0, 0, 0);
    }

    void Update() {
        screen1.localPosition = Vector3.Lerp(screen1.localPosition, screen1goal, Time.deltaTime * changeSpeed);
        screen2.localPosition = Vector3.Lerp(screen2.localPosition, screen2goal, Time.deltaTime * changeSpeed);
        screen3.localPosition = Vector3.Lerp(screen3.localPosition, screen3goal, Time.deltaTime * changeSpeed);
        screen4.localPosition = Vector3.Lerp(screen4.localPosition, screen4goal, Time.deltaTime * changeSpeed);
    }

    public void BackToMenu() {
        SoundManager.I.Spawn_ui2();
        SceneManager.LoadScene("Menu");
    }
}
