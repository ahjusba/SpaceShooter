using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    float bulletVelocity;
    public GameObject muzzleFlash;
    public GameObject hitFlash;
    Transform shooter;

    Rigidbody2D bulletRb;
    float projectileDmg;
    Transform particleContainer;

    string explodeSound;

    private void Awake() {
        particleContainer = GameObject.Find("ParticleContainer").transform;
        bulletRb = GetComponent<Rigidbody2D>();
        explodeSound = "PlayerGunHit";
    }

    private void Start() {

        Instantiate(muzzleFlash, transform.position, Quaternion.Euler(muzzleFlash.transform.rotation.x,0,0), particleContainer);           
        
        Destroy(this.gameObject, 5f);
    }

    public void SetStats(Vector2 parentSpeed, float damage, float projectileVelocity, Vector2 direction, Transform _shooter, string _explodeSound) {
        shooter = _shooter;
        explodeSound = _explodeSound;
        bulletVelocity = projectileVelocity;
        Vector2 bulletSpeed;
        if (direction == Vector2.up) {
            if (parentSpeed.y > 0) {
                bulletSpeed = (direction * bulletVelocity) + new Vector2(0, parentSpeed.y / 2);
            } else {
                bulletSpeed = (direction * bulletVelocity) + new Vector2(0, parentSpeed.y / 6);
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
                Instantiate(hitFlash, transform.position, Quaternion.identity);
                AudioFW.Play(explodeSound);
                Destroy(this.gameObject);
            }
        }
    }
}
