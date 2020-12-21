using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLayerBehaviour : MonoBehaviour, IDamageable {

    float currentSpeed;
    [SerializeField] float startSpeed = 1f;
    [SerializeField] float startUpTime = 0.5f;
    [SerializeField] float speed = 0.2f;

    [SerializeField] float health = 700f;

    [SerializeField] float secondsPerShot = 4f;
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

        Destroy(this.gameObject);
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(InitialSpeed());
        StartCoroutine(FireCooldown(2f));
    }
    public void FixedUpdate() {
        float xVelocity = Mathf.Sin(Time.time);
        moveSpeed = new Vector2(xVelocity, -currentSpeed);
        rb.velocity = moveSpeed;

        if (!hasFired) {
            StartCoroutine(FireCooldown(secondsPerShot));
            GameObject bullet = Instantiate(projectilePreFab, tail.position, transform.rotation, projectileParent);
            print("Pewpew");
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
