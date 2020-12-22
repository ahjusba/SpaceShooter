using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable {

    public float health;
    bool emergencyProtocol = false;
    bool isDoomed = false;
    public float timeSlow = 0.2f;

    public AudioSource music;
    public float pitchAndVolume;

    public Image spacecontrol;

    Rigidbody2D playerRB;

    private void Awake() {
        health = PlayerPrefs.GetFloat("health", 1);
        spacecontrol.gameObject.SetActive(false);
        playerRB = GetComponent<Rigidbody2D>();
    }

    public void ApplyDamage(float damage) {
        health -= damage;
        print("Took damage");
        if (health <= 0 && !isDoomed) {
            emergencyProtocol = true;
        }
    }

    private void Update() {

        print(health);
        if (emergencyProtocol) {
            spacecontrol.gameObject.SetActive(true);
            RemovePlayerControls();
            emergencyProtocol = false;
            isDoomed = true;
            AudioFW.Play("PlayerDeadAlert");
        }

        if (isDoomed) {
            pitchAndVolume = music.volume;
            pitchAndVolume -= Time.deltaTime * 0.1f;
            music.volume = pitchAndVolume;
            music.pitch = pitchAndVolume - 0.5f;

            if (Input.GetKeyDown(KeyCode.Space)) {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
                SceneManager.LoadScene(2);
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
