using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserGun : MonoBehaviour {

    
    [SerializeField] float bulletSpeed = 4f;

    PlayerController target;
    Vector2 direction = Vector2.zero;

    public float targetOffset = 0;
    public float secondsPerShot = 3f;
    float shotDamage = 10f;
    bool hasFired = false;

    public GameObject projectilePreFab;
    public Transform projectileParent;

    private void Awake() {

        target = FindObjectOfType<PlayerController>();
        RotateTowards(target.transform.position);
        StartCoroutine(FireCooldown(1.5f));
    }

    private void FixedUpdate() {
        RotateTowards(target.transform.position);
        print(transform.position);

        if (!hasFired) {
            StartCoroutine(FireCooldown(secondsPerShot));
            AudioFW.Play("FighterGunFire");
            GameObject bullet = Instantiate(projectilePreFab, transform.position, transform.rotation, projectileParent);
            bullet.GetComponent<ProjectileBehaviour>().SetStats(Vector2.zero, shotDamage, bulletSpeed, direction, gameObject.transform.parent.transform, "FighterGunHit");

        }
    }

    public void RotateTowards(Vector2 target) {
        var offset = -90f;
        direction = target - (Vector2)transform.position + new Vector2(targetOffset,Mathf.Abs(targetOffset));
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
