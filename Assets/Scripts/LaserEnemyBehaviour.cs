using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemyBehaviour : MonoBehaviour, IDamageable {

    float startUpTime;
    [SerializeField] float speedMax = 0.5f;
    [SerializeField] float speedMin = 0.2f;
    float speed = 2f;

    [SerializeField] float health = 700f;

    [SerializeField] float secondsPerShot = 4f;

    Vector2 moveSpeed;

    public GameObject laserBeamColliders;
    bool hasFired = false;

    public Transform nose;

    LineRenderer beamVisuals;
    float beamRampWidth = 0.2f;
    bool rampUp = false;
    bool rampDown = false;

    float beamTimer = 0;
    float t = 0f;
    float rampUpTime = 2.1f;
    float damageTime = 0.6f;
    float followTime = 0.3f;
    float totalTime;
    public AnimationCurve lineCurve;

    public bool looksRight; //awful stuff.. just to make three variants.
    public bool looksLeft;
    public bool looksDown;
    public bool looksPlayer;

    public GameObject ballSprite;


    Rigidbody2D rb;

    public void ApplyDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            UnitDestroy();
        }
    }

    public void UnitDestroy() {
        Destroy(this.gameObject);
    }

    private void Awake() {

        StartCoroutine(InitialCoolDown());

        if (looksRight) {
            transform.parent.transform.Rotate(0, 0, -90);
        } else if (looksLeft) {
            transform.parent.transform.Rotate(0, 0, 90);
        } else if (looksDown) {
            transform.parent.transform.Rotate(0, 0, 180);
        } else if (looksPlayer) {
            Vector2 target = FindObjectOfType<PlayerController>().transform.position;
            Vector2 direction;

            var offset = -90f;
            direction = target - (Vector2)transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        }
        totalTime = rampUpTime + damageTime + followTime;

        beamVisuals = GetComponent<LineRenderer>();
        beamVisuals.startWidth = 0f;

        rb = GetComponent<Rigidbody2D>();
        laserBeamColliders.SetActive(false);
        Destroy(this.gameObject.transform.parent.gameObject, 7f);
    }

    public void FixedUpdate() {
        float xVelocity = Mathf.Sin(Time.time);
        moveSpeed = new Vector2(xVelocity / 2, -speed);
        rb.velocity = moveSpeed;

        if (!hasFired) {
            StartCoroutine(LaserCooldown());
        }

        if (rampUp) {
            t = Mathf.Lerp(0, beamRampWidth, beamTimer / rampUpTime);
            beamVisuals.startWidth = t;
            beamVisuals.endWidth = t;
            ballSprite.transform.localScale = new Vector3(0.1f * t, 0.1f * t, 0.1f * t);
            beamTimer += Time.fixedDeltaTime;
        } else if (rampDown) {
            t = Mathf.Lerp(1, 0, beamTimer / followTime);
            beamVisuals.startWidth = t;
            beamVisuals.endWidth = t;
            ballSprite.transform.localScale = new Vector3(0.1f * t, 0.1f * t, 0.1f * t);
            beamTimer += Time.fixedDeltaTime;
        }
    }


    IEnumerator LaserCooldown() {
        hasFired = true;
        beamVisuals.startWidth = 0;
        beamVisuals.endWidth = 0;
        rampUp = true;
        AudioFW.Play("LaserGunCharge");
        yield return new WaitForSeconds(rampUpTime);
        laserBeamColliders.SetActive(true);
        rampUp = false;
        AudioFW.Play("LaserGunFire");
        ballSprite.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        beamVisuals.startWidth = 1;
        beamVisuals.endWidth = 1;
        yield return new WaitForSeconds(damageTime);
        beamTimer = 0;
        rampDown = true;
        laserBeamColliders.SetActive(false);
        beamVisuals.startWidth = 0;
        beamVisuals.endWidth = 0;
        yield return new WaitForSeconds(followTime);
        beamTimer = 0;
        rampDown = false;
        hasFired = false;
    }

    IEnumerator InitialCoolDown() {
        hasFired = true;
        yield return new WaitForSeconds(0.5f);
        hasFired = false;
    }


}
