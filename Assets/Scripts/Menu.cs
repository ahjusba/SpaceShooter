using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    float health = 0;
    public void Normal() {
        health = 1;
        PlayerPrefs.SetFloat("health", health);
        SceneManager.LoadScene(1);
    }

    public void Invinsible() {
        health = 99999;
        PlayerPrefs.SetFloat("health", health);
        SceneManager.LoadScene(1);
    }
}
