using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public static IntroController I;
    public IntroNumber number3;
    public IntroNumber number2;
    public IntroNumber number1;
    public IntroNumber g;
    public IntroNumber o;

    public float counter = 5f;

    void Awake() {
        I = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > -0.5f) { counter -= Time.deltaTime; }

        if (counter <= 4f) { number3.StartMoving(); }
        if (counter <= 2.9f) { number3.Fling(); }
        if (counter <= 3f) { number2.StartMoving(); }
        if (counter <= 1.9f) { number2.Fling(); }
        if (counter <= 2f) { number1.StartMoving(); }
        if (counter <= 0.9f) { number1.Fling(); }
        if (counter <= 1f) { g.StartMoving(); o.StartMoving(); }
        if (counter <= 0.1f) { g.Fling(); o.Fling(); }
    }

    public bool IsDonePlaying() {
        return counter <= 0f;
    }
}
