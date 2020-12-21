using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    float bulletVelocity;
    public GameObject muzzleFlash;
    Transform shooter;

    Rigidbody2D bulletRb;
    float projectileDmg;

    private void Awake() {
        bulletRb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        Instantiate(muzzleFlash, gameObject.transform);
        Destroy(this.gameObject, 5f);
    }

    public void SetStats(Vector2 parentSpeed, float damage, float projectileVelocity, Vector2 direction, Transform _shooter) {
        shooter = _shooter;
        bulletVelocity = projectileVelocity;
        Vector2 bulletSpeed;
        if (direction == Vector2.up) {
            if (parentSpeed.y > 0) {
                bulletSpeed = (direction * bulletVelocity) + new Vector2(0, parentSpeed.y);
            } else {
                bulletSpeed = (direction * bulletVelocity) + new Vector2(0, parentSpeed.y / 3);
            }

        } else {
            bulletSpeed = (direction * bulletVelocity) + parentSpeed;
        }

        bulletRb.velocity = bulletSpeed;

        projectileDmg = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform != shooter) {
            if (collision.GetComponent<IDamageable>() != null) {
                collision.GetComponent<IDamageable>().ApplyDamage(projectileDmg);
                Destroy(this.gameObject);
            }
        }
    }
}
