using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public static ScoreController I;

    [Header("Score Animation")]
    public float speed = 2f;
    public float intensity = 2f;
    public float scalingSpeed = 4f;
    public float scalingIntensity = 0.1f;
    public float colorRestoreSpeed = 2f;
    public Color scoreAddColor = Color.green;
    public Color bounceAddColor = Color.cyan;
    public Color cancelAddColor = Color.magenta;

    [Header("Objects")]
    public TextMeshPro textMesh;

    [Header("Scoring")]
    public float timeAfterDecay = 0.5f;
    public float currentTimeAfterDecay = 0f;
    public List<string> recentAttacks = new List<string>();
    public int recentAttacksMax = 4;
    public AnimationCurve recentDuplicateFalloff;
    public float cancelAdd = 0.1f;
    public float wallBounceAdd = 0.2f;
    public float groundBounceAdd = 0.4f;

    [SerializeField]
    private float maxTime = 12f;

    private float time;
    public float timeSurvived = 0f;

    void Awake()
    {
        time = maxTime;
        for (int i = 0; i < recentAttacksMax; i++) {
            recentAttacks.Add("");
        }
        I = this;
    }

    void Start() {
        SoundManager.I.Spawn_music3();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IntroController.I.IsDonePlaying() || ScoreController.I.IsGameOver()) { return; }

        timeSurvived += Time.deltaTime;
        time -= Time.deltaTime;
        time = Mathf.Clamp(time, 0f, maxTime);
        textMesh.text = time.ToString("F1") + "s";

        if (time > 0f) {
            transform.rotation = Quaternion.AngleAxis(intensity * Mathf.Sin(Time.time * speed), Vector3.back);
            textMesh.transform.localScale = Vector3.one * (0.2f + scalingIntensity * Mathf.Sin(Time.time * scalingSpeed));
            textMesh.color = Color.Lerp(textMesh.color, Color.white, Time.deltaTime * colorRestoreSpeed);
        } else {
            transform.rotation = Quaternion.identity;
            textMesh.transform.localScale = Vector3.one * 0.2f;
            textMesh.color = Color.red;
        }
    }

    public void AddAttack(string attackName, float style) {
        float recentMultiplier = 1f;
        for (int i = 0; i < recentAttacksMax; i++) {
            if (recentAttacks[i] == attackName) {
                recentMultiplier -= recentDuplicateFalloff.Evaluate((float)i / (float)recentAttacksMax);
            }
        }
        recentMultiplier = Mathf.Clamp(recentMultiplier, 0f, 1f);

        recentAttacks.Add(attackName);
        recentAttacks.RemoveAt(0);

        time += style * recentMultiplier;
        textMesh.color = Color.Lerp(Color.gray, scoreAddColor, recentMultiplier);
    }

    public void AddCancel() {
        time += cancelAdd;
        textMesh.color = cancelAddColor;
    }

    public void AddWallBounce() {
        time += wallBounceAdd;
        textMesh.color = bounceAddColor;
    }

    public void AddGroundBounce() {
        time += groundBounceAdd;
        textMesh.color = bounceAddColor;
    }

    public bool IsGameOver() {
        return time <= 0f;
    }
}
