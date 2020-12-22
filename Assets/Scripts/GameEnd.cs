using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour {
    public GameObject creditObject;
    PlayerInput player;

    private void Awake() {
        creditObject.SetActive(false);
        player = FindObjectOfType<PlayerInput>();
    }

    public void GameWin() {

        creditObject.SetActive(true);
        Destroy(player);
    }
}
