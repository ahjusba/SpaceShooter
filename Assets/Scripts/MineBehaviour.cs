using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour {
    float bulletVelocity;
    //public GameObject muzzleFlash;
    Transform shooter;

    Rigidbody2D bulletRb;
    float projectileDmg;

    float explodeTimer = 5f;
    float timer = 0f;
    float t = 0f;

    public Color startingColor;
    public Color endColor;

    public AnimationCurve curve;

    SpriteRenderer rend;

    [SerializeField] float shotDamage = 10f;
    [SerializeField] float bulletSpeed = 3f;

    public GameObject projectilePrefab;
    [SerializeField] float spawnRadius = 0.1f;
    [SerializeField] int spawnCount = 8;

    private void Awake() {
        bulletRb = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        //Instantiate(muzzleFlash, gameObject.transform.parent);
        StartCoroutine(Explode(explodeTimer));
    }

    IEnumerator Explode(float timer) {
        yield return new WaitForSeconds(timer);
        InstantiateCircle();
        Destroy(this.gameObject);
    }

    public void InstantiateCircle() {
        float angle = 360f / (float)spawnCount;
        float randomAngle = Random.Range(0, 90f);
        for (int i = 0; i < spawnCount; i++) {
            Quaternion rotation = Quaternion.AngleAxis(i * angle + randomAngle, Vector3.forward);
            Vector3 direction = rotation * Vector3.up;

            Vector3 position = transform.position + (direction * spawnRadius);
            GameObject bullet = Instantiate(projectilePrefab, position, rotation);
            bullet.GetComponent<ProjectileBehaviour>().SetStats(bulletRb.velocity, shotDamage, bulletSpeed, direction, gameObject.transform);
        }
    }

    public void FadeColorAndScale() {        
        t = curve.Evaluate(timer / explodeTimer);
        rend.material.color = Color.Lerp(startingColor, endColor, t);
        if(t > 0.85f) {
            rend.transform.localScale = new Vector3(transform.localScale.x + 0.008f, transform.localScale.y + 0.008f, transform.localScale.z + 0.008f);
        }
        if(t < 1) {
            timer += Time.deltaTime;
        }
    }

    private void Update() {
        FadeColorAndScale(); //Just visualization stff
    }

    public void SetStats(Vector2 parentSpeed, float damage, float projectileVelocity, Vector2 direction, Transform _shooter) {
        shooter = _shooter;
        bulletVelocity = projectileVelocity;
        Vector2 bulletSpeed;
        
        bulletSpeed = new Vector2(0, -projectileVelocity);
        

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
