using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable {

    float health = 1f;
    bool emergencyProtocol = false;
    bool isDoomed = false;
    public float timeSlow = 0.2f;

    Rigidbody2D playerRB;

    private void Awake() {
        playerRB = GetComponent<Rigidbody2D>();
    }

    public void ApplyDamage(float damage) {
        health -= damage;
        if(health <= 0 && !isDoomed) {
            emergencyProtocol =  true;
        }        
    }

    private void Update() {
        if (emergencyProtocol) {
            RemovePlayerControls();
            emergencyProtocol = false;
            isDoomed = true;
        }

        if (isDoomed) {
                     
            if (Input.GetKeyDown(KeyCode.Space)) {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
                SceneManager.LoadScene(0);
            }
        }
    }

    private void FixedUpdate() {
        if (isDoomed) {

            playerRB.rotation += 0.1f;
        }
    }

    public void RemovePlayerControls() {
        PlayerInput input = GetComponent<PlayerInput>();
        Destroy(input);

        SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();
        rend.transform.Rotate(new Vector3(0, 0, 45f)); //Tilt the visuals


        Time.timeScale = timeSlow;
        Time.fixedDeltaTime = timeSlow * 0.02f;

        playerRB.drag = 50f;

    }
}
