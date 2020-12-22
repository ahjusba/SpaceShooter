using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLayerBehaviour : MonoBehaviour, IDamageable {

    float currentSpeed;
    [SerializeField] float startSpeed = 1f;
    float startUpTime;
    [SerializeField] float speed = 0.2f;

    [SerializeField] float health = 700f;

    [SerializeField] float secondsPerShot;
    [SerializeField] float bulletSpeed = 3f;
    [SerializeField] float shotDamage = 10f; //This is a mine, which of course does damage, but will later burst into a circle of other projectiles

    Vector2 moveSpeed;

    public GameObject projectilePreFab;
    bool hasFired = false;

    public Transform tail;
    public Transform projectileParent;

    Rigidbody2D rb;

    public void ApplyDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            UnitDestroy();
        }
    }

    public void UnitDestroy() {
        //Spawn seperate particleFX
        //Instantiate(explosionFX, transform.position, Quaternion.identity);
        //Explosion sound   

        AudioFW.Play("MineDead");
        Destroy(this.gameObject);
    }

    private void Awake() {
        secondsPerShot = Random.Range(2.5f, 5f);
        startUpTime = Random.Range(0.2f, 1.4f);
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(InitialSpeed());
        StartCoroutine(FireCooldown(2f));
        Destroy(this.gameObject.transform.parent.gameObject, 20f);
    }
    public void FixedUpdate() {
        float xVelocity = Mathf.Sin(Time.time);
        moveSpeed = new Vector2(xVelocity, -currentSpeed);
        rb.velocity = moveSpeed;

        if (!hasFired) {
            StartCoroutine(FireCooldown(secondsPerShot));
            AudioFW.Play("MineGunFire");
            GameObject bullet = Instantiate(projectilePreFab, tail.position, transform.rotation, projectileParent);
            bullet.GetComponent<MineBehaviour>().SetStats(rb.velocity, shotDamage, bulletSpeed, Vector2.down, gameObject.transform);
        }
    }

    IEnumerator InitialSpeed() {
        currentSpeed = startSpeed;
        yield return new WaitForSeconds(startUpTime);
        currentSpeed = speed;
    }
    IEnumerator FireCooldown(float secondsPerShot) {
        hasFired = true;
        yield return new WaitForSeconds(secondsPerShot);
        hasFired = false;
    }


}
