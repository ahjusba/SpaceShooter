using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GibsonGameEnd : MonoBehaviour {

    GameEnd gameEnd;
    SpriteRenderer rend;

    private void Awake() {
        gameEnd = FindObjectOfType<GameEnd>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            rend.color = new Color(0, 0, 0, 0);
            gameEnd.GameWin();
        }
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(0);
        }
    }


}
