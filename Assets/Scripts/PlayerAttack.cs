using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] int rocketsPerClip = 5;
    [SerializeField] int bulletsPerClip = 99999;

    [SerializeField] float rocketDmg = 1000;
    [SerializeField] float bulletDmg = 50;

    [SerializeField] float secPerRocket = 1f;           //Dps = 1000
    [SerializeField] float secPerBullet = 0.06f;        //Dps = 750

    [SerializeField] float clipReloadTime = 2f;         //Per clip
    [SerializeField] float rocketReloadTime = 1.5f;     //Per one rocket

    [SerializeField] float rocketSpeed = 2f;
    [SerializeField] float bulletSpeed = 1.5f;

    float rocketTimer = 0;

    private int rocketsInClip;
    private int bulletsInClip;

    public GameObject rocketPreFab;
    public GameObject bulletPreFab;

    GameObject newBullet; //Used f or calling methods inside the bullet prefab
    GameObject newRocket; //Same

    public Transform rightWingPos;
    public Transform leftWingPos;
    Transform currentWing;
    public Transform nosePos;

    Rigidbody2D playerRb; //Used to give the projectiles speed based on player speed



    bool isShootingGun = false;
    bool isShootingRocket = false;
    bool gunReloadInProgress = false;

    private void Awake() {
        playerRb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        currentWing = rightWingPos;
        rocketsInClip = rocketsPerClip;
        bulletsInClip = bulletsPerClip;
    }

    private void Update() {
        if (rocketsInClip < rocketsPerClip) {
            rocketTimer += Time.deltaTime;
            if (rocketTimer >= rocketReloadTime) {
                rocketsInClip++;
                rocketTimer -= rocketReloadTime; //Sets to around zero, depending on how long the last update was. Correct way to do this.
            }
        }
    }

    public void Attack(bool machineGunActive, bool rocketsActive, bool reloadActive) {
        if (machineGunActive && !gunReloadInProgress) {
            ShootGun();
        } else if (reloadActive && bulletsInClip < bulletsPerClip) {
            ReloadGun();
        }

        if (rocketsActive) {
            ShootRockets();
        }
    }

    public void ShootGun() {
        if (bulletsInClip > 0) {
            if (!isShootingGun) {
                newBullet = Instantiate(bulletPreFab, nosePos.position, Quaternion.identity, nosePos);
                AudioFW.Play("PlayerGunFire");
                newBullet.GetComponent<ProjectileBehaviour>().SetStats(playerRb.velocity, bulletDmg, bulletSpeed, Vector2.up, gameObject.transform, "PlayerGunHit");
                bulletsInClip--;
                StartCoroutine(GunCooldown(secPerBullet));
            }
        } else {
            ReloadGun();
        }
    }

    public void ShootRockets() {
        if (rocketsInClip > 0) {
            if (!isShootingRocket) {
                AudioFW.Play("PlayerRocketFire");
                newRocket = Instantiate(rocketPreFab, currentWing.position, Quaternion.identity, currentWing);
                newRocket.GetComponent<ProjectileBehaviour>().SetStats(playerRb.velocity, rocketDmg, rocketSpeed, Vector2.up, gameObject.transform, "PlayerRocketHit");
                rocketsInClip--;
                if (currentWing == rightWingPos) {
                    currentWing = leftWingPos;
                } else {
                    currentWing = rightWingPos;
                }
                StartCoroutine(RocketCooldown(secPerRocket));
            }
        }
    }

    public void ReloadGun() {
        if (!gunReloadInProgress) {
            StartCoroutine(ReloadCooldown(clipReloadTime));
        }
    }

    IEnumerator GunCooldown(float time) {
        isShootingGun = true;
        yield return new WaitForSeconds(time);
        isShootingGun = false;
    }

    IEnumerator RocketCooldown(float time) {
        isShootingRocket = true;
        yield return new WaitForSeconds(time);
        isShootingRocket = false;
    }

    IEnumerator ReloadCooldown(float reloadTime) {
        //Sound fx
        gunReloadInProgress = true;
        yield return new WaitForSeconds(reloadTime);
        bulletsInClip = bulletsPerClip;
        gunReloadInProgress = false;
    }



}
