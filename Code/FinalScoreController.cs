using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreMesh;

    void Start() {
        SoundManager.I.Spawn_music4();
    }

    // Update is called once per frame
    void Update()
    {
        if (OutroController.I != null) {
            scoreMesh.text = OutroController.I.finalScore.ToString("F2") + " Seconds";
        }
    }
    
    public void BackToMenu() {
        SoundManager.I.Spawn_ui2();
        SceneManager.LoadScene("Menu");
    }
    
    public void PlayAgain() {
        SoundManager.I.Spawn_ui3();
        SceneManager.LoadScene("Game");
    }
}
