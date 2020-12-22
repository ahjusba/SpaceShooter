using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInPicture : MonoBehaviour {

    SpriteRenderer rend;
    Color initialColor;
    public Color startingColor;
    public Color targetColor;

    public Transform target;

    public float fadeTime = 60f;
    float timer = 0f;
    float t; //0...1 lerp value
    public bool fadeStarted = false;

    private void Awake() {
        rend = GetComponent<SpriteRenderer>();
        initialColor = rend.color;
        startingColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
        targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1);
        rend.color = startingColor;
    }

    private void Update() {
        if (transform.position.y < -10) {
            fadeStarted = true;
        }

        if (fadeStarted) {
            t = Mathf.Clamp01(timer / fadeTime);
            print(t);
            
            rend.color = Color.Lerp(startingColor, targetColor, t);
            timer += Time.deltaTime;
        }
    }
}
