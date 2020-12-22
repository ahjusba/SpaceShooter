using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour, IDamageable {

    Rigidbody2D rbRock;

    float rockVelocityY = 2.5f;
    public float acceleration = 0.05f; //If the Y-velocity is not correct, it will accelerate towards it with this speed per fixed update
    public float health = 1000f;

    float adjustedXVel;

    public bool spawnRocksOnDestroy = true;
    public GameObject smallerRock;
    public int spawnAmount;
    public float explosionForce = 5f;
    public float randomRotation;

    public GameObject explosionFX;

    public List<Sprite> spriteVariations;
    Sprite thisSprite;
    SpriteRenderer rend;

    float rockVelocityX;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<IDamageable>().ApplyDamage(10f); //arbitrary 10f, as long as it's over playerhealth
        }
    }

    private void Awake() {
        rend = GetComponentInChildren<SpriteRenderer>();
        thisSprite = spriteVariations[Random.Range(0, spriteVariations.Count-1)];
        rend.sprite = thisSprite;
        rbRock = GetComponent<Rigidbody2D>();
        rockVelocityX = Mathf.Sin(Random.Range(-6, 6));
        rbRock.velocity = new Vector2(rockVelocityX, -rockVelocityY);
        rbRock.rotation = Random.Range(0, 180);
        randomRotation = Random.Range(-0.5f, 0.5f);
        Destroy(this.gameObject, 15f);
    }

    private void FixedUpdate() {
        rbRock.rotation += randomRotation;

        if(Mathf.Abs(rbRock.velocity.x) > 1) {
            adjustedXVel = rbRock.velocity.x * 0.8f;
        } else {
            adjustedXVel = rbRock.velocity.x;
        }

        if(rbRock.velocity.y > rockVelocityY) {
            rbRock.velocity = new Vector2(adjustedXVel, rbRock.velocity.y - acceleration);
        }
    }

    public void ApplyDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            UnitDestroy();
        }
    }

    public void UnitDestroy() {
        //Spawn seperate particleFX
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        //Explosion sound
        List<Rigidbody2D> rbs = new List<Rigidbody2D>();
        if (spawnRocksOnDestroy) {
            for(int i = 0; i < spawnAmount; i++) {
                GameObject go = Instantiate(smallerRock, transform.position + Vector3.right * Random.Range(-0.5f,0.5f) + Vector3.up * Random.Range(-0.5f,0.5f), Quaternion.identity);
                rbs.Add(go.GetComponentInChildren<Rigidbody2D>());
            }

            foreach(Rigidbody2D rb in rbs) {
                Vector2 direction = new Vector2(rb.position.x - rbRock.position.x, rb.position.y - rbRock.position.y);
                direction.Normalize();
                rb.AddForce(direction * explosionForce, ForceMode2D.Force);
            }
        }

        AudioFW.Play("RockDead");
        AudioFW.Play("RockShatter");

        Destroy(this.gameObject);
    }
}
