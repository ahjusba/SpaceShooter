using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour, IDamageable {


    Rigidbody2D rb;

    PlayerHealth playerHealth;

    [SerializeField] float health = 4000f;

    float xVel;
    float yVel;

    public GameObject smallExplosionFX;
    public GameObject bigExplosionFX;

    public GameObject gibsonGuitar;

    public Transform particleContainer;

    bool bossDoomed = false;
    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;


    public float rampTime;
    public float silenceTime;
    public float explosionTime;

    private void Awake() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        
    }

    private void FixedUpdate() {

        xVel = Mathf.Sin(Time.time);
        yVel = 0;

        rb.velocity = new Vector2(xVel, yVel);
    }
    public void ApplyDamage(float damage) {
        health -= damage;
        if (health <= 0 && !bossDoomed) {
            bossDoomed = true;
            StartCoroutine(DestroyBoss());
        }
    }

    IEnumerator DestroyBoss() {

        gun1.SetActive(false);
        gun2.SetActive(false);
        gun3.SetActive(false);

        Instantiate(smallExplosionFX, transform.position, Quaternion.identity, particleContainer);
        transform.Rotate(new Vector3(0, 0, 45f));
        playerHealth.health = 10000;

        AudioFW.Play("BossDead");

        yield return new WaitForSeconds(rampTime);

        yield return new WaitForSeconds(silenceTime);

        SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();
        Destroy(rend.gameObject);
        Instantiate(bigExplosionFX, transform.position, Quaternion.identity, particleContainer);
        Instantiate(gibsonGuitar, transform.position, Quaternion.identity, particleContainer);
        Destroy(gameObject.transform.parent.gameObject);

        yield return new WaitForSeconds(explosionTime);

        
    }
}
