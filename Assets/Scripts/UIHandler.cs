using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    float fadeTimeStamp = 15f;
    float fadeTime = 2f;
    float t = 0;


    public Image keycontrols;
    public Image arrowcontrols;

    float timer = 0;

    Color startingColor = Color.white;
    Color endColor = Color.clear;


    private void Update() {



        if (timer > fadeTimeStamp && t < 1) {

            t = (Time.time - fadeTimeStamp) / fadeTime;

            keycontrols.color = Color.Lerp(startingColor, endColor, t);
            arrowcontrols.color = Color.Lerp(startingColor, endColor, t);

        }

        timer += Time.deltaTime;

    }
}
