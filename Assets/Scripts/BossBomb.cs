using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour {

    [SerializeField] float secondsPerShot = 5f;
    [SerializeField] float bulletSpeed = 3f;
    [SerializeField] float shotDamage = 10f; //This is a mine, which of course does damage, but will later burst into a circle of other projectiles
    [SerializeField] float startUpTime = 2f;


    public GameObject projectilePreFab;
    public bool hasFired = false;

    public Transform tail;
    public Transform projectileParent;

    private void Awake() {
        StartCoroutine(FireCooldown(startUpTime));
    }
    public void FixedUpdate() {

        if (!hasFired) {
            StartCoroutine(FireCooldown(secondsPerShot));
            AudioFW.Play("MineGunFire");
            GameObject bullet = Instantiate(projectilePreFab, transform.position, transform.rotation, projectileParent);
            bullet.GetComponent<MineBehaviour>().SetStats(Vector2.zero, shotDamage, bulletSpeed, Vector2.down, gameObject.transform.parent.transform);
        }
    }

    IEnumerator FireCooldown(float secondsPerShot) {
        hasFired = true;
        yield return new WaitForSeconds(secondsPerShot);
        hasFired = false;
    }

}
