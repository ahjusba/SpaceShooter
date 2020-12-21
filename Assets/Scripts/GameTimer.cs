using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    float gameTimer = 0;
    public Text timerText;

    int currentTimeFloored;
    int displaySeconds;
    int displayMinutes;
    string time;

    private void Update() {
        gameTimer += Time.deltaTime;
        UpdateClockVisuals();
    }
    void UpdateClockVisuals() {
        currentTimeFloored = Mathf.FloorToInt(gameTimer);
        displaySeconds = currentTimeFloored % 60;
        displayMinutes = Mathf.FloorToInt(currentTimeFloored / 60);
        if (displaySeconds >= 10) {
            time = displayMinutes.ToString() + ":" + displaySeconds.ToString();
        } else {
            time = displayMinutes.ToString() + ":0" + displaySeconds.ToString();
        }
        timerText.text = time;
    }
}
