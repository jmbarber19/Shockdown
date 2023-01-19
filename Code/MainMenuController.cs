using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    void Start() {
        SoundManager.I.Spawn_music1();
    }

    public void GotoInfo() {
        SoundManager.I.Spawn_ui2();
        SceneManager.LoadScene("Info");
    }

    public void GotoGame() {
        SoundManager.I.Spawn_ui3();
        SceneManager.LoadScene("Game");
    }

    public void Quit() {
        SoundManager.I.Spawn_ui2();
        Application.Quit();
    }
}
