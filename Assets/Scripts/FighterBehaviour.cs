using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterBehaviour : MonoBehaviour, IDamageable {


    Rigidbody2D rb;

    [SerializeField] float health = 200f;

    [SerializeField] float speed = 1f;
    [SerializeField] float bulletSpeed;

    //public GameObject explosionFX;

    PlayerController target;
    Vector2 direction = Vector2.zero;

    float secondsPerShot = 1f;
    float shotDamage = 10f;
    bool hasFired = false;

    public GameObject projectilePreFab;
    public Transform nose;
    public Transform projectileParent;

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
        AudioFW.Play("FighterDead");
        Destroy(this.gameObject);
    }

    private void Awake() {
        secondsPerShot = Random.Range(secondsPerShot - 0.2f, secondsPerShot + 0.2f);
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>();
        RotateTowards(target.transform.position);
        MoveTowards(target.transform.position);
        StartCoroutine(FireCooldown(1.5f));
        Destroy(this.gameObject.transform.parent.gameObject, 15f);
    }

    private void FixedUpdate() {
        RotateTowards(target.transform.position);

        if (Vector3.Distance(rb.position, target.transform.position) > 1f) {
            MoveTowards(target.transform.position);
        }

        if (!hasFired) {
            StartCoroutine(FireCooldown(secondsPerShot));
            AudioFW.Play("FighterGunFire");
            GameObject bullet = Instantiate(projectilePreFab, nose.position, transform.rotation, projectileParent);
            bullet.GetComponent<ProjectileBehaviour>().SetStats(rb.velocity, shotDamage, bulletSpeed, direction, gameObject.transform, "FighterGunHit");
        }
    }

    private void MoveTowards(Vector2 target) {
        rb.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
    }

    public void RotateTowards(Vector2 target) {
        var offset = -90f;
        direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    IEnumerator FireCooldown(float secondsPerShot) {
        hasFired = true;
        yield return new WaitForSeconds(secondsPerShot);
        hasFired = false;
    }
}
