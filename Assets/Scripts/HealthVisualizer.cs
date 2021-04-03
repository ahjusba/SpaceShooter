using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisualizer : MonoBehaviour
{
    PlayerHealth playerHealth;
    float health;
    public Image h1;
    public Image h2;
    public Image h3;

    private void Awake() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        health = playerHealth.health;
    }

    private void Update() {
        health = playerHealth.health;
        if(health > 4) {
            h1.enabled = false;
            h2.enabled = false;
            h3.enabled = false;
        } else if(health == 3) {

        } else if(health == 2) {
            h3.enabled = false;
        } else if(health == 1) {
            h2.enabled = false;
        } else if (health == 0) {
            h1.enabled = false;
        }
    }
}
